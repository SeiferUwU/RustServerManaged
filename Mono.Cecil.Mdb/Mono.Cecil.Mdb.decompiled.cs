using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: AssemblyProduct("Mono.Cecil")]
[assembly: AssemblyCopyright("Copyright © 2008 - 2018 Jb Evain")]
[assembly: ComVisible(false)]
[assembly: AssemblyFileVersion("0.11.5.0")]
[assembly: AssemblyInformationalVersion("0.11.5.0")]
[assembly: AssemblyTitle("Mono.Cecil.Mdb")]
[assembly: CLSCompliant(false)]
[assembly: TargetFramework(".NETFramework,Version=v4.0", FrameworkDisplayName = ".NET Framework 4")]
[assembly: AssemblyVersion("0.11.5.0")]
namespace Mono.CompilerServices.SymbolWriter
{
	public class MonoSymbolFileException : Exception
	{
		public MonoSymbolFileException()
		{
		}

		public MonoSymbolFileException(string message, params object[] args)
			: base(string.Format(message, args))
		{
		}

		public MonoSymbolFileException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
	internal sealed class MyBinaryWriter : BinaryWriter
	{
		public MyBinaryWriter(Stream stream)
			: base(stream)
		{
		}

		public void WriteLeb128(int value)
		{
			Write7BitEncodedInt(value);
		}
	}
	internal class MyBinaryReader : BinaryReader
	{
		public MyBinaryReader(Stream stream)
			: base(stream)
		{
		}

		public int ReadLeb128()
		{
			return Read7BitEncodedInt();
		}

		public string ReadString(int offset)
		{
			long position = BaseStream.Position;
			BaseStream.Position = offset;
			string result = ReadString();
			BaseStream.Position = position;
			return result;
		}
	}
	public interface ISourceFile
	{
		SourceFileEntry Entry { get; }
	}
	public interface ICompileUnit
	{
		CompileUnitEntry Entry { get; }
	}
	public interface IMethodDef
	{
		string Name { get; }

		int Token { get; }
	}
	public class MonoSymbolFile : IDisposable
	{
		private List<MethodEntry> methods = new List<MethodEntry>();

		private List<SourceFileEntry> sources = new List<SourceFileEntry>();

		private List<CompileUnitEntry> comp_units = new List<CompileUnitEntry>();

		private Dictionary<int, AnonymousScopeEntry> anonymous_scopes;

		private OffsetTable ot;

		private int last_type_index;

		private int last_method_index;

		private int last_namespace_index;

		public readonly int MajorVersion = 50;

		public readonly int MinorVersion = 0;

		public int NumLineNumbers;

		private MyBinaryReader reader;

		private Dictionary<int, SourceFileEntry> source_file_hash;

		private Dictionary<int, CompileUnitEntry> compile_unit_hash;

		private List<MethodEntry> method_list;

		private Dictionary<int, MethodEntry> method_token_hash;

		private Dictionary<string, int> source_name_hash;

		private Guid guid;

		internal int LineNumberCount = 0;

		internal int LocalCount = 0;

		internal int StringSize = 0;

		internal int LineNumberSize = 0;

		internal int ExtendedLineNumberSize = 0;

		public int CompileUnitCount => ot.CompileUnitCount;

		public int SourceCount => ot.SourceCount;

		public int MethodCount => ot.MethodCount;

		public int TypeCount => ot.TypeCount;

		public int AnonymousScopeCount => ot.AnonymousScopeCount;

		public int NamespaceCount => last_namespace_index;

		public Guid Guid => guid;

		public OffsetTable OffsetTable => ot;

		public SourceFileEntry[] Sources
		{
			get
			{
				if (reader == null)
				{
					throw new InvalidOperationException();
				}
				SourceFileEntry[] array = new SourceFileEntry[SourceCount];
				for (int i = 0; i < SourceCount; i++)
				{
					array[i] = GetSourceFile(i + 1);
				}
				return array;
			}
		}

		public CompileUnitEntry[] CompileUnits
		{
			get
			{
				if (reader == null)
				{
					throw new InvalidOperationException();
				}
				CompileUnitEntry[] array = new CompileUnitEntry[CompileUnitCount];
				for (int i = 0; i < CompileUnitCount; i++)
				{
					array[i] = GetCompileUnit(i + 1);
				}
				return array;
			}
		}

		public MethodEntry[] Methods
		{
			get
			{
				if (reader == null)
				{
					throw new InvalidOperationException();
				}
				lock (this)
				{
					read_methods();
					MethodEntry[] array = new MethodEntry[MethodCount];
					method_list.CopyTo(array, 0);
					return array;
				}
			}
		}

		internal MyBinaryReader BinaryReader
		{
			get
			{
				if (reader == null)
				{
					throw new InvalidOperationException();
				}
				return reader;
			}
		}

		public MonoSymbolFile()
		{
			ot = new OffsetTable();
		}

		public int AddSource(SourceFileEntry source)
		{
			sources.Add(source);
			return sources.Count;
		}

		public int AddCompileUnit(CompileUnitEntry entry)
		{
			comp_units.Add(entry);
			return comp_units.Count;
		}

		public void AddMethod(MethodEntry entry)
		{
			methods.Add(entry);
		}

		public MethodEntry DefineMethod(CompileUnitEntry comp_unit, int token, ScopeVariable[] scope_vars, LocalVariableEntry[] locals, LineNumberEntry[] lines, CodeBlockEntry[] code_blocks, string real_name, MethodEntry.Flags flags, int namespace_id)
		{
			if (reader != null)
			{
				throw new InvalidOperationException();
			}
			MethodEntry methodEntry = new MethodEntry(this, comp_unit, token, scope_vars, locals, lines, code_blocks, real_name, flags, namespace_id);
			AddMethod(methodEntry);
			return methodEntry;
		}

		internal void DefineAnonymousScope(int id)
		{
			if (reader != null)
			{
				throw new InvalidOperationException();
			}
			if (anonymous_scopes == null)
			{
				anonymous_scopes = new Dictionary<int, AnonymousScopeEntry>();
			}
			anonymous_scopes.Add(id, new AnonymousScopeEntry(id));
		}

		internal void DefineCapturedVariable(int scope_id, string name, string captured_name, CapturedVariable.CapturedKind kind)
		{
			if (reader != null)
			{
				throw new InvalidOperationException();
			}
			AnonymousScopeEntry anonymousScopeEntry = anonymous_scopes[scope_id];
			anonymousScopeEntry.AddCapturedVariable(name, captured_name, kind);
		}

		internal void DefineCapturedScope(int scope_id, int id, string captured_name)
		{
			if (reader != null)
			{
				throw new InvalidOperationException();
			}
			AnonymousScopeEntry anonymousScopeEntry = anonymous_scopes[scope_id];
			anonymousScopeEntry.AddCapturedScope(id, captured_name);
		}

		internal int GetNextTypeIndex()
		{
			return ++last_type_index;
		}

		internal int GetNextMethodIndex()
		{
			return ++last_method_index;
		}

		internal int GetNextNamespaceIndex()
		{
			return ++last_namespace_index;
		}

		private void Write(MyBinaryWriter bw, Guid guid)
		{
			bw.Write(5037318119232611860L);
			bw.Write(MajorVersion);
			bw.Write(MinorVersion);
			bw.Write(guid.ToByteArray());
			long position = bw.BaseStream.Position;
			ot.Write(bw, MajorVersion, MinorVersion);
			methods.Sort();
			for (int i = 0; i < methods.Count; i++)
			{
				methods[i].Index = i + 1;
			}
			ot.DataSectionOffset = (int)bw.BaseStream.Position;
			foreach (SourceFileEntry source in sources)
			{
				source.WriteData(bw);
			}
			foreach (CompileUnitEntry comp_unit in comp_units)
			{
				comp_unit.WriteData(bw);
			}
			foreach (MethodEntry method in methods)
			{
				method.WriteData(this, bw);
			}
			ot.DataSectionSize = (int)bw.BaseStream.Position - ot.DataSectionOffset;
			ot.MethodTableOffset = (int)bw.BaseStream.Position;
			for (int j = 0; j < methods.Count; j++)
			{
				MethodEntry methodEntry = methods[j];
				methodEntry.Write(bw);
			}
			ot.MethodTableSize = (int)bw.BaseStream.Position - ot.MethodTableOffset;
			ot.SourceTableOffset = (int)bw.BaseStream.Position;
			for (int k = 0; k < sources.Count; k++)
			{
				SourceFileEntry sourceFileEntry = sources[k];
				sourceFileEntry.Write(bw);
			}
			ot.SourceTableSize = (int)bw.BaseStream.Position - ot.SourceTableOffset;
			ot.CompileUnitTableOffset = (int)bw.BaseStream.Position;
			for (int l = 0; l < comp_units.Count; l++)
			{
				CompileUnitEntry compileUnitEntry = comp_units[l];
				compileUnitEntry.Write(bw);
			}
			ot.CompileUnitTableSize = (int)bw.BaseStream.Position - ot.CompileUnitTableOffset;
			ot.AnonymousScopeCount = ((anonymous_scopes != null) ? anonymous_scopes.Count : 0);
			ot.AnonymousScopeTableOffset = (int)bw.BaseStream.Position;
			if (anonymous_scopes != null)
			{
				foreach (AnonymousScopeEntry value in anonymous_scopes.Values)
				{
					value.Write(bw);
				}
			}
			ot.AnonymousScopeTableSize = (int)bw.BaseStream.Position - ot.AnonymousScopeTableOffset;
			ot.TypeCount = last_type_index;
			ot.MethodCount = methods.Count;
			ot.SourceCount = sources.Count;
			ot.CompileUnitCount = comp_units.Count;
			ot.TotalFileSize = (int)bw.BaseStream.Position;
			bw.Seek((int)position, SeekOrigin.Begin);
			ot.Write(bw, MajorVersion, MinorVersion);
			bw.Seek(0, SeekOrigin.End);
		}

		public void CreateSymbolFile(Guid guid, FileStream fs)
		{
			if (reader != null)
			{
				throw new InvalidOperationException();
			}
			Write(new MyBinaryWriter(fs), guid);
		}

