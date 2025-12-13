using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyVersion("0.0.0.0")]
[CompilerGenerated]
[EditorBrowsable(EditorBrowsableState.Never)]
[GeneratedCode("Unity.MonoScriptGenerator.MonoScriptInfoGenerator", null)]
internal class UnitySourceGeneratedAssemblyMonoScriptTypes_v1
{
	private struct MonoScriptData
	{
		public byte[] FilePathsData;

		public byte[] TypesData;

		public int TotalTypes;

		public int TotalFiles;

		public bool IsEditorOnly;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static MonoScriptData Get()
	{
		return new MonoScriptData
		{
			FilePathsData = new byte[56]
			{
				0, 0, 0, 1, 0, 0, 0, 48, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 71, 111, 111, 103, 108, 101,
				83, 104, 101, 101, 116, 115, 92, 73, 109, 112,
				111, 114, 116, 46, 99, 115
			},
			TypesData = new byte[34]
			{
				0, 0, 0, 0, 29, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 71, 111, 111, 103, 108,
				101, 83, 104, 101, 101, 116, 115, 124, 73, 109,
				112, 111, 114, 116
			},
			TotalFiles = 1,
			TotalTypes = 1,
			IsEditorOnly = false
		};
	}
}
namespace Facepunch.GoogleSheets;

public static class Import
{
	private static Regex regex = new Regex("(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)");

	public static T[] FromUrl<T>(string sheetId) where T : new()
	{
		MonoSecurityBullshitHack();
		string address = $"http://docs.google.com/spreadsheets/d/{sheetId}/pub?output=csv";
		string text = new WebClient().DownloadString(address);
		Console.WriteLine(text);
		string[] source = text.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
		PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		string[] columnNames = GetCSVColumns(source.First());
		List<T> list = new List<T>();
		foreach (string item in source.Skip(1))
		{
			string[] cSVColumns = GetCSVColumns(item);
			T val = new T();
			int i;
			for (i = 0; i < cSVColumns.Length; i++)
			{
				foreach (PropertyInfo item2 in properties.Where((PropertyInfo x) => x.Name.Equals(columnNames[i], StringComparison.CurrentCultureIgnoreCase)))
				{
					item2.SetValue(val, Convert.ChangeType(cSVColumns[i], item2.PropertyType), null);
				}
			}
			list.Add(val);
		}
		return list.ToArray();
	}

	private static string[] GetCSVColumns(string line)
	{
		line = line.Trim('\n', '\r');
		return regex.Matches(line).Cast<Match>().Select(delegate(Match x)
		{
			string text = x.Value.Replace("\"\"", "\"");
			if (text.EndsWith("\"") && text.StartsWith("\""))
			{
				text = text.Substring(1, text.Length - 2);
			}
			return text;
		})
			.ToArray();
	}

	private static void MonoSecurityBullshitHack()
	{
		ServicePointManager.ServerCertificateValidationCallback = (object a, X509Certificate b, X509Chain c, SslPolicyErrors d) => true;
	}
}
