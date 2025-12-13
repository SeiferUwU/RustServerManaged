using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Ionic.BZip2;
using Ionic.Crc;
using Ionic.Zip;
using Ionic.Zlib;
using Microsoft.CSharp;

[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Guid("918818b1-7141-49b3-bbdf-858588ad19bc")]
[assembly: CLSCompliant(true)]
[assembly: CompilationRelaxations(8)]
[assembly: AssemblyCompany("Dino Chiesa")]
[assembly: AssemblyProduct("DotNetZip Library")]
[assembly: AssemblyCopyright("Copyright © Dino Chiesa 2006 - 2011")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyFileVersion("1.9.1.8")]
[assembly: AllowPartiallyTrustedCallers]
[assembly: AssemblyTitle("Ionic's Zip Library (Reduced)")]
[assembly: AssemblyConfiguration("Retail")]
[assembly: AssemblyDescription("a library for handling zip archives. http://www.codeplex.com/DotNetZip.  This is a reduced version; it lacks SFX support. (Flavor=Retail)")]
[assembly: ComVisible(true)]
[assembly: AssemblyVersion("1.9.1.8")]
namespace Ionic.Zip
{
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000F")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class ComHelper
	{
		public bool IsZipFile(string filename)
		{
			return ZipFile.IsZipFile(filename);
		}

		public bool IsZipFileWithExtract(string filename)
		{
			return ZipFile.IsZipFile(filename, testExtract: true);
		}

		public bool CheckZip(string filename)
		{
			return ZipFile.CheckZip(filename);
		}

		public bool CheckZipPassword(string filename, string password)
		{
			return ZipFile.CheckZipPassword(filename, password);
		}

		public void FixZipDirectory(string filename)
		{
			ZipFile.FixZipDirectory(filename);
		}

		public string GetZipLibraryVersion()
		{
			return ZipFile.LibraryVersion.ToString();
		}
	}
	public enum EncryptionAlgorithm
	{
		None,
		PkzipWeak,
		WinZipAes128,
		WinZipAes256,
		Unsupported
	}
	public delegate void WriteDelegate(string entryName, Stream stream);
	public delegate Stream OpenDelegate(string entryName);
	public delegate void CloseDelegate(string entryName, Stream stream);
	public delegate CompressionLevel SetCompressionCallback(string localFileName, string fileNameInArchive);
	public enum ZipProgressEventType
	{
		Adding_Started,
		Adding_AfterAddEntry,
		Adding_Completed,
		Reading_Started,
		Reading_BeforeReadEntry,
		Reading_AfterReadEntry,
		Reading_Completed,
		Reading_ArchiveBytesRead,
		Saving_Started,
		Saving_BeforeWriteEntry,
		Saving_AfterWriteEntry,
		Saving_Completed,
		Saving_AfterSaveTempArchive,
		Saving_BeforeRenameTempArchive,
		Saving_AfterRenameTempArchive,
		Saving_AfterCompileSelfExtractor,
		Saving_EntryBytesRead,
		Extracting_BeforeExtractEntry,
		Extracting_AfterExtractEntry,
		Extracting_ExtractEntryWouldOverwrite,
		Extracting_EntryBytesWritten,
		Extracting_BeforeExtractAll,
		Extracting_AfterExtractAll,
		Error_Saving
	}
	public class ZipProgressEventArgs : EventArgs
	{
		private int _entriesTotal;

		private bool _cancel;

		private ZipEntry _latestEntry;

		private ZipProgressEventType _flavor;

		private string _archiveName;

		private long _bytesTransferred;

		private long _totalBytesToTransfer;

		public int EntriesTotal
		{
			get
			{
				return _entriesTotal;
			}
			set
			{
				_entriesTotal = value;
			}
		}

		public ZipEntry CurrentEntry
		{
			get
			{
				return _latestEntry;
			}
			set
			{
				_latestEntry = value;
			}
		}

		public bool Cancel
		{
			get
			{
				return _cancel;
			}
			set
			{
				_cancel = _cancel || value;
			}
		}

		public ZipProgressEventType EventType
		{
			get
			{
				return _flavor;
			}
			set
			{
				_flavor = value;
			}
		}

		public string ArchiveName
		{
			get
			{
				return _archiveName;
			}
			set
			{
				_archiveName = value;
			}
		}

		public long BytesTransferred
		{
			get
			{
				return _bytesTransferred;
			}
			set
			{
				_bytesTransferred = value;
			}
		}

		public long TotalBytesToTransfer
		{
			get
			{
				return _totalBytesToTransfer;
			}
			set
			{
				_totalBytesToTransfer = value;
			}
		}

		internal ZipProgressEventArgs()
		{
		}

		internal ZipProgressEventArgs(string archiveName, ZipProgressEventType flavor)
		{
			_archiveName = archiveName;
			_flavor = flavor;
		}
	}
	public class ReadProgressEventArgs : ZipProgressEventArgs
	{
		internal ReadProgressEventArgs()
		{
		}

		private ReadProgressEventArgs(string archiveName, ZipProgressEventType flavor)
			: base(archiveName, flavor)
		{
		}

		internal static ReadProgressEventArgs Before(string archiveName, int entriesTotal)
		{
			ReadProgressEventArgs e = new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_BeforeReadEntry);
			e.EntriesTotal = entriesTotal;
			return e;
		}

		internal static ReadProgressEventArgs After(string archiveName, ZipEntry entry, int entriesTotal)
		{
			ReadProgressEventArgs e = new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_AfterReadEntry);
			e.EntriesTotal = entriesTotal;
			e.CurrentEntry = entry;
			return e;
		}

		internal static ReadProgressEventArgs Started(string archiveName)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_Started);
		}

		internal static ReadProgressEventArgs ByteUpdate(string archiveName, ZipEntry entry, long bytesXferred, long totalBytes)
		{
			ReadProgressEventArgs e = new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_ArchiveBytesRead);
			e.CurrentEntry = entry;
			e.BytesTransferred = bytesXferred;
			e.TotalBytesToTransfer = totalBytes;
			return e;
		}

		internal static ReadProgressEventArgs Completed(string archiveName)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_Completed);
		}
	}
	public class AddProgressEventArgs : ZipProgressEventArgs
	{
		internal AddProgressEventArgs()
		{
		}

		private AddProgressEventArgs(string archiveName, ZipProgressEventType flavor)
			: base(archiveName, flavor)
		{
		}

		internal static AddProgressEventArgs AfterEntry(string archiveName, ZipEntry entry, int entriesTotal)
		{
			AddProgressEventArgs e = new AddProgressEventArgs(archiveName, ZipProgressEventType.Adding_AfterAddEntry);
			e.EntriesTotal = entriesTotal;
			e.CurrentEntry = entry;
			return e;
		}

		internal static AddProgressEventArgs Started(string archiveName)
		{
			return new AddProgressEventArgs(archiveName, ZipProgressEventType.Adding_Started);
		}

		internal static AddProgressEventArgs Completed(string archiveName)
		{
			return new AddProgressEventArgs(archiveName, ZipProgressEventType.Adding_Completed);
		}
	}
	public class SaveProgressEventArgs : ZipProgressEventArgs
	{
		private int _entriesSaved;

		public int EntriesSaved => _entriesSaved;

		internal SaveProgressEventArgs(string archiveName, bool before, int entriesTotal, int entriesSaved, ZipEntry entry)
			: base(archiveName, before ? ZipProgressEventType.Saving_BeforeWriteEntry : ZipProgressEventType.Saving_AfterWriteEntry)
		{
			base.EntriesTotal = entriesTotal;
			base.CurrentEntry = entry;
			_entriesSaved = entriesSaved;
		}

		internal SaveProgressEventArgs()
		{
		}

		internal SaveProgressEventArgs(string archiveName, ZipProgressEventType flavor)
			: base(archiveName, flavor)
		{
		}

		internal static SaveProgressEventArgs ByteUpdate(string archiveName, ZipEntry entry, long bytesXferred, long totalBytes)
		{
			SaveProgressEventArgs e = new SaveProgressEventArgs(archiveName, ZipProgressEventType.Saving_EntryBytesRead);
			e.ArchiveName = archiveName;
			e.CurrentEntry = entry;
			e.BytesTransferred = bytesXferred;
			e.TotalBytesToTransfer = totalBytes;
			return e;
		}

		internal static SaveProgressEventArgs Started(string archiveName)
		{
			return new SaveProgressEventArgs(archiveName, ZipProgressEventType.Saving_Started);
		}

		internal static SaveProgressEventArgs Completed(string archiveName)
		{
			return new SaveProgressEventArgs(archiveName, ZipProgressEventType.Saving_Completed);
		}
	}
	public class ExtractProgressEventArgs : ZipProgressEventArgs
	{
		private int _entriesExtracted;

		private string _target;

		public int EntriesExtracted => _entriesExtracted;

		public string ExtractLocation => _target;

		internal ExtractProgressEventArgs(string archiveName, bool before, int entriesTotal, int entriesExtracted, ZipEntry entry, string extractLocation)
			: base(archiveName, before ? ZipProgressEventType.Extracting_BeforeExtractEntry : ZipProgressEventType.Extracting_AfterExtractEntry)
		{
			base.EntriesTotal = entriesTotal;
			base.CurrentEntry = entry;
			_entriesExtracted = entriesExtracted;
			_target = extractLocation;
		}

		internal ExtractProgressEventArgs(string archiveName, ZipProgressEventType flavor)
			: base(archiveName, flavor)
		{
		}

		internal ExtractProgressEventArgs()
		{
		}

		internal static ExtractProgressEventArgs BeforeExtractEntry(string archiveName, ZipEntry entry, string extractLocation)
		{
			ExtractProgressEventArgs e = new ExtractProgressEventArgs();
			e.ArchiveName = archiveName;
			e.EventType = ZipProgressEventType.Extracting_BeforeExtractEntry;
			e.CurrentEntry = entry;
			e._target = extractLocation;
			return e;
		}

		internal static ExtractProgressEventArgs ExtractExisting(string archiveName, ZipEntry entry, string extractLocation)
		{
			ExtractProgressEventArgs e = new ExtractProgressEventArgs();
			e.ArchiveName = archiveName;
			e.EventType = ZipProgressEventType.Extracting_ExtractEntryWouldOverwrite;
			e.CurrentEntry = entry;
			e._target = extractLocation;
			return e;
		}

		internal static ExtractProgressEventArgs AfterExtractEntry(string archiveName, ZipEntry entry, string extractLocation)
		{
			ExtractProgressEventArgs e = new ExtractProgressEventArgs();
			e.ArchiveName = archiveName;
			e.EventType = ZipProgressEventType.Extracting_AfterExtractEntry;
			e.CurrentEntry = entry;
			e._target = extractLocation;
			return e;
		}

		internal static ExtractProgressEventArgs ExtractAllStarted(string archiveName, string extractLocation)
		{
			ExtractProgressEventArgs e = new ExtractProgressEventArgs(archiveName, ZipProgressEventType.Extracting_BeforeExtractAll);
			e._target = extractLocation;
			return e;
		}

		internal static ExtractProgressEventArgs ExtractAllCompleted(string archiveName, string extractLocation)
		{
			ExtractProgressEventArgs e = new ExtractProgressEventArgs(archiveName, ZipProgressEventType.Extracting_AfterExtractAll);
			e._target = extractLocation;
			return e;
		}

		internal static ExtractProgressEventArgs ByteUpdate(string archiveName, ZipEntry entry, long bytesWritten, long totalBytes)
		{
			ExtractProgressEventArgs e = new ExtractProgressEventArgs(archiveName, ZipProgressEventType.Extracting_EntryBytesWritten);
			e.ArchiveName = archiveName;
			e.CurrentEntry = entry;
			e.BytesTransferred = bytesWritten;
			e.TotalBytesToTransfer = totalBytes;
			return e;
		}
	}
	public class ZipErrorEventArgs : ZipProgressEventArgs
	{
		private Exception _exc;

		public Exception Exception => _exc;

		public string FileName => base.CurrentEntry.LocalFileName;

		private ZipErrorEventArgs()
		{
		}

		internal static ZipErrorEventArgs Saving(string archiveName, ZipEntry entry, Exception exception)
		{
			ZipErrorEventArgs e = new ZipErrorEventArgs();
			e.EventType = ZipProgressEventType.Error_Saving;
			e.ArchiveName = archiveName;
			e.CurrentEntry = entry;
			e._exc = exception;
			return e;
		}
	}
	[Serializable]
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00006")]
	public class ZipException : Exception
	{
		public ZipException()
		{
		}

		public ZipException(string message)
			: base(message)
		{
		}

		public ZipException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected ZipException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	[Serializable]
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000B")]
	public class BadPasswordException : ZipException
	{
		public BadPasswordException()
		{
		}

		public BadPasswordException(string message)
			: base(message)
		{
		}

		public BadPasswordException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected BadPasswordException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	[Serializable]
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000A")]
	public class BadReadException : ZipException
	{
		public BadReadException()
		{
		}

		public BadReadException(string message)
			: base(message)
		{
		}

		public BadReadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected BadReadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	[Serializable]
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00009")]
	public class BadCrcException : ZipException
	{
		public BadCrcException()
		{
		}

		public BadCrcException(string message)
			: base(message)
		{
		}

		protected BadCrcException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	[Serializable]
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00008")]
	public class SfxGenerationException : ZipException
	{
		public SfxGenerationException()
		{
		}

		public SfxGenerationException(string message)
			: base(message)
		{
		}

		protected SfxGenerationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	[Serializable]
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00007")]
	public class BadStateException : ZipException
	{
		public BadStateException()
		{
		}

		public BadStateException(string message)
			: base(message)
		{
		}

		public BadStateException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected BadStateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	public enum ExtractExistingFileAction
	{
		Throw,
		OverwriteSilently,
		DoNotOverwrite,
		InvokeExtractProgressEvent
	}
}
namespace Ionic
{
	internal enum LogicalConjunction
	{
		NONE,
		AND,
		OR,
		XOR
	}
	internal enum WhichTime
	{
		atime,
		mtime,
		ctime
	}
	internal enum ComparisonOperator
	{
		[Description(">")]
		GreaterThan,
		[Description(">=")]
		GreaterThanOrEqualTo,
		[Description("<")]
		LesserThan,
		[Description("<=")]
		LesserThanOrEqualTo,
		[Description("=")]
		EqualTo,
		[Description("!=")]
		NotEqualTo
	}
	internal abstract class SelectionCriterion
	{
		internal virtual bool Verbose { get; set; }

		internal abstract bool Evaluate(string filename);

		[Conditional("SelectorTrace")]
		protected static void CriterionTrace(string format, params object[] args)
		{
		}

		internal abstract bool Evaluate(ZipEntry entry);
	}
	internal class SizeCriterion : SelectionCriterion
	{
		internal ComparisonOperator Operator;

		internal long Size;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("size ").Append(EnumUtil.GetDescription(Operator)).Append(" ")
				.Append(Size.ToString());
			return stringBuilder.ToString();
		}

		internal override bool Evaluate(string filename)
		{
			FileInfo fileInfo = new FileInfo(filename);
			return _Evaluate(fileInfo.Length);
		}

		private bool _Evaluate(long Length)
		{
			bool flag = false;
			return Operator switch
			{
				ComparisonOperator.GreaterThanOrEqualTo => Length >= Size, 
				ComparisonOperator.GreaterThan => Length > Size, 
				ComparisonOperator.LesserThanOrEqualTo => Length <= Size, 
				ComparisonOperator.LesserThan => Length < Size, 
				ComparisonOperator.EqualTo => Length == Size, 
				ComparisonOperator.NotEqualTo => Length != Size, 
				_ => throw new ArgumentException("Operator"), 
			};
		}

		internal override bool Evaluate(ZipEntry entry)
		{
			return _Evaluate(entry.UncompressedSize);
		}
	}
	internal class TimeCriterion : SelectionCriterion
	{
		internal ComparisonOperator Operator;

		internal WhichTime Which;

		internal DateTime Time;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Which.ToString()).Append(" ").Append(EnumUtil.GetDescription(Operator))
				.Append(" ")
				.Append(Time.ToString("yyyy-MM-dd-HH:mm:ss"));
			return stringBuilder.ToString();
		}

		internal override bool Evaluate(string filename)
		{
			return _Evaluate(Which switch
			{
				WhichTime.atime => File.GetLastAccessTime(filename).ToUniversalTime(), 
				WhichTime.mtime => File.GetLastWriteTime(filename).ToUniversalTime(), 
				WhichTime.ctime => File.GetCreationTime(filename).ToUniversalTime(), 
				_ => throw new ArgumentException("Operator"), 
			});
		}

		private bool _Evaluate(DateTime x)
		{
			bool flag = false;
			return Operator switch
			{
				ComparisonOperator.GreaterThanOrEqualTo => x >= Time, 
				ComparisonOperator.GreaterThan => x > Time, 
				ComparisonOperator.LesserThanOrEqualTo => x <= Time, 
				ComparisonOperator.LesserThan => x < Time, 
				ComparisonOperator.EqualTo => x == Time, 
				ComparisonOperator.NotEqualTo => x != Time, 
				_ => throw new ArgumentException("Operator"), 
			};
		}

		internal override bool Evaluate(ZipEntry entry)
		{
			return _Evaluate(Which switch
			{
				WhichTime.atime => entry.AccessedTime, 
				WhichTime.mtime => entry.ModifiedTime, 
				WhichTime.ctime => entry.CreationTime, 
				_ => throw new ArgumentException("??time"), 
			});
		}
	}
	internal class NameCriterion : SelectionCriterion
	{
		private Regex _re;

		private string _regexString;

		internal ComparisonOperator Operator;

		private string _MatchingFileSpec;

		internal virtual string MatchingFileSpec
		{
			set
			{
				if (Directory.Exists(value))
				{
					_MatchingFileSpec = ".\\" + value + "\\*.*";
				}
				else
				{
					_MatchingFileSpec = value;
				}
				_regexString = "^" + Regex.Escape(_MatchingFileSpec).Replace("\\\\\\*\\.\\*", "\\\\([^\\.]+|.*\\.[^\\\\\\.]*)").Replace("\\.\\*", "\\.[^\\\\\\.]*")
					.Replace("\\*", ".*")
					.Replace("\\?", "[^\\\\\\.]") + "$";
				_re = new Regex(_regexString, RegexOptions.IgnoreCase);
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("name ").Append(EnumUtil.GetDescription(Operator)).Append(" '")
				.Append(_MatchingFileSpec)
				.Append("'");
			return stringBuilder.ToString();
		}

		internal override bool Evaluate(string filename)
		{
			return _Evaluate(filename);
		}

		private bool _Evaluate(string fullpath)
		{
			string input = ((_MatchingFileSpec.IndexOf('\\') == -1) ? Path.GetFileName(fullpath) : fullpath);
			bool flag = _re.IsMatch(input);
			if (Operator != ComparisonOperator.EqualTo)
			{
				flag = !flag;
			}
			return flag;
		}

		internal override bool Evaluate(ZipEntry entry)
		{
			string fullpath = entry.FileName.Replace("/", "\\");
			return _Evaluate(fullpath);
		}
	}
	internal class TypeCriterion : SelectionCriterion
	{
		private char ObjectType;

		internal ComparisonOperator Operator;

		internal string AttributeString
		{
			get
			{
				return ObjectType.ToString();
			}
			set
			{
				if (value.Length != 1 || (value[0] != 'D' && value[0] != 'F'))
				{
					throw new ArgumentException("Specify a single character: either D or F");
				}
				ObjectType = value[0];
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("type ").Append(EnumUtil.GetDescription(Operator)).Append(" ")
				.Append(AttributeString);
			return stringBuilder.ToString();
		}

		internal override bool Evaluate(string filename)
		{
			bool flag = ((ObjectType == 'D') ? Directory.Exists(filename) : File.Exists(filename));
			if (Operator != ComparisonOperator.EqualTo)
			{
				flag = !flag;
			}
			return flag;
		}

		internal override bool Evaluate(ZipEntry entry)
		{
			bool flag = ((ObjectType == 'D') ? entry.IsDirectory : (!entry.IsDirectory));
			if (Operator != ComparisonOperator.EqualTo)
			{
				flag = !flag;
			}
			return flag;
		}
	}
	internal class AttributesCriterion : SelectionCriterion
	{
		private FileAttributes _Attributes;

		internal ComparisonOperator Operator;

		internal string AttributeString
		{
			get
			{
				string text = "";
				if ((_Attributes & FileAttributes.Hidden) != 0)
				{
					text += "H";
				}
				if ((_Attributes & FileAttributes.System) != 0)
				{
					text += "S";
				}
				if ((_Attributes & FileAttributes.ReadOnly) != 0)
				{
					text += "R";
				}
				if ((_Attributes & FileAttributes.Archive) != 0)
				{
					text += "A";
				}
				if ((_Attributes & FileAttributes.ReparsePoint) != 0)
				{
					text += "L";
				}
				if ((_Attributes & FileAttributes.NotContentIndexed) != 0)
				{
					text += "I";
				}
				return text;
			}
			set
			{
				_Attributes = FileAttributes.Normal;
				string text = value.ToUpper();
				foreach (char c in text)
				{
					switch (c)
					{
					case 'H':
						if ((_Attributes & FileAttributes.Hidden) != 0)
						{
							throw new ArgumentException($"Repeated flag. ({c})", "value");
						}
						_Attributes |= FileAttributes.Hidden;
						break;
					case 'R':
						if ((_Attributes & FileAttributes.ReadOnly) != 0)
						{
							throw new ArgumentException($"Repeated flag. ({c})", "value");
						}
						_Attributes |= FileAttributes.ReadOnly;
						break;
					case 'S':
						if ((_Attributes & FileAttributes.System) != 0)
						{
							throw new ArgumentException($"Repeated flag. ({c})", "value");
						}
						_Attributes |= FileAttributes.System;
						break;
					case 'A':
						if ((_Attributes & FileAttributes.Archive) != 0)
						{
							throw new ArgumentException($"Repeated flag. ({c})", "value");
						}
						_Attributes |= FileAttributes.Archive;
						break;
					case 'I':
						if ((_Attributes & FileAttributes.NotContentIndexed) != 0)
						{
							throw new ArgumentException($"Repeated flag. ({c})", "value");
						}
						_Attributes |= FileAttributes.NotContentIndexed;
						break;
					case 'L':
						if ((_Attributes & FileAttributes.ReparsePoint) != 0)
						{
							throw new ArgumentException($"Repeated flag. ({c})", "value");
						}
						_Attributes |= FileAttributes.ReparsePoint;
						break;
					default:
						throw new ArgumentException(value);
					}
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("attributes ").Append(EnumUtil.GetDescription(Operator)).Append(" ")
				.Append(AttributeString);
			return stringBuilder.ToString();
		}

		private bool _EvaluateOne(FileAttributes fileAttrs, FileAttributes criterionAttrs)
		{
			bool flag = false;
			if ((_Attributes & criterionAttrs) == criterionAttrs)
			{
				return (fileAttrs & criterionAttrs) == criterionAttrs;
			}
			return true;
		}

		internal override bool Evaluate(string filename)
		{
			if (Directory.Exists(filename))
			{
				return Operator != ComparisonOperator.EqualTo;
			}
			FileAttributes attributes = File.GetAttributes(filename);
			return _Evaluate(attributes);
		}

		private bool _Evaluate(FileAttributes fileAttrs)
		{
			bool flag = _EvaluateOne(fileAttrs, FileAttributes.Hidden);
			if (flag)
			{
				flag = _EvaluateOne(fileAttrs, FileAttributes.System);
			}
			if (flag)
			{
				flag = _EvaluateOne(fileAttrs, FileAttributes.ReadOnly);
			}
			if (flag)
			{
				flag = _EvaluateOne(fileAttrs, FileAttributes.Archive);
			}
			if (flag)
			{
				flag = _EvaluateOne(fileAttrs, FileAttributes.NotContentIndexed);
			}
			if (flag)
			{
				flag = _EvaluateOne(fileAttrs, FileAttributes.ReparsePoint);
			}
			if (Operator != ComparisonOperator.EqualTo)
			{
				flag = !flag;
			}
			return flag;
		}

		internal override bool Evaluate(ZipEntry entry)
		{
			FileAttributes attributes = entry.Attributes;
			return _Evaluate(attributes);
		}
	}
	internal class CompoundCriterion : SelectionCriterion
	{
		internal LogicalConjunction Conjunction;

		internal SelectionCriterion Left;

		private SelectionCriterion _Right;

		internal SelectionCriterion Right
		{
			get
			{
				return _Right;
			}
			set
			{
				_Right = value;
				if (value == null)
				{
					Conjunction = LogicalConjunction.NONE;
				}
				else if (Conjunction == LogicalConjunction.NONE)
				{
					Conjunction = LogicalConjunction.AND;
				}
			}
		}

		internal override bool Evaluate(string filename)
		{
			bool flag = Left.Evaluate(filename);
			switch (Conjunction)
			{
			case LogicalConjunction.AND:
				if (flag)
				{
					flag = Right.Evaluate(filename);
				}
				break;
			case LogicalConjunction.OR:
				if (!flag)
				{
					flag = Right.Evaluate(filename);
				}
				break;
			case LogicalConjunction.XOR:
				flag ^= Right.Evaluate(filename);
				break;
			default:
				throw new ArgumentException("Conjunction");
			}
			return flag;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(").Append((Left != null) ? Left.ToString() : "null").Append(" ")
				.Append(Conjunction.ToString())
				.Append(" ")
				.Append((Right != null) ? Right.ToString() : "null")
				.Append(")");
			return stringBuilder.ToString();
		}

		internal override bool Evaluate(ZipEntry entry)
		{
			bool flag = Left.Evaluate(entry);
			switch (Conjunction)
			{
			case LogicalConjunction.AND:
				if (flag)
				{
					flag = Right.Evaluate(entry);
				}
				break;
			case LogicalConjunction.OR:
				if (!flag)
				{
					flag = Right.Evaluate(entry);
				}
				break;
			case LogicalConjunction.XOR:
				flag ^= Right.Evaluate(entry);
				break;
			}
			return flag;
		}
	}
	public class FileSelector
	{
		private enum ParseState
		{
			Start,
			OpenParen,
			CriterionDone,
			ConjunctionPending,
			Whitespace
		}

		private static class RegexAssertions
		{
			public static readonly string PrecededByOddNumberOfSingleQuotes = "(?<=(?:[^']*'[^']*')*'[^']*)";

			public static readonly string FollowedByOddNumberOfSingleQuotesAndLineEnd = "(?=[^']*'(?:[^']*'[^']*')*[^']*$)";

			public static readonly string PrecededByEvenNumberOfSingleQuotes = "(?<=(?:[^']*'[^']*')*[^']*)";

			public static readonly string FollowedByEvenNumberOfSingleQuotesAndLineEnd = "(?=(?:[^']*'[^']*')*[^']*$)";
		}

		internal SelectionCriterion _Criterion;

		public string SelectionCriteria
		{
			get
			{
				if (_Criterion == null)
				{
					return null;
				}
				return _Criterion.ToString();
			}
			set
			{
				if (value == null)
				{
					_Criterion = null;
				}
				else if (value.Trim() == "")
				{
					_Criterion = null;
				}
				else
				{
					_Criterion = _ParseCriterion(value);
				}
			}
		}

		public bool TraverseReparsePoints { get; set; }

		public FileSelector(string selectionCriteria)
			: this(selectionCriteria, traverseDirectoryReparsePoints: true)
		{
		}

		public FileSelector(string selectionCriteria, bool traverseDirectoryReparsePoints)
		{
			if (!string.IsNullOrEmpty(selectionCriteria))
			{
				_Criterion = _ParseCriterion(selectionCriteria);
			}
			TraverseReparsePoints = traverseDirectoryReparsePoints;
		}

		private static string NormalizeCriteriaExpression(string source)
		{
			string[][] array = new string[11][]
			{
				new string[2] { "([^']*)\\(\\(([^']+)", "$1( ($2" },
				new string[2] { "(.)\\)\\)", "$1) )" },
				new string[2] { "\\((\\S)", "( $1" },
				new string[2] { "(\\S)\\)", "$1 )" },
				new string[2] { "^\\)", " )" },
				new string[2] { "(\\S)\\(", "$1 (" },
				new string[2] { "\\)(\\S)", ") $1" },
				new string[2] { "(=)('[^']*')", "$1 $2" },
				new string[2] { "([^ !><])(>|<|!=|=)", "$1 $2" },
				new string[2] { "(>|<|!=|=)([^ =])", "$1 $2" },
				new string[2] { "/", "\\" }
			};
			string input = source;
			for (int i = 0; i < array.Length; i++)
			{
				string pattern = RegexAssertions.PrecededByEvenNumberOfSingleQuotes + array[i][0] + RegexAssertions.FollowedByEvenNumberOfSingleQuotesAndLineEnd;
				input = Regex.Replace(input, pattern, array[i][1]);
			}
			string pattern2 = "/" + RegexAssertions.FollowedByOddNumberOfSingleQuotesAndLineEnd;
			input = Regex.Replace(input, pattern2, "\\");
			pattern2 = " " + RegexAssertions.FollowedByOddNumberOfSingleQuotesAndLineEnd;
			return Regex.Replace(input, pattern2, "\u0006");
		}

		private static SelectionCriterion _ParseCriterion(string s)
		{
			if (s == null)
			{
				return null;
			}
			s = NormalizeCriteriaExpression(s);
			if (s.IndexOf(" ") == -1)
			{
				s = "name = " + s;
			}
			string[] array = s.Trim().Split(' ', '\t');
			if (array.Length < 3)
			{
				throw new ArgumentException(s);
			}
			SelectionCriterion selectionCriterion = null;
			LogicalConjunction logicalConjunction = LogicalConjunction.NONE;
			Stack<ParseState> stack = new Stack<ParseState>();
			Stack<SelectionCriterion> stack2 = new Stack<SelectionCriterion>();
			stack.Push(ParseState.Start);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].ToLower();
				ParseState parseState;
				switch (text)
				{
				case "and":
				case "xor":
				case "or":
				{
					parseState = stack.Peek();
					if (parseState != ParseState.CriterionDone)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					if (array.Length <= i + 3)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					logicalConjunction = (LogicalConjunction)Enum.Parse(typeof(LogicalConjunction), array[i].ToUpper(), ignoreCase: true);
					CompoundCriterion compoundCriterion = new CompoundCriterion();
					compoundCriterion.Left = selectionCriterion;
					compoundCriterion.Right = null;
					compoundCriterion.Conjunction = logicalConjunction;
					selectionCriterion = compoundCriterion;
					stack.Push(parseState);
					stack.Push(ParseState.ConjunctionPending);
					stack2.Push(selectionCriterion);
					break;
				}
				case "(":
					parseState = stack.Peek();
					if (parseState != ParseState.Start && parseState != ParseState.ConjunctionPending && parseState != ParseState.OpenParen)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					if (array.Length <= i + 4)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					stack.Push(ParseState.OpenParen);
					break;
				case ")":
					parseState = stack.Pop();
					if (stack.Peek() != ParseState.OpenParen)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					stack.Pop();
					stack.Push(ParseState.CriterionDone);
					break;
				case "atime":
				case "ctime":
				case "mtime":
				{
					if (array.Length <= i + 2)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					DateTime value;
					try
					{
						value = DateTime.ParseExact(array[i + 2], "yyyy-MM-dd-HH:mm:ss", null);
					}
					catch (FormatException)
					{
						try
						{
							value = DateTime.ParseExact(array[i + 2], "yyyy/MM/dd-HH:mm:ss", null);
						}
						catch (FormatException)
						{
							try
							{
								value = DateTime.ParseExact(array[i + 2], "yyyy/MM/dd", null);
								goto end_IL_035d;
							}
							catch (FormatException)
							{
								try
								{
									value = DateTime.ParseExact(array[i + 2], "MM/dd/yyyy", null);
									goto end_IL_035d;
								}
								catch (FormatException)
								{
									value = DateTime.ParseExact(array[i + 2], "yyyy-MM-dd", null);
									goto end_IL_035d;
								}
							}
							end_IL_035d:;
						}
					}
					value = DateTime.SpecifyKind(value, DateTimeKind.Local).ToUniversalTime();
					TimeCriterion timeCriterion = new TimeCriterion();
					timeCriterion.Which = (WhichTime)Enum.Parse(typeof(WhichTime), array[i], ignoreCase: true);
					timeCriterion.Operator = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1]);
					timeCriterion.Time = value;
					selectionCriterion = timeCriterion;
					i += 2;
					stack.Push(ParseState.CriterionDone);
					break;
				}
				case "length":
				case "size":
				{
					if (array.Length <= i + 2)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					long num = 0L;
					string text2 = array[i + 2];
					num = (text2.ToUpper().EndsWith("K") ? (long.Parse(text2.Substring(0, text2.Length - 1)) * 1024) : (text2.ToUpper().EndsWith("KB") ? (long.Parse(text2.Substring(0, text2.Length - 2)) * 1024) : (text2.ToUpper().EndsWith("M") ? (long.Parse(text2.Substring(0, text2.Length - 1)) * 1024 * 1024) : (text2.ToUpper().EndsWith("MB") ? (long.Parse(text2.Substring(0, text2.Length - 2)) * 1024 * 1024) : (text2.ToUpper().EndsWith("G") ? (long.Parse(text2.Substring(0, text2.Length - 1)) * 1024 * 1024 * 1024) : ((!text2.ToUpper().EndsWith("GB")) ? long.Parse(array[i + 2]) : (long.Parse(text2.Substring(0, text2.Length - 2)) * 1024 * 1024 * 1024)))))));
					SizeCriterion sizeCriterion = new SizeCriterion();
					sizeCriterion.Size = num;
					sizeCriterion.Operator = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1]);
					selectionCriterion = sizeCriterion;
					i += 2;
					stack.Push(ParseState.CriterionDone);
					break;
				}
				case "filename":
				case "name":
				{
					if (array.Length <= i + 2)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					ComparisonOperator comparisonOperator2 = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1]);
					if (comparisonOperator2 != ComparisonOperator.NotEqualTo && comparisonOperator2 != ComparisonOperator.EqualTo)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					string text3 = array[i + 2];
					if (text3.StartsWith("'") && text3.EndsWith("'"))
					{
						text3 = text3.Substring(1, text3.Length - 2).Replace("\u0006", " ");
					}
					NameCriterion nameCriterion = new NameCriterion();
					nameCriterion.MatchingFileSpec = text3;
					nameCriterion.Operator = comparisonOperator2;
					selectionCriterion = nameCriterion;
					i += 2;
					stack.Push(ParseState.CriterionDone);
					break;
				}
				case "attrs":
				case "attributes":
				case "type":
				{
					if (array.Length <= i + 2)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					ComparisonOperator comparisonOperator = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1]);
					if (comparisonOperator != ComparisonOperator.NotEqualTo && comparisonOperator != ComparisonOperator.EqualTo)
					{
						throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
					}
					object obj;
					if (!(text == "type"))
					{
						AttributesCriterion attributesCriterion = new AttributesCriterion();
						attributesCriterion.AttributeString = array[i + 2];
						attributesCriterion.Operator = comparisonOperator;
						obj = attributesCriterion;
					}
					else
					{
						TypeCriterion typeCriterion = new TypeCriterion();
						typeCriterion.AttributeString = array[i + 2];
						typeCriterion.Operator = comparisonOperator;
						obj = typeCriterion;
					}
					selectionCriterion = (SelectionCriterion)obj;
					i += 2;
					stack.Push(ParseState.CriterionDone);
					break;
				}
				case "":
					stack.Push(ParseState.Whitespace);
					break;
				default:
					throw new ArgumentException("'" + array[i] + "'");
				}
				parseState = stack.Peek();
				if (parseState == ParseState.CriterionDone)
				{
					stack.Pop();
					if (stack.Peek() == ParseState.ConjunctionPending)
					{
						while (stack.Peek() == ParseState.ConjunctionPending)
						{
							CompoundCriterion compoundCriterion2 = stack2.Pop() as CompoundCriterion;
							compoundCriterion2.Right = selectionCriterion;
							selectionCriterion = compoundCriterion2;
							stack.Pop();
							parseState = stack.Pop();
							if (parseState != ParseState.CriterionDone)
							{
								throw new ArgumentException("??");
							}
						}
					}
					else
					{
						stack.Push(ParseState.CriterionDone);
					}
				}
				if (parseState == ParseState.Whitespace)
				{
					stack.Pop();
				}
			}
			return selectionCriterion;
		}

		public override string ToString()
		{
			return "FileSelector(" + _Criterion.ToString() + ")";
		}

		private bool Evaluate(string filename)
		{
			return _Criterion.Evaluate(filename);
		}

		[Conditional("SelectorTrace")]
		private void SelectorTrace(string format, params object[] args)
		{
			if (_Criterion != null && _Criterion.Verbose)
			{
				Console.WriteLine(format, args);
			}
		}

		public ICollection<string> SelectFiles(string directory)
		{
			return SelectFiles(directory, recurseDirectories: false);
		}

		public ReadOnlyCollection<string> SelectFiles(string directory, bool recurseDirectories)
		{
			if (_Criterion == null)
			{
				throw new ArgumentException("SelectionCriteria has not been set");
			}
			List<string> list = new List<string>();
			try
			{
				if (Directory.Exists(directory))
				{
					string[] files = Directory.GetFiles(directory);
					string[] array = files;
					foreach (string text in array)
					{
						if (Evaluate(text))
						{
							list.Add(text);
						}
					}
					if (recurseDirectories)
					{
						string[] directories = Directory.GetDirectories(directory);
						string[] array2 = directories;
						foreach (string text2 in array2)
						{
							if (TraverseReparsePoints || (File.GetAttributes(text2) & FileAttributes.ReparsePoint) == 0)
							{
								if (Evaluate(text2))
								{
									list.Add(text2);
								}
								list.AddRange(SelectFiles(text2, recurseDirectories));
							}
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (IOException)
			{
			}
			return list.AsReadOnly();
		}

		private bool Evaluate(ZipEntry entry)
		{
			return _Criterion.Evaluate(entry);
		}

		public ICollection<ZipEntry> SelectEntries(ZipFile zip)
		{
			if (zip == null)
			{
				throw new ArgumentNullException("zip");
			}
			List<ZipEntry> list = new List<ZipEntry>();
			foreach (ZipEntry item in zip)
			{
				if (Evaluate(item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		public ICollection<ZipEntry> SelectEntries(ZipFile zip, string directoryPathInArchive)
		{
			if (zip == null)
			{
				throw new ArgumentNullException("zip");
			}
			List<ZipEntry> list = new List<ZipEntry>();
			string text = directoryPathInArchive?.Replace("/", "\\");
			if (text != null)
			{
				while (text.EndsWith("\\"))
				{
					text = text.Substring(0, text.Length - 1);
				}
			}
			foreach (ZipEntry item in zip)
			{
				if ((directoryPathInArchive == null || Path.GetDirectoryName(item.FileName) == directoryPathInArchive || Path.GetDirectoryName(item.FileName) == text) && Evaluate(item))
				{
					list.Add(item);
				}
			}
			return list;
		}
	}
	internal sealed class EnumUtil
	{
		private EnumUtil()
		{
		}

		internal static string GetDescription(Enum value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
			if (array.Length > 0)
			{
				return array[0].Description;
			}
			return value.ToString();
		}

		internal static object Parse(Type enumType, string stringRepresentation)
		{
			return Parse(enumType, stringRepresentation, ignoreCase: false);
		}

		internal static object Parse(Type enumType, string stringRepresentation, bool ignoreCase)
		{
			if (ignoreCase)
			{
				stringRepresentation = stringRepresentation.ToLower();
			}
			foreach (Enum value in Enum.GetValues(enumType))
			{
				string text = GetDescription(value);
				if (ignoreCase)
				{
					text = text.ToLower();
				}
				if (text == stringRepresentation)
				{
					return value;
				}
			}
			return Enum.Parse(enumType, stringRepresentation, ignoreCase);
		}
	}
}
namespace Ionic.Zip
{
	internal class OffsetStream : Stream, IDisposable
	{
		private long _originalPosition;

		private Stream _innerStream;

		public override bool CanRead => _innerStream.CanRead;

		public override bool CanSeek => _innerStream.CanSeek;

		public override bool CanWrite => false;

		public override long Length => _innerStream.Length;

		public override long Position
		{
			get
			{
				return _innerStream.Position - _originalPosition;
			}
			set
			{
				_innerStream.Position = _originalPosition + value;
			}
		}

		public OffsetStream(Stream s)
		{
			_originalPosition = s.Position;
			_innerStream = s;
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return _innerStream.Read(buffer, offset, count);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		public override void Flush()
		{
			_innerStream.Flush();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return _innerStream.Seek(_originalPosition + offset, origin) - _originalPosition;
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		void IDisposable.Dispose()
		{
			Close();
		}

		public override void Close()
		{
			base.Close();
		}
	}
	internal static class SharedUtilities
	{
		private static Regex doubleDotRegex1 = new Regex("^(.*/)?([^/\\\\.]+/\\\\.\\\\./)(.+)$");

		private static Encoding ibm437 = Encoding.GetEncoding("IBM437");

		private static Encoding utf8 = Encoding.GetEncoding("UTF-8");

		public static long GetFileLength(string fileName)
		{
			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException(fileName);
			}
			long num = 0L;
			FileShare fileShare = FileShare.ReadWrite;
			fileShare |= FileShare.Delete;
			using FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, fileShare);
			return fileStream.Length;
		}

		[Conditional("NETCF")]
		public static void Workaround_Ladybug318918(Stream s)
		{
			s.Flush();
		}

		private static string SimplifyFwdSlashPath(string path)
		{
			if (path.StartsWith("./"))
			{
				path = path.Substring(2);
			}
			path = path.Replace("/./", "/");
			path = doubleDotRegex1.Replace(path, "$1$3");
			return path;
		}

		public static string NormalizePathForUseInZipFile(string pathName)
		{
			if (string.IsNullOrEmpty(pathName))
			{
				return pathName;
			}
			if (pathName.Length >= 2 && pathName[1] == ':' && pathName[2] == '\\')
			{
				pathName = pathName.Substring(3);
			}
			pathName = pathName.Replace('\\', '/');
			while (pathName.StartsWith("/"))
			{
				pathName = pathName.Substring(1);
			}
			return SimplifyFwdSlashPath(pathName);
		}

		internal static byte[] StringToByteArray(string value, Encoding encoding)
		{
			return encoding.GetBytes(value);
		}

		internal static byte[] StringToByteArray(string value)
		{
			return StringToByteArray(value, ibm437);
		}

		internal static string Utf8StringFromBuffer(byte[] buf)
		{
			return StringFromBuffer(buf, utf8);
		}

		internal static string StringFromBuffer(byte[] buf, Encoding encoding)
		{
			return encoding.GetString(buf, 0, buf.Length);
		}

		internal static int ReadSignature(Stream s)
		{
			int result = 0;
			try
			{
				result = _ReadFourBytes(s, "n/a");
			}
			catch (BadReadException)
			{
			}
			return result;
		}

		internal static int ReadEntrySignature(Stream s)
		{
			int num = 0;
			try
			{
				num = _ReadFourBytes(s, "n/a");
				if (num == 134695760)
				{
					s.Seek(12L, SeekOrigin.Current);
					num = _ReadFourBytes(s, "n/a");
					if (num != 67324752)
					{
						s.Seek(8L, SeekOrigin.Current);
						num = _ReadFourBytes(s, "n/a");
						if (num != 67324752)
						{
							s.Seek(-24L, SeekOrigin.Current);
							num = _ReadFourBytes(s, "n/a");
						}
					}
				}
			}
			catch (BadReadException)
			{
			}
			return num;
		}

		internal static int ReadInt(Stream s)
		{
			return _ReadFourBytes(s, "Could not read block - no data!  (position 0x{0:X8})");
		}

		private static int _ReadFourBytes(Stream s, string message)
		{
			int num = 0;
			byte[] array = new byte[4];
			num = s.Read(array, 0, array.Length);
			if (num != array.Length)
			{
				throw new BadReadException(string.Format(message, s.Position));
			}
			return ((array[3] * 256 + array[2]) * 256 + array[1]) * 256 + array[0];
		}

		internal static long FindSignature(Stream stream, int SignatureToFind)
		{
			long position = stream.Position;
			int num = 65536;
			byte[] array = new byte[4]
			{
				(byte)(SignatureToFind >> 24),
				(byte)((SignatureToFind & 0xFF0000) >> 16),
				(byte)((SignatureToFind & 0xFF00) >> 8),
				(byte)(SignatureToFind & 0xFF)
			};
			byte[] array2 = new byte[num];
			int num2 = 0;
			bool flag = false;
			do
			{
				num2 = stream.Read(array2, 0, array2.Length);
				if (num2 == 0)
				{
					break;
				}
				for (int i = 0; i < num2; i++)
				{
					if (array2[i] == array[3])
					{
						long position2 = stream.Position;
						stream.Seek(i - num2, SeekOrigin.Current);
						int num3 = ReadSignature(stream);
						flag = num3 == SignatureToFind;
						if (flag)
						{
							break;
						}
						stream.Seek(position2, SeekOrigin.Begin);
					}
				}
			}
			while (!flag);
			if (!flag)
			{
				stream.Seek(position, SeekOrigin.Begin);
				return -1L;
			}
			return stream.Position - position - 4;
		}

		internal static DateTime AdjustTime_Reverse(DateTime time)
		{
			if (time.Kind == DateTimeKind.Utc)
			{
				return time;
			}
			DateTime result = time;
			if (DateTime.Now.IsDaylightSavingTime() && !time.IsDaylightSavingTime())
			{
				result = time - new TimeSpan(1, 0, 0);
			}
			else if (!DateTime.Now.IsDaylightSavingTime() && time.IsDaylightSavingTime())
			{
				result = time + new TimeSpan(1, 0, 0);
			}
			return result;
		}

		internal static DateTime PackedToDateTime(int packedDateTime)
		{
			if (packedDateTime == 65535 || packedDateTime == 0)
			{
				return new DateTime(1995, 1, 1, 0, 0, 0, 0);
			}
			short num = (short)(packedDateTime & 0xFFFF);
			short num2 = (short)((packedDateTime & 0xFFFF0000u) >> 16);
			int i = 1980 + ((num2 & 0xFE00) >> 9);
			int j = (num2 & 0x1E0) >> 5;
			int k = num2 & 0x1F;
			int num3 = (num & 0xF800) >> 11;
			int l = (num & 0x7E0) >> 5;
			int m = (num & 0x1F) * 2;
			if (m >= 60)
			{
				l++;
				m = 0;
			}
			if (l >= 60)
			{
				num3++;
				l = 0;
			}
			if (num3 >= 24)
			{
				k++;
				num3 = 0;
			}
			DateTime value = DateTime.Now;
			bool flag = false;
			try
			{
				value = new DateTime(i, j, k, num3, l, m, 0);
				flag = true;
			}
			catch (ArgumentOutOfRangeException)
			{
				if (i == 1980 && (j == 0 || k == 0))
				{
					try
					{
						value = new DateTime(1980, 1, 1, num3, l, m, 0);
						flag = true;
					}
					catch (ArgumentOutOfRangeException)
					{
						try
						{
							value = new DateTime(1980, 1, 1, 0, 0, 0, 0);
							flag = true;
							goto end_IL_00f1;
						}
						catch (ArgumentOutOfRangeException)
						{
							goto end_IL_00f1;
						}
						end_IL_00f1:;
					}
				}
				else
				{
					try
					{
						for (; i < 1980; i++)
						{
						}
						while (i > 2030)
						{
							i--;
						}
						for (; j < 1; j++)
						{
						}
						while (j > 12)
						{
							j--;
						}
						for (; k < 1; k++)
						{
						}
						while (k > 28)
						{
							k--;
						}
						for (; l < 0; l++)
						{
						}
						while (l > 59)
						{
							l--;
						}
						for (; m < 0; m++)
						{
						}
						while (m > 59)
						{
							m--;
						}
						value = new DateTime(i, j, k, num3, l, m, 0);
						flag = true;
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			if (!flag)
			{
				string arg = $"y({i}) m({j}) d({k}) h({num3}) m({l}) s({m})";
				throw new ZipException($"Bad date/time format in the zip file. ({arg})");
			}
			return DateTime.SpecifyKind(value, DateTimeKind.Local);
		}

		internal static int DateTimeToPacked(DateTime time)
		{
			time = time.ToLocalTime();
			ushort num = (ushort)((time.Day & 0x1F) | ((time.Month << 5) & 0x1E0) | ((time.Year - 1980 << 9) & 0xFE00));
			ushort num2 = (ushort)(((time.Second / 2) & 0x1F) | ((time.Minute << 5) & 0x7E0) | ((time.Hour << 11) & 0xF800));
			return (num << 16) | num2;
		}

		public static void CreateAndOpenUniqueTempFile(string dir, out Stream fs, out string filename)
		{
			for (int i = 0; i < 3; i++)
			{
				try
				{
					filename = Path.Combine(dir, InternalGetTempFileName());
					fs = new FileStream(filename, FileMode.CreateNew);
					return;
				}
				catch (IOException)
				{
					if (i == 2)
					{
						throw;
					}
				}
			}
			throw new IOException();
		}

		public static string InternalGetTempFileName()
		{
			return "DotNetZip-" + Path.GetRandomFileName().Substring(0, 8) + ".tmp";
		}

		internal static int ReadWithRetry(Stream s, byte[] buffer, int offset, int count, string FileName)
		{
			int result = 0;
			bool flag = false;
			int num = 0;
			do
			{
				try
				{
					result = s.Read(buffer, offset, count);
					flag = true;
				}
				catch (IOException ex)
				{
					SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
					if (securityPermission.IsUnrestricted())
					{
						uint num2 = _HRForException(ex);
						if (num2 != 2147942433u)
						{
							throw new IOException($"Cannot read file {FileName}", ex);
						}
						num++;
						if (num > 10)
						{
							throw new IOException($"Cannot read file {FileName}, at offset 0x{offset:X8} after 10 retries", ex);
						}
						Thread.Sleep(250 + num * 550);
						continue;
					}
					throw;
				}
			}
			while (!flag);
			return result;
		}

		private static uint _HRForException(Exception ex1)
		{
			return (uint)Marshal.GetHRForException(ex1);
		}
	}
	public class CountingStream : Stream
	{
		private Stream _s;

		private long _bytesWritten;

		private long _bytesRead;

		private long _initialOffset;

		public Stream WrappedStream => _s;

		public long BytesWritten => _bytesWritten;

		public long BytesRead => _bytesRead;

		public override bool CanRead => _s.CanRead;

		public override bool CanSeek => _s.CanSeek;

		public override bool CanWrite => _s.CanWrite;

		public override long Length => _s.Length;

		public long ComputedPosition => _initialOffset + _bytesWritten;

		public override long Position
		{
			get
			{
				return _s.Position;
			}
			set
			{
				_s.Seek(value, SeekOrigin.Begin);
			}
		}

		public CountingStream(Stream stream)
		{
			_s = stream;
			try
			{
				_initialOffset = _s.Position;
			}
			catch
			{
				_initialOffset = 0L;
			}
		}

		public void Adjust(long delta)
		{
			_bytesWritten -= delta;
			if (_bytesWritten < 0)
			{
				throw new InvalidOperationException();
			}
			if (_s is CountingStream)
			{
				((CountingStream)_s).Adjust(delta);
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = _s.Read(buffer, offset, count);
			_bytesRead += num;
			return num;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count != 0)
			{
				_s.Write(buffer, offset, count);
				_bytesWritten += count;
			}
		}

		public override void Flush()
		{
			_s.Flush();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return _s.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			_s.SetLength(value);
		}
	}
	internal class WinZipAesCrypto
	{
		internal byte[] _Salt;

		internal byte[] _providedPv;

		internal byte[] _generatedPv;

		internal int _KeyStrengthInBits;

		private byte[] _MacInitializationVector;

		private byte[] _StoredMac;

		private byte[] _keyBytes;

		private short PasswordVerificationStored;

		private short PasswordVerificationGenerated;

		private int Rfc2898KeygenIterations = 1000;

		private string _Password;

		private bool _cryptoGenerated;

		public byte[] CalculatedMac;

		public byte[] GeneratedPV
		{
			get
			{
				if (!_cryptoGenerated)
				{
					_GenerateCryptoBytes();
				}
				return _generatedPv;
			}
		}

		public byte[] Salt => _Salt;

		private int _KeyStrengthInBytes => _KeyStrengthInBits / 8;

		public int SizeOfEncryptionMetadata => _KeyStrengthInBytes / 2 + 10 + 2;

		public string Password
		{
			private get
			{
				return _Password;
			}
			set
			{
				_Password = value;
				if (_Password != null)
				{
					PasswordVerificationGenerated = (short)(GeneratedPV[0] + GeneratedPV[1] * 256);
					if (PasswordVerificationGenerated != PasswordVerificationStored)
					{
						throw new BadPasswordException();
					}
				}
			}
		}

		public byte[] KeyBytes
		{
			get
			{
				if (!_cryptoGenerated)
				{
					_GenerateCryptoBytes();
				}
				return _keyBytes;
			}
		}

		public byte[] MacIv
		{
			get
			{
				if (!_cryptoGenerated)
				{
					_GenerateCryptoBytes();
				}
				return _MacInitializationVector;
			}
		}

		private WinZipAesCrypto(string password, int KeyStrengthInBits)
		{
			_Password = password;
			_KeyStrengthInBits = KeyStrengthInBits;
		}

		public static WinZipAesCrypto Generate(string password, int KeyStrengthInBits)
		{
			WinZipAesCrypto winZipAesCrypto = new WinZipAesCrypto(password, KeyStrengthInBits);
			int num = winZipAesCrypto._KeyStrengthInBytes / 2;
			winZipAesCrypto._Salt = new byte[num];
			Random random = new Random();
			random.NextBytes(winZipAesCrypto._Salt);
			return winZipAesCrypto;
		}

		public static WinZipAesCrypto ReadFromStream(string password, int KeyStrengthInBits, Stream s)
		{
			WinZipAesCrypto winZipAesCrypto = new WinZipAesCrypto(password, KeyStrengthInBits);
			int num = winZipAesCrypto._KeyStrengthInBytes / 2;
			winZipAesCrypto._Salt = new byte[num];
			winZipAesCrypto._providedPv = new byte[2];
			s.Read(winZipAesCrypto._Salt, 0, winZipAesCrypto._Salt.Length);
			s.Read(winZipAesCrypto._providedPv, 0, winZipAesCrypto._providedPv.Length);
			winZipAesCrypto.PasswordVerificationStored = (short)(winZipAesCrypto._providedPv[0] + winZipAesCrypto._providedPv[1] * 256);
			if (password != null)
			{
				winZipAesCrypto.PasswordVerificationGenerated = (short)(winZipAesCrypto.GeneratedPV[0] + winZipAesCrypto.GeneratedPV[1] * 256);
				if (winZipAesCrypto.PasswordVerificationGenerated != winZipAesCrypto.PasswordVerificationStored)
				{
					throw new BadPasswordException("bad password");
				}
			}
			return winZipAesCrypto;
		}

		private void _GenerateCryptoBytes()
		{
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(_Password, Salt, Rfc2898KeygenIterations);
			_keyBytes = rfc2898DeriveBytes.GetBytes(_KeyStrengthInBytes);
			_MacInitializationVector = rfc2898DeriveBytes.GetBytes(_KeyStrengthInBytes);
			_generatedPv = rfc2898DeriveBytes.GetBytes(2);
			_cryptoGenerated = true;
		}

		public void ReadAndVerifyMac(Stream s)
		{
			bool flag = false;
			_StoredMac = new byte[10];
			s.Read(_StoredMac, 0, _StoredMac.Length);
			if (_StoredMac.Length != CalculatedMac.Length)
			{
				flag = true;
			}
			if (!flag)
			{
				for (int i = 0; i < _StoredMac.Length; i++)
				{
					if (_StoredMac[i] != CalculatedMac[i])
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				throw new BadStateException("The MAC does not match.");
			}
		}
	}
	internal class WinZipAesCipherStream : Stream
	{
		private const int BLOCK_SIZE_IN_BYTES = 16;

		private WinZipAesCrypto _params;

		private Stream _s;

		private CryptoMode _mode;

		private int _nonce;

		private bool _finalBlock;

		internal HMACSHA1 _mac;

		internal RijndaelManaged _aesCipher;

		internal ICryptoTransform _xform;

		private byte[] counter = new byte[16];

		private byte[] counterOut = new byte[16];

		private long _length;

		private long _totalBytesXferred;

		private byte[] _PendingWriteBlock;

		private int _pendingCount;

		private byte[] _iobuf;

		private object _outputLock = new object();

		public byte[] FinalAuthentication
		{
			get
			{
				if (!_finalBlock)
				{
					if (_totalBytesXferred != 0)
					{
						throw new BadStateException("The final hash has not been computed.");
					}
					byte[] buffer = new byte[0];
					_mac.ComputeHash(buffer);
				}
				byte[] array = new byte[10];
				Array.Copy(_mac.Hash, 0, array, 0, 10);
				return array;
			}
		}

		public override bool CanRead
		{
			get
			{
				if (_mode != CryptoMode.Decrypt)
				{
					return false;
				}
				return true;
			}
		}

		public override bool CanSeek => false;

		public override bool CanWrite => _mode == CryptoMode.Encrypt;

		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		internal WinZipAesCipherStream(Stream s, WinZipAesCrypto cryptoParams, long length, CryptoMode mode)
			: this(s, cryptoParams, mode)
		{
			_length = length;
		}

		internal WinZipAesCipherStream(Stream s, WinZipAesCrypto cryptoParams, CryptoMode mode)
		{
			_params = cryptoParams;
			_s = s;
			_mode = mode;
			_nonce = 1;
			if (_params == null)
			{
				throw new BadPasswordException("Supply a password to use AES encryption.");
			}
			int num = _params.KeyBytes.Length * 8;
			if (num != 256 && num != 128 && num != 192)
			{
				throw new ArgumentOutOfRangeException("keysize", "size of key must be 128, 192, or 256");
			}
			_mac = new HMACSHA1(_params.MacIv);
			_aesCipher = new RijndaelManaged();
			_aesCipher.BlockSize = 128;
			_aesCipher.KeySize = num;
			_aesCipher.Mode = CipherMode.ECB;
			_aesCipher.Padding = PaddingMode.None;
			byte[] rgbIV = new byte[16];
			_xform = _aesCipher.CreateEncryptor(_params.KeyBytes, rgbIV);
			if (_mode == CryptoMode.Encrypt)
			{
				_iobuf = new byte[2048];
				_PendingWriteBlock = new byte[16];
			}
		}

		private void XorInPlace(byte[] buffer, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				buffer[offset + i] = (byte)(counterOut[i] ^ buffer[offset + i]);
			}
		}

		private void WriteTransformOneBlock(byte[] buffer, int offset)
		{
			Array.Copy(BitConverter.GetBytes(_nonce++), 0, counter, 0, 4);
			_xform.TransformBlock(counter, 0, 16, counterOut, 0);
			XorInPlace(buffer, offset, 16);
			_mac.TransformBlock(buffer, offset, 16, null, 0);
		}

		private void WriteTransformBlocks(byte[] buffer, int offset, int count)
		{
			int i = offset;
			for (int num = count + offset; i < buffer.Length && i < num; i += 16)
			{
				WriteTransformOneBlock(buffer, i);
			}
		}

		private void WriteTransformFinalBlock()
		{
			if (_pendingCount == 0)
			{
				throw new InvalidOperationException("No bytes available.");
			}
			if (_finalBlock)
			{
				throw new InvalidOperationException("The final block has already been transformed.");
			}
			Array.Copy(BitConverter.GetBytes(_nonce++), 0, counter, 0, 4);
			counterOut = _xform.TransformFinalBlock(counter, 0, 16);
			XorInPlace(_PendingWriteBlock, 0, _pendingCount);
			_mac.TransformFinalBlock(_PendingWriteBlock, 0, _pendingCount);
			_finalBlock = true;
		}

		private int ReadTransformOneBlock(byte[] buffer, int offset, int last)
		{
			if (_finalBlock)
			{
				throw new NotSupportedException();
			}
			int num = last - offset;
			int num2 = ((num > 16) ? 16 : num);
			Array.Copy(BitConverter.GetBytes(_nonce++), 0, counter, 0, 4);
			if (num2 == num && _length > 0 && _totalBytesXferred + last == _length)
			{
				_mac.TransformFinalBlock(buffer, offset, num2);
				counterOut = _xform.TransformFinalBlock(counter, 0, 16);
				_finalBlock = true;
			}
			else
			{
				_mac.TransformBlock(buffer, offset, num2, null, 0);
				_xform.TransformBlock(counter, 0, 16, counterOut, 0);
			}
			XorInPlace(buffer, offset, num2);
			return num2;
		}

		private void ReadTransformBlocks(byte[] buffer, int offset, int count)
		{
			int i = offset;
			int num2;
			for (int num = count + offset; i < buffer.Length && i < num; i += num2)
			{
				num2 = ReadTransformOneBlock(buffer, i, num);
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (_mode == CryptoMode.Encrypt)
			{
				throw new NotSupportedException();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must not be less than zero.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must not be less than zero.");
			}
			if (buffer.Length < offset + count)
			{
				throw new ArgumentException("The buffer is too small");
			}
			int count2 = count;
			if (_totalBytesXferred >= _length)
			{
				return 0;
			}
			long num = _length - _totalBytesXferred;
			if (num < count)
			{
				count2 = (int)num;
			}
			int num2 = _s.Read(buffer, offset, count2);
			ReadTransformBlocks(buffer, offset, count2);
			_totalBytesXferred += num2;
			return num2;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (_finalBlock)
			{
				throw new InvalidOperationException("The final block has already been transformed.");
			}
			if (_mode == CryptoMode.Decrypt)
			{
				throw new NotSupportedException();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must not be less than zero.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must not be less than zero.");
			}
			if (buffer.Length < offset + count)
			{
				throw new ArgumentException("The offset and count are too large");
			}
			if (count == 0)
			{
				return;
			}
			if (count + _pendingCount <= 16)
			{
				Buffer.BlockCopy(buffer, offset, _PendingWriteBlock, _pendingCount, count);
				_pendingCount += count;
				return;
			}
			int num = count;
			int num2 = offset;
			if (_pendingCount != 0)
			{
				int num3 = 16 - _pendingCount;
				if (num3 > 0)
				{
					Buffer.BlockCopy(buffer, offset, _PendingWriteBlock, _pendingCount, num3);
					num -= num3;
					num2 += num3;
				}
				WriteTransformOneBlock(_PendingWriteBlock, 0);
				_s.Write(_PendingWriteBlock, 0, 16);
				_totalBytesXferred += 16L;
				_pendingCount = 0;
			}
			int num4 = (num - 1) / 16;
			_pendingCount = num - num4 * 16;
			Buffer.BlockCopy(buffer, num2 + num - _pendingCount, _PendingWriteBlock, 0, _pendingCount);
			num -= _pendingCount;
			_totalBytesXferred += num;
			if (num4 <= 0)
			{
				return;
			}
			do
			{
				int num5 = _iobuf.Length;
				if (num5 > num)
				{
					num5 = num;
				}
				Buffer.BlockCopy(buffer, num2, _iobuf, 0, num5);
				WriteTransformBlocks(_iobuf, 0, num5);
				_s.Write(_iobuf, 0, num5);
				num -= num5;
				num2 += num5;
			}
			while (num > 0);
		}

		public override void Close()
		{
			if (_pendingCount > 0)
			{
				WriteTransformFinalBlock();
				_s.Write(_PendingWriteBlock, 0, _pendingCount);
				_totalBytesXferred += _pendingCount;
				_pendingCount = 0;
			}
			_s.Close();
		}

		public override void Flush()
		{
			_s.Flush();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		[Conditional("Trace")]
		private void TraceOutput(string format, params object[] varParams)
		{
			lock (_outputLock)
			{
				int hashCode = Thread.CurrentThread.GetHashCode();
				Console.ForegroundColor = (ConsoleColor)(hashCode % 8 + 8);
				Console.Write("{0:000} WZACS ", hashCode);
				Console.WriteLine(format, varParams);
				Console.ResetColor();
			}
		}
	}
	internal static class ZipConstants
	{
		public const uint PackedToRemovableMedia = 808471376u;

		public const uint Zip64EndOfCentralDirectoryRecordSignature = 101075792u;

		public const uint Zip64EndOfCentralDirectoryLocatorSignature = 117853008u;

		public const uint EndOfCentralDirectorySignature = 101010256u;

		public const int ZipEntrySignature = 67324752;

		public const int ZipEntryDataDescriptorSignature = 134695760;

		public const int SplitArchiveSignature = 134695760;

		public const int ZipDirEntrySignature = 33639248;

		public const int AesKeySize = 192;

		public const int AesBlockSize = 128;

		public const ushort AesAlgId128 = 26126;

		public const ushort AesAlgId192 = 26127;

		public const ushort AesAlgId256 = 26128;
	}
	internal class ZipCrypto
	{
		private uint[] _Keys = new uint[3] { 305419896u, 591751049u, 878082192u };

		private CRC32 crc32 = new CRC32();

		private byte MagicByte
		{
			get
			{
				ushort num = (ushort)((ushort)(_Keys[2] & 0xFFFF) | 2);
				return (byte)(num * (num ^ 1) >> 8);
			}
		}

		private ZipCrypto()
		{
		}

		public static ZipCrypto ForWrite(string password)
		{
			ZipCrypto zipCrypto = new ZipCrypto();
			if (password == null)
			{
				throw new BadPasswordException("This entry requires a password.");
			}
			zipCrypto.InitCipher(password);
			return zipCrypto;
		}

		public static ZipCrypto ForRead(string password, ZipEntry e)
		{
			Stream archiveStream = e._archiveStream;
			e._WeakEncryptionHeader = new byte[12];
			byte[] weakEncryptionHeader = e._WeakEncryptionHeader;
			ZipCrypto zipCrypto = new ZipCrypto();
			if (password == null)
			{
				throw new BadPasswordException("This entry requires a password.");
			}
			zipCrypto.InitCipher(password);
			ZipEntry.ReadWeakEncryptionHeader(archiveStream, weakEncryptionHeader);
			byte[] array = zipCrypto.DecryptMessage(weakEncryptionHeader, weakEncryptionHeader.Length);
			if (array[11] != (byte)((e._Crc32 >> 24) & 0xFF))
			{
				if ((e._BitField & 8) != 8)
				{
					throw new BadPasswordException("The password did not match.");
				}
				if (array[11] != (byte)((e._TimeBlob >> 8) & 0xFF))
				{
					throw new BadPasswordException("The password did not match.");
				}
			}
			return zipCrypto;
		}

		public byte[] DecryptMessage(byte[] cipherText, int length)
		{
			if (cipherText == null)
			{
				throw new ArgumentNullException("cipherText");
			}
			if (length > cipherText.Length)
			{
				throw new ArgumentOutOfRangeException("length", "Bad length during Decryption: the length parameter must be smaller than or equal to the size of the destination array.");
			}
			byte[] array = new byte[length];
			for (int i = 0; i < length; i++)
			{
				byte b = (byte)(cipherText[i] ^ MagicByte);
				UpdateKeys(b);
				array[i] = b;
			}
			return array;
		}

		public byte[] EncryptMessage(byte[] plainText, int length)
		{
			if (plainText == null)
			{
				throw new ArgumentNullException("plaintext");
			}
			if (length > plainText.Length)
			{
				throw new ArgumentOutOfRangeException("length", "Bad length during Encryption: The length parameter must be smaller than or equal to the size of the destination array.");
			}
			byte[] array = new byte[length];
			for (int i = 0; i < length; i++)
			{
				byte byteValue = plainText[i];
				array[i] = (byte)(plainText[i] ^ MagicByte);
				UpdateKeys(byteValue);
			}
			return array;
		}

		public void InitCipher(string passphrase)
		{
			byte[] array = SharedUtilities.StringToByteArray(passphrase);
			for (int i = 0; i < passphrase.Length; i++)
			{
				UpdateKeys(array[i]);
			}
		}

		private void UpdateKeys(byte byteValue)
		{
			_Keys[0] = (uint)crc32.ComputeCrc32((int)_Keys[0], byteValue);
			_Keys[1] = _Keys[1] + (byte)_Keys[0];
			_Keys[1] = _Keys[1] * 134775813 + 1;
			_Keys[2] = (uint)crc32.ComputeCrc32((int)_Keys[2], (byte)(_Keys[1] >> 24));
		}
	}
	internal enum CryptoMode
	{
		Encrypt,
		Decrypt
	}
	internal class ZipCipherStream : Stream
	{
		private ZipCrypto _cipher;

		private Stream _s;

		private CryptoMode _mode;

		public override bool CanRead => _mode == CryptoMode.Decrypt;

		public override bool CanSeek => false;

		public override bool CanWrite => _mode == CryptoMode.Encrypt;

		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public ZipCipherStream(Stream s, ZipCrypto cipher, CryptoMode mode)
		{
			_cipher = cipher;
			_s = s;
			_mode = mode;
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (_mode == CryptoMode.Encrypt)
			{
				throw new NotSupportedException("This stream does not encrypt via Read()");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			byte[] array = new byte[count];
			int num = _s.Read(array, 0, count);
			byte[] array2 = _cipher.DecryptMessage(array, num);
			for (int i = 0; i < num; i++)
			{
				buffer[offset + i] = array2[i];
			}
			return num;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (_mode == CryptoMode.Decrypt)
			{
				throw new NotSupportedException("This stream does not Decrypt via Write()");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count == 0)
			{
				return;
			}
			byte[] array = null;
			if (offset != 0)
			{
				array = new byte[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = buffer[offset + i];
				}
			}
			else
			{
				array = buffer;
			}
			byte[] array2 = _cipher.EncryptMessage(array, count);
			_s.Write(array2, 0, array2.Length);
		}

		public override void Flush()
		{
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}
	}
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00004")]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	public class ZipEntry
	{
		private class CopyHelper
		{
			private static Regex re = new Regex(" \\(copy (\\d+)\\)$");

			private static int callCount = 0;

			internal static string AppendCopyToFileName(string f)
			{
				callCount++;
				if (callCount > 25)
				{
					throw new OverflowException("overflow while creating filename");
				}
				int num = 1;
				int num2 = f.LastIndexOf(".");
				if (num2 == -1)
				{
					Match match = re.Match(f);
					if (match.Success)
					{
						num = int.Parse(match.Groups[1].Value) + 1;
						string text = $" (copy {num})";
						f = f.Substring(0, match.Index) + text;
					}
					else
					{
						string text2 = $" (copy {num})";
						f += text2;
					}
				}
				else
				{
					Match match2 = re.Match(f.Substring(0, num2));
					if (match2.Success)
					{
						num = int.Parse(match2.Groups[1].Value) + 1;
						string text3 = $" (copy {num})";
						f = f.Substring(0, match2.Index) + text3 + f.Substring(num2);
					}
					else
					{
						string text4 = $" (copy {num})";
						f = f.Substring(0, num2) + text4 + f.Substring(num2);
					}
				}
				return f;
			}
		}

		private delegate T Func<T>();

		private short _VersionMadeBy;

		private short _InternalFileAttrs;

		private int _ExternalFileAttrs;

		private short _filenameLength;

		private short _extraFieldLength;

		private short _commentLength;

		private ZipCrypto _zipCrypto_forExtract;

		private ZipCrypto _zipCrypto_forWrite;

		private WinZipAesCrypto _aesCrypto_forExtract;

		private WinZipAesCrypto _aesCrypto_forWrite;

		private short _WinZipAesMethod;

		internal DateTime _LastModified;

		private DateTime _Mtime;

		private DateTime _Atime;

		private DateTime _Ctime;

		private bool _ntfsTimesAreSet;

		private bool _emitNtfsTimes = true;

		private bool _emitUnixTimes;

		private bool _TrimVolumeFromFullyQualifiedPaths = true;

		internal string _LocalFileName;

		private string _FileNameInArchive;

		internal short _VersionNeeded;

		internal short _BitField;

		internal short _CompressionMethod;

		private short _CompressionMethod_FromZipFile;

		private CompressionLevel _CompressionLevel;

		internal string _Comment;

		private bool _IsDirectory;

		private byte[] _CommentBytes;

		internal long _CompressedSize;

		internal long _CompressedFileDataSize;

		internal long _UncompressedSize;

		internal int _TimeBlob;

		private bool _crcCalculated;

		internal int _Crc32;

		internal byte[] _Extra;

		private bool _metadataChanged;

		private bool _restreamRequiredOnSave;

		private bool _sourceIsEncrypted;

		private bool _skippedDuringSave;

		private uint _diskNumber;

		private static Encoding ibm437 = Encoding.GetEncoding("IBM437");

		private Encoding _actualEncoding;

		internal ZipContainer _container;

		private long __FileDataPosition = -1L;

		private byte[] _EntryHeader;

		internal long _RelativeOffsetOfLocalHeader;

		private long _future_ROLH;

		private long _TotalEntrySize;

		private int _LengthOfHeader;

		private int _LengthOfTrailer;

		internal bool _InputUsesZip64;

		private uint _UnsupportedAlgorithmId;

		internal string _Password;

		internal ZipEntrySource _Source;

		internal EncryptionAlgorithm _Encryption;

		internal EncryptionAlgorithm _Encryption_FromZipFile;

		internal byte[] _WeakEncryptionHeader;

		internal Stream _archiveStream;

		private Stream _sourceStream;

		private long? _sourceStreamOriginalPosition;

		private bool _sourceWasJitProvided;

		private bool _ioOperationCanceled;

		private bool _presumeZip64;

		private bool? _entryRequiresZip64;

		private bool? _OutputUsesZip64;

		private bool _IsText;

		private ZipEntryTimestamp _timestamp;

		private static DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		private static DateTime _win32Epoch = DateTime.FromFileTimeUtc(0L);

		private static DateTime _zeroHour = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		private WriteDelegate _WriteDelegate;

		private OpenDelegate _OpenDelegate;

		private CloseDelegate _CloseDelegate;

		private Stream _inputDecryptorStream;

		private int _readExtraDepth;

		private object _outputLock = new object();

		internal bool AttributesIndicateDirectory
		{
			get
			{
				if (_InternalFileAttrs == 0)
				{
					return (_ExternalFileAttrs & 0x10) == 16;
				}
				return false;
			}
		}

		public string Info
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append($"          ZipEntry: {FileName}\n").Append($"   Version Made By: {_VersionMadeBy}\n").Append($" Needed to extract: {VersionNeeded}\n");
				if (_IsDirectory)
				{
					stringBuilder.Append("        Entry type: directory\n");
				}
				else
				{
					stringBuilder.Append(string.Format("         File type: {0}\n", _IsText ? "text" : "binary")).Append($"       Compression: {CompressionMethod}\n").Append($"        Compressed: 0x{CompressedSize:X}\n")
						.Append($"      Uncompressed: 0x{UncompressedSize:X}\n")
						.Append($"             CRC32: 0x{_Crc32:X8}\n");
				}
				stringBuilder.Append($"       Disk Number: {_diskNumber}\n");
				if (_RelativeOffsetOfLocalHeader > uint.MaxValue)
				{
					stringBuilder.Append($"   Relative Offset: 0x{_RelativeOffsetOfLocalHeader:X16}\n");
				}
				else
				{
					stringBuilder.Append($"   Relative Offset: 0x{_RelativeOffsetOfLocalHeader:X8}\n");
				}
				stringBuilder.Append($"         Bit Field: 0x{_BitField:X4}\n").Append($"        Encrypted?: {_sourceIsEncrypted}\n").Append($"          Timeblob: 0x{_TimeBlob:X8}\n")
					.Append($"              Time: {SharedUtilities.PackedToDateTime(_TimeBlob)}\n");
				stringBuilder.Append($"         Is Zip64?: {_InputUsesZip64}\n");
				if (!string.IsNullOrEmpty(_Comment))
				{
					stringBuilder.Append($"           Comment: {_Comment}\n");
				}
				stringBuilder.Append("\n");
				return stringBuilder.ToString();
			}
		}

		public DateTime LastModified
		{
			get
			{
				return _LastModified.ToLocalTime();
			}
			set
			{
				_LastModified = ((value.Kind == DateTimeKind.Unspecified) ? DateTime.SpecifyKind(value, DateTimeKind.Local) : value.ToLocalTime());
				_Mtime = SharedUtilities.AdjustTime_Reverse(_LastModified).ToUniversalTime();
				_metadataChanged = true;
			}
		}

		private int BufferSize => _container.BufferSize;

		public DateTime ModifiedTime
		{
			get
			{
				return _Mtime;
			}
			set
			{
				SetEntryTimes(_Ctime, _Atime, value);
			}
		}

		public DateTime AccessedTime
		{
			get
			{
				return _Atime;
			}
			set
			{
				SetEntryTimes(_Ctime, value, _Mtime);
			}
		}

		public DateTime CreationTime
		{
			get
			{
				return _Ctime;
			}
			set
			{
				SetEntryTimes(value, _Atime, _Mtime);
			}
		}

		public bool EmitTimesInWindowsFormatWhenSaving
		{
			get
			{
				return _emitNtfsTimes;
			}
			set
			{
				_emitNtfsTimes = value;
				_metadataChanged = true;
			}
		}

		public bool EmitTimesInUnixFormatWhenSaving
		{
			get
			{
				return _emitUnixTimes;
			}
			set
			{
				_emitUnixTimes = value;
				_metadataChanged = true;
			}
		}

		public ZipEntryTimestamp Timestamp => _timestamp;

		public FileAttributes Attributes
		{
			get
			{
				return (FileAttributes)_ExternalFileAttrs;
			}
			set
			{
				_ExternalFileAttrs = (int)value;
				_VersionMadeBy = 45;
				_metadataChanged = true;
			}
		}

		internal string LocalFileName => _LocalFileName;

		public string FileName
		{
			get
			{
				return _FileNameInArchive;
			}
			set
			{
				if (_container.ZipFile == null)
				{
					throw new ZipException("Cannot rename; this is not supported in ZipOutputStream/ZipInputStream.");
				}
				if (string.IsNullOrEmpty(value))
				{
					throw new ZipException("The FileName must be non empty and non-null.");
				}
				string text = NameInArchive(value, null);
				if (!(_FileNameInArchive == text))
				{
					_container.ZipFile.RemoveEntry(this);
					_container.ZipFile.InternalAddEntry(text, this);
					_FileNameInArchive = text;
					_container.ZipFile.NotifyEntryChanged();
					_metadataChanged = true;
				}
			}
		}

		public Stream InputStream
		{
			get
			{
				return _sourceStream;
			}
			set
			{
				if (_Source != ZipEntrySource.Stream)
				{
					throw new ZipException("You must not set the input stream for this entry.");
				}
				_sourceWasJitProvided = true;
				_sourceStream = value;
			}
		}

		public bool InputStreamWasJitProvided => _sourceWasJitProvided;

		public ZipEntrySource Source => _Source;

		public short VersionNeeded => _VersionNeeded;

		public string Comment
		{
			get
			{
				return _Comment;
			}
			set
			{
				_Comment = value;
				_metadataChanged = true;
			}
		}

		public bool? RequiresZip64 => _entryRequiresZip64;

		public bool? OutputUsedZip64 => _OutputUsesZip64;

		public short BitField => _BitField;

		public CompressionMethod CompressionMethod
		{
			get
			{
				return (CompressionMethod)_CompressionMethod;
			}
			set
			{
				if (value != (CompressionMethod)_CompressionMethod)
				{
					if (value != CompressionMethod.None && value != CompressionMethod.Deflate && value != CompressionMethod.BZip2)
					{
						throw new InvalidOperationException("Unsupported compression method.");
					}
					_CompressionMethod = (short)value;
					if (_CompressionMethod == 0)
					{
						_CompressionLevel = CompressionLevel.None;
					}
					else if (CompressionLevel == CompressionLevel.None)
					{
						_CompressionLevel = CompressionLevel.Default;
					}
					if (_container.ZipFile != null)
					{
						_container.ZipFile.NotifyEntryChanged();
					}
					_restreamRequiredOnSave = true;
				}
			}
		}

		public CompressionLevel CompressionLevel
		{
			get
			{
				return _CompressionLevel;
			}
			set
			{
				if ((_CompressionMethod != 8 && _CompressionMethod != 0) || (value == CompressionLevel.Default && _CompressionMethod == 8))
				{
					return;
				}
				_CompressionLevel = value;
				if (value != CompressionLevel.None || _CompressionMethod != 0)
				{
					if (_CompressionLevel == CompressionLevel.None)
					{
						_CompressionMethod = 0;
					}
					else
					{
						_CompressionMethod = 8;
					}
					if (_container.ZipFile != null)
					{
						_container.ZipFile.NotifyEntryChanged();
					}
					_restreamRequiredOnSave = true;
				}
			}
		}

		public long CompressedSize => _CompressedSize;

		public long UncompressedSize => _UncompressedSize;

		public double CompressionRatio
		{
			get
			{
				if (UncompressedSize == 0)
				{
					return 0.0;
				}
				return 100.0 * (1.0 - 1.0 * (double)CompressedSize / (1.0 * (double)UncompressedSize));
			}
		}

		public int Crc => _Crc32;

		public bool IsDirectory => _IsDirectory;

		public bool UsesEncryption => _Encryption_FromZipFile != EncryptionAlgorithm.None;

		public EncryptionAlgorithm Encryption
		{
			get
			{
				return _Encryption;
			}
			set
			{
				if (value != _Encryption)
				{
					if (value == EncryptionAlgorithm.Unsupported)
					{
						throw new InvalidOperationException("You may not set Encryption to that value.");
					}
					_Encryption = value;
					_restreamRequiredOnSave = true;
					if (_container.ZipFile != null)
					{
						_container.ZipFile.NotifyEntryChanged();
					}
				}
			}
		}

		public string Password
		{
			private get
			{
				return _Password;
			}
			set
			{
				_Password = value;
				if (_Password == null)
				{
					_Encryption = EncryptionAlgorithm.None;
					return;
				}
				if (_Source == ZipEntrySource.ZipFile && !_sourceIsEncrypted)
				{
					_restreamRequiredOnSave = true;
				}
				if (Encryption == EncryptionAlgorithm.None)
				{
					_Encryption = EncryptionAlgorithm.PkzipWeak;
				}
			}
		}

		internal bool IsChanged => _restreamRequiredOnSave | _metadataChanged;

		public ExtractExistingFileAction ExtractExistingFile { get; set; }

		public ZipErrorAction ZipErrorAction { get; set; }

		public bool IncludedInMostRecentSave => !_skippedDuringSave;

		public SetCompressionCallback SetCompression { get; set; }

		[Obsolete("Beginning with v1.9.1.6 of DotNetZip, this property is obsolete.  It will be removed in a future version of the library. Your applications should  use AlternateEncoding and AlternateEncodingUsage instead.")]
		public bool UseUnicodeAsNecessary
		{
			get
			{
				if (AlternateEncoding == Encoding.GetEncoding("UTF-8"))
				{
					return AlternateEncodingUsage == ZipOption.AsNecessary;
				}
				return false;
			}
			set
			{
				if (value)
				{
					AlternateEncoding = Encoding.GetEncoding("UTF-8");
					AlternateEncodingUsage = ZipOption.AsNecessary;
				}
				else
				{
					AlternateEncoding = ZipFile.DefaultEncoding;
					AlternateEncodingUsage = ZipOption.Default;
				}
			}
		}

		[Obsolete("This property is obsolete since v1.9.1.6. Use AlternateEncoding and AlternateEncodingUsage instead.", true)]
		public Encoding ProvisionalAlternateEncoding { get; set; }

		public Encoding AlternateEncoding { get; set; }

		public ZipOption AlternateEncodingUsage { get; set; }

		public bool IsText
		{
			get
			{
				return _IsText;
			}
			set
			{
				_IsText = value;
			}
		}

		internal Stream ArchiveStream
		{
			get
			{
				if (_archiveStream == null)
				{
					if (_container.ZipFile != null)
					{
						ZipFile zipFile = _container.ZipFile;
						zipFile.Reset(whileSaving: false);
						_archiveStream = zipFile.StreamForDiskNumber(_diskNumber);
					}
					else
					{
						_archiveStream = _container.ZipOutputStream.OutputStream;
					}
				}
				return _archiveStream;
			}
		}

		internal long FileDataPosition
		{
			get
			{
				if (__FileDataPosition == -1)
				{
					SetFdpLoh();
				}
				return __FileDataPosition;
			}
		}

		private int LengthOfHeader
		{
			get
			{
				if (_LengthOfHeader == 0)
				{
					SetFdpLoh();
				}
				return _LengthOfHeader;
			}
		}

		private string UnsupportedAlgorithm
		{
			get
			{
				string empty = string.Empty;
				return _UnsupportedAlgorithmId switch
				{
					0u => "--", 
					26113u => "DES", 
					26114u => "RC2", 
					26115u => "3DES-168", 
					26121u => "3DES-112", 
					26126u => "PKWare AES128", 
					26127u => "PKWare AES192", 
					26128u => "PKWare AES256", 
					26370u => "RC2", 
					26400u => "Blowfish", 
					26401u => "Twofish", 
					26625u => "RC4", 
					_ => $"Unknown (0x{_UnsupportedAlgorithmId:X4})", 
				};
			}
		}

		private string UnsupportedCompressionMethod
		{
			get
			{
				string empty = string.Empty;
				return (int)_CompressionMethod switch
				{
					0 => "Store", 
					1 => "Shrink", 
					8 => "DEFLATE", 
					9 => "Deflate64", 
					12 => "BZIP2", 
					14 => "LZMA", 
					19 => "LZ77", 
					98 => "PPMd", 
					_ => $"Unknown (0x{_CompressionMethod:X4})", 
				};
			}
		}

		internal void ResetDirEntry()
		{
			__FileDataPosition = -1L;
			_LengthOfHeader = 0;
		}

		internal static ZipEntry ReadDirEntry(ZipFile zf, Dictionary<string, object> previouslySeen)
		{
			Stream readStream = zf.ReadStream;
			Encoding encoding = ((zf.AlternateEncodingUsage == ZipOption.Always) ? zf.AlternateEncoding : ZipFile.DefaultEncoding);
			int num = SharedUtilities.ReadSignature(readStream);
			if (IsNotValidZipDirEntrySig(num))
			{
				readStream.Seek(-4L, SeekOrigin.Current);
				if ((long)num != 101010256 && (long)num != 101075792 && num != 67324752)
				{
					throw new BadReadException($"  Bad signature (0x{num:X8}) at position 0x{readStream.Position:X8}");
				}
				return null;
			}
			int num2 = 46;
			byte[] array = new byte[42];
			int num3 = readStream.Read(array, 0, array.Length);
			if (num3 != array.Length)
			{
				return null;
			}
			int num4 = 0;
			ZipEntry zipEntry = new ZipEntry();
			zipEntry.AlternateEncoding = encoding;
			zipEntry._Source = ZipEntrySource.ZipFile;
			zipEntry._container = new ZipContainer(zf);
			zipEntry._VersionMadeBy = (short)(array[num4++] + array[num4++] * 256);
			zipEntry._VersionNeeded = (short)(array[num4++] + array[num4++] * 256);
			zipEntry._BitField = (short)(array[num4++] + array[num4++] * 256);
			zipEntry._CompressionMethod = (short)(array[num4++] + array[num4++] * 256);
			zipEntry._TimeBlob = array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256;
			zipEntry._LastModified = SharedUtilities.PackedToDateTime(zipEntry._TimeBlob);
			zipEntry._timestamp |= ZipEntryTimestamp.DOS;
			zipEntry._Crc32 = array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256;
			zipEntry._CompressedSize = (uint)(array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256);
			zipEntry._UncompressedSize = (uint)(array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256);
			zipEntry._CompressionMethod_FromZipFile = zipEntry._CompressionMethod;
			zipEntry._filenameLength = (short)(array[num4++] + array[num4++] * 256);
			zipEntry._extraFieldLength = (short)(array[num4++] + array[num4++] * 256);
			zipEntry._commentLength = (short)(array[num4++] + array[num4++] * 256);
			zipEntry._diskNumber = (uint)(array[num4++] + array[num4++] * 256);
			zipEntry._InternalFileAttrs = (short)(array[num4++] + array[num4++] * 256);
			zipEntry._ExternalFileAttrs = array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256;
			zipEntry._RelativeOffsetOfLocalHeader = (uint)(array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256);
			zipEntry.IsText = (zipEntry._InternalFileAttrs & 1) == 1;
			array = new byte[zipEntry._filenameLength];
			num3 = readStream.Read(array, 0, array.Length);
			num2 += num3;
			if ((zipEntry._BitField & 0x800) == 2048)
			{
				zipEntry._FileNameInArchive = SharedUtilities.Utf8StringFromBuffer(array);
			}
			else
			{
				zipEntry._FileNameInArchive = SharedUtilities.StringFromBuffer(array, encoding);
			}
			while (previouslySeen.ContainsKey(zipEntry._FileNameInArchive))
			{
				zipEntry._FileNameInArchive = CopyHelper.AppendCopyToFileName(zipEntry._FileNameInArchive);
				zipEntry._metadataChanged = true;
			}
			if (zipEntry.AttributesIndicateDirectory)
			{
				zipEntry.MarkAsDirectory();
			}
			else if (zipEntry._FileNameInArchive.EndsWith("/"))
			{
				zipEntry.MarkAsDirectory();
			}
			zipEntry._CompressedFileDataSize = zipEntry._CompressedSize;
			if ((zipEntry._BitField & 1) == 1)
			{
				zipEntry._Encryption_FromZipFile = (zipEntry._Encryption = EncryptionAlgorithm.PkzipWeak);
				zipEntry._sourceIsEncrypted = true;
			}
			if (zipEntry._extraFieldLength > 0)
			{
				zipEntry._InputUsesZip64 = zipEntry._CompressedSize == uint.MaxValue || zipEntry._UncompressedSize == uint.MaxValue || zipEntry._RelativeOffsetOfLocalHeader == uint.MaxValue;
				num2 += zipEntry.ProcessExtraField(readStream, zipEntry._extraFieldLength);
				zipEntry._CompressedFileDataSize = zipEntry._CompressedSize;
			}
			if (zipEntry._Encryption == EncryptionAlgorithm.PkzipWeak)
			{
				zipEntry._CompressedFileDataSize -= 12L;
			}
			else if (zipEntry.Encryption == EncryptionAlgorithm.WinZipAes128 || zipEntry.Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				zipEntry._CompressedFileDataSize = zipEntry.CompressedSize - (GetLengthOfCryptoHeaderBytes(zipEntry.Encryption) + 10);
				zipEntry._LengthOfTrailer = 10;
			}
			if ((zipEntry._BitField & 8) == 8)
			{
				if (zipEntry._InputUsesZip64)
				{
					zipEntry._LengthOfTrailer += 24;
				}
				else
				{
					zipEntry._LengthOfTrailer += 16;
				}
			}
			zipEntry.AlternateEncoding = (((zipEntry._BitField & 0x800) == 2048) ? Encoding.UTF8 : encoding);
			zipEntry.AlternateEncodingUsage = ZipOption.Always;
			if (zipEntry._commentLength > 0)
			{
				array = new byte[zipEntry._commentLength];
				num3 = readStream.Read(array, 0, array.Length);
				num2 += num3;
				if ((zipEntry._BitField & 0x800) == 2048)
				{
					zipEntry._Comment = SharedUtilities.Utf8StringFromBuffer(array);
				}
				else
				{
					zipEntry._Comment = SharedUtilities.StringFromBuffer(array, encoding);
				}
			}
			return zipEntry;
		}

		internal static bool IsNotValidZipDirEntrySig(int signature)
		{
			return signature != 33639248;
		}

		public ZipEntry()
		{
			_CompressionMethod = 8;
			_CompressionLevel = CompressionLevel.Default;
			_Encryption = EncryptionAlgorithm.None;
			_Source = ZipEntrySource.None;
			AlternateEncoding = Encoding.GetEncoding("IBM437");
			AlternateEncodingUsage = ZipOption.Default;
		}

		public void SetEntryTimes(DateTime created, DateTime accessed, DateTime modified)
		{
			_ntfsTimesAreSet = true;
			if (created == _zeroHour && created.Kind == _zeroHour.Kind)
			{
				created = _win32Epoch;
			}
			if (accessed == _zeroHour && accessed.Kind == _zeroHour.Kind)
			{
				accessed = _win32Epoch;
			}
			if (modified == _zeroHour && modified.Kind == _zeroHour.Kind)
			{
				modified = _win32Epoch;
			}
			_Ctime = created.ToUniversalTime();
			_Atime = accessed.ToUniversalTime();
			_Mtime = modified.ToUniversalTime();
			_LastModified = _Mtime;
			if (!_emitUnixTimes && !_emitNtfsTimes)
			{
				_emitNtfsTimes = true;
			}
			_metadataChanged = true;
		}

		internal static string NameInArchive(string filename, string directoryPathInArchive)
		{
			string text = null;
			text = ((directoryPathInArchive == null) ? filename : ((!string.IsNullOrEmpty(directoryPathInArchive)) ? Path.Combine(directoryPathInArchive, Path.GetFileName(filename)) : Path.GetFileName(filename)));
			return SharedUtilities.NormalizePathForUseInZipFile(text);
		}

		internal static ZipEntry CreateFromNothing(string nameInArchive)
		{
			return Create(nameInArchive, ZipEntrySource.None, null, null);
		}

		internal static ZipEntry CreateFromFile(string filename, string nameInArchive)
		{
			return Create(nameInArchive, ZipEntrySource.FileSystem, filename, null);
		}

		internal static ZipEntry CreateForStream(string entryName, Stream s)
		{
			return Create(entryName, ZipEntrySource.Stream, s, null);
		}

		internal static ZipEntry CreateForWriter(string entryName, WriteDelegate d)
		{
			return Create(entryName, ZipEntrySource.WriteDelegate, d, null);
		}

		internal static ZipEntry CreateForJitStreamProvider(string nameInArchive, OpenDelegate opener, CloseDelegate closer)
		{
			return Create(nameInArchive, ZipEntrySource.JitStream, opener, closer);
		}

		internal static ZipEntry CreateForZipOutputStream(string nameInArchive)
		{
			return Create(nameInArchive, ZipEntrySource.ZipOutputStream, null, null);
		}

		private static ZipEntry Create(string nameInArchive, ZipEntrySource source, object arg1, object arg2)
		{
			if (string.IsNullOrEmpty(nameInArchive))
			{
				throw new ZipException("The entry name must be non-null and non-empty.");
			}
			ZipEntry zipEntry = new ZipEntry();
			zipEntry._VersionMadeBy = 45;
			zipEntry._Source = source;
			zipEntry._Mtime = (zipEntry._Atime = (zipEntry._Ctime = DateTime.UtcNow));
			switch (source)
			{
			case ZipEntrySource.Stream:
				zipEntry._sourceStream = arg1 as Stream;
				break;
			case ZipEntrySource.WriteDelegate:
				zipEntry._WriteDelegate = arg1 as WriteDelegate;
				break;
			case ZipEntrySource.JitStream:
				zipEntry._OpenDelegate = arg1 as OpenDelegate;
				zipEntry._CloseDelegate = arg2 as CloseDelegate;
				break;
			case ZipEntrySource.None:
				zipEntry._Source = ZipEntrySource.FileSystem;
				break;
			default:
			{
				string text = arg1 as string;
				if (string.IsNullOrEmpty(text))
				{
					throw new ZipException("The filename must be non-null and non-empty.");
				}
				try
				{
					zipEntry._Mtime = File.GetLastWriteTime(text).ToUniversalTime();
					zipEntry._Ctime = File.GetCreationTime(text).ToUniversalTime();
					zipEntry._Atime = File.GetLastAccessTime(text).ToUniversalTime();
					if (File.Exists(text) || Directory.Exists(text))
					{
						zipEntry._ExternalFileAttrs = (int)File.GetAttributes(text);
					}
					zipEntry._ntfsTimesAreSet = true;
					zipEntry._LocalFileName = Path.GetFullPath(text);
				}
				catch (PathTooLongException innerException)
				{
					string message = $"The path is too long, filename={text}";
					throw new ZipException(message, innerException);
				}
				break;
			}
			case ZipEntrySource.ZipOutputStream:
				break;
			}
			zipEntry._LastModified = zipEntry._Mtime;
			zipEntry._FileNameInArchive = SharedUtilities.NormalizePathForUseInZipFile(nameInArchive);
			return zipEntry;
		}

		internal void MarkAsDirectory()
		{
			_IsDirectory = true;
			if (!_FileNameInArchive.EndsWith("/"))
			{
				_FileNameInArchive += "/";
			}
		}

		public override string ToString()
		{
			return $"ZipEntry::{FileName}";
		}

		private void SetFdpLoh()
		{
			long position = ArchiveStream.Position;
			try
			{
				ArchiveStream.Seek(_RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
			}
			catch (IOException innerException)
			{
				string message = $"Exception seeking  entry({FileName}) offset(0x{_RelativeOffsetOfLocalHeader:X8}) len(0x{ArchiveStream.Length:X8})";
				throw new BadStateException(message, innerException);
			}
			byte[] array = new byte[30];
			ArchiveStream.Read(array, 0, array.Length);
			short num = (short)(array[26] + array[27] * 256);
			short num2 = (short)(array[28] + array[29] * 256);
			ArchiveStream.Seek(num + num2, SeekOrigin.Current);
			_LengthOfHeader = 30 + num2 + num + GetLengthOfCryptoHeaderBytes(_Encryption_FromZipFile);
			__FileDataPosition = _RelativeOffsetOfLocalHeader + _LengthOfHeader;
			ArchiveStream.Seek(position, SeekOrigin.Begin);
		}

		private static int GetKeyStrengthInBits(EncryptionAlgorithm a)
		{
			return a switch
			{
				EncryptionAlgorithm.WinZipAes256 => 256, 
				EncryptionAlgorithm.WinZipAes128 => 128, 
				_ => -1, 
			};
		}

		internal static int GetLengthOfCryptoHeaderBytes(EncryptionAlgorithm a)
		{
			switch (a)
			{
			case EncryptionAlgorithm.None:
				return 0;
			case EncryptionAlgorithm.WinZipAes128:
			case EncryptionAlgorithm.WinZipAes256:
			{
				int keyStrengthInBits = GetKeyStrengthInBits(a);
				return keyStrengthInBits / 8 / 2 + 2;
			}
			case EncryptionAlgorithm.PkzipWeak:
				return 12;
			default:
				throw new ZipException("internal error");
			}
		}

		public void Extract()
		{
			InternalExtract(".", null, null);
		}

		public void Extract(ExtractExistingFileAction extractExistingFile)
		{
			ExtractExistingFile = extractExistingFile;
			InternalExtract(".", null, null);
		}

		public void Extract(Stream stream)
		{
			InternalExtract(null, stream, null);
		}

		public void Extract(string baseDirectory)
		{
			InternalExtract(baseDirectory, null, null);
		}

		public void Extract(string baseDirectory, ExtractExistingFileAction extractExistingFile)
		{
			ExtractExistingFile = extractExistingFile;
			InternalExtract(baseDirectory, null, null);
		}

		public void ExtractWithPassword(string password)
		{
			InternalExtract(".", null, password);
		}

		public void ExtractWithPassword(string baseDirectory, string password)
		{
			InternalExtract(baseDirectory, null, password);
		}

		public void ExtractWithPassword(ExtractExistingFileAction extractExistingFile, string password)
		{
			ExtractExistingFile = extractExistingFile;
			InternalExtract(".", null, password);
		}

		public void ExtractWithPassword(string baseDirectory, ExtractExistingFileAction extractExistingFile, string password)
		{
			ExtractExistingFile = extractExistingFile;
			InternalExtract(baseDirectory, null, password);
		}

		public void ExtractWithPassword(Stream stream, string password)
		{
			InternalExtract(null, stream, password);
		}

		public CrcCalculatorStream OpenReader()
		{
			if (_container.ZipFile == null)
			{
				throw new InvalidOperationException("Use OpenReader() only with ZipFile.");
			}
			return InternalOpenReader(_Password ?? _container.Password);
		}

		public CrcCalculatorStream OpenReader(string password)
		{
			if (_container.ZipFile == null)
			{
				throw new InvalidOperationException("Use OpenReader() only with ZipFile.");
			}
			return InternalOpenReader(password);
		}

		internal CrcCalculatorStream InternalOpenReader(string password)
		{
			ValidateCompression();
			ValidateEncryption();
			SetupCryptoForExtract(password);
			if (_Source != ZipEntrySource.ZipFile)
			{
				throw new BadStateException("You must call ZipFile.Save before calling OpenReader");
			}
			long length = ((_CompressionMethod_FromZipFile == 0) ? _CompressedFileDataSize : UncompressedSize);
			Stream archiveStream = ArchiveStream;
			ArchiveStream.Seek(FileDataPosition, SeekOrigin.Begin);
			_inputDecryptorStream = GetExtractDecryptor(archiveStream);
			Stream extractDecompressor = GetExtractDecompressor(_inputDecryptorStream);
			return new CrcCalculatorStream(extractDecompressor, length);
		}

		private void OnExtractProgress(long bytesWritten, long totalBytesToWrite)
		{
			if (_container.ZipFile != null)
			{
				_ioOperationCanceled = _container.ZipFile.OnExtractBlock(this, bytesWritten, totalBytesToWrite);
			}
		}

		private void OnBeforeExtract(string path)
		{
			if (_container.ZipFile != null && !_container.ZipFile._inExtractAll)
			{
				_ioOperationCanceled = _container.ZipFile.OnSingleEntryExtract(this, path, before: true);
			}
		}

		private void OnAfterExtract(string path)
		{
			if (_container.ZipFile != null && !_container.ZipFile._inExtractAll)
			{
				_container.ZipFile.OnSingleEntryExtract(this, path, before: false);
			}
		}

		private void OnExtractExisting(string path)
		{
			if (_container.ZipFile != null)
			{
				_ioOperationCanceled = _container.ZipFile.OnExtractExisting(this, path);
			}
		}

		private static void ReallyDelete(string fileName)
		{
			if ((File.GetAttributes(fileName) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
			{
				File.SetAttributes(fileName, FileAttributes.Normal);
			}
			File.Delete(fileName);
		}

		private void WriteStatus(string format, params object[] args)
		{
			if (_container.ZipFile != null && _container.ZipFile.Verbose)
			{
				_container.ZipFile.StatusMessageTextWriter.WriteLine(format, args);
			}
		}

		private void InternalExtract(string baseDir, Stream outstream, string password)
		{
			if (_container == null)
			{
				throw new BadStateException("This entry is an orphan");
			}
			if (_container.ZipFile == null)
			{
				throw new InvalidOperationException("Use Extract() only with ZipFile.");
			}
			_container.ZipFile.Reset(whileSaving: false);
			if (_Source != ZipEntrySource.ZipFile)
			{
				throw new BadStateException("You must call ZipFile.Save before calling any Extract method");
			}
			OnBeforeExtract(baseDir);
			_ioOperationCanceled = false;
			string outFileName = null;
			Stream stream = null;
			bool flag = false;
			bool flag2 = false;
			try
			{
				ValidateCompression();
				ValidateEncryption();
				if (ValidateOutput(baseDir, outstream, out outFileName))
				{
					WriteStatus("extract dir {0}...", outFileName);
					OnAfterExtract(baseDir);
					return;
				}
				if (outFileName != null && File.Exists(outFileName))
				{
					flag = true;
					int num = CheckExtractExistingFile(baseDir, outFileName);
					if (num == 2 || num == 1)
					{
						return;
					}
				}
				string text = password ?? _Password ?? _container.Password;
				if (_Encryption_FromZipFile != EncryptionAlgorithm.None)
				{
					if (text == null)
					{
						throw new BadPasswordException();
					}
					SetupCryptoForExtract(text);
				}
				if (outFileName != null)
				{
					WriteStatus("extract file {0}...", outFileName);
					outFileName += ".tmp";
					string directoryName = Path.GetDirectoryName(outFileName);
					if (!Directory.Exists(directoryName))
					{
						Directory.CreateDirectory(directoryName);
					}
					else if (_container.ZipFile != null)
					{
						flag2 = _container.ZipFile._inExtractAll;
					}
					stream = new FileStream(outFileName, FileMode.CreateNew);
				}
				else
				{
					WriteStatus("extract entry {0} to stream...", FileName);
					stream = outstream;
				}
				if (_ioOperationCanceled)
				{
					return;
				}
				int actualCrc = ExtractOne(stream);
				if (_ioOperationCanceled)
				{
					return;
				}
				VerifyCrcAfterExtract(actualCrc);
				if (outFileName != null)
				{
					stream.Close();
					stream = null;
					string text2 = outFileName;
					string text3 = null;
					outFileName = text2.Substring(0, text2.Length - 4);
					if (flag)
					{
						text3 = outFileName + ".PendingOverwrite";
						File.Move(outFileName, text3);
					}
					File.Move(text2, outFileName);
					_SetTimes(outFileName, isFile: true);
					if (text3 != null && File.Exists(text3))
					{
						ReallyDelete(text3);
					}
					if (flag2 && FileName.IndexOf('/') != -1)
					{
						string directoryName2 = Path.GetDirectoryName(FileName);
						if (_container.ZipFile[directoryName2] == null)
						{
							_SetTimes(Path.GetDirectoryName(outFileName), isFile: false);
						}
					}
					if ((_VersionMadeBy & 0xFF00) == 2560 || (_VersionMadeBy & 0xFF00) == 0)
					{
						File.SetAttributes(outFileName, (FileAttributes)_ExternalFileAttrs);
					}
				}
				OnAfterExtract(baseDir);
			}
			catch (Exception)
			{
				_ioOperationCanceled = true;
				throw;
			}
			finally
			{
				if (_ioOperationCanceled && outFileName != null)
				{
					stream?.Close();
					if (File.Exists(outFileName) && !flag)
					{
						File.Delete(outFileName);
					}
				}
			}
		}

		internal void VerifyCrcAfterExtract(int actualCrc32)
		{
			if (actualCrc32 != _Crc32 && ((Encryption != EncryptionAlgorithm.WinZipAes128 && Encryption != EncryptionAlgorithm.WinZipAes256) || _WinZipAesMethod != 2))
			{
				throw new BadCrcException("CRC error: the file being extracted appears to be corrupted. " + $"Expected 0x{_Crc32:X8}, Actual 0x{actualCrc32:X8}");
			}
			if (UncompressedSize != 0 && (Encryption == EncryptionAlgorithm.WinZipAes128 || Encryption == EncryptionAlgorithm.WinZipAes256))
			{
				WinZipAesCipherStream winZipAesCipherStream = _inputDecryptorStream as WinZipAesCipherStream;
				_aesCrypto_forExtract.CalculatedMac = winZipAesCipherStream.FinalAuthentication;
				_aesCrypto_forExtract.ReadAndVerifyMac(ArchiveStream);
			}
		}

		private int CheckExtractExistingFile(string baseDir, string targetFileName)
		{
			int num = 0;
			while (true)
			{
				switch (ExtractExistingFile)
				{
				case ExtractExistingFileAction.OverwriteSilently:
					WriteStatus("the file {0} exists; will overwrite it...", targetFileName);
					return 0;
				case ExtractExistingFileAction.DoNotOverwrite:
					WriteStatus("the file {0} exists; not extracting entry...", FileName);
					OnAfterExtract(baseDir);
					return 1;
				case ExtractExistingFileAction.InvokeExtractProgressEvent:
					if (num > 0)
					{
						throw new ZipException($"The file {targetFileName} already exists.");
					}
					OnExtractExisting(baseDir);
					if (_ioOperationCanceled)
					{
						return 2;
					}
					break;
				default:
					throw new ZipException($"The file {targetFileName} already exists.");
				}
				num++;
			}
		}

		private void _CheckRead(int nbytes)
		{
			if (nbytes == 0)
			{
				throw new BadReadException($"bad read of entry {FileName} from compressed archive.");
			}
		}

		private int ExtractOne(Stream output)
		{
			int num = 0;
			Stream archiveStream = ArchiveStream;
			try
			{
				archiveStream.Seek(FileDataPosition, SeekOrigin.Begin);
				byte[] array = new byte[BufferSize];
				long num2 = ((_CompressionMethod_FromZipFile != 0) ? UncompressedSize : _CompressedFileDataSize);
				_inputDecryptorStream = GetExtractDecryptor(archiveStream);
				Stream extractDecompressor = GetExtractDecompressor(_inputDecryptorStream);
				long num3 = 0L;
				using CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(extractDecompressor);
				while (num2 > 0)
				{
					int count = (int)((num2 > array.Length) ? array.Length : num2);
					int num4 = crcCalculatorStream.Read(array, 0, count);
					_CheckRead(num4);
					output.Write(array, 0, num4);
					num2 -= num4;
					num3 += num4;
					OnExtractProgress(num3, UncompressedSize);
					if (_ioOperationCanceled)
					{
						break;
					}
				}
				return crcCalculatorStream.Crc;
			}
			finally
			{
				if (archiveStream is ZipSegmentedStream zipSegmentedStream)
				{
					zipSegmentedStream.Dispose();
					_archiveStream = null;
				}
			}
		}

		internal Stream GetExtractDecompressor(Stream input2)
		{
			return _CompressionMethod_FromZipFile switch
			{
				0 => input2, 
				8 => new DeflateStream(input2, CompressionMode.Decompress, leaveOpen: true), 
				12 => new BZip2InputStream(input2, leaveOpen: true), 
				_ => null, 
			};
		}

		internal Stream GetExtractDecryptor(Stream input)
		{
			Stream stream = null;
			if (_Encryption_FromZipFile == EncryptionAlgorithm.PkzipWeak)
			{
				return new ZipCipherStream(input, _zipCrypto_forExtract, CryptoMode.Decrypt);
			}
			if (_Encryption_FromZipFile == EncryptionAlgorithm.WinZipAes128 || _Encryption_FromZipFile == EncryptionAlgorithm.WinZipAes256)
			{
				return new WinZipAesCipherStream(input, _aesCrypto_forExtract, _CompressedFileDataSize, CryptoMode.Decrypt);
			}
			return input;
		}

		internal void _SetTimes(string fileOrDirectory, bool isFile)
		{
			try
			{
				if (_ntfsTimesAreSet)
				{
					if (isFile)
					{
						if (File.Exists(fileOrDirectory))
						{
							File.SetCreationTimeUtc(fileOrDirectory, _Ctime);
							File.SetLastAccessTimeUtc(fileOrDirectory, _Atime);
							File.SetLastWriteTimeUtc(fileOrDirectory, _Mtime);
						}
					}
					else if (Directory.Exists(fileOrDirectory))
					{
						Directory.SetCreationTimeUtc(fileOrDirectory, _Ctime);
						Directory.SetLastAccessTimeUtc(fileOrDirectory, _Atime);
						Directory.SetLastWriteTimeUtc(fileOrDirectory, _Mtime);
					}
				}
				else
				{
					DateTime lastWriteTime = SharedUtilities.AdjustTime_Reverse(LastModified);
					if (isFile)
					{
						File.SetLastWriteTime(fileOrDirectory, lastWriteTime);
					}
					else
					{
						Directory.SetLastWriteTime(fileOrDirectory, lastWriteTime);
					}
				}
			}
			catch (IOException ex)
			{
				WriteStatus("failed to set time on {0}: {1}", fileOrDirectory, ex.Message);
			}
		}

		internal void ValidateEncryption()
		{
			if (Encryption != EncryptionAlgorithm.PkzipWeak && Encryption != EncryptionAlgorithm.WinZipAes128 && Encryption != EncryptionAlgorithm.WinZipAes256 && Encryption != EncryptionAlgorithm.None)
			{
				if (_UnsupportedAlgorithmId != 0)
				{
					throw new ZipException($"Cannot extract: Entry {FileName} is encrypted with an algorithm not supported by DotNetZip: {UnsupportedAlgorithm}");
				}
				throw new ZipException($"Cannot extract: Entry {FileName} uses an unsupported encryption algorithm ({(int)Encryption:X2})");
			}
		}

		private void ValidateCompression()
		{
			if (_CompressionMethod_FromZipFile != 0 && _CompressionMethod_FromZipFile != 8 && _CompressionMethod_FromZipFile != 12)
			{
				throw new ZipException($"Entry {FileName} uses an unsupported compression method (0x{_CompressionMethod_FromZipFile:X2}, {UnsupportedCompressionMethod})");
			}
		}

		private void SetupCryptoForExtract(string password)
		{
			if (_Encryption_FromZipFile == EncryptionAlgorithm.None)
			{
				return;
			}
			if (_Encryption_FromZipFile == EncryptionAlgorithm.PkzipWeak)
			{
				if (password == null)
				{
					throw new ZipException("Missing password.");
				}
				ArchiveStream.Seek(FileDataPosition - 12, SeekOrigin.Begin);
				_zipCrypto_forExtract = ZipCrypto.ForRead(password, this);
			}
			else if (_Encryption_FromZipFile == EncryptionAlgorithm.WinZipAes128 || _Encryption_FromZipFile == EncryptionAlgorithm.WinZipAes256)
			{
				if (password == null)
				{
					throw new ZipException("Missing password.");
				}
				if (_aesCrypto_forExtract != null)
				{
					_aesCrypto_forExtract.Password = password;
					return;
				}
				int lengthOfCryptoHeaderBytes = GetLengthOfCryptoHeaderBytes(_Encryption_FromZipFile);
				ArchiveStream.Seek(FileDataPosition - lengthOfCryptoHeaderBytes, SeekOrigin.Begin);
				int keyStrengthInBits = GetKeyStrengthInBits(_Encryption_FromZipFile);
				_aesCrypto_forExtract = WinZipAesCrypto.ReadFromStream(password, keyStrengthInBits, ArchiveStream);
			}
		}

		private bool ValidateOutput(string basedir, Stream outstream, out string outFileName)
		{
			if (basedir != null)
			{
				string text = FileName.Replace("\\", "/");
				if (text.IndexOf(':') == 1)
				{
					text = text.Substring(2);
				}
				if (text.StartsWith("/"))
				{
					text = text.Substring(1);
				}
				if (_container.ZipFile.FlattenFoldersOnExtract)
				{
					outFileName = Path.Combine(basedir, (text.IndexOf('/') != -1) ? Path.GetFileName(text) : text);
				}
				else
				{
					outFileName = Path.Combine(basedir, text);
				}
				outFileName = outFileName.Replace("/", "\\");
				if (IsDirectory || FileName.EndsWith("/"))
				{
					if (!Directory.Exists(outFileName))
					{
						Directory.CreateDirectory(outFileName);
						_SetTimes(outFileName, isFile: false);
					}
					else if (ExtractExistingFile == ExtractExistingFileAction.OverwriteSilently)
					{
						_SetTimes(outFileName, isFile: false);
					}
					return true;
				}
				return false;
			}
			if (outstream != null)
			{
				outFileName = null;
				if (IsDirectory || FileName.EndsWith("/"))
				{
					return true;
				}
				return false;
			}
			throw new ArgumentNullException("outstream");
		}

		private void ReadExtraField()
		{
			_readExtraDepth++;
			long position = ArchiveStream.Position;
			ArchiveStream.Seek(_RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
			byte[] array = new byte[30];
			ArchiveStream.Read(array, 0, array.Length);
			int num = 26;
			short num2 = (short)(array[num++] + array[num++] * 256);
			short extraFieldLength = (short)(array[num++] + array[num++] * 256);
			ArchiveStream.Seek(num2, SeekOrigin.Current);
			ProcessExtraField(ArchiveStream, extraFieldLength);
			ArchiveStream.Seek(position, SeekOrigin.Begin);
			_readExtraDepth--;
		}

		private static bool ReadHeader(ZipEntry ze, Encoding defaultEncoding)
		{
			int num = 0;
			ze._RelativeOffsetOfLocalHeader = ze.ArchiveStream.Position;
			int num2 = SharedUtilities.ReadEntrySignature(ze.ArchiveStream);
			num += 4;
			if (IsNotValidSig(num2))
			{
				ze.ArchiveStream.Seek(-4L, SeekOrigin.Current);
				if (IsNotValidZipDirEntrySig(num2) && (long)num2 != 101010256)
				{
					throw new BadReadException($"  Bad signature (0x{num2:X8}) at position  0x{ze.ArchiveStream.Position:X8}");
				}
				return false;
			}
			byte[] array = new byte[26];
			int num3 = ze.ArchiveStream.Read(array, 0, array.Length);
			if (num3 != array.Length)
			{
				return false;
			}
			num += num3;
			int num4 = 0;
			ze._VersionNeeded = (short)(array[num4++] + array[num4++] * 256);
			ze._BitField = (short)(array[num4++] + array[num4++] * 256);
			ze._CompressionMethod_FromZipFile = (ze._CompressionMethod = (short)(array[num4++] + array[num4++] * 256));
			ze._TimeBlob = array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256;
			ze._LastModified = SharedUtilities.PackedToDateTime(ze._TimeBlob);
			ze._timestamp |= ZipEntryTimestamp.DOS;
			if ((ze._BitField & 1) == 1)
			{
				ze._Encryption_FromZipFile = (ze._Encryption = EncryptionAlgorithm.PkzipWeak);
				ze._sourceIsEncrypted = true;
			}
			ze._Crc32 = array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256;
			ze._CompressedSize = (uint)(array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256);
			ze._UncompressedSize = (uint)(array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256);
			if ((int)ze._CompressedSize == -1 || (int)ze._UncompressedSize == -1)
			{
				ze._InputUsesZip64 = true;
			}
			short num5 = (short)(array[num4++] + array[num4++] * 256);
			short extraFieldLength = (short)(array[num4++] + array[num4++] * 256);
			array = new byte[num5];
			num3 = ze.ArchiveStream.Read(array, 0, array.Length);
			num += num3;
			if ((ze._BitField & 0x800) == 2048)
			{
				ze.AlternateEncoding = Encoding.UTF8;
				ze.AlternateEncodingUsage = ZipOption.Always;
			}
			ze._FileNameInArchive = ze.AlternateEncoding.GetString(array, 0, array.Length);
			if (ze._FileNameInArchive.EndsWith("/"))
			{
				ze.MarkAsDirectory();
			}
			num += ze.ProcessExtraField(ze.ArchiveStream, extraFieldLength);
			ze._LengthOfTrailer = 0;
			if (!ze._FileNameInArchive.EndsWith("/") && (ze._BitField & 8) == 8)
			{
				long position = ze.ArchiveStream.Position;
				bool flag = true;
				long num6 = 0L;
				int num7 = 0;
				while (flag)
				{
					num7++;
					if (ze._container.ZipFile != null)
					{
						ze._container.ZipFile.OnReadBytes(ze);
					}
					long num8 = SharedUtilities.FindSignature(ze.ArchiveStream, 134695760);
					if (num8 == -1)
					{
						return false;
					}
					num6 += num8;
					if (ze._InputUsesZip64)
					{
						array = new byte[20];
						num3 = ze.ArchiveStream.Read(array, 0, array.Length);
						if (num3 != 20)
						{
							return false;
						}
						num4 = 0;
						ze._Crc32 = array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256;
						ze._CompressedSize = BitConverter.ToInt64(array, num4);
						num4 += 8;
						ze._UncompressedSize = BitConverter.ToInt64(array, num4);
						num4 += 8;
						ze._LengthOfTrailer += 24;
					}
					else
					{
						array = new byte[12];
						num3 = ze.ArchiveStream.Read(array, 0, array.Length);
						if (num3 != 12)
						{
							return false;
						}
						num4 = 0;
						ze._Crc32 = array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256;
						ze._CompressedSize = (uint)(array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256);
						ze._UncompressedSize = (uint)(array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256);
						ze._LengthOfTrailer += 16;
					}
					flag = num6 != ze._CompressedSize;
					if (flag)
					{
						ze.ArchiveStream.Seek(-12L, SeekOrigin.Current);
						num6 += 4;
					}
				}
				ze.ArchiveStream.Seek(position, SeekOrigin.Begin);
			}
			ze._CompressedFileDataSize = ze._CompressedSize;
			if ((ze._BitField & 1) == 1)
			{
				if (ze.Encryption == EncryptionAlgorithm.WinZipAes128 || ze.Encryption == EncryptionAlgorithm.WinZipAes256)
				{
					int keyStrengthInBits = GetKeyStrengthInBits(ze._Encryption_FromZipFile);
					ze._aesCrypto_forExtract = WinZipAesCrypto.ReadFromStream(null, keyStrengthInBits, ze.ArchiveStream);
					num += ze._aesCrypto_forExtract.SizeOfEncryptionMetadata - 10;
					ze._CompressedFileDataSize -= ze._aesCrypto_forExtract.SizeOfEncryptionMetadata;
					ze._LengthOfTrailer += 10;
				}
				else
				{
					ze._WeakEncryptionHeader = new byte[12];
					num += ReadWeakEncryptionHeader(ze._archiveStream, ze._WeakEncryptionHeader);
					ze._CompressedFileDataSize -= 12L;
				}
			}
			ze._LengthOfHeader = num;
			ze._TotalEntrySize = ze._LengthOfHeader + ze._CompressedFileDataSize + ze._LengthOfTrailer;
			return true;
		}

		internal static int ReadWeakEncryptionHeader(Stream s, byte[] buffer)
		{
			int num = s.Read(buffer, 0, 12);
			if (num != 12)
			{
				throw new ZipException($"Unexpected end of data at position 0x{s.Position:X8}");
			}
			return num;
		}

		private static bool IsNotValidSig(int signature)
		{
			return signature != 67324752;
		}

		internal static ZipEntry ReadEntry(ZipContainer zc, bool first)
		{
			ZipFile zipFile = zc.ZipFile;
			Stream readStream = zc.ReadStream;
			Encoding alternateEncoding = zc.AlternateEncoding;
			ZipEntry zipEntry = new ZipEntry();
			zipEntry._Source = ZipEntrySource.ZipFile;
			zipEntry._container = zc;
			zipEntry._archiveStream = readStream;
			zipFile?.OnReadEntry(before: true, null);
			if (first)
			{
				HandlePK00Prefix(readStream);
			}
			if (!ReadHeader(zipEntry, alternateEncoding))
			{
				return null;
			}
			zipEntry.__FileDataPosition = zipEntry.ArchiveStream.Position;
			readStream.Seek(zipEntry._CompressedFileDataSize + zipEntry._LengthOfTrailer, SeekOrigin.Current);
			HandleUnexpectedDataDescriptor(zipEntry);
			if (zipFile != null)
			{
				zipFile.OnReadBytes(zipEntry);
				zipFile.OnReadEntry(before: false, zipEntry);
			}
			return zipEntry;
		}

		internal static void HandlePK00Prefix(Stream s)
		{
			uint num = (uint)SharedUtilities.ReadInt(s);
			if (num != 808471376)
			{
				s.Seek(-4L, SeekOrigin.Current);
			}
		}

		private static void HandleUnexpectedDataDescriptor(ZipEntry entry)
		{
			Stream archiveStream = entry.ArchiveStream;
			uint num = (uint)SharedUtilities.ReadInt(archiveStream);
			if (num == entry._Crc32)
			{
				int num2 = SharedUtilities.ReadInt(archiveStream);
				if (num2 == entry._CompressedSize)
				{
					num2 = SharedUtilities.ReadInt(archiveStream);
					if (num2 != entry._UncompressedSize)
					{
						archiveStream.Seek(-12L, SeekOrigin.Current);
					}
				}
				else
				{
					archiveStream.Seek(-8L, SeekOrigin.Current);
				}
			}
			else
			{
				archiveStream.Seek(-4L, SeekOrigin.Current);
			}
		}

		internal static int FindExtraFieldSegment(byte[] extra, int offx, ushort targetHeaderId)
		{
			short num2;
			for (int i = offx; i + 3 < extra.Length; i += num2)
			{
				ushort num = (ushort)(extra[i++] + extra[i++] * 256);
				if (num == targetHeaderId)
				{
					return i - 2;
				}
				num2 = (short)(extra[i++] + extra[i++] * 256);
			}
			return -1;
		}

		internal int ProcessExtraField(Stream s, short extraFieldLength)
		{
			int num = 0;
			if (extraFieldLength > 0)
			{
				byte[] array = (_Extra = new byte[extraFieldLength]);
				num = s.Read(array, 0, array.Length);
				long posn = s.Position - num;
				int num2 = 0;
				while (num2 + 3 < array.Length)
				{
					int num3 = num2;
					ushort num4 = (ushort)(array[num2++] + array[num2++] * 256);
					short num5 = (short)(array[num2++] + array[num2++] * 256);
					switch (num4)
					{
					case 10:
						num2 = ProcessExtraFieldWindowsTimes(array, num2, num5, posn);
						break;
					case 21589:
						num2 = ProcessExtraFieldUnixTimes(array, num2, num5, posn);
						break;
					case 22613:
						num2 = ProcessExtraFieldInfoZipTimes(array, num2, num5, posn);
						break;
					case 1:
						num2 = ProcessExtraFieldZip64(array, num2, num5, posn);
						break;
					case 39169:
						num2 = ProcessExtraFieldWinZipAes(array, num2, num5, posn);
						break;
					case 23:
						num2 = ProcessExtraFieldPkwareStrongEncryption(array, num2);
						break;
					}
					num2 = num3 + num5 + 4;
				}
			}
			return num;
		}

		private int ProcessExtraFieldPkwareStrongEncryption(byte[] Buffer, int j)
		{
			j += 2;
			_UnsupportedAlgorithmId = (ushort)(Buffer[j++] + Buffer[j++] * 256);
			_Encryption_FromZipFile = (_Encryption = EncryptionAlgorithm.Unsupported);
			return j;
		}

		private int ProcessExtraFieldWinZipAes(byte[] buffer, int j, short dataSize, long posn)
		{
			if (_CompressionMethod == 99)
			{
				if ((_BitField & 1) != 1)
				{
					throw new BadReadException($"  Inconsistent metadata at position 0x{posn:X16}");
				}
				_sourceIsEncrypted = true;
				if (dataSize != 7)
				{
					throw new BadReadException($"  Inconsistent size (0x{dataSize:X4}) in WinZip AES field at position 0x{posn:X16}");
				}
				_WinZipAesMethod = BitConverter.ToInt16(buffer, j);
				j += 2;
				if (_WinZipAesMethod != 1 && _WinZipAesMethod != 2)
				{
					throw new BadReadException($"  Unexpected vendor version number (0x{_WinZipAesMethod:X4}) for WinZip AES metadata at position 0x{posn:X16}");
				}
				short num = BitConverter.ToInt16(buffer, j);
				j += 2;
				if (num != 17729)
				{
					throw new BadReadException($"  Unexpected vendor ID (0x{num:X4}) for WinZip AES metadata at position 0x{posn:X16}");
				}
				int num2 = ((buffer[j] == 1) ? 128 : ((buffer[j] == 3) ? 256 : (-1)));
				if (num2 < 0)
				{
					throw new BadReadException($"Invalid key strength ({num2})");
				}
				_Encryption_FromZipFile = (_Encryption = ((num2 == 128) ? EncryptionAlgorithm.WinZipAes128 : EncryptionAlgorithm.WinZipAes256));
				j++;
				_CompressionMethod_FromZipFile = (_CompressionMethod = BitConverter.ToInt16(buffer, j));
				j += 2;
			}
			return j;
		}

		private int ProcessExtraFieldZip64(byte[] buffer, int j, short dataSize, long posn)
		{
			_InputUsesZip64 = true;
			if (dataSize > 28)
			{
				throw new BadReadException($"  Inconsistent size (0x{dataSize:X4}) for ZIP64 extra field at position 0x{posn:X16}");
			}
			int remainingData = dataSize;
			Func<long> func = delegate
			{
				if (remainingData < 8)
				{
					throw new BadReadException($"  Missing data for ZIP64 extra field, position 0x{posn:X16}");
				}
				long result = BitConverter.ToInt64(buffer, j);
				j += 8;
				remainingData -= 8;
				return result;
			};
			if (_UncompressedSize == uint.MaxValue)
			{
				_UncompressedSize = func();
			}
			if (_CompressedSize == uint.MaxValue)
			{
				_CompressedSize = func();
			}
			if (_RelativeOffsetOfLocalHeader == uint.MaxValue)
			{
				_RelativeOffsetOfLocalHeader = func();
			}
			return j;
		}

		private int ProcessExtraFieldInfoZipTimes(byte[] buffer, int j, short dataSize, long posn)
		{
			if (dataSize != 12 && dataSize != 8)
			{
				throw new BadReadException($"  Unexpected size (0x{dataSize:X4}) for InfoZip v1 extra field at position 0x{posn:X16}");
			}
			int num = BitConverter.ToInt32(buffer, j);
			_Mtime = _unixEpoch.AddSeconds(num);
			j += 4;
			num = BitConverter.ToInt32(buffer, j);
			_Atime = _unixEpoch.AddSeconds(num);
			j += 4;
			_Ctime = DateTime.UtcNow;
			_ntfsTimesAreSet = true;
			_timestamp |= ZipEntryTimestamp.InfoZip1;
			return j;
		}

		private int ProcessExtraFieldUnixTimes(byte[] buffer, int j, short dataSize, long posn)
		{
			if (dataSize != 13 && dataSize != 9 && dataSize != 5)
			{
				throw new BadReadException($"  Unexpected size (0x{dataSize:X4}) for Extended Timestamp extra field at position 0x{posn:X16}");
			}
			int remainingData = dataSize;
			Func<DateTime> func = delegate
			{
				int num = BitConverter.ToInt32(buffer, j);
				j += 4;
				remainingData -= 4;
				return _unixEpoch.AddSeconds(num);
			};
			if (dataSize == 13 || _readExtraDepth > 0)
			{
				byte b = buffer[j++];
				remainingData--;
				if ((b & 1) != 0 && remainingData >= 4)
				{
					_Mtime = func();
				}
				_Atime = (((b & 2) != 0 && remainingData >= 4) ? func() : DateTime.UtcNow);
				_Ctime = (((b & 4) != 0 && remainingData >= 4) ? func() : DateTime.UtcNow);
				_timestamp |= ZipEntryTimestamp.Unix;
				_ntfsTimesAreSet = true;
				_emitUnixTimes = true;
			}
			else
			{
				ReadExtraField();
			}
			return j;
		}

		private int ProcessExtraFieldWindowsTimes(byte[] buffer, int j, short dataSize, long posn)
		{
			if (dataSize != 32)
			{
				throw new BadReadException($"  Unexpected size (0x{dataSize:X4}) for NTFS times extra field at position 0x{posn:X16}");
			}
			j += 4;
			short num = (short)(buffer[j] + buffer[j + 1] * 256);
			short num2 = (short)(buffer[j + 2] + buffer[j + 3] * 256);
			j += 4;
			if (num == 1 && num2 == 24)
			{
				long fileTime = BitConverter.ToInt64(buffer, j);
				_Mtime = DateTime.FromFileTimeUtc(fileTime);
				j += 8;
				fileTime = BitConverter.ToInt64(buffer, j);
				_Atime = DateTime.FromFileTimeUtc(fileTime);
				j += 8;
				fileTime = BitConverter.ToInt64(buffer, j);
				_Ctime = DateTime.FromFileTimeUtc(fileTime);
				j += 8;
				_ntfsTimesAreSet = true;
				_timestamp |= ZipEntryTimestamp.Windows;
				_emitNtfsTimes = true;
			}
			return j;
		}

		internal void WriteCentralDirectoryEntry(Stream s)
		{
			byte[] array = new byte[4096];
			int num = 0;
			array[num++] = 80;
			array[num++] = 75;
			array[num++] = 1;
			array[num++] = 2;
			array[num++] = (byte)(_VersionMadeBy & 0xFF);
			array[num++] = (byte)((_VersionMadeBy & 0xFF00) >> 8);
			short num2 = (short)((VersionNeeded != 0) ? VersionNeeded : 20);
			if (!_OutputUsesZip64.HasValue)
			{
				_OutputUsesZip64 = _container.Zip64 == Zip64Option.Always;
			}
			short num3 = (short)(_OutputUsesZip64.Value ? 45 : num2);
			if (CompressionMethod == CompressionMethod.BZip2)
			{
				num3 = 46;
			}
			array[num++] = (byte)(num3 & 0xFF);
			array[num++] = (byte)((num3 & 0xFF00) >> 8);
			array[num++] = (byte)(_BitField & 0xFF);
			array[num++] = (byte)((_BitField & 0xFF00) >> 8);
			array[num++] = (byte)(_CompressionMethod & 0xFF);
			array[num++] = (byte)((_CompressionMethod & 0xFF00) >> 8);
			if (Encryption == EncryptionAlgorithm.WinZipAes128 || Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				num -= 2;
				array[num++] = 99;
				array[num++] = 0;
			}
			array[num++] = (byte)(_TimeBlob & 0xFF);
			array[num++] = (byte)((_TimeBlob & 0xFF00) >> 8);
			array[num++] = (byte)((_TimeBlob & 0xFF0000) >> 16);
			array[num++] = (byte)((_TimeBlob & 0xFF000000u) >> 24);
			array[num++] = (byte)(_Crc32 & 0xFF);
			array[num++] = (byte)((_Crc32 & 0xFF00) >> 8);
			array[num++] = (byte)((_Crc32 & 0xFF0000) >> 16);
			array[num++] = (byte)((_Crc32 & 0xFF000000u) >> 24);
			int num4 = 0;
			if (_OutputUsesZip64.Value)
			{
				for (num4 = 0; num4 < 8; num4++)
				{
					array[num++] = byte.MaxValue;
				}
			}
			else
			{
				array[num++] = (byte)(_CompressedSize & 0xFF);
				array[num++] = (byte)((_CompressedSize & 0xFF00) >> 8);
				array[num++] = (byte)((_CompressedSize & 0xFF0000) >> 16);
				array[num++] = (byte)((_CompressedSize & 0xFF000000u) >> 24);
				array[num++] = (byte)(_UncompressedSize & 0xFF);
				array[num++] = (byte)((_UncompressedSize & 0xFF00) >> 8);
				array[num++] = (byte)((_UncompressedSize & 0xFF0000) >> 16);
				array[num++] = (byte)((_UncompressedSize & 0xFF000000u) >> 24);
			}
			byte[] encodedFileNameBytes = GetEncodedFileNameBytes();
			short num5 = (short)encodedFileNameBytes.Length;
			array[num++] = (byte)(num5 & 0xFF);
			array[num++] = (byte)((num5 & 0xFF00) >> 8);
			_presumeZip64 = _OutputUsesZip64.Value;
			_Extra = ConstructExtraField(forCentralDirectory: true);
			short num6 = (short)((_Extra != null) ? _Extra.Length : 0);
			array[num++] = (byte)(num6 & 0xFF);
			array[num++] = (byte)((num6 & 0xFF00) >> 8);
			int num7 = ((_CommentBytes != null) ? _CommentBytes.Length : 0);
			if (num7 + num > array.Length)
			{
				num7 = array.Length - num;
			}
			array[num++] = (byte)(num7 & 0xFF);
			array[num++] = (byte)((num7 & 0xFF00) >> 8);
			if (_container.ZipFile != null && _container.ZipFile.MaxOutputSegmentSize != 0)
			{
				array[num++] = (byte)(_diskNumber & 0xFF);
				array[num++] = (byte)((_diskNumber & 0xFF00) >> 8);
			}
			else
			{
				array[num++] = 0;
				array[num++] = 0;
			}
			array[num++] = (byte)(_IsText ? 1u : 0u);
			array[num++] = 0;
			array[num++] = (byte)(_ExternalFileAttrs & 0xFF);
			array[num++] = (byte)((_ExternalFileAttrs & 0xFF00) >> 8);
			array[num++] = (byte)((_ExternalFileAttrs & 0xFF0000) >> 16);
			array[num++] = (byte)((_ExternalFileAttrs & 0xFF000000u) >> 24);
			if (_RelativeOffsetOfLocalHeader > uint.MaxValue)
			{
				array[num++] = byte.MaxValue;
				array[num++] = byte.MaxValue;
				array[num++] = byte.MaxValue;
				array[num++] = byte.MaxValue;
			}
			else
			{
				array[num++] = (byte)(_RelativeOffsetOfLocalHeader & 0xFF);
				array[num++] = (byte)((_RelativeOffsetOfLocalHeader & 0xFF00) >> 8);
				array[num++] = (byte)((_RelativeOffsetOfLocalHeader & 0xFF0000) >> 16);
				array[num++] = (byte)((_RelativeOffsetOfLocalHeader & 0xFF000000u) >> 24);
			}
			Buffer.BlockCopy(encodedFileNameBytes, 0, array, num, num5);
			num += num5;
			if (_Extra != null)
			{
				byte[] extra = _Extra;
				int srcOffset = 0;
				Buffer.BlockCopy(extra, srcOffset, array, num, num6);
				num += num6;
			}
			if (num7 != 0)
			{
				Buffer.BlockCopy(_CommentBytes, 0, array, num, num7);
				num += num7;
			}
			s.Write(array, 0, num);
		}

		private byte[] ConstructExtraField(bool forCentralDirectory)
		{
			List<byte[]> list = new List<byte[]>();
			if (_container.Zip64 == Zip64Option.Always || (_container.Zip64 == Zip64Option.AsNecessary && (!forCentralDirectory || _entryRequiresZip64.Value)))
			{
				int num = 4 + (forCentralDirectory ? 28 : 16);
				byte[] array = new byte[num];
				int num2 = 0;
				if (_presumeZip64 || forCentralDirectory)
				{
					array[num2++] = 1;
					array[num2++] = 0;
				}
				else
				{
					array[num2++] = 153;
					array[num2++] = 153;
				}
				array[num2++] = (byte)(num - 4);
				array[num2++] = 0;
				Array.Copy(BitConverter.GetBytes(_UncompressedSize), 0, array, num2, 8);
				num2 += 8;
				Array.Copy(BitConverter.GetBytes(_CompressedSize), 0, array, num2, 8);
				num2 += 8;
				if (forCentralDirectory)
				{
					Array.Copy(BitConverter.GetBytes(_RelativeOffsetOfLocalHeader), 0, array, num2, 8);
					num2 += 8;
					Array.Copy(BitConverter.GetBytes(0), 0, array, num2, 4);
				}
				list.Add(array);
			}
			if (Encryption == EncryptionAlgorithm.WinZipAes128 || Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				byte[] array = new byte[11];
				int num3 = 0;
				array[num3++] = 1;
				array[num3++] = 153;
				array[num3++] = 7;
				array[num3++] = 0;
				array[num3++] = 1;
				array[num3++] = 0;
				array[num3++] = 65;
				array[num3++] = 69;
				switch (GetKeyStrengthInBits(Encryption))
				{
				case 128:
					array[num3] = 1;
					break;
				case 256:
					array[num3] = 3;
					break;
				default:
					array[num3] = byte.MaxValue;
					break;
				}
				num3++;
				array[num3++] = (byte)(_CompressionMethod & 0xFF);
				array[num3++] = (byte)(_CompressionMethod & 0xFF00);
				list.Add(array);
			}
			if (_ntfsTimesAreSet && _emitNtfsTimes)
			{
				byte[] array = new byte[36];
				int num4 = 0;
				array[num4++] = 10;
				array[num4++] = 0;
				array[num4++] = 32;
				array[num4++] = 0;
				num4 += 4;
				array[num4++] = 1;
				array[num4++] = 0;
				array[num4++] = 24;
				array[num4++] = 0;
				long value = _Mtime.ToFileTime();
				Array.Copy(BitConverter.GetBytes(value), 0, array, num4, 8);
				num4 += 8;
				value = _Atime.ToFileTime();
				Array.Copy(BitConverter.GetBytes(value), 0, array, num4, 8);
				num4 += 8;
				value = _Ctime.ToFileTime();
				Array.Copy(BitConverter.GetBytes(value), 0, array, num4, 8);
				num4 += 8;
				list.Add(array);
			}
			if (_ntfsTimesAreSet && _emitUnixTimes)
			{
				int num5 = 9;
				if (!forCentralDirectory)
				{
					num5 += 8;
				}
				byte[] array = new byte[num5];
				int num6 = 0;
				array[num6++] = 85;
				array[num6++] = 84;
				array[num6++] = (byte)(num5 - 4);
				array[num6++] = 0;
				array[num6++] = 7;
				int value2 = (int)(_Mtime - _unixEpoch).TotalSeconds;
				Array.Copy(BitConverter.GetBytes(value2), 0, array, num6, 4);
				num6 += 4;
				if (!forCentralDirectory)
				{
					value2 = (int)(_Atime - _unixEpoch).TotalSeconds;
					Array.Copy(BitConverter.GetBytes(value2), 0, array, num6, 4);
					num6 += 4;
					value2 = (int)(_Ctime - _unixEpoch).TotalSeconds;
					Array.Copy(BitConverter.GetBytes(value2), 0, array, num6, 4);
					num6 += 4;
				}
				list.Add(array);
			}
			byte[] array2 = null;
			if (list.Count > 0)
			{
				int num7 = 0;
				int num8 = 0;
				for (int i = 0; i < list.Count; i++)
				{
					num7 += list[i].Length;
				}
				array2 = new byte[num7];
				for (int i = 0; i < list.Count; i++)
				{
					Array.Copy(list[i], 0, array2, num8, list[i].Length);
					num8 += list[i].Length;
				}
			}
			return array2;
		}

		private string NormalizeFileName()
		{
			string text = FileName.Replace("\\", "/");
			string text2 = null;
			if (_TrimVolumeFromFullyQualifiedPaths && FileName.Length >= 3 && FileName[1] == ':' && text[2] == '/')
			{
				return text.Substring(3);
			}
			if (FileName.Length >= 4 && text[0] == '/' && text[1] == '/')
			{
				int num = text.IndexOf('/', 2);
				if (num == -1)
				{
					throw new ArgumentException("The path for that entry appears to be badly formatted");
				}
				return text.Substring(num + 1);
			}
			if (FileName.Length >= 3 && text[0] == '.' && text[1] == '/')
			{
				return text.Substring(2);
			}
			return text;
		}

		private byte[] GetEncodedFileNameBytes()
		{
			string text = NormalizeFileName();
			switch (AlternateEncodingUsage)
			{
			case ZipOption.Always:
				if (_Comment != null && _Comment.Length != 0)
				{
					_CommentBytes = AlternateEncoding.GetBytes(_Comment);
				}
				_actualEncoding = AlternateEncoding;
				return AlternateEncoding.GetBytes(text);
			case ZipOption.Default:
				if (_Comment != null && _Comment.Length != 0)
				{
					_CommentBytes = ibm437.GetBytes(_Comment);
				}
				_actualEncoding = ibm437;
				return ibm437.GetBytes(text);
			default:
			{
				byte[] bytes = ibm437.GetBytes(text);
				string text2 = ibm437.GetString(bytes, 0, bytes.Length);
				_CommentBytes = null;
				if (text2 != text)
				{
					bytes = AlternateEncoding.GetBytes(text);
					if (_Comment != null && _Comment.Length != 0)
					{
						_CommentBytes = AlternateEncoding.GetBytes(_Comment);
					}
					_actualEncoding = AlternateEncoding;
					return bytes;
				}
				_actualEncoding = ibm437;
				if (_Comment == null || _Comment.Length == 0)
				{
					return bytes;
				}
				byte[] bytes2 = ibm437.GetBytes(_Comment);
				string text3 = ibm437.GetString(bytes2, 0, bytes2.Length);
				if (text3 != Comment)
				{
					bytes = AlternateEncoding.GetBytes(text);
					_CommentBytes = AlternateEncoding.GetBytes(_Comment);
					_actualEncoding = AlternateEncoding;
					return bytes;
				}
				_CommentBytes = bytes2;
				return bytes;
			}
			}
		}

		private bool WantReadAgain()
		{
			if (_UncompressedSize < 16)
			{
				return false;
			}
			if (_CompressionMethod == 0)
			{
				return false;
			}
			if (CompressionLevel == CompressionLevel.None)
			{
				return false;
			}
			if (_CompressedSize < _UncompressedSize)
			{
				return false;
			}
			if (_Source == ZipEntrySource.Stream && !_sourceStream.CanSeek)
			{
				return false;
			}
			if (_aesCrypto_forWrite != null && CompressedSize - _aesCrypto_forWrite.SizeOfEncryptionMetadata <= UncompressedSize + 16)
			{
				return false;
			}
			if (_zipCrypto_forWrite != null && CompressedSize - 12 <= UncompressedSize)
			{
				return false;
			}
			return true;
		}

		private void MaybeUnsetCompressionMethodForWriting(int cycle)
		{
			if (cycle > 1)
			{
				_CompressionMethod = 0;
			}
			else if (IsDirectory)
			{
				_CompressionMethod = 0;
			}
			else
			{
				if (_Source == ZipEntrySource.ZipFile)
				{
					return;
				}
				if (_Source == ZipEntrySource.Stream)
				{
					if (_sourceStream != null && _sourceStream.CanSeek)
					{
						long length = _sourceStream.Length;
						if (length == 0)
						{
							_CompressionMethod = 0;
							return;
						}
					}
				}
				else if (_Source == ZipEntrySource.FileSystem && SharedUtilities.GetFileLength(LocalFileName) == 0)
				{
					_CompressionMethod = 0;
					return;
				}
				if (SetCompression != null)
				{
					CompressionLevel = SetCompression(LocalFileName, _FileNameInArchive);
				}
				if (CompressionLevel == CompressionLevel.None && CompressionMethod == CompressionMethod.Deflate)
				{
					_CompressionMethod = 0;
				}
			}
		}

		internal void WriteHeader(Stream s, int cycle)
		{
			_future_ROLH = (s as CountingStream)?.ComputedPosition ?? s.Position;
			int num = 0;
			int num2 = 0;
			byte[] array = new byte[30];
			array[num2++] = 80;
			array[num2++] = 75;
			array[num2++] = 3;
			array[num2++] = 4;
			_presumeZip64 = _container.Zip64 == Zip64Option.Always || (_container.Zip64 == Zip64Option.AsNecessary && !s.CanSeek);
			short num3 = (short)(_presumeZip64 ? 45 : 20);
			if (CompressionMethod == CompressionMethod.BZip2)
			{
				num3 = 46;
			}
			array[num2++] = (byte)(num3 & 0xFF);
			array[num2++] = (byte)((num3 & 0xFF00) >> 8);
			byte[] encodedFileNameBytes = GetEncodedFileNameBytes();
			short num4 = (short)encodedFileNameBytes.Length;
			if (_Encryption == EncryptionAlgorithm.None)
			{
				_BitField &= -2;
			}
			else
			{
				_BitField |= 1;
			}
			if (_actualEncoding.CodePage == Encoding.UTF8.CodePage)
			{
				_BitField |= 2048;
			}
			if (IsDirectory || cycle == 99)
			{
				_BitField &= -9;
				_BitField &= -2;
				Encryption = EncryptionAlgorithm.None;
				Password = null;
			}
			else if (!s.CanSeek)
			{
				_BitField |= 8;
			}
			array[num2++] = (byte)(_BitField & 0xFF);
			array[num2++] = (byte)((_BitField & 0xFF00) >> 8);
			if (__FileDataPosition == -1)
			{
				_CompressedSize = 0L;
				_crcCalculated = false;
			}
			MaybeUnsetCompressionMethodForWriting(cycle);
			array[num2++] = (byte)(_CompressionMethod & 0xFF);
			array[num2++] = (byte)((_CompressionMethod & 0xFF00) >> 8);
			if (cycle == 99)
			{
				SetZip64Flags();
			}
			else if (Encryption == EncryptionAlgorithm.WinZipAes128 || Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				num2 -= 2;
				array[num2++] = 99;
				array[num2++] = 0;
			}
			_TimeBlob = SharedUtilities.DateTimeToPacked(LastModified);
			array[num2++] = (byte)(_TimeBlob & 0xFF);
			array[num2++] = (byte)((_TimeBlob & 0xFF00) >> 8);
			array[num2++] = (byte)((_TimeBlob & 0xFF0000) >> 16);
			array[num2++] = (byte)((_TimeBlob & 0xFF000000u) >> 24);
			array[num2++] = (byte)(_Crc32 & 0xFF);
			array[num2++] = (byte)((_Crc32 & 0xFF00) >> 8);
			array[num2++] = (byte)((_Crc32 & 0xFF0000) >> 16);
			array[num2++] = (byte)((_Crc32 & 0xFF000000u) >> 24);
			if (_presumeZip64)
			{
				for (num = 0; num < 8; num++)
				{
					array[num2++] = byte.MaxValue;
				}
			}
			else
			{
				array[num2++] = (byte)(_CompressedSize & 0xFF);
				array[num2++] = (byte)((_CompressedSize & 0xFF00) >> 8);
				array[num2++] = (byte)((_CompressedSize & 0xFF0000) >> 16);
				array[num2++] = (byte)((_CompressedSize & 0xFF000000u) >> 24);
				array[num2++] = (byte)(_UncompressedSize & 0xFF);
				array[num2++] = (byte)((_UncompressedSize & 0xFF00) >> 8);
				array[num2++] = (byte)((_UncompressedSize & 0xFF0000) >> 16);
				array[num2++] = (byte)((_UncompressedSize & 0xFF000000u) >> 24);
			}
			array[num2++] = (byte)(num4 & 0xFF);
			array[num2++] = (byte)((num4 & 0xFF00) >> 8);
			_Extra = ConstructExtraField(forCentralDirectory: false);
			short num5 = (short)((_Extra != null) ? _Extra.Length : 0);
			array[num2++] = (byte)(num5 & 0xFF);
			array[num2++] = (byte)((num5 & 0xFF00) >> 8);
			byte[] array2 = new byte[num2 + num4 + num5];
			Buffer.BlockCopy(array, 0, array2, 0, num2);
			Buffer.BlockCopy(encodedFileNameBytes, 0, array2, num2, encodedFileNameBytes.Length);
			num2 += encodedFileNameBytes.Length;
			if (_Extra != null)
			{
				Buffer.BlockCopy(_Extra, 0, array2, num2, _Extra.Length);
				num2 += _Extra.Length;
			}
			_LengthOfHeader = num2;
			ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
			if (zipSegmentedStream != null)
			{
				zipSegmentedStream.ContiguousWrite = true;
				uint num6 = zipSegmentedStream.ComputeSegment(num2);
				if (num6 != zipSegmentedStream.CurrentSegment)
				{
					_future_ROLH = 0L;
				}
				else
				{
					_future_ROLH = zipSegmentedStream.Position;
				}
				_diskNumber = num6;
			}
			if (_container.Zip64 == Zip64Option.Default && (uint)_RelativeOffsetOfLocalHeader >= uint.MaxValue)
			{
				throw new ZipException("Offset within the zip archive exceeds 0xFFFFFFFF. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
			}
			s.Write(array2, 0, num2);
			if (zipSegmentedStream != null)
			{
				zipSegmentedStream.ContiguousWrite = false;
			}
			_EntryHeader = array2;
		}

		private int FigureCrc32()
		{
			if (!_crcCalculated)
			{
				Stream stream = null;
				if (_Source == ZipEntrySource.WriteDelegate)
				{
					CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(Stream.Null);
					_WriteDelegate(FileName, crcCalculatorStream);
					_Crc32 = crcCalculatorStream.Crc;
				}
				else if (_Source != ZipEntrySource.ZipFile)
				{
					if (_Source == ZipEntrySource.Stream)
					{
						PrepSourceStream();
						stream = _sourceStream;
					}
					else if (_Source == ZipEntrySource.JitStream)
					{
						if (_sourceStream == null)
						{
							_sourceStream = _OpenDelegate(FileName);
						}
						PrepSourceStream();
						stream = _sourceStream;
					}
					else if (_Source != ZipEntrySource.ZipOutputStream)
					{
						stream = File.Open(LocalFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					}
					CRC32 cRC = new CRC32();
					_Crc32 = cRC.GetCrc32(stream);
					if (_sourceStream == null)
					{
						stream.Dispose();
					}
				}
				_crcCalculated = true;
			}
			return _Crc32;
		}

		private void PrepSourceStream()
		{
			if (_sourceStream == null)
			{
				throw new ZipException($"The input stream is null for entry '{FileName}'.");
			}
			if (_sourceStreamOriginalPosition.HasValue)
			{
				_sourceStream.Position = _sourceStreamOriginalPosition.Value;
			}
			else if (_sourceStream.CanSeek)
			{
				_sourceStreamOriginalPosition = _sourceStream.Position;
			}
			else if (Encryption == EncryptionAlgorithm.PkzipWeak && _Source != ZipEntrySource.ZipFile && (_BitField & 8) != 8)
			{
				throw new ZipException("It is not possible to use PKZIP encryption on a non-seekable input stream");
			}
		}

		internal void CopyMetaData(ZipEntry source)
		{
			__FileDataPosition = source.__FileDataPosition;
			CompressionMethod = source.CompressionMethod;
			_CompressionMethod_FromZipFile = source._CompressionMethod_FromZipFile;
			_CompressedFileDataSize = source._CompressedFileDataSize;
			_UncompressedSize = source._UncompressedSize;
			_BitField = source._BitField;
			_Source = source._Source;
			_LastModified = source._LastModified;
			_Mtime = source._Mtime;
			_Atime = source._Atime;
			_Ctime = source._Ctime;
			_ntfsTimesAreSet = source._ntfsTimesAreSet;
			_emitUnixTimes = source._emitUnixTimes;
			_emitNtfsTimes = source._emitNtfsTimes;
		}

		private void OnWriteBlock(long bytesXferred, long totalBytesToXfer)
		{
			if (_container.ZipFile != null)
			{
				_ioOperationCanceled = _container.ZipFile.OnSaveBlock(this, bytesXferred, totalBytesToXfer);
			}
		}

		private void _WriteEntryData(Stream s)
		{
			Stream input = null;
			long _FileDataPosition = -1L;
			try
			{
				_FileDataPosition = s.Position;
			}
			catch (Exception)
			{
			}
			try
			{
				long num = SetInputAndFigureFileLength(ref input);
				CountingStream countingStream = new CountingStream(s);
				Stream stream;
				Stream stream2;
				if (num != 0)
				{
					stream = MaybeApplyEncryption(countingStream);
					stream2 = MaybeApplyCompression(stream, num);
				}
				else
				{
					stream = (stream2 = countingStream);
				}
				CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(stream2, leaveOpen: true);
				if (_Source == ZipEntrySource.WriteDelegate)
				{
					_WriteDelegate(FileName, crcCalculatorStream);
				}
				else
				{
					byte[] array = new byte[BufferSize];
					int count;
					while ((count = SharedUtilities.ReadWithRetry(input, array, 0, array.Length, FileName)) != 0)
					{
						crcCalculatorStream.Write(array, 0, count);
						OnWriteBlock(crcCalculatorStream.TotalBytesSlurped, num);
						if (_ioOperationCanceled)
						{
							break;
						}
					}
				}
				FinishOutputStream(s, countingStream, stream, stream2, crcCalculatorStream);
			}
			finally
			{
				if (_Source == ZipEntrySource.JitStream)
				{
					if (_CloseDelegate != null)
					{
						_CloseDelegate(FileName, input);
					}
				}
				else if (input is FileStream)
				{
					input.Dispose();
				}
			}
			if (!_ioOperationCanceled)
			{
				__FileDataPosition = _FileDataPosition;
				PostProcessOutput(s);
			}
		}

		private long SetInputAndFigureFileLength(ref Stream input)
		{
			long result = -1L;
			if (_Source == ZipEntrySource.Stream)
			{
				PrepSourceStream();
				input = _sourceStream;
				try
				{
					result = _sourceStream.Length;
				}
				catch (NotSupportedException)
				{
				}
			}
			else if (_Source == ZipEntrySource.ZipFile)
			{
				string password = ((_Encryption_FromZipFile == EncryptionAlgorithm.None) ? null : (_Password ?? _container.Password));
				_sourceStream = InternalOpenReader(password);
				PrepSourceStream();
				input = _sourceStream;
				result = _sourceStream.Length;
			}
			else if (_Source == ZipEntrySource.JitStream)
			{
				if (_sourceStream == null)
				{
					_sourceStream = _OpenDelegate(FileName);
				}
				PrepSourceStream();
				input = _sourceStream;
				try
				{
					result = _sourceStream.Length;
				}
				catch (NotSupportedException)
				{
				}
			}
			else if (_Source == ZipEntrySource.FileSystem)
			{
				FileShare fileShare = FileShare.ReadWrite;
				fileShare |= FileShare.Delete;
				input = File.Open(LocalFileName, FileMode.Open, FileAccess.Read, fileShare);
				result = input.Length;
			}
			return result;
		}

		internal void FinishOutputStream(Stream s, CountingStream entryCounter, Stream encryptor, Stream compressor, CrcCalculatorStream output)
		{
			if (output != null)
			{
				output.Close();
				if (compressor is DeflateStream)
				{
					compressor.Close();
				}
				else if (compressor is BZip2OutputStream)
				{
					compressor.Close();
				}
				else if (compressor is ParallelBZip2OutputStream)
				{
					compressor.Close();
				}
				else if (compressor is ParallelDeflateOutputStream)
				{
					compressor.Close();
				}
				encryptor.Flush();
				encryptor.Close();
				_LengthOfTrailer = 0;
				_UncompressedSize = output.TotalBytesSlurped;
				if (encryptor is WinZipAesCipherStream winZipAesCipherStream && _UncompressedSize > 0)
				{
					s.Write(winZipAesCipherStream.FinalAuthentication, 0, 10);
					_LengthOfTrailer += 10;
				}
				_CompressedFileDataSize = entryCounter.BytesWritten;
				_CompressedSize = _CompressedFileDataSize;
				_Crc32 = output.Crc;
				StoreRelativeOffset();
			}
		}

		internal void PostProcessOutput(Stream s)
		{
			CountingStream countingStream = s as CountingStream;
			if (_UncompressedSize == 0 && _CompressedSize == 0)
			{
				if (_Source == ZipEntrySource.ZipOutputStream)
				{
					return;
				}
				if (_Password != null)
				{
					int num = 0;
					if (Encryption == EncryptionAlgorithm.PkzipWeak)
					{
						num = 12;
					}
					else if (Encryption == EncryptionAlgorithm.WinZipAes128 || Encryption == EncryptionAlgorithm.WinZipAes256)
					{
						num = _aesCrypto_forWrite._Salt.Length + _aesCrypto_forWrite.GeneratedPV.Length;
					}
					if (_Source == ZipEntrySource.ZipOutputStream && !s.CanSeek)
					{
						throw new ZipException("Zero bytes written, encryption in use, and non-seekable output.");
					}
					if (Encryption != EncryptionAlgorithm.None)
					{
						s.Seek(-1 * num, SeekOrigin.Current);
						s.SetLength(s.Position);
						countingStream?.Adjust(num);
						_LengthOfHeader -= num;
						__FileDataPosition -= num;
					}
					_Password = null;
					_BitField &= -2;
					int num2 = 6;
					_EntryHeader[num2++] = (byte)(_BitField & 0xFF);
					_EntryHeader[num2++] = (byte)((_BitField & 0xFF00) >> 8);
					if (Encryption == EncryptionAlgorithm.WinZipAes128 || Encryption == EncryptionAlgorithm.WinZipAes256)
					{
						short num3 = (short)(_EntryHeader[26] + _EntryHeader[27] * 256);
						int offx = 30 + num3;
						int num4 = FindExtraFieldSegment(_EntryHeader, offx, 39169);
						if (num4 >= 0)
						{
							_EntryHeader[num4++] = 153;
							_EntryHeader[num4++] = 153;
						}
					}
				}
				CompressionMethod = CompressionMethod.None;
				Encryption = EncryptionAlgorithm.None;
			}
			else if (_zipCrypto_forWrite != null || _aesCrypto_forWrite != null)
			{
				if (Encryption == EncryptionAlgorithm.PkzipWeak)
				{
					_CompressedSize += 12L;
				}
				else if (Encryption == EncryptionAlgorithm.WinZipAes128 || Encryption == EncryptionAlgorithm.WinZipAes256)
				{
					_CompressedSize += _aesCrypto_forWrite.SizeOfEncryptionMetadata;
				}
			}
			int num5 = 8;
			_EntryHeader[num5++] = (byte)(_CompressionMethod & 0xFF);
			_EntryHeader[num5++] = (byte)((_CompressionMethod & 0xFF00) >> 8);
			num5 = 14;
			_EntryHeader[num5++] = (byte)(_Crc32 & 0xFF);
			_EntryHeader[num5++] = (byte)((_Crc32 & 0xFF00) >> 8);
			_EntryHeader[num5++] = (byte)((_Crc32 & 0xFF0000) >> 16);
			_EntryHeader[num5++] = (byte)((_Crc32 & 0xFF000000u) >> 24);
			SetZip64Flags();
			short num6 = (short)(_EntryHeader[26] + _EntryHeader[27] * 256);
			short num7 = (short)(_EntryHeader[28] + _EntryHeader[29] * 256);
			if (_OutputUsesZip64.Value)
			{
				_EntryHeader[4] = 45;
				_EntryHeader[5] = 0;
				for (int i = 0; i < 8; i++)
				{
					_EntryHeader[num5++] = byte.MaxValue;
				}
				num5 = 30 + num6;
				_EntryHeader[num5++] = 1;
				_EntryHeader[num5++] = 0;
				num5 += 2;
				Array.Copy(BitConverter.GetBytes(_UncompressedSize), 0, _EntryHeader, num5, 8);
				num5 += 8;
				Array.Copy(BitConverter.GetBytes(_CompressedSize), 0, _EntryHeader, num5, 8);
			}
			else
			{
				_EntryHeader[4] = 20;
				_EntryHeader[5] = 0;
				num5 = 18;
				_EntryHeader[num5++] = (byte)(_CompressedSize & 0xFF);
				_EntryHeader[num5++] = (byte)((_CompressedSize & 0xFF00) >> 8);
				_EntryHeader[num5++] = (byte)((_CompressedSize & 0xFF0000) >> 16);
				_EntryHeader[num5++] = (byte)((_CompressedSize & 0xFF000000u) >> 24);
				_EntryHeader[num5++] = (byte)(_UncompressedSize & 0xFF);
				_EntryHeader[num5++] = (byte)((_UncompressedSize & 0xFF00) >> 8);
				_EntryHeader[num5++] = (byte)((_UncompressedSize & 0xFF0000) >> 16);
				_EntryHeader[num5++] = (byte)((_UncompressedSize & 0xFF000000u) >> 24);
				if (num7 != 0)
				{
					num5 = 30 + num6;
					short num8 = (short)(_EntryHeader[num5 + 2] + _EntryHeader[num5 + 3] * 256);
					if (num8 == 16)
					{
						_EntryHeader[num5++] = 153;
						_EntryHeader[num5++] = 153;
					}
				}
			}
			if (Encryption == EncryptionAlgorithm.WinZipAes128 || Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				num5 = 8;
				_EntryHeader[num5++] = 99;
				_EntryHeader[num5++] = 0;
				num5 = 30 + num6;
				do
				{
					ushort num9 = (ushort)(_EntryHeader[num5] + _EntryHeader[num5 + 1] * 256);
					short num10 = (short)(_EntryHeader[num5 + 2] + _EntryHeader[num5 + 3] * 256);
					if (num9 != 39169)
					{
						num5 += num10 + 4;
						continue;
					}
					num5 += 9;
					_EntryHeader[num5++] = (byte)(_CompressionMethod & 0xFF);
					_EntryHeader[num5++] = (byte)(_CompressionMethod & 0xFF00);
				}
				while (num5 < num7 - 30 - num6);
			}
			if ((_BitField & 8) != 8 || (_Source == ZipEntrySource.ZipOutputStream && s.CanSeek))
			{
				if (s is ZipSegmentedStream zipSegmentedStream && _diskNumber != zipSegmentedStream.CurrentSegment)
				{
					using Stream stream = ZipSegmentedStream.ForUpdate(_container.ZipFile.Name, _diskNumber);
					stream.Seek(_RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
					stream.Write(_EntryHeader, 0, _EntryHeader.Length);
				}
				else
				{
					s.Seek(_RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
					s.Write(_EntryHeader, 0, _EntryHeader.Length);
					countingStream?.Adjust(_EntryHeader.Length);
					s.Seek(_CompressedSize, SeekOrigin.Current);
				}
			}
			if ((_BitField & 8) == 8 && !IsDirectory)
			{
				byte[] array = new byte[16 + (_OutputUsesZip64.Value ? 8 : 0)];
				num5 = 0;
				Array.Copy(BitConverter.GetBytes(134695760), 0, array, num5, 4);
				num5 += 4;
				Array.Copy(BitConverter.GetBytes(_Crc32), 0, array, num5, 4);
				num5 += 4;
				if (_OutputUsesZip64.Value)
				{
					Array.Copy(BitConverter.GetBytes(_CompressedSize), 0, array, num5, 8);
					num5 += 8;
					Array.Copy(BitConverter.GetBytes(_UncompressedSize), 0, array, num5, 8);
					num5 += 8;
				}
				else
				{
					array[num5++] = (byte)(_CompressedSize & 0xFF);
					array[num5++] = (byte)((_CompressedSize & 0xFF00) >> 8);
					array[num5++] = (byte)((_CompressedSize & 0xFF0000) >> 16);
					array[num5++] = (byte)((_CompressedSize & 0xFF000000u) >> 24);
					array[num5++] = (byte)(_UncompressedSize & 0xFF);
					array[num5++] = (byte)((_UncompressedSize & 0xFF00) >> 8);
					array[num5++] = (byte)((_UncompressedSize & 0xFF0000) >> 16);
					array[num5++] = (byte)((_UncompressedSize & 0xFF000000u) >> 24);
				}
				s.Write(array, 0, array.Length);
				_LengthOfTrailer += array.Length;
			}
		}

		private void SetZip64Flags()
		{
			_entryRequiresZip64 = _CompressedSize >= uint.MaxValue || _UncompressedSize >= uint.MaxValue || _RelativeOffsetOfLocalHeader >= uint.MaxValue;
			if (_container.Zip64 == Zip64Option.Default && _entryRequiresZip64.Value)
			{
				throw new ZipException("Compressed or Uncompressed size, or offset exceeds the maximum value. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
			}
			_OutputUsesZip64 = _container.Zip64 == Zip64Option.Always || _entryRequiresZip64.Value;
		}

		internal void PrepOutputStream(Stream s, long streamLength, out CountingStream outputCounter, out Stream encryptor, out Stream compressor, out CrcCalculatorStream output)
		{
			outputCounter = new CountingStream(s);
			if (streamLength != 0)
			{
				encryptor = MaybeApplyEncryption(outputCounter);
				compressor = MaybeApplyCompression(encryptor, streamLength);
			}
			else
			{
				encryptor = (compressor = outputCounter);
			}
			output = new CrcCalculatorStream(compressor, leaveOpen: true);
		}

		private Stream MaybeApplyCompression(Stream s, long streamLength)
		{
			if (_CompressionMethod == 8 && CompressionLevel != CompressionLevel.None)
			{
				if (_container.ParallelDeflateThreshold == 0 || (streamLength > _container.ParallelDeflateThreshold && _container.ParallelDeflateThreshold > 0))
				{
					if (_container.ParallelDeflater == null)
					{
						_container.ParallelDeflater = new ParallelDeflateOutputStream(s, CompressionLevel, _container.Strategy, leaveOpen: true);
						if (_container.CodecBufferSize > 0)
						{
							_container.ParallelDeflater.BufferSize = _container.CodecBufferSize;
						}
						if (_container.ParallelDeflateMaxBufferPairs > 0)
						{
							_container.ParallelDeflater.MaxBufferPairs = _container.ParallelDeflateMaxBufferPairs;
						}
					}
					ParallelDeflateOutputStream parallelDeflater = _container.ParallelDeflater;
					parallelDeflater.Reset(s);
					return parallelDeflater;
				}
				DeflateStream deflateStream = new DeflateStream(s, CompressionMode.Compress, CompressionLevel, leaveOpen: true);
				if (_container.CodecBufferSize > 0)
				{
					deflateStream.BufferSize = _container.CodecBufferSize;
				}
				deflateStream.Strategy = _container.Strategy;
				return deflateStream;
			}
			if (_CompressionMethod == 12)
			{
				if (_container.ParallelDeflateThreshold == 0 || (streamLength > _container.ParallelDeflateThreshold && _container.ParallelDeflateThreshold > 0))
				{
					return new ParallelBZip2OutputStream(s, leaveOpen: true);
				}
				return new BZip2OutputStream(s, leaveOpen: true);
			}
			return s;
		}

		private Stream MaybeApplyEncryption(Stream s)
		{
			if (Encryption == EncryptionAlgorithm.PkzipWeak)
			{
				return new ZipCipherStream(s, _zipCrypto_forWrite, CryptoMode.Encrypt);
			}
			if (Encryption == EncryptionAlgorithm.WinZipAes128 || Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				return new WinZipAesCipherStream(s, _aesCrypto_forWrite, CryptoMode.Encrypt);
			}
			return s;
		}

		private void OnZipErrorWhileSaving(Exception e)
		{
			if (_container.ZipFile != null)
			{
				_ioOperationCanceled = _container.ZipFile.OnZipErrorSaving(this, e);
			}
		}

		internal void Write(Stream s)
		{
			CountingStream countingStream = s as CountingStream;
			ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
			bool flag = false;
			do
			{
				try
				{
					if (_Source == ZipEntrySource.ZipFile && !_restreamRequiredOnSave)
					{
						CopyThroughOneEntry(s);
						break;
					}
					if (IsDirectory)
					{
						WriteHeader(s, 1);
						StoreRelativeOffset();
						_entryRequiresZip64 = _RelativeOffsetOfLocalHeader >= uint.MaxValue;
						_OutputUsesZip64 = _container.Zip64 == Zip64Option.Always || _entryRequiresZip64.Value;
						if (zipSegmentedStream != null)
						{
							_diskNumber = zipSegmentedStream.CurrentSegment;
						}
						break;
					}
					bool flag2 = true;
					int num = 0;
					do
					{
						num++;
						WriteHeader(s, num);
						WriteSecurityMetadata(s);
						_WriteEntryData(s);
						_TotalEntrySize = _LengthOfHeader + _CompressedFileDataSize + _LengthOfTrailer;
						flag2 = num <= 1 && s.CanSeek && WantReadAgain();
						if (flag2)
						{
							if (zipSegmentedStream != null)
							{
								zipSegmentedStream.TruncateBackward(_diskNumber, _RelativeOffsetOfLocalHeader);
							}
							else
							{
								s.Seek(_RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
							}
							s.SetLength(s.Position);
							countingStream?.Adjust(_TotalEntrySize);
						}
					}
					while (flag2);
					_skippedDuringSave = false;
					flag = true;
				}
				catch (Exception ex)
				{
					ZipErrorAction zipErrorAction = ZipErrorAction;
					int num2 = 0;
					while (true)
					{
						if (ZipErrorAction == ZipErrorAction.Throw)
						{
							throw;
						}
						if (ZipErrorAction == ZipErrorAction.Skip || ZipErrorAction == ZipErrorAction.Retry)
						{
							long num3 = countingStream?.ComputedPosition ?? s.Position;
							long num4 = num3 - _future_ROLH;
							if (num4 > 0)
							{
								s.Seek(num4, SeekOrigin.Current);
								long position = s.Position;
								s.SetLength(s.Position);
								countingStream?.Adjust(num3 - position);
							}
							if (ZipErrorAction == ZipErrorAction.Skip)
							{
								WriteStatus("Skipping file {0} (exception: {1})", LocalFileName, ex.ToString());
								_skippedDuringSave = true;
								flag = true;
							}
							else
							{
								ZipErrorAction = zipErrorAction;
							}
							break;
						}
						if (num2 > 0)
						{
							throw;
						}
						if (ZipErrorAction == ZipErrorAction.InvokeErrorEvent)
						{
							OnZipErrorWhileSaving(ex);
							if (_ioOperationCanceled)
							{
								flag = true;
								break;
							}
						}
						num2++;
					}
				}
			}
			while (!flag);
		}

		internal void StoreRelativeOffset()
		{
			_RelativeOffsetOfLocalHeader = _future_ROLH;
		}

		internal void NotifySaveComplete()
		{
			_Encryption_FromZipFile = _Encryption;
			_CompressionMethod_FromZipFile = _CompressionMethod;
			_restreamRequiredOnSave = false;
			_metadataChanged = false;
			_Source = ZipEntrySource.ZipFile;
		}

		internal void WriteSecurityMetadata(Stream outstream)
		{
			if (Encryption == EncryptionAlgorithm.None)
			{
				return;
			}
			string password = _Password;
			if (_Source == ZipEntrySource.ZipFile && password == null)
			{
				password = _container.Password;
			}
			if (password == null)
			{
				_zipCrypto_forWrite = null;
				_aesCrypto_forWrite = null;
			}
			else if (Encryption == EncryptionAlgorithm.PkzipWeak)
			{
				_zipCrypto_forWrite = ZipCrypto.ForWrite(password);
				Random random = new Random();
				byte[] array = new byte[12];
				random.NextBytes(array);
				if ((_BitField & 8) == 8)
				{
					_TimeBlob = SharedUtilities.DateTimeToPacked(LastModified);
					array[11] = (byte)((_TimeBlob >> 8) & 0xFF);
				}
				else
				{
					FigureCrc32();
					array[11] = (byte)((_Crc32 >> 24) & 0xFF);
				}
				byte[] array2 = _zipCrypto_forWrite.EncryptMessage(array, array.Length);
				outstream.Write(array2, 0, array2.Length);
				_LengthOfHeader += array2.Length;
			}
			else if (Encryption == EncryptionAlgorithm.WinZipAes128 || Encryption == EncryptionAlgorithm.WinZipAes256)
			{
				int keyStrengthInBits = GetKeyStrengthInBits(Encryption);
				_aesCrypto_forWrite = WinZipAesCrypto.Generate(password, keyStrengthInBits);
				outstream.Write(_aesCrypto_forWrite.Salt, 0, _aesCrypto_forWrite._Salt.Length);
				outstream.Write(_aesCrypto_forWrite.GeneratedPV, 0, _aesCrypto_forWrite.GeneratedPV.Length);
				_LengthOfHeader += _aesCrypto_forWrite._Salt.Length + _aesCrypto_forWrite.GeneratedPV.Length;
			}
		}

		private void CopyThroughOneEntry(Stream outStream)
		{
			if (LengthOfHeader == 0)
			{
				throw new BadStateException("Bad header length.");
			}
			if (_metadataChanged || ArchiveStream is ZipSegmentedStream || outStream is ZipSegmentedStream || (_InputUsesZip64 && _container.UseZip64WhenSaving == Zip64Option.Default) || (!_InputUsesZip64 && _container.UseZip64WhenSaving == Zip64Option.Always))
			{
				CopyThroughWithRecompute(outStream);
			}
			else
			{
				CopyThroughWithNoChange(outStream);
			}
			_entryRequiresZip64 = _CompressedSize >= uint.MaxValue || _UncompressedSize >= uint.MaxValue || _RelativeOffsetOfLocalHeader >= uint.MaxValue;
			_OutputUsesZip64 = _container.Zip64 == Zip64Option.Always || _entryRequiresZip64.Value;
		}

		private void CopyThroughWithRecompute(Stream outstream)
		{
			byte[] array = new byte[BufferSize];
			CountingStream countingStream = new CountingStream(ArchiveStream);
			long relativeOffsetOfLocalHeader = _RelativeOffsetOfLocalHeader;
			int lengthOfHeader = LengthOfHeader;
			WriteHeader(outstream, 0);
			StoreRelativeOffset();
			if (!FileName.EndsWith("/"))
			{
				long num = relativeOffsetOfLocalHeader + lengthOfHeader;
				int lengthOfCryptoHeaderBytes = GetLengthOfCryptoHeaderBytes(_Encryption_FromZipFile);
				num -= lengthOfCryptoHeaderBytes;
				_LengthOfHeader += lengthOfCryptoHeaderBytes;
				countingStream.Seek(num, SeekOrigin.Begin);
				long num2 = _CompressedSize;
				while (num2 > 0)
				{
					lengthOfCryptoHeaderBytes = (int)((num2 > array.Length) ? array.Length : num2);
					int num3 = countingStream.Read(array, 0, lengthOfCryptoHeaderBytes);
					outstream.Write(array, 0, num3);
					num2 -= num3;
					OnWriteBlock(countingStream.BytesRead, _CompressedSize);
					if (_ioOperationCanceled)
					{
						break;
					}
				}
				if ((_BitField & 8) == 8)
				{
					int num4 = 16;
					if (_InputUsesZip64)
					{
						num4 += 8;
					}
					byte[] buffer = new byte[num4];
					countingStream.Read(buffer, 0, num4);
					if (_InputUsesZip64 && _container.UseZip64WhenSaving == Zip64Option.Default)
					{
						outstream.Write(buffer, 0, 8);
						if (_CompressedSize > uint.MaxValue)
						{
							throw new InvalidOperationException("ZIP64 is required");
						}
						outstream.Write(buffer, 8, 4);
						if (_UncompressedSize > uint.MaxValue)
						{
							throw new InvalidOperationException("ZIP64 is required");
						}
						outstream.Write(buffer, 16, 4);
						_LengthOfTrailer -= 8;
					}
					else if (!_InputUsesZip64 && _container.UseZip64WhenSaving == Zip64Option.Always)
					{
						byte[] buffer2 = new byte[4];
						outstream.Write(buffer, 0, 8);
						outstream.Write(buffer, 8, 4);
						outstream.Write(buffer2, 0, 4);
						outstream.Write(buffer, 12, 4);
						outstream.Write(buffer2, 0, 4);
						_LengthOfTrailer += 8;
					}
					else
					{
						outstream.Write(buffer, 0, num4);
					}
				}
			}
			_TotalEntrySize = _LengthOfHeader + _CompressedFileDataSize + _LengthOfTrailer;
		}

		private void CopyThroughWithNoChange(Stream outstream)
		{
			byte[] array = new byte[BufferSize];
			CountingStream countingStream = new CountingStream(ArchiveStream);
			countingStream.Seek(_RelativeOffsetOfLocalHeader, SeekOrigin.Begin);
			if (_TotalEntrySize == 0)
			{
				_TotalEntrySize = _LengthOfHeader + _CompressedFileDataSize + _LengthOfTrailer;
			}
			_RelativeOffsetOfLocalHeader = (outstream as CountingStream)?.ComputedPosition ?? outstream.Position;
			long num = _TotalEntrySize;
			while (num > 0)
			{
				int count = (int)((num > array.Length) ? array.Length : num);
				int num2 = countingStream.Read(array, 0, count);
				outstream.Write(array, 0, num2);
				num -= num2;
				OnWriteBlock(countingStream.BytesRead, _TotalEntrySize);
				if (_ioOperationCanceled)
				{
					break;
				}
			}
		}

		[Conditional("Trace")]
		private void TraceWriteLine(string format, params object[] varParams)
		{
			lock (_outputLock)
			{
				int hashCode = Thread.CurrentThread.GetHashCode();
				Console.ForegroundColor = (ConsoleColor)(hashCode % 8 + 8);
				Console.Write("{0:000} ZipEntry.Write ", hashCode);
				Console.WriteLine(format, varParams);
				Console.ResetColor();
			}
		}
	}
	[Flags]
	public enum ZipEntryTimestamp
	{
		None = 0,
		DOS = 1,
		Windows = 2,
		Unix = 4,
		InfoZip1 = 8
	}
	public enum CompressionMethod
	{
		None = 0,
		Deflate = 8,
		BZip2 = 12
	}
	public enum ZipEntrySource
	{
		None,
		FileSystem,
		Stream,
		ZipFile,
		WriteDelegate,
		JitStream,
		ZipOutputStream
	}
	public enum ZipErrorAction
	{
		Throw,
		Skip,
		Retry,
		InvokeErrorEvent
	}
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00005")]
	[ComVisible(true)]
	public class ZipFile : IEnumerable<ZipEntry>, IEnumerable, IDisposable
	{
		private class ExtractorSettings
		{
			public SelfExtractorFlavor Flavor;

			public List<string> ReferencedAssemblies;

			public List<string> CopyThroughResources;

			public List<string> ResourcesToCompile;
		}

		private TextWriter _StatusMessageTextWriter;

		private bool _CaseSensitiveRetrieval;

		private Stream _readstream;

		private Stream _writestream;

		private ushort _versionMadeBy;

		private ushort _versionNeededToExtract;

		private uint _diskNumberWithCd;

		private int _maxOutputSegmentSize;

		private uint _numberOfSegmentsForMostRecentSave;

		private ZipErrorAction _zipErrorAction;

		private bool _disposed;

		private Dictionary<string, ZipEntry> _entries;

		private List<ZipEntry> _zipEntriesAsList;

		private string _name;

		private string _readName;

		private string _Comment;

		internal string _Password;

		private bool _emitNtfsTimes = true;

		private bool _emitUnixTimes;

		private CompressionStrategy _Strategy;

		private CompressionMethod _compressionMethod = CompressionMethod.Deflate;

		private bool _fileAlreadyExists;

		private string _temporaryFileName;

		private bool _contentsChanged;

		private bool _hasBeenSaved;

		private string _TempFileFolder;

		private bool _ReadStreamIsOurs = true;

		private object LOCK = new object();

		private bool _saveOperationCanceled;

		private bool _extractOperationCanceled;

		private bool _addOperationCanceled;

		private EncryptionAlgorithm _Encryption;

		private bool _JustSaved;

		private long _locEndOfCDS = -1L;

		private uint _OffsetOfCentralDirectory;

		private long _OffsetOfCentralDirectory64;

		private bool? _OutputUsesZip64;

		internal bool _inExtractAll;

		private Encoding _alternateEncoding = Encoding.GetEncoding("IBM437");

		private ZipOption _alternateEncodingUsage;

		private static Encoding _defaultEncoding = Encoding.GetEncoding("IBM437");

		private int _BufferSize = BufferSizeDefault;

		internal ParallelDeflateOutputStream ParallelDeflater;

		private long _ParallelDeflateThreshold;

		private int _maxBufferPairs = 16;

		internal Zip64Option _zip64;

		private bool _SavingSfx;

		public static readonly int BufferSizeDefault = 32768;

		private long _lengthOfReadStream = -99L;

		private static ExtractorSettings[] SettingsList = new ExtractorSettings[2]
		{
			new ExtractorSettings
			{
				Flavor = SelfExtractorFlavor.WinFormsApplication,
				ReferencedAssemblies = new List<string> { "System.dll", "System.Windows.Forms.dll", "System.Drawing.dll" },
				CopyThroughResources = new List<string> { "Ionic.Zip.WinFormsSelfExtractorStub.resources", "Ionic.Zip.Forms.PasswordDialog.resources", "Ionic.Zip.Forms.ZipContentsDialog.resources" },
				ResourcesToCompile = new List<string> { "WinFormsSelfExtractorStub.cs", "WinFormsSelfExtractorStub.Designer.cs", "PasswordDialog.cs", "PasswordDialog.Designer.cs", "ZipContentsDialog.cs", "ZipContentsDialog.Designer.cs", "FolderBrowserDialogEx.cs" }
			},
			new ExtractorSettings
			{
				Flavor = SelfExtractorFlavor.ConsoleApplication,
				ReferencedAssemblies = new List<string> { "System.dll" },
				CopyThroughResources = null,
				ResourcesToCompile = new List<string> { "CommandLineSelfExtractorStub.cs" }
			}
		};

		public string Info
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append($"          ZipFile: {Name}\n");
				if (!string.IsNullOrEmpty(_Comment))
				{
					stringBuilder.Append($"          Comment: {_Comment}\n");
				}
				if (_versionMadeBy != 0)
				{
					stringBuilder.Append($"  version made by: 0x{_versionMadeBy:X4}\n");
				}
				if (_versionNeededToExtract != 0)
				{
					stringBuilder.Append($"needed to extract: 0x{_versionNeededToExtract:X4}\n");
				}
				stringBuilder.Append($"       uses ZIP64: {InputUsesZip64}\n");
				stringBuilder.Append($"     disk with CD: {_diskNumberWithCd}\n");
				if (_OffsetOfCentralDirectory == uint.MaxValue)
				{
					stringBuilder.Append($"      CD64 offset: 0x{_OffsetOfCentralDirectory64:X16}\n");
				}
				else
				{
					stringBuilder.Append($"        CD offset: 0x{_OffsetOfCentralDirectory:X8}\n");
				}
				stringBuilder.Append("\n");
				foreach (ZipEntry value in _entries.Values)
				{
					stringBuilder.Append(value.Info);
				}
				return stringBuilder.ToString();
			}
		}

		public bool FullScan { get; set; }

		public bool SortEntriesBeforeSaving { get; set; }

		public bool AddDirectoryWillTraverseReparsePoints { get; set; }

		public int BufferSize
		{
			get
			{
				return _BufferSize;
			}
			set
			{
				_BufferSize = value;
			}
		}

		public int CodecBufferSize { get; set; }

		public bool FlattenFoldersOnExtract { get; set; }

		public CompressionStrategy Strategy
		{
			get
			{
				return _Strategy;
			}
			set
			{
				_Strategy = value;
			}
		}

		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		public CompressionLevel CompressionLevel { get; set; }

		public CompressionMethod CompressionMethod
		{
			get
			{
				return _compressionMethod;
			}
			set
			{
				_compressionMethod = value;
			}
		}

		public string Comment
		{
			get
			{
				return _Comment;
			}
			set
			{
				_Comment = value;
				_contentsChanged = true;
			}
		}

		public bool EmitTimesInWindowsFormatWhenSaving
		{
			get
			{
				return _emitNtfsTimes;
			}
			set
			{
				_emitNtfsTimes = value;
			}
		}

		public bool EmitTimesInUnixFormatWhenSaving
		{
			get
			{
				return _emitUnixTimes;
			}
			set
			{
				_emitUnixTimes = value;
			}
		}

		internal bool Verbose => _StatusMessageTextWriter != null;

		public bool CaseSensitiveRetrieval
		{
			get
			{
				return _CaseSensitiveRetrieval;
			}
			set
			{
				if (value != _CaseSensitiveRetrieval)
				{
					_CaseSensitiveRetrieval = value;
					_initEntriesDictionary();
				}
			}
		}

		[Obsolete("Beginning with v1.9.1.6 of DotNetZip, this property is obsolete.  It will be removed in a future version of the library. Your applications should  use AlternateEncoding and AlternateEncodingUsage instead.")]
		public bool UseUnicodeAsNecessary
		{
			get
			{
				if (_alternateEncoding == Encoding.GetEncoding("UTF-8"))
				{
					return _alternateEncodingUsage == ZipOption.AsNecessary;
				}
				return false;
			}
			set
			{
				if (value)
				{
					_alternateEncoding = Encoding.GetEncoding("UTF-8");
					_alternateEncodingUsage = ZipOption.AsNecessary;
				}
				else
				{
					_alternateEncoding = DefaultEncoding;
					_alternateEncodingUsage = ZipOption.Default;
				}
			}
		}

		public Zip64Option UseZip64WhenSaving
		{
			get
			{
				return _zip64;
			}
			set
			{
				_zip64 = value;
			}
		}

		public bool? RequiresZip64
		{
			get
			{
				if (_entries.Count > 65534)
				{
					return true;
				}
				if (!_hasBeenSaved || _contentsChanged)
				{
					return null;
				}
				foreach (ZipEntry value in _entries.Values)
				{
					if (value.RequiresZip64.Value)
					{
						return true;
					}
				}
				return false;
			}
		}

		public bool? OutputUsedZip64 => _OutputUsesZip64;

		public bool? InputUsesZip64
		{
			get
			{
				if (_entries.Count > 65534)
				{
					return true;
				}
				using (IEnumerator<ZipEntry> enumerator = GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ZipEntry current = enumerator.Current;
						if (current.Source != ZipEntrySource.ZipFile)
						{
							return null;
						}
						if (current._InputUsesZip64)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		[Obsolete("use AlternateEncoding instead.")]
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				if (_alternateEncodingUsage == ZipOption.AsNecessary)
				{
					return _alternateEncoding;
				}
				return null;
			}
			set
			{
				_alternateEncoding = value;
				_alternateEncodingUsage = ZipOption.AsNecessary;
			}
		}

		public Encoding AlternateEncoding
		{
			get
			{
				return _alternateEncoding;
			}
			set
			{
				_alternateEncoding = value;
			}
		}

		public ZipOption AlternateEncodingUsage
		{
			get
			{
				return _alternateEncodingUsage;
			}
			set
			{
				_alternateEncodingUsage = value;
			}
		}

		public static Encoding DefaultEncoding => _defaultEncoding;

		public TextWriter StatusMessageTextWriter
		{
			get
			{
				return _StatusMessageTextWriter;
			}
			set
			{
				_StatusMessageTextWriter = value;
			}
		}

		public string TempFileFolder
		{
			get
			{
				return _TempFileFolder;
			}
			set
			{
				_TempFileFolder = value;
				if (value == null || Directory.Exists(value))
				{
					return;
				}
				throw new FileNotFoundException($"That directory ({value}) does not exist.");
			}
		}

		public string Password
		{
			private get
			{
				return _Password;
			}
			set
			{
				_Password = value;
				if (_Password == null)
				{
					Encryption = EncryptionAlgorithm.None;
				}
				else if (Encryption == EncryptionAlgorithm.None)
				{
					Encryption = EncryptionAlgorithm.PkzipWeak;
				}
			}
		}

		public ExtractExistingFileAction ExtractExistingFile { get; set; }

		public ZipErrorAction ZipErrorAction
		{
			get
			{
				if (this.ZipError != null)
				{
					_zipErrorAction = ZipErrorAction.InvokeErrorEvent;
				}
				return _zipErrorAction;
			}
			set
			{
				_zipErrorAction = value;
				if (_zipErrorAction != ZipErrorAction.InvokeErrorEvent && this.ZipError != null)
				{
					this.ZipError = null;
				}
			}
		}

		public EncryptionAlgorithm Encryption
		{
			get
			{
				return _Encryption;
			}
			set
			{
				if (value == EncryptionAlgorithm.Unsupported)
				{
					throw new InvalidOperationException("You may not set Encryption to that value.");
				}
				_Encryption = value;
			}
		}

		public SetCompressionCallback SetCompression { get; set; }

		public int MaxOutputSegmentSize
		{
			get
			{
				return _maxOutputSegmentSize;
			}
			set
			{
				if (value < 65536 && value != 0)
				{
					throw new ZipException("The minimum acceptable segment size is 65536.");
				}
				_maxOutputSegmentSize = value;
			}
		}

		public int NumberOfSegmentsForMostRecentSave => (int)(_numberOfSegmentsForMostRecentSave + 1);

		public long ParallelDeflateThreshold
		{
			get
			{
				return _ParallelDeflateThreshold;
			}
			set
			{
				if (value != 0 && value != -1 && value < 65536)
				{
					throw new ArgumentOutOfRangeException("ParallelDeflateThreshold should be -1, 0, or > 65536");
				}
				_ParallelDeflateThreshold = value;
			}
		}

		public int ParallelDeflateMaxBufferPairs
		{
			get
			{
				return _maxBufferPairs;
			}
			set
			{
				if (value < 4)
				{
					throw new ArgumentOutOfRangeException("ParallelDeflateMaxBufferPairs", "Value must be 4 or greater.");
				}
				_maxBufferPairs = value;
			}
		}

		public static Version LibraryVersion => Assembly.GetExecutingAssembly().GetName().Version;

		private List<ZipEntry> ZipEntriesAsList
		{
			get
			{
				if (_zipEntriesAsList == null)
				{
					_zipEntriesAsList = new List<ZipEntry>(_entries.Values);
				}
				return _zipEntriesAsList;
			}
		}

		public ZipEntry this[int ix] => ZipEntriesAsList[ix];

		public ZipEntry this[string fileName]
		{
			get
			{
				string text = SharedUtilities.NormalizePathForUseInZipFile(fileName);
				if (_entries.ContainsKey(text))
				{
					return _entries[text];
				}
				text = text.Replace("/", "\\");
				if (_entries.ContainsKey(text))
				{
					return _entries[text];
				}
				return null;
			}
		}

		public ICollection<string> EntryFileNames => _entries.Keys;

		public ICollection<ZipEntry> Entries => _entries.Values;

		public ICollection<ZipEntry> EntriesSorted
		{
			get
			{
				List<ZipEntry> list = new List<ZipEntry>();
				foreach (ZipEntry entry in Entries)
				{
					list.Add(entry);
				}
				StringComparison sc = (CaseSensitiveRetrieval ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
				list.Sort((ZipEntry x, ZipEntry y) => string.Compare(x.FileName, y.FileName, sc));
				return list.AsReadOnly();
			}
		}

		public int Count => _entries.Count;

		internal Stream ReadStream
		{
			get
			{
				if (_readstream == null && (_readName != null || _name != null))
				{
					_readstream = File.Open(_readName ?? _name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					_ReadStreamIsOurs = true;
				}
				return _readstream;
			}
		}

		private Stream WriteStream
		{
			get
			{
				if (_writestream != null)
				{
					return _writestream;
				}
				if (_name == null)
				{
					return _writestream;
				}
				if (_maxOutputSegmentSize != 0)
				{
					_writestream = ZipSegmentedStream.ForWriting(_name, _maxOutputSegmentSize);
					return _writestream;
				}
				SharedUtilities.CreateAndOpenUniqueTempFile(TempFileFolder ?? Path.GetDirectoryName(_name), out _writestream, out _temporaryFileName);
				return _writestream;
			}
			set
			{
				if (value != null)
				{
					throw new ZipException("Cannot set the stream to a non-null value.");
				}
				_writestream = null;
			}
		}

		private string ArchiveNameForEvent
		{
			get
			{
				if (_name == null)
				{
					return "(stream)";
				}
				return _name;
			}
		}

		private long LengthOfReadStream
		{
			get
			{
				if (_lengthOfReadStream == -99)
				{
					_lengthOfReadStream = (_ReadStreamIsOurs ? SharedUtilities.GetFileLength(_name) : (-1));
				}
				return _lengthOfReadStream;
			}
		}

		public event EventHandler<SaveProgressEventArgs> SaveProgress;

		public event EventHandler<ReadProgressEventArgs> ReadProgress;

		public event EventHandler<ExtractProgressEventArgs> ExtractProgress;

		public event EventHandler<AddProgressEventArgs> AddProgress;

		public event EventHandler<ZipErrorEventArgs> ZipError;

		public ZipEntry AddItem(string fileOrDirectoryName)
		{
			return AddItem(fileOrDirectoryName, null);
		}

		public ZipEntry AddItem(string fileOrDirectoryName, string directoryPathInArchive)
		{
			if (File.Exists(fileOrDirectoryName))
			{
				return AddFile(fileOrDirectoryName, directoryPathInArchive);
			}
			if (Directory.Exists(fileOrDirectoryName))
			{
				return AddDirectory(fileOrDirectoryName, directoryPathInArchive);
			}
			throw new FileNotFoundException($"That file or directory ({fileOrDirectoryName}) does not exist!");
		}

		public ZipEntry AddFile(string fileName)
		{
			return AddFile(fileName, null);
		}

		public ZipEntry AddFile(string fileName, string directoryPathInArchive)
		{
			string nameInArchive = ZipEntry.NameInArchive(fileName, directoryPathInArchive);
			ZipEntry ze = ZipEntry.CreateFromFile(fileName, nameInArchive);
			if (Verbose)
			{
				StatusMessageTextWriter.WriteLine("adding {0}...", fileName);
			}
			return _InternalAddEntry(ze);
		}

		public void RemoveEntries(ICollection<ZipEntry> entriesToRemove)
		{
			if (entriesToRemove == null)
			{
				throw new ArgumentNullException("entriesToRemove");
			}
			foreach (ZipEntry item in entriesToRemove)
			{
				RemoveEntry(item);
			}
		}

		public void RemoveEntries(ICollection<string> entriesToRemove)
		{
			if (entriesToRemove == null)
			{
				throw new ArgumentNullException("entriesToRemove");
			}
			foreach (string item in entriesToRemove)
			{
				RemoveEntry(item);
			}
		}

		public void AddFiles(IEnumerable<string> fileNames)
		{
			AddFiles(fileNames, null);
		}

		public void UpdateFiles(IEnumerable<string> fileNames)
		{
			UpdateFiles(fileNames, null);
		}

		public void AddFiles(IEnumerable<string> fileNames, string directoryPathInArchive)
		{
			AddFiles(fileNames, preserveDirHierarchy: false, directoryPathInArchive);
		}

		public void AddFiles(IEnumerable<string> fileNames, bool preserveDirHierarchy, string directoryPathInArchive)
		{
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			_addOperationCanceled = false;
			OnAddStarted();
			if (preserveDirHierarchy)
			{
				foreach (string fileName in fileNames)
				{
					if (!_addOperationCanceled)
					{
						if (directoryPathInArchive != null)
						{
							string fullPath = Path.GetFullPath(Path.Combine(directoryPathInArchive, Path.GetDirectoryName(fileName)));
							AddFile(fileName, fullPath);
						}
						else
						{
							AddFile(fileName, null);
						}
						continue;
					}
					break;
				}
			}
			else
			{
				foreach (string fileName2 in fileNames)
				{
					if (!_addOperationCanceled)
					{
						AddFile(fileName2, directoryPathInArchive);
						continue;
					}
					break;
				}
			}
			if (!_addOperationCanceled)
			{
				OnAddCompleted();
			}
		}

		public void UpdateFiles(IEnumerable<string> fileNames, string directoryPathInArchive)
		{
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			OnAddStarted();
			foreach (string fileName in fileNames)
			{
				UpdateFile(fileName, directoryPathInArchive);
			}
			OnAddCompleted();
		}

		public ZipEntry UpdateFile(string fileName)
		{
			return UpdateFile(fileName, null);
		}

		public ZipEntry UpdateFile(string fileName, string directoryPathInArchive)
		{
			string fileName2 = ZipEntry.NameInArchive(fileName, directoryPathInArchive);
			if (this[fileName2] != null)
			{
				RemoveEntry(fileName2);
			}
			return AddFile(fileName, directoryPathInArchive);
		}

		public ZipEntry UpdateDirectory(string directoryName)
		{
			return UpdateDirectory(directoryName, null);
		}

		public ZipEntry UpdateDirectory(string directoryName, string directoryPathInArchive)
		{
			return AddOrUpdateDirectoryImpl(directoryName, directoryPathInArchive, AddOrUpdateAction.AddOrUpdate);
		}

		public void UpdateItem(string itemName)
		{
			UpdateItem(itemName, null);
		}

		public void UpdateItem(string itemName, string directoryPathInArchive)
		{
			if (File.Exists(itemName))
			{
				UpdateFile(itemName, directoryPathInArchive);
				return;
			}
			if (Directory.Exists(itemName))
			{
				UpdateDirectory(itemName, directoryPathInArchive);
				return;
			}
			throw new FileNotFoundException($"That file or directory ({itemName}) does not exist!");
		}

		public ZipEntry AddEntry(string entryName, string content)
		{
			return AddEntry(entryName, content, Encoding.Default);
		}

		public ZipEntry AddEntry(string entryName, string content, Encoding encoding)
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream, encoding);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return AddEntry(entryName, memoryStream);
		}

		public ZipEntry AddEntry(string entryName, Stream stream)
		{
			ZipEntry zipEntry = ZipEntry.CreateForStream(entryName, stream);
			zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			if (Verbose)
			{
				StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
			}
			return _InternalAddEntry(zipEntry);
		}

		public ZipEntry AddEntry(string entryName, WriteDelegate writer)
		{
			ZipEntry ze = ZipEntry.CreateForWriter(entryName, writer);
			if (Verbose)
			{
				StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
			}
			return _InternalAddEntry(ze);
		}

		public ZipEntry AddEntry(string entryName, OpenDelegate opener, CloseDelegate closer)
		{
			ZipEntry zipEntry = ZipEntry.CreateForJitStreamProvider(entryName, opener, closer);
			zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			if (Verbose)
			{
				StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
			}
			return _InternalAddEntry(zipEntry);
		}

		private ZipEntry _InternalAddEntry(ZipEntry ze)
		{
			ze._container = new ZipContainer(this);
			ze.CompressionMethod = CompressionMethod;
			ze.CompressionLevel = CompressionLevel;
			ze.ExtractExistingFile = ExtractExistingFile;
			ze.ZipErrorAction = ZipErrorAction;
			ze.SetCompression = SetCompression;
			ze.AlternateEncoding = AlternateEncoding;
			ze.AlternateEncodingUsage = AlternateEncodingUsage;
			ze.Password = _Password;
			ze.Encryption = Encryption;
			ze.EmitTimesInWindowsFormatWhenSaving = _emitNtfsTimes;
			ze.EmitTimesInUnixFormatWhenSaving = _emitUnixTimes;
			InternalAddEntry(ze.FileName, ze);
			AfterAddEntry(ze);
			return ze;
		}

		public ZipEntry UpdateEntry(string entryName, string content)
		{
			return UpdateEntry(entryName, content, Encoding.Default);
		}

		public ZipEntry UpdateEntry(string entryName, string content, Encoding encoding)
		{
			RemoveEntryForUpdate(entryName);
			return AddEntry(entryName, content, encoding);
		}

		public ZipEntry UpdateEntry(string entryName, WriteDelegate writer)
		{
			RemoveEntryForUpdate(entryName);
			return AddEntry(entryName, writer);
		}

		public ZipEntry UpdateEntry(string entryName, OpenDelegate opener, CloseDelegate closer)
		{
			RemoveEntryForUpdate(entryName);
			return AddEntry(entryName, opener, closer);
		}

		public ZipEntry UpdateEntry(string entryName, Stream stream)
		{
			RemoveEntryForUpdate(entryName);
			return AddEntry(entryName, stream);
		}

		private void RemoveEntryForUpdate(string entryName)
		{
			if (string.IsNullOrEmpty(entryName))
			{
				throw new ArgumentNullException("entryName");
			}
			string directoryPathInArchive = null;
			if (entryName.IndexOf('\\') != -1)
			{
				directoryPathInArchive = Path.GetDirectoryName(entryName);
				entryName = Path.GetFileName(entryName);
			}
			string fileName = ZipEntry.NameInArchive(entryName, directoryPathInArchive);
			if (this[fileName] != null)
			{
				RemoveEntry(fileName);
			}
		}

		public ZipEntry AddEntry(string entryName, byte[] byteContent)
		{
			if (byteContent == null)
			{
				throw new ArgumentException("bad argument", "byteContent");
			}
			MemoryStream stream = new MemoryStream(byteContent);
			return AddEntry(entryName, stream);
		}

		public ZipEntry UpdateEntry(string entryName, byte[] byteContent)
		{
			RemoveEntryForUpdate(entryName);
			return AddEntry(entryName, byteContent);
		}

		public ZipEntry AddDirectory(string directoryName)
		{
			return AddDirectory(directoryName, null);
		}

		public ZipEntry AddDirectory(string directoryName, string directoryPathInArchive)
		{
			return AddOrUpdateDirectoryImpl(directoryName, directoryPathInArchive, AddOrUpdateAction.AddOnly);
		}

		public ZipEntry AddDirectoryByName(string directoryNameInArchive)
		{
			ZipEntry zipEntry = ZipEntry.CreateFromNothing(directoryNameInArchive);
			zipEntry._container = new ZipContainer(this);
			zipEntry.MarkAsDirectory();
			zipEntry.AlternateEncoding = AlternateEncoding;
			zipEntry.AlternateEncodingUsage = AlternateEncodingUsage;
			zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			zipEntry.EmitTimesInWindowsFormatWhenSaving = _emitNtfsTimes;
			zipEntry.EmitTimesInUnixFormatWhenSaving = _emitUnixTimes;
			zipEntry._Source = ZipEntrySource.Stream;
			InternalAddEntry(zipEntry.FileName, zipEntry);
			AfterAddEntry(zipEntry);
			return zipEntry;
		}

		private ZipEntry AddOrUpdateDirectoryImpl(string directoryName, string rootDirectoryPathInArchive, AddOrUpdateAction action)
		{
			if (rootDirectoryPathInArchive == null)
			{
				rootDirectoryPathInArchive = "";
			}
			return AddOrUpdateDirectoryImpl(directoryName, rootDirectoryPathInArchive, action, recurse: true, 0);
		}

		internal void InternalAddEntry(string name, ZipEntry entry)
		{
			_entries.Add(name, entry);
			_zipEntriesAsList = null;
			_contentsChanged = true;
		}

		private ZipEntry AddOrUpdateDirectoryImpl(string directoryName, string rootDirectoryPathInArchive, AddOrUpdateAction action, bool recurse, int level)
		{
			if (Verbose)
			{
				StatusMessageTextWriter.WriteLine("{0} {1}...", (action == AddOrUpdateAction.AddOnly) ? "adding" : "Adding or updating", directoryName);
			}
			if (level == 0)
			{
				_addOperationCanceled = false;
				OnAddStarted();
			}
			if (_addOperationCanceled)
			{
				return null;
			}
			string text = rootDirectoryPathInArchive;
			ZipEntry zipEntry = null;
			if (level > 0)
			{
				int num = directoryName.Length;
				for (int num2 = level; num2 > 0; num2--)
				{
					num = directoryName.LastIndexOfAny("/\\".ToCharArray(), num - 1, num - 1);
				}
				text = directoryName.Substring(num + 1);
				text = Path.Combine(rootDirectoryPathInArchive, text);
			}
			if (level > 0 || rootDirectoryPathInArchive != "")
			{
				zipEntry = ZipEntry.CreateFromFile(directoryName, text);
				zipEntry._container = new ZipContainer(this);
				zipEntry.AlternateEncoding = AlternateEncoding;
				zipEntry.AlternateEncodingUsage = AlternateEncodingUsage;
				zipEntry.MarkAsDirectory();
				zipEntry.EmitTimesInWindowsFormatWhenSaving = _emitNtfsTimes;
				zipEntry.EmitTimesInUnixFormatWhenSaving = _emitUnixTimes;
				if (!_entries.ContainsKey(zipEntry.FileName))
				{
					InternalAddEntry(zipEntry.FileName, zipEntry);
					AfterAddEntry(zipEntry);
				}
				text = zipEntry.FileName;
			}
			if (!_addOperationCanceled)
			{
				string[] files = Directory.GetFiles(directoryName);
				if (recurse)
				{
					string[] array = files;
					foreach (string fileName in array)
					{
						if (_addOperationCanceled)
						{
							break;
						}
						if (action == AddOrUpdateAction.AddOnly)
						{
							AddFile(fileName, text);
						}
						else
						{
							UpdateFile(fileName, text);
						}
					}
					if (!_addOperationCanceled)
					{
						string[] directories = Directory.GetDirectories(directoryName);
						string[] array2 = directories;
						foreach (string text2 in array2)
						{
							FileAttributes attributes = File.GetAttributes(text2);
							if (AddDirectoryWillTraverseReparsePoints || (attributes & FileAttributes.ReparsePoint) == 0)
							{
								AddOrUpdateDirectoryImpl(text2, rootDirectoryPathInArchive, action, recurse, level + 1);
							}
						}
					}
				}
			}
			if (level == 0)
			{
				OnAddCompleted();
			}
			return zipEntry;
		}

		public static bool CheckZip(string zipFileName)
		{
			return CheckZip(zipFileName, fixIfNecessary: false, null);
		}

		public static bool CheckZip(string zipFileName, bool fixIfNecessary, TextWriter writer)
		{
			ZipFile zipFile = null;
			ZipFile zipFile2 = null;
			bool flag = true;
			try
			{
				zipFile = new ZipFile();
				zipFile.FullScan = true;
				zipFile.Initialize(zipFileName);
				zipFile2 = Read(zipFileName);
				foreach (ZipEntry item in zipFile)
				{
					foreach (ZipEntry item2 in zipFile2)
					{
						if (item.FileName == item2.FileName)
						{
							if (item._RelativeOffsetOfLocalHeader != item2._RelativeOffsetOfLocalHeader)
							{
								flag = false;
								writer?.WriteLine("{0}: mismatch in RelativeOffsetOfLocalHeader  (0x{1:X16} != 0x{2:X16})", item.FileName, item._RelativeOffsetOfLocalHeader, item2._RelativeOffsetOfLocalHeader);
							}
							if (item._CompressedSize != item2._CompressedSize)
							{
								flag = false;
								writer?.WriteLine("{0}: mismatch in CompressedSize  (0x{1:X16} != 0x{2:X16})", item.FileName, item._CompressedSize, item2._CompressedSize);
							}
							if (item._UncompressedSize != item2._UncompressedSize)
							{
								flag = false;
								writer?.WriteLine("{0}: mismatch in UncompressedSize  (0x{1:X16} != 0x{2:X16})", item.FileName, item._UncompressedSize, item2._UncompressedSize);
							}
							if (item.CompressionMethod != item2.CompressionMethod)
							{
								flag = false;
								writer?.WriteLine("{0}: mismatch in CompressionMethod  (0x{1:X4} != 0x{2:X4})", item.FileName, item.CompressionMethod, item2.CompressionMethod);
							}
							if (item.Crc != item2.Crc)
							{
								flag = false;
								writer?.WriteLine("{0}: mismatch in Crc32  (0x{1:X4} != 0x{2:X4})", item.FileName, item.Crc, item2.Crc);
							}
							break;
						}
					}
				}
				zipFile2.Dispose();
				zipFile2 = null;
				if (!flag && fixIfNecessary)
				{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(zipFileName);
					fileNameWithoutExtension = $"{fileNameWithoutExtension}_fixed.zip";
					zipFile.Save(fileNameWithoutExtension);
				}
			}
			finally
			{
				zipFile?.Dispose();
				zipFile2?.Dispose();
			}
			return flag;
		}

		public static void FixZipDirectory(string zipFileName)
		{
			using ZipFile zipFile = new ZipFile();
			zipFile.FullScan = true;
			zipFile.Initialize(zipFileName);
			zipFile.Save(zipFileName);
		}

		public static bool CheckZipPassword(string zipFileName, string password)
		{
			bool result = false;
			try
			{
				using (ZipFile zipFile = Read(zipFileName))
				{
					foreach (ZipEntry item in zipFile)
					{
						if (!item.IsDirectory && item.UsesEncryption)
						{
							item.ExtractWithPassword(Stream.Null, password);
						}
					}
				}
				result = true;
			}
			catch (BadPasswordException)
			{
			}
			return result;
		}

		public bool ContainsEntry(string name)
		{
			return _entries.ContainsKey(SharedUtilities.NormalizePathForUseInZipFile(name));
		}

		public override string ToString()
		{
			return $"ZipFile::{Name}";
		}

		internal void NotifyEntryChanged()
		{
			_contentsChanged = true;
		}

		internal Stream StreamForDiskNumber(uint diskNumber)
		{
			if (diskNumber + 1 == _diskNumberWithCd || (diskNumber == 0 && _diskNumberWithCd == 0))
			{
				return ReadStream;
			}
			return ZipSegmentedStream.ForReading(_readName ?? _name, diskNumber, _diskNumberWithCd);
		}

		internal void Reset(bool whileSaving)
		{
			if (!_JustSaved)
			{
				return;
			}
			using (ZipFile zipFile = new ZipFile())
			{
				zipFile._readName = (zipFile._name = (whileSaving ? (_readName ?? _name) : _name));
				zipFile.AlternateEncoding = AlternateEncoding;
				zipFile.AlternateEncodingUsage = AlternateEncodingUsage;
				ReadIntoInstance(zipFile);
				foreach (ZipEntry item in zipFile)
				{
					using IEnumerator<ZipEntry> enumerator2 = GetEnumerator();
					while (enumerator2.MoveNext())
					{
						ZipEntry current2 = enumerator2.Current;
						if (item.FileName == current2.FileName)
						{
							current2.CopyMetaData(item);
							break;
						}
					}
				}
			}
			_JustSaved = false;
		}

		public ZipFile(string fileName)
		{
			try
			{
				_InitInstance(fileName, null);
			}
			catch (Exception innerException)
			{
				throw new ZipException($"Could not read {fileName} as a zip file", innerException);
			}
		}

		public ZipFile(string fileName, Encoding encoding)
		{
			try
			{
				AlternateEncoding = encoding;
				AlternateEncodingUsage = ZipOption.Always;
				_InitInstance(fileName, null);
			}
			catch (Exception innerException)
			{
				throw new ZipException($"{fileName} is not a valid zip file", innerException);
			}
		}

		public ZipFile()
		{
			_InitInstance(null, null);
		}

		public ZipFile(Encoding encoding)
		{
			AlternateEncoding = encoding;
			AlternateEncodingUsage = ZipOption.Always;
			_InitInstance(null, null);
		}

		public ZipFile(string fileName, TextWriter statusMessageWriter)
		{
			try
			{
				_InitInstance(fileName, statusMessageWriter);
			}
			catch (Exception innerException)
			{
				throw new ZipException($"{fileName} is not a valid zip file", innerException);
			}
		}

		public ZipFile(string fileName, TextWriter statusMessageWriter, Encoding encoding)
		{
			try
			{
				AlternateEncoding = encoding;
				AlternateEncodingUsage = ZipOption.Always;
				_InitInstance(fileName, statusMessageWriter);
			}
			catch (Exception innerException)
			{
				throw new ZipException($"{fileName} is not a valid zip file", innerException);
			}
		}

		public void Initialize(string fileName)
		{
			try
			{
				_InitInstance(fileName, null);
			}
			catch (Exception innerException)
			{
				throw new ZipException($"{fileName} is not a valid zip file", innerException);
			}
		}

		private void _initEntriesDictionary()
		{
			StringComparer comparer = (CaseSensitiveRetrieval ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);
			_entries = ((_entries == null) ? new Dictionary<string, ZipEntry>(comparer) : new Dictionary<string, ZipEntry>(_entries, comparer));
		}

		private void _InitInstance(string zipFileName, TextWriter statusMessageWriter)
		{
			_name = zipFileName;
			_StatusMessageTextWriter = statusMessageWriter;
			_contentsChanged = true;
			AddDirectoryWillTraverseReparsePoints = true;
			CompressionLevel = CompressionLevel.Default;
			ParallelDeflateThreshold = 524288L;
			_initEntriesDictionary();
			if (File.Exists(_name))
			{
				if (FullScan)
				{
					ReadIntoInstance_Orig(this);
				}
				else
				{
					ReadIntoInstance(this);
				}
				_fileAlreadyExists = true;
			}
		}

		public void RemoveEntry(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			_entries.Remove(SharedUtilities.NormalizePathForUseInZipFile(entry.FileName));
			_zipEntriesAsList = null;
			_contentsChanged = true;
		}

		public void RemoveEntry(string fileName)
		{
			string fileName2 = ZipEntry.NameInArchive(fileName, null);
			ZipEntry zipEntry = this[fileName2];
			if (zipEntry == null)
			{
				throw new ArgumentException("The entry you specified was not found in the zip archive.");
			}
			RemoveEntry(zipEntry);
		}

		public void Dispose()
		{
			Dispose(disposeManagedResources: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposeManagedResources)
		{
			if (_disposed)
			{
				return;
			}
			if (disposeManagedResources)
			{
				if (_ReadStreamIsOurs && _readstream != null)
				{
					_readstream.Dispose();
					_readstream = null;
				}
				if (_temporaryFileName != null && _name != null && _writestream != null)
				{
					_writestream.Dispose();
					_writestream = null;
				}
				if (ParallelDeflater != null)
				{
					ParallelDeflater.Dispose();
					ParallelDeflater = null;
				}
			}
			_disposed = true;
		}

		internal bool OnSaveBlock(ZipEntry entry, long bytesXferred, long totalBytesToXfer)
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs e = SaveProgressEventArgs.ByteUpdate(ArchiveNameForEvent, entry, bytesXferred, totalBytesToXfer);
				saveProgress(this, e);
				if (e.Cancel)
				{
					_saveOperationCanceled = true;
				}
			}
			return _saveOperationCanceled;
		}

		private void OnSaveEntry(int current, ZipEntry entry, bool before)
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs e = new SaveProgressEventArgs(ArchiveNameForEvent, before, _entries.Count, current, entry);
				saveProgress(this, e);
				if (e.Cancel)
				{
					_saveOperationCanceled = true;
				}
			}
		}

		private void OnSaveEvent(ZipProgressEventType eventFlavor)
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs e = new SaveProgressEventArgs(ArchiveNameForEvent, eventFlavor);
				saveProgress(this, e);
				if (e.Cancel)
				{
					_saveOperationCanceled = true;
				}
			}
		}

		private void OnSaveStarted()
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs e = SaveProgressEventArgs.Started(ArchiveNameForEvent);
				saveProgress(this, e);
				if (e.Cancel)
				{
					_saveOperationCanceled = true;
				}
			}
		}

		private void OnSaveCompleted()
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs e = SaveProgressEventArgs.Completed(ArchiveNameForEvent);
				saveProgress(this, e);
			}
		}

		private void OnReadStarted()
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs e = ReadProgressEventArgs.Started(ArchiveNameForEvent);
				readProgress(this, e);
			}
		}

		private void OnReadCompleted()
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs e = ReadProgressEventArgs.Completed(ArchiveNameForEvent);
				readProgress(this, e);
			}
		}

		internal void OnReadBytes(ZipEntry entry)
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs e = ReadProgressEventArgs.ByteUpdate(ArchiveNameForEvent, entry, ReadStream.Position, LengthOfReadStream);
				readProgress(this, e);
			}
		}

		internal void OnReadEntry(bool before, ZipEntry entry)
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs e = (before ? ReadProgressEventArgs.Before(ArchiveNameForEvent, _entries.Count) : ReadProgressEventArgs.After(ArchiveNameForEvent, entry, _entries.Count));
				readProgress(this, e);
			}
		}

		private void OnExtractEntry(int current, bool before, ZipEntry currentEntry, string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs e = new ExtractProgressEventArgs(ArchiveNameForEvent, before, _entries.Count, current, currentEntry, path);
				extractProgress(this, e);
				if (e.Cancel)
				{
					_extractOperationCanceled = true;
				}
			}
		}

		internal bool OnExtractBlock(ZipEntry entry, long bytesWritten, long totalBytesToWrite)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs e = ExtractProgressEventArgs.ByteUpdate(ArchiveNameForEvent, entry, bytesWritten, totalBytesToWrite);
				extractProgress(this, e);
				if (e.Cancel)
				{
					_extractOperationCanceled = true;
				}
			}
			return _extractOperationCanceled;
		}

		internal bool OnSingleEntryExtract(ZipEntry entry, string path, bool before)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs e = (before ? ExtractProgressEventArgs.BeforeExtractEntry(ArchiveNameForEvent, entry, path) : ExtractProgressEventArgs.AfterExtractEntry(ArchiveNameForEvent, entry, path));
				extractProgress(this, e);
				if (e.Cancel)
				{
					_extractOperationCanceled = true;
				}
			}
			return _extractOperationCanceled;
		}

		internal bool OnExtractExisting(ZipEntry entry, string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs e = ExtractProgressEventArgs.ExtractExisting(ArchiveNameForEvent, entry, path);
				extractProgress(this, e);
				if (e.Cancel)
				{
					_extractOperationCanceled = true;
				}
			}
			return _extractOperationCanceled;
		}

		private void OnExtractAllCompleted(string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs e = ExtractProgressEventArgs.ExtractAllCompleted(ArchiveNameForEvent, path);
				extractProgress(this, e);
			}
		}

		private void OnExtractAllStarted(string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs e = ExtractProgressEventArgs.ExtractAllStarted(ArchiveNameForEvent, path);
				extractProgress(this, e);
			}
		}

		private void OnAddStarted()
		{
			EventHandler<AddProgressEventArgs> addProgress = this.AddProgress;
			if (addProgress != null)
			{
				AddProgressEventArgs e = AddProgressEventArgs.Started(ArchiveNameForEvent);
				addProgress(this, e);
				if (e.Cancel)
				{
					_addOperationCanceled = true;
				}
			}
		}

		private void OnAddCompleted()
		{
			EventHandler<AddProgressEventArgs> addProgress = this.AddProgress;
			if (addProgress != null)
			{
				AddProgressEventArgs e = AddProgressEventArgs.Completed(ArchiveNameForEvent);
				addProgress(this, e);
			}
		}

		internal void AfterAddEntry(ZipEntry entry)
		{
			EventHandler<AddProgressEventArgs> addProgress = this.AddProgress;
			if (addProgress != null)
			{
				AddProgressEventArgs e = AddProgressEventArgs.AfterEntry(ArchiveNameForEvent, entry, _entries.Count);
				addProgress(this, e);
				if (e.Cancel)
				{
					_addOperationCanceled = true;
				}
			}
		}

		internal bool OnZipErrorSaving(ZipEntry entry, Exception exc)
		{
			if (this.ZipError != null)
			{
				lock (LOCK)
				{
					ZipErrorEventArgs e = ZipErrorEventArgs.Saving(Name, entry, exc);
					this.ZipError(this, e);
					if (e.Cancel)
					{
						_saveOperationCanceled = true;
					}
				}
			}
			return _saveOperationCanceled;
		}

		public void ExtractAll(string path)
		{
			_InternalExtractAll(path, overrideExtractExistingProperty: true);
		}

		public void ExtractAll(string path, ExtractExistingFileAction extractExistingFile)
		{
			ExtractExistingFile = extractExistingFile;
			_InternalExtractAll(path, overrideExtractExistingProperty: true);
		}

		private void _InternalExtractAll(string path, bool overrideExtractExistingProperty)
		{
			bool flag = Verbose;
			_inExtractAll = true;
			try
			{
				OnExtractAllStarted(path);
				int num = 0;
				foreach (ZipEntry value in _entries.Values)
				{
					if (flag)
					{
						StatusMessageTextWriter.WriteLine("\n{1,-22} {2,-8} {3,4}   {4,-8}  {0}", "Name", "Modified", "Size", "Ratio", "Packed");
						StatusMessageTextWriter.WriteLine(new string('-', 72));
						flag = false;
					}
					if (Verbose)
					{
						StatusMessageTextWriter.WriteLine("{1,-22} {2,-8} {3,4:F0}%   {4,-8} {0}", value.FileName, value.LastModified.ToString("yyyy-MM-dd HH:mm:ss"), value.UncompressedSize, value.CompressionRatio, value.CompressedSize);
						if (!string.IsNullOrEmpty(value.Comment))
						{
							StatusMessageTextWriter.WriteLine("  Comment: {0}", value.Comment);
						}
					}
					value.Password = _Password;
					OnExtractEntry(num, before: true, value, path);
					if (overrideExtractExistingProperty)
					{
						value.ExtractExistingFile = ExtractExistingFile;
					}
					value.Extract(path);
					num++;
					OnExtractEntry(num, before: false, value, path);
					if (_extractOperationCanceled)
					{
						break;
					}
				}
				if (_extractOperationCanceled)
				{
					return;
				}
				foreach (ZipEntry value2 in _entries.Values)
				{
					if (value2.IsDirectory || value2.FileName.EndsWith("/"))
					{
						string fileOrDirectory = (value2.FileName.StartsWith("/") ? Path.Combine(path, value2.FileName.Substring(1)) : Path.Combine(path, value2.FileName));
						value2._SetTimes(fileOrDirectory, isFile: false);
					}
				}
				OnExtractAllCompleted(path);
			}
			finally
			{
				_inExtractAll = false;
			}
		}

		public static ZipFile Read(string fileName)
		{
			return Read(fileName, null, null, null);
		}

		public static ZipFile Read(string fileName, ReadOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return Read(fileName, options.StatusMessageWriter, options.Encoding, options.ReadProgress);
		}

		private static ZipFile Read(string fileName, TextWriter statusMessageWriter, Encoding encoding, EventHandler<ReadProgressEventArgs> readProgress)
		{
			ZipFile zipFile = new ZipFile();
			zipFile.AlternateEncoding = encoding ?? DefaultEncoding;
			zipFile.AlternateEncodingUsage = ZipOption.Always;
			zipFile._StatusMessageTextWriter = statusMessageWriter;
			zipFile._name = fileName;
			if (readProgress != null)
			{
				zipFile.ReadProgress = readProgress;
			}
			if (zipFile.Verbose)
			{
				zipFile._StatusMessageTextWriter.WriteLine("reading from {0}...", fileName);
			}
			ReadIntoInstance(zipFile);
			zipFile._fileAlreadyExists = true;
			return zipFile;
		}

		public static ZipFile Read(Stream zipStream)
		{
			return Read(zipStream, null, null, null);
		}

		public static ZipFile Read(Stream zipStream, ReadOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return Read(zipStream, options.StatusMessageWriter, options.Encoding, options.ReadProgress);
		}

		private static ZipFile Read(Stream zipStream, TextWriter statusMessageWriter, Encoding encoding, EventHandler<ReadProgressEventArgs> readProgress)
		{
			if (zipStream == null)
			{
				throw new ArgumentNullException("zipStream");
			}
			ZipFile zipFile = new ZipFile();
			zipFile._StatusMessageTextWriter = statusMessageWriter;
			zipFile._alternateEncoding = encoding ?? DefaultEncoding;
			zipFile._alternateEncodingUsage = ZipOption.Always;
			if (readProgress != null)
			{
				zipFile.ReadProgress += readProgress;
			}
			zipFile._readstream = ((zipStream.Position == 0) ? zipStream : new OffsetStream(zipStream));
			zipFile._ReadStreamIsOurs = false;
			if (zipFile.Verbose)
			{
				zipFile._StatusMessageTextWriter.WriteLine("reading from stream...");
			}
			ReadIntoInstance(zipFile);
			return zipFile;
		}

		private static void ReadIntoInstance(ZipFile zf)
		{
			Stream readStream = zf.ReadStream;
			try
			{
				zf._readName = zf._name;
				if (!readStream.CanSeek)
				{
					ReadIntoInstance_Orig(zf);
					return;
				}
				zf.OnReadStarted();
				uint num = ReadFirstFourBytes(readStream);
				if (num == 101010256)
				{
					return;
				}
				int num2 = 0;
				bool flag = false;
				long num3 = readStream.Length - 64;
				long num4 = Math.Max(readStream.Length - 16384, 10L);
				do
				{
					if (num3 < 0)
					{
						num3 = 0L;
					}
					readStream.Seek(num3, SeekOrigin.Begin);
					long num5 = SharedUtilities.FindSignature(readStream, 101010256);
					if (num5 != -1)
					{
						flag = true;
						continue;
					}
					if (num3 == 0)
					{
						break;
					}
					num2++;
					num3 -= 32 * (num2 + 1) * num2;
				}
				while (!flag && num3 > num4);
				if (flag)
				{
					zf._locEndOfCDS = readStream.Position - 4;
					byte[] array = new byte[16];
					readStream.Read(array, 0, array.Length);
					zf._diskNumberWithCd = BitConverter.ToUInt16(array, 2);
					if (zf._diskNumberWithCd == 65535)
					{
						throw new ZipException("Spanned archives with more than 65534 segments are not supported at this time.");
					}
					zf._diskNumberWithCd++;
					int startIndex = 12;
					uint num6 = BitConverter.ToUInt32(array, startIndex);
					if (num6 == uint.MaxValue)
					{
						Zip64SeekToCentralDirectory(zf);
					}
					else
					{
						zf._OffsetOfCentralDirectory = num6;
						readStream.Seek(num6, SeekOrigin.Begin);
					}
					ReadCentralDirectory(zf);
				}
				else
				{
					readStream.Seek(0L, SeekOrigin.Begin);
					ReadIntoInstance_Orig(zf);
				}
			}
			catch (Exception innerException)
			{
				if (zf._ReadStreamIsOurs && zf._readstream != null)
				{
					zf._readstream.Dispose();
					zf._readstream = null;
				}
				throw new ZipException("Cannot read that as a ZipFile", innerException);
			}
			zf._contentsChanged = false;
		}

		private static void Zip64SeekToCentralDirectory(ZipFile zf)
		{
			Stream readStream = zf.ReadStream;
			byte[] array = new byte[16];
			readStream.Seek(-40L, SeekOrigin.Current);
			readStream.Read(array, 0, 16);
			long num = BitConverter.ToInt64(array, 8);
			zf._OffsetOfCentralDirectory = uint.MaxValue;
			zf._OffsetOfCentralDirectory64 = num;
			readStream.Seek(num, SeekOrigin.Begin);
			uint num2 = (uint)SharedUtilities.ReadInt(readStream);
			if (num2 != 101075792)
			{
				throw new BadReadException($"  Bad signature (0x{num2:X8}) looking for ZIP64 EoCD Record at position 0x{readStream.Position:X8}");
			}
			readStream.Read(array, 0, 8);
			long num3 = BitConverter.ToInt64(array, 0);
			array = new byte[num3];
			readStream.Read(array, 0, array.Length);
			num = BitConverter.ToInt64(array, 36);
			readStream.Seek(num, SeekOrigin.Begin);
		}

		private static uint ReadFirstFourBytes(Stream s)
		{
			return (uint)SharedUtilities.ReadInt(s);
		}

		private static void ReadCentralDirectory(ZipFile zf)
		{
			bool flag = false;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ZipEntry zipEntry;
			while ((zipEntry = ZipEntry.ReadDirEntry(zf, dictionary)) != null)
			{
				zipEntry.ResetDirEntry();
				zf.OnReadEntry(before: true, null);
				if (zf.Verbose)
				{
					zf.StatusMessageTextWriter.WriteLine("entry {0}", zipEntry.FileName);
				}
				zf._entries.Add(zipEntry.FileName, zipEntry);
				if (zipEntry._InputUsesZip64)
				{
					flag = true;
				}
				dictionary.Add(zipEntry.FileName, null);
			}
			if (flag)
			{
				zf.UseZip64WhenSaving = Zip64Option.Always;
			}
			if (zf._locEndOfCDS > 0)
			{
				zf.ReadStream.Seek(zf._locEndOfCDS, SeekOrigin.Begin);
			}
			ReadCentralDirectoryFooter(zf);
			if (zf.Verbose && !string.IsNullOrEmpty(zf.Comment))
			{
				zf.StatusMessageTextWriter.WriteLine("Zip file Comment: {0}", zf.Comment);
			}
			if (zf.Verbose)
			{
				zf.StatusMessageTextWriter.WriteLine("read in {0} entries.", zf._entries.Count);
			}
			zf.OnReadCompleted();
		}

		private static void ReadIntoInstance_Orig(ZipFile zf)
		{
			zf.OnReadStarted();
			zf._entries = new Dictionary<string, ZipEntry>();
			if (zf.Verbose)
			{
				if (zf.Name == null)
				{
					zf.StatusMessageTextWriter.WriteLine("Reading zip from stream...");
				}
				else
				{
					zf.StatusMessageTextWriter.WriteLine("Reading zip {0}...", zf.Name);
				}
			}
			bool first = true;
			ZipContainer zc = new ZipContainer(zf);
			ZipEntry zipEntry;
			while ((zipEntry = ZipEntry.ReadEntry(zc, first)) != null)
			{
				if (zf.Verbose)
				{
					zf.StatusMessageTextWriter.WriteLine("  {0}", zipEntry.FileName);
				}
				zf._entries.Add(zipEntry.FileName, zipEntry);
				first = false;
			}
			try
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				ZipEntry zipEntry2;
				while ((zipEntry2 = ZipEntry.ReadDirEntry(zf, dictionary)) != null)
				{
					ZipEntry zipEntry3 = zf._entries[zipEntry2.FileName];
					if (zipEntry3 != null)
					{
						zipEntry3._Comment = zipEntry2.Comment;
						if (zipEntry2.IsDirectory)
						{
							zipEntry3.MarkAsDirectory();
						}
					}
					dictionary.Add(zipEntry2.FileName, null);
				}
				if (zf._locEndOfCDS > 0)
				{
					zf.ReadStream.Seek(zf._locEndOfCDS, SeekOrigin.Begin);
				}
				ReadCentralDirectoryFooter(zf);
				if (zf.Verbose && !string.IsNullOrEmpty(zf.Comment))
				{
					zf.StatusMessageTextWriter.WriteLine("Zip file Comment: {0}", zf.Comment);
				}
			}
			catch (ZipException)
			{
			}
			catch (IOException)
			{
			}
			zf.OnReadCompleted();
		}

		private static void ReadCentralDirectoryFooter(ZipFile zf)
		{
			Stream readStream = zf.ReadStream;
			int num = SharedUtilities.ReadSignature(readStream);
			byte[] array = null;
			int num2 = 0;
			if ((long)num == 101075792)
			{
				array = new byte[52];
				readStream.Read(array, 0, array.Length);
				long num3 = BitConverter.ToInt64(array, 0);
				if (num3 < 44)
				{
					throw new ZipException("Bad size in the ZIP64 Central Directory.");
				}
				zf._versionMadeBy = BitConverter.ToUInt16(array, num2);
				num2 += 2;
				zf._versionNeededToExtract = BitConverter.ToUInt16(array, num2);
				num2 += 2;
				zf._diskNumberWithCd = BitConverter.ToUInt32(array, num2);
				num2 += 2;
				array = new byte[num3 - 44];
				readStream.Read(array, 0, array.Length);
				num = SharedUtilities.ReadSignature(readStream);
				if ((long)num != 117853008)
				{
					throw new ZipException("Inconsistent metadata in the ZIP64 Central Directory.");
				}
				array = new byte[16];
				readStream.Read(array, 0, array.Length);
				num = SharedUtilities.ReadSignature(readStream);
			}
			if ((long)num != 101010256)
			{
				readStream.Seek(-4L, SeekOrigin.Current);
				throw new BadReadException($"Bad signature ({num:X8}) at position 0x{readStream.Position:X8}");
			}
			array = new byte[16];
			zf.ReadStream.Read(array, 0, array.Length);
			if (zf._diskNumberWithCd == 0)
			{
				zf._diskNumberWithCd = BitConverter.ToUInt16(array, 2);
			}
			ReadZipFileComment(zf);
		}

		private static void ReadZipFileComment(ZipFile zf)
		{
			byte[] array = new byte[2];
			zf.ReadStream.Read(array, 0, array.Length);
			short num = (short)(array[0] + array[1] * 256);
			if (num > 0)
			{
				array = new byte[num];
				zf.ReadStream.Read(array, 0, array.Length);
				string comment = zf.AlternateEncoding.GetString(array, 0, array.Length);
				zf.Comment = comment;
			}
		}

		public static bool IsZipFile(string fileName)
		{
			return IsZipFile(fileName, testExtract: false);
		}

		public static bool IsZipFile(string fileName, bool testExtract)
		{
			bool result = false;
			try
			{
				if (!File.Exists(fileName))
				{
					return false;
				}
				using FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				result = IsZipFile(stream, testExtract);
			}
			catch (IOException)
			{
			}
			catch (ZipException)
			{
			}
			return result;
		}

		public static bool IsZipFile(Stream stream, bool testExtract)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			bool result = false;
			try
			{
				if (!stream.CanRead)
				{
					return false;
				}
				Stream stream2 = Stream.Null;
				using (ZipFile zipFile = Read(stream, null, null, null))
				{
					if (testExtract)
					{
						foreach (ZipEntry item in zipFile)
						{
							if (!item.IsDirectory)
							{
								item.Extract(stream2);
							}
						}
					}
				}
				result = true;
			}
			catch (IOException)
			{
			}
			catch (ZipException)
			{
			}
			return result;
		}

		private void DeleteFileWithRetry(string filename)
		{
			bool flag = false;
			int num = 3;
			for (int i = 0; i < num; i++)
			{
				if (flag)
				{
					break;
				}
				try
				{
					File.Delete(filename);
					flag = true;
				}
				catch (UnauthorizedAccessException)
				{
					Console.WriteLine("************************************************** Retry delete.");
					Thread.Sleep(200 + i * 200);
				}
			}
		}

		public void Save()
		{
			try
			{
				bool flag = false;
				_saveOperationCanceled = false;
				_numberOfSegmentsForMostRecentSave = 0u;
				OnSaveStarted();
				if (WriteStream == null)
				{
					throw new BadStateException("You haven't specified where to save the zip.");
				}
				if (_name != null && _name.EndsWith(".exe") && !_SavingSfx)
				{
					throw new BadStateException("You specified an EXE for a plain zip file.");
				}
				if (!_contentsChanged)
				{
					OnSaveCompleted();
					if (Verbose)
					{
						StatusMessageTextWriter.WriteLine("No save is necessary....");
					}
					return;
				}
				Reset(whileSaving: true);
				if (Verbose)
				{
					StatusMessageTextWriter.WriteLine("saving....");
				}
				if (_entries.Count >= 65535 && _zip64 == Zip64Option.Default)
				{
					throw new ZipException("The number of entries is 65535 or greater. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
				}
				int num = 0;
				ICollection<ZipEntry> collection = (SortEntriesBeforeSaving ? EntriesSorted : Entries);
				foreach (ZipEntry item in collection)
				{
					OnSaveEntry(num, item, before: true);
					item.Write(WriteStream);
					if (!_saveOperationCanceled)
					{
						num++;
						OnSaveEntry(num, item, before: false);
						if (!_saveOperationCanceled)
						{
							if (item.IncludedInMostRecentSave)
							{
								flag |= item.OutputUsedZip64.Value;
							}
							continue;
						}
						break;
					}
					break;
				}
				if (_saveOperationCanceled)
				{
					return;
				}
				ZipSegmentedStream zipSegmentedStream = WriteStream as ZipSegmentedStream;
				_numberOfSegmentsForMostRecentSave = zipSegmentedStream?.CurrentSegment ?? 1;
				bool flag2 = ZipOutput.WriteCentralDirectoryStructure(WriteStream, collection, _numberOfSegmentsForMostRecentSave, _zip64, Comment, new ZipContainer(this));
				OnSaveEvent(ZipProgressEventType.Saving_AfterSaveTempArchive);
				_hasBeenSaved = true;
				_contentsChanged = false;
				flag = flag || flag2;
				_OutputUsesZip64 = flag;
				if (_name != null && (_temporaryFileName != null || zipSegmentedStream != null))
				{
					WriteStream.Dispose();
					if (_saveOperationCanceled)
					{
						return;
					}
					if (_fileAlreadyExists && _readstream != null)
					{
						_readstream.Close();
						_readstream = null;
						foreach (ZipEntry item2 in collection)
						{
							if (item2._archiveStream is ZipSegmentedStream zipSegmentedStream2)
							{
								zipSegmentedStream2.Dispose();
							}
							item2._archiveStream = null;
						}
					}
					string text = null;
					if (File.Exists(_name))
					{
						text = _name + "." + Path.GetRandomFileName();
						if (File.Exists(text))
						{
							DeleteFileWithRetry(text);
						}
						File.Move(_name, text);
					}
					OnSaveEvent(ZipProgressEventType.Saving_BeforeRenameTempArchive);
					File.Move((zipSegmentedStream != null) ? zipSegmentedStream.CurrentTempName : _temporaryFileName, _name);
					OnSaveEvent(ZipProgressEventType.Saving_AfterRenameTempArchive);
					if (text != null)
					{
						try
						{
							if (File.Exists(text))
							{
								File.Delete(text);
							}
						}
						catch
						{
						}
					}
					_fileAlreadyExists = true;
				}
				NotifyEntriesSaveComplete(collection);
				OnSaveCompleted();
				_JustSaved = true;
			}
			finally
			{
				CleanupAfterSaveOperation();
			}
		}

		private static void NotifyEntriesSaveComplete(ICollection<ZipEntry> c)
		{
			foreach (ZipEntry item in c)
			{
				item.NotifySaveComplete();
			}
		}

		private void RemoveTempFile()
		{
			try
			{
				if (File.Exists(_temporaryFileName))
				{
					File.Delete(_temporaryFileName);
				}
			}
			catch (IOException ex)
			{
				if (Verbose)
				{
					StatusMessageTextWriter.WriteLine("ZipFile::Save: could not delete temp file: {0}.", ex.Message);
				}
			}
		}

		private void CleanupAfterSaveOperation()
		{
			if (_name == null)
			{
				return;
			}
			if (_writestream != null)
			{
				try
				{
					_writestream.Dispose();
				}
				catch (IOException)
				{
				}
			}
			_writestream = null;
			if (_temporaryFileName != null)
			{
				RemoveTempFile();
				_temporaryFileName = null;
			}
		}

		public void Save(string fileName)
		{
			if (_name == null)
			{
				_writestream = null;
			}
			else
			{
				_readName = _name;
			}
			_name = fileName;
			if (Directory.Exists(_name))
			{
				throw new ZipException("Bad Directory", new ArgumentException("That name specifies an existing directory. Please specify a filename.", "fileName"));
			}
			_contentsChanged = true;
			_fileAlreadyExists = File.Exists(_name);
			Save();
		}

		public void Save(Stream outputStream)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (!outputStream.CanWrite)
			{
				throw new ArgumentException("Must be a writable stream.", "outputStream");
			}
			_name = null;
			_writestream = new CountingStream(outputStream);
			_contentsChanged = true;
			_fileAlreadyExists = false;
			Save();
		}

		public void SaveSelfExtractor(string exeToGenerate, SelfExtractorFlavor flavor)
		{
			SelfExtractorSaveOptions selfExtractorSaveOptions = new SelfExtractorSaveOptions();
			selfExtractorSaveOptions.Flavor = flavor;
			SaveSelfExtractor(exeToGenerate, selfExtractorSaveOptions);
		}

		public void SaveSelfExtractor(string exeToGenerate, SelfExtractorSaveOptions options)
		{
			if (_name == null)
			{
				_writestream = null;
			}
			_SavingSfx = true;
			_name = exeToGenerate;
			if (Directory.Exists(_name))
			{
				throw new ZipException("Bad Directory", new ArgumentException("That name specifies an existing directory. Please specify a filename.", "exeToGenerate"));
			}
			_contentsChanged = true;
			_fileAlreadyExists = File.Exists(_name);
			_SaveSfxStub(exeToGenerate, options);
			Save();
			_SavingSfx = false;
		}

		private static void ExtractResourceToFile(Assembly a, string resourceName, string filename)
		{
			int num = 0;
			byte[] array = new byte[1024];
			using Stream stream = a.GetManifestResourceStream(resourceName);
			if (stream == null)
			{
				throw new ZipException($"missing resource '{resourceName}'");
			}
			using FileStream fileStream = File.OpenWrite(filename);
			do
			{
				num = stream.Read(array, 0, array.Length);
				fileStream.Write(array, 0, num);
			}
			while (num > 0);
		}

		private void _SaveSfxStub(string exeToGenerate, SelfExtractorSaveOptions options)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			try
			{
				if (File.Exists(exeToGenerate) && Verbose)
				{
					StatusMessageTextWriter.WriteLine("The existing file ({0}) will be overwritten.", exeToGenerate);
				}
				if (!exeToGenerate.EndsWith(".exe") && Verbose)
				{
					StatusMessageTextWriter.WriteLine("Warning: The generated self-extracting file will not have an .exe extension.");
				}
				text4 = TempFileFolder ?? Path.GetDirectoryName(exeToGenerate);
				text2 = GenerateTempPathname(text4, "exe");
				Assembly assembly = typeof(ZipFile).Assembly;
				using (CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider())
				{
					ExtractorSettings extractorSettings = null;
					ExtractorSettings[] settingsList = SettingsList;
					foreach (ExtractorSettings extractorSettings2 in settingsList)
					{
						if (extractorSettings2.Flavor == options.Flavor)
						{
							extractorSettings = extractorSettings2;
							break;
						}
					}
					if (extractorSettings == null)
					{
						throw new BadStateException($"While saving a Self-Extracting Zip, Cannot find that flavor ({options.Flavor})?");
					}
					CompilerParameters compilerParameters = new CompilerParameters();
					compilerParameters.ReferencedAssemblies.Add(assembly.Location);
					if (extractorSettings.ReferencedAssemblies != null)
					{
						foreach (string referencedAssembly in extractorSettings.ReferencedAssemblies)
						{
							compilerParameters.ReferencedAssemblies.Add(referencedAssembly);
						}
					}
					compilerParameters.GenerateInMemory = false;
					compilerParameters.GenerateExecutable = true;
					compilerParameters.IncludeDebugInformation = false;
					compilerParameters.CompilerOptions = "";
					Assembly executingAssembly = Assembly.GetExecutingAssembly();
					StringBuilder stringBuilder = new StringBuilder();
					string text5 = GenerateTempPathname(text4, "cs");
					using (ZipFile zipFile = Read(executingAssembly.GetManifestResourceStream("Ionic.Zip.Resources.ZippedResources.zip")))
					{
						text3 = GenerateTempPathname(text4, "tmp");
						if (string.IsNullOrEmpty(options.IconFile))
						{
							Directory.CreateDirectory(text3);
							ZipEntry zipEntry = zipFile["zippedFile.ico"];
							if ((zipEntry.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
							{
								zipEntry.Attributes ^= FileAttributes.ReadOnly;
							}
							zipEntry.Extract(text3);
							text = Path.Combine(text3, "zippedFile.ico");
							compilerParameters.CompilerOptions += $"/win32icon:\"{text}\"";
						}
						else
						{
							compilerParameters.CompilerOptions += $"/win32icon:\"{options.IconFile}\"";
						}
						compilerParameters.OutputAssembly = text2;
						if (options.Flavor == SelfExtractorFlavor.WinFormsApplication)
						{
							compilerParameters.CompilerOptions += " /target:winexe";
						}
						if (!string.IsNullOrEmpty(options.AdditionalCompilerSwitches))
						{
							compilerParameters.CompilerOptions = compilerParameters.CompilerOptions + " " + options.AdditionalCompilerSwitches;
						}
						if (string.IsNullOrEmpty(compilerParameters.CompilerOptions))
						{
							compilerParameters.CompilerOptions = null;
						}
						if (extractorSettings.CopyThroughResources != null && extractorSettings.CopyThroughResources.Count != 0)
						{
							if (!Directory.Exists(text3))
							{
								Directory.CreateDirectory(text3);
							}
							foreach (string copyThroughResource in extractorSettings.CopyThroughResources)
							{
								string text6 = Path.Combine(text3, copyThroughResource);
								ExtractResourceToFile(executingAssembly, copyThroughResource, text6);
								compilerParameters.EmbeddedResources.Add(text6);
							}
						}
						compilerParameters.EmbeddedResources.Add(assembly.Location);
						stringBuilder.Append("// " + Path.GetFileName(text5) + "\n").Append("// --------------------------------------------\n//\n").Append("// This SFX source file was generated by DotNetZip ")
							.Append(LibraryVersion.ToString())
							.Append("\n//         at ")
							.Append(DateTime.Now.ToString("yyyy MMMM dd  HH:mm:ss"))
							.Append("\n//\n// --------------------------------------------\n\n\n");
						if (!string.IsNullOrEmpty(options.Description))
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyTitle(\"" + options.Description.Replace("\"", "") + "\")]\n");
						}
						else
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyTitle(\"DotNetZip SFX Archive\")]\n");
						}
						if (!string.IsNullOrEmpty(options.ProductVersion))
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyInformationalVersion(\"" + options.ProductVersion.Replace("\"", "") + "\")]\n");
						}
						string text7 = (string.IsNullOrEmpty(options.Copyright) ? "Extractor: Copyright © Dino Chiesa 2008-2011" : options.Copyright.Replace("\"", ""));
						if (!string.IsNullOrEmpty(options.ProductName))
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyProduct(\"").Append(options.ProductName.Replace("\"", "")).Append("\")]\n");
						}
						else
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyProduct(\"DotNetZip\")]\n");
						}
						stringBuilder.Append("[assembly: System.Reflection.AssemblyCopyright(\"" + text7 + "\")]\n").Append($"[assembly: System.Reflection.AssemblyVersion(\"{LibraryVersion.ToString()}\")]\n");
						if (options.FileVersion != null)
						{
							stringBuilder.Append($"[assembly: System.Reflection.AssemblyFileVersion(\"{options.FileVersion.ToString()}\")]\n");
						}
						stringBuilder.Append("\n\n\n");
						string text8 = options.DefaultExtractDirectory;
						if (text8 != null)
						{
							text8 = text8.Replace("\"", "").Replace("\\", "\\\\");
						}
						string text9 = options.PostExtractCommandLine;
						if (text9 != null)
						{
							text9 = text9.Replace("\\", "\\\\");
							text9 = text9.Replace("\"", "\\\"");
						}
						foreach (string item in extractorSettings.ResourcesToCompile)
						{
							using Stream stream = zipFile[item].OpenReader();
							if (stream == null)
							{
								throw new ZipException($"missing resource '{item}'");
							}
							using (StreamReader streamReader = new StreamReader(stream))
							{
								while (streamReader.Peek() >= 0)
								{
									string text10 = streamReader.ReadLine();
									if (text8 != null)
									{
										text10 = text10.Replace("@@EXTRACTLOCATION", text8);
									}
									text10 = text10.Replace("@@REMOVE_AFTER_EXECUTE", options.RemoveUnpackedFilesAfterExecute.ToString());
									text10 = text10.Replace("@@QUIET", options.Quiet.ToString());
									if (!string.IsNullOrEmpty(options.SfxExeWindowTitle))
									{
										text10 = text10.Replace("@@SFX_EXE_WINDOW_TITLE", options.SfxExeWindowTitle);
									}
									text10 = text10.Replace("@@EXTRACT_EXISTING_FILE", ((int)options.ExtractExistingFile).ToString());
									if (text9 != null)
									{
										text10 = text10.Replace("@@POST_UNPACK_CMD_LINE", text9);
									}
									stringBuilder.Append(text10).Append("\n");
								}
							}
							stringBuilder.Append("\n\n");
						}
					}
					string text11 = stringBuilder.ToString();
					CompilerResults compilerResults = cSharpCodeProvider.CompileAssemblyFromSource(compilerParameters, text11);
					if (compilerResults == null)
					{
						throw new SfxGenerationException("Cannot compile the extraction logic!");
					}
					if (Verbose)
					{
						StringEnumerator enumerator4 = compilerResults.Output.GetEnumerator();
						try
						{
							while (enumerator4.MoveNext())
							{
								string current4 = enumerator4.Current;
								StatusMessageTextWriter.WriteLine(current4);
							}
						}
						finally
						{
							if (enumerator4 is IDisposable disposable)
							{
								disposable.Dispose();
							}
						}
					}
					if (compilerResults.Errors.Count != 0)
					{
						using (TextWriter textWriter = new StreamWriter(text5))
						{
							textWriter.Write(text11);
							textWriter.Write("\n\n\n// ------------------------------------------------------------------\n");
							textWriter.Write("// Errors during compilation: \n//\n");
							string fileName = Path.GetFileName(text5);
							foreach (CompilerError error in compilerResults.Errors)
							{
								textWriter.Write(string.Format("//   {0}({1},{2}): {3} {4}: {5}\n//\n", fileName, error.Line, error.Column, error.IsWarning ? "Warning" : "error", error.ErrorNumber, error.ErrorText));
							}
						}
						throw new SfxGenerationException($"Errors compiling the extraction logic!  {text5}");
					}
					OnSaveEvent(ZipProgressEventType.Saving_AfterCompileSelfExtractor);
					using Stream stream2 = File.OpenRead(text2);
					byte[] array = new byte[4000];
					int num = 1;
					while (num != 0)
					{
						num = stream2.Read(array, 0, array.Length);
						if (num != 0)
						{
							WriteStream.Write(array, 0, num);
						}
					}
				}
				OnSaveEvent(ZipProgressEventType.Saving_AfterSaveTempArchive);
			}
			finally
			{
				try
				{
					if (Directory.Exists(text3))
					{
						try
						{
							Directory.Delete(text3, recursive: true);
						}
						catch (IOException arg)
						{
							StatusMessageTextWriter.WriteLine("Warning: Exception: {0}", arg);
						}
					}
					if (File.Exists(text2))
					{
						try
						{
							File.Delete(text2);
						}
						catch (IOException arg2)
						{
							StatusMessageTextWriter.WriteLine("Warning: Exception: {0}", arg2);
						}
					}
				}
				catch (IOException)
				{
				}
			}
		}

		internal static string GenerateTempPathname(string dir, string extension)
		{
			string text = null;
			string name = Assembly.GetExecutingAssembly().GetName().Name;
			do
			{
				string text2 = Guid.NewGuid().ToString();
				string path = string.Format("{0}-{1}-{2}.{3}", name, DateTime.Now.ToString("yyyyMMMdd-HHmmss"), text2, extension);
				text = Path.Combine(dir, path);
			}
			while (File.Exists(text) || Directory.Exists(text));
			return text;
		}

		public void AddSelectedFiles(string selectionCriteria)
		{
			AddSelectedFiles(selectionCriteria, ".", null, recurseDirectories: false);
		}

		public void AddSelectedFiles(string selectionCriteria, bool recurseDirectories)
		{
			AddSelectedFiles(selectionCriteria, ".", null, recurseDirectories);
		}

		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk)
		{
			AddSelectedFiles(selectionCriteria, directoryOnDisk, null, recurseDirectories: false);
		}

		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk, bool recurseDirectories)
		{
			AddSelectedFiles(selectionCriteria, directoryOnDisk, null, recurseDirectories);
		}

		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive)
		{
			AddSelectedFiles(selectionCriteria, directoryOnDisk, directoryPathInArchive, recurseDirectories: false);
		}

		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive, bool recurseDirectories)
		{
			_AddOrUpdateSelectedFiles(selectionCriteria, directoryOnDisk, directoryPathInArchive, recurseDirectories, wantUpdate: false);
		}

		public void UpdateSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive, bool recurseDirectories)
		{
			_AddOrUpdateSelectedFiles(selectionCriteria, directoryOnDisk, directoryPathInArchive, recurseDirectories, wantUpdate: true);
		}

		private string EnsureendInSlash(string s)
		{
			if (s.EndsWith("\\"))
			{
				return s;
			}
			return s + "\\";
		}

		private void _AddOrUpdateSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive, bool recurseDirectories, bool wantUpdate)
		{
			if (directoryOnDisk == null && Directory.Exists(selectionCriteria))
			{
				directoryOnDisk = selectionCriteria;
				selectionCriteria = "*.*";
			}
			else if (string.IsNullOrEmpty(directoryOnDisk))
			{
				directoryOnDisk = ".";
			}
			while (directoryOnDisk.EndsWith("\\"))
			{
				directoryOnDisk = directoryOnDisk.Substring(0, directoryOnDisk.Length - 1);
			}
			if (Verbose)
			{
				StatusMessageTextWriter.WriteLine("adding selection '{0}' from dir '{1}'...", selectionCriteria, directoryOnDisk);
			}
			FileSelector fileSelector = new FileSelector(selectionCriteria, AddDirectoryWillTraverseReparsePoints);
			ReadOnlyCollection<string> readOnlyCollection = fileSelector.SelectFiles(directoryOnDisk, recurseDirectories);
			if (Verbose)
			{
				StatusMessageTextWriter.WriteLine("found {0} files...", readOnlyCollection.Count);
			}
			OnAddStarted();
			AddOrUpdateAction action = (wantUpdate ? AddOrUpdateAction.AddOrUpdate : AddOrUpdateAction.AddOnly);
			foreach (string item in readOnlyCollection)
			{
				string text = ((directoryPathInArchive == null) ? null : ReplaceLeadingDirectory(Path.GetDirectoryName(item), directoryOnDisk, directoryPathInArchive));
				if (File.Exists(item))
				{
					if (wantUpdate)
					{
						UpdateFile(item, text);
					}
					else
					{
						AddFile(item, text);
					}
				}
				else
				{
					AddOrUpdateDirectoryImpl(item, text, action, recurse: false, 0);
				}
			}
			OnAddCompleted();
		}

		private static string ReplaceLeadingDirectory(string original, string pattern, string replacement)
		{
			string text = original.ToUpper();
			string text2 = pattern.ToUpper();
			if (text.IndexOf(text2) != 0)
			{
				return original;
			}
			return replacement + original.Substring(text2.Length);
		}

		public ICollection<ZipEntry> SelectEntries(string selectionCriteria)
		{
			FileSelector fileSelector = new FileSelector(selectionCriteria, AddDirectoryWillTraverseReparsePoints);
			return fileSelector.SelectEntries(this);
		}

		public ICollection<ZipEntry> SelectEntries(string selectionCriteria, string directoryPathInArchive)
		{
			FileSelector fileSelector = new FileSelector(selectionCriteria, AddDirectoryWillTraverseReparsePoints);
			return fileSelector.SelectEntries(this, directoryPathInArchive);
		}

		public int RemoveSelectedEntries(string selectionCriteria)
		{
			ICollection<ZipEntry> collection = SelectEntries(selectionCriteria);
			RemoveEntries(collection);
			return collection.Count;
		}

		public int RemoveSelectedEntries(string selectionCriteria, string directoryPathInArchive)
		{
			ICollection<ZipEntry> collection = SelectEntries(selectionCriteria, directoryPathInArchive);
			RemoveEntries(collection);
			return collection.Count;
		}

		public void ExtractSelectedEntries(string selectionCriteria)
		{
			foreach (ZipEntry item in SelectEntries(selectionCriteria))
			{
				item.Password = _Password;
				item.Extract();
			}
		}

		public void ExtractSelectedEntries(string selectionCriteria, ExtractExistingFileAction extractExistingFile)
		{
			foreach (ZipEntry item in SelectEntries(selectionCriteria))
			{
				item.Password = _Password;
				item.Extract(extractExistingFile);
			}
		}

		public void ExtractSelectedEntries(string selectionCriteria, string directoryPathInArchive)
		{
			foreach (ZipEntry item in SelectEntries(selectionCriteria, directoryPathInArchive))
			{
				item.Password = _Password;
				item.Extract();
			}
		}

		public void ExtractSelectedEntries(string selectionCriteria, string directoryInArchive, string extractDirectory)
		{
			foreach (ZipEntry item in SelectEntries(selectionCriteria, directoryInArchive))
			{
				item.Password = _Password;
				item.Extract(extractDirectory);
			}
		}

		public void ExtractSelectedEntries(string selectionCriteria, string directoryPathInArchive, string extractDirectory, ExtractExistingFileAction extractExistingFile)
		{
			foreach (ZipEntry item in SelectEntries(selectionCriteria, directoryPathInArchive))
			{
				item.Password = _Password;
				item.Extract(extractDirectory, extractExistingFile);
			}
		}

		public IEnumerator<ZipEntry> GetEnumerator()
		{
			foreach (ZipEntry value in _entries.Values)
			{
				yield return value;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		[DispId(-4)]
		public IEnumerator GetNewEnum()
		{
			return GetEnumerator();
		}
	}
	public enum Zip64Option
	{
		Default = 0,
		Never = 0,
		AsNecessary = 1,
		Always = 2
	}
	public enum ZipOption
	{
		Default = 0,
		Never = 0,
		AsNecessary = 1,
		Always = 2
	}
	internal enum AddOrUpdateAction
	{
		AddOnly,
		AddOrUpdate
	}
	public class ReadOptions
	{
		public EventHandler<ReadProgressEventArgs> ReadProgress { get; set; }

		public TextWriter StatusMessageWriter { get; set; }

		public Encoding Encoding { get; set; }
	}
	internal static class ZipOutput
	{
		public static bool WriteCentralDirectoryStructure(Stream s, ICollection<ZipEntry> entries, uint numSegments, Zip64Option zip64, string comment, ZipContainer container)
		{
			ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
			if (zipSegmentedStream != null)
			{
				zipSegmentedStream.ContiguousWrite = true;
			}
			long num = 0L;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				foreach (ZipEntry entry in entries)
				{
					if (entry.IncludedInMostRecentSave)
					{
						entry.WriteCentralDirectoryEntry(memoryStream);
					}
				}
				byte[] array = memoryStream.ToArray();
				s.Write(array, 0, array.Length);
				num = array.Length;
			}
			long num2 = ((s is CountingStream countingStream) ? countingStream.ComputedPosition : s.Position);
			long num3 = num2 - num;
			uint num4 = zipSegmentedStream?.CurrentSegment ?? 0;
			long num5 = num2 - num3;
			int num6 = CountEntries(entries);
			bool flag = zip64 == Zip64Option.Always || num6 >= 65535 || num5 > uint.MaxValue || num3 > uint.MaxValue;
			byte[] array2 = null;
			if (flag)
			{
				if (zip64 == Zip64Option.Default)
				{
					StackFrame stackFrame = new StackFrame(1);
					if ((object)stackFrame.GetMethod().DeclaringType == typeof(ZipFile))
					{
						throw new ZipException("The archive requires a ZIP64 Central Directory. Consider setting the ZipFile.UseZip64WhenSaving property.");
					}
					throw new ZipException("The archive requires a ZIP64 Central Directory. Consider setting the ZipOutputStream.EnableZip64 property.");
				}
				byte[] array3 = GenZip64EndOfCentralDirectory(num3, num2, num6, numSegments);
				array2 = GenCentralDirectoryFooter(num3, num2, zip64, num6, comment, container);
				if (num4 != 0)
				{
					uint value = zipSegmentedStream.ComputeSegment(array3.Length + array2.Length);
					int num7 = 16;
					Array.Copy(BitConverter.GetBytes(value), 0, array3, num7, 4);
					num7 += 4;
					Array.Copy(BitConverter.GetBytes(value), 0, array3, num7, 4);
					num7 = 60;
					Array.Copy(BitConverter.GetBytes(value), 0, array3, num7, 4);
					num7 += 4;
					num7 += 8;
					Array.Copy(BitConverter.GetBytes(value), 0, array3, num7, 4);
				}
				s.Write(array3, 0, array3.Length);
			}
			else
			{
				array2 = GenCentralDirectoryFooter(num3, num2, zip64, num6, comment, container);
			}
			if (num4 != 0)
			{
				ushort value2 = (ushort)zipSegmentedStream.ComputeSegment(array2.Length);
				int num8 = 4;
				Array.Copy(BitConverter.GetBytes(value2), 0, array2, num8, 2);
				num8 += 2;
				Array.Copy(BitConverter.GetBytes(value2), 0, array2, num8, 2);
				num8 += 2;
			}
			s.Write(array2, 0, array2.Length);
			if (zipSegmentedStream != null)
			{
				zipSegmentedStream.ContiguousWrite = false;
			}
			return flag;
		}

		private static Encoding GetEncoding(ZipContainer container, string t)
		{
			switch (container.AlternateEncodingUsage)
			{
			case ZipOption.Always:
				return container.AlternateEncoding;
			case ZipOption.Default:
				return container.DefaultEncoding;
			default:
			{
				Encoding defaultEncoding = container.DefaultEncoding;
				if (t == null)
				{
					return defaultEncoding;
				}
				byte[] bytes = defaultEncoding.GetBytes(t);
				string text = defaultEncoding.GetString(bytes, 0, bytes.Length);
				if (text.Equals(t))
				{
					return defaultEncoding;
				}
				return container.AlternateEncoding;
			}
			}
		}

		private static byte[] GenCentralDirectoryFooter(long StartOfCentralDirectory, long EndOfCentralDirectory, Zip64Option zip64, int entryCount, string comment, ZipContainer container)
		{
			Encoding encoding = GetEncoding(container, comment);
			int num = 0;
			int num2 = 22;
			byte[] array = null;
			short num3 = 0;
			if (comment != null && comment.Length != 0)
			{
				array = encoding.GetBytes(comment);
				num3 = (short)array.Length;
			}
			num2 += num3;
			byte[] array2 = new byte[num2];
			int num4 = 0;
			byte[] bytes = BitConverter.GetBytes(101010256u);
			Array.Copy(bytes, 0, array2, num4, 4);
			num4 += 4;
			array2[num4++] = 0;
			array2[num4++] = 0;
			array2[num4++] = 0;
			array2[num4++] = 0;
			if (entryCount >= 65535 || zip64 == Zip64Option.Always)
			{
				for (num = 0; num < 4; num++)
				{
					array2[num4++] = byte.MaxValue;
				}
			}
			else
			{
				array2[num4++] = (byte)(entryCount & 0xFF);
				array2[num4++] = (byte)((entryCount & 0xFF00) >> 8);
				array2[num4++] = (byte)(entryCount & 0xFF);
				array2[num4++] = (byte)((entryCount & 0xFF00) >> 8);
			}
			long num5 = EndOfCentralDirectory - StartOfCentralDirectory;
			if (num5 >= uint.MaxValue || StartOfCentralDirectory >= uint.MaxValue)
			{
				for (num = 0; num < 8; num++)
				{
					array2[num4++] = byte.MaxValue;
				}
			}
			else
			{
				array2[num4++] = (byte)(num5 & 0xFF);
				array2[num4++] = (byte)((num5 & 0xFF00) >> 8);
				array2[num4++] = (byte)((num5 & 0xFF0000) >> 16);
				array2[num4++] = (byte)((num5 & 0xFF000000u) >> 24);
				array2[num4++] = (byte)(StartOfCentralDirectory & 0xFF);
				array2[num4++] = (byte)((StartOfCentralDirectory & 0xFF00) >> 8);
				array2[num4++] = (byte)((StartOfCentralDirectory & 0xFF0000) >> 16);
				array2[num4++] = (byte)((StartOfCentralDirectory & 0xFF000000u) >> 24);
			}
			if (comment == null || comment.Length == 0)
			{
				array2[num4++] = 0;
				array2[num4++] = 0;
			}
			else
			{
				if (num3 + num4 + 2 > array2.Length)
				{
					num3 = (short)(array2.Length - num4 - 2);
				}
				array2[num4++] = (byte)(num3 & 0xFF);
				array2[num4++] = (byte)((num3 & 0xFF00) >> 8);
				if (num3 != 0)
				{
					for (num = 0; num < num3 && num4 + num < array2.Length; num++)
					{
						array2[num4 + num] = array[num];
					}
					num4 += num;
				}
			}
			return array2;
		}

		private static byte[] GenZip64EndOfCentralDirectory(long StartOfCentralDirectory, long EndOfCentralDirectory, int entryCount, uint numSegments)
		{
			byte[] array = new byte[76];
			int num = 0;
			byte[] bytes = BitConverter.GetBytes(101075792u);
			Array.Copy(bytes, 0, array, num, 4);
			num += 4;
			long value = 44L;
			Array.Copy(BitConverter.GetBytes(value), 0, array, num, 8);
			num += 8;
			array[num++] = 45;
			array[num++] = 0;
			array[num++] = 45;
			array[num++] = 0;
			for (int i = 0; i < 8; i++)
			{
				array[num++] = 0;
			}
			long value2 = entryCount;
			Array.Copy(BitConverter.GetBytes(value2), 0, array, num, 8);
			num += 8;
			Array.Copy(BitConverter.GetBytes(value2), 0, array, num, 8);
			num += 8;
			long value3 = EndOfCentralDirectory - StartOfCentralDirectory;
			Array.Copy(BitConverter.GetBytes(value3), 0, array, num, 8);
			num += 8;
			Array.Copy(BitConverter.GetBytes(StartOfCentralDirectory), 0, array, num, 8);
			num += 8;
			bytes = BitConverter.GetBytes(117853008u);
			Array.Copy(bytes, 0, array, num, 4);
			num += 4;
			uint value4 = ((numSegments != 0) ? (numSegments - 1) : 0u);
			Array.Copy(BitConverter.GetBytes(value4), 0, array, num, 4);
			num += 4;
			Array.Copy(BitConverter.GetBytes(EndOfCentralDirectory), 0, array, num, 8);
			num += 8;
			Array.Copy(BitConverter.GetBytes(numSegments), 0, array, num, 4);
			num += 4;
			return array;
		}

		private static int CountEntries(ICollection<ZipEntry> _entries)
		{
			int num = 0;
			foreach (ZipEntry _entry in _entries)
			{
				if (_entry.IncludedInMostRecentSave)
				{
					num++;
				}
			}
			return num;
		}
	}
	public enum SelfExtractorFlavor
	{
		ConsoleApplication,
		WinFormsApplication
	}
	public class SelfExtractorSaveOptions
	{
		public SelfExtractorFlavor Flavor { get; set; }

		public string PostExtractCommandLine { get; set; }

		public string DefaultExtractDirectory { get; set; }

		public string IconFile { get; set; }

		public bool Quiet { get; set; }

		public ExtractExistingFileAction ExtractExistingFile { get; set; }

		public bool RemoveUnpackedFilesAfterExecute { get; set; }

		public Version FileVersion { get; set; }

		public string ProductVersion { get; set; }

		public string Copyright { get; set; }

		public string Description { get; set; }

		public string ProductName { get; set; }

		public string SfxExeWindowTitle { get; set; }

		public string AdditionalCompilerSwitches { get; set; }
	}
	public class ZipInputStream : Stream
	{
		private Stream _inputStream;

		private Encoding _provisionalAlternateEncoding;

		private ZipEntry _currentEntry;

		private bool _firstEntry;

		private bool _needSetup;

		private ZipContainer _container;

		private CrcCalculatorStream _crcStream;

		private long _LeftToRead;

		internal string _Password;

		private long _endOfEntry;

		private string _name;

		private bool _leaveUnderlyingStreamOpen;

		private bool _closed;

		private bool _findRequired;

		private bool _exceptionPending;

		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				return _provisionalAlternateEncoding;
			}
			set
			{
				_provisionalAlternateEncoding = value;
			}
		}

		public int CodecBufferSize { get; set; }

		public string Password
		{
			set
			{
				if (_closed)
				{
					_exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				_Password = value;
			}
		}

		internal Stream ReadStream => _inputStream;

		public override bool CanRead => true;

		public override bool CanSeek => _inputStream.CanSeek;

		public override bool CanWrite => false;

		public override long Length => _inputStream.Length;

		public override long Position
		{
			get
			{
				return _inputStream.Position;
			}
			set
			{
				Seek(value, SeekOrigin.Begin);
			}
		}

		public ZipInputStream(Stream stream)
			: this(stream, leaveOpen: false)
		{
		}

		public ZipInputStream(string fileName)
		{
			Stream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			_Init(stream, leaveOpen: false, fileName);
		}

		public ZipInputStream(Stream stream, bool leaveOpen)
		{
			_Init(stream, leaveOpen, null);
		}

		private void _Init(Stream stream, bool leaveOpen, string name)
		{
			_inputStream = stream;
			if (!_inputStream.CanRead)
			{
				throw new ZipException("The stream must be readable.");
			}
			_container = new ZipContainer(this);
			_provisionalAlternateEncoding = Encoding.GetEncoding("IBM437");
			_leaveUnderlyingStreamOpen = leaveOpen;
			_findRequired = true;
			_name = name ?? "(stream)";
		}

		public override string ToString()
		{
			return $"ZipInputStream::{_name}(leaveOpen({_leaveUnderlyingStreamOpen})))";
		}

		private void SetupStream()
		{
			_crcStream = _currentEntry.InternalOpenReader(_Password);
			_LeftToRead = _crcStream.Length;
			_needSetup = false;
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (_closed)
			{
				_exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			if (_needSetup)
			{
				SetupStream();
			}
			if (_LeftToRead == 0)
			{
				return 0;
			}
			int count2 = (int)((_LeftToRead > count) ? count : _LeftToRead);
			int num = _crcStream.Read(buffer, offset, count2);
			_LeftToRead -= num;
			if (_LeftToRead == 0)
			{
				int crc = _crcStream.Crc;
				_currentEntry.VerifyCrcAfterExtract(crc);
				_inputStream.Seek(_endOfEntry, SeekOrigin.Begin);
			}
			return num;
		}

		public ZipEntry GetNextEntry()
		{
			if (_findRequired)
			{
				long num = SharedUtilities.FindSignature(_inputStream, 67324752);
				if (num == -1)
				{
					return null;
				}
				_inputStream.Seek(-4L, SeekOrigin.Current);
			}
			else if (_firstEntry)
			{
				_inputStream.Seek(_endOfEntry, SeekOrigin.Begin);
			}
			_currentEntry = ZipEntry.ReadEntry(_container, !_firstEntry);
			_endOfEntry = _inputStream.Position;
			_firstEntry = true;
			_needSetup = true;
			_findRequired = false;
			return _currentEntry;
		}

		protected override void Dispose(bool disposing)
		{
			if (_closed)
			{
				return;
			}
			if (disposing)
			{
				if (_exceptionPending)
				{
					return;
				}
				if (!_leaveUnderlyingStreamOpen)
				{
					_inputStream.Dispose();
				}
			}
			_closed = true;
		}

		public override void Flush()
		{
			throw new NotSupportedException("Flush");
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("Write");
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			_findRequired = true;
			return _inputStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}
	}
	public class ZipOutputStream : Stream
	{
		private EncryptionAlgorithm _encryption;

		private ZipEntryTimestamp _timestamp;

		internal string _password;

		private string _comment;

		private Stream _outputStream;

		private ZipEntry _currentEntry;

		internal Zip64Option _zip64;

		private Dictionary<string, ZipEntry> _entriesWritten;

		private int _entryCount;

		private ZipOption _alternateEncodingUsage;

		private Encoding _alternateEncoding = Encoding.GetEncoding("IBM437");

		private bool _leaveUnderlyingStreamOpen;

		private bool _disposed;

		private bool _exceptionPending;

		private bool _anyEntriesUsedZip64;

		private bool _directoryNeededZip64;

		private CountingStream _outputCounter;

		private Stream _encryptor;

		private Stream _deflater;

		private CrcCalculatorStream _entryOutputStream;

		private bool _needToWriteEntryHeader;

		private string _name;

		private bool _DontIgnoreCase;

		internal ParallelDeflateOutputStream ParallelDeflater;

		private long _ParallelDeflateThreshold;

		private int _maxBufferPairs = 16;

		public string Password
		{
			set
			{
				if (_disposed)
				{
					_exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				_password = value;
				if (_password == null)
				{
					_encryption = EncryptionAlgorithm.None;
				}
				else if (_encryption == EncryptionAlgorithm.None)
				{
					_encryption = EncryptionAlgorithm.PkzipWeak;
				}
			}
		}

		public EncryptionAlgorithm Encryption
		{
			get
			{
				return _encryption;
			}
			set
			{
				if (_disposed)
				{
					_exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				if (value == EncryptionAlgorithm.Unsupported)
				{
					_exceptionPending = true;
					throw new InvalidOperationException("You may not set Encryption to that value.");
				}
				_encryption = value;
			}
		}

		public int CodecBufferSize { get; set; }

		public CompressionStrategy Strategy { get; set; }

		public ZipEntryTimestamp Timestamp
		{
			get
			{
				return _timestamp;
			}
			set
			{
				if (_disposed)
				{
					_exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				_timestamp = value;
			}
		}

		public CompressionLevel CompressionLevel { get; set; }

		public CompressionMethod CompressionMethod { get; set; }

		public string Comment
		{
			get
			{
				return _comment;
			}
			set
			{
				if (_disposed)
				{
					_exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				_comment = value;
			}
		}

		public Zip64Option EnableZip64
		{
			get
			{
				return _zip64;
			}
			set
			{
				if (_disposed)
				{
					_exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				_zip64 = value;
			}
		}

		public bool OutputUsedZip64
		{
			get
			{
				if (!_anyEntriesUsedZip64)
				{
					return _directoryNeededZip64;
				}
				return true;
			}
		}

		public bool IgnoreCase
		{
			get
			{
				return !_DontIgnoreCase;
			}
			set
			{
				_DontIgnoreCase = !value;
			}
		}

		[Obsolete("Beginning with v1.9.1.6 of DotNetZip, this property is obsolete. It will be removed in a future version of the library. Use AlternateEncoding and AlternateEncodingUsage instead.")]
		public bool UseUnicodeAsNecessary
		{
			get
			{
				if (_alternateEncoding == Encoding.UTF8)
				{
					return AlternateEncodingUsage == ZipOption.AsNecessary;
				}
				return false;
			}
			set
			{
				if (value)
				{
					_alternateEncoding = Encoding.UTF8;
					_alternateEncodingUsage = ZipOption.AsNecessary;
				}
				else
				{
					_alternateEncoding = DefaultEncoding;
					_alternateEncodingUsage = ZipOption.Default;
				}
			}
		}

		[Obsolete("use AlternateEncoding and AlternateEncodingUsage instead.")]
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				if (_alternateEncodingUsage == ZipOption.AsNecessary)
				{
					return _alternateEncoding;
				}
				return null;
			}
			set
			{
				_alternateEncoding = value;
				_alternateEncodingUsage = ZipOption.AsNecessary;
			}
		}

		public Encoding AlternateEncoding
		{
			get
			{
				return _alternateEncoding;
			}
			set
			{
				_alternateEncoding = value;
			}
		}

		public ZipOption AlternateEncodingUsage
		{
			get
			{
				return _alternateEncodingUsage;
			}
			set
			{
				_alternateEncodingUsage = value;
			}
		}

		public static Encoding DefaultEncoding => Encoding.GetEncoding("IBM437");

		public long ParallelDeflateThreshold
		{
			get
			{
				return _ParallelDeflateThreshold;
			}
			set
			{
				if (value != 0 && value != -1 && value < 65536)
				{
					throw new ArgumentOutOfRangeException("value must be greater than 64k, or 0, or -1");
				}
				_ParallelDeflateThreshold = value;
			}
		}

		public int ParallelDeflateMaxBufferPairs
		{
			get
			{
				return _maxBufferPairs;
			}
			set
			{
				if (value < 4)
				{
					throw new ArgumentOutOfRangeException("ParallelDeflateMaxBufferPairs", "Value must be 4 or greater.");
				}
				_maxBufferPairs = value;
			}
		}

		internal Stream OutputStream => _outputStream;

		internal string Name => _name;

		public override bool CanRead => false;

		public override bool CanSeek => false;

		public override bool CanWrite => true;

		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public override long Position
		{
			get
			{
				return _outputStream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public ZipOutputStream(Stream stream)
			: this(stream, leaveOpen: false)
		{
		}

		public ZipOutputStream(string fileName)
		{
			Stream stream = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
			_Init(stream, leaveOpen: false, fileName);
		}

		public ZipOutputStream(Stream stream, bool leaveOpen)
		{
			_Init(stream, leaveOpen, null);
		}

		private void _Init(Stream stream, bool leaveOpen, string name)
		{
			_outputStream = (stream.CanRead ? stream : new CountingStream(stream));
			CompressionLevel = CompressionLevel.Default;
			CompressionMethod = CompressionMethod.Deflate;
			_encryption = EncryptionAlgorithm.None;
			_entriesWritten = new Dictionary<string, ZipEntry>(StringComparer.Ordinal);
			_zip64 = Zip64Option.Default;
			_leaveUnderlyingStreamOpen = leaveOpen;
			Strategy = CompressionStrategy.Default;
			_name = name ?? "(stream)";
			ParallelDeflateThreshold = -1L;
		}

		public override string ToString()
		{
			return $"ZipOutputStream::{_name}(leaveOpen({_leaveUnderlyingStreamOpen})))";
		}

		private void InsureUniqueEntry(ZipEntry ze1)
		{
			if (_entriesWritten.ContainsKey(ze1.FileName))
			{
				_exceptionPending = true;
				throw new ArgumentException($"The entry '{ze1.FileName}' already exists in the zip archive.");
			}
		}

		public bool ContainsEntry(string name)
		{
			return _entriesWritten.ContainsKey(SharedUtilities.NormalizePathForUseInZipFile(name));
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (_disposed)
			{
				_exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			if (buffer == null)
			{
				_exceptionPending = true;
				throw new ArgumentNullException("buffer");
			}
			if (_currentEntry == null)
			{
				_exceptionPending = true;
				throw new InvalidOperationException("You must call PutNextEntry() before calling Write().");
			}
			if (_currentEntry.IsDirectory)
			{
				_exceptionPending = true;
				throw new InvalidOperationException("You cannot Write() data for an entry that is a directory.");
			}
			if (_needToWriteEntryHeader)
			{
				_InitiateCurrentEntry(finishing: false);
			}
			if (count != 0)
			{
				_entryOutputStream.Write(buffer, offset, count);
			}
		}

		public ZipEntry PutNextEntry(string entryName)
		{
			if (string.IsNullOrEmpty(entryName))
			{
				throw new ArgumentNullException("entryName");
			}
			if (_disposed)
			{
				_exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			_FinishCurrentEntry();
			_currentEntry = ZipEntry.CreateForZipOutputStream(entryName);
			_currentEntry._container = new ZipContainer(this);
			_currentEntry._BitField |= 8;
			_currentEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			_currentEntry.CompressionLevel = CompressionLevel;
			_currentEntry.CompressionMethod = CompressionMethod;
			_currentEntry.Password = _password;
			_currentEntry.Encryption = Encryption;
			_currentEntry.AlternateEncoding = AlternateEncoding;
			_currentEntry.AlternateEncodingUsage = AlternateEncodingUsage;
			if (entryName.EndsWith("/"))
			{
				_currentEntry.MarkAsDirectory();
			}
			_currentEntry.EmitTimesInWindowsFormatWhenSaving = (_timestamp & ZipEntryTimestamp.Windows) != 0;
			_currentEntry.EmitTimesInUnixFormatWhenSaving = (_timestamp & ZipEntryTimestamp.Unix) != 0;
			InsureUniqueEntry(_currentEntry);
			_needToWriteEntryHeader = true;
			return _currentEntry;
		}

		private void _InitiateCurrentEntry(bool finishing)
		{
			_entriesWritten.Add(_currentEntry.FileName, _currentEntry);
			_entryCount++;
			if (_entryCount > 65534 && _zip64 == Zip64Option.Default)
			{
				_exceptionPending = true;
				throw new InvalidOperationException("Too many entries. Consider setting ZipOutputStream.EnableZip64.");
			}
			_currentEntry.WriteHeader(_outputStream, finishing ? 99 : 0);
			_currentEntry.StoreRelativeOffset();
			if (!_currentEntry.IsDirectory)
			{
				_currentEntry.WriteSecurityMetadata(_outputStream);
				_currentEntry.PrepOutputStream(_outputStream, (!finishing) ? (-1) : 0, out _outputCounter, out _encryptor, out _deflater, out _entryOutputStream);
			}
			_needToWriteEntryHeader = false;
		}

		private void _FinishCurrentEntry()
		{
			if (_currentEntry != null)
			{
				if (_needToWriteEntryHeader)
				{
					_InitiateCurrentEntry(finishing: true);
				}
				_currentEntry.FinishOutputStream(_outputStream, _outputCounter, _encryptor, _deflater, _entryOutputStream);
				_currentEntry.PostProcessOutput(_outputStream);
				if (_currentEntry.OutputUsedZip64.HasValue)
				{
					_anyEntriesUsedZip64 |= _currentEntry.OutputUsedZip64.Value;
				}
				_outputCounter = null;
				_encryptor = (_deflater = null);
				_entryOutputStream = null;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}
			if (disposing && !_exceptionPending)
			{
				_FinishCurrentEntry();
				_directoryNeededZip64 = ZipOutput.WriteCentralDirectoryStructure(_outputStream, _entriesWritten.Values, 1u, _zip64, Comment, new ZipContainer(this));
				Stream stream = null;
				if (_outputStream is CountingStream countingStream)
				{
					stream = countingStream.WrappedStream;
					countingStream.Dispose();
				}
				else
				{
					stream = _outputStream;
				}
				if (!_leaveUnderlyingStreamOpen)
				{
					stream.Dispose();
				}
				_outputStream = null;
			}
			_disposed = true;
		}

		public override void Flush()
		{
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("Read");
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Seek");
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}
	}
	internal class ZipContainer
	{
		private ZipFile _zf;

		private ZipOutputStream _zos;

		private ZipInputStream _zis;

		public ZipFile ZipFile => _zf;

		public ZipOutputStream ZipOutputStream => _zos;

		public string Name
		{
			get
			{
				if (_zf != null)
				{
					return _zf.Name;
				}
				if (_zis != null)
				{
					throw new NotSupportedException();
				}
				return _zos.Name;
			}
		}

		public string Password
		{
			get
			{
				if (_zf != null)
				{
					return _zf._Password;
				}
				if (_zis != null)
				{
					return _zis._Password;
				}
				return _zos._password;
			}
		}

		public Zip64Option Zip64
		{
			get
			{
				if (_zf != null)
				{
					return _zf._zip64;
				}
				if (_zis != null)
				{
					throw new NotSupportedException();
				}
				return _zos._zip64;
			}
		}

		public int BufferSize
		{
			get
			{
				if (_zf != null)
				{
					return _zf.BufferSize;
				}
				if (_zis != null)
				{
					throw new NotSupportedException();
				}
				return 0;
			}
		}

		public ParallelDeflateOutputStream ParallelDeflater
		{
			get
			{
				if (_zf != null)
				{
					return _zf.ParallelDeflater;
				}
				if (_zis != null)
				{
					return null;
				}
				return _zos.ParallelDeflater;
			}
			set
			{
				if (_zf != null)
				{
					_zf.ParallelDeflater = value;
				}
				else if (_zos != null)
				{
					_zos.ParallelDeflater = value;
				}
			}
		}

		public long ParallelDeflateThreshold
		{
			get
			{
				if (_zf != null)
				{
					return _zf.ParallelDeflateThreshold;
				}
				return _zos.ParallelDeflateThreshold;
			}
		}

		public int ParallelDeflateMaxBufferPairs
		{
			get
			{
				if (_zf != null)
				{
					return _zf.ParallelDeflateMaxBufferPairs;
				}
				return _zos.ParallelDeflateMaxBufferPairs;
			}
		}

		public int CodecBufferSize
		{
			get
			{
				if (_zf != null)
				{
					return _zf.CodecBufferSize;
				}
				if (_zis != null)
				{
					return _zis.CodecBufferSize;
				}
				return _zos.CodecBufferSize;
			}
		}

		public CompressionStrategy Strategy
		{
			get
			{
				if (_zf != null)
				{
					return _zf.Strategy;
				}
				return _zos.Strategy;
			}
		}

		public Zip64Option UseZip64WhenSaving
		{
			get
			{
				if (_zf != null)
				{
					return _zf.UseZip64WhenSaving;
				}
				return _zos.EnableZip64;
			}
		}

		public Encoding AlternateEncoding
		{
			get
			{
				if (_zf != null)
				{
					return _zf.AlternateEncoding;
				}
				if (_zos != null)
				{
					return _zos.AlternateEncoding;
				}
				return null;
			}
		}

		public Encoding DefaultEncoding
		{
			get
			{
				if (_zf != null)
				{
					return ZipFile.DefaultEncoding;
				}
				if (_zos != null)
				{
					return ZipOutputStream.DefaultEncoding;
				}
				return null;
			}
		}

		public ZipOption AlternateEncodingUsage
		{
			get
			{
				if (_zf != null)
				{
					return _zf.AlternateEncodingUsage;
				}
				if (_zos != null)
				{
					return _zos.AlternateEncodingUsage;
				}
				return ZipOption.Default;
			}
		}

		public Stream ReadStream
		{
			get
			{
				if (_zf != null)
				{
					return _zf.ReadStream;
				}
				return _zis.ReadStream;
			}
		}

		public ZipContainer(object o)
		{
			_zf = o as ZipFile;
			_zos = o as ZipOutputStream;
			_zis = o as ZipInputStream;
		}
	}
	internal class ZipSegmentedStream : Stream
	{
		private enum RwMode
		{
			None,
			ReadOnly,
			Write
		}

		private RwMode rwMode;

		private bool _exceptionPending;

		private string _baseName;

		private string _baseDir;

		private string _currentName;

		private string _currentTempName;

		private uint _currentDiskNumber;

		private uint _maxDiskNumber;

		private int _maxSegmentSize;

		private Stream _innerStream;

		public bool ContiguousWrite { get; set; }

		public uint CurrentSegment
		{
			get
			{
				return _currentDiskNumber;
			}
			private set
			{
				_currentDiskNumber = value;
				_currentName = null;
			}
		}

		public string CurrentName
		{
			get
			{
				if (_currentName == null)
				{
					_currentName = _NameForSegment(CurrentSegment);
				}
				return _currentName;
			}
		}

		public string CurrentTempName => _currentTempName;

		public override bool CanRead
		{
			get
			{
				if (rwMode == RwMode.ReadOnly && _innerStream != null)
				{
					return _innerStream.CanRead;
				}
				return false;
			}
		}

		public override bool CanSeek
		{
			get
			{
				if (_innerStream != null)
				{
					return _innerStream.CanSeek;
				}
				return false;
			}
		}

		public override bool CanWrite
		{
			get
			{
				if (rwMode == RwMode.Write && _innerStream != null)
				{
					return _innerStream.CanWrite;
				}
				return false;
			}
		}

		public override long Length => _innerStream.Length;

		public override long Position
		{
			get
			{
				return _innerStream.Position;
			}
			set
			{
				_innerStream.Position = value;
			}
		}

		private ZipSegmentedStream()
		{
			_exceptionPending = false;
		}

		public static ZipSegmentedStream ForReading(string name, uint initialDiskNumber, uint maxDiskNumber)
		{
			ZipSegmentedStream zipSegmentedStream = new ZipSegmentedStream();
			zipSegmentedStream.rwMode = RwMode.ReadOnly;
			zipSegmentedStream.CurrentSegment = initialDiskNumber;
			zipSegmentedStream._maxDiskNumber = maxDiskNumber;
			zipSegmentedStream._baseName = name;
			ZipSegmentedStream zipSegmentedStream2 = zipSegmentedStream;
			zipSegmentedStream2._SetReadStream();
			return zipSegmentedStream2;
		}

		public static ZipSegmentedStream ForWriting(string name, int maxSegmentSize)
		{
			ZipSegmentedStream zipSegmentedStream = new ZipSegmentedStream();
			zipSegmentedStream.rwMode = RwMode.Write;
			zipSegmentedStream.CurrentSegment = 0u;
			zipSegmentedStream._baseName = name;
			zipSegmentedStream._maxSegmentSize = maxSegmentSize;
			zipSegmentedStream._baseDir = Path.GetDirectoryName(name);
			ZipSegmentedStream zipSegmentedStream2 = zipSegmentedStream;
			if (zipSegmentedStream2._baseDir == "")
			{
				zipSegmentedStream2._baseDir = ".";
			}
			zipSegmentedStream2._SetWriteStream(0u);
			return zipSegmentedStream2;
		}

		public static Stream ForUpdate(string name, uint diskNumber)
		{
			if (diskNumber >= 99)
			{
				throw new ArgumentOutOfRangeException("diskNumber");
			}
			string path = $"{Path.Combine(Path.GetDirectoryName(name), Path.GetFileNameWithoutExtension(name))}.z{diskNumber + 1:D2}";
			return File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
		}

		private string _NameForSegment(uint diskNumber)
		{
			if (diskNumber >= 99)
			{
				_exceptionPending = true;
				throw new OverflowException("The number of zip segments would exceed 99.");
			}
			return $"{Path.Combine(Path.GetDirectoryName(_baseName), Path.GetFileNameWithoutExtension(_baseName))}.z{diskNumber + 1:D2}";
		}

		public uint ComputeSegment(int length)
		{
			if (_innerStream.Position + length > _maxSegmentSize)
			{
				return CurrentSegment + 1;
			}
			return CurrentSegment;
		}

		public override string ToString()
		{
			return string.Format("{0}[{1}][{2}], pos=0x{3:X})", "ZipSegmentedStream", CurrentName, rwMode.ToString(), Position);
		}

		private void _SetReadStream()
		{
			if (_innerStream != null)
			{
				_innerStream.Dispose();
			}
			if (CurrentSegment + 1 == _maxDiskNumber)
			{
				_currentName = _baseName;
			}
			_innerStream = File.OpenRead(CurrentName);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (rwMode != RwMode.ReadOnly)
			{
				_exceptionPending = true;
				throw new InvalidOperationException("Stream Error: Cannot Read.");
			}
			int num = _innerStream.Read(buffer, offset, count);
			int num2 = num;
			while (num2 != count)
			{
				if (_innerStream.Position != _innerStream.Length)
				{
					_exceptionPending = true;
					throw new ZipException($"Read error in file {CurrentName}");
				}
				if (CurrentSegment + 1 == _maxDiskNumber)
				{
					return num;
				}
				CurrentSegment++;
				_SetReadStream();
				offset += num2;
				count -= num2;
				num2 = _innerStream.Read(buffer, offset, count);
				num += num2;
			}
			return num;
		}

		private void _SetWriteStream(uint increment)
		{
			if (_innerStream != null)
			{
				_innerStream.Dispose();
				if (File.Exists(CurrentName))
				{
					File.Delete(CurrentName);
				}
				File.Move(_currentTempName, CurrentName);
			}
			if (increment != 0)
			{
				CurrentSegment += increment;
			}
			SharedUtilities.CreateAndOpenUniqueTempFile(_baseDir, out _innerStream, out _currentTempName);
			if (CurrentSegment == 0)
			{
				_innerStream.Write(BitConverter.GetBytes(134695760), 0, 4);
			}
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (rwMode != RwMode.Write)
			{
				_exceptionPending = true;
				throw new InvalidOperationException("Stream Error: Cannot Write.");
			}
			if (ContiguousWrite)
			{
				if (_innerStream.Position + count > _maxSegmentSize)
				{
					_SetWriteStream(1u);
				}
			}
			else
			{
				while (_innerStream.Position + count > _maxSegmentSize)
				{
					int num = _maxSegmentSize - (int)_innerStream.Position;
					_innerStream.Write(buffer, offset, num);
					_SetWriteStream(1u);
					count -= num;
					offset += num;
				}
			}
			_innerStream.Write(buffer, offset, count);
		}

		public long TruncateBackward(uint diskNumber, long offset)
		{
			if (diskNumber >= 99)
			{
				throw new ArgumentOutOfRangeException("diskNumber");
			}
			if (rwMode != RwMode.Write)
			{
				_exceptionPending = true;
				throw new ZipException("bad state.");
			}
			if (diskNumber == CurrentSegment)
			{
				return _innerStream.Seek(offset, SeekOrigin.Begin);
			}
			if (_innerStream != null)
			{
				_innerStream.Dispose();
				if (File.Exists(_currentTempName))
				{
					File.Delete(_currentTempName);
				}
			}
			for (uint num = CurrentSegment - 1; num > diskNumber; num--)
			{
				string path = _NameForSegment(num);
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
			CurrentSegment = diskNumber;
			for (int i = 0; i < 3; i++)
			{
				try
				{
					_currentTempName = SharedUtilities.InternalGetTempFileName();
					File.Move(CurrentName, _currentTempName);
				}
				catch (IOException)
				{
					if (i == 2)
					{
						throw;
					}
					continue;
				}
				break;
			}
			_innerStream = new FileStream(_currentTempName, FileMode.Open);
			return _innerStream.Seek(offset, SeekOrigin.Begin);
		}

		public override void Flush()
		{
			_innerStream.Flush();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return _innerStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			if (rwMode != RwMode.Write)
			{
				_exceptionPending = true;
				throw new InvalidOperationException();
			}
			_innerStream.SetLength(value);
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (_innerStream != null)
				{
					_innerStream.Dispose();
					if (rwMode == RwMode.Write)
					{
						_ = _exceptionPending;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}
	}
}
namespace Ionic.BZip2
{
	internal class BitWriter
	{
		private uint accumulator;

		private int nAccumulatedBits;

		private Stream output;

		private int totalBytesWrittenOut;

		public byte RemainingBits => (byte)((accumulator >> 32 - nAccumulatedBits) & 0xFF);

		public int NumRemainingBits => nAccumulatedBits;

		public int TotalBytesWrittenOut => totalBytesWrittenOut;

		public BitWriter(Stream s)
		{
			output = s;
		}

		public void Reset()
		{
			accumulator = 0u;
			nAccumulatedBits = 0;
			totalBytesWrittenOut = 0;
			output.Seek(0L, SeekOrigin.Begin);
			output.SetLength(0L);
		}

		public void WriteBits(int nbits, uint value)
		{
			int num = nAccumulatedBits;
			uint num2 = accumulator;
			while (num >= 8)
			{
				output.WriteByte((byte)((num2 >> 24) & 0xFF));
				totalBytesWrittenOut++;
				num2 <<= 8;
				num -= 8;
			}
			accumulator = num2 | (value << 32 - num - nbits);
			nAccumulatedBits = num + nbits;
		}

		public void WriteByte(byte b)
		{
			WriteBits(8, b);
		}

		public void WriteInt(uint u)
		{
			WriteBits(8, (u >> 24) & 0xFF);
			WriteBits(8, (u >> 16) & 0xFF);
			WriteBits(8, (u >> 8) & 0xFF);
			WriteBits(8, u & 0xFF);
		}

		public void Flush()
		{
			WriteBits(0, 0u);
		}

		public void FinishAndPad()
		{
			Flush();
			if (NumRemainingBits > 0)
			{
				byte value = (byte)((accumulator >> 24) & 0xFF);
				output.WriteByte(value);
				totalBytesWrittenOut++;
			}
		}
	}
	internal class BZip2Compressor
	{
		private class CompressionState
		{
			public readonly bool[] inUse = new bool[256];

			public readonly byte[] unseqToSeq = new byte[256];

			public readonly int[] mtfFreq = new int[BZip2.MaxAlphaSize];

			public readonly byte[] selector = new byte[BZip2.MaxSelectors];

			public readonly byte[] selectorMtf = new byte[BZip2.MaxSelectors];

			public readonly byte[] generateMTFValues_yy = new byte[256];

			public byte[][] sendMTFValues_len;

			public int[][] sendMTFValues_rfreq;

			public readonly int[] sendMTFValues_fave = new int[BZip2.NGroups];

			public readonly short[] sendMTFValues_cost = new short[BZip2.NGroups];

			public int[][] sendMTFValues_code;

			public readonly byte[] sendMTFValues2_pos = new byte[BZip2.NGroups];

			public readonly bool[] sentMTFValues4_inUse16 = new bool[16];

			public readonly int[] stack_ll = new int[BZip2.QSORT_STACK_SIZE];

			public readonly int[] stack_hh = new int[BZip2.QSORT_STACK_SIZE];

			public readonly int[] stack_dd = new int[BZip2.QSORT_STACK_SIZE];

			public readonly int[] mainSort_runningOrder = new int[256];

			public readonly int[] mainSort_copy = new int[256];

			public readonly bool[] mainSort_bigDone = new bool[256];

			public int[] heap = new int[BZip2.MaxAlphaSize + 2];

			public int[] weight = new int[BZip2.MaxAlphaSize * 2];

			public int[] parent = new int[BZip2.MaxAlphaSize * 2];

			public readonly int[] ftab = new int[65537];

			public byte[] block;

			public int[] fmap;

			public char[] sfmap;

			public char[] quadrant;

			public CompressionState(int blockSize100k)
			{
				int num = blockSize100k * BZip2.BlockSizeMultiple;
				block = new byte[num + 1 + BZip2.NUM_OVERSHOOT_BYTES];
				fmap = new int[num];
				sfmap = new char[2 * num];
				quadrant = sfmap;
				sendMTFValues_len = BZip2.InitRectangularArray<byte>(BZip2.NGroups, BZip2.MaxAlphaSize);
				sendMTFValues_rfreq = BZip2.InitRectangularArray<int>(BZip2.NGroups, BZip2.MaxAlphaSize);
				sendMTFValues_code = BZip2.InitRectangularArray<int>(BZip2.NGroups, BZip2.MaxAlphaSize);
			}
		}

		private int blockSize100k;

		private int currentByte = -1;

		private int runLength;

		private int last;

		private int outBlockFillThreshold;

		private CompressionState cstate;

		private readonly CRC32 crc = new CRC32(reverseBits: true);

		private BitWriter bw;

		private int runs;

		private int workDone;

		private int workLimit;

		private bool firstAttempt;

		private bool blockRandomised;

		private int origPtr;

		private int nInUse;

		private int nMTF;

		private static readonly int SETMASK = 2097152;

		private static readonly int CLEARMASK = ~SETMASK;

		private static readonly byte GREATER_ICOST = 15;

		private static readonly byte LESSER_ICOST = 0;

		private static readonly int SMALL_THRESH = 20;

		private static readonly int DEPTH_THRESH = 10;

		private static readonly int WORK_FACTOR = 30;

		private static readonly int[] increments = new int[14]
		{
			1, 4, 13, 40, 121, 364, 1093, 3280, 9841, 29524,
			88573, 265720, 797161, 2391484
		};

		public int BlockSize => blockSize100k;

		public uint Crc32 { get; private set; }

		public int AvailableBytesOut { get; private set; }

		public int UncompressedBytes => last + 1;

		public BZip2Compressor(BitWriter writer)
			: this(writer, BZip2.MaxBlockSize)
		{
		}

		public BZip2Compressor(BitWriter writer, int blockSize)
		{
			blockSize100k = blockSize;
			bw = writer;
			outBlockFillThreshold = blockSize * BZip2.BlockSizeMultiple - 20;
			cstate = new CompressionState(blockSize);
			Reset();
		}

		private void Reset()
		{
			crc.Reset();
			currentByte = -1;
			runLength = 0;
			last = -1;
			int num = 256;
			while (--num >= 0)
			{
				cstate.inUse[num] = false;
			}
		}

		public int Fill(byte[] buffer, int offset, int count)
		{
			if (last >= outBlockFillThreshold)
			{
				return 0;
			}
			int num = 0;
			int num2 = offset + count;
			int num3;
			do
			{
				num3 = write0(buffer[offset++]);
				if (num3 > 0)
				{
					num++;
				}
			}
			while (offset < num2 && num3 == 1);
			return num;
		}

		private int write0(byte b)
		{
			if (currentByte == -1)
			{
				currentByte = b;
				runLength++;
				return 1;
			}
			if (currentByte == b)
			{
				if (++runLength > 254)
				{
					bool flag = AddRunToOutputBlock(final: false);
					currentByte = -1;
					runLength = 0;
					if (!flag)
					{
						return 1;
					}
					return 2;
				}
				return 1;
			}
			if (AddRunToOutputBlock(final: false))
			{
				currentByte = -1;
				runLength = 0;
				return 0;
			}
			runLength = 1;
			currentByte = b;
			return 1;
		}

		private bool AddRunToOutputBlock(bool final)
		{
			runs++;
			int num = last;
			if (num >= outBlockFillThreshold && !final)
			{
				string message = string.Format("block overrun(final={2}): {0} >= threshold ({1})", num, outBlockFillThreshold, final);
				throw new Exception(message);
			}
			byte b = (byte)currentByte;
			byte[] block = cstate.block;
			cstate.inUse[b] = true;
			int num2 = runLength;
			crc.UpdateCRC(b, num2);
			switch (num2)
			{
			case 1:
				block[num + 2] = b;
				last = num + 1;
				break;
			case 2:
				block[num + 2] = b;
				block[num + 3] = b;
				last = num + 2;
				break;
			case 3:
				block[num + 2] = b;
				block[num + 3] = b;
				block[num + 4] = b;
				last = num + 3;
				break;
			default:
				num2 -= 4;
				cstate.inUse[num2] = true;
				block[num + 2] = b;
				block[num + 3] = b;
				block[num + 4] = b;
				block[num + 5] = b;
				block[num + 6] = (byte)num2;
				last = num + 5;
				break;
			}
			return last >= outBlockFillThreshold;
		}

		public void CompressAndWrite()
		{
			if (runLength > 0)
			{
				AddRunToOutputBlock(final: true);
			}
			currentByte = -1;
			if (last != -1)
			{
				blockSort();
				bw.WriteByte(49);
				bw.WriteByte(65);
				bw.WriteByte(89);
				bw.WriteByte(38);
				bw.WriteByte(83);
				bw.WriteByte(89);
				Crc32 = (uint)crc.Crc32Result;
				bw.WriteInt(Crc32);
				bw.WriteBits(1, blockRandomised ? 1u : 0u);
				moveToFrontCodeAndSend();
				Reset();
			}
		}

		private void randomiseBlock()
		{
			bool[] inUse = cstate.inUse;
			byte[] block = cstate.block;
			int num = last;
			int num2 = 256;
			while (--num2 >= 0)
			{
				inUse[num2] = false;
			}
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 1;
			while (num5 <= num)
			{
				if (num3 == 0)
				{
					num3 = (ushort)Rand.Rnums(num4);
					if (++num4 == 512)
					{
						num4 = 0;
					}
				}
				num3--;
				block[num6] ^= ((num3 == 1) ? ((byte)1) : ((byte)0));
				inUse[block[num6] & 0xFF] = true;
				num5 = num6;
				num6++;
			}
			blockRandomised = true;
		}

		private void mainSort()
		{
			CompressionState compressionState = cstate;
			int[] mainSort_runningOrder = compressionState.mainSort_runningOrder;
			int[] mainSort_copy = compressionState.mainSort_copy;
			bool[] mainSort_bigDone = compressionState.mainSort_bigDone;
			int[] ftab = compressionState.ftab;
			byte[] block = compressionState.block;
			int[] fmap = compressionState.fmap;
			char[] quadrant = compressionState.quadrant;
			int num = last;
			int num2 = workLimit;
			bool flag = firstAttempt;
			int num3 = 65537;
			while (--num3 >= 0)
			{
				ftab[num3] = 0;
			}
			for (int i = 0; i < BZip2.NUM_OVERSHOOT_BYTES; i++)
			{
				block[num + i + 2] = block[i % (num + 1) + 1];
			}
			int num4 = num + BZip2.NUM_OVERSHOOT_BYTES + 1;
			while (--num4 >= 0)
			{
				quadrant[num4] = '\0';
			}
			block[0] = block[num + 1];
			int num5 = block[0] & 0xFF;
			for (int j = 0; j <= num; j++)
			{
				int num6 = block[j + 1] & 0xFF;
				ftab[(num5 << 8) + num6]++;
				num5 = num6;
			}
			for (int k = 1; k <= 65536; k++)
			{
				ftab[k] += ftab[k - 1];
			}
			num5 = block[1] & 0xFF;
			for (int l = 0; l < num; l++)
			{
				int num7 = block[l + 2] & 0xFF;
				fmap[--ftab[(num5 << 8) + num7]] = l;
				num5 = num7;
			}
			fmap[--ftab[((block[num + 1] & 0xFF) << 8) + (block[1] & 0xFF)]] = num;
			int num8 = 256;
			while (--num8 >= 0)
			{
				mainSort_bigDone[num8] = false;
				mainSort_runningOrder[num8] = num8;
			}
			int num9 = 364;
			while (num9 != 1)
			{
				num9 /= 3;
				for (int m = num9; m <= 255; m++)
				{
					int num10 = mainSort_runningOrder[m];
					int num11 = ftab[num10 + 1 << 8] - ftab[num10 << 8];
					int num12 = num9 - 1;
					int num13 = m;
					int num14 = mainSort_runningOrder[num13 - num9];
					while (ftab[num14 + 1 << 8] - ftab[num14 << 8] > num11)
					{
						mainSort_runningOrder[num13] = num14;
						num13 -= num9;
						if (num13 <= num12)
						{
							break;
						}
						num14 = mainSort_runningOrder[num13 - num9];
					}
					mainSort_runningOrder[num13] = num10;
				}
			}
			for (int n = 0; n <= 255; n++)
			{
				int num15 = mainSort_runningOrder[n];
				for (int num16 = 0; num16 <= 255; num16++)
				{
					int num17 = (num15 << 8) + num16;
					int num18 = ftab[num17];
					if ((num18 & SETMASK) == SETMASK)
					{
						continue;
					}
					int num19 = num18 & CLEARMASK;
					int num20 = (ftab[num17 + 1] & CLEARMASK) - 1;
					if (num20 > num19)
					{
						mainQSort3(compressionState, num19, num20, 2);
						if (flag && workDone > num2)
						{
							return;
						}
					}
					ftab[num17] = num18 | SETMASK;
				}
				for (int num21 = 0; num21 <= 255; num21++)
				{
					mainSort_copy[num21] = ftab[(num21 << 8) + num15] & CLEARMASK;
				}
				int num22 = ftab[num15 << 8] & CLEARMASK;
				for (int num23 = ftab[num15 + 1 << 8] & CLEARMASK; num22 < num23; num22++)
				{
					int num24 = fmap[num22];
					num5 = block[num24] & 0xFF;
					if (!mainSort_bigDone[num5])
					{
						fmap[mainSort_copy[num5]] = ((num24 == 0) ? num : (num24 - 1));
						mainSort_copy[num5]++;
					}
				}
				int num25 = 256;
				while (--num25 >= 0)
				{
					ftab[(num25 << 8) + num15] |= SETMASK;
				}
				mainSort_bigDone[num15] = true;
				if (n >= 255)
				{
					continue;
				}
				int num26 = ftab[num15 << 8] & CLEARMASK;
				int num27 = (ftab[num15 + 1 << 8] & CLEARMASK) - num26;
				int num28;
				for (num28 = 0; num27 >> num28 > 65534; num28++)
				{
				}
				for (int num29 = 0; num29 < num27; num29++)
				{
					int num30 = fmap[num26 + num29];
					char c = (quadrant[num30] = (char)(num29 >> num28));
					if (num30 < BZip2.NUM_OVERSHOOT_BYTES)
					{
						quadrant[num30 + num + 1] = c;
					}
				}
			}
		}

		private void blockSort()
		{
			workLimit = WORK_FACTOR * last;
			workDone = 0;
			blockRandomised = false;
			firstAttempt = true;
			mainSort();
			if (firstAttempt && workDone > workLimit)
			{
				randomiseBlock();
				workLimit = (workDone = 0);
				firstAttempt = false;
				mainSort();
			}
			int[] fmap = cstate.fmap;
			origPtr = -1;
			int i = 0;
			for (int num = last; i <= num; i++)
			{
				if (fmap[i] == 0)
				{
					origPtr = i;
					break;
				}
			}
		}

		private bool mainSimpleSort(CompressionState dataShadow, int lo, int hi, int d)
		{
			int num = hi - lo + 1;
			if (num < 2)
			{
				if (firstAttempt)
				{
					return workDone > workLimit;
				}
				return false;
			}
			int i;
			for (i = 0; increments[i] < num; i++)
			{
			}
			int[] fmap = dataShadow.fmap;
			char[] quadrant = dataShadow.quadrant;
			byte[] block = dataShadow.block;
			int num2 = last;
			int num3 = num2 + 1;
			bool flag = firstAttempt;
			int num4 = workLimit;
			int num5 = workDone;
			while (--i >= 0)
			{
				int num6 = increments[i];
				int num7 = lo + num6 - 1;
				int j = lo + num6;
				while (j <= hi)
				{
					int num8 = 3;
					for (; j <= hi; j++)
					{
						if (--num8 < 0)
						{
							break;
						}
						int num9 = fmap[j];
						int num10 = num9 + d;
						int num11 = j;
						bool flag2 = false;
						int num12 = 0;
						int num13;
						int num14;
						do
						{
							IL_00b1:
							if (flag2)
							{
								fmap[num11] = num12;
								if ((num11 -= num6) <= num7)
								{
									break;
								}
							}
							else
							{
								flag2 = true;
							}
							num12 = fmap[num11 - num6];
							num13 = num12 + d;
							num14 = num10;
							if (block[num13 + 1] == block[num14 + 1])
							{
								if (block[num13 + 2] == block[num14 + 2])
								{
									if (block[num13 + 3] == block[num14 + 3])
									{
										if (block[num13 + 4] == block[num14 + 4])
										{
											if (block[num13 + 5] == block[num14 + 5])
											{
												if (block[num13 += 6] == block[num14 += 6])
												{
													int num15 = num2;
													while (num15 > 0)
													{
														num15 -= 4;
														if (block[num13 + 1] == block[num14 + 1])
														{
															if (quadrant[num13] == quadrant[num14])
															{
																if (block[num13 + 2] == block[num14 + 2])
																{
																	if (quadrant[num13 + 1] == quadrant[num14 + 1])
																	{
																		if (block[num13 + 3] == block[num14 + 3])
																		{
																			if (quadrant[num13 + 2] == quadrant[num14 + 2])
																			{
																				if (block[num13 + 4] == block[num14 + 4])
																				{
																					if (quadrant[num13 + 3] == quadrant[num14 + 3])
																					{
																						if ((num13 += 4) >= num3)
																						{
																							num13 -= num3;
																						}
																						if ((num14 += 4) >= num3)
																						{
																							num14 -= num3;
																						}
																						num5++;
																						continue;
																					}
																					goto IL_021f;
																				}
																				goto IL_0235;
																			}
																			goto IL_0259;
																		}
																		goto IL_026f;
																	}
																	goto IL_0293;
																}
																goto IL_02a9;
															}
															goto IL_02cd;
														}
														goto IL_02df;
													}
													break;
												}
												if ((block[num13] & 0xFF) <= (block[num14] & 0xFF))
												{
													break;
												}
											}
											else if ((block[num13 + 5] & 0xFF) <= (block[num14 + 5] & 0xFF))
											{
												break;
											}
										}
										else if ((block[num13 + 4] & 0xFF) <= (block[num14 + 4] & 0xFF))
										{
											break;
										}
									}
									else if ((block[num13 + 3] & 0xFF) <= (block[num14 + 3] & 0xFF))
									{
										break;
									}
								}
								else if ((block[num13 + 2] & 0xFF) <= (block[num14 + 2] & 0xFF))
								{
									break;
								}
							}
							else if ((block[num13 + 1] & 0xFF) <= (block[num14 + 1] & 0xFF))
							{
								break;
							}
							goto IL_00b1;
							IL_0259:
							if (quadrant[num13 + 2] <= quadrant[num14 + 2])
							{
								break;
							}
							goto IL_00b1;
							IL_021f:
							if (quadrant[num13 + 3] <= quadrant[num14 + 3])
							{
								break;
							}
							goto IL_00b1;
							IL_02cd:
							if (quadrant[num13] <= quadrant[num14])
							{
								break;
							}
							goto IL_00b1;
							IL_0235:
							if ((block[num13 + 4] & 0xFF) <= (block[num14 + 4] & 0xFF))
							{
								break;
							}
							goto IL_00b1;
							IL_026f:
							if ((block[num13 + 3] & 0xFF) <= (block[num14 + 3] & 0xFF))
							{
								break;
							}
							goto IL_00b1;
							IL_02a9:
							if ((block[num13 + 2] & 0xFF) <= (block[num14 + 2] & 0xFF))
							{
								break;
							}
							goto IL_00b1;
							IL_02df:
							if ((block[num13 + 1] & 0xFF) <= (block[num14 + 1] & 0xFF))
							{
								break;
							}
							goto IL_00b1;
							IL_0293:;
						}
						while (quadrant[num13 + 1] > quadrant[num14 + 1]);
						fmap[num11] = num9;
					}
					if (flag && j <= hi && num5 > num4)
					{
						goto end_IL_040b;
					}
				}
				continue;
				end_IL_040b:
				break;
			}
			workDone = num5;
			if (flag)
			{
				return num5 > num4;
			}
			return false;
		}

		private static void vswap(int[] fmap, int p1, int p2, int n)
		{
			n += p1;
			while (p1 < n)
			{
				int num = fmap[p1];
				fmap[p1++] = fmap[p2];
				fmap[p2++] = num;
			}
		}

		private static byte med3(byte a, byte b, byte c)
		{
			if (a >= b)
			{
				if (b <= c)
				{
					if (a <= c)
					{
						return a;
					}
					return c;
				}
				return b;
			}
			if (b >= c)
			{
				if (a >= c)
				{
					return a;
				}
				return c;
			}
			return b;
		}

		private void mainQSort3(CompressionState dataShadow, int loSt, int hiSt, int dSt)
		{
			int[] stack_ll = dataShadow.stack_ll;
			int[] stack_hh = dataShadow.stack_hh;
			int[] stack_dd = dataShadow.stack_dd;
			int[] fmap = dataShadow.fmap;
			byte[] block = dataShadow.block;
			stack_ll[0] = loSt;
			stack_hh[0] = hiSt;
			stack_dd[0] = dSt;
			int num = 1;
			while (--num >= 0)
			{
				int num2 = stack_ll[num];
				int num3 = stack_hh[num];
				int num4 = stack_dd[num];
				if (num3 - num2 < SMALL_THRESH || num4 > DEPTH_THRESH)
				{
					if (mainSimpleSort(dataShadow, num2, num3, num4))
					{
						break;
					}
					continue;
				}
				int num5 = num4 + 1;
				int num6 = med3(block[fmap[num2] + num5], block[fmap[num3] + num5], block[fmap[num2 + num3 >> 1] + num5]) & 0xFF;
				int num7 = num2;
				int num8 = num3;
				int num9 = num2;
				int num10 = num3;
				while (true)
				{
					if (num7 <= num8)
					{
						int num11 = (block[fmap[num7] + num5] & 0xFF) - num6;
						if (num11 == 0)
						{
							int num12 = fmap[num7];
							fmap[num7++] = fmap[num9];
							fmap[num9++] = num12;
							continue;
						}
						if (num11 < 0)
						{
							num7++;
							continue;
						}
					}
					while (num7 <= num8)
					{
						int num13 = (block[fmap[num8] + num5] & 0xFF) - num6;
						if (num13 == 0)
						{
							int num14 = fmap[num8];
							fmap[num8--] = fmap[num10];
							fmap[num10--] = num14;
							continue;
						}
						if (num13 <= 0)
						{
							break;
						}
						num8--;
					}
					if (num7 > num8)
					{
						break;
					}
					int num15 = fmap[num7];
					fmap[num7++] = fmap[num8];
					fmap[num8--] = num15;
				}
				if (num10 < num9)
				{
					stack_ll[num] = num2;
					stack_hh[num] = num3;
					stack_dd[num] = num5;
					num++;
					continue;
				}
				int num16 = ((num9 - num2 < num7 - num9) ? (num9 - num2) : (num7 - num9));
				vswap(fmap, num2, num7 - num16, num16);
				int num17 = ((num3 - num10 < num10 - num8) ? (num3 - num10) : (num10 - num8));
				vswap(fmap, num7, num3 - num17 + 1, num17);
				num16 = num2 + num7 - num9 - 1;
				num17 = num3 - (num10 - num8) + 1;
				stack_ll[num] = num2;
				stack_hh[num] = num16;
				stack_dd[num] = num4;
				num++;
				stack_ll[num] = num16 + 1;
				stack_hh[num] = num17 - 1;
				stack_dd[num] = num5;
				num++;
				stack_ll[num] = num17;
				stack_hh[num] = num3;
				stack_dd[num] = num4;
				num++;
			}
		}

		private void generateMTFValues()
		{
			int num = last;
			CompressionState compressionState = cstate;
			bool[] inUse = compressionState.inUse;
			byte[] block = compressionState.block;
			int[] fmap = compressionState.fmap;
			char[] sfmap = compressionState.sfmap;
			int[] mtfFreq = compressionState.mtfFreq;
			byte[] unseqToSeq = compressionState.unseqToSeq;
			byte[] generateMTFValues_yy = compressionState.generateMTFValues_yy;
			int num2 = 0;
			for (int i = 0; i < 256; i++)
			{
				if (inUse[i])
				{
					unseqToSeq[i] = (byte)num2;
					num2++;
				}
			}
			nInUse = num2;
			int num3 = num2 + 1;
			for (int num4 = num3; num4 >= 0; num4--)
			{
				mtfFreq[num4] = 0;
			}
			int num5 = num2;
			while (--num5 >= 0)
			{
				generateMTFValues_yy[num5] = (byte)num5;
			}
			int num6 = 0;
			int num7 = 0;
			for (int j = 0; j <= num; j++)
			{
				byte b = unseqToSeq[block[fmap[j]] & 0xFF];
				byte b2 = generateMTFValues_yy[0];
				int num8 = 0;
				while (b != b2)
				{
					num8++;
					byte b3 = b2;
					b2 = generateMTFValues_yy[num8];
					generateMTFValues_yy[num8] = b3;
				}
				generateMTFValues_yy[0] = b2;
				if (num8 == 0)
				{
					num7++;
					continue;
				}
				if (num7 > 0)
				{
					num7--;
					while (true)
					{
						if ((num7 & 1) == 0)
						{
							sfmap[num6] = BZip2.RUNA;
							num6++;
							mtfFreq[(uint)BZip2.RUNA]++;
						}
						else
						{
							sfmap[num6] = BZip2.RUNB;
							num6++;
							mtfFreq[(uint)BZip2.RUNB]++;
						}
						if (num7 < 2)
						{
							break;
						}
						num7 = num7 - 2 >> 1;
					}
					num7 = 0;
				}
				sfmap[num6] = (char)(num8 + 1);
				num6++;
				mtfFreq[num8 + 1]++;
			}
			if (num7 > 0)
			{
				num7--;
				while (true)
				{
					if ((num7 & 1) == 0)
					{
						sfmap[num6] = BZip2.RUNA;
						num6++;
						mtfFreq[(uint)BZip2.RUNA]++;
					}
					else
					{
						sfmap[num6] = BZip2.RUNB;
						num6++;
						mtfFreq[(uint)BZip2.RUNB]++;
					}
					if (num7 < 2)
					{
						break;
					}
					num7 = num7 - 2 >> 1;
				}
			}
			sfmap[num6] = (char)num3;
			mtfFreq[num3]++;
			nMTF = num6 + 1;
		}

		private static void hbAssignCodes(int[] code, byte[] length, int minLen, int maxLen, int alphaSize)
		{
			int num = 0;
			for (int i = minLen; i <= maxLen; i++)
			{
				for (int j = 0; j < alphaSize; j++)
				{
					if ((length[j] & 0xFF) == i)
					{
						code[j] = num;
						num++;
					}
				}
				num <<= 1;
			}
		}

		private void sendMTFValues()
		{
			byte[][] sendMTFValues_len = cstate.sendMTFValues_len;
			int num = nInUse + 2;
			int num2 = BZip2.NGroups;
			while (--num2 >= 0)
			{
				byte[] array = sendMTFValues_len[num2];
				int num3 = num;
				while (--num3 >= 0)
				{
					array[num3] = GREATER_ICOST;
				}
			}
			int nGroups = ((nMTF < 200) ? 2 : ((nMTF < 600) ? 3 : ((nMTF < 1200) ? 4 : ((nMTF < 2400) ? 5 : 6))));
			sendMTFValues0(nGroups, num);
			int nSelectors = sendMTFValues1(nGroups, num);
			sendMTFValues2(nGroups, nSelectors);
			sendMTFValues3(nGroups, num);
			sendMTFValues4();
			sendMTFValues5(nGroups, nSelectors);
			sendMTFValues6(nGroups, num);
			sendMTFValues7(nSelectors);
		}

		private void sendMTFValues0(int nGroups, int alphaSize)
		{
			byte[][] sendMTFValues_len = cstate.sendMTFValues_len;
			int[] mtfFreq = cstate.mtfFreq;
			int num = nMTF;
			int num2 = 0;
			for (int num3 = nGroups; num3 > 0; num3--)
			{
				int num4 = num / num3;
				int num5 = num2 - 1;
				int i = 0;
				int num6 = alphaSize - 1;
				for (; i < num4; i += mtfFreq[++num5])
				{
					if (num5 >= num6)
					{
						break;
					}
				}
				if (num5 > num2 && num3 != nGroups && num3 != 1 && ((nGroups - num3) & 1) != 0)
				{
					i -= mtfFreq[num5--];
				}
				byte[] array = sendMTFValues_len[num3 - 1];
				int num7 = alphaSize;
				while (--num7 >= 0)
				{
					if (num7 >= num2 && num7 <= num5)
					{
						array[num7] = LESSER_ICOST;
					}
					else
					{
						array[num7] = GREATER_ICOST;
					}
				}
				num2 = num5 + 1;
				num -= i;
			}
		}

		private static void hbMakeCodeLengths(byte[] len, int[] freq, CompressionState state1, int alphaSize, int maxLen)
		{
			int[] heap = state1.heap;
			int[] weight = state1.weight;
			int[] parent = state1.parent;
			int num = alphaSize;
			while (--num >= 0)
			{
				weight[num + 1] = ((freq[num] == 0) ? 1 : freq[num]) << 8;
			}
			bool flag = true;
			while (flag)
			{
				flag = false;
				int num2 = alphaSize;
				int num3 = 0;
				heap[0] = 0;
				weight[0] = 0;
				parent[0] = -2;
				for (int i = 1; i <= alphaSize; i++)
				{
					parent[i] = -1;
					num3++;
					heap[num3] = i;
					int num4 = num3;
					int num5 = heap[num4];
					while (weight[num5] < weight[heap[num4 >> 1]])
					{
						heap[num4] = heap[num4 >> 1];
						num4 >>= 1;
					}
					heap[num4] = num5;
				}
				while (num3 > 1)
				{
					int num6 = heap[1];
					heap[1] = heap[num3];
					num3--;
					int num7 = 0;
					int num8 = 1;
					int num9 = heap[1];
					while (true)
					{
						num7 = num8 << 1;
						if (num7 > num3)
						{
							break;
						}
						if (num7 < num3 && weight[heap[num7 + 1]] < weight[heap[num7]])
						{
							num7++;
						}
						if (weight[num9] < weight[heap[num7]])
						{
							break;
						}
						heap[num8] = heap[num7];
						num8 = num7;
					}
					heap[num8] = num9;
					int num10 = heap[1];
					heap[1] = heap[num3];
					num3--;
					num7 = 0;
					num8 = 1;
					num9 = heap[1];
					while (true)
					{
						num7 = num8 << 1;
						if (num7 > num3)
						{
							break;
						}
						if (num7 < num3 && weight[heap[num7 + 1]] < weight[heap[num7]])
						{
							num7++;
						}
						if (weight[num9] < weight[heap[num7]])
						{
							break;
						}
						heap[num8] = heap[num7];
						num8 = num7;
					}
					heap[num8] = num9;
					num2++;
					parent[num6] = (parent[num10] = num2);
					int num11 = weight[num6];
					int num12 = weight[num10];
					weight[num2] = ((num11 & -256) + (num12 & -256)) | (1 + (((num11 & 0xFF) > (num12 & 0xFF)) ? (num11 & 0xFF) : (num12 & 0xFF)));
					parent[num2] = -1;
					num3++;
					heap[num3] = num2;
					num9 = 0;
					num8 = num3;
					num9 = heap[num8];
					int num13 = weight[num9];
					while (num13 < weight[heap[num8 >> 1]])
					{
						heap[num8] = heap[num8 >> 1];
						num8 >>= 1;
					}
					heap[num8] = num9;
				}
				for (int j = 1; j <= alphaSize; j++)
				{
					int num14 = 0;
					int num15 = j;
					int num16;
					while ((num16 = parent[num15]) >= 0)
					{
						num15 = num16;
						num14++;
					}
					len[j - 1] = (byte)num14;
					if (num14 > maxLen)
					{
						flag = true;
					}
				}
				if (flag)
				{
					for (int k = 1; k < alphaSize; k++)
					{
						int num17 = weight[k] >> 8;
						num17 = 1 + (num17 >> 1);
						weight[k] = num17 << 8;
					}
				}
			}
		}

		private int sendMTFValues1(int nGroups, int alphaSize)
		{
			CompressionState compressionState = cstate;
			int[][] sendMTFValues_rfreq = compressionState.sendMTFValues_rfreq;
			int[] sendMTFValues_fave = compressionState.sendMTFValues_fave;
			short[] sendMTFValues_cost = compressionState.sendMTFValues_cost;
			char[] sfmap = compressionState.sfmap;
			byte[] selector = compressionState.selector;
			byte[][] sendMTFValues_len = compressionState.sendMTFValues_len;
			byte[] array = sendMTFValues_len[0];
			byte[] array2 = sendMTFValues_len[1];
			byte[] array3 = sendMTFValues_len[2];
			byte[] array4 = sendMTFValues_len[3];
			byte[] array5 = sendMTFValues_len[4];
			byte[] array6 = sendMTFValues_len[5];
			int num = nMTF;
			int num2 = 0;
			for (int i = 0; i < BZip2.N_ITERS; i++)
			{
				int num3 = nGroups;
				while (--num3 >= 0)
				{
					sendMTFValues_fave[num3] = 0;
					int[] array7 = sendMTFValues_rfreq[num3];
					int num4 = alphaSize;
					while (--num4 >= 0)
					{
						array7[num4] = 0;
					}
				}
				num2 = 0;
				int num5 = 0;
				while (num5 < nMTF)
				{
					int num6 = Math.Min(num5 + BZip2.G_SIZE - 1, num - 1);
					if (nGroups == BZip2.NGroups)
					{
						int[] array8 = new int[6];
						for (int j = num5; j <= num6; j++)
						{
							int num7 = sfmap[j];
							array8[0] += array[num7] & 0xFF;
							array8[1] += array2[num7] & 0xFF;
							array8[2] += array3[num7] & 0xFF;
							array8[3] += array4[num7] & 0xFF;
							array8[4] += array5[num7] & 0xFF;
							array8[5] += array6[num7] & 0xFF;
						}
						sendMTFValues_cost[0] = (short)array8[0];
						sendMTFValues_cost[1] = (short)array8[1];
						sendMTFValues_cost[2] = (short)array8[2];
						sendMTFValues_cost[3] = (short)array8[3];
						sendMTFValues_cost[4] = (short)array8[4];
						sendMTFValues_cost[5] = (short)array8[5];
					}
					else
					{
						int num8 = nGroups;
						while (--num8 >= 0)
						{
							sendMTFValues_cost[num8] = 0;
						}
						for (int k = num5; k <= num6; k++)
						{
							int num9 = sfmap[k];
							int num10 = nGroups;
							while (--num10 >= 0)
							{
								sendMTFValues_cost[num10] += (short)(sendMTFValues_len[num10][num9] & 0xFF);
							}
						}
					}
					int num11 = -1;
					int num12 = nGroups;
					int num13 = 999999999;
					while (--num12 >= 0)
					{
						int num14 = sendMTFValues_cost[num12];
						if (num14 < num13)
						{
							num13 = num14;
							num11 = num12;
						}
					}
					sendMTFValues_fave[num11]++;
					selector[num2] = (byte)num11;
					num2++;
					int[] array9 = sendMTFValues_rfreq[num11];
					for (int l = num5; l <= num6; l++)
					{
						array9[(uint)sfmap[l]]++;
					}
					num5 = num6 + 1;
				}
				for (int m = 0; m < nGroups; m++)
				{
					hbMakeCodeLengths(sendMTFValues_len[m], sendMTFValues_rfreq[m], cstate, alphaSize, 20);
				}
			}
			return num2;
		}

		private void sendMTFValues2(int nGroups, int nSelectors)
		{
			CompressionState compressionState = cstate;
			byte[] sendMTFValues2_pos = compressionState.sendMTFValues2_pos;
			int num = nGroups;
			while (--num >= 0)
			{
				sendMTFValues2_pos[num] = (byte)num;
			}
			for (int i = 0; i < nSelectors; i++)
			{
				byte b = compressionState.selector[i];
				byte b2 = sendMTFValues2_pos[0];
				int num2 = 0;
				while (b != b2)
				{
					num2++;
					byte b3 = b2;
					b2 = sendMTFValues2_pos[num2];
					sendMTFValues2_pos[num2] = b3;
				}
				sendMTFValues2_pos[0] = b2;
				compressionState.selectorMtf[i] = (byte)num2;
			}
		}

		private void sendMTFValues3(int nGroups, int alphaSize)
		{
			int[][] sendMTFValues_code = cstate.sendMTFValues_code;
			byte[][] sendMTFValues_len = cstate.sendMTFValues_len;
			for (int i = 0; i < nGroups; i++)
			{
				int num = 32;
				int num2 = 0;
				byte[] array = sendMTFValues_len[i];
				int num3 = alphaSize;
				while (--num3 >= 0)
				{
					int num4 = array[num3] & 0xFF;
					if (num4 > num2)
					{
						num2 = num4;
					}
					if (num4 < num)
					{
						num = num4;
					}
				}
				hbAssignCodes(sendMTFValues_code[i], sendMTFValues_len[i], num, num2, alphaSize);
			}
		}

		private void sendMTFValues4()
		{
			bool[] inUse = cstate.inUse;
			bool[] sentMTFValues4_inUse = cstate.sentMTFValues4_inUse16;
			int num = 16;
			while (--num >= 0)
			{
				sentMTFValues4_inUse[num] = false;
				int num2 = num * 16;
				int num3 = 16;
				while (--num3 >= 0)
				{
					if (inUse[num2 + num3])
					{
						sentMTFValues4_inUse[num] = true;
					}
				}
			}
			uint num4 = 0u;
			for (int i = 0; i < 16; i++)
			{
				if (sentMTFValues4_inUse[i])
				{
					num4 |= (uint)(1 << 16 - i - 1);
				}
			}
			bw.WriteBits(16, num4);
			for (int j = 0; j < 16; j++)
			{
				if (!sentMTFValues4_inUse[j])
				{
					continue;
				}
				int num5 = j * 16;
				num4 = 0u;
				for (int k = 0; k < 16; k++)
				{
					if (inUse[num5 + k])
					{
						num4 |= (uint)(1 << 16 - k - 1);
					}
				}
				bw.WriteBits(16, num4);
			}
		}

		private void sendMTFValues5(int nGroups, int nSelectors)
		{
			bw.WriteBits(3, (uint)nGroups);
			bw.WriteBits(15, (uint)nSelectors);
			byte[] selectorMtf = cstate.selectorMtf;
			for (int i = 0; i < nSelectors; i++)
			{
				int j = 0;
				for (int num = selectorMtf[i] & 0xFF; j < num; j++)
				{
					bw.WriteBits(1, 1u);
				}
				bw.WriteBits(1, 0u);
			}
		}

		private void sendMTFValues6(int nGroups, int alphaSize)
		{
			byte[][] sendMTFValues_len = cstate.sendMTFValues_len;
			for (int i = 0; i < nGroups; i++)
			{
				byte[] array = sendMTFValues_len[i];
				uint num = (uint)(array[0] & 0xFF);
				bw.WriteBits(5, num);
				for (int j = 0; j < alphaSize; j++)
				{
					int num2;
					for (num2 = array[j] & 0xFF; num < num2; num++)
					{
						bw.WriteBits(2, 2u);
					}
					while (num > num2)
					{
						bw.WriteBits(2, 3u);
						num--;
					}
					bw.WriteBits(1, 0u);
				}
			}
		}

		private void sendMTFValues7(int nSelectors)
		{
			byte[][] sendMTFValues_len = cstate.sendMTFValues_len;
			int[][] sendMTFValues_code = cstate.sendMTFValues_code;
			byte[] selector = cstate.selector;
			char[] sfmap = cstate.sfmap;
			int num = nMTF;
			int num2 = 0;
			int i = 0;
			while (i < num)
			{
				int num3 = Math.Min(i + BZip2.G_SIZE - 1, num - 1);
				int num4 = selector[num2] & 0xFF;
				int[] array = sendMTFValues_code[num4];
				byte[] array2 = sendMTFValues_len[num4];
				for (; i <= num3; i++)
				{
					int num5 = sfmap[i];
					int nbits = array2[num5] & 0xFF;
					bw.WriteBits(nbits, (uint)array[num5]);
				}
				i = num3 + 1;
				num2++;
			}
		}

		private void moveToFrontCodeAndSend()
		{
			bw.WriteBits(24, (uint)origPtr);
			generateMTFValues();
			sendMTFValues();
		}
	}
	public class BZip2InputStream : Stream
	{
		private enum CState
		{
			EOF,
			START_BLOCK,
			RAND_PART_A,
			RAND_PART_B,
			RAND_PART_C,
			NO_RAND_PART_A,
			NO_RAND_PART_B,
			NO_RAND_PART_C
		}

		private sealed class DecompressionState
		{
			public readonly bool[] inUse = new bool[256];

			public readonly byte[] seqToUnseq = new byte[256];

			public readonly byte[] selector = new byte[BZip2.MaxSelectors];

			public readonly byte[] selectorMtf = new byte[BZip2.MaxSelectors];

			public readonly int[] unzftab;

			public readonly int[][] gLimit;

			public readonly int[][] gBase;

			public readonly int[][] gPerm;

			public readonly int[] gMinlen;

			public readonly int[] cftab;

			public readonly byte[] getAndMoveToFrontDecode_yy;

			public readonly char[][] temp_charArray2d;

			public readonly byte[] recvDecodingTables_pos;

			public int[] tt;

			public byte[] ll8;

			public DecompressionState(int blockSize100k)
			{
				unzftab = new int[256];
				gLimit = BZip2.InitRectangularArray<int>(BZip2.NGroups, BZip2.MaxAlphaSize);
				gBase = BZip2.InitRectangularArray<int>(BZip2.NGroups, BZip2.MaxAlphaSize);
				gPerm = BZip2.InitRectangularArray<int>(BZip2.NGroups, BZip2.MaxAlphaSize);
				gMinlen = new int[BZip2.NGroups];
				cftab = new int[257];
				getAndMoveToFrontDecode_yy = new byte[256];
				temp_charArray2d = BZip2.InitRectangularArray<char>(BZip2.NGroups, BZip2.MaxAlphaSize);
				recvDecodingTables_pos = new byte[BZip2.NGroups];
				ll8 = new byte[blockSize100k * BZip2.BlockSizeMultiple];
			}

			public int[] initTT(int length)
			{
				int[] array = tt;
				if (array == null || array.Length < length)
				{
					array = (tt = new int[length]);
				}
				return array;
			}
		}

		private bool _disposed;

		private bool _leaveOpen;

		private long totalBytesRead;

		private int last;

		private int origPtr;

		private int blockSize100k;

		private bool blockRandomised;

		private int bsBuff;

		private int bsLive;

		private readonly CRC32 crc = new CRC32(reverseBits: true);

		private int nInUse;

		private Stream input;

		private int currentChar = -1;

		private CState currentState = CState.START_BLOCK;

		private uint storedBlockCRC;

		private uint storedCombinedCRC;

		private uint computedBlockCRC;

		private uint computedCombinedCRC;

		private int su_count;

		private int su_ch2;

		private int su_chPrev;

		private int su_i2;

		private int su_j2;

		private int su_rNToGo;

		private int su_rTPos;

		private int su_tPos;

		private char su_z;

		private DecompressionState data;

		public override bool CanRead
		{
			get
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("BZip2Stream");
				}
				return input.CanRead;
			}
		}

		public override bool CanSeek => false;

		public override bool CanWrite
		{
			get
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("BZip2Stream");
				}
				return input.CanWrite;
			}
		}

		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override long Position
		{
			get
			{
				return totalBytesRead;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public BZip2InputStream(Stream input)
			: this(input, leaveOpen: false)
		{
		}

		public BZip2InputStream(Stream input, bool leaveOpen)
		{
			this.input = input;
			_leaveOpen = leaveOpen;
			init();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (offset < 0)
			{
				throw new IndexOutOfRangeException($"offset ({offset}) must be > 0");
			}
			if (count < 0)
			{
				throw new IndexOutOfRangeException($"count ({count}) must be > 0");
			}
			if (offset + count > buffer.Length)
			{
				throw new IndexOutOfRangeException($"offset({offset}) count({count}) bLength({buffer.Length})");
			}
			if (input == null)
			{
				throw new IOException("the stream is not open");
			}
			int num = offset + count;
			int num2 = offset;
			int num3;
			while (num2 < num && (num3 = ReadByte()) >= 0)
			{
				buffer[num2++] = (byte)num3;
			}
			if (num2 != offset)
			{
				return num2 - offset;
			}
			return -1;
		}

		private void MakeMaps()
		{
			bool[] inUse = data.inUse;
			byte[] seqToUnseq = data.seqToUnseq;
			int num = 0;
			for (int i = 0; i < 256; i++)
			{
				if (inUse[i])
				{
					seqToUnseq[num++] = (byte)i;
				}
			}
			nInUse = num;
		}

		public override int ReadByte()
		{
			int result = currentChar;
			totalBytesRead++;
			switch (currentState)
			{
			case CState.EOF:
				return -1;
			case CState.START_BLOCK:
				throw new IOException("bad state");
			case CState.RAND_PART_A:
				throw new IOException("bad state");
			case CState.RAND_PART_B:
				SetupRandPartB();
				break;
			case CState.RAND_PART_C:
				SetupRandPartC();
				break;
			case CState.NO_RAND_PART_A:
				throw new IOException("bad state");
			case CState.NO_RAND_PART_B:
				SetupNoRandPartB();
				break;
			case CState.NO_RAND_PART_C:
				SetupNoRandPartC();
				break;
			default:
				throw new IOException("bad state");
			}
			return result;
		}

		public override void Flush()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("BZip2Stream");
			}
			input.Flush();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!_disposed)
				{
					if (disposing && input != null)
					{
						input.Close();
					}
					_disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		private void init()
		{
			if (input == null)
			{
				throw new IOException("No input Stream");
			}
			if (!input.CanRead)
			{
				throw new IOException("Unreadable input Stream");
			}
			CheckMagicChar('B', 0);
			CheckMagicChar('Z', 1);
			CheckMagicChar('h', 2);
			int num = input.ReadByte();
			if (num < 49 || num > 57)
			{
				throw new IOException("Stream is not BZip2 formatted: illegal blocksize " + (char)num);
			}
			blockSize100k = num - 48;
			InitBlock();
			SetupBlock();
		}

		private void CheckMagicChar(char expected, int position)
		{
			int num = input.ReadByte();
			if (num != expected)
			{
				string message = $"Not a valid BZip2 stream. byte {position}, expected '{(int)expected}', got '{num}'";
				throw new IOException(message);
			}
		}

		private void InitBlock()
		{
			char c = bsGetUByte();
			char c2 = bsGetUByte();
			char c3 = bsGetUByte();
			char c4 = bsGetUByte();
			char c5 = bsGetUByte();
			char c6 = bsGetUByte();
			if (c == '\u0017' && c2 == 'r' && c3 == 'E' && c4 == '8' && c5 == 'P' && c6 == '\u0090')
			{
				complete();
				return;
			}
			if (c != '1' || c2 != 'A' || c3 != 'Y' || c4 != '&' || c5 != 'S' || c6 != 'Y')
			{
				currentState = CState.EOF;
				string message = $"bad block header at offset 0x{input.Position:X}";
				throw new IOException(message);
			}
			storedBlockCRC = bsGetInt();
			blockRandomised = GetBits(1) == 1;
			if (data == null)
			{
				data = new DecompressionState(blockSize100k);
			}
			getAndMoveToFrontDecode();
			crc.Reset();
			currentState = CState.START_BLOCK;
		}

		private void EndBlock()
		{
			computedBlockCRC = (uint)crc.Crc32Result;
			if (storedBlockCRC != computedBlockCRC)
			{
				string message = $"BZip2 CRC error (expected {storedBlockCRC:X8}, computed {computedBlockCRC:X8})";
				throw new IOException(message);
			}
			computedCombinedCRC = (computedCombinedCRC << 1) | (computedCombinedCRC >> 31);
			computedCombinedCRC ^= computedBlockCRC;
		}

		private void complete()
		{
			storedCombinedCRC = bsGetInt();
			currentState = CState.EOF;
			data = null;
			if (storedCombinedCRC != computedCombinedCRC)
			{
				string message = $"BZip2 CRC error (expected {storedCombinedCRC:X8}, computed {computedCombinedCRC:X8})";
				throw new IOException(message);
			}
		}

		public override void Close()
		{
			Stream stream = input;
			if (stream == null)
			{
				return;
			}
			try
			{
				if (!_leaveOpen)
				{
					stream.Close();
				}
			}
			finally
			{
				data = null;
				input = null;
			}
		}

		private int GetBits(int n)
		{
			int num = bsLive;
			int num2 = bsBuff;
			if (num < n)
			{
				do
				{
					int num3 = input.ReadByte();
					if (num3 < 0)
					{
						throw new IOException("unexpected end of stream");
					}
					num2 = (num2 << 8) | num3;
					num += 8;
				}
				while (num < n);
				bsBuff = num2;
			}
			bsLive = num - n;
			return (num2 >> num - n) & ((1 << n) - 1);
		}

		private bool bsGetBit()
		{
			int bits = GetBits(1);
			return bits != 0;
		}

		private char bsGetUByte()
		{
			return (char)GetBits(8);
		}

		private uint bsGetInt()
		{
			return (uint)((((((GetBits(8) << 8) | GetBits(8)) << 8) | GetBits(8)) << 8) | GetBits(8));
		}

		private static void hbCreateDecodeTables(int[] limit, int[] bbase, int[] perm, char[] length, int minLen, int maxLen, int alphaSize)
		{
			int i = minLen;
			int num = 0;
			for (; i <= maxLen; i++)
			{
				for (int j = 0; j < alphaSize; j++)
				{
					if (length[j] == i)
					{
						perm[num++] = j;
					}
				}
			}
			int num2 = BZip2.MaxCodeLength;
			while (--num2 > 0)
			{
				bbase[num2] = 0;
				limit[num2] = 0;
			}
			for (int k = 0; k < alphaSize; k++)
			{
				bbase[length[k] + 1]++;
			}
			int l = 1;
			int num3 = bbase[0];
			for (; l < BZip2.MaxCodeLength; l++)
			{
				num3 = (bbase[l] = num3 + bbase[l]);
			}
			int m = minLen;
			int num4 = 0;
			int num5 = bbase[m];
			for (; m <= maxLen; m++)
			{
				int num6 = bbase[m + 1];
				num4 += num6 - num5;
				num5 = num6;
				limit[m] = num4 - 1;
				num4 <<= 1;
			}
			for (int n = minLen + 1; n <= maxLen; n++)
			{
				bbase[n] = (limit[n - 1] + 1 << 1) - bbase[n];
			}
		}

		private void recvDecodingTables()
		{
			DecompressionState decompressionState = data;
			bool[] inUse = decompressionState.inUse;
			byte[] recvDecodingTables_pos = decompressionState.recvDecodingTables_pos;
			int num = 0;
			for (int i = 0; i < 16; i++)
			{
				if (bsGetBit())
				{
					num |= 1 << i;
				}
			}
			int num2 = 256;
			while (--num2 >= 0)
			{
				inUse[num2] = false;
			}
			for (int j = 0; j < 16; j++)
			{
				if ((num & (1 << j)) == 0)
				{
					continue;
				}
				int num3 = j << 4;
				for (int k = 0; k < 16; k++)
				{
					if (bsGetBit())
					{
						inUse[num3 + k] = true;
					}
				}
			}
			MakeMaps();
			int num4 = nInUse + 2;
			int bits = GetBits(3);
			int bits2 = GetBits(15);
			for (int l = 0; l < bits2; l++)
			{
				int num5 = 0;
				while (bsGetBit())
				{
					num5++;
				}
				decompressionState.selectorMtf[l] = (byte)num5;
			}
			int num6 = bits;
			while (--num6 >= 0)
			{
				recvDecodingTables_pos[num6] = (byte)num6;
			}
			for (int m = 0; m < bits2; m++)
			{
				int num7 = decompressionState.selectorMtf[m];
				byte b = recvDecodingTables_pos[num7];
				while (num7 > 0)
				{
					recvDecodingTables_pos[num7] = recvDecodingTables_pos[num7 - 1];
					num7--;
				}
				recvDecodingTables_pos[0] = b;
				decompressionState.selector[m] = b;
			}
			char[][] temp_charArray2d = decompressionState.temp_charArray2d;
			for (int n = 0; n < bits; n++)
			{
				int num8 = GetBits(5);
				char[] array = temp_charArray2d[n];
				for (int num9 = 0; num9 < num4; num9++)
				{
					while (bsGetBit())
					{
						num8 += ((!bsGetBit()) ? 1 : (-1));
					}
					array[num9] = (char)num8;
				}
			}
			createHuffmanDecodingTables(num4, bits);
		}

		private void createHuffmanDecodingTables(int alphaSize, int nGroups)
		{
			DecompressionState decompressionState = data;
			char[][] temp_charArray2d = decompressionState.temp_charArray2d;
			for (int i = 0; i < nGroups; i++)
			{
				int num = 32;
				int num2 = 0;
				char[] array = temp_charArray2d[i];
				int num3 = alphaSize;
				while (--num3 >= 0)
				{
					char c = array[num3];
					if (c > num2)
					{
						num2 = c;
					}
					if (c < num)
					{
						num = c;
					}
				}
				hbCreateDecodeTables(decompressionState.gLimit[i], decompressionState.gBase[i], decompressionState.gPerm[i], temp_charArray2d[i], num, num2, alphaSize);
				decompressionState.gMinlen[i] = num;
			}
		}

		private void getAndMoveToFrontDecode()
		{
			DecompressionState decompressionState = data;
			origPtr = GetBits(24);
			if (origPtr < 0)
			{
				throw new IOException("BZ_DATA_ERROR");
			}
			if (origPtr > 10 + BZip2.BlockSizeMultiple * blockSize100k)
			{
				throw new IOException("BZ_DATA_ERROR");
			}
			recvDecodingTables();
			byte[] getAndMoveToFrontDecode_yy = decompressionState.getAndMoveToFrontDecode_yy;
			int num = blockSize100k * BZip2.BlockSizeMultiple;
			int num2 = 256;
			while (--num2 >= 0)
			{
				getAndMoveToFrontDecode_yy[num2] = (byte)num2;
				decompressionState.unzftab[num2] = 0;
			}
			int num3 = 0;
			int num4 = BZip2.G_SIZE - 1;
			int num5 = nInUse + 1;
			int num6 = getAndMoveToFrontDecode0(0);
			int num7 = bsBuff;
			int i = bsLive;
			int num8 = -1;
			int num9 = decompressionState.selector[num3] & 0xFF;
			int[] array = decompressionState.gBase[num9];
			int[] array2 = decompressionState.gLimit[num9];
			int[] array3 = decompressionState.gPerm[num9];
			int num10 = decompressionState.gMinlen[num9];
			while (num6 != num5)
			{
				if (num6 == BZip2.RUNA || num6 == BZip2.RUNB)
				{
					int num11 = -1;
					int num12 = 1;
					while (true)
					{
						if (num6 == BZip2.RUNA)
						{
							num11 += num12;
						}
						else
						{
							if (num6 != BZip2.RUNB)
							{
								break;
							}
							num11 += num12 << 1;
						}
						if (num4 == 0)
						{
							num4 = BZip2.G_SIZE - 1;
							num9 = decompressionState.selector[++num3] & 0xFF;
							array = decompressionState.gBase[num9];
							array2 = decompressionState.gLimit[num9];
							array3 = decompressionState.gPerm[num9];
							num10 = decompressionState.gMinlen[num9];
						}
						else
						{
							num4--;
						}
						int num13;
						for (num13 = num10; i < num13; i += 8)
						{
							int num14 = input.ReadByte();
							if (num14 >= 0)
							{
								num7 = (num7 << 8) | num14;
								continue;
							}
							throw new IOException("unexpected end of stream");
						}
						int num15 = (num7 >> i - num13) & ((1 << num13) - 1);
						i -= num13;
						while (num15 > array2[num13])
						{
							num13++;
							for (; i < 1; i += 8)
							{
								int num16 = input.ReadByte();
								if (num16 >= 0)
								{
									num7 = (num7 << 8) | num16;
									continue;
								}
								throw new IOException("unexpected end of stream");
							}
							i--;
							num15 = (num15 << 1) | ((num7 >> i) & 1);
						}
						num6 = array3[num15 - array[num13]];
						num12 <<= 1;
					}
					byte b = decompressionState.seqToUnseq[getAndMoveToFrontDecode_yy[0]];
					decompressionState.unzftab[b & 0xFF] += num11 + 1;
					while (num11-- >= 0)
					{
						decompressionState.ll8[++num8] = b;
					}
					if (num8 >= num)
					{
						throw new IOException("block overrun");
					}
					continue;
				}
				if (++num8 >= num)
				{
					throw new IOException("block overrun");
				}
				byte b2 = getAndMoveToFrontDecode_yy[num6 - 1];
				decompressionState.unzftab[decompressionState.seqToUnseq[b2] & 0xFF]++;
				decompressionState.ll8[num8] = decompressionState.seqToUnseq[b2];
				if (num6 <= 16)
				{
					int num17 = num6 - 1;
					while (num17 > 0)
					{
						getAndMoveToFrontDecode_yy[num17] = getAndMoveToFrontDecode_yy[--num17];
					}
				}
				else
				{
					Buffer.BlockCopy(getAndMoveToFrontDecode_yy, 0, getAndMoveToFrontDecode_yy, 1, num6 - 1);
				}
				getAndMoveToFrontDecode_yy[0] = b2;
				if (num4 == 0)
				{
					num4 = BZip2.G_SIZE - 1;
					num9 = decompressionState.selector[++num3] & 0xFF;
					array = decompressionState.gBase[num9];
					array2 = decompressionState.gLimit[num9];
					array3 = decompressionState.gPerm[num9];
					num10 = decompressionState.gMinlen[num9];
				}
				else
				{
					num4--;
				}
				int num18;
				for (num18 = num10; i < num18; i += 8)
				{
					int num19 = input.ReadByte();
					if (num19 >= 0)
					{
						num7 = (num7 << 8) | num19;
						continue;
					}
					throw new IOException("unexpected end of stream");
				}
				int num20 = (num7 >> i - num18) & ((1 << num18) - 1);
				i -= num18;
				while (num20 > array2[num18])
				{
					num18++;
					for (; i < 1; i += 8)
					{
						int num21 = input.ReadByte();
						if (num21 >= 0)
						{
							num7 = (num7 << 8) | num21;
							continue;
						}
						throw new IOException("unexpected end of stream");
					}
					i--;
					num20 = (num20 << 1) | ((num7 >> i) & 1);
				}
				num6 = array3[num20 - array[num18]];
			}
			last = num8;
			bsLive = i;
			bsBuff = num7;
		}

		private int getAndMoveToFrontDecode0(int groupNo)
		{
			DecompressionState decompressionState = data;
			int num = decompressionState.selector[groupNo] & 0xFF;
			int[] array = decompressionState.gLimit[num];
			int num2 = decompressionState.gMinlen[num];
			int num3 = GetBits(num2);
			int i = bsLive;
			int num4 = bsBuff;
			while (num3 > array[num2])
			{
				num2++;
				for (; i < 1; i += 8)
				{
					int num5 = input.ReadByte();
					if (num5 >= 0)
					{
						num4 = (num4 << 8) | num5;
						continue;
					}
					throw new IOException("unexpected end of stream");
				}
				i--;
				num3 = (num3 << 1) | ((num4 >> i) & 1);
			}
			bsLive = i;
			bsBuff = num4;
			return decompressionState.gPerm[num][num3 - decompressionState.gBase[num][num2]];
		}

		private void SetupBlock()
		{
			if (data == null)
			{
				return;
			}
			DecompressionState decompressionState = data;
			int[] array = decompressionState.initTT(last + 1);
			int i;
			for (i = 0; i <= 255; i++)
			{
				if (decompressionState.unzftab[i] < 0 || decompressionState.unzftab[i] > last)
				{
					throw new Exception("BZ_DATA_ERROR");
				}
			}
			decompressionState.cftab[0] = 0;
			for (i = 1; i <= 256; i++)
			{
				decompressionState.cftab[i] = decompressionState.unzftab[i - 1];
			}
			for (i = 1; i <= 256; i++)
			{
				decompressionState.cftab[i] += decompressionState.cftab[i - 1];
			}
			for (i = 0; i <= 256; i++)
			{
				if (decompressionState.cftab[i] < 0 || decompressionState.cftab[i] > last + 1)
				{
					string message = $"BZ_DATA_ERROR: cftab[{i}]={decompressionState.cftab[i]} last={last}";
					throw new Exception(message);
				}
			}
			for (i = 1; i <= 256; i++)
			{
				if (decompressionState.cftab[i - 1] > decompressionState.cftab[i])
				{
					throw new Exception("BZ_DATA_ERROR");
				}
			}
			i = 0;
			for (int num = last; i <= num; i++)
			{
				array[decompressionState.cftab[decompressionState.ll8[i] & 0xFF]++] = i;
			}
			if (origPtr < 0 || origPtr >= array.Length)
			{
				throw new IOException("stream corrupted");
			}
			su_tPos = array[origPtr];
			su_count = 0;
			su_i2 = 0;
			su_ch2 = 256;
			if (blockRandomised)
			{
				su_rNToGo = 0;
				su_rTPos = 0;
				SetupRandPartA();
			}
			else
			{
				SetupNoRandPartA();
			}
		}

		private void SetupRandPartA()
		{
			if (su_i2 <= last)
			{
				su_chPrev = su_ch2;
				int num = data.ll8[su_tPos] & 0xFF;
				su_tPos = data.tt[su_tPos];
				if (su_rNToGo == 0)
				{
					su_rNToGo = Rand.Rnums(su_rTPos) - 1;
					if (++su_rTPos == 512)
					{
						su_rTPos = 0;
					}
				}
				else
				{
					su_rNToGo--;
				}
				num = (su_ch2 = num ^ ((su_rNToGo == 1) ? 1 : 0));
				su_i2++;
				currentChar = num;
				currentState = CState.RAND_PART_B;
				crc.UpdateCRC((byte)num);
			}
			else
			{
				EndBlock();
				InitBlock();
				SetupBlock();
			}
		}

		private void SetupNoRandPartA()
		{
			if (su_i2 <= last)
			{
				su_chPrev = su_ch2;
				int num = (su_ch2 = data.ll8[su_tPos] & 0xFF);
				su_tPos = data.tt[su_tPos];
				su_i2++;
				currentChar = num;
				currentState = CState.NO_RAND_PART_B;
				crc.UpdateCRC((byte)num);
			}
			else
			{
				currentState = CState.NO_RAND_PART_A;
				EndBlock();
				InitBlock();
				SetupBlock();
			}
		}

		private void SetupRandPartB()
		{
			if (su_ch2 != su_chPrev)
			{
				currentState = CState.RAND_PART_A;
				su_count = 1;
				SetupRandPartA();
			}
			else if (++su_count >= 4)
			{
				su_z = (char)(data.ll8[su_tPos] & 0xFF);
				su_tPos = data.tt[su_tPos];
				if (su_rNToGo == 0)
				{
					su_rNToGo = Rand.Rnums(su_rTPos) - 1;
					if (++su_rTPos == 512)
					{
						su_rTPos = 0;
					}
				}
				else
				{
					su_rNToGo--;
				}
				su_j2 = 0;
				currentState = CState.RAND_PART_C;
				if (su_rNToGo == 1)
				{
					su_z ^= '\u0001';
				}
				SetupRandPartC();
			}
			else
			{
				currentState = CState.RAND_PART_A;
				SetupRandPartA();
			}
		}

		private void SetupRandPartC()
		{
			if (su_j2 < su_z)
			{
				currentChar = su_ch2;
				crc.UpdateCRC((byte)su_ch2);
				su_j2++;
			}
			else
			{
				currentState = CState.RAND_PART_A;
				su_i2++;
				su_count = 0;
				SetupRandPartA();
			}
		}

		private void SetupNoRandPartB()
		{
			if (su_ch2 != su_chPrev)
			{
				su_count = 1;
				SetupNoRandPartA();
			}
			else if (++su_count >= 4)
			{
				su_z = (char)(data.ll8[su_tPos] & 0xFF);
				su_tPos = data.tt[su_tPos];
				su_j2 = 0;
				SetupNoRandPartC();
			}
			else
			{
				SetupNoRandPartA();
			}
		}

		private void SetupNoRandPartC()
		{
			if (su_j2 < su_z)
			{
				int num = (currentChar = su_ch2);
				crc.UpdateCRC((byte)num);
				su_j2++;
				currentState = CState.NO_RAND_PART_C;
			}
			else
			{
				su_i2++;
				su_count = 0;
				SetupNoRandPartA();
			}
		}
	}
	internal static class BZip2
	{
		public static readonly int BlockSizeMultiple = 100000;

		public static readonly int MinBlockSize = 1;

		public static readonly int MaxBlockSize = 9;

		public static readonly int MaxAlphaSize = 258;

		public static readonly int MaxCodeLength = 23;

		public static readonly char RUNA = '\0';

		public static readonly char RUNB = '\u0001';

		public static readonly int NGroups = 6;

		public static readonly int G_SIZE = 50;

		public static readonly int N_ITERS = 4;

		public static readonly int MaxSelectors = 2 + 900000 / G_SIZE;

		public static readonly int NUM_OVERSHOOT_BYTES = 20;

		internal static readonly int QSORT_STACK_SIZE = 1000;

		internal static T[][] InitRectangularArray<T>(int d1, int d2)
		{
			T[][] array = new T[d1][];
			for (int i = 0; i < d1; i++)
			{
				array[i] = new T[d2];
			}
			return array;
		}
	}
	public class BZip2OutputStream : Stream
	{
		[Flags]
		private enum TraceBits : uint
		{
			None = 0u,
			Crc = 1u,
			Write = 2u,
			All = uint.MaxValue
		}

		private int totalBytesWrittenIn;

		private bool leaveOpen;

		private BZip2Compressor compressor;

		private uint combinedCRC;

		private Stream output;

		private BitWriter bw;

		private int blockSize100k;

		private TraceBits desiredTrace = TraceBits.Crc | TraceBits.Write;

		public int BlockSize => blockSize100k;

		public override bool CanRead => false;

		public override bool CanSeek => false;

		public override bool CanWrite
		{
			get
			{
				if (output == null)
				{
					throw new ObjectDisposedException("BZip2Stream");
				}
				return output.CanWrite;
			}
		}

		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override long Position
		{
			get
			{
				return totalBytesWrittenIn;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public BZip2OutputStream(Stream output)
			: this(output, BZip2.MaxBlockSize, leaveOpen: false)
		{
		}

		public BZip2OutputStream(Stream output, int blockSize)
			: this(output, blockSize, leaveOpen: false)
		{
		}

		public BZip2OutputStream(Stream output, bool leaveOpen)
			: this(output, BZip2.MaxBlockSize, leaveOpen)
		{
		}

		public BZip2OutputStream(Stream output, int blockSize, bool leaveOpen)
		{
			if (blockSize < BZip2.MinBlockSize || blockSize > BZip2.MaxBlockSize)
			{
				string message = $"blockSize={blockSize} is out of range; must be between {BZip2.MinBlockSize} and {BZip2.MaxBlockSize}";
				throw new ArgumentException(message, "blockSize");
			}
			this.output = output;
			if (!this.output.CanWrite)
			{
				throw new ArgumentException("The stream is not writable.", "output");
			}
			bw = new BitWriter(this.output);
			blockSize100k = blockSize;
			compressor = new BZip2Compressor(bw, blockSize);
			this.leaveOpen = leaveOpen;
			combinedCRC = 0u;
			EmitHeader();
		}

		public override void Close()
		{
			if (output != null)
			{
				Stream stream = output;
				Finish();
				if (!leaveOpen)
				{
					stream.Close();
				}
			}
		}

		public override void Flush()
		{
			if (output != null)
			{
				bw.Flush();
				output.Flush();
			}
		}

		private void EmitHeader()
		{
			byte[] array = new byte[4] { 66, 90, 104, 0 };
			array[3] = (byte)(48 + blockSize100k);
			byte[] array2 = array;
			output.Write(array2, 0, array2.Length);
		}

		private void EmitTrailer()
		{
			bw.WriteByte(23);
			bw.WriteByte(114);
			bw.WriteByte(69);
			bw.WriteByte(56);
			bw.WriteByte(80);
			bw.WriteByte(144);
			bw.WriteInt(combinedCRC);
			bw.FinishAndPad();
		}

		private void Finish()
		{
			try
			{
				_ = bw.TotalBytesWrittenOut;
				compressor.CompressAndWrite();
				combinedCRC = (combinedCRC << 1) | (combinedCRC >> 31);
				combinedCRC ^= compressor.Crc32;
				EmitTrailer();
			}
			finally
			{
				output = null;
				compressor = null;
				bw = null;
			}
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (offset < 0)
			{
				throw new IndexOutOfRangeException($"offset ({offset}) must be > 0");
			}
			if (count < 0)
			{
				throw new IndexOutOfRangeException($"count ({count}) must be > 0");
			}
			if (offset + count > buffer.Length)
			{
				throw new IndexOutOfRangeException($"offset({offset}) count({count}) bLength({buffer.Length})");
			}
			if (output == null)
			{
				throw new IOException("the stream is not open");
			}
			if (count == 0)
			{
				return;
			}
			int num = 0;
			int num2 = count;
			do
			{
				int num3 = compressor.Fill(buffer, offset, num2);
				if (num3 != num2)
				{
					_ = bw.TotalBytesWrittenOut;
					compressor.CompressAndWrite();
					combinedCRC = (combinedCRC << 1) | (combinedCRC >> 31);
					combinedCRC ^= compressor.Crc32;
					offset += num3;
				}
				num2 -= num3;
				num += num3;
			}
			while (num2 > 0);
			totalBytesWrittenIn += num;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		[Conditional("Trace")]
		private void TraceOutput(TraceBits bits, string format, params object[] varParams)
		{
			if ((bits & desiredTrace) != TraceBits.None)
			{
				int hashCode = Thread.CurrentThread.GetHashCode();
				Console.ForegroundColor = (ConsoleColor)(hashCode % 8 + 10);
				Console.Write("{0:000} PBOS ", hashCode);
				Console.WriteLine(format, varParams);
				Console.ResetColor();
			}
		}
	}
	internal class WorkItem
	{
		public int index;

		public MemoryStream ms;

		public int ordinal;

		public BitWriter bw;

		public BZip2Compressor Compressor { get; private set; }

		public WorkItem(int ix, int blockSize)
		{
			ms = new MemoryStream();
			bw = new BitWriter(ms);
			Compressor = new BZip2Compressor(bw, blockSize);
			index = ix;
		}
	}
	public class ParallelBZip2OutputStream : Stream
	{
		[Flags]
		private enum TraceBits : uint
		{
			None = 0u,
			Crc = 1u,
			Write = 2u,
			All = uint.MaxValue
		}

		private static readonly int BufferPairsPerCore = 4;

		private int _maxWorkers;

		private bool firstWriteDone;

		private int lastFilled;

		private int lastWritten;

		private int latestCompressed;

		private int currentlyFilling;

		private volatile Exception pendingException;

		private bool handlingException;

		private bool emitting;

		private Queue<int> toWrite;

		private Queue<int> toFill;

		private List<WorkItem> pool;

		private object latestLock = new object();

		private object eLock = new object();

		private object outputLock = new object();

		private AutoResetEvent newlyCompressedBlob;

		private long totalBytesWrittenIn;

		private long totalBytesWrittenOut;

		private bool leaveOpen;

		private uint combinedCRC;

		private Stream output;

		private BitWriter bw;

		private int blockSize100k;

		private TraceBits desiredTrace = TraceBits.Crc | TraceBits.Write;

		public int MaxWorkers
		{
			get
			{
				return _maxWorkers;
			}
			set
			{
				if (value < 4)
				{
					throw new ArgumentException("MaxWorkers", "Value must be 4 or greater.");
				}
				_maxWorkers = value;
			}
		}

		public int BlockSize => blockSize100k;

		public override bool CanRead => false;

		public override bool CanSeek => false;

		public override bool CanWrite
		{
			get
			{
				if (output == null)
				{
					throw new ObjectDisposedException("BZip2Stream");
				}
				return output.CanWrite;
			}
		}

		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override long Position
		{
			get
			{
				return totalBytesWrittenIn;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public long BytesWrittenOut => totalBytesWrittenOut;

		public ParallelBZip2OutputStream(Stream output)
			: this(output, BZip2.MaxBlockSize, leaveOpen: false)
		{
		}

		public ParallelBZip2OutputStream(Stream output, int blockSize)
			: this(output, blockSize, leaveOpen: false)
		{
		}

		public ParallelBZip2OutputStream(Stream output, bool leaveOpen)
			: this(output, BZip2.MaxBlockSize, leaveOpen)
		{
		}

		public ParallelBZip2OutputStream(Stream output, int blockSize, bool leaveOpen)
		{
			if (blockSize < BZip2.MinBlockSize || blockSize > BZip2.MaxBlockSize)
			{
				string message = $"blockSize={blockSize} is out of range; must be between {BZip2.MinBlockSize} and {BZip2.MaxBlockSize}";
				throw new ArgumentException(message, "blockSize");
			}
			this.output = output;
			if (!this.output.CanWrite)
			{
				throw new ArgumentException("The stream is not writable.", "output");
			}
			bw = new BitWriter(this.output);
			blockSize100k = blockSize;
			this.leaveOpen = leaveOpen;
			combinedCRC = 0u;
			MaxWorkers = 16;
			EmitHeader();
		}

		private void InitializePoolOfWorkItems()
		{
			toWrite = new Queue<int>();
			toFill = new Queue<int>();
			pool = new List<WorkItem>();
			int val = BufferPairsPerCore * Environment.ProcessorCount;
			val = Math.Min(val, MaxWorkers);
			for (int i = 0; i < val; i++)
			{
				pool.Add(new WorkItem(i, blockSize100k));
				toFill.Enqueue(i);
			}
			newlyCompressedBlob = new AutoResetEvent(initialState: false);
			currentlyFilling = -1;
			lastFilled = -1;
			lastWritten = -1;
			latestCompressed = -1;
		}

		public override void Close()
		{
			if (pendingException != null)
			{
				handlingException = true;
				Exception ex = pendingException;
				pendingException = null;
				throw ex;
			}
			if (!handlingException && output != null)
			{
				Stream stream = output;
				try
				{
					FlushOutput(lastInput: true);
				}
				finally
				{
					output = null;
					bw = null;
				}
				if (!leaveOpen)
				{
					stream.Close();
				}
			}
		}

		private void FlushOutput(bool lastInput)
		{
			if (!emitting)
			{
				if (currentlyFilling >= 0)
				{
					WorkItem wi = pool[currentlyFilling];
					CompressOne(wi);
					currentlyFilling = -1;
				}
				if (lastInput)
				{
					EmitPendingBuffers(doAll: true, mustWait: false);
					EmitTrailer();
				}
				else
				{
					EmitPendingBuffers(doAll: false, mustWait: false);
				}
			}
		}

		public override void Flush()
		{
			if (output != null)
			{
				FlushOutput(lastInput: false);
				bw.Flush();
				output.Flush();
			}
		}

		private void EmitHeader()
		{
			byte[] array = new byte[4] { 66, 90, 104, 0 };
			array[3] = (byte)(48 + blockSize100k);
			byte[] array2 = array;
			output.Write(array2, 0, array2.Length);
		}

		private void EmitTrailer()
		{
			bw.WriteByte(23);
			bw.WriteByte(114);
			bw.WriteByte(69);
			bw.WriteByte(56);
			bw.WriteByte(80);
			bw.WriteByte(144);
			bw.WriteInt(combinedCRC);
			bw.FinishAndPad();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			bool mustWait = false;
			if (output == null)
			{
				throw new IOException("the stream is not open");
			}
			if (pendingException != null)
			{
				handlingException = true;
				Exception ex = pendingException;
				pendingException = null;
				throw ex;
			}
			if (offset < 0)
			{
				throw new IndexOutOfRangeException($"offset ({offset}) must be > 0");
			}
			if (count < 0)
			{
				throw new IndexOutOfRangeException($"count ({count}) must be > 0");
			}
			if (offset + count > buffer.Length)
			{
				throw new IndexOutOfRangeException($"offset({offset}) count({count}) bLength({buffer.Length})");
			}
			if (count == 0)
			{
				return;
			}
			if (!firstWriteDone)
			{
				InitializePoolOfWorkItems();
				firstWriteDone = true;
			}
			int num = 0;
			int num2 = count;
			do
			{
				EmitPendingBuffers(doAll: false, mustWait);
				mustWait = false;
				int num3 = -1;
				if (currentlyFilling >= 0)
				{
					num3 = currentlyFilling;
				}
				else
				{
					if (toFill.Count == 0)
					{
						mustWait = true;
						continue;
					}
					num3 = toFill.Dequeue();
					lastFilled++;
				}
				WorkItem workItem = pool[num3];
				workItem.ordinal = lastFilled;
				int num4 = workItem.Compressor.Fill(buffer, offset, num2);
				if (num4 != num2)
				{
					if (!ThreadPool.QueueUserWorkItem(CompressOne, workItem))
					{
						throw new Exception("Cannot enqueue workitem");
					}
					currentlyFilling = -1;
					offset += num4;
				}
				else
				{
					currentlyFilling = num3;
				}
				num2 -= num4;
				num += num4;
			}
			while (num2 > 0);
			totalBytesWrittenIn += num;
		}

		private void EmitPendingBuffers(bool doAll, bool mustWait)
		{
			if (emitting)
			{
				return;
			}
			emitting = true;
			if (doAll || mustWait)
			{
				newlyCompressedBlob.WaitOne();
			}
			do
			{
				int num = -1;
				int num2 = (doAll ? 200 : (mustWait ? (-1) : 0));
				int num3 = -1;
				do
				{
					if (Monitor.TryEnter(toWrite, num2))
					{
						num3 = -1;
						try
						{
							if (toWrite.Count > 0)
							{
								num3 = toWrite.Dequeue();
							}
						}
						finally
						{
							Monitor.Exit(toWrite);
						}
						if (num3 < 0)
						{
							continue;
						}
						WorkItem workItem = pool[num3];
						if (workItem.ordinal != lastWritten + 1)
						{
							lock (toWrite)
							{
								toWrite.Enqueue(num3);
							}
							if (num == num3)
							{
								newlyCompressedBlob.WaitOne();
								num = -1;
							}
							else if (num == -1)
							{
								num = num3;
							}
							continue;
						}
						num = -1;
						BitWriter bitWriter = workItem.bw;
						bitWriter.Flush();
						MemoryStream ms = workItem.ms;
						ms.Seek(0L, SeekOrigin.Begin);
						long num4 = 0L;
						byte[] array = new byte[1024];
						int num5;
						while ((num5 = ms.Read(array, 0, array.Length)) > 0)
						{
							for (int i = 0; i < num5; i++)
							{
								bw.WriteByte(array[i]);
							}
							num4 += num5;
						}
						if (bitWriter.NumRemainingBits > 0)
						{
							bw.WriteBits(bitWriter.NumRemainingBits, bitWriter.RemainingBits);
						}
						combinedCRC = (combinedCRC << 1) | (combinedCRC >> 31);
						combinedCRC ^= workItem.Compressor.Crc32;
						totalBytesWrittenOut += num4;
						bitWriter.Reset();
						lastWritten = workItem.ordinal;
						workItem.ordinal = -1;
						toFill.Enqueue(workItem.index);
						if (num2 == -1)
						{
							num2 = 0;
						}
					}
					else
					{
						num3 = -1;
					}
				}
				while (num3 >= 0);
			}
			while (doAll && lastWritten != latestCompressed);
			emitting = false;
		}

		private void CompressOne(object wi)
		{
			WorkItem workItem = (WorkItem)wi;
			try
			{
				workItem.Compressor.CompressAndWrite();
				lock (latestLock)
				{
					if (workItem.ordinal > latestCompressed)
					{
						latestCompressed = workItem.ordinal;
					}
				}
				lock (toWrite)
				{
					toWrite.Enqueue(workItem.index);
				}
				newlyCompressedBlob.Set();
			}
			catch (Exception ex)
			{
				lock (eLock)
				{
					if (pendingException != null)
					{
						pendingException = ex;
					}
				}
			}
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		[Conditional("Trace")]
		private void TraceOutput(TraceBits bits, string format, params object[] varParams)
		{
			if ((bits & desiredTrace) != TraceBits.None)
			{
				lock (outputLock)
				{
					int hashCode = Thread.CurrentThread.GetHashCode();
					Console.ForegroundColor = (ConsoleColor)(hashCode % 8 + 10);
					Console.Write("{0:000} PBOS ", hashCode);
					Console.WriteLine(format, varParams);
					Console.ResetColor();
				}
			}
		}
	}
	internal static class Rand
	{
		private static int[] RNUMS = new int[512]
		{
			619, 720, 127, 481, 931, 816, 813, 233, 566, 247,
			985, 724, 205, 454, 863, 491, 741, 242, 949, 214,
			733, 859, 335, 708, 621, 574, 73, 654, 730, 472,
			419, 436, 278, 496, 867, 210, 399, 680, 480, 51,
			878, 465, 811, 169, 869, 675, 611, 697, 867, 561,
			862, 687, 507, 283, 482, 129, 807, 591, 733, 623,
			150, 238, 59, 379, 684, 877, 625, 169, 643, 105,
			170, 607, 520, 932, 727, 476, 693, 425, 174, 647,
			73, 122, 335, 530, 442, 853, 695, 249, 445, 515,
			909, 545, 703, 919, 874, 474, 882, 500, 594, 612,
			641, 801, 220, 162, 819, 984, 589, 513, 495, 799,
			161, 604, 958, 533, 221, 400, 386, 867, 600, 782,
			382, 596, 414, 171, 516, 375, 682, 485, 911, 276,
			98, 553, 163, 354, 666, 933, 424, 341, 533, 870,
			227, 730, 475, 186, 263, 647, 537, 686, 600, 224,
			469, 68, 770, 919, 190, 373, 294, 822, 808, 206,
			184, 943, 795, 384, 383, 461, 404, 758, 839, 887,
			715, 67, 618, 276, 204, 918, 873, 777, 604, 560,
			951, 160, 578, 722, 79, 804, 96, 409, 713, 940,
			652, 934, 970, 447, 318, 353, 859, 672, 112, 785,
			645, 863, 803, 350, 139, 93, 354, 99, 820, 908,
			609, 772, 154, 274, 580, 184, 79, 626, 630, 742,
			653, 282, 762, 623, 680, 81, 927, 626, 789, 125,
			411, 521, 938, 300, 821, 78, 343, 175, 128, 250,
			170, 774, 972, 275, 999, 639, 495, 78, 352, 126,
			857, 956, 358, 619, 580, 124, 737, 594, 701, 612,
			669, 112, 134, 694, 363, 992, 809, 743, 168, 974,
			944, 375, 748, 52, 600, 747, 642, 182, 862, 81,
			344, 805, 988, 739, 511, 655, 814, 334, 249, 515,
			897, 955, 664, 981, 649, 113, 974, 459, 893, 228,
			433, 837, 553, 268, 926, 240, 102, 654, 459, 51,
			686, 754, 806, 760, 493, 403, 415, 394, 687, 700,
			946, 670, 656, 610, 738, 392, 760, 799, 887, 653,
			978, 321, 576, 617, 626, 502, 894, 679, 243, 440,
			680, 879, 194, 572, 640, 724, 926, 56, 204, 700,
			707, 151, 457, 449, 797, 195, 791, 558, 945, 679,
			297, 59, 87, 824, 713, 663, 412, 693, 342, 606,
			134, 108, 571, 364, 631, 212, 174, 643, 304, 329,
			343, 97, 430, 751, 497, 314, 983, 374, 822, 928,
			140, 206, 73, 263, 980, 736, 876, 478, 430, 305,
			170, 514, 364, 692, 829, 82, 855, 953, 676, 246,
			369, 970, 294, 750, 807, 827, 150, 790, 288, 923,
			804, 378, 215, 828, 592, 281, 565, 555, 710, 82,
			896, 831, 547, 261, 524, 462, 293, 465, 502, 56,
			661, 821, 976, 991, 658, 869, 905, 758, 745, 193,
			768, 550, 608, 933, 378, 286, 215, 979, 792, 961,
			61, 688, 793, 644, 986, 403, 106, 366, 905, 644,
			372, 567, 466, 434, 645, 210, 389, 550, 919, 135,
			780, 773, 635, 389, 707, 100, 626, 958, 165, 504,
			920, 176, 193, 713, 857, 265, 203, 50, 668, 108,
			645, 990, 626, 197, 510, 357, 358, 850, 858, 364,
			936, 638
		};

		internal static int Rnums(int i)
		{
			return RNUMS[i];
		}
	}
}
namespace Ionic.Zlib
{
	internal enum BlockState
	{
		NeedMore,
		BlockDone,
		FinishStarted,
		FinishDone
	}
	internal enum DeflateFlavor
	{
		Store,
		Fast,
		Slow
	}
	internal sealed class DeflateManager
	{
		internal delegate BlockState CompressFunc(FlushType flush);

		internal class Config
		{
			internal int GoodLength;

			internal int MaxLazy;

			internal int NiceLength;

			internal int MaxChainLength;

			internal DeflateFlavor Flavor;

			private static readonly Config[] Table;

			private Config(int goodLength, int maxLazy, int niceLength, int maxChainLength, DeflateFlavor flavor)
			{
				GoodLength = goodLength;
				MaxLazy = maxLazy;
				NiceLength = niceLength;
				MaxChainLength = maxChainLength;
				Flavor = flavor;
			}

			public static Config Lookup(CompressionLevel level)
			{
				return Table[(int)level];
			}

			static Config()
			{
				Table = new Config[10]
				{
					new Config(0, 0, 0, 0, DeflateFlavor.Store),
					new Config(4, 4, 8, 4, DeflateFlavor.Fast),
					new Config(4, 5, 16, 8, DeflateFlavor.Fast),
					new Config(4, 6, 32, 32, DeflateFlavor.Fast),
					new Config(4, 4, 16, 16, DeflateFlavor.Slow),
					new Config(8, 16, 32, 32, DeflateFlavor.Slow),
					new Config(8, 16, 128, 128, DeflateFlavor.Slow),
					new Config(8, 32, 128, 256, DeflateFlavor.Slow),
					new Config(32, 128, 258, 1024, DeflateFlavor.Slow),
					new Config(32, 258, 258, 4096, DeflateFlavor.Slow)
				};
			}
		}

		private static readonly int MEM_LEVEL_MAX = 9;

		private static readonly int MEM_LEVEL_DEFAULT = 8;

		private CompressFunc DeflateFunction;

		private static readonly string[] _ErrorMessage = new string[10] { "need dictionary", "stream end", "", "file error", "stream error", "data error", "insufficient memory", "buffer error", "incompatible version", "" };

		private static readonly int PRESET_DICT = 32;

		private static readonly int INIT_STATE = 42;

		private static readonly int BUSY_STATE = 113;

		private static readonly int FINISH_STATE = 666;

		private static readonly int Z_DEFLATED = 8;

		private static readonly int STORED_BLOCK = 0;

		private static readonly int STATIC_TREES = 1;

		private static readonly int DYN_TREES = 2;

		private static readonly int Z_BINARY = 0;

		private static readonly int Z_ASCII = 1;

		private static readonly int Z_UNKNOWN = 2;

		private static readonly int Buf_size = 16;

		private static readonly int MIN_MATCH = 3;

		private static readonly int MAX_MATCH = 258;

		private static readonly int MIN_LOOKAHEAD = MAX_MATCH + MIN_MATCH + 1;

		private static readonly int HEAP_SIZE = 2 * InternalConstants.L_CODES + 1;

		private static readonly int END_BLOCK = 256;

		internal ZlibCodec _codec;

		internal int status;

		internal byte[] pending;

		internal int nextPending;

		internal int pendingCount;

		internal sbyte data_type;

		internal int last_flush;

		internal int w_size;

		internal int w_bits;

		internal int w_mask;

		internal byte[] window;

		internal int window_size;

		internal short[] prev;

		internal short[] head;

		internal int ins_h;

		internal int hash_size;

		internal int hash_bits;

		internal int hash_mask;

		internal int hash_shift;

		internal int block_start;

		private Config config;

		internal int match_length;

		internal int prev_match;

		internal int match_available;

		internal int strstart;

		internal int match_start;

		internal int lookahead;

		internal int prev_length;

		internal CompressionLevel compressionLevel;

		internal CompressionStrategy compressionStrategy;

		internal short[] dyn_ltree;

		internal short[] dyn_dtree;

		internal short[] bl_tree;

		internal Tree treeLiterals = new Tree();

		internal Tree treeDistances = new Tree();

		internal Tree treeBitLengths = new Tree();

		internal short[] bl_count = new short[InternalConstants.MAX_BITS + 1];

		internal int[] heap = new int[2 * InternalConstants.L_CODES + 1];

		internal int heap_len;

		internal int heap_max;

		internal sbyte[] depth = new sbyte[2 * InternalConstants.L_CODES + 1];

		internal int _lengthOffset;

		internal int lit_bufsize;

		internal int last_lit;

		internal int _distanceOffset;

		internal int opt_len;

		internal int static_len;

		internal int matches;

		internal int last_eob_len;

		internal short bi_buf;

		internal int bi_valid;

		private bool Rfc1950BytesEmitted;

		private bool _WantRfc1950HeaderBytes = true;

		internal bool WantRfc1950HeaderBytes
		{
			get
			{
				return _WantRfc1950HeaderBytes;
			}
			set
			{
				_WantRfc1950HeaderBytes = value;
			}
		}

		internal DeflateManager()
		{
			dyn_ltree = new short[HEAP_SIZE * 2];
			dyn_dtree = new short[(2 * InternalConstants.D_CODES + 1) * 2];
			bl_tree = new short[(2 * InternalConstants.BL_CODES + 1) * 2];
		}

		private void _InitializeLazyMatch()
		{
			window_size = 2 * w_size;
			Array.Clear(head, 0, hash_size);
			config = Config.Lookup(compressionLevel);
			SetDeflater();
			strstart = 0;
			block_start = 0;
			lookahead = 0;
			match_length = (prev_length = MIN_MATCH - 1);
			match_available = 0;
			ins_h = 0;
		}

		private void _InitializeTreeData()
		{
			treeLiterals.dyn_tree = dyn_ltree;
			treeLiterals.staticTree = StaticTree.Literals;
			treeDistances.dyn_tree = dyn_dtree;
			treeDistances.staticTree = StaticTree.Distances;
			treeBitLengths.dyn_tree = bl_tree;
			treeBitLengths.staticTree = StaticTree.BitLengths;
			bi_buf = 0;
			bi_valid = 0;
			last_eob_len = 8;
			_InitializeBlocks();
		}

		internal void _InitializeBlocks()
		{
			for (int i = 0; i < InternalConstants.L_CODES; i++)
			{
				dyn_ltree[i * 2] = 0;
			}
			for (int j = 0; j < InternalConstants.D_CODES; j++)
			{
				dyn_dtree[j * 2] = 0;
			}
			for (int k = 0; k < InternalConstants.BL_CODES; k++)
			{
				bl_tree[k * 2] = 0;
			}
			dyn_ltree[END_BLOCK * 2] = 1;
			opt_len = (static_len = 0);
			last_lit = (matches = 0);
		}

		internal void pqdownheap(short[] tree, int k)
		{
			int num = heap[k];
			for (int num2 = k << 1; num2 <= heap_len; num2 <<= 1)
			{
				if (num2 < heap_len && _IsSmaller(tree, heap[num2 + 1], heap[num2], depth))
				{
					num2++;
				}
				if (_IsSmaller(tree, num, heap[num2], depth))
				{
					break;
				}
				heap[k] = heap[num2];
				k = num2;
			}
			heap[k] = num;
		}

		internal static bool _IsSmaller(short[] tree, int n, int m, sbyte[] depth)
		{
			short num = tree[n * 2];
			short num2 = tree[m * 2];
			if (num >= num2)
			{
				if (num == num2)
				{
					return depth[n] <= depth[m];
				}
				return false;
			}
			return true;
		}

		internal void scan_tree(short[] tree, int max_code)
		{
			int num = -1;
			int num2 = tree[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			if (num2 == 0)
			{
				num4 = 138;
				num5 = 3;
			}
			tree[(max_code + 1) * 2 + 1] = short.MaxValue;
			for (int i = 0; i <= max_code; i++)
			{
				int num6 = num2;
				num2 = tree[(i + 1) * 2 + 1];
				if (++num3 < num4 && num6 == num2)
				{
					continue;
				}
				if (num3 < num5)
				{
					bl_tree[num6 * 2] = (short)(bl_tree[num6 * 2] + num3);
				}
				else if (num6 != 0)
				{
					if (num6 != num)
					{
						bl_tree[num6 * 2]++;
					}
					bl_tree[InternalConstants.REP_3_6 * 2]++;
				}
				else if (num3 <= 10)
				{
					bl_tree[InternalConstants.REPZ_3_10 * 2]++;
				}
				else
				{
					bl_tree[InternalConstants.REPZ_11_138 * 2]++;
				}
				num3 = 0;
				num = num6;
				if (num2 == 0)
				{
					num4 = 138;
					num5 = 3;
				}
				else if (num6 == num2)
				{
					num4 = 6;
					num5 = 3;
				}
				else
				{
					num4 = 7;
					num5 = 4;
				}
			}
		}

		internal int build_bl_tree()
		{
			scan_tree(dyn_ltree, treeLiterals.max_code);
			scan_tree(dyn_dtree, treeDistances.max_code);
			treeBitLengths.build_tree(this);
			int num = InternalConstants.BL_CODES - 1;
			while (num >= 3 && bl_tree[Tree.bl_order[num] * 2 + 1] == 0)
			{
				num--;
			}
			opt_len += 3 * (num + 1) + 5 + 5 + 4;
			return num;
		}

		internal void send_all_trees(int lcodes, int dcodes, int blcodes)
		{
			send_bits(lcodes - 257, 5);
			send_bits(dcodes - 1, 5);
			send_bits(blcodes - 4, 4);
			for (int i = 0; i < blcodes; i++)
			{
				send_bits(bl_tree[Tree.bl_order[i] * 2 + 1], 3);
			}
			send_tree(dyn_ltree, lcodes - 1);
			send_tree(dyn_dtree, dcodes - 1);
		}

		internal void send_tree(short[] tree, int max_code)
		{
			int num = -1;
			int num2 = tree[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			if (num2 == 0)
			{
				num4 = 138;
				num5 = 3;
			}
			for (int i = 0; i <= max_code; i++)
			{
				int num6 = num2;
				num2 = tree[(i + 1) * 2 + 1];
				if (++num3 < num4 && num6 == num2)
				{
					continue;
				}
				if (num3 < num5)
				{
					do
					{
						send_code(num6, bl_tree);
					}
					while (--num3 != 0);
				}
				else if (num6 != 0)
				{
					if (num6 != num)
					{
						send_code(num6, bl_tree);
						num3--;
					}
					send_code(InternalConstants.REP_3_6, bl_tree);
					send_bits(num3 - 3, 2);
				}
				else if (num3 <= 10)
				{
					send_code(InternalConstants.REPZ_3_10, bl_tree);
					send_bits(num3 - 3, 3);
				}
				else
				{
					send_code(InternalConstants.REPZ_11_138, bl_tree);
					send_bits(num3 - 11, 7);
				}
				num3 = 0;
				num = num6;
				if (num2 == 0)
				{
					num4 = 138;
					num5 = 3;
				}
				else if (num6 == num2)
				{
					num4 = 6;
					num5 = 3;
				}
				else
				{
					num4 = 7;
					num5 = 4;
				}
			}
		}

		private void put_bytes(byte[] p, int start, int len)
		{
			Array.Copy(p, start, pending, pendingCount, len);
			pendingCount += len;
		}

		internal void send_code(int c, short[] tree)
		{
			int num = c * 2;
			send_bits(tree[num] & 0xFFFF, tree[num + 1] & 0xFFFF);
		}

		internal void send_bits(int value, int length)
		{
			if (bi_valid > Buf_size - length)
			{
				bi_buf |= (short)((value << bi_valid) & 0xFFFF);
				pending[pendingCount++] = (byte)bi_buf;
				pending[pendingCount++] = (byte)(bi_buf >> 8);
				bi_buf = (short)(value >>> Buf_size - bi_valid);
				bi_valid += length - Buf_size;
			}
			else
			{
				bi_buf |= (short)((value << bi_valid) & 0xFFFF);
				bi_valid += length;
			}
		}

		internal void _tr_align()
		{
			send_bits(STATIC_TREES << 1, 3);
			send_code(END_BLOCK, StaticTree.lengthAndLiteralsTreeCodes);
			bi_flush();
			if (1 + last_eob_len + 10 - bi_valid < 9)
			{
				send_bits(STATIC_TREES << 1, 3);
				send_code(END_BLOCK, StaticTree.lengthAndLiteralsTreeCodes);
				bi_flush();
			}
			last_eob_len = 7;
		}

		internal bool _tr_tally(int dist, int lc)
		{
			pending[_distanceOffset + last_lit * 2] = (byte)((uint)dist >> 8);
			pending[_distanceOffset + last_lit * 2 + 1] = (byte)dist;
			pending[_lengthOffset + last_lit] = (byte)lc;
			last_lit++;
			if (dist == 0)
			{
				dyn_ltree[lc * 2]++;
			}
			else
			{
				matches++;
				dist--;
				dyn_ltree[(Tree.LengthCode[lc] + InternalConstants.LITERALS + 1) * 2]++;
				dyn_dtree[Tree.DistanceCode(dist) * 2]++;
			}
			if ((last_lit & 0x1FFF) == 0 && compressionLevel > CompressionLevel.Level2)
			{
				int num = last_lit << 3;
				int num2 = strstart - block_start;
				for (int i = 0; i < InternalConstants.D_CODES; i++)
				{
					num = (int)(num + dyn_dtree[i * 2] * (5L + (long)Tree.ExtraDistanceBits[i]));
				}
				num >>= 3;
				if (matches < last_lit / 2 && num < num2 / 2)
				{
					return true;
				}
			}
			if (last_lit != lit_bufsize - 1)
			{
				return last_lit == lit_bufsize;
			}
			return true;
		}

		internal void send_compressed_block(short[] ltree, short[] dtree)
		{
			int num = 0;
			if (last_lit != 0)
			{
				do
				{
					int num2 = _distanceOffset + num * 2;
					int num3 = ((pending[num2] << 8) & 0xFF00) | (pending[num2 + 1] & 0xFF);
					int num4 = pending[_lengthOffset + num] & 0xFF;
					num++;
					if (num3 == 0)
					{
						send_code(num4, ltree);
						continue;
					}
					int num5 = Tree.LengthCode[num4];
					send_code(num5 + InternalConstants.LITERALS + 1, ltree);
					int num6 = Tree.ExtraLengthBits[num5];
					if (num6 != 0)
					{
						num4 -= Tree.LengthBase[num5];
						send_bits(num4, num6);
					}
					num3--;
					num5 = Tree.DistanceCode(num3);
					send_code(num5, dtree);
					num6 = Tree.ExtraDistanceBits[num5];
					if (num6 != 0)
					{
						num3 -= Tree.DistanceBase[num5];
						send_bits(num3, num6);
					}
				}
				while (num < last_lit);
			}
			send_code(END_BLOCK, ltree);
			last_eob_len = ltree[END_BLOCK * 2 + 1];
		}

		internal void set_data_type()
		{
			int i = 0;
			int num = 0;
			int num2 = 0;
			for (; i < 7; i++)
			{
				num2 += dyn_ltree[i * 2];
			}
			for (; i < 128; i++)
			{
				num += dyn_ltree[i * 2];
			}
			for (; i < InternalConstants.LITERALS; i++)
			{
				num2 += dyn_ltree[i * 2];
			}
			data_type = (sbyte)((num2 > num >> 2) ? Z_BINARY : Z_ASCII);
		}

		internal void bi_flush()
		{
			if (bi_valid == 16)
			{
				pending[pendingCount++] = (byte)bi_buf;
				pending[pendingCount++] = (byte)(bi_buf >> 8);
				bi_buf = 0;
				bi_valid = 0;
			}
			else if (bi_valid >= 8)
			{
				pending[pendingCount++] = (byte)bi_buf;
				bi_buf >>= 8;
				bi_valid -= 8;
			}
		}

		internal void bi_windup()
		{
			if (bi_valid > 8)
			{
				pending[pendingCount++] = (byte)bi_buf;
				pending[pendingCount++] = (byte)(bi_buf >> 8);
			}
			else if (bi_valid > 0)
			{
				pending[pendingCount++] = (byte)bi_buf;
			}
			bi_buf = 0;
			bi_valid = 0;
		}

		internal void copy_block(int buf, int len, bool header)
		{
			bi_windup();
			last_eob_len = 8;
			if (header)
			{
				pending[pendingCount++] = (byte)len;
				pending[pendingCount++] = (byte)(len >> 8);
				pending[pendingCount++] = (byte)(~len);
				pending[pendingCount++] = (byte)(~len >> 8);
			}
			put_bytes(window, buf, len);
		}

		internal void flush_block_only(bool eof)
		{
			_tr_flush_block((block_start >= 0) ? block_start : (-1), strstart - block_start, eof);
			block_start = strstart;
			_codec.flush_pending();
		}

		internal BlockState DeflateNone(FlushType flush)
		{
			int num = 65535;
			if (num > pending.Length - 5)
			{
				num = pending.Length - 5;
			}
			while (true)
			{
				if (lookahead <= 1)
				{
					_fillWindow();
					if (lookahead == 0 && flush == FlushType.None)
					{
						return BlockState.NeedMore;
					}
					if (lookahead == 0)
					{
						break;
					}
				}
				strstart += lookahead;
				lookahead = 0;
				int num2 = block_start + num;
				if (strstart == 0 || strstart >= num2)
				{
					lookahead = strstart - num2;
					strstart = num2;
					flush_block_only(eof: false);
					if (_codec.AvailableBytesOut == 0)
					{
						return BlockState.NeedMore;
					}
				}
				if (strstart - block_start >= w_size - MIN_LOOKAHEAD)
				{
					flush_block_only(eof: false);
					if (_codec.AvailableBytesOut == 0)
					{
						return BlockState.NeedMore;
					}
				}
			}
			flush_block_only(flush == FlushType.Finish);
			if (_codec.AvailableBytesOut == 0)
			{
				if (flush != FlushType.Finish)
				{
					return BlockState.NeedMore;
				}
				return BlockState.FinishStarted;
			}
			if (flush != FlushType.Finish)
			{
				return BlockState.BlockDone;
			}
			return BlockState.FinishDone;
		}

		internal void _tr_stored_block(int buf, int stored_len, bool eof)
		{
			send_bits((STORED_BLOCK << 1) + (eof ? 1 : 0), 3);
			copy_block(buf, stored_len, header: true);
		}

		internal void _tr_flush_block(int buf, int stored_len, bool eof)
		{
			int num = 0;
			int num2;
			int num3;
			if (compressionLevel > CompressionLevel.None)
			{
				if (data_type == Z_UNKNOWN)
				{
					set_data_type();
				}
				treeLiterals.build_tree(this);
				treeDistances.build_tree(this);
				num = build_bl_tree();
				num2 = opt_len + 3 + 7 >> 3;
				num3 = static_len + 3 + 7 >> 3;
				if (num3 <= num2)
				{
					num2 = num3;
				}
			}
			else
			{
				num2 = (num3 = stored_len + 5);
			}
			if (stored_len + 4 <= num2 && buf != -1)
			{
				_tr_stored_block(buf, stored_len, eof);
			}
			else if (num3 == num2)
			{
				send_bits((STATIC_TREES << 1) + (eof ? 1 : 0), 3);
				send_compressed_block(StaticTree.lengthAndLiteralsTreeCodes, StaticTree.distTreeCodes);
			}
			else
			{
				send_bits((DYN_TREES << 1) + (eof ? 1 : 0), 3);
				send_all_trees(treeLiterals.max_code + 1, treeDistances.max_code + 1, num + 1);
				send_compressed_block(dyn_ltree, dyn_dtree);
			}
			_InitializeBlocks();
			if (eof)
			{
				bi_windup();
			}
		}

		private void _fillWindow()
		{
			do
			{
				int num = window_size - lookahead - strstart;
				int num2;
				if (num == 0 && strstart == 0 && lookahead == 0)
				{
					num = w_size;
				}
				else if (num == -1)
				{
					num--;
				}
				else if (strstart >= w_size + w_size - MIN_LOOKAHEAD)
				{
					Array.Copy(window, w_size, window, 0, w_size);
					match_start -= w_size;
					strstart -= w_size;
					block_start -= w_size;
					num2 = hash_size;
					int num3 = num2;
					do
					{
						int num4 = head[--num3] & 0xFFFF;
						head[num3] = (short)((num4 >= w_size) ? (num4 - w_size) : 0);
					}
					while (--num2 != 0);
					num2 = w_size;
					num3 = num2;
					do
					{
						int num4 = prev[--num3] & 0xFFFF;
						prev[num3] = (short)((num4 >= w_size) ? (num4 - w_size) : 0);
					}
					while (--num2 != 0);
					num += w_size;
				}
				if (_codec.AvailableBytesIn == 0)
				{
					break;
				}
				num2 = _codec.read_buf(window, strstart + lookahead, num);
				lookahead += num2;
				if (lookahead >= MIN_MATCH)
				{
					ins_h = window[strstart] & 0xFF;
					ins_h = ((ins_h << hash_shift) ^ (window[strstart + 1] & 0xFF)) & hash_mask;
				}
			}
			while (lookahead < MIN_LOOKAHEAD && _codec.AvailableBytesIn != 0);
		}

		internal BlockState DeflateFast(FlushType flush)
		{
			int num = 0;
			while (true)
			{
				if (lookahead < MIN_LOOKAHEAD)
				{
					_fillWindow();
					if (lookahead < MIN_LOOKAHEAD && flush == FlushType.None)
					{
						return BlockState.NeedMore;
					}
					if (lookahead == 0)
					{
						break;
					}
				}
				if (lookahead >= MIN_MATCH)
				{
					ins_h = ((ins_h << hash_shift) ^ (window[strstart + (MIN_MATCH - 1)] & 0xFF)) & hash_mask;
					num = head[ins_h] & 0xFFFF;
					prev[strstart & w_mask] = head[ins_h];
					head[ins_h] = (short)strstart;
				}
				if ((long)num != 0 && ((strstart - num) & 0xFFFF) <= w_size - MIN_LOOKAHEAD && compressionStrategy != CompressionStrategy.HuffmanOnly)
				{
					match_length = longest_match(num);
				}
				bool flag;
				if (match_length >= MIN_MATCH)
				{
					flag = _tr_tally(strstart - match_start, match_length - MIN_MATCH);
					lookahead -= match_length;
					if (match_length <= config.MaxLazy && lookahead >= MIN_MATCH)
					{
						match_length--;
						do
						{
							strstart++;
							ins_h = ((ins_h << hash_shift) ^ (window[strstart + (MIN_MATCH - 1)] & 0xFF)) & hash_mask;
							num = head[ins_h] & 0xFFFF;
							prev[strstart & w_mask] = head[ins_h];
							head[ins_h] = (short)strstart;
						}
						while (--match_length != 0);
						strstart++;
					}
					else
					{
						strstart += match_length;
						match_length = 0;
						ins_h = window[strstart] & 0xFF;
						ins_h = ((ins_h << hash_shift) ^ (window[strstart + 1] & 0xFF)) & hash_mask;
					}
				}
				else
				{
					flag = _tr_tally(0, window[strstart] & 0xFF);
					lookahead--;
					strstart++;
				}
				if (flag)
				{
					flush_block_only(eof: false);
					if (_codec.AvailableBytesOut == 0)
					{
						return BlockState.NeedMore;
					}
				}
			}
			flush_block_only(flush == FlushType.Finish);
			if (_codec.AvailableBytesOut == 0)
			{
				if (flush == FlushType.Finish)
				{
					return BlockState.FinishStarted;
				}
				return BlockState.NeedMore;
			}
			if (flush != FlushType.Finish)
			{
				return BlockState.BlockDone;
			}
			return BlockState.FinishDone;
		}

		internal BlockState DeflateSlow(FlushType flush)
		{
			int num = 0;
			while (true)
			{
				if (lookahead < MIN_LOOKAHEAD)
				{
					_fillWindow();
					if (lookahead < MIN_LOOKAHEAD && flush == FlushType.None)
					{
						return BlockState.NeedMore;
					}
					if (lookahead == 0)
					{
						break;
					}
				}
				if (lookahead >= MIN_MATCH)
				{
					ins_h = ((ins_h << hash_shift) ^ (window[strstart + (MIN_MATCH - 1)] & 0xFF)) & hash_mask;
					num = head[ins_h] & 0xFFFF;
					prev[strstart & w_mask] = head[ins_h];
					head[ins_h] = (short)strstart;
				}
				prev_length = match_length;
				prev_match = match_start;
				match_length = MIN_MATCH - 1;
				if (num != 0 && prev_length < config.MaxLazy && ((strstart - num) & 0xFFFF) <= w_size - MIN_LOOKAHEAD)
				{
					if (compressionStrategy != CompressionStrategy.HuffmanOnly)
					{
						match_length = longest_match(num);
					}
					if (match_length <= 5 && (compressionStrategy == CompressionStrategy.Filtered || (match_length == MIN_MATCH && strstart - match_start > 4096)))
					{
						match_length = MIN_MATCH - 1;
					}
				}
				if (prev_length >= MIN_MATCH && match_length <= prev_length)
				{
					int num2 = strstart + lookahead - MIN_MATCH;
					bool flag = _tr_tally(strstart - 1 - prev_match, prev_length - MIN_MATCH);
					lookahead -= prev_length - 1;
					prev_length -= 2;
					do
					{
						if (++strstart <= num2)
						{
							ins_h = ((ins_h << hash_shift) ^ (window[strstart + (MIN_MATCH - 1)] & 0xFF)) & hash_mask;
							num = head[ins_h] & 0xFFFF;
							prev[strstart & w_mask] = head[ins_h];
							head[ins_h] = (short)strstart;
						}
					}
					while (--prev_length != 0);
					match_available = 0;
					match_length = MIN_MATCH - 1;
					strstart++;
					if (flag)
					{
						flush_block_only(eof: false);
						if (_codec.AvailableBytesOut == 0)
						{
							return BlockState.NeedMore;
						}
					}
				}
				else if (match_available != 0)
				{
					if (_tr_tally(0, window[strstart - 1] & 0xFF))
					{
						flush_block_only(eof: false);
					}
					strstart++;
					lookahead--;
					if (_codec.AvailableBytesOut == 0)
					{
						return BlockState.NeedMore;
					}
				}
				else
				{
					match_available = 1;
					strstart++;
					lookahead--;
				}
			}
			if (match_available != 0)
			{
				bool flag = _tr_tally(0, window[strstart - 1] & 0xFF);
				match_available = 0;
			}
			flush_block_only(flush == FlushType.Finish);
			if (_codec.AvailableBytesOut == 0)
			{
				if (flush == FlushType.Finish)
				{
					return BlockState.FinishStarted;
				}
				return BlockState.NeedMore;
			}
			if (flush != FlushType.Finish)
			{
				return BlockState.BlockDone;
			}
			return BlockState.FinishDone;
		}

		internal int longest_match(int cur_match)
		{
			int num = config.MaxChainLength;
			int num2 = strstart;
			int num3 = prev_length;
			int num4 = ((strstart > w_size - MIN_LOOKAHEAD) ? (strstart - (w_size - MIN_LOOKAHEAD)) : 0);
			int niceLength = config.NiceLength;
			int num5 = w_mask;
			int num6 = strstart + MAX_MATCH;
			byte b = window[num2 + num3 - 1];
			byte b2 = window[num2 + num3];
			if (prev_length >= config.GoodLength)
			{
				num >>= 2;
			}
			if (niceLength > lookahead)
			{
				niceLength = lookahead;
			}
			do
			{
				int num7 = cur_match;
				if (window[num7 + num3] != b2 || window[num7 + num3 - 1] != b || window[num7] != window[num2] || window[++num7] != window[num2 + 1])
				{
					continue;
				}
				num2 += 2;
				num7++;
				while (window[++num2] == window[++num7] && window[++num2] == window[++num7] && window[++num2] == window[++num7] && window[++num2] == window[++num7] && window[++num2] == window[++num7] && window[++num2] == window[++num7] && window[++num2] == window[++num7] && window[++num2] == window[++num7] && num2 < num6)
				{
				}
				int num8 = MAX_MATCH - (num6 - num2);
				num2 = num6 - MAX_MATCH;
				if (num8 > num3)
				{
					match_start = cur_match;
					num3 = num8;
					if (num8 >= niceLength)
					{
						break;
					}
					b = window[num2 + num3 - 1];
					b2 = window[num2 + num3];
				}
			}
			while ((cur_match = prev[cur_match & num5] & 0xFFFF) > num4 && --num != 0);
			if (num3 <= lookahead)
			{
				return num3;
			}
			return lookahead;
		}

		internal int Initialize(ZlibCodec codec, CompressionLevel level)
		{
			return Initialize(codec, level, 15);
		}

		internal int Initialize(ZlibCodec codec, CompressionLevel level, int bits)
		{
			return Initialize(codec, level, bits, MEM_LEVEL_DEFAULT, CompressionStrategy.Default);
		}

		internal int Initialize(ZlibCodec codec, CompressionLevel level, int bits, CompressionStrategy compressionStrategy)
		{
			return Initialize(codec, level, bits, MEM_LEVEL_DEFAULT, compressionStrategy);
		}

		internal int Initialize(ZlibCodec codec, CompressionLevel level, int windowBits, int memLevel, CompressionStrategy strategy)
		{
			_codec = codec;
			_codec.Message = null;
			if (windowBits < 9 || windowBits > 15)
			{
				throw new ZlibException("windowBits must be in the range 9..15.");
			}
			if (memLevel < 1 || memLevel > MEM_LEVEL_MAX)
			{
				throw new ZlibException($"memLevel must be in the range 1.. {MEM_LEVEL_MAX}");
			}
			_codec.dstate = this;
			w_bits = windowBits;
			w_size = 1 << w_bits;
			w_mask = w_size - 1;
			hash_bits = memLevel + 7;
			hash_size = 1 << hash_bits;
			hash_mask = hash_size - 1;
			hash_shift = (hash_bits + MIN_MATCH - 1) / MIN_MATCH;
			window = new byte[w_size * 2];
			prev = new short[w_size];
			head = new short[hash_size];
			lit_bufsize = 1 << memLevel + 6;
			pending = new byte[lit_bufsize * 4];
			_distanceOffset = lit_bufsize;
			_lengthOffset = 3 * lit_bufsize;
			compressionLevel = level;
			compressionStrategy = strategy;
			Reset();
			return 0;
		}

		internal void Reset()
		{
			_codec.TotalBytesIn = (_codec.TotalBytesOut = 0L);
			_codec.Message = null;
			pendingCount = 0;
			nextPending = 0;
			Rfc1950BytesEmitted = false;
			status = (WantRfc1950HeaderBytes ? INIT_STATE : BUSY_STATE);
			_codec._Adler32 = Adler.Adler32(0u, null, 0, 0);
			last_flush = 0;
			_InitializeTreeData();
			_InitializeLazyMatch();
		}

		internal int End()
		{
			if (status != INIT_STATE && status != BUSY_STATE && status != FINISH_STATE)
			{
				return -2;
			}
			pending = null;
			head = null;
			prev = null;
			window = null;
			if (status != BUSY_STATE)
			{
				return 0;
			}
			return -3;
		}

		private void SetDeflater()
		{
			switch (config.Flavor)
			{
			case DeflateFlavor.Store:
				DeflateFunction = DeflateNone;
				break;
			case DeflateFlavor.Fast:
				DeflateFunction = DeflateFast;
				break;
			case DeflateFlavor.Slow:
				DeflateFunction = DeflateSlow;
				break;
			}
		}

		internal int SetParams(CompressionLevel level, CompressionStrategy strategy)
		{
			int result = 0;
			if (compressionLevel != level)
			{
				Config config = Config.Lookup(level);
				if (config.Flavor != this.config.Flavor && _codec.TotalBytesIn != 0)
				{
					result = _codec.Deflate(FlushType.Partial);
				}
				compressionLevel = level;
				this.config = config;
				SetDeflater();
			}
			compressionStrategy = strategy;
			return result;
		}

		internal int SetDictionary(byte[] dictionary)
		{
			int num = dictionary.Length;
			int sourceIndex = 0;
			if (dictionary == null || status != INIT_STATE)
			{
				throw new ZlibException("Stream error.");
			}
			_codec._Adler32 = Adler.Adler32(_codec._Adler32, dictionary, 0, dictionary.Length);
			if (num < MIN_MATCH)
			{
				return 0;
			}
			if (num > w_size - MIN_LOOKAHEAD)
			{
				num = w_size - MIN_LOOKAHEAD;
				sourceIndex = dictionary.Length - num;
			}
			Array.Copy(dictionary, sourceIndex, window, 0, num);
			strstart = num;
			block_start = num;
			ins_h = window[0] & 0xFF;
			ins_h = ((ins_h << hash_shift) ^ (window[1] & 0xFF)) & hash_mask;
			for (int i = 0; i <= num - MIN_MATCH; i++)
			{
				ins_h = ((ins_h << hash_shift) ^ (window[i + (MIN_MATCH - 1)] & 0xFF)) & hash_mask;
				prev[i & w_mask] = head[ins_h];
				head[ins_h] = (short)i;
			}
			return 0;
		}

		internal int Deflate(FlushType flush)
		{
			if (_codec.OutputBuffer == null || (_codec.InputBuffer == null && _codec.AvailableBytesIn != 0) || (status == FINISH_STATE && flush != FlushType.Finish))
			{
				_codec.Message = _ErrorMessage[4];
				throw new ZlibException($"Something is fishy. [{_codec.Message}]");
			}
			if (_codec.AvailableBytesOut == 0)
			{
				_codec.Message = _ErrorMessage[7];
				throw new ZlibException("OutputBuffer is full (AvailableBytesOut == 0)");
			}
			int num = last_flush;
			last_flush = (int)flush;
			if (status == INIT_STATE)
			{
				int num2 = Z_DEFLATED + (w_bits - 8 << 4) << 8;
				int num3 = (int)((compressionLevel - 1) & (CompressionLevel)255) >> 1;
				if (num3 > 3)
				{
					num3 = 3;
				}
				num2 |= num3 << 6;
				if (strstart != 0)
				{
					num2 |= PRESET_DICT;
				}
				num2 += 31 - num2 % 31;
				status = BUSY_STATE;
				pending[pendingCount++] = (byte)(num2 >> 8);
				pending[pendingCount++] = (byte)num2;
				if (strstart != 0)
				{
					pending[pendingCount++] = (byte)((_codec._Adler32 & 0xFF000000u) >> 24);
					pending[pendingCount++] = (byte)((_codec._Adler32 & 0xFF0000) >> 16);
					pending[pendingCount++] = (byte)((_codec._Adler32 & 0xFF00) >> 8);
					pending[pendingCount++] = (byte)(_codec._Adler32 & 0xFF);
				}
				_codec._Adler32 = Adler.Adler32(0u, null, 0, 0);
			}
			if (pendingCount != 0)
			{
				_codec.flush_pending();
				if (_codec.AvailableBytesOut == 0)
				{
					last_flush = -1;
					return 0;
				}
			}
			else if (_codec.AvailableBytesIn == 0 && (int)flush <= num && flush != FlushType.Finish)
			{
				return 0;
			}
			if (status == FINISH_STATE && _codec.AvailableBytesIn != 0)
			{
				_codec.Message = _ErrorMessage[7];
				throw new ZlibException("status == FINISH_STATE && _codec.AvailableBytesIn != 0");
			}
			if (_codec.AvailableBytesIn != 0 || lookahead != 0 || (flush != FlushType.None && status != FINISH_STATE))
			{
				BlockState blockState = DeflateFunction(flush);
				if (blockState == BlockState.FinishStarted || blockState == BlockState.FinishDone)
				{
					status = FINISH_STATE;
				}
				switch (blockState)
				{
				case BlockState.NeedMore:
				case BlockState.FinishStarted:
					if (_codec.AvailableBytesOut == 0)
					{
						last_flush = -1;
					}
					return 0;
				case BlockState.BlockDone:
					if (flush == FlushType.Partial)
					{
						_tr_align();
					}
					else
					{
						_tr_stored_block(0, 0, eof: false);
						if (flush == FlushType.Full)
						{
							for (int i = 0; i < hash_size; i++)
							{
								head[i] = 0;
							}
						}
					}
					_codec.flush_pending();
					if (_codec.AvailableBytesOut == 0)
					{
						last_flush = -1;
						return 0;
					}
					break;
				}
			}
			if (flush != FlushType.Finish)
			{
				return 0;
			}
			if (!WantRfc1950HeaderBytes || Rfc1950BytesEmitted)
			{
				return 1;
			}
			pending[pendingCount++] = (byte)((_codec._Adler32 & 0xFF000000u) >> 24);
			pending[pendingCount++] = (byte)((_codec._Adler32 & 0xFF0000) >> 16);
			pending[pendingCount++] = (byte)((_codec._Adler32 & 0xFF00) >> 8);
			pending[pendingCount++] = (byte)(_codec._Adler32 & 0xFF);
			_codec.flush_pending();
			Rfc1950BytesEmitted = true;
			if (pendingCount == 0)
			{
				return 1;
			}
			return 0;
		}
	}
	public class DeflateStream : Stream
	{
		internal ZlibBaseStream _baseStream;

		internal Stream _innerStream;

		private bool _disposed;

		public virtual FlushType FlushMode
		{
			get
			{
				return _baseStream._flushMode;
			}
			set
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				_baseStream._flushMode = value;
			}
		}

		public int BufferSize
		{
			get
			{
				return _baseStream._bufferSize;
			}
			set
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				if (_baseStream._workingBuffer != null)
				{
					throw new ZlibException("The working buffer is already set.");
				}
				if (value < 1024)
				{
					throw new ZlibException($"Don't be silly. {value} bytes?? Use a bigger buffer, at least {1024}.");
				}
				_baseStream._bufferSize = value;
			}
		}

		public CompressionStrategy Strategy
		{
			get
			{
				return _baseStream.Strategy;
			}
			set
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				_baseStream.Strategy = value;
			}
		}

		public virtual long TotalIn => _baseStream._z.TotalBytesIn;

		public virtual long TotalOut => _baseStream._z.TotalBytesOut;

		public override bool CanRead
		{
			get
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return _baseStream._stream.CanRead;
			}
		}

		public override bool CanSeek => false;

		public override bool CanWrite
		{
			get
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return _baseStream._stream.CanWrite;
			}
		}

		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override long Position
		{
			get
			{
				if (_baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return _baseStream._z.TotalBytesOut;
				}
				if (_baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return _baseStream._z.TotalBytesIn;
				}
				return 0L;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public DeflateStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, leaveOpen: false)
		{
		}

		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, leaveOpen: false)
		{
		}

		public DeflateStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			_innerStream = stream;
			_baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.DEFLATE, leaveOpen);
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!_disposed)
				{
					if (disposing && _baseStream != null)
					{
						_baseStream.Close();
					}
					_disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		public override void Flush()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			_baseStream.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			return _baseStream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			_baseStream.Write(buffer, offset, count);
		}

		public static byte[] CompressString(string s)
		{
			using MemoryStream memoryStream = new MemoryStream();
			Stream compressor = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
			ZlibBaseStream.CompressString(s, compressor);
			return memoryStream.ToArray();
		}

		public static byte[] CompressBuffer(byte[] b)
		{
			using MemoryStream memoryStream = new MemoryStream();
			Stream compressor = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
			ZlibBaseStream.CompressBuffer(b, compressor);
			return memoryStream.ToArray();
		}

		public static string UncompressString(byte[] compressed)
		{
			using MemoryStream stream = new MemoryStream(compressed);
			Stream decompressor = new DeflateStream(stream, CompressionMode.Decompress);
			return ZlibBaseStream.UncompressString(compressed, decompressor);
		}

		public static byte[] UncompressBuffer(byte[] compressed)
		{
			using MemoryStream stream = new MemoryStream(compressed);
			Stream decompressor = new DeflateStream(stream, CompressionMode.Decompress);
			return ZlibBaseStream.UncompressBuffer(compressed, decompressor);
		}
	}
	public class GZipStream : Stream
	{
		public DateTime? LastModified;

		private int _headerByteCount;

		internal ZlibBaseStream _baseStream;

		private bool _disposed;

		private bool _firstReadDone;

		private string _FileName;

		private string _Comment;

		private int _Crc32;

		internal static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		internal static readonly Encoding iso8859dash1 = Encoding.GetEncoding("iso-8859-1");

		public string Comment
		{
			get
			{
				return _Comment;
			}
			set
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				_Comment = value;
			}
		}

		public string FileName
		{
			get
			{
				return _FileName;
			}
			set
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				_FileName = value;
				if (_FileName != null)
				{
					if (_FileName.IndexOf("/") != -1)
					{
						_FileName = _FileName.Replace("/", "\\");
					}
					if (_FileName.EndsWith("\\"))
					{
						throw new Exception("Illegal filename");
					}
					if (_FileName.IndexOf("\\") != -1)
					{
						_FileName = Path.GetFileName(_FileName);
					}
				}
			}
		}

		public int Crc32 => _Crc32;

		public virtual FlushType FlushMode
		{
			get
			{
				return _baseStream._flushMode;
			}
			set
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				_baseStream._flushMode = value;
			}
		}

		public int BufferSize
		{
			get
			{
				return _baseStream._bufferSize;
			}
			set
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				if (_baseStream._workingBuffer != null)
				{
					throw new ZlibException("The working buffer is already set.");
				}
				if (value < 1024)
				{
					throw new ZlibException($"Don't be silly. {value} bytes?? Use a bigger buffer, at least {1024}.");
				}
				_baseStream._bufferSize = value;
			}
		}

		public virtual long TotalIn => _baseStream._z.TotalBytesIn;

		public virtual long TotalOut => _baseStream._z.TotalBytesOut;

		public override bool CanRead
		{
			get
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return _baseStream._stream.CanRead;
			}
		}

		public override bool CanSeek => false;

		public override bool CanWrite
		{
			get
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return _baseStream._stream.CanWrite;
			}
		}

		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override long Position
		{
			get
			{
				if (_baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return _baseStream._z.TotalBytesOut + _headerByteCount;
				}
				if (_baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return _baseStream._z.TotalBytesIn + _baseStream._gzipHeaderByteCount;
				}
				return 0L;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public GZipStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, leaveOpen: false)
		{
		}

		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, leaveOpen: false)
		{
		}

		public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			_baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.GZIP, leaveOpen);
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!_disposed)
				{
					if (disposing && _baseStream != null)
					{
						_baseStream.Close();
						_Crc32 = _baseStream.Crc32;
					}
					_disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		public override void Flush()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			_baseStream.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			int result = _baseStream.Read(buffer, offset, count);
			if (!_firstReadDone)
			{
				_firstReadDone = true;
				FileName = _baseStream._GzipFileName;
				Comment = _baseStream._GzipComment;
			}
			return result;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			if (_baseStream._streamMode == ZlibBaseStream.StreamMode.Undefined)
			{
				if (!_baseStream._wantCompress)
				{
					throw new InvalidOperationException();
				}
				_headerByteCount = EmitHeader();
			}
			_baseStream.Write(buffer, offset, count);
		}

		private int EmitHeader()
		{
			byte[] array = ((Comment == null) ? null : iso8859dash1.GetBytes(Comment));
			byte[] array2 = ((FileName == null) ? null : iso8859dash1.GetBytes(FileName));
			int num = ((Comment != null) ? (array.Length + 1) : 0);
			int num2 = ((FileName != null) ? (array2.Length + 1) : 0);
			int num3 = 10 + num + num2;
			byte[] array3 = new byte[num3];
			int num4 = 0;
			array3[num4++] = 31;
			array3[num4++] = 139;
			array3[num4++] = 8;
			byte b = 0;
			if (Comment != null)
			{
				b ^= 0x10;
			}
			if (FileName != null)
			{
				b ^= 8;
			}
			array3[num4++] = b;
			if (!LastModified.HasValue)
			{
				LastModified = DateTime.Now;
			}
			int value = (int)(LastModified.Value - _unixEpoch).TotalSeconds;
			Array.Copy(BitConverter.GetBytes(value), 0, array3, num4, 4);
			num4 += 4;
			array3[num4++] = 0;
			array3[num4++] = byte.MaxValue;
			if (num2 != 0)
			{
				Array.Copy(array2, 0, array3, num4, num2 - 1);
				num4 += num2 - 1;
				array3[num4++] = 0;
			}
			if (num != 0)
			{
				Array.Copy(array, 0, array3, num4, num - 1);
				num4 += num - 1;
				array3[num4++] = 0;
			}
			_baseStream._stream.Write(array3, 0, array3.Length);
			return array3.Length;
		}

		public static byte[] CompressString(string s)
		{
			using MemoryStream memoryStream = new MemoryStream();
			Stream compressor = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
			ZlibBaseStream.CompressString(s, compressor);
			return memoryStream.ToArray();
		}

		public static byte[] CompressBuffer(byte[] b)
		{
			using MemoryStream memoryStream = new MemoryStream();
			Stream compressor = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
			ZlibBaseStream.CompressBuffer(b, compressor);
			return memoryStream.ToArray();
		}

		public static string UncompressString(byte[] compressed)
		{
			using MemoryStream stream = new MemoryStream(compressed);
			Stream decompressor = new GZipStream(stream, CompressionMode.Decompress);
			return ZlibBaseStream.UncompressString(compressed, decompressor);
		}

		public static byte[] UncompressBuffer(byte[] compressed)
		{
			using MemoryStream stream = new MemoryStream(compressed);
			Stream decompressor = new GZipStream(stream, CompressionMode.Decompress);
			return ZlibBaseStream.UncompressBuffer(compressed, decompressor);
		}
	}
	internal sealed class InflateBlocks
	{
		private enum InflateBlockMode
		{
			TYPE,
			LENS,
			STORED,
			TABLE,
			BTREE,
			DTREE,
			CODES,
			DRY,
			DONE,
			BAD
		}

		private const int MANY = 1440;

		internal static readonly int[] border = new int[19]
		{
			16, 17, 18, 0, 8, 7, 9, 6, 10, 5,
			11, 4, 12, 3, 13, 2, 14, 1, 15
		};

		private InflateBlockMode mode;

		internal int left;

		internal int table;

		internal int index;

		internal int[] blens;

		internal int[] bb = new int[1];

		internal int[] tb = new int[1];

		internal InflateCodes codes = new InflateCodes();

		internal int last;

		internal ZlibCodec _codec;

		internal int bitk;

		internal int bitb;

		internal int[] hufts;

		internal byte[] window;

		internal int end;

		internal int readAt;

		internal int writeAt;

		internal object checkfn;

		internal uint check;

		internal InfTree inftree = new InfTree();

		internal InflateBlocks(ZlibCodec codec, object checkfn, int w)
		{
			_codec = codec;
			hufts = new int[4320];
			window = new byte[w];
			end = w;
			this.checkfn = checkfn;
			mode = InflateBlockMode.TYPE;
			Reset();
		}

		internal uint Reset()
		{
			uint result = check;
			mode = InflateBlockMode.TYPE;
			bitk = 0;
			bitb = 0;
			readAt = (writeAt = 0);
			if (checkfn != null)
			{
				_codec._Adler32 = (check = Adler.Adler32(0u, null, 0, 0));
			}
			return result;
		}

		internal int Process(int r)
		{
			int num = _codec.NextIn;
			int num2 = _codec.AvailableBytesIn;
			int num3 = bitb;
			int i = bitk;
			int num4 = writeAt;
			int num5 = ((num4 < readAt) ? (readAt - num4 - 1) : (end - num4));
			while (true)
			{
				switch (mode)
				{
				case InflateBlockMode.TYPE:
				{
					for (; i < 3; i += 8)
					{
						if (num2 != 0)
						{
							r = 0;
							num2--;
							num3 |= (_codec.InputBuffer[num++] & 0xFF) << i;
							continue;
						}
						bitb = num3;
						bitk = i;
						_codec.AvailableBytesIn = num2;
						_codec.TotalBytesIn += num - _codec.NextIn;
						_codec.NextIn = num;
						writeAt = num4;
						return Flush(r);
					}
					int num6 = num3 & 7;
					last = num6 & 1;
					switch ((uint)(num6 >>> 1))
					{
					case 0u:
						num3 >>= 3;
						i -= 3;
						num6 = i & 7;
						num3 >>= num6;
						i -= num6;
						mode = InflateBlockMode.LENS;
						break;
					case 1u:
					{
						int[] array = new int[1];
						int[] array2 = new int[1];
						int[][] array3 = new int[1][];
						int[][] array4 = new int[1][];
						InfTree.inflate_trees_fixed(array, array2, array3, array4, _codec);
						codes.Init(array[0], array2[0], array3[0], 0, array4[0], 0);
						num3 >>= 3;
						i -= 3;
						mode = InflateBlockMode.CODES;
						break;
					}
					case 2u:
						num3 >>= 3;
						i -= 3;
						mode = InflateBlockMode.TABLE;
						break;
					case 3u:
						num3 >>= 3;
						i -= 3;
						mode = InflateBlockMode.BAD;
						_codec.Message = "invalid block type";
						r = -3;
						bitb = num3;
						bitk = i;
						_codec.AvailableBytesIn = num2;
						_codec.TotalBytesIn += num - _codec.NextIn;
						_codec.NextIn = num;
						writeAt = num4;
						return Flush(r);
					}
					break;
				}
				case InflateBlockMode.LENS:
					for (; i < 32; i += 8)
					{
						if (num2 != 0)
						{
							r = 0;
							num2--;
							num3 |= (_codec.InputBuffer[num++] & 0xFF) << i;
							continue;
						}
						bitb = num3;
						bitk = i;
						_codec.AvailableBytesIn = num2;
						_codec.TotalBytesIn += num - _codec.NextIn;
						_codec.NextIn = num;
						writeAt = num4;
						return Flush(r);
					}
					if (((~num3 >> 16) & 0xFFFF) != (num3 & 0xFFFF))
					{
						mode = InflateBlockMode.BAD;
						_codec.Message = "invalid stored block lengths";
						r = -3;
						bitb = num3;
						bitk = i;
						_codec.AvailableBytesIn = num2;
						_codec.TotalBytesIn += num - _codec.NextIn;
						_codec.NextIn = num;
						writeAt = num4;
						return Flush(r);
					}
					left = num3 & 0xFFFF;
					num3 = (i = 0);
					mode = ((left != 0) ? InflateBlockMode.STORED : ((last != 0) ? InflateBlockMode.DRY : InflateBlockMode.TYPE));
					break;
				case InflateBlockMode.STORED:
				{
					if (num2 == 0)
					{
						bitb = num3;
						bitk = i;
						_codec.AvailableBytesIn = num2;
						_codec.TotalBytesIn += num - _codec.NextIn;
						_codec.NextIn = num;
						writeAt = num4;
						return Flush(r);
					}
					if (num5 == 0)
					{
						if (num4 == end && readAt != 0)
						{
							num4 = 0;
							num5 = ((num4 < readAt) ? (readAt - num4 - 1) : (end - num4));
						}
						if (num5 == 0)
						{
							writeAt = num4;
							r = Flush(r);
							num4 = writeAt;
							num5 = ((num4 < readAt) ? (readAt - num4 - 1) : (end - num4));
							if (num4 == end && readAt != 0)
							{
								num4 = 0;
								num5 = ((num4 < readAt) ? (readAt - num4 - 1) : (end - num4));
							}
							if (num5 == 0)
							{
								bitb = num3;
								bitk = i;
								_codec.AvailableBytesIn = num2;
								_codec.TotalBytesIn += num - _codec.NextIn;
								_codec.NextIn = num;
								writeAt = num4;
								return Flush(r);
							}
						}
					}
					r = 0;
					int num6 = left;
					if (num6 > num2)
					{
						num6 = num2;
					}
					if (num6 > num5)
					{
						num6 = num5;
					}
					Array.Copy(_codec.InputBuffer, num, window, num4, num6);
					num += num6;
					num2 -= num6;
					num4 += num6;
					num5 -= num6;
					if ((left -= num6) == 0)
					{
						mode = ((last != 0) ? InflateBlockMode.DRY : InflateBlockMode.TYPE);
					}
					break;
				}
				case InflateBlockMode.TABLE:
				{
					for (; i < 14; i += 8)
					{
						if (num2 != 0)
						{
							r = 0;
							num2--;
							num3 |= (_codec.InputBuffer[num++] & 0xFF) << i;
							continue;
						}
						bitb = num3;
						bitk = i;
						_codec.AvailableBytesIn = num2;
						_codec.TotalBytesIn += num - _codec.NextIn;
						_codec.NextIn = num;
						writeAt = num4;
						return Flush(r);
					}
					int num6 = (table = num3 & 0x3FFF);
					if ((num6 & 0x1F) > 29 || ((num6 >> 5) & 0x1F) > 29)
					{
						mode = InflateBlockMode.BAD;
						_codec.Message = "too many length or distance symbols";
						r = -3;
						bitb = num3;
						bitk = i;
						_codec.AvailableBytesIn = num2;
						_codec.TotalBytesIn += num - _codec.NextIn;
						_codec.NextIn = num;
						writeAt = num4;
						return Flush(r);
					}
					num6 = 258 + (num6 & 0x1F) + ((num6 >> 5) & 0x1F);
					if (blens == null || blens.Length < num6)
					{
						blens = new int[num6];
					}
					else
					{
						Array.Clear(blens, 0, num6);
					}
					num3 >>= 14;
					i -= 14;
					index = 0;
					mode = InflateBlockMode.BTREE;
					goto case InflateBlockMode.BTREE;
				}
				case InflateBlockMode.BTREE:
				{
					while (index < 4 + (table >> 10))
					{
						for (; i < 3; i += 8)
						{
							if (num2 != 0)
							{
								r = 0;
								num2--;
								num3 |= (_codec.InputBuffer[num++] & 0xFF) << i;
								continue;
							}
							bitb = num3;
							bitk = i;
							_codec.AvailableBytesIn = num2;
							_codec.TotalBytesIn += num - _codec.NextIn;
							_codec.NextIn = num;
							writeAt = num4;
							return Flush(r);
						}
						blens[border[index++]] = num3 & 7;
						num3 >>= 3;
						i -= 3;
					}
					while (index < 19)
					{
						blens[border[index++]] = 0;
					}
					bb[0] = 7;
					int num6 = inftree.inflate_trees_bits(blens, bb, tb, hufts, _codec);
					if (num6 != 0)
					{
						r = num6;
						if (r == -3)
						{
							blens = null;
							mode = InflateBlockMode.BAD;
						}
						bitb = num3;
						bitk = i;
						_codec.AvailableBytesIn = num2;
						_codec.TotalBytesIn += num - _codec.NextIn;
						_codec.NextIn = num;
						writeAt = num4;
						return Flush(r);
					}
					index = 0;
					mode = InflateBlockMode.DTREE;
					goto case InflateBlockMode.DTREE;
				}
				case InflateBlockMode.DTREE:
				{
					int num6;
					while (true)
					{
						num6 = table;
						if (index >= 258 + (num6 & 0x1F) + ((num6 >> 5) & 0x1F))
						{
							break;
						}
						for (num6 = bb[0]; i < num6; i += 8)
						{
							if (num2 != 0)
							{
								r = 0;
								num2--;
								num3 |= (_codec.InputBuffer[num++] & 0xFF) << i;
								continue;
							}
							bitb = num3;
							bitk = i;
							_codec.AvailableBytesIn = num2;
							_codec.TotalBytesIn += num - _codec.NextIn;
							_codec.NextIn = num;
							writeAt = num4;
							return Flush(r);
						}
						num6 = hufts[(tb[0] + (num3 & InternalInflateConstants.InflateMask[num6])) * 3 + 1];
						int num7 = hufts[(tb[0] + (num3 & InternalInflateConstants.InflateMask[num6])) * 3 + 2];
						if (num7 < 16)
						{
							num3 >>= num6;
							i -= num6;
							blens[index++] = num7;
							continue;
						}
						int num8 = ((num7 == 18) ? 7 : (num7 - 14));
						int num9 = ((num7 == 18) ? 11 : 3);
						for (; i < num6 + num8; i += 8)
						{
							if (num2 != 0)
							{
								r = 0;
								num2--;
								num3 |= (_codec.InputBuffer[num++] & 0xFF) << i;
								continue;
							}
							bitb = num3;
							bitk = i;
							_codec.AvailableBytesIn = num2;
							_codec.TotalBytesIn += num - _codec.NextIn;
							_codec.NextIn = num;
							writeAt = num4;
							return Flush(r);
						}
						num3 >>= num6;
						i -= num6;
						num9 += num3 & InternalInflateConstants.InflateMask[num8];
						num3 >>= num8;
						i -= num8;
						num8 = index;
						num6 = table;
						if (num8 + num9 > 258 + (num6 & 0x1F) + ((num6 >> 5) & 0x1F) || (num7 == 16 && num8 < 1))
						{
							blens = null;
							mode = InflateBlockMode.BAD;
							_codec.Message = "invalid bit length repeat";
							r = -3;
							bitb = num3;
							bitk = i;
							_codec.AvailableBytesIn = num2;
							_codec.TotalBytesIn += num - _codec.NextIn;
							_codec.NextIn = num;
							writeAt = num4;
							return Flush(r);
						}
						num7 = ((num7 == 16) ? blens[num8 - 1] : 0);
						do
						{
							blens[num8++] = num7;
						}
						while (--num9 != 0);
						index = num8;
					}
					tb[0] = -1;
					int[] array5 = new int[1] { 9 };
					int[] array6 = new int[1] { 6 };
					int[] array7 = new int[1];
					int[] array8 = new int[1];
					num6 = table;
					num6 = inftree.inflate_trees_dynamic(257 + (num6 & 0x1F), 1 + ((num6 >> 5) & 0x1F), blens, array5, array6, array7, array8, hufts, _codec);
					if (num6 != 0)
					{
						if (num6 == -3)
						{
							blens = null;
							mode = InflateBlockMode.BAD;
						}
						r = num6;
						bitb = num3;
						bitk = i;
						_codec.AvailableBytesIn = num2;
						_codec.TotalBytesIn += num - _codec.NextIn;
						_codec.NextIn = num;
						writeAt = num4;
						return Flush(r);
					}
					codes.Init(array5[0], array6[0], hufts, array7[0], hufts, array8[0]);
					mode = InflateBlockMode.CODES;
					goto case InflateBlockMode.CODES;
				}
				case InflateBlockMode.CODES:
					bitb = num3;
					bitk = i;
					_codec.AvailableBytesIn = num2;
					_codec.TotalBytesIn += num - _codec.NextIn;
					_codec.NextIn = num;
					writeAt = num4;
					r = codes.Process(this, r);
					if (r != 1)
					{
						return Flush(r);
					}
					r = 0;
					num = _codec.NextIn;
					num2 = _codec.AvailableBytesIn;
					num3 = bitb;
					i = bitk;
					num4 = writeAt;
					num5 = ((num4 < readAt) ? (readAt - num4 - 1) : (end - num4));
					if (last == 0)
					{
						mode = InflateBlockMode.TYPE;
						break;
					}
					mode = InflateBlockMode.DRY;
					goto case InflateBlockMode.DRY;
				case InflateBlockMode.DRY:
					writeAt = num4;
					r = Flush(r);
					num4 = writeAt;
					num5 = ((num4 < readAt) ? (readAt - num4 - 1) : (end - num4));
					if (readAt != writeAt)
					{
						bitb = num3;
						bitk = i;
						_codec.AvailableBytesIn = num2;
						_codec.TotalBytesIn += num - _codec.NextIn;
						_codec.NextIn = num;
						writeAt = num4;
						return Flush(r);
					}
					mode = InflateBlockMode.DONE;
					goto case InflateBlockMode.DONE;
				case InflateBlockMode.DONE:
					r = 1;
					bitb = num3;
					bitk = i;
					_codec.AvailableBytesIn = num2;
					_codec.TotalBytesIn += num - _codec.NextIn;
					_codec.NextIn = num;
					writeAt = num4;
					return Flush(r);
				case InflateBlockMode.BAD:
					r = -3;
					bitb = num3;
					bitk = i;
					_codec.AvailableBytesIn = num2;
					_codec.TotalBytesIn += num - _codec.NextIn;
					_codec.NextIn = num;
					writeAt = num4;
					return Flush(r);
				default:
					r = -2;
					bitb = num3;
					bitk = i;
					_codec.AvailableBytesIn = num2;
					_codec.TotalBytesIn += num - _codec.NextIn;
					_codec.NextIn = num;
					writeAt = num4;
					return Flush(r);
				}
			}
		}

		internal void Free()
		{
			Reset();
			window = null;
			hufts = null;
		}

		internal void SetDictionary(byte[] d, int start, int n)
		{
			Array.Copy(d, start, window, 0, n);
			readAt = (writeAt = n);
		}

		internal int SyncPoint()
		{
			if (mode != InflateBlockMode.LENS)
			{
				return 0;
			}
			return 1;
		}

		internal int Flush(int r)
		{
			for (int i = 0; i < 2; i++)
			{
				int num = ((i != 0) ? (writeAt - readAt) : (((readAt <= writeAt) ? writeAt : end) - readAt));
				if (num == 0)
				{
					if (r == -5)
					{
						r = 0;
					}
					return r;
				}
				if (num > _codec.AvailableBytesOut)
				{
					num = _codec.AvailableBytesOut;
				}
				if (num != 0 && r == -5)
				{
					r = 0;
				}
				_codec.AvailableBytesOut -= num;
				_codec.TotalBytesOut += num;
				if (checkfn != null)
				{
					_codec._Adler32 = (check = Adler.Adler32(check, window, readAt, num));
				}
				Array.Copy(window, readAt, _codec.OutputBuffer, _codec.NextOut, num);
				_codec.NextOut += num;
				readAt += num;
				if (readAt == end && i == 0)
				{
					readAt = 0;
					if (writeAt == end)
					{
						writeAt = 0;
					}
				}
				else
				{
					i++;
				}
			}
			return r;
		}
	}
	internal static class InternalInflateConstants
	{
		internal static readonly int[] InflateMask = new int[17]
		{
			0, 1, 3, 7, 15, 31, 63, 127, 255, 511,
			1023, 2047, 4095, 8191, 16383, 32767, 65535
		};
	}
	internal sealed class InflateCodes
	{
		private const int START = 0;

		private const int LEN = 1;

		private const int LENEXT = 2;

		private const int DIST = 3;

		private const int DISTEXT = 4;

		private const int COPY = 5;

		private const int LIT = 6;

		private const int WASH = 7;

		private const int END = 8;

		private const int BADCODE = 9;

		internal int mode;

		internal int len;

		internal int[] tree;

		internal int tree_index;

		internal int need;

		internal int lit;

		internal int bitsToGet;

		internal int dist;

		internal byte lbits;

		internal byte dbits;

		internal int[] ltree;

		internal int ltree_index;

		internal int[] dtree;

		internal int dtree_index;

		internal InflateCodes()
		{
		}

		internal void Init(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index)
		{
			mode = 0;
			lbits = (byte)bl;
			dbits = (byte)bd;
			ltree = tl;
			ltree_index = tl_index;
			dtree = td;
			dtree_index = td_index;
			tree = null;
		}

		internal int Process(InflateBlocks blocks, int r)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			ZlibCodec codec = blocks._codec;
			num3 = codec.NextIn;
			int num4 = codec.AvailableBytesIn;
			num = blocks.bitb;
			num2 = blocks.bitk;
			int num5 = blocks.writeAt;
			int num6 = ((num5 < blocks.readAt) ? (blocks.readAt - num5 - 1) : (blocks.end - num5));
			while (true)
			{
				switch (mode)
				{
				case 0:
					if (num6 >= 258 && num4 >= 10)
					{
						blocks.bitb = num;
						blocks.bitk = num2;
						codec.AvailableBytesIn = num4;
						codec.TotalBytesIn += num3 - codec.NextIn;
						codec.NextIn = num3;
						blocks.writeAt = num5;
						r = InflateFast(lbits, dbits, ltree, ltree_index, dtree, dtree_index, blocks, codec);
						num3 = codec.NextIn;
						num4 = codec.AvailableBytesIn;
						num = blocks.bitb;
						num2 = blocks.bitk;
						num5 = blocks.writeAt;
						num6 = ((num5 < blocks.readAt) ? (blocks.readAt - num5 - 1) : (blocks.end - num5));
						if (r != 0)
						{
							mode = ((r == 1) ? 7 : 9);
							break;
						}
					}
					need = lbits;
					tree = ltree;
					tree_index = ltree_index;
					mode = 1;
					goto case 1;
				case 1:
				{
					int num7;
					for (num7 = need; num2 < num7; num2 += 8)
					{
						if (num4 != 0)
						{
							r = 0;
							num4--;
							num |= (codec.InputBuffer[num3++] & 0xFF) << num2;
							continue;
						}
						blocks.bitb = num;
						blocks.bitk = num2;
						codec.AvailableBytesIn = num4;
						codec.TotalBytesIn += num3 - codec.NextIn;
						codec.NextIn = num3;
						blocks.writeAt = num5;
						return blocks.Flush(r);
					}
					int num8 = (tree_index + (num & InternalInflateConstants.InflateMask[num7])) * 3;
					num >>= tree[num8 + 1];
					num2 -= tree[num8 + 1];
					int num9 = tree[num8];
					if (num9 == 0)
					{
						lit = tree[num8 + 2];
						mode = 6;
						break;
					}
					if ((num9 & 0x10) != 0)
					{
						bitsToGet = num9 & 0xF;
						len = tree[num8 + 2];
						mode = 2;
						break;
					}
					if ((num9 & 0x40) == 0)
					{
						need = num9;
						tree_index = num8 / 3 + tree[num8 + 2];
						break;
					}
					if ((num9 & 0x20) != 0)
					{
						mode = 7;
						break;
					}
					mode = 9;
					codec.Message = "invalid literal/length code";
					r = -3;
					blocks.bitb = num;
					blocks.bitk = num2;
					codec.AvailableBytesIn = num4;
					codec.TotalBytesIn += num3 - codec.NextIn;
					codec.NextIn = num3;
					blocks.writeAt = num5;
					return blocks.Flush(r);
				}
				case 2:
				{
					int num7;
					for (num7 = bitsToGet; num2 < num7; num2 += 8)
					{
						if (num4 != 0)
						{
							r = 0;
							num4--;
							num |= (codec.InputBuffer[num3++] & 0xFF) << num2;
							continue;
						}
						blocks.bitb = num;
						blocks.bitk = num2;
						codec.AvailableBytesIn = num4;
						codec.TotalBytesIn += num3 - codec.NextIn;
						codec.NextIn = num3;
						blocks.writeAt = num5;
						return blocks.Flush(r);
					}
					len += num & InternalInflateConstants.InflateMask[num7];
					num >>= num7;
					num2 -= num7;
					need = dbits;
					tree = dtree;
					tree_index = dtree_index;
					mode = 3;
					goto case 3;
				}
				case 3:
				{
					int num7;
					for (num7 = need; num2 < num7; num2 += 8)
					{
						if (num4 != 0)
						{
							r = 0;
							num4--;
							num |= (codec.InputBuffer[num3++] & 0xFF) << num2;
							continue;
						}
						blocks.bitb = num;
						blocks.bitk = num2;
						codec.AvailableBytesIn = num4;
						codec.TotalBytesIn += num3 - codec.NextIn;
						codec.NextIn = num3;
						blocks.writeAt = num5;
						return blocks.Flush(r);
					}
					int num8 = (tree_index + (num & InternalInflateConstants.InflateMask[num7])) * 3;
					num >>= tree[num8 + 1];
					num2 -= tree[num8 + 1];
					int num9 = tree[num8];
					if ((num9 & 0x10) != 0)
					{
						bitsToGet = num9 & 0xF;
						dist = tree[num8 + 2];
						mode = 4;
						break;
					}
					if ((num9 & 0x40) == 0)
					{
						need = num9;
						tree_index = num8 / 3 + tree[num8 + 2];
						break;
					}
					mode = 9;
					codec.Message = "invalid distance code";
					r = -3;
					blocks.bitb = num;
					blocks.bitk = num2;
					codec.AvailableBytesIn = num4;
					codec.TotalBytesIn += num3 - codec.NextIn;
					codec.NextIn = num3;
					blocks.writeAt = num5;
					return blocks.Flush(r);
				}
				case 4:
				{
					int num7;
					for (num7 = bitsToGet; num2 < num7; num2 += 8)
					{
						if (num4 != 0)
						{
							r = 0;
							num4--;
							num |= (codec.InputBuffer[num3++] & 0xFF) << num2;
							continue;
						}
						blocks.bitb = num;
						blocks.bitk = num2;
						codec.AvailableBytesIn = num4;
						codec.TotalBytesIn += num3 - codec.NextIn;
						codec.NextIn = num3;
						blocks.writeAt = num5;
						return blocks.Flush(r);
					}
					dist += num & InternalInflateConstants.InflateMask[num7];
					num >>= num7;
					num2 -= num7;
					mode = 5;
					goto case 5;
				}
				case 5:
				{
					int i;
					for (i = num5 - dist; i < 0; i += blocks.end)
					{
					}
					while (len != 0)
					{
						if (num6 == 0)
						{
							if (num5 == blocks.end && blocks.readAt != 0)
							{
								num5 = 0;
								num6 = ((num5 < blocks.readAt) ? (blocks.readAt - num5 - 1) : (blocks.end - num5));
							}
							if (num6 == 0)
							{
								blocks.writeAt = num5;
								r = blocks.Flush(r);
								num5 = blocks.writeAt;
								num6 = ((num5 < blocks.readAt) ? (blocks.readAt - num5 - 1) : (blocks.end - num5));
								if (num5 == blocks.end && blocks.readAt != 0)
								{
									num5 = 0;
									num6 = ((num5 < blocks.readAt) ? (blocks.readAt - num5 - 1) : (blocks.end - num5));
								}
								if (num6 == 0)
								{
									blocks.bitb = num;
									blocks.bitk = num2;
									codec.AvailableBytesIn = num4;
									codec.TotalBytesIn += num3 - codec.NextIn;
									codec.NextIn = num3;
									blocks.writeAt = num5;
									return blocks.Flush(r);
								}
							}
						}
						blocks.window[num5++] = blocks.window[i++];
						num6--;
						if (i == blocks.end)
						{
							i = 0;
						}
						len--;
					}
					mode = 0;
					break;
				}
				case 6:
					if (num6 == 0)
					{
						if (num5 == blocks.end && blocks.readAt != 0)
						{
							num5 = 0;
							num6 = ((num5 < blocks.readAt) ? (blocks.readAt - num5 - 1) : (blocks.end - num5));
						}
						if (num6 == 0)
						{
							blocks.writeAt = num5;
							r = blocks.Flush(r);
							num5 = blocks.writeAt;
							num6 = ((num5 < blocks.readAt) ? (blocks.readAt - num5 - 1) : (blocks.end - num5));
							if (num5 == blocks.end && blocks.readAt != 0)
							{
								num5 = 0;
								num6 = ((num5 < blocks.readAt) ? (blocks.readAt - num5 - 1) : (blocks.end - num5));
							}
							if (num6 == 0)
							{
								blocks.bitb = num;
								blocks.bitk = num2;
								codec.AvailableBytesIn = num4;
								codec.TotalBytesIn += num3 - codec.NextIn;
								codec.NextIn = num3;
								blocks.writeAt = num5;
								return blocks.Flush(r);
							}
						}
					}
					r = 0;
					blocks.window[num5++] = (byte)lit;
					num6--;
					mode = 0;
					break;
				case 7:
					if (num2 > 7)
					{
						num2 -= 8;
						num4++;
						num3--;
					}
					blocks.writeAt = num5;
					r = blocks.Flush(r);
					num5 = blocks.writeAt;
					num6 = ((num5 < blocks.readAt) ? (blocks.readAt - num5 - 1) : (blocks.end - num5));
					if (blocks.readAt != blocks.writeAt)
					{
						blocks.bitb = num;
						blocks.bitk = num2;
						codec.AvailableBytesIn = num4;
						codec.TotalBytesIn += num3 - codec.NextIn;
						codec.NextIn = num3;
						blocks.writeAt = num5;
						return blocks.Flush(r);
					}
					mode = 8;
					goto case 8;
				case 8:
					r = 1;
					blocks.bitb = num;
					blocks.bitk = num2;
					codec.AvailableBytesIn = num4;
					codec.TotalBytesIn += num3 - codec.NextIn;
					codec.NextIn = num3;
					blocks.writeAt = num5;
					return blocks.Flush(r);
				case 9:
					r = -3;
					blocks.bitb = num;
					blocks.bitk = num2;
					codec.AvailableBytesIn = num4;
					codec.TotalBytesIn += num3 - codec.NextIn;
					codec.NextIn = num3;
					blocks.writeAt = num5;
					return blocks.Flush(r);
				default:
					r = -2;
					blocks.bitb = num;
					blocks.bitk = num2;
					codec.AvailableBytesIn = num4;
					codec.TotalBytesIn += num3 - codec.NextIn;
					codec.NextIn = num3;
					blocks.writeAt = num5;
					return blocks.Flush(r);
				}
			}
		}

		internal int InflateFast(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index, InflateBlocks s, ZlibCodec z)
		{
			int nextIn = z.NextIn;
			int num = z.AvailableBytesIn;
			int num2 = s.bitb;
			int num3 = s.bitk;
			int num4 = s.writeAt;
			int num5 = ((num4 < s.readAt) ? (s.readAt - num4 - 1) : (s.end - num4));
			int num6 = InternalInflateConstants.InflateMask[bl];
			int num7 = InternalInflateConstants.InflateMask[bd];
			int num12;
			while (true)
			{
				if (num3 < 20)
				{
					num--;
					num2 |= (z.InputBuffer[nextIn++] & 0xFF) << num3;
					num3 += 8;
					continue;
				}
				int num8 = num2 & num6;
				int[] array = tl;
				int num9 = tl_index;
				int num10 = (num9 + num8) * 3;
				int num11;
				if ((num11 = array[num10]) == 0)
				{
					num2 >>= array[num10 + 1];
					num3 -= array[num10 + 1];
					s.window[num4++] = (byte)array[num10 + 2];
					num5--;
				}
				else
				{
					while (true)
					{
						num2 >>= array[num10 + 1];
						num3 -= array[num10 + 1];
						if ((num11 & 0x10) != 0)
						{
							num11 &= 0xF;
							num12 = array[num10 + 2] + (num2 & InternalInflateConstants.InflateMask[num11]);
							num2 >>= num11;
							for (num3 -= num11; num3 < 15; num3 += 8)
							{
								num--;
								num2 |= (z.InputBuffer[nextIn++] & 0xFF) << num3;
							}
							num8 = num2 & num7;
							array = td;
							num9 = td_index;
							num10 = (num9 + num8) * 3;
							num11 = array[num10];
							while (true)
							{
								num2 >>= array[num10 + 1];
								num3 -= array[num10 + 1];
								if ((num11 & 0x10) != 0)
								{
									break;
								}
								if ((num11 & 0x40) == 0)
								{
									num8 += array[num10 + 2];
									num8 += num2 & InternalInflateConstants.InflateMask[num11];
									num10 = (num9 + num8) * 3;
									num11 = array[num10];
									continue;
								}
								z.Message = "invalid distance code";
								num12 = z.AvailableBytesIn - num;
								num12 = ((num3 >> 3 < num12) ? (num3 >> 3) : num12);
								num += num12;
								nextIn -= num12;
								num3 -= num12 << 3;
								s.bitb = num2;
								s.bitk = num3;
								z.AvailableBytesIn = num;
								z.TotalBytesIn += nextIn - z.NextIn;
								z.NextIn = nextIn;
								s.writeAt = num4;
								return -3;
							}
							for (num11 &= 0xF; num3 < num11; num3 += 8)
							{
								num--;
								num2 |= (z.InputBuffer[nextIn++] & 0xFF) << num3;
							}
							int num13 = array[num10 + 2] + (num2 & InternalInflateConstants.InflateMask[num11]);
							num2 >>= num11;
							num3 -= num11;
							num5 -= num12;
							int num14;
							if (num4 >= num13)
							{
								num14 = num4 - num13;
								if (num4 - num14 > 0 && 2 > num4 - num14)
								{
									s.window[num4++] = s.window[num14++];
									s.window[num4++] = s.window[num14++];
									num12 -= 2;
								}
								else
								{
									Array.Copy(s.window, num14, s.window, num4, 2);
									num4 += 2;
									num14 += 2;
									num12 -= 2;
								}
							}
							else
							{
								num14 = num4 - num13;
								do
								{
									num14 += s.end;
								}
								while (num14 < 0);
								num11 = s.end - num14;
								if (num12 > num11)
								{
									num12 -= num11;
									if (num4 - num14 > 0 && num11 > num4 - num14)
									{
										do
										{
											s.window[num4++] = s.window[num14++];
										}
										while (--num11 != 0);
									}
									else
									{
										Array.Copy(s.window, num14, s.window, num4, num11);
										num4 += num11;
										num14 += num11;
										num11 = 0;
									}
									num14 = 0;
								}
							}
							if (num4 - num14 > 0 && num12 > num4 - num14)
							{
								do
								{
									s.window[num4++] = s.window[num14++];
								}
								while (--num12 != 0);
								break;
							}
							Array.Copy(s.window, num14, s.window, num4, num12);
							num4 += num12;
							num14 += num12;
							num12 = 0;
							break;
						}
						if ((num11 & 0x40) == 0)
						{
							num8 += array[num10 + 2];
							num8 += num2 & InternalInflateConstants.InflateMask[num11];
							num10 = (num9 + num8) * 3;
							if ((num11 = array[num10]) == 0)
							{
								num2 >>= array[num10 + 1];
								num3 -= array[num10 + 1];
								s.window[num4++] = (byte)array[num10 + 2];
								num5--;
								break;
							}
							continue;
						}
						if ((num11 & 0x20) != 0)
						{
							num12 = z.AvailableBytesIn - num;
							num12 = ((num3 >> 3 < num12) ? (num3 >> 3) : num12);
							num += num12;
							nextIn -= num12;
							num3 -= num12 << 3;
							s.bitb = num2;
							s.bitk = num3;
							z.AvailableBytesIn = num;
							z.TotalBytesIn += nextIn - z.NextIn;
							z.NextIn = nextIn;
							s.writeAt = num4;
							return 1;
						}
						z.Message = "invalid literal/length code";
						num12 = z.AvailableBytesIn - num;
						num12 = ((num3 >> 3 < num12) ? (num3 >> 3) : num12);
						num += num12;
						nextIn -= num12;
						num3 -= num12 << 3;
						s.bitb = num2;
						s.bitk = num3;
						z.AvailableBytesIn = num;
						z.TotalBytesIn += nextIn - z.NextIn;
						z.NextIn = nextIn;
						s.writeAt = num4;
						return -3;
					}
				}
				if (num5 < 258 || num < 10)
				{
					break;
				}
			}
			num12 = z.AvailableBytesIn - num;
			num12 = ((num3 >> 3 < num12) ? (num3 >> 3) : num12);
			num += num12;
			nextIn -= num12;
			num3 -= num12 << 3;
			s.bitb = num2;
			s.bitk = num3;
			z.AvailableBytesIn = num;
			z.TotalBytesIn += nextIn - z.NextIn;
			z.NextIn = nextIn;
			s.writeAt = num4;
			return 0;
		}
	}
	internal sealed class InflateManager
	{
		private enum InflateManagerMode
		{
			METHOD,
			FLAG,
			DICT4,
			DICT3,
			DICT2,
			DICT1,
			DICT0,
			BLOCKS,
			CHECK4,
			CHECK3,
			CHECK2,
			CHECK1,
			DONE,
			BAD
		}

		private const int PRESET_DICT = 32;

		private const int Z_DEFLATED = 8;

		private InflateManagerMode mode;

		internal ZlibCodec _codec;

		internal int method;

		internal uint computedCheck;

		internal uint expectedCheck;

		internal int marker;

		private bool _handleRfc1950HeaderBytes = true;

		internal int wbits;

		internal InflateBlocks blocks;

		private static readonly byte[] mark = new byte[4] { 0, 0, 255, 255 };

		internal bool HandleRfc1950HeaderBytes
		{
			get
			{
				return _handleRfc1950HeaderBytes;
			}
			set
			{
				_handleRfc1950HeaderBytes = value;
			}
		}

		public InflateManager()
		{
		}

		public InflateManager(bool expectRfc1950HeaderBytes)
		{
			_handleRfc1950HeaderBytes = expectRfc1950HeaderBytes;
		}

		internal int Reset()
		{
			_codec.TotalBytesIn = (_codec.TotalBytesOut = 0L);
			_codec.Message = null;
			mode = ((!HandleRfc1950HeaderBytes) ? InflateManagerMode.BLOCKS : InflateManagerMode.METHOD);
			blocks.Reset();
			return 0;
		}

		internal int End()
		{
			if (blocks != null)
			{
				blocks.Free();
			}
			blocks = null;
			return 0;
		}

		internal int Initialize(ZlibCodec codec, int w)
		{
			_codec = codec;
			_codec.Message = null;
			blocks = null;
			if (w < 8 || w > 15)
			{
				End();
				throw new ZlibException("Bad window size.");
			}
			wbits = w;
			blocks = new InflateBlocks(codec, HandleRfc1950HeaderBytes ? this : null, 1 << w);
			Reset();
			return 0;
		}

		internal int Inflate(FlushType flush)
		{
			if (_codec.InputBuffer == null)
			{
				throw new ZlibException("InputBuffer is null. ");
			}
			int num = 0;
			int num2 = -5;
			while (true)
			{
				switch (mode)
				{
				case InflateManagerMode.METHOD:
					if (_codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					_codec.AvailableBytesIn--;
					_codec.TotalBytesIn++;
					if (((method = _codec.InputBuffer[_codec.NextIn++]) & 0xF) != 8)
					{
						mode = InflateManagerMode.BAD;
						_codec.Message = $"unknown compression method (0x{method:X2})";
						marker = 5;
					}
					else if ((method >> 4) + 8 > wbits)
					{
						mode = InflateManagerMode.BAD;
						_codec.Message = $"invalid window size ({(method >> 4) + 8})";
						marker = 5;
					}
					else
					{
						mode = InflateManagerMode.FLAG;
					}
					break;
				case InflateManagerMode.FLAG:
				{
					if (_codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					_codec.AvailableBytesIn--;
					_codec.TotalBytesIn++;
					int num3 = _codec.InputBuffer[_codec.NextIn++] & 0xFF;
					if (((method << 8) + num3) % 31 != 0)
					{
						mode = InflateManagerMode.BAD;
						_codec.Message = "incorrect header check";
						marker = 5;
					}
					else
					{
						mode = (((num3 & 0x20) == 0) ? InflateManagerMode.BLOCKS : InflateManagerMode.DICT4);
					}
					break;
				}
				case InflateManagerMode.DICT4:
					if (_codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					_codec.AvailableBytesIn--;
					_codec.TotalBytesIn++;
					expectedCheck = (uint)((_codec.InputBuffer[_codec.NextIn++] << 24) & 0xFF000000u);
					mode = InflateManagerMode.DICT3;
					break;
				case InflateManagerMode.DICT3:
					if (_codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					_codec.AvailableBytesIn--;
					_codec.TotalBytesIn++;
					expectedCheck += (uint)((_codec.InputBuffer[_codec.NextIn++] << 16) & 0xFF0000);
					mode = InflateManagerMode.DICT2;
					break;
				case InflateManagerMode.DICT2:
					if (_codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					_codec.AvailableBytesIn--;
					_codec.TotalBytesIn++;
					expectedCheck += (uint)((_codec.InputBuffer[_codec.NextIn++] << 8) & 0xFF00);
					mode = InflateManagerMode.DICT1;
					break;
				case InflateManagerMode.DICT1:
					if (_codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					_codec.AvailableBytesIn--;
					_codec.TotalBytesIn++;
					expectedCheck += (uint)(_codec.InputBuffer[_codec.NextIn++] & 0xFF);
					_codec._Adler32 = expectedCheck;
					mode = InflateManagerMode.DICT0;
					return 2;
				case InflateManagerMode.DICT0:
					mode = InflateManagerMode.BAD;
					_codec.Message = "need dictionary";
					marker = 0;
					return -2;
				case InflateManagerMode.BLOCKS:
					num2 = blocks.Process(num2);
					switch (num2)
					{
					case -3:
						mode = InflateManagerMode.BAD;
						marker = 0;
						goto end_IL_0025;
					case 0:
						num2 = num;
						break;
					}
					if (num2 != 1)
					{
						return num2;
					}
					num2 = num;
					computedCheck = blocks.Reset();
					if (!HandleRfc1950HeaderBytes)
					{
						mode = InflateManagerMode.DONE;
						return 1;
					}
					mode = InflateManagerMode.CHECK4;
					break;
				case InflateManagerMode.CHECK4:
					if (_codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					_codec.AvailableBytesIn--;
					_codec.TotalBytesIn++;
					expectedCheck = (uint)((_codec.InputBuffer[_codec.NextIn++] << 24) & 0xFF000000u);
					mode = InflateManagerMode.CHECK3;
					break;
				case InflateManagerMode.CHECK3:
					if (_codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					_codec.AvailableBytesIn--;
					_codec.TotalBytesIn++;
					expectedCheck += (uint)((_codec.InputBuffer[_codec.NextIn++] << 16) & 0xFF0000);
					mode = InflateManagerMode.CHECK2;
					break;
				case InflateManagerMode.CHECK2:
					if (_codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					_codec.AvailableBytesIn--;
					_codec.TotalBytesIn++;
					expectedCheck += (uint)((_codec.InputBuffer[_codec.NextIn++] << 8) & 0xFF00);
					mode = InflateManagerMode.CHECK1;
					break;
				case InflateManagerMode.CHECK1:
					if (_codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					_codec.AvailableBytesIn--;
					_codec.TotalBytesIn++;
					expectedCheck += (uint)(_codec.InputBuffer[_codec.NextIn++] & 0xFF);
					if (computedCheck != expectedCheck)
					{
						mode = InflateManagerMode.BAD;
						_codec.Message = "incorrect data check";
						marker = 5;
						break;
					}
					mode = InflateManagerMode.DONE;
					return 1;
				case InflateManagerMode.DONE:
					return 1;
				case InflateManagerMode.BAD:
					throw new ZlibException($"Bad state ({_codec.Message})");
				default:
					{
						throw new ZlibException("Stream error.");
					}
					end_IL_0025:
					break;
				}
			}
		}

		internal int SetDictionary(byte[] dictionary)
		{
			int start = 0;
			int num = dictionary.Length;
			if (mode != InflateManagerMode.DICT0)
			{
				throw new ZlibException("Stream error.");
			}
			if (Adler.Adler32(1u, dictionary, 0, dictionary.Length) != _codec._Adler32)
			{
				return -3;
			}
			_codec._Adler32 = Adler.Adler32(0u, null, 0, 0);
			if (num >= 1 << wbits)
			{
				num = (1 << wbits) - 1;
				start = dictionary.Length - num;
			}
			blocks.SetDictionary(dictionary, start, num);
			mode = InflateManagerMode.BLOCKS;
			return 0;
		}

		internal int Sync()
		{
			if (mode != InflateManagerMode.BAD)
			{
				mode = InflateManagerMode.BAD;
				marker = 0;
			}
			int num;
			if ((num = _codec.AvailableBytesIn) == 0)
			{
				return -5;
			}
			int num2 = _codec.NextIn;
			int num3 = marker;
			while (num != 0 && num3 < 4)
			{
				num3 = ((_codec.InputBuffer[num2] != mark[num3]) ? ((_codec.InputBuffer[num2] == 0) ? (4 - num3) : 0) : (num3 + 1));
				num2++;
				num--;
			}
			_codec.TotalBytesIn += num2 - _codec.NextIn;
			_codec.NextIn = num2;
			_codec.AvailableBytesIn = num;
			marker = num3;
			if (num3 != 4)
			{
				return -3;
			}
			long totalBytesIn = _codec.TotalBytesIn;
			long totalBytesOut = _codec.TotalBytesOut;
			Reset();
			_codec.TotalBytesIn = totalBytesIn;
			_codec.TotalBytesOut = totalBytesOut;
			mode = InflateManagerMode.BLOCKS;
			return 0;
		}

		internal int SyncPoint(ZlibCodec z)
		{
			return blocks.SyncPoint();
		}
	}
	internal sealed class InfTree
	{
		private const int MANY = 1440;

		private const int Z_OK = 0;

		private const int Z_STREAM_END = 1;

		private const int Z_NEED_DICT = 2;

		private const int Z_ERRNO = -1;

		private const int Z_STREAM_ERROR = -2;

		private const int Z_DATA_ERROR = -3;

		private const int Z_MEM_ERROR = -4;

		private const int Z_BUF_ERROR = -5;

		private const int Z_VERSION_ERROR = -6;

		internal const int fixed_bl = 9;

		internal const int fixed_bd = 5;

		internal const int BMAX = 15;

		internal static readonly int[] fixed_tl = new int[1536]
		{
			96, 7, 256, 0, 8, 80, 0, 8, 16, 84,
			8, 115, 82, 7, 31, 0, 8, 112, 0, 8,
			48, 0, 9, 192, 80, 7, 10, 0, 8, 96,
			0, 8, 32, 0, 9, 160, 0, 8, 0, 0,
			8, 128, 0, 8, 64, 0, 9, 224, 80, 7,
			6, 0, 8, 88, 0, 8, 24, 0, 9, 144,
			83, 7, 59, 0, 8, 120, 0, 8, 56, 0,
			9, 208, 81, 7, 17, 0, 8, 104, 0, 8,
			40, 0, 9, 176, 0, 8, 8, 0, 8, 136,
			0, 8, 72, 0, 9, 240, 80, 7, 4, 0,
			8, 84, 0, 8, 20, 85, 8, 227, 83, 7,
			43, 0, 8, 116, 0, 8, 52, 0, 9, 200,
			81, 7, 13, 0, 8, 100, 0, 8, 36, 0,
			9, 168, 0, 8, 4, 0, 8, 132, 0, 8,
			68, 0, 9, 232, 80, 7, 8, 0, 8, 92,
			0, 8, 28, 0, 9, 152, 84, 7, 83, 0,
			8, 124, 0, 8, 60, 0, 9, 216, 82, 7,
			23, 0, 8, 108, 0, 8, 44, 0, 9, 184,
			0, 8, 12, 0, 8, 140, 0, 8, 76, 0,
			9, 248, 80, 7, 3, 0, 8, 82, 0, 8,
			18, 85, 8, 163, 83, 7, 35, 0, 8, 114,
			0, 8, 50, 0, 9, 196, 81, 7, 11, 0,
			8, 98, 0, 8, 34, 0, 9, 164, 0, 8,
			2, 0, 8, 130, 0, 8, 66, 0, 9, 228,
			80, 7, 7, 0, 8, 90, 0, 8, 26, 0,
			9, 148, 84, 7, 67, 0, 8, 122, 0, 8,
			58, 0, 9, 212, 82, 7, 19, 0, 8, 106,
			0, 8, 42, 0, 9, 180, 0, 8, 10, 0,
			8, 138, 0, 8, 74, 0, 9, 244, 80, 7,
			5, 0, 8, 86, 0, 8, 22, 192, 8, 0,
			83, 7, 51, 0, 8, 118, 0, 8, 54, 0,
			9, 204, 81, 7, 15, 0, 8, 102, 0, 8,
			38, 0, 9, 172, 0, 8, 6, 0, 8, 134,
			0, 8, 70, 0, 9, 236, 80, 7, 9, 0,
			8, 94, 0, 8, 30, 0, 9, 156, 84, 7,
			99, 0, 8, 126, 0, 8, 62, 0, 9, 220,
			82, 7, 27, 0, 8, 110, 0, 8, 46, 0,
			9, 188, 0, 8, 14, 0, 8, 142, 0, 8,
			78, 0, 9, 252, 96, 7, 256, 0, 8, 81,
			0, 8, 17, 85, 8, 131, 82, 7, 31, 0,
			8, 113, 0, 8, 49, 0, 9, 194, 80, 7,
			10, 0, 8, 97, 0, 8, 33, 0, 9, 162,
			0, 8, 1, 0, 8, 129, 0, 8, 65, 0,
			9, 226, 80, 7, 6, 0, 8, 89, 0, 8,
			25, 0, 9, 146, 83, 7, 59, 0, 8, 121,
			0, 8, 57, 0, 9, 210, 81, 7, 17, 0,
			8, 105, 0, 8, 41, 0, 9, 178, 0, 8,
			9, 0, 8, 137, 0, 8, 73, 0, 9, 242,
			80, 7, 4, 0, 8, 85, 0, 8, 21, 80,
			8, 258, 83, 7, 43, 0, 8, 117, 0, 8,
			53, 0, 9, 202, 81, 7, 13, 0, 8, 101,
			0, 8, 37, 0, 9, 170, 0, 8, 5, 0,
			8, 133, 0, 8, 69, 0, 9, 234, 80, 7,
			8, 0, 8, 93, 0, 8, 29, 0, 9, 154,
			84, 7, 83, 0, 8, 125, 0, 8, 61, 0,
			9, 218, 82, 7, 23, 0, 8, 109, 0, 8,
			45, 0, 9, 186, 0, 8, 13, 0, 8, 141,
			0, 8, 77, 0, 9, 250, 80, 7, 3, 0,
			8, 83, 0, 8, 19, 85, 8, 195, 83, 7,
			35, 0, 8, 115, 0, 8, 51, 0, 9, 198,
			81, 7, 11, 0, 8, 99, 0, 8, 35, 0,
			9, 166, 0, 8, 3, 0, 8, 131, 0, 8,
			67, 0, 9, 230, 80, 7, 7, 0, 8, 91,
			0, 8, 27, 0, 9, 150, 84, 7, 67, 0,
			8, 123, 0, 8, 59, 0, 9, 214, 82, 7,
			19, 0, 8, 107, 0, 8, 43, 0, 9, 182,
			0, 8, 11, 0, 8, 139, 0, 8, 75, 0,
			9, 246, 80, 7, 5, 0, 8, 87, 0, 8,
			23, 192, 8, 0, 83, 7, 51, 0, 8, 119,
			0, 8, 55, 0, 9, 206, 81, 7, 15, 0,
			8, 103, 0, 8, 39, 0, 9, 174, 0, 8,
			7, 0, 8, 135, 0, 8, 71, 0, 9, 238,
			80, 7, 9, 0, 8, 95, 0, 8, 31, 0,
			9, 158, 84, 7, 99, 0, 8, 127, 0, 8,
			63, 0, 9, 222, 82, 7, 27, 0, 8, 111,
			0, 8, 47, 0, 9, 190, 0, 8, 15, 0,
			8, 143, 0, 8, 79, 0, 9, 254, 96, 7,
			256, 0, 8, 80, 0, 8, 16, 84, 8, 115,
			82, 7, 31, 0, 8, 112, 0, 8, 48, 0,
			9, 193, 80, 7, 10, 0, 8, 96, 0, 8,
			32, 0, 9, 161, 0, 8, 0, 0, 8, 128,
			0, 8, 64, 0, 9, 225, 80, 7, 6, 0,
			8, 88, 0, 8, 24, 0, 9, 145, 83, 7,
			59, 0, 8, 120, 0, 8, 56, 0, 9, 209,
			81, 7, 17, 0, 8, 104, 0, 8, 40, 0,
			9, 177, 0, 8, 8, 0, 8, 136, 0, 8,
			72, 0, 9, 241, 80, 7, 4, 0, 8, 84,
			0, 8, 20, 85, 8, 227, 83, 7, 43, 0,
			8, 116, 0, 8, 52, 0, 9, 201, 81, 7,
			13, 0, 8, 100, 0, 8, 36, 0, 9, 169,
			0, 8, 4, 0, 8, 132, 0, 8, 68, 0,
			9, 233, 80, 7, 8, 0, 8, 92, 0, 8,
			28, 0, 9, 153, 84, 7, 83, 0, 8, 124,
			0, 8, 60, 0, 9, 217, 82, 7, 23, 0,
			8, 108, 0, 8, 44, 0, 9, 185, 0, 8,
			12, 0, 8, 140, 0, 8, 76, 0, 9, 249,
			80, 7, 3, 0, 8, 82, 0, 8, 18, 85,
			8, 163, 83, 7, 35, 0, 8, 114, 0, 8,
			50, 0, 9, 197, 81, 7, 11, 0, 8, 98,
			0, 8, 34, 0, 9, 165, 0, 8, 2, 0,
			8, 130, 0, 8, 66, 0, 9, 229, 80, 7,
			7, 0, 8, 90, 0, 8, 26, 0, 9, 149,
			84, 7, 67, 0, 8, 122, 0, 8, 58, 0,
			9, 213, 82, 7, 19, 0, 8, 106, 0, 8,
			42, 0, 9, 181, 0, 8, 10, 0, 8, 138,
			0, 8, 74, 0, 9, 245, 80, 7, 5, 0,
			8, 86, 0, 8, 22, 192, 8, 0, 83, 7,
			51, 0, 8, 118, 0, 8, 54, 0, 9, 205,
			81, 7, 15, 0, 8, 102, 0, 8, 38, 0,
			9, 173, 0, 8, 6, 0, 8, 134, 0, 8,
			70, 0, 9, 237, 80, 7, 9, 0, 8, 94,
			0, 8, 30, 0, 9, 157, 84, 7, 99, 0,
			8, 126, 0, 8, 62, 0, 9, 221, 82, 7,
			27, 0, 8, 110, 0, 8, 46, 0, 9, 189,
			0, 8, 14, 0, 8, 142, 0, 8, 78, 0,
			9, 253, 96, 7, 256, 0, 8, 81, 0, 8,
			17, 85, 8, 131, 82, 7, 31, 0, 8, 113,
			0, 8, 49, 0, 9, 195, 80, 7, 10, 0,
			8, 97, 0, 8, 33, 0, 9, 163, 0, 8,
			1, 0, 8, 129, 0, 8, 65, 0, 9, 227,
			80, 7, 6, 0, 8, 89, 0, 8, 25, 0,
			9, 147, 83, 7, 59, 0, 8, 121, 0, 8,
			57, 0, 9, 211, 81, 7, 17, 0, 8, 105,
			0, 8, 41, 0, 9, 179, 0, 8, 9, 0,
			8, 137, 0, 8, 73, 0, 9, 243, 80, 7,
			4, 0, 8, 85, 0, 8, 21, 80, 8, 258,
			83, 7, 43, 0, 8, 117, 0, 8, 53, 0,
			9, 203, 81, 7, 13, 0, 8, 101, 0, 8,
			37, 0, 9, 171, 0, 8, 5, 0, 8, 133,
			0, 8, 69, 0, 9, 235, 80, 7, 8, 0,
			8, 93, 0, 8, 29, 0, 9, 155, 84, 7,
			83, 0, 8, 125, 0, 8, 61, 0, 9, 219,
			82, 7, 23, 0, 8, 109, 0, 8, 45, 0,
			9, 187, 0, 8, 13, 0, 8, 141, 0, 8,
			77, 0, 9, 251, 80, 7, 3, 0, 8, 83,
			0, 8, 19, 85, 8, 195, 83, 7, 35, 0,
			8, 115, 0, 8, 51, 0, 9, 199, 81, 7,
			11, 0, 8, 99, 0, 8, 35, 0, 9, 167,
			0, 8, 3, 0, 8, 131, 0, 8, 67, 0,
			9, 231, 80, 7, 7, 0, 8, 91, 0, 8,
			27, 0, 9, 151, 84, 7, 67, 0, 8, 123,
			0, 8, 59, 0, 9, 215, 82, 7, 19, 0,
			8, 107, 0, 8, 43, 0, 9, 183, 0, 8,
			11, 0, 8, 139, 0, 8, 75, 0, 9, 247,
			80, 7, 5, 0, 8, 87, 0, 8, 23, 192,
			8, 0, 83, 7, 51, 0, 8, 119, 0, 8,
			55, 0, 9, 207, 81, 7, 15, 0, 8, 103,
			0, 8, 39, 0, 9, 175, 0, 8, 7, 0,
			8, 135, 0, 8, 71, 0, 9, 239, 80, 7,
			9, 0, 8, 95, 0, 8, 31, 0, 9, 159,
			84, 7, 99, 0, 8, 127, 0, 8, 63, 0,
			9, 223, 82, 7, 27, 0, 8, 111, 0, 8,
			47, 0, 9, 191, 0, 8, 15, 0, 8, 143,
			0, 8, 79, 0, 9, 255
		};

		internal static readonly int[] fixed_td = new int[96]
		{
			80, 5, 1, 87, 5, 257, 83, 5, 17, 91,
			5, 4097, 81, 5, 5, 89, 5, 1025, 85, 5,
			65, 93, 5, 16385, 80, 5, 3, 88, 5, 513,
			84, 5, 33, 92, 5, 8193, 82, 5, 9, 90,
			5, 2049, 86, 5, 129, 192, 5, 24577, 80, 5,
			2, 87, 5, 385, 83, 5, 25, 91, 5, 6145,
			81, 5, 7, 89, 5, 1537, 85, 5, 97, 93,
			5, 24577, 80, 5, 4, 88, 5, 769, 84, 5,
			49, 92, 5, 12289, 82, 5, 13, 90, 5, 3073,
			86, 5, 193, 192, 5, 24577
		};

		internal static readonly int[] cplens = new int[31]
		{
			3, 4, 5, 6, 7, 8, 9, 10, 11, 13,
			15, 17, 19, 23, 27, 31, 35, 43, 51, 59,
			67, 83, 99, 115, 131, 163, 195, 227, 258, 0,
			0
		};

		internal static readonly int[] cplext = new int[31]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1, 2, 2, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 4, 5, 5, 5, 5, 0, 112,
			112
		};

		internal static readonly int[] cpdist = new int[30]
		{
			1, 2, 3, 4, 5, 7, 9, 13, 17, 25,
			33, 49, 65, 97, 129, 193, 257, 385, 513, 769,
			1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577
		};

		internal static readonly int[] cpdext = new int[30]
		{
			0, 0, 0, 0, 1, 1, 2, 2, 3, 3,
			4, 4, 5, 5, 6, 6, 7, 7, 8, 8,
			9, 9, 10, 10, 11, 11, 12, 12, 13, 13
		};

		internal int[] hn;

		internal int[] v;

		internal int[] c;

		internal int[] r;

		internal int[] u;

		internal int[] x;

		private int huft_build(int[] b, int bindex, int n, int s, int[] d, int[] e, int[] t, int[] m, int[] hp, int[] hn, int[] v)
		{
			int num = 0;
			int num2 = n;
			do
			{
				c[b[bindex + num]]++;
				num++;
				num2--;
			}
			while (num2 != 0);
			if (c[0] == n)
			{
				t[0] = -1;
				m[0] = 0;
				return 0;
			}
			int num3 = m[0];
			int i;
			for (i = 1; i <= 15 && c[i] == 0; i++)
			{
			}
			int j = i;
			if (num3 < i)
			{
				num3 = i;
			}
			num2 = 15;
			while (num2 != 0 && c[num2] == 0)
			{
				num2--;
			}
			int num4 = num2;
			if (num3 > num2)
			{
				num3 = num2;
			}
			m[0] = num3;
			int num5 = 1 << i;
			while (i < num2)
			{
				if ((num5 -= c[i]) < 0)
				{
					return -3;
				}
				i++;
				num5 <<= 1;
			}
			if ((num5 -= c[num2]) < 0)
			{
				return -3;
			}
			c[num2] += num5;
			i = (x[1] = 0);
			num = 1;
			int num6 = 2;
			while (--num2 != 0)
			{
				i = (x[num6] = i + c[num]);
				num6++;
				num++;
			}
			num2 = 0;
			num = 0;
			do
			{
				if ((i = b[bindex + num]) != 0)
				{
					v[x[i]++] = num2;
				}
				num++;
			}
			while (++num2 < n);
			n = x[num4];
			num2 = (x[0] = 0);
			num = 0;
			int num7 = -1;
			int num8 = -num3;
			u[0] = 0;
			int num9 = 0;
			int num10 = 0;
			for (; j <= num4; j++)
			{
				int num11 = c[j];
				while (num11-- != 0)
				{
					int num12;
					while (j > num8 + num3)
					{
						num7++;
						num8 += num3;
						num10 = num4 - num8;
						num10 = ((num10 > num3) ? num3 : num10);
						if ((num12 = 1 << (i = j - num8)) > num11 + 1)
						{
							num12 -= num11 + 1;
							num6 = j;
							if (i < num10)
							{
								while (++i < num10 && (num12 <<= 1) > c[++num6])
								{
									num12 -= c[num6];
								}
							}
						}
						num10 = 1 << i;
						if (hn[0] + num10 > 1440)
						{
							return -3;
						}
						num9 = (u[num7] = hn[0]);
						hn[0] += num10;
						if (num7 != 0)
						{
							x[num7] = num2;
							r[0] = (sbyte)i;
							r[1] = (sbyte)num3;
							i = SharedUtils.URShift(num2, num8 - num3);
							r[2] = num9 - u[num7 - 1] - i;
							Array.Copy(r, 0, hp, (u[num7 - 1] + i) * 3, 3);
						}
						else
						{
							t[0] = num9;
						}
					}
					r[1] = (sbyte)(j - num8);
					if (num >= n)
					{
						r[0] = 192;
					}
					else if (v[num] < s)
					{
						r[0] = (sbyte)((v[num] >= 256) ? 96 : 0);
						r[2] = v[num++];
					}
					else
					{
						r[0] = (sbyte)(e[v[num] - s] + 16 + 64);
						r[2] = d[v[num++] - s];
					}
					num12 = 1 << j - num8;
					for (i = SharedUtils.URShift(num2, num8); i < num10; i += num12)
					{
						Array.Copy(r, 0, hp, (num9 + i) * 3, 3);
					}
					i = 1 << j - 1;
					while ((num2 & i) != 0)
					{
						num2 ^= i;
						i = SharedUtils.URShift(i, 1);
					}
					num2 ^= i;
					int num13 = (1 << num8) - 1;
					while ((num2 & num13) != x[num7])
					{
						num7--;
						num8 -= num3;
						num13 = (1 << num8) - 1;
					}
				}
			}
			if (num5 == 0 || num4 == 1)
			{
				return 0;
			}
			return -5;
		}

		internal int inflate_trees_bits(int[] c, int[] bb, int[] tb, int[] hp, ZlibCodec z)
		{
			initWorkArea(19);
			hn[0] = 0;
			int num = huft_build(c, 0, 19, 19, null, null, tb, bb, hp, hn, v);
			if (num == -3)
			{
				z.Message = "oversubscribed dynamic bit lengths tree";
			}
			else if (num == -5 || bb[0] == 0)
			{
				z.Message = "incomplete dynamic bit lengths tree";
				num = -3;
			}
			return num;
		}

		internal int inflate_trees_dynamic(int nl, int nd, int[] c, int[] bl, int[] bd, int[] tl, int[] td, int[] hp, ZlibCodec z)
		{
			initWorkArea(288);
			hn[0] = 0;
			int num = huft_build(c, 0, nl, 257, cplens, cplext, tl, bl, hp, hn, v);
			if (num != 0 || bl[0] == 0)
			{
				switch (num)
				{
				case -3:
					z.Message = "oversubscribed literal/length tree";
					break;
				default:
					z.Message = "incomplete literal/length tree";
					num = -3;
					break;
				case -4:
					break;
				}
				return num;
			}
			initWorkArea(288);
			num = huft_build(c, nl, nd, 0, cpdist, cpdext, td, bd, hp, hn, v);
			if (num != 0 || (bd[0] == 0 && nl > 257))
			{
				switch (num)
				{
				case -3:
					z.Message = "oversubscribed distance tree";
					break;
				case -5:
					z.Message = "incomplete distance tree";
					num = -3;
					break;
				default:
					z.Message = "empty distance tree with lengths";
					num = -3;
					break;
				case -4:
					break;
				}
				return num;
			}
			return 0;
		}

		internal static int inflate_trees_fixed(int[] bl, int[] bd, int[][] tl, int[][] td, ZlibCodec z)
		{
			bl[0] = 9;
			bd[0] = 5;
			tl[0] = fixed_tl;
			td[0] = fixed_td;
			return 0;
		}

		private void initWorkArea(int vsize)
		{
			if (hn == null)
			{
				hn = new int[1];
				v = new int[vsize];
				c = new int[16];
				r = new int[3];
				u = new int[15];
				x = new int[16];
				return;
			}
			if (v.Length < vsize)
			{
				v = new int[vsize];
			}
			Array.Clear(v, 0, vsize);
			Array.Clear(c, 0, 16);
			r[0] = 0;
			r[1] = 0;
			r[2] = 0;
			Array.Clear(u, 0, 15);
			Array.Clear(x, 0, 16);
		}
	}
	internal class WorkItem
	{
		public byte[] buffer;

		public byte[] compressed;

		public int crc;

		public int index;

		public int ordinal;

		public int inputBytesAvailable;

		public int compressedBytesAvailable;

		public ZlibCodec compressor;

		public WorkItem(int size, CompressionLevel compressLevel, CompressionStrategy strategy, int ix)
		{
			buffer = new byte[size];
			int num = size + (size / 32768 + 1) * 5 * 2;
			compressed = new byte[num];
			compressor = new ZlibCodec();
			compressor.InitializeDeflate(compressLevel, wantRfc1950Header: false);
			compressor.OutputBuffer = compressed;
			compressor.InputBuffer = buffer;
			index = ix;
		}
	}
	public class ParallelDeflateOutputStream : Stream
	{
		[Flags]
		private enum TraceBits : uint
		{
			None = 0u,
			NotUsed1 = 1u,
			EmitLock = 2u,
			EmitEnter = 4u,
			EmitBegin = 8u,
			EmitDone = 0x10u,
			EmitSkip = 0x20u,
			EmitAll = 0x3Au,
			Flush = 0x40u,
			Lifecycle = 0x80u,
			Session = 0x100u,
			Synch = 0x200u,
			Instance = 0x400u,
			Compress = 0x800u,
			Write = 0x1000u,
			WriteEnter = 0x2000u,
			WriteTake = 0x4000u,
			All = uint.MaxValue
		}

		private static readonly int IO_BUFFER_SIZE_DEFAULT = 65536;

		private static readonly int BufferPairsPerCore = 4;

		private List<WorkItem> _pool;

		private bool _leaveOpen;

		private bool emitting;

		private Stream _outStream;

		private int _maxBufferPairs;

		private int _bufferSize = IO_BUFFER_SIZE_DEFAULT;

		private AutoResetEvent _newlyCompressedBlob;

		private object _outputLock = new object();

		private bool _isClosed;

		private bool _firstWriteDone;

		private int _currentlyFilling;

		private int _lastFilled;

		private int _lastWritten;

		private int _latestCompressed;

		private int _Crc32;

		private CRC32 _runningCrc;

		private object _latestLock = new object();

		private Queue<int> _toWrite;

		private Queue<int> _toFill;

		private long _totalBytesProcessed;

		private CompressionLevel _compressLevel;

		private volatile Exception _pendingException;

		private bool _handlingException;

		private object _eLock = new object();

		private TraceBits _DesiredTrace = TraceBits.EmitAll | TraceBits.EmitEnter | TraceBits.Session | TraceBits.Compress | TraceBits.WriteEnter | TraceBits.WriteTake;

		public CompressionStrategy Strategy { get; private set; }

		public int MaxBufferPairs
		{
			get
			{
				return _maxBufferPairs;
			}
			set
			{
				if (value < 4)
				{
					throw new ArgumentException("MaxBufferPairs", "Value must be 4 or greater.");
				}
				_maxBufferPairs = value;
			}
		}

		public int BufferSize
		{
			get
			{
				return _bufferSize;
			}
			set
			{
				if (value < 1024)
				{
					throw new ArgumentOutOfRangeException("BufferSize", "BufferSize must be greater than 1024 bytes");
				}
				_bufferSize = value;
			}
		}

		public int Crc32 => _Crc32;

		public long BytesProcessed => _totalBytesProcessed;

		public override bool CanSeek => false;

		public override bool CanRead => false;

		public override bool CanWrite => _outStream.CanWrite;

		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public override long Position
		{
			get
			{
				return _outStream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public ParallelDeflateOutputStream(Stream stream)
			: this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen: false)
		{
		}

		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level)
			: this(stream, level, CompressionStrategy.Default, leaveOpen: false)
		{
		}

		public ParallelDeflateOutputStream(Stream stream, bool leaveOpen)
			: this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen)
		{
		}

		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level, bool leaveOpen)
			: this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen)
		{
		}

		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level, CompressionStrategy strategy, bool leaveOpen)
		{
			_outStream = stream;
			_compressLevel = level;
			Strategy = strategy;
			_leaveOpen = leaveOpen;
			MaxBufferPairs = 16;
		}

		private void _InitializePoolOfWorkItems()
		{
			_toWrite = new Queue<int>();
			_toFill = new Queue<int>();
			_pool = new List<WorkItem>();
			int val = BufferPairsPerCore * Environment.ProcessorCount;
			val = Math.Min(val, _maxBufferPairs);
			for (int i = 0; i < val; i++)
			{
				_pool.Add(new WorkItem(_bufferSize, _compressLevel, Strategy, i));
				_toFill.Enqueue(i);
			}
			_newlyCompressedBlob = new AutoResetEvent(initialState: false);
			_runningCrc = new CRC32();
			_currentlyFilling = -1;
			_lastFilled = -1;
			_lastWritten = -1;
			_latestCompressed = -1;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			bool mustWait = false;
			if (_isClosed)
			{
				throw new InvalidOperationException();
			}
			if (_pendingException != null)
			{
				_handlingException = true;
				Exception pendingException = _pendingException;
				_pendingException = null;
				throw pendingException;
			}
			if (count == 0)
			{
				return;
			}
			if (!_firstWriteDone)
			{
				_InitializePoolOfWorkItems();
				_firstWriteDone = true;
			}
			do
			{
				EmitPendingBuffers(doAll: false, mustWait);
				mustWait = false;
				int num = -1;
				if (_currentlyFilling >= 0)
				{
					num = _currentlyFilling;
				}
				else
				{
					if (_toFill.Count == 0)
					{
						mustWait = true;
						continue;
					}
					num = _toFill.Dequeue();
					_lastFilled++;
				}
				WorkItem workItem = _pool[num];
				int num2 = ((workItem.buffer.Length - workItem.inputBytesAvailable > count) ? count : (workItem.buffer.Length - workItem.inputBytesAvailable));
				workItem.ordinal = _lastFilled;
				Buffer.BlockCopy(buffer, offset, workItem.buffer, workItem.inputBytesAvailable, num2);
				count -= num2;
				offset += num2;
				workItem.inputBytesAvailable += num2;
				if (workItem.inputBytesAvailable == workItem.buffer.Length)
				{
					if (!ThreadPool.QueueUserWorkItem(_DeflateOne, workItem))
					{
						throw new Exception("Cannot enqueue workitem");
					}
					_currentlyFilling = -1;
				}
				else
				{
					_currentlyFilling = num;
				}
				_ = 0;
			}
			while (count > 0);
		}

		private void _FlushFinish()
		{
			byte[] array = new byte[128];
			ZlibCodec zlibCodec = new ZlibCodec();
			int num = zlibCodec.InitializeDeflate(_compressLevel, wantRfc1950Header: false);
			zlibCodec.InputBuffer = null;
			zlibCodec.NextIn = 0;
			zlibCodec.AvailableBytesIn = 0;
			zlibCodec.OutputBuffer = array;
			zlibCodec.NextOut = 0;
			zlibCodec.AvailableBytesOut = array.Length;
			num = zlibCodec.Deflate(FlushType.Finish);
			if (num != 1 && num != 0)
			{
				throw new Exception("deflating: " + zlibCodec.Message);
			}
			if (array.Length - zlibCodec.AvailableBytesOut > 0)
			{
				_outStream.Write(array, 0, array.Length - zlibCodec.AvailableBytesOut);
			}
			zlibCodec.EndDeflate();
			_Crc32 = _runningCrc.Crc32Result;
		}

		private void _Flush(bool lastInput)
		{
			if (_isClosed)
			{
				throw new InvalidOperationException();
			}
			if (!emitting)
			{
				if (_currentlyFilling >= 0)
				{
					WorkItem wi = _pool[_currentlyFilling];
					_DeflateOne(wi);
					_currentlyFilling = -1;
				}
				if (lastInput)
				{
					EmitPendingBuffers(doAll: true, mustWait: false);
					_FlushFinish();
				}
				else
				{
					EmitPendingBuffers(doAll: false, mustWait: false);
				}
			}
		}

		public override void Flush()
		{
			if (_pendingException != null)
			{
				_handlingException = true;
				Exception pendingException = _pendingException;
				_pendingException = null;
				throw pendingException;
			}
			if (!_handlingException)
			{
				_Flush(lastInput: false);
			}
		}

		public override void Close()
		{
			if (_pendingException != null)
			{
				_handlingException = true;
				Exception pendingException = _pendingException;
				_pendingException = null;
				throw pendingException;
			}
			if (!_handlingException && !_isClosed)
			{
				_Flush(lastInput: true);
				if (!_leaveOpen)
				{
					_outStream.Close();
				}
				_isClosed = true;
			}
		}

		public new void Dispose()
		{
			Close();
			_pool = null;
			Dispose(disposing: true);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		public void Reset(Stream stream)
		{
			if (!_firstWriteDone)
			{
				return;
			}
			_toWrite.Clear();
			_toFill.Clear();
			foreach (WorkItem item in _pool)
			{
				_toFill.Enqueue(item.index);
				item.ordinal = -1;
			}
			_firstWriteDone = false;
			_totalBytesProcessed = 0L;
			_runningCrc = new CRC32();
			_isClosed = false;
			_currentlyFilling = -1;
			_lastFilled = -1;
			_lastWritten = -1;
			_latestCompressed = -1;
			_outStream = stream;
		}

		private void EmitPendingBuffers(bool doAll, bool mustWait)
		{
			if (emitting)
			{
				return;
			}
			emitting = true;
			if (doAll || mustWait)
			{
				_newlyCompressedBlob.WaitOne();
			}
			do
			{
				int num = -1;
				int num2 = (doAll ? 200 : (mustWait ? (-1) : 0));
				int num3 = -1;
				do
				{
					if (Monitor.TryEnter(_toWrite, num2))
					{
						num3 = -1;
						try
						{
							if (_toWrite.Count > 0)
							{
								num3 = _toWrite.Dequeue();
							}
						}
						finally
						{
							Monitor.Exit(_toWrite);
						}
						if (num3 < 0)
						{
							continue;
						}
						WorkItem workItem = _pool[num3];
						if (workItem.ordinal != _lastWritten + 1)
						{
							lock (_toWrite)
							{
								_toWrite.Enqueue(num3);
							}
							if (num == num3)
							{
								_newlyCompressedBlob.WaitOne();
								num = -1;
							}
							else if (num == -1)
							{
								num = num3;
							}
							continue;
						}
						num = -1;
						_outStream.Write(workItem.compressed, 0, workItem.compressedBytesAvailable);
						_runningCrc.Combine(workItem.crc, workItem.inputBytesAvailable);
						_totalBytesProcessed += workItem.inputBytesAvailable;
						workItem.inputBytesAvailable = 0;
						_lastWritten = workItem.ordinal;
						_toFill.Enqueue(workItem.index);
						if (num2 == -1)
						{
							num2 = 0;
						}
					}
					else
					{
						num3 = -1;
					}
				}
				while (num3 >= 0);
			}
			while (doAll && _lastWritten != _latestCompressed);
			emitting = false;
		}

		private void _DeflateOne(object wi)
		{
			WorkItem workItem = (WorkItem)wi;
			try
			{
				_ = workItem.index;
				CRC32 cRC = new CRC32();
				cRC.SlurpBlock(workItem.buffer, 0, workItem.inputBytesAvailable);
				DeflateOneSegment(workItem);
				workItem.crc = cRC.Crc32Result;
				lock (_latestLock)
				{
					if (workItem.ordinal > _latestCompressed)
					{
						_latestCompressed = workItem.ordinal;
					}
				}
				lock (_toWrite)
				{
					_toWrite.Enqueue(workItem.index);
				}
				_newlyCompressedBlob.Set();
			}
			catch (Exception pendingException)
			{
				lock (_eLock)
				{
					if (_pendingException != null)
					{
						_pendingException = pendingException;
					}
				}
			}
		}

		private bool DeflateOneSegment(WorkItem workitem)
		{
			ZlibCodec compressor = workitem.compressor;
			compressor.ResetDeflate();
			compressor.NextIn = 0;
			compressor.AvailableBytesIn = workitem.inputBytesAvailable;
			compressor.NextOut = 0;
			compressor.AvailableBytesOut = workitem.compressed.Length;
			do
			{
				compressor.Deflate(FlushType.None);
			}
			while (compressor.AvailableBytesIn > 0 || compressor.AvailableBytesOut == 0);
			compressor.Deflate(FlushType.Sync);
			workitem.compressedBytesAvailable = (int)compressor.TotalBytesOut;
			return true;
		}

		[Conditional("Trace")]
		private void TraceOutput(TraceBits bits, string format, params object[] varParams)
		{
			if ((bits & _DesiredTrace) != TraceBits.None)
			{
				lock (_outputLock)
				{
					int hashCode = Thread.CurrentThread.GetHashCode();
					Console.ForegroundColor = (ConsoleColor)(hashCode % 8 + 8);
					Console.Write("{0:000} PDOS ", hashCode);
					Console.WriteLine(format, varParams);
					Console.ResetColor();
				}
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}
	}
	internal sealed class Tree
	{
		internal const int Buf_size = 16;

		private static readonly int HEAP_SIZE = 2 * InternalConstants.L_CODES + 1;

		internal static readonly int[] ExtraLengthBits = new int[29]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1, 2, 2, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 4, 5, 5, 5, 5, 0
		};

		internal static readonly int[] ExtraDistanceBits = new int[30]
		{
			0, 0, 0, 0, 1, 1, 2, 2, 3, 3,
			4, 4, 5, 5, 6, 6, 7, 7, 8, 8,
			9, 9, 10, 10, 11, 11, 12, 12, 13, 13
		};

		internal static readonly int[] extra_blbits = new int[19]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 2, 3, 7
		};

		internal static readonly sbyte[] bl_order = new sbyte[19]
		{
			16, 17, 18, 0, 8, 7, 9, 6, 10, 5,
			11, 4, 12, 3, 13, 2, 14, 1, 15
		};

		private static readonly sbyte[] _dist_code = new sbyte[512]
		{
			0, 1, 2, 3, 4, 4, 5, 5, 6, 6,
			6, 6, 7, 7, 7, 7, 8, 8, 8, 8,
			8, 8, 8, 8, 9, 9, 9, 9, 9, 9,
			9, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 11, 11,
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 12, 12, 12, 12, 12, 12,
			12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
			12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
			12, 12, 12, 12, 12, 12, 13, 13, 13, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 0, 0, 16, 17,
			18, 18, 19, 19, 20, 20, 20, 20, 21, 21,
			21, 21, 22, 22, 22, 22, 22, 22, 22, 22,
			23, 23, 23, 23, 23, 23, 23, 23, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			24, 24, 24, 24, 25, 25, 25, 25, 25, 25,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29
		};

		internal static readonly sbyte[] LengthCode = new sbyte[256]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 8,
			9, 9, 10, 10, 11, 11, 12, 12, 12, 12,
			13, 13, 13, 13, 14, 14, 14, 14, 15, 15,
			15, 15, 16, 16, 16, 16, 16, 16, 16, 16,
			17, 17, 17, 17, 17, 17, 17, 17, 18, 18,
			18, 18, 18, 18, 18, 18, 19, 19, 19, 19,
			19, 19, 19, 19, 20, 20, 20, 20, 20, 20,
			20, 20, 20, 20, 20, 20, 20, 20, 20, 20,
			21, 21, 21, 21, 21, 21, 21, 21, 21, 21,
			21, 21, 21, 21, 21, 21, 22, 22, 22, 22,
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			25, 25, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 28
		};

		internal static readonly int[] LengthBase = new int[29]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 10,
			12, 14, 16, 20, 24, 28, 32, 40, 48, 56,
			64, 80, 96, 112, 128, 160, 192, 224, 0
		};

		internal static readonly int[] DistanceBase = new int[30]
		{
			0, 1, 2, 3, 4, 6, 8, 12, 16, 24,
			32, 48, 64, 96, 128, 192, 256, 384, 512, 768,
			1024, 1536, 2048, 3072, 4096, 6144, 8192, 12288, 16384, 24576
		};

		internal short[] dyn_tree;

		internal int max_code;

		internal StaticTree staticTree;

		internal static int DistanceCode(int dist)
		{
			if (dist >= 256)
			{
				return _dist_code[256 + SharedUtils.URShift(dist, 7)];
			}
			return _dist_code[dist];
		}

		internal void gen_bitlen(DeflateManager s)
		{
			short[] array = dyn_tree;
			short[] treeCodes = staticTree.treeCodes;
			int[] extraBits = staticTree.extraBits;
			int extraBase = staticTree.extraBase;
			int maxLength = staticTree.maxLength;
			int num = 0;
			for (int i = 0; i <= InternalConstants.MAX_BITS; i++)
			{
				s.bl_count[i] = 0;
			}
			array[s.heap[s.heap_max] * 2 + 1] = 0;
			int j;
			for (j = s.heap_max + 1; j < HEAP_SIZE; j++)
			{
				int num2 = s.heap[j];
				int i = array[array[num2 * 2 + 1] * 2 + 1] + 1;
				if (i > maxLength)
				{
					i = maxLength;
					num++;
				}
				array[num2 * 2 + 1] = (short)i;
				if (num2 <= max_code)
				{
					s.bl_count[i]++;
					int num3 = 0;
					if (num2 >= extraBase)
					{
						num3 = extraBits[num2 - extraBase];
					}
					short num4 = array[num2 * 2];
					s.opt_len += num4 * (i + num3);
					if (treeCodes != null)
					{
						s.static_len += num4 * (treeCodes[num2 * 2 + 1] + num3);
					}
				}
			}
			if (num == 0)
			{
				return;
			}
			do
			{
				int i = maxLength - 1;
				while (s.bl_count[i] == 0)
				{
					i--;
				}
				s.bl_count[i]--;
				s.bl_count[i + 1] = (short)(s.bl_count[i + 1] + 2);
				s.bl_count[maxLength]--;
				num -= 2;
			}
			while (num > 0);
			for (int i = maxLength; i != 0; i--)
			{
				int num2 = s.bl_count[i];
				while (num2 != 0)
				{
					int num5 = s.heap[--j];
					if (num5 <= max_code)
					{
						if (array[num5 * 2 + 1] != i)
						{
							s.opt_len = (int)(s.opt_len + ((long)i - (long)array[num5 * 2 + 1]) * array[num5 * 2]);
							array[num5 * 2 + 1] = (short)i;
						}
						num2--;
					}
				}
			}
		}

		internal void build_tree(DeflateManager s)
		{
			short[] array = dyn_tree;
			short[] treeCodes = staticTree.treeCodes;
			int elems = staticTree.elems;
			int num = -1;
			s.heap_len = 0;
			s.heap_max = HEAP_SIZE;
			for (int i = 0; i < elems; i++)
			{
				if (array[i * 2] != 0)
				{
					num = (s.heap[++s.heap_len] = i);
					s.depth[i] = 0;
				}
				else
				{
					array[i * 2 + 1] = 0;
				}
			}
			int num2;
			while (s.heap_len < 2)
			{
				num2 = (s.heap[++s.heap_len] = ((num < 2) ? (++num) : 0));
				array[num2 * 2] = 1;
				s.depth[num2] = 0;
				s.opt_len--;
				if (treeCodes != null)
				{
					s.static_len -= treeCodes[num2 * 2 + 1];
				}
			}
			max_code = num;
			for (int i = s.heap_len / 2; i >= 1; i--)
			{
				s.pqdownheap(array, i);
			}
			num2 = elems;
			do
			{
				int i = s.heap[1];
				s.heap[1] = s.heap[s.heap_len--];
				s.pqdownheap(array, 1);
				int num3 = s.heap[1];
				s.heap[--s.heap_max] = i;
				s.heap[--s.heap_max] = num3;
				array[num2 * 2] = (short)(array[i * 2] + array[num3 * 2]);
				s.depth[num2] = (sbyte)(Math.Max((byte)s.depth[i], (byte)s.depth[num3]) + 1);
				array[i * 2 + 1] = (array[num3 * 2 + 1] = (short)num2);
				s.heap[1] = num2++;
				s.pqdownheap(array, 1);
			}
			while (s.heap_len >= 2);
			s.heap[--s.heap_max] = s.heap[1];
			gen_bitlen(s);
			gen_codes(array, num, s.bl_count);
		}

		internal static void gen_codes(short[] tree, int max_code, short[] bl_count)
		{
			short[] array = new short[InternalConstants.MAX_BITS + 1];
			short num = 0;
			for (int i = 1; i <= InternalConstants.MAX_BITS; i++)
			{
				num = (array[i] = (short)(num + bl_count[i - 1] << 1));
			}
			for (int j = 0; j <= max_code; j++)
			{
				int num2 = tree[j * 2 + 1];
				if (num2 != 0)
				{
					tree[j * 2] = (short)bi_reverse(array[num2]++, num2);
				}
			}
		}

		internal static int bi_reverse(int code, int len)
		{
			int num = 0;
			do
			{
				num |= code & 1;
				code >>= 1;
				num <<= 1;
			}
			while (--len > 0);
			return num >> 1;
		}
	}
	public enum FlushType
	{
		None,
		Partial,
		Sync,
		Full,
		Finish
	}
	public enum CompressionLevel
	{
		None = 0,
		Level0 = 0,
		BestSpeed = 1,
		Level1 = 1,
		Level2 = 2,
		Level3 = 3,
		Level4 = 4,
		Level5 = 5,
		Default = 6,
		Level6 = 6,
		Level7 = 7,
		Level8 = 8,
		BestCompression = 9,
		Level9 = 9
	}
	public enum CompressionStrategy
	{
		Default,
		Filtered,
		HuffmanOnly
	}
	public enum CompressionMode
	{
		Compress,
		Decompress
	}
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000E")]
	public class ZlibException : Exception
	{
		public ZlibException()
		{
		}

		public ZlibException(string s)
			: base(s)
		{
		}
	}
	internal class SharedUtils
	{
		public static int URShift(int number, int bits)
		{
			return number >>> bits;
		}

		public static int ReadInput(TextReader sourceTextReader, byte[] target, int start, int count)
		{
			if (target.Length == 0)
			{
				return 0;
			}
			char[] array = new char[target.Length];
			int num = sourceTextReader.Read(array, start, count);
			if (num == 0)
			{
				return -1;
			}
			for (int i = start; i < start + num; i++)
			{
				target[i] = (byte)array[i];
			}
			return num;
		}

		internal static byte[] ToByteArray(string sourceString)
		{
			return Encoding.UTF8.GetBytes(sourceString);
		}

		internal static char[] ToCharArray(byte[] byteArray)
		{
			return Encoding.UTF8.GetChars(byteArray);
		}
	}
	internal static class InternalConstants
	{
		internal static readonly int MAX_BITS = 15;

		internal static readonly int BL_CODES = 19;

		internal static readonly int D_CODES = 30;

		internal static readonly int LITERALS = 256;

		internal static readonly int LENGTH_CODES = 29;

		internal static readonly int L_CODES = LITERALS + 1 + LENGTH_CODES;

		internal static readonly int MAX_BL_BITS = 7;

		internal static readonly int REP_3_6 = 16;

		internal static readonly int REPZ_3_10 = 17;

		internal static readonly int REPZ_11_138 = 18;
	}
	internal sealed class StaticTree
	{
		internal static readonly short[] lengthAndLiteralsTreeCodes;

		internal static readonly short[] distTreeCodes;

		internal static readonly StaticTree Literals;

		internal static readonly StaticTree Distances;

		internal static readonly StaticTree BitLengths;

		internal short[] treeCodes;

		internal int[] extraBits;

		internal int extraBase;

		internal int elems;

		internal int maxLength;

		private StaticTree(short[] treeCodes, int[] extraBits, int extraBase, int elems, int maxLength)
		{
			this.treeCodes = treeCodes;
			this.extraBits = extraBits;
			this.extraBase = extraBase;
			this.elems = elems;
			this.maxLength = maxLength;
		}

		static StaticTree()
		{
			lengthAndLiteralsTreeCodes = new short[576]
			{
				12, 8, 140, 8, 76, 8, 204, 8, 44, 8,
				172, 8, 108, 8, 236, 8, 28, 8, 156, 8,
				92, 8, 220, 8, 60, 8, 188, 8, 124, 8,
				252, 8, 2, 8, 130, 8, 66, 8, 194, 8,
				34, 8, 162, 8, 98, 8, 226, 8, 18, 8,
				146, 8, 82, 8, 210, 8, 50, 8, 178, 8,
				114, 8, 242, 8, 10, 8, 138, 8, 74, 8,
				202, 8, 42, 8, 170, 8, 106, 8, 234, 8,
				26, 8, 154, 8, 90, 8, 218, 8, 58, 8,
				186, 8, 122, 8, 250, 8, 6, 8, 134, 8,
				70, 8, 198, 8, 38, 8, 166, 8, 102, 8,
				230, 8, 22, 8, 150, 8, 86, 8, 214, 8,
				54, 8, 182, 8, 118, 8, 246, 8, 14, 8,
				142, 8, 78, 8, 206, 8, 46, 8, 174, 8,
				110, 8, 238, 8, 30, 8, 158, 8, 94, 8,
				222, 8, 62, 8, 190, 8, 126, 8, 254, 8,
				1, 8, 129, 8, 65, 8, 193, 8, 33, 8,
				161, 8, 97, 8, 225, 8, 17, 8, 145, 8,
				81, 8, 209, 8, 49, 8, 177, 8, 113, 8,
				241, 8, 9, 8, 137, 8, 73, 8, 201, 8,
				41, 8, 169, 8, 105, 8, 233, 8, 25, 8,
				153, 8, 89, 8, 217, 8, 57, 8, 185, 8,
				121, 8, 249, 8, 5, 8, 133, 8, 69, 8,
				197, 8, 37, 8, 165, 8, 101, 8, 229, 8,
				21, 8, 149, 8, 85, 8, 213, 8, 53, 8,
				181, 8, 117, 8, 245, 8, 13, 8, 141, 8,
				77, 8, 205, 8, 45, 8, 173, 8, 109, 8,
				237, 8, 29, 8, 157, 8, 93, 8, 221, 8,
				61, 8, 189, 8, 125, 8, 253, 8, 19, 9,
				275, 9, 147, 9, 403, 9, 83, 9, 339, 9,
				211, 9, 467, 9, 51, 9, 307, 9, 179, 9,
				435, 9, 115, 9, 371, 9, 243, 9, 499, 9,
				11, 9, 267, 9, 139, 9, 395, 9, 75, 9,
				331, 9, 203, 9, 459, 9, 43, 9, 299, 9,
				171, 9, 427, 9, 107, 9, 363, 9, 235, 9,
				491, 9, 27, 9, 283, 9, 155, 9, 411, 9,
				91, 9, 347, 9, 219, 9, 475, 9, 59, 9,
				315, 9, 187, 9, 443, 9, 123, 9, 379, 9,
				251, 9, 507, 9, 7, 9, 263, 9, 135, 9,
				391, 9, 71, 9, 327, 9, 199, 9, 455, 9,
				39, 9, 295, 9, 167, 9, 423, 9, 103, 9,
				359, 9, 231, 9, 487, 9, 23, 9, 279, 9,
				151, 9, 407, 9, 87, 9, 343, 9, 215, 9,
				471, 9, 55, 9, 311, 9, 183, 9, 439, 9,
				119, 9, 375, 9, 247, 9, 503, 9, 15, 9,
				271, 9, 143, 9, 399, 9, 79, 9, 335, 9,
				207, 9, 463, 9, 47, 9, 303, 9, 175, 9,
				431, 9, 111, 9, 367, 9, 239, 9, 495, 9,
				31, 9, 287, 9, 159, 9, 415, 9, 95, 9,
				351, 9, 223, 9, 479, 9, 63, 9, 319, 9,
				191, 9, 447, 9, 127, 9, 383, 9, 255, 9,
				511, 9, 0, 7, 64, 7, 32, 7, 96, 7,
				16, 7, 80, 7, 48, 7, 112, 7, 8, 7,
				72, 7, 40, 7, 104, 7, 24, 7, 88, 7,
				56, 7, 120, 7, 4, 7, 68, 7, 36, 7,
				100, 7, 20, 7, 84, 7, 52, 7, 116, 7,
				3, 8, 131, 8, 67, 8, 195, 8, 35, 8,
				163, 8, 99, 8, 227, 8
			};
			distTreeCodes = new short[60]
			{
				0, 5, 16, 5, 8, 5, 24, 5, 4, 5,
				20, 5, 12, 5, 28, 5, 2, 5, 18, 5,
				10, 5, 26, 5, 6, 5, 22, 5, 14, 5,
				30, 5, 1, 5, 17, 5, 9, 5, 25, 5,
				5, 5, 21, 5, 13, 5, 29, 5, 3, 5,
				19, 5, 11, 5, 27, 5, 7, 5, 23, 5
			};
			Literals = new StaticTree(lengthAndLiteralsTreeCodes, Tree.ExtraLengthBits, InternalConstants.LITERALS + 1, InternalConstants.L_CODES, InternalConstants.MAX_BITS);
			Distances = new StaticTree(distTreeCodes, Tree.ExtraDistanceBits, 0, InternalConstants.D_CODES, InternalConstants.MAX_BITS);
			BitLengths = new StaticTree(null, Tree.extra_blbits, 0, InternalConstants.BL_CODES, InternalConstants.MAX_BL_BITS);
		}
	}
	public sealed class Adler
	{
		private static readonly uint BASE = 65521u;

		private static readonly int NMAX = 5552;

		public static uint Adler32(uint adler, byte[] buf, int index, int len)
		{
			if (buf == null)
			{
				return 1u;
			}
			uint num = adler & 0xFFFF;
			uint num2 = (adler >> 16) & 0xFFFF;
			while (len > 0)
			{
				int num3 = ((len < NMAX) ? len : NMAX);
				len -= num3;
				while (num3 >= 16)
				{
					num += buf[index++];
					num2 += num;
					num += buf[index++];
					num2 += num;
					num += buf[index++];
					num2 += num;
					num += buf[index++];
					num2 += num;
					num += buf[index++];
					num2 += num;
					num += buf[index++];
					num2 += num;
					num += buf[index++];
					num2 += num;
					num += buf[index++];
					num2 += num;
					num += buf[index++];
					num2 += num;
					num += buf[index++];
					num2 += num;
					num += buf[index++];
					num2 += num;
					num += buf[index++];
					num2 += num;
					num += buf[index++];
					num2 += num;
					num += buf[index++];
					num2 += num;
					num += buf[index++];
					num2 += num;
					num += buf[index++];
					num2 += num;
					num3 -= 16;
				}
				if (num3 != 0)
				{
					do
					{
						num += buf[index++];
						num2 += num;
					}
					while (--num3 != 0);
				}
				num %= BASE;
				num2 %= BASE;
			}
			return (num2 << 16) | num;
		}
	}
	internal enum ZlibStreamFlavor
	{
		ZLIB = 1950,
		DEFLATE,
		GZIP
	}
	internal class ZlibBaseStream : Stream
	{
		internal enum StreamMode
		{
			Writer,
			Reader,
			Undefined
		}

		protected internal ZlibCodec _z;

		protected internal StreamMode _streamMode = StreamMode.Undefined;

		protected internal FlushType _flushMode;

		protected internal ZlibStreamFlavor _flavor;

		protected internal CompressionMode _compressionMode;

		protected internal CompressionLevel _level;

		protected internal bool _leaveOpen;

		protected internal byte[] _workingBuffer;

		protected internal int _bufferSize = 16384;

		protected internal byte[] _buf1 = new byte[1];

		protected internal Stream _stream;

		protected internal CompressionStrategy Strategy;

		private CRC32 crc;

		protected internal string _GzipFileName;

		protected internal string _GzipComment;

		protected internal DateTime _GzipMtime;

		protected internal int _gzipHeaderByteCount;

		private bool nomoreinput;

		internal int Crc32
		{
			get
			{
				if (crc == null)
				{
					return 0;
				}
				return crc.Crc32Result;
			}
		}

		protected internal bool _wantCompress => _compressionMode == CompressionMode.Compress;

		private ZlibCodec z
		{
			get
			{
				if (_z == null)
				{
					bool flag = _flavor == ZlibStreamFlavor.ZLIB;
					_z = new ZlibCodec();
					if (_compressionMode == CompressionMode.Decompress)
					{
						_z.InitializeInflate(flag);
					}
					else
					{
						_z.Strategy = Strategy;
						_z.InitializeDeflate(_level, flag);
					}
				}
				return _z;
			}
		}

		private byte[] workingBuffer
		{
			get
			{
				if (_workingBuffer == null)
				{
					_workingBuffer = new byte[_bufferSize];
				}
				return _workingBuffer;
			}
		}

		public override bool CanRead => _stream.CanRead;

		public override bool CanSeek => _stream.CanSeek;

		public override bool CanWrite => _stream.CanWrite;

		public override long Length => _stream.Length;

		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public ZlibBaseStream(Stream stream, CompressionMode compressionMode, CompressionLevel level, ZlibStreamFlavor flavor, bool leaveOpen)
		{
			_flushMode = FlushType.None;
			_stream = stream;
			_leaveOpen = leaveOpen;
			_compressionMode = compressionMode;
			_flavor = flavor;
			_level = level;
			if (flavor == ZlibStreamFlavor.GZIP)
			{
				crc = new CRC32();
			}
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (crc != null)
			{
				crc.SlurpBlock(buffer, offset, count);
			}
			if (_streamMode == StreamMode.Undefined)
			{
				_streamMode = StreamMode.Writer;
			}
			else if (_streamMode != StreamMode.Writer)
			{
				throw new ZlibException("Cannot Write after Reading.");
			}
			if (count == 0)
			{
				return;
			}
			z.InputBuffer = buffer;
			_z.NextIn = offset;
			_z.AvailableBytesIn = count;
			bool flag = false;
			do
			{
				_z.OutputBuffer = workingBuffer;
				_z.NextOut = 0;
				_z.AvailableBytesOut = _workingBuffer.Length;
				int num = (_wantCompress ? _z.Deflate(_flushMode) : _z.Inflate(_flushMode));
				if (num != 0 && num != 1)
				{
					throw new ZlibException((_wantCompress ? "de" : "in") + "flating: " + _z.Message);
				}
				_stream.Write(_workingBuffer, 0, _workingBuffer.Length - _z.AvailableBytesOut);
				flag = _z.AvailableBytesIn == 0 && _z.AvailableBytesOut != 0;
				if (_flavor == ZlibStreamFlavor.GZIP && !_wantCompress)
				{
					flag = _z.AvailableBytesIn == 8 && _z.AvailableBytesOut != 0;
				}
			}
			while (!flag);
		}

		private void finish()
		{
			if (_z == null)
			{
				return;
			}
			if (_streamMode == StreamMode.Writer)
			{
				bool flag = false;
				do
				{
					_z.OutputBuffer = workingBuffer;
					_z.NextOut = 0;
					_z.AvailableBytesOut = _workingBuffer.Length;
					int num = (_wantCompress ? _z.Deflate(FlushType.Finish) : _z.Inflate(FlushType.Finish));
					if (num != 1 && num != 0)
					{
						string text = (_wantCompress ? "de" : "in") + "flating";
						if (_z.Message == null)
						{
							throw new ZlibException($"{text}: (rc = {num})");
						}
						throw new ZlibException(text + ": " + _z.Message);
					}
					if (_workingBuffer.Length - _z.AvailableBytesOut > 0)
					{
						_stream.Write(_workingBuffer, 0, _workingBuffer.Length - _z.AvailableBytesOut);
					}
					flag = _z.AvailableBytesIn == 0 && _z.AvailableBytesOut != 0;
					if (_flavor == ZlibStreamFlavor.GZIP && !_wantCompress)
					{
						flag = _z.AvailableBytesIn == 8 && _z.AvailableBytesOut != 0;
					}
				}
				while (!flag);
				Flush();
				if (_flavor == ZlibStreamFlavor.GZIP)
				{
					if (!_wantCompress)
					{
						throw new ZlibException("Writing with decompression is not supported.");
					}
					int crc32Result = crc.Crc32Result;
					_stream.Write(BitConverter.GetBytes(crc32Result), 0, 4);
					int value = (int)(crc.TotalBytesRead & 0xFFFFFFFFu);
					_stream.Write(BitConverter.GetBytes(value), 0, 4);
				}
			}
			else
			{
				if (_streamMode != StreamMode.Reader || _flavor != ZlibStreamFlavor.GZIP)
				{
					return;
				}
				if (_wantCompress)
				{
					throw new ZlibException("Reading with compression is not supported.");
				}
				if (_z.TotalBytesOut == 0)
				{
					return;
				}
				byte[] array = new byte[8];
				if (_z.AvailableBytesIn < 8)
				{
					Array.Copy(_z.InputBuffer, _z.NextIn, array, 0, _z.AvailableBytesIn);
					int num2 = 8 - _z.AvailableBytesIn;
					int num3 = _stream.Read(array, _z.AvailableBytesIn, num2);
					if (num2 != num3)
					{
						throw new ZlibException($"Missing or incomplete GZIP trailer. Expected 8 bytes, got {_z.AvailableBytesIn + num3}.");
					}
				}
				else
				{
					Array.Copy(_z.InputBuffer, _z.NextIn, array, 0, array.Length);
				}
				int num4 = BitConverter.ToInt32(array, 0);
				int crc32Result2 = crc.Crc32Result;
				int num5 = BitConverter.ToInt32(array, 4);
				int num6 = (int)(_z.TotalBytesOut & 0xFFFFFFFFu);
				if (crc32Result2 != num4)
				{
					throw new ZlibException($"Bad CRC32 in GZIP trailer. (actual({crc32Result2:X8})!=expected({num4:X8}))");
				}
				if (num6 != num5)
				{
					throw new ZlibException($"Bad size in GZIP trailer. (actual({num6})!=expected({num5}))");
				}
			}
		}

		private void end()
		{
			if (z != null)
			{
				if (_wantCompress)
				{
					_z.EndDeflate();
				}
				else
				{
					_z.EndInflate();
				}
				_z = null;
			}
		}

		public override void Close()
		{
			if (_stream == null)
			{
				return;
			}
			try
			{
				finish();
			}
			finally
			{
				end();
				if (!_leaveOpen)
				{
					_stream.Close();
				}
				_stream = null;
			}
		}

		public override void Flush()
		{
			_stream.Flush();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			_stream.SetLength(value);
		}

		private string ReadZeroTerminatedString()
		{
			List<byte> list = new List<byte>();
			bool flag = false;
			do
			{
				int num = _stream.Read(_buf1, 0, 1);
				if (num != 1)
				{
					throw new ZlibException("Unexpected EOF reading GZIP header.");
				}
				if (_buf1[0] == 0)
				{
					flag = true;
				}
				else
				{
					list.Add(_buf1[0]);
				}
			}
			while (!flag);
			byte[] array = list.ToArray();
			return GZipStream.iso8859dash1.GetString(array, 0, array.Length);
		}

		private int _ReadAndValidateGzipHeader()
		{
			int num = 0;
			byte[] array = new byte[10];
			int num2 = _stream.Read(array, 0, array.Length);
			switch (num2)
			{
			case 0:
				return 0;
			default:
				throw new ZlibException("Not a valid GZIP stream.");
			case 10:
			{
				if (array[0] != 31 || array[1] != 139 || array[2] != 8)
				{
					throw new ZlibException("Bad GZIP header.");
				}
				int num3 = BitConverter.ToInt32(array, 4);
				DateTime unixEpoch = GZipStream._unixEpoch;
				_GzipMtime = unixEpoch.AddSeconds(num3);
				num += num2;
				if ((array[3] & 4) == 4)
				{
					num2 = _stream.Read(array, 0, 2);
					num += num2;
					short num4 = (short)(array[0] + array[1] * 256);
					byte[] array2 = new byte[num4];
					num2 = _stream.Read(array2, 0, array2.Length);
					if (num2 != num4)
					{
						throw new ZlibException("Unexpected end-of-file reading GZIP header.");
					}
					num += num2;
				}
				if ((array[3] & 8) == 8)
				{
					_GzipFileName = ReadZeroTerminatedString();
				}
				if ((array[3] & 0x10) == 16)
				{
					_GzipComment = ReadZeroTerminatedString();
				}
				if ((array[3] & 2) == 2)
				{
					Read(_buf1, 0, 1);
				}
				return num;
			}
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (_streamMode == StreamMode.Undefined)
			{
				if (!_stream.CanRead)
				{
					throw new ZlibException("The stream is not readable.");
				}
				_streamMode = StreamMode.Reader;
				z.AvailableBytesIn = 0;
				if (_flavor == ZlibStreamFlavor.GZIP)
				{
					_gzipHeaderByteCount = _ReadAndValidateGzipHeader();
					if (_gzipHeaderByteCount == 0)
					{
						return 0;
					}
				}
			}
			if (_streamMode != StreamMode.Reader)
			{
				throw new ZlibException("Cannot Read after Writing.");
			}
			if (count == 0)
			{
				return 0;
			}
			if (nomoreinput && _wantCompress)
			{
				return 0;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (offset < buffer.GetLowerBound(0))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.GetLength(0))
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num = 0;
			_z.OutputBuffer = buffer;
			_z.NextOut = offset;
			_z.AvailableBytesOut = count;
			_z.InputBuffer = workingBuffer;
			do
			{
				if (_z.AvailableBytesIn == 0 && !nomoreinput)
				{
					_z.NextIn = 0;
					_z.AvailableBytesIn = _stream.Read(_workingBuffer, 0, _workingBuffer.Length);
					if (_z.AvailableBytesIn == 0)
					{
						nomoreinput = true;
					}
				}
				num = (_wantCompress ? _z.Deflate(_flushMode) : _z.Inflate(_flushMode));
				if (nomoreinput && num == -5)
				{
					return 0;
				}
				if (num != 0 && num != 1)
				{
					throw new ZlibException(string.Format("{0}flating:  rc={1}  msg={2}", _wantCompress ? "de" : "in", num, _z.Message));
				}
			}
			while (((!nomoreinput && num != 1) || _z.AvailableBytesOut != count) && _z.AvailableBytesOut > 0 && !nomoreinput && num == 0);
			if (_z.AvailableBytesOut > 0)
			{
				if (num == 0)
				{
					_ = _z.AvailableBytesIn;
				}
				if (nomoreinput && _wantCompress)
				{
					num = _z.Deflate(FlushType.Finish);
					if (num != 0 && num != 1)
					{
						throw new ZlibException($"Deflating:  rc={num}  msg={_z.Message}");
					}
				}
			}
			num = count - _z.AvailableBytesOut;
			if (crc != null)
			{
				crc.SlurpBlock(buffer, offset, num);
			}
			return num;
		}

		public static void CompressString(string s, Stream compressor)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			using (compressor)
			{
				compressor.Write(bytes, 0, bytes.Length);
			}
		}

		public static void CompressBuffer(byte[] b, Stream compressor)
		{
			using (compressor)
			{
				compressor.Write(b, 0, b.Length);
			}
		}

		public static string UncompressString(byte[] compressed, Stream decompressor)
		{
			byte[] array = new byte[1024];
			Encoding uTF = Encoding.UTF8;
			using MemoryStream memoryStream = new MemoryStream();
			using (decompressor)
			{
				int count;
				while ((count = decompressor.Read(array, 0, array.Length)) != 0)
				{
					memoryStream.Write(array, 0, count);
				}
			}
			memoryStream.Seek(0L, SeekOrigin.Begin);
			StreamReader streamReader = new StreamReader(memoryStream, uTF);
			return streamReader.ReadToEnd();
		}

		public static byte[] UncompressBuffer(byte[] compressed, Stream decompressor)
		{
			byte[] array = new byte[1024];
			using MemoryStream memoryStream = new MemoryStream();
			using (decompressor)
			{
				int count;
				while ((count = decompressor.Read(array, 0, array.Length)) != 0)
				{
					memoryStream.Write(array, 0, count);
				}
			}
			return memoryStream.ToArray();
		}
	}
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000D")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public sealed class ZlibCodec
	{
		public byte[] InputBuffer;

		public int NextIn;

		public int AvailableBytesIn;

		public long TotalBytesIn;

		public byte[] OutputBuffer;

		public int NextOut;

		public int AvailableBytesOut;

		public long TotalBytesOut;

		public string Message;

		internal DeflateManager dstate;

		internal InflateManager istate;

		internal uint _Adler32;

		public CompressionLevel CompressLevel = CompressionLevel.Default;

		public int WindowBits = 15;

		public CompressionStrategy Strategy;

		public int Adler32 => (int)_Adler32;

		public ZlibCodec()
		{
		}

		public ZlibCodec(CompressionMode mode)
		{
			switch (mode)
			{
			case CompressionMode.Compress:
				if (InitializeDeflate() != 0)
				{
					throw new ZlibException("Cannot initialize for deflate.");
				}
				break;
			case CompressionMode.Decompress:
				if (InitializeInflate() != 0)
				{
					throw new ZlibException("Cannot initialize for inflate.");
				}
				break;
			default:
				throw new ZlibException("Invalid ZlibStreamFlavor.");
			}
		}

		public int InitializeInflate()
		{
			return InitializeInflate(WindowBits);
		}

		public int InitializeInflate(bool expectRfc1950Header)
		{
			return InitializeInflate(WindowBits, expectRfc1950Header);
		}

		public int InitializeInflate(int windowBits)
		{
			WindowBits = windowBits;
			return InitializeInflate(windowBits, expectRfc1950Header: true);
		}

		public int InitializeInflate(int windowBits, bool expectRfc1950Header)
		{
			WindowBits = windowBits;
			if (dstate != null)
			{
				throw new ZlibException("You may not call InitializeInflate() after calling InitializeDeflate().");
			}
			istate = new InflateManager(expectRfc1950Header);
			return istate.Initialize(this, windowBits);
		}

		public int Inflate(FlushType flush)
		{
			if (istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return istate.Inflate(flush);
		}

		public int EndInflate()
		{
			if (istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			int result = istate.End();
			istate = null;
			return result;
		}

		public int SyncInflate()
		{
			if (istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return istate.Sync();
		}

		public int InitializeDeflate()
		{
			return _InternalInitializeDeflate(wantRfc1950Header: true);
		}

		public int InitializeDeflate(CompressionLevel level)
		{
			CompressLevel = level;
			return _InternalInitializeDeflate(wantRfc1950Header: true);
		}

		public int InitializeDeflate(CompressionLevel level, bool wantRfc1950Header)
		{
			CompressLevel = level;
			return _InternalInitializeDeflate(wantRfc1950Header);
		}

		public int InitializeDeflate(CompressionLevel level, int bits)
		{
			CompressLevel = level;
			WindowBits = bits;
			return _InternalInitializeDeflate(wantRfc1950Header: true);
		}

		public int InitializeDeflate(CompressionLevel level, int bits, bool wantRfc1950Header)
		{
			CompressLevel = level;
			WindowBits = bits;
			return _InternalInitializeDeflate(wantRfc1950Header);
		}

		private int _InternalInitializeDeflate(bool wantRfc1950Header)
		{
			if (istate != null)
			{
				throw new ZlibException("You may not call InitializeDeflate() after calling InitializeInflate().");
			}
			dstate = new DeflateManager();
			dstate.WantRfc1950HeaderBytes = wantRfc1950Header;
			return dstate.Initialize(this, CompressLevel, WindowBits, Strategy);
		}

		public int Deflate(FlushType flush)
		{
			if (dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return dstate.Deflate(flush);
		}

		public int EndDeflate()
		{
			if (dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			dstate = null;
			return 0;
		}

		public void ResetDeflate()
		{
			if (dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			dstate.Reset();
		}

		public int SetDeflateParams(CompressionLevel level, CompressionStrategy strategy)
		{
			if (dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return dstate.SetParams(level, strategy);
		}

		public int SetDictionary(byte[] dictionary)
		{
			if (istate != null)
			{
				return istate.SetDictionary(dictionary);
			}
			if (dstate != null)
			{
				return dstate.SetDictionary(dictionary);
			}
			throw new ZlibException("No Inflate or Deflate state!");
		}

		internal void flush_pending()
		{
			int num = dstate.pendingCount;
			if (num > AvailableBytesOut)
			{
				num = AvailableBytesOut;
			}
			if (num != 0)
			{
				if (dstate.pending.Length <= dstate.nextPending || OutputBuffer.Length <= NextOut || dstate.pending.Length < dstate.nextPending + num || OutputBuffer.Length < NextOut + num)
				{
					throw new ZlibException($"Invalid State. (pending.Length={dstate.pending.Length}, pendingCount={dstate.pendingCount})");
				}
				Array.Copy(dstate.pending, dstate.nextPending, OutputBuffer, NextOut, num);
				NextOut += num;
				dstate.nextPending += num;
				TotalBytesOut += num;
				AvailableBytesOut -= num;
				dstate.pendingCount -= num;
				if (dstate.pendingCount == 0)
				{
					dstate.nextPending = 0;
				}
			}
		}

		internal int read_buf(byte[] buf, int start, int size)
		{
			int num = AvailableBytesIn;
			if (num > size)
			{
				num = size;
			}
			if (num == 0)
			{
				return 0;
			}
			AvailableBytesIn -= num;
			if (dstate.WantRfc1950HeaderBytes)
			{
				_Adler32 = Adler.Adler32(_Adler32, InputBuffer, NextIn, num);
			}
			Array.Copy(InputBuffer, NextIn, buf, start, num);
			NextIn += num;
			TotalBytesIn += num;
			return num;
		}
	}
	public static class ZlibConstants
	{
		public const int WindowBitsMax = 15;

		public const int WindowBitsDefault = 15;

		public const int Z_OK = 0;

		public const int Z_STREAM_END = 1;

		public const int Z_NEED_DICT = 2;

		public const int Z_STREAM_ERROR = -2;

		public const int Z_DATA_ERROR = -3;

		public const int Z_BUF_ERROR = -5;

		public const int WorkingBufferSizeDefault = 16384;

		public const int WorkingBufferSizeMin = 1024;
	}
	public class ZlibStream : Stream
	{
		internal ZlibBaseStream _baseStream;

		private bool _disposed;

		public virtual FlushType FlushMode
		{
			get
			{
				return _baseStream._flushMode;
			}
			set
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				_baseStream._flushMode = value;
			}
		}

		public int BufferSize
		{
			get
			{
				return _baseStream._bufferSize;
			}
			set
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				if (_baseStream._workingBuffer != null)
				{
					throw new ZlibException("The working buffer is already set.");
				}
				if (value < 1024)
				{
					throw new ZlibException($"Don't be silly. {value} bytes?? Use a bigger buffer, at least {1024}.");
				}
				_baseStream._bufferSize = value;
			}
		}

		public virtual long TotalIn => _baseStream._z.TotalBytesIn;

		public virtual long TotalOut => _baseStream._z.TotalBytesOut;

		public override bool CanRead
		{
			get
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return _baseStream._stream.CanRead;
			}
		}

		public override bool CanSeek => false;

		public override bool CanWrite
		{
			get
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return _baseStream._stream.CanWrite;
			}
		}

		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public override long Position
		{
			get
			{
				if (_baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return _baseStream._z.TotalBytesOut;
				}
				if (_baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return _baseStream._z.TotalBytesIn;
				}
				return 0L;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public ZlibStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, leaveOpen: false)
		{
		}

		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, leaveOpen: false)
		{
		}

		public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			_baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.ZLIB, leaveOpen);
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!_disposed)
				{
					if (disposing && _baseStream != null)
					{
						_baseStream.Close();
					}
					_disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		public override void Flush()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			_baseStream.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			return _baseStream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			_baseStream.Write(buffer, offset, count);
		}

		public static byte[] CompressString(string s)
		{
			using MemoryStream memoryStream = new MemoryStream();
			Stream compressor = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
			ZlibBaseStream.CompressString(s, compressor);
			return memoryStream.ToArray();
		}

		public static byte[] CompressBuffer(byte[] b)
		{
			using MemoryStream memoryStream = new MemoryStream();
			Stream compressor = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
			ZlibBaseStream.CompressBuffer(b, compressor);
			return memoryStream.ToArray();
		}

		public static string UncompressString(byte[] compressed)
		{
			using MemoryStream stream = new MemoryStream(compressed);
			Stream decompressor = new ZlibStream(stream, CompressionMode.Decompress);
			return ZlibBaseStream.UncompressString(compressed, decompressor);
		}

		public static byte[] UncompressBuffer(byte[] compressed)
		{
			using MemoryStream stream = new MemoryStream(compressed);
			Stream decompressor = new ZlibStream(stream, CompressionMode.Decompress);
			return ZlibBaseStream.UncompressBuffer(compressed, decompressor);
		}
	}
}
namespace Ionic.Crc
{
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000C")]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	public class CRC32
	{
		private const int BUFFER_SIZE = 8192;

		private uint dwPolynomial;

		private long _TotalBytesRead;

		private bool reverseBits;

		private uint[] crc32Table;

		private uint _register = uint.MaxValue;

		public long TotalBytesRead => _TotalBytesRead;

		public int Crc32Result => (int)(~_register);

		public int GetCrc32(Stream input)
		{
			return GetCrc32AndCopy(input, null);
		}

		public int GetCrc32AndCopy(Stream input, Stream output)
		{
			if (input == null)
			{
				throw new Exception("The input stream must not be null.");
			}
			byte[] array = new byte[8192];
			int count = 8192;
			_TotalBytesRead = 0L;
			int num = input.Read(array, 0, count);
			output?.Write(array, 0, num);
			_TotalBytesRead += num;
			while (num > 0)
			{
				SlurpBlock(array, 0, num);
				num = input.Read(array, 0, count);
				output?.Write(array, 0, num);
				_TotalBytesRead += num;
			}
			return (int)(~_register);
		}

		public int ComputeCrc32(int W, byte B)
		{
			return _InternalComputeCrc32((uint)W, B);
		}

		internal int _InternalComputeCrc32(uint W, byte B)
		{
			return (int)(crc32Table[(W ^ B) & 0xFF] ^ (W >> 8));
		}

		public void SlurpBlock(byte[] block, int offset, int count)
		{
			if (block == null)
			{
				throw new Exception("The data buffer must not be null.");
			}
			for (int i = 0; i < count; i++)
			{
				int num = offset + i;
				byte b = block[num];
				if (reverseBits)
				{
					uint num2 = (_register >> 24) ^ b;
					_register = (_register << 8) ^ crc32Table[num2];
				}
				else
				{
					uint num3 = (_register & 0xFF) ^ b;
					_register = (_register >> 8) ^ crc32Table[num3];
				}
			}
			_TotalBytesRead += count;
		}

		public void UpdateCRC(byte b)
		{
			if (reverseBits)
			{
				uint num = (_register >> 24) ^ b;
				_register = (_register << 8) ^ crc32Table[num];
			}
			else
			{
				uint num2 = (_register & 0xFF) ^ b;
				_register = (_register >> 8) ^ crc32Table[num2];
			}
		}

		public void UpdateCRC(byte b, int n)
		{
			while (n-- > 0)
			{
				if (reverseBits)
				{
					uint num = (_register >> 24) ^ b;
					_register = (_register << 8) ^ crc32Table[(num >= 0) ? num : (num + 256)];
				}
				else
				{
					uint num2 = (_register & 0xFF) ^ b;
					_register = (_register >> 8) ^ crc32Table[(num2 >= 0) ? num2 : (num2 + 256)];
				}
			}
		}

		private static uint ReverseBits(uint data)
		{
			uint num = data;
			num = ((num & 0x55555555) << 1) | ((num >> 1) & 0x55555555);
			num = ((num & 0x33333333) << 2) | ((num >> 2) & 0x33333333);
			num = ((num & 0xF0F0F0F) << 4) | ((num >> 4) & 0xF0F0F0F);
			return (num << 24) | ((num & 0xFF00) << 8) | ((num >> 8) & 0xFF00) | (num >> 24);
		}

		private static byte ReverseBits(byte data)
		{
			uint num = (uint)(data * 131586);
			uint num2 = 17055760u;
			uint num3 = num & num2;
			uint num4 = (num << 2) & (num2 << 1);
			return (byte)(16781313 * (num3 + num4) >> 24);
		}

		private void GenerateLookupTable()
		{
			crc32Table = new uint[256];
			byte b = 0;
			do
			{
				uint num = b;
				for (byte b2 = 8; b2 > 0; b2--)
				{
					num = (((num & 1) != 1) ? (num >> 1) : ((num >> 1) ^ dwPolynomial));
				}
				if (reverseBits)
				{
					crc32Table[ReverseBits(b)] = ReverseBits(num);
				}
				else
				{
					crc32Table[b] = num;
				}
				b++;
			}
			while (b != 0);
		}

		private uint gf2_matrix_times(uint[] matrix, uint vec)
		{
			uint num = 0u;
			int num2 = 0;
			while (vec != 0)
			{
				if ((vec & 1) == 1)
				{
					num ^= matrix[num2];
				}
				vec >>= 1;
				num2++;
			}
			return num;
		}

		private void gf2_matrix_square(uint[] square, uint[] mat)
		{
			for (int i = 0; i < 32; i++)
			{
				square[i] = gf2_matrix_times(mat, mat[i]);
			}
		}

		public void Combine(int crc, int length)
		{
			uint[] array = new uint[32];
			uint[] array2 = new uint[32];
			if (length == 0)
			{
				return;
			}
			uint num = ~_register;
			array2[0] = dwPolynomial;
			uint num2 = 1u;
			for (int i = 1; i < 32; i++)
			{
				array2[i] = num2;
				num2 <<= 1;
			}
			gf2_matrix_square(array, array2);
			gf2_matrix_square(array2, array);
			uint num3 = (uint)length;
			do
			{
				gf2_matrix_square(array, array2);
				if ((num3 & 1) == 1)
				{
					num = gf2_matrix_times(array, num);
				}
				num3 >>= 1;
				if (num3 == 0)
				{
					break;
				}
				gf2_matrix_square(array2, array);
				if ((num3 & 1) == 1)
				{
					num = gf2_matrix_times(array2, num);
				}
				num3 >>= 1;
			}
			while (num3 != 0);
			num ^= (uint)crc;
			_register = ~num;
		}

		public CRC32()
			: this(reverseBits: false)
		{
		}

		public CRC32(bool reverseBits)
			: this(-306674912, reverseBits)
		{
		}

		public CRC32(int polynomial, bool reverseBits)
		{
			this.reverseBits = reverseBits;
			dwPolynomial = (uint)polynomial;
			GenerateLookupTable();
		}

		public void Reset()
		{
			_register = uint.MaxValue;
		}
	}
	public class CrcCalculatorStream : Stream, IDisposable
	{
		private static readonly long UnsetLengthLimit = -99L;

		internal Stream _innerStream;

		private CRC32 _Crc32;

		private long _lengthLimit = -99L;

		private bool _leaveOpen;

		public long TotalBytesSlurped => _Crc32.TotalBytesRead;

		public int Crc => _Crc32.Crc32Result;

		public bool LeaveOpen
		{
			get
			{
				return _leaveOpen;
			}
			set
			{
				_leaveOpen = value;
			}
		}

		public override bool CanRead => _innerStream.CanRead;

		public override bool CanSeek => false;

		public override bool CanWrite => _innerStream.CanWrite;

		public override long Length
		{
			get
			{
				if (_lengthLimit == UnsetLengthLimit)
				{
					return _innerStream.Length;
				}
				return _lengthLimit;
			}
		}

		public override long Position
		{
			get
			{
				return _Crc32.TotalBytesRead;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public CrcCalculatorStream(Stream stream)
			: this(leaveOpen: true, UnsetLengthLimit, stream, null)
		{
		}

		public CrcCalculatorStream(Stream stream, bool leaveOpen)
			: this(leaveOpen, UnsetLengthLimit, stream, null)
		{
		}

		public CrcCalculatorStream(Stream stream, long length)
			: this(leaveOpen: true, length, stream, null)
		{
			if (length < 0)
			{
				throw new ArgumentException("length");
			}
		}

		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen)
			: this(leaveOpen, length, stream, null)
		{
			if (length < 0)
			{
				throw new ArgumentException("length");
			}
		}

		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen, CRC32 crc32)
			: this(leaveOpen, length, stream, crc32)
		{
			if (length < 0)
			{
				throw new ArgumentException("length");
			}
		}

		private CrcCalculatorStream(bool leaveOpen, long length, Stream stream, CRC32 crc32)
		{
			_innerStream = stream;
			_Crc32 = crc32 ?? new CRC32();
			_lengthLimit = length;
			_leaveOpen = leaveOpen;
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int count2 = count;
			if (_lengthLimit != UnsetLengthLimit)
			{
				if (_Crc32.TotalBytesRead >= _lengthLimit)
				{
					return 0;
				}
				long num = _lengthLimit - _Crc32.TotalBytesRead;
				if (num < count)
				{
					count2 = (int)num;
				}
			}
			int num2 = _innerStream.Read(buffer, offset, count2);
			if (num2 > 0)
			{
				_Crc32.SlurpBlock(buffer, offset, num2);
			}
			return num2;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count > 0)
			{
				_Crc32.SlurpBlock(buffer, offset, count);
			}
			_innerStream.Write(buffer, offset, count);
		}

		public override void Flush()
		{
			_innerStream.Flush();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		void IDisposable.Dispose()
		{
			Close();
		}

		public override void Close()
		{
			base.Close();
			if (!_leaveOpen)
			{
				_innerStream.Close();
			}
		}
	}
}