		private MonoSymbolFile(Stream stream)
		{
			reader = new MyBinaryReader(stream);
			try
			{
				long num = reader.ReadInt64();
				int num2 = reader.ReadInt32();
				int num3 = reader.ReadInt32();
				if (num != 5037318119232611860L)
				{
					throw new MonoSymbolFileException("Symbol file is not a valid");
				}
				if (num2 != 50)
				{
					throw new MonoSymbolFileException("Symbol file has version {0} but expected {1}", num2, 50);
				}
				if (num3 != 0)
				{
					throw new MonoSymbolFileException("Symbol file has version {0}.{1} but expected {2}.{3}", num2, num3, 50, 0);
				}
				MajorVersion = num2;
				MinorVersion = num3;
				guid = new Guid(reader.ReadBytes(16));
				ot = new OffsetTable(reader, num2, num3);
			}
			catch (Exception innerException)
			{
				throw new MonoSymbolFileException("Cannot read symbol file", innerException);
			}
			source_file_hash = new Dictionary<int, SourceFileEntry>();
			compile_unit_hash = new Dictionary<int, CompileUnitEntry>();
		}

		public static MonoSymbolFile ReadSymbolFile(Assembly assembly)
		{
			string location = assembly.Location;
			string mdbFilename = location + ".mdb";
			Module[] modules = assembly.GetModules();
			Guid moduleVersionId = modules[0].ModuleVersionId;
			return ReadSymbolFile(mdbFilename, moduleVersionId);
		}

		public static MonoSymbolFile ReadSymbolFile(string mdbFilename)
		{
			return ReadSymbolFile(new FileStream(mdbFilename, FileMode.Open, FileAccess.Read));
		}

		public static MonoSymbolFile ReadSymbolFile(string mdbFilename, Guid assemblyGuid)
		{
			MonoSymbolFile monoSymbolFile = ReadSymbolFile(mdbFilename);
			if (assemblyGuid != monoSymbolFile.guid)
			{
				throw new MonoSymbolFileException("Symbol file `{0}' does not match assembly", mdbFilename);
			}
			return monoSymbolFile;
		}

		public static MonoSymbolFile ReadSymbolFile(Stream stream)
		{
			return new MonoSymbolFile(stream);
		}

		public SourceFileEntry GetSourceFile(int index)
		{
			if (index < 1 || index > ot.SourceCount)
			{
				throw new ArgumentException();
			}
			if (reader == null)
			{
				throw new InvalidOperationException();
			}
			lock (this)
			{
				if (source_file_hash.TryGetValue(index, out var value))
				{
					return value;
				}
				long position = reader.BaseStream.Position;
				reader.BaseStream.Position = ot.SourceTableOffset + SourceFileEntry.Size * (index - 1);
				value = new SourceFileEntry(this, reader);
				source_file_hash.Add(index, value);
				reader.BaseStream.Position = position;
				return value;
			}
		}

		public CompileUnitEntry GetCompileUnit(int index)
		{
			if (index < 1 || index > ot.CompileUnitCount)
			{
				throw new ArgumentException();
			}
			if (reader == null)
			{
				throw new InvalidOperationException();
			}
			lock (this)
			{
				if (compile_unit_hash.TryGetValue(index, out var value))
				{
					return value;
				}
				long position = reader.BaseStream.Position;
				reader.BaseStream.Position = ot.CompileUnitTableOffset + CompileUnitEntry.Size * (index - 1);
				value = new CompileUnitEntry(this, reader);
				compile_unit_hash.Add(index, value);
				reader.BaseStream.Position = position;
				return value;
			}
		}

		private void read_methods()
		{
			lock (this)
			{
				if (method_token_hash == null)
				{
					method_token_hash = new Dictionary<int, MethodEntry>();
					method_list = new List<MethodEntry>();
					long position = reader.BaseStream.Position;
					reader.BaseStream.Position = ot.MethodTableOffset;
					for (int i = 0; i < MethodCount; i++)
					{
						MethodEntry methodEntry = new MethodEntry(this, reader, i + 1);
						method_token_hash.Add(methodEntry.Token, methodEntry);
						method_list.Add(methodEntry);
					}
					reader.BaseStream.Position = position;
				}
			}
		}

		public MethodEntry GetMethodByToken(int token)
		{
			if (reader == null)
			{
				throw new InvalidOperationException();
			}
			lock (this)
			{
				read_methods();
				method_token_hash.TryGetValue(token, out var value);
				return value;
			}
		}

		public MethodEntry GetMethod(int index)
		{
			if (index < 1 || index > ot.MethodCount)
			{
				throw new ArgumentException();
			}
			if (reader == null)
			{
				throw new InvalidOperationException();
			}
			lock (this)
			{
				read_methods();
				return method_list[index - 1];
			}
		}

		public int FindSource(string file_name)
		{
			if (reader == null)
			{
				throw new InvalidOperationException();
			}
			lock (this)
			{
				if (source_name_hash == null)
				{
					source_name_hash = new Dictionary<string, int>();
					for (int i = 0; i < ot.SourceCount; i++)
					{
						SourceFileEntry sourceFile = GetSourceFile(i + 1);
						source_name_hash.Add(sourceFile.FileName, i);
					}
				}
				if (!source_name_hash.TryGetValue(file_name, out var value))
				{
					return -1;
				}
				return value;
			}
		}

		public AnonymousScopeEntry GetAnonymousScope(int id)
		{
			if (reader == null)
			{
				throw new InvalidOperationException();
			}
			lock (this)
			{
				if (anonymous_scopes != null)
				{
					anonymous_scopes.TryGetValue(id, out var value);
					return value;
				}
				anonymous_scopes = new Dictionary<int, AnonymousScopeEntry>();
				reader.BaseStream.Position = ot.AnonymousScopeTableOffset;
				for (int i = 0; i < ot.AnonymousScopeCount; i++)
				{
					AnonymousScopeEntry value = new AnonymousScopeEntry(reader);
					anonymous_scopes.Add(value.ID, value);
				}
				return anonymous_scopes[id];
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing && reader != null)
			{
				reader.Close();
				reader = null;
			}
		}
	}
	public class OffsetTable
	{
		[Flags]
		public enum Flags
		{
			IsAspxSource = 1,
			WindowsFileNames = 2
		}

		public const int MajorVersion = 50;

		public const int MinorVersion = 0;

		public const long Magic = 5037318119232611860L;

		public int TotalFileSize;

		public int DataSectionOffset;

		public int DataSectionSize;

		public int CompileUnitCount;

		public int CompileUnitTableOffset;

		public int CompileUnitTableSize;

		public int SourceCount;

		public int SourceTableOffset;

		public int SourceTableSize;

		public int MethodCount;

		public int MethodTableOffset;

		public int MethodTableSize;

		public int TypeCount;

		public int AnonymousScopeCount;

		public int AnonymousScopeTableOffset;

		public int AnonymousScopeTableSize;

		public Flags FileFlags;

		public int LineNumberTable_LineBase = -1;

		public int LineNumberTable_LineRange = 8;

		public int LineNumberTable_OpcodeBase = 9;

		internal OffsetTable()
		{
			int platform = (int)Environment.OSVersion.Platform;
			if (platform != 4 && platform != 128)
			{
				FileFlags |= Flags.WindowsFileNames;
			}
		}

		internal OffsetTable(BinaryReader reader, int major_version, int minor_version)
		{
			TotalFileSize = reader.ReadInt32();
			DataSectionOffset = reader.ReadInt32();
			DataSectionSize = reader.ReadInt32();
			CompileUnitCount = reader.ReadInt32();
			CompileUnitTableOffset = reader.ReadInt32();
			CompileUnitTableSize = reader.ReadInt32();
			SourceCount = reader.ReadInt32();
			SourceTableOffset = reader.ReadInt32();
			SourceTableSize = reader.ReadInt32();
			MethodCount = reader.ReadInt32();
			MethodTableOffset = reader.ReadInt32();
			MethodTableSize = reader.ReadInt32();
			TypeCount = reader.ReadInt32();
			AnonymousScopeCount = reader.ReadInt32();
			AnonymousScopeTableOffset = reader.ReadInt32();
			AnonymousScopeTableSize = reader.ReadInt32();
			LineNumberTable_LineBase = reader.ReadInt32();
			LineNumberTable_LineRange = reader.ReadInt32();
			LineNumberTable_OpcodeBase = reader.ReadInt32();
			FileFlags = (Flags)reader.ReadInt32();
		}

		internal void Write(BinaryWriter bw, int major_version, int minor_version)
		{
			bw.Write(TotalFileSize);
			bw.Write(DataSectionOffset);
			bw.Write(DataSectionSize);
			bw.Write(CompileUnitCount);
			bw.Write(CompileUnitTableOffset);
			bw.Write(CompileUnitTableSize);
			bw.Write(SourceCount);
			bw.Write(SourceTableOffset);
			bw.Write(SourceTableSize);
			bw.Write(MethodCount);
			bw.Write(MethodTableOffset);
			bw.Write(MethodTableSize);
			bw.Write(TypeCount);
			bw.Write(AnonymousScopeCount);
			bw.Write(AnonymousScopeTableOffset);
			bw.Write(AnonymousScopeTableSize);
			bw.Write(LineNumberTable_LineBase);
			bw.Write(LineNumberTable_LineRange);
			bw.Write(LineNumberTable_OpcodeBase);
			bw.Write((int)FileFlags);
		}

		public override string ToString()
		{
			return $"OffsetTable [{TotalFileSize} - {DataSectionOffset}:{DataSectionSize} - {SourceCount}:{SourceTableOffset}:{SourceTableSize} - {MethodCount}:{MethodTableOffset}:{MethodTableSize} - {TypeCount}]";
		}
	}
	public class LineNumberEntry
	{
		public sealed class LocationComparer : IComparer<LineNumberEntry>
		{
			public static readonly LocationComparer Default = new LocationComparer();

			public int Compare(LineNumberEntry l1, LineNumberEntry l2)
			{
				int result;
				if (l1.Row != l2.Row)
				{
					int row = l1.Row;
					result = row.CompareTo(l2.Row);
				}
				else
				{
					result = l1.Column.CompareTo(l2.Column);
				}
				return result;
			}
		}

		public readonly int Row;

		public int Column;

		public int EndRow;

		public int EndColumn;

		public readonly int File;

		public readonly int Offset;

		public readonly bool IsHidden;

		public static readonly LineNumberEntry Null = new LineNumberEntry(0, 0, 0, 0);

		public LineNumberEntry(int file, int row, int column, int offset)
			: this(file, row, column, offset, is_hidden: false)
		{
		}

		public LineNumberEntry(int file, int row, int offset)
			: this(file, row, -1, offset, is_hidden: false)
		{
		}

		public LineNumberEntry(int file, int row, int column, int offset, bool is_hidden)
			: this(file, row, column, -1, -1, offset, is_hidden)
		{
		}

		public LineNumberEntry(int file, int row, int column, int end_row, int end_column, int offset, bool is_hidden)
		{
			File = file;
			Row = row;
			Column = column;
			EndRow = end_row;
			EndColumn = end_column;
			Offset = offset;
			IsHidden = is_hidden;
		}

		public override string ToString()
		{
			return $"[Line {File}:{Row},{Column}-{EndRow},{EndColumn}:{Offset}]";
		}
	}
	public class CodeBlockEntry
	{
		public enum Type
		{
			Lexical = 1,
			CompilerGenerated,
			IteratorBody,
			IteratorDispatcher
		}

		public int Index;

		public int Parent;

		public Type BlockType;

		public int StartOffset;

		public int EndOffset;

		public CodeBlockEntry(int index, int parent, Type type, int start_offset)
		{
			Index = index;
			Parent = parent;
			BlockType = type;
			StartOffset = start_offset;
		}

		internal CodeBlockEntry(int index, MyBinaryReader reader)
		{
			Index = index;
			int num = reader.ReadLeb128();
			BlockType = (Type)(num & 0x3F);
			Parent = reader.ReadLeb128();
			StartOffset = reader.ReadLeb128();
			EndOffset = reader.ReadLeb128();
			if ((num & 0x40) != 0)
			{
				int num2 = reader.ReadInt16();
				reader.BaseStream.Position += num2;
			}
		}

		public void Close(int end_offset)
		{
			EndOffset = end_offset;
		}

		internal void Write(MyBinaryWriter bw)
		{
			bw.WriteLeb128((int)BlockType);
			bw.WriteLeb128(Parent);
			bw.WriteLeb128(StartOffset);
			bw.WriteLeb128(EndOffset);
		}

		public override string ToString()
		{
			return $"[CodeBlock {Index}:{Parent}:{BlockType}:{StartOffset}:{EndOffset}]";
		}
	}
	public struct LocalVariableEntry
	{
		public readonly int Index;

		public readonly string Name;

		public readonly int BlockIndex;

		public LocalVariableEntry(int index, string name, int block)
		{
			Index = index;
			Name = name;
			BlockIndex = block;
		}

		internal LocalVariableEntry(MonoSymbolFile file, MyBinaryReader reader)
		{
			Index = reader.ReadLeb128();
			Name = reader.ReadString();
			BlockIndex = reader.ReadLeb128();
		}

		internal void Write(MonoSymbolFile file, MyBinaryWriter bw)
		{
			bw.WriteLeb128(Index);
			bw.Write(Name);
			bw.WriteLeb128(BlockIndex);
		}

		public override string ToString()
		{
			return $"[LocalVariable {Name}:{Index}:{BlockIndex - 1}]";
		}
	}
	public struct CapturedVariable
	{
		public enum CapturedKind : byte
		{
			Local,
			Parameter,
			This
		}

		public readonly string Name;

		public readonly string CapturedName;

		public readonly CapturedKind Kind;

		public CapturedVariable(string name, string captured_name, CapturedKind kind)
		{
			Name = name;
			CapturedName = captured_name;
			Kind = kind;
		}

		internal CapturedVariable(MyBinaryReader reader)
		{
			Name = reader.ReadString();
			CapturedName = reader.ReadString();
			Kind = (CapturedKind)reader.ReadByte();
		}

		internal void Write(MyBinaryWriter bw)
		{
			bw.Write(Name);
			bw.Write(CapturedName);
			bw.Write((byte)Kind);
		}

		public override string ToString()
		{
			return $"[CapturedVariable {Name}:{CapturedName}:{Kind}]";
		}
	}
	public struct CapturedScope
	{
		public readonly int Scope;

		public readonly string CapturedName;

		public CapturedScope(int scope, string captured_name)
		{
			Scope = scope;
			CapturedName = captured_name;
		}

		internal CapturedScope(MyBinaryReader reader)
		{
			Scope = reader.ReadLeb128();
			CapturedName = reader.ReadString();
		}

		internal void Write(MyBinaryWriter bw)
		{
			bw.WriteLeb128(Scope);
			bw.Write(CapturedName);
		}

		public override string ToString()
		{
			return $"[CapturedScope {Scope}:{CapturedName}]";
		}
	}
	public struct ScopeVariable
	{
		public readonly int Scope;

		public readonly int Index;

		public ScopeVariable(int scope, int index)
		{
			Scope = scope;
			Index = index;
		}

		internal ScopeVariable(MyBinaryReader reader)
		{
			Scope = reader.ReadLeb128();
			Index = reader.ReadLeb128();
		}

		internal void Write(MyBinaryWriter bw)
		{
			bw.WriteLeb128(Scope);
			bw.WriteLeb128(Index);
		}

		public override string ToString()
		{
			return $"[ScopeVariable {Scope}:{Index}]";
		}
	}
	public class AnonymousScopeEntry
	{
		public readonly int ID;

		private List<CapturedVariable> captured_vars = new List<CapturedVariable>();

		private List<CapturedScope> captured_scopes = new List<CapturedScope>();

		public CapturedVariable[] CapturedVariables
		{
			get
			{
				CapturedVariable[] array = new CapturedVariable[captured_vars.Count];
				captured_vars.CopyTo(array, 0);
				return array;
			}
		}

		public CapturedScope[] CapturedScopes
		{
			get
			{
				CapturedScope[] array = new CapturedScope[captured_scopes.Count];
				captured_scopes.CopyTo(array, 0);
				return array;
			}
		}

		public AnonymousScopeEntry(int id)
		{
			ID = id;
		}

		internal AnonymousScopeEntry(MyBinaryReader reader)
		{
			ID = reader.ReadLeb128();
			int num = reader.ReadLeb128();
			for (int i = 0; i < num; i++)
			{
				captured_vars.Add(new CapturedVariable(reader));
			}
			int num2 = reader.ReadLeb128();
			for (int j = 0; j < num2; j++)
			{
				captured_scopes.Add(new CapturedScope(reader));
			}
		}

		internal void AddCapturedVariable(string name, string captured_name, CapturedVariable.CapturedKind kind)
		{
			captured_vars.Add(new CapturedVariable(name, captured_name, kind));
		}

		internal void AddCapturedScope(int scope, string captured_name)
		{
			captured_scopes.Add(new CapturedScope(scope, captured_name));
		}

		internal void Write(MyBinaryWriter bw)
		{
			bw.WriteLeb128(ID);
			bw.WriteLeb128(captured_vars.Count);
			foreach (CapturedVariable captured_var in captured_vars)
			{
				captured_var.Write(bw);
			}
			bw.WriteLeb128(captured_scopes.Count);
			foreach (CapturedScope captured_scope in captured_scopes)
			{
				captured_scope.Write(bw);
			}
		}

		public override string ToString()
		{
			return $"[AnonymousScope {ID}]";
		}
	}
	public class CompileUnitEntry : ICompileUnit
	{
		public readonly int Index;

		private int DataOffset;

		private MonoSymbolFile file;

		private SourceFileEntry source;

		private List<SourceFileEntry> include_files;

		private List<NamespaceEntry> namespaces;

		private bool creating;

		public static int Size => 8;

		CompileUnitEntry ICompileUnit.Entry => this;

		public SourceFileEntry SourceFile
		{
			get
			{
				if (creating)
				{
					return source;
				}
				ReadData();
				return source;
			}
		}

		public NamespaceEntry[] Namespaces
		{
			get
			{
				ReadData();
				NamespaceEntry[] array = new NamespaceEntry[namespaces.Count];
				namespaces.CopyTo(array, 0);
				return array;
			}
		}

		public SourceFileEntry[] IncludeFiles
		{
			get
			{
				ReadData();
				if (include_files == null)
				{
					return new SourceFileEntry[0];
				}
				SourceFileEntry[] array = new SourceFileEntry[include_files.Count];
				include_files.CopyTo(array, 0);
				return array;
			}
		}

		public CompileUnitEntry(MonoSymbolFile file, SourceFileEntry source)
		{
			this.file = file;
			this.source = source;
			Index = file.AddCompileUnit(this);
			creating = true;
			namespaces = new List<NamespaceEntry>();
		}

		public void AddFile(SourceFileEntry file)
		{
			if (!creating)
			{
				throw new InvalidOperationException();
			}
			if (include_files == null)
			{
				include_files = new List<SourceFileEntry>();
			}
			include_files.Add(file);
		}

		public int DefineNamespace(string name, string[] using_clauses, int parent)
		{
			if (!creating)
			{
				throw new InvalidOperationException();
			}
			int nextNamespaceIndex = file.GetNextNamespaceIndex();
			NamespaceEntry item = new NamespaceEntry(name, nextNamespaceIndex, using_clauses, parent);
			namespaces.Add(item);
			return nextNamespaceIndex;
		}

		internal void WriteData(MyBinaryWriter bw)
		{
			DataOffset = (int)bw.BaseStream.Position;
			bw.WriteLeb128(source.Index);
			int value = ((include_files != null) ? include_files.Count : 0);
			bw.WriteLeb128(value);
			if (include_files != null)
			{
				foreach (SourceFileEntry include_file in include_files)
				{
					bw.WriteLeb128(include_file.Index);
				}
			}
			bw.WriteLeb128(namespaces.Count);
			foreach (NamespaceEntry @namespace in namespaces)
			{
				@namespace.Write(file, bw);
			}
		}

		internal void Write(BinaryWriter bw)
		{
			bw.Write(Index);
			bw.Write(DataOffset);
		}

		internal CompileUnitEntry(MonoSymbolFile file, MyBinaryReader reader)
		{
			this.file = file;
			Index = reader.ReadInt32();
			DataOffset = reader.ReadInt32();
		}

		public void ReadAll()
		{
			ReadData();
		}

		private void ReadData()
		{
			if (creating)
			{
				throw new InvalidOperationException();
			}
			lock (file)
			{
				if (namespaces != null)
				{
					return;
				}
				MyBinaryReader binaryReader = file.BinaryReader;
				int num = (int)binaryReader.BaseStream.Position;
				binaryReader.BaseStream.Position = DataOffset;
				int index = binaryReader.ReadLeb128();
				source = file.GetSourceFile(index);
				int num2 = binaryReader.ReadLeb128();
				if (num2 > 0)
				{
					include_files = new List<SourceFileEntry>();
					for (int i = 0; i < num2; i++)
					{
						include_files.Add(file.GetSourceFile(binaryReader.ReadLeb128()));
					}
				}
				int num3 = binaryReader.ReadLeb128();
				namespaces = new List<NamespaceEntry>();
				for (int j = 0; j < num3; j++)
				{
					namespaces.Add(new NamespaceEntry(file, binaryReader));
				}
				binaryReader.BaseStream.Position = num;
			}
		}
	}
	public class SourceFileEntry
	{
		public readonly int Index;

		private int DataOffset;

		private MonoSymbolFile file;

		private string file_name;

		private byte[] guid;

		private byte[] hash;

		private bool creating;

		private bool auto_generated;

		private readonly string sourceFile;

		public static int Size => 8;

		public byte[] Checksum => hash;

		public string FileName
		{
			get
			{
				return file_name;
			}
			set
			{
				file_name = value;
			}
		}

		public bool AutoGenerated => auto_generated;

		public SourceFileEntry(MonoSymbolFile file, string file_name)
		{
			this.file = file;
			this.file_name = file_name;
			Index = file.AddSource(this);
			creating = true;
		}

		public SourceFileEntry(MonoSymbolFile file, string sourceFile, byte[] guid, byte[] checksum)
			: this(file, sourceFile, sourceFile, guid, checksum)
		{
		}

		public SourceFileEntry(MonoSymbolFile file, string fileName, string sourceFile, byte[] guid, byte[] checksum)
			: this(file, fileName)
		{
			this.guid = guid;
			hash = checksum;
			this.sourceFile = sourceFile;
		}

		internal void WriteData(MyBinaryWriter bw)
		{
			DataOffset = (int)bw.BaseStream.Position;
			bw.Write(file_name);
			if (guid == null)
			{
				guid = new byte[16];
			}
			if (hash == null)
			{
				try
				{
					using FileStream inputStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
					MD5 mD = MD5.Create();
					hash = mD.ComputeHash(inputStream);
				}
				catch
				{
					hash = new byte[16];
				}
			}
			bw.Write(guid);
			bw.Write(hash);
			bw.Write((byte)(auto_generated ? 1u : 0u));
		}

		internal void Write(BinaryWriter bw)
		{
			bw.Write(Index);
			bw.Write(DataOffset);
		}

		internal SourceFileEntry(MonoSymbolFile file, MyBinaryReader reader)
		{
			this.file = file;
			Index = reader.ReadInt32();
			DataOffset = reader.ReadInt32();
			int num = (int)reader.BaseStream.Position;
			reader.BaseStream.Position = DataOffset;
			sourceFile = (file_name = reader.ReadString());
			guid = reader.ReadBytes(16);
			hash = reader.ReadBytes(16);
			auto_generated = reader.ReadByte() == 1;
			reader.BaseStream.Position = num;
		}

		public void SetAutoGenerated()
		{
			if (!creating)
			{
				throw new InvalidOperationException();
			}
			auto_generated = true;
			file.OffsetTable.FileFlags |= OffsetTable.Flags.IsAspxSource;
		}

		public bool CheckChecksum()
		{
			try
			{
				using FileStream inputStream = new FileStream(sourceFile, FileMode.Open);
				MD5 mD = MD5.Create();
				byte[] array = mD.ComputeHash(inputStream);
				for (int i = 0; i < 16; i++)
				{
					if (array[i] != hash[i])
					{
						return false;
					}
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public override string ToString()
		{
			return $"SourceFileEntry ({Index}:{DataOffset})";
		}
	}
	public class LineNumberTable
	{
		protected LineNumberEntry[] _line_numbers;

		public readonly int LineBase;

		public readonly int LineRange;

		public readonly byte OpcodeBase;

		public readonly int MaxAddressIncrement;

		public const int Default_LineBase = -1;

		public const int Default_LineRange = 8;

		public const byte Default_OpcodeBase = 9;

		public const byte DW_LNS_copy = 1;

		public const byte DW_LNS_advance_pc = 2;

		public const byte DW_LNS_advance_line = 3;

		public const byte DW_LNS_set_file = 4;

		public const byte DW_LNS_const_add_pc = 8;

		public const byte DW_LNE_end_sequence = 1;

		public const byte DW_LNE_MONO_negate_is_hidden = 64;

		internal const byte DW_LNE_MONO__extensions_start = 64;

		internal const byte DW_LNE_MONO__extensions_end = 127;

		public LineNumberEntry[] LineNumbers => _line_numbers;

		protected LineNumberTable(MonoSymbolFile file)
		{
			LineBase = file.OffsetTable.LineNumberTable_LineBase;
			LineRange = file.OffsetTable.LineNumberTable_LineRange;
			OpcodeBase = (byte)file.OffsetTable.LineNumberTable_OpcodeBase;
			MaxAddressIncrement = (255 - OpcodeBase) / LineRange;
		}

		internal LineNumberTable(MonoSymbolFile file, LineNumberEntry[] lines)
			: this(file)
		{
			_line_numbers = lines;
		}

		internal void Write(MonoSymbolFile file, MyBinaryWriter bw, bool hasColumnsInfo, bool hasEndInfo)
		{
			int num = (int)bw.BaseStream.Position;
			bool flag = false;
			int num2 = 1;
			int num3 = 0;
			int num4 = 1;
			for (int i = 0; i < LineNumbers.Length; i++)
			{
				int num5 = LineNumbers[i].Row - num2;
				int num6 = LineNumbers[i].Offset - num3;
				if (LineNumbers[i].File != num4)
				{
					bw.Write((byte)4);
					bw.WriteLeb128(LineNumbers[i].File);
					num4 = LineNumbers[i].File;
				}
				if (LineNumbers[i].IsHidden != flag)
				{
					bw.Write((byte)0);
					bw.Write((byte)1);
					bw.Write((byte)64);
					flag = LineNumbers[i].IsHidden;
				}
				if (num6 >= MaxAddressIncrement)
				{
					if (num6 < 2 * MaxAddressIncrement)
					{
						bw.Write((byte)8);
						num6 -= MaxAddressIncrement;
					}
					else
					{
						bw.Write((byte)2);
						bw.WriteLeb128(num6);
						num6 = 0;
					}
				}
				if (num5 < LineBase || num5 >= LineBase + LineRange)
				{
					bw.Write((byte)3);
					bw.WriteLeb128(num5);
					if (num6 != 0)
					{
						bw.Write((byte)2);
						bw.WriteLeb128(num6);
					}
					bw.Write((byte)1);
				}
				else
				{
					byte value = (byte)(num5 - LineBase + LineRange * num6 + OpcodeBase);
					bw.Write(value);
				}
				num2 = LineNumbers[i].Row;
				num3 = LineNumbers[i].Offset;
			}
			bw.Write((byte)0);
			bw.Write((byte)1);
			bw.Write((byte)1);
			if (hasColumnsInfo)
			{
				for (int j = 0; j < LineNumbers.Length; j++)
				{
					LineNumberEntry lineNumberEntry = LineNumbers[j];
					if (lineNumberEntry.Row >= 0)
					{
						bw.WriteLeb128(lineNumberEntry.Column);
					}
				}
			}
			if (hasEndInfo)
			{
				for (int k = 0; k < LineNumbers.Length; k++)
				{
					LineNumberEntry lineNumberEntry2 = LineNumbers[k];
					if (lineNumberEntry2.EndRow == -1 || lineNumberEntry2.EndColumn == -1 || lineNumberEntry2.Row > lineNumberEntry2.EndRow)
					{
						bw.WriteLeb128(16777215);
						continue;
					}
					bw.WriteLeb128(lineNumberEntry2.EndRow - lineNumberEntry2.Row);
					bw.WriteLeb128(lineNumberEntry2.EndColumn);
				}
			}
			file.ExtendedLineNumberSize += (int)bw.BaseStream.Position - num;
		}

		internal static LineNumberTable Read(MonoSymbolFile file, MyBinaryReader br, bool readColumnsInfo, bool readEndInfo)
		{
			LineNumberTable lineNumberTable = new LineNumberTable(file);
			lineNumberTable.DoRead(file, br, readColumnsInfo, readEndInfo);
			return lineNumberTable;
		}

		private void DoRead(MonoSymbolFile file, MyBinaryReader br, bool includesColumns, bool includesEnds)
		{
			List<LineNumberEntry> list = new List<LineNumberEntry>();
			bool flag = false;
			bool flag2 = false;
			int num = 1;
			int num2 = 0;
			int file2 = 1;
			while (true)
			{
				byte b = br.ReadByte();
				if (b == 0)
				{
					byte b2 = br.ReadByte();
					long position = br.BaseStream.Position + b2;
					b = br.ReadByte();
					if (b == 1)
					{
						if (flag2)
						{
							list.Add(new LineNumberEntry(file2, num, -1, num2, flag));
						}
						break;
					}
					if (b == 64)
					{
						flag = !flag;
						flag2 = true;
					}
					else if (b < 64 || b > 127)
					{
						throw new MonoSymbolFileException("Unknown extended opcode {0:x}", b);
					}
					br.BaseStream.Position = position;
				}
				else if (b < OpcodeBase)
				{
					switch (b)
					{
					case 1:
						list.Add(new LineNumberEntry(file2, num, -1, num2, flag));
						flag2 = false;
						break;
					case 2:
						num2 += br.ReadLeb128();
						flag2 = true;
						break;
					case 3:
						num += br.ReadLeb128();
						flag2 = true;
						break;
					case 4:
						file2 = br.ReadLeb128();
						flag2 = true;
						break;
					case 8:
						num2 += MaxAddressIncrement;
						flag2 = true;
						break;
					default:
						throw new MonoSymbolFileException("Unknown standard opcode {0:x} in LNT", b);
					}
				}
				else
				{
					b -= OpcodeBase;
					num2 += b / LineRange;
					num += LineBase + b % LineRange;
					list.Add(new LineNumberEntry(file2, num, -1, num2, flag));
					flag2 = false;
				}
			}
			_line_numbers = list.ToArray();
			if (includesColumns)
			{
				for (int i = 0; i < _line_numbers.Length; i++)
				{
					LineNumberEntry lineNumberEntry = _line_numbers[i];
					if (lineNumberEntry.Row >= 0)
					{
						lineNumberEntry.Column = br.ReadLeb128();
					}
				}
			}
			if (!includesEnds)
			{
				return;
			}
			for (int j = 0; j < _line_numbers.Length; j++)
			{
				LineNumberEntry lineNumberEntry2 = _line_numbers[j];
				int num3 = br.ReadLeb128();
				if (num3 == 16777215)
				{
					lineNumberEntry2.EndRow = -1;
					lineNumberEntry2.EndColumn = -1;
				}
				else
				{
					lineNumberEntry2.EndRow = lineNumberEntry2.Row + num3;
					lineNumberEntry2.EndColumn = br.ReadLeb128();
				}
			}
		}

		public bool GetMethodBounds(out LineNumberEntry start, out LineNumberEntry end)
		{
			if (_line_numbers.Length > 1)
			{
				start = _line_numbers[0];
				end = _line_numbers[_line_numbers.Length - 1];
				return true;
			}
			start = LineNumberEntry.Null;
			end = LineNumberEntry.Null;
			return false;
		}
	}
	public class MethodEntry : IComparable
	{
		[Flags]
		public enum Flags
		{
			LocalNamesAmbiguous = 1,
			ColumnsInfoIncluded = 2,
			EndInfoIncluded = 4
		}

		public readonly int CompileUnitIndex;

		public readonly int Token;

		public readonly int NamespaceID;

		private int DataOffset;

		private int LocalVariableTableOffset;

		private int LineNumberTableOffset;

		private int CodeBlockTableOffset;

		private int ScopeVariableTableOffset;

		private int RealNameOffset;

		private Flags flags;

		private int index;

		public readonly CompileUnitEntry CompileUnit;

		private LocalVariableEntry[] locals;

		private CodeBlockEntry[] code_blocks;

		private ScopeVariable[] scope_vars;

		private LineNumberTable lnt;

		private string real_name;

		public readonly MonoSymbolFile SymbolFile;

		public const int Size = 12;

		public Flags MethodFlags => flags;

		public int Index
		{
			get
			{
				return index;
			}
			set
			{
				index = value;
			}
		}

		internal MethodEntry(MonoSymbolFile file, MyBinaryReader reader, int index)
		{
			SymbolFile = file;
			this.index = index;
			Token = reader.ReadInt32();
			DataOffset = reader.ReadInt32();
			LineNumberTableOffset = reader.ReadInt32();
			long position = reader.BaseStream.Position;
			reader.BaseStream.Position = DataOffset;
			CompileUnitIndex = reader.ReadLeb128();
			LocalVariableTableOffset = reader.ReadLeb128();
			NamespaceID = reader.ReadLeb128();
			CodeBlockTableOffset = reader.ReadLeb128();
			ScopeVariableTableOffset = reader.ReadLeb128();
			RealNameOffset = reader.ReadLeb128();
			flags = (Flags)reader.ReadLeb128();
			reader.BaseStream.Position = position;
			CompileUnit = file.GetCompileUnit(CompileUnitIndex);
		}

		internal MethodEntry(MonoSymbolFile file, CompileUnitEntry comp_unit, int token, ScopeVariable[] scope_vars, LocalVariableEntry[] locals, LineNumberEntry[] lines, CodeBlockEntry[] code_blocks, string real_name, Flags flags, int namespace_id)
		{
			SymbolFile = file;
			this.real_name = real_name;
			this.locals = locals;
			this.code_blocks = code_blocks;
			this.scope_vars = scope_vars;
			this.flags = flags;
			index = -1;
			Token = token;
			CompileUnitIndex = comp_unit.Index;
			CompileUnit = comp_unit;
			NamespaceID = namespace_id;
			CheckLineNumberTable(lines);
			lnt = new LineNumberTable(file, lines);
			file.NumLineNumbers += lines.Length;
			int num = ((locals != null) ? locals.Length : 0);
			if (num <= 32)
			{
				for (int i = 0; i < num; i++)
				{
					string name = locals[i].Name;
					for (int j = i + 1; j < num; j++)
					{
						if (locals[j].Name == name)
						{
							flags |= Flags.LocalNamesAmbiguous;
							return;
						}
					}
				}
				return;
			}
			Dictionary<string, LocalVariableEntry> dictionary = new Dictionary<string, LocalVariableEntry>();
			for (int k = 0; k < locals.Length; k++)
			{
				LocalVariableEntry value = locals[k];
				if (dictionary.ContainsKey(value.Name))
				{
					flags |= Flags.LocalNamesAmbiguous;
					break;
				}
				dictionary.Add(value.Name, value);
			}
		}

		private static void CheckLineNumberTable(LineNumberEntry[] line_numbers)
		{
			int num = -1;
			int num2 = -1;
			if (line_numbers == null)
			{
				return;
			}
			foreach (LineNumberEntry lineNumberEntry in line_numbers)
			{
				if (lineNumberEntry.Equals(LineNumberEntry.Null))
				{
					throw new MonoSymbolFileException();
				}
				if (lineNumberEntry.Offset < num)
				{
					throw new MonoSymbolFileException();
				}
				if (lineNumberEntry.Offset > num)
				{
					num2 = lineNumberEntry.Row;
					num = lineNumberEntry.Offset;
				}
				else if (lineNumberEntry.Row > num2)
				{
					num2 = lineNumberEntry.Row;
				}
			}
		}

		internal void Write(MyBinaryWriter bw)
		{
			if (index <= 0 || DataOffset == 0)
			{
				throw new InvalidOperationException();
			}
			bw.Write(Token);
			bw.Write(DataOffset);
			bw.Write(LineNumberTableOffset);
		}

		internal void WriteData(MonoSymbolFile file, MyBinaryWriter bw)
		{
			if (index <= 0)
			{
				throw new InvalidOperationException();
			}
			LocalVariableTableOffset = (int)bw.BaseStream.Position;
			int num = ((locals != null) ? locals.Length : 0);
			bw.WriteLeb128(num);
			for (int i = 0; i < num; i++)
			{
				locals[i].Write(file, bw);
			}
			file.LocalCount += num;
			CodeBlockTableOffset = (int)bw.BaseStream.Position;
			int num2 = ((code_blocks != null) ? code_blocks.Length : 0);
			bw.WriteLeb128(num2);
			for (int j = 0; j < num2; j++)
			{
				code_blocks[j].Write(bw);
			}
			ScopeVariableTableOffset = (int)bw.BaseStream.Position;
			int num3 = ((scope_vars != null) ? scope_vars.Length : 0);
			bw.WriteLeb128(num3);
			for (int k = 0; k < num3; k++)
			{
				scope_vars[k].Write(bw);
			}
			if (real_name != null)
			{
				RealNameOffset = (int)bw.BaseStream.Position;
				bw.Write(real_name);
			}
			LineNumberEntry[] lineNumbers = lnt.LineNumbers;
			foreach (LineNumberEntry lineNumberEntry in lineNumbers)
			{
				if (lineNumberEntry.EndRow != -1 || lineNumberEntry.EndColumn != -1)
				{
					flags |= Flags.EndInfoIncluded;
				}
			}
			LineNumberTableOffset = (int)bw.BaseStream.Position;
			lnt.Write(file, bw, (flags & Flags.ColumnsInfoIncluded) != 0, (flags & Flags.EndInfoIncluded) != 0);
			DataOffset = (int)bw.BaseStream.Position;
			bw.WriteLeb128(CompileUnitIndex);
			bw.WriteLeb128(LocalVariableTableOffset);
			bw.WriteLeb128(NamespaceID);
			bw.WriteLeb128(CodeBlockTableOffset);
			bw.WriteLeb128(ScopeVariableTableOffset);
			bw.WriteLeb128(RealNameOffset);
			bw.WriteLeb128((int)flags);
		}

		public void ReadAll()
		{
			GetLineNumberTable();
			GetLocals();
			GetCodeBlocks();
			GetScopeVariables();
			GetRealName();
		}

		public LineNumberTable GetLineNumberTable()
		{
			lock (SymbolFile)
			{
				if (lnt != null)
				{
					return lnt;
				}
				if (LineNumberTableOffset == 0)
				{
					return null;
				}
				MyBinaryReader binaryReader = SymbolFile.BinaryReader;
				long position = binaryReader.BaseStream.Position;
				binaryReader.BaseStream.Position = LineNumberTableOffset;
				lnt = LineNumberTable.Read(SymbolFile, binaryReader, (flags & Flags.ColumnsInfoIncluded) != 0, (flags & Flags.EndInfoIncluded) != 0);
				binaryReader.BaseStream.Position = position;
				return lnt;
			}
		}

		public LocalVariableEntry[] GetLocals()
		{
			lock (SymbolFile)
			{
				if (locals != null)
				{
					return locals;
				}
				if (LocalVariableTableOffset == 0)
				{
					return null;
				}
				MyBinaryReader binaryReader = SymbolFile.BinaryReader;
				long position = binaryReader.BaseStream.Position;
				binaryReader.BaseStream.Position = LocalVariableTableOffset;
				int num = binaryReader.ReadLeb128();
				locals = new LocalVariableEntry[num];
				for (int i = 0; i < num; i++)
				{
					locals[i] = new LocalVariableEntry(SymbolFile, binaryReader);
				}
				binaryReader.BaseStream.Position = position;
				return locals;
			}
		}

		public CodeBlockEntry[] GetCodeBlocks()
		{
			lock (SymbolFile)
			{
				if (code_blocks != null)
				{
					return code_blocks;
				}
				if (CodeBlockTableOffset == 0)
				{
					return null;
				}
				MyBinaryReader binaryReader = SymbolFile.BinaryReader;
				long position = binaryReader.BaseStream.Position;
				binaryReader.BaseStream.Position = CodeBlockTableOffset;
				int num = binaryReader.ReadLeb128();
				code_blocks = new CodeBlockEntry[num];
				for (int i = 0; i < num; i++)
				{
					code_blocks[i] = new CodeBlockEntry(i, binaryReader);
				}
				binaryReader.BaseStream.Position = position;
				return code_blocks;
			}
		}

		public ScopeVariable[] GetScopeVariables()
		{
			lock (SymbolFile)
			{
				if (scope_vars != null)
				{
					return scope_vars;
				}
				if (ScopeVariableTableOffset == 0)
				{
					return null;
				}
				MyBinaryReader binaryReader = SymbolFile.BinaryReader;
				long position = binaryReader.BaseStream.Position;
				binaryReader.BaseStream.Position = ScopeVariableTableOffset;
				int num = binaryReader.ReadLeb128();
				scope_vars = new ScopeVariable[num];
				for (int i = 0; i < num; i++)
				{
					scope_vars[i] = new ScopeVariable(binaryReader);
				}
				binaryReader.BaseStream.Position = position;
				return scope_vars;
			}
		}

		public string GetRealName()
		{
			lock (SymbolFile)
			{
				if (real_name != null)
				{
					return real_name;
				}
				if (RealNameOffset == 0)
				{
					return null;
				}
				real_name = SymbolFile.BinaryReader.ReadString(RealNameOffset);
				return real_name;
			}
		}

		public int CompareTo(object obj)
		{
			MethodEntry methodEntry = (MethodEntry)obj;
			if (methodEntry.Token < Token)
			{
				return 1;
			}
			if (methodEntry.Token > Token)
			{
				return -1;
			}
			return 0;
		}

		public override string ToString()
		{
			return $"[Method {index}:{Token:x}:{CompileUnitIndex}:{CompileUnit}]";
		}
	}
	public struct NamespaceEntry
	{
		public readonly string Name;

		public readonly int Index;

		public readonly int Parent;

		public readonly string[] UsingClauses;

		public NamespaceEntry(string name, int index, string[] using_clauses, int parent)
		{
			Name = name;
			Index = index;
			Parent = parent;
			UsingClauses = ((using_clauses != null) ? using_clauses : new string[0]);
		}

		internal NamespaceEntry(MonoSymbolFile file, MyBinaryReader reader)
		{
			Name = reader.ReadString();
			Index = reader.ReadLeb128();
			Parent = reader.ReadLeb128();
			int num = reader.ReadLeb128();
			UsingClauses = new string[num];
			for (int i = 0; i < num; i++)
			{
				UsingClauses[i] = reader.ReadString();
			}
		}

		internal void Write(MonoSymbolFile file, MyBinaryWriter bw)
		{
			bw.Write(Name);
			bw.WriteLeb128(Index);
			bw.WriteLeb128(Parent);
			bw.WriteLeb128(UsingClauses.Length);
			string[] usingClauses = UsingClauses;
			foreach (string value in usingClauses)
			{
				bw.Write(value);
			}
		}

		public override string ToString()
		{
			return $"[Namespace {Name}:{Index}:{Parent}]";
		}
	}
	public class MonoSymbolWriter
	{
		private List<SourceMethodBuilder> methods;

		private List<SourceFileEntry> sources;

		private List<CompileUnitEntry> comp_units;

		protected readonly MonoSymbolFile file;

		private string filename;

		private SourceMethodBuilder current_method;

		private Stack<SourceMethodBuilder> current_method_stack = new Stack<SourceMethodBuilder>();

		public MonoSymbolFile SymbolFile => file;

		public MonoSymbolWriter(string filename)
		{
			methods = new List<SourceMethodBuilder>();
			sources = new List<SourceFileEntry>();
			comp_units = new List<CompileUnitEntry>();
			file = new MonoSymbolFile();
			this.filename = filename + ".mdb";
		}

		public void CloseNamespace()
		{
		}

		public void DefineLocalVariable(int index, string name)
		{
			if (current_method != null)
			{
				current_method.AddLocal(index, name);
			}
		}

		public void DefineCapturedLocal(int scope_id, string name, string captured_name)
		{
			file.DefineCapturedVariable(scope_id, name, captured_name, CapturedVariable.CapturedKind.Local);
		}

		public void DefineCapturedParameter(int scope_id, string name, string captured_name)
		{
			file.DefineCapturedVariable(scope_id, name, captured_name, CapturedVariable.CapturedKind.Parameter);
		}

		public void DefineCapturedThis(int scope_id, string captured_name)
		{
			file.DefineCapturedVariable(scope_id, "this", captured_name, CapturedVariable.CapturedKind.This);
		}

		public void DefineCapturedScope(int scope_id, int id, string captured_name)
		{
			file.DefineCapturedScope(scope_id, id, captured_name);
		}

		public void DefineScopeVariable(int scope, int index)
		{
			if (current_method != null)
			{
				current_method.AddScopeVariable(scope, index);
			}
		}

		public void MarkSequencePoint(int offset, SourceFileEntry file, int line, int column, bool is_hidden)
		{
			if (current_method != null)
			{
				current_method.MarkSequencePoint(offset, file, line, column, is_hidden);
			}
		}

		public SourceMethodBuilder OpenMethod(ICompileUnit file, int ns_id, IMethodDef method)
		{
			SourceMethodBuilder result = new SourceMethodBuilder(file, ns_id, method);
			current_method_stack.Push(current_method);
			current_method = result;
			methods.Add(current_method);
			return result;
		}

		public void CloseMethod()
		{
			current_method = current_method_stack.Pop();
		}

		public SourceFileEntry DefineDocument(string url)
		{
			SourceFileEntry sourceFileEntry = new SourceFileEntry(file, url);
			sources.Add(sourceFileEntry);
			return sourceFileEntry;
		}

		public SourceFileEntry DefineDocument(string url, byte[] guid, byte[] checksum)
		{
			SourceFileEntry sourceFileEntry = new SourceFileEntry(file, url, guid, checksum);
			sources.Add(sourceFileEntry);
			return sourceFileEntry;
		}

		public CompileUnitEntry DefineCompilationUnit(SourceFileEntry source)
		{
			CompileUnitEntry compileUnitEntry = new CompileUnitEntry(file, source);
			comp_units.Add(compileUnitEntry);
			return compileUnitEntry;
		}

		public int DefineNamespace(string name, CompileUnitEntry unit, string[] using_clauses, int parent)
		{
			if (unit == null || using_clauses == null)
			{
				throw new NullReferenceException();
			}
			return unit.DefineNamespace(name, using_clauses, parent);
		}

		public int OpenScope(int start_offset)
		{
			if (current_method == null)
			{
				return 0;
			}
			current_method.StartBlock(CodeBlockEntry.Type.Lexical, start_offset);
			return 0;
		}

		public void CloseScope(int end_offset)
		{
			if (current_method != null)
			{
				current_method.EndBlock(end_offset);
			}
		}

		public void OpenCompilerGeneratedBlock(int start_offset)
		{
			if (current_method != null)
			{
				current_method.StartBlock(CodeBlockEntry.Type.CompilerGenerated, start_offset);
			}
		}

		public void CloseCompilerGeneratedBlock(int end_offset)
		{
			if (current_method != null)
			{
				current_method.EndBlock(end_offset);
			}
		}

		public void StartIteratorBody(int start_offset)
		{
			current_method.StartBlock(CodeBlockEntry.Type.IteratorBody, start_offset);
		}

		public void EndIteratorBody(int end_offset)
		{
			current_method.EndBlock(end_offset);
		}

		public void StartIteratorDispatcher(int start_offset)
		{
			current_method.StartBlock(CodeBlockEntry.Type.IteratorDispatcher, start_offset);
		}

		public void EndIteratorDispatcher(int end_offset)
		{
			current_method.EndBlock(end_offset);
		}

		public void DefineAnonymousScope(int id)
		{
			file.DefineAnonymousScope(id);
		}

		public void WriteSymbolFile(Guid guid)
		{
			foreach (SourceMethodBuilder method in methods)
			{
				method.DefineMethod(file);
			}
			try
			{
				File.Delete(filename);
			}
			catch
			{
			}
			using FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
			file.CreateSymbolFile(guid, fs);
		}
	}
	public class SourceMethodBuilder
	{
		private List<LocalVariableEntry> _locals;

		private List<CodeBlockEntry> _blocks;

		private List<ScopeVariable> _scope_vars;

		private Stack<CodeBlockEntry> _block_stack;

		private readonly List<LineNumberEntry> method_lines;

		private readonly ICompileUnit _comp_unit;

		private readonly int ns_id;

		private readonly IMethodDef method;

		public CodeBlockEntry[] Blocks
		{
			get
			{
				if (_blocks == null)
				{
					return new CodeBlockEntry[0];
				}
				CodeBlockEntry[] array = new CodeBlockEntry[_blocks.Count];
				_blocks.CopyTo(array, 0);
				return array;
			}
		}

		public CodeBlockEntry CurrentBlock
		{
			get
			{
				if (_block_stack != null && _block_stack.Count > 0)
				{
					return _block_stack.Peek();
				}
				return null;
			}
		}

		public LocalVariableEntry[] Locals
		{
			get
			{
				if (_locals == null)
				{
					return new LocalVariableEntry[0];
				}
				return _locals.ToArray();
			}
		}

		public ICompileUnit SourceFile => _comp_unit;

		public ScopeVariable[] ScopeVariables
		{
			get
			{
				if (_scope_vars == null)
				{
					return new ScopeVariable[0];
				}
				return _scope_vars.ToArray();
			}
		}

		public SourceMethodBuilder(ICompileUnit comp_unit)
		{
			_comp_unit = comp_unit;
			method_lines = new List<LineNumberEntry>();
		}

		public SourceMethodBuilder(ICompileUnit comp_unit, int ns_id, IMethodDef method)
			: this(comp_unit)
		{
			this.ns_id = ns_id;
			this.method = method;
		}

		public void MarkSequencePoint(int offset, SourceFileEntry file, int line, int column, bool is_hidden)
		{
			MarkSequencePoint(offset, file, line, column, -1, -1, is_hidden);
		}

		public void MarkSequencePoint(int offset, SourceFileEntry file, int line, int column, int end_line, int end_column, bool is_hidden)
		{
			int file2 = file?.Index ?? 0;
			LineNumberEntry lineNumberEntry = new LineNumberEntry(file2, line, column, end_line, end_column, offset, is_hidden);
			if (method_lines.Count > 0)
			{
				LineNumberEntry lineNumberEntry2 = method_lines[method_lines.Count - 1];
				if (lineNumberEntry2.Offset == offset)
				{
					if (LineNumberEntry.LocationComparer.Default.Compare(lineNumberEntry, lineNumberEntry2) > 0)
					{
						method_lines[method_lines.Count - 1] = lineNumberEntry;
					}
					return;
				}
			}
			method_lines.Add(lineNumberEntry);
		}

		public void StartBlock(CodeBlockEntry.Type type, int start_offset)
		{
			StartBlock(type, start_offset, (_blocks == null) ? 1 : (_blocks.Count + 1));
		}

		public void StartBlock(CodeBlockEntry.Type type, int start_offset, int scopeIndex)
		{
			if (_block_stack == null)
			{
				_block_stack = new Stack<CodeBlockEntry>();
			}
			if (_blocks == null)
			{
				_blocks = new List<CodeBlockEntry>();
			}
			int parent = ((CurrentBlock != null) ? CurrentBlock.Index : (-1));
			CodeBlockEntry item = new CodeBlockEntry(scopeIndex, parent, type, start_offset);
			_block_stack.Push(item);
			_blocks.Add(item);
		}

		public void EndBlock(int end_offset)
		{
			CodeBlockEntry codeBlockEntry = _block_stack.Pop();
			codeBlockEntry.Close(end_offset);
		}

		public void AddLocal(int index, string name)
		{
			if (_locals == null)
			{
				_locals = new List<LocalVariableEntry>();
			}
			int block = ((CurrentBlock != null) ? CurrentBlock.Index : 0);
			_locals.Add(new LocalVariableEntry(index, name, block));
		}

		public void AddScopeVariable(int scope, int index)
		{
			if (_scope_vars == null)
			{
				_scope_vars = new List<ScopeVariable>();
			}
			_scope_vars.Add(new ScopeVariable(scope, index));
		}

		public void DefineMethod(MonoSymbolFile file)
		{
			DefineMethod(file, method.Token);
		}

		public void DefineMethod(MonoSymbolFile file, int token)
		{
			CodeBlockEntry[] array = Blocks;
			if (array.Length != 0)
			{
				List<CodeBlockEntry> list = new List<CodeBlockEntry>(array.Length);
				int num = 0;
				for (int i = 0; i < array.Length; i++)
				{
					num = System.Math.Max(num, array[i].Index);
				}
				for (int j = 0; j < num; j++)
				{
					int num2 = j + 1;
					if (j < array.Length && array[j].Index == num2)
					{
						list.Add(array[j]);
						continue;
					}
					bool flag = false;
					for (int k = 0; k < array.Length; k++)
					{
						if (array[k].Index == num2)
						{
							list.Add(array[k]);
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						list.Add(new CodeBlockEntry(num2, -1, CodeBlockEntry.Type.CompilerGenerated, 0));
					}
				}
				array = list.ToArray();
			}
			MethodEntry entry = new MethodEntry(file, _comp_unit.Entry, token, ScopeVariables, Locals, method_lines.ToArray(), array, null, MethodEntry.Flags.ColumnsInfoIncluded, ns_id);
			file.AddMethod(entry);
		}
	}
	public class SymbolWriterImpl : System.Diagnostics.SymbolStore.ISymbolWriter
	{
		private MonoSymbolWriter msw;

		private int nextLocalIndex;

		private int currentToken;

		private string methodName;

		private Stack namespaceStack = new Stack();

		private bool methodOpened;

		private Hashtable documents = new Hashtable();

		private Guid guid;

		public SymbolWriterImpl(Guid guid)
		{
			this.guid = guid;
		}

		public void Close()
		{
			msw.WriteSymbolFile(guid);
		}

		public void CloseMethod()
		{
			if (methodOpened)
			{
				methodOpened = false;
				nextLocalIndex = 0;
				msw.CloseMethod();
			}
		}

		public void CloseNamespace()
		{
			namespaceStack.Pop();
			msw.CloseNamespace();
		}

		public void CloseScope(int endOffset)
		{
			msw.CloseScope(endOffset);
		}

		public ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType)
		{
			SymbolDocumentWriterImpl symbolDocumentWriterImpl = (SymbolDocumentWriterImpl)documents[url];
			if (symbolDocumentWriterImpl == null)
			{
				SourceFileEntry source = msw.DefineDocument(url);
				CompileUnitEntry comp_unit = msw.DefineCompilationUnit(source);
				symbolDocumentWriterImpl = new SymbolDocumentWriterImpl(comp_unit);
				documents[url] = symbolDocumentWriterImpl;
			}
			return symbolDocumentWriterImpl;
		}

		public void DefineField(SymbolToken parent, string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3)
		{
		}

		public void DefineGlobalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3)
		{
		}

		public void DefineLocalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3, int startOffset, int endOffset)
		{
			msw.DefineLocalVariable(nextLocalIndex++, name);
		}

		public void DefineParameter(string name, ParameterAttributes attributes, int sequence, SymAddressKind addrKind, int addr1, int addr2, int addr3)
		{
		}

		public void DefineSequencePoints(ISymbolDocumentWriter document, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns)
		{
			SourceFileEntry file = ((SymbolDocumentWriterImpl)document)?.Entry.SourceFile;
			for (int i = 0; i < offsets.Length; i++)
			{
				if (i <= 0 || offsets[i] != offsets[i - 1] || lines[i] != lines[i - 1] || columns[i] != columns[i - 1])
				{
					msw.MarkSequencePoint(offsets[i], file, lines[i], columns[i], is_hidden: false);
				}
			}
		}

		public void Initialize(IntPtr emitter, string filename, bool fFullBuild)
		{
			msw = new MonoSymbolWriter(filename);
		}

		public void OpenMethod(SymbolToken method)
		{
			currentToken = method.GetToken();
		}

		public void OpenNamespace(string name)
		{
			NamespaceInfo namespaceInfo = new NamespaceInfo();
			namespaceInfo.NamespaceID = -1;
			namespaceInfo.Name = name;
			namespaceStack.Push(namespaceInfo);
		}

		public int OpenScope(int startOffset)
		{
			return msw.OpenScope(startOffset);
		}

		public void SetMethodSourceRange(ISymbolDocumentWriter startDoc, int startLine, int startColumn, ISymbolDocumentWriter endDoc, int endLine, int endColumn)
		{
			int currentNamespace = GetCurrentNamespace(startDoc);
			SourceMethodImpl method = new SourceMethodImpl(methodName, currentToken, currentNamespace);
			msw.OpenMethod(((ICompileUnit)startDoc).Entry, currentNamespace, method);
			methodOpened = true;
		}

		public void SetScopeRange(int scopeID, int startOffset, int endOffset)
		{
		}

		public void SetSymAttribute(SymbolToken parent, string name, byte[] data)
		{
			if (name == "__name")
			{
				methodName = Encoding.UTF8.GetString(data);
			}
		}

		public void SetUnderlyingWriter(IntPtr underlyingWriter)
		{
		}

		public void SetUserEntryPoint(SymbolToken entryMethod)
		{
		}

		public void UsingNamespace(string fullName)
		{
			if (namespaceStack.Count == 0)
			{
				OpenNamespace("");
			}
			NamespaceInfo namespaceInfo = (NamespaceInfo)namespaceStack.Peek();
			if (namespaceInfo.NamespaceID != -1)
			{
				NamespaceInfo namespaceInfo2 = namespaceInfo;
				CloseNamespace();
				OpenNamespace(namespaceInfo2.Name);
				namespaceInfo = (NamespaceInfo)namespaceStack.Peek();
				namespaceInfo.UsingClauses = namespaceInfo2.UsingClauses;
			}
			namespaceInfo.UsingClauses.Add(fullName);
		}

		private int GetCurrentNamespace(ISymbolDocumentWriter doc)
		{
			if (namespaceStack.Count == 0)
			{
				OpenNamespace("");
			}
			NamespaceInfo namespaceInfo = (NamespaceInfo)namespaceStack.Peek();
			if (namespaceInfo.NamespaceID == -1)
			{
				string[] using_clauses = (string[])namespaceInfo.UsingClauses.ToArray(typeof(string));
				int parent = 0;
				if (namespaceStack.Count > 1)
				{
					namespaceStack.Pop();
					parent = ((NamespaceInfo)namespaceStack.Peek()).NamespaceID;
					namespaceStack.Push(namespaceInfo);
				}
				namespaceInfo.NamespaceID = msw.DefineNamespace(namespaceInfo.Name, ((ICompileUnit)doc).Entry, using_clauses, parent);
			}
			return namespaceInfo.NamespaceID;
		}
	}
	internal class SymbolDocumentWriterImpl : ISymbolDocumentWriter, ISourceFile, ICompileUnit
	{
		private CompileUnitEntry comp_unit;

		SourceFileEntry ISourceFile.Entry => comp_unit.SourceFile;

		public CompileUnitEntry Entry => comp_unit;

		public SymbolDocumentWriterImpl(CompileUnitEntry comp_unit)
		{
			this.comp_unit = comp_unit;
		}

		public void SetCheckSum(Guid algorithmId, byte[] checkSum)
		{
		}

		public void SetSource(byte[] source)
		{
		}
	}
	internal class SourceMethodImpl : IMethodDef
	{
		private string name;

		private int token;

		private int namespaceID;

		public string Name => name;

		public int NamespaceID => namespaceID;

		public int Token => token;

		public SourceMethodImpl(string name, int token, int namespaceID)
		{
			this.name = name;
			this.token = token;
			this.namespaceID = namespaceID;
		}
	}
	internal class NamespaceInfo
	{
		public string Name;

		public int NamespaceID;

		public ArrayList UsingClauses = new ArrayList();
	}
}
namespace Mono.Cecil.Mdb
{
	public sealed class MdbReaderProvider : ISymbolReaderProvider
	{
		public Mono.Cecil.Cil.ISymbolReader GetSymbolReader(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			return new MdbReader(module, MonoSymbolFile.ReadSymbolFile(Mixin.GetMdbFileName(fileName)));
		}

		public Mono.Cecil.Cil.ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream)
		{
			Mixin.CheckModule(module);
			Mixin.CheckStream(symbolStream);
			return new MdbReader(module, MonoSymbolFile.ReadSymbolFile(symbolStream));
		}
	}
	public sealed class MdbReader : Mono.Cecil.Cil.ISymbolReader, IDisposable
	{
		private readonly ModuleDefinition module;

		private readonly MonoSymbolFile symbol_file;

		private readonly Dictionary<string, Document> documents;

		public MdbReader(ModuleDefinition module, MonoSymbolFile symFile)
		{
			this.module = module;
			symbol_file = symFile;
			documents = new Dictionary<string, Document>();
		}

		public ISymbolWriterProvider GetWriterProvider()
		{
			return new MdbWriterProvider();
		}

		public bool ProcessDebugHeader(ImageDebugHeader header)
		{
			return symbol_file.Guid == module.Mvid;
		}

		public MethodDebugInformation Read(MethodDefinition method)
		{
			MetadataToken metadataToken = method.MetadataToken;
			MethodEntry methodByToken = symbol_file.GetMethodByToken(metadataToken.ToInt32());
			if (methodByToken == null)
			{
				return null;
			}
			MethodDebugInformation methodDebugInformation = new MethodDebugInformation(method);
			methodDebugInformation.code_size = ReadCodeSize(method);
			ScopeDebugInformation[] scopes = ReadScopes(methodByToken, methodDebugInformation);
			ReadLineNumbers(methodByToken, methodDebugInformation);
			ReadLocalVariables(methodByToken, scopes);
			return methodDebugInformation;
		}

		private static int ReadCodeSize(MethodDefinition method)
		{
			return method.Module.Read(method, (MethodDefinition m, MetadataReader reader) => reader.ReadCodeSize(m));
		}

		private static void ReadLocalVariables(MethodEntry entry, ScopeDebugInformation[] scopes)
		{
			LocalVariableEntry[] locals = entry.GetLocals();
			LocalVariableEntry[] array = locals;
			for (int i = 0; i < array.Length; i++)
			{
				LocalVariableEntry localVariableEntry = array[i];
				VariableDebugInformation item = new VariableDebugInformation(localVariableEntry.Index, localVariableEntry.Name);
				int blockIndex = localVariableEntry.BlockIndex;
				if (blockIndex >= 0 && blockIndex < scopes.Length)
				{
					scopes[blockIndex]?.Variables.Add(item);
				}
			}
		}

		private void ReadLineNumbers(MethodEntry entry, MethodDebugInformation info)
		{
			LineNumberTable lineNumberTable = entry.GetLineNumberTable();
			info.sequence_points = new Collection<SequencePoint>(lineNumberTable.LineNumbers.Length);
			for (int i = 0; i < lineNumberTable.LineNumbers.Length; i++)
			{
				LineNumberEntry lineNumberEntry = lineNumberTable.LineNumbers[i];
				if (i <= 0 || lineNumberTable.LineNumbers[i - 1].Offset != lineNumberEntry.Offset)
				{
					info.sequence_points.Add(LineToSequencePoint(lineNumberEntry));
				}
			}
		}

		private Document GetDocument(SourceFileEntry file)
		{
			string fileName = file.FileName;
			if (documents.TryGetValue(fileName, out var value))
			{
				return value;
			}
			value = new Document(fileName)
			{
				Hash = file.Checksum
			};
			documents.Add(fileName, value);
			return value;
		}

		private static ScopeDebugInformation[] ReadScopes(MethodEntry entry, MethodDebugInformation info)
		{
			CodeBlockEntry[] codeBlocks = entry.GetCodeBlocks();
			ScopeDebugInformation[] array = new ScopeDebugInformation[codeBlocks.Length + 1];
			ScopeDebugInformation obj = new ScopeDebugInformation
			{
				Start = new InstructionOffset(0),
				End = new InstructionOffset(info.code_size)
			};
			ScopeDebugInformation scope = obj;
			array[0] = obj;
			info.scope = scope;
			CodeBlockEntry[] array2 = codeBlocks;
			foreach (CodeBlockEntry codeBlockEntry in array2)
			{
				if (codeBlockEntry.BlockType == CodeBlockEntry.Type.Lexical || codeBlockEntry.BlockType == CodeBlockEntry.Type.CompilerGenerated)
				{
					ScopeDebugInformation scopeDebugInformation = new ScopeDebugInformation();
					scopeDebugInformation.Start = new InstructionOffset(codeBlockEntry.StartOffset);
					scopeDebugInformation.End = new InstructionOffset(codeBlockEntry.EndOffset);
					array[codeBlockEntry.Index + 1] = scopeDebugInformation;
					if (!AddScope(info.scope.Scopes, scopeDebugInformation))
					{
						info.scope.Scopes.Add(scopeDebugInformation);
					}
				}
			}
			return array;
		}

		private static bool AddScope(Collection<ScopeDebugInformation> scopes, ScopeDebugInformation scope)
		{
			foreach (ScopeDebugInformation scope2 in scopes)
			{
				if (scope2.HasScopes && AddScope(scope2.Scopes, scope))
				{
					return true;
				}
				if (scope.Start.Offset >= scope2.Start.Offset && scope.End.Offset <= scope2.End.Offset)
				{
					scope2.Scopes.Add(scope);
					return true;
				}
			}
			return false;
		}

		private SequencePoint LineToSequencePoint(LineNumberEntry line)
		{
			SourceFileEntry sourceFile = symbol_file.GetSourceFile(line.File);
			return new SequencePoint(line.Offset, GetDocument(sourceFile))
			{
				StartLine = line.Row,
				EndLine = line.EndRow,
				StartColumn = line.Column,
				EndColumn = line.EndColumn
			};
		}

		public void Dispose()
		{
			symbol_file.Dispose();
		}
	}
	internal static class MethodEntryExtensions
	{
		public static bool HasColumnInfo(this MethodEntry entry)
		{
			return (entry.MethodFlags & MethodEntry.Flags.ColumnsInfoIncluded) != 0;
		}

		public static bool HasEndInfo(this MethodEntry entry)
		{
			return (entry.MethodFlags & MethodEntry.Flags.EndInfoIncluded) != 0;
		}
	}
	public sealed class MdbWriterProvider : ISymbolWriterProvider
	{
		public Mono.Cecil.Cil.ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			return new MdbWriter(module, fileName);
		}

		public Mono.Cecil.Cil.ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream)
		{
			throw new NotImplementedException();
		}
	}
	public sealed class MdbWriter : Mono.Cecil.Cil.ISymbolWriter, IDisposable
	{
		private class SourceFile : ISourceFile
		{
			private readonly CompileUnitEntry compilation_unit;

			private readonly SourceFileEntry entry;

			public SourceFileEntry Entry => entry;

			public CompileUnitEntry CompilationUnit => compilation_unit;

			public SourceFile(CompileUnitEntry comp_unit, SourceFileEntry entry)
			{
				compilation_unit = comp_unit;
				this.entry = entry;
			}
		}

		private class SourceMethod : IMethodDef
		{
			private readonly MethodDefinition method;

			public string Name => method.Name;

			public int Token => method.MetadataToken.ToInt32();

			public SourceMethod(MethodDefinition method)
			{
				this.method = method;
			}
		}

		private readonly ModuleDefinition module;

		private readonly MonoSymbolWriter writer;

		private readonly Dictionary<string, SourceFile> source_files;

		public MdbWriter(ModuleDefinition module, string assembly)
		{
			this.module = module;
			writer = new MonoSymbolWriter(assembly);
			source_files = new Dictionary<string, SourceFile>();
		}

		public ISymbolReaderProvider GetReaderProvider()
		{
			return new MdbReaderProvider();
		}

		private SourceFile GetSourceFile(Document document)
		{
			string url = document.Url;
			if (source_files.TryGetValue(url, out var value))
			{
				return value;
			}
			SourceFileEntry sourceFileEntry = writer.DefineDocument(url, null, (document.Hash != null && document.Hash.Length == 16) ? document.Hash : null);
			CompileUnitEntry comp_unit = writer.DefineCompilationUnit(sourceFileEntry);
			value = new SourceFile(comp_unit, sourceFileEntry);
			source_files.Add(url, value);
			return value;
		}

		private void Populate(Collection<SequencePoint> sequencePoints, int[] offsets, int[] startRows, int[] endRows, int[] startCols, int[] endCols, out SourceFile file)
		{
			SourceFile sourceFile = null;
			for (int i = 0; i < sequencePoints.Count; i++)
			{
				SequencePoint sequencePoint = sequencePoints[i];
				offsets[i] = sequencePoint.Offset;
				if (sourceFile == null)
				{
					sourceFile = GetSourceFile(sequencePoint.Document);
				}
				startRows[i] = sequencePoint.StartLine;
				endRows[i] = sequencePoint.EndLine;
				startCols[i] = sequencePoint.StartColumn;
				endCols[i] = sequencePoint.EndColumn;
			}
			file = sourceFile;
		}

		public void Write(MethodDebugInformation info)
		{
			SourceMethod method = new SourceMethod(info.method);
			Collection<SequencePoint> sequencePoints = info.SequencePoints;
			int count = sequencePoints.Count;
			if (count != 0)
			{
				int[] array = new int[count];
				int[] array2 = new int[count];
				int[] array3 = new int[count];
				int[] array4 = new int[count];
				int[] array5 = new int[count];
				Populate(sequencePoints, array, array2, array3, array4, array5, out var file);
				SourceMethodBuilder sourceMethodBuilder = writer.OpenMethod(file.CompilationUnit, 0, method);
				for (int i = 0; i < count; i++)
				{
					sourceMethodBuilder.MarkSequencePoint(array[i], file.CompilationUnit.SourceFile, array2[i], array4[i], array3[i], array5[i], is_hidden: false);
				}
				if (info.scope != null)
				{
					WriteRootScope(info.scope, info);
				}
				writer.CloseMethod();
			}
		}

		private void WriteRootScope(ScopeDebugInformation scope, MethodDebugInformation info)
		{
			WriteScopeVariables(scope);
			if (scope.HasScopes)
			{
				WriteScopes(scope.Scopes, info);
			}
		}

		private void WriteScope(ScopeDebugInformation scope, MethodDebugInformation info)
		{
			writer.OpenScope(scope.Start.Offset);
			WriteScopeVariables(scope);
			if (scope.HasScopes)
			{
				WriteScopes(scope.Scopes, info);
			}
			writer.CloseScope(scope.End.IsEndOfMethod ? info.code_size : scope.End.Offset);
		}

		private void WriteScopes(Collection<ScopeDebugInformation> scopes, MethodDebugInformation info)
		{
			for (int i = 0; i < scopes.Count; i++)
			{
				WriteScope(scopes[i], info);
			}
		}

		private void WriteScopeVariables(ScopeDebugInformation scope)
		{
			if (!scope.HasVariables)
			{
				return;
			}
			foreach (VariableDebugInformation variable in scope.variables)
			{
				if (!string.IsNullOrEmpty(variable.Name))
				{
					writer.DefineLocalVariable(variable.Index, variable.Name);
				}
			}
		}

		public ImageDebugHeader GetDebugHeader()
		{
			return new ImageDebugHeader();
		}

		public void Write()
		{
		}

		public void Dispose()
		{
			writer.WriteSymbolFile(module.Mvid);
		}
	}
}
