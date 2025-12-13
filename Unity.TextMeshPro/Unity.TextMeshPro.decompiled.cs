using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UI;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: InternalsVisibleTo("Unity.TextCore")]
[assembly: InternalsVisibleTo("Unity.FontEngine.Tests")]
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
			FilePathsData = new byte[3070]
			{
				0, 0, 0, 4, 0, 0, 0, 57, 92, 80,
				97, 99, 107, 97, 103, 101, 115, 92, 117, 110,
				105, 116, 121, 46, 116, 101, 120, 116, 109, 101,
				115, 104, 112, 114, 111, 92, 83, 99, 114, 105,
				112, 116, 115, 92, 82, 117, 110, 116, 105, 109,
				101, 92, 70, 97, 115, 116, 65, 99, 116, 105,
				111, 110, 46, 99, 115, 0, 0, 0, 2, 0,
				0, 0, 71, 92, 80, 97, 99, 107, 97, 103,
				101, 115, 92, 117, 110, 105, 116, 121, 46, 116,
				101, 120, 116, 109, 101, 115, 104, 112, 114, 111,
				92, 83, 99, 114, 105, 112, 116, 115, 92, 82,
				117, 110, 116, 105, 109, 101, 92, 77, 97, 116,
				101, 114, 105, 97, 108, 82, 101, 102, 101, 114,
				101, 110, 99, 101, 77, 97, 110, 97, 103, 101,
				114, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 60, 92, 80, 97, 99, 107, 97, 103, 101,
				115, 92, 117, 110, 105, 116, 121, 46, 116, 101,
				120, 116, 109, 101, 115, 104, 112, 114, 111, 92,
				83, 99, 114, 105, 112, 116, 115, 92, 82, 117,
				110, 116, 105, 109, 101, 92, 84, 101, 120, 116,
				67, 111, 110, 116, 97, 105, 110, 101, 114, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 58,
				92, 80, 97, 99, 107, 97, 103, 101, 115, 92,
				117, 110, 105, 116, 121, 46, 116, 101, 120, 116,
				109, 101, 115, 104, 112, 114, 111, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 82, 117, 110, 116,
				105, 109, 101, 92, 84, 101, 120, 116, 77, 101,
				115, 104, 80, 114, 111, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 62, 92, 80, 97, 99,
				107, 97, 103, 101, 115, 92, 117, 110, 105, 116,
				121, 46, 116, 101, 120, 116, 109, 101, 115, 104,
				112, 114, 111, 92, 83, 99, 114, 105, 112, 116,
				115, 92, 82, 117, 110, 116, 105, 109, 101, 92,
				84, 101, 120, 116, 77, 101, 115, 104, 80, 114,
				111, 85, 71, 85, 73, 46, 99, 115, 0, 0,
				0, 2, 0, 0, 0, 65, 92, 80, 97, 99,
				107, 97, 103, 101, 115, 92, 117, 110, 105, 116,
				121, 46, 116, 101, 120, 116, 109, 101, 115, 104,
				112, 114, 111, 92, 83, 99, 114, 105, 112, 116,
				115, 92, 82, 117, 110, 116, 105, 109, 101, 92,
				84, 77, 80, 114, 111, 95, 69, 118, 101, 110,
				116, 77, 97, 110, 97, 103, 101, 114, 46, 99,
				115, 0, 0, 0, 2, 0, 0, 0, 69, 92,
				80, 97, 99, 107, 97, 103, 101, 115, 92, 117,
				110, 105, 116, 121, 46, 116, 101, 120, 116, 109,
				101, 115, 104, 112, 114, 111, 92, 83, 99, 114,
				105, 112, 116, 115, 92, 82, 117, 110, 116, 105,
				109, 101, 92, 84, 77, 80, 114, 111, 95, 69,
				120, 116, 101, 110, 115, 105, 111, 110, 77, 101,
				116, 104, 111, 100, 115, 46, 99, 115, 0, 0,
				0, 10, 0, 0, 0, 66, 92, 80, 97, 99,
				107, 97, 103, 101, 115, 92, 117, 110, 105, 116,
				121, 46, 116, 101, 120, 116, 109, 101, 115, 104,
				112, 114, 111, 92, 83, 99, 114, 105, 112, 116,
				115, 92, 82, 117, 110, 116, 105, 109, 101, 92,
				84, 77, 80, 114, 111, 95, 77, 101, 115, 104,
				85, 116, 105, 108, 105, 116, 105, 101, 115, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 60,
				92, 80, 97, 99, 107, 97, 103, 101, 115, 92,
				117, 110, 105, 116, 121, 46, 116, 101, 120, 116,
				109, 101, 115, 104, 112, 114, 111, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 82, 117, 110, 116,
				105, 109, 101, 92, 84, 77, 80, 114, 111, 95,
				80, 114, 105, 118, 97, 116, 101, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 65, 92, 80,
				97, 99, 107, 97, 103, 101, 115, 92, 117, 110,
				105, 116, 121, 46, 116, 101, 120, 116, 109, 101,
				115, 104, 112, 114, 111, 92, 83, 99, 114, 105,
				112, 116, 115, 92, 82, 117, 110, 116, 105, 109,
				101, 92, 84, 77, 80, 114, 111, 95, 85, 71,
				85, 73, 95, 80, 114, 105, 118, 97, 116, 101,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				56, 92, 80, 97, 99, 107, 97, 103, 101, 115,
				92, 117, 110, 105, 116, 121, 46, 116, 101, 120,
				116, 109, 101, 115, 104, 112, 114, 111, 92, 83,
				99, 114, 105, 112, 116, 115, 92, 82, 117, 110,
				116, 105, 109, 101, 92, 84, 77, 80, 95, 65,
				115, 115, 101, 116, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 60, 92, 80, 97, 99, 107,
				97, 103, 101, 115, 92, 117, 110, 105, 116, 121,
				46, 116, 101, 120, 116, 109, 101, 115, 104, 112,
				114, 111, 92, 83, 99, 114, 105, 112, 116, 115,
				92, 82, 117, 110, 116, 105, 109, 101, 92, 84,
				77, 80, 95, 67, 104, 97, 114, 97, 99, 116,
				101, 114, 46, 99, 115, 0, 0, 0, 2, 0,
				0, 0, 64, 92, 80, 97, 99, 107, 97, 103,
				101, 115, 92, 117, 110, 105, 116, 121, 46, 116,
				101, 120, 116, 109, 101, 115, 104, 112, 114, 111,
				92, 83, 99, 114, 105, 112, 116, 115, 92, 82,
				117, 110, 116, 105, 109, 101, 92, 84, 77, 80,
				95, 67, 104, 97, 114, 97, 99, 116, 101, 114,
				73, 110, 102, 111, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 64, 92, 80, 97, 99, 107,
				97, 103, 101, 115, 92, 117, 110, 105, 116, 121,
				46, 116, 101, 120, 116, 109, 101, 115, 104, 112,
				114, 111, 92, 83, 99, 114, 105, 112, 116, 115,
				92, 82, 117, 110, 116, 105, 109, 101, 92, 84,
				77, 80, 95, 67, 111, 108, 111, 114, 71, 114,
				97, 100, 105, 101, 110, 116, 46, 99, 115, 0,
				0, 0, 6, 0, 0, 0, 65, 92, 80, 97,
				99, 107, 97, 103, 101, 115, 92, 117, 110, 105,
				116, 121, 46, 116, 101, 120, 116, 109, 101, 115,
				104, 112, 114, 111, 92, 83, 99, 114, 105, 112,
				116, 115, 92, 82, 117, 110, 116, 105, 109, 101,
				92, 84, 77, 80, 95, 67, 111, 114, 111, 117,
				116, 105, 110, 101, 84, 119, 101, 101, 110, 46,
				99, 115, 0, 0, 0, 2, 0, 0, 0, 66,
				92, 80, 97, 99, 107, 97, 103, 101, 115, 92,
				117, 110, 105, 116, 121, 46, 116, 101, 120, 116,
				109, 101, 115, 104, 112, 114, 111, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 82, 117, 110, 116,
				105, 109, 101, 92, 84, 77, 80, 95, 68, 101,
				102, 97, 117, 108, 116, 67, 111, 110, 116, 114,
				111, 108, 115, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 60, 92, 80, 97, 99, 107, 97,
				103, 101, 115, 92, 117, 110, 105, 116, 121, 46,
				116, 101, 120, 116, 109, 101, 115, 104, 112, 114,
				111, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				82, 117, 110, 116, 105, 109, 101, 92, 84, 77,
				80, 95, 70, 111, 110, 116, 65, 115, 115, 101,
				116, 46, 99, 115, 0, 0, 0, 9, 0, 0,
				0, 66, 92, 80, 97, 99, 107, 97, 103, 101,
				115, 92, 117, 110, 105, 116, 121, 46, 116, 101,
				120, 116, 109, 101, 115, 104, 112, 114, 111, 92,
				83, 99, 114, 105, 112, 116, 115, 92, 82, 117,
				110, 116, 105, 109, 101, 92, 84, 77, 80, 95,
				70, 111, 110, 116, 65, 115, 115, 101, 116, 67,
				111, 109, 109, 111, 110, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 69, 92, 80, 97, 99,
				107, 97, 103, 101, 115, 92, 117, 110, 105, 116,
				121, 46, 116, 101, 120, 116, 109, 101, 115, 104,
				112, 114, 111, 92, 83, 99, 114, 105, 112, 116,
				115, 92, 82, 117, 110, 116, 105, 109, 101, 92,
				84, 77, 80, 95, 70, 111, 110, 116, 65, 115,
				115, 101, 116, 85, 116, 105, 108, 105, 116, 105,
				101, 115, 46, 99, 115, 0, 0, 0, 4, 0,
				0, 0, 69, 92, 80, 97, 99, 107, 97, 103,
				101, 115, 92, 117, 110, 105, 116, 121, 46, 116,
				101, 120, 116, 109, 101, 115, 104, 112, 114, 111,
				92, 83, 99, 114, 105, 112, 116, 115, 92, 82,
				117, 110, 116, 105, 109, 101, 92, 84, 77, 80,
				95, 70, 111, 110, 116, 70, 101, 97, 116, 117,
				114, 101, 115, 67, 111, 109, 109, 111, 110, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 67,
				92, 80, 97, 99, 107, 97, 103, 101, 115, 92,
				117, 110, 105, 116, 121, 46, 116, 101, 120, 116,
				109, 101, 115, 104, 112, 114, 111, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 82, 117, 110, 116,
				105, 109, 101, 92, 84, 77, 80, 95, 70, 111,
				110, 116, 70, 101, 97, 116, 117, 114, 101, 84,
				97, 98, 108, 101, 46, 99, 115, 0, 0, 0,
				7, 0, 0, 0, 61, 92, 80, 97, 99, 107,
				97, 103, 101, 115, 92, 117, 110, 105, 116, 121,
				46, 116, 101, 120, 116, 109, 101, 115, 104, 112,
				114, 111, 92, 83, 99, 114, 105, 112, 116, 115,
				92, 82, 117, 110, 116, 105, 109, 101, 92, 84,
				77, 80, 95, 73, 110, 112, 117, 116, 70, 105,
				101, 108, 100, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 65, 92, 80, 97, 99, 107, 97,
				103, 101, 115, 92, 117, 110, 105, 116, 121, 46,
				116, 101, 120, 116, 109, 101, 115, 104, 112, 114,
				111, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				82, 117, 110, 116, 105, 109, 101, 92, 84, 77,
				80, 95, 73, 110, 112, 117, 116, 86, 97, 108,
				105, 100, 97, 116, 111, 114, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 59, 92, 80, 97,
				99, 107, 97, 103, 101, 115, 92, 117, 110, 105,
				116, 121, 46, 116, 101, 120, 116, 109, 101, 115,
				104, 112, 114, 111, 92, 83, 99, 114, 105, 112,
				116, 115, 92, 82, 117, 110, 116, 105, 109, 101,
				92, 84, 77, 80, 95, 76, 105, 110, 101, 73,
				110, 102, 111, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 59, 92, 80, 97, 99, 107, 97,
				103, 101, 115, 92, 117, 110, 105, 116, 121, 46,
				116, 101, 120, 116, 109, 101, 115, 104, 112, 114,
				111, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				82, 117, 110, 116, 105, 109, 101, 92, 84, 77,
				80, 95, 76, 105, 115, 116, 80, 111, 111, 108,
				46, 99, 115, 0, 0, 0, 3, 0, 0, 0,
				66, 92, 80, 97, 99, 107, 97, 103, 101, 115,
				92, 117, 110, 105, 116, 121, 46, 116, 101, 120,
				116, 109, 101, 115, 104, 112, 114, 111, 92, 83,
				99, 114, 105, 112, 116, 115, 92, 82, 117, 110,
				116, 105, 109, 101, 92, 84, 77, 80, 95, 77,
				97, 116, 101, 114, 105, 97, 108, 77, 97, 110,
				97, 103, 101, 114, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 59, 92, 80, 97, 99, 107,
				97, 103, 101, 115, 92, 117, 110, 105, 116, 121,
				46, 116, 101, 120, 116, 109, 101, 115, 104, 112,
				114, 111, 92, 83, 99, 114, 105, 112, 116, 115,
				92, 82, 117, 110, 116, 105, 109, 101, 92, 84,
				77, 80, 95, 77, 101, 115, 104, 73, 110, 102,
				111, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 61, 92, 80, 97, 99, 107, 97, 103, 101,
				115, 92, 117, 110, 105, 116, 121, 46, 116, 101,
				120, 116, 109, 101, 115, 104, 112, 114, 111, 92,
				83, 99, 114, 105, 112, 116, 115, 92, 82, 117,
				110, 116, 105, 109, 101, 92, 84, 77, 80, 95,
				79, 98, 106, 101, 99, 116, 80, 111, 111, 108,
				46, 99, 115, 0, 0, 0, 2, 0, 0, 0,
				67, 92, 80, 97, 99, 107, 97, 103, 101, 115,
				92, 117, 110, 105, 116, 121, 46, 116, 101, 120,
				116, 109, 101, 115, 104, 112, 114, 111, 92, 83,
				99, 114, 105, 112, 116, 115, 92, 82, 117, 110,
				116, 105, 109, 101, 92, 84, 77, 80, 95, 82,
				105, 99, 104, 84, 101, 120, 116, 84, 97, 103,
				83, 116, 97, 99, 107, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 72, 92, 80, 97, 99,
				107, 97, 103, 101, 115, 92, 117, 110, 105, 116,
				121, 46, 116, 101, 120, 116, 109, 101, 115, 104,
				112, 114, 111, 92, 83, 99, 114, 105, 112, 116,
				115, 92, 82, 117, 110, 116, 105, 109, 101, 92,
				84, 77, 80, 95, 83, 99, 114, 111, 108, 108,
				98, 97, 114, 69, 118, 101, 110, 116, 72, 97,
				110, 100, 108, 101, 114, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 65, 92, 80, 97, 99,
				107, 97, 103, 101, 115, 92, 117, 110, 105, 116,
				121, 46, 116, 101, 120, 116, 109, 101, 115, 104,
				112, 114, 111, 92, 83, 99, 114, 105, 112, 116,
				115, 92, 82, 117, 110, 116, 105, 109, 101, 92,
				84, 77, 80, 95, 83, 101, 108, 101, 99, 116,
				105, 111, 110, 67, 97, 114, 101, 116, 46, 99,
				115, 0, 0, 0, 2, 0, 0, 0, 59, 92,
				80, 97, 99, 107, 97, 103, 101, 115, 92, 117,
				110, 105, 116, 121, 46, 116, 101, 120, 116, 109,
				101, 115, 104, 112, 114, 111, 92, 83, 99, 114,
				105, 112, 116, 115, 92, 82, 117, 110, 116, 105,
				109, 101, 92, 84, 77, 80, 95, 83, 101, 116,
				116, 105, 110, 103, 115, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 66, 92, 80, 97, 99,
				107, 97, 103, 101, 115, 92, 117, 110, 105, 116,
				121, 46, 116, 101, 120, 116, 109, 101, 115, 104,
				112, 114, 111, 92, 83, 99, 114, 105, 112, 116,
				115, 92, 82, 117, 110, 116, 105, 109, 101, 92,
				84, 77, 80, 95, 83, 104, 97, 100, 101, 114,
				85, 116, 105, 108, 105, 116, 105, 101, 115, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 58,
				92, 80, 97, 99, 107, 97, 103, 101, 115, 92,
				117, 110, 105, 116, 121, 46, 116, 101, 120, 116,
				109, 101, 115, 104, 112, 114, 111, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 82, 117, 110, 116,
				105, 109, 101, 92, 84, 77, 80, 95, 83, 117,
				98, 77, 101, 115, 104, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 60, 92, 80, 97, 99,
				107, 97, 103, 101, 115, 92, 117, 110, 105, 116,
				121, 46, 116, 101, 120, 116, 109, 101, 115, 104,
				112, 114, 111, 92, 83, 99, 114, 105, 112, 116,
				115, 92, 82, 117, 110, 116, 105, 109, 101, 92,
				84, 77, 80, 95, 83, 117, 98, 77, 101, 115,
				104, 85, 73, 46, 99, 115, 0, 0, 0, 3,
				0, 0, 0, 55, 92, 80, 97, 99, 107, 97,
				103, 101, 115, 92, 117, 110, 105, 116, 121, 46,
				116, 101, 120, 116, 109, 101, 115, 104, 112, 114,
				111, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				82, 117, 110, 116, 105, 109, 101, 92, 84, 77,
				80, 95, 84, 101, 120, 116, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 62, 92, 80, 97,
				99, 107, 97, 103, 101, 115, 92, 117, 110, 105,
				116, 121, 46, 116, 101, 120, 116, 109, 101, 115,
				104, 112, 114, 111, 92, 83, 99, 114, 105, 112,
				116, 115, 92, 82, 117, 110, 116, 105, 109, 101,
				92, 84, 77, 80, 95, 84, 101, 120, 116, 69,
				108, 101, 109, 101, 110, 116, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 69, 92, 80, 97,
				99, 107, 97, 103, 101, 115, 92, 117, 110, 105,
				116, 121, 46, 116, 101, 120, 116, 109, 101, 115,
				104, 112, 114, 111, 92, 83, 99, 114, 105, 112,
				116, 115, 92, 82, 117, 110, 116, 105, 109, 101,
				92, 84, 77, 80, 95, 84, 101, 120, 116, 69,
				108, 101, 109, 101, 110, 116, 95, 76, 101, 103,
				97, 99, 121, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 59, 92, 80, 97, 99, 107, 97,
				103, 101, 115, 92, 117, 110, 105, 116, 121, 46,
				116, 101, 120, 116, 109, 101, 115, 104, 112, 114,
				111, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				82, 117, 110, 116, 105, 109, 101, 92, 84, 77,
				80, 95, 84, 101, 120, 116, 73, 110, 102, 111,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				71, 92, 80, 97, 99, 107, 97, 103, 101, 115,
				92, 117, 110, 105, 116, 121, 46, 116, 101, 120,
				116, 109, 101, 115, 104, 112, 114, 111, 92, 83,
				99, 114, 105, 112, 116, 115, 92, 82, 117, 110,
				116, 105, 109, 101, 92, 84, 77, 80, 95, 84,
				101, 120, 116, 80, 97, 114, 115, 105, 110, 103,
				85, 116, 105, 108, 105, 116, 105, 101, 115, 46,
				99, 115, 0, 0, 0, 3, 0, 0, 0, 64,
				92, 80, 97, 99, 107, 97, 103, 101, 115, 92,
				117, 110, 105, 116, 121, 46, 116, 101, 120, 116,
				109, 101, 115, 104, 112, 114, 111, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 82, 117, 110, 116,
				105, 109, 101, 92, 84, 77, 80, 95, 84, 101,
				120, 116, 85, 116, 105, 108, 105, 116, 105, 101,
				115, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 64, 92, 80, 97, 99, 107, 97, 103, 101,
				115, 92, 117, 110, 105, 116, 121, 46, 116, 101,
				120, 116, 109, 101, 115, 104, 112, 114, 111, 92,
				83, 99, 114, 105, 112, 116, 115, 92, 82, 117,
				110, 116, 105, 109, 101, 92, 84, 77, 80, 95,
				85, 112, 100, 97, 116, 101, 77, 97, 110, 97,
				103, 101, 114, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 66, 92, 80, 97, 99, 107, 97,
				103, 101, 115, 92, 117, 110, 105, 116, 121, 46,
				116, 101, 120, 116, 109, 101, 115, 104, 112, 114,
				111, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				82, 117, 110, 116, 105, 109, 101, 92, 84, 77,
				80, 95, 85, 112, 100, 97, 116, 101, 82, 101,
				103, 105, 115, 116, 101, 114, 121, 46, 99, 115
			},
			TypesData = new byte[2521]
			{
				1, 0, 0, 0, 16, 84, 77, 80, 114, 111,
				124, 70, 97, 115, 116, 65, 99, 116, 105, 111,
				110, 1, 0, 0, 0, 16, 84, 77, 80, 114,
				111, 124, 70, 97, 115, 116, 65, 99, 116, 105,
				111, 110, 1, 0, 0, 0, 16, 84, 77, 80,
				114, 111, 124, 70, 97, 115, 116, 65, 99, 116,
				105, 111, 110, 1, 0, 0, 0, 16, 84, 77,
				80, 114, 111, 124, 70, 97, 115, 116, 65, 99,
				116, 105, 111, 110, 0, 0, 0, 0, 30, 84,
				77, 80, 114, 111, 124, 77, 97, 116, 101, 114,
				105, 97, 108, 82, 101, 102, 101, 114, 101, 110,
				99, 101, 77, 97, 110, 97, 103, 101, 114, 0,
				0, 0, 0, 23, 84, 77, 80, 114, 111, 124,
				77, 97, 116, 101, 114, 105, 97, 108, 82, 101,
				102, 101, 114, 101, 110, 99, 101, 0, 0, 0,
				0, 19, 84, 77, 80, 114, 111, 124, 84, 101,
				120, 116, 67, 111, 110, 116, 97, 105, 110, 101,
				114, 1, 0, 0, 0, 17, 84, 77, 80, 114,
				111, 124, 84, 101, 120, 116, 77, 101, 115, 104,
				80, 114, 111, 1, 0, 0, 0, 21, 84, 77,
				80, 114, 111, 124, 84, 101, 120, 116, 77, 101,
				115, 104, 80, 114, 111, 85, 71, 85, 73, 0,
				0, 0, 0, 24, 84, 77, 80, 114, 111, 124,
				84, 77, 80, 114, 111, 95, 69, 118, 101, 110,
				116, 77, 97, 110, 97, 103, 101, 114, 0, 0,
				0, 0, 26, 84, 77, 80, 114, 111, 124, 67,
				111, 109, 112, 117, 116, 101, 95, 68, 84, 95,
				69, 118, 101, 110, 116, 65, 114, 103, 115, 0,
				0, 0, 0, 28, 84, 77, 80, 114, 111, 124,
				84, 77, 80, 114, 111, 95, 69, 120, 116, 101,
				110, 115, 105, 111, 110, 77, 101, 116, 104, 111,
				100, 115, 0, 0, 0, 0, 14, 84, 77, 80,
				114, 111, 124, 84, 77, 80, 95, 77, 97, 116,
				104, 0, 0, 0, 0, 20, 84, 77, 80, 114,
				111, 124, 86, 101, 114, 116, 101, 120, 71, 114,
				97, 100, 105, 101, 110, 116, 0, 0, 0, 0,
				18, 84, 77, 80, 114, 111, 124, 84, 77, 80,
				95, 80, 97, 103, 101, 73, 110, 102, 111, 0,
				0, 0, 0, 18, 84, 77, 80, 114, 111, 124,
				84, 77, 80, 95, 76, 105, 110, 107, 73, 110,
				102, 111, 0, 0, 0, 0, 18, 84, 77, 80,
				114, 111, 124, 84, 77, 80, 95, 87, 111, 114,
				100, 73, 110, 102, 111, 0, 0, 0, 0, 20,
				84, 77, 80, 114, 111, 124, 84, 77, 80, 95,
				83, 112, 114, 105, 116, 101, 73, 110, 102, 111,
				0, 0, 0, 0, 13, 84, 77, 80, 114, 111,
				124, 69, 120, 116, 101, 110, 116, 115, 0, 0,
				0, 0, 18, 84, 77, 80, 114, 111, 124, 77,
				101, 115, 104, 95, 69, 120, 116, 101, 110, 116,
				115, 0, 0, 0, 0, 19, 84, 77, 80, 114,
				111, 124, 87, 111, 114, 100, 87, 114, 97, 112,
				83, 116, 97, 116, 101, 0, 0, 0, 0, 18,
				84, 77, 80, 114, 111, 124, 84, 97, 103, 65,
				116, 116, 114, 105, 98, 117, 116, 101, 0, 0,
				0, 0, 26, 84, 77, 80, 114, 111, 124, 82,
				105, 99, 104, 84, 101, 120, 116, 84, 97, 103,
				65, 116, 116, 114, 105, 98, 117, 116, 101, 1,
				0, 0, 0, 17, 84, 77, 80, 114, 111, 124,
				84, 101, 120, 116, 77, 101, 115, 104, 80, 114,
				111, 1, 0, 0, 0, 21, 84, 77, 80, 114,
				111, 124, 84, 101, 120, 116, 77, 101, 115, 104,
				80, 114, 111, 85, 71, 85, 73, 0, 0, 0,
				0, 15, 84, 77, 80, 114, 111, 124, 84, 77,
				80, 95, 65, 115, 115, 101, 116, 0, 0, 0,
				0, 19, 84, 77, 80, 114, 111, 124, 84, 77,
				80, 95, 67, 104, 97, 114, 97, 99, 116, 101,
				114, 0, 0, 0, 0, 16, 84, 77, 80, 114,
				111, 124, 84, 77, 80, 95, 86, 101, 114, 116,
				101, 120, 0, 0, 0, 0, 23, 84, 77, 80,
				114, 111, 124, 84, 77, 80, 95, 67, 104, 97,
				114, 97, 99, 116, 101, 114, 73, 110, 102, 111,
				0, 0, 0, 0, 23, 84, 77, 80, 114, 111,
				124, 84, 77, 80, 95, 67, 111, 108, 111, 114,
				71, 114, 97, 100, 105, 101, 110, 116, 0, 0,
				0, 0, 17, 84, 77, 80, 114, 111, 124, 73,
				84, 119, 101, 101, 110, 86, 97, 108, 117, 101,
				0, 0, 0, 0, 16, 84, 77, 80, 114, 111,
				124, 67, 111, 108, 111, 114, 84, 119, 101, 101,
				110, 0, 0, 0, 0, 25, 84, 77, 80, 114,
				111, 46, 124, 67, 111, 108, 111, 114, 84, 119,
				101, 101, 110, 67, 97, 108, 108, 98, 97, 99,
				107, 0, 0, 0, 0, 16, 84, 77, 80, 114,
				111, 124, 70, 108, 111, 97, 116, 84, 119, 101,
				101, 110, 0, 0, 0, 0, 25, 84, 77, 80,
				114, 111, 46, 124, 70, 108, 111, 97, 116, 84,
				119, 101, 101, 110, 67, 97, 108, 108, 98, 97,
				99, 107, 0, 0, 0, 0, 17, 84, 77, 80,
				114, 111, 124, 84, 119, 101, 101, 110, 82, 117,
				110, 110, 101, 114, 0, 0, 0, 0, 25, 84,
				77, 80, 114, 111, 124, 84, 77, 80, 95, 68,
				101, 102, 97, 117, 108, 116, 67, 111, 110, 116,
				114, 111, 108, 115, 0, 0, 0, 0, 35, 84,
				77, 80, 114, 111, 46, 84, 77, 80, 95, 68,
				101, 102, 97, 117, 108, 116, 67, 111, 110, 116,
				114, 111, 108, 115, 124, 82, 101, 115, 111, 117,
				114, 99, 101, 115, 0, 0, 0, 0, 19, 84,
				77, 80, 114, 111, 124, 84, 77, 80, 95, 70,
				111, 110, 116, 65, 115, 115, 101, 116, 0, 0,
				0, 0, 21, 84, 77, 80, 114, 111, 124, 70,
				97, 99, 101, 73, 110, 102, 111, 95, 76, 101,
				103, 97, 99, 121, 0, 0, 0, 0, 15, 84,
				77, 80, 114, 111, 124, 84, 77, 80, 95, 71,
				108, 121, 112, 104, 0, 0, 0, 0, 31, 84,
				77, 80, 114, 111, 124, 70, 111, 110, 116, 65,
				115, 115, 101, 116, 67, 114, 101, 97, 116, 105,
				111, 110, 83, 101, 116, 116, 105, 110, 103, 115,
				0, 0, 0, 0, 24, 84, 77, 80, 114, 111,
				124, 84, 77, 80, 95, 70, 111, 110, 116, 87,
				101, 105, 103, 104, 116, 80, 97, 105, 114, 0,
				0, 0, 0, 20, 84, 77, 80, 114, 111, 124,
				75, 101, 114, 110, 105, 110, 103, 80, 97, 105,
				114, 75, 101, 121, 0, 0, 0, 0, 29, 84,
				77, 80, 114, 111, 124, 71, 108, 121, 112, 104,
				86, 97, 108, 117, 101, 82, 101, 99, 111, 114,
				100, 95, 76, 101, 103, 97, 99, 121, 0, 0,
				0, 0, 17, 84, 77, 80, 114, 111, 124, 75,
				101, 114, 110, 105, 110, 103, 80, 97, 105, 114,
				0, 0, 0, 0, 18, 84, 77, 80, 114, 111,
				124, 75, 101, 114, 110, 105, 110, 103, 84, 97,
				98, 108, 101, 0, 0, 0, 0, 23, 84, 77,
				80, 114, 111, 124, 84, 77, 80, 95, 70, 111,
				110, 116, 85, 116, 105, 108, 105, 116, 105, 101,
				115, 0, 0, 0, 0, 28, 84, 77, 80, 114,
				111, 124, 84, 77, 80, 95, 70, 111, 110, 116,
				65, 115, 115, 101, 116, 85, 116, 105, 108, 105,
				116, 105, 101, 115, 0, 0, 0, 0, 26, 84,
				77, 80, 114, 111, 124, 84, 77, 80, 95, 71,
				108, 121, 112, 104, 86, 97, 108, 117, 101, 82,
				101, 99, 111, 114, 100, 0, 0, 0, 0, 31,
				84, 77, 80, 114, 111, 124, 84, 77, 80, 95,
				71, 108, 121, 112, 104, 65, 100, 106, 117, 115,
				116, 109, 101, 110, 116, 82, 101, 99, 111, 114,
				100, 0, 0, 0, 0, 35, 84, 77, 80, 114,
				111, 124, 84, 77, 80, 95, 71, 108, 121, 112,
				104, 80, 97, 105, 114, 65, 100, 106, 117, 115,
				116, 109, 101, 110, 116, 82, 101, 99, 111, 114,
				100, 0, 0, 0, 0, 18, 84, 77, 80, 114,
				111, 124, 71, 108, 121, 112, 104, 80, 97, 105,
				114, 75, 101, 121, 0, 0, 0, 0, 26, 84,
				77, 80, 114, 111, 124, 84, 77, 80, 95, 70,
				111, 110, 116, 70, 101, 97, 116, 117, 114, 101,
				84, 97, 98, 108, 101, 0, 0, 0, 0, 20,
				84, 77, 80, 114, 111, 124, 84, 77, 80, 95,
				73, 110, 112, 117, 116, 70, 105, 101, 108, 100,
				0, 0, 0, 0, 32, 84, 77, 80, 114, 111,
				46, 84, 77, 80, 95, 73, 110, 112, 117, 116,
				70, 105, 101, 108, 100, 124, 83, 117, 98, 109,
				105, 116, 69, 118, 101, 110, 116, 0, 0, 0,
				0, 34, 84, 77, 80, 114, 111, 46, 84, 77,
				80, 95, 73, 110, 112, 117, 116, 70, 105, 101,
				108, 100, 124, 79, 110, 67, 104, 97, 110, 103,
				101, 69, 118, 101, 110, 116, 0, 0, 0, 0,
				35, 84, 77, 80, 114, 111, 46, 84, 77, 80,
				95, 73, 110, 112, 117, 116, 70, 105, 101, 108,
				100, 124, 83, 101, 108, 101, 99, 116, 105, 111,
				110, 69, 118, 101, 110, 116, 0, 0, 0, 0,
				39, 84, 77, 80, 114, 111, 46, 84, 77, 80,
				95, 73, 110, 112, 117, 116, 70, 105, 101, 108,
				100, 124, 84, 101, 120, 116, 83, 101, 108, 101,
				99, 116, 105, 111, 110, 69, 118, 101, 110, 116,
				0, 0, 0, 0, 45, 84, 77, 80, 114, 111,
				46, 84, 77, 80, 95, 73, 110, 112, 117, 116,
				70, 105, 101, 108, 100, 124, 84, 111, 117, 99,
				104, 83, 99, 114, 101, 101, 110, 75, 101, 121,
				98, 111, 97, 114, 100, 69, 118, 101, 110, 116,
				0, 0, 0, 0, 24, 84, 77, 80, 114, 111,
				124, 83, 101, 116, 80, 114, 111, 112, 101, 114,
				116, 121, 85, 116, 105, 108, 105, 116, 121, 0,
				0, 0, 0, 24, 84, 77, 80, 114, 111, 124,
				84, 77, 80, 95, 73, 110, 112, 117, 116, 86,
				97, 108, 105, 100, 97, 116, 111, 114, 0, 0,
				0, 0, 18, 84, 77, 80, 114, 111, 124, 84,
				77, 80, 95, 76, 105, 110, 101, 73, 110, 102,
				111, 0, 0, 0, 0, 18, 84, 77, 80, 114,
				111, 124, 84, 77, 80, 95, 76, 105, 115, 116,
				80, 111, 111, 108, 0, 0, 0, 0, 25, 84,
				77, 80, 114, 111, 124, 84, 77, 80, 95, 77,
				97, 116, 101, 114, 105, 97, 108, 77, 97, 110,
				97, 103, 101, 114, 0, 0, 0, 0, 42, 84,
				77, 80, 114, 111, 46, 84, 77, 80, 95, 77,
				97, 116, 101, 114, 105, 97, 108, 77, 97, 110,
				97, 103, 101, 114, 124, 70, 97, 108, 108, 98,
				97, 99, 107, 77, 97, 116, 101, 114, 105, 97,
				108, 0, 0, 0, 0, 41, 84, 77, 80, 114,
				111, 46, 84, 77, 80, 95, 77, 97, 116, 101,
				114, 105, 97, 108, 77, 97, 110, 97, 103, 101,
				114, 124, 77, 97, 115, 107, 105, 110, 103, 77,
				97, 116, 101, 114, 105, 97, 108, 0, 0, 0,
				0, 18, 84, 77, 80, 114, 111, 124, 84, 77,
				80, 95, 77, 101, 115, 104, 73, 110, 102, 111,
				0, 0, 0, 0, 20, 84, 77, 80, 114, 111,
				124, 84, 77, 80, 95, 79, 98, 106, 101, 99,
				116, 80, 111, 111, 108, 0, 0, 0, 0, 24,
				84, 77, 80, 114, 111, 124, 84, 77, 80, 95,
				70, 111, 110, 116, 83, 116, 121, 108, 101, 83,
				116, 97, 99, 107, 0, 0, 0, 0, 26, 84,
				77, 80, 114, 111, 124, 84, 77, 80, 95, 82,
				105, 99, 104, 84, 101, 120, 116, 84, 97, 103,
				83, 116, 97, 99, 107, 0, 0, 0, 0, 31,
				84, 77, 80, 114, 111, 124, 84, 77, 80, 95,
				83, 99, 114, 111, 108, 108, 98, 97, 114, 69,
				118, 101, 110, 116, 72, 97, 110, 100, 108, 101,
				114, 0, 0, 0, 0, 24, 84, 77, 80, 114,
				111, 124, 84, 77, 80, 95, 83, 101, 108, 101,
				99, 116, 105, 111, 110, 67, 97, 114, 101, 116,
				0, 0, 0, 0, 18, 84, 77, 80, 114, 111,
				124, 84, 77, 80, 95, 83, 101, 116, 116, 105,
				110, 103, 115, 0, 0, 0, 0, 36, 84, 77,
				80, 114, 111, 46, 84, 77, 80, 95, 83, 101,
				116, 116, 105, 110, 103, 115, 124, 76, 105, 110,
				101, 66, 114, 101, 97, 107, 105, 110, 103, 84,
				97, 98, 108, 101, 0, 0, 0, 0, 21, 84,
				77, 80, 114, 111, 124, 83, 104, 97, 100, 101,
				114, 85, 116, 105, 108, 105, 116, 105, 101, 115,
				0, 0, 0, 0, 17, 84, 77, 80, 114, 111,
				124, 84, 77, 80, 95, 83, 117, 98, 77, 101,
				115, 104, 0, 0, 0, 0, 19, 84, 77, 80,
				114, 111, 124, 84, 77, 80, 95, 83, 117, 98,
				77, 101, 115, 104, 85, 73, 0, 0, 0, 0,
				18, 84, 77, 80, 114, 111, 124, 73, 84, 101,
				120, 116, 69, 108, 101, 109, 101, 110, 116, 0,
				0, 0, 0, 14, 84, 77, 80, 114, 111, 124,
				84, 77, 80, 95, 84, 101, 120, 116, 0, 0,
				0, 0, 26, 84, 77, 80, 114, 111, 46, 84,
				77, 80, 95, 84, 101, 120, 116, 124, 85, 110,
				105, 99, 111, 100, 101, 67, 104, 97, 114, 0,
				0, 0, 0, 21, 84, 77, 80, 114, 111, 124,
				84, 77, 80, 95, 84, 101, 120, 116, 69, 108,
				101, 109, 101, 110, 116, 0, 0, 0, 0, 28,
				84, 77, 80, 114, 111, 124, 84, 77, 80, 95,
				84, 101, 120, 116, 69, 108, 101, 109, 101, 110,
				116, 95, 76, 101, 103, 97, 99, 121, 0, 0,
				0, 0, 18, 84, 77, 80, 114, 111, 124, 84,
				77, 80, 95, 84, 101, 120, 116, 73, 110, 102,
				111, 0, 0, 0, 0, 30, 84, 77, 80, 114,
				111, 124, 84, 77, 80, 95, 84, 101, 120, 116,
				80, 97, 114, 115, 105, 110, 103, 85, 116, 105,
				108, 105, 116, 105, 101, 115, 0, 0, 0, 0,
				15, 84, 77, 80, 114, 111, 124, 67, 97, 114,
				101, 116, 73, 110, 102, 111, 0, 0, 0, 0,
				23, 84, 77, 80, 114, 111, 124, 84, 77, 80,
				95, 84, 101, 120, 116, 85, 116, 105, 108, 105,
				116, 105, 101, 115, 0, 0, 0, 0, 35, 84,
				77, 80, 114, 111, 46, 84, 77, 80, 95, 84,
				101, 120, 116, 85, 116, 105, 108, 105, 116, 105,
				101, 115, 124, 76, 105, 110, 101, 83, 101, 103,
				109, 101, 110, 116, 0, 0, 0, 0, 23, 84,
				77, 80, 114, 111, 124, 84, 77, 80, 95, 85,
				112, 100, 97, 116, 101, 77, 97, 110, 97, 103,
				101, 114, 0, 0, 0, 0, 24, 84, 77, 80,
				114, 111, 124, 84, 77, 80, 95, 85, 112, 100,
				97, 116, 101, 82, 101, 103, 105, 115, 116, 114,
				121
			},
			TotalFiles = 43,
			TotalTypes = 90,
			IsEditorOnly = false
		};
	}
}
namespace TMPro;

public class FastAction
{
	private LinkedList<Action> delegates = new LinkedList<Action>();

	private Dictionary<Action, LinkedListNode<Action>> lookup = new Dictionary<Action, LinkedListNode<Action>>();

	public void Add(Action rhs)
	{
		if (!lookup.ContainsKey(rhs))
		{
			lookup[rhs] = delegates.AddLast(rhs);
		}
	}

	public void Remove(Action rhs)
	{
		if (lookup.TryGetValue(rhs, out var value))
		{
			lookup.Remove(rhs);
			delegates.Remove(value);
		}
	}

	public void Call()
	{
		for (LinkedListNode<Action> linkedListNode = delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			linkedListNode.Value();
		}
	}
}
public class FastAction<A>
{
	private LinkedList<Action<A>> delegates = new LinkedList<Action<A>>();

	private Dictionary<Action<A>, LinkedListNode<Action<A>>> lookup = new Dictionary<Action<A>, LinkedListNode<Action<A>>>();

	public void Add(Action<A> rhs)
	{
		if (!lookup.ContainsKey(rhs))
		{
			lookup[rhs] = delegates.AddLast(rhs);
		}
	}

	public void Remove(Action<A> rhs)
	{
		if (lookup.TryGetValue(rhs, out var value))
		{
			lookup.Remove(rhs);
			delegates.Remove(value);
		}
	}

	public void Call(A a)
	{
		for (LinkedListNode<Action<A>> linkedListNode = delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			linkedListNode.Value(a);
		}
	}
}
public class FastAction<A, B>
{
	private LinkedList<Action<A, B>> delegates = new LinkedList<Action<A, B>>();

	private Dictionary<Action<A, B>, LinkedListNode<Action<A, B>>> lookup = new Dictionary<Action<A, B>, LinkedListNode<Action<A, B>>>();

	public void Add(Action<A, B> rhs)
	{
		if (!lookup.ContainsKey(rhs))
		{
			lookup[rhs] = delegates.AddLast(rhs);
		}
	}

	public void Remove(Action<A, B> rhs)
	{
		if (lookup.TryGetValue(rhs, out var value))
		{
			lookup.Remove(rhs);
			delegates.Remove(value);
		}
	}

	public void Call(A a, B b)
	{
		for (LinkedListNode<Action<A, B>> linkedListNode = delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			linkedListNode.Value(a, b);
		}
	}
}
public class FastAction<A, B, C>
{
	private LinkedList<Action<A, B, C>> delegates = new LinkedList<Action<A, B, C>>();

	private Dictionary<Action<A, B, C>, LinkedListNode<Action<A, B, C>>> lookup = new Dictionary<Action<A, B, C>, LinkedListNode<Action<A, B, C>>>();

	public void Add(Action<A, B, C> rhs)
	{
		if (!lookup.ContainsKey(rhs))
		{
			lookup[rhs] = delegates.AddLast(rhs);
		}
	}

	public void Remove(Action<A, B, C> rhs)
	{
		if (lookup.TryGetValue(rhs, out var value))
		{
			lookup.Remove(rhs);
			delegates.Remove(value);
		}
	}

	public void Call(A a, B b, C c)
	{
		for (LinkedListNode<Action<A, B, C>> linkedListNode = delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			linkedListNode.Value(a, b, c);
		}
	}
}
public class MaterialReferenceManager
{
	private static MaterialReferenceManager s_Instance;

	private Dictionary<int, Material> m_FontMaterialReferenceLookup = new Dictionary<int, Material>();

	private Dictionary<int, TMP_FontAsset> m_FontAssetReferenceLookup = new Dictionary<int, TMP_FontAsset>();

	private Dictionary<int, TMP_ColorGradient> m_ColorGradientReferenceLookup = new Dictionary<int, TMP_ColorGradient>();

	public static MaterialReferenceManager instance
	{
		get
		{
			if (s_Instance == null)
			{
				s_Instance = new MaterialReferenceManager();
			}
			return s_Instance;
		}
	}

	public static void AddFontAsset(TMP_FontAsset fontAsset)
	{
		instance.AddFontAssetInternal(fontAsset);
	}

	private void AddFontAssetInternal(TMP_FontAsset fontAsset)
	{
		if (!m_FontAssetReferenceLookup.ContainsKey(fontAsset.hashCode))
		{
			m_FontAssetReferenceLookup.Add(fontAsset.hashCode, fontAsset);
			m_FontMaterialReferenceLookup.Add(fontAsset.materialHashCode, fontAsset.material);
		}
	}

	public static void AddFontMaterial(int hashCode, Material material)
	{
		instance.AddFontMaterialInternal(hashCode, material);
	}

	private void AddFontMaterialInternal(int hashCode, Material material)
	{
		m_FontMaterialReferenceLookup.Add(hashCode, material);
	}

	public static void AddColorGradientPreset(int hashCode, TMP_ColorGradient spriteAsset)
	{
		instance.AddColorGradientPreset_Internal(hashCode, spriteAsset);
	}

	private void AddColorGradientPreset_Internal(int hashCode, TMP_ColorGradient spriteAsset)
	{
		if (!m_ColorGradientReferenceLookup.ContainsKey(hashCode))
		{
			m_ColorGradientReferenceLookup.Add(hashCode, spriteAsset);
		}
	}

	public bool Contains(TMP_FontAsset font)
	{
		if (m_FontAssetReferenceLookup.ContainsKey(font.hashCode))
		{
			return true;
		}
		return false;
	}

	public static bool TryGetFontAsset(int hashCode, out TMP_FontAsset fontAsset)
	{
		return instance.TryGetFontAssetInternal(hashCode, out fontAsset);
	}

	private bool TryGetFontAssetInternal(int hashCode, out TMP_FontAsset fontAsset)
	{
		fontAsset = null;
		if (m_FontAssetReferenceLookup.TryGetValue(hashCode, out fontAsset))
		{
			return true;
		}
		return false;
	}

	public static bool TryGetColorGradientPreset(int hashCode, out TMP_ColorGradient gradientPreset)
	{
		return instance.TryGetColorGradientPresetInternal(hashCode, out gradientPreset);
	}

	private bool TryGetColorGradientPresetInternal(int hashCode, out TMP_ColorGradient gradientPreset)
	{
		gradientPreset = null;
		if (m_ColorGradientReferenceLookup.TryGetValue(hashCode, out gradientPreset))
		{
			return true;
		}
		return false;
	}

	public static bool TryGetMaterial(int hashCode, out Material material)
	{
		return instance.TryGetMaterialInternal(hashCode, out material);
	}

	private bool TryGetMaterialInternal(int hashCode, out Material material)
	{
		material = null;
		if (m_FontMaterialReferenceLookup.TryGetValue(hashCode, out material))
		{
			return true;
		}
		return false;
	}
}
public struct MaterialReference
{
	public int index;

	public TMP_FontAsset fontAsset;

	public Material material;

	public bool isDefaultMaterial;

	public bool isFallbackMaterial;

	public Material fallbackMaterial;

	public float padding;

	public int referenceCount;

	public MaterialReference(int index, TMP_FontAsset fontAsset, Material material, float padding)
	{
		this.index = index;
		this.fontAsset = fontAsset;
		this.material = material;
		isDefaultMaterial = material != null && material.GetInstanceID() == fontAsset.material.GetInstanceID();
		isFallbackMaterial = false;
		fallbackMaterial = null;
		this.padding = padding;
		referenceCount = 0;
	}

	public static bool Contains(MaterialReference[] materialReferences, TMP_FontAsset fontAsset)
	{
		int instanceID = fontAsset.GetInstanceID();
		for (int i = 0; i < materialReferences.Length && materialReferences[i].fontAsset != null; i++)
		{
			if (materialReferences[i].fontAsset.GetInstanceID() == instanceID)
			{
				return true;
			}
		}
		return false;
	}

	public static int AddMaterialReference(Material material, TMP_FontAsset fontAsset, MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
	{
		int instanceID = material.GetInstanceID();
		if (materialReferenceIndexLookup.TryGetValue(instanceID, out var value))
		{
			return value;
		}
		value = (materialReferenceIndexLookup[instanceID] = materialReferenceIndexLookup.Count);
		materialReferences[value].index = value;
		materialReferences[value].fontAsset = fontAsset;
		materialReferences[value].material = material;
		materialReferences[value].isDefaultMaterial = fontAsset != null && instanceID == fontAsset.material.GetInstanceID();
		materialReferences[value].referenceCount = 0;
		return value;
	}

	public static int AddMaterialReference(Material material, MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
	{
		int instanceID = material.GetInstanceID();
		if (materialReferenceIndexLookup.TryGetValue(instanceID, out var value))
		{
			return value;
		}
		value = (materialReferenceIndexLookup[instanceID] = materialReferenceIndexLookup.Count);
		materialReferences[value].index = value;
		materialReferences[value].fontAsset = materialReferences[0].fontAsset;
		materialReferences[value].material = material;
		materialReferences[value].isDefaultMaterial = true;
		materialReferences[value].referenceCount = 0;
		return value;
	}
}
public enum TextContainerAnchors
{
	TopLeft,
	Top,
	TopRight,
	Left,
	Middle,
	Right,
	BottomLeft,
	Bottom,
	BottomRight,
	Custom
}
[RequireComponent(typeof(RectTransform))]
[AddComponentMenu("Layout/Text Container")]
public class TextContainer : UIBehaviour
{
	private bool m_hasChanged;

	[SerializeField]
	private Vector2 m_pivot;

	[SerializeField]
	private TextContainerAnchors m_anchorPosition = TextContainerAnchors.Middle;

	[SerializeField]
	private Rect m_rect;

	private bool m_isDefaultWidth;

	private bool m_isDefaultHeight;

	private bool m_isAutoFitting;

	private Vector3[] m_corners = new Vector3[4];

	private Vector3[] m_worldCorners = new Vector3[4];

	[SerializeField]
	private Vector4 m_margins;

	private RectTransform m_rectTransform;

	private static Vector2 k_defaultSize = new Vector2(100f, 100f);

	private TextMeshPro m_textMeshPro;

	public bool hasChanged
	{
		get
		{
			return m_hasChanged;
		}
		set
		{
			m_hasChanged = value;
		}
	}

	public Vector2 pivot
	{
		get
		{
			return m_pivot;
		}
		set
		{
			if (m_pivot != value)
			{
				m_pivot = value;
				m_anchorPosition = GetAnchorPosition(m_pivot);
				m_hasChanged = true;
				OnContainerChanged();
			}
		}
	}

	public TextContainerAnchors anchorPosition
	{
		get
		{
			return m_anchorPosition;
		}
		set
		{
			if (m_anchorPosition != value)
			{
				m_anchorPosition = value;
				m_pivot = GetPivot(m_anchorPosition);
				m_hasChanged = true;
				OnContainerChanged();
			}
		}
	}

	public Rect rect
	{
		get
		{
			return m_rect;
		}
		set
		{
			if (m_rect != value)
			{
				m_rect = value;
				m_hasChanged = true;
				OnContainerChanged();
			}
		}
	}

	public Vector2 size
	{
		get
		{
			return new Vector2(m_rect.width, m_rect.height);
		}
		set
		{
			if (new Vector2(m_rect.width, m_rect.height) != value)
			{
				SetRect(value);
				m_hasChanged = true;
				m_isDefaultWidth = false;
				m_isDefaultHeight = false;
				OnContainerChanged();
			}
		}
	}

	public float width
	{
		get
		{
			return m_rect.width;
		}
		set
		{
			SetRect(new Vector2(value, m_rect.height));
			m_hasChanged = true;
			m_isDefaultWidth = false;
			OnContainerChanged();
		}
	}

	public float height
	{
		get
		{
			return m_rect.height;
		}
		set
		{
			SetRect(new Vector2(m_rect.width, value));
			m_hasChanged = true;
			m_isDefaultHeight = false;
			OnContainerChanged();
		}
	}

	public bool isDefaultWidth => m_isDefaultWidth;

	public bool isDefaultHeight => m_isDefaultHeight;

	public bool isAutoFitting
	{
		get
		{
			return m_isAutoFitting;
		}
		set
		{
			m_isAutoFitting = value;
		}
	}

	public Vector3[] corners => m_corners;

	public Vector3[] worldCorners => m_worldCorners;

	public Vector4 margins
	{
		get
		{
			return m_margins;
		}
		set
		{
			if (m_margins != value)
			{
				m_margins = value;
				m_hasChanged = true;
				OnContainerChanged();
			}
		}
	}

	public RectTransform rectTransform
	{
		get
		{
			if (m_rectTransform == null)
			{
				m_rectTransform = GetComponent<RectTransform>();
			}
			return m_rectTransform;
		}
	}

	public TextMeshPro textMeshPro
	{
		get
		{
			if (m_textMeshPro == null)
			{
				m_textMeshPro = GetComponent<TextMeshPro>();
			}
			return m_textMeshPro;
		}
	}

	protected override void Awake()
	{
		UnityEngine.Debug.LogWarning("The Text Container component is now Obsolete and can safely be removed from [" + base.gameObject.name + "].", this);
	}

	protected override void OnEnable()
	{
		OnContainerChanged();
	}

	protected override void OnDisable()
	{
	}

	private void OnContainerChanged()
	{
		UpdateCorners();
		if (m_rectTransform != null)
		{
			m_rectTransform.sizeDelta = size;
			m_rectTransform.hasChanged = true;
		}
		if (textMeshPro != null)
		{
			m_textMeshPro.SetVerticesDirty();
			m_textMeshPro.margin = m_margins;
		}
	}

	protected override void OnRectTransformDimensionsChange()
	{
		if (rectTransform == null)
		{
			m_rectTransform = base.gameObject.AddComponent<RectTransform>();
		}
		if (m_rectTransform.sizeDelta != k_defaultSize)
		{
			size = m_rectTransform.sizeDelta;
		}
		pivot = m_rectTransform.pivot;
		m_hasChanged = true;
		OnContainerChanged();
	}

	private void SetRect(Vector2 size)
	{
		m_rect = new Rect(m_rect.x, m_rect.y, size.x, size.y);
	}

	private void UpdateCorners()
	{
		m_corners[0] = new Vector3((0f - m_pivot.x) * m_rect.width, (0f - m_pivot.y) * m_rect.height);
		m_corners[1] = new Vector3((0f - m_pivot.x) * m_rect.width, (1f - m_pivot.y) * m_rect.height);
		m_corners[2] = new Vector3((1f - m_pivot.x) * m_rect.width, (1f - m_pivot.y) * m_rect.height);
		m_corners[3] = new Vector3((1f - m_pivot.x) * m_rect.width, (0f - m_pivot.y) * m_rect.height);
		if (m_rectTransform != null)
		{
			m_rectTransform.pivot = m_pivot;
		}
	}

	private Vector2 GetPivot(TextContainerAnchors anchor)
	{
		Vector2 result = Vector2.zero;
		switch (anchor)
		{
		case TextContainerAnchors.TopLeft:
			result = new Vector2(0f, 1f);
			break;
		case TextContainerAnchors.Top:
			result = new Vector2(0.5f, 1f);
			break;
		case TextContainerAnchors.TopRight:
			result = new Vector2(1f, 1f);
			break;
		case TextContainerAnchors.Left:
			result = new Vector2(0f, 0.5f);
			break;
		case TextContainerAnchors.Middle:
			result = new Vector2(0.5f, 0.5f);
			break;
		case TextContainerAnchors.Right:
			result = new Vector2(1f, 0.5f);
			break;
		case TextContainerAnchors.BottomLeft:
			result = new Vector2(0f, 0f);
			break;
		case TextContainerAnchors.Bottom:
			result = new Vector2(0.5f, 0f);
			break;
		case TextContainerAnchors.BottomRight:
			result = new Vector2(1f, 0f);
			break;
		}
		return result;
	}

	private TextContainerAnchors GetAnchorPosition(Vector2 pivot)
	{
		if (pivot == new Vector2(0f, 1f))
		{
			return TextContainerAnchors.TopLeft;
		}
		if (pivot == new Vector2(0.5f, 1f))
		{
			return TextContainerAnchors.Top;
		}
		if (pivot == new Vector2(1f, 1f))
		{
			return TextContainerAnchors.TopRight;
		}
		if (pivot == new Vector2(0f, 0.5f))
		{
			return TextContainerAnchors.Left;
		}
		if (pivot == new Vector2(0.5f, 0.5f))
		{
			return TextContainerAnchors.Middle;
		}
		if (pivot == new Vector2(1f, 0.5f))
		{
			return TextContainerAnchors.Right;
		}
		if (pivot == new Vector2(0f, 0f))
		{
			return TextContainerAnchors.BottomLeft;
		}
		if (pivot == new Vector2(0.5f, 0f))
		{
			return TextContainerAnchors.Bottom;
		}
		if (pivot == new Vector2(1f, 0f))
		{
			return TextContainerAnchors.BottomRight;
		}
		return TextContainerAnchors.Custom;
	}
}
[DisallowMultipleComponent]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[AddComponentMenu("")]
[ExecuteAlways]
public class TextMeshPro : TMP_Text, ILayoutElement
{
	private bool m_currentAutoSizeMode;

	[SerializeField]
	private bool m_hasFontAssetChanged;

	private float m_previousLossyScaleY = -1f;

	[SerializeField]
	private Renderer m_renderer;

	private MeshFilter m_meshFilter;

	private bool m_isFirstAllocation;

	private int m_max_characters = 8;

	private int m_max_numberOfLines = 4;

	[SerializeField]
	protected TMP_SubMesh[] m_subTextObjects = new TMP_SubMesh[8];

	private bool m_isMaskingEnabled;

	private bool isMaskUpdateRequired;

	[SerializeField]
	private MaskingTypes m_maskType;

	private Matrix4x4 m_EnvMapMatrix;

	private Vector3[] m_RectTransformCorners = new Vector3[4];

	[NonSerialized]
	private bool m_isRegisteredForEvents;

	private int loopCountA;

	public int sortingLayerID
	{
		get
		{
			return m_renderer.sortingLayerID;
		}
		set
		{
			m_renderer.sortingLayerID = value;
		}
	}

	public int sortingOrder
	{
		get
		{
			return m_renderer.sortingOrder;
		}
		set
		{
			m_renderer.sortingOrder = value;
		}
	}

	public override bool autoSizeTextContainer
	{
		get
		{
			return m_autoSizeTextContainer;
		}
		set
		{
			if (m_autoSizeTextContainer != value)
			{
				m_autoSizeTextContainer = value;
				if (m_autoSizeTextContainer)
				{
					TMP_UpdateManager.RegisterTextElementForLayoutRebuild(this);
					SetLayoutDirty();
				}
			}
		}
	}

	[Obsolete("The TextContainer is now obsolete. Use the RectTransform instead.")]
	public TextContainer textContainer => null;

	public new Transform transform
	{
		get
		{
			if (m_transform == null)
			{
				m_transform = GetComponent<Transform>();
			}
			return m_transform;
		}
	}

	public Renderer renderer
	{
		get
		{
			if (m_renderer == null)
			{
				m_renderer = GetComponent<Renderer>();
			}
			return m_renderer;
		}
	}

	public override Mesh mesh
	{
		get
		{
			if (m_mesh == null)
			{
				m_mesh = new Mesh();
				m_mesh.hideFlags = HideFlags.HideAndDontSave;
				meshFilter.mesh = m_mesh;
			}
			return m_mesh;
		}
	}

	public MeshFilter meshFilter
	{
		get
		{
			if (m_meshFilter == null)
			{
				m_meshFilter = GetComponent<MeshFilter>();
			}
			return m_meshFilter;
		}
	}

	public MaskingTypes maskType
	{
		get
		{
			return m_maskType;
		}
		set
		{
			m_maskType = value;
			SetMask(m_maskType);
		}
	}

	public void SetMask(MaskingTypes type, Vector4 maskCoords)
	{
		SetMask(type);
		SetMaskCoordinates(maskCoords);
	}

	public void SetMask(MaskingTypes type, Vector4 maskCoords, float softnessX, float softnessY)
	{
		SetMask(type);
		SetMaskCoordinates(maskCoords, softnessX, softnessY);
	}

	public override void SetVerticesDirty()
	{
		if (!m_verticesAlreadyDirty && !(this == null) && IsActive())
		{
			TMP_UpdateManager.RegisterTextElementForGraphicRebuild(this);
			m_verticesAlreadyDirty = true;
		}
	}

	public override void SetLayoutDirty()
	{
		m_isPreferredWidthDirty = true;
		m_isPreferredHeightDirty = true;
		if (!m_layoutAlreadyDirty && !(this == null) && IsActive())
		{
			m_layoutAlreadyDirty = true;
			m_isLayoutDirty = true;
		}
	}

	public override void SetMaterialDirty()
	{
		UpdateMaterial();
	}

	public override void SetAllDirty()
	{
		m_isInputParsingRequired = true;
		SetLayoutDirty();
		SetVerticesDirty();
		SetMaterialDirty();
	}

	public override void Rebuild(CanvasUpdate update)
	{
		if (this == null)
		{
			return;
		}
		switch (update)
		{
		case CanvasUpdate.Prelayout:
			if (m_autoSizeTextContainer)
			{
				m_rectTransform.sizeDelta = GetPreferredValues(float.PositiveInfinity, float.PositiveInfinity);
			}
			break;
		case CanvasUpdate.PreRender:
			OnPreRenderObject();
			m_verticesAlreadyDirty = false;
			m_layoutAlreadyDirty = false;
			if (m_isMaterialDirty)
			{
				UpdateMaterial();
				m_isMaterialDirty = false;
			}
			break;
		}
	}

	protected override void UpdateMaterial()
	{
		if (!(m_sharedMaterial == null))
		{
			if (m_renderer == null)
			{
				m_renderer = renderer;
			}
			if (m_renderer.sharedMaterial.GetInstanceID() != m_sharedMaterial.GetInstanceID())
			{
				m_renderer.sharedMaterial = m_sharedMaterial;
			}
		}
	}

	public override void UpdateMeshPadding()
	{
		m_padding = ShaderUtilities.GetPadding(m_sharedMaterial, m_enableExtraPadding, m_isUsingBold);
		m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
		m_havePropertiesChanged = true;
		checkPaddingRequired = false;
		if (m_textInfo != null)
		{
			for (int i = 1; i < m_textInfo.materialCount; i++)
			{
				m_subTextObjects[i].UpdateMeshPadding(m_enableExtraPadding, m_isUsingBold);
			}
		}
	}

	public override void ForceMeshUpdate()
	{
		m_havePropertiesChanged = true;
		OnPreRenderObject();
	}

	public override void ForceMeshUpdate(bool ignoreInactive)
	{
		m_havePropertiesChanged = true;
		m_ignoreActiveState = true;
		OnPreRenderObject();
	}

	public override TMP_TextInfo GetTextInfo(string text)
	{
		StringToCharArray(text, ref m_TextParsingBuffer);
		SetArraySizes(m_TextParsingBuffer);
		m_renderMode = TextRenderFlags.DontRender;
		ComputeMarginSize();
		GenerateTextMesh();
		m_renderMode = TextRenderFlags.Render;
		return base.textInfo;
	}

	public override void ClearMesh(bool updateMesh)
	{
		if (m_textInfo.meshInfo[0].mesh == null)
		{
			m_textInfo.meshInfo[0].mesh = m_mesh;
		}
		m_textInfo.ClearMeshInfo(updateMesh);
	}

	public override void UpdateGeometry(Mesh mesh, int index)
	{
		mesh.RecalculateBounds();
	}

	public override void UpdateVertexData(TMP_VertexDataUpdateFlags flags)
	{
		int materialCount = m_textInfo.materialCount;
		for (int i = 0; i < materialCount; i++)
		{
			Mesh mesh = ((i != 0) ? m_subTextObjects[i].mesh : m_mesh);
			if ((flags & TMP_VertexDataUpdateFlags.Vertices) == TMP_VertexDataUpdateFlags.Vertices)
			{
				mesh.vertices = m_textInfo.meshInfo[i].vertices;
			}
			if ((flags & TMP_VertexDataUpdateFlags.Uv0) == TMP_VertexDataUpdateFlags.Uv0)
			{
				mesh.uv = m_textInfo.meshInfo[i].uvs0;
			}
			if ((flags & TMP_VertexDataUpdateFlags.Uv2) == TMP_VertexDataUpdateFlags.Uv2)
			{
				mesh.uv2 = m_textInfo.meshInfo[i].uvs2;
			}
			if ((flags & TMP_VertexDataUpdateFlags.Colors32) == TMP_VertexDataUpdateFlags.Colors32)
			{
				mesh.colors32 = m_textInfo.meshInfo[i].colors32;
			}
			mesh.RecalculateBounds();
		}
	}

	public override void UpdateVertexData()
	{
		int materialCount = m_textInfo.materialCount;
		for (int i = 0; i < materialCount; i++)
		{
			Mesh mesh;
			if (i == 0)
			{
				mesh = m_mesh;
			}
			else
			{
				m_textInfo.meshInfo[i].ClearUnusedVertices();
				mesh = m_subTextObjects[i].mesh;
			}
			mesh.vertices = m_textInfo.meshInfo[i].vertices;
			mesh.uv = m_textInfo.meshInfo[i].uvs0;
			mesh.uv2 = m_textInfo.meshInfo[i].uvs2;
			mesh.colors32 = m_textInfo.meshInfo[i].colors32;
			mesh.RecalculateBounds();
		}
	}

	public void UpdateFontAsset()
	{
		LoadFontAsset();
	}

	public void CalculateLayoutInputHorizontal()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		m_currentAutoSizeMode = m_enableAutoSizing;
		if (m_isCalculateSizeRequired || m_rectTransform.hasChanged)
		{
			m_minWidth = 0f;
			m_flexibleWidth = 0f;
			if (m_enableAutoSizing)
			{
				m_fontSize = m_fontSizeMax;
			}
			m_marginWidth = TMP_Text.k_LargePositiveFloat;
			m_marginHeight = TMP_Text.k_LargePositiveFloat;
			if (m_isInputParsingRequired || m_isTextTruncated)
			{
				ParseInputText();
			}
			GenerateTextMesh();
			m_renderMode = TextRenderFlags.Render;
			ComputeMarginSize();
			m_isLayoutDirty = true;
		}
	}

	public void CalculateLayoutInputVertical()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (m_isCalculateSizeRequired || m_rectTransform.hasChanged)
		{
			m_minHeight = 0f;
			m_flexibleHeight = 0f;
			if (m_enableAutoSizing)
			{
				m_currentAutoSizeMode = true;
				m_enableAutoSizing = false;
			}
			m_marginHeight = TMP_Text.k_LargePositiveFloat;
			GenerateTextMesh();
			m_enableAutoSizing = m_currentAutoSizeMode;
			m_renderMode = TextRenderFlags.Render;
			ComputeMarginSize();
			m_isLayoutDirty = true;
		}
		m_isCalculateSizeRequired = false;
	}

	protected override void Awake()
	{
		m_renderer = GetComponent<Renderer>();
		if (m_renderer == null)
		{
			m_renderer = base.gameObject.AddComponent<Renderer>();
		}
		if (base.canvasRenderer != null)
		{
			base.canvasRenderer.hideFlags = HideFlags.HideInInspector;
		}
		else
		{
			base.gameObject.AddComponent<CanvasRenderer>().hideFlags = HideFlags.HideInInspector;
		}
		m_rectTransform = base.rectTransform;
		m_transform = transform;
		m_meshFilter = GetComponent<MeshFilter>();
		if (m_meshFilter == null)
		{
			m_meshFilter = base.gameObject.AddComponent<MeshFilter>();
		}
		if (m_mesh == null)
		{
			m_mesh = new Mesh();
			m_mesh.hideFlags = HideFlags.HideAndDontSave;
			m_meshFilter.mesh = m_mesh;
			m_textInfo = new TMP_TextInfo(this);
		}
		m_meshFilter.hideFlags = HideFlags.HideInInspector;
		LoadDefaultSettings();
		LoadFontAsset();
		if (m_TextParsingBuffer == null)
		{
			m_TextParsingBuffer = new UnicodeChar[m_max_characters];
		}
		m_cached_TextElement = new TMP_Character();
		m_isFirstAllocation = true;
		if (m_fontAsset == null)
		{
			UnityEngine.Debug.LogWarning("Please assign a Font Asset to this " + transform.name + " gameobject.", this);
			return;
		}
		TMP_SubMesh[] componentsInChildren = GetComponentsInChildren<TMP_SubMesh>();
		if (componentsInChildren.Length != 0)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				m_subTextObjects[i + 1] = componentsInChildren[i];
			}
		}
		m_isInputParsingRequired = true;
		m_havePropertiesChanged = true;
		m_isCalculateSizeRequired = true;
		m_isAwake = true;
	}

	protected override void OnEnable()
	{
		if (m_isAwake)
		{
			if (!m_isRegisteredForEvents)
			{
				m_isRegisteredForEvents = true;
			}
			if (!m_IsTextObjectScaleStatic)
			{
				TMP_UpdateManager.RegisterTextObjectForUpdate(this);
			}
			meshFilter.sharedMesh = mesh;
			SetActiveSubMeshes(state: true);
			ComputeMarginSize();
			m_isInputParsingRequired = true;
			m_havePropertiesChanged = true;
			m_verticesAlreadyDirty = false;
			SetVerticesDirty();
		}
	}

	protected override void OnDisable()
	{
		if (m_isAwake)
		{
			TMP_UpdateManager.UnRegisterTextElementForRebuild(this);
			TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
			m_meshFilter.sharedMesh = null;
			SetActiveSubMeshes(state: false);
		}
	}

	protected override void OnDestroy()
	{
		if (m_mesh != null)
		{
			UnityEngine.Object.DestroyImmediate(m_mesh);
		}
		m_isRegisteredForEvents = false;
		TMP_UpdateManager.UnRegisterTextElementForRebuild(this);
		TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
	}

	protected override void LoadFontAsset()
	{
		ShaderUtilities.GetShaderPropertyIDs();
		if (m_fontAsset == null)
		{
			if (TMP_Settings.defaultFontAsset != null)
			{
				m_fontAsset = TMP_Settings.defaultFontAsset;
			}
			else
			{
				m_fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
			}
			if (m_fontAsset == null)
			{
				UnityEngine.Debug.LogWarning("The LiberationSans SDF Font Asset was not found. There is no Font Asset assigned to " + base.gameObject.name + ".", this);
				return;
			}
			if (m_fontAsset.characterLookupTable == null)
			{
				UnityEngine.Debug.Log("Dictionary is Null!");
			}
			m_renderer.sharedMaterial = m_fontAsset.material;
			m_sharedMaterial = m_fontAsset.material;
			m_sharedMaterial.SetFloat("_CullMode", 0f);
			m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 4f);
			m_renderer.receiveShadows = false;
			m_renderer.shadowCastingMode = ShadowCastingMode.Off;
		}
		else
		{
			if (m_fontAsset.characterLookupTable == null)
			{
				m_fontAsset.ReadFontAssetDefinition();
			}
			if (m_renderer.sharedMaterial == null || m_renderer.sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex) == null || m_fontAsset.atlasTexture.GetInstanceID() != m_renderer.sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
			{
				m_renderer.sharedMaterial = m_fontAsset.material;
				m_sharedMaterial = m_fontAsset.material;
			}
			else
			{
				m_sharedMaterial = m_renderer.sharedMaterial;
			}
			m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 4f);
			if (m_sharedMaterial.passCount == 1)
			{
				m_renderer.receiveShadows = false;
				m_renderer.shadowCastingMode = ShadowCastingMode.Off;
			}
		}
		m_padding = GetPaddingForMaterial();
		m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
		GetSpecialCharacters(m_fontAsset);
	}

	private void UpdateEnvMapMatrix()
	{
		if (m_sharedMaterial.HasProperty(ShaderUtilities.ID_EnvMap) && !(m_sharedMaterial.GetTexture(ShaderUtilities.ID_EnvMap) == null))
		{
			Vector3 euler = m_sharedMaterial.GetVector(ShaderUtilities.ID_EnvMatrixRotation);
			m_EnvMapMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(euler), Vector3.one);
			m_sharedMaterial.SetMatrix(ShaderUtilities.ID_EnvMatrix, m_EnvMapMatrix);
		}
	}

	private void SetMask(MaskingTypes maskType)
	{
		switch (maskType)
		{
		case MaskingTypes.MaskOff:
			m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
			m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
			m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
			break;
		case MaskingTypes.MaskSoft:
			m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
			m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
			m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
			break;
		case MaskingTypes.MaskHard:
			m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_HARD);
			m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
			m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
			break;
		}
	}

	private void SetMaskCoordinates(Vector4 coords)
	{
		m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, coords);
	}

	private void SetMaskCoordinates(Vector4 coords, float softX, float softY)
	{
		m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, coords);
		m_sharedMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessX, softX);
		m_sharedMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessY, softY);
	}

	private void EnableMasking()
	{
		if (m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
		{
			m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
			m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
			m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
			m_isMaskingEnabled = true;
			UpdateMask();
		}
	}

	private void DisableMasking()
	{
		if (m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
		{
			m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
			m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
			m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
			m_isMaskingEnabled = false;
			UpdateMask();
		}
	}

	private void UpdateMask()
	{
		if (m_isMaskingEnabled && m_isMaskingEnabled && m_fontMaterial == null)
		{
			CreateMaterialInstance();
		}
	}

	protected override Material GetMaterial(Material mat)
	{
		if (m_fontMaterial == null || m_fontMaterial.GetInstanceID() != mat.GetInstanceID())
		{
			m_fontMaterial = CreateMaterialInstance(mat);
		}
		m_sharedMaterial = m_fontMaterial;
		m_padding = GetPaddingForMaterial();
		SetVerticesDirty();
		SetMaterialDirty();
		return m_sharedMaterial;
	}

	protected override Material[] GetMaterials(Material[] mats)
	{
		int materialCount = m_textInfo.materialCount;
		if (m_fontMaterials == null)
		{
			m_fontMaterials = new Material[materialCount];
		}
		else if (m_fontMaterials.Length != materialCount)
		{
			TMP_TextInfo.Resize(ref m_fontMaterials, materialCount, isBlockAllocated: false);
		}
		for (int i = 0; i < materialCount; i++)
		{
			if (i == 0)
			{
				m_fontMaterials[i] = base.fontMaterial;
			}
			else
			{
				m_fontMaterials[i] = m_subTextObjects[i].material;
			}
		}
		m_fontSharedMaterials = m_fontMaterials;
		return m_fontMaterials;
	}

	protected override void SetSharedMaterial(Material mat)
	{
		m_sharedMaterial = mat;
		m_padding = GetPaddingForMaterial();
		SetMaterialDirty();
	}

	protected override Material[] GetSharedMaterials()
	{
		int materialCount = m_textInfo.materialCount;
		if (m_fontSharedMaterials == null)
		{
			m_fontSharedMaterials = new Material[materialCount];
		}
		else if (m_fontSharedMaterials.Length != materialCount)
		{
			TMP_TextInfo.Resize(ref m_fontSharedMaterials, materialCount, isBlockAllocated: false);
		}
		for (int i = 0; i < materialCount; i++)
		{
			if (i == 0)
			{
				m_fontSharedMaterials[i] = m_sharedMaterial;
			}
			else
			{
				m_fontSharedMaterials[i] = m_subTextObjects[i].sharedMaterial;
			}
		}
		return m_fontSharedMaterials;
	}

	protected override void SetSharedMaterials(Material[] materials)
	{
		int materialCount = m_textInfo.materialCount;
		if (m_fontSharedMaterials == null)
		{
			m_fontSharedMaterials = new Material[materialCount];
		}
		else if (m_fontSharedMaterials.Length != materialCount)
		{
			TMP_TextInfo.Resize(ref m_fontSharedMaterials, materialCount, isBlockAllocated: false);
		}
		for (int i = 0; i < materialCount; i++)
		{
			Texture texture = materials[i].GetTexture(ShaderUtilities.ID_MainTex);
			if (i == 0)
			{
				if (!(texture == null) && texture.GetInstanceID() == m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
				{
					m_sharedMaterial = (m_fontSharedMaterials[i] = materials[i]);
					m_padding = GetPaddingForMaterial(m_sharedMaterial);
				}
			}
			else if (!(texture == null) && texture.GetInstanceID() == m_subTextObjects[i].sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID() && m_subTextObjects[i].isDefaultMaterial)
			{
				m_subTextObjects[i].sharedMaterial = (m_fontSharedMaterials[i] = materials[i]);
			}
		}
	}

	protected override void SetOutlineThickness(float thickness)
	{
		thickness = Mathf.Clamp01(thickness);
		m_renderer.material.SetFloat(ShaderUtilities.ID_OutlineWidth, thickness);
		if (m_fontMaterial == null)
		{
			m_fontMaterial = m_renderer.material;
		}
		m_fontMaterial = m_renderer.material;
		m_sharedMaterial = m_fontMaterial;
		m_padding = GetPaddingForMaterial();
	}

	protected override void SetFaceColor(Color32 color)
	{
		m_renderer.material.SetColor(ShaderUtilities.ID_FaceColor, color);
		if (m_fontMaterial == null)
		{
			m_fontMaterial = m_renderer.material;
		}
		m_sharedMaterial = m_fontMaterial;
	}

	protected override void SetOutlineColor(Color32 color)
	{
		m_renderer.material.SetColor(ShaderUtilities.ID_OutlineColor, color);
		if (m_fontMaterial == null)
		{
			m_fontMaterial = m_renderer.material;
		}
		m_sharedMaterial = m_fontMaterial;
	}

	private void CreateMaterialInstance()
	{
		Material material = new Material(m_sharedMaterial);
		material.shaderKeywords = m_sharedMaterial.shaderKeywords;
		material.name += " Instance";
		m_fontMaterial = material;
	}

	protected override void SetShaderDepth()
	{
		if (m_isOverlay)
		{
			m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 0f);
			m_renderer.material.renderQueue = 4000;
			m_sharedMaterial = m_renderer.material;
		}
		else
		{
			m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 4f);
			m_renderer.material.renderQueue = -1;
			m_sharedMaterial = m_renderer.material;
		}
	}

	protected override void SetCulling()
	{
		if (m_isCullingEnabled)
		{
			m_renderer.material.SetFloat("_CullMode", 2f);
			for (int i = 1; i < m_subTextObjects.Length && m_subTextObjects[i] != null; i++)
			{
				Renderer renderer = m_subTextObjects[i].renderer;
				if (renderer != null)
				{
					renderer.material.SetFloat(ShaderUtilities.ShaderTag_CullMode, 2f);
				}
			}
			return;
		}
		m_renderer.material.SetFloat("_CullMode", 0f);
		for (int j = 1; j < m_subTextObjects.Length && m_subTextObjects[j] != null; j++)
		{
			Renderer renderer2 = m_subTextObjects[j].renderer;
			if (renderer2 != null)
			{
				renderer2.material.SetFloat(ShaderUtilities.ShaderTag_CullMode, 0f);
			}
		}
	}

	private void SetPerspectiveCorrection()
	{
		if (m_isOrthographic)
		{
			m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0f);
		}
		else
		{
			m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0.875f);
		}
	}

	protected override float GetPaddingForMaterial(Material mat)
	{
		m_padding = ShaderUtilities.GetPadding(mat, m_enableExtraPadding, m_isUsingBold);
		m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
		m_isSDFShader = mat.HasProperty(ShaderUtilities.ID_WeightNormal);
		return m_padding;
	}

	protected override float GetPaddingForMaterial()
	{
		ShaderUtilities.GetShaderPropertyIDs();
		if (m_sharedMaterial == null)
		{
			return 0f;
		}
		m_padding = ShaderUtilities.GetPadding(m_sharedMaterial, m_enableExtraPadding, m_isUsingBold);
		m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
		m_isSDFShader = m_sharedMaterial.HasProperty(ShaderUtilities.ID_WeightNormal);
		return m_padding;
	}

	protected override int SetArraySizes(UnicodeChar[] chars)
	{
		int spriteCount = 0;
		m_totalCharacterCount = 0;
		m_isUsingBold = false;
		m_isParsingText = false;
		tag_NoParsing = false;
		m_FontStyleInternal = m_fontStyle;
		m_FontWeightInternal = (((m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? FontWeight.Bold : m_fontWeight);
		m_FontWeightStack.SetDefault(m_FontWeightInternal);
		m_currentFontAsset = m_fontAsset;
		m_currentMaterial = m_sharedMaterial;
		m_currentMaterialIndex = 0;
		m_materialReferenceStack.SetDefault(new MaterialReference(m_currentMaterialIndex, m_currentFontAsset, m_currentMaterial, m_padding));
		m_materialReferenceIndexLookup.Clear();
		MaterialReference.AddMaterialReference(m_currentMaterial, m_currentFontAsset, m_materialReferences, m_materialReferenceIndexLookup);
		if (m_textInfo == null)
		{
			m_textInfo = new TMP_TextInfo();
		}
		m_textElementType = TMP_TextElementType.Character;
		if (m_linkedTextComponent != null)
		{
			m_linkedTextComponent.text = string.Empty;
			m_linkedTextComponent.ForceMeshUpdate();
		}
		for (int i = 0; i < chars.Length && chars[i].unicode != 0; i++)
		{
			if (m_textInfo.characterInfo == null || m_totalCharacterCount >= m_textInfo.characterInfo.Length)
			{
				TMP_TextInfo.Resize(ref m_textInfo.characterInfo, m_totalCharacterCount + 1, isBlockAllocated: true);
			}
			int num = chars[i].unicode;
			if (m_isRichText && num == 60)
			{
				_ = m_currentMaterialIndex;
				if (ValidateHtmlTag(chars, i + 1, out var endIndex))
				{
					_ = ref chars[i];
					i = endIndex;
					if ((m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
					{
						m_isUsingBold = true;
					}
					continue;
				}
			}
			bool isAlternativeTypeface = false;
			bool flag = false;
			TMP_FontAsset currentFontAsset = m_currentFontAsset;
			Material currentMaterial = m_currentMaterial;
			int currentMaterialIndex = m_currentMaterialIndex;
			if (m_textElementType == TMP_TextElementType.Character)
			{
				if ((m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
				{
					if (char.IsLower((char)num))
					{
						num = char.ToUpper((char)num);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
				{
					if (char.IsUpper((char)num))
					{
						num = char.ToLower((char)num);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num))
				{
					num = char.ToUpper((char)num);
				}
			}
			TMP_FontAsset fontAsset;
			TMP_Character tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num, m_currentFontAsset, includeFallbacks: false, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
			if (tMP_Character == null && m_currentFontAsset.fallbackFontAssetTable != null && m_currentFontAsset.fallbackFontAssetTable.Count > 0)
			{
				tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets((uint)num, m_currentFontAsset.fallbackFontAssetTable, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
			}
			if (tMP_Character == null && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
			{
				tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets((uint)num, TMP_Settings.fallbackFontAssets, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
			}
			if (tMP_Character == null && TMP_Settings.defaultFontAsset != null)
			{
				tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num, TMP_Settings.defaultFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
			}
			if (tMP_Character == null)
			{
				int num2 = num;
				num = (chars[i].unicode = ((TMP_Settings.missingGlyphCharacter == 0) ? 9633 : TMP_Settings.missingGlyphCharacter));
				tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num, m_currentFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
				if (tMP_Character == null && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
				{
					tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets((uint)num, TMP_Settings.fallbackFontAssets, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
				}
				if (tMP_Character == null && TMP_Settings.defaultFontAsset != null)
				{
					tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num, TMP_Settings.defaultFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
				}
				if (tMP_Character == null)
				{
					num = (chars[i].unicode = 32);
					tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num, m_currentFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
					if (!TMP_Settings.warningsDisabled)
					{
						UnityEngine.Debug.LogWarning("Character with ASCII value of " + num2 + " was not found in the Font Asset Glyph Table. It was replaced by a space.", this);
					}
				}
			}
			if (fontAsset != null && fontAsset.GetInstanceID() != m_currentFontAsset.GetInstanceID())
			{
				flag = true;
				m_currentFontAsset = fontAsset;
			}
			m_textInfo.characterInfo[m_totalCharacterCount].elementType = TMP_TextElementType.Character;
			m_textInfo.characterInfo[m_totalCharacterCount].textElement = tMP_Character;
			m_textInfo.characterInfo[m_totalCharacterCount].isUsingAlternateTypeface = isAlternativeTypeface;
			m_textInfo.characterInfo[m_totalCharacterCount].character = (char)num;
			m_textInfo.characterInfo[m_totalCharacterCount].fontAsset = m_currentFontAsset;
			m_textInfo.characterInfo[m_totalCharacterCount].index = chars[i].stringIndex;
			m_textInfo.characterInfo[m_totalCharacterCount].stringLength = chars[i].length;
			if (flag)
			{
				if (TMP_Settings.matchMaterialPreset)
				{
					m_currentMaterial = TMP_MaterialManager.GetFallbackMaterial(m_currentMaterial, m_currentFontAsset.material);
				}
				else
				{
					m_currentMaterial = m_currentFontAsset.material;
				}
				m_currentMaterialIndex = MaterialReference.AddMaterialReference(m_currentMaterial, m_currentFontAsset, m_materialReferences, m_materialReferenceIndexLookup);
			}
			if (!char.IsWhiteSpace((char)num) && num != 8203)
			{
				if (m_materialReferences[m_currentMaterialIndex].referenceCount < 16383)
				{
					m_materialReferences[m_currentMaterialIndex].referenceCount++;
				}
				else
				{
					m_currentMaterialIndex = MaterialReference.AddMaterialReference(new Material(m_currentMaterial), m_currentFontAsset, m_materialReferences, m_materialReferenceIndexLookup);
					m_materialReferences[m_currentMaterialIndex].referenceCount++;
				}
			}
			m_textInfo.characterInfo[m_totalCharacterCount].material = m_currentMaterial;
			m_textInfo.characterInfo[m_totalCharacterCount].materialReferenceIndex = m_currentMaterialIndex;
			m_materialReferences[m_currentMaterialIndex].isFallbackMaterial = flag;
			if (flag)
			{
				m_materialReferences[m_currentMaterialIndex].fallbackMaterial = currentMaterial;
				m_currentFontAsset = currentFontAsset;
				m_currentMaterial = currentMaterial;
				m_currentMaterialIndex = currentMaterialIndex;
			}
			m_totalCharacterCount++;
		}
		if (m_isCalculatingPreferredValues)
		{
			m_isCalculatingPreferredValues = false;
			m_isInputParsingRequired = true;
			return m_totalCharacterCount;
		}
		m_textInfo.spriteCount = spriteCount;
		int num3 = (m_textInfo.materialCount = m_materialReferenceIndexLookup.Count);
		if (num3 > m_textInfo.meshInfo.Length)
		{
			TMP_TextInfo.Resize(ref m_textInfo.meshInfo, num3, isBlockAllocated: false);
		}
		if (num3 > m_subTextObjects.Length)
		{
			TMP_TextInfo.Resize(ref m_subTextObjects, Mathf.NextPowerOfTwo(num3 + 1));
		}
		if (m_textInfo.characterInfo.Length - m_totalCharacterCount > 256)
		{
			TMP_TextInfo.Resize(ref m_textInfo.characterInfo, Mathf.Max(m_totalCharacterCount + 1, 256), isBlockAllocated: true);
		}
		for (int j = 0; j < num3; j++)
		{
			if (j > 0)
			{
				if (m_subTextObjects[j] == null)
				{
					m_subTextObjects[j] = TMP_SubMesh.AddSubTextObject(this, m_materialReferences[j]);
					m_textInfo.meshInfo[j].vertices = null;
				}
				if (m_subTextObjects[j].sharedMaterial == null || m_subTextObjects[j].sharedMaterial.GetInstanceID() != m_materialReferences[j].material.GetInstanceID())
				{
					bool isDefaultMaterial = m_materialReferences[j].isDefaultMaterial;
					m_subTextObjects[j].isDefaultMaterial = isDefaultMaterial;
					if (!isDefaultMaterial || m_subTextObjects[j].sharedMaterial == null || m_subTextObjects[j].sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID() != m_materialReferences[j].material.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
					{
						m_subTextObjects[j].sharedMaterial = m_materialReferences[j].material;
						m_subTextObjects[j].fontAsset = m_materialReferences[j].fontAsset;
					}
				}
				if (m_materialReferences[j].isFallbackMaterial)
				{
					m_subTextObjects[j].fallbackMaterial = m_materialReferences[j].material;
					m_subTextObjects[j].fallbackSourceMaterial = m_materialReferences[j].fallbackMaterial;
				}
			}
			int referenceCount = m_materialReferences[j].referenceCount;
			if (m_textInfo.meshInfo[j].vertices == null || m_textInfo.meshInfo[j].vertices.Length < referenceCount * ((!m_isVolumetricText) ? 4 : 8))
			{
				if (m_textInfo.meshInfo[j].vertices == null)
				{
					if (j == 0)
					{
						m_textInfo.meshInfo[j] = new TMP_MeshInfo(m_mesh, referenceCount + 1, m_isVolumetricText);
					}
					else
					{
						m_textInfo.meshInfo[j] = new TMP_MeshInfo(m_subTextObjects[j].mesh, referenceCount + 1, m_isVolumetricText);
					}
				}
				else
				{
					m_textInfo.meshInfo[j].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount + 1), m_isVolumetricText);
				}
			}
			else if (m_VertexBufferAutoSizeReduction && referenceCount > 0 && m_textInfo.meshInfo[j].vertices.Length - referenceCount * ((!m_isVolumetricText) ? 4 : 8) > 1024)
			{
				m_textInfo.meshInfo[j].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount + 1), m_isVolumetricText);
			}
		}
		for (int k = num3; k < m_subTextObjects.Length && m_subTextObjects[k] != null; k++)
		{
			if (k < m_textInfo.meshInfo.Length)
			{
				m_textInfo.meshInfo[k].ClearUnusedVertices(0, updateMesh: true);
			}
		}
		return m_totalCharacterCount;
	}

	public override void ComputeMarginSize()
	{
		if (base.rectTransform != null)
		{
			m_marginWidth = m_rectTransform.rect.width - m_margin.x - m_margin.z;
			m_marginHeight = m_rectTransform.rect.height - m_margin.y - m_margin.w;
			m_RectTransformCorners = GetTextContainerLocalCorners();
		}
	}

	protected override void OnDidApplyAnimationProperties()
	{
		m_havePropertiesChanged = true;
		isMaskUpdateRequired = true;
		SetVerticesDirty();
	}

	protected override void OnTransformParentChanged()
	{
		SetVerticesDirty();
		SetLayoutDirty();
	}

	protected override void OnRectTransformDimensionsChange()
	{
		ComputeMarginSize();
		SetVerticesDirty();
		SetLayoutDirty();
	}

	internal override void InternalUpdate()
	{
		if (!m_havePropertiesChanged)
		{
			float y = m_rectTransform.lossyScale.y;
			if (y != m_previousLossyScaleY && m_text != string.Empty && m_text != null)
			{
				float scaleDelta = y / m_previousLossyScaleY;
				UpdateSDFScale(scaleDelta);
				m_previousLossyScaleY = y;
			}
		}
		if (m_isUsingLegacyAnimationComponent)
		{
			m_havePropertiesChanged = true;
			OnPreRenderObject();
		}
	}

	private void OnPreRenderObject()
	{
		if (!m_isAwake || (!IsActive() && !m_ignoreActiveState))
		{
			return;
		}
		loopCountA = 0;
		if (m_havePropertiesChanged || m_isLayoutDirty)
		{
			if (isMaskUpdateRequired)
			{
				UpdateMask();
				isMaskUpdateRequired = false;
			}
			if (checkPaddingRequired)
			{
				UpdateMeshPadding();
			}
			if (m_isInputParsingRequired || m_isTextTruncated)
			{
				ParseInputText();
			}
			if (m_enableAutoSizing)
			{
				m_fontSize = Mathf.Clamp(m_fontSizeBase, m_fontSizeMin, m_fontSizeMax);
			}
			m_maxFontSize = m_fontSizeMax;
			m_minFontSize = m_fontSizeMin;
			m_lineSpacingDelta = 0f;
			m_charWidthAdjDelta = 0f;
			m_isCharacterWrappingEnabled = false;
			m_isTextTruncated = false;
			m_havePropertiesChanged = false;
			m_isLayoutDirty = false;
			m_ignoreActiveState = false;
			GenerateTextMesh();
		}
	}

	protected override void GenerateTextMesh()
	{
		if (m_fontAsset == null || m_fontAsset.characterLookupTable == null)
		{
			UnityEngine.Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned to Object ID: " + GetInstanceID());
			return;
		}
		if (m_textInfo != null)
		{
			m_textInfo.Clear();
		}
		if (m_TextParsingBuffer == null || m_TextParsingBuffer.Length == 0 || m_TextParsingBuffer[0].unicode == 0)
		{
			ClearMesh(updateMesh: true);
			m_preferredWidth = 0f;
			m_preferredHeight = 0f;
			TMPro_EventManager.ON_TEXT_CHANGED(this);
			return;
		}
		m_currentFontAsset = m_fontAsset;
		m_currentMaterial = m_sharedMaterial;
		m_currentMaterialIndex = 0;
		m_materialReferenceStack.SetDefault(new MaterialReference(m_currentMaterialIndex, m_currentFontAsset, m_currentMaterial, m_padding));
		int totalCharacterCount = m_totalCharacterCount;
		float num = (m_fontScale = m_fontSize / (float)m_fontAsset.faceInfo.pointSize * m_fontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f));
		float num2 = num;
		m_fontScaleMultiplier = 1f;
		m_currentFontSize = m_fontSize;
		m_sizeStack.SetDefault(m_currentFontSize);
		float num3 = 0f;
		int num4 = 0;
		m_FontStyleInternal = m_fontStyle;
		m_FontWeightInternal = (((m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? FontWeight.Bold : m_fontWeight);
		m_FontWeightStack.SetDefault(m_FontWeightInternal);
		m_fontStyleStack.Clear();
		m_lineJustification = m_textAlignment;
		m_lineJustificationStack.SetDefault(m_lineJustification);
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 1f;
		m_baselineOffset = 0f;
		m_baselineOffsetStack.Clear();
		bool flag = false;
		Vector3 start = Vector3.zero;
		Vector3 zero = Vector3.zero;
		bool flag2 = false;
		Vector3 start2 = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		bool flag3 = false;
		Vector3 start3 = Vector3.zero;
		Vector3 vector = Vector3.zero;
		m_fontColor32 = m_fontColor;
		m_htmlColor = m_fontColor32;
		m_underlineColor = m_htmlColor;
		m_strikethroughColor = m_htmlColor;
		m_colorStack.SetDefault(m_htmlColor);
		m_underlineColorStack.SetDefault(m_htmlColor);
		m_strikethroughColorStack.SetDefault(m_htmlColor);
		m_highlightColorStack.SetDefault(m_htmlColor);
		m_colorGradientPreset = null;
		m_colorGradientStack.SetDefault(null);
		m_actionStack.Clear();
		m_isFXMatrixSet = false;
		m_lineOffset = 0f;
		m_lineHeight = -32767f;
		float num8 = m_currentFontAsset.faceInfo.lineHeight - (m_currentFontAsset.faceInfo.ascentLine - m_currentFontAsset.faceInfo.descentLine);
		m_cSpacing = 0f;
		m_monoSpacing = 0f;
		float num9 = 0f;
		m_xAdvance = 0f;
		tag_LineIndent = 0f;
		tag_Indent = 0f;
		m_indentStack.SetDefault(0f);
		tag_NoParsing = false;
		m_characterCount = 0;
		m_firstCharacterOfLine = 0;
		m_lastCharacterOfLine = 0;
		m_firstVisibleCharacterOfLine = 0;
		m_lastVisibleCharacterOfLine = 0;
		m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
		m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
		m_lineNumber = 0;
		m_lineVisibleCharacterCount = 0;
		bool flag4 = true;
		m_firstOverflowCharacterIndex = -1;
		m_pageNumber = 0;
		int num10 = Mathf.Clamp(m_pageToDisplay - 1, 0, m_textInfo.pageInfo.Length - 1);
		int num11 = 0;
		int num12 = 0;
		Vector4 vector2 = m_margin;
		float marginWidth = m_marginWidth;
		float marginHeight = m_marginHeight;
		m_marginLeft = 0f;
		m_marginRight = 0f;
		m_width = -1f;
		float num13 = marginWidth + 0.0001f - m_marginLeft - m_marginRight;
		m_meshExtents.min = TMP_Text.k_LargePositiveVector2;
		m_meshExtents.max = TMP_Text.k_LargeNegativeVector2;
		m_textInfo.ClearLineInfo();
		m_maxCapHeight = 0f;
		m_maxAscender = 0f;
		m_maxDescender = 0f;
		float num14 = 0f;
		float num15 = 0f;
		bool flag5 = false;
		m_isNewPage = false;
		bool flag6 = true;
		m_isNonBreakingSpace = false;
		bool flag7 = false;
		bool flag8 = false;
		int num16 = 0;
		SaveWordWrappingState(ref m_SavedWordWrapState, -1, -1);
		SaveWordWrappingState(ref m_SavedLineState, -1, -1);
		loopCountA++;
		Vector3 vector3 = default(Vector3);
		Vector3 vector4 = default(Vector3);
		Vector3 vector5 = default(Vector3);
		Vector3 vector6 = default(Vector3);
		for (int i = 0; i < m_TextParsingBuffer.Length && m_TextParsingBuffer[i].unicode != 0; i++)
		{
			num4 = m_TextParsingBuffer[i].unicode;
			if (m_isRichText && num4 == 60)
			{
				m_isParsingText = true;
				m_textElementType = TMP_TextElementType.Character;
				if (ValidateHtmlTag(m_TextParsingBuffer, i + 1, out var endIndex))
				{
					i = endIndex;
					if (m_textElementType == TMP_TextElementType.Character)
					{
						continue;
					}
				}
			}
			else
			{
				m_textElementType = m_textInfo.characterInfo[m_characterCount].elementType;
				m_currentMaterialIndex = m_textInfo.characterInfo[m_characterCount].materialReferenceIndex;
				m_currentFontAsset = m_textInfo.characterInfo[m_characterCount].fontAsset;
			}
			_ = m_currentMaterialIndex;
			bool isUsingAlternateTypeface = m_textInfo.characterInfo[m_characterCount].isUsingAlternateTypeface;
			m_isParsingText = false;
			if (m_characterCount < m_firstVisibleCharacter)
			{
				m_textInfo.characterInfo[m_characterCount].isVisible = false;
				m_textInfo.characterInfo[m_characterCount].character = '\u200b';
				m_characterCount++;
				continue;
			}
			float num17 = 1f;
			if (m_textElementType == TMP_TextElementType.Character)
			{
				if ((m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
				{
					if (char.IsLower((char)num4))
					{
						num4 = char.ToUpper((char)num4);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
				{
					if (char.IsUpper((char)num4))
					{
						num4 = char.ToLower((char)num4);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num4))
				{
					num17 = 0.8f;
					num4 = char.ToUpper((char)num4);
				}
			}
			if (m_textElementType == TMP_TextElementType.Character)
			{
				m_cached_TextElement = m_textInfo.characterInfo[m_characterCount].textElement;
				if (m_cached_TextElement == null)
				{
					continue;
				}
				m_currentFontAsset = m_textInfo.characterInfo[m_characterCount].fontAsset;
				m_currentMaterial = m_textInfo.characterInfo[m_characterCount].material;
				m_currentMaterialIndex = m_textInfo.characterInfo[m_characterCount].materialReferenceIndex;
				m_fontScale = m_currentFontSize * num17 / (float)m_currentFontAsset.faceInfo.pointSize * m_currentFontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f);
				num2 = m_fontScale * m_fontScaleMultiplier * m_cached_TextElement.scale * m_cached_TextElement.glyph.scale;
				m_textInfo.characterInfo[m_characterCount].elementType = TMP_TextElementType.Character;
				m_textInfo.characterInfo[m_characterCount].scale = num2;
				num5 = ((m_currentMaterialIndex == 0) ? m_padding : m_subTextObjects[m_currentMaterialIndex].padding);
			}
			float num18 = num2;
			if (num4 == 173)
			{
				num2 = 0f;
			}
			m_textInfo.characterInfo[m_characterCount].character = (char)num4;
			m_textInfo.characterInfo[m_characterCount].pointSize = m_currentFontSize;
			m_textInfo.characterInfo[m_characterCount].color = m_htmlColor;
			m_textInfo.characterInfo[m_characterCount].underlineColor = m_underlineColor;
			m_textInfo.characterInfo[m_characterCount].strikethroughColor = m_strikethroughColor;
			m_textInfo.characterInfo[m_characterCount].highlightColor = m_highlightColor;
			m_textInfo.characterInfo[m_characterCount].style = m_FontStyleInternal;
			TMP_GlyphValueRecord tMP_GlyphValueRecord = default(TMP_GlyphValueRecord);
			float num19 = m_characterSpacing;
			if (m_enableKerning)
			{
				if (m_characterCount < totalCharacterCount - 1)
				{
					uint glyphIndex = m_cached_TextElement.glyphIndex;
					uint glyphIndex2 = m_textInfo.characterInfo[m_characterCount + 1].textElement.glyphIndex;
					long key = new GlyphPairKey(glyphIndex, glyphIndex2).key;
					if (m_currentFontAsset.fontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(key, out var value))
					{
						tMP_GlyphValueRecord = value.firstAdjustmentRecord.glyphValueRecord;
						num19 = (((value.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num19);
					}
				}
				if (m_characterCount >= 1)
				{
					uint glyphIndex3 = m_textInfo.characterInfo[m_characterCount - 1].textElement.glyphIndex;
					uint glyphIndex4 = m_cached_TextElement.glyphIndex;
					long key2 = new GlyphPairKey(glyphIndex3, glyphIndex4).key;
					if (m_currentFontAsset.fontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(key2, out var value2))
					{
						tMP_GlyphValueRecord += value2.secondAdjustmentRecord.glyphValueRecord;
						num19 = (((value2.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num19);
					}
				}
			}
			if (m_isRightToLeft)
			{
				m_xAdvance -= ((m_cached_TextElement.glyph.metrics.horizontalAdvance * num7 + num19 + m_wordSpacing + m_currentFontAsset.normalSpacingOffset) * num2 + m_cSpacing) * (1f - m_charWidthAdjDelta);
				if (char.IsWhiteSpace((char)num4) || num4 == 8203)
				{
					m_xAdvance -= m_wordSpacing * num2;
				}
			}
			float num20 = 0f;
			if (m_monoSpacing != 0f)
			{
				num20 = (m_monoSpacing / 2f - (m_cached_TextElement.glyph.metrics.width / 2f + m_cached_TextElement.glyph.metrics.horizontalBearingX) * num2) * (1f - m_charWidthAdjDelta);
				m_xAdvance += num20;
			}
			if (m_textElementType == TMP_TextElementType.Character && !isUsingAlternateTypeface && (m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
			{
				if (m_currentMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
				{
					float num21 = m_currentMaterial.GetFloat(ShaderUtilities.ID_GradientScale);
					num6 = m_currentFontAsset.boldStyle / 4f * num21 * m_currentMaterial.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
					if (num6 + num5 > num21)
					{
						num5 = num21 - num6;
					}
				}
				else
				{
					num6 = 0f;
				}
				num7 = 1f + m_currentFontAsset.boldSpacing * 0.01f;
			}
			else
			{
				if (m_currentMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
				{
					float num22 = m_currentMaterial.GetFloat(ShaderUtilities.ID_GradientScale);
					num6 = m_currentFontAsset.normalStyle / 4f * num22 * m_currentMaterial.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
					if (num6 + num5 > num22)
					{
						num5 = num22 - num6;
					}
				}
				else
				{
					num6 = 0f;
				}
				num7 = 1f;
			}
			float num23 = m_currentFontAsset.faceInfo.baseline * m_fontScale * m_fontScaleMultiplier * m_currentFontAsset.faceInfo.scale;
			vector3.x = m_xAdvance + (m_cached_TextElement.glyph.metrics.horizontalBearingX - num5 - num6 + tMP_GlyphValueRecord.xPlacement) * num2 * (1f - m_charWidthAdjDelta);
			vector3.y = num23 + (m_cached_TextElement.glyph.metrics.horizontalBearingY + num5 + tMP_GlyphValueRecord.yPlacement) * num2 - m_lineOffset + m_baselineOffset;
			vector3.z = 0f;
			vector4.x = vector3.x;
			vector4.y = vector3.y - (m_cached_TextElement.glyph.metrics.height + num5 * 2f) * num2;
			vector4.z = 0f;
			vector5.x = vector4.x + (m_cached_TextElement.glyph.metrics.width + num5 * 2f + num6 * 2f) * num2 * (1f - m_charWidthAdjDelta);
			vector5.y = vector3.y;
			vector5.z = 0f;
			vector6.x = vector5.x;
			vector6.y = vector4.y;
			vector6.z = 0f;
			if (m_textElementType == TMP_TextElementType.Character && !isUsingAlternateTypeface && (m_FontStyleInternal & FontStyles.Italic) == FontStyles.Italic)
			{
				float num24 = (float)(int)m_currentFontAsset.italicStyle * 0.01f;
				Vector3 vector7 = new Vector3(num24 * ((m_cached_TextElement.glyph.metrics.horizontalBearingY + num5 + num6) * num2), 0f, 0f);
				Vector3 vector8 = new Vector3(num24 * ((m_cached_TextElement.glyph.metrics.horizontalBearingY - m_cached_TextElement.glyph.metrics.height - num5 - num6) * num2), 0f, 0f);
				vector3 += vector7;
				vector4 += vector8;
				vector5 += vector7;
				vector6 += vector8;
			}
			if (m_isFXMatrixSet)
			{
				_ = m_FXMatrix.lossyScale.x;
				_ = 1f;
				Vector3 vector9 = (vector5 + vector4) / 2f;
				vector3 = m_FXMatrix.MultiplyPoint3x4(vector3 - vector9) + vector9;
				vector4 = m_FXMatrix.MultiplyPoint3x4(vector4 - vector9) + vector9;
				vector5 = m_FXMatrix.MultiplyPoint3x4(vector5 - vector9) + vector9;
				vector6 = m_FXMatrix.MultiplyPoint3x4(vector6 - vector9) + vector9;
			}
			m_textInfo.characterInfo[m_characterCount].bottomLeft = vector4;
			m_textInfo.characterInfo[m_characterCount].topLeft = vector3;
			m_textInfo.characterInfo[m_characterCount].topRight = vector5;
			m_textInfo.characterInfo[m_characterCount].bottomRight = vector6;
			m_textInfo.characterInfo[m_characterCount].origin = m_xAdvance;
			m_textInfo.characterInfo[m_characterCount].baseLine = num23 - m_lineOffset + m_baselineOffset;
			m_textInfo.characterInfo[m_characterCount].aspectRatio = (vector5.x - vector4.x) / (vector3.y - vector4.y);
			float num25 = m_currentFontAsset.faceInfo.ascentLine * ((m_textElementType == TMP_TextElementType.Character) ? (num2 / num17) : m_textInfo.characterInfo[m_characterCount].scale) + m_baselineOffset;
			m_textInfo.characterInfo[m_characterCount].ascender = num25 - m_lineOffset;
			m_maxLineAscender = ((num25 > m_maxLineAscender) ? num25 : m_maxLineAscender);
			float num26 = m_currentFontAsset.faceInfo.descentLine * ((m_textElementType == TMP_TextElementType.Character) ? (num2 / num17) : m_textInfo.characterInfo[m_characterCount].scale) + m_baselineOffset;
			float num27 = (m_textInfo.characterInfo[m_characterCount].descender = num26 - m_lineOffset);
			m_maxLineDescender = ((num26 < m_maxLineDescender) ? num26 : m_maxLineDescender);
			if ((m_FontStyleInternal & FontStyles.Subscript) == FontStyles.Subscript || (m_FontStyleInternal & FontStyles.Superscript) == FontStyles.Superscript)
			{
				float num28 = (num25 - m_baselineOffset) / m_currentFontAsset.faceInfo.subscriptSize;
				num25 = m_maxLineAscender;
				m_maxLineAscender = ((num28 > m_maxLineAscender) ? num28 : m_maxLineAscender);
				float num29 = (num26 - m_baselineOffset) / m_currentFontAsset.faceInfo.subscriptSize;
				num26 = m_maxLineDescender;
				m_maxLineDescender = ((num29 < m_maxLineDescender) ? num29 : m_maxLineDescender);
			}
			if (m_lineNumber == 0 || m_isNewPage)
			{
				m_maxAscender = ((m_maxAscender > num25) ? m_maxAscender : num25);
				m_maxCapHeight = Mathf.Max(m_maxCapHeight, m_currentFontAsset.faceInfo.capLine * num2 / num17);
			}
			if (m_lineOffset == 0f)
			{
				num14 = ((num14 > num25) ? num14 : num25);
			}
			m_textInfo.characterInfo[m_characterCount].isVisible = false;
			if (num4 == 9 || num4 == 160 || num4 == 8199 || (!char.IsWhiteSpace((char)num4) && num4 != 8203))
			{
				m_textInfo.characterInfo[m_characterCount].isVisible = true;
				num13 = ((m_width != -1f) ? Mathf.Min(marginWidth + 0.0001f - m_marginLeft - m_marginRight, m_width) : (marginWidth + 0.0001f - m_marginLeft - m_marginRight));
				m_textInfo.lineInfo[m_lineNumber].marginLeft = m_marginLeft;
				bool flag9 = (m_lineJustification & (TextAlignmentOptions)16) == (TextAlignmentOptions)16 || (m_lineJustification & (TextAlignmentOptions)8) == (TextAlignmentOptions)8;
				if (Mathf.Abs(m_xAdvance) + ((!m_isRightToLeft) ? m_cached_TextElement.glyph.metrics.horizontalAdvance : 0f) * (1f - m_charWidthAdjDelta) * ((num4 != 173) ? num2 : num18) > num13 * (flag9 ? 1.05f : 1f))
				{
					num12 = m_characterCount - 1;
					if (base.enableWordWrapping && m_characterCount != m_firstCharacterOfLine)
					{
						if (num16 == m_SavedWordWrapState.previous_WordBreak || flag6)
						{
							if (m_enableAutoSizing && m_fontSize > m_fontSizeMin)
							{
								if (m_charWidthAdjDelta < m_charWidthMaxAdj / 100f)
								{
									loopCountA = 0;
									m_charWidthAdjDelta += 0.01f;
									GenerateTextMesh();
									return;
								}
								m_maxFontSize = m_fontSize;
								m_fontSize -= Mathf.Max((m_fontSize - m_minFontSize) / 2f, 0.05f);
								m_fontSize = (float)(int)(Mathf.Max(m_fontSize, m_fontSizeMin) * 20f + 0.5f) / 20f;
								if (loopCountA <= 20)
								{
									GenerateTextMesh();
								}
								return;
							}
							if (!m_isCharacterWrappingEnabled)
							{
								if (!flag7)
								{
									flag7 = true;
								}
								else
								{
									m_isCharacterWrappingEnabled = true;
								}
							}
							else
							{
								flag8 = true;
							}
						}
						i = RestoreWordWrappingState(ref m_SavedWordWrapState);
						num16 = i;
						if (m_TextParsingBuffer[i].unicode == 173)
						{
							m_isTextTruncated = true;
							m_TextParsingBuffer[i].unicode = 45;
							GenerateTextMesh();
							return;
						}
						if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == -32767f && !m_isNewPage)
						{
							float num30 = m_maxLineAscender - m_startOfLineAscender;
							AdjustLineOffset(m_firstCharacterOfLine, m_characterCount, num30);
							m_lineOffset += num30;
							m_SavedWordWrapState.lineOffset = m_lineOffset;
							m_SavedWordWrapState.previousLineAscender = m_maxLineAscender;
						}
						m_isNewPage = false;
						float num31 = m_maxLineAscender - m_lineOffset;
						float num32 = m_maxLineDescender - m_lineOffset;
						m_maxDescender = ((m_maxDescender < num32) ? m_maxDescender : num32);
						if (!flag5)
						{
							num15 = m_maxDescender;
						}
						if (m_useMaxVisibleDescender && (m_characterCount >= m_maxVisibleCharacters || m_lineNumber >= m_maxVisibleLines))
						{
							flag5 = true;
						}
						m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex = m_firstCharacterOfLine;
						m_textInfo.lineInfo[m_lineNumber].firstVisibleCharacterIndex = (m_firstVisibleCharacterOfLine = ((m_firstCharacterOfLine > m_firstVisibleCharacterOfLine) ? m_firstCharacterOfLine : m_firstVisibleCharacterOfLine));
						m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex = (m_lastCharacterOfLine = ((m_characterCount - 1 > 0) ? (m_characterCount - 1) : 0));
						m_textInfo.lineInfo[m_lineNumber].lastVisibleCharacterIndex = (m_lastVisibleCharacterOfLine = ((m_lastVisibleCharacterOfLine < m_firstVisibleCharacterOfLine) ? m_firstVisibleCharacterOfLine : m_lastVisibleCharacterOfLine));
						m_textInfo.lineInfo[m_lineNumber].characterCount = m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex - m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex + 1;
						m_textInfo.lineInfo[m_lineNumber].visibleCharacterCount = m_lineVisibleCharacterCount;
						m_textInfo.lineInfo[m_lineNumber].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_firstVisibleCharacterOfLine].bottomLeft.x, num32);
						m_textInfo.lineInfo[m_lineNumber].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].topRight.x, num31);
						m_textInfo.lineInfo[m_lineNumber].length = m_textInfo.lineInfo[m_lineNumber].lineExtents.max.x;
						m_textInfo.lineInfo[m_lineNumber].width = num13;
						m_textInfo.lineInfo[m_lineNumber].maxAdvance = m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].xAdvance - (num19 + m_currentFontAsset.normalSpacingOffset) * num2 - m_cSpacing;
						m_textInfo.lineInfo[m_lineNumber].baseline = 0f - m_lineOffset;
						m_textInfo.lineInfo[m_lineNumber].ascender = num31;
						m_textInfo.lineInfo[m_lineNumber].descender = num32;
						m_textInfo.lineInfo[m_lineNumber].lineHeight = num31 - num32 + num8 * num;
						m_firstCharacterOfLine = m_characterCount;
						m_lineVisibleCharacterCount = 0;
						SaveWordWrappingState(ref m_SavedLineState, i, m_characterCount - 1);
						m_lineNumber++;
						flag4 = true;
						flag6 = true;
						if (m_lineNumber >= m_textInfo.lineInfo.Length)
						{
							ResizeLineExtents(m_lineNumber);
						}
						if (m_lineHeight == -32767f)
						{
							float num33 = m_textInfo.characterInfo[m_characterCount].ascender - m_textInfo.characterInfo[m_characterCount].baseLine;
							num9 = 0f - m_maxLineDescender + num33 + (num8 + m_lineSpacing + m_lineSpacingDelta) * num;
							m_lineOffset += num9;
							m_startOfLineAscender = num33;
						}
						else
						{
							m_lineOffset += m_lineHeight + m_lineSpacing * num;
						}
						m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
						m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
						m_xAdvance = 0f + tag_Indent;
						continue;
					}
					if (m_enableAutoSizing && m_fontSize > m_fontSizeMin)
					{
						if (m_charWidthAdjDelta < m_charWidthMaxAdj / 100f)
						{
							loopCountA = 0;
							m_charWidthAdjDelta += 0.01f;
							GenerateTextMesh();
							return;
						}
						m_maxFontSize = m_fontSize;
						m_fontSize -= Mathf.Max((m_fontSize - m_minFontSize) / 2f, 0.05f);
						m_fontSize = (float)(int)(Mathf.Max(m_fontSize, m_fontSizeMin) * 20f + 0.5f) / 20f;
						if (loopCountA <= 20)
						{
							GenerateTextMesh();
						}
						return;
					}
					switch (m_overflowMode)
					{
					case TextOverflowModes.Overflow:
						if (m_isMaskingEnabled)
						{
							DisableMasking();
						}
						break;
					case TextOverflowModes.Ellipsis:
						if (m_isMaskingEnabled)
						{
							DisableMasking();
						}
						m_isTextTruncated = true;
						if (m_characterCount < 1)
						{
							m_textInfo.characterInfo[m_characterCount].isVisible = false;
							break;
						}
						m_TextParsingBuffer[i - 1].unicode = 8230;
						m_TextParsingBuffer[i].unicode = 0;
						if (m_cached_Ellipsis_Character != null)
						{
							m_textInfo.characterInfo[num12].character = '…';
							m_textInfo.characterInfo[num12].textElement = m_cached_Ellipsis_Character;
							m_textInfo.characterInfo[num12].fontAsset = m_materialReferences[0].fontAsset;
							m_textInfo.characterInfo[num12].material = m_materialReferences[0].material;
							m_textInfo.characterInfo[num12].materialReferenceIndex = 0;
						}
						else
						{
							UnityEngine.Debug.LogWarning("Unable to use Ellipsis character since it wasn't found in the current Font Asset [" + m_fontAsset.name + "]. Consider regenerating this font asset to include the Ellipsis character (u+2026).\nNote: Warnings can be disabled in the TMP Settings file.", this);
						}
						m_totalCharacterCount = num12 + 1;
						GenerateTextMesh();
						return;
					case TextOverflowModes.Truncate:
						if (m_isMaskingEnabled)
						{
							DisableMasking();
						}
						m_textInfo.characterInfo[m_characterCount].isVisible = false;
						break;
					}
				}
				if (num4 == 9 || num4 == 160 || num4 == 8199)
				{
					m_textInfo.characterInfo[m_characterCount].isVisible = false;
					m_lastVisibleCharacterOfLine = m_characterCount;
					m_textInfo.lineInfo[m_lineNumber].spaceCount++;
					m_textInfo.spaceCount++;
					if (num4 == 160)
					{
						m_textInfo.lineInfo[m_lineNumber].controlCharacterCount++;
					}
				}
				else
				{
					Color32 vertexColor = ((!m_overrideHtmlColors) ? m_htmlColor : m_fontColor32);
					if (m_textElementType == TMP_TextElementType.Character)
					{
						SaveGlyphVertexInfo(num5, num6, vertexColor);
					}
				}
				if (m_textInfo.characterInfo[m_characterCount].isVisible && num4 != 173)
				{
					if (flag4)
					{
						flag4 = false;
						m_firstVisibleCharacterOfLine = m_characterCount;
					}
					m_lineVisibleCharacterCount++;
					m_lastVisibleCharacterOfLine = m_characterCount;
				}
			}
			else if ((num4 == 10 || char.IsSeparator((char)num4)) && num4 != 173 && num4 != 8203 && num4 != 8288)
			{
				m_textInfo.lineInfo[m_lineNumber].spaceCount++;
				m_textInfo.spaceCount++;
			}
			if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == -32767f && !m_isNewPage)
			{
				float num34 = m_maxLineAscender - m_startOfLineAscender;
				AdjustLineOffset(m_firstCharacterOfLine, m_characterCount, num34);
				num27 -= num34;
				m_lineOffset += num34;
				m_startOfLineAscender += num34;
				m_SavedWordWrapState.lineOffset = m_lineOffset;
				m_SavedWordWrapState.previousLineAscender = m_startOfLineAscender;
			}
			m_textInfo.characterInfo[m_characterCount].lineNumber = m_lineNumber;
			m_textInfo.characterInfo[m_characterCount].pageNumber = m_pageNumber;
			if ((num4 != 10 && num4 != 13 && num4 != 8230) || m_textInfo.lineInfo[m_lineNumber].characterCount == 1)
			{
				m_textInfo.lineInfo[m_lineNumber].alignment = m_lineJustification;
			}
			if (m_maxAscender - num27 > marginHeight + 0.0001f)
			{
				if (m_enableAutoSizing && m_lineSpacingDelta > m_lineSpacingMax && m_lineNumber > 0)
				{
					loopCountA = 0;
					m_lineSpacingDelta -= 1f;
					GenerateTextMesh();
					return;
				}
				if (m_enableAutoSizing && m_fontSize > m_fontSizeMin)
				{
					m_maxFontSize = m_fontSize;
					m_fontSize -= Mathf.Max((m_fontSize - m_minFontSize) / 2f, 0.05f);
					m_fontSize = (float)(int)(Mathf.Max(m_fontSize, m_fontSizeMin) * 20f + 0.5f) / 20f;
					if (loopCountA <= 20)
					{
						GenerateTextMesh();
					}
					return;
				}
				if (m_firstOverflowCharacterIndex == -1)
				{
					m_firstOverflowCharacterIndex = m_characterCount;
				}
				switch (m_overflowMode)
				{
				case TextOverflowModes.Overflow:
					if (m_isMaskingEnabled)
					{
						DisableMasking();
					}
					break;
				case TextOverflowModes.Ellipsis:
					if (m_isMaskingEnabled)
					{
						DisableMasking();
					}
					if (m_lineNumber > 0)
					{
						m_TextParsingBuffer[m_textInfo.characterInfo[num12].index].unicode = 8230;
						m_TextParsingBuffer[m_textInfo.characterInfo[num12].index + 1].unicode = 0;
						if (m_cached_Ellipsis_Character != null)
						{
							m_textInfo.characterInfo[num12].character = '…';
							m_textInfo.characterInfo[num12].textElement = m_cached_Ellipsis_Character;
							m_textInfo.characterInfo[num12].fontAsset = m_materialReferences[0].fontAsset;
							m_textInfo.characterInfo[num12].material = m_materialReferences[0].material;
							m_textInfo.characterInfo[num12].materialReferenceIndex = 0;
						}
						else
						{
							UnityEngine.Debug.LogWarning("Unable to use Ellipsis character since it wasn't found in the current Font Asset [" + m_fontAsset.name + "]. Consider regenerating this font asset to include the Ellipsis character (u+2026).\nNote: Warnings can be disabled in the TMP Settings file.", this);
						}
						m_totalCharacterCount = num12 + 1;
						GenerateTextMesh();
						m_isTextTruncated = true;
					}
					else
					{
						ClearMesh(updateMesh: false);
					}
					return;
				case TextOverflowModes.Truncate:
					if (m_isMaskingEnabled)
					{
						DisableMasking();
					}
					if (m_lineNumber > 0)
					{
						m_TextParsingBuffer[m_textInfo.characterInfo[num12].index + 1].unicode = 0;
						m_totalCharacterCount = num12 + 1;
						GenerateTextMesh();
						m_isTextTruncated = true;
					}
					else
					{
						ClearMesh(updateMesh: false);
					}
					return;
				case TextOverflowModes.Page:
					if (m_isMaskingEnabled)
					{
						DisableMasking();
					}
					if (num4 != 13 && num4 != 10)
					{
						if (i == 0)
						{
							ClearMesh();
							return;
						}
						if (num11 == i)
						{
							m_TextParsingBuffer[i].unicode = 0;
							m_isTextTruncated = true;
						}
						num11 = i;
						i = RestoreWordWrappingState(ref m_SavedLineState);
						m_isNewPage = true;
						m_xAdvance = 0f + tag_Indent;
						m_lineOffset = 0f;
						m_maxAscender = 0f;
						num14 = 0f;
						m_lineNumber++;
						m_pageNumber++;
						continue;
					}
					break;
				case TextOverflowModes.Linked:
					if (m_linkedTextComponent != null)
					{
						m_linkedTextComponent.text = base.text;
						m_linkedTextComponent.firstVisibleCharacter = m_characterCount;
						m_linkedTextComponent.ForceMeshUpdate();
					}
					if (m_lineNumber > 0)
					{
						m_TextParsingBuffer[i].unicode = 0;
						m_totalCharacterCount = m_characterCount;
						GenerateTextMesh();
						m_isTextTruncated = true;
					}
					else
					{
						ClearMesh(updateMesh: true);
					}
					return;
				}
			}
			if (num4 == 9)
			{
				float num35 = m_currentFontAsset.faceInfo.tabWidth * (float)(int)m_currentFontAsset.tabSize * num2;
				float num36 = Mathf.Ceil(m_xAdvance / num35) * num35;
				m_xAdvance = ((num36 > m_xAdvance) ? num36 : (m_xAdvance + num35));
			}
			else if (m_monoSpacing != 0f)
			{
				m_xAdvance += (m_monoSpacing - num20 + (num19 + m_currentFontAsset.normalSpacingOffset) * num2 + m_cSpacing) * (1f - m_charWidthAdjDelta);
				if (char.IsWhiteSpace((char)num4) || num4 == 8203)
				{
					m_xAdvance += m_wordSpacing * num2;
				}
			}
			else if (!m_isRightToLeft)
			{
				float num37 = 1f;
				if (m_isFXMatrixSet)
				{
					num37 = m_FXMatrix.lossyScale.x;
				}
				m_xAdvance += ((m_cached_TextElement.glyph.metrics.horizontalAdvance * num37 * num7 + num19 + m_currentFontAsset.normalSpacingOffset + tMP_GlyphValueRecord.xAdvance) * num2 + m_cSpacing) * (1f - m_charWidthAdjDelta);
				if (char.IsWhiteSpace((char)num4) || num4 == 8203)
				{
					m_xAdvance += m_wordSpacing * num2;
				}
			}
			else
			{
				m_xAdvance -= tMP_GlyphValueRecord.xAdvance * num2;
			}
			m_textInfo.characterInfo[m_characterCount].xAdvance = m_xAdvance;
			if (num4 == 13)
			{
				m_xAdvance = 0f + tag_Indent;
			}
			if (num4 == 10 || m_characterCount == totalCharacterCount - 1)
			{
				if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == -32767f && !m_isNewPage)
				{
					float num38 = m_maxLineAscender - m_startOfLineAscender;
					AdjustLineOffset(m_firstCharacterOfLine, m_characterCount, num38);
					num27 -= num38;
					m_lineOffset += num38;
				}
				m_isNewPage = false;
				float num39 = m_maxLineAscender - m_lineOffset;
				float num40 = m_maxLineDescender - m_lineOffset;
				m_maxDescender = ((m_maxDescender < num40) ? m_maxDescender : num40);
				if (!flag5)
				{
					num15 = m_maxDescender;
				}
				if (m_useMaxVisibleDescender && (m_characterCount >= m_maxVisibleCharacters || m_lineNumber >= m_maxVisibleLines))
				{
					flag5 = true;
				}
				m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex = m_firstCharacterOfLine;
				m_textInfo.lineInfo[m_lineNumber].firstVisibleCharacterIndex = (m_firstVisibleCharacterOfLine = ((m_firstCharacterOfLine > m_firstVisibleCharacterOfLine) ? m_firstCharacterOfLine : m_firstVisibleCharacterOfLine));
				m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex = (m_lastCharacterOfLine = m_characterCount);
				m_textInfo.lineInfo[m_lineNumber].lastVisibleCharacterIndex = (m_lastVisibleCharacterOfLine = ((m_lastVisibleCharacterOfLine < m_firstVisibleCharacterOfLine) ? m_firstVisibleCharacterOfLine : m_lastVisibleCharacterOfLine));
				m_textInfo.lineInfo[m_lineNumber].characterCount = m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex - m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex + 1;
				m_textInfo.lineInfo[m_lineNumber].visibleCharacterCount = m_lineVisibleCharacterCount;
				m_textInfo.lineInfo[m_lineNumber].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_firstVisibleCharacterOfLine].bottomLeft.x, num40);
				m_textInfo.lineInfo[m_lineNumber].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].topRight.x, num39);
				m_textInfo.lineInfo[m_lineNumber].length = m_textInfo.lineInfo[m_lineNumber].lineExtents.max.x - num5 * num2;
				m_textInfo.lineInfo[m_lineNumber].width = num13;
				if (m_textInfo.lineInfo[m_lineNumber].characterCount == 1)
				{
					m_textInfo.lineInfo[m_lineNumber].alignment = m_lineJustification;
				}
				if (m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].isVisible)
				{
					m_textInfo.lineInfo[m_lineNumber].maxAdvance = m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].xAdvance - (num19 + m_currentFontAsset.normalSpacingOffset) * num2 - m_cSpacing;
				}
				else
				{
					m_textInfo.lineInfo[m_lineNumber].maxAdvance = m_textInfo.characterInfo[m_lastCharacterOfLine].xAdvance - (num19 + m_currentFontAsset.normalSpacingOffset) * num2 - m_cSpacing;
				}
				m_textInfo.lineInfo[m_lineNumber].baseline = 0f - m_lineOffset;
				m_textInfo.lineInfo[m_lineNumber].ascender = num39;
				m_textInfo.lineInfo[m_lineNumber].descender = num40;
				m_textInfo.lineInfo[m_lineNumber].lineHeight = num39 - num40 + num8 * num;
				m_firstCharacterOfLine = m_characterCount + 1;
				m_lineVisibleCharacterCount = 0;
				if (num4 == 10)
				{
					SaveWordWrappingState(ref m_SavedLineState, i, m_characterCount);
					SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
					m_lineNumber++;
					flag4 = true;
					flag7 = false;
					flag6 = true;
					if (m_lineNumber >= m_textInfo.lineInfo.Length)
					{
						ResizeLineExtents(m_lineNumber);
					}
					if (m_lineHeight == -32767f)
					{
						num9 = 0f - m_maxLineDescender + num25 + (num8 + m_lineSpacing + m_paragraphSpacing + m_lineSpacingDelta) * num;
						m_lineOffset += num9;
					}
					else
					{
						m_lineOffset += m_lineHeight + (m_lineSpacing + m_paragraphSpacing) * num;
					}
					m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
					m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
					m_startOfLineAscender = num25;
					m_xAdvance = 0f + tag_LineIndent + tag_Indent;
					num12 = m_characterCount - 1;
					m_characterCount++;
					continue;
				}
			}
			if (m_textInfo.characterInfo[m_characterCount].isVisible)
			{
				m_meshExtents.min.x = Mathf.Min(m_meshExtents.min.x, m_textInfo.characterInfo[m_characterCount].bottomLeft.x);
				m_meshExtents.min.y = Mathf.Min(m_meshExtents.min.y, m_textInfo.characterInfo[m_characterCount].bottomLeft.y);
				m_meshExtents.max.x = Mathf.Max(m_meshExtents.max.x, m_textInfo.characterInfo[m_characterCount].topRight.x);
				m_meshExtents.max.y = Mathf.Max(m_meshExtents.max.y, m_textInfo.characterInfo[m_characterCount].topRight.y);
			}
			if (m_overflowMode == TextOverflowModes.Page && num4 != 13 && num4 != 10)
			{
				if (m_pageNumber + 1 > m_textInfo.pageInfo.Length)
				{
					TMP_TextInfo.Resize(ref m_textInfo.pageInfo, m_pageNumber + 1, isBlockAllocated: true);
				}
				m_textInfo.pageInfo[m_pageNumber].ascender = num14;
				m_textInfo.pageInfo[m_pageNumber].descender = ((num26 < m_textInfo.pageInfo[m_pageNumber].descender) ? num26 : m_textInfo.pageInfo[m_pageNumber].descender);
				if (m_pageNumber == 0 && m_characterCount == 0)
				{
					m_textInfo.pageInfo[m_pageNumber].firstCharacterIndex = m_characterCount;
				}
				else if (m_characterCount > 0 && m_pageNumber != m_textInfo.characterInfo[m_characterCount - 1].pageNumber)
				{
					m_textInfo.pageInfo[m_pageNumber - 1].lastCharacterIndex = m_characterCount - 1;
					m_textInfo.pageInfo[m_pageNumber].firstCharacterIndex = m_characterCount;
				}
				else if (m_characterCount == totalCharacterCount - 1)
				{
					m_textInfo.pageInfo[m_pageNumber].lastCharacterIndex = m_characterCount;
				}
			}
			if (m_enableWordWrapping || m_overflowMode == TextOverflowModes.Truncate || m_overflowMode == TextOverflowModes.Ellipsis)
			{
				if ((char.IsWhiteSpace((char)num4) || num4 == 8203 || num4 == 45 || num4 == 173) && (!m_isNonBreakingSpace || flag7) && num4 != 160 && num4 != 8199 && num4 != 8209 && num4 != 8239 && num4 != 8288)
				{
					SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
					m_isCharacterWrappingEnabled = false;
					flag6 = false;
				}
				else if (((num4 > 4352 && num4 < 4607) || (num4 > 11904 && num4 < 40959) || (num4 > 43360 && num4 < 43391) || (num4 > 44032 && num4 < 55295) || (num4 > 63744 && num4 < 64255) || (num4 > 65072 && num4 < 65103) || (num4 > 65280 && num4 < 65519)) && !m_isNonBreakingSpace)
				{
					if (flag6 || flag8 || (!TMP_Settings.linebreakingRules.leadingCharacters.ContainsKey(num4) && m_characterCount < totalCharacterCount - 1 && !TMP_Settings.linebreakingRules.followingCharacters.ContainsKey(m_textInfo.characterInfo[m_characterCount + 1].character)))
					{
						SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
						m_isCharacterWrappingEnabled = false;
						flag6 = false;
					}
				}
				else if (flag6 || m_isCharacterWrappingEnabled || flag8)
				{
					SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
				}
			}
			m_characterCount++;
		}
		num3 = m_maxFontSize - m_minFontSize;
		if (!m_isCharacterWrappingEnabled && m_enableAutoSizing && num3 > 0.051f && m_fontSize < m_fontSizeMax)
		{
			m_minFontSize = m_fontSize;
			m_fontSize += Mathf.Max((m_maxFontSize - m_fontSize) / 2f, 0.05f);
			m_fontSize = (float)(int)(Mathf.Min(m_fontSize, m_fontSizeMax) * 20f + 0.5f) / 20f;
			if (loopCountA <= 20)
			{
				GenerateTextMesh();
			}
			return;
		}
		m_isCharacterWrappingEnabled = false;
		if (m_characterCount == 0)
		{
			ClearMesh(updateMesh: true);
			TMPro_EventManager.ON_TEXT_CHANGED(this);
			return;
		}
		int index = m_materialReferences[0].referenceCount * ((!m_isVolumetricText) ? 4 : 8);
		m_textInfo.meshInfo[0].Clear(uploadChanges: false);
		Vector3 vector10 = Vector3.zero;
		Vector3[] rectTransformCorners = m_RectTransformCorners;
		switch (m_textAlignment)
		{
		case TextAlignmentOptions.TopLeft:
		case TextAlignmentOptions.Top:
		case TextAlignmentOptions.TopRight:
		case TextAlignmentOptions.TopJustified:
		case TextAlignmentOptions.TopFlush:
		case TextAlignmentOptions.TopGeoAligned:
			vector10 = ((m_overflowMode == TextOverflowModes.Page) ? (rectTransformCorners[1] + new Vector3(0f + vector2.x, 0f - m_textInfo.pageInfo[num10].ascender - vector2.y, 0f)) : (rectTransformCorners[1] + new Vector3(0f + vector2.x, 0f - m_maxAscender - vector2.y, 0f)));
			break;
		case TextAlignmentOptions.Left:
		case TextAlignmentOptions.Center:
		case TextAlignmentOptions.Right:
		case TextAlignmentOptions.Justified:
		case TextAlignmentOptions.Flush:
		case TextAlignmentOptions.CenterGeoAligned:
			vector10 = ((m_overflowMode == TextOverflowModes.Page) ? ((rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + vector2.x, 0f - (m_textInfo.pageInfo[num10].ascender + vector2.y + m_textInfo.pageInfo[num10].descender - vector2.w) / 2f, 0f)) : ((rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + vector2.x, 0f - (m_maxAscender + vector2.y + num15 - vector2.w) / 2f, 0f)));
			break;
		case TextAlignmentOptions.BottomLeft:
		case TextAlignmentOptions.Bottom:
		case TextAlignmentOptions.BottomRight:
		case TextAlignmentOptions.BottomJustified:
		case TextAlignmentOptions.BottomFlush:
		case TextAlignmentOptions.BottomGeoAligned:
			vector10 = ((m_overflowMode == TextOverflowModes.Page) ? (rectTransformCorners[0] + new Vector3(0f + vector2.x, 0f - m_textInfo.pageInfo[num10].descender + vector2.w, 0f)) : (rectTransformCorners[0] + new Vector3(0f + vector2.x, 0f - num15 + vector2.w, 0f)));
			break;
		case TextAlignmentOptions.BaselineLeft:
		case TextAlignmentOptions.Baseline:
		case TextAlignmentOptions.BaselineRight:
		case TextAlignmentOptions.BaselineJustified:
		case TextAlignmentOptions.BaselineFlush:
		case TextAlignmentOptions.BaselineGeoAligned:
			vector10 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + vector2.x, 0f, 0f);
			break;
		case TextAlignmentOptions.MidlineLeft:
		case TextAlignmentOptions.Midline:
		case TextAlignmentOptions.MidlineRight:
		case TextAlignmentOptions.MidlineJustified:
		case TextAlignmentOptions.MidlineFlush:
		case TextAlignmentOptions.MidlineGeoAligned:
			vector10 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + vector2.x, 0f - (m_meshExtents.max.y + vector2.y + m_meshExtents.min.y - vector2.w) / 2f, 0f);
			break;
		case TextAlignmentOptions.CaplineLeft:
		case TextAlignmentOptions.Capline:
		case TextAlignmentOptions.CaplineRight:
		case TextAlignmentOptions.CaplineJustified:
		case TextAlignmentOptions.CaplineFlush:
		case TextAlignmentOptions.CaplineGeoAligned:
			vector10 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + vector2.x, 0f - (m_maxCapHeight - vector2.y - vector2.w) / 2f, 0f);
			break;
		}
		Vector3 vector11 = Vector3.zero;
		Vector3 zero3 = Vector3.zero;
		int num41 = 0;
		int lineCount = 0;
		int num42 = 0;
		bool flag10 = false;
		bool flag11 = false;
		int num43 = 0;
		int num44 = 0;
		float f = (m_previousLossyScaleY = transform.lossyScale.y);
		Color32 color = Color.white;
		Color32 underlineColor = Color.white;
		Color32 color2 = new Color32(byte.MaxValue, byte.MaxValue, 0, 64);
		float num45 = 0f;
		float num46 = 0f;
		float num47 = 0f;
		float num48 = 0f;
		float num49 = 0f;
		float num50 = TMP_Text.k_LargePositiveFloat;
		int num51 = 0;
		float num52 = 0f;
		float num53 = 0f;
		float b = 0f;
		TMP_CharacterInfo[] characterInfo = m_textInfo.characterInfo;
		for (int j = 0; j < m_characterCount; j++)
		{
			TMP_FontAsset fontAsset = characterInfo[j].fontAsset;
			char character = characterInfo[j].character;
			int lineNumber = characterInfo[j].lineNumber;
			TMP_LineInfo tMP_LineInfo = m_textInfo.lineInfo[lineNumber];
			lineCount = lineNumber + 1;
			TextAlignmentOptions textAlignmentOptions = tMP_LineInfo.alignment;
			switch (textAlignmentOptions)
			{
			case TextAlignmentOptions.TopLeft:
			case TextAlignmentOptions.Left:
			case TextAlignmentOptions.BottomLeft:
			case TextAlignmentOptions.BaselineLeft:
			case TextAlignmentOptions.MidlineLeft:
			case TextAlignmentOptions.CaplineLeft:
				vector11 = (m_isRightToLeft ? new Vector3(0f - tMP_LineInfo.maxAdvance, 0f, 0f) : new Vector3(0f + tMP_LineInfo.marginLeft, 0f, 0f));
				break;
			case TextAlignmentOptions.Top:
			case TextAlignmentOptions.Center:
			case TextAlignmentOptions.Bottom:
			case TextAlignmentOptions.Baseline:
			case TextAlignmentOptions.Midline:
			case TextAlignmentOptions.Capline:
				vector11 = new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width / 2f - tMP_LineInfo.maxAdvance / 2f, 0f, 0f);
				break;
			case TextAlignmentOptions.TopGeoAligned:
			case TextAlignmentOptions.CenterGeoAligned:
			case TextAlignmentOptions.BottomGeoAligned:
			case TextAlignmentOptions.BaselineGeoAligned:
			case TextAlignmentOptions.MidlineGeoAligned:
			case TextAlignmentOptions.CaplineGeoAligned:
				vector11 = new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width / 2f - (tMP_LineInfo.lineExtents.min.x + tMP_LineInfo.lineExtents.max.x) / 2f, 0f, 0f);
				break;
			case TextAlignmentOptions.TopRight:
			case TextAlignmentOptions.Right:
			case TextAlignmentOptions.BottomRight:
			case TextAlignmentOptions.BaselineRight:
			case TextAlignmentOptions.MidlineRight:
			case TextAlignmentOptions.CaplineRight:
				vector11 = (m_isRightToLeft ? new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width, 0f, 0f) : new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width - tMP_LineInfo.maxAdvance, 0f, 0f));
				break;
			case TextAlignmentOptions.TopJustified:
			case TextAlignmentOptions.TopFlush:
			case TextAlignmentOptions.Justified:
			case TextAlignmentOptions.Flush:
			case TextAlignmentOptions.BottomJustified:
			case TextAlignmentOptions.BottomFlush:
			case TextAlignmentOptions.BaselineJustified:
			case TextAlignmentOptions.BaselineFlush:
			case TextAlignmentOptions.MidlineJustified:
			case TextAlignmentOptions.MidlineFlush:
			case TextAlignmentOptions.CaplineJustified:
			case TextAlignmentOptions.CaplineFlush:
			{
				if (character == '\u00ad' || character == '\u200b' || character == '\u2060')
				{
					break;
				}
				char character2 = characterInfo[tMP_LineInfo.lastCharacterIndex].character;
				bool flag12 = (textAlignmentOptions & (TextAlignmentOptions)16) == (TextAlignmentOptions)16;
				if ((!char.IsControl(character2) && lineNumber < m_lineNumber) || flag12 || tMP_LineInfo.maxAdvance > tMP_LineInfo.width)
				{
					if (lineNumber != num42 || j == 0 || j == m_firstVisibleCharacter)
					{
						vector11 = (m_isRightToLeft ? new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width, 0f, 0f) : new Vector3(tMP_LineInfo.marginLeft, 0f, 0f));
						flag10 = (char.IsSeparator(character) ? true : false);
						break;
					}
					float num54 = ((!m_isRightToLeft) ? (tMP_LineInfo.width - tMP_LineInfo.maxAdvance) : (tMP_LineInfo.width + tMP_LineInfo.maxAdvance));
					int num55 = tMP_LineInfo.visibleCharacterCount - 1 + tMP_LineInfo.controlCharacterCount;
					int num56 = (characterInfo[tMP_LineInfo.lastCharacterIndex].isVisible ? tMP_LineInfo.spaceCount : (tMP_LineInfo.spaceCount - 1)) - tMP_LineInfo.controlCharacterCount;
					if (flag10)
					{
						num56--;
						num55++;
					}
					float num57 = ((num56 > 0) ? m_wordWrappingRatios : 1f);
					if (num56 < 1)
					{
						num56 = 1;
					}
					if (character != '\u00a0' && (character == '\t' || char.IsSeparator(character)))
					{
						if (!m_isRightToLeft)
						{
							vector11 += new Vector3(num54 * (1f - num57) / (float)num56, 0f, 0f);
						}
						else
						{
							vector11 -= new Vector3(num54 * (1f - num57) / (float)num56, 0f, 0f);
						}
					}
					else if (!m_isRightToLeft)
					{
						vector11 += new Vector3(num54 * num57 / (float)num55, 0f, 0f);
					}
					else
					{
						vector11 -= new Vector3(num54 * num57 / (float)num55, 0f, 0f);
					}
				}
				else
				{
					vector11 = (m_isRightToLeft ? new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width, 0f, 0f) : new Vector3(tMP_LineInfo.marginLeft, 0f, 0f));
				}
				break;
			}
			}
			zero3 = vector10 + vector11;
			if (characterInfo[j].isVisible)
			{
				TMP_TextElementType elementType = characterInfo[j].elementType;
				if (elementType == TMP_TextElementType.Character)
				{
					Extents lineExtents = tMP_LineInfo.lineExtents;
					float num58 = m_uvLineOffset * (float)lineNumber % 1f;
					switch (m_horizontalMapping)
					{
					case TextureMappingOptions.Character:
						characterInfo[j].vertex_BL.uv2.x = 0f;
						characterInfo[j].vertex_TL.uv2.x = 0f;
						characterInfo[j].vertex_TR.uv2.x = 1f;
						characterInfo[j].vertex_BR.uv2.x = 1f;
						break;
					case TextureMappingOptions.Line:
						if (m_textAlignment != TextAlignmentOptions.Justified)
						{
							characterInfo[j].vertex_BL.uv2.x = (characterInfo[j].vertex_BL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num58;
							characterInfo[j].vertex_TL.uv2.x = (characterInfo[j].vertex_TL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num58;
							characterInfo[j].vertex_TR.uv2.x = (characterInfo[j].vertex_TR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num58;
							characterInfo[j].vertex_BR.uv2.x = (characterInfo[j].vertex_BR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num58;
						}
						else
						{
							characterInfo[j].vertex_BL.uv2.x = (characterInfo[j].vertex_BL.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
							characterInfo[j].vertex_TL.uv2.x = (characterInfo[j].vertex_TL.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
							characterInfo[j].vertex_TR.uv2.x = (characterInfo[j].vertex_TR.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
							characterInfo[j].vertex_BR.uv2.x = (characterInfo[j].vertex_BR.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
						}
						break;
					case TextureMappingOptions.Paragraph:
						characterInfo[j].vertex_BL.uv2.x = (characterInfo[j].vertex_BL.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
						characterInfo[j].vertex_TL.uv2.x = (characterInfo[j].vertex_TL.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
						characterInfo[j].vertex_TR.uv2.x = (characterInfo[j].vertex_TR.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
						characterInfo[j].vertex_BR.uv2.x = (characterInfo[j].vertex_BR.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
						break;
					case TextureMappingOptions.MatchAspect:
					{
						switch (m_verticalMapping)
						{
						case TextureMappingOptions.Character:
							characterInfo[j].vertex_BL.uv2.y = 0f;
							characterInfo[j].vertex_TL.uv2.y = 1f;
							characterInfo[j].vertex_TR.uv2.y = 0f;
							characterInfo[j].vertex_BR.uv2.y = 1f;
							break;
						case TextureMappingOptions.Line:
							characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num58;
							characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num58;
							characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
							characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
							break;
						case TextureMappingOptions.Paragraph:
							characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y) + num58;
							characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y) + num58;
							characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
							characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
							break;
						case TextureMappingOptions.MatchAspect:
							UnityEngine.Debug.Log("ERROR: Cannot Match both Vertical & Horizontal.");
							break;
						}
						float num59 = (1f - (characterInfo[j].vertex_BL.uv2.y + characterInfo[j].vertex_TL.uv2.y) * characterInfo[j].aspectRatio) / 2f;
						characterInfo[j].vertex_BL.uv2.x = characterInfo[j].vertex_BL.uv2.y * characterInfo[j].aspectRatio + num59 + num58;
						characterInfo[j].vertex_TL.uv2.x = characterInfo[j].vertex_BL.uv2.x;
						characterInfo[j].vertex_TR.uv2.x = characterInfo[j].vertex_TL.uv2.y * characterInfo[j].aspectRatio + num59 + num58;
						characterInfo[j].vertex_BR.uv2.x = characterInfo[j].vertex_TR.uv2.x;
						break;
					}
					}
					switch (m_verticalMapping)
					{
					case TextureMappingOptions.Character:
						characterInfo[j].vertex_BL.uv2.y = 0f;
						characterInfo[j].vertex_TL.uv2.y = 1f;
						characterInfo[j].vertex_TR.uv2.y = 1f;
						characterInfo[j].vertex_BR.uv2.y = 0f;
						break;
					case TextureMappingOptions.Line:
						characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - tMP_LineInfo.descender) / (tMP_LineInfo.ascender - tMP_LineInfo.descender);
						characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - tMP_LineInfo.descender) / (tMP_LineInfo.ascender - tMP_LineInfo.descender);
						characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
						characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
						break;
					case TextureMappingOptions.Paragraph:
						characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y);
						characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y);
						characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
						characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
						break;
					case TextureMappingOptions.MatchAspect:
					{
						float num60 = (1f - (characterInfo[j].vertex_BL.uv2.x + characterInfo[j].vertex_TR.uv2.x) / characterInfo[j].aspectRatio) / 2f;
						characterInfo[j].vertex_BL.uv2.y = num60 + characterInfo[j].vertex_BL.uv2.x / characterInfo[j].aspectRatio;
						characterInfo[j].vertex_TL.uv2.y = num60 + characterInfo[j].vertex_TR.uv2.x / characterInfo[j].aspectRatio;
						characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
						characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
						break;
					}
					}
					num45 = characterInfo[j].scale * Mathf.Abs(f) * (1f - m_charWidthAdjDelta);
					if (!characterInfo[j].isUsingAlternateTypeface && (characterInfo[j].style & FontStyles.Bold) == FontStyles.Bold)
					{
						num45 *= -1f;
					}
					float x = characterInfo[j].vertex_BL.uv2.x;
					float y = characterInfo[j].vertex_BL.uv2.y;
					float x2 = characterInfo[j].vertex_TR.uv2.x;
					float y2 = characterInfo[j].vertex_TR.uv2.y;
					float num61 = (int)x;
					float num62 = (int)y;
					x -= num61;
					x2 -= num61;
					y -= num62;
					y2 -= num62;
					characterInfo[j].vertex_BL.uv2.x = PackUV(x, y);
					characterInfo[j].vertex_BL.uv2.y = num45;
					characterInfo[j].vertex_TL.uv2.x = PackUV(x, y2);
					characterInfo[j].vertex_TL.uv2.y = num45;
					characterInfo[j].vertex_TR.uv2.x = PackUV(x2, y2);
					characterInfo[j].vertex_TR.uv2.y = num45;
					characterInfo[j].vertex_BR.uv2.x = PackUV(x2, y);
					characterInfo[j].vertex_BR.uv2.y = num45;
				}
				if (j < m_maxVisibleCharacters && num41 < m_maxVisibleWords && lineNumber < m_maxVisibleLines && m_overflowMode != TextOverflowModes.Page)
				{
					characterInfo[j].vertex_BL.position += zero3;
					characterInfo[j].vertex_TL.position += zero3;
					characterInfo[j].vertex_TR.position += zero3;
					characterInfo[j].vertex_BR.position += zero3;
				}
				else if (j < m_maxVisibleCharacters && num41 < m_maxVisibleWords && lineNumber < m_maxVisibleLines && m_overflowMode == TextOverflowModes.Page && characterInfo[j].pageNumber == num10)
				{
					characterInfo[j].vertex_BL.position += zero3;
					characterInfo[j].vertex_TL.position += zero3;
					characterInfo[j].vertex_TR.position += zero3;
					characterInfo[j].vertex_BR.position += zero3;
				}
				else
				{
					characterInfo[j].vertex_BL.position = Vector3.zero;
					characterInfo[j].vertex_TL.position = Vector3.zero;
					characterInfo[j].vertex_TR.position = Vector3.zero;
					characterInfo[j].vertex_BR.position = Vector3.zero;
					characterInfo[j].isVisible = false;
				}
				if (elementType == TMP_TextElementType.Character)
				{
					FillCharacterVertexBuffers(j, m_isVolumetricText);
				}
			}
			m_textInfo.characterInfo[j].bottomLeft += zero3;
			m_textInfo.characterInfo[j].topLeft += zero3;
			m_textInfo.characterInfo[j].topRight += zero3;
			m_textInfo.characterInfo[j].bottomRight += zero3;
			m_textInfo.characterInfo[j].origin += zero3.x;
			m_textInfo.characterInfo[j].xAdvance += zero3.x;
			m_textInfo.characterInfo[j].ascender += zero3.y;
			m_textInfo.characterInfo[j].descender += zero3.y;
			m_textInfo.characterInfo[j].baseLine += zero3.y;
			if (lineNumber != num42 || j == m_characterCount - 1)
			{
				if (lineNumber != num42)
				{
					m_textInfo.lineInfo[num42].baseline += zero3.y;
					m_textInfo.lineInfo[num42].ascender += zero3.y;
					m_textInfo.lineInfo[num42].descender += zero3.y;
					m_textInfo.lineInfo[num42].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[num42].firstCharacterIndex].bottomLeft.x, m_textInfo.lineInfo[num42].descender);
					m_textInfo.lineInfo[num42].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[num42].lastVisibleCharacterIndex].topRight.x, m_textInfo.lineInfo[num42].ascender);
				}
				if (j == m_characterCount - 1)
				{
					m_textInfo.lineInfo[lineNumber].baseline += zero3.y;
					m_textInfo.lineInfo[lineNumber].ascender += zero3.y;
					m_textInfo.lineInfo[lineNumber].descender += zero3.y;
					m_textInfo.lineInfo[lineNumber].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[lineNumber].firstCharacterIndex].bottomLeft.x, m_textInfo.lineInfo[lineNumber].descender);
					m_textInfo.lineInfo[lineNumber].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[lineNumber].lastVisibleCharacterIndex].topRight.x, m_textInfo.lineInfo[lineNumber].ascender);
				}
			}
			if (char.IsLetterOrDigit(character) || character == '-' || character == '\u00ad' || character == '‐' || character == '‑')
			{
				if (!flag11)
				{
					flag11 = true;
					num43 = j;
				}
				if (flag11 && j == m_characterCount - 1)
				{
					int num63 = m_textInfo.wordInfo.Length;
					int wordCount = m_textInfo.wordCount;
					if (m_textInfo.wordCount + 1 > num63)
					{
						TMP_TextInfo.Resize(ref m_textInfo.wordInfo, num63 + 1);
					}
					num44 = j;
					m_textInfo.wordInfo[wordCount].firstCharacterIndex = num43;
					m_textInfo.wordInfo[wordCount].lastCharacterIndex = num44;
					m_textInfo.wordInfo[wordCount].characterCount = num44 - num43 + 1;
					m_textInfo.wordInfo[wordCount].textComponent = this;
					num41++;
					m_textInfo.wordCount++;
					m_textInfo.lineInfo[lineNumber].wordCount++;
				}
			}
			else if ((flag11 || (j == 0 && (!char.IsPunctuation(character) || char.IsWhiteSpace(character) || character == '\u200b' || j == m_characterCount - 1))) && (j <= 0 || j >= characterInfo.Length - 1 || j >= m_characterCount || (character != '\'' && character != '’') || !char.IsLetterOrDigit(characterInfo[j - 1].character) || !char.IsLetterOrDigit(characterInfo[j + 1].character)))
			{
				num44 = ((j == m_characterCount - 1 && char.IsLetterOrDigit(character)) ? j : (j - 1));
				flag11 = false;
				int num64 = m_textInfo.wordInfo.Length;
				int wordCount2 = m_textInfo.wordCount;
				if (m_textInfo.wordCount + 1 > num64)
				{
					TMP_TextInfo.Resize(ref m_textInfo.wordInfo, num64 + 1);
				}
				m_textInfo.wordInfo[wordCount2].firstCharacterIndex = num43;
				m_textInfo.wordInfo[wordCount2].lastCharacterIndex = num44;
				m_textInfo.wordInfo[wordCount2].characterCount = num44 - num43 + 1;
				m_textInfo.wordInfo[wordCount2].textComponent = this;
				num41++;
				m_textInfo.wordCount++;
				m_textInfo.lineInfo[lineNumber].wordCount++;
			}
			if ((m_textInfo.characterInfo[j].style & FontStyles.Underline) == FontStyles.Underline)
			{
				bool flag13 = true;
				int pageNumber = m_textInfo.characterInfo[j].pageNumber;
				if (j > m_maxVisibleCharacters || lineNumber > m_maxVisibleLines || (m_overflowMode == TextOverflowModes.Page && pageNumber + 1 != m_pageToDisplay))
				{
					flag13 = false;
				}
				if (!char.IsWhiteSpace(character) && character != '\u200b')
				{
					num49 = Mathf.Max(num49, m_textInfo.characterInfo[j].scale);
					num46 = Mathf.Max(num46, Mathf.Abs(num45));
					num50 = Mathf.Min((pageNumber == num51) ? num50 : TMP_Text.k_LargePositiveFloat, m_textInfo.characterInfo[j].baseLine + base.font.faceInfo.underlineOffset * num49);
					num51 = pageNumber;
				}
				if (!flag && flag13 && j <= tMP_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\r' && (j != tMP_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
				{
					flag = true;
					num47 = m_textInfo.characterInfo[j].scale;
					if (num49 == 0f)
					{
						num49 = num47;
						num46 = num45;
					}
					start = new Vector3(m_textInfo.characterInfo[j].bottomLeft.x, num50, 0f);
					color = m_textInfo.characterInfo[j].underlineColor;
				}
				if (flag && m_characterCount == 1)
				{
					flag = false;
					zero = new Vector3(m_textInfo.characterInfo[j].topRight.x, num50, 0f);
					num48 = m_textInfo.characterInfo[j].scale;
					DrawUnderlineMesh(start, zero, ref index, num47, num48, num49, num46, color);
					num49 = 0f;
					num46 = 0f;
					num50 = TMP_Text.k_LargePositiveFloat;
				}
				else if (flag && (j == tMP_LineInfo.lastCharacterIndex || j >= tMP_LineInfo.lastVisibleCharacterIndex))
				{
					if (char.IsWhiteSpace(character) || character == '\u200b')
					{
						int lastVisibleCharacterIndex = tMP_LineInfo.lastVisibleCharacterIndex;
						zero = new Vector3(m_textInfo.characterInfo[lastVisibleCharacterIndex].topRight.x, num50, 0f);
						num48 = m_textInfo.characterInfo[lastVisibleCharacterIndex].scale;
					}
					else
					{
						zero = new Vector3(m_textInfo.characterInfo[j].topRight.x, num50, 0f);
						num48 = m_textInfo.characterInfo[j].scale;
					}
					flag = false;
					DrawUnderlineMesh(start, zero, ref index, num47, num48, num49, num46, color);
					num49 = 0f;
					num46 = 0f;
					num50 = TMP_Text.k_LargePositiveFloat;
				}
				else if (flag && !flag13)
				{
					flag = false;
					zero = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, num50, 0f);
					num48 = m_textInfo.characterInfo[j - 1].scale;
					DrawUnderlineMesh(start, zero, ref index, num47, num48, num49, num46, color);
					num49 = 0f;
					num46 = 0f;
					num50 = TMP_Text.k_LargePositiveFloat;
				}
				else if (flag && j < m_characterCount - 1 && !color.Compare(m_textInfo.characterInfo[j + 1].underlineColor))
				{
					flag = false;
					zero = new Vector3(m_textInfo.characterInfo[j].topRight.x, num50, 0f);
					num48 = m_textInfo.characterInfo[j].scale;
					DrawUnderlineMesh(start, zero, ref index, num47, num48, num49, num46, color);
					num49 = 0f;
					num46 = 0f;
					num50 = TMP_Text.k_LargePositiveFloat;
				}
			}
			else if (flag)
			{
				flag = false;
				zero = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, num50, 0f);
				num48 = m_textInfo.characterInfo[j - 1].scale;
				DrawUnderlineMesh(start, zero, ref index, num47, num48, num49, num46, color);
				num49 = 0f;
				num46 = 0f;
				num50 = TMP_Text.k_LargePositiveFloat;
			}
			bool num65 = (m_textInfo.characterInfo[j].style & FontStyles.Strikethrough) == FontStyles.Strikethrough;
			float strikethroughOffset = fontAsset.faceInfo.strikethroughOffset;
			if (num65)
			{
				bool flag14 = true;
				if (j > m_maxVisibleCharacters || lineNumber > m_maxVisibleLines || (m_overflowMode == TextOverflowModes.Page && m_textInfo.characterInfo[j].pageNumber + 1 != m_pageToDisplay))
				{
					flag14 = false;
				}
				if (!flag2 && flag14 && j <= tMP_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\r' && (j != tMP_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
				{
					flag2 = true;
					num52 = m_textInfo.characterInfo[j].pointSize;
					num53 = m_textInfo.characterInfo[j].scale;
					start2 = new Vector3(m_textInfo.characterInfo[j].bottomLeft.x, m_textInfo.characterInfo[j].baseLine + strikethroughOffset * num53, 0f);
					underlineColor = m_textInfo.characterInfo[j].strikethroughColor;
					b = m_textInfo.characterInfo[j].baseLine;
				}
				if (flag2 && m_characterCount == 1)
				{
					flag2 = false;
					zero2 = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + strikethroughOffset * num53, 0f);
					DrawUnderlineMesh(start2, zero2, ref index, num53, num53, num53, num45, underlineColor);
				}
				else if (flag2 && j == tMP_LineInfo.lastCharacterIndex)
				{
					if (char.IsWhiteSpace(character) || character == '\u200b')
					{
						int lastVisibleCharacterIndex2 = tMP_LineInfo.lastVisibleCharacterIndex;
						zero2 = new Vector3(m_textInfo.characterInfo[lastVisibleCharacterIndex2].topRight.x, m_textInfo.characterInfo[lastVisibleCharacterIndex2].baseLine + strikethroughOffset * num53, 0f);
					}
					else
					{
						zero2 = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + strikethroughOffset * num53, 0f);
					}
					flag2 = false;
					DrawUnderlineMesh(start2, zero2, ref index, num53, num53, num53, num45, underlineColor);
				}
				else if (flag2 && j < m_characterCount && (m_textInfo.characterInfo[j + 1].pointSize != num52 || !TMP_Math.Approximately(m_textInfo.characterInfo[j + 1].baseLine + zero3.y, b)))
				{
					flag2 = false;
					int lastVisibleCharacterIndex3 = tMP_LineInfo.lastVisibleCharacterIndex;
					zero2 = ((j <= lastVisibleCharacterIndex3) ? new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + strikethroughOffset * num53, 0f) : new Vector3(m_textInfo.characterInfo[lastVisibleCharacterIndex3].topRight.x, m_textInfo.characterInfo[lastVisibleCharacterIndex3].baseLine + strikethroughOffset * num53, 0f));
					DrawUnderlineMesh(start2, zero2, ref index, num53, num53, num53, num45, underlineColor);
				}
				else if (flag2 && j < m_characterCount && fontAsset.GetInstanceID() != characterInfo[j + 1].fontAsset.GetInstanceID())
				{
					flag2 = false;
					zero2 = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + strikethroughOffset * num53, 0f);
					DrawUnderlineMesh(start2, zero2, ref index, num53, num53, num53, num45, underlineColor);
				}
				else if (flag2 && !flag14)
				{
					flag2 = false;
					zero2 = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, m_textInfo.characterInfo[j - 1].baseLine + strikethroughOffset * num53, 0f);
					DrawUnderlineMesh(start2, zero2, ref index, num53, num53, num53, num45, underlineColor);
				}
			}
			else if (flag2)
			{
				flag2 = false;
				zero2 = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, m_textInfo.characterInfo[j - 1].baseLine + strikethroughOffset * num53, 0f);
				DrawUnderlineMesh(start2, zero2, ref index, num53, num53, num53, num45, underlineColor);
			}
			if ((m_textInfo.characterInfo[j].style & FontStyles.Highlight) == FontStyles.Highlight)
			{
				bool flag15 = true;
				int pageNumber2 = m_textInfo.characterInfo[j].pageNumber;
				if (j > m_maxVisibleCharacters || lineNumber > m_maxVisibleLines || (m_overflowMode == TextOverflowModes.Page && pageNumber2 + 1 != m_pageToDisplay))
				{
					flag15 = false;
				}
				if (!flag3 && flag15 && j <= tMP_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\r' && (j != tMP_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
				{
					flag3 = true;
					start3 = TMP_Text.k_LargePositiveVector2;
					vector = TMP_Text.k_LargeNegativeVector2;
					color2 = m_textInfo.characterInfo[j].highlightColor;
				}
				if (flag3)
				{
					Color32 highlightColor = m_textInfo.characterInfo[j].highlightColor;
					bool flag16 = false;
					if (!color2.Compare(highlightColor))
					{
						vector.x = (vector.x + m_textInfo.characterInfo[j].bottomLeft.x) / 2f;
						start3.y = Mathf.Min(start3.y, m_textInfo.characterInfo[j].descender);
						vector.y = Mathf.Max(vector.y, m_textInfo.characterInfo[j].ascender);
						DrawTextHighlight(start3, vector, ref index, color2);
						flag3 = true;
						start3 = vector;
						vector = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].descender, 0f);
						color2 = m_textInfo.characterInfo[j].highlightColor;
						flag16 = true;
					}
					if (!flag16)
					{
						start3.x = Mathf.Min(start3.x, m_textInfo.characterInfo[j].bottomLeft.x);
						start3.y = Mathf.Min(start3.y, m_textInfo.characterInfo[j].descender);
						vector.x = Mathf.Max(vector.x, m_textInfo.characterInfo[j].topRight.x);
						vector.y = Mathf.Max(vector.y, m_textInfo.characterInfo[j].ascender);
					}
				}
				if (flag3 && m_characterCount == 1)
				{
					flag3 = false;
					DrawTextHighlight(start3, vector, ref index, color2);
				}
				else if (flag3 && (j == tMP_LineInfo.lastCharacterIndex || j >= tMP_LineInfo.lastVisibleCharacterIndex))
				{
					flag3 = false;
					DrawTextHighlight(start3, vector, ref index, color2);
				}
				else if (flag3 && !flag15)
				{
					flag3 = false;
					DrawTextHighlight(start3, vector, ref index, color2);
				}
			}
			else if (flag3)
			{
				flag3 = false;
				DrawTextHighlight(start3, vector, ref index, color2);
			}
			num42 = lineNumber;
		}
		m_textInfo.characterCount = m_characterCount;
		m_textInfo.spriteCount = m_spriteCount;
		m_textInfo.lineCount = lineCount;
		m_textInfo.wordCount = ((num41 == 0 || m_characterCount <= 0) ? 1 : num41);
		m_textInfo.pageCount = m_pageNumber + 1;
		if (m_renderMode == TextRenderFlags.Render && IsActive())
		{
			if (m_geometrySortingOrder != VertexSortingOrder.Normal)
			{
				m_textInfo.meshInfo[0].SortGeometry(VertexSortingOrder.Reverse);
			}
			m_mesh.MarkDynamic();
			m_mesh.vertices = m_textInfo.meshInfo[0].vertices;
			m_mesh.uv = m_textInfo.meshInfo[0].uvs0;
			m_mesh.uv2 = m_textInfo.meshInfo[0].uvs2;
			m_mesh.colors32 = m_textInfo.meshInfo[0].colors32;
			m_mesh.RecalculateBounds();
			for (int k = 1; k < m_textInfo.materialCount; k++)
			{
				m_textInfo.meshInfo[k].ClearUnusedVertices();
				if (!(m_subTextObjects[k] == null))
				{
					if (m_geometrySortingOrder != VertexSortingOrder.Normal)
					{
						m_textInfo.meshInfo[k].SortGeometry(VertexSortingOrder.Reverse);
					}
					m_subTextObjects[k].mesh.vertices = m_textInfo.meshInfo[k].vertices;
					m_subTextObjects[k].mesh.uv = m_textInfo.meshInfo[k].uvs0;
					m_subTextObjects[k].mesh.uv2 = m_textInfo.meshInfo[k].uvs2;
					m_subTextObjects[k].mesh.colors32 = m_textInfo.meshInfo[k].colors32;
					m_subTextObjects[k].mesh.RecalculateBounds();
				}
			}
		}
		TMPro_EventManager.ON_TEXT_CHANGED(this);
	}

	protected override Vector3[] GetTextContainerLocalCorners()
	{
		if (m_rectTransform == null)
		{
			m_rectTransform = base.rectTransform;
		}
		m_rectTransform.GetLocalCorners(m_RectTransformCorners);
		return m_RectTransformCorners;
	}

	private void SetMeshFilters(bool state)
	{
		if (m_meshFilter != null)
		{
			if (state)
			{
				m_meshFilter.sharedMesh = m_mesh;
			}
			else
			{
				m_meshFilter.sharedMesh = null;
			}
		}
		for (int i = 1; i < m_subTextObjects.Length && m_subTextObjects[i] != null; i++)
		{
			if (m_subTextObjects[i].meshFilter != null)
			{
				if (state)
				{
					m_subTextObjects[i].meshFilter.sharedMesh = m_subTextObjects[i].mesh;
				}
				else
				{
					m_subTextObjects[i].meshFilter.sharedMesh = null;
				}
			}
		}
	}

	protected override void SetActiveSubMeshes(bool state)
	{
		for (int i = 1; i < m_subTextObjects.Length && m_subTextObjects[i] != null; i++)
		{
			if (m_subTextObjects[i].enabled != state)
			{
				m_subTextObjects[i].enabled = state;
			}
		}
	}

	protected override void ClearSubMeshObjects()
	{
		for (int i = 1; i < m_subTextObjects.Length && m_subTextObjects[i] != null; i++)
		{
			UnityEngine.Debug.Log("Destroying Sub Text object[" + i + "].");
			UnityEngine.Object.DestroyImmediate(m_subTextObjects[i]);
		}
	}

	protected override Bounds GetCompoundBounds()
	{
		Bounds bounds = m_mesh.bounds;
		Vector3 min = bounds.min;
		Vector3 max = bounds.max;
		for (int i = 1; i < m_subTextObjects.Length && m_subTextObjects[i] != null; i++)
		{
			Bounds bounds2 = m_subTextObjects[i].mesh.bounds;
			min.x = ((min.x < bounds2.min.x) ? min.x : bounds2.min.x);
			min.y = ((min.y < bounds2.min.y) ? min.y : bounds2.min.y);
			max.x = ((max.x > bounds2.max.x) ? max.x : bounds2.max.x);
			max.y = ((max.y > bounds2.max.y) ? max.y : bounds2.max.y);
		}
		Vector3 center = (min + max) / 2f;
		Vector2 vector = max - min;
		return new Bounds(center, vector);
	}

	private void UpdateSDFScale(float scaleDelta)
	{
		if (scaleDelta == 0f || scaleDelta == float.PositiveInfinity)
		{
			m_havePropertiesChanged = true;
			OnPreRenderObject();
			return;
		}
		for (int i = 0; i < m_textInfo.materialCount; i++)
		{
			TMP_MeshInfo tMP_MeshInfo = m_textInfo.meshInfo[i];
			for (int j = 0; j < tMP_MeshInfo.uvs2.Length; j++)
			{
				tMP_MeshInfo.uvs2[j].y *= Mathf.Abs(scaleDelta);
			}
		}
		for (int k = 0; k < m_textInfo.meshInfo.Length; k++)
		{
			if (k == 0)
			{
				m_mesh.uv2 = m_textInfo.meshInfo[0].uvs2;
			}
			else
			{
				m_subTextObjects[k].mesh.uv2 = m_textInfo.meshInfo[k].uvs2;
			}
		}
	}

	protected override void AdjustLineOffset(int startIndex, int endIndex, float offset)
	{
		Vector3 vector = new Vector3(0f, offset, 0f);
		for (int i = startIndex; i <= endIndex; i++)
		{
			m_textInfo.characterInfo[i].bottomLeft -= vector;
			m_textInfo.characterInfo[i].topLeft -= vector;
			m_textInfo.characterInfo[i].topRight -= vector;
			m_textInfo.characterInfo[i].bottomRight -= vector;
			m_textInfo.characterInfo[i].ascender -= vector.y;
			m_textInfo.characterInfo[i].baseLine -= vector.y;
			m_textInfo.characterInfo[i].descender -= vector.y;
			if (m_textInfo.characterInfo[i].isVisible)
			{
				m_textInfo.characterInfo[i].vertex_BL.position -= vector;
				m_textInfo.characterInfo[i].vertex_TL.position -= vector;
				m_textInfo.characterInfo[i].vertex_TR.position -= vector;
				m_textInfo.characterInfo[i].vertex_BR.position -= vector;
			}
		}
	}
}
[DisallowMultipleComponent]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasRenderer))]
[AddComponentMenu("", 11)]
[ExecuteAlways]
public class TextMeshProUGUI : TMP_Text, ILayoutElement
{
	private bool m_isRebuildingLayout;

	[SerializeField]
	private bool m_hasFontAssetChanged;

	[SerializeField]
	protected TMP_SubMeshUI[] m_subTextObjects = new TMP_SubMeshUI[8];

	private float m_previousLossyScaleY = -1f;

	private Vector3[] m_RectTransformCorners = new Vector3[4];

	private CanvasRenderer m_canvasRenderer;

	private Canvas m_canvas;

	private bool m_isFirstAllocation;

	private int m_max_characters = 8;

	private bool m_isMaskingEnabled;

	[SerializeField]
	private Material m_baseMaterial;

	private bool m_isScrollRegionSet;

	private int m_stencilID;

	[SerializeField]
	private Vector4 m_maskOffset;

	private Matrix4x4 m_EnvMapMatrix;

	[NonSerialized]
	private bool m_isRegisteredForEvents;

	private int m_recursiveCountA;

	private int loopCountA;

	private int m_truncateRecurseCount;

	public override Material materialForRendering => TMP_MaterialManager.GetMaterialForRendering(this, m_sharedMaterial);

	public override bool autoSizeTextContainer
	{
		get
		{
			return m_autoSizeTextContainer;
		}
		set
		{
			if (m_autoSizeTextContainer != value)
			{
				m_autoSizeTextContainer = value;
				if (m_autoSizeTextContainer)
				{
					CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
					SetLayoutDirty();
				}
			}
		}
	}

	public override Mesh mesh => m_mesh;

	public new CanvasRenderer canvasRenderer
	{
		get
		{
			if (m_canvasRenderer == null)
			{
				m_canvasRenderer = GetComponent<CanvasRenderer>();
			}
			return m_canvasRenderer;
		}
	}

	public Vector4 maskOffset
	{
		get
		{
			return m_maskOffset;
		}
		set
		{
			m_maskOffset = value;
			UpdateMask();
			m_havePropertiesChanged = true;
		}
	}

	public bool blockRectTransformChange { get; set; }

	public override event Action<TMP_TextInfo> OnPreRenderText;

	public void CalculateLayoutInputHorizontal()
	{
		if (base.gameObject.activeInHierarchy && (m_isCalculateSizeRequired || m_rectTransform.hasChanged))
		{
			m_preferredWidth = GetPreferredWidth();
			ComputeMarginSize();
			m_isLayoutDirty = true;
		}
	}

	public void CalculateLayoutInputVertical()
	{
		if (base.gameObject.activeInHierarchy)
		{
			if (m_isCalculateSizeRequired || m_rectTransform.hasChanged)
			{
				m_preferredHeight = GetPreferredHeight();
				ComputeMarginSize();
				m_isLayoutDirty = true;
			}
			m_isCalculateSizeRequired = false;
		}
	}

	public override void SetVerticesDirty()
	{
		if (!m_verticesAlreadyDirty && !(this == null) && IsActive() && !CanvasUpdateRegistry.IsRebuildingGraphics())
		{
			m_verticesAlreadyDirty = true;
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			if (m_OnDirtyVertsCallback != null)
			{
				m_OnDirtyVertsCallback();
			}
		}
	}

	public override void SetLayoutDirty()
	{
		m_isPreferredWidthDirty = true;
		m_isPreferredHeightDirty = true;
		if (!m_layoutAlreadyDirty && !(this == null) && IsActive())
		{
			m_layoutAlreadyDirty = true;
			LayoutRebuilder.MarkLayoutForRebuild(base.rectTransform);
			m_isLayoutDirty = true;
			if (m_OnDirtyLayoutCallback != null)
			{
				m_OnDirtyLayoutCallback();
			}
		}
	}

	public override void SetMaterialDirty()
	{
		if (!(this == null) && IsActive() && !CanvasUpdateRegistry.IsRebuildingGraphics())
		{
			m_isMaterialDirty = true;
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			if (m_OnDirtyMaterialCallback != null)
			{
				m_OnDirtyMaterialCallback();
			}
		}
	}

	public override void SetAllDirty()
	{
		m_isInputParsingRequired = true;
		SetLayoutDirty();
		SetVerticesDirty();
		SetMaterialDirty();
	}

	public override void Rebuild(CanvasUpdate update)
	{
		if (this == null || m_fontAsset == null)
		{
			return;
		}
		switch (update)
		{
		case CanvasUpdate.Prelayout:
			if (m_autoSizeTextContainer)
			{
				m_rectTransform.sizeDelta = GetPreferredValues(float.PositiveInfinity, float.PositiveInfinity);
			}
			break;
		case CanvasUpdate.PreRender:
			OnPreRenderCanvas();
			m_verticesAlreadyDirty = false;
			m_layoutAlreadyDirty = false;
			if (m_isMaterialDirty)
			{
				UpdateMaterial();
				m_isMaterialDirty = false;
			}
			break;
		}
	}

	private void UpdateSubObjectPivot()
	{
		if (m_textInfo != null)
		{
			for (int i = 1; i < m_subTextObjects.Length && m_subTextObjects[i] != null; i++)
			{
				m_subTextObjects[i].SetPivotDirty();
			}
		}
	}

	public override Material GetModifiedMaterial(Material baseMaterial)
	{
		Material material = baseMaterial;
		if (m_ShouldRecalculateStencil)
		{
			m_stencilID = TMP_MaterialManager.GetStencilID(base.gameObject);
			m_ShouldRecalculateStencil = false;
		}
		if (m_stencilID > 0)
		{
			material = TMP_MaterialManager.GetStencilMaterial(baseMaterial, m_stencilID);
			if (m_MaskMaterial != null)
			{
				TMP_MaterialManager.ReleaseStencilMaterial(m_MaskMaterial);
			}
			m_MaskMaterial = material;
		}
		return material;
	}

	protected override void UpdateMaterial()
	{
		if (!(m_sharedMaterial == null))
		{
			if (m_canvasRenderer == null)
			{
				m_canvasRenderer = canvasRenderer;
			}
			m_canvasRenderer.materialCount = 1;
			m_canvasRenderer.SetMaterial(materialForRendering, 0);
		}
	}

	public override void RecalculateClipping()
	{
		base.RecalculateClipping();
	}

	public override void RecalculateMasking()
	{
		m_ShouldRecalculateStencil = true;
		SetMaterialDirty();
	}

	public override void Cull(Rect clipRect, bool validRect)
	{
		if (!m_ignoreRectMaskCulling)
		{
			base.Cull(clipRect, validRect);
		}
	}

	public override void UpdateMeshPadding()
	{
		m_padding = ShaderUtilities.GetPadding(m_sharedMaterial, m_enableExtraPadding, m_isUsingBold);
		m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
		m_havePropertiesChanged = true;
		checkPaddingRequired = false;
		if (m_textInfo != null)
		{
			for (int i = 1; i < m_textInfo.materialCount; i++)
			{
				m_subTextObjects[i].UpdateMeshPadding(m_enableExtraPadding, m_isUsingBold);
			}
		}
	}

	protected override void InternalCrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
	{
		int materialCount = m_textInfo.materialCount;
		for (int i = 1; i < materialCount; i++)
		{
			m_subTextObjects[i].CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha);
		}
	}

	protected override void InternalCrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
	{
		int materialCount = m_textInfo.materialCount;
		for (int i = 1; i < materialCount; i++)
		{
			m_subTextObjects[i].CrossFadeAlpha(alpha, duration, ignoreTimeScale);
		}
	}

	public override void ForceMeshUpdate()
	{
		m_havePropertiesChanged = true;
		OnPreRenderCanvas();
	}

	public override void ForceMeshUpdate(bool ignoreInactive)
	{
		m_havePropertiesChanged = true;
		m_ignoreActiveState = true;
		if (m_canvas == null)
		{
			m_canvas = GetComponentInParent<Canvas>();
		}
		OnPreRenderCanvas();
	}

	public override TMP_TextInfo GetTextInfo(string text)
	{
		StringToCharArray(text, ref m_TextParsingBuffer);
		SetArraySizes(m_TextParsingBuffer);
		m_renderMode = TextRenderFlags.DontRender;
		ComputeMarginSize();
		if (m_canvas == null)
		{
			m_canvas = base.canvas;
		}
		GenerateTextMesh();
		m_renderMode = TextRenderFlags.Render;
		return base.textInfo;
	}

	public override void ClearMesh()
	{
		m_canvasRenderer.SetMesh(null);
		for (int i = 1; i < m_subTextObjects.Length && m_subTextObjects[i] != null; i++)
		{
			m_subTextObjects[i].canvasRenderer.SetMesh(null);
		}
	}

	public override void UpdateGeometry(Mesh mesh, int index)
	{
		mesh.RecalculateBounds();
		if (index == 0)
		{
			m_canvasRenderer.SetMesh(mesh);
		}
		else
		{
			m_subTextObjects[index].canvasRenderer.SetMesh(mesh);
		}
	}

	public override void UpdateVertexData(TMP_VertexDataUpdateFlags flags)
	{
		int materialCount = m_textInfo.materialCount;
		for (int i = 0; i < materialCount; i++)
		{
			Mesh mesh = ((i != 0) ? m_subTextObjects[i].mesh : m_mesh);
			if ((flags & TMP_VertexDataUpdateFlags.Vertices) == TMP_VertexDataUpdateFlags.Vertices)
			{
				mesh.vertices = m_textInfo.meshInfo[i].vertices;
			}
			if ((flags & TMP_VertexDataUpdateFlags.Uv0) == TMP_VertexDataUpdateFlags.Uv0)
			{
				mesh.uv = m_textInfo.meshInfo[i].uvs0;
			}
			if ((flags & TMP_VertexDataUpdateFlags.Uv2) == TMP_VertexDataUpdateFlags.Uv2)
			{
				mesh.uv2 = m_textInfo.meshInfo[i].uvs2;
			}
			if ((flags & TMP_VertexDataUpdateFlags.Colors32) == TMP_VertexDataUpdateFlags.Colors32)
			{
				mesh.colors32 = m_textInfo.meshInfo[i].colors32;
			}
			mesh.RecalculateBounds();
			if (i == 0)
			{
				m_canvasRenderer.SetMesh(mesh);
			}
			else
			{
				m_subTextObjects[i].canvasRenderer.SetMesh(mesh);
			}
		}
	}

	public override void UpdateVertexData()
	{
		int num = m_textInfo.meshInfo.Length;
		for (int i = 0; i < num; i++)
		{
			Mesh mesh;
			if (i == 0)
			{
				mesh = m_mesh;
			}
			else
			{
				m_textInfo.meshInfo[i].ClearUnusedVertices();
				mesh = m_subTextObjects[i].mesh;
			}
			if (!(mesh == null))
			{
				mesh.vertices = m_textInfo.meshInfo[i].vertices;
				mesh.uv = m_textInfo.meshInfo[i].uvs0;
				mesh.uv2 = m_textInfo.meshInfo[i].uvs2;
				mesh.colors32 = m_textInfo.meshInfo[i].colors32;
				mesh.RecalculateBounds();
				if (i == 0)
				{
					m_canvasRenderer.SetMesh(mesh);
				}
				else
				{
					m_subTextObjects[i].canvasRenderer.SetMesh(mesh);
				}
			}
		}
	}

	public void UpdateFontAsset()
	{
		LoadFontAsset();
	}

	protected override void Awake()
	{
		m_canvas = base.canvas;
		m_isOrthographic = true;
		m_rectTransform = base.gameObject.GetComponent<RectTransform>();
		if (m_rectTransform == null)
		{
			m_rectTransform = base.gameObject.AddComponent<RectTransform>();
		}
		m_canvasRenderer = GetComponent<CanvasRenderer>();
		if (m_canvasRenderer == null)
		{
			m_canvasRenderer = base.gameObject.AddComponent<CanvasRenderer>();
		}
		if (m_mesh == null)
		{
			m_mesh = new Mesh();
			m_mesh.hideFlags = HideFlags.HideAndDontSave;
			m_textInfo = new TMP_TextInfo(this);
		}
		LoadDefaultSettings();
		LoadFontAsset();
		if (m_TextParsingBuffer == null)
		{
			m_TextParsingBuffer = new UnicodeChar[m_max_characters];
		}
		m_cached_TextElement = new TMP_Character();
		m_isFirstAllocation = true;
		if (m_fontAsset == null)
		{
			UnityEngine.Debug.LogWarning("Please assign a Font Asset to this " + base.transform.name + " gameobject.", this);
			return;
		}
		TMP_SubMeshUI[] componentsInChildren = GetComponentsInChildren<TMP_SubMeshUI>();
		if (componentsInChildren.Length != 0)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				m_subTextObjects[i + 1] = componentsInChildren[i];
			}
		}
		m_isInputParsingRequired = true;
		m_havePropertiesChanged = true;
		m_isCalculateSizeRequired = true;
		m_isAwake = true;
	}

	protected override void OnEnable()
	{
		if (m_isAwake)
		{
			if (!m_isRegisteredForEvents)
			{
				m_isRegisteredForEvents = true;
			}
			m_canvas = GetCanvas();
			SetActiveSubMeshes(state: true);
			GraphicRegistry.RegisterGraphicForCanvas(m_canvas, this);
			if (!m_IsTextObjectScaleStatic && (m_canvas == null || m_canvas.enabled))
			{
				TMP_UpdateManager.RegisterTextObjectForUpdate(this);
			}
			ComputeMarginSize();
			m_verticesAlreadyDirty = false;
			m_layoutAlreadyDirty = false;
			m_ShouldRecalculateStencil = true;
			m_isInputParsingRequired = true;
			SetAllDirty();
			RecalculateClipping();
		}
	}

	protected override void OnDisable()
	{
		if (m_isAwake)
		{
			if (m_MaskMaterial != null)
			{
				TMP_MaterialManager.ReleaseStencilMaterial(m_MaskMaterial);
				m_MaskMaterial = null;
			}
			GraphicRegistry.UnregisterGraphicForCanvas(m_canvas, this);
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
			if (m_canvasRenderer != null)
			{
				m_canvasRenderer.Clear();
			}
			SetActiveSubMeshes(state: false);
			LayoutRebuilder.MarkLayoutForRebuild(m_rectTransform);
			RecalculateClipping();
		}
	}

	protected override void OnDestroy()
	{
		GraphicRegistry.UnregisterGraphicForCanvas(m_canvas, this);
		TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
		if (m_mesh != null)
		{
			UnityEngine.Object.DestroyImmediate(m_mesh);
		}
		if (m_MaskMaterial != null)
		{
			TMP_MaterialManager.ReleaseStencilMaterial(m_MaskMaterial);
			m_MaskMaterial = null;
		}
		m_isRegisteredForEvents = false;
	}

	protected override void LoadFontAsset()
	{
		ShaderUtilities.GetShaderPropertyIDs();
		if (m_fontAsset == null)
		{
			if (TMP_Settings.defaultFontAsset != null)
			{
				m_fontAsset = TMP_Settings.defaultFontAsset;
			}
			else
			{
				m_fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
			}
			if (m_fontAsset == null)
			{
				UnityEngine.Debug.LogWarning("The LiberationSans SDF Font Asset was not found. There is no Font Asset assigned to " + base.gameObject.name + ".", this);
				return;
			}
			if (m_fontAsset.characterLookupTable == null)
			{
				UnityEngine.Debug.Log("Dictionary is Null!");
			}
			m_sharedMaterial = m_fontAsset.material;
		}
		else
		{
			if (m_fontAsset.characterLookupTable == null)
			{
				m_fontAsset.ReadFontAssetDefinition();
			}
			if (m_sharedMaterial == null && m_baseMaterial != null)
			{
				m_sharedMaterial = m_baseMaterial;
				m_baseMaterial = null;
			}
			if (m_sharedMaterial == null || m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex) == null || m_fontAsset.atlasTexture.GetInstanceID() != m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
			{
				if (m_fontAsset.material == null)
				{
					UnityEngine.Debug.LogWarning("The Font Atlas Texture of the Font Asset " + m_fontAsset.name + " assigned to " + base.gameObject.name + " is missing.", this);
				}
				else
				{
					m_sharedMaterial = m_fontAsset.material;
				}
			}
		}
		GetSpecialCharacters(m_fontAsset);
		m_padding = GetPaddingForMaterial();
		SetMaterialDirty();
	}

	private Canvas GetCanvas()
	{
		Canvas result = null;
		List<Canvas> list = TMP_ListPool<Canvas>.Get();
		base.gameObject.GetComponentsInParent(includeInactive: false, list);
		if (list.Count > 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].isActiveAndEnabled)
				{
					result = list[i];
					break;
				}
			}
		}
		TMP_ListPool<Canvas>.Release(list);
		return result;
	}

	private void EnableMasking()
	{
		if (m_fontMaterial == null)
		{
			m_fontMaterial = CreateMaterialInstance(m_sharedMaterial);
			m_canvasRenderer.SetMaterial(m_fontMaterial, m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex));
		}
		m_sharedMaterial = m_fontMaterial;
		if (m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
		{
			m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
			m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
			m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
			UpdateMask();
		}
		m_isMaskingEnabled = true;
	}

	private void DisableMasking()
	{
		if (m_fontMaterial != null)
		{
			if (m_stencilID > 0)
			{
				m_sharedMaterial = m_MaskMaterial;
			}
			else
			{
				m_sharedMaterial = m_baseMaterial;
			}
			m_canvasRenderer.SetMaterial(m_sharedMaterial, m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex));
			UnityEngine.Object.DestroyImmediate(m_fontMaterial);
		}
		m_isMaskingEnabled = false;
	}

	private void UpdateMask()
	{
		if (m_rectTransform != null)
		{
			if (!ShaderUtilities.isInitialized)
			{
				ShaderUtilities.GetShaderPropertyIDs();
			}
			m_isScrollRegionSet = true;
			float num = Mathf.Min(Mathf.Min(m_margin.x, m_margin.z), m_sharedMaterial.GetFloat(ShaderUtilities.ID_MaskSoftnessX));
			float num2 = Mathf.Min(Mathf.Min(m_margin.y, m_margin.w), m_sharedMaterial.GetFloat(ShaderUtilities.ID_MaskSoftnessY));
			num = ((num > 0f) ? num : 0f);
			num2 = ((num2 > 0f) ? num2 : 0f);
			float z = (m_rectTransform.rect.width - Mathf.Max(m_margin.x, 0f) - Mathf.Max(m_margin.z, 0f)) / 2f + num;
			float w = (m_rectTransform.rect.height - Mathf.Max(m_margin.y, 0f) - Mathf.Max(m_margin.w, 0f)) / 2f + num2;
			Vector2 vector = m_rectTransform.localPosition + new Vector3((0.5f - m_rectTransform.pivot.x) * m_rectTransform.rect.width + (Mathf.Max(m_margin.x, 0f) - Mathf.Max(m_margin.z, 0f)) / 2f, (0.5f - m_rectTransform.pivot.y) * m_rectTransform.rect.height + (0f - Mathf.Max(m_margin.y, 0f) + Mathf.Max(m_margin.w, 0f)) / 2f);
			Vector4 value = new Vector4(vector.x, vector.y, z, w);
			m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, value);
		}
	}

	protected override Material GetMaterial(Material mat)
	{
		ShaderUtilities.GetShaderPropertyIDs();
		if (m_fontMaterial == null || m_fontMaterial.GetInstanceID() != mat.GetInstanceID())
		{
			m_fontMaterial = CreateMaterialInstance(mat);
		}
		m_sharedMaterial = m_fontMaterial;
		m_padding = GetPaddingForMaterial();
		m_ShouldRecalculateStencil = true;
		SetVerticesDirty();
		SetMaterialDirty();
		return m_sharedMaterial;
	}

	protected override Material[] GetMaterials(Material[] mats)
	{
		int materialCount = m_textInfo.materialCount;
		if (m_fontMaterials == null)
		{
			m_fontMaterials = new Material[materialCount];
		}
		else if (m_fontMaterials.Length != materialCount)
		{
			TMP_TextInfo.Resize(ref m_fontMaterials, materialCount, isBlockAllocated: false);
		}
		for (int i = 0; i < materialCount; i++)
		{
			if (i == 0)
			{
				m_fontMaterials[i] = base.fontMaterial;
			}
			else
			{
				m_fontMaterials[i] = m_subTextObjects[i].material;
			}
		}
		m_fontSharedMaterials = m_fontMaterials;
		return m_fontMaterials;
	}

	protected override void SetSharedMaterial(Material mat)
	{
		m_sharedMaterial = mat;
		m_padding = GetPaddingForMaterial();
		SetMaterialDirty();
	}

	protected override Material[] GetSharedMaterials()
	{
		int materialCount = m_textInfo.materialCount;
		if (m_fontSharedMaterials == null)
		{
			m_fontSharedMaterials = new Material[materialCount];
		}
		else if (m_fontSharedMaterials.Length != materialCount)
		{
			TMP_TextInfo.Resize(ref m_fontSharedMaterials, materialCount, isBlockAllocated: false);
		}
		for (int i = 0; i < materialCount; i++)
		{
			if (i == 0)
			{
				m_fontSharedMaterials[i] = m_sharedMaterial;
			}
			else
			{
				m_fontSharedMaterials[i] = m_subTextObjects[i].sharedMaterial;
			}
		}
		return m_fontSharedMaterials;
	}

	protected override void SetSharedMaterials(Material[] materials)
	{
		int materialCount = m_textInfo.materialCount;
		if (m_fontSharedMaterials == null)
		{
			m_fontSharedMaterials = new Material[materialCount];
		}
		else if (m_fontSharedMaterials.Length != materialCount)
		{
			TMP_TextInfo.Resize(ref m_fontSharedMaterials, materialCount, isBlockAllocated: false);
		}
		for (int i = 0; i < materialCount; i++)
		{
			if (i == 0)
			{
				if (!(materials[i].GetTexture(ShaderUtilities.ID_MainTex) == null) && materials[i].GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID() == m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
				{
					m_sharedMaterial = (m_fontSharedMaterials[i] = materials[i]);
					m_padding = GetPaddingForMaterial(m_sharedMaterial);
				}
			}
			else if (!(materials[i].GetTexture(ShaderUtilities.ID_MainTex) == null) && materials[i].GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID() == m_subTextObjects[i].sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID() && m_subTextObjects[i].isDefaultMaterial)
			{
				m_subTextObjects[i].sharedMaterial = (m_fontSharedMaterials[i] = materials[i]);
			}
		}
	}

	protected override void SetOutlineThickness(float thickness)
	{
		if (m_fontMaterial != null && m_sharedMaterial.GetInstanceID() != m_fontMaterial.GetInstanceID())
		{
			m_sharedMaterial = m_fontMaterial;
			m_canvasRenderer.SetMaterial(m_sharedMaterial, m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex));
		}
		else if (m_fontMaterial == null)
		{
			m_fontMaterial = CreateMaterialInstance(m_sharedMaterial);
			m_sharedMaterial = m_fontMaterial;
			m_canvasRenderer.SetMaterial(m_sharedMaterial, m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex));
		}
		thickness = Mathf.Clamp01(thickness);
		m_sharedMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, thickness);
		m_padding = GetPaddingForMaterial();
	}

	protected override void SetFaceColor(Color32 color)
	{
		if (m_fontMaterial == null)
		{
			m_fontMaterial = CreateMaterialInstance(m_sharedMaterial);
		}
		m_sharedMaterial = m_fontMaterial;
		m_padding = GetPaddingForMaterial();
		m_sharedMaterial.SetColor(ShaderUtilities.ID_FaceColor, color);
	}

	protected override void SetOutlineColor(Color32 color)
	{
		if (m_fontMaterial == null)
		{
			m_fontMaterial = CreateMaterialInstance(m_sharedMaterial);
		}
		m_sharedMaterial = m_fontMaterial;
		m_padding = GetPaddingForMaterial();
		m_sharedMaterial.SetColor(ShaderUtilities.ID_OutlineColor, color);
	}

	protected override void SetShaderDepth()
	{
		if (!(m_canvas == null) && !(m_sharedMaterial == null))
		{
			if (m_canvas.renderMode == RenderMode.ScreenSpaceOverlay || m_isOverlay)
			{
				m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 0f);
			}
			else
			{
				m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 4f);
			}
		}
	}

	protected override void SetCulling()
	{
		if (m_isCullingEnabled)
		{
			Material material = materialForRendering;
			if (material != null)
			{
				material.SetFloat("_CullMode", 2f);
			}
			for (int i = 1; i < m_subTextObjects.Length && m_subTextObjects[i] != null; i++)
			{
				material = m_subTextObjects[i].materialForRendering;
				if (material != null)
				{
					material.SetFloat(ShaderUtilities.ShaderTag_CullMode, 2f);
				}
			}
			return;
		}
		Material material2 = materialForRendering;
		if (material2 != null)
		{
			material2.SetFloat("_CullMode", 0f);
		}
		for (int j = 1; j < m_subTextObjects.Length && m_subTextObjects[j] != null; j++)
		{
			material2 = m_subTextObjects[j].materialForRendering;
			if (material2 != null)
			{
				material2.SetFloat(ShaderUtilities.ShaderTag_CullMode, 0f);
			}
		}
	}

	private void SetPerspectiveCorrection()
	{
		if (m_isOrthographic)
		{
			m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0f);
		}
		else
		{
			m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0.875f);
		}
	}

	protected override float GetPaddingForMaterial(Material mat)
	{
		m_padding = ShaderUtilities.GetPadding(mat, m_enableExtraPadding, m_isUsingBold);
		m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
		m_isSDFShader = mat.HasProperty(ShaderUtilities.ID_WeightNormal);
		return m_padding;
	}

	protected override float GetPaddingForMaterial()
	{
		ShaderUtilities.GetShaderPropertyIDs();
		if (m_sharedMaterial != null)
		{
			m_padding = ShaderUtilities.GetPadding(m_sharedMaterial, m_enableExtraPadding, m_isUsingBold);
			m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
			m_isSDFShader = m_sharedMaterial.HasProperty(ShaderUtilities.ID_WeightNormal);
		}
		return m_padding;
	}

	private void SetMeshArrays(int size)
	{
		m_textInfo.meshInfo[0].ResizeMeshInfo(size);
		m_canvasRenderer.SetMesh(m_textInfo.meshInfo[0].mesh);
	}

	protected override int SetArraySizes(UnicodeChar[] chars)
	{
		int spriteCount = 0;
		m_totalCharacterCount = 0;
		m_isUsingBold = false;
		m_isParsingText = false;
		tag_NoParsing = false;
		m_FontStyleInternal = m_fontStyle;
		m_FontWeightInternal = (((m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? FontWeight.Bold : m_fontWeight);
		m_FontWeightStack.SetDefault(m_FontWeightInternal);
		m_currentFontAsset = m_fontAsset;
		m_currentMaterial = m_sharedMaterial;
		m_currentMaterialIndex = 0;
		m_materialReferenceStack.SetDefault(new MaterialReference(m_currentMaterialIndex, m_currentFontAsset, m_currentMaterial, m_padding));
		m_materialReferenceIndexLookup.Clear();
		MaterialReference.AddMaterialReference(m_currentMaterial, m_currentFontAsset, m_materialReferences, m_materialReferenceIndexLookup);
		if (m_textInfo == null)
		{
			m_textInfo = new TMP_TextInfo();
		}
		m_textElementType = TMP_TextElementType.Character;
		if (m_linkedTextComponent != null)
		{
			m_linkedTextComponent.text = string.Empty;
			m_linkedTextComponent.ForceMeshUpdate();
		}
		for (int i = 0; i < chars.Length && chars[i].unicode != 0; i++)
		{
			if (m_textInfo.characterInfo == null || m_totalCharacterCount >= m_textInfo.characterInfo.Length)
			{
				TMP_TextInfo.Resize(ref m_textInfo.characterInfo, m_totalCharacterCount + 1, isBlockAllocated: true);
			}
			int num = chars[i].unicode;
			if (m_isRichText && num == 60)
			{
				_ = m_currentMaterialIndex;
				if (ValidateHtmlTag(chars, i + 1, out var endIndex))
				{
					_ = ref chars[i];
					i = endIndex;
					if ((m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
					{
						m_isUsingBold = true;
					}
					continue;
				}
			}
			bool isAlternativeTypeface = false;
			bool flag = false;
			TMP_FontAsset currentFontAsset = m_currentFontAsset;
			Material currentMaterial = m_currentMaterial;
			int currentMaterialIndex = m_currentMaterialIndex;
			if (m_textElementType == TMP_TextElementType.Character)
			{
				if ((m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
				{
					if (char.IsLower((char)num))
					{
						num = char.ToUpper((char)num);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
				{
					if (char.IsUpper((char)num))
					{
						num = char.ToLower((char)num);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num))
				{
					num = char.ToUpper((char)num);
				}
			}
			TMP_FontAsset fontAsset;
			TMP_Character tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num, m_currentFontAsset, includeFallbacks: false, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
			if (tMP_Character == null && m_currentFontAsset.fallbackFontAssetTable != null && m_currentFontAsset.fallbackFontAssetTable.Count > 0)
			{
				tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets((uint)num, m_currentFontAsset.fallbackFontAssetTable, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
			}
			if (tMP_Character == null && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
			{
				tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets((uint)num, TMP_Settings.fallbackFontAssets, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
			}
			if (tMP_Character == null && TMP_Settings.defaultFontAsset != null)
			{
				tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num, TMP_Settings.defaultFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
			}
			if (tMP_Character == null)
			{
				int num2 = num;
				num = (chars[i].unicode = ((TMP_Settings.missingGlyphCharacter == 0) ? 9633 : TMP_Settings.missingGlyphCharacter));
				tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num, m_currentFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
				if (tMP_Character == null && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
				{
					tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets((uint)num, TMP_Settings.fallbackFontAssets, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
				}
				if (tMP_Character == null && TMP_Settings.defaultFontAsset != null)
				{
					tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num, TMP_Settings.defaultFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
				}
				if (tMP_Character == null)
				{
					num = (chars[i].unicode = 32);
					tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num, m_currentFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
					if (!TMP_Settings.warningsDisabled)
					{
						UnityEngine.Debug.LogWarning("Character with ASCII value of " + num2 + " was not found in the Font Asset Glyph Table. It was replaced by a space.", this);
					}
				}
			}
			if (fontAsset != null && fontAsset.GetInstanceID() != m_currentFontAsset.GetInstanceID())
			{
				flag = true;
				m_currentFontAsset = fontAsset;
			}
			m_textInfo.characterInfo[m_totalCharacterCount].elementType = TMP_TextElementType.Character;
			m_textInfo.characterInfo[m_totalCharacterCount].textElement = tMP_Character;
			m_textInfo.characterInfo[m_totalCharacterCount].isUsingAlternateTypeface = isAlternativeTypeface;
			m_textInfo.characterInfo[m_totalCharacterCount].character = (char)num;
			m_textInfo.characterInfo[m_totalCharacterCount].fontAsset = m_currentFontAsset;
			m_textInfo.characterInfo[m_totalCharacterCount].index = chars[i].stringIndex;
			m_textInfo.characterInfo[m_totalCharacterCount].stringLength = chars[i].length;
			if (flag)
			{
				if (TMP_Settings.matchMaterialPreset)
				{
					m_currentMaterial = TMP_MaterialManager.GetFallbackMaterial(m_currentMaterial, m_currentFontAsset.material);
				}
				else
				{
					m_currentMaterial = m_currentFontAsset.material;
				}
				m_currentMaterialIndex = MaterialReference.AddMaterialReference(m_currentMaterial, m_currentFontAsset, m_materialReferences, m_materialReferenceIndexLookup);
			}
			if (!char.IsWhiteSpace((char)num) && num != 8203)
			{
				if (m_materialReferences[m_currentMaterialIndex].referenceCount < 16383)
				{
					m_materialReferences[m_currentMaterialIndex].referenceCount++;
				}
				else
				{
					m_currentMaterialIndex = MaterialReference.AddMaterialReference(new Material(m_currentMaterial), m_currentFontAsset, m_materialReferences, m_materialReferenceIndexLookup);
					m_materialReferences[m_currentMaterialIndex].referenceCount++;
				}
			}
			m_textInfo.characterInfo[m_totalCharacterCount].material = m_currentMaterial;
			m_textInfo.characterInfo[m_totalCharacterCount].materialReferenceIndex = m_currentMaterialIndex;
			m_materialReferences[m_currentMaterialIndex].isFallbackMaterial = flag;
			if (flag)
			{
				m_materialReferences[m_currentMaterialIndex].fallbackMaterial = currentMaterial;
				m_currentFontAsset = currentFontAsset;
				m_currentMaterial = currentMaterial;
				m_currentMaterialIndex = currentMaterialIndex;
			}
			m_totalCharacterCount++;
		}
		if (m_isCalculatingPreferredValues)
		{
			m_isCalculatingPreferredValues = false;
			m_isInputParsingRequired = true;
			return m_totalCharacterCount;
		}
		m_textInfo.spriteCount = spriteCount;
		int num3 = (m_textInfo.materialCount = m_materialReferenceIndexLookup.Count);
		if (num3 > m_textInfo.meshInfo.Length)
		{
			TMP_TextInfo.Resize(ref m_textInfo.meshInfo, num3, isBlockAllocated: false);
		}
		if (num3 > m_subTextObjects.Length)
		{
			TMP_TextInfo.Resize(ref m_subTextObjects, Mathf.NextPowerOfTwo(num3 + 1));
		}
		if (m_textInfo.characterInfo.Length - m_totalCharacterCount > 256)
		{
			TMP_TextInfo.Resize(ref m_textInfo.characterInfo, Mathf.Max(m_totalCharacterCount + 1, 256), isBlockAllocated: true);
		}
		for (int j = 0; j < num3; j++)
		{
			if (j > 0)
			{
				if (m_subTextObjects[j] == null)
				{
					m_subTextObjects[j] = TMP_SubMeshUI.AddSubTextObject(this, m_materialReferences[j]);
					m_textInfo.meshInfo[j].vertices = null;
				}
				if (m_rectTransform.pivot != m_subTextObjects[j].rectTransform.pivot)
				{
					m_subTextObjects[j].rectTransform.pivot = m_rectTransform.pivot;
				}
				if (m_subTextObjects[j].sharedMaterial == null || m_subTextObjects[j].sharedMaterial.GetInstanceID() != m_materialReferences[j].material.GetInstanceID())
				{
					bool isDefaultMaterial = m_materialReferences[j].isDefaultMaterial;
					m_subTextObjects[j].isDefaultMaterial = isDefaultMaterial;
					if (!isDefaultMaterial || m_subTextObjects[j].sharedMaterial == null || m_subTextObjects[j].sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID() != m_materialReferences[j].material.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
					{
						m_subTextObjects[j].sharedMaterial = m_materialReferences[j].material;
						m_subTextObjects[j].fontAsset = m_materialReferences[j].fontAsset;
					}
				}
				if (m_materialReferences[j].isFallbackMaterial)
				{
					m_subTextObjects[j].fallbackMaterial = m_materialReferences[j].material;
					m_subTextObjects[j].fallbackSourceMaterial = m_materialReferences[j].fallbackMaterial;
				}
			}
			int referenceCount = m_materialReferences[j].referenceCount;
			if (m_textInfo.meshInfo[j].vertices == null || m_textInfo.meshInfo[j].vertices.Length < referenceCount * 4)
			{
				if (m_textInfo.meshInfo[j].vertices == null)
				{
					if (j == 0)
					{
						m_textInfo.meshInfo[j] = new TMP_MeshInfo(m_mesh, referenceCount + 1);
					}
					else
					{
						m_textInfo.meshInfo[j] = new TMP_MeshInfo(m_subTextObjects[j].mesh, referenceCount + 1);
					}
				}
				else
				{
					m_textInfo.meshInfo[j].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount + 1));
				}
			}
			else if (m_VertexBufferAutoSizeReduction && referenceCount > 0 && m_textInfo.meshInfo[j].vertices.Length / 4 - referenceCount > 256)
			{
				m_textInfo.meshInfo[j].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount + 1));
			}
		}
		for (int k = num3; k < m_subTextObjects.Length && m_subTextObjects[k] != null; k++)
		{
			if (k < m_textInfo.meshInfo.Length)
			{
				m_subTextObjects[k].canvasRenderer.SetMesh(null);
			}
		}
		return m_totalCharacterCount;
	}

	public override void ComputeMarginSize()
	{
		if (base.rectTransform != null)
		{
			m_marginWidth = m_rectTransform.rect.width - m_margin.x - m_margin.z;
			m_marginHeight = m_rectTransform.rect.height - m_margin.y - m_margin.w;
			m_RectTransformCorners = GetTextContainerLocalCorners();
		}
	}

	protected override void OnDidApplyAnimationProperties()
	{
		m_havePropertiesChanged = true;
		SetVerticesDirty();
		SetLayoutDirty();
	}

	protected override void OnCanvasHierarchyChanged()
	{
		base.OnCanvasHierarchyChanged();
		m_canvas = base.canvas;
		if (m_isAwake && base.isActiveAndEnabled)
		{
			if (m_canvas == null || !m_canvas.enabled)
			{
				TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
			}
			else if (!m_IsTextObjectScaleStatic)
			{
				TMP_UpdateManager.RegisterTextObjectForUpdate(this);
			}
		}
	}

	protected override void OnTransformParentChanged()
	{
		base.OnTransformParentChanged();
		m_canvas = base.canvas;
		ComputeMarginSize();
		m_havePropertiesChanged = true;
	}

	protected override void OnRectTransformDimensionsChange()
	{
		if (base.gameObject.activeInHierarchy && !blockRectTransformChange)
		{
			ComputeMarginSize();
			UpdateSubObjectPivot();
			SetVerticesDirty();
			SetLayoutDirty();
		}
	}

	internal override void InternalUpdate()
	{
		if (!m_havePropertiesChanged)
		{
			float y = m_rectTransform.lossyScale.y;
			if (y != m_previousLossyScaleY && !string.IsNullOrEmpty(m_text))
			{
				float num = y / m_previousLossyScaleY;
				if (num < 0.8f || num > 1.25f)
				{
					UpdateSDFScale(num);
					m_previousLossyScaleY = y;
				}
			}
		}
		if (m_isUsingLegacyAnimationComponent)
		{
			m_havePropertiesChanged = true;
			OnPreRenderCanvas();
		}
	}

	private void OnPreRenderCanvas()
	{
		if (!m_isAwake || (!IsActive() && !m_ignoreActiveState))
		{
			return;
		}
		if (m_canvas == null)
		{
			m_canvas = base.canvas;
			if (m_canvas == null)
			{
				return;
			}
		}
		loopCountA = 0;
		if (m_havePropertiesChanged || m_isLayoutDirty)
		{
			if (checkPaddingRequired)
			{
				UpdateMeshPadding();
			}
			if (m_isInputParsingRequired || m_isTextTruncated)
			{
				ParseInputText();
			}
			if (m_enableAutoSizing)
			{
				m_fontSize = Mathf.Clamp(m_fontSizeBase, m_fontSizeMin, m_fontSizeMax);
			}
			m_maxFontSize = m_fontSizeMax;
			m_minFontSize = m_fontSizeMin;
			m_lineSpacingDelta = 0f;
			m_charWidthAdjDelta = 0f;
			m_isCharacterWrappingEnabled = false;
			m_isTextTruncated = false;
			m_havePropertiesChanged = false;
			m_isLayoutDirty = false;
			m_ignoreActiveState = false;
			GenerateTextMesh();
		}
	}

	protected override void GenerateTextMesh()
	{
		if (m_fontAsset == null || m_fontAsset.characterLookupTable == null)
		{
			UnityEngine.Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned to Object ID: " + GetInstanceID());
			return;
		}
		if (m_textInfo != null)
		{
			m_textInfo.Clear();
		}
		if (m_TextParsingBuffer == null || m_TextParsingBuffer.Length == 0 || m_TextParsingBuffer[0].unicode == 0)
		{
			ClearMesh();
			m_preferredWidth = 0f;
			m_preferredHeight = 0f;
			TMPro_EventManager.ON_TEXT_CHANGED(this);
			return;
		}
		m_currentFontAsset = m_fontAsset;
		m_currentMaterial = m_sharedMaterial;
		m_currentMaterialIndex = 0;
		m_materialReferenceStack.SetDefault(new MaterialReference(m_currentMaterialIndex, m_currentFontAsset, m_currentMaterial, m_padding));
		int totalCharacterCount = m_totalCharacterCount;
		float num = (m_fontScale = m_fontSize / (float)m_fontAsset.faceInfo.pointSize * m_fontAsset.faceInfo.scale);
		float num2 = num;
		m_fontScaleMultiplier = 1f;
		m_currentFontSize = m_fontSize;
		m_sizeStack.SetDefault(m_currentFontSize);
		float num3 = 0f;
		int num4 = 0;
		m_FontStyleInternal = m_fontStyle;
		m_FontWeightInternal = (((m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? FontWeight.Bold : m_fontWeight);
		m_FontWeightStack.SetDefault(m_FontWeightInternal);
		m_fontStyleStack.Clear();
		m_lineJustification = m_textAlignment;
		m_lineJustificationStack.SetDefault(m_lineJustification);
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 1f;
		m_baselineOffset = 0f;
		m_baselineOffsetStack.Clear();
		bool flag = false;
		Vector3 start = Vector3.zero;
		Vector3 zero = Vector3.zero;
		bool flag2 = false;
		Vector3 start2 = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		bool flag3 = false;
		Vector3 start3 = Vector3.zero;
		Vector3 vector = Vector3.zero;
		m_fontColor32 = m_fontColor;
		m_htmlColor = m_fontColor32;
		m_underlineColor = m_htmlColor;
		m_strikethroughColor = m_htmlColor;
		m_colorStack.SetDefault(m_htmlColor);
		m_underlineColorStack.SetDefault(m_htmlColor);
		m_strikethroughColorStack.SetDefault(m_htmlColor);
		m_highlightColorStack.SetDefault(m_htmlColor);
		m_colorGradientPreset = null;
		m_colorGradientStack.SetDefault(null);
		m_actionStack.Clear();
		m_isFXMatrixSet = false;
		m_lineOffset = 0f;
		m_lineHeight = -32767f;
		float num8 = m_currentFontAsset.faceInfo.lineHeight - (m_currentFontAsset.faceInfo.ascentLine - m_currentFontAsset.faceInfo.descentLine);
		m_cSpacing = 0f;
		m_monoSpacing = 0f;
		float num9 = 0f;
		m_xAdvance = 0f;
		tag_LineIndent = 0f;
		tag_Indent = 0f;
		m_indentStack.SetDefault(0f);
		tag_NoParsing = false;
		m_characterCount = 0;
		m_firstCharacterOfLine = 0;
		m_lastCharacterOfLine = 0;
		m_firstVisibleCharacterOfLine = 0;
		m_lastVisibleCharacterOfLine = 0;
		m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
		m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
		m_lineNumber = 0;
		m_lineVisibleCharacterCount = 0;
		bool flag4 = true;
		m_firstOverflowCharacterIndex = -1;
		m_pageNumber = 0;
		int num10 = Mathf.Clamp(m_pageToDisplay - 1, 0, m_textInfo.pageInfo.Length - 1);
		int num11 = 0;
		int num12 = 0;
		Vector4 vector2 = m_margin;
		float marginWidth = m_marginWidth;
		float marginHeight = m_marginHeight;
		m_marginLeft = 0f;
		m_marginRight = 0f;
		m_width = -1f;
		float num13 = marginWidth + 0.0001f - m_marginLeft - m_marginRight;
		m_meshExtents.min = TMP_Text.k_LargePositiveVector2;
		m_meshExtents.max = TMP_Text.k_LargeNegativeVector2;
		m_textInfo.ClearLineInfo();
		m_maxCapHeight = 0f;
		m_maxAscender = 0f;
		m_maxDescender = 0f;
		float num14 = 0f;
		float num15 = 0f;
		bool flag5 = false;
		m_isNewPage = false;
		bool flag6 = true;
		m_isNonBreakingSpace = false;
		bool flag7 = false;
		bool flag8 = false;
		int num16 = 0;
		SaveWordWrappingState(ref m_SavedWordWrapState, -1, -1);
		SaveWordWrappingState(ref m_SavedLineState, -1, -1);
		loopCountA++;
		Vector3 vector3 = default(Vector3);
		Vector3 vector4 = default(Vector3);
		Vector3 vector5 = default(Vector3);
		Vector3 vector6 = default(Vector3);
		for (int i = 0; i < m_TextParsingBuffer.Length && m_TextParsingBuffer[i].unicode != 0; i++)
		{
			num4 = m_TextParsingBuffer[i].unicode;
			if (m_isRichText && num4 == 60)
			{
				m_isParsingText = true;
				m_textElementType = TMP_TextElementType.Character;
				if (ValidateHtmlTag(m_TextParsingBuffer, i + 1, out var endIndex))
				{
					i = endIndex;
					if (m_textElementType == TMP_TextElementType.Character)
					{
						continue;
					}
				}
			}
			else
			{
				m_textElementType = m_textInfo.characterInfo[m_characterCount].elementType;
				m_currentMaterialIndex = m_textInfo.characterInfo[m_characterCount].materialReferenceIndex;
				m_currentFontAsset = m_textInfo.characterInfo[m_characterCount].fontAsset;
			}
			_ = m_currentMaterialIndex;
			bool isUsingAlternateTypeface = m_textInfo.characterInfo[m_characterCount].isUsingAlternateTypeface;
			m_isParsingText = false;
			if (m_characterCount < m_firstVisibleCharacter)
			{
				m_textInfo.characterInfo[m_characterCount].isVisible = false;
				m_textInfo.characterInfo[m_characterCount].character = '\u200b';
				m_characterCount++;
				continue;
			}
			float num17 = 1f;
			if (m_textElementType == TMP_TextElementType.Character)
			{
				if ((m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
				{
					if (char.IsLower((char)num4))
					{
						num4 = char.ToUpper((char)num4);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
				{
					if (char.IsUpper((char)num4))
					{
						num4 = char.ToLower((char)num4);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num4))
				{
					num17 = 0.8f;
					num4 = char.ToUpper((char)num4);
				}
			}
			if (m_textElementType == TMP_TextElementType.Character)
			{
				m_cached_TextElement = m_textInfo.characterInfo[m_characterCount].textElement;
				if (m_cached_TextElement == null)
				{
					continue;
				}
				m_currentFontAsset = m_textInfo.characterInfo[m_characterCount].fontAsset;
				m_currentMaterial = m_textInfo.characterInfo[m_characterCount].material;
				m_currentMaterialIndex = m_textInfo.characterInfo[m_characterCount].materialReferenceIndex;
				m_fontScale = m_currentFontSize * num17 / (float)m_currentFontAsset.faceInfo.pointSize * m_currentFontAsset.faceInfo.scale;
				num2 = m_fontScale * m_fontScaleMultiplier * m_cached_TextElement.scale * m_cached_TextElement.glyph.scale;
				m_textInfo.characterInfo[m_characterCount].elementType = TMP_TextElementType.Character;
				m_textInfo.characterInfo[m_characterCount].scale = num2;
				num5 = ((m_currentMaterialIndex == 0) ? m_padding : m_subTextObjects[m_currentMaterialIndex].padding);
			}
			float num18 = num2;
			if (num4 == 173)
			{
				num2 = 0f;
			}
			m_textInfo.characterInfo[m_characterCount].character = (char)num4;
			m_textInfo.characterInfo[m_characterCount].pointSize = m_currentFontSize;
			m_textInfo.characterInfo[m_characterCount].color = m_htmlColor;
			m_textInfo.characterInfo[m_characterCount].underlineColor = m_underlineColor;
			m_textInfo.characterInfo[m_characterCount].strikethroughColor = m_strikethroughColor;
			m_textInfo.characterInfo[m_characterCount].highlightColor = m_highlightColor;
			m_textInfo.characterInfo[m_characterCount].style = m_FontStyleInternal;
			TMP_GlyphValueRecord tMP_GlyphValueRecord = default(TMP_GlyphValueRecord);
			float num19 = m_characterSpacing;
			if (m_enableKerning)
			{
				if (m_characterCount < totalCharacterCount - 1)
				{
					uint glyphIndex = m_cached_TextElement.glyphIndex;
					uint glyphIndex2 = m_textInfo.characterInfo[m_characterCount + 1].textElement.glyphIndex;
					long key = new GlyphPairKey(glyphIndex, glyphIndex2).key;
					if (m_currentFontAsset.fontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(key, out var value))
					{
						tMP_GlyphValueRecord = value.firstAdjustmentRecord.glyphValueRecord;
						num19 = (((value.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num19);
					}
				}
				if (m_characterCount >= 1)
				{
					uint glyphIndex3 = m_textInfo.characterInfo[m_characterCount - 1].textElement.glyphIndex;
					uint glyphIndex4 = m_cached_TextElement.glyphIndex;
					long key2 = new GlyphPairKey(glyphIndex3, glyphIndex4).key;
					if (m_currentFontAsset.fontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(key2, out var value2))
					{
						tMP_GlyphValueRecord += value2.secondAdjustmentRecord.glyphValueRecord;
						num19 = (((value2.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num19);
					}
				}
			}
			if (m_isRightToLeft)
			{
				m_xAdvance -= ((m_cached_TextElement.glyph.metrics.horizontalAdvance * num7 + num19 + m_wordSpacing + m_currentFontAsset.normalSpacingOffset) * num2 + m_cSpacing) * (1f - m_charWidthAdjDelta);
				if (char.IsWhiteSpace((char)num4) || num4 == 8203)
				{
					m_xAdvance -= m_wordSpacing * num2;
				}
			}
			float num20 = 0f;
			if (m_monoSpacing != 0f)
			{
				num20 = (m_monoSpacing / 2f - (m_cached_TextElement.glyph.metrics.width / 2f + m_cached_TextElement.glyph.metrics.horizontalBearingX) * num2) * (1f - m_charWidthAdjDelta);
				m_xAdvance += num20;
			}
			if (m_textElementType == TMP_TextElementType.Character && !isUsingAlternateTypeface && (m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
			{
				if (m_currentMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
				{
					float num21 = m_currentMaterial.GetFloat(ShaderUtilities.ID_GradientScale);
					num6 = m_currentFontAsset.boldStyle / 4f * num21 * m_currentMaterial.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
					if (num6 + num5 > num21)
					{
						num5 = num21 - num6;
					}
				}
				else
				{
					num6 = 0f;
				}
				num7 = 1f + m_currentFontAsset.boldSpacing * 0.01f;
			}
			else
			{
				if (m_currentMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
				{
					float num22 = m_currentMaterial.GetFloat(ShaderUtilities.ID_GradientScale);
					num6 = m_currentFontAsset.normalStyle / 4f * num22 * m_currentMaterial.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
					if (num6 + num5 > num22)
					{
						num5 = num22 - num6;
					}
				}
				else
				{
					num6 = 0f;
				}
				num7 = 1f;
			}
			float num23 = m_currentFontAsset.faceInfo.baseline * m_fontScale * m_fontScaleMultiplier * m_currentFontAsset.faceInfo.scale;
			vector3.x = m_xAdvance + (m_cached_TextElement.glyph.metrics.horizontalBearingX - num5 - num6 + tMP_GlyphValueRecord.xPlacement) * num2 * (1f - m_charWidthAdjDelta);
			vector3.y = num23 + (m_cached_TextElement.glyph.metrics.horizontalBearingY + num5 + tMP_GlyphValueRecord.yPlacement) * num2 - m_lineOffset + m_baselineOffset;
			vector3.z = 0f;
			vector4.x = vector3.x;
			vector4.y = vector3.y - (m_cached_TextElement.glyph.metrics.height + num5 * 2f) * num2;
			vector4.z = 0f;
			vector5.x = vector4.x + (m_cached_TextElement.glyph.metrics.width + num5 * 2f + num6 * 2f) * num2 * (1f - m_charWidthAdjDelta);
			vector5.y = vector3.y;
			vector5.z = 0f;
			vector6.x = vector5.x;
			vector6.y = vector4.y;
			vector6.z = 0f;
			if (m_textElementType == TMP_TextElementType.Character && !isUsingAlternateTypeface && (m_FontStyleInternal & FontStyles.Italic) == FontStyles.Italic)
			{
				float num24 = (float)(int)m_currentFontAsset.italicStyle * 0.01f;
				Vector3 vector7 = new Vector3(num24 * ((m_cached_TextElement.glyph.metrics.horizontalBearingY + num5 + num6) * num2), 0f, 0f);
				Vector3 vector8 = new Vector3(num24 * ((m_cached_TextElement.glyph.metrics.horizontalBearingY - m_cached_TextElement.glyph.metrics.height - num5 - num6) * num2), 0f, 0f);
				vector3 += vector7;
				vector4 += vector8;
				vector5 += vector7;
				vector6 += vector8;
			}
			if (m_isFXMatrixSet)
			{
				_ = m_FXMatrix.lossyScale.x;
				_ = 1f;
				Vector3 vector9 = (vector5 + vector4) / 2f;
				vector3 = m_FXMatrix.MultiplyPoint3x4(vector3 - vector9) + vector9;
				vector4 = m_FXMatrix.MultiplyPoint3x4(vector4 - vector9) + vector9;
				vector5 = m_FXMatrix.MultiplyPoint3x4(vector5 - vector9) + vector9;
				vector6 = m_FXMatrix.MultiplyPoint3x4(vector6 - vector9) + vector9;
			}
			m_textInfo.characterInfo[m_characterCount].bottomLeft = vector4;
			m_textInfo.characterInfo[m_characterCount].topLeft = vector3;
			m_textInfo.characterInfo[m_characterCount].topRight = vector5;
			m_textInfo.characterInfo[m_characterCount].bottomRight = vector6;
			m_textInfo.characterInfo[m_characterCount].origin = m_xAdvance;
			m_textInfo.characterInfo[m_characterCount].baseLine = num23 - m_lineOffset + m_baselineOffset;
			m_textInfo.characterInfo[m_characterCount].aspectRatio = (vector5.x - vector4.x) / (vector3.y - vector4.y);
			float num25 = m_currentFontAsset.faceInfo.ascentLine * ((m_textElementType == TMP_TextElementType.Character) ? (num2 / num17) : m_textInfo.characterInfo[m_characterCount].scale) + m_baselineOffset;
			m_textInfo.characterInfo[m_characterCount].ascender = num25 - m_lineOffset;
			m_maxLineAscender = ((num25 > m_maxLineAscender) ? num25 : m_maxLineAscender);
			float num26 = m_currentFontAsset.faceInfo.descentLine * ((m_textElementType == TMP_TextElementType.Character) ? (num2 / num17) : m_textInfo.characterInfo[m_characterCount].scale) + m_baselineOffset;
			float num27 = (m_textInfo.characterInfo[m_characterCount].descender = num26 - m_lineOffset);
			m_maxLineDescender = ((num26 < m_maxLineDescender) ? num26 : m_maxLineDescender);
			if ((m_FontStyleInternal & FontStyles.Subscript) == FontStyles.Subscript || (m_FontStyleInternal & FontStyles.Superscript) == FontStyles.Superscript)
			{
				float num28 = (num25 - m_baselineOffset) / m_currentFontAsset.faceInfo.subscriptSize;
				num25 = m_maxLineAscender;
				m_maxLineAscender = ((num28 > m_maxLineAscender) ? num28 : m_maxLineAscender);
				float num29 = (num26 - m_baselineOffset) / m_currentFontAsset.faceInfo.subscriptSize;
				num26 = m_maxLineDescender;
				m_maxLineDescender = ((num29 < m_maxLineDescender) ? num29 : m_maxLineDescender);
			}
			if (m_lineNumber == 0 || m_isNewPage)
			{
				m_maxAscender = ((m_maxAscender > num25) ? m_maxAscender : num25);
				m_maxCapHeight = Mathf.Max(m_maxCapHeight, m_currentFontAsset.faceInfo.capLine * num2 / num17);
			}
			if (m_lineOffset == 0f)
			{
				num14 = ((num14 > num25) ? num14 : num25);
			}
			m_textInfo.characterInfo[m_characterCount].isVisible = false;
			if (num4 == 9 || num4 == 160 || num4 == 8199 || (!char.IsWhiteSpace((char)num4) && num4 != 8203))
			{
				m_textInfo.characterInfo[m_characterCount].isVisible = true;
				num13 = ((m_width != -1f) ? Mathf.Min(marginWidth + 0.0001f - m_marginLeft - m_marginRight, m_width) : (marginWidth + 0.0001f - m_marginLeft - m_marginRight));
				m_textInfo.lineInfo[m_lineNumber].marginLeft = m_marginLeft;
				bool flag9 = (m_lineJustification & (TextAlignmentOptions)16) == (TextAlignmentOptions)16 || (m_lineJustification & (TextAlignmentOptions)8) == (TextAlignmentOptions)8;
				if (Mathf.Abs(m_xAdvance) + ((!m_isRightToLeft) ? m_cached_TextElement.glyph.metrics.horizontalAdvance : 0f) * (1f - m_charWidthAdjDelta) * ((num4 != 173) ? num2 : num18) > num13 * (flag9 ? 1.05f : 1f))
				{
					num12 = m_characterCount - 1;
					if (base.enableWordWrapping && m_characterCount != m_firstCharacterOfLine)
					{
						if (num16 == m_SavedWordWrapState.previous_WordBreak || flag6)
						{
							if (m_enableAutoSizing && m_fontSize > m_fontSizeMin)
							{
								if (m_charWidthAdjDelta < m_charWidthMaxAdj / 100f)
								{
									loopCountA = 0;
									m_charWidthAdjDelta += 0.01f;
									GenerateTextMesh();
									return;
								}
								m_maxFontSize = m_fontSize;
								m_fontSize -= Mathf.Max((m_fontSize - m_minFontSize) / 2f, 0.05f);
								m_fontSize = (float)(int)(Mathf.Max(m_fontSize, m_fontSizeMin) * 20f + 0.5f) / 20f;
								if (loopCountA <= 20)
								{
									GenerateTextMesh();
								}
								return;
							}
							if (!m_isCharacterWrappingEnabled)
							{
								if (!flag7)
								{
									flag7 = true;
								}
								else
								{
									m_isCharacterWrappingEnabled = true;
								}
							}
							else
							{
								flag8 = true;
							}
						}
						i = RestoreWordWrappingState(ref m_SavedWordWrapState);
						num16 = i;
						if (m_TextParsingBuffer[i].unicode == 173)
						{
							m_isTextTruncated = true;
							m_TextParsingBuffer[i].unicode = 45;
							GenerateTextMesh();
							return;
						}
						if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == -32767f && !m_isNewPage)
						{
							float num30 = m_maxLineAscender - m_startOfLineAscender;
							AdjustLineOffset(m_firstCharacterOfLine, m_characterCount, num30);
							m_lineOffset += num30;
							m_SavedWordWrapState.lineOffset = m_lineOffset;
							m_SavedWordWrapState.previousLineAscender = m_maxLineAscender;
						}
						m_isNewPage = false;
						float num31 = m_maxLineAscender - m_lineOffset;
						float num32 = m_maxLineDescender - m_lineOffset;
						m_maxDescender = ((m_maxDescender < num32) ? m_maxDescender : num32);
						if (!flag5)
						{
							num15 = m_maxDescender;
						}
						if (m_useMaxVisibleDescender && (m_characterCount >= m_maxVisibleCharacters || m_lineNumber >= m_maxVisibleLines))
						{
							flag5 = true;
						}
						m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex = m_firstCharacterOfLine;
						m_textInfo.lineInfo[m_lineNumber].firstVisibleCharacterIndex = (m_firstVisibleCharacterOfLine = ((m_firstCharacterOfLine > m_firstVisibleCharacterOfLine) ? m_firstCharacterOfLine : m_firstVisibleCharacterOfLine));
						m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex = (m_lastCharacterOfLine = ((m_characterCount - 1 > 0) ? (m_characterCount - 1) : 0));
						m_textInfo.lineInfo[m_lineNumber].lastVisibleCharacterIndex = (m_lastVisibleCharacterOfLine = ((m_lastVisibleCharacterOfLine < m_firstVisibleCharacterOfLine) ? m_firstVisibleCharacterOfLine : m_lastVisibleCharacterOfLine));
						m_textInfo.lineInfo[m_lineNumber].characterCount = m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex - m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex + 1;
						m_textInfo.lineInfo[m_lineNumber].visibleCharacterCount = m_lineVisibleCharacterCount;
						m_textInfo.lineInfo[m_lineNumber].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_firstVisibleCharacterOfLine].bottomLeft.x, num32);
						m_textInfo.lineInfo[m_lineNumber].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].topRight.x, num31);
						m_textInfo.lineInfo[m_lineNumber].length = m_textInfo.lineInfo[m_lineNumber].lineExtents.max.x;
						m_textInfo.lineInfo[m_lineNumber].width = num13;
						m_textInfo.lineInfo[m_lineNumber].maxAdvance = m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].xAdvance - (num19 + m_currentFontAsset.normalSpacingOffset) * num2 - m_cSpacing;
						m_textInfo.lineInfo[m_lineNumber].baseline = 0f - m_lineOffset;
						m_textInfo.lineInfo[m_lineNumber].ascender = num31;
						m_textInfo.lineInfo[m_lineNumber].descender = num32;
						m_textInfo.lineInfo[m_lineNumber].lineHeight = num31 - num32 + num8 * num;
						m_firstCharacterOfLine = m_characterCount;
						m_lineVisibleCharacterCount = 0;
						SaveWordWrappingState(ref m_SavedLineState, i, m_characterCount - 1);
						m_lineNumber++;
						flag4 = true;
						flag6 = true;
						if (m_lineNumber >= m_textInfo.lineInfo.Length)
						{
							ResizeLineExtents(m_lineNumber);
						}
						if (m_lineHeight == -32767f)
						{
							float num33 = m_textInfo.characterInfo[m_characterCount].ascender - m_textInfo.characterInfo[m_characterCount].baseLine;
							num9 = 0f - m_maxLineDescender + num33 + (num8 + m_lineSpacing + m_lineSpacingDelta) * num;
							m_lineOffset += num9;
							m_startOfLineAscender = num33;
						}
						else
						{
							m_lineOffset += m_lineHeight + m_lineSpacing * num;
						}
						m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
						m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
						m_xAdvance = 0f + tag_Indent;
						continue;
					}
					if (m_enableAutoSizing && m_fontSize > m_fontSizeMin)
					{
						if (m_charWidthAdjDelta < m_charWidthMaxAdj / 100f)
						{
							loopCountA = 0;
							m_charWidthAdjDelta += 0.01f;
							GenerateTextMesh();
							return;
						}
						m_maxFontSize = m_fontSize;
						m_fontSize -= Mathf.Max((m_fontSize - m_minFontSize) / 2f, 0.05f);
						m_fontSize = (float)(int)(Mathf.Max(m_fontSize, m_fontSizeMin) * 20f + 0.5f) / 20f;
						if (loopCountA <= 20)
						{
							GenerateTextMesh();
						}
						return;
					}
					switch (m_overflowMode)
					{
					case TextOverflowModes.Overflow:
						if (m_isMaskingEnabled)
						{
							DisableMasking();
						}
						break;
					case TextOverflowModes.Ellipsis:
						if (m_isMaskingEnabled)
						{
							DisableMasking();
						}
						m_isTextTruncated = true;
						if (m_characterCount < 1)
						{
							m_textInfo.characterInfo[m_characterCount].isVisible = false;
							break;
						}
						m_TextParsingBuffer[i - 1].unicode = 8230;
						m_TextParsingBuffer[i].unicode = 0;
						if (m_cached_Ellipsis_Character != null)
						{
							m_textInfo.characterInfo[num12].character = '…';
							m_textInfo.characterInfo[num12].textElement = m_cached_Ellipsis_Character;
							m_textInfo.characterInfo[num12].fontAsset = m_materialReferences[0].fontAsset;
							m_textInfo.characterInfo[num12].material = m_materialReferences[0].material;
							m_textInfo.characterInfo[num12].materialReferenceIndex = 0;
						}
						else
						{
							UnityEngine.Debug.LogWarning("Unable to use Ellipsis character since it wasn't found in the current Font Asset [" + m_fontAsset.name + "]. Consider regenerating this font asset to include the Ellipsis character (u+2026).\nNote: Warnings can be disabled in the TMP Settings file.", this);
						}
						m_totalCharacterCount = num12 + 1;
						GenerateTextMesh();
						return;
					case TextOverflowModes.Truncate:
						if (m_isMaskingEnabled)
						{
							DisableMasking();
						}
						m_textInfo.characterInfo[m_characterCount].isVisible = false;
						break;
					}
				}
				if (num4 == 9 || num4 == 160 || num4 == 8199)
				{
					m_textInfo.characterInfo[m_characterCount].isVisible = false;
					m_lastVisibleCharacterOfLine = m_characterCount;
					m_textInfo.lineInfo[m_lineNumber].spaceCount++;
					m_textInfo.spaceCount++;
					if (num4 == 160)
					{
						m_textInfo.lineInfo[m_lineNumber].controlCharacterCount++;
					}
				}
				else
				{
					Color32 vertexColor = ((!m_overrideHtmlColors) ? m_htmlColor : m_fontColor32);
					if (m_textElementType == TMP_TextElementType.Character)
					{
						SaveGlyphVertexInfo(num5, num6, vertexColor);
					}
				}
				if (m_textInfo.characterInfo[m_characterCount].isVisible && num4 != 173)
				{
					if (flag4)
					{
						flag4 = false;
						m_firstVisibleCharacterOfLine = m_characterCount;
					}
					m_lineVisibleCharacterCount++;
					m_lastVisibleCharacterOfLine = m_characterCount;
				}
			}
			else if ((num4 == 10 || char.IsSeparator((char)num4)) && num4 != 173 && num4 != 8203 && num4 != 8288)
			{
				m_textInfo.lineInfo[m_lineNumber].spaceCount++;
				m_textInfo.spaceCount++;
			}
			if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == -32767f && !m_isNewPage)
			{
				float num34 = m_maxLineAscender - m_startOfLineAscender;
				AdjustLineOffset(m_firstCharacterOfLine, m_characterCount, num34);
				num27 -= num34;
				m_lineOffset += num34;
				m_startOfLineAscender += num34;
				m_SavedWordWrapState.lineOffset = m_lineOffset;
				m_SavedWordWrapState.previousLineAscender = m_startOfLineAscender;
			}
			m_textInfo.characterInfo[m_characterCount].lineNumber = m_lineNumber;
			m_textInfo.characterInfo[m_characterCount].pageNumber = m_pageNumber;
			if ((num4 != 10 && num4 != 13 && num4 != 8230) || m_textInfo.lineInfo[m_lineNumber].characterCount == 1)
			{
				m_textInfo.lineInfo[m_lineNumber].alignment = m_lineJustification;
			}
			if (m_maxAscender - num27 > marginHeight + 0.0001f)
			{
				if (m_enableAutoSizing && m_lineSpacingDelta > m_lineSpacingMax && m_lineNumber > 0)
				{
					loopCountA = 0;
					m_lineSpacingDelta -= 1f;
					GenerateTextMesh();
					return;
				}
				if (m_enableAutoSizing && m_fontSize > m_fontSizeMin)
				{
					m_maxFontSize = m_fontSize;
					m_fontSize -= Mathf.Max((m_fontSize - m_minFontSize) / 2f, 0.05f);
					m_fontSize = (float)(int)(Mathf.Max(m_fontSize, m_fontSizeMin) * 20f + 0.5f) / 20f;
					if (loopCountA <= 20)
					{
						GenerateTextMesh();
					}
					return;
				}
				if (m_firstOverflowCharacterIndex == -1)
				{
					m_firstOverflowCharacterIndex = m_characterCount;
				}
				switch (m_overflowMode)
				{
				case TextOverflowModes.Overflow:
					if (m_isMaskingEnabled)
					{
						DisableMasking();
					}
					break;
				case TextOverflowModes.Ellipsis:
					if (m_isMaskingEnabled)
					{
						DisableMasking();
					}
					if (m_lineNumber > 0)
					{
						m_TextParsingBuffer[m_textInfo.characterInfo[num12].index].unicode = 8230;
						m_TextParsingBuffer[m_textInfo.characterInfo[num12].index + 1].unicode = 0;
						if (m_cached_Ellipsis_Character != null)
						{
							m_textInfo.characterInfo[num12].character = '…';
							m_textInfo.characterInfo[num12].textElement = m_cached_Ellipsis_Character;
							m_textInfo.characterInfo[num12].fontAsset = m_materialReferences[0].fontAsset;
							m_textInfo.characterInfo[num12].material = m_materialReferences[0].material;
							m_textInfo.characterInfo[num12].materialReferenceIndex = 0;
						}
						else
						{
							UnityEngine.Debug.LogWarning("Unable to use Ellipsis character since it wasn't found in the current Font Asset [" + m_fontAsset.name + "]. Consider regenerating this font asset to include the Ellipsis character (u+2026).\nNote: Warnings can be disabled in the TMP Settings file.", this);
						}
						m_totalCharacterCount = num12 + 1;
						GenerateTextMesh();
						m_isTextTruncated = true;
					}
					else
					{
						ClearMesh();
					}
					return;
				case TextOverflowModes.Truncate:
					if (m_isMaskingEnabled)
					{
						DisableMasking();
					}
					if (m_lineNumber > 0)
					{
						if (m_truncateRecurseCount < 4)
						{
							m_TextParsingBuffer[m_textInfo.characterInfo[num12].index + 1].unicode = 0;
							m_truncateRecurseCount++;
							try
							{
								GenerateTextMesh();
							}
							finally
							{
								m_truncateRecurseCount--;
							}
							m_isTextTruncated = true;
						}
					}
					else
					{
						ClearMesh();
					}
					return;
				case TextOverflowModes.Page:
					if (m_isMaskingEnabled)
					{
						DisableMasking();
					}
					if (num4 != 13 && num4 != 10)
					{
						if (i == 0)
						{
							ClearMesh();
							return;
						}
						if (num11 == i)
						{
							m_TextParsingBuffer[i].unicode = 0;
							m_isTextTruncated = true;
						}
						num11 = i;
						i = RestoreWordWrappingState(ref m_SavedLineState);
						m_isNewPage = true;
						m_xAdvance = 0f + tag_Indent;
						m_lineOffset = 0f;
						m_maxAscender = 0f;
						num14 = 0f;
						m_lineNumber++;
						m_pageNumber++;
						continue;
					}
					break;
				case TextOverflowModes.Linked:
					if (m_linkedTextComponent != null)
					{
						m_linkedTextComponent.text = base.text;
						m_linkedTextComponent.firstVisibleCharacter = m_characterCount;
						m_linkedTextComponent.ForceMeshUpdate();
					}
					if (m_lineNumber > 0)
					{
						m_TextParsingBuffer[i].unicode = 0;
						m_totalCharacterCount = m_characterCount;
						GenerateTextMesh();
						m_isTextTruncated = true;
					}
					else
					{
						ClearMesh();
					}
					return;
				}
			}
			if (num4 == 9)
			{
				float num35 = m_currentFontAsset.faceInfo.tabWidth * (float)(int)m_currentFontAsset.tabSize * num2;
				float num36 = Mathf.Ceil(m_xAdvance / num35) * num35;
				m_xAdvance = ((num36 > m_xAdvance) ? num36 : (m_xAdvance + num35));
			}
			else if (m_monoSpacing != 0f)
			{
				m_xAdvance += (m_monoSpacing - num20 + (num19 + m_currentFontAsset.normalSpacingOffset) * num2 + m_cSpacing) * (1f - m_charWidthAdjDelta);
				if (char.IsWhiteSpace((char)num4) || num4 == 8203)
				{
					m_xAdvance += m_wordSpacing * num2;
				}
			}
			else if (!m_isRightToLeft)
			{
				float num37 = 1f;
				if (m_isFXMatrixSet)
				{
					num37 = m_FXMatrix.lossyScale.x;
				}
				m_xAdvance += ((m_cached_TextElement.glyph.metrics.horizontalAdvance * num37 * num7 + num19 + m_currentFontAsset.normalSpacingOffset + tMP_GlyphValueRecord.xAdvance) * num2 + m_cSpacing) * (1f - m_charWidthAdjDelta);
				if (char.IsWhiteSpace((char)num4) || num4 == 8203)
				{
					m_xAdvance += m_wordSpacing * num2;
				}
			}
			else
			{
				m_xAdvance -= tMP_GlyphValueRecord.xAdvance * num2;
			}
			m_textInfo.characterInfo[m_characterCount].xAdvance = m_xAdvance;
			if (num4 == 13)
			{
				m_xAdvance = 0f + tag_Indent;
			}
			if (num4 == 10 || m_characterCount == totalCharacterCount - 1)
			{
				if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == -32767f && !m_isNewPage)
				{
					float num38 = m_maxLineAscender - m_startOfLineAscender;
					AdjustLineOffset(m_firstCharacterOfLine, m_characterCount, num38);
					num27 -= num38;
					m_lineOffset += num38;
				}
				m_isNewPage = false;
				float num39 = m_maxLineAscender - m_lineOffset;
				float num40 = m_maxLineDescender - m_lineOffset;
				m_maxDescender = ((m_maxDescender < num40) ? m_maxDescender : num40);
				if (!flag5)
				{
					num15 = m_maxDescender;
				}
				if (m_useMaxVisibleDescender && (m_characterCount >= m_maxVisibleCharacters || m_lineNumber >= m_maxVisibleLines))
				{
					flag5 = true;
				}
				m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex = m_firstCharacterOfLine;
				m_textInfo.lineInfo[m_lineNumber].firstVisibleCharacterIndex = (m_firstVisibleCharacterOfLine = ((m_firstCharacterOfLine > m_firstVisibleCharacterOfLine) ? m_firstCharacterOfLine : m_firstVisibleCharacterOfLine));
				m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex = (m_lastCharacterOfLine = m_characterCount);
				m_textInfo.lineInfo[m_lineNumber].lastVisibleCharacterIndex = (m_lastVisibleCharacterOfLine = ((m_lastVisibleCharacterOfLine < m_firstVisibleCharacterOfLine) ? m_firstVisibleCharacterOfLine : m_lastVisibleCharacterOfLine));
				m_textInfo.lineInfo[m_lineNumber].characterCount = m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex - m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex + 1;
				m_textInfo.lineInfo[m_lineNumber].visibleCharacterCount = m_lineVisibleCharacterCount;
				m_textInfo.lineInfo[m_lineNumber].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_firstVisibleCharacterOfLine].bottomLeft.x, num40);
				m_textInfo.lineInfo[m_lineNumber].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].topRight.x, num39);
				m_textInfo.lineInfo[m_lineNumber].length = m_textInfo.lineInfo[m_lineNumber].lineExtents.max.x - num5 * num2;
				m_textInfo.lineInfo[m_lineNumber].width = num13;
				if (m_textInfo.lineInfo[m_lineNumber].characterCount == 1)
				{
					m_textInfo.lineInfo[m_lineNumber].alignment = m_lineJustification;
				}
				if (m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].isVisible)
				{
					m_textInfo.lineInfo[m_lineNumber].maxAdvance = m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].xAdvance - (num19 + m_currentFontAsset.normalSpacingOffset) * num2 - m_cSpacing;
				}
				else
				{
					m_textInfo.lineInfo[m_lineNumber].maxAdvance = m_textInfo.characterInfo[m_lastCharacterOfLine].xAdvance - (num19 + m_currentFontAsset.normalSpacingOffset) * num2 - m_cSpacing;
				}
				m_textInfo.lineInfo[m_lineNumber].baseline = 0f - m_lineOffset;
				m_textInfo.lineInfo[m_lineNumber].ascender = num39;
				m_textInfo.lineInfo[m_lineNumber].descender = num40;
				m_textInfo.lineInfo[m_lineNumber].lineHeight = num39 - num40 + num8 * num;
				m_firstCharacterOfLine = m_characterCount + 1;
				m_lineVisibleCharacterCount = 0;
				if (num4 == 10)
				{
					SaveWordWrappingState(ref m_SavedLineState, i, m_characterCount);
					SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
					m_lineNumber++;
					flag4 = true;
					flag7 = false;
					flag6 = true;
					if (m_lineNumber >= m_textInfo.lineInfo.Length)
					{
						ResizeLineExtents(m_lineNumber);
					}
					if (m_lineHeight == -32767f)
					{
						num9 = 0f - m_maxLineDescender + num25 + (num8 + m_lineSpacing + m_paragraphSpacing + m_lineSpacingDelta) * num;
						m_lineOffset += num9;
					}
					else
					{
						m_lineOffset += m_lineHeight + (m_lineSpacing + m_paragraphSpacing) * num;
					}
					m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
					m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
					m_startOfLineAscender = num25;
					m_xAdvance = 0f + tag_LineIndent + tag_Indent;
					num12 = m_characterCount - 1;
					m_characterCount++;
					continue;
				}
			}
			if (m_textInfo.characterInfo[m_characterCount].isVisible)
			{
				m_meshExtents.min.x = Mathf.Min(m_meshExtents.min.x, m_textInfo.characterInfo[m_characterCount].bottomLeft.x);
				m_meshExtents.min.y = Mathf.Min(m_meshExtents.min.y, m_textInfo.characterInfo[m_characterCount].bottomLeft.y);
				m_meshExtents.max.x = Mathf.Max(m_meshExtents.max.x, m_textInfo.characterInfo[m_characterCount].topRight.x);
				m_meshExtents.max.y = Mathf.Max(m_meshExtents.max.y, m_textInfo.characterInfo[m_characterCount].topRight.y);
			}
			if (m_overflowMode == TextOverflowModes.Page && num4 != 13 && num4 != 10)
			{
				if (m_pageNumber + 1 > m_textInfo.pageInfo.Length)
				{
					TMP_TextInfo.Resize(ref m_textInfo.pageInfo, m_pageNumber + 1, isBlockAllocated: true);
				}
				m_textInfo.pageInfo[m_pageNumber].ascender = num14;
				m_textInfo.pageInfo[m_pageNumber].descender = ((num26 < m_textInfo.pageInfo[m_pageNumber].descender) ? num26 : m_textInfo.pageInfo[m_pageNumber].descender);
				if (m_pageNumber == 0 && m_characterCount == 0)
				{
					m_textInfo.pageInfo[m_pageNumber].firstCharacterIndex = m_characterCount;
				}
				else if (m_characterCount > 0 && m_pageNumber != m_textInfo.characterInfo[m_characterCount - 1].pageNumber)
				{
					m_textInfo.pageInfo[m_pageNumber - 1].lastCharacterIndex = m_characterCount - 1;
					m_textInfo.pageInfo[m_pageNumber].firstCharacterIndex = m_characterCount;
				}
				else if (m_characterCount == totalCharacterCount - 1)
				{
					m_textInfo.pageInfo[m_pageNumber].lastCharacterIndex = m_characterCount;
				}
			}
			if (m_enableWordWrapping || m_overflowMode == TextOverflowModes.Truncate || m_overflowMode == TextOverflowModes.Ellipsis)
			{
				if ((char.IsWhiteSpace((char)num4) || num4 == 8203 || num4 == 45 || num4 == 173) && (!m_isNonBreakingSpace || flag7) && num4 != 160 && num4 != 8199 && num4 != 8209 && num4 != 8239 && num4 != 8288)
				{
					SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
					m_isCharacterWrappingEnabled = false;
					flag6 = false;
				}
				else if (((num4 > 4352 && num4 < 4607) || (num4 > 11904 && num4 < 40959) || (num4 > 43360 && num4 < 43391) || (num4 > 44032 && num4 < 55295) || (num4 > 63744 && num4 < 64255) || (num4 > 65072 && num4 < 65103) || (num4 > 65280 && num4 < 65519)) && !m_isNonBreakingSpace)
				{
					if (flag6 || flag8 || (!TMP_Settings.linebreakingRules.leadingCharacters.ContainsKey(num4) && m_characterCount < totalCharacterCount - 1 && !TMP_Settings.linebreakingRules.followingCharacters.ContainsKey(m_textInfo.characterInfo[m_characterCount + 1].character)))
					{
						SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
						m_isCharacterWrappingEnabled = false;
						flag6 = false;
					}
				}
				else if (flag6 || m_isCharacterWrappingEnabled || flag8)
				{
					SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
				}
			}
			m_characterCount++;
		}
		num3 = m_maxFontSize - m_minFontSize;
		if (!m_isCharacterWrappingEnabled && m_enableAutoSizing && num3 > 0.051f && m_fontSize < m_fontSizeMax)
		{
			m_minFontSize = m_fontSize;
			m_fontSize += Mathf.Max((m_maxFontSize - m_fontSize) / 2f, 0.05f);
			m_fontSize = (float)(int)(Mathf.Min(m_fontSize, m_fontSizeMax) * 20f + 0.5f) / 20f;
			if (loopCountA <= 20)
			{
				GenerateTextMesh();
			}
			return;
		}
		m_isCharacterWrappingEnabled = false;
		if (m_characterCount == 0)
		{
			ClearMesh();
			TMPro_EventManager.ON_TEXT_CHANGED(this);
			return;
		}
		int index = m_materialReferences[0].referenceCount * 4;
		m_textInfo.meshInfo[0].Clear(uploadChanges: false);
		Vector3 vector10 = Vector3.zero;
		Vector3[] rectTransformCorners = m_RectTransformCorners;
		switch (m_textAlignment)
		{
		case TextAlignmentOptions.TopLeft:
		case TextAlignmentOptions.Top:
		case TextAlignmentOptions.TopRight:
		case TextAlignmentOptions.TopJustified:
		case TextAlignmentOptions.TopFlush:
		case TextAlignmentOptions.TopGeoAligned:
			vector10 = ((m_overflowMode == TextOverflowModes.Page) ? (rectTransformCorners[1] + new Vector3(0f + vector2.x, 0f - m_textInfo.pageInfo[num10].ascender - vector2.y, 0f)) : (rectTransformCorners[1] + new Vector3(0f + vector2.x, 0f - m_maxAscender - vector2.y, 0f)));
			break;
		case TextAlignmentOptions.Left:
		case TextAlignmentOptions.Center:
		case TextAlignmentOptions.Right:
		case TextAlignmentOptions.Justified:
		case TextAlignmentOptions.Flush:
		case TextAlignmentOptions.CenterGeoAligned:
			vector10 = ((m_overflowMode == TextOverflowModes.Page) ? ((rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + vector2.x, 0f - (m_textInfo.pageInfo[num10].ascender + vector2.y + m_textInfo.pageInfo[num10].descender - vector2.w) / 2f, 0f)) : ((rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + vector2.x, 0f - (m_maxAscender + vector2.y + num15 - vector2.w) / 2f, 0f)));
			break;
		case TextAlignmentOptions.BottomLeft:
		case TextAlignmentOptions.Bottom:
		case TextAlignmentOptions.BottomRight:
		case TextAlignmentOptions.BottomJustified:
		case TextAlignmentOptions.BottomFlush:
		case TextAlignmentOptions.BottomGeoAligned:
			vector10 = ((m_overflowMode == TextOverflowModes.Page) ? (rectTransformCorners[0] + new Vector3(0f + vector2.x, 0f - m_textInfo.pageInfo[num10].descender + vector2.w, 0f)) : (rectTransformCorners[0] + new Vector3(0f + vector2.x, 0f - num15 + vector2.w, 0f)));
			break;
		case TextAlignmentOptions.BaselineLeft:
		case TextAlignmentOptions.Baseline:
		case TextAlignmentOptions.BaselineRight:
		case TextAlignmentOptions.BaselineJustified:
		case TextAlignmentOptions.BaselineFlush:
		case TextAlignmentOptions.BaselineGeoAligned:
			vector10 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + vector2.x, 0f, 0f);
			break;
		case TextAlignmentOptions.MidlineLeft:
		case TextAlignmentOptions.Midline:
		case TextAlignmentOptions.MidlineRight:
		case TextAlignmentOptions.MidlineJustified:
		case TextAlignmentOptions.MidlineFlush:
		case TextAlignmentOptions.MidlineGeoAligned:
			vector10 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + vector2.x, 0f - (m_meshExtents.max.y + vector2.y + m_meshExtents.min.y - vector2.w) / 2f, 0f);
			break;
		case TextAlignmentOptions.CaplineLeft:
		case TextAlignmentOptions.Capline:
		case TextAlignmentOptions.CaplineRight:
		case TextAlignmentOptions.CaplineJustified:
		case TextAlignmentOptions.CaplineFlush:
		case TextAlignmentOptions.CaplineGeoAligned:
			vector10 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + vector2.x, 0f - (m_maxCapHeight - vector2.y - vector2.w) / 2f, 0f);
			break;
		}
		Vector3 vector11 = Vector3.zero;
		Vector3 zero3 = Vector3.zero;
		int num41 = 0;
		int lineCount = 0;
		int num42 = 0;
		bool flag10 = false;
		bool flag11 = false;
		int num43 = 0;
		int num44 = 0;
		bool flag12 = !(m_canvas.worldCamera == null);
		float f = (m_previousLossyScaleY = base.transform.lossyScale.y);
		RenderMode renderMode = m_canvas.renderMode;
		float scaleFactor = m_canvas.scaleFactor;
		Color32 color = Color.white;
		Color32 underlineColor = Color.white;
		Color32 color2 = new Color32(byte.MaxValue, byte.MaxValue, 0, 64);
		float num45 = 0f;
		float num46 = 0f;
		float num47 = 0f;
		float num48 = 0f;
		float num49 = 0f;
		float num50 = TMP_Text.k_LargePositiveFloat;
		int num51 = 0;
		float num52 = 0f;
		float num53 = 0f;
		float b = 0f;
		TMP_CharacterInfo[] characterInfo = m_textInfo.characterInfo;
		for (int j = 0; j < m_characterCount; j++)
		{
			TMP_FontAsset fontAsset = characterInfo[j].fontAsset;
			char character = characterInfo[j].character;
			int lineNumber = characterInfo[j].lineNumber;
			TMP_LineInfo tMP_LineInfo = m_textInfo.lineInfo[lineNumber];
			lineCount = lineNumber + 1;
			TextAlignmentOptions textAlignmentOptions = tMP_LineInfo.alignment;
			switch (textAlignmentOptions)
			{
			case TextAlignmentOptions.TopLeft:
			case TextAlignmentOptions.Left:
			case TextAlignmentOptions.BottomLeft:
			case TextAlignmentOptions.BaselineLeft:
			case TextAlignmentOptions.MidlineLeft:
			case TextAlignmentOptions.CaplineLeft:
				vector11 = (m_isRightToLeft ? new Vector3(0f - tMP_LineInfo.maxAdvance, 0f, 0f) : new Vector3(0f + tMP_LineInfo.marginLeft, 0f, 0f));
				break;
			case TextAlignmentOptions.Top:
			case TextAlignmentOptions.Center:
			case TextAlignmentOptions.Bottom:
			case TextAlignmentOptions.Baseline:
			case TextAlignmentOptions.Midline:
			case TextAlignmentOptions.Capline:
				vector11 = new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width / 2f - tMP_LineInfo.maxAdvance / 2f, 0f, 0f);
				break;
			case TextAlignmentOptions.TopGeoAligned:
			case TextAlignmentOptions.CenterGeoAligned:
			case TextAlignmentOptions.BottomGeoAligned:
			case TextAlignmentOptions.BaselineGeoAligned:
			case TextAlignmentOptions.MidlineGeoAligned:
			case TextAlignmentOptions.CaplineGeoAligned:
				vector11 = new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width / 2f - (tMP_LineInfo.lineExtents.min.x + tMP_LineInfo.lineExtents.max.x) / 2f, 0f, 0f);
				break;
			case TextAlignmentOptions.TopRight:
			case TextAlignmentOptions.Right:
			case TextAlignmentOptions.BottomRight:
			case TextAlignmentOptions.BaselineRight:
			case TextAlignmentOptions.MidlineRight:
			case TextAlignmentOptions.CaplineRight:
				vector11 = (m_isRightToLeft ? new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width, 0f, 0f) : new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width - tMP_LineInfo.maxAdvance, 0f, 0f));
				break;
			case TextAlignmentOptions.TopJustified:
			case TextAlignmentOptions.TopFlush:
			case TextAlignmentOptions.Justified:
			case TextAlignmentOptions.Flush:
			case TextAlignmentOptions.BottomJustified:
			case TextAlignmentOptions.BottomFlush:
			case TextAlignmentOptions.BaselineJustified:
			case TextAlignmentOptions.BaselineFlush:
			case TextAlignmentOptions.MidlineJustified:
			case TextAlignmentOptions.MidlineFlush:
			case TextAlignmentOptions.CaplineJustified:
			case TextAlignmentOptions.CaplineFlush:
			{
				if (character == '\u00ad' || character == '\u200b' || character == '\u2060')
				{
					break;
				}
				char character2 = characterInfo[tMP_LineInfo.lastCharacterIndex].character;
				bool flag13 = (textAlignmentOptions & (TextAlignmentOptions)16) == (TextAlignmentOptions)16;
				if ((!char.IsControl(character2) && lineNumber < m_lineNumber) || flag13 || tMP_LineInfo.maxAdvance > tMP_LineInfo.width)
				{
					if (lineNumber != num42 || j == 0 || j == m_firstVisibleCharacter)
					{
						vector11 = (m_isRightToLeft ? new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width, 0f, 0f) : new Vector3(tMP_LineInfo.marginLeft, 0f, 0f));
						flag10 = (char.IsSeparator(character) ? true : false);
						break;
					}
					float num54 = ((!m_isRightToLeft) ? (tMP_LineInfo.width - tMP_LineInfo.maxAdvance) : (tMP_LineInfo.width + tMP_LineInfo.maxAdvance));
					int num55 = tMP_LineInfo.visibleCharacterCount - 1 + tMP_LineInfo.controlCharacterCount;
					int num56 = (characterInfo[tMP_LineInfo.lastCharacterIndex].isVisible ? tMP_LineInfo.spaceCount : (tMP_LineInfo.spaceCount - 1)) - tMP_LineInfo.controlCharacterCount;
					if (flag10)
					{
						num56--;
						num55++;
					}
					float num57 = ((num56 > 0) ? m_wordWrappingRatios : 1f);
					if (num56 < 1)
					{
						num56 = 1;
					}
					if (character != '\u00a0' && (character == '\t' || char.IsSeparator(character)))
					{
						if (!m_isRightToLeft)
						{
							vector11 += new Vector3(num54 * (1f - num57) / (float)num56, 0f, 0f);
						}
						else
						{
							vector11 -= new Vector3(num54 * (1f - num57) / (float)num56, 0f, 0f);
						}
					}
					else if (!m_isRightToLeft)
					{
						vector11 += new Vector3(num54 * num57 / (float)num55, 0f, 0f);
					}
					else
					{
						vector11 -= new Vector3(num54 * num57 / (float)num55, 0f, 0f);
					}
				}
				else
				{
					vector11 = (m_isRightToLeft ? new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width, 0f, 0f) : new Vector3(tMP_LineInfo.marginLeft, 0f, 0f));
				}
				break;
			}
			}
			zero3 = vector10 + vector11;
			if (characterInfo[j].isVisible)
			{
				TMP_TextElementType elementType = characterInfo[j].elementType;
				if (elementType == TMP_TextElementType.Character)
				{
					Extents lineExtents = tMP_LineInfo.lineExtents;
					float num58 = m_uvLineOffset * (float)lineNumber % 1f;
					switch (m_horizontalMapping)
					{
					case TextureMappingOptions.Character:
						characterInfo[j].vertex_BL.uv2.x = 0f;
						characterInfo[j].vertex_TL.uv2.x = 0f;
						characterInfo[j].vertex_TR.uv2.x = 1f;
						characterInfo[j].vertex_BR.uv2.x = 1f;
						break;
					case TextureMappingOptions.Line:
						if (m_textAlignment != TextAlignmentOptions.Justified)
						{
							characterInfo[j].vertex_BL.uv2.x = (characterInfo[j].vertex_BL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num58;
							characterInfo[j].vertex_TL.uv2.x = (characterInfo[j].vertex_TL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num58;
							characterInfo[j].vertex_TR.uv2.x = (characterInfo[j].vertex_TR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num58;
							characterInfo[j].vertex_BR.uv2.x = (characterInfo[j].vertex_BR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num58;
						}
						else
						{
							characterInfo[j].vertex_BL.uv2.x = (characterInfo[j].vertex_BL.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
							characterInfo[j].vertex_TL.uv2.x = (characterInfo[j].vertex_TL.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
							characterInfo[j].vertex_TR.uv2.x = (characterInfo[j].vertex_TR.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
							characterInfo[j].vertex_BR.uv2.x = (characterInfo[j].vertex_BR.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
						}
						break;
					case TextureMappingOptions.Paragraph:
						characterInfo[j].vertex_BL.uv2.x = (characterInfo[j].vertex_BL.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
						characterInfo[j].vertex_TL.uv2.x = (characterInfo[j].vertex_TL.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
						characterInfo[j].vertex_TR.uv2.x = (characterInfo[j].vertex_TR.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
						characterInfo[j].vertex_BR.uv2.x = (characterInfo[j].vertex_BR.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num58;
						break;
					case TextureMappingOptions.MatchAspect:
					{
						switch (m_verticalMapping)
						{
						case TextureMappingOptions.Character:
							characterInfo[j].vertex_BL.uv2.y = 0f;
							characterInfo[j].vertex_TL.uv2.y = 1f;
							characterInfo[j].vertex_TR.uv2.y = 0f;
							characterInfo[j].vertex_BR.uv2.y = 1f;
							break;
						case TextureMappingOptions.Line:
							characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num58;
							characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num58;
							characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
							characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
							break;
						case TextureMappingOptions.Paragraph:
							characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y) + num58;
							characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y) + num58;
							characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
							characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
							break;
						case TextureMappingOptions.MatchAspect:
							UnityEngine.Debug.Log("ERROR: Cannot Match both Vertical & Horizontal.");
							break;
						}
						float num59 = (1f - (characterInfo[j].vertex_BL.uv2.y + characterInfo[j].vertex_TL.uv2.y) * characterInfo[j].aspectRatio) / 2f;
						characterInfo[j].vertex_BL.uv2.x = characterInfo[j].vertex_BL.uv2.y * characterInfo[j].aspectRatio + num59 + num58;
						characterInfo[j].vertex_TL.uv2.x = characterInfo[j].vertex_BL.uv2.x;
						characterInfo[j].vertex_TR.uv2.x = characterInfo[j].vertex_TL.uv2.y * characterInfo[j].aspectRatio + num59 + num58;
						characterInfo[j].vertex_BR.uv2.x = characterInfo[j].vertex_TR.uv2.x;
						break;
					}
					}
					switch (m_verticalMapping)
					{
					case TextureMappingOptions.Character:
						characterInfo[j].vertex_BL.uv2.y = 0f;
						characterInfo[j].vertex_TL.uv2.y = 1f;
						characterInfo[j].vertex_TR.uv2.y = 1f;
						characterInfo[j].vertex_BR.uv2.y = 0f;
						break;
					case TextureMappingOptions.Line:
						characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - tMP_LineInfo.descender) / (tMP_LineInfo.ascender - tMP_LineInfo.descender);
						characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - tMP_LineInfo.descender) / (tMP_LineInfo.ascender - tMP_LineInfo.descender);
						characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
						characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
						break;
					case TextureMappingOptions.Paragraph:
						characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y);
						characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y);
						characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
						characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
						break;
					case TextureMappingOptions.MatchAspect:
					{
						float num60 = (1f - (characterInfo[j].vertex_BL.uv2.x + characterInfo[j].vertex_TR.uv2.x) / characterInfo[j].aspectRatio) / 2f;
						characterInfo[j].vertex_BL.uv2.y = num60 + characterInfo[j].vertex_BL.uv2.x / characterInfo[j].aspectRatio;
						characterInfo[j].vertex_TL.uv2.y = num60 + characterInfo[j].vertex_TR.uv2.x / characterInfo[j].aspectRatio;
						characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
						characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
						break;
					}
					}
					num45 = characterInfo[j].scale * (1f - m_charWidthAdjDelta);
					if (!characterInfo[j].isUsingAlternateTypeface && (characterInfo[j].style & FontStyles.Bold) == FontStyles.Bold)
					{
						num45 *= -1f;
					}
					switch (renderMode)
					{
					case RenderMode.ScreenSpaceOverlay:
						num45 *= Mathf.Abs(f) / scaleFactor;
						break;
					case RenderMode.ScreenSpaceCamera:
						num45 *= (flag12 ? Mathf.Abs(f) : 1f);
						break;
					case RenderMode.WorldSpace:
						num45 *= Mathf.Abs(f);
						break;
					}
					float x = characterInfo[j].vertex_BL.uv2.x;
					float y = characterInfo[j].vertex_BL.uv2.y;
					float x2 = characterInfo[j].vertex_TR.uv2.x;
					float y2 = characterInfo[j].vertex_TR.uv2.y;
					float num61 = (int)x;
					float num62 = (int)y;
					x -= num61;
					x2 -= num61;
					y -= num62;
					y2 -= num62;
					characterInfo[j].vertex_BL.uv2.x = PackUV(x, y);
					characterInfo[j].vertex_BL.uv2.y = num45;
					characterInfo[j].vertex_TL.uv2.x = PackUV(x, y2);
					characterInfo[j].vertex_TL.uv2.y = num45;
					characterInfo[j].vertex_TR.uv2.x = PackUV(x2, y2);
					characterInfo[j].vertex_TR.uv2.y = num45;
					characterInfo[j].vertex_BR.uv2.x = PackUV(x2, y);
					characterInfo[j].vertex_BR.uv2.y = num45;
				}
				if (j < m_maxVisibleCharacters && num41 < m_maxVisibleWords && lineNumber < m_maxVisibleLines && m_overflowMode != TextOverflowModes.Page)
				{
					characterInfo[j].vertex_BL.position += zero3;
					characterInfo[j].vertex_TL.position += zero3;
					characterInfo[j].vertex_TR.position += zero3;
					characterInfo[j].vertex_BR.position += zero3;
				}
				else if (j < m_maxVisibleCharacters && num41 < m_maxVisibleWords && lineNumber < m_maxVisibleLines && m_overflowMode == TextOverflowModes.Page && characterInfo[j].pageNumber == num10)
				{
					characterInfo[j].vertex_BL.position += zero3;
					characterInfo[j].vertex_TL.position += zero3;
					characterInfo[j].vertex_TR.position += zero3;
					characterInfo[j].vertex_BR.position += zero3;
				}
				else
				{
					characterInfo[j].vertex_BL.position = Vector3.zero;
					characterInfo[j].vertex_TL.position = Vector3.zero;
					characterInfo[j].vertex_TR.position = Vector3.zero;
					characterInfo[j].vertex_BR.position = Vector3.zero;
					characterInfo[j].isVisible = false;
				}
				if (elementType == TMP_TextElementType.Character)
				{
					FillCharacterVertexBuffers(j);
				}
			}
			m_textInfo.characterInfo[j].bottomLeft += zero3;
			m_textInfo.characterInfo[j].topLeft += zero3;
			m_textInfo.characterInfo[j].topRight += zero3;
			m_textInfo.characterInfo[j].bottomRight += zero3;
			m_textInfo.characterInfo[j].origin += zero3.x;
			m_textInfo.characterInfo[j].xAdvance += zero3.x;
			m_textInfo.characterInfo[j].ascender += zero3.y;
			m_textInfo.characterInfo[j].descender += zero3.y;
			m_textInfo.characterInfo[j].baseLine += zero3.y;
			if (lineNumber != num42 || j == m_characterCount - 1)
			{
				if (lineNumber != num42)
				{
					m_textInfo.lineInfo[num42].baseline += zero3.y;
					m_textInfo.lineInfo[num42].ascender += zero3.y;
					m_textInfo.lineInfo[num42].descender += zero3.y;
					m_textInfo.lineInfo[num42].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[num42].firstCharacterIndex].bottomLeft.x, m_textInfo.lineInfo[num42].descender);
					m_textInfo.lineInfo[num42].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[num42].lastVisibleCharacterIndex].topRight.x, m_textInfo.lineInfo[num42].ascender);
				}
				if (j == m_characterCount - 1)
				{
					m_textInfo.lineInfo[lineNumber].baseline += zero3.y;
					m_textInfo.lineInfo[lineNumber].ascender += zero3.y;
					m_textInfo.lineInfo[lineNumber].descender += zero3.y;
					m_textInfo.lineInfo[lineNumber].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[lineNumber].firstCharacterIndex].bottomLeft.x, m_textInfo.lineInfo[lineNumber].descender);
					m_textInfo.lineInfo[lineNumber].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[lineNumber].lastVisibleCharacterIndex].topRight.x, m_textInfo.lineInfo[lineNumber].ascender);
				}
			}
			if (char.IsLetterOrDigit(character) || character == '-' || character == '\u00ad' || character == '‐' || character == '‑')
			{
				if (!flag11)
				{
					flag11 = true;
					num43 = j;
				}
				if (flag11 && j == m_characterCount - 1)
				{
					int num63 = m_textInfo.wordInfo.Length;
					int wordCount = m_textInfo.wordCount;
					if (m_textInfo.wordCount + 1 > num63)
					{
						TMP_TextInfo.Resize(ref m_textInfo.wordInfo, num63 + 1);
					}
					num44 = j;
					m_textInfo.wordInfo[wordCount].firstCharacterIndex = num43;
					m_textInfo.wordInfo[wordCount].lastCharacterIndex = num44;
					m_textInfo.wordInfo[wordCount].characterCount = num44 - num43 + 1;
					m_textInfo.wordInfo[wordCount].textComponent = this;
					num41++;
					m_textInfo.wordCount++;
					m_textInfo.lineInfo[lineNumber].wordCount++;
				}
			}
			else if ((flag11 || (j == 0 && (!char.IsPunctuation(character) || char.IsWhiteSpace(character) || character == '\u200b' || j == m_characterCount - 1))) && (j <= 0 || j >= characterInfo.Length - 1 || j >= m_characterCount || (character != '\'' && character != '’') || !char.IsLetterOrDigit(characterInfo[j - 1].character) || !char.IsLetterOrDigit(characterInfo[j + 1].character)))
			{
				num44 = ((j == m_characterCount - 1 && char.IsLetterOrDigit(character)) ? j : (j - 1));
				flag11 = false;
				int num64 = m_textInfo.wordInfo.Length;
				int wordCount2 = m_textInfo.wordCount;
				if (m_textInfo.wordCount + 1 > num64)
				{
					TMP_TextInfo.Resize(ref m_textInfo.wordInfo, num64 + 1);
				}
				m_textInfo.wordInfo[wordCount2].firstCharacterIndex = num43;
				m_textInfo.wordInfo[wordCount2].lastCharacterIndex = num44;
				m_textInfo.wordInfo[wordCount2].characterCount = num44 - num43 + 1;
				m_textInfo.wordInfo[wordCount2].textComponent = this;
				num41++;
				m_textInfo.wordCount++;
				m_textInfo.lineInfo[lineNumber].wordCount++;
			}
			if ((m_textInfo.characterInfo[j].style & FontStyles.Underline) == FontStyles.Underline)
			{
				bool flag14 = true;
				int pageNumber = m_textInfo.characterInfo[j].pageNumber;
				if (j > m_maxVisibleCharacters || lineNumber > m_maxVisibleLines || (m_overflowMode == TextOverflowModes.Page && pageNumber + 1 != m_pageToDisplay))
				{
					flag14 = false;
				}
				if (!char.IsWhiteSpace(character) && character != '\u200b')
				{
					num49 = Mathf.Max(num49, m_textInfo.characterInfo[j].scale);
					num46 = Mathf.Max(num46, Mathf.Abs(num45));
					num50 = Mathf.Min((pageNumber == num51) ? num50 : TMP_Text.k_LargePositiveFloat, m_textInfo.characterInfo[j].baseLine + base.font.faceInfo.underlineOffset * num49);
					num51 = pageNumber;
				}
				if (!flag && flag14 && j <= tMP_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\r' && (j != tMP_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
				{
					flag = true;
					num47 = m_textInfo.characterInfo[j].scale;
					if (num49 == 0f)
					{
						num49 = num47;
						num46 = num45;
					}
					start = new Vector3(m_textInfo.characterInfo[j].bottomLeft.x, num50, 0f);
					color = m_textInfo.characterInfo[j].underlineColor;
				}
				if (flag && m_characterCount == 1)
				{
					flag = false;
					zero = new Vector3(m_textInfo.characterInfo[j].topRight.x, num50, 0f);
					num48 = m_textInfo.characterInfo[j].scale;
					DrawUnderlineMesh(start, zero, ref index, num47, num48, num49, num46, color);
					num49 = 0f;
					num46 = 0f;
					num50 = TMP_Text.k_LargePositiveFloat;
				}
				else if (flag && (j == tMP_LineInfo.lastCharacterIndex || j >= tMP_LineInfo.lastVisibleCharacterIndex))
				{
					if (char.IsWhiteSpace(character) || character == '\u200b')
					{
						int lastVisibleCharacterIndex = tMP_LineInfo.lastVisibleCharacterIndex;
						zero = new Vector3(m_textInfo.characterInfo[lastVisibleCharacterIndex].topRight.x, num50, 0f);
						num48 = m_textInfo.characterInfo[lastVisibleCharacterIndex].scale;
					}
					else
					{
						zero = new Vector3(m_textInfo.characterInfo[j].topRight.x, num50, 0f);
						num48 = m_textInfo.characterInfo[j].scale;
					}
					flag = false;
					DrawUnderlineMesh(start, zero, ref index, num47, num48, num49, num46, color);
					num49 = 0f;
					num46 = 0f;
					num50 = TMP_Text.k_LargePositiveFloat;
				}
				else if (flag && !flag14)
				{
					flag = false;
					zero = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, num50, 0f);
					num48 = m_textInfo.characterInfo[j - 1].scale;
					DrawUnderlineMesh(start, zero, ref index, num47, num48, num49, num46, color);
					num49 = 0f;
					num46 = 0f;
					num50 = TMP_Text.k_LargePositiveFloat;
				}
				else if (flag && j < m_characterCount - 1 && !color.Compare(m_textInfo.characterInfo[j + 1].underlineColor))
				{
					flag = false;
					zero = new Vector3(m_textInfo.characterInfo[j].topRight.x, num50, 0f);
					num48 = m_textInfo.characterInfo[j].scale;
					DrawUnderlineMesh(start, zero, ref index, num47, num48, num49, num46, color);
					num49 = 0f;
					num46 = 0f;
					num50 = TMP_Text.k_LargePositiveFloat;
				}
			}
			else if (flag)
			{
				flag = false;
				zero = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, num50, 0f);
				num48 = m_textInfo.characterInfo[j - 1].scale;
				DrawUnderlineMesh(start, zero, ref index, num47, num48, num49, num46, color);
				num49 = 0f;
				num46 = 0f;
				num50 = TMP_Text.k_LargePositiveFloat;
			}
			bool num65 = (m_textInfo.characterInfo[j].style & FontStyles.Strikethrough) == FontStyles.Strikethrough;
			float strikethroughOffset = fontAsset.faceInfo.strikethroughOffset;
			if (num65)
			{
				bool flag15 = true;
				if (j > m_maxVisibleCharacters || lineNumber > m_maxVisibleLines || (m_overflowMode == TextOverflowModes.Page && m_textInfo.characterInfo[j].pageNumber + 1 != m_pageToDisplay))
				{
					flag15 = false;
				}
				if (!flag2 && flag15 && j <= tMP_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\r' && (j != tMP_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
				{
					flag2 = true;
					num52 = m_textInfo.characterInfo[j].pointSize;
					num53 = m_textInfo.characterInfo[j].scale;
					start2 = new Vector3(m_textInfo.characterInfo[j].bottomLeft.x, m_textInfo.characterInfo[j].baseLine + strikethroughOffset * num53, 0f);
					underlineColor = m_textInfo.characterInfo[j].strikethroughColor;
					b = m_textInfo.characterInfo[j].baseLine;
				}
				if (flag2 && m_characterCount == 1)
				{
					flag2 = false;
					zero2 = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + strikethroughOffset * num53, 0f);
					DrawUnderlineMesh(start2, zero2, ref index, num53, num53, num53, num45, underlineColor);
				}
				else if (flag2 && j == tMP_LineInfo.lastCharacterIndex)
				{
					if (char.IsWhiteSpace(character) || character == '\u200b')
					{
						int lastVisibleCharacterIndex2 = tMP_LineInfo.lastVisibleCharacterIndex;
						zero2 = new Vector3(m_textInfo.characterInfo[lastVisibleCharacterIndex2].topRight.x, m_textInfo.characterInfo[lastVisibleCharacterIndex2].baseLine + strikethroughOffset * num53, 0f);
					}
					else
					{
						zero2 = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + strikethroughOffset * num53, 0f);
					}
					flag2 = false;
					DrawUnderlineMesh(start2, zero2, ref index, num53, num53, num53, num45, underlineColor);
				}
				else if (flag2 && j < m_characterCount && (m_textInfo.characterInfo[j + 1].pointSize != num52 || !TMP_Math.Approximately(m_textInfo.characterInfo[j + 1].baseLine + zero3.y, b)))
				{
					flag2 = false;
					int lastVisibleCharacterIndex3 = tMP_LineInfo.lastVisibleCharacterIndex;
					zero2 = ((j <= lastVisibleCharacterIndex3) ? new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + strikethroughOffset * num53, 0f) : new Vector3(m_textInfo.characterInfo[lastVisibleCharacterIndex3].topRight.x, m_textInfo.characterInfo[lastVisibleCharacterIndex3].baseLine + strikethroughOffset * num53, 0f));
					DrawUnderlineMesh(start2, zero2, ref index, num53, num53, num53, num45, underlineColor);
				}
				else if (flag2 && j < m_characterCount && fontAsset.GetInstanceID() != characterInfo[j + 1].fontAsset.GetInstanceID())
				{
					flag2 = false;
					zero2 = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + strikethroughOffset * num53, 0f);
					DrawUnderlineMesh(start2, zero2, ref index, num53, num53, num53, num45, underlineColor);
				}
				else if (flag2 && !flag15)
				{
					flag2 = false;
					zero2 = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, m_textInfo.characterInfo[j - 1].baseLine + strikethroughOffset * num53, 0f);
					DrawUnderlineMesh(start2, zero2, ref index, num53, num53, num53, num45, underlineColor);
				}
			}
			else if (flag2)
			{
				flag2 = false;
				zero2 = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, m_textInfo.characterInfo[j - 1].baseLine + strikethroughOffset * num53, 0f);
				DrawUnderlineMesh(start2, zero2, ref index, num53, num53, num53, num45, underlineColor);
			}
			if ((m_textInfo.characterInfo[j].style & FontStyles.Highlight) == FontStyles.Highlight)
			{
				bool flag16 = true;
				int pageNumber2 = m_textInfo.characterInfo[j].pageNumber;
				if (j > m_maxVisibleCharacters || lineNumber > m_maxVisibleLines || (m_overflowMode == TextOverflowModes.Page && pageNumber2 + 1 != m_pageToDisplay))
				{
					flag16 = false;
				}
				if (!flag3 && flag16 && j <= tMP_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\r' && (j != tMP_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
				{
					flag3 = true;
					start3 = TMP_Text.k_LargePositiveVector2;
					vector = TMP_Text.k_LargeNegativeVector2;
					color2 = m_textInfo.characterInfo[j].highlightColor;
				}
				if (flag3)
				{
					Color32 highlightColor = m_textInfo.characterInfo[j].highlightColor;
					bool flag17 = false;
					if (!color2.Compare(highlightColor))
					{
						vector.x = (vector.x + m_textInfo.characterInfo[j].bottomLeft.x) / 2f;
						start3.y = Mathf.Min(start3.y, m_textInfo.characterInfo[j].descender);
						vector.y = Mathf.Max(vector.y, m_textInfo.characterInfo[j].ascender);
						DrawTextHighlight(start3, vector, ref index, color2);
						flag3 = true;
						start3 = vector;
						vector = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].descender, 0f);
						color2 = m_textInfo.characterInfo[j].highlightColor;
						flag17 = true;
					}
					if (!flag17)
					{
						start3.x = Mathf.Min(start3.x, m_textInfo.characterInfo[j].bottomLeft.x);
						start3.y = Mathf.Min(start3.y, m_textInfo.characterInfo[j].descender);
						vector.x = Mathf.Max(vector.x, m_textInfo.characterInfo[j].topRight.x);
						vector.y = Mathf.Max(vector.y, m_textInfo.characterInfo[j].ascender);
					}
				}
				if (flag3 && m_characterCount == 1)
				{
					flag3 = false;
					DrawTextHighlight(start3, vector, ref index, color2);
				}
				else if (flag3 && (j == tMP_LineInfo.lastCharacterIndex || j >= tMP_LineInfo.lastVisibleCharacterIndex))
				{
					flag3 = false;
					DrawTextHighlight(start3, vector, ref index, color2);
				}
				else if (flag3 && !flag16)
				{
					flag3 = false;
					DrawTextHighlight(start3, vector, ref index, color2);
				}
			}
			else if (flag3)
			{
				flag3 = false;
				DrawTextHighlight(start3, vector, ref index, color2);
			}
			num42 = lineNumber;
		}
		m_textInfo.characterCount = m_characterCount;
		m_textInfo.spriteCount = m_spriteCount;
		m_textInfo.lineCount = lineCount;
		m_textInfo.wordCount = ((num41 == 0 || m_characterCount <= 0) ? 1 : num41);
		m_textInfo.pageCount = m_pageNumber + 1;
		if (m_renderMode == TextRenderFlags.Render && IsActive())
		{
			OnPreRenderText?.Invoke(m_textInfo);
			if (m_canvas.additionalShaderChannels != (AdditionalCanvasShaderChannels.TexCoord1 | AdditionalCanvasShaderChannels.Normal | AdditionalCanvasShaderChannels.Tangent))
			{
				m_canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1 | AdditionalCanvasShaderChannels.Normal | AdditionalCanvasShaderChannels.Tangent;
			}
			if (m_geometrySortingOrder != VertexSortingOrder.Normal)
			{
				m_textInfo.meshInfo[0].SortGeometry(VertexSortingOrder.Reverse);
			}
			m_mesh.MarkDynamic();
			m_mesh.vertices = m_textInfo.meshInfo[0].vertices;
			m_mesh.uv = m_textInfo.meshInfo[0].uvs0;
			m_mesh.uv2 = m_textInfo.meshInfo[0].uvs2;
			m_mesh.colors32 = m_textInfo.meshInfo[0].colors32;
			m_mesh.RecalculateBounds();
			m_canvasRenderer.SetMesh(m_mesh);
			Color color3 = m_canvasRenderer.GetColor();
			for (int k = 1; k < m_textInfo.materialCount; k++)
			{
				m_textInfo.meshInfo[k].ClearUnusedVertices();
				if (!(m_subTextObjects[k] == null))
				{
					if (m_geometrySortingOrder != VertexSortingOrder.Normal)
					{
						m_textInfo.meshInfo[k].SortGeometry(VertexSortingOrder.Reverse);
					}
					m_subTextObjects[k].mesh.vertices = m_textInfo.meshInfo[k].vertices;
					m_subTextObjects[k].mesh.uv = m_textInfo.meshInfo[k].uvs0;
					m_subTextObjects[k].mesh.uv2 = m_textInfo.meshInfo[k].uvs2;
					m_subTextObjects[k].mesh.colors32 = m_textInfo.meshInfo[k].colors32;
					m_subTextObjects[k].mesh.RecalculateBounds();
					m_subTextObjects[k].canvasRenderer.SetMesh(m_subTextObjects[k].mesh);
					m_subTextObjects[k].canvasRenderer.SetColor(color3);
				}
			}
		}
		TMPro_EventManager.ON_TEXT_CHANGED(this);
	}

	protected override Vector3[] GetTextContainerLocalCorners()
	{
		if (m_rectTransform == null)
		{
			m_rectTransform = base.rectTransform;
		}
		m_rectTransform.GetLocalCorners(m_RectTransformCorners);
		return m_RectTransformCorners;
	}

	protected override void SetActiveSubMeshes(bool state)
	{
		for (int i = 1; i < m_subTextObjects.Length && m_subTextObjects[i] != null; i++)
		{
			if (m_subTextObjects[i].enabled != state)
			{
				m_subTextObjects[i].enabled = state;
			}
		}
	}

	protected override Bounds GetCompoundBounds()
	{
		Bounds bounds = m_mesh.bounds;
		Vector3 min = bounds.min;
		Vector3 max = bounds.max;
		for (int i = 1; i < m_subTextObjects.Length && m_subTextObjects[i] != null; i++)
		{
			Bounds bounds2 = m_subTextObjects[i].mesh.bounds;
			min.x = ((min.x < bounds2.min.x) ? min.x : bounds2.min.x);
			min.y = ((min.y < bounds2.min.y) ? min.y : bounds2.min.y);
			max.x = ((max.x > bounds2.max.x) ? max.x : bounds2.max.x);
			max.y = ((max.y > bounds2.max.y) ? max.y : bounds2.max.y);
		}
		Vector3 center = (min + max) / 2f;
		Vector2 vector = max - min;
		return new Bounds(center, vector);
	}

	private void UpdateSDFScale(float scaleDelta)
	{
		if (scaleDelta == 0f || scaleDelta == float.PositiveInfinity)
		{
			m_havePropertiesChanged = true;
			OnPreRenderCanvas();
			return;
		}
		for (int i = 0; i < m_textInfo.materialCount; i++)
		{
			TMP_MeshInfo tMP_MeshInfo = m_textInfo.meshInfo[i];
			if (tMP_MeshInfo.uvs2 != null)
			{
				for (int j = 0; j < tMP_MeshInfo.uvs2.Length; j++)
				{
					tMP_MeshInfo.uvs2[j].y *= Mathf.Abs(scaleDelta);
				}
			}
		}
		for (int k = 0; k < m_textInfo.materialCount; k++)
		{
			if (m_textInfo.meshInfo[k].uvs2 != null)
			{
				if (k == 0)
				{
					m_mesh.uv2 = m_textInfo.meshInfo[0].uvs2;
					m_canvasRenderer.SetMesh(m_mesh);
				}
				else
				{
					m_subTextObjects[k].mesh.uv2 = m_textInfo.meshInfo[k].uvs2;
					m_subTextObjects[k].canvasRenderer.SetMesh(m_subTextObjects[k].mesh);
				}
			}
		}
	}

	protected override void AdjustLineOffset(int startIndex, int endIndex, float offset)
	{
		Vector3 vector = new Vector3(0f, offset, 0f);
		for (int i = startIndex; i <= endIndex; i++)
		{
			m_textInfo.characterInfo[i].bottomLeft -= vector;
			m_textInfo.characterInfo[i].topLeft -= vector;
			m_textInfo.characterInfo[i].topRight -= vector;
			m_textInfo.characterInfo[i].bottomRight -= vector;
			m_textInfo.characterInfo[i].ascender -= vector.y;
			m_textInfo.characterInfo[i].baseLine -= vector.y;
			m_textInfo.characterInfo[i].descender -= vector.y;
			if (m_textInfo.characterInfo[i].isVisible)
			{
				m_textInfo.characterInfo[i].vertex_BL.position -= vector;
				m_textInfo.characterInfo[i].vertex_TL.position -= vector;
				m_textInfo.characterInfo[i].vertex_TR.position -= vector;
				m_textInfo.characterInfo[i].vertex_BR.position -= vector;
			}
		}
	}
}
public enum Compute_DistanceTransform_EventTypes
{
	Processing,
	Completed
}
public static class TMPro_EventManager
{
	public static readonly FastAction<object, Compute_DT_EventArgs> COMPUTE_DT_EVENT = new FastAction<object, Compute_DT_EventArgs>();

	public static readonly FastAction<bool, Material> MATERIAL_PROPERTY_EVENT = new FastAction<bool, Material>();

	public static readonly FastAction<bool, TMP_FontAsset> FONT_PROPERTY_EVENT = new FastAction<bool, TMP_FontAsset>();

	public static readonly FastAction<bool, UnityEngine.Object> SPRITE_ASSET_PROPERTY_EVENT = new FastAction<bool, UnityEngine.Object>();

	public static readonly FastAction<bool, TextMeshPro> TEXTMESHPRO_PROPERTY_EVENT = new FastAction<bool, TextMeshPro>();

	public static readonly FastAction<GameObject, Material, Material> DRAG_AND_DROP_MATERIAL_EVENT = new FastAction<GameObject, Material, Material>();

	public static readonly FastAction<bool> TEXT_STYLE_PROPERTY_EVENT = new FastAction<bool>();

	public static readonly FastAction<TMP_ColorGradient> COLOR_GRADIENT_PROPERTY_EVENT = new FastAction<TMP_ColorGradient>();

	public static readonly FastAction TMP_SETTINGS_PROPERTY_EVENT = new FastAction();

	public static readonly FastAction RESOURCE_LOAD_EVENT = new FastAction();

	public static readonly FastAction<bool, TextMeshProUGUI> TEXTMESHPRO_UGUI_PROPERTY_EVENT = new FastAction<bool, TextMeshProUGUI>();

	public static readonly FastAction OnPreRenderObject_Event = new FastAction();

	public static readonly FastAction<UnityEngine.Object> TEXT_CHANGED_EVENT = new FastAction<UnityEngine.Object>();

	public static void ON_PRE_RENDER_OBJECT_CHANGED()
	{
		OnPreRenderObject_Event.Call();
	}

	public static void ON_MATERIAL_PROPERTY_CHANGED(bool isChanged, Material mat)
	{
		MATERIAL_PROPERTY_EVENT.Call(isChanged, mat);
	}

	public static void ON_FONT_PROPERTY_CHANGED(bool isChanged, TMP_FontAsset font)
	{
		FONT_PROPERTY_EVENT.Call(isChanged, font);
	}

	public static void ON_SPRITE_ASSET_PROPERTY_CHANGED(bool isChanged, UnityEngine.Object obj)
	{
		SPRITE_ASSET_PROPERTY_EVENT.Call(isChanged, obj);
	}

	public static void ON_TEXTMESHPRO_PROPERTY_CHANGED(bool isChanged, TextMeshPro obj)
	{
		TEXTMESHPRO_PROPERTY_EVENT.Call(isChanged, obj);
	}

	public static void ON_DRAG_AND_DROP_MATERIAL_CHANGED(GameObject sender, Material currentMaterial, Material newMaterial)
	{
		DRAG_AND_DROP_MATERIAL_EVENT.Call(sender, currentMaterial, newMaterial);
	}

	public static void ON_TEXT_STYLE_PROPERTY_CHANGED(bool isChanged)
	{
		TEXT_STYLE_PROPERTY_EVENT.Call(isChanged);
	}

	public static void ON_COLOR_GRAIDENT_PROPERTY_CHANGED(TMP_ColorGradient gradient)
	{
		COLOR_GRADIENT_PROPERTY_EVENT.Call(gradient);
	}

	public static void ON_TEXT_CHANGED(UnityEngine.Object obj)
	{
		TEXT_CHANGED_EVENT.Call(obj);
	}

	public static void ON_TMP_SETTINGS_CHANGED()
	{
		TMP_SETTINGS_PROPERTY_EVENT.Call();
	}

	public static void ON_RESOURCES_LOADED()
	{
		RESOURCE_LOAD_EVENT.Call();
	}

	public static void ON_TEXTMESHPRO_UGUI_PROPERTY_CHANGED(bool isChanged, TextMeshProUGUI obj)
	{
		TEXTMESHPRO_UGUI_PROPERTY_EVENT.Call(isChanged, obj);
	}

	public static void ON_COMPUTE_DT_EVENT(object Sender, Compute_DT_EventArgs e)
	{
		COMPUTE_DT_EVENT.Call(Sender, e);
	}
}
public class Compute_DT_EventArgs
{
	public Compute_DistanceTransform_EventTypes EventType;

	public float ProgressPercentage;

	public Color[] Colors;

	public Compute_DT_EventArgs(Compute_DistanceTransform_EventTypes type, float progress)
	{
		EventType = type;
		ProgressPercentage = progress;
	}

	public Compute_DT_EventArgs(Compute_DistanceTransform_EventTypes type, Color[] colors)
	{
		EventType = type;
		Colors = colors;
	}
}
public static class TMPro_ExtensionMethods
{
	public static string ArrayToString(this char[] chars)
	{
		string text = string.Empty;
		for (int i = 0; i < chars.Length && chars[i] != 0; i++)
		{
			text += chars[i];
		}
		return text;
	}

	public static string IntToString(this int[] unicodes)
	{
		char[] array = new char[unicodes.Length];
		for (int i = 0; i < unicodes.Length; i++)
		{
			array[i] = (char)unicodes[i];
		}
		return new string(array);
	}

	public static string IntToString(this int[] unicodes, int start, int length)
	{
		if (start > unicodes.Length)
		{
			return string.Empty;
		}
		int num = Mathf.Min(start + length, unicodes.Length);
		char[] array = new char[num - start];
		int num2 = 0;
		for (int i = start; i < num; i++)
		{
			array[num2++] = (char)unicodes[i];
		}
		return new string(array);
	}

	public static int FindInstanceID<T>(this List<T> list, T target) where T : UnityEngine.Object
	{
		int instanceID = target.GetInstanceID();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].GetInstanceID() == instanceID)
			{
				return i;
			}
		}
		return -1;
	}

	public static bool Compare(this Color32 a, Color32 b)
	{
		if (a.r == b.r && a.g == b.g && a.b == b.b)
		{
			return a.a == b.a;
		}
		return false;
	}

	public static bool CompareRGB(this Color32 a, Color32 b)
	{
		if (a.r == b.r && a.g == b.g)
		{
			return a.b == b.b;
		}
		return false;
	}

	public static bool Compare(this Color a, Color b)
	{
		if (a.r == b.r && a.g == b.g && a.b == b.b)
		{
			return a.a == b.a;
		}
		return false;
	}

	public static bool CompareRGB(this Color a, Color b)
	{
		if (a.r == b.r && a.g == b.g)
		{
			return a.b == b.b;
		}
		return false;
	}

	public static Color32 Multiply(this Color32 c1, Color32 c2)
	{
		byte r = (byte)((float)(int)c1.r / 255f * ((float)(int)c2.r / 255f) * 255f);
		byte g = (byte)((float)(int)c1.g / 255f * ((float)(int)c2.g / 255f) * 255f);
		byte b = (byte)((float)(int)c1.b / 255f * ((float)(int)c2.b / 255f) * 255f);
		byte a = (byte)((float)(int)c1.a / 255f * ((float)(int)c2.a / 255f) * 255f);
		return new Color32(r, g, b, a);
	}

	public static Color32 Tint(this Color32 c1, Color32 c2)
	{
		byte r = (byte)((float)(int)c1.r / 255f * ((float)(int)c2.r / 255f) * 255f);
		byte g = (byte)((float)(int)c1.g / 255f * ((float)(int)c2.g / 255f) * 255f);
		byte b = (byte)((float)(int)c1.b / 255f * ((float)(int)c2.b / 255f) * 255f);
		byte a = (byte)((float)(int)c1.a / 255f * ((float)(int)c2.a / 255f) * 255f);
		return new Color32(r, g, b, a);
	}

	public static Color32 Tint(this Color32 c1, float tint)
	{
		byte r = (byte)Mathf.Clamp((float)(int)c1.r / 255f * tint * 255f, 0f, 255f);
		byte g = (byte)Mathf.Clamp((float)(int)c1.g / 255f * tint * 255f, 0f, 255f);
		byte b = (byte)Mathf.Clamp((float)(int)c1.b / 255f * tint * 255f, 0f, 255f);
		byte a = (byte)Mathf.Clamp((float)(int)c1.a / 255f * tint * 255f, 0f, 255f);
		return new Color32(r, g, b, a);
	}

	public static bool Compare(this Vector3 v1, Vector3 v2, int accuracy)
	{
		bool num = (int)(v1.x * (float)accuracy) == (int)(v2.x * (float)accuracy);
		bool flag = (int)(v1.y * (float)accuracy) == (int)(v2.y * (float)accuracy);
		bool flag2 = (int)(v1.z * (float)accuracy) == (int)(v2.z * (float)accuracy);
		return num && flag && flag2;
	}

	public static bool Compare(this Quaternion q1, Quaternion q2, int accuracy)
	{
		bool num = (int)(q1.x * (float)accuracy) == (int)(q2.x * (float)accuracy);
		bool flag = (int)(q1.y * (float)accuracy) == (int)(q2.y * (float)accuracy);
		bool flag2 = (int)(q1.z * (float)accuracy) == (int)(q2.z * (float)accuracy);
		bool flag3 = (int)(q1.w * (float)accuracy) == (int)(q2.w * (float)accuracy);
		return num && flag && flag2 && flag3;
	}
}
public static class TMP_Math
{
	public const float FLOAT_MAX = 32767f;

	public const float FLOAT_MIN = -32767f;

	public const int INT_MAX = int.MaxValue;

	public const int INT_MIN = -2147483647;

	public const float FLOAT_UNSET = -32767f;

	public const int INT_UNSET = -32767;

	public static Vector2 MAX_16BIT = new Vector2(32767f, 32767f);

	public static Vector2 MIN_16BIT = new Vector2(-32767f, -32767f);

	public static bool Approximately(float a, float b)
	{
		if (b - 0.0001f < a)
		{
			return a < b + 0.0001f;
		}
		return false;
	}
}
public enum TMP_VertexDataUpdateFlags
{
	None = 0,
	Vertices = 1,
	Uv0 = 2,
	Uv2 = 4,
	Uv4 = 8,
	Colors32 = 16,
	All = 255
}
[Serializable]
public struct VertexGradient
{
	public Color topLeft;

	public Color topRight;

	public Color bottomLeft;

	public Color bottomRight;

	public VertexGradient(Color color)
	{
		topLeft = color;
		topRight = color;
		bottomLeft = color;
		bottomRight = color;
	}

	public VertexGradient(Color color0, Color color1, Color color2, Color color3)
	{
		topLeft = color0;
		topRight = color1;
		bottomLeft = color2;
		bottomRight = color3;
	}
}
public struct TMP_PageInfo
{
	public int firstCharacterIndex;

	public int lastCharacterIndex;

	public float ascender;

	public float baseLine;

	public float descender;
}
public struct TMP_LinkInfo
{
	public TMP_Text textComponent;

	public int hashCode;

	public int linkIdFirstCharacterIndex;

	public int linkIdLength;

	public int linkTextfirstCharacterIndex;

	public int linkTextLength;

	internal char[] linkID;

	internal void SetLinkID(char[] text, int startIndex, int length)
	{
		if (linkID == null || linkID.Length < length)
		{
			linkID = new char[length];
		}
		for (int i = 0; i < length; i++)
		{
			linkID[i] = text[startIndex + i];
		}
	}

	public string GetLinkText()
	{
		string text = string.Empty;
		TMP_TextInfo textInfo = textComponent.textInfo;
		for (int i = linkTextfirstCharacterIndex; i < linkTextfirstCharacterIndex + linkTextLength; i++)
		{
			text += textInfo.characterInfo[i].character;
		}
		return text;
	}

	public string GetLinkID()
	{
		if (textComponent == null)
		{
			return string.Empty;
		}
		return new string(linkID, 0, linkIdLength);
	}
}
public struct TMP_WordInfo
{
	public TMP_Text textComponent;

	public int firstCharacterIndex;

	public int lastCharacterIndex;

	public int characterCount;

	public string GetWord()
	{
		string text = string.Empty;
		TMP_CharacterInfo[] characterInfo = textComponent.textInfo.characterInfo;
		for (int i = firstCharacterIndex; i < lastCharacterIndex + 1; i++)
		{
			text += characterInfo[i].character;
		}
		return text;
	}
}
public struct TMP_SpriteInfo
{
	public int spriteIndex;

	public int characterIndex;

	public int vertexIndex;
}
public struct Extents
{
	public Vector2 min;

	public Vector2 max;

	public Extents(Vector2 min, Vector2 max)
	{
		this.min = min;
		this.max = max;
	}

	public override string ToString()
	{
		return "Min (" + min.x.ToString("f2") + ", " + min.y.ToString("f2") + ")   Max (" + max.x.ToString("f2") + ", " + max.y.ToString("f2") + ")";
	}
}
[Serializable]
public struct Mesh_Extents
{
	public Vector2 min;

	public Vector2 max;

	public Mesh_Extents(Vector2 min, Vector2 max)
	{
		this.min = min;
		this.max = max;
	}

	public override string ToString()
	{
		return "Min (" + min.x.ToString("f2") + ", " + min.y.ToString("f2") + ")   Max (" + max.x.ToString("f2") + ", " + max.y.ToString("f2") + ")";
	}
}
public struct WordWrapState
{
	public int previous_WordBreak;

	public int total_CharacterCount;

	public int visible_CharacterCount;

	public int visible_SpriteCount;

	public int visible_LinkCount;

	public int firstCharacterIndex;

	public int firstVisibleCharacterIndex;

	public int lastCharacterIndex;

	public int lastVisibleCharIndex;

	public int lineNumber;

	public float maxCapHeight;

	public float maxAscender;

	public float maxDescender;

	public float maxLineAscender;

	public float maxLineDescender;

	public float previousLineAscender;

	public float xAdvance;

	public float preferredWidth;

	public float preferredHeight;

	public float previousLineScale;

	public int wordCount;

	public FontStyles fontStyle;

	public float fontScale;

	public float fontScaleMultiplier;

	public float currentFontSize;

	public float baselineOffset;

	public float lineOffset;

	public TMP_TextInfo textInfo;

	public TMP_LineInfo lineInfo;

	public Color32 vertexColor;

	public Color32 underlineColor;

	public Color32 strikethroughColor;

	public Color32 highlightColor;

	public TMP_FontStyleStack basicStyleStack;

	public TMP_RichTextTagStack<Color32> colorStack;

	public TMP_RichTextTagStack<Color32> underlineColorStack;

	public TMP_RichTextTagStack<Color32> strikethroughColorStack;

	public TMP_RichTextTagStack<Color32> highlightColorStack;

	public TMP_RichTextTagStack<TMP_ColorGradient> colorGradientStack;

	public TMP_RichTextTagStack<float> sizeStack;

	public TMP_RichTextTagStack<float> indentStack;

	public TMP_RichTextTagStack<FontWeight> fontWeightStack;

	public TMP_RichTextTagStack<int> styleStack;

	public TMP_RichTextTagStack<float> baselineStack;

	public TMP_RichTextTagStack<int> actionStack;

	public TMP_RichTextTagStack<MaterialReference> materialReferenceStack;

	public TMP_RichTextTagStack<TextAlignmentOptions> lineJustificationStack;

	public int spriteAnimationID;

	public TMP_FontAsset currentFontAsset;

	public Material currentMaterial;

	public int currentMaterialIndex;

	public Extents meshExtents;

	public bool tagNoParsing;

	public bool isNonBreakingSpace;
}
public struct TagAttribute
{
	public int startIndex;

	public int length;

	public int hashCode;
}
public struct RichTextTagAttribute
{
	public int nameHashCode;

	public int valueHashCode;

	public TagValueType valueType;

	public int valueStartIndex;

	public int valueLength;

	public TagUnitType unitType;
}
[Serializable]
public class TMP_Asset : ScriptableObject
{
	public int hashCode;

	public Material material;

	public int materialHashCode;
}
[Serializable]
public class TMP_Character : TMP_TextElement
{
	public TMP_Character()
	{
		m_ElementType = TextElementType.Character;
		base.scale = 1f;
	}

	public TMP_Character(uint unicode, Glyph glyph)
	{
		m_ElementType = TextElementType.Character;
		base.unicode = unicode;
		base.glyph = glyph;
		base.glyphIndex = glyph.index;
		base.scale = 1f;
	}

	internal TMP_Character(uint unicode, uint glyphIndex)
	{
		m_ElementType = TextElementType.Character;
		base.unicode = unicode;
		base.glyph = null;
		base.glyphIndex = glyphIndex;
		base.scale = 1f;
	}
}
public struct TMP_Vertex
{
	public Vector3 position;

	public Vector2 uv;

	public Vector2 uv2;

	public Vector2 uv4;

	public Color32 color;
}
public struct TMP_CharacterInfo
{
	public char character;

	public int index;

	public int stringLength;

	public TMP_TextElementType elementType;

	public TMP_TextElement textElement;

	public TMP_FontAsset fontAsset;

	public Material material;

	public int materialReferenceIndex;

	public bool isUsingAlternateTypeface;

	public float pointSize;

	public int lineNumber;

	public int pageNumber;

	public int vertexIndex;

	public TMP_Vertex vertex_BL;

	public TMP_Vertex vertex_TL;

	public TMP_Vertex vertex_TR;

	public TMP_Vertex vertex_BR;

	public Vector3 topLeft;

	public Vector3 bottomLeft;

	public Vector3 topRight;

	public Vector3 bottomRight;

	public float origin;

	public float ascender;

	public float baseLine;

	public float descender;

	public float xAdvance;

	public float aspectRatio;

	public float scale;

	public Color32 color;

	public Color32 underlineColor;

	public Color32 strikethroughColor;

	public Color32 highlightColor;

	public FontStyles style;

	public bool isVisible;
}
public enum ColorMode
{
	Single,
	HorizontalGradient,
	VerticalGradient,
	FourCornersGradient
}
[Serializable]
public class TMP_ColorGradient : ScriptableObject
{
	public ColorMode colorMode = ColorMode.FourCornersGradient;

	public Color topLeft;

	public Color topRight;

	public Color bottomLeft;

	public Color bottomRight;

	private const ColorMode k_DefaultColorMode = ColorMode.FourCornersGradient;

	private static readonly Color k_DefaultColor = Color.white;

	public TMP_ColorGradient()
	{
		colorMode = ColorMode.FourCornersGradient;
		topLeft = k_DefaultColor;
		topRight = k_DefaultColor;
		bottomLeft = k_DefaultColor;
		bottomRight = k_DefaultColor;
	}

	public TMP_ColorGradient(Color color)
	{
		colorMode = ColorMode.FourCornersGradient;
		topLeft = color;
		topRight = color;
		bottomLeft = color;
		bottomRight = color;
	}

	public TMP_ColorGradient(Color color0, Color color1, Color color2, Color color3)
	{
		colorMode = ColorMode.FourCornersGradient;
		topLeft = color0;
		topRight = color1;
		bottomLeft = color2;
		bottomRight = color3;
	}
}
internal interface ITweenValue
{
	bool ignoreTimeScale { get; }

	float duration { get; }

	void TweenValue(float floatPercentage);

	bool ValidTarget();
}
internal struct ColorTween : ITweenValue
{
	public enum ColorTweenMode
	{
		All,
		RGB,
		Alpha
	}

	public class ColorTweenCallback : UnityEvent<Color>
	{
	}

	private ColorTweenCallback m_Target;

	private Color m_StartColor;

	private Color m_TargetColor;

	private ColorTweenMode m_TweenMode;

	private float m_Duration;

	private bool m_IgnoreTimeScale;

	public Color startColor
	{
		get
		{
			return m_StartColor;
		}
		set
		{
			m_StartColor = value;
		}
	}

	public Color targetColor
	{
		get
		{
			return m_TargetColor;
		}
		set
		{
			m_TargetColor = value;
		}
	}

	public ColorTweenMode tweenMode
	{
		get
		{
			return m_TweenMode;
		}
		set
		{
			m_TweenMode = value;
		}
	}

	public float duration
	{
		get
		{
			return m_Duration;
		}
		set
		{
			m_Duration = value;
		}
	}

	public bool ignoreTimeScale
	{
		get
		{
			return m_IgnoreTimeScale;
		}
		set
		{
			m_IgnoreTimeScale = value;
		}
	}

	public void TweenValue(float floatPercentage)
	{
		if (ValidTarget())
		{
			Color arg = Color.Lerp(m_StartColor, m_TargetColor, floatPercentage);
			if (m_TweenMode == ColorTweenMode.Alpha)
			{
				arg.r = m_StartColor.r;
				arg.g = m_StartColor.g;
				arg.b = m_StartColor.b;
			}
			else if (m_TweenMode == ColorTweenMode.RGB)
			{
				arg.a = m_StartColor.a;
			}
			m_Target.Invoke(arg);
		}
	}

	public void AddOnChangedCallback(UnityAction<Color> callback)
	{
		if (m_Target == null)
		{
			m_Target = new ColorTweenCallback();
		}
		m_Target.AddListener(callback);
	}

	public bool GetIgnoreTimescale()
	{
		return m_IgnoreTimeScale;
	}

	public float GetDuration()
	{
		return m_Duration;
	}

	public bool ValidTarget()
	{
		return m_Target != null;
	}
}
internal struct FloatTween : ITweenValue
{
	public class FloatTweenCallback : UnityEvent<float>
	{
	}

	private FloatTweenCallback m_Target;

	private float m_StartValue;

	private float m_TargetValue;

	private float m_Duration;

	private bool m_IgnoreTimeScale;

	public float startValue
	{
		get
		{
			return m_StartValue;
		}
		set
		{
			m_StartValue = value;
		}
	}

	public float targetValue
	{
		get
		{
			return m_TargetValue;
		}
		set
		{
			m_TargetValue = value;
		}
	}

	public float duration
	{
		get
		{
			return m_Duration;
		}
		set
		{
			m_Duration = value;
		}
	}

	public bool ignoreTimeScale
	{
		get
		{
			return m_IgnoreTimeScale;
		}
		set
		{
			m_IgnoreTimeScale = value;
		}
	}

	public void TweenValue(float floatPercentage)
	{
		if (ValidTarget())
		{
			float arg = Mathf.Lerp(m_StartValue, m_TargetValue, floatPercentage);
			m_Target.Invoke(arg);
		}
	}

	public void AddOnChangedCallback(UnityAction<float> callback)
	{
		if (m_Target == null)
		{
			m_Target = new FloatTweenCallback();
		}
		m_Target.AddListener(callback);
	}

	public bool GetIgnoreTimescale()
	{
		return m_IgnoreTimeScale;
	}

	public float GetDuration()
	{
		return m_Duration;
	}

	public bool ValidTarget()
	{
		return m_Target != null;
	}
}
internal class TweenRunner<T> where T : struct, ITweenValue
{
	protected MonoBehaviour m_CoroutineContainer;

	protected IEnumerator m_Tween;

	private static IEnumerator Start(T tweenInfo)
	{
		if (tweenInfo.ValidTarget())
		{
			float elapsedTime = 0f;
			while (elapsedTime < tweenInfo.duration)
			{
				elapsedTime += (tweenInfo.ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime);
				float floatPercentage = Mathf.Clamp01(elapsedTime / tweenInfo.duration);
				tweenInfo.TweenValue(floatPercentage);
				yield return null;
			}
			tweenInfo.TweenValue(1f);
		}
	}

	public void Init(MonoBehaviour coroutineContainer)
	{
		m_CoroutineContainer = coroutineContainer;
	}

	public void StartTween(T info)
	{
		if (m_CoroutineContainer == null)
		{
			UnityEngine.Debug.LogWarning("Coroutine container not configured... did you forget to call Init?");
			return;
		}
		StopTween();
		if (!m_CoroutineContainer.gameObject.activeInHierarchy)
		{
			info.TweenValue(1f);
			return;
		}
		m_Tween = Start(info);
		m_CoroutineContainer.StartCoroutine(m_Tween);
	}

	public void StopTween()
	{
		if (m_Tween != null)
		{
			m_CoroutineContainer.StopCoroutine(m_Tween);
			m_Tween = null;
		}
	}
}
public static class TMP_DefaultControls
{
	public struct Resources
	{
		public Sprite standard;

		public Sprite background;

		public Sprite inputField;

		public Sprite knob;

		public Sprite checkmark;

		public Sprite dropdown;

		public Sprite mask;
	}

	private const float kWidth = 160f;

	private const float kThickHeight = 30f;

	private const float kThinHeight = 20f;

	private static Vector2 s_ThickElementSize = new Vector2(160f, 30f);

	private static Vector2 s_ThinElementSize = new Vector2(160f, 20f);

	private static Color s_DefaultSelectableColor = new Color(1f, 1f, 1f, 1f);

	private static Color s_TextColor = new Color(10f / 51f, 10f / 51f, 10f / 51f, 1f);

	private static GameObject CreateUIElementRoot(string name, Vector2 size)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.AddComponent<RectTransform>().sizeDelta = size;
		return gameObject;
	}

	private static GameObject CreateUIObject(string name, GameObject parent)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.AddComponent<RectTransform>();
		SetParentAndAlign(gameObject, parent);
		return gameObject;
	}

	private static void SetDefaultTextValues(TMP_Text lbl)
	{
		lbl.color = s_TextColor;
		lbl.fontSize = 14f;
	}

	private static void SetDefaultColorTransitionValues(Selectable slider)
	{
		ColorBlock colors = slider.colors;
		colors.highlightedColor = new Color(0.882f, 0.882f, 0.882f);
		colors.pressedColor = new Color(0.698f, 0.698f, 0.698f);
		colors.disabledColor = new Color(0.521f, 0.521f, 0.521f);
	}

	private static void SetParentAndAlign(GameObject child, GameObject parent)
	{
		if (!(parent == null))
		{
			child.transform.SetParent(parent.transform, worldPositionStays: false);
			SetLayerRecursively(child, parent.layer);
		}
	}

	private static void SetLayerRecursively(GameObject go, int layer)
	{
		go.layer = layer;
		Transform transform = go.transform;
		for (int i = 0; i < transform.childCount; i++)
		{
			SetLayerRecursively(transform.GetChild(i).gameObject, layer);
		}
	}

	public static GameObject CreateScrollbar(Resources resources)
	{
		GameObject gameObject = CreateUIElementRoot("Scrollbar", s_ThinElementSize);
		GameObject gameObject2 = CreateUIObject("Sliding Area", gameObject);
		GameObject gameObject3 = CreateUIObject("Handle", gameObject2);
		Image image = gameObject.AddComponent<Image>();
		image.sprite = resources.background;
		image.type = Image.Type.Sliced;
		image.color = s_DefaultSelectableColor;
		Image image2 = gameObject3.AddComponent<Image>();
		image2.sprite = resources.standard;
		image2.type = Image.Type.Sliced;
		image2.color = s_DefaultSelectableColor;
		RectTransform component = gameObject2.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(-20f, -20f);
		component.anchorMin = Vector2.zero;
		component.anchorMax = Vector2.one;
		RectTransform component2 = gameObject3.GetComponent<RectTransform>();
		component2.sizeDelta = new Vector2(20f, 20f);
		Scrollbar scrollbar = gameObject.AddComponent<Scrollbar>();
		scrollbar.handleRect = component2;
		scrollbar.targetGraphic = image2;
		SetDefaultColorTransitionValues(scrollbar);
		return gameObject;
	}

	public static GameObject CreateButton(Resources resources)
	{
		GameObject gameObject = CreateUIElementRoot("Button", s_ThickElementSize);
		GameObject gameObject2 = new GameObject("Text (TMP)");
		gameObject2.AddComponent<RectTransform>();
		SetParentAndAlign(gameObject2, gameObject);
		Image image = gameObject.AddComponent<Image>();
		image.sprite = resources.standard;
		image.type = Image.Type.Sliced;
		image.color = s_DefaultSelectableColor;
		SetDefaultColorTransitionValues(gameObject.AddComponent<Button>());
		TextMeshProUGUI textMeshProUGUI = gameObject2.AddComponent<TextMeshProUGUI>();
		textMeshProUGUI.text = "Button";
		textMeshProUGUI.alignment = TextAlignmentOptions.Center;
		SetDefaultTextValues(textMeshProUGUI);
		RectTransform component = gameObject2.GetComponent<RectTransform>();
		component.anchorMin = Vector2.zero;
		component.anchorMax = Vector2.one;
		component.sizeDelta = Vector2.zero;
		return gameObject;
	}

	public static GameObject CreateText(Resources resources)
	{
		GameObject gameObject = CreateUIElementRoot("Text (TMP)", s_ThickElementSize);
		TextMeshProUGUI textMeshProUGUI = gameObject.AddComponent<TextMeshProUGUI>();
		textMeshProUGUI.text = "New Text";
		SetDefaultTextValues(textMeshProUGUI);
		return gameObject;
	}

	public static GameObject CreateInputField(Resources resources)
	{
		GameObject gameObject = CreateUIElementRoot("InputField (TMP)", s_ThickElementSize);
		GameObject gameObject2 = CreateUIObject("Text Area", gameObject);
		GameObject gameObject3 = CreateUIObject("Placeholder", gameObject2);
		GameObject gameObject4 = CreateUIObject("Text", gameObject2);
		Image image = gameObject.AddComponent<Image>();
		image.sprite = resources.inputField;
		image.type = Image.Type.Sliced;
		image.color = s_DefaultSelectableColor;
		TMP_InputField tMP_InputField = gameObject.AddComponent<TMP_InputField>();
		SetDefaultColorTransitionValues(tMP_InputField);
		gameObject2.AddComponent<RectMask2D>();
		RectTransform component = gameObject2.GetComponent<RectTransform>();
		component.anchorMin = Vector2.zero;
		component.anchorMax = Vector2.one;
		component.sizeDelta = Vector2.zero;
		component.offsetMin = new Vector2(10f, 6f);
		component.offsetMax = new Vector2(-10f, -7f);
		TextMeshProUGUI textMeshProUGUI = gameObject4.AddComponent<TextMeshProUGUI>();
		textMeshProUGUI.text = "";
		textMeshProUGUI.enableWordWrapping = false;
		textMeshProUGUI.extraPadding = true;
		textMeshProUGUI.richText = true;
		SetDefaultTextValues(textMeshProUGUI);
		TextMeshProUGUI textMeshProUGUI2 = gameObject3.AddComponent<TextMeshProUGUI>();
		textMeshProUGUI2.text = "Enter text...";
		textMeshProUGUI2.fontSize = 14f;
		textMeshProUGUI2.fontStyle = FontStyles.Italic;
		textMeshProUGUI2.enableWordWrapping = false;
		textMeshProUGUI2.extraPadding = true;
		Color color = textMeshProUGUI.color;
		color.a *= 0.5f;
		textMeshProUGUI2.color = color;
		RectTransform component2 = gameObject4.GetComponent<RectTransform>();
		component2.anchorMin = Vector2.zero;
		component2.anchorMax = Vector2.one;
		component2.sizeDelta = Vector2.zero;
		component2.offsetMin = new Vector2(0f, 0f);
		component2.offsetMax = new Vector2(0f, 0f);
		RectTransform component3 = gameObject3.GetComponent<RectTransform>();
		component3.anchorMin = Vector2.zero;
		component3.anchorMax = Vector2.one;
		component3.sizeDelta = Vector2.zero;
		component3.offsetMin = new Vector2(0f, 0f);
		component3.offsetMax = new Vector2(0f, 0f);
		tMP_InputField.textViewport = component;
		tMP_InputField.textComponent = textMeshProUGUI;
		tMP_InputField.placeholder = textMeshProUGUI2;
		tMP_InputField.fontAsset = textMeshProUGUI.font;
		return gameObject;
	}
}
public enum AtlasPopulationMode
{
	Static,
	Dynamic
}
[Serializable]
public class TMP_FontAsset : TMP_Asset
{
	[SerializeField]
	private string m_Version;

	[SerializeField]
	internal string m_SourceFontFileGUID;

	[SerializeField]
	private Font m_SourceFontFile;

	[SerializeField]
	private AtlasPopulationMode m_AtlasPopulationMode;

	[SerializeField]
	private FaceInfo m_FaceInfo;

	[SerializeField]
	private List<Glyph> m_GlyphTable = new List<Glyph>();

	private Dictionary<uint, Glyph> m_GlyphLookupDictionary;

	[SerializeField]
	private List<TMP_Character> m_CharacterTable = new List<TMP_Character>();

	private Dictionary<uint, TMP_Character> m_CharacterLookupDictionary;

	private Texture2D m_AtlasTexture;

	[SerializeField]
	private Texture2D[] m_AtlasTextures;

	[SerializeField]
	internal int m_AtlasTextureIndex;

	[SerializeField]
	private List<GlyphRect> m_UsedGlyphRects;

	[SerializeField]
	private List<GlyphRect> m_FreeGlyphRects;

	[SerializeField]
	private FaceInfo_Legacy m_fontInfo;

	[SerializeField]
	public Texture2D atlas;

	[SerializeField]
	private int m_AtlasWidth;

	[SerializeField]
	private int m_AtlasHeight;

	[SerializeField]
	private int m_AtlasPadding;

	[SerializeField]
	private GlyphRenderMode m_AtlasRenderMode;

	[SerializeField]
	internal List<TMP_Glyph> m_glyphInfoList;

	[SerializeField]
	[FormerlySerializedAs("m_kerningInfo")]
	internal KerningTable m_KerningTable = new KerningTable();

	[SerializeField]
	private TMP_FontFeatureTable m_FontFeatureTable = new TMP_FontFeatureTable();

	[SerializeField]
	private List<TMP_FontAsset> fallbackFontAssets;

	[SerializeField]
	public List<TMP_FontAsset> m_FallbackFontAssetTable;

	[SerializeField]
	internal FontAssetCreationSettings m_CreationSettings;

	[SerializeField]
	private TMP_FontWeightPair[] m_FontWeightTable = new TMP_FontWeightPair[10];

	[SerializeField]
	private TMP_FontWeightPair[] fontWeights;

	public float normalStyle;

	public float normalSpacingOffset;

	public float boldStyle = 0.75f;

	public float boldSpacing = 7f;

	public byte italicStyle = 35;

	public byte tabSize = 10;

	private byte m_oldTabSize;

	internal bool m_IsFontAssetLookupTablesDirty;

	private List<Glyph> m_GlyphsToPack = new List<Glyph>();

	private List<Glyph> m_GlyphsPacked = new List<Glyph>();

	private List<Glyph> m_GlyphsToRender = new List<Glyph>();

	private List<uint> m_GlyphIndexList = new List<uint>();

	private List<TMP_Character> m_CharactersToAdd = new List<TMP_Character>();

	internal static uint[] s_GlyphIndexArray = new uint[16];

	internal static List<uint> s_MissingCharacterList = new List<uint>(16);

	public string version
	{
		get
		{
			return m_Version;
		}
		internal set
		{
			m_Version = value;
		}
	}

	public Font sourceFontFile
	{
		get
		{
			return m_SourceFontFile;
		}
		internal set
		{
			m_SourceFontFile = value;
		}
	}

	public AtlasPopulationMode atlasPopulationMode
	{
		get
		{
			return m_AtlasPopulationMode;
		}
		set
		{
			m_AtlasPopulationMode = value;
		}
	}

	public FaceInfo faceInfo
	{
		get
		{
			return m_FaceInfo;
		}
		internal set
		{
			m_FaceInfo = value;
		}
	}

	public List<Glyph> glyphTable
	{
		get
		{
			return m_GlyphTable;
		}
		internal set
		{
			m_GlyphTable = value;
		}
	}

	public Dictionary<uint, Glyph> glyphLookupTable
	{
		get
		{
			if (m_GlyphLookupDictionary == null)
			{
				ReadFontAssetDefinition();
			}
			return m_GlyphLookupDictionary;
		}
	}

	public List<TMP_Character> characterTable
	{
		get
		{
			return m_CharacterTable;
		}
		internal set
		{
			m_CharacterTable = value;
		}
	}

	public Dictionary<uint, TMP_Character> characterLookupTable
	{
		get
		{
			if (m_CharacterLookupDictionary == null)
			{
				ReadFontAssetDefinition();
			}
			return m_CharacterLookupDictionary;
		}
	}

	public Texture2D atlasTexture
	{
		get
		{
			if (m_AtlasTexture == null)
			{
				m_AtlasTexture = atlasTextures[0];
			}
			return m_AtlasTexture;
		}
	}

	public Texture2D[] atlasTextures
	{
		get
		{
			_ = m_AtlasTextures;
			return m_AtlasTextures;
		}
		set
		{
			m_AtlasTextures = value;
		}
	}

	internal List<GlyphRect> usedGlyphRects
	{
		get
		{
			return m_UsedGlyphRects;
		}
		set
		{
			m_UsedGlyphRects = value;
		}
	}

	internal List<GlyphRect> freeGlyphRects
	{
		get
		{
			return m_FreeGlyphRects;
		}
		set
		{
			m_FreeGlyphRects = value;
		}
	}

	[Obsolete("The fontInfo property and underlying type is now obsolete. Please use the faceInfo property and FaceInfo type instead.")]
	public FaceInfo_Legacy fontInfo => m_fontInfo;

	public int atlasWidth
	{
		get
		{
			return m_AtlasWidth;
		}
		internal set
		{
			m_AtlasWidth = value;
		}
	}

	public int atlasHeight
	{
		get
		{
			return m_AtlasHeight;
		}
		internal set
		{
			m_AtlasHeight = value;
		}
	}

	public int atlasPadding
	{
		get
		{
			return m_AtlasPadding;
		}
		internal set
		{
			m_AtlasPadding = value;
		}
	}

	public GlyphRenderMode atlasRenderMode
	{
		get
		{
			return m_AtlasRenderMode;
		}
		internal set
		{
			m_AtlasRenderMode = value;
		}
	}

	public TMP_FontFeatureTable fontFeatureTable
	{
		get
		{
			return m_FontFeatureTable;
		}
		internal set
		{
			m_FontFeatureTable = value;
		}
	}

	public List<TMP_FontAsset> fallbackFontAssetTable
	{
		get
		{
			return m_FallbackFontAssetTable;
		}
		set
		{
			m_FallbackFontAssetTable = value;
		}
	}

	public FontAssetCreationSettings creationSettings
	{
		get
		{
			return m_CreationSettings;
		}
		set
		{
			m_CreationSettings = value;
		}
	}

	public TMP_FontWeightPair[] fontWeightTable
	{
		get
		{
			return m_FontWeightTable;
		}
		internal set
		{
			m_FontWeightTable = value;
		}
	}

	public static TMP_FontAsset CreateFontAsset(Font font)
	{
		return CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024);
	}

	public static TMP_FontAsset CreateFontAsset(Font font, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.Dynamic)
	{
		TMP_FontAsset tMP_FontAsset = ScriptableObject.CreateInstance<TMP_FontAsset>();
		tMP_FontAsset.m_Version = "1.1.0";
		FontEngine.InitializeFontEngine();
		FontEngine.LoadFontFace(font, samplingPointSize);
		tMP_FontAsset.faceInfo = FontEngine.GetFaceInfo();
		if (atlasPopulationMode == AtlasPopulationMode.Dynamic)
		{
			tMP_FontAsset.sourceFontFile = font;
		}
		tMP_FontAsset.atlasPopulationMode = atlasPopulationMode;
		tMP_FontAsset.atlasWidth = atlasWidth;
		tMP_FontAsset.atlasHeight = atlasHeight;
		tMP_FontAsset.atlasPadding = atlasPadding;
		tMP_FontAsset.atlasRenderMode = renderMode;
		tMP_FontAsset.atlasTextures = new Texture2D[1];
		Texture2D texture2D = new Texture2D(0, 0, TextureFormat.Alpha8, mipChain: false);
		tMP_FontAsset.atlasTextures[0] = texture2D;
		int num;
		if ((renderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16)
		{
			num = 0;
			Material material = new Material(ShaderUtilities.ShaderRef_MobileBitmap);
			material.SetTexture(ShaderUtilities.ID_MainTex, texture2D);
			material.SetFloat(ShaderUtilities.ID_TextureWidth, atlasWidth);
			material.SetFloat(ShaderUtilities.ID_TextureHeight, atlasHeight);
			tMP_FontAsset.material = material;
		}
		else
		{
			num = 1;
			Material material2 = new Material(ShaderUtilities.ShaderRef_MobileSDF);
			material2.SetTexture(ShaderUtilities.ID_MainTex, texture2D);
			material2.SetFloat(ShaderUtilities.ID_TextureWidth, atlasWidth);
			material2.SetFloat(ShaderUtilities.ID_TextureHeight, atlasHeight);
			material2.SetFloat(ShaderUtilities.ID_GradientScale, atlasPadding + num);
			material2.SetFloat(ShaderUtilities.ID_WeightNormal, tMP_FontAsset.normalStyle);
			material2.SetFloat(ShaderUtilities.ID_WeightBold, tMP_FontAsset.boldStyle);
			tMP_FontAsset.material = material2;
		}
		tMP_FontAsset.freeGlyphRects = new List<GlyphRect>
		{
			new GlyphRect(0, 0, atlasWidth - num, atlasHeight - num)
		};
		tMP_FontAsset.usedGlyphRects = new List<GlyphRect>();
		tMP_FontAsset.ReadFontAssetDefinition();
		return tMP_FontAsset;
	}

	private void Awake()
	{
		if (material != null && string.IsNullOrEmpty(m_Version))
		{
			UpgradeFontAsset();
		}
	}

	internal void InitializeDictionaryLookupTables()
	{
		if (m_GlyphLookupDictionary == null)
		{
			m_GlyphLookupDictionary = new Dictionary<uint, Glyph>();
		}
		else
		{
			m_GlyphLookupDictionary.Clear();
		}
		int count = m_GlyphTable.Count;
		if (m_GlyphIndexList == null)
		{
			m_GlyphIndexList = new List<uint>();
		}
		else
		{
			m_GlyphIndexList.Clear();
		}
		for (int i = 0; i < count; i++)
		{
			Glyph glyph = m_GlyphTable[i];
			uint index = glyph.index;
			if (!m_GlyphLookupDictionary.ContainsKey(index))
			{
				m_GlyphLookupDictionary.Add(index, glyph);
				m_GlyphIndexList.Add(index);
			}
		}
		if (m_CharacterLookupDictionary == null)
		{
			m_CharacterLookupDictionary = new Dictionary<uint, TMP_Character>();
		}
		else
		{
			m_CharacterLookupDictionary.Clear();
		}
		for (int j = 0; j < m_CharacterTable.Count; j++)
		{
			TMP_Character tMP_Character = m_CharacterTable[j];
			uint unicode = tMP_Character.unicode;
			uint glyphIndex = tMP_Character.glyphIndex;
			if (!m_CharacterLookupDictionary.ContainsKey(unicode))
			{
				m_CharacterLookupDictionary.Add(unicode, tMP_Character);
			}
			if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				tMP_Character.glyph = m_GlyphLookupDictionary[glyphIndex];
			}
		}
		if (m_KerningTable != null && m_KerningTable.kerningPairs != null && m_KerningTable.kerningPairs.Count > 0)
		{
			UpgradeGlyphAdjustmentTableToFontFeatureTable();
		}
		if (m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary == null)
		{
			m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary = new Dictionary<long, TMP_GlyphPairAdjustmentRecord>();
		}
		else
		{
			m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.Clear();
		}
		List<TMP_GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords = m_FontFeatureTable.m_GlyphPairAdjustmentRecords;
		if (glyphPairAdjustmentRecords != null)
		{
			for (int k = 0; k < glyphPairAdjustmentRecords.Count; k++)
			{
				TMP_GlyphPairAdjustmentRecord tMP_GlyphPairAdjustmentRecord = glyphPairAdjustmentRecords[k];
				long key = new GlyphPairKey(tMP_GlyphPairAdjustmentRecord).key;
				m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryAdd(key, tMP_GlyphPairAdjustmentRecord);
			}
		}
	}

	public void ReadFontAssetDefinition()
	{
		if (material != null && string.IsNullOrEmpty(m_Version))
		{
			UpgradeFontAsset();
		}
		InitializeDictionaryLookupTables();
		if (!m_CharacterLookupDictionary.ContainsKey(9u))
		{
			Glyph glyph = new Glyph(0u, new GlyphMetrics(0f, 0f, 0f, 0f, m_FaceInfo.tabWidth * (float)(int)tabSize), GlyphRect.zero, 1f, 0);
			m_CharacterLookupDictionary.Add(9u, new TMP_Character(9u, glyph));
		}
		if (!m_CharacterLookupDictionary.ContainsKey(10u))
		{
			Glyph glyph2 = new Glyph(0u, new GlyphMetrics(10f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
			m_CharacterLookupDictionary.Add(10u, new TMP_Character(10u, glyph2));
			if (!m_CharacterLookupDictionary.ContainsKey(13u))
			{
				m_CharacterLookupDictionary.Add(13u, new TMP_Character(13u, glyph2));
			}
		}
		if (!m_CharacterLookupDictionary.ContainsKey(8203u))
		{
			Glyph glyph3 = new Glyph(0u, new GlyphMetrics(0f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
			m_CharacterLookupDictionary.Add(8203u, new TMP_Character(8203u, glyph3));
		}
		if (!m_CharacterLookupDictionary.ContainsKey(8288u))
		{
			Glyph glyph4 = new Glyph(0u, new GlyphMetrics(0f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
			m_CharacterLookupDictionary.Add(8288u, new TMP_Character(8288u, glyph4));
		}
		if (!m_CharacterLookupDictionary.ContainsKey(8209u) && m_CharacterLookupDictionary.TryGetValue(45u, out var value))
		{
			m_CharacterLookupDictionary.Add(8209u, new TMP_Character(8209u, value.glyph));
		}
		if (m_FaceInfo.capLine == 0f && m_CharacterLookupDictionary.ContainsKey(72u))
		{
			uint glyphIndex = m_CharacterLookupDictionary[72u].glyphIndex;
			m_FaceInfo.capLine = m_GlyphLookupDictionary[glyphIndex].metrics.horizontalBearingY;
		}
		if (m_FaceInfo.scale == 0f)
		{
			m_FaceInfo.scale = 1f;
		}
		if (m_FaceInfo.strikethroughOffset == 0f)
		{
			m_FaceInfo.strikethroughOffset = m_FaceInfo.capLine / 2.5f;
		}
		if (m_AtlasPadding == 0 && material.HasProperty(ShaderUtilities.ID_GradientScale))
		{
			m_AtlasPadding = (int)material.GetFloat(ShaderUtilities.ID_GradientScale) - 1;
		}
		hashCode = TMP_TextUtilities.GetSimpleHashCode(base.name);
		materialHashCode = TMP_TextUtilities.GetSimpleHashCode(material.name);
		m_IsFontAssetLookupTablesDirty = false;
	}

	internal void SortCharacterTable()
	{
		if (m_CharacterTable != null && m_CharacterTable.Count > 0)
		{
			m_CharacterTable = m_CharacterTable.OrderBy((TMP_Character c) => c.unicode).ToList();
		}
	}

	internal void SortGlyphTable()
	{
		if (m_GlyphTable != null && m_GlyphTable.Count > 0)
		{
			m_GlyphTable = m_GlyphTable.OrderBy((Glyph c) => c.index).ToList();
		}
	}

	internal void SortGlyphAndCharacterTables()
	{
		SortGlyphTable();
		SortCharacterTable();
	}

	public bool HasCharacter(int character)
	{
		if (m_CharacterLookupDictionary == null)
		{
			return false;
		}
		if (m_CharacterLookupDictionary.ContainsKey((uint)character))
		{
			return true;
		}
		return false;
	}

	public bool HasCharacter(char character)
	{
		if (m_CharacterLookupDictionary == null)
		{
			return false;
		}
		if (m_CharacterLookupDictionary.ContainsKey(character))
		{
			return true;
		}
		return false;
	}

	public bool HasCharacter(char character, bool searchFallbacks)
	{
		if (m_CharacterLookupDictionary == null)
		{
			ReadFontAssetDefinition();
			if (m_CharacterLookupDictionary == null)
			{
				return false;
			}
		}
		if (m_CharacterLookupDictionary.ContainsKey(character))
		{
			return true;
		}
		if (m_AtlasPopulationMode == AtlasPopulationMode.Dynamic && TryAddCharacterInternal(character, out var _))
		{
			return true;
		}
		if (searchFallbacks)
		{
			if (fallbackFontAssetTable != null && fallbackFontAssetTable.Count > 0)
			{
				for (int i = 0; i < fallbackFontAssetTable.Count && fallbackFontAssetTable[i] != null; i++)
				{
					if (fallbackFontAssetTable[i].HasCharacter_Internal(character, searchFallbacks))
					{
						return true;
					}
				}
			}
			if (TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
			{
				for (int j = 0; j < TMP_Settings.fallbackFontAssets.Count && TMP_Settings.fallbackFontAssets[j] != null; j++)
				{
					if (TMP_Settings.fallbackFontAssets[j].m_CharacterLookupDictionary == null)
					{
						TMP_Settings.fallbackFontAssets[j].ReadFontAssetDefinition();
					}
					if (TMP_Settings.fallbackFontAssets[j].m_CharacterLookupDictionary != null && TMP_Settings.fallbackFontAssets[j].HasCharacter_Internal(character, searchFallbacks))
					{
						return true;
					}
				}
			}
			if (TMP_Settings.defaultFontAsset != null)
			{
				if (TMP_Settings.defaultFontAsset.m_CharacterLookupDictionary == null)
				{
					TMP_Settings.defaultFontAsset.ReadFontAssetDefinition();
				}
				if (TMP_Settings.defaultFontAsset.m_CharacterLookupDictionary != null && TMP_Settings.defaultFontAsset.HasCharacter_Internal(character, searchFallbacks))
				{
					return true;
				}
			}
		}
		return false;
	}

	private bool HasCharacter_Internal(char character, bool searchFallbacks)
	{
		if (m_CharacterLookupDictionary == null)
		{
			ReadFontAssetDefinition();
			if (m_CharacterLookupDictionary == null)
			{
				return false;
			}
		}
		if (m_CharacterLookupDictionary.ContainsKey(character))
		{
			return true;
		}
		if (searchFallbacks && fallbackFontAssetTable != null && fallbackFontAssetTable.Count > 0)
		{
			for (int i = 0; i < fallbackFontAssetTable.Count && fallbackFontAssetTable[i] != null; i++)
			{
				if (fallbackFontAssetTable[i].HasCharacter_Internal(character, searchFallbacks))
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool HasCharacters(string text, out List<char> missingCharacters)
	{
		if (m_CharacterLookupDictionary == null)
		{
			missingCharacters = null;
			return false;
		}
		missingCharacters = new List<char>();
		for (int i = 0; i < text.Length; i++)
		{
			if (!m_CharacterLookupDictionary.ContainsKey(text[i]))
			{
				missingCharacters.Add(text[i]);
			}
		}
		if (missingCharacters.Count == 0)
		{
			return true;
		}
		return false;
	}

	public bool HasCharacters(string text)
	{
		if (m_CharacterLookupDictionary == null)
		{
			return false;
		}
		for (int i = 0; i < text.Length; i++)
		{
			if (!m_CharacterLookupDictionary.ContainsKey(text[i]))
			{
				return false;
			}
		}
		return true;
	}

	public static string GetCharacters(TMP_FontAsset fontAsset)
	{
		string text = string.Empty;
		for (int i = 0; i < fontAsset.characterTable.Count; i++)
		{
			text += (char)fontAsset.characterTable[i].unicode;
		}
		return text;
	}

	public static int[] GetCharactersArray(TMP_FontAsset fontAsset)
	{
		int[] array = new int[fontAsset.characterTable.Count];
		for (int i = 0; i < fontAsset.characterTable.Count; i++)
		{
			array[i] = (int)fontAsset.characterTable[i].unicode;
		}
		return array;
	}

	public bool TryAddCharacters(uint[] unicodes)
	{
		uint[] missingUnicodes;
		return TryAddCharacters(unicodes, out missingUnicodes);
	}

	public bool TryAddCharacters(uint[] unicodes, out uint[] missingUnicodes)
	{
		s_MissingCharacterList.Clear();
		if (unicodes == null || unicodes.Length == 0 || m_AtlasPopulationMode == AtlasPopulationMode.Static)
		{
			if (m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				UnityEngine.Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
			}
			else
			{
				UnityEngine.Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided Unicode list is Null or Empty.", this);
			}
			missingUnicodes = unicodes.ToArray();
			return false;
		}
		if (FontEngine.LoadFontFace(m_SourceFontFile, m_FaceInfo.pointSize) != FontEngineError.Success)
		{
			missingUnicodes = unicodes.ToArray();
			return false;
		}
		m_GlyphIndexList.Clear();
		m_CharactersToAdd.Clear();
		bool flag = false;
		int num = unicodes.Length;
		for (int i = 0; i < num; i++)
		{
			uint num2 = unicodes[i];
			if (m_CharacterLookupDictionary.ContainsKey(num2))
			{
				continue;
			}
			uint glyphIndex = FontEngine.GetGlyphIndex(num2);
			if (glyphIndex == 0)
			{
				flag = true;
				continue;
			}
			TMP_Character tMP_Character = new TMP_Character(num2, glyphIndex);
			if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				tMP_Character.glyph = m_GlyphLookupDictionary[glyphIndex];
				m_CharacterTable.Add(tMP_Character);
				m_CharacterLookupDictionary.Add(num2, tMP_Character);
			}
			else
			{
				m_GlyphIndexList.Add(glyphIndex);
				m_CharactersToAdd.Add(tMP_Character);
			}
		}
		if (m_GlyphIndexList.Count == 0)
		{
			missingUnicodes = unicodes.ToArray();
			return false;
		}
		if (m_AtlasTextures[m_AtlasTextureIndex].width == 0 || m_AtlasTextures[m_AtlasTextureIndex].height == 0)
		{
			m_AtlasTextures[m_AtlasTextureIndex].Resize(m_AtlasWidth, m_AtlasHeight);
			FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
		}
		Glyph[] glyphs;
		bool flag2 = FontEngine.TryAddGlyphsToTexture(m_GlyphIndexList, m_AtlasPadding, GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyphs);
		foreach (Glyph glyph in glyphs)
		{
			uint index = glyph.index;
			m_GlyphTable.Add(glyph);
			m_GlyphLookupDictionary.Add(index, glyph);
		}
		for (int k = 0; k < m_CharactersToAdd.Count; k++)
		{
			TMP_Character tMP_Character2 = m_CharactersToAdd[k];
			if (!m_GlyphLookupDictionary.TryGetValue(tMP_Character2.glyphIndex, out var value))
			{
				s_MissingCharacterList.Add(tMP_Character2.unicode);
				continue;
			}
			tMP_Character2.glyph = value;
			m_CharacterTable.Add(tMP_Character2);
			m_CharacterLookupDictionary.Add(tMP_Character2.unicode, tMP_Character2);
		}
		missingUnicodes = null;
		if (s_MissingCharacterList.Count > 0)
		{
			missingUnicodes = s_MissingCharacterList.ToArray();
		}
		if (flag2)
		{
			return !flag;
		}
		return false;
	}

	public bool TryAddCharacters(string characters)
	{
		string missingCharacters;
		return TryAddCharacters(characters, out missingCharacters);
	}

	public bool TryAddCharacters(string characters, out string missingCharacters)
	{
		if (string.IsNullOrEmpty(characters) || m_AtlasPopulationMode == AtlasPopulationMode.Static)
		{
			if (m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				UnityEngine.Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
			}
			else
			{
				UnityEngine.Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided character list is Null or Empty.", this);
			}
			missingCharacters = characters;
			return false;
		}
		if (FontEngine.LoadFontFace(m_SourceFontFile, m_FaceInfo.pointSize) != FontEngineError.Success)
		{
			missingCharacters = characters;
			return false;
		}
		m_GlyphIndexList.Clear();
		m_CharactersToAdd.Clear();
		bool flag = false;
		int length = characters.Length;
		for (int i = 0; i < length; i++)
		{
			uint num = characters[i];
			if (m_CharacterLookupDictionary.ContainsKey(num))
			{
				continue;
			}
			uint glyphIndex = FontEngine.GetGlyphIndex(num);
			if (glyphIndex == 0)
			{
				flag = true;
				continue;
			}
			TMP_Character tMP_Character = new TMP_Character(num, glyphIndex);
			if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				tMP_Character.glyph = m_GlyphLookupDictionary[glyphIndex];
				m_CharacterTable.Add(tMP_Character);
				m_CharacterLookupDictionary.Add(num, tMP_Character);
			}
			else
			{
				m_GlyphIndexList.Add(glyphIndex);
				m_CharactersToAdd.Add(tMP_Character);
			}
		}
		if (m_GlyphIndexList.Count == 0)
		{
			missingCharacters = characters;
			return false;
		}
		if (m_AtlasTextures[m_AtlasTextureIndex].width == 0 || m_AtlasTextures[m_AtlasTextureIndex].height == 0)
		{
			m_AtlasTextures[m_AtlasTextureIndex].Resize(m_AtlasWidth, m_AtlasHeight);
			FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
		}
		Glyph[] glyphs;
		bool flag2 = FontEngine.TryAddGlyphsToTexture(m_GlyphIndexList, m_AtlasPadding, GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyphs);
		foreach (Glyph glyph in glyphs)
		{
			uint index = glyph.index;
			m_GlyphTable.Add(glyph);
			m_GlyphLookupDictionary.Add(index, glyph);
		}
		missingCharacters = string.Empty;
		for (int k = 0; k < m_CharactersToAdd.Count; k++)
		{
			TMP_Character tMP_Character2 = m_CharactersToAdd[k];
			if (!m_GlyphLookupDictionary.TryGetValue(tMP_Character2.glyphIndex, out var value))
			{
				missingCharacters += (char)tMP_Character2.unicode;
				continue;
			}
			tMP_Character2.glyph = value;
			m_CharacterTable.Add(tMP_Character2);
			m_CharacterLookupDictionary.Add(tMP_Character2.unicode, tMP_Character2);
		}
		if (flag2)
		{
			return !flag;
		}
		return false;
	}

	internal bool TryAddCharacter_Internal(uint unicode)
	{
		TMP_Character tMP_Character = null;
		if (m_CharacterLookupDictionary.ContainsKey(unicode))
		{
			return true;
		}
		uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
		if (glyphIndex == 0)
		{
			return false;
		}
		if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
		{
			tMP_Character = new TMP_Character(unicode, m_GlyphLookupDictionary[glyphIndex]);
			m_CharacterTable.Add(tMP_Character);
			m_CharacterLookupDictionary.Add(unicode, tMP_Character);
			return true;
		}
		if (m_AtlasTextures[m_AtlasTextureIndex].width == 0 || m_AtlasTextures[m_AtlasTextureIndex].height == 0)
		{
			m_AtlasTextures[m_AtlasTextureIndex].Resize(m_AtlasWidth, m_AtlasHeight);
			FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
		}
		if (FontEngine.TryAddGlyphToTexture(glyphIndex, m_AtlasPadding, GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out var glyph))
		{
			m_GlyphTable.Add(glyph);
			m_GlyphLookupDictionary.Add(glyphIndex, glyph);
			tMP_Character = new TMP_Character(unicode, glyph);
			m_CharacterTable.Add(tMP_Character);
			m_CharacterLookupDictionary.Add(unicode, tMP_Character);
			return true;
		}
		return false;
	}

	internal TMP_Character AddCharacter_Internal(uint unicode, Glyph glyph)
	{
		if (m_CharacterLookupDictionary.ContainsKey(unicode))
		{
			return m_CharacterLookupDictionary[unicode];
		}
		uint index = glyph.index;
		if (m_AtlasTextures[m_AtlasTextureIndex].width == 0 || m_AtlasTextures[m_AtlasTextureIndex].height == 0)
		{
			m_AtlasTextures[m_AtlasTextureIndex].Resize(m_AtlasWidth, m_AtlasHeight);
			FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
		}
		if (!m_GlyphLookupDictionary.ContainsKey(index))
		{
			if (glyph.glyphRect.width == 0 || glyph.glyphRect.width == 0)
			{
				m_GlyphTable.Add(glyph);
			}
			else
			{
				if (!FontEngine.TryPackGlyphInAtlas(glyph, m_AtlasPadding, GlyphPackingMode.ContactPointRule, m_AtlasRenderMode, m_AtlasWidth, m_AtlasHeight, m_FreeGlyphRects, m_UsedGlyphRects))
				{
					return null;
				}
				m_GlyphsToRender.Add(glyph);
			}
		}
		TMP_Character tMP_Character = new TMP_Character(unicode, glyph);
		m_CharacterTable.Add(tMP_Character);
		m_CharacterLookupDictionary.Add(unicode, tMP_Character);
		UpdateAtlasTexture();
		return tMP_Character;
	}

	internal bool TryAddCharacterInternal(uint unicode, out TMP_Character character)
	{
		character = null;
		if (FontEngine.LoadFontFace(sourceFontFile, m_FaceInfo.pointSize) != FontEngineError.Success)
		{
			return false;
		}
		uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
		if (glyphIndex == 0)
		{
			return false;
		}
		if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
		{
			character = new TMP_Character(unicode, m_GlyphLookupDictionary[glyphIndex]);
			m_CharacterTable.Add(character);
			m_CharacterLookupDictionary.Add(unicode, character);
			if (TMP_Settings.getFontFeaturesAtRuntime)
			{
				UpdateGlyphAdjustmentRecords(unicode, glyphIndex);
			}
			return true;
		}
		if (m_AtlasTextures[m_AtlasTextureIndex].width == 0 || m_AtlasTextures[m_AtlasTextureIndex].height == 0)
		{
			if (!m_AtlasTextures[m_AtlasTextureIndex].isReadable)
			{
				UnityEngine.Debug.LogWarning("Unable to add the requested character to font asset [" + base.name + "]'s atlas texture. Please make the texture [" + m_AtlasTextures[m_AtlasTextureIndex].name + "] readable.", m_AtlasTextures[m_AtlasTextureIndex]);
				return false;
			}
			m_AtlasTextures[m_AtlasTextureIndex].Resize(m_AtlasWidth, m_AtlasHeight);
			FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
		}
		if (FontEngine.TryAddGlyphToTexture(glyphIndex, m_AtlasPadding, GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out var glyph))
		{
			m_GlyphTable.Add(glyph);
			m_GlyphLookupDictionary.Add(glyphIndex, glyph);
			character = new TMP_Character(unicode, glyph);
			m_CharacterTable.Add(character);
			m_CharacterLookupDictionary.Add(unicode, character);
			m_GlyphIndexList.Add(glyphIndex);
			if (TMP_Settings.getFontFeaturesAtRuntime)
			{
				UpdateGlyphAdjustmentRecords(unicode, glyphIndex);
			}
			return true;
		}
		return false;
	}

	internal uint GetGlyphIndex(uint unicode)
	{
		if (FontEngine.LoadFontFace(sourceFontFile, m_FaceInfo.pointSize) != FontEngineError.Success)
		{
			return 0u;
		}
		return FontEngine.GetGlyphIndex(unicode);
	}

	internal void UpdateAtlasTexture()
	{
		if (m_GlyphsToRender.Count != 0)
		{
			if (m_AtlasTextures[m_AtlasTextureIndex].width == 0 || m_AtlasTextures[m_AtlasTextureIndex].height == 0)
			{
				m_AtlasTextures[m_AtlasTextureIndex].Resize(m_AtlasWidth, m_AtlasHeight);
				FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
			}
			FontEngine.RenderGlyphsToTexture(m_GlyphsToRender, m_AtlasPadding, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex]);
			m_AtlasTextures[m_AtlasTextureIndex].Apply(updateMipmaps: false, makeNoLongerReadable: false);
			for (int i = 0; i < m_GlyphsToRender.Count; i++)
			{
				Glyph glyph = m_GlyphsToRender[i];
				glyph.atlasIndex = m_AtlasTextureIndex;
				m_GlyphTable.Add(glyph);
				m_GlyphLookupDictionary.Add(glyph.index, glyph);
			}
			m_GlyphsPacked.Clear();
			m_GlyphsToRender.Clear();
			_ = m_GlyphsToPack.Count;
			_ = 0;
		}
	}

	internal void UpdateGlyphAdjustmentRecords(uint unicode, uint glyphIndex)
	{
		int count = m_GlyphIndexList.Count;
		if (s_GlyphIndexArray.Length <= count)
		{
			s_GlyphIndexArray = new uint[Mathf.NextPowerOfTwo(count + 1)];
		}
		for (int i = 0; i < count; i++)
		{
			s_GlyphIndexArray[i] = m_GlyphIndexList[i];
		}
		Array.Clear(s_GlyphIndexArray, count, s_GlyphIndexArray.Length - count);
		GlyphPairAdjustmentRecord[] glyphPairAdjustmentTable = FontEngine.GetGlyphPairAdjustmentTable(s_GlyphIndexArray);
		if (glyphPairAdjustmentTable == null || glyphPairAdjustmentTable.Length == 0)
		{
			return;
		}
		if (m_FontFeatureTable == null)
		{
			m_FontFeatureTable = new TMP_FontFeatureTable();
		}
		for (int j = 0; j < glyphPairAdjustmentTable.Length && glyphPairAdjustmentTable[j].firstAdjustmentRecord.glyphIndex != 0; j++)
		{
			long key = (long)(((ulong)glyphPairAdjustmentTable[j].secondAdjustmentRecord.glyphIndex << 32) | glyphPairAdjustmentTable[j].firstAdjustmentRecord.glyphIndex);
			if (!m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.ContainsKey(key))
			{
				TMP_GlyphPairAdjustmentRecord tMP_GlyphPairAdjustmentRecord = new TMP_GlyphPairAdjustmentRecord(glyphPairAdjustmentTable[j]);
				m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(tMP_GlyphPairAdjustmentRecord);
				m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.Add(key, tMP_GlyphPairAdjustmentRecord);
			}
		}
	}

	public void ClearFontAssetData(bool setAtlasSizeToZero = false)
	{
		if (m_GlyphTable != null)
		{
			m_GlyphTable.Clear();
		}
		if (m_CharacterTable != null)
		{
			m_CharacterTable.Clear();
		}
		if (m_UsedGlyphRects != null)
		{
			m_UsedGlyphRects.Clear();
		}
		if (m_FreeGlyphRects != null)
		{
			int num = (((m_AtlasRenderMode & (GlyphRenderMode)16) != (GlyphRenderMode)16) ? 1 : 0);
			m_FreeGlyphRects = new List<GlyphRect>
			{
				new GlyphRect(0, 0, m_AtlasWidth - num, m_AtlasHeight - num)
			};
		}
		if (m_GlyphsToPack != null)
		{
			m_GlyphsToPack.Clear();
		}
		if (m_GlyphsPacked != null)
		{
			m_GlyphsPacked.Clear();
		}
		if (m_FontFeatureTable != null && m_FontFeatureTable.m_GlyphPairAdjustmentRecords != null)
		{
			m_FontFeatureTable.glyphPairAdjustmentRecords.Clear();
		}
		m_AtlasTextureIndex = 0;
		if (m_AtlasTextures != null)
		{
			for (int i = 0; i < m_AtlasTextures.Length; i++)
			{
				Texture2D texture2D = m_AtlasTextures[i];
				if (i > 0)
				{
					UnityEngine.Object.DestroyImmediate(texture2D, allowDestroyingAssets: true);
				}
				if (texture2D == null)
				{
					continue;
				}
				if (!m_AtlasTextures[i].isReadable)
				{
					UnityEngine.Debug.LogWarning("Unable to reset font asset [" + base.name + "]'s atlas texture. Please make the texture [" + m_AtlasTextures[i].name + "] readable.", m_AtlasTextures[i]);
					continue;
				}
				if (setAtlasSizeToZero)
				{
					texture2D.Resize(0, 0, TextureFormat.Alpha8, hasMipMap: false);
				}
				else if (texture2D.width != m_AtlasWidth || texture2D.height != m_AtlasHeight)
				{
					texture2D.Resize(m_AtlasWidth, m_AtlasHeight, TextureFormat.Alpha8, hasMipMap: false);
				}
				FontEngine.ResetAtlasTexture(texture2D);
				texture2D.Apply();
				if (i == 0)
				{
					m_AtlasTexture = texture2D;
				}
				m_AtlasTextures[i] = texture2D;
			}
		}
		ReadFontAssetDefinition();
	}

	private void UpgradeFontAsset()
	{
		m_Version = "1.1.0";
		UnityEngine.Debug.Log("Upgrading font asset [" + base.name + "] to version " + m_Version + ".", this);
		m_FaceInfo.familyName = m_fontInfo.Name;
		m_FaceInfo.styleName = string.Empty;
		m_FaceInfo.pointSize = (int)m_fontInfo.PointSize;
		m_FaceInfo.scale = m_fontInfo.Scale;
		m_FaceInfo.lineHeight = m_fontInfo.LineHeight;
		m_FaceInfo.ascentLine = m_fontInfo.Ascender;
		m_FaceInfo.capLine = m_fontInfo.CapHeight;
		m_FaceInfo.meanLine = m_fontInfo.CenterLine;
		m_FaceInfo.baseline = m_fontInfo.Baseline;
		m_FaceInfo.descentLine = m_fontInfo.Descender;
		m_FaceInfo.superscriptOffset = m_fontInfo.SuperscriptOffset;
		m_FaceInfo.superscriptSize = m_fontInfo.SubSize;
		m_FaceInfo.subscriptOffset = m_fontInfo.SubscriptOffset;
		m_FaceInfo.subscriptSize = m_fontInfo.SubSize;
		m_FaceInfo.underlineOffset = m_fontInfo.Underline;
		m_FaceInfo.underlineThickness = m_fontInfo.UnderlineThickness;
		m_FaceInfo.strikethroughOffset = m_fontInfo.strikethrough;
		m_FaceInfo.strikethroughThickness = m_fontInfo.strikethroughThickness;
		m_FaceInfo.tabWidth = m_fontInfo.TabWidth;
		if (m_AtlasTextures == null || m_AtlasTextures.Length == 0)
		{
			m_AtlasTextures = new Texture2D[1];
		}
		m_AtlasTextures[0] = atlas;
		m_AtlasWidth = (int)m_fontInfo.AtlasWidth;
		m_AtlasHeight = (int)m_fontInfo.AtlasHeight;
		m_AtlasPadding = (int)m_fontInfo.Padding;
		switch (m_CreationSettings.renderMode)
		{
		case 0:
			m_AtlasRenderMode = GlyphRenderMode.SMOOTH_HINTED;
			break;
		case 1:
			m_AtlasRenderMode = GlyphRenderMode.SMOOTH;
			break;
		case 2:
			m_AtlasRenderMode = GlyphRenderMode.RASTER_HINTED;
			break;
		case 3:
			m_AtlasRenderMode = GlyphRenderMode.RASTER;
			break;
		case 6:
			m_AtlasRenderMode = GlyphRenderMode.SDF16;
			break;
		case 7:
			m_AtlasRenderMode = GlyphRenderMode.SDF32;
			break;
		}
		if (fontWeights != null)
		{
			m_FontWeightTable[4] = fontWeights[4];
			m_FontWeightTable[7] = fontWeights[7];
		}
		if (fallbackFontAssets != null && fallbackFontAssets.Count > 0)
		{
			if (m_FallbackFontAssetTable == null)
			{
				m_FallbackFontAssetTable = new List<TMP_FontAsset>(fallbackFontAssets.Count);
			}
			for (int i = 0; i < fallbackFontAssets.Count; i++)
			{
				m_FallbackFontAssetTable.Add(fallbackFontAssets[i]);
			}
		}
		if (m_CreationSettings.sourceFontFileGUID != null || m_CreationSettings.sourceFontFileGUID != string.Empty)
		{
			m_SourceFontFileGUID = m_CreationSettings.sourceFontFileGUID;
		}
		else
		{
			UnityEngine.Debug.LogWarning("Font asset [" + base.name + "] doesn't have a reference to its source font file. Please assign the appropriate source font file for this asset in the Font Atlas & Material section of font asset inspector.", this);
		}
		m_GlyphTable.Clear();
		m_CharacterTable.Clear();
		bool flag = false;
		for (int j = 0; j < m_glyphInfoList.Count; j++)
		{
			TMP_Glyph tMP_Glyph = m_glyphInfoList[j];
			Glyph glyph = new Glyph();
			uint index = (uint)(j + 1);
			glyph.index = index;
			glyph.glyphRect = new GlyphRect((int)tMP_Glyph.x, m_AtlasHeight - (int)(tMP_Glyph.y + tMP_Glyph.height + 0.5f), (int)(tMP_Glyph.width + 0.5f), (int)(tMP_Glyph.height + 0.5f));
			glyph.metrics = new GlyphMetrics(tMP_Glyph.width, tMP_Glyph.height, tMP_Glyph.xOffset, tMP_Glyph.yOffset, tMP_Glyph.xAdvance);
			glyph.scale = tMP_Glyph.scale;
			glyph.atlasIndex = 0;
			m_GlyphTable.Add(glyph);
			TMP_Character item = new TMP_Character((uint)tMP_Glyph.id, glyph);
			if (tMP_Glyph.id == 32)
			{
				flag = true;
			}
			m_CharacterTable.Add(item);
		}
		if (!flag)
		{
			UnityEngine.Debug.Log("Synthesizing Space for [" + base.name + "]");
			Glyph glyph2 = new Glyph(0u, new GlyphMetrics(0f, 0f, 0f, 0f, m_FaceInfo.ascentLine / 5f), GlyphRect.zero, 1f, 0);
			m_GlyphTable.Add(glyph2);
			m_CharacterTable.Add(new TMP_Character(32u, glyph2));
		}
		ReadFontAssetDefinition();
	}

	private void UpgradeGlyphAdjustmentTableToFontFeatureTable()
	{
		UnityEngine.Debug.Log("Upgrading font asset [" + base.name + "] Glyph Adjustment Table.", this);
		if (m_FontFeatureTable == null)
		{
			m_FontFeatureTable = new TMP_FontFeatureTable();
		}
		int count = m_KerningTable.kerningPairs.Count;
		m_FontFeatureTable.m_GlyphPairAdjustmentRecords = new List<TMP_GlyphPairAdjustmentRecord>(count);
		for (int i = 0; i < count; i++)
		{
			KerningPair kerningPair = m_KerningTable.kerningPairs[i];
			uint glyphIndex = 0u;
			if (m_CharacterLookupDictionary.TryGetValue(kerningPair.firstGlyph, out var value))
			{
				glyphIndex = value.glyphIndex;
			}
			uint glyphIndex2 = 0u;
			if (m_CharacterLookupDictionary.TryGetValue(kerningPair.secondGlyph, out var value2))
			{
				glyphIndex2 = value2.glyphIndex;
			}
			TMP_GlyphAdjustmentRecord firstAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphIndex, new TMP_GlyphValueRecord(kerningPair.firstGlyphAdjustments.xPlacement, kerningPair.firstGlyphAdjustments.yPlacement, kerningPair.firstGlyphAdjustments.xAdvance, kerningPair.firstGlyphAdjustments.yAdvance));
			TMP_GlyphAdjustmentRecord secondAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphIndex2, new TMP_GlyphValueRecord(kerningPair.secondGlyphAdjustments.xPlacement, kerningPair.secondGlyphAdjustments.yPlacement, kerningPair.secondGlyphAdjustments.xAdvance, kerningPair.secondGlyphAdjustments.yAdvance));
			TMP_GlyphPairAdjustmentRecord item = new TMP_GlyphPairAdjustmentRecord(firstAdjustmentRecord, secondAdjustmentRecord);
			m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(item);
		}
		m_KerningTable.kerningPairs = null;
		m_KerningTable = null;
	}
}
[Serializable]
public class FaceInfo_Legacy
{
	public string Name;

	public float PointSize;

	public float Scale;

	public int CharacterCount;

	public float LineHeight;

	public float Baseline;

	public float Ascender;

	public float CapHeight;

	public float Descender;

	public float CenterLine;

	public float SuperscriptOffset;

	public float SubscriptOffset;

	public float SubSize;

	public float Underline;

	public float UnderlineThickness;

	public float strikethrough;

	public float strikethroughThickness;

	public float TabWidth;

	public float Padding;

	public float AtlasWidth;

	public float AtlasHeight;
}
[Serializable]
public class TMP_Glyph : TMP_TextElement_Legacy
{
	public static TMP_Glyph Clone(TMP_Glyph source)
	{
		return new TMP_Glyph
		{
			id = source.id,
			x = source.x,
			y = source.y,
			width = source.width,
			height = source.height,
			xOffset = source.xOffset,
			yOffset = source.yOffset,
			xAdvance = source.xAdvance,
			scale = source.scale
		};
	}
}
[Serializable]
public struct FontAssetCreationSettings
{
	public string sourceFontFileName;

	public string sourceFontFileGUID;

	public int pointSizeSamplingMode;

	public int pointSize;

	public int padding;

	public int packingMode;

	public int atlasWidth;

	public int atlasHeight;

	public int characterSetSelectionMode;

	public string characterSequence;

	public string referencedFontAssetGUID;

	public string referencedTextAssetGUID;

	public int fontStyle;

	public float fontStyleModifier;

	public int renderMode;

	public bool includeFontFeatures;

	internal FontAssetCreationSettings(string sourceFontFileGUID, int pointSize, int pointSizeSamplingMode, int padding, int packingMode, int atlasWidth, int atlasHeight, int characterSelectionMode, string characterSet, int renderMode)
	{
		sourceFontFileName = string.Empty;
		this.sourceFontFileGUID = sourceFontFileGUID;
		this.pointSize = pointSize;
		this.pointSizeSamplingMode = pointSizeSamplingMode;
		this.padding = padding;
		this.packingMode = packingMode;
		this.atlasWidth = atlasWidth;
		this.atlasHeight = atlasHeight;
		characterSequence = characterSet;
		characterSetSelectionMode = characterSelectionMode;
		this.renderMode = renderMode;
		referencedFontAssetGUID = string.Empty;
		referencedTextAssetGUID = string.Empty;
		fontStyle = 0;
		fontStyleModifier = 0f;
		includeFontFeatures = false;
	}
}
[Serializable]
public struct TMP_FontWeightPair
{
	public TMP_FontAsset regularTypeface;

	public TMP_FontAsset italicTypeface;
}
public struct KerningPairKey
{
	public uint ascii_Left;

	public uint ascii_Right;

	public uint key;

	public KerningPairKey(uint ascii_left, uint ascii_right)
	{
		ascii_Left = ascii_left;
		ascii_Right = ascii_right;
		key = (ascii_right << 16) + ascii_left;
	}
}
[Serializable]
public struct GlyphValueRecord_Legacy
{
	public float xPlacement;

	public float yPlacement;

	public float xAdvance;

	public float yAdvance;

	internal GlyphValueRecord_Legacy(GlyphValueRecord valueRecord)
	{
		xPlacement = valueRecord.xPlacement;
		yPlacement = valueRecord.yPlacement;
		xAdvance = valueRecord.xAdvance;
		yAdvance = valueRecord.yAdvance;
	}

	public static GlyphValueRecord_Legacy operator +(GlyphValueRecord_Legacy a, GlyphValueRecord_Legacy b)
	{
		GlyphValueRecord_Legacy result = default(GlyphValueRecord_Legacy);
		result.xPlacement = a.xPlacement + b.xPlacement;
		result.yPlacement = a.yPlacement + b.yPlacement;
		result.xAdvance = a.xAdvance + b.xAdvance;
		result.yAdvance = a.yAdvance + b.yAdvance;
		return result;
	}
}
[Serializable]
public class KerningPair
{
	[FormerlySerializedAs("AscII_Left")]
	[SerializeField]
	private uint m_FirstGlyph;

	[SerializeField]
	private GlyphValueRecord_Legacy m_FirstGlyphAdjustments;

	[FormerlySerializedAs("AscII_Right")]
	[SerializeField]
	private uint m_SecondGlyph;

	[SerializeField]
	private GlyphValueRecord_Legacy m_SecondGlyphAdjustments;

	[FormerlySerializedAs("XadvanceOffset")]
	public float xOffset;

	internal static KerningPair empty = new KerningPair(0u, default(GlyphValueRecord_Legacy), 0u, default(GlyphValueRecord_Legacy));

	[SerializeField]
	private bool m_IgnoreSpacingAdjustments;

	public uint firstGlyph
	{
		get
		{
			return m_FirstGlyph;
		}
		set
		{
			m_FirstGlyph = value;
		}
	}

	public GlyphValueRecord_Legacy firstGlyphAdjustments => m_FirstGlyphAdjustments;

	public uint secondGlyph
	{
		get
		{
			return m_SecondGlyph;
		}
		set
		{
			m_SecondGlyph = value;
		}
	}

	public GlyphValueRecord_Legacy secondGlyphAdjustments => m_SecondGlyphAdjustments;

	public bool ignoreSpacingAdjustments => m_IgnoreSpacingAdjustments;

	public KerningPair()
	{
		m_FirstGlyph = 0u;
		m_FirstGlyphAdjustments = default(GlyphValueRecord_Legacy);
		m_SecondGlyph = 0u;
		m_SecondGlyphAdjustments = default(GlyphValueRecord_Legacy);
	}

	public KerningPair(uint left, uint right, float offset)
	{
		firstGlyph = left;
		m_SecondGlyph = right;
		xOffset = offset;
	}

	public KerningPair(uint firstGlyph, GlyphValueRecord_Legacy firstGlyphAdjustments, uint secondGlyph, GlyphValueRecord_Legacy secondGlyphAdjustments)
	{
		m_FirstGlyph = firstGlyph;
		m_FirstGlyphAdjustments = firstGlyphAdjustments;
		m_SecondGlyph = secondGlyph;
		m_SecondGlyphAdjustments = secondGlyphAdjustments;
	}

	internal void ConvertLegacyKerningData()
	{
		m_FirstGlyphAdjustments.xAdvance = xOffset;
	}
}
[Serializable]
public class KerningTable
{
	public List<KerningPair> kerningPairs;

	public KerningTable()
	{
		kerningPairs = new List<KerningPair>();
	}

	public void AddKerningPair()
	{
		if (kerningPairs.Count == 0)
		{
			kerningPairs.Add(new KerningPair(0u, 0u, 0f));
			return;
		}
		uint firstGlyph = kerningPairs.Last().firstGlyph;
		uint secondGlyph = kerningPairs.Last().secondGlyph;
		float xOffset = kerningPairs.Last().xOffset;
		kerningPairs.Add(new KerningPair(firstGlyph, secondGlyph, xOffset));
	}

	public int AddKerningPair(uint first, uint second, float offset)
	{
		if (kerningPairs.FindIndex((KerningPair item) => item.firstGlyph == first && item.secondGlyph == second) == -1)
		{
			kerningPairs.Add(new KerningPair(first, second, offset));
			return 0;
		}
		return -1;
	}

	public int AddGlyphPairAdjustmentRecord(uint first, GlyphValueRecord_Legacy firstAdjustments, uint second, GlyphValueRecord_Legacy secondAdjustments)
	{
		if (kerningPairs.FindIndex((KerningPair item) => item.firstGlyph == first && item.secondGlyph == second) == -1)
		{
			kerningPairs.Add(new KerningPair(first, firstAdjustments, second, secondAdjustments));
			return 0;
		}
		return -1;
	}

	public void RemoveKerningPair(int left, int right)
	{
		int num = kerningPairs.FindIndex((KerningPair item) => item.firstGlyph == left && item.secondGlyph == right);
		if (num != -1)
		{
			kerningPairs.RemoveAt(num);
		}
	}

	public void RemoveKerningPair(int index)
	{
		kerningPairs.RemoveAt(index);
	}

	public void SortKerningPairs()
	{
		if (kerningPairs.Count > 0)
		{
			kerningPairs = (from s in kerningPairs
				orderby s.firstGlyph, s.secondGlyph
				select s).ToList();
		}
	}
}
public static class TMP_FontUtilities
{
	private static List<int> k_searchedFontAssets;

	public static TMP_FontAsset SearchForCharacter(TMP_FontAsset font, uint unicode, out TMP_Character character)
	{
		if (k_searchedFontAssets == null)
		{
			k_searchedFontAssets = new List<int>();
		}
		k_searchedFontAssets.Clear();
		return SearchForCharacterInternal(font, unicode, out character);
	}

	public static TMP_FontAsset SearchForCharacter(List<TMP_FontAsset> fonts, uint unicode, out TMP_Character character)
	{
		return SearchForCharacterInternal(fonts, unicode, out character);
	}

	private static TMP_FontAsset SearchForCharacterInternal(TMP_FontAsset font, uint unicode, out TMP_Character character)
	{
		character = null;
		if (font == null)
		{
			return null;
		}
		if (font.characterLookupTable.TryGetValue(unicode, out character))
		{
			return font;
		}
		if (font.fallbackFontAssetTable != null && font.fallbackFontAssetTable.Count > 0)
		{
			for (int i = 0; i < font.fallbackFontAssetTable.Count; i++)
			{
				if (character != null)
				{
					break;
				}
				TMP_FontAsset tMP_FontAsset = font.fallbackFontAssetTable[i];
				if (tMP_FontAsset == null)
				{
					continue;
				}
				int instanceID = tMP_FontAsset.GetInstanceID();
				if (!k_searchedFontAssets.Contains(instanceID))
				{
					k_searchedFontAssets.Add(instanceID);
					tMP_FontAsset = SearchForCharacterInternal(tMP_FontAsset, unicode, out character);
					if (tMP_FontAsset != null)
					{
						return tMP_FontAsset;
					}
				}
			}
		}
		return null;
	}

	private static TMP_FontAsset SearchForCharacterInternal(List<TMP_FontAsset> fonts, uint unicode, out TMP_Character character)
	{
		character = null;
		if (fonts != null && fonts.Count > 0)
		{
			for (int i = 0; i < fonts.Count; i++)
			{
				TMP_FontAsset tMP_FontAsset = SearchForCharacterInternal(fonts[i], unicode, out character);
				if (tMP_FontAsset != null)
				{
					return tMP_FontAsset;
				}
			}
		}
		return null;
	}
}
public class TMP_FontAssetUtilities
{
	private static readonly TMP_FontAssetUtilities s_Instance;

	private static List<int> k_SearchedFontAssets;

	private static bool k_IsFontEngineInitialized;

	public static TMP_FontAssetUtilities instance => s_Instance;

	static TMP_FontAssetUtilities()
	{
		s_Instance = new TMP_FontAssetUtilities();
	}

	public static TMP_Character GetCharacterFromFontAsset(uint unicode, TMP_FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface, out TMP_FontAsset fontAsset)
	{
		if (includeFallbacks)
		{
			if (k_SearchedFontAssets == null)
			{
				k_SearchedFontAssets = new List<int>();
			}
			else
			{
				k_SearchedFontAssets.Clear();
			}
		}
		return GetCharacterFromFontAsset_Internal(unicode, sourceFontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, out fontAsset);
	}

	private static TMP_Character GetCharacterFromFontAsset_Internal(uint unicode, TMP_FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface, out TMP_FontAsset fontAsset)
	{
		fontAsset = null;
		isAlternativeTypeface = false;
		TMP_Character value = null;
		bool flag = (fontStyle & FontStyles.Italic) == FontStyles.Italic;
		if (flag || fontWeight != FontWeight.Regular)
		{
			TMP_FontWeightPair[] fontWeightTable = sourceFontAsset.fontWeightTable;
			int num = 4;
			switch (fontWeight)
			{
			case FontWeight.Thin:
				num = 1;
				break;
			case FontWeight.ExtraLight:
				num = 2;
				break;
			case FontWeight.Light:
				num = 3;
				break;
			case FontWeight.Regular:
				num = 4;
				break;
			case FontWeight.Medium:
				num = 5;
				break;
			case FontWeight.SemiBold:
				num = 6;
				break;
			case FontWeight.Bold:
				num = 7;
				break;
			case FontWeight.Heavy:
				num = 8;
				break;
			case FontWeight.Black:
				num = 9;
				break;
			}
			fontAsset = (flag ? fontWeightTable[num].italicTypeface : fontWeightTable[num].regularTypeface);
			if (fontAsset != null)
			{
				if (fontAsset.characterLookupTable.TryGetValue(unicode, out value))
				{
					isAlternativeTypeface = true;
					return value;
				}
				if (fontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic && fontAsset.TryAddCharacterInternal(unicode, out value))
				{
					isAlternativeTypeface = true;
					return value;
				}
			}
		}
		if (sourceFontAsset.characterLookupTable.TryGetValue(unicode, out value))
		{
			fontAsset = sourceFontAsset;
			return value;
		}
		if (sourceFontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic && sourceFontAsset.TryAddCharacterInternal(unicode, out value))
		{
			fontAsset = sourceFontAsset;
			return value;
		}
		if (value == null && includeFallbacks && sourceFontAsset.fallbackFontAssetTable != null)
		{
			List<TMP_FontAsset> fallbackFontAssetTable = sourceFontAsset.fallbackFontAssetTable;
			int count = fallbackFontAssetTable.Count;
			if (fallbackFontAssetTable != null && count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					if (value != null)
					{
						break;
					}
					TMP_FontAsset tMP_FontAsset = fallbackFontAssetTable[i];
					if (tMP_FontAsset == null)
					{
						continue;
					}
					int instanceID = tMP_FontAsset.GetInstanceID();
					if (!k_SearchedFontAssets.Contains(instanceID))
					{
						k_SearchedFontAssets.Add(instanceID);
						value = GetCharacterFromFontAsset_Internal(unicode, tMP_FontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, out fontAsset);
						if (value != null)
						{
							return value;
						}
					}
				}
			}
		}
		return null;
	}

	public static TMP_Character GetCharacterFromFontAssets(uint unicode, List<TMP_FontAsset> fontAssets, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface, out TMP_FontAsset fontAsset)
	{
		isAlternativeTypeface = false;
		if (fontAssets == null || fontAssets.Count == 0)
		{
			fontAsset = null;
			return null;
		}
		if (includeFallbacks)
		{
			if (k_SearchedFontAssets == null)
			{
				k_SearchedFontAssets = new List<int>();
			}
			else
			{
				k_SearchedFontAssets.Clear();
			}
		}
		int count = fontAssets.Count;
		for (int i = 0; i < count; i++)
		{
			if (!(fontAssets[i] == null))
			{
				TMP_Character characterFromFontAsset_Internal = GetCharacterFromFontAsset_Internal(unicode, fontAssets[i], includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, out fontAsset);
				if (characterFromFontAsset_Internal != null)
				{
					return characterFromFontAsset_Internal;
				}
			}
		}
		fontAsset = null;
		return null;
	}

	private static bool TryGetCharacterFromFontFile(uint unicode, TMP_FontAsset fontAsset, out TMP_Character character)
	{
		character = null;
		if (!k_IsFontEngineInitialized && FontEngine.InitializeFontEngine() == FontEngineError.Success)
		{
			k_IsFontEngineInitialized = true;
		}
		FontEngine.LoadFontFace(fontAsset.sourceFontFile, fontAsset.faceInfo.pointSize);
		Glyph value = null;
		uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
		if (fontAsset.glyphLookupTable.TryGetValue(glyphIndex, out value))
		{
			character = fontAsset.AddCharacter_Internal(unicode, value);
			return true;
		}
		GlyphLoadFlags flags = (((fontAsset.atlasRenderMode & (GlyphRenderMode)8) == (GlyphRenderMode)8) ? GlyphLoadFlags.LOAD_RENDER : (GlyphLoadFlags.LOAD_NO_HINTING | GlyphLoadFlags.LOAD_RENDER));
		if (FontEngine.TryGetGlyphWithUnicodeValue(unicode, flags, out value))
		{
			character = fontAsset.AddCharacter_Internal(unicode, value);
			return true;
		}
		return false;
	}

	public static bool TryGetGlyphFromFontFile(uint glyphIndex, TMP_FontAsset fontAsset, out Glyph glyph)
	{
		glyph = null;
		if (!k_IsFontEngineInitialized && FontEngine.InitializeFontEngine() == FontEngineError.Success)
		{
			k_IsFontEngineInitialized = true;
		}
		FontEngine.LoadFontFace(fontAsset.sourceFontFile, fontAsset.faceInfo.pointSize);
		GlyphLoadFlags flags = (((fontAsset.atlasRenderMode & (GlyphRenderMode)8) == (GlyphRenderMode)8) ? GlyphLoadFlags.LOAD_RENDER : (GlyphLoadFlags.LOAD_NO_HINTING | GlyphLoadFlags.LOAD_RENDER));
		if (FontEngine.TryGetGlyphWithIndexValue(glyphIndex, flags, out glyph))
		{
			return true;
		}
		return false;
	}
}
public enum FontFeatureLookupFlags
{
	IgnoreLigatures = 4,
	IgnoreSpacingAdjustments = 0x100
}
[Serializable]
public struct TMP_GlyphValueRecord
{
	[SerializeField]
	private float m_XPlacement;

	[SerializeField]
	private float m_YPlacement;

	[SerializeField]
	private float m_XAdvance;

	[SerializeField]
	private float m_YAdvance;

	public float xPlacement
	{
		get
		{
			return m_XPlacement;
		}
		set
		{
			m_XPlacement = value;
		}
	}

	public float yPlacement
	{
		get
		{
			return m_YPlacement;
		}
		set
		{
			m_YPlacement = value;
		}
	}

	public float xAdvance
	{
		get
		{
			return m_XAdvance;
		}
		set
		{
			m_XAdvance = value;
		}
	}

	public float yAdvance
	{
		get
		{
			return m_YAdvance;
		}
		set
		{
			m_YAdvance = value;
		}
	}

	public TMP_GlyphValueRecord(float xPlacement, float yPlacement, float xAdvance, float yAdvance)
	{
		m_XPlacement = xPlacement;
		m_YPlacement = yPlacement;
		m_XAdvance = xAdvance;
		m_YAdvance = yAdvance;
	}

	internal TMP_GlyphValueRecord(GlyphValueRecord_Legacy valueRecord)
	{
		m_XPlacement = valueRecord.xPlacement;
		m_YPlacement = valueRecord.yPlacement;
		m_XAdvance = valueRecord.xAdvance;
		m_YAdvance = valueRecord.yAdvance;
	}

	internal TMP_GlyphValueRecord(GlyphValueRecord valueRecord)
	{
		m_XPlacement = valueRecord.xPlacement;
		m_YPlacement = valueRecord.yPlacement;
		m_XAdvance = valueRecord.xAdvance;
		m_YAdvance = valueRecord.yAdvance;
	}

	public static TMP_GlyphValueRecord operator +(TMP_GlyphValueRecord a, TMP_GlyphValueRecord b)
	{
		TMP_GlyphValueRecord result = default(TMP_GlyphValueRecord);
		result.m_XPlacement = a.xPlacement + b.xPlacement;
		result.m_YPlacement = a.yPlacement + b.yPlacement;
		result.m_XAdvance = a.xAdvance + b.xAdvance;
		result.m_YAdvance = a.yAdvance + b.yAdvance;
		return result;
	}
}
[Serializable]
public struct TMP_GlyphAdjustmentRecord
{
	[SerializeField]
	private uint m_GlyphIndex;

	[SerializeField]
	private TMP_GlyphValueRecord m_GlyphValueRecord;

	public uint glyphIndex
	{
		get
		{
			return m_GlyphIndex;
		}
		set
		{
			m_GlyphIndex = value;
		}
	}

	public TMP_GlyphValueRecord glyphValueRecord
	{
		get
		{
			return m_GlyphValueRecord;
		}
		set
		{
			m_GlyphValueRecord = value;
		}
	}

	public TMP_GlyphAdjustmentRecord(uint glyphIndex, TMP_GlyphValueRecord glyphValueRecord)
	{
		m_GlyphIndex = glyphIndex;
		m_GlyphValueRecord = glyphValueRecord;
	}

	internal TMP_GlyphAdjustmentRecord(GlyphAdjustmentRecord adjustmentRecord)
	{
		m_GlyphIndex = adjustmentRecord.glyphIndex;
		m_GlyphValueRecord = new TMP_GlyphValueRecord(adjustmentRecord.glyphValueRecord);
	}
}
[Serializable]
public class TMP_GlyphPairAdjustmentRecord
{
	[SerializeField]
	private TMP_GlyphAdjustmentRecord m_FirstAdjustmentRecord;

	[SerializeField]
	private TMP_GlyphAdjustmentRecord m_SecondAdjustmentRecord;

	[SerializeField]
	private FontFeatureLookupFlags m_FeatureLookupFlags;

	public TMP_GlyphAdjustmentRecord firstAdjustmentRecord
	{
		get
		{
			return m_FirstAdjustmentRecord;
		}
		set
		{
			m_FirstAdjustmentRecord = value;
		}
	}

	public TMP_GlyphAdjustmentRecord secondAdjustmentRecord
	{
		get
		{
			return m_SecondAdjustmentRecord;
		}
		set
		{
			m_SecondAdjustmentRecord = value;
		}
	}

	public FontFeatureLookupFlags featureLookupFlags
	{
		get
		{
			return m_FeatureLookupFlags;
		}
		set
		{
			m_FeatureLookupFlags = value;
		}
	}

	public TMP_GlyphPairAdjustmentRecord(TMP_GlyphAdjustmentRecord firstAdjustmentRecord, TMP_GlyphAdjustmentRecord secondAdjustmentRecord)
	{
		m_FirstAdjustmentRecord = firstAdjustmentRecord;
		m_SecondAdjustmentRecord = secondAdjustmentRecord;
	}

	internal TMP_GlyphPairAdjustmentRecord(GlyphPairAdjustmentRecord glyphPairAdjustmentRecord)
	{
		m_FirstAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphPairAdjustmentRecord.firstAdjustmentRecord);
		m_SecondAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphPairAdjustmentRecord.secondAdjustmentRecord);
	}
}
public struct GlyphPairKey
{
	public uint firstGlyphIndex;

	public uint secondGlyphIndex;

	public long key;

	public GlyphPairKey(uint firstGlyphIndex, uint secondGlyphIndex)
	{
		this.firstGlyphIndex = firstGlyphIndex;
		this.secondGlyphIndex = secondGlyphIndex;
		key = (long)(((ulong)secondGlyphIndex << 32) | firstGlyphIndex);
	}

	internal GlyphPairKey(TMP_GlyphPairAdjustmentRecord record)
	{
		firstGlyphIndex = record.firstAdjustmentRecord.glyphIndex;
		secondGlyphIndex = record.secondAdjustmentRecord.glyphIndex;
		key = (long)(((ulong)secondGlyphIndex << 32) | firstGlyphIndex);
	}
}
[Serializable]
public class TMP_FontFeatureTable
{
	[SerializeField]
	internal List<TMP_GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecords;

	internal Dictionary<long, TMP_GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecordLookupDictionary;

	internal List<TMP_GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords
	{
		get
		{
			return m_GlyphPairAdjustmentRecords;
		}
		set
		{
			m_GlyphPairAdjustmentRecords = value;
		}
	}

	public TMP_FontFeatureTable()
	{
		m_GlyphPairAdjustmentRecords = new List<TMP_GlyphPairAdjustmentRecord>();
		m_GlyphPairAdjustmentRecordLookupDictionary = new Dictionary<long, TMP_GlyphPairAdjustmentRecord>();
	}

	public void SortGlyphPairAdjustmentRecords()
	{
		if (m_GlyphPairAdjustmentRecords.Count > 0)
		{
			m_GlyphPairAdjustmentRecords = (from s in m_GlyphPairAdjustmentRecords
				orderby s.firstAdjustmentRecord.glyphIndex, s.secondAdjustmentRecord.glyphIndex
				select s).ToList();
		}
	}
}
[AddComponentMenu("", 11)]
public class TMP_InputField : Selectable, IUpdateSelectedHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, ISubmitHandler, ICanvasElement, ILayoutElement, IScrollHandler
{
	public enum ContentType
	{
		Standard,
		Autocorrected,
		IntegerNumber,
		DecimalNumber,
		Alphanumeric,
		Name,
		EmailAddress,
		Password,
		Pin,
		Custom,
		UnsignedIntegerNumber,
		UnsignedDecimalNumber
	}

	public enum InputType
	{
		Standard,
		AutoCorrect,
		Password
	}

	public enum CharacterValidation
	{
		None,
		Digit,
		Integer,
		Decimal,
		Alphanumeric,
		Name,
		Regex,
		EmailAddress,
		CustomValidator,
		UnsignedInteger,
		UnsignedDecimal
	}

	public enum LineType
	{
		SingleLine,
		MultiLineSubmit,
		MultiLineNewline
	}

	public delegate char OnValidateInput(string text, int charIndex, char addedChar);

	[Serializable]
	public class SubmitEvent : UnityEvent<string>
	{
	}

	[Serializable]
	public class OnChangeEvent : UnityEvent<string>
	{
	}

	[Serializable]
	public class SelectionEvent : UnityEvent<string>
	{
	}

	[Serializable]
	public class TextSelectionEvent : UnityEvent<string, int, int>
	{
	}

	[Serializable]
	public class TouchScreenKeyboardEvent : UnityEvent<TouchScreenKeyboard.Status>
	{
	}

	protected enum EditState
	{
		Continue,
		Finish
	}

	protected TouchScreenKeyboard m_SoftKeyboard;

	private static readonly char[] kSeparators = new char[6] { ' ', '.', ',', '\t', '\r', '\n' };

	[SerializeField]
	protected RectTransform m_TextViewport;

	[SerializeField]
	protected TMP_Text m_TextComponent;

	protected RectTransform m_TextComponentRectTransform;

	[SerializeField]
	protected Graphic m_Placeholder;

	[SerializeField]
	protected Scrollbar m_VerticalScrollbar;

	[SerializeField]
	protected TMP_ScrollbarEventHandler m_VerticalScrollbarEventHandler;

	private bool m_IsDrivenByLayoutComponents;

	private float m_ScrollPosition;

	[SerializeField]
	protected float m_ScrollSensitivity = 1f;

	[SerializeField]
	private ContentType m_ContentType;

	[SerializeField]
	private InputType m_InputType;

	[SerializeField]
	private char m_AsteriskChar = '*';

	[SerializeField]
	private TouchScreenKeyboardType m_KeyboardType;

	[SerializeField]
	private LineType m_LineType;

	[SerializeField]
	private bool m_HideMobileInput;

	[SerializeField]
	private bool m_HideSoftKeyboard;

	[SerializeField]
	private CharacterValidation m_CharacterValidation;

	[SerializeField]
	private string m_RegexValue = string.Empty;

	[SerializeField]
	private float m_GlobalPointSize = 14f;

	[SerializeField]
	private int m_CharacterLimit;

	[SerializeField]
	private SubmitEvent m_OnEndEdit = new SubmitEvent();

	[SerializeField]
	private SubmitEvent m_OnSubmit = new SubmitEvent();

	[SerializeField]
	private SelectionEvent m_OnSelect = new SelectionEvent();

	[SerializeField]
	private SelectionEvent m_OnDeselect = new SelectionEvent();

	[SerializeField]
	private TextSelectionEvent m_OnTextSelection = new TextSelectionEvent();

	[SerializeField]
	private TextSelectionEvent m_OnEndTextSelection = new TextSelectionEvent();

	[SerializeField]
	private OnChangeEvent m_OnValueChanged = new OnChangeEvent();

	[SerializeField]
	private TouchScreenKeyboardEvent m_OnTouchScreenKeyboardStatusChanged = new TouchScreenKeyboardEvent();

	[SerializeField]
	private OnValidateInput m_OnValidateInput;

	[SerializeField]
	private Color m_CaretColor = new Color(10f / 51f, 10f / 51f, 10f / 51f, 1f);

	[SerializeField]
	private bool m_CustomCaretColor;

	[SerializeField]
	private Color m_SelectionColor = new Color(56f / 85f, 0.80784315f, 1f, 64f / 85f);

	[SerializeField]
	[TextArea(5, 10)]
	protected string m_Text = string.Empty;

	[SerializeField]
	[Range(0f, 4f)]
	private float m_CaretBlinkRate = 0.85f;

	[SerializeField]
	[Range(1f, 5f)]
	private int m_CaretWidth = 1;

	[SerializeField]
	private bool m_ReadOnly;

	[SerializeField]
	private bool m_RichText = true;

	protected int m_StringPosition;

	protected int m_StringSelectPosition;

	protected int m_CaretPosition;

	protected int m_CaretSelectPosition;

	private RectTransform caretRectTrans;

	protected UIVertex[] m_CursorVerts;

	private CanvasRenderer m_CachedInputRenderer;

	private Vector2 m_LastPosition;

	[NonSerialized]
	protected Mesh m_Mesh;

	private bool m_AllowInput;

	private bool m_ShouldActivateNextUpdate;

	private bool m_UpdateDrag;

	private bool m_DragPositionOutOfBounds;

	private const float kHScrollSpeed = 0.05f;

	private const float kVScrollSpeed = 0.1f;

	protected bool m_CaretVisible;

	private Coroutine m_BlinkCoroutine;

	private float m_BlinkStartTime;

	private Coroutine m_DragCoroutine;

	private string m_OriginalText = "";

	private bool m_WasCanceled;

	private bool m_HasDoneFocusTransition;

	private WaitForSecondsRealtime m_WaitForSecondsRealtime;

	private bool m_PreventCallback;

	private bool m_TouchKeyboardAllowsInPlaceEditing;

	private bool m_IsTextComponentUpdateRequired;

	private bool m_IsScrollbarUpdateRequired;

	private bool m_IsUpdatingScrollbarValues;

	private bool m_isLastKeyBackspace;

	private float m_PointerDownClickStartTime;

	private float m_KeyDownStartTime;

	private float m_DoubleClickDelay = 0.5f;

	private const string kEmailSpecialCharacters = "!#$%&'*+-/=?^_`{|}~";

	[SerializeField]
	protected TMP_FontAsset m_GlobalFontAsset;

	[SerializeField]
	protected bool m_OnFocusSelectAll = true;

	protected bool m_isSelectAll;

	[SerializeField]
	protected bool m_ResetOnDeActivation = true;

	private bool m_SelectionStillActive;

	private bool m_ReleaseSelection;

	private GameObject m_SelectedObject;

	[SerializeField]
	protected bool m_UnFocusOnSubmit = true;

	[SerializeField]
	private bool m_RestoreOriginalTextOnEscape = true;

	[SerializeField]
	protected bool m_isRichTextEditingAllowed;

	[SerializeField]
	protected int m_LineLimit;

	[SerializeField]
	protected TMP_InputValidator m_InputValidator;

	private bool m_isSelected;

	private bool m_IsStringPositionDirty;

	private bool m_IsCaretPositionDirty;

	private bool m_forceRectTransformAdjustment;

	private Event m_ProcessingEvent = new Event();

	private bool isRunningSubstitution;

	private BaseInput inputSystem
	{
		get
		{
			if ((bool)EventSystem.current && (bool)EventSystem.current.currentInputModule)
			{
				return EventSystem.current.currentInputModule.input;
			}
			return null;
		}
	}

	private string compositionString
	{
		get
		{
			if (!(inputSystem != null))
			{
				return Input.compositionString;
			}
			return inputSystem.compositionString;
		}
	}

	protected Mesh mesh
	{
		get
		{
			if (m_Mesh == null)
			{
				m_Mesh = new Mesh();
			}
			return m_Mesh;
		}
	}

	public bool shouldHideMobileInput
	{
		get
		{
			RuntimePlatform platform = Application.platform;
			if (platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.Android || platform == RuntimePlatform.tvOS)
			{
				return m_HideMobileInput;
			}
			return true;
		}
		set
		{
			RuntimePlatform platform = Application.platform;
			if (platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.Android || platform == RuntimePlatform.tvOS)
			{
				SetPropertyUtility.SetStruct(ref m_HideMobileInput, value);
			}
			else
			{
				m_HideMobileInput = true;
			}
		}
	}

	public bool shouldHideSoftKeyboard
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.IPhonePlayer:
			case RuntimePlatform.Android:
			case RuntimePlatform.MetroPlayerX86:
			case RuntimePlatform.MetroPlayerX64:
			case RuntimePlatform.MetroPlayerARM:
			case RuntimePlatform.tvOS:
				return m_HideSoftKeyboard;
			default:
				return true;
			}
		}
		set
		{
			switch (Application.platform)
			{
			case RuntimePlatform.IPhonePlayer:
			case RuntimePlatform.Android:
			case RuntimePlatform.MetroPlayerX86:
			case RuntimePlatform.MetroPlayerX64:
			case RuntimePlatform.MetroPlayerARM:
			case RuntimePlatform.tvOS:
				SetPropertyUtility.SetStruct(ref m_HideSoftKeyboard, value);
				break;
			default:
				m_HideSoftKeyboard = true;
				break;
			}
			if (m_HideSoftKeyboard && m_SoftKeyboard != null && TouchScreenKeyboard.isSupported && m_SoftKeyboard.active)
			{
				m_SoftKeyboard.active = false;
				m_SoftKeyboard = null;
			}
		}
	}

	public string text
	{
		get
		{
			return m_Text;
		}
		set
		{
			SetText(value);
		}
	}

	public bool isFocused => m_AllowInput;

	public float caretBlinkRate
	{
		get
		{
			return m_CaretBlinkRate;
		}
		set
		{
			if (SetPropertyUtility.SetStruct(ref m_CaretBlinkRate, value) && m_AllowInput)
			{
				SetCaretActive();
			}
		}
	}

	public int caretWidth
	{
		get
		{
			return m_CaretWidth;
		}
		set
		{
			if (SetPropertyUtility.SetStruct(ref m_CaretWidth, value))
			{
				MarkGeometryAsDirty();
			}
		}
	}

	public RectTransform textViewport
	{
		get
		{
			return m_TextViewport;
		}
		set
		{
			SetPropertyUtility.SetClass(ref m_TextViewport, value);
		}
	}

	public TMP_Text textComponent
	{
		get
		{
			return m_TextComponent;
		}
		set
		{
			if (SetPropertyUtility.SetClass(ref m_TextComponent, value))
			{
				SetTextComponentWrapMode();
			}
		}
	}

	public Graphic placeholder
	{
		get
		{
			return m_Placeholder;
		}
		set
		{
			SetPropertyUtility.SetClass(ref m_Placeholder, value);
		}
	}

	public Scrollbar verticalScrollbar
	{
		get
		{
			return m_VerticalScrollbar;
		}
		set
		{
			if (m_VerticalScrollbar != null)
			{
				m_VerticalScrollbar.onValueChanged.RemoveListener(OnScrollbarValueChange);
			}
			SetPropertyUtility.SetClass(ref m_VerticalScrollbar, value);
			if ((bool)m_VerticalScrollbar)
			{
				m_VerticalScrollbar.onValueChanged.AddListener(OnScrollbarValueChange);
			}
		}
	}

	public float scrollSensitivity
	{
		get
		{
			return m_ScrollSensitivity;
		}
		set
		{
			if (SetPropertyUtility.SetStruct(ref m_ScrollSensitivity, value))
			{
				MarkGeometryAsDirty();
			}
		}
	}

	public Color caretColor
	{
		get
		{
			if (!customCaretColor)
			{
				return textComponent.color;
			}
			return m_CaretColor;
		}
		set
		{
			if (SetPropertyUtility.SetColor(ref m_CaretColor, value))
			{
				MarkGeometryAsDirty();
			}
		}
	}

	public bool customCaretColor
	{
		get
		{
			return m_CustomCaretColor;
		}
		set
		{
			if (m_CustomCaretColor != value)
			{
				m_CustomCaretColor = value;
				MarkGeometryAsDirty();
			}
		}
	}

	public Color selectionColor
	{
		get
		{
			return m_SelectionColor;
		}
		set
		{
			if (SetPropertyUtility.SetColor(ref m_SelectionColor, value))
			{
				MarkGeometryAsDirty();
			}
		}
	}

	public SubmitEvent onEndEdit
	{
		get
		{
			return m_OnEndEdit;
		}
		set
		{
			SetPropertyUtility.SetClass(ref m_OnEndEdit, value);
		}
	}

	public SubmitEvent onSubmit
	{
		get
		{
			return m_OnSubmit;
		}
		set
		{
			SetPropertyUtility.SetClass(ref m_OnSubmit, value);
		}
	}

	public SelectionEvent onSelect
	{
		get
		{
			return m_OnSelect;
		}
		set
		{
			SetPropertyUtility.SetClass(ref m_OnSelect, value);
		}
	}

	public SelectionEvent onDeselect
	{
		get
		{
			return m_OnDeselect;
		}
		set
		{
			SetPropertyUtility.SetClass(ref m_OnDeselect, value);
		}
	}

	public TextSelectionEvent onTextSelection
	{
		get
		{
			return m_OnTextSelection;
		}
		set
		{
			SetPropertyUtility.SetClass(ref m_OnTextSelection, value);
		}
	}

	public TextSelectionEvent onEndTextSelection
	{
		get
		{
			return m_OnEndTextSelection;
		}
		set
		{
			SetPropertyUtility.SetClass(ref m_OnEndTextSelection, value);
		}
	}

	public OnChangeEvent onValueChanged
	{
		get
		{
			return m_OnValueChanged;
		}
		set
		{
			SetPropertyUtility.SetClass(ref m_OnValueChanged, value);
		}
	}

	public TouchScreenKeyboardEvent onTouchScreenKeyboardStatusChanged
	{
		get
		{
			return m_OnTouchScreenKeyboardStatusChanged;
		}
		set
		{
			SetPropertyUtility.SetClass(ref m_OnTouchScreenKeyboardStatusChanged, value);
		}
	}

	public OnValidateInput onValidateInput
	{
		get
		{
			return m_OnValidateInput;
		}
		set
		{
			SetPropertyUtility.SetClass(ref m_OnValidateInput, value);
		}
	}

	public int characterLimit
	{
		get
		{
			return m_CharacterLimit;
		}
		set
		{
			if (SetPropertyUtility.SetStruct(ref m_CharacterLimit, Math.Max(0, value)))
			{
				UpdateLabel();
				if (m_SoftKeyboard != null)
				{
					m_SoftKeyboard.characterLimit = value;
				}
			}
		}
	}

	public float pointSize
	{
		get
		{
			return m_GlobalPointSize;
		}
		set
		{
			if (SetPropertyUtility.SetStruct(ref m_GlobalPointSize, Math.Max(0f, value)))
			{
				SetGlobalPointSize(m_GlobalPointSize);
				UpdateLabel();
			}
		}
	}

	public TMP_FontAsset fontAsset
	{
		get
		{
			return m_GlobalFontAsset;
		}
		set
		{
			if (SetPropertyUtility.SetClass(ref m_GlobalFontAsset, value))
			{
				SetGlobalFontAsset(m_GlobalFontAsset);
				UpdateLabel();
			}
		}
	}

	public bool onFocusSelectAll
	{
		get
		{
			return m_OnFocusSelectAll;
		}
		set
		{
			m_OnFocusSelectAll = value;
		}
	}

	public bool resetOnDeActivation
	{
		get
		{
			return m_ResetOnDeActivation;
		}
		set
		{
			m_ResetOnDeActivation = value;
		}
	}

	public bool unFocusOnSubmit
	{
		get
		{
			return m_UnFocusOnSubmit;
		}
		set
		{
			m_UnFocusOnSubmit = value;
		}
	}

	public bool restoreOriginalTextOnEscape
	{
		get
		{
			return m_RestoreOriginalTextOnEscape;
		}
		set
		{
			m_RestoreOriginalTextOnEscape = value;
		}
	}

	public bool isRichTextEditingAllowed
	{
		get
		{
			return m_isRichTextEditingAllowed;
		}
		set
		{
			m_isRichTextEditingAllowed = value;
		}
	}

	public ContentType contentType
	{
		get
		{
			return m_ContentType;
		}
		set
		{
			if (SetPropertyUtility.SetStruct(ref m_ContentType, value))
			{
				EnforceContentType();
			}
		}
	}

	public LineType lineType
	{
		get
		{
			return m_LineType;
		}
		set
		{
			if (SetPropertyUtility.SetStruct(ref m_LineType, value))
			{
				SetToCustomIfContentTypeIsNot(ContentType.Standard, ContentType.Autocorrected);
				SetTextComponentWrapMode();
			}
		}
	}

	public int lineLimit
	{
		get
		{
			return m_LineLimit;
		}
		set
		{
			if (m_LineType == LineType.SingleLine)
			{
				m_LineLimit = 1;
			}
			else
			{
				SetPropertyUtility.SetStruct(ref m_LineLimit, value);
			}
		}
	}

	public InputType inputType
	{
		get
		{
			return m_InputType;
		}
		set
		{
			if (SetPropertyUtility.SetStruct(ref m_InputType, value))
			{
				SetToCustom();
			}
		}
	}

	public TouchScreenKeyboardType keyboardType
	{
		get
		{
			return m_KeyboardType;
		}
		set
		{
			if (SetPropertyUtility.SetStruct(ref m_KeyboardType, value))
			{
				SetToCustom();
			}
		}
	}

	public CharacterValidation characterValidation
	{
		get
		{
			return m_CharacterValidation;
		}
		set
		{
			if (SetPropertyUtility.SetStruct(ref m_CharacterValidation, value))
			{
				SetToCustom();
			}
		}
	}

	public TMP_InputValidator inputValidator
	{
		get
		{
			return m_InputValidator;
		}
		set
		{
			if (SetPropertyUtility.SetClass(ref m_InputValidator, value))
			{
				SetToCustom(CharacterValidation.CustomValidator);
			}
		}
	}

	public bool readOnly
	{
		get
		{
			return m_ReadOnly;
		}
		set
		{
			m_ReadOnly = value;
		}
	}

	public bool richText
	{
		get
		{
			return m_RichText;
		}
		set
		{
			m_RichText = value;
			SetTextComponentRichTextMode();
		}
	}

	public bool multiLine
	{
		get
		{
			if (m_LineType != LineType.MultiLineNewline)
			{
				return lineType == LineType.MultiLineSubmit;
			}
			return true;
		}
	}

	public char asteriskChar
	{
		get
		{
			return m_AsteriskChar;
		}
		set
		{
			if (SetPropertyUtility.SetStruct(ref m_AsteriskChar, value))
			{
				UpdateLabel();
			}
		}
	}

	public bool wasCanceled => m_WasCanceled;

	protected int caretPositionInternal
	{
		get
		{
			return m_CaretPosition + compositionString.Length;
		}
		set
		{
			m_CaretPosition = value;
			ClampCaretPos(ref m_CaretPosition);
		}
	}

	protected int stringPositionInternal
	{
		get
		{
			return m_StringPosition + compositionString.Length;
		}
		set
		{
			m_StringPosition = value;
			ClampStringPos(ref m_StringPosition);
		}
	}

	protected int caretSelectPositionInternal
	{
		get
		{
			return m_CaretSelectPosition + compositionString.Length;
		}
		set
		{
			m_CaretSelectPosition = value;
			ClampCaretPos(ref m_CaretSelectPosition);
		}
	}

	protected int stringSelectPositionInternal
	{
		get
		{
			return m_StringSelectPosition + compositionString.Length;
		}
		set
		{
			m_StringSelectPosition = value;
			ClampStringPos(ref m_StringSelectPosition);
		}
	}

	private bool hasSelection => stringPositionInternal != stringSelectPositionInternal;

	public int caretPosition
	{
		get
		{
			return caretSelectPositionInternal;
		}
		set
		{
			selectionAnchorPosition = value;
			selectionFocusPosition = value;
			m_IsStringPositionDirty = true;
		}
	}

	public int selectionAnchorPosition
	{
		get
		{
			return caretPositionInternal;
		}
		set
		{
			if (compositionString.Length == 0)
			{
				caretPositionInternal = value;
				m_IsStringPositionDirty = true;
			}
		}
	}

	public int selectionFocusPosition
	{
		get
		{
			return caretSelectPositionInternal;
		}
		set
		{
			if (compositionString.Length == 0)
			{
				caretSelectPositionInternal = value;
				m_IsStringPositionDirty = true;
			}
		}
	}

	public int stringPosition
	{
		get
		{
			return stringSelectPositionInternal;
		}
		set
		{
			selectionStringAnchorPosition = value;
			selectionStringFocusPosition = value;
			m_IsCaretPositionDirty = true;
		}
	}

	public int selectionStringAnchorPosition
	{
		get
		{
			return stringPositionInternal;
		}
		set
		{
			if (compositionString.Length == 0)
			{
				stringPositionInternal = value;
				m_IsCaretPositionDirty = true;
			}
		}
	}

	public int selectionStringFocusPosition
	{
		get
		{
			return stringSelectPositionInternal;
		}
		set
		{
			if (compositionString.Length == 0)
			{
				stringSelectPositionInternal = value;
				m_IsCaretPositionDirty = true;
			}
		}
	}

	private static string clipboard
	{
		get
		{
			return GUIUtility.systemCopyBuffer;
		}
		set
		{
			GUIUtility.systemCopyBuffer = value;
		}
	}

	public virtual float minWidth => 0f;

	public virtual float preferredWidth
	{
		get
		{
			if (textComponent == null)
			{
				return 0f;
			}
			return m_TextComponent.preferredWidth + 16f + (float)m_CaretWidth + 1f;
		}
	}

	public virtual float flexibleWidth => -1f;

	public virtual float minHeight => 0f;

	public virtual float preferredHeight
	{
		get
		{
			if (textComponent == null)
			{
				return 0f;
			}
			return m_TextComponent.preferredHeight + 16f;
		}
	}

	public virtual float flexibleHeight => -1f;

	public virtual int layoutPriority => 1;

	protected TMP_InputField()
	{
		SetTextComponentWrapMode();
	}

	private bool isKeyboardUsingEvents()
	{
		RuntimePlatform platform = Application.platform;
		if (platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.Android || platform == RuntimePlatform.tvOS)
		{
			return false;
		}
		return true;
	}

	public void SetTextWithoutNotify(string input)
	{
		SetText(input, sendCallback: false);
	}

	private void SetText(string value, bool sendCallback = true)
	{
		if (!(text == value))
		{
			if (value == null)
			{
				value = "";
			}
			value = value.Replace("\0", string.Empty);
			m_Text = value;
			if (m_SoftKeyboard != null)
			{
				m_SoftKeyboard.text = m_Text;
			}
			if (m_StringPosition > m_Text.Length)
			{
				m_StringPosition = (m_StringSelectPosition = m_Text.Length);
			}
			else if (m_StringSelectPosition > m_Text.Length)
			{
				m_StringSelectPosition = m_Text.Length;
			}
			AdjustTextPositionRelativeToViewport(0f);
			m_forceRectTransformAdjustment = true;
			m_IsTextComponentUpdateRequired = true;
			UpdateLabel();
			if (sendCallback)
			{
				SendOnValueChanged();
			}
		}
	}

	protected void ClampStringPos(ref int pos)
	{
		if (pos < 0)
		{
			pos = 0;
		}
		else if (pos > text.Length)
		{
			pos = text.Length;
		}
	}

	protected void ClampCaretPos(ref int pos)
	{
		if (pos < 0)
		{
			pos = 0;
		}
		else if (pos > m_TextComponent.textInfo.characterCount - 1)
		{
			pos = m_TextComponent.textInfo.characterCount - 1;
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (m_Text == null)
		{
			m_Text = string.Empty;
		}
		if (Application.isPlaying && m_CachedInputRenderer == null && m_TextComponent != null)
		{
			m_IsDrivenByLayoutComponents = GetComponent<ILayoutController>() != null;
			GameObject gameObject = new GameObject(base.transform.name + " Input Caret", typeof(RectTransform));
			TMP_SelectionCaret tMP_SelectionCaret = gameObject.AddComponent<TMP_SelectionCaret>();
			tMP_SelectionCaret.raycastTarget = false;
			tMP_SelectionCaret.color = Color.clear;
			gameObject.hideFlags = HideFlags.DontSave;
			gameObject.transform.SetParent(m_TextComponent.transform.parent);
			gameObject.transform.SetAsFirstSibling();
			gameObject.layer = base.gameObject.layer;
			caretRectTrans = gameObject.GetComponent<RectTransform>();
			m_CachedInputRenderer = gameObject.GetComponent<CanvasRenderer>();
			m_CachedInputRenderer.SetMaterial(Graphic.defaultGraphicMaterial, Texture2D.whiteTexture);
			gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
			AssignPositioningIfNeeded();
		}
		if (m_CachedInputRenderer != null)
		{
			m_CachedInputRenderer.SetMaterial(Graphic.defaultGraphicMaterial, Texture2D.whiteTexture);
		}
		if (m_TextComponent != null)
		{
			m_TextComponent.RegisterDirtyVerticesCallback(MarkGeometryAsDirty);
			m_TextComponent.RegisterDirtyVerticesCallback(UpdateLabel);
			if (m_VerticalScrollbar != null)
			{
				m_TextComponent.ignoreRectMaskCulling = true;
				m_VerticalScrollbar.onValueChanged.AddListener(OnScrollbarValueChange);
			}
			UpdateLabel();
		}
		TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
	}

	protected override void OnDisable()
	{
		m_BlinkCoroutine = null;
		DeactivateInputField();
		if (m_TextComponent != null)
		{
			m_TextComponent.UnregisterDirtyVerticesCallback(MarkGeometryAsDirty);
			m_TextComponent.UnregisterDirtyVerticesCallback(UpdateLabel);
			if (m_VerticalScrollbar != null)
			{
				m_VerticalScrollbar.onValueChanged.RemoveListener(OnScrollbarValueChange);
			}
		}
		CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
		if (m_CachedInputRenderer != null)
		{
			m_CachedInputRenderer.Clear();
		}
		if (m_Mesh != null)
		{
			UnityEngine.Object.DestroyImmediate(m_Mesh);
		}
		m_Mesh = null;
		TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
		base.OnDisable();
	}

	private void ON_TEXT_CHANGED(UnityEngine.Object obj)
	{
		if (obj == m_TextComponent && Application.isPlaying && compositionString.Length == 0)
		{
			caretPositionInternal = GetCaretPositionFromStringIndex(stringPositionInternal);
			caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
		}
	}

	private IEnumerator CaretBlink()
	{
		m_CaretVisible = true;
		yield return null;
		while ((isFocused || m_SelectionStillActive) && m_CaretBlinkRate > 0f)
		{
			float num = 1f / m_CaretBlinkRate;
			bool flag = (Time.unscaledTime - m_BlinkStartTime) % num < num / 2f;
			if (m_CaretVisible != flag)
			{
				m_CaretVisible = flag;
				if (!hasSelection)
				{
					MarkGeometryAsDirty();
				}
			}
			yield return null;
		}
		m_BlinkCoroutine = null;
	}

	private void SetCaretVisible()
	{
		if (m_AllowInput)
		{
			m_CaretVisible = true;
			m_BlinkStartTime = Time.unscaledTime;
			SetCaretActive();
		}
	}

	private void SetCaretActive()
	{
		if (!m_AllowInput)
		{
			return;
		}
		if (m_CaretBlinkRate > 0f)
		{
			if (m_BlinkCoroutine == null)
			{
				m_BlinkCoroutine = StartCoroutine(CaretBlink());
			}
		}
		else
		{
			m_CaretVisible = true;
		}
	}

	protected void OnFocus()
	{
		if (m_OnFocusSelectAll)
		{
			SelectAll();
		}
	}

	protected void SelectAll()
	{
		m_isSelectAll = true;
		stringPositionInternal = text.Length;
		stringSelectPositionInternal = 0;
	}

	public void MoveTextEnd(bool shift)
	{
		if (m_isRichTextEditingAllowed)
		{
			int length = text.Length;
			if (shift)
			{
				stringSelectPositionInternal = length;
			}
			else
			{
				stringPositionInternal = length;
				stringSelectPositionInternal = stringPositionInternal;
			}
		}
		else
		{
			int num = m_TextComponent.textInfo.characterCount - 1;
			if (shift)
			{
				caretSelectPositionInternal = num;
				stringSelectPositionInternal = GetStringIndexFromCaretPosition(num);
			}
			else
			{
				int num2 = (caretSelectPositionInternal = num);
				caretPositionInternal = num2;
				num2 = (stringPositionInternal = GetStringIndexFromCaretPosition(num));
				stringSelectPositionInternal = num2;
			}
		}
		UpdateLabel();
	}

	public void MoveTextStart(bool shift)
	{
		if (m_isRichTextEditingAllowed)
		{
			int num = 0;
			if (shift)
			{
				stringSelectPositionInternal = num;
			}
			else
			{
				stringPositionInternal = num;
				stringSelectPositionInternal = stringPositionInternal;
			}
		}
		else
		{
			int num2 = 0;
			if (shift)
			{
				caretSelectPositionInternal = num2;
				stringSelectPositionInternal = GetStringIndexFromCaretPosition(num2);
			}
			else
			{
				int num3 = (caretSelectPositionInternal = num2);
				caretPositionInternal = num3;
				num3 = (stringPositionInternal = GetStringIndexFromCaretPosition(num2));
				stringSelectPositionInternal = num3;
			}
		}
		UpdateLabel();
	}

	public void MoveToEndOfLine(bool shift, bool ctrl)
	{
		int lineNumber = m_TextComponent.textInfo.characterInfo[caretPositionInternal].lineNumber;
		int num = (ctrl ? (m_TextComponent.textInfo.characterCount - 1) : m_TextComponent.textInfo.lineInfo[lineNumber].lastCharacterIndex);
		int index = m_TextComponent.textInfo.characterInfo[num].index;
		if (shift)
		{
			stringSelectPositionInternal = index;
			caretSelectPositionInternal = num;
		}
		else
		{
			stringPositionInternal = index;
			stringSelectPositionInternal = stringPositionInternal;
			int num2 = (caretPositionInternal = num);
			caretSelectPositionInternal = num2;
		}
		UpdateLabel();
	}

	public void MoveToStartOfLine(bool shift, bool ctrl)
	{
		int lineNumber = m_TextComponent.textInfo.characterInfo[caretPositionInternal].lineNumber;
		int num = ((!ctrl) ? m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex : 0);
		int num2 = 0;
		if (num > 0)
		{
			num2 = m_TextComponent.textInfo.characterInfo[num - 1].index + m_TextComponent.textInfo.characterInfo[num - 1].stringLength;
		}
		if (shift)
		{
			stringSelectPositionInternal = num2;
			caretSelectPositionInternal = num;
		}
		else
		{
			stringPositionInternal = num2;
			stringSelectPositionInternal = stringPositionInternal;
			int num3 = (caretPositionInternal = num);
			caretSelectPositionInternal = num3;
		}
		UpdateLabel();
	}

	private bool InPlaceEditing()
	{
		if (m_TouchKeyboardAllowsInPlaceEditing || (TouchScreenKeyboard.isSupported && (Application.platform == RuntimePlatform.MetroPlayerX86 || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerARM)))
		{
			return true;
		}
		if (TouchScreenKeyboard.isSupported && shouldHideSoftKeyboard)
		{
			return true;
		}
		if (TouchScreenKeyboard.isSupported && !shouldHideSoftKeyboard && !shouldHideMobileInput)
		{
			return false;
		}
		return true;
	}

	private void UpdateStringPositionFromKeyboard()
	{
		RangeInt selection = m_SoftKeyboard.selection;
		if (selection.start != 0 || selection.length != 0)
		{
			int start = selection.start;
			int end = selection.end;
			bool flag = false;
			if (stringPositionInternal != start)
			{
				flag = true;
				stringPositionInternal = start;
				caretPositionInternal = GetCaretPositionFromStringIndex(stringPositionInternal);
			}
			if (stringSelectPositionInternal != end)
			{
				stringSelectPositionInternal = end;
				flag = true;
				caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
			}
			if (flag)
			{
				m_BlinkStartTime = Time.unscaledTime;
				UpdateLabel();
			}
		}
	}

	protected virtual void LateUpdate()
	{
		if (m_ShouldActivateNextUpdate)
		{
			if (!isFocused)
			{
				ActivateInputFieldInternal();
				m_ShouldActivateNextUpdate = false;
				return;
			}
			m_ShouldActivateNextUpdate = false;
		}
		if (m_IsScrollbarUpdateRequired)
		{
			UpdateScrollbar();
			m_IsScrollbarUpdateRequired = false;
		}
		if (!isFocused && m_SelectionStillActive)
		{
			GameObject gameObject = ((EventSystem.current != null) ? EventSystem.current.currentSelectedGameObject : null);
			if (gameObject != null && gameObject != base.gameObject)
			{
				if (gameObject != m_SelectedObject)
				{
					m_SelectedObject = gameObject;
					if (gameObject.GetComponent<TMP_InputField>() != null)
					{
						m_SelectionStillActive = false;
						MarkGeometryAsDirty();
						m_SelectedObject = null;
					}
				}
				return;
			}
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				bool flag = false;
				float unscaledTime = Time.unscaledTime;
				if (m_KeyDownStartTime + m_DoubleClickDelay > unscaledTime)
				{
					flag = true;
				}
				m_KeyDownStartTime = unscaledTime;
				if (flag)
				{
					m_SelectionStillActive = false;
					MarkGeometryAsDirty();
					return;
				}
			}
		}
		if ((InPlaceEditing() && isKeyboardUsingEvents()) || !isFocused)
		{
			return;
		}
		AssignPositioningIfNeeded();
		if (m_SoftKeyboard == null || m_SoftKeyboard.status != TouchScreenKeyboard.Status.Visible)
		{
			if (m_SoftKeyboard != null)
			{
				if (!m_ReadOnly)
				{
					this.text = m_SoftKeyboard.text;
				}
				if (m_SoftKeyboard.status == TouchScreenKeyboard.Status.LostFocus)
				{
					SendTouchScreenKeyboardStatusChanged();
				}
				if (m_SoftKeyboard.status == TouchScreenKeyboard.Status.Canceled)
				{
					m_ReleaseSelection = true;
					m_WasCanceled = true;
					SendTouchScreenKeyboardStatusChanged();
				}
				if (m_SoftKeyboard.status == TouchScreenKeyboard.Status.Done)
				{
					m_ReleaseSelection = true;
					OnSubmit(null);
					SendTouchScreenKeyboardStatusChanged();
				}
			}
			OnDeselect(null);
			return;
		}
		string text = m_SoftKeyboard.text;
		if (m_Text != text)
		{
			if (m_ReadOnly)
			{
				m_SoftKeyboard.text = m_Text;
			}
			else
			{
				m_Text = "";
				for (int i = 0; i < text.Length; i++)
				{
					char c = text[i];
					if (c == '\r' || c == '\u0003')
					{
						c = '\n';
					}
					if (onValidateInput != null)
					{
						c = onValidateInput(m_Text, m_Text.Length, c);
					}
					else if (characterValidation != CharacterValidation.None)
					{
						c = Validate(m_Text, m_Text.Length, c);
					}
					if (lineType == LineType.MultiLineSubmit && c == '\n')
					{
						m_SoftKeyboard.text = m_Text;
						OnSubmit(null);
						OnDeselect(null);
						return;
					}
					if (c != 0)
					{
						m_Text += c;
					}
				}
				if (characterLimit > 0 && m_Text.Length > characterLimit)
				{
					m_Text = m_Text.Substring(0, characterLimit);
				}
				UpdateStringPositionFromKeyboard();
				if (m_Text != text)
				{
					m_SoftKeyboard.text = m_Text;
				}
				SendOnValueChangedAndUpdateLabel();
			}
		}
		else if (m_HideMobileInput && Application.platform == RuntimePlatform.Android)
		{
			UpdateStringPositionFromKeyboard();
		}
		if (m_SoftKeyboard.status != TouchScreenKeyboard.Status.Visible)
		{
			if (m_SoftKeyboard.status == TouchScreenKeyboard.Status.Canceled)
			{
				m_WasCanceled = true;
			}
			OnDeselect(null);
		}
	}

	private bool MayDrag(PointerEventData eventData)
	{
		if (IsActive() && IsInteractable() && eventData.button == PointerEventData.InputButton.Left && m_TextComponent != null)
		{
			if (m_SoftKeyboard != null && !shouldHideSoftKeyboard)
			{
				return shouldHideMobileInput;
			}
			return true;
		}
		return false;
	}

	public virtual void OnBeginDrag(PointerEventData eventData)
	{
		if (MayDrag(eventData))
		{
			m_UpdateDrag = true;
		}
	}

	public virtual void OnDrag(PointerEventData eventData)
	{
		if (!MayDrag(eventData))
		{
			return;
		}
		CaretPosition cursor;
		int cursorIndexFromPosition = TMP_TextUtilities.GetCursorIndexFromPosition(m_TextComponent, eventData.position, eventData.pressEventCamera, out cursor);
		if (m_isRichTextEditingAllowed)
		{
			switch (cursor)
			{
			case CaretPosition.Left:
				stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
				break;
			case CaretPosition.Right:
				stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
				break;
			}
		}
		else
		{
			switch (cursor)
			{
			case CaretPosition.Left:
				stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? m_TextComponent.textInfo.characterInfo[0].index : (m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength));
				break;
			case CaretPosition.Right:
				stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
				break;
			}
		}
		caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
		MarkGeometryAsDirty();
		m_DragPositionOutOfBounds = !RectTransformUtility.RectangleContainsScreenPoint(textViewport, eventData.position, eventData.pressEventCamera);
		if (m_DragPositionOutOfBounds && m_DragCoroutine == null)
		{
			m_DragCoroutine = StartCoroutine(MouseDragOutsideRect(eventData));
		}
		eventData.Use();
	}

	private IEnumerator MouseDragOutsideRect(PointerEventData eventData)
	{
		while (m_UpdateDrag && m_DragPositionOutOfBounds)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(textViewport, eventData.position, eventData.pressEventCamera, out var localPoint);
			Rect rect = textViewport.rect;
			if (multiLine)
			{
				if (localPoint.y > rect.yMax)
				{
					MoveUp(shift: true, goToFirstChar: true);
				}
				else if (localPoint.y < rect.yMin)
				{
					MoveDown(shift: true, goToLastChar: true);
				}
			}
			else if (localPoint.x < rect.xMin)
			{
				MoveLeft(shift: true, ctrl: false);
			}
			else if (localPoint.x > rect.xMax)
			{
				MoveRight(shift: true, ctrl: false);
			}
			UpdateLabel();
			float num = (multiLine ? 0.1f : 0.05f);
			if (m_WaitForSecondsRealtime == null)
			{
				m_WaitForSecondsRealtime = new WaitForSecondsRealtime(num);
			}
			else
			{
				m_WaitForSecondsRealtime.waitTime = num;
			}
			yield return m_WaitForSecondsRealtime;
		}
		m_DragCoroutine = null;
	}

	public virtual void OnEndDrag(PointerEventData eventData)
	{
		if (MayDrag(eventData))
		{
			m_UpdateDrag = false;
		}
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		if (!MayDrag(eventData))
		{
			return;
		}
		EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
		bool allowInput = m_AllowInput;
		base.OnPointerDown(eventData);
		if (!InPlaceEditing() && (m_SoftKeyboard == null || !m_SoftKeyboard.active))
		{
			OnSelect(eventData);
			return;
		}
		bool flag = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
		bool flag2 = false;
		float unscaledTime = Time.unscaledTime;
		if (m_PointerDownClickStartTime + m_DoubleClickDelay > unscaledTime)
		{
			flag2 = true;
		}
		m_PointerDownClickStartTime = unscaledTime;
		if (allowInput || !m_OnFocusSelectAll)
		{
			CaretPosition cursor = CaretPosition.Right;
			int num = 0;
			if (!string.IsNullOrEmpty(m_Text))
			{
				num = TMP_TextUtilities.GetCursorIndexFromPosition(m_TextComponent, eventData.position, eventData.pressEventCamera, out cursor);
			}
			if (flag)
			{
				if (m_isRichTextEditingAllowed)
				{
					switch (cursor)
					{
					case CaretPosition.Left:
						stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[num].index;
						break;
					case CaretPosition.Right:
						stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[num].index + m_TextComponent.textInfo.characterInfo[num].stringLength;
						break;
					}
				}
				else
				{
					switch (cursor)
					{
					case CaretPosition.Left:
						stringSelectPositionInternal = ((num == 0) ? m_TextComponent.textInfo.characterInfo[0].index : (m_TextComponent.textInfo.characterInfo[num - 1].index + m_TextComponent.textInfo.characterInfo[num - 1].stringLength));
						break;
					case CaretPosition.Right:
						stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[num].index + m_TextComponent.textInfo.characterInfo[num].stringLength;
						break;
					}
				}
			}
			else if (m_isRichTextEditingAllowed)
			{
				switch (cursor)
				{
				case CaretPosition.Left:
				{
					int num2 = (stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[num].index);
					stringPositionInternal = num2;
					break;
				}
				case CaretPosition.Right:
				{
					int num2 = (stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[num].index + m_TextComponent.textInfo.characterInfo[num].stringLength);
					stringPositionInternal = num2;
					break;
				}
				}
			}
			else
			{
				switch (cursor)
				{
				case CaretPosition.Left:
				{
					int num2 = (stringSelectPositionInternal = ((num == 0) ? m_TextComponent.textInfo.characterInfo[0].index : (m_TextComponent.textInfo.characterInfo[num - 1].index + m_TextComponent.textInfo.characterInfo[num - 1].stringLength)));
					stringPositionInternal = num2;
					break;
				}
				case CaretPosition.Right:
				{
					int num2 = (stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[num].index + m_TextComponent.textInfo.characterInfo[num].stringLength);
					stringPositionInternal = num2;
					break;
				}
				}
			}
			if (flag2)
			{
				int num6 = TMP_TextUtilities.FindIntersectingWord(m_TextComponent, eventData.position, eventData.pressEventCamera);
				if (num6 != -1)
				{
					caretPositionInternal = m_TextComponent.textInfo.wordInfo[num6].firstCharacterIndex;
					caretSelectPositionInternal = m_TextComponent.textInfo.wordInfo[num6].lastCharacterIndex + 1;
					stringPositionInternal = m_TextComponent.textInfo.characterInfo[caretPositionInternal].index;
					stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 1].index + m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 1].stringLength;
				}
				else
				{
					caretPositionInternal = num;
					caretSelectPositionInternal = caretPositionInternal + 1;
					stringPositionInternal = m_TextComponent.textInfo.characterInfo[num].index;
					stringSelectPositionInternal = stringPositionInternal + m_TextComponent.textInfo.characterInfo[num].stringLength;
				}
			}
			else
			{
				int num2 = (caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringPositionInternal));
				caretPositionInternal = num2;
			}
		}
		UpdateLabel();
		eventData.Use();
	}

	protected EditState KeyPressed(Event evt)
	{
		EventModifiers modifiers = evt.modifiers;
		bool flag = ((SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX) ? ((modifiers & EventModifiers.Command) != 0) : ((modifiers & EventModifiers.Control) != 0));
		bool flag2 = (modifiers & EventModifiers.Shift) != 0;
		bool flag3 = (modifiers & EventModifiers.Alt) != 0;
		bool flag4 = flag && !flag3 && !flag2;
		switch (evt.keyCode)
		{
		case KeyCode.Backspace:
			Backspace();
			return EditState.Continue;
		case KeyCode.Delete:
			DeleteKey();
			return EditState.Continue;
		case KeyCode.Home:
			MoveToStartOfLine(flag2, flag);
			return EditState.Continue;
		case KeyCode.End:
			MoveToEndOfLine(flag2, flag);
			return EditState.Continue;
		case KeyCode.A:
			if (flag4)
			{
				SelectAll();
				return EditState.Continue;
			}
			break;
		case KeyCode.C:
			if (flag4)
			{
				if (inputType != InputType.Password)
				{
					clipboard = GetSelectedString();
				}
				else
				{
					clipboard = "";
				}
				return EditState.Continue;
			}
			break;
		case KeyCode.V:
			if (flag4)
			{
				Append(clipboard);
				return EditState.Continue;
			}
			break;
		case KeyCode.X:
			if (flag4)
			{
				if (inputType != InputType.Password)
				{
					clipboard = GetSelectedString();
				}
				else
				{
					clipboard = "";
				}
				Delete();
				UpdateTouchKeyboardFromEditChanges();
				SendOnValueChangedAndUpdateLabel();
				return EditState.Continue;
			}
			break;
		case KeyCode.LeftArrow:
			if (textComponent.isRightToLeftText)
			{
				MoveRight(flag2, flag);
			}
			else
			{
				MoveLeft(flag2, flag);
			}
			return EditState.Continue;
		case KeyCode.RightArrow:
			if (textComponent.isRightToLeftText)
			{
				MoveLeft(flag2, flag);
			}
			else
			{
				MoveRight(flag2, flag);
			}
			return EditState.Continue;
		case KeyCode.UpArrow:
			MoveUp(flag2);
			return EditState.Continue;
		case KeyCode.DownArrow:
			MoveDown(flag2);
			return EditState.Continue;
		case KeyCode.PageUp:
			MovePageUp(flag2);
			return EditState.Continue;
		case KeyCode.PageDown:
			MovePageDown(flag2);
			return EditState.Continue;
		case KeyCode.Return:
		case KeyCode.KeypadEnter:
			if (ShouldAcceptReturn() && lineType != LineType.MultiLineNewline)
			{
				m_ReleaseSelection = true;
				return EditState.Finish;
			}
			break;
		case KeyCode.Escape:
			m_ReleaseSelection = true;
			m_WasCanceled = true;
			return EditState.Finish;
		}
		char c = evt.character;
		if (!multiLine && (c == '\t' || c == '\r' || c == '\n'))
		{
			return EditState.Continue;
		}
		if (c == '\r' || c == '\u0003')
		{
			c = '\n';
		}
		if (IsValidChar(c))
		{
			Append(c);
		}
		if (c == '\0' && compositionString.Length > 0)
		{
			UpdateLabel();
		}
		return EditState.Continue;
	}

	protected virtual bool ShouldAcceptReturn()
	{
		return true;
	}

	protected virtual bool IsValidChar(char c)
	{
		switch (c)
		{
		case '\u007f':
			return false;
		default:
			_ = 10;
			break;
		case '\t':
			break;
		}
		return true;
	}

	public void ProcessEvent(Event e)
	{
		KeyPressed(e);
	}

	public virtual void OnUpdateSelected(BaseEventData eventData)
	{
		if (!isFocused)
		{
			return;
		}
		bool flag = false;
		while (Event.PopEvent(m_ProcessingEvent))
		{
			if (m_ProcessingEvent.rawType == EventType.KeyDown)
			{
				flag = true;
				if (KeyPressed(m_ProcessingEvent) == EditState.Finish)
				{
					SendOnSubmit();
					if (unFocusOnSubmit)
					{
						DeactivateInputField();
					}
					break;
				}
			}
			EventType type = m_ProcessingEvent.type;
			if ((uint)(type - 13) <= 1u && m_ProcessingEvent.commandName == "SelectAll")
			{
				SelectAll();
				flag = true;
			}
		}
		if (flag)
		{
			UpdateLabel();
		}
		eventData.Use();
	}

	public virtual void OnScroll(PointerEventData eventData)
	{
		if (m_TextComponent.preferredHeight <= m_TextViewport.rect.height)
		{
			ExecuteEvents.ExecuteHierarchy(base.gameObject.transform.parent.gameObject, eventData, ExecuteEvents.scrollHandler);
			return;
		}
		float num = 0f - eventData.scrollDelta.y;
		m_ScrollPosition += 1f / (float)m_TextComponent.textInfo.lineCount * num * m_ScrollSensitivity;
		m_ScrollPosition = Mathf.Clamp01(m_ScrollPosition);
		AdjustTextPositionRelativeToViewport(m_ScrollPosition);
		m_AllowInput = false;
		if ((bool)m_VerticalScrollbar)
		{
			m_IsUpdatingScrollbarValues = true;
			m_VerticalScrollbar.value = m_ScrollPosition;
		}
	}

	private string GetSelectedString()
	{
		if (!hasSelection)
		{
			return "";
		}
		int num = stringPositionInternal;
		int num2 = stringSelectPositionInternal;
		if (num > num2)
		{
			int num3 = num;
			num = num2;
			num2 = num3;
		}
		return text.Substring(num, num2 - num);
	}

	private int FindNextWordBegin()
	{
		if (stringSelectPositionInternal + 1 >= text.Length)
		{
			return text.Length;
		}
		int num = text.IndexOfAny(kSeparators, stringSelectPositionInternal + 1);
		if (num == -1)
		{
			return text.Length;
		}
		return num + 1;
	}

	private void MoveRight(bool shift, bool ctrl)
	{
		int num;
		if (hasSelection && !shift)
		{
			num = (stringSelectPositionInternal = Mathf.Max(stringPositionInternal, stringSelectPositionInternal));
			stringPositionInternal = num;
			num = (caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal));
			caretPositionInternal = num;
			return;
		}
		int num3 = (ctrl ? FindNextWordBegin() : ((!m_isRichTextEditingAllowed) ? (m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal].index + m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal].stringLength) : ((stringSelectPositionInternal >= text.Length || !char.IsHighSurrogate(text[stringSelectPositionInternal])) ? (stringSelectPositionInternal + 1) : (stringSelectPositionInternal + 2))));
		if (shift)
		{
			stringSelectPositionInternal = num3;
			caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
			return;
		}
		num = (stringPositionInternal = num3);
		stringSelectPositionInternal = num;
		if (stringPositionInternal >= m_TextComponent.textInfo.characterInfo[caretPositionInternal].index + m_TextComponent.textInfo.characterInfo[caretPositionInternal].stringLength)
		{
			num = (caretPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal));
			caretSelectPositionInternal = num;
		}
	}

	private int FindPrevWordBegin()
	{
		if (stringSelectPositionInternal - 2 < 0)
		{
			return 0;
		}
		int num = text.LastIndexOfAny(kSeparators, stringSelectPositionInternal - 2);
		if (num == -1)
		{
			return 0;
		}
		return num + 1;
	}

	private void MoveLeft(bool shift, bool ctrl)
	{
		int num;
		if (hasSelection && !shift)
		{
			num = (stringSelectPositionInternal = Mathf.Min(stringPositionInternal, stringSelectPositionInternal));
			stringPositionInternal = num;
			num = (caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal));
			caretPositionInternal = num;
			return;
		}
		int num3 = (ctrl ? FindPrevWordBegin() : ((!m_isRichTextEditingAllowed) ? ((caretSelectPositionInternal < 2) ? m_TextComponent.textInfo.characterInfo[0].index : (m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 2].index + m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 2].stringLength)) : ((stringSelectPositionInternal <= 0 || !char.IsLowSurrogate(text[stringSelectPositionInternal - 1])) ? (stringSelectPositionInternal - 1) : (stringSelectPositionInternal - 2))));
		if (shift)
		{
			stringSelectPositionInternal = num3;
			caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
			return;
		}
		num = (stringPositionInternal = num3);
		stringSelectPositionInternal = num;
		if (caretPositionInternal > 0 && stringPositionInternal <= m_TextComponent.textInfo.characterInfo[caretPositionInternal - 1].index)
		{
			num = (caretPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal));
			caretSelectPositionInternal = num;
		}
	}

	private int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
	{
		if (originalPos >= m_TextComponent.textInfo.characterCount)
		{
			originalPos--;
		}
		TMP_CharacterInfo tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[originalPos];
		int lineNumber = tMP_CharacterInfo.lineNumber;
		if (lineNumber - 1 < 0)
		{
			if (!goToFirstChar)
			{
				return originalPos;
			}
			return 0;
		}
		int num = m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex - 1;
		int num2 = -1;
		float num3 = 32767f;
		float num4 = 0f;
		for (int i = m_TextComponent.textInfo.lineInfo[lineNumber - 1].firstCharacterIndex; i < num; i++)
		{
			TMP_CharacterInfo tMP_CharacterInfo2 = m_TextComponent.textInfo.characterInfo[i];
			float num5 = tMP_CharacterInfo.origin - tMP_CharacterInfo2.origin;
			float num6 = num5 / (tMP_CharacterInfo2.xAdvance - tMP_CharacterInfo2.origin);
			if (num6 >= 0f && num6 <= 1f)
			{
				if (num6 < 0.5f)
				{
					return i;
				}
				return i + 1;
			}
			num5 = Mathf.Abs(num5);
			if (num5 < num3)
			{
				num2 = i;
				num3 = num5;
				num4 = num6;
			}
		}
		if (num2 == -1)
		{
			return num;
		}
		if (num4 < 0.5f)
		{
			return num2;
		}
		return num2 + 1;
	}

	private int LineDownCharacterPosition(int originalPos, bool goToLastChar)
	{
		if (originalPos >= m_TextComponent.textInfo.characterCount)
		{
			return m_TextComponent.textInfo.characterCount - 1;
		}
		TMP_CharacterInfo tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[originalPos];
		int lineNumber = tMP_CharacterInfo.lineNumber;
		if (lineNumber + 1 >= m_TextComponent.textInfo.lineCount)
		{
			if (!goToLastChar)
			{
				return originalPos;
			}
			return m_TextComponent.textInfo.characterCount - 1;
		}
		int lastCharacterIndex = m_TextComponent.textInfo.lineInfo[lineNumber + 1].lastCharacterIndex;
		int num = -1;
		float num2 = 32767f;
		float num3 = 0f;
		for (int i = m_TextComponent.textInfo.lineInfo[lineNumber + 1].firstCharacterIndex; i < lastCharacterIndex; i++)
		{
			TMP_CharacterInfo tMP_CharacterInfo2 = m_TextComponent.textInfo.characterInfo[i];
			float num4 = tMP_CharacterInfo.origin - tMP_CharacterInfo2.origin;
			float num5 = num4 / (tMP_CharacterInfo2.xAdvance - tMP_CharacterInfo2.origin);
			if (num5 >= 0f && num5 <= 1f)
			{
				if (num5 < 0.5f)
				{
					return i;
				}
				return i + 1;
			}
			num4 = Mathf.Abs(num4);
			if (num4 < num2)
			{
				num = i;
				num2 = num4;
				num3 = num5;
			}
		}
		if (num == -1)
		{
			return lastCharacterIndex;
		}
		if (num3 < 0.5f)
		{
			return num;
		}
		return num + 1;
	}

	private int PageUpCharacterPosition(int originalPos, bool goToFirstChar)
	{
		if (originalPos >= m_TextComponent.textInfo.characterCount)
		{
			originalPos--;
		}
		TMP_CharacterInfo tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[originalPos];
		int lineNumber = tMP_CharacterInfo.lineNumber;
		if (lineNumber - 1 < 0)
		{
			if (!goToFirstChar)
			{
				return originalPos;
			}
			return 0;
		}
		float height = m_TextViewport.rect.height;
		int num = lineNumber - 1;
		while (num > 0 && !(m_TextComponent.textInfo.lineInfo[num].baseline > m_TextComponent.textInfo.lineInfo[lineNumber].baseline + height))
		{
			num--;
		}
		int lastCharacterIndex = m_TextComponent.textInfo.lineInfo[num].lastCharacterIndex;
		int num2 = -1;
		float num3 = 32767f;
		float num4 = 0f;
		for (int i = m_TextComponent.textInfo.lineInfo[num].firstCharacterIndex; i < lastCharacterIndex; i++)
		{
			TMP_CharacterInfo tMP_CharacterInfo2 = m_TextComponent.textInfo.characterInfo[i];
			float num5 = tMP_CharacterInfo.origin - tMP_CharacterInfo2.origin;
			float num6 = num5 / (tMP_CharacterInfo2.xAdvance - tMP_CharacterInfo2.origin);
			if (num6 >= 0f && num6 <= 1f)
			{
				if (num6 < 0.5f)
				{
					return i;
				}
				return i + 1;
			}
			num5 = Mathf.Abs(num5);
			if (num5 < num3)
			{
				num2 = i;
				num3 = num5;
				num4 = num6;
			}
		}
		if (num2 == -1)
		{
			return lastCharacterIndex;
		}
		if (num4 < 0.5f)
		{
			return num2;
		}
		return num2 + 1;
	}

	private int PageDownCharacterPosition(int originalPos, bool goToLastChar)
	{
		if (originalPos >= m_TextComponent.textInfo.characterCount)
		{
			return m_TextComponent.textInfo.characterCount - 1;
		}
		TMP_CharacterInfo tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[originalPos];
		int lineNumber = tMP_CharacterInfo.lineNumber;
		if (lineNumber + 1 >= m_TextComponent.textInfo.lineCount)
		{
			if (!goToLastChar)
			{
				return originalPos;
			}
			return m_TextComponent.textInfo.characterCount - 1;
		}
		float height = m_TextViewport.rect.height;
		int i;
		for (i = lineNumber + 1; i < m_TextComponent.textInfo.lineCount - 1 && !(m_TextComponent.textInfo.lineInfo[i].baseline < m_TextComponent.textInfo.lineInfo[lineNumber].baseline - height); i++)
		{
		}
		int lastCharacterIndex = m_TextComponent.textInfo.lineInfo[i].lastCharacterIndex;
		int num = -1;
		float num2 = 32767f;
		float num3 = 0f;
		for (int j = m_TextComponent.textInfo.lineInfo[i].firstCharacterIndex; j < lastCharacterIndex; j++)
		{
			TMP_CharacterInfo tMP_CharacterInfo2 = m_TextComponent.textInfo.characterInfo[j];
			float num4 = tMP_CharacterInfo.origin - tMP_CharacterInfo2.origin;
			float num5 = num4 / (tMP_CharacterInfo2.xAdvance - tMP_CharacterInfo2.origin);
			if (num5 >= 0f && num5 <= 1f)
			{
				if (num5 < 0.5f)
				{
					return j;
				}
				return j + 1;
			}
			num4 = Mathf.Abs(num4);
			if (num4 < num2)
			{
				num = j;
				num2 = num4;
				num3 = num5;
			}
		}
		if (num == -1)
		{
			return lastCharacterIndex;
		}
		if (num3 < 0.5f)
		{
			return num;
		}
		return num + 1;
	}

	private void MoveDown(bool shift)
	{
		MoveDown(shift, goToLastChar: true);
	}

	protected virtual void MoveDown(bool shift, bool goToLastChar)
	{
		int num;
		if (hasSelection && !shift)
		{
			num = (caretSelectPositionInternal = Mathf.Max(caretPositionInternal, caretSelectPositionInternal));
			caretPositionInternal = num;
		}
		int num3 = (multiLine ? LineDownCharacterPosition(caretSelectPositionInternal, goToLastChar) : (m_TextComponent.textInfo.characterCount - 1));
		if (shift)
		{
			caretSelectPositionInternal = num3;
			stringSelectPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal);
			return;
		}
		num = (caretPositionInternal = num3);
		caretSelectPositionInternal = num;
		num = (stringPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal));
		stringSelectPositionInternal = num;
	}

	private void MoveUp(bool shift)
	{
		MoveUp(shift, goToFirstChar: true);
	}

	protected virtual void MoveUp(bool shift, bool goToFirstChar)
	{
		int num;
		if (hasSelection && !shift)
		{
			num = (caretSelectPositionInternal = Mathf.Min(caretPositionInternal, caretSelectPositionInternal));
			caretPositionInternal = num;
		}
		int num3 = (multiLine ? LineUpCharacterPosition(caretSelectPositionInternal, goToFirstChar) : 0);
		if (shift)
		{
			caretSelectPositionInternal = num3;
			stringSelectPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal);
			return;
		}
		num = (caretPositionInternal = num3);
		caretSelectPositionInternal = num;
		num = (stringPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal));
		stringSelectPositionInternal = num;
	}

	private void MovePageUp(bool shift)
	{
		MovePageUp(shift, goToFirstChar: true);
	}

	private void MovePageUp(bool shift, bool goToFirstChar)
	{
		if (hasSelection && !shift)
		{
			int num = (caretSelectPositionInternal = Mathf.Min(caretPositionInternal, caretSelectPositionInternal));
			caretPositionInternal = num;
		}
		int num3 = (multiLine ? PageUpCharacterPosition(caretSelectPositionInternal, goToFirstChar) : 0);
		if (shift)
		{
			caretSelectPositionInternal = num3;
			stringSelectPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal);
		}
		else
		{
			int num = (caretPositionInternal = num3);
			caretSelectPositionInternal = num;
			num = (stringPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal));
			stringSelectPositionInternal = num;
		}
		if (m_LineType != LineType.SingleLine)
		{
			float height = m_TextViewport.rect.height;
			float num5 = m_TextComponent.rectTransform.position.y + m_TextComponent.textBounds.max.y;
			float num6 = m_TextViewport.position.y + m_TextViewport.rect.yMax;
			height = ((num6 > num5 + height) ? height : (num6 - num5));
			m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, height);
			AssignPositioningIfNeeded();
			m_IsScrollbarUpdateRequired = true;
		}
	}

	private void MovePageDown(bool shift)
	{
		MovePageDown(shift, goToLastChar: true);
	}

	private void MovePageDown(bool shift, bool goToLastChar)
	{
		if (hasSelection && !shift)
		{
			int num = (caretSelectPositionInternal = Mathf.Max(caretPositionInternal, caretSelectPositionInternal));
			caretPositionInternal = num;
		}
		int num3 = (multiLine ? PageDownCharacterPosition(caretSelectPositionInternal, goToLastChar) : (m_TextComponent.textInfo.characterCount - 1));
		if (shift)
		{
			caretSelectPositionInternal = num3;
			stringSelectPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal);
		}
		else
		{
			int num = (caretPositionInternal = num3);
			caretSelectPositionInternal = num;
			num = (stringPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal));
			stringSelectPositionInternal = num;
		}
		if (m_LineType != LineType.SingleLine)
		{
			float height = m_TextViewport.rect.height;
			float num5 = m_TextComponent.rectTransform.position.y + m_TextComponent.textBounds.min.y;
			float num6 = m_TextViewport.position.y + m_TextViewport.rect.yMin;
			height = ((num6 > num5 + height) ? height : (num6 - num5));
			m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, height);
			AssignPositioningIfNeeded();
			m_IsScrollbarUpdateRequired = true;
		}
	}

	private void Delete()
	{
		if (m_ReadOnly || stringPositionInternal == stringSelectPositionInternal || (stringPositionInternal >= text.Length && stringSelectPositionInternal >= text.Length))
		{
			return;
		}
		if (m_isRichTextEditingAllowed || m_isSelectAll)
		{
			if (stringPositionInternal < stringSelectPositionInternal)
			{
				m_Text = text.Remove(stringPositionInternal, stringSelectPositionInternal - stringPositionInternal);
				stringSelectPositionInternal = stringPositionInternal;
			}
			else
			{
				m_Text = text.Remove(stringSelectPositionInternal, stringPositionInternal - stringSelectPositionInternal);
				stringPositionInternal = stringSelectPositionInternal;
			}
			m_isSelectAll = false;
		}
		else if (caretPositionInternal < caretSelectPositionInternal)
		{
			stringPositionInternal = m_TextComponent.textInfo.characterInfo[caretPositionInternal].index;
			stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 1].index + m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 1].stringLength;
			m_Text = text.Remove(stringPositionInternal, stringSelectPositionInternal - stringPositionInternal);
			stringSelectPositionInternal = stringPositionInternal;
			caretSelectPositionInternal = caretPositionInternal;
		}
		else
		{
			stringPositionInternal = m_TextComponent.textInfo.characterInfo[caretPositionInternal - 1].index + m_TextComponent.textInfo.characterInfo[caretPositionInternal - 1].stringLength;
			stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal].index;
			m_Text = text.Remove(stringSelectPositionInternal, stringPositionInternal - stringSelectPositionInternal);
			stringPositionInternal = stringSelectPositionInternal;
			caretPositionInternal = caretSelectPositionInternal;
		}
	}

	private void DeleteKey()
	{
		if (m_ReadOnly)
		{
			return;
		}
		if (hasSelection)
		{
			Delete();
			UpdateTouchKeyboardFromEditChanges();
			SendOnValueChangedAndUpdateLabel();
		}
		else if (m_isRichTextEditingAllowed)
		{
			if (stringPositionInternal < text.Length)
			{
				if (char.IsHighSurrogate(text[stringPositionInternal]))
				{
					m_Text = text.Remove(stringPositionInternal, 2);
				}
				else
				{
					m_Text = text.Remove(stringPositionInternal, 1);
				}
				UpdateTouchKeyboardFromEditChanges();
				SendOnValueChangedAndUpdateLabel();
			}
		}
		else if (caretPositionInternal < m_TextComponent.textInfo.characterCount - 1)
		{
			int stringLength = m_TextComponent.textInfo.characterInfo[caretPositionInternal].stringLength;
			int index = m_TextComponent.textInfo.characterInfo[caretPositionInternal].index;
			m_Text = text.Remove(index, stringLength);
			SendOnValueChangedAndUpdateLabel();
		}
	}

	private void Backspace()
	{
		if (m_ReadOnly)
		{
			return;
		}
		if (hasSelection)
		{
			Delete();
			UpdateTouchKeyboardFromEditChanges();
			SendOnValueChangedAndUpdateLabel();
			return;
		}
		if (m_isRichTextEditingAllowed)
		{
			if (stringPositionInternal > 0)
			{
				int num = 1;
				if (char.IsLowSurrogate(text[stringPositionInternal - 1]))
				{
					num = 2;
				}
				stringSelectPositionInternal = (stringPositionInternal -= num);
				m_Text = text.Remove(stringPositionInternal, num);
				caretSelectPositionInternal = --caretPositionInternal;
				m_isLastKeyBackspace = true;
				UpdateTouchKeyboardFromEditChanges();
				SendOnValueChangedAndUpdateLabel();
			}
			return;
		}
		if (caretPositionInternal > 0)
		{
			int stringLength = m_TextComponent.textInfo.characterInfo[caretPositionInternal - 1].stringLength;
			int index = m_TextComponent.textInfo.characterInfo[caretPositionInternal - 1].index;
			if (index < 0 || index + stringLength > text.Length)
			{
				return;
			}
			m_Text = text.Remove(index, stringLength);
			int num2 = (stringPositionInternal = ((caretPositionInternal < 2) ? m_TextComponent.textInfo.characterInfo[0].index : (m_TextComponent.textInfo.characterInfo[caretPositionInternal - 2].index + m_TextComponent.textInfo.characterInfo[caretPositionInternal - 2].stringLength)));
			stringSelectPositionInternal = num2;
			caretSelectPositionInternal = --caretPositionInternal;
		}
		m_isLastKeyBackspace = true;
		UpdateTouchKeyboardFromEditChanges();
		SendOnValueChangedAndUpdateLabel();
	}

	protected virtual void Append(string input)
	{
		if (m_ReadOnly || !InPlaceEditing())
		{
			return;
		}
		int i = 0;
		for (int length = input.Length; i < length; i++)
		{
			char c = input[i];
			if (c >= ' ' || c == '\t' || c == '\r' || c == '\n' || c == '\n')
			{
				Append(c);
			}
		}
	}

	protected virtual void Append(char input)
	{
		if (m_ReadOnly || !InPlaceEditing())
		{
			return;
		}
		if (onValidateInput != null)
		{
			input = onValidateInput(text, stringPositionInternal, input);
		}
		else
		{
			if (characterValidation == CharacterValidation.CustomValidator)
			{
				input = Validate(text, stringPositionInternal, input);
				if (input != 0)
				{
					SendOnValueChanged();
					UpdateLabel();
				}
				return;
			}
			if (characterValidation != CharacterValidation.None)
			{
				input = Validate(text, stringPositionInternal, input);
			}
		}
		if (input != 0)
		{
			Insert(input);
		}
	}

	private void Insert(char c)
	{
		if (m_ReadOnly)
		{
			return;
		}
		string value = c.ToString();
		Delete();
		if ((characterLimit <= 0 || text.Length < characterLimit) && (c != '\n' || !multiLine || lineLimit <= 0 || m_TextComponent.textInfo.lineCount < lineLimit))
		{
			m_Text = text.Insert(m_StringPosition, value);
			if (!char.IsHighSurrogate(c))
			{
				caretSelectPositionInternal = ++caretPositionInternal;
			}
			stringSelectPositionInternal = ++stringPositionInternal;
			UpdateTouchKeyboardFromEditChanges();
			SendOnValueChanged();
		}
	}

	private void UpdateTouchKeyboardFromEditChanges()
	{
		if (m_SoftKeyboard != null && InPlaceEditing())
		{
			m_SoftKeyboard.text = m_Text;
		}
	}

	private void SendOnValueChangedAndUpdateLabel()
	{
		UpdateLabel();
		SendOnValueChanged();
	}

	private void SendOnValueChanged()
	{
		if (onValueChanged != null)
		{
			onValueChanged.Invoke(text);
		}
	}

	protected void SendOnEndEdit()
	{
		if (onEndEdit != null)
		{
			onEndEdit.Invoke(m_Text);
		}
	}

	protected void SendOnSubmit()
	{
		if (onSubmit != null)
		{
			onSubmit.Invoke(m_Text);
		}
	}

	protected void SendOnFocus()
	{
		if (onSelect != null)
		{
			onSelect.Invoke(m_Text);
		}
	}

	protected void SendOnFocusLost()
	{
		if (onDeselect != null)
		{
			onDeselect.Invoke(m_Text);
		}
	}

	protected void SendOnTextSelection()
	{
		m_isSelected = true;
		if (onTextSelection != null)
		{
			onTextSelection.Invoke(m_Text, stringPositionInternal, stringSelectPositionInternal);
		}
	}

	protected void SendOnEndTextSelection()
	{
		if (m_isSelected)
		{
			if (onEndTextSelection != null)
			{
				onEndTextSelection.Invoke(m_Text, stringPositionInternal, stringSelectPositionInternal);
			}
			m_isSelected = false;
		}
	}

	protected void SendTouchScreenKeyboardStatusChanged()
	{
		if (onTouchScreenKeyboardStatusChanged != null)
		{
			onTouchScreenKeyboardStatusChanged.Invoke(m_SoftKeyboard.status);
		}
	}

	protected void UpdateLabel()
	{
		if (!(m_TextComponent != null) || !(m_TextComponent.font != null) || m_PreventCallback)
		{
			return;
		}
		m_PreventCallback = true;
		string inputString = ((compositionString.Length <= 0) ? this.text : (this.text.Substring(0, m_StringPosition) + compositionString + this.text.Substring(m_StringPosition)));
		if (!isRunningSubstitution && ApplyStringSubstitutions(ref inputString))
		{
			isRunningSubstitution = true;
			m_PreventCallback = false;
			float num = (float)(caretPosition + 1) / (float)m_TextComponent.text.Length;
			this.text = inputString;
			isRunningSubstitution = false;
			caretPosition = (int)(num * (float)inputString.Length);
			return;
		}
		string text = ((inputType != InputType.Password) ? inputString : new string(asteriskChar, inputString.Length));
		bool flag = string.IsNullOrEmpty(inputString);
		if (m_Placeholder != null)
		{
			m_Placeholder.enabled = flag;
		}
		if (!flag)
		{
			SetCaretVisible();
		}
		if (!OverrideTextSet(text + "\u200b"))
		{
			m_TextComponent.text = text + "\u200b";
		}
		if (m_LineLimit > 0)
		{
			m_TextComponent.ForceMeshUpdate();
			if (m_TextComponent.textInfo.lineCount > m_LineLimit)
			{
				int lastCharacterIndex = m_TextComponent.textInfo.lineInfo[m_LineLimit - 1].lastCharacterIndex;
				int num2 = m_TextComponent.textInfo.characterInfo[lastCharacterIndex].index + m_TextComponent.textInfo.characterInfo[lastCharacterIndex].stringLength;
				this.text = text.Remove(num2, text.Length - num2);
				m_TextComponent.text = this.text + "\u200b";
			}
		}
		if (m_IsTextComponentUpdateRequired)
		{
			m_IsTextComponentUpdateRequired = false;
			m_TextComponent.ForceMeshUpdate();
		}
		MarkGeometryAsDirty();
		m_IsScrollbarUpdateRequired = true;
		m_PreventCallback = false;
	}

	protected virtual bool ApplyStringSubstitutions(ref string inputString)
	{
		return false;
	}

	protected virtual bool OverrideTextSet(string textToDisplay)
	{
		return false;
	}

	private void UpdateScrollbar()
	{
		if ((bool)m_VerticalScrollbar)
		{
			float size = m_TextViewport.rect.height / m_TextComponent.preferredHeight;
			m_IsUpdatingScrollbarValues = true;
			m_VerticalScrollbar.size = size;
			float scrollPosition = (m_VerticalScrollbar.value = m_TextComponent.rectTransform.anchoredPosition.y / (m_TextComponent.preferredHeight - m_TextViewport.rect.height));
			m_ScrollPosition = scrollPosition;
		}
	}

	private void OnScrollbarValueChange(float value)
	{
		if (m_IsUpdatingScrollbarValues)
		{
			m_IsUpdatingScrollbarValues = false;
		}
		else if (!(value < 0f) && !(value > 1f))
		{
			AdjustTextPositionRelativeToViewport(value);
			m_ScrollPosition = value;
		}
	}

	private void AdjustTextPositionRelativeToViewport(float relativePosition)
	{
		if (!(m_TextViewport == null))
		{
			TMP_TextInfo textInfo = m_TextComponent.textInfo;
			if (textInfo != null && textInfo.lineInfo != null && textInfo.lineCount != 0 && textInfo.lineCount <= textInfo.lineInfo.Length)
			{
				m_TextComponent.rectTransform.anchoredPosition = new Vector2(m_TextComponent.rectTransform.anchoredPosition.x, (m_TextComponent.preferredHeight - m_TextViewport.rect.height) * relativePosition);
				AssignPositioningIfNeeded();
			}
		}
	}

	private int GetCaretPositionFromStringIndex(int stringIndex)
	{
		int characterCount = m_TextComponent.textInfo.characterCount;
		for (int i = 0; i < characterCount; i++)
		{
			if (m_TextComponent.textInfo.characterInfo[i].index >= stringIndex)
			{
				return i;
			}
		}
		return characterCount;
	}

	private int GetMinCaretPositionFromStringIndex(int stringIndex)
	{
		int characterCount = m_TextComponent.textInfo.characterCount;
		for (int i = 0; i < characterCount; i++)
		{
			if (stringIndex < m_TextComponent.textInfo.characterInfo[i].index + m_TextComponent.textInfo.characterInfo[i].stringLength)
			{
				return i;
			}
		}
		return characterCount;
	}

	private int GetMaxCaretPositionFromStringIndex(int stringIndex)
	{
		int characterCount = m_TextComponent.textInfo.characterCount;
		for (int i = 0; i < characterCount; i++)
		{
			if (m_TextComponent.textInfo.characterInfo[i].index >= stringIndex)
			{
				return i;
			}
		}
		return characterCount;
	}

	private int GetStringIndexFromCaretPosition(int caretPosition)
	{
		ClampCaretPos(ref caretPosition);
		return m_TextComponent.textInfo.characterInfo[caretPosition].index;
	}

	public void ForceLabelUpdate()
	{
		UpdateLabel();
	}

	private void MarkGeometryAsDirty()
	{
		CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
	}

	public virtual void Rebuild(CanvasUpdate update)
	{
		if (update == CanvasUpdate.LatePreRender)
		{
			UpdateGeometry();
		}
	}

	public virtual void LayoutComplete()
	{
	}

	public virtual void GraphicUpdateComplete()
	{
	}

	private void UpdateGeometry()
	{
		if (InPlaceEditing() && !(m_CachedInputRenderer == null))
		{
			OnFillVBO(mesh);
			m_CachedInputRenderer.SetMesh(mesh);
		}
	}

	private void AssignPositioningIfNeeded()
	{
		if (m_TextComponent != null && caretRectTrans != null && (caretRectTrans.localPosition != m_TextComponent.rectTransform.localPosition || caretRectTrans.localRotation != m_TextComponent.rectTransform.localRotation || caretRectTrans.localScale != m_TextComponent.rectTransform.localScale || caretRectTrans.anchorMin != m_TextComponent.rectTransform.anchorMin || caretRectTrans.anchorMax != m_TextComponent.rectTransform.anchorMax || caretRectTrans.anchoredPosition != m_TextComponent.rectTransform.anchoredPosition || caretRectTrans.sizeDelta != m_TextComponent.rectTransform.sizeDelta || caretRectTrans.pivot != m_TextComponent.rectTransform.pivot))
		{
			caretRectTrans.localPosition = m_TextComponent.rectTransform.localPosition;
			caretRectTrans.localRotation = m_TextComponent.rectTransform.localRotation;
			caretRectTrans.localScale = m_TextComponent.rectTransform.localScale;
			caretRectTrans.anchorMin = m_TextComponent.rectTransform.anchorMin;
			caretRectTrans.anchorMax = m_TextComponent.rectTransform.anchorMax;
			caretRectTrans.anchoredPosition = m_TextComponent.rectTransform.anchoredPosition;
			caretRectTrans.sizeDelta = m_TextComponent.rectTransform.sizeDelta;
			caretRectTrans.pivot = m_TextComponent.rectTransform.pivot;
		}
	}

	public void FixCaret()
	{
		caretRectTrans.localPosition = m_TextComponent.rectTransform.localPosition;
		caretRectTrans.localRotation = m_TextComponent.rectTransform.localRotation;
		caretRectTrans.localScale = m_TextComponent.rectTransform.localScale;
		caretRectTrans.anchorMin = m_TextComponent.rectTransform.anchorMin;
		caretRectTrans.anchorMax = m_TextComponent.rectTransform.anchorMax;
		caretRectTrans.anchoredPosition = m_TextComponent.rectTransform.anchoredPosition;
		caretRectTrans.sizeDelta = m_TextComponent.rectTransform.sizeDelta;
		caretRectTrans.pivot = m_TextComponent.rectTransform.pivot;
	}

	private void OnFillVBO(Mesh vbo)
	{
		using VertexHelper vertexHelper = new VertexHelper();
		if (!isFocused && !m_SelectionStillActive)
		{
			vertexHelper.FillMesh(vbo);
			return;
		}
		if (m_IsStringPositionDirty)
		{
			stringPositionInternal = GetStringIndexFromCaretPosition(m_CaretPosition);
			stringSelectPositionInternal = GetStringIndexFromCaretPosition(m_CaretSelectPosition);
			m_IsStringPositionDirty = false;
		}
		if (m_IsCaretPositionDirty)
		{
			caretPositionInternal = GetCaretPositionFromStringIndex(stringPositionInternal);
			caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
			m_IsCaretPositionDirty = false;
		}
		if (!hasSelection && !m_ReadOnly)
		{
			GenerateCaret(vertexHelper, Vector2.zero);
			SendOnEndTextSelection();
		}
		else
		{
			GenerateHightlight(vertexHelper, Vector2.zero);
			SendOnTextSelection();
		}
		vertexHelper.FillMesh(vbo);
	}

	private void GenerateCaret(VertexHelper vbo, Vector2 roundingOffset)
	{
		if (m_CaretVisible && !(m_TextComponent.canvas == null))
		{
			if (m_CursorVerts == null)
			{
				CreateCursorVerts();
			}
			float num = m_CaretWidth;
			Vector2 zero = Vector2.zero;
			float num2 = 0f;
			int lineNumber = m_TextComponent.textInfo.characterInfo[caretPositionInternal].lineNumber;
			TMP_CharacterInfo tMP_CharacterInfo;
			if (caretPositionInternal == m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex)
			{
				tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[caretPositionInternal];
				zero = new Vector2(tMP_CharacterInfo.origin, tMP_CharacterInfo.descender);
				num2 = tMP_CharacterInfo.ascender - tMP_CharacterInfo.descender;
			}
			else
			{
				tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[caretPositionInternal - 1];
				zero = new Vector2(tMP_CharacterInfo.xAdvance, tMP_CharacterInfo.descender);
				num2 = tMP_CharacterInfo.ascender - tMP_CharacterInfo.descender;
			}
			if (m_SoftKeyboard != null)
			{
				m_SoftKeyboard.selection = new RangeInt(stringPositionInternal, 0);
			}
			if ((isFocused && zero != m_LastPosition) || m_forceRectTransformAdjustment)
			{
				AdjustRectTransformRelativeToViewport(zero, num2, tMP_CharacterInfo.isVisible);
			}
			m_LastPosition = zero;
			float num3 = zero.y + num2;
			float y = num3 - num2;
			float scaleFactor = m_TextComponent.canvas.scaleFactor;
			m_CursorVerts[0].position = new Vector3(zero.x, y, 0f);
			m_CursorVerts[1].position = new Vector3(zero.x, num3, 0f);
			m_CursorVerts[2].position = new Vector3(zero.x + (num + 1f) / scaleFactor, num3, 0f);
			m_CursorVerts[3].position = new Vector3(zero.x + (num + 1f) / scaleFactor, y, 0f);
			m_CursorVerts[0].color = caretColor;
			m_CursorVerts[1].color = caretColor;
			m_CursorVerts[2].color = caretColor;
			m_CursorVerts[3].color = caretColor;
			vbo.AddUIVertexQuad(m_CursorVerts);
			int height = Screen.height;
			zero.y = (float)height - zero.y;
			inputSystem.compositionCursorPos = zero;
		}
	}

	private void CreateCursorVerts()
	{
		m_CursorVerts = new UIVertex[4];
		for (int i = 0; i < m_CursorVerts.Length; i++)
		{
			m_CursorVerts[i] = UIVertex.simpleVert;
			m_CursorVerts[i].uv0 = Vector2.zero;
		}
	}

	private void GenerateHightlight(VertexHelper vbo, Vector2 roundingOffset)
	{
		TMP_TextInfo textInfo = m_TextComponent.textInfo;
		if (textInfo == null || textInfo.characterCount == 0)
		{
			return;
		}
		caretPositionInternal = (m_CaretPosition = GetCaretPositionFromStringIndex(stringPositionInternal));
		caretSelectPositionInternal = (m_CaretSelectPosition = GetCaretPositionFromStringIndex(stringSelectPositionInternal));
		caretPositionInternal = Mathf.Clamp(caretPositionInternal, 0, textInfo.characterInfo.Length - 1);
		caretSelectPositionInternal = Mathf.Clamp(caretSelectPositionInternal, 0, textInfo.characterInfo.Length - 1);
		if (m_SoftKeyboard != null)
		{
			int num = ((caretPositionInternal < caretSelectPositionInternal) ? textInfo.characterInfo[caretPositionInternal].index : textInfo.characterInfo[caretSelectPositionInternal].index);
			int length = ((caretPositionInternal < caretSelectPositionInternal) ? (stringSelectPositionInternal - num) : (stringPositionInternal - num));
			m_SoftKeyboard.selection = new RangeInt(num, length);
		}
		float num2 = 0f;
		Vector2 startPosition;
		if (caretSelectPositionInternal < textInfo.characterCount)
		{
			startPosition = new Vector2(textInfo.characterInfo[caretSelectPositionInternal].origin, textInfo.characterInfo[caretSelectPositionInternal].descender);
			num2 = textInfo.characterInfo[caretSelectPositionInternal].ascender - textInfo.characterInfo[caretSelectPositionInternal].descender;
		}
		else
		{
			startPosition = new Vector2(textInfo.characterInfo[caretSelectPositionInternal - 1].xAdvance, textInfo.characterInfo[caretSelectPositionInternal - 1].descender);
			num2 = textInfo.characterInfo[caretSelectPositionInternal - 1].ascender - textInfo.characterInfo[caretSelectPositionInternal - 1].descender;
		}
		AdjustRectTransformRelativeToViewport(startPosition, num2, isCharVisible: true);
		int num3 = Mathf.Max(0, caretPositionInternal);
		int num4 = Mathf.Max(0, caretSelectPositionInternal);
		if (num3 > num4)
		{
			int num5 = num3;
			num3 = num4;
			num4 = num5;
		}
		num4--;
		if (num3 >= textInfo.characterInfo.Length || num4 < 0 || num4 >= textInfo.characterInfo.Length)
		{
			return;
		}
		int num6 = textInfo.characterInfo[num3].lineNumber;
		int lastCharacterIndex = textInfo.lineInfo[num6].lastCharacterIndex;
		UIVertex simpleVert = UIVertex.simpleVert;
		simpleVert.uv0 = Vector2.zero;
		simpleVert.color = selectionColor;
		for (int i = num3; i <= num4 && i < textInfo.characterCount; i++)
		{
			if (i == lastCharacterIndex || i == num4)
			{
				TMP_CharacterInfo tMP_CharacterInfo = textInfo.characterInfo[num3];
				TMP_CharacterInfo tMP_CharacterInfo2 = textInfo.characterInfo[i];
				if (i > 0 && tMP_CharacterInfo2.character == '\n' && textInfo.characterInfo[i - 1].character == '\r')
				{
					tMP_CharacterInfo2 = textInfo.characterInfo[i - 1];
				}
				Vector2 vector = new Vector2(tMP_CharacterInfo.origin, textInfo.lineInfo[num6].ascender);
				Vector2 vector2 = new Vector2(tMP_CharacterInfo2.xAdvance, textInfo.lineInfo[num6].descender);
				int currentVertCount = vbo.currentVertCount;
				simpleVert.position = new Vector3(vector.x, vector2.y, 0f);
				vbo.AddVert(simpleVert);
				simpleVert.position = new Vector3(vector2.x, vector2.y, 0f);
				vbo.AddVert(simpleVert);
				simpleVert.position = new Vector3(vector2.x, vector.y, 0f);
				vbo.AddVert(simpleVert);
				simpleVert.position = new Vector3(vector.x, vector.y, 0f);
				vbo.AddVert(simpleVert);
				vbo.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
				vbo.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
				num3 = i + 1;
				num6++;
				if (num6 < textInfo.lineCount)
				{
					lastCharacterIndex = textInfo.lineInfo[num6].lastCharacterIndex;
				}
			}
		}
		m_IsScrollbarUpdateRequired = true;
	}

	private void AdjustRectTransformRelativeToViewport(Vector2 startPosition, float height, bool isCharVisible)
	{
		if (m_TextViewport == null || m_IsDrivenByLayoutComponents)
		{
			return;
		}
		float xMin = m_TextViewport.rect.xMin;
		float xMax = m_TextViewport.rect.xMax;
		float num = xMax - (m_TextComponent.rectTransform.anchoredPosition.x + startPosition.x + m_TextComponent.margin.z + (float)m_CaretWidth);
		if (num < 0f && (!multiLine || (multiLine && isCharVisible)))
		{
			m_TextComponent.rectTransform.anchoredPosition += new Vector2(num, 0f);
			AssignPositioningIfNeeded();
		}
		float num2 = m_TextComponent.rectTransform.anchoredPosition.x + startPosition.x - m_TextComponent.margin.x - xMin;
		if (num2 < 0f)
		{
			m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f - num2, 0f);
			AssignPositioningIfNeeded();
		}
		if (m_LineType != LineType.SingleLine)
		{
			float num3 = m_TextViewport.rect.yMax - (m_TextComponent.rectTransform.anchoredPosition.y + startPosition.y + height);
			if (num3 < -0.0001f)
			{
				m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, num3);
				AssignPositioningIfNeeded();
				m_IsScrollbarUpdateRequired = true;
			}
			float num4 = m_TextComponent.rectTransform.anchoredPosition.y + startPosition.y - m_TextViewport.rect.yMin;
			if (num4 < 0f)
			{
				m_TextComponent.rectTransform.anchoredPosition -= new Vector2(0f, num4);
				AssignPositioningIfNeeded();
				m_IsScrollbarUpdateRequired = true;
			}
		}
		if (m_isLastKeyBackspace)
		{
			float num5 = m_TextComponent.rectTransform.anchoredPosition.x + m_TextComponent.textInfo.characterInfo[0].origin - m_TextComponent.margin.x;
			float num6 = m_TextComponent.rectTransform.anchoredPosition.x + m_TextComponent.textInfo.characterInfo[m_TextComponent.textInfo.characterCount - 1].origin + m_TextComponent.margin.z;
			if (m_TextComponent.rectTransform.anchoredPosition.x + startPosition.x <= xMin + 0.0001f)
			{
				if (num5 < xMin)
				{
					float x = Mathf.Min((xMax - xMin) / 2f, xMin - num5);
					m_TextComponent.rectTransform.anchoredPosition += new Vector2(x, 0f);
					AssignPositioningIfNeeded();
				}
			}
			else if (num6 < xMax && num5 < xMin)
			{
				float x2 = Mathf.Min(xMax - num6, xMin - num5);
				m_TextComponent.rectTransform.anchoredPosition += new Vector2(x2, 0f);
				AssignPositioningIfNeeded();
			}
			m_isLastKeyBackspace = false;
		}
		m_forceRectTransformAdjustment = false;
	}

	protected char Validate(string text, int pos, char ch)
	{
		if (characterValidation == CharacterValidation.None || !base.enabled)
		{
			return ch;
		}
		if (characterValidation == CharacterValidation.Integer || characterValidation == CharacterValidation.Decimal || characterValidation == CharacterValidation.UnsignedInteger || characterValidation == CharacterValidation.UnsignedDecimal)
		{
			bool flag = pos == 0 && text.Length > 0 && text[0] == '-';
			bool flag2 = stringPositionInternal == 0 || stringSelectPositionInternal == 0;
			if (characterValidation == CharacterValidation.UnsignedInteger || characterValidation == CharacterValidation.UnsignedDecimal)
			{
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
				if (characterValidation == CharacterValidation.UnsignedDecimal && ch == '.' && !text.Contains("."))
				{
					return ch;
				}
			}
			else if (!flag)
			{
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
				if (ch == '-' && (pos == 0 || flag2))
				{
					return ch;
				}
				if (ch == '.' && characterValidation == CharacterValidation.Decimal && !text.Contains("."))
				{
					return ch;
				}
			}
		}
		else if (characterValidation == CharacterValidation.Digit)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (characterValidation == CharacterValidation.Alphanumeric)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch;
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (characterValidation == CharacterValidation.Name)
		{
			char c = ((text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
			char c2 = ((text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
			if (char.IsLetter(ch))
			{
				if (char.IsLower(ch) && c == ' ')
				{
					return char.ToUpper(ch);
				}
				if (char.IsUpper(ch) && c != ' ' && c != '\'')
				{
					return char.ToLower(ch);
				}
				return ch;
			}
			switch (ch)
			{
			case '\'':
				if (c != ' ' && c != '\'' && c2 != '\'' && !text.Contains("'"))
				{
					return ch;
				}
				break;
			case ' ':
				if (c != ' ' && c != '\'' && c2 != ' ' && c2 != '\'')
				{
					return ch;
				}
				break;
			}
		}
		else if (characterValidation == CharacterValidation.EmailAddress)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch;
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '@' && text.IndexOf('@') == -1)
			{
				return ch;
			}
			if ("!#$%&'*+-/=?^_`{|}~".IndexOf(ch) != -1)
			{
				return ch;
			}
			if (ch == '.')
			{
				char num = ((text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
				char c3 = ((text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
				if (num != '.' && c3 != '.')
				{
					return ch;
				}
			}
		}
		else if (characterValidation == CharacterValidation.Regex)
		{
			if (Regex.IsMatch(ch.ToString(), m_RegexValue))
			{
				return ch;
			}
		}
		else if (characterValidation == CharacterValidation.CustomValidator && m_InputValidator != null)
		{
			char result = m_InputValidator.Validate(ref text, ref pos, ch);
			m_Text = text;
			int num2 = (stringPositionInternal = pos);
			stringSelectPositionInternal = num2;
			return result;
		}
		return '\0';
	}

	public void ActivateInputField()
	{
		if (!(m_TextComponent == null) && !(m_TextComponent.font == null) && IsActive() && IsInteractable())
		{
			if (isFocused && m_SoftKeyboard != null && !m_SoftKeyboard.active)
			{
				m_SoftKeyboard.active = true;
				m_SoftKeyboard.text = m_Text;
			}
			m_ShouldActivateNextUpdate = true;
		}
	}

	private void ActivateInputFieldInternal()
	{
		if (EventSystem.current == null)
		{
			return;
		}
		if (EventSystem.current.currentSelectedGameObject != base.gameObject)
		{
			EventSystem.current.SetSelectedGameObject(base.gameObject);
		}
		if (TouchScreenKeyboard.isSupported && !shouldHideSoftKeyboard)
		{
			if (inputSystem.touchSupported)
			{
				TouchScreenKeyboard.hideInput = shouldHideMobileInput;
			}
			if (!shouldHideSoftKeyboard && !m_ReadOnly && contentType != ContentType.Custom)
			{
				m_SoftKeyboard = ((inputType == InputType.Password) ? TouchScreenKeyboard.Open(m_Text, keyboardType, autocorrection: false, multiLine, secure: true, alert: false, "", characterLimit) : TouchScreenKeyboard.Open(m_Text, keyboardType, inputType == InputType.AutoCorrect, multiLine, secure: false, alert: false, "", characterLimit));
				if (!shouldHideMobileInput)
				{
					MoveTextEnd(shift: false);
				}
				else
				{
					OnFocus();
					if (m_SoftKeyboard != null)
					{
						int length = ((stringPositionInternal < stringSelectPositionInternal) ? (stringSelectPositionInternal - stringPositionInternal) : (stringPositionInternal - stringSelectPositionInternal));
						m_SoftKeyboard.selection = new RangeInt((stringPositionInternal < stringSelectPositionInternal) ? stringPositionInternal : stringSelectPositionInternal, length);
					}
				}
			}
			m_TouchKeyboardAllowsInPlaceEditing = TouchScreenKeyboard.isInPlaceEditingAllowed;
		}
		else
		{
			if (!TouchScreenKeyboard.isSupported)
			{
				inputSystem.imeCompositionMode = IMECompositionMode.On;
			}
			OnFocus();
		}
		m_AllowInput = true;
		m_OriginalText = text;
		m_WasCanceled = false;
		SetCaretVisible();
		UpdateLabel();
		SendOnFocus();
	}

	public override void OnSelect(BaseEventData eventData)
	{
		base.OnSelect(eventData);
		SendOnFocus();
		ActivateInputField();
	}

	public virtual void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			ActivateInputField();
		}
	}

	public void OnControlClick()
	{
	}

	public void ReleaseSelection()
	{
		m_SelectionStillActive = false;
		MarkGeometryAsDirty();
	}

	public void DeactivateInputField(bool clearSelection = false)
	{
		if (!m_AllowInput)
		{
			return;
		}
		m_HasDoneFocusTransition = false;
		m_AllowInput = false;
		if (m_Placeholder != null)
		{
			m_Placeholder.enabled = string.IsNullOrEmpty(m_Text);
		}
		if (m_TextComponent != null && IsInteractable())
		{
			if (m_WasCanceled && m_RestoreOriginalTextOnEscape)
			{
				text = m_OriginalText;
			}
			if (m_SoftKeyboard != null)
			{
				m_SoftKeyboard.active = false;
				m_SoftKeyboard = null;
			}
			m_SelectionStillActive = true;
			if (m_ResetOnDeActivation || m_ReleaseSelection)
			{
				m_SelectionStillActive = false;
				m_ReleaseSelection = false;
				m_SelectedObject = null;
			}
			SendOnEndEdit();
			SendOnEndTextSelection();
			inputSystem.imeCompositionMode = IMECompositionMode.Auto;
		}
		MarkGeometryAsDirty();
		m_IsScrollbarUpdateRequired = true;
	}

	public override void OnDeselect(BaseEventData eventData)
	{
		DeactivateInputField();
		base.OnDeselect(eventData);
		SendOnFocusLost();
	}

	public virtual void OnSubmit(BaseEventData eventData)
	{
		if (IsActive() && IsInteractable())
		{
			if (!isFocused)
			{
				m_ShouldActivateNextUpdate = true;
			}
			SendOnSubmit();
		}
	}

	private void EnforceContentType()
	{
		switch (contentType)
		{
		case ContentType.Standard:
			m_InputType = InputType.Standard;
			m_KeyboardType = TouchScreenKeyboardType.Default;
			m_CharacterValidation = CharacterValidation.None;
			break;
		case ContentType.Autocorrected:
			m_InputType = InputType.AutoCorrect;
			m_KeyboardType = TouchScreenKeyboardType.Default;
			m_CharacterValidation = CharacterValidation.None;
			break;
		case ContentType.IntegerNumber:
			m_LineType = LineType.SingleLine;
			m_InputType = InputType.Standard;
			m_KeyboardType = TouchScreenKeyboardType.NumberPad;
			m_CharacterValidation = CharacterValidation.Integer;
			break;
		case ContentType.UnsignedIntegerNumber:
			m_LineType = LineType.SingleLine;
			m_InputType = InputType.Standard;
			m_KeyboardType = TouchScreenKeyboardType.NumberPad;
			m_CharacterValidation = CharacterValidation.UnsignedInteger;
			break;
		case ContentType.DecimalNumber:
			m_LineType = LineType.SingleLine;
			m_InputType = InputType.Standard;
			m_KeyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
			m_CharacterValidation = CharacterValidation.Decimal;
			break;
		case ContentType.UnsignedDecimalNumber:
			m_LineType = LineType.SingleLine;
			m_InputType = InputType.Standard;
			m_KeyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
			m_CharacterValidation = CharacterValidation.UnsignedDecimal;
			break;
		case ContentType.Alphanumeric:
			m_LineType = LineType.SingleLine;
			m_InputType = InputType.Standard;
			m_KeyboardType = TouchScreenKeyboardType.ASCIICapable;
			m_CharacterValidation = CharacterValidation.Alphanumeric;
			break;
		case ContentType.Name:
			m_LineType = LineType.SingleLine;
			m_InputType = InputType.Standard;
			m_KeyboardType = TouchScreenKeyboardType.Default;
			m_CharacterValidation = CharacterValidation.Name;
			break;
		case ContentType.EmailAddress:
			m_LineType = LineType.SingleLine;
			m_InputType = InputType.Standard;
			m_KeyboardType = TouchScreenKeyboardType.EmailAddress;
			m_CharacterValidation = CharacterValidation.EmailAddress;
			break;
		case ContentType.Password:
			m_LineType = LineType.SingleLine;
			m_InputType = InputType.Password;
			m_KeyboardType = TouchScreenKeyboardType.Default;
			m_CharacterValidation = CharacterValidation.None;
			break;
		case ContentType.Pin:
			m_LineType = LineType.SingleLine;
			m_InputType = InputType.Password;
			m_KeyboardType = TouchScreenKeyboardType.NumberPad;
			m_CharacterValidation = CharacterValidation.Digit;
			break;
		}
		SetTextComponentWrapMode();
	}

	private void SetTextComponentWrapMode()
	{
		if (!(m_TextComponent == null))
		{
			if (multiLine)
			{
				m_TextComponent.enableWordWrapping = true;
			}
			else
			{
				m_TextComponent.enableWordWrapping = false;
			}
		}
	}

	private void SetTextComponentRichTextMode()
	{
		if (!(m_TextComponent == null))
		{
			m_TextComponent.richText = m_RichText;
		}
	}

	private void SetToCustomIfContentTypeIsNot(params ContentType[] allowedContentTypes)
	{
		if (contentType == ContentType.Custom)
		{
			return;
		}
		for (int i = 0; i < allowedContentTypes.Length; i++)
		{
			if (contentType == allowedContentTypes[i])
			{
				return;
			}
		}
		contentType = ContentType.Custom;
	}

	private void SetToCustom()
	{
		if (contentType != ContentType.Custom)
		{
			contentType = ContentType.Custom;
		}
	}

	private void SetToCustom(CharacterValidation characterValidation)
	{
		if (contentType == ContentType.Custom)
		{
			characterValidation = CharacterValidation.CustomValidator;
			return;
		}
		contentType = ContentType.Custom;
		characterValidation = CharacterValidation.CustomValidator;
	}

	protected override void DoStateTransition(SelectionState state, bool instant)
	{
		if (m_HasDoneFocusTransition)
		{
			state = SelectionState.Highlighted;
		}
		else if (state == SelectionState.Pressed)
		{
			m_HasDoneFocusTransition = true;
		}
		base.DoStateTransition(state, instant);
	}

	public virtual void CalculateLayoutInputHorizontal()
	{
	}

	public virtual void CalculateLayoutInputVertical()
	{
	}

	public void SetGlobalPointSize(float pointSize)
	{
		TMP_Text tMP_Text = m_Placeholder as TMP_Text;
		if (tMP_Text != null)
		{
			tMP_Text.fontSize = pointSize;
		}
		textComponent.fontSize = pointSize;
	}

	public void SetGlobalFontAsset(TMP_FontAsset fontAsset)
	{
		TMP_Text tMP_Text = m_Placeholder as TMP_Text;
		if (tMP_Text != null)
		{
			tMP_Text.font = fontAsset;
		}
		textComponent.font = fontAsset;
	}

	Transform ICanvasElement.get_transform()
	{
		return base.transform;
	}
}
internal static class SetPropertyUtility
{
	public static bool SetColor(ref Color currentValue, Color newValue)
	{
		if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
		{
			return false;
		}
		currentValue = newValue;
		return true;
	}

	public static bool SetEquatableStruct<T>(ref T currentValue, T newValue) where T : IEquatable<T>
	{
		if (currentValue.Equals(newValue))
		{
			return false;
		}
		currentValue = newValue;
		return true;
	}

	public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
	{
		if (currentValue.Equals(newValue))
		{
			return false;
		}
		currentValue = newValue;
		return true;
	}

	public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
	{
		if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
		{
			return false;
		}
		currentValue = newValue;
		return true;
	}
}
[Serializable]
public abstract class TMP_InputValidator : ScriptableObject
{
	public abstract char Validate(ref string text, ref int pos, char ch);
}
public struct TMP_LineInfo
{
	internal int controlCharacterCount;

	public int characterCount;

	public int visibleCharacterCount;

	public int spaceCount;

	public int wordCount;

	public int firstCharacterIndex;

	public int firstVisibleCharacterIndex;

	public int lastCharacterIndex;

	public int lastVisibleCharacterIndex;

	public float length;

	public float lineHeight;

	public float ascender;

	public float baseline;

	public float descender;

	public float maxAdvance;

	public float width;

	public float marginLeft;

	public float marginRight;

	public TextAlignmentOptions alignment;

	public Extents lineExtents;
}
internal static class TMP_ListPool<T>
{
	private static readonly TMP_ObjectPool<List<T>> s_ListPool = new TMP_ObjectPool<List<T>>(null, delegate(List<T> l)
	{
		l.Clear();
	});

	public static List<T> Get()
	{
		return s_ListPool.Get();
	}

	public static void Release(List<T> toRelease)
	{
		s_ListPool.Release(toRelease);
	}
}
public static class TMP_MaterialManager
{
	private class FallbackMaterial
	{
		public int baseID;

		public Material baseMaterial;

		public long fallbackID;

		public Material fallbackMaterial;

		public int count;
	}

	private class MaskingMaterial
	{
		public Material baseMaterial;

		public Material stencilMaterial;

		public int count;

		public int stencilID;
	}

	private static List<MaskingMaterial> m_materialList;

	private static Dictionary<long, FallbackMaterial> m_fallbackMaterials;

	private static Dictionary<int, long> m_fallbackMaterialLookup;

	private static List<FallbackMaterial> m_fallbackCleanupList;

	private static bool isFallbackListDirty;

	static TMP_MaterialManager()
	{
		m_materialList = new List<MaskingMaterial>();
		m_fallbackMaterials = new Dictionary<long, FallbackMaterial>();
		m_fallbackMaterialLookup = new Dictionary<int, long>();
		m_fallbackCleanupList = new List<FallbackMaterial>();
		Camera.onPreRender = (Camera.CameraCallback)Delegate.Combine(Camera.onPreRender, new Camera.CameraCallback(OnPreRender));
		Canvas.willRenderCanvases += OnPreRenderCanvas;
	}

	private static void OnPreRender(Camera cam)
	{
		if (isFallbackListDirty)
		{
			CleanupFallbackMaterials();
			isFallbackListDirty = false;
		}
	}

	private static void OnPreRenderCanvas()
	{
		if (isFallbackListDirty)
		{
			CleanupFallbackMaterials();
			isFallbackListDirty = false;
		}
	}

	public static Material GetStencilMaterial(Material baseMaterial, int stencilID)
	{
		if (!baseMaterial.HasProperty(ShaderUtilities.ID_StencilID))
		{
			UnityEngine.Debug.LogWarning("Selected Shader does not support Stencil Masking. Please select the Distance Field or Mobile Distance Field Shader.");
			return baseMaterial;
		}
		int instanceID = baseMaterial.GetInstanceID();
		for (int i = 0; i < m_materialList.Count; i++)
		{
			if (m_materialList[i].baseMaterial.GetInstanceID() == instanceID && m_materialList[i].stencilID == stencilID)
			{
				m_materialList[i].count++;
				return m_materialList[i].stencilMaterial;
			}
		}
		Material material = new Material(baseMaterial);
		material.hideFlags = HideFlags.HideAndDontSave;
		material.shaderKeywords = baseMaterial.shaderKeywords;
		ShaderUtilities.GetShaderPropertyIDs();
		material.SetFloat(ShaderUtilities.ID_StencilID, stencilID);
		material.SetFloat(ShaderUtilities.ID_StencilComp, 4f);
		MaskingMaterial maskingMaterial = new MaskingMaterial();
		maskingMaterial.baseMaterial = baseMaterial;
		maskingMaterial.stencilMaterial = material;
		maskingMaterial.stencilID = stencilID;
		maskingMaterial.count = 1;
		m_materialList.Add(maskingMaterial);
		return material;
	}

	public static void ReleaseStencilMaterial(Material stencilMaterial)
	{
		int instanceID = stencilMaterial.GetInstanceID();
		for (int i = 0; i < m_materialList.Count; i++)
		{
			if (m_materialList[i].stencilMaterial.GetInstanceID() == instanceID)
			{
				if (m_materialList[i].count > 1)
				{
					m_materialList[i].count--;
					break;
				}
				UnityEngine.Object.DestroyImmediate(m_materialList[i].stencilMaterial);
				m_materialList.RemoveAt(i);
				stencilMaterial = null;
				break;
			}
		}
	}

	public static Material GetBaseMaterial(Material stencilMaterial)
	{
		int num = m_materialList.FindIndex((MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
		if (num == -1)
		{
			return null;
		}
		return m_materialList[num].baseMaterial;
	}

	public static Material SetStencil(Material material, int stencilID)
	{
		material.SetFloat(ShaderUtilities.ID_StencilID, stencilID);
		if (stencilID == 0)
		{
			material.SetFloat(ShaderUtilities.ID_StencilComp, 8f);
		}
		else
		{
			material.SetFloat(ShaderUtilities.ID_StencilComp, 4f);
		}
		return material;
	}

	public static void AddMaskingMaterial(Material baseMaterial, Material stencilMaterial, int stencilID)
	{
		int num = m_materialList.FindIndex((MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
		if (num == -1)
		{
			MaskingMaterial maskingMaterial = new MaskingMaterial();
			maskingMaterial.baseMaterial = baseMaterial;
			maskingMaterial.stencilMaterial = stencilMaterial;
			maskingMaterial.stencilID = stencilID;
			maskingMaterial.count = 1;
			m_materialList.Add(maskingMaterial);
		}
		else
		{
			stencilMaterial = m_materialList[num].stencilMaterial;
			m_materialList[num].count++;
		}
	}

	public static void RemoveStencilMaterial(Material stencilMaterial)
	{
		int num = m_materialList.FindIndex((MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
		if (num != -1)
		{
			m_materialList.RemoveAt(num);
		}
	}

	public static void ReleaseBaseMaterial(Material baseMaterial)
	{
		int num = m_materialList.FindIndex((MaskingMaterial item) => item.baseMaterial == baseMaterial);
		if (num == -1)
		{
			UnityEngine.Debug.Log("No Masking Material exists for " + baseMaterial.name);
		}
		else if (m_materialList[num].count > 1)
		{
			m_materialList[num].count--;
			UnityEngine.Debug.Log("Removed (1) reference to " + m_materialList[num].stencilMaterial.name + ". There are " + m_materialList[num].count + " references left.");
		}
		else
		{
			UnityEngine.Debug.Log("Removed last reference to " + m_materialList[num].stencilMaterial.name + " with ID " + m_materialList[num].stencilMaterial.GetInstanceID());
			UnityEngine.Object.DestroyImmediate(m_materialList[num].stencilMaterial);
			m_materialList.RemoveAt(num);
		}
	}

	public static void ClearMaterials()
	{
		if (m_materialList.Count == 0)
		{
			UnityEngine.Debug.Log("Material List has already been cleared.");
			return;
		}
		for (int i = 0; i < m_materialList.Count; i++)
		{
			UnityEngine.Object.DestroyImmediate(m_materialList[i].stencilMaterial);
			m_materialList.RemoveAt(i);
		}
	}

	public static int GetStencilID(GameObject obj)
	{
		int num = 0;
		Transform transform = obj.transform;
		Transform transform2 = FindRootSortOverrideCanvas(transform);
		if (transform == transform2)
		{
			return num;
		}
		Transform parent = transform.parent;
		List<Mask> list = TMP_ListPool<Mask>.Get();
		while (parent != null)
		{
			parent.GetComponents(list);
			for (int i = 0; i < list.Count; i++)
			{
				Mask mask = list[i];
				if (mask != null && mask.MaskEnabled() && mask.graphic.IsActive())
				{
					num++;
					break;
				}
			}
			if (parent == transform2)
			{
				break;
			}
			parent = parent.parent;
		}
		TMP_ListPool<Mask>.Release(list);
		return Mathf.Min((1 << num) - 1, 255);
	}

	public static Material GetMaterialForRendering(MaskableGraphic graphic, Material baseMaterial)
	{
		if (baseMaterial == null)
		{
			return null;
		}
		List<IMaterialModifier> list = TMP_ListPool<IMaterialModifier>.Get();
		graphic.GetComponents(list);
		Material material = baseMaterial;
		for (int i = 0; i < list.Count; i++)
		{
			material = list[i].GetModifiedMaterial(material);
		}
		TMP_ListPool<IMaterialModifier>.Release(list);
		return material;
	}

	private static Transform FindRootSortOverrideCanvas(Transform start)
	{
		List<Canvas> list = TMP_ListPool<Canvas>.Get();
		start.GetComponentsInParent(includeInactive: false, list);
		Canvas canvas = null;
		for (int i = 0; i < list.Count; i++)
		{
			canvas = list[i];
			if (canvas.overrideSorting)
			{
				break;
			}
		}
		TMP_ListPool<Canvas>.Release(list);
		if (!(canvas != null))
		{
			return null;
		}
		return canvas.transform;
	}

	public static Material GetFallbackMaterial(Material sourceMaterial, Material targetMaterial)
	{
		int instanceID = sourceMaterial.GetInstanceID();
		Texture texture = targetMaterial.GetTexture(ShaderUtilities.ID_MainTex);
		int instanceID2 = texture.GetInstanceID();
		long num = ((long)instanceID << 32) | (uint)instanceID2;
		if (m_fallbackMaterials.TryGetValue(num, out var value))
		{
			return value.fallbackMaterial;
		}
		Material material = null;
		if (sourceMaterial.HasProperty(ShaderUtilities.ID_GradientScale) && targetMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
		{
			material = new Material(sourceMaterial);
			material.hideFlags = HideFlags.HideAndDontSave;
			material.SetTexture(ShaderUtilities.ID_MainTex, texture);
			material.SetFloat(ShaderUtilities.ID_GradientScale, targetMaterial.GetFloat(ShaderUtilities.ID_GradientScale));
			material.SetFloat(ShaderUtilities.ID_TextureWidth, targetMaterial.GetFloat(ShaderUtilities.ID_TextureWidth));
			material.SetFloat(ShaderUtilities.ID_TextureHeight, targetMaterial.GetFloat(ShaderUtilities.ID_TextureHeight));
			material.SetFloat(ShaderUtilities.ID_WeightNormal, targetMaterial.GetFloat(ShaderUtilities.ID_WeightNormal));
			material.SetFloat(ShaderUtilities.ID_WeightBold, targetMaterial.GetFloat(ShaderUtilities.ID_WeightBold));
		}
		else
		{
			material = new Material(targetMaterial);
		}
		value = new FallbackMaterial();
		value.baseID = instanceID;
		value.baseMaterial = sourceMaterial;
		value.fallbackID = num;
		value.fallbackMaterial = material;
		value.count = 0;
		m_fallbackMaterials.Add(num, value);
		m_fallbackMaterialLookup.Add(material.GetInstanceID(), num);
		return material;
	}

	public static void AddFallbackMaterialReference(Material targetMaterial)
	{
		if (!(targetMaterial == null))
		{
			int instanceID = targetMaterial.GetInstanceID();
			if (m_fallbackMaterialLookup.TryGetValue(instanceID, out var value) && m_fallbackMaterials.TryGetValue(value, out var value2))
			{
				value2.count++;
			}
		}
	}

	public static void RemoveFallbackMaterialReference(Material targetMaterial)
	{
		if (targetMaterial == null)
		{
			return;
		}
		int instanceID = targetMaterial.GetInstanceID();
		if (m_fallbackMaterialLookup.TryGetValue(instanceID, out var value) && m_fallbackMaterials.TryGetValue(value, out var value2))
		{
			value2.count--;
			if (value2.count < 1)
			{
				m_fallbackCleanupList.Add(value2);
			}
		}
	}

	public static void CleanupFallbackMaterials()
	{
		if (m_fallbackCleanupList.Count == 0)
		{
			return;
		}
		for (int i = 0; i < m_fallbackCleanupList.Count; i++)
		{
			FallbackMaterial fallbackMaterial = m_fallbackCleanupList[i];
			if (fallbackMaterial.count < 1)
			{
				Material fallbackMaterial2 = fallbackMaterial.fallbackMaterial;
				m_fallbackMaterials.Remove(fallbackMaterial.fallbackID);
				m_fallbackMaterialLookup.Remove(fallbackMaterial2.GetInstanceID());
				UnityEngine.Object.DestroyImmediate(fallbackMaterial2);
				fallbackMaterial2 = null;
			}
		}
		m_fallbackCleanupList.Clear();
	}

	public static void ReleaseFallbackMaterial(Material fallackMaterial)
	{
		if (fallackMaterial == null)
		{
			return;
		}
		int instanceID = fallackMaterial.GetInstanceID();
		if (m_fallbackMaterialLookup.TryGetValue(instanceID, out var value) && m_fallbackMaterials.TryGetValue(value, out var value2))
		{
			value2.count--;
			if (value2.count < 1)
			{
				m_fallbackCleanupList.Add(value2);
			}
		}
		isFallbackListDirty = true;
	}

	public static void CopyMaterialPresetProperties(Material source, Material destination)
	{
		if (source.HasProperty(ShaderUtilities.ID_GradientScale) && destination.HasProperty(ShaderUtilities.ID_GradientScale))
		{
			Texture texture = destination.GetTexture(ShaderUtilities.ID_MainTex);
			float value = destination.GetFloat(ShaderUtilities.ID_GradientScale);
			float value2 = destination.GetFloat(ShaderUtilities.ID_TextureWidth);
			float value3 = destination.GetFloat(ShaderUtilities.ID_TextureHeight);
			float value4 = destination.GetFloat(ShaderUtilities.ID_WeightNormal);
			float value5 = destination.GetFloat(ShaderUtilities.ID_WeightBold);
			destination.CopyPropertiesFromMaterial(source);
			destination.shaderKeywords = source.shaderKeywords;
			destination.SetTexture(ShaderUtilities.ID_MainTex, texture);
			destination.SetFloat(ShaderUtilities.ID_GradientScale, value);
			destination.SetFloat(ShaderUtilities.ID_TextureWidth, value2);
			destination.SetFloat(ShaderUtilities.ID_TextureHeight, value3);
			destination.SetFloat(ShaderUtilities.ID_WeightNormal, value4);
			destination.SetFloat(ShaderUtilities.ID_WeightBold, value5);
		}
	}
}
public enum VertexSortingOrder
{
	Normal,
	Reverse
}
public struct TMP_MeshInfo
{
	private static readonly Color32 s_DefaultColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	private static readonly Vector3 s_DefaultNormal = new Vector3(0f, 0f, -1f);

	private static readonly Vector4 s_DefaultTangent = new Vector4(-1f, 0f, 0f, 1f);

	private static readonly Bounds s_DefaultBounds = default(Bounds);

	public Mesh mesh;

	public int vertexCount;

	public Vector3[] vertices;

	public Vector3[] normals;

	public Vector4[] tangents;

	public Vector2[] uvs0;

	public Vector2[] uvs2;

	public Color32[] colors32;

	public int[] triangles;

	public TMP_MeshInfo(Mesh mesh, int size)
	{
		if (mesh == null)
		{
			mesh = new Mesh();
		}
		else
		{
			mesh.Clear();
		}
		this.mesh = mesh;
		size = Mathf.Min(size, 16383);
		int num = size * 4;
		int num2 = size * 6;
		vertexCount = 0;
		vertices = new Vector3[num];
		uvs0 = new Vector2[num];
		uvs2 = new Vector2[num];
		colors32 = new Color32[num];
		normals = new Vector3[num];
		tangents = new Vector4[num];
		triangles = new int[num2];
		int num3 = 0;
		int num4 = 0;
		while (num4 / 4 < size)
		{
			for (int i = 0; i < 4; i++)
			{
				vertices[num4 + i] = Vector3.zero;
				uvs0[num4 + i] = Vector2.zero;
				uvs2[num4 + i] = Vector2.zero;
				colors32[num4 + i] = s_DefaultColor;
				normals[num4 + i] = s_DefaultNormal;
				tangents[num4 + i] = s_DefaultTangent;
			}
			triangles[num3] = num4;
			triangles[num3 + 1] = num4 + 1;
			triangles[num3 + 2] = num4 + 2;
			triangles[num3 + 3] = num4 + 2;
			triangles[num3 + 4] = num4 + 3;
			triangles[num3 + 5] = num4;
			num4 += 4;
			num3 += 6;
		}
		this.mesh.vertices = vertices;
		this.mesh.normals = normals;
		this.mesh.tangents = tangents;
		this.mesh.triangles = triangles;
		this.mesh.bounds = s_DefaultBounds;
	}

	public TMP_MeshInfo(Mesh mesh, int size, bool isVolumetric)
	{
		if (mesh == null)
		{
			mesh = new Mesh();
		}
		else
		{
			mesh.Clear();
		}
		this.mesh = mesh;
		int num = ((!isVolumetric) ? 4 : 8);
		int num2 = ((!isVolumetric) ? 6 : 36);
		size = Mathf.Min(size, 65532 / num);
		int num3 = size * num;
		int num4 = size * num2;
		vertexCount = 0;
		vertices = new Vector3[num3];
		uvs0 = new Vector2[num3];
		uvs2 = new Vector2[num3];
		colors32 = new Color32[num3];
		normals = new Vector3[num3];
		tangents = new Vector4[num3];
		triangles = new int[num4];
		int num5 = 0;
		int num6 = 0;
		while (num5 / num < size)
		{
			for (int i = 0; i < num; i++)
			{
				vertices[num5 + i] = Vector3.zero;
				uvs0[num5 + i] = Vector2.zero;
				uvs2[num5 + i] = Vector2.zero;
				colors32[num5 + i] = s_DefaultColor;
				normals[num5 + i] = s_DefaultNormal;
				tangents[num5 + i] = s_DefaultTangent;
			}
			triangles[num6] = num5;
			triangles[num6 + 1] = num5 + 1;
			triangles[num6 + 2] = num5 + 2;
			triangles[num6 + 3] = num5 + 2;
			triangles[num6 + 4] = num5 + 3;
			triangles[num6 + 5] = num5;
			if (isVolumetric)
			{
				triangles[num6 + 6] = num5 + 4;
				triangles[num6 + 7] = num5 + 5;
				triangles[num6 + 8] = num5 + 1;
				triangles[num6 + 9] = num5 + 1;
				triangles[num6 + 10] = num5;
				triangles[num6 + 11] = num5 + 4;
				triangles[num6 + 12] = num5 + 3;
				triangles[num6 + 13] = num5 + 2;
				triangles[num6 + 14] = num5 + 6;
				triangles[num6 + 15] = num5 + 6;
				triangles[num6 + 16] = num5 + 7;
				triangles[num6 + 17] = num5 + 3;
				triangles[num6 + 18] = num5 + 1;
				triangles[num6 + 19] = num5 + 5;
				triangles[num6 + 20] = num5 + 6;
				triangles[num6 + 21] = num5 + 6;
				triangles[num6 + 22] = num5 + 2;
				triangles[num6 + 23] = num5 + 1;
				triangles[num6 + 24] = num5 + 4;
				triangles[num6 + 25] = num5;
				triangles[num6 + 26] = num5 + 3;
				triangles[num6 + 27] = num5 + 3;
				triangles[num6 + 28] = num5 + 7;
				triangles[num6 + 29] = num5 + 4;
				triangles[num6 + 30] = num5 + 7;
				triangles[num6 + 31] = num5 + 6;
				triangles[num6 + 32] = num5 + 5;
				triangles[num6 + 33] = num5 + 5;
				triangles[num6 + 34] = num5 + 4;
				triangles[num6 + 35] = num5 + 7;
			}
			num5 += num;
			num6 += num2;
		}
		this.mesh.vertices = vertices;
		this.mesh.normals = normals;
		this.mesh.tangents = tangents;
		this.mesh.triangles = triangles;
		this.mesh.bounds = s_DefaultBounds;
	}

	public void ResizeMeshInfo(int size)
	{
		size = Mathf.Min(size, 16383);
		int newSize = size * 4;
		int newSize2 = size * 6;
		int num = vertices.Length / 4;
		Array.Resize(ref vertices, newSize);
		Array.Resize(ref normals, newSize);
		Array.Resize(ref tangents, newSize);
		Array.Resize(ref uvs0, newSize);
		Array.Resize(ref uvs2, newSize);
		Array.Resize(ref colors32, newSize);
		Array.Resize(ref triangles, newSize2);
		if (size <= num)
		{
			mesh.triangles = triangles;
			mesh.vertices = vertices;
			mesh.normals = normals;
			mesh.tangents = tangents;
			return;
		}
		for (int i = num; i < size; i++)
		{
			int num2 = i * 4;
			int num3 = i * 6;
			normals[num2] = s_DefaultNormal;
			normals[1 + num2] = s_DefaultNormal;
			normals[2 + num2] = s_DefaultNormal;
			normals[3 + num2] = s_DefaultNormal;
			tangents[num2] = s_DefaultTangent;
			tangents[1 + num2] = s_DefaultTangent;
			tangents[2 + num2] = s_DefaultTangent;
			tangents[3 + num2] = s_DefaultTangent;
			triangles[num3] = num2;
			triangles[1 + num3] = 1 + num2;
			triangles[2 + num3] = 2 + num2;
			triangles[3 + num3] = 2 + num2;
			triangles[4 + num3] = 3 + num2;
			triangles[5 + num3] = num2;
		}
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.tangents = tangents;
		mesh.triangles = triangles;
	}

	public void ResizeMeshInfo(int size, bool isVolumetric)
	{
		int num = ((!isVolumetric) ? 4 : 8);
		int num2 = ((!isVolumetric) ? 6 : 36);
		size = Mathf.Min(size, 65532 / num);
		int newSize = size * num;
		int newSize2 = size * num2;
		int num3 = vertices.Length / num;
		Array.Resize(ref vertices, newSize);
		Array.Resize(ref normals, newSize);
		Array.Resize(ref tangents, newSize);
		Array.Resize(ref uvs0, newSize);
		Array.Resize(ref uvs2, newSize);
		Array.Resize(ref colors32, newSize);
		Array.Resize(ref triangles, newSize2);
		if (size <= num3)
		{
			mesh.triangles = triangles;
			mesh.vertices = vertices;
			mesh.normals = normals;
			mesh.tangents = tangents;
			return;
		}
		for (int i = num3; i < size; i++)
		{
			int num4 = i * num;
			int num5 = i * num2;
			normals[num4] = s_DefaultNormal;
			normals[1 + num4] = s_DefaultNormal;
			normals[2 + num4] = s_DefaultNormal;
			normals[3 + num4] = s_DefaultNormal;
			tangents[num4] = s_DefaultTangent;
			tangents[1 + num4] = s_DefaultTangent;
			tangents[2 + num4] = s_DefaultTangent;
			tangents[3 + num4] = s_DefaultTangent;
			if (isVolumetric)
			{
				normals[4 + num4] = s_DefaultNormal;
				normals[5 + num4] = s_DefaultNormal;
				normals[6 + num4] = s_DefaultNormal;
				normals[7 + num4] = s_DefaultNormal;
				tangents[4 + num4] = s_DefaultTangent;
				tangents[5 + num4] = s_DefaultTangent;
				tangents[6 + num4] = s_DefaultTangent;
				tangents[7 + num4] = s_DefaultTangent;
			}
			triangles[num5] = num4;
			triangles[1 + num5] = 1 + num4;
			triangles[2 + num5] = 2 + num4;
			triangles[3 + num5] = 2 + num4;
			triangles[4 + num5] = 3 + num4;
			triangles[5 + num5] = num4;
			if (isVolumetric)
			{
				triangles[num5 + 6] = num4 + 4;
				triangles[num5 + 7] = num4 + 5;
				triangles[num5 + 8] = num4 + 1;
				triangles[num5 + 9] = num4 + 1;
				triangles[num5 + 10] = num4;
				triangles[num5 + 11] = num4 + 4;
				triangles[num5 + 12] = num4 + 3;
				triangles[num5 + 13] = num4 + 2;
				triangles[num5 + 14] = num4 + 6;
				triangles[num5 + 15] = num4 + 6;
				triangles[num5 + 16] = num4 + 7;
				triangles[num5 + 17] = num4 + 3;
				triangles[num5 + 18] = num4 + 1;
				triangles[num5 + 19] = num4 + 5;
				triangles[num5 + 20] = num4 + 6;
				triangles[num5 + 21] = num4 + 6;
				triangles[num5 + 22] = num4 + 2;
				triangles[num5 + 23] = num4 + 1;
				triangles[num5 + 24] = num4 + 4;
				triangles[num5 + 25] = num4;
				triangles[num5 + 26] = num4 + 3;
				triangles[num5 + 27] = num4 + 3;
				triangles[num5 + 28] = num4 + 7;
				triangles[num5 + 29] = num4 + 4;
				triangles[num5 + 30] = num4 + 7;
				triangles[num5 + 31] = num4 + 6;
				triangles[num5 + 32] = num4 + 5;
				triangles[num5 + 33] = num4 + 5;
				triangles[num5 + 34] = num4 + 4;
				triangles[num5 + 35] = num4 + 7;
			}
		}
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.tangents = tangents;
		mesh.triangles = triangles;
	}

	public void Clear()
	{
		if (vertices != null)
		{
			Array.Clear(vertices, 0, vertices.Length);
			vertexCount = 0;
			if (mesh != null)
			{
				mesh.vertices = vertices;
			}
		}
	}

	public void Clear(bool uploadChanges)
	{
		if (vertices != null)
		{
			Array.Clear(vertices, 0, vertices.Length);
			vertexCount = 0;
			if (uploadChanges && mesh != null)
			{
				mesh.vertices = vertices;
			}
			if (mesh != null)
			{
				mesh.bounds = s_DefaultBounds;
			}
		}
	}

	public void ClearUnusedVertices()
	{
		int num = vertices.Length - vertexCount;
		if (num > 0)
		{
			Array.Clear(vertices, vertexCount, num);
		}
	}

	public void ClearUnusedVertices(int startIndex)
	{
		int num = vertices.Length - startIndex;
		if (num > 0)
		{
			Array.Clear(vertices, startIndex, num);
		}
	}

	public void ClearUnusedVertices(int startIndex, bool updateMesh)
	{
		int num = vertices.Length - startIndex;
		if (num > 0)
		{
			Array.Clear(vertices, startIndex, num);
		}
		if (updateMesh && mesh != null)
		{
			mesh.vertices = vertices;
		}
	}

	public void SortGeometry(VertexSortingOrder order)
	{
		if (order == VertexSortingOrder.Normal || order != VertexSortingOrder.Reverse)
		{
			return;
		}
		int num = vertexCount / 4;
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 4;
			int num3 = (num - i - 1) * 4;
			if (num2 < num3)
			{
				SwapVertexData(num2, num3);
			}
		}
	}

	public void SortGeometry(IList<int> sortingOrder)
	{
		int count = sortingOrder.Count;
		if (count * 4 > vertices.Length)
		{
			return;
		}
		for (int i = 0; i < count; i++)
		{
			int num;
			for (num = sortingOrder[i]; num < i; num = sortingOrder[num])
			{
			}
			if (num != i)
			{
				SwapVertexData(num * 4, i * 4);
			}
		}
	}

	public void SwapVertexData(int src, int dst)
	{
		Vector3 vector = vertices[dst];
		vertices[dst] = vertices[src];
		vertices[src] = vector;
		vector = vertices[dst + 1];
		vertices[dst + 1] = vertices[src + 1];
		vertices[src + 1] = vector;
		vector = vertices[dst + 2];
		vertices[dst + 2] = vertices[src + 2];
		vertices[src + 2] = vector;
		vector = vertices[dst + 3];
		vertices[dst + 3] = vertices[src + 3];
		vertices[src + 3] = vector;
		Vector2 vector2 = uvs0[dst];
		uvs0[dst] = uvs0[src];
		uvs0[src] = vector2;
		vector2 = uvs0[dst + 1];
		uvs0[dst + 1] = uvs0[src + 1];
		uvs0[src + 1] = vector2;
		vector2 = uvs0[dst + 2];
		uvs0[dst + 2] = uvs0[src + 2];
		uvs0[src + 2] = vector2;
		vector2 = uvs0[dst + 3];
		uvs0[dst + 3] = uvs0[src + 3];
		uvs0[src + 3] = vector2;
		vector2 = uvs2[dst];
		uvs2[dst] = uvs2[src];
		uvs2[src] = vector2;
		vector2 = uvs2[dst + 1];
		uvs2[dst + 1] = uvs2[src + 1];
		uvs2[src + 1] = vector2;
		vector2 = uvs2[dst + 2];
		uvs2[dst + 2] = uvs2[src + 2];
		uvs2[src + 2] = vector2;
		vector2 = uvs2[dst + 3];
		uvs2[dst + 3] = uvs2[src + 3];
		uvs2[src + 3] = vector2;
		Color32 color = colors32[dst];
		colors32[dst] = colors32[src];
		colors32[src] = color;
		color = colors32[dst + 1];
		colors32[dst + 1] = colors32[src + 1];
		colors32[src + 1] = color;
		color = colors32[dst + 2];
		colors32[dst + 2] = colors32[src + 2];
		colors32[src + 2] = color;
		color = colors32[dst + 3];
		colors32[dst + 3] = colors32[src + 3];
		colors32[src + 3] = color;
	}
}
internal class TMP_ObjectPool<T> where T : new()
{
	private readonly Stack<T> m_Stack = new Stack<T>();

	private readonly UnityAction<T> m_ActionOnGet;

	private readonly UnityAction<T> m_ActionOnRelease;

	public int countAll { get; private set; }

	public int countActive => countAll - countInactive;

	public int countInactive => m_Stack.Count;

	public TMP_ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
	{
		m_ActionOnGet = actionOnGet;
		m_ActionOnRelease = actionOnRelease;
	}

	public T Get()
	{
		T val;
		if (m_Stack.Count == 0)
		{
			val = new T();
			countAll++;
		}
		else
		{
			val = m_Stack.Pop();
		}
		if (m_ActionOnGet != null)
		{
			m_ActionOnGet(val);
		}
		return val;
	}

	public void Release(T element)
	{
		if (m_Stack.Count > 0 && (object)m_Stack.Peek() == (object)element)
		{
			UnityEngine.Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
		}
		if (m_ActionOnRelease != null)
		{
			m_ActionOnRelease(element);
		}
		m_Stack.Push(element);
	}
}
internal enum RichTextTag : uint
{
	BOLD = 66u,
	SLASH_BOLD = 1613u,
	ITALIC = 73u,
	SLASH_ITALIC = 1606u,
	UNDERLINE = 85u,
	SLASH_UNDERLINE = 1626u,
	STRIKETHROUGH = 83u,
	SLASH_STRIKETHROUGH = 1628u,
	COLOR = 81999901u,
	SLASH_COLOR = 1909026194u,
	SIZE = 3061285u,
	SLASH_SIZE = 58429962u,
	SPRITE = 3303439849u,
	BR = 2256u,
	STYLE = 100252951u,
	SLASH_STYLE = 1927738392u,
	FONT = 2586451u,
	SLASH_FONT = 57747708u,
	LINK = 2656128u,
	SLASH_LINK = 57686191u,
	FONT_WEIGHT = 2405071134u,
	SLASH_FONT_WEIGHT = 3536990865u,
	LIGA = 2655971u,
	SLASH_LIGA = 57686604u,
	FRAC = 2598518u,
	SLASH_FRAC = 57774681u,
	NAME = 2875623u,
	INDEX = 84268030u,
	TINT = 2960519u,
	ANIM = 2283339u,
	MATERIAL = 825491659u,
	RED = 91635u,
	GREEN = 87065851u,
	BLUE = 2457214u,
	YELLOW = 3412522628u,
	ORANGE = 3186379376u,
	PLUS = 43u,
	MINUS = 45u,
	PX = 2568u,
	PLUS_PX = 49507u,
	MINUS_PX = 47461u,
	EM = 2216u,
	PLUS_EM = 49091u,
	MINUS_EM = 46789u,
	PCT = 85031u,
	PLUS_PCT = 1634348u,
	MINUS_PCT = 1567082u,
	PERCENTAGE = 37u,
	PLUS_PERCENTAGE = 1454u,
	MINUS_PERCENTAGE = 1512u,
	TRUE = 2932022u,
	FALSE = 85422813u,
	DEFAULT = 3673993291u
}
public enum TagValueType
{
	None = 0,
	NumericalValue = 1,
	StringValue = 2,
	ColorValue = 4
}
public enum TagUnitType
{
	Pixels,
	FontUnits,
	Percentage
}
internal enum UnicodeCharacter : uint
{
	HYPHEN_MINUS = 45u,
	SOFT_HYPHEN = 173u,
	HYPHEN = 8208u,
	NON_BREAKING_HYPHEN = 8209u,
	ZERO_WIDTH_SPACE = 8203u,
	RIGHT_SINGLE_QUOTATION = 8217u,
	APOSTROPHE = 39u,
	WORD_JOINER = 8288u
}
public struct TMP_FontStyleStack
{
	public byte bold;

	public byte italic;

	public byte underline;

	public byte strikethrough;

	public byte highlight;

	public byte superscript;

	public byte subscript;

	public byte uppercase;

	public byte lowercase;

	public byte smallcaps;

	public void Clear()
	{
		bold = 0;
		italic = 0;
		underline = 0;
		strikethrough = 0;
		highlight = 0;
		superscript = 0;
		subscript = 0;
		uppercase = 0;
		lowercase = 0;
		smallcaps = 0;
	}

	public byte Add(FontStyles style)
	{
		switch (style)
		{
		case FontStyles.Bold:
			bold++;
			return bold;
		case FontStyles.Italic:
			italic++;
			return italic;
		case FontStyles.Underline:
			underline++;
			return underline;
		case FontStyles.Strikethrough:
			strikethrough++;
			return strikethrough;
		case FontStyles.Superscript:
			superscript++;
			return superscript;
		case FontStyles.Subscript:
			subscript++;
			return subscript;
		case FontStyles.Highlight:
			highlight++;
			return highlight;
		default:
			return 0;
		}
	}

	public byte Remove(FontStyles style)
	{
		switch (style)
		{
		case FontStyles.Bold:
			if (bold > 1)
			{
				bold--;
			}
			else
			{
				bold = 0;
			}
			return bold;
		case FontStyles.Italic:
			if (italic > 1)
			{
				italic--;
			}
			else
			{
				italic = 0;
			}
			return italic;
		case FontStyles.Underline:
			if (underline > 1)
			{
				underline--;
			}
			else
			{
				underline = 0;
			}
			return underline;
		case FontStyles.Strikethrough:
			if (strikethrough > 1)
			{
				strikethrough--;
			}
			else
			{
				strikethrough = 0;
			}
			return strikethrough;
		case FontStyles.Highlight:
			if (highlight > 1)
			{
				highlight--;
			}
			else
			{
				highlight = 0;
			}
			return highlight;
		case FontStyles.Superscript:
			if (superscript > 1)
			{
				superscript--;
			}
			else
			{
				superscript = 0;
			}
			return superscript;
		case FontStyles.Subscript:
			if (subscript > 1)
			{
				subscript--;
			}
			else
			{
				subscript = 0;
			}
			return subscript;
		default:
			return 0;
		}
	}
}
public struct TMP_RichTextTagStack<T>
{
	public T[] m_ItemStack;

	public int m_Index;

	private int m_Capacity;

	private T m_DefaultItem;

	private const int k_DefaultCapacity = 4;

	public TMP_RichTextTagStack(T[] tagStack)
	{
		m_ItemStack = tagStack;
		m_Capacity = tagStack.Length;
		m_Index = 0;
		m_DefaultItem = default(T);
	}

	public TMP_RichTextTagStack(int capacity)
	{
		m_ItemStack = new T[capacity];
		m_Capacity = capacity;
		m_Index = 0;
		m_DefaultItem = default(T);
	}

	public void Clear()
	{
		m_Index = 0;
	}

	public void SetDefault(T item)
	{
		m_ItemStack[0] = item;
		m_Index = 1;
	}

	public void Add(T item)
	{
		if (m_Index < m_ItemStack.Length)
		{
			m_ItemStack[m_Index] = item;
			m_Index++;
		}
	}

	public T Remove()
	{
		m_Index--;
		if (m_Index <= 0)
		{
			m_Index = 1;
			return m_ItemStack[0];
		}
		return m_ItemStack[m_Index - 1];
	}

	public void Push(T item)
	{
		if (m_Index == m_Capacity)
		{
			m_Capacity *= 2;
			if (m_Capacity == 0)
			{
				m_Capacity = 4;
			}
			Array.Resize(ref m_ItemStack, m_Capacity);
		}
		m_ItemStack[m_Index] = item;
		m_Index++;
	}

	public T Pop()
	{
		if (m_Index == 0)
		{
			return default(T);
		}
		m_Index--;
		T result = m_ItemStack[m_Index];
		m_ItemStack[m_Index] = m_DefaultItem;
		return result;
	}

	public T Peek()
	{
		if (m_Index == 0)
		{
			return m_DefaultItem;
		}
		return m_ItemStack[m_Index - 1];
	}

	public T CurrentItem()
	{
		if (m_Index > 0)
		{
			return m_ItemStack[m_Index - 1];
		}
		return m_ItemStack[0];
	}

	public T PreviousItem()
	{
		if (m_Index > 1)
		{
			return m_ItemStack[m_Index - 2];
		}
		return m_ItemStack[0];
	}
}
public class TMP_ScrollbarEventHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, ISelectHandler, IDeselectHandler
{
	public bool isSelected;

	public void OnPointerClick(PointerEventData eventData)
	{
		UnityEngine.Debug.Log("Scrollbar click...");
	}

	public void OnSelect(BaseEventData eventData)
	{
		UnityEngine.Debug.Log("Scrollbar selected");
		isSelected = true;
	}

	public void OnDeselect(BaseEventData eventData)
	{
		UnityEngine.Debug.Log("Scrollbar De-Selected");
		isSelected = false;
	}
}
public class TMP_SelectionCaret : MaskableGraphic
{
	public override void Cull(Rect clipRect, bool validRect)
	{
	}
}
[Serializable]
public class TMP_Settings : ScriptableObject
{
	public class LineBreakingTable
	{
		public Dictionary<int, char> leadingCharacters;

		public Dictionary<int, char> followingCharacters;
	}

	private static TMP_Settings s_Instance;

	[SerializeField]
	private bool m_enableWordWrapping;

	[SerializeField]
	private bool m_enableKerning;

	[SerializeField]
	private bool m_enableExtraPadding;

	[SerializeField]
	private bool m_enableTintAllSprites;

	[SerializeField]
	private bool m_EnableRaycastTarget = true;

	[SerializeField]
	private bool m_GetFontFeaturesAtRuntime = true;

	[SerializeField]
	private int m_missingGlyphCharacter;

	[SerializeField]
	private bool m_warningsDisabled;

	[SerializeField]
	private TMP_FontAsset m_defaultFontAsset;

	[SerializeField]
	private string m_defaultFontAssetPath;

	[SerializeField]
	private float m_defaultFontSize;

	[SerializeField]
	private float m_defaultAutoSizeMinRatio;

	[SerializeField]
	private float m_defaultAutoSizeMaxRatio;

	[SerializeField]
	private Vector2 m_defaultTextMeshProTextContainerSize;

	[SerializeField]
	private Vector2 m_defaultTextMeshProUITextContainerSize;

	[SerializeField]
	private bool m_autoSizeTextContainer;

	[SerializeField]
	private List<TMP_FontAsset> m_fallbackFontAssets;

	[SerializeField]
	private bool m_matchMaterialPreset;

	[SerializeField]
	private string m_defaultColorGradientPresetsPath;

	[SerializeField]
	private bool m_enableEmojiSupport;

	[SerializeField]
	private TextAsset m_leadingCharacters;

	[SerializeField]
	private TextAsset m_followingCharacters;

	[SerializeField]
	private LineBreakingTable m_linebreakingRules;

	public static string version => "1.4.0";

	public static bool enableWordWrapping => instance.m_enableWordWrapping;

	public static bool enableKerning => instance.m_enableKerning;

	public static bool enableExtraPadding => instance.m_enableExtraPadding;

	public static bool enableTintAllSprites => instance.m_enableTintAllSprites;

	public static bool enableRaycastTarget => instance.m_EnableRaycastTarget;

	public static bool getFontFeaturesAtRuntime => instance.m_GetFontFeaturesAtRuntime;

	public static int missingGlyphCharacter
	{
		get
		{
			return instance.m_missingGlyphCharacter;
		}
		set
		{
			instance.m_missingGlyphCharacter = value;
		}
	}

	public static bool warningsDisabled => instance.m_warningsDisabled;

	public static TMP_FontAsset defaultFontAsset => instance.m_defaultFontAsset;

	public static string defaultFontAssetPath => instance.m_defaultFontAssetPath;

	public static float defaultFontSize => instance.m_defaultFontSize;

	public static float defaultTextAutoSizingMinRatio => instance.m_defaultAutoSizeMinRatio;

	public static float defaultTextAutoSizingMaxRatio => instance.m_defaultAutoSizeMaxRatio;

	public static Vector2 defaultTextMeshProTextContainerSize => instance.m_defaultTextMeshProTextContainerSize;

	public static Vector2 defaultTextMeshProUITextContainerSize => instance.m_defaultTextMeshProUITextContainerSize;

	public static bool autoSizeTextContainer => instance.m_autoSizeTextContainer;

	public static List<TMP_FontAsset> fallbackFontAssets => instance.m_fallbackFontAssets;

	public static bool matchMaterialPreset => instance.m_matchMaterialPreset;

	public static string defaultColorGradientPresetsPath => instance.m_defaultColorGradientPresetsPath;

	public static bool enableEmojiSupport
	{
		get
		{
			return instance.m_enableEmojiSupport;
		}
		set
		{
			instance.m_enableEmojiSupport = value;
		}
	}

	public static TextAsset leadingCharacters => instance.m_leadingCharacters;

	public static TextAsset followingCharacters => instance.m_followingCharacters;

	public static LineBreakingTable linebreakingRules
	{
		get
		{
			if (instance.m_linebreakingRules == null)
			{
				LoadLinebreakingRules();
			}
			return instance.m_linebreakingRules;
		}
	}

	public static TMP_Settings instance
	{
		get
		{
			if (s_Instance == null)
			{
				s_Instance = Resources.Load<TMP_Settings>("TMP Settings");
			}
			return s_Instance;
		}
	}

	public static TMP_Settings LoadDefaultSettings()
	{
		if (s_Instance == null)
		{
			TMP_Settings tMP_Settings = Resources.Load<TMP_Settings>("TMP Settings");
			if (tMP_Settings != null)
			{
				s_Instance = tMP_Settings;
			}
		}
		return s_Instance;
	}

	public static TMP_Settings GetSettings()
	{
		if (instance == null)
		{
			return null;
		}
		return instance;
	}

	public static TMP_FontAsset GetFontAsset()
	{
		if (instance == null)
		{
			return null;
		}
		return instance.m_defaultFontAsset;
	}

	public static void LoadLinebreakingRules()
	{
		if (!(instance == null))
		{
			if (s_Instance.m_linebreakingRules == null)
			{
				s_Instance.m_linebreakingRules = new LineBreakingTable();
			}
			s_Instance.m_linebreakingRules.leadingCharacters = GetCharacters(s_Instance.m_leadingCharacters);
			s_Instance.m_linebreakingRules.followingCharacters = GetCharacters(s_Instance.m_followingCharacters);
		}
	}

	private static Dictionary<int, char> GetCharacters(TextAsset file)
	{
		Dictionary<int, char> dictionary = new Dictionary<int, char>();
		string text = file.text;
		foreach (char c in text)
		{
			if (!dictionary.ContainsKey(c))
			{
				dictionary.Add(c, c);
			}
		}
		return dictionary;
	}
}
public static class ShaderUtilities
{
	public static int ID_MainTex;

	public static int ID_FaceTex;

	public static int ID_FaceColor;

	public static int ID_FaceDilate;

	public static int ID_Shininess;

	public static int ID_DepthOffset;

	public static int ID_CullMode;

	public static int ID_UseSceneLighting;

	public static int ID_UnderlayColor;

	public static int ID_UnderlayOffsetX;

	public static int ID_UnderlayOffsetY;

	public static int ID_UnderlayDilate;

	public static int ID_UnderlaySoftness;

	public static int ID_WeightNormal;

	public static int ID_WeightBold;

	public static int ID_OutlineTex;

	public static int ID_OutlineWidth;

	public static int ID_OutlineSoftness;

	public static int ID_OutlineColor;

	public static int ID_Padding;

	public static int ID_GradientScale;

	public static int ID_ScaleX;

	public static int ID_ScaleY;

	public static int ID_PerspectiveFilter;

	public static int ID_Sharpness;

	public static int ID_TextureWidth;

	public static int ID_TextureHeight;

	public static int ID_BevelAmount;

	public static int ID_GlowColor;

	public static int ID_GlowOffset;

	public static int ID_GlowPower;

	public static int ID_GlowOuter;

	public static int ID_LightAngle;

	public static int ID_EnvMap;

	public static int ID_EnvMatrix;

	public static int ID_EnvMatrixRotation;

	public static int ID_MaskCoord;

	public static int ID_ClipRect;

	public static int ID_MaskSoftnessX;

	public static int ID_MaskSoftnessY;

	public static int ID_VertexOffsetX;

	public static int ID_VertexOffsetY;

	public static int ID_UseClipRect;

	public static int ID_StencilID;

	public static int ID_StencilOp;

	public static int ID_StencilComp;

	public static int ID_StencilReadMask;

	public static int ID_StencilWriteMask;

	public static int ID_ShaderFlags;

	public static int ID_ScaleRatio_A;

	public static int ID_ScaleRatio_B;

	public static int ID_ScaleRatio_C;

	public static string Keyword_Bevel;

	public static string Keyword_Glow;

	public static string Keyword_Underlay;

	public static string Keyword_Ratios;

	public static string Keyword_MASK_SOFT;

	public static string Keyword_MASK_HARD;

	public static string Keyword_MASK_TEX;

	public static string Keyword_Outline;

	public static string ShaderTag_ZTestMode;

	public static string ShaderTag_CullMode;

	private static float m_clamp;

	public static bool isInitialized;

	private static Shader k_ShaderRef_MobileSDF;

	private static Shader k_ShaderRef_MobileBitmap;

	internal static Shader ShaderRef_MobileSDF
	{
		get
		{
			if (k_ShaderRef_MobileSDF == null)
			{
				k_ShaderRef_MobileSDF = Shader.Find("TextMeshPro/Mobile/Distance Field");
			}
			return k_ShaderRef_MobileSDF;
		}
	}

	internal static Shader ShaderRef_MobileBitmap
	{
		get
		{
			if (k_ShaderRef_MobileBitmap == null)
			{
				k_ShaderRef_MobileBitmap = Shader.Find("TextMeshPro/Mobile/Bitmap");
			}
			return k_ShaderRef_MobileBitmap;
		}
	}

	static ShaderUtilities()
	{
		Keyword_Bevel = "BEVEL_ON";
		Keyword_Glow = "GLOW_ON";
		Keyword_Underlay = "UNDERLAY_ON";
		Keyword_Ratios = "RATIOS_OFF";
		Keyword_MASK_SOFT = "MASK_SOFT";
		Keyword_MASK_HARD = "MASK_HARD";
		Keyword_MASK_TEX = "MASK_TEX";
		Keyword_Outline = "OUTLINE_ON";
		ShaderTag_ZTestMode = "unity_GUIZTestMode";
		ShaderTag_CullMode = "_CullMode";
		m_clamp = 1f;
		isInitialized = false;
		GetShaderPropertyIDs();
	}

	public static void GetShaderPropertyIDs()
	{
		if (!isInitialized)
		{
			isInitialized = true;
			ID_MainTex = Shader.PropertyToID("_MainTex");
			ID_FaceTex = Shader.PropertyToID("_FaceTex");
			ID_FaceColor = Shader.PropertyToID("_FaceColor");
			ID_FaceDilate = Shader.PropertyToID("_FaceDilate");
			ID_Shininess = Shader.PropertyToID("_FaceShininess");
			ID_DepthOffset = Shader.PropertyToID("_DepthOffset");
			ID_CullMode = Shader.PropertyToID("_CullMode");
			ID_UseSceneLighting = Shader.PropertyToID("_UseSceneLighting");
			ID_UnderlayColor = Shader.PropertyToID("_UnderlayColor");
			ID_UnderlayOffsetX = Shader.PropertyToID("_UnderlayOffsetX");
			ID_UnderlayOffsetY = Shader.PropertyToID("_UnderlayOffsetY");
			ID_UnderlayDilate = Shader.PropertyToID("_UnderlayDilate");
			ID_UnderlaySoftness = Shader.PropertyToID("_UnderlaySoftness");
			ID_WeightNormal = Shader.PropertyToID("_WeightNormal");
			ID_WeightBold = Shader.PropertyToID("_WeightBold");
			ID_OutlineTex = Shader.PropertyToID("_OutlineTex");
			ID_OutlineWidth = Shader.PropertyToID("_OutlineWidth");
			ID_OutlineSoftness = Shader.PropertyToID("_OutlineSoftness");
			ID_OutlineColor = Shader.PropertyToID("_OutlineColor");
			ID_Padding = Shader.PropertyToID("_Padding");
			ID_GradientScale = Shader.PropertyToID("_GradientScale");
			ID_ScaleX = Shader.PropertyToID("_ScaleX");
			ID_ScaleY = Shader.PropertyToID("_ScaleY");
			ID_PerspectiveFilter = Shader.PropertyToID("_PerspectiveFilter");
			ID_Sharpness = Shader.PropertyToID("_Sharpness");
			ID_TextureWidth = Shader.PropertyToID("_TextureWidth");
			ID_TextureHeight = Shader.PropertyToID("_TextureHeight");
			ID_BevelAmount = Shader.PropertyToID("_Bevel");
			ID_LightAngle = Shader.PropertyToID("_LightAngle");
			ID_EnvMap = Shader.PropertyToID("_Cube");
			ID_EnvMatrix = Shader.PropertyToID("_EnvMatrix");
			ID_EnvMatrixRotation = Shader.PropertyToID("_EnvMatrixRotation");
			ID_GlowColor = Shader.PropertyToID("_GlowColor");
			ID_GlowOffset = Shader.PropertyToID("_GlowOffset");
			ID_GlowPower = Shader.PropertyToID("_GlowPower");
			ID_GlowOuter = Shader.PropertyToID("_GlowOuter");
			ID_MaskCoord = Shader.PropertyToID("_MaskCoord");
			ID_ClipRect = Shader.PropertyToID("_ClipRect");
			ID_UseClipRect = Shader.PropertyToID("_UseClipRect");
			ID_MaskSoftnessX = Shader.PropertyToID("_MaskSoftnessX");
			ID_MaskSoftnessY = Shader.PropertyToID("_MaskSoftnessY");
			ID_VertexOffsetX = Shader.PropertyToID("_VertexOffsetX");
			ID_VertexOffsetY = Shader.PropertyToID("_VertexOffsetY");
			ID_StencilID = Shader.PropertyToID("_Stencil");
			ID_StencilOp = Shader.PropertyToID("_StencilOp");
			ID_StencilComp = Shader.PropertyToID("_StencilComp");
			ID_StencilReadMask = Shader.PropertyToID("_StencilReadMask");
			ID_StencilWriteMask = Shader.PropertyToID("_StencilWriteMask");
			ID_ShaderFlags = Shader.PropertyToID("_ShaderFlags");
			ID_ScaleRatio_A = Shader.PropertyToID("_ScaleRatioA");
			ID_ScaleRatio_B = Shader.PropertyToID("_ScaleRatioB");
			ID_ScaleRatio_C = Shader.PropertyToID("_ScaleRatioC");
			if (k_ShaderRef_MobileSDF == null)
			{
				k_ShaderRef_MobileSDF = Shader.Find("TextMeshPro/Mobile/Distance Field");
			}
			if (k_ShaderRef_MobileBitmap == null)
			{
				k_ShaderRef_MobileBitmap = Shader.Find("TextMeshPro/Mobile/Bitmap");
			}
		}
	}

	public static void UpdateShaderRatios(Material mat)
	{
		float num = 1f;
		float num2 = 1f;
		float num3 = 1f;
		bool flag = !mat.shaderKeywords.Contains(Keyword_Ratios);
		float num4 = mat.GetFloat(ID_GradientScale);
		float num5 = mat.GetFloat(ID_FaceDilate);
		float num6 = mat.GetFloat(ID_OutlineWidth);
		float num7 = mat.GetFloat(ID_OutlineSoftness);
		float num8 = Mathf.Max(mat.GetFloat(ID_WeightNormal), mat.GetFloat(ID_WeightBold)) / 4f;
		float num9 = Mathf.Max(1f, num8 + num5 + num6 + num7);
		num = (flag ? ((num4 - m_clamp) / (num4 * num9)) : 1f);
		mat.SetFloat(ID_ScaleRatio_A, num);
		if (mat.HasProperty(ID_GlowOffset))
		{
			float num10 = mat.GetFloat(ID_GlowOffset);
			float num11 = mat.GetFloat(ID_GlowOuter);
			float num12 = (num8 + num5) * (num4 - m_clamp);
			num9 = Mathf.Max(1f, num10 + num11);
			num2 = (flag ? (Mathf.Max(0f, num4 - m_clamp - num12) / (num4 * num9)) : 1f);
			mat.SetFloat(ID_ScaleRatio_B, num2);
		}
		if (mat.HasProperty(ID_UnderlayOffsetX))
		{
			float f = mat.GetFloat(ID_UnderlayOffsetX);
			float f2 = mat.GetFloat(ID_UnderlayOffsetY);
			float num13 = mat.GetFloat(ID_UnderlayDilate);
			float num14 = mat.GetFloat(ID_UnderlaySoftness);
			float num15 = (num8 + num5) * (num4 - m_clamp);
			num9 = Mathf.Max(1f, Mathf.Max(Mathf.Abs(f), Mathf.Abs(f2)) + num13 + num14);
			num3 = (flag ? (Mathf.Max(0f, num4 - m_clamp - num15) / (num4 * num9)) : 1f);
			mat.SetFloat(ID_ScaleRatio_C, num3);
		}
	}

	public static Vector4 GetFontExtent(Material material)
	{
		return Vector4.zero;
	}

	public static bool IsMaskingEnabled(Material material)
	{
		if (material == null || !material.HasProperty(ID_ClipRect))
		{
			return false;
		}
		if (material.shaderKeywords.Contains(Keyword_MASK_SOFT) || material.shaderKeywords.Contains(Keyword_MASK_HARD) || material.shaderKeywords.Contains(Keyword_MASK_TEX))
		{
			return true;
		}
		return false;
	}

	public static float GetPadding(Material material, bool enableExtraPadding, bool isBold)
	{
		if (!isInitialized)
		{
			GetShaderPropertyIDs();
		}
		if (material == null)
		{
			return 0f;
		}
		int num = (enableExtraPadding ? 4 : 0);
		if (!material.HasProperty(ID_GradientScale))
		{
			if (material.HasProperty(ID_Padding))
			{
				num += (int)material.GetFloat(ID_Padding);
			}
			return num;
		}
		Vector4 zero = Vector4.zero;
		Vector4 zero2 = Vector4.zero;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		UpdateShaderRatios(material);
		string[] shaderKeywords = material.shaderKeywords;
		if (material.HasProperty(ID_ScaleRatio_A))
		{
			num5 = material.GetFloat(ID_ScaleRatio_A);
		}
		if (material.HasProperty(ID_FaceDilate))
		{
			num2 = material.GetFloat(ID_FaceDilate) * num5;
		}
		if (material.HasProperty(ID_OutlineSoftness))
		{
			num3 = material.GetFloat(ID_OutlineSoftness) * num5;
		}
		if (material.HasProperty(ID_OutlineWidth))
		{
			num4 = material.GetFloat(ID_OutlineWidth) * num5;
		}
		num10 = num4 + num3 + num2;
		if (material.HasProperty(ID_GlowOffset) && shaderKeywords.Contains(Keyword_Glow))
		{
			if (material.HasProperty(ID_ScaleRatio_B))
			{
				num6 = material.GetFloat(ID_ScaleRatio_B);
			}
			num8 = material.GetFloat(ID_GlowOffset) * num6;
			num9 = material.GetFloat(ID_GlowOuter) * num6;
		}
		num10 = Mathf.Max(num10, num2 + num8 + num9);
		if (material.HasProperty(ID_UnderlaySoftness) && shaderKeywords.Contains(Keyword_Underlay))
		{
			if (material.HasProperty(ID_ScaleRatio_C))
			{
				num7 = material.GetFloat(ID_ScaleRatio_C);
			}
			float num11 = material.GetFloat(ID_UnderlayOffsetX) * num7;
			float num12 = material.GetFloat(ID_UnderlayOffsetY) * num7;
			float num13 = material.GetFloat(ID_UnderlayDilate) * num7;
			float num14 = material.GetFloat(ID_UnderlaySoftness) * num7;
			zero.x = Mathf.Max(zero.x, num2 + num13 + num14 - num11);
			zero.y = Mathf.Max(zero.y, num2 + num13 + num14 - num12);
			zero.z = Mathf.Max(zero.z, num2 + num13 + num14 + num11);
			zero.w = Mathf.Max(zero.w, num2 + num13 + num14 + num12);
		}
		zero.x = Mathf.Max(zero.x, num10);
		zero.y = Mathf.Max(zero.y, num10);
		zero.z = Mathf.Max(zero.z, num10);
		zero.w = Mathf.Max(zero.w, num10);
		zero.x += num;
		zero.y += num;
		zero.z += num;
		zero.w += num;
		zero.x = Mathf.Min(zero.x, 1f);
		zero.y = Mathf.Min(zero.y, 1f);
		zero.z = Mathf.Min(zero.z, 1f);
		zero.w = Mathf.Min(zero.w, 1f);
		zero2.x = ((zero2.x < zero.x) ? zero.x : zero2.x);
		zero2.y = ((zero2.y < zero.y) ? zero.y : zero2.y);
		zero2.z = ((zero2.z < zero.z) ? zero.z : zero2.z);
		zero2.w = ((zero2.w < zero.w) ? zero.w : zero2.w);
		float num15 = material.GetFloat(ID_GradientScale);
		zero *= num15;
		num10 = Mathf.Max(zero.x, zero.y);
		num10 = Mathf.Max(zero.z, num10);
		num10 = Mathf.Max(zero.w, num10);
		return num10 + 1.25f;
	}

	public static float GetPadding(Material[] materials, bool enableExtraPadding, bool isBold)
	{
		if (!isInitialized)
		{
			GetShaderPropertyIDs();
		}
		if (materials == null)
		{
			return 0f;
		}
		int num = (enableExtraPadding ? 4 : 0);
		if (materials[0].HasProperty(ID_Padding))
		{
			return (float)num + materials[0].GetFloat(ID_Padding);
		}
		Vector4 zero = Vector4.zero;
		Vector4 zero2 = Vector4.zero;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		for (int i = 0; i < materials.Length; i++)
		{
			UpdateShaderRatios(materials[i]);
			string[] shaderKeywords = materials[i].shaderKeywords;
			if (materials[i].HasProperty(ID_ScaleRatio_A))
			{
				num5 = materials[i].GetFloat(ID_ScaleRatio_A);
			}
			if (materials[i].HasProperty(ID_FaceDilate))
			{
				num2 = materials[i].GetFloat(ID_FaceDilate) * num5;
			}
			if (materials[i].HasProperty(ID_OutlineSoftness))
			{
				num3 = materials[i].GetFloat(ID_OutlineSoftness) * num5;
			}
			if (materials[i].HasProperty(ID_OutlineWidth))
			{
				num4 = materials[i].GetFloat(ID_OutlineWidth) * num5;
			}
			num10 = num4 + num3 + num2;
			if (materials[i].HasProperty(ID_GlowOffset) && shaderKeywords.Contains(Keyword_Glow))
			{
				if (materials[i].HasProperty(ID_ScaleRatio_B))
				{
					num6 = materials[i].GetFloat(ID_ScaleRatio_B);
				}
				num8 = materials[i].GetFloat(ID_GlowOffset) * num6;
				num9 = materials[i].GetFloat(ID_GlowOuter) * num6;
			}
			num10 = Mathf.Max(num10, num2 + num8 + num9);
			if (materials[i].HasProperty(ID_UnderlaySoftness) && shaderKeywords.Contains(Keyword_Underlay))
			{
				if (materials[i].HasProperty(ID_ScaleRatio_C))
				{
					num7 = materials[i].GetFloat(ID_ScaleRatio_C);
				}
				float num11 = materials[i].GetFloat(ID_UnderlayOffsetX) * num7;
				float num12 = materials[i].GetFloat(ID_UnderlayOffsetY) * num7;
				float num13 = materials[i].GetFloat(ID_UnderlayDilate) * num7;
				float num14 = materials[i].GetFloat(ID_UnderlaySoftness) * num7;
				zero.x = Mathf.Max(zero.x, num2 + num13 + num14 - num11);
				zero.y = Mathf.Max(zero.y, num2 + num13 + num14 - num12);
				zero.z = Mathf.Max(zero.z, num2 + num13 + num14 + num11);
				zero.w = Mathf.Max(zero.w, num2 + num13 + num14 + num12);
			}
			zero.x = Mathf.Max(zero.x, num10);
			zero.y = Mathf.Max(zero.y, num10);
			zero.z = Mathf.Max(zero.z, num10);
			zero.w = Mathf.Max(zero.w, num10);
			zero.x += num;
			zero.y += num;
			zero.z += num;
			zero.w += num;
			zero.x = Mathf.Min(zero.x, 1f);
			zero.y = Mathf.Min(zero.y, 1f);
			zero.z = Mathf.Min(zero.z, 1f);
			zero.w = Mathf.Min(zero.w, 1f);
			zero2.x = ((zero2.x < zero.x) ? zero.x : zero2.x);
			zero2.y = ((zero2.y < zero.y) ? zero.y : zero2.y);
			zero2.z = ((zero2.z < zero.z) ? zero.z : zero2.z);
			zero2.w = ((zero2.w < zero.w) ? zero.w : zero2.w);
		}
		float num15 = materials[0].GetFloat(ID_GradientScale);
		zero *= num15;
		num10 = Mathf.Max(zero.x, zero.y);
		num10 = Mathf.Max(zero.z, num10);
		num10 = Mathf.Max(zero.w, num10);
		return num10 + 0.25f;
	}
}
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteAlways]
public class TMP_SubMesh : MonoBehaviour
{
	[SerializeField]
	private TMP_FontAsset m_fontAsset;

	[SerializeField]
	private Material m_material;

	[SerializeField]
	private Material m_sharedMaterial;

	private Material m_fallbackMaterial;

	private Material m_fallbackSourceMaterial;

	[SerializeField]
	private bool m_isDefaultMaterial;

	[SerializeField]
	private float m_padding;

	[SerializeField]
	private Renderer m_renderer;

	[SerializeField]
	private MeshFilter m_meshFilter;

	private Mesh m_mesh;

	[SerializeField]
	private TextMeshPro m_TextComponent;

	[NonSerialized]
	private bool m_isRegisteredForEvents;

	public TMP_FontAsset fontAsset
	{
		get
		{
			return m_fontAsset;
		}
		set
		{
			m_fontAsset = value;
		}
	}

	public Material material
	{
		get
		{
			return GetMaterial(m_sharedMaterial);
		}
		set
		{
			if (m_sharedMaterial.GetInstanceID() != value.GetInstanceID())
			{
				m_sharedMaterial = (m_material = value);
				m_padding = GetPaddingForMaterial();
				SetVerticesDirty();
				SetMaterialDirty();
			}
		}
	}

	public Material sharedMaterial
	{
		get
		{
			return m_sharedMaterial;
		}
		set
		{
			SetSharedMaterial(value);
		}
	}

	public Material fallbackMaterial
	{
		get
		{
			return m_fallbackMaterial;
		}
		set
		{
			if (!(m_fallbackMaterial == value))
			{
				if (m_fallbackMaterial != null && m_fallbackMaterial != value)
				{
					TMP_MaterialManager.ReleaseFallbackMaterial(m_fallbackMaterial);
				}
				m_fallbackMaterial = value;
				TMP_MaterialManager.AddFallbackMaterialReference(m_fallbackMaterial);
				SetSharedMaterial(m_fallbackMaterial);
			}
		}
	}

	public Material fallbackSourceMaterial
	{
		get
		{
			return m_fallbackSourceMaterial;
		}
		set
		{
			m_fallbackSourceMaterial = value;
		}
	}

	public bool isDefaultMaterial
	{
		get
		{
			return m_isDefaultMaterial;
		}
		set
		{
			m_isDefaultMaterial = value;
		}
	}

	public float padding
	{
		get
		{
			return m_padding;
		}
		set
		{
			m_padding = value;
		}
	}

	public Renderer renderer
	{
		get
		{
			if (m_renderer == null)
			{
				m_renderer = GetComponent<Renderer>();
			}
			return m_renderer;
		}
	}

	public MeshFilter meshFilter
	{
		get
		{
			if (m_meshFilter == null)
			{
				m_meshFilter = GetComponent<MeshFilter>();
			}
			return m_meshFilter;
		}
	}

	public Mesh mesh
	{
		get
		{
			if (m_mesh == null)
			{
				m_mesh = new Mesh();
				m_mesh.hideFlags = HideFlags.HideAndDontSave;
				meshFilter.mesh = m_mesh;
			}
			return m_mesh;
		}
		set
		{
			m_mesh = value;
		}
	}

	private void OnEnable()
	{
		if (!m_isRegisteredForEvents)
		{
			m_isRegisteredForEvents = true;
		}
		meshFilter.sharedMesh = mesh;
		if (m_sharedMaterial != null)
		{
			m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, new Vector4(-32767f, -32767f, 32767f, 32767f));
		}
	}

	private void OnDisable()
	{
		m_meshFilter.sharedMesh = null;
		if (m_fallbackMaterial != null)
		{
			TMP_MaterialManager.ReleaseFallbackMaterial(m_fallbackMaterial);
			m_fallbackMaterial = null;
		}
	}

	private void OnDestroy()
	{
		if (m_mesh != null)
		{
			UnityEngine.Object.DestroyImmediate(m_mesh);
		}
		if (m_fallbackMaterial != null)
		{
			TMP_MaterialManager.ReleaseFallbackMaterial(m_fallbackMaterial);
			m_fallbackMaterial = null;
		}
		m_isRegisteredForEvents = false;
	}

	public static TMP_SubMesh AddSubTextObject(TextMeshPro textComponent, MaterialReference materialReference)
	{
		GameObject gameObject = new GameObject("TMP SubMesh [" + materialReference.material.name + "]", typeof(TMP_SubMesh));
		TMP_SubMesh component = gameObject.GetComponent<TMP_SubMesh>();
		gameObject.transform.SetParent(textComponent.transform, worldPositionStays: false);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
		gameObject.layer = textComponent.gameObject.layer;
		component.m_meshFilter = gameObject.GetComponent<MeshFilter>();
		component.m_TextComponent = textComponent;
		component.m_fontAsset = materialReference.fontAsset;
		component.m_isDefaultMaterial = materialReference.isDefaultMaterial;
		component.SetSharedMaterial(materialReference.material);
		component.renderer.sortingLayerID = textComponent.renderer.sortingLayerID;
		component.renderer.sortingOrder = textComponent.renderer.sortingOrder;
		return component;
	}

	public void DestroySelf()
	{
		UnityEngine.Object.Destroy(base.gameObject, 1f);
	}

	private Material GetMaterial(Material mat)
	{
		if (m_renderer == null)
		{
			m_renderer = GetComponent<Renderer>();
		}
		if (m_material == null || m_material.GetInstanceID() != mat.GetInstanceID())
		{
			m_material = CreateMaterialInstance(mat);
		}
		m_sharedMaterial = m_material;
		m_padding = GetPaddingForMaterial();
		SetVerticesDirty();
		SetMaterialDirty();
		return m_sharedMaterial;
	}

	private Material CreateMaterialInstance(Material source)
	{
		Material obj = new Material(source)
		{
			shaderKeywords = source.shaderKeywords
		};
		obj.name += " (Instance)";
		return obj;
	}

	private Material GetSharedMaterial()
	{
		if (m_renderer == null)
		{
			m_renderer = GetComponent<Renderer>();
		}
		return m_renderer.sharedMaterial;
	}

	private void SetSharedMaterial(Material mat)
	{
		m_sharedMaterial = mat;
		m_padding = GetPaddingForMaterial();
		SetMaterialDirty();
	}

	public float GetPaddingForMaterial()
	{
		return ShaderUtilities.GetPadding(m_sharedMaterial, m_TextComponent.extraPadding, m_TextComponent.isUsingBold);
	}

	public void UpdateMeshPadding(bool isExtraPadding, bool isUsingBold)
	{
		m_padding = ShaderUtilities.GetPadding(m_sharedMaterial, isExtraPadding, isUsingBold);
	}

	public void SetVerticesDirty()
	{
		if (base.enabled && m_TextComponent != null)
		{
			m_TextComponent.havePropertiesChanged = true;
			m_TextComponent.SetVerticesDirty();
		}
	}

	public void SetMaterialDirty()
	{
		UpdateMaterial();
	}

	protected void UpdateMaterial()
	{
		if (m_renderer == null)
		{
			m_renderer = renderer;
		}
		m_renderer.sharedMaterial = m_sharedMaterial;
	}
}
[ExecuteAlways]
public class TMP_SubMeshUI : MaskableGraphic, IClippable, IMaskable, IMaterialModifier
{
	[SerializeField]
	private TMP_FontAsset m_fontAsset;

	[SerializeField]
	private Material m_material;

	[SerializeField]
	private Material m_sharedMaterial;

	private Material m_fallbackMaterial;

	private Material m_fallbackSourceMaterial;

	[SerializeField]
	private bool m_isDefaultMaterial;

	[SerializeField]
	private float m_padding;

	[SerializeField]
	private CanvasRenderer m_canvasRenderer;

	private Mesh m_mesh;

	[SerializeField]
	private TextMeshProUGUI m_TextComponent;

	[NonSerialized]
	private bool m_isRegisteredForEvents;

	private bool m_materialDirty;

	[SerializeField]
	private int m_materialReferenceIndex;

	public TMP_FontAsset fontAsset
	{
		get
		{
			return m_fontAsset;
		}
		set
		{
			m_fontAsset = value;
		}
	}

	public override Texture mainTexture
	{
		get
		{
			if (sharedMaterial != null)
			{
				return sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex);
			}
			return null;
		}
	}

	public override Material material
	{
		get
		{
			return GetMaterial(m_sharedMaterial);
		}
		set
		{
			if (!(m_sharedMaterial != null) || m_sharedMaterial.GetInstanceID() != value.GetInstanceID())
			{
				m_sharedMaterial = (m_material = value);
				m_padding = GetPaddingForMaterial();
				SetVerticesDirty();
				SetMaterialDirty();
			}
		}
	}

	public Material sharedMaterial
	{
		get
		{
			return m_sharedMaterial;
		}
		set
		{
			SetSharedMaterial(value);
		}
	}

	public Material fallbackMaterial
	{
		get
		{
			return m_fallbackMaterial;
		}
		set
		{
			if (!(m_fallbackMaterial == value))
			{
				if (m_fallbackMaterial != null && m_fallbackMaterial != value)
				{
					TMP_MaterialManager.ReleaseFallbackMaterial(m_fallbackMaterial);
				}
				m_fallbackMaterial = value;
				TMP_MaterialManager.AddFallbackMaterialReference(m_fallbackMaterial);
				SetSharedMaterial(m_fallbackMaterial);
			}
		}
	}

	public Material fallbackSourceMaterial
	{
		get
		{
			return m_fallbackSourceMaterial;
		}
		set
		{
			m_fallbackSourceMaterial = value;
		}
	}

	public override Material materialForRendering => TMP_MaterialManager.GetMaterialForRendering(this, m_sharedMaterial);

	public bool isDefaultMaterial
	{
		get
		{
			return m_isDefaultMaterial;
		}
		set
		{
			m_isDefaultMaterial = value;
		}
	}

	public float padding
	{
		get
		{
			return m_padding;
		}
		set
		{
			m_padding = value;
		}
	}

	public new CanvasRenderer canvasRenderer
	{
		get
		{
			if (m_canvasRenderer == null)
			{
				m_canvasRenderer = GetComponent<CanvasRenderer>();
			}
			return m_canvasRenderer;
		}
	}

	public Mesh mesh
	{
		get
		{
			if (m_mesh == null)
			{
				m_mesh = new Mesh();
				m_mesh.hideFlags = HideFlags.HideAndDontSave;
			}
			return m_mesh;
		}
		set
		{
			m_mesh = value;
		}
	}

	public TextMeshProUGUI textComponent => m_TextComponent;

	public static TMP_SubMeshUI AddSubTextObject(TextMeshProUGUI textComponent, MaterialReference materialReference)
	{
		GameObject obj = new GameObject("TMP UI SubObject [" + materialReference.material.name + "]", typeof(RectTransform));
		obj.transform.SetParent(textComponent.transform, worldPositionStays: false);
		obj.layer = textComponent.gameObject.layer;
		RectTransform component = obj.GetComponent<RectTransform>();
		component.anchorMin = Vector2.zero;
		component.anchorMax = Vector2.one;
		component.sizeDelta = Vector2.zero;
		component.pivot = textComponent.rectTransform.pivot;
		TMP_SubMeshUI tMP_SubMeshUI = obj.AddComponent<TMP_SubMeshUI>();
		tMP_SubMeshUI.m_canvasRenderer = tMP_SubMeshUI.canvasRenderer;
		tMP_SubMeshUI.m_TextComponent = textComponent;
		tMP_SubMeshUI.m_materialReferenceIndex = materialReference.index;
		tMP_SubMeshUI.m_fontAsset = materialReference.fontAsset;
		tMP_SubMeshUI.m_isDefaultMaterial = materialReference.isDefaultMaterial;
		tMP_SubMeshUI.SetSharedMaterial(materialReference.material);
		return tMP_SubMeshUI;
	}

	protected override void OnEnable()
	{
		if (!m_isRegisteredForEvents)
		{
			m_isRegisteredForEvents = true;
		}
		m_ShouldRecalculateStencil = true;
		RecalculateClipping();
		RecalculateMasking();
	}

	protected override void OnDisable()
	{
		TMP_UpdateRegistry.UnRegisterCanvasElementForRebuild(this);
		if (m_MaskMaterial != null)
		{
			TMP_MaterialManager.ReleaseStencilMaterial(m_MaskMaterial);
			m_MaskMaterial = null;
		}
		if (m_fallbackMaterial != null)
		{
			TMP_MaterialManager.ReleaseFallbackMaterial(m_fallbackMaterial);
			m_fallbackMaterial = null;
		}
		base.OnDisable();
	}

	protected override void OnDestroy()
	{
		if (m_mesh != null)
		{
			UnityEngine.Object.DestroyImmediate(m_mesh);
		}
		if (m_MaskMaterial != null)
		{
			TMP_MaterialManager.ReleaseStencilMaterial(m_MaskMaterial);
		}
		if (m_fallbackMaterial != null)
		{
			TMP_MaterialManager.ReleaseFallbackMaterial(m_fallbackMaterial);
			m_fallbackMaterial = null;
		}
		m_isRegisteredForEvents = false;
		RecalculateClipping();
	}

	protected override void OnTransformParentChanged()
	{
		if (IsActive())
		{
			m_ShouldRecalculateStencil = true;
			RecalculateClipping();
			RecalculateMasking();
		}
	}

	public override Material GetModifiedMaterial(Material baseMaterial)
	{
		Material material = baseMaterial;
		if (m_ShouldRecalculateStencil)
		{
			m_StencilValue = TMP_MaterialManager.GetStencilID(base.gameObject);
			m_ShouldRecalculateStencil = false;
		}
		if (m_StencilValue > 0)
		{
			material = TMP_MaterialManager.GetStencilMaterial(baseMaterial, m_StencilValue);
			if (m_MaskMaterial != null)
			{
				TMP_MaterialManager.ReleaseStencilMaterial(m_MaskMaterial);
			}
			m_MaskMaterial = material;
		}
		return material;
	}

	public float GetPaddingForMaterial()
	{
		return ShaderUtilities.GetPadding(m_sharedMaterial, m_TextComponent.extraPadding, m_TextComponent.isUsingBold);
	}

	public float GetPaddingForMaterial(Material mat)
	{
		return ShaderUtilities.GetPadding(mat, m_TextComponent.extraPadding, m_TextComponent.isUsingBold);
	}

	public void UpdateMeshPadding(bool isExtraPadding, bool isUsingBold)
	{
		m_padding = ShaderUtilities.GetPadding(m_sharedMaterial, isExtraPadding, isUsingBold);
	}

	public override void SetAllDirty()
	{
	}

	public override void SetVerticesDirty()
	{
		if (IsActive() && m_TextComponent != null)
		{
			m_TextComponent.havePropertiesChanged = true;
			m_TextComponent.SetVerticesDirty();
		}
	}

	public override void SetLayoutDirty()
	{
	}

	public override void SetMaterialDirty()
	{
		m_materialDirty = true;
		UpdateMaterial();
		if (m_OnDirtyMaterialCallback != null)
		{
			m_OnDirtyMaterialCallback();
		}
	}

	public void SetPivotDirty()
	{
		if (IsActive())
		{
			base.rectTransform.pivot = m_TextComponent.rectTransform.pivot;
		}
	}

	public override void Cull(Rect clipRect, bool validRect)
	{
		if (!m_TextComponent.ignoreRectMaskCulling)
		{
			base.Cull(clipRect, validRect);
		}
	}

	protected override void UpdateGeometry()
	{
		UnityEngine.Debug.Log("UpdateGeometry()");
	}

	public override void Rebuild(CanvasUpdate update)
	{
		if (update == CanvasUpdate.PreRender && m_materialDirty)
		{
			UpdateMaterial();
			m_materialDirty = false;
		}
	}

	public void RefreshMaterial()
	{
		UpdateMaterial();
	}

	protected override void UpdateMaterial()
	{
		if (m_canvasRenderer == null)
		{
			m_canvasRenderer = canvasRenderer;
		}
		m_canvasRenderer.materialCount = 1;
		m_canvasRenderer.SetMaterial(materialForRendering, 0);
		m_canvasRenderer.SetTexture(mainTexture);
	}

	public override void RecalculateClipping()
	{
		base.RecalculateClipping();
	}

	public override void RecalculateMasking()
	{
		m_ShouldRecalculateStencil = true;
		SetMaterialDirty();
	}

	private Material GetMaterial()
	{
		return m_sharedMaterial;
	}

	private Material GetMaterial(Material mat)
	{
		if (m_material == null || m_material.GetInstanceID() != mat.GetInstanceID())
		{
			m_material = CreateMaterialInstance(mat);
		}
		m_sharedMaterial = m_material;
		m_padding = GetPaddingForMaterial();
		SetVerticesDirty();
		SetMaterialDirty();
		return m_sharedMaterial;
	}

	private Material CreateMaterialInstance(Material source)
	{
		Material obj = new Material(source)
		{
			shaderKeywords = source.shaderKeywords
		};
		obj.name += " (Instance)";
		return obj;
	}

	private Material GetSharedMaterial()
	{
		if (m_canvasRenderer == null)
		{
			m_canvasRenderer = GetComponent<CanvasRenderer>();
		}
		return m_canvasRenderer.GetMaterial();
	}

	private void SetSharedMaterial(Material mat)
	{
		m_sharedMaterial = mat;
		m_Material = m_sharedMaterial;
		m_padding = GetPaddingForMaterial();
		SetMaterialDirty();
	}
}
public interface ITextElement
{
	Material sharedMaterial { get; }

	void Rebuild(CanvasUpdate update);

	int GetInstanceID();
}
public enum TextAlignmentOptions
{
	TopLeft = 257,
	Top = 258,
	TopRight = 260,
	TopJustified = 264,
	TopFlush = 272,
	TopGeoAligned = 288,
	Left = 513,
	Center = 514,
	Right = 516,
	Justified = 520,
	Flush = 528,
	CenterGeoAligned = 544,
	BottomLeft = 1025,
	Bottom = 1026,
	BottomRight = 1028,
	BottomJustified = 1032,
	BottomFlush = 1040,
	BottomGeoAligned = 1056,
	BaselineLeft = 2049,
	Baseline = 2050,
	BaselineRight = 2052,
	BaselineJustified = 2056,
	BaselineFlush = 2064,
	BaselineGeoAligned = 2080,
	MidlineLeft = 4097,
	Midline = 4098,
	MidlineRight = 4100,
	MidlineJustified = 4104,
	MidlineFlush = 4112,
	MidlineGeoAligned = 4128,
	CaplineLeft = 8193,
	Capline = 8194,
	CaplineRight = 8196,
	CaplineJustified = 8200,
	CaplineFlush = 8208,
	CaplineGeoAligned = 8224
}
public enum _HorizontalAlignmentOptions
{
	Left = 1,
	Center = 2,
	Right = 4,
	Justified = 8,
	Flush = 0x10,
	Geometry = 0x20
}
public enum _VerticalAlignmentOptions
{
	Top = 0x100,
	Middle = 0x200,
	Bottom = 0x400,
	Baseline = 0x800,
	Geometry = 0x1000,
	Capline = 0x2000
}
public enum TextRenderFlags
{
	DontRender = 0,
	Render = 255
}
public enum TMP_TextElementType
{
	Character,
	Unused
}
public enum MaskingTypes
{
	MaskOff,
	MaskHard,
	MaskSoft
}
public enum TextOverflowModes
{
	Overflow,
	Ellipsis,
	Masking,
	Truncate,
	ScrollRect,
	Page,
	Linked
}
public enum MaskingOffsetMode
{
	Percentage,
	Pixel
}
public enum TextureMappingOptions
{
	Character,
	Line,
	Paragraph,
	MatchAspect
}
public enum FontStyles
{
	Normal = 0,
	Bold = 1,
	Italic = 2,
	Underline = 4,
	LowerCase = 8,
	UpperCase = 0x10,
	SmallCaps = 0x20,
	Strikethrough = 0x40,
	Superscript = 0x80,
	Subscript = 0x100,
	Highlight = 0x200
}
public enum FontWeight
{
	Thin = 100,
	ExtraLight = 200,
	Light = 300,
	Regular = 400,
	Medium = 500,
	SemiBold = 600,
	Bold = 700,
	Heavy = 800,
	Black = 900
}
public abstract class TMP_Text : MaskableGraphic
{
	protected struct UnicodeChar
	{
		public int unicode;

		public int stringIndex;

		public int length;
	}

	[SerializeField]
	[TextArea(5, 10)]
	protected string m_text;

	[SerializeField]
	protected bool m_isRightToLeft;

	[SerializeField]
	protected TMP_FontAsset m_fontAsset;

	protected TMP_FontAsset m_currentFontAsset;

	protected bool m_isSDFShader;

	[SerializeField]
	protected Material m_sharedMaterial;

	protected Material m_currentMaterial;

	protected MaterialReference[] m_materialReferences = new MaterialReference[32];

	protected Dictionary<int, int> m_materialReferenceIndexLookup = new Dictionary<int, int>();

	protected TMP_RichTextTagStack<MaterialReference> m_materialReferenceStack = new TMP_RichTextTagStack<MaterialReference>(new MaterialReference[16]);

	protected int m_currentMaterialIndex;

	[SerializeField]
	protected Material[] m_fontSharedMaterials;

	[SerializeField]
	protected Material m_fontMaterial;

	[SerializeField]
	protected Material[] m_fontMaterials;

	protected bool m_isMaterialDirty;

	[SerializeField]
	[ColorUsage(true, true)]
	protected Color32 m_fontColor32 = Color.white;

	[SerializeField]
	[ColorUsage(true, true)]
	protected Color m_fontColor = Color.white;

	protected static Color32 s_colorWhite = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	protected Color32 m_underlineColor = s_colorWhite;

	protected Color32 m_strikethroughColor = s_colorWhite;

	protected Color32 m_highlightColor = s_colorWhite;

	protected Vector4 m_highlightPadding = Vector4.zero;

	[SerializeField]
	protected bool m_enableVertexGradient;

	[SerializeField]
	protected ColorMode m_colorMode = ColorMode.FourCornersGradient;

	[SerializeField]
	protected VertexGradient m_fontColorGradient = new VertexGradient(Color.white);

	[SerializeField]
	protected TMP_ColorGradient m_fontColorGradientPreset;

	[SerializeField]
	protected bool m_overrideHtmlColors;

	[SerializeField]
	protected Color32 m_faceColor = Color.white;

	[SerializeField]
	protected Color32 m_outlineColor = Color.black;

	protected float m_outlineWidth;

	[SerializeField]
	protected float m_fontSize = 36f;

	protected float m_currentFontSize;

	[SerializeField]
	protected float m_fontSizeBase = 36f;

	protected TMP_RichTextTagStack<float> m_sizeStack = new TMP_RichTextTagStack<float>(16);

	[SerializeField]
	protected FontWeight m_fontWeight = FontWeight.Regular;

	protected FontWeight m_FontWeightInternal = FontWeight.Regular;

	protected TMP_RichTextTagStack<FontWeight> m_FontWeightStack = new TMP_RichTextTagStack<FontWeight>(8);

	[SerializeField]
	protected bool m_enableAutoSizing;

	protected float m_maxFontSize;

	protected float m_minFontSize;

	[SerializeField]
	protected float m_fontSizeMin;

	[SerializeField]
	protected float m_fontSizeMax;

	[SerializeField]
	protected FontStyles m_fontStyle;

	protected FontStyles m_FontStyleInternal;

	protected TMP_FontStyleStack m_fontStyleStack;

	protected bool m_isUsingBold;

	[SerializeField]
	[FormerlySerializedAs("m_lineJustification")]
	protected TextAlignmentOptions m_textAlignment = TextAlignmentOptions.TopLeft;

	protected TextAlignmentOptions m_lineJustification;

	protected TMP_RichTextTagStack<TextAlignmentOptions> m_lineJustificationStack = new TMP_RichTextTagStack<TextAlignmentOptions>(new TextAlignmentOptions[16]);

	protected Vector3[] m_textContainerLocalCorners = new Vector3[4];

	[SerializeField]
	protected float m_characterSpacing;

	protected float m_cSpacing;

	protected float m_monoSpacing;

	[SerializeField]
	protected float m_wordSpacing;

	[SerializeField]
	protected float m_lineSpacing;

	protected float m_lineSpacingDelta;

	protected float m_lineHeight = -32767f;

	[SerializeField]
	protected float m_lineSpacingMax;

	[SerializeField]
	protected float m_paragraphSpacing;

	[SerializeField]
	protected float m_charWidthMaxAdj;

	protected float m_charWidthAdjDelta;

	[SerializeField]
	protected bool m_enableWordWrapping;

	protected bool m_isCharacterWrappingEnabled;

	protected bool m_isNonBreakingSpace;

	protected bool m_isIgnoringAlignment;

	[SerializeField]
	protected float m_wordWrappingRatios = 0.4f;

	[SerializeField]
	protected TextOverflowModes m_overflowMode;

	[SerializeField]
	protected int m_firstOverflowCharacterIndex = -1;

	[SerializeField]
	protected TMP_Text m_linkedTextComponent;

	[SerializeField]
	protected bool m_isLinkedTextComponent;

	[SerializeField]
	protected bool m_isTextTruncated;

	[SerializeField]
	protected bool m_enableKerning;

	[SerializeField]
	protected bool m_enableExtraPadding;

	[SerializeField]
	protected bool checkPaddingRequired;

	[SerializeField]
	protected bool m_isRichText;

	protected bool m_isOverlay;

	[SerializeField]
	protected bool m_isOrthographic;

	[SerializeField]
	protected bool m_isCullingEnabled;

	[SerializeField]
	protected bool m_ignoreRectMaskCulling;

	[SerializeField]
	protected bool m_ignoreCulling = true;

	[SerializeField]
	protected TextureMappingOptions m_horizontalMapping;

	[SerializeField]
	protected TextureMappingOptions m_verticalMapping;

	[SerializeField]
	protected float m_uvLineOffset;

	protected TextRenderFlags m_renderMode = TextRenderFlags.Render;

	[SerializeField]
	protected VertexSortingOrder m_geometrySortingOrder;

	[SerializeField]
	protected bool m_IsTextObjectScaleStatic = true;

	[SerializeField]
	protected bool m_VertexBufferAutoSizeReduction = true;

	[SerializeField]
	protected int m_firstVisibleCharacter;

	protected int m_maxVisibleCharacters = 99999;

	protected int m_maxVisibleWords = 99999;

	protected int m_maxVisibleLines = 99999;

	[SerializeField]
	protected bool m_useMaxVisibleDescender = true;

	[SerializeField]
	protected int m_pageToDisplay = 1;

	protected bool m_isNewPage;

	[SerializeField]
	protected Vector4 m_margin = new Vector4(0f, 0f, 0f, 0f);

	protected float m_marginLeft;

	protected float m_marginRight;

	protected float m_marginWidth;

	protected float m_marginHeight;

	protected float m_width = -1f;

	[SerializeField]
	protected TMP_TextInfo m_textInfo;

	protected bool m_havePropertiesChanged;

	[SerializeField]
	protected bool m_isUsingLegacyAnimationComponent;

	protected Transform m_transform;

	protected RectTransform m_rectTransform;

	protected bool m_autoSizeTextContainer;

	protected Mesh m_mesh;

	[SerializeField]
	protected bool m_isVolumetricText;

	protected float m_flexibleHeight = -1f;

	protected float m_flexibleWidth = -1f;

	protected float m_minWidth;

	protected float m_minHeight;

	protected float m_maxWidth;

	protected float m_maxHeight;

	protected LayoutElement m_LayoutElement;

	protected float m_preferredWidth;

	protected float m_renderedWidth;

	protected bool m_isPreferredWidthDirty;

	protected float m_preferredHeight;

	protected float m_renderedHeight;

	protected bool m_isPreferredHeightDirty;

	protected bool m_isCalculatingPreferredValues;

	private int m_recursiveCount;

	protected int m_layoutPriority;

	protected bool m_isCalculateSizeRequired;

	protected bool m_isLayoutDirty;

	protected bool m_verticesAlreadyDirty;

	protected bool m_layoutAlreadyDirty;

	protected bool m_isAwake;

	internal bool m_isWaitingOnResourceLoad;

	internal bool m_isInputParsingRequired;

	protected string old_text;

	protected float m_fontScale;

	protected float m_fontScaleMultiplier;

	protected char[] m_htmlTag = new char[128];

	protected RichTextTagAttribute[] m_xmlAttribute = new RichTextTagAttribute[8];

	protected float[] m_attributeParameterValues = new float[16];

	protected float tag_LineIndent;

	protected float tag_Indent;

	protected TMP_RichTextTagStack<float> m_indentStack = new TMP_RichTextTagStack<float>(new float[16]);

	protected bool tag_NoParsing;

	protected bool m_isParsingText;

	protected Matrix4x4 m_FXMatrix;

	protected bool m_isFXMatrixSet;

	protected UnicodeChar[] m_TextParsingBuffer;

	private TMP_CharacterInfo[] m_internalCharacterInfo;

	protected int m_totalCharacterCount;

	protected WordWrapState m_SavedWordWrapState;

	protected WordWrapState m_SavedLineState;

	protected int m_characterCount;

	protected int m_firstCharacterOfLine;

	protected int m_firstVisibleCharacterOfLine;

	protected int m_lastCharacterOfLine;

	protected int m_lastVisibleCharacterOfLine;

	protected int m_lineNumber;

	protected int m_lineVisibleCharacterCount;

	protected int m_pageNumber;

	protected float m_maxAscender;

	protected float m_maxCapHeight;

	protected float m_maxDescender;

	protected float m_maxLineAscender;

	protected float m_maxLineDescender;

	protected float m_startOfLineAscender;

	protected float m_lineOffset;

	protected Extents m_meshExtents;

	protected Color32 m_htmlColor = new Color(255f, 255f, 255f, 128f);

	protected TMP_RichTextTagStack<Color32> m_colorStack = new TMP_RichTextTagStack<Color32>(new Color32[16]);

	protected TMP_RichTextTagStack<Color32> m_underlineColorStack = new TMP_RichTextTagStack<Color32>(new Color32[16]);

	protected TMP_RichTextTagStack<Color32> m_strikethroughColorStack = new TMP_RichTextTagStack<Color32>(new Color32[16]);

	protected TMP_RichTextTagStack<Color32> m_highlightColorStack = new TMP_RichTextTagStack<Color32>(new Color32[16]);

	protected TMP_ColorGradient m_colorGradientPreset;

	protected TMP_RichTextTagStack<TMP_ColorGradient> m_colorGradientStack = new TMP_RichTextTagStack<TMP_ColorGradient>(new TMP_ColorGradient[16]);

	protected float m_tabSpacing;

	protected float m_spacing;

	protected TMP_RichTextTagStack<int> m_styleStack = new TMP_RichTextTagStack<int>(new int[16]);

	protected TMP_RichTextTagStack<int> m_actionStack = new TMP_RichTextTagStack<int>(new int[16]);

	protected float m_padding;

	protected float m_baselineOffset;

	protected TMP_RichTextTagStack<float> m_baselineOffsetStack = new TMP_RichTextTagStack<float>(new float[16]);

	protected float m_xAdvance;

	protected TMP_TextElementType m_textElementType;

	protected TMP_TextElement m_cached_TextElement;

	protected TMP_Character m_cached_Underline_Character;

	protected TMP_Character m_cached_Ellipsis_Character;

	protected int m_spriteCount;

	protected int m_spriteIndex;

	protected int m_spriteAnimationID;

	protected bool m_ignoreActiveState;

	protected static Vector2 k_LargePositiveVector2 = new Vector2(2.1474836E+09f, 2.1474836E+09f);

	protected static Vector2 k_LargeNegativeVector2 = new Vector2(-2.1474836E+09f, -2.1474836E+09f);

	protected static float k_LargePositiveFloat = 32767f;

	protected static float k_LargeNegativeFloat = -32767f;

	protected static int k_LargePositiveInt = int.MaxValue;

	protected static int k_LargeNegativeInt = -2147483647;

	public string text
	{
		get
		{
			return m_text;
		}
		set
		{
			if (!(m_text == value))
			{
				m_text = (old_text = value);
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				m_isInputParsingRequired = true;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public bool isRightToLeftText
	{
		get
		{
			return m_isRightToLeft;
		}
		set
		{
			if (m_isRightToLeft != value)
			{
				m_isRightToLeft = value;
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				m_isInputParsingRequired = true;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public TMP_FontAsset font
	{
		get
		{
			return m_fontAsset;
		}
		set
		{
			if (!(m_fontAsset == value))
			{
				m_fontAsset = value;
				LoadFontAsset();
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				m_isInputParsingRequired = true;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public virtual Material fontSharedMaterial
	{
		get
		{
			return m_sharedMaterial;
		}
		set
		{
			if (!(m_sharedMaterial == value))
			{
				SetSharedMaterial(value);
				m_havePropertiesChanged = true;
				m_isInputParsingRequired = true;
				SetVerticesDirty();
				SetMaterialDirty();
			}
		}
	}

	public virtual Material[] fontSharedMaterials
	{
		get
		{
			return GetSharedMaterials();
		}
		set
		{
			SetSharedMaterials(value);
			m_havePropertiesChanged = true;
			m_isInputParsingRequired = true;
			SetVerticesDirty();
			SetMaterialDirty();
		}
	}

	public Material fontMaterial
	{
		get
		{
			return GetMaterial(m_sharedMaterial);
		}
		set
		{
			if (!(m_sharedMaterial != null) || m_sharedMaterial.GetInstanceID() != value.GetInstanceID())
			{
				m_sharedMaterial = value;
				m_padding = GetPaddingForMaterial();
				m_havePropertiesChanged = true;
				m_isInputParsingRequired = true;
				SetVerticesDirty();
				SetMaterialDirty();
			}
		}
	}

	public virtual Material[] fontMaterials
	{
		get
		{
			return GetMaterials(m_fontSharedMaterials);
		}
		set
		{
			SetSharedMaterials(value);
			m_havePropertiesChanged = true;
			m_isInputParsingRequired = true;
			SetVerticesDirty();
			SetMaterialDirty();
		}
	}

	public override Color color
	{
		get
		{
			return m_fontColor;
		}
		set
		{
			if (!(m_fontColor == value))
			{
				m_havePropertiesChanged = true;
				m_fontColor = value;
				SetVerticesDirty();
			}
		}
	}

	public float alpha
	{
		get
		{
			return m_fontColor.a;
		}
		set
		{
			if (m_fontColor.a != value)
			{
				m_fontColor.a = value;
				m_havePropertiesChanged = true;
				SetVerticesDirty();
			}
		}
	}

	public bool enableVertexGradient
	{
		get
		{
			return m_enableVertexGradient;
		}
		set
		{
			if (m_enableVertexGradient != value)
			{
				m_havePropertiesChanged = true;
				m_enableVertexGradient = value;
				SetVerticesDirty();
			}
		}
	}

	public VertexGradient colorGradient
	{
		get
		{
			return m_fontColorGradient;
		}
		set
		{
			m_havePropertiesChanged = true;
			m_fontColorGradient = value;
			SetVerticesDirty();
		}
	}

	public TMP_ColorGradient colorGradientPreset
	{
		get
		{
			return m_fontColorGradientPreset;
		}
		set
		{
			m_havePropertiesChanged = true;
			m_fontColorGradientPreset = value;
			SetVerticesDirty();
		}
	}

	public bool overrideColorTags
	{
		get
		{
			return m_overrideHtmlColors;
		}
		set
		{
			if (m_overrideHtmlColors != value)
			{
				m_havePropertiesChanged = true;
				m_overrideHtmlColors = value;
				SetVerticesDirty();
			}
		}
	}

	public Color32 faceColor
	{
		get
		{
			if (m_sharedMaterial == null)
			{
				return m_faceColor;
			}
			m_faceColor = m_sharedMaterial.GetColor(ShaderUtilities.ID_FaceColor);
			return m_faceColor;
		}
		set
		{
			if (!m_faceColor.Compare(value))
			{
				SetFaceColor(value);
				m_havePropertiesChanged = true;
				m_faceColor = value;
				SetVerticesDirty();
				SetMaterialDirty();
			}
		}
	}

	public Color32 outlineColor
	{
		get
		{
			if (m_sharedMaterial == null)
			{
				return m_outlineColor;
			}
			m_outlineColor = m_sharedMaterial.GetColor(ShaderUtilities.ID_OutlineColor);
			return m_outlineColor;
		}
		set
		{
			if (!m_outlineColor.Compare(value))
			{
				SetOutlineColor(value);
				m_havePropertiesChanged = true;
				m_outlineColor = value;
				SetVerticesDirty();
			}
		}
	}

	public float outlineWidth
	{
		get
		{
			if (m_sharedMaterial == null)
			{
				return m_outlineWidth;
			}
			m_outlineWidth = m_sharedMaterial.GetFloat(ShaderUtilities.ID_OutlineWidth);
			return m_outlineWidth;
		}
		set
		{
			if (m_outlineWidth != value)
			{
				SetOutlineThickness(value);
				m_havePropertiesChanged = true;
				m_outlineWidth = value;
				SetVerticesDirty();
			}
		}
	}

	public float fontSize
	{
		get
		{
			return m_fontSize;
		}
		set
		{
			if (m_fontSize != value)
			{
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				m_fontSize = value;
				if (!m_enableAutoSizing)
				{
					m_fontSizeBase = m_fontSize;
				}
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public float fontScale => m_fontScale;

	public FontWeight fontWeight
	{
		get
		{
			return m_fontWeight;
		}
		set
		{
			if (m_fontWeight != value)
			{
				m_fontWeight = value;
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				m_isInputParsingRequired = true;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public float pixelsPerUnit
	{
		get
		{
			Canvas canvas = base.canvas;
			if (!canvas)
			{
				return 1f;
			}
			if (!font)
			{
				return canvas.scaleFactor;
			}
			if (m_currentFontAsset == null || m_currentFontAsset.faceInfo.pointSize <= 0 || m_fontSize <= 0f)
			{
				return 1f;
			}
			return m_fontSize / (float)m_currentFontAsset.faceInfo.pointSize;
		}
	}

	public bool enableAutoSizing
	{
		get
		{
			return m_enableAutoSizing;
		}
		set
		{
			if (m_enableAutoSizing != value)
			{
				m_enableAutoSizing = value;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public float fontSizeMin
	{
		get
		{
			return m_fontSizeMin;
		}
		set
		{
			if (m_fontSizeMin != value)
			{
				m_fontSizeMin = value;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public float fontSizeMax
	{
		get
		{
			return m_fontSizeMax;
		}
		set
		{
			if (m_fontSizeMax != value)
			{
				m_fontSizeMax = value;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public FontStyles fontStyle
	{
		get
		{
			return m_fontStyle;
		}
		set
		{
			if (m_fontStyle != value)
			{
				m_fontStyle = value;
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				m_isInputParsingRequired = true;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public bool isUsingBold => m_isUsingBold;

	public TextAlignmentOptions alignment
	{
		get
		{
			return m_textAlignment;
		}
		set
		{
			if (m_textAlignment != value)
			{
				m_havePropertiesChanged = true;
				m_textAlignment = value;
				SetVerticesDirty();
			}
		}
	}

	public float characterSpacing
	{
		get
		{
			return m_characterSpacing;
		}
		set
		{
			if (m_characterSpacing != value)
			{
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				m_characterSpacing = value;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public float wordSpacing
	{
		get
		{
			return m_wordSpacing;
		}
		set
		{
			if (m_wordSpacing != value)
			{
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				m_wordSpacing = value;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public float lineSpacing
	{
		get
		{
			return m_lineSpacing;
		}
		set
		{
			if (m_lineSpacing != value)
			{
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				m_lineSpacing = value;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public float lineSpacingAdjustment
	{
		get
		{
			return m_lineSpacingMax;
		}
		set
		{
			if (m_lineSpacingMax != value)
			{
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				m_lineSpacingMax = value;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public float paragraphSpacing
	{
		get
		{
			return m_paragraphSpacing;
		}
		set
		{
			if (m_paragraphSpacing != value)
			{
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				m_paragraphSpacing = value;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public float characterWidthAdjustment
	{
		get
		{
			return m_charWidthMaxAdj;
		}
		set
		{
			if (m_charWidthMaxAdj != value)
			{
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				m_charWidthMaxAdj = value;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public bool enableWordWrapping
	{
		get
		{
			return m_enableWordWrapping;
		}
		set
		{
			if (m_enableWordWrapping != value)
			{
				m_havePropertiesChanged = true;
				m_isInputParsingRequired = true;
				m_isCalculateSizeRequired = true;
				m_enableWordWrapping = value;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public float wordWrappingRatios
	{
		get
		{
			return m_wordWrappingRatios;
		}
		set
		{
			if (m_wordWrappingRatios != value)
			{
				m_wordWrappingRatios = value;
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public TextOverflowModes overflowMode
	{
		get
		{
			return m_overflowMode;
		}
		set
		{
			if (m_overflowMode != value)
			{
				m_overflowMode = value;
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public bool isTextOverflowing
	{
		get
		{
			if (m_firstOverflowCharacterIndex != -1)
			{
				return true;
			}
			return false;
		}
	}

	public int firstOverflowCharacterIndex => m_firstOverflowCharacterIndex;

	public TMP_Text linkedTextComponent
	{
		get
		{
			return m_linkedTextComponent;
		}
		set
		{
			if (m_linkedTextComponent != value)
			{
				if (m_linkedTextComponent != null)
				{
					m_linkedTextComponent.overflowMode = TextOverflowModes.Overflow;
					m_linkedTextComponent.linkedTextComponent = null;
					m_linkedTextComponent.isLinkedTextComponent = false;
				}
				m_linkedTextComponent = value;
				if (m_linkedTextComponent != null)
				{
					m_linkedTextComponent.isLinkedTextComponent = true;
				}
			}
			m_havePropertiesChanged = true;
			m_isCalculateSizeRequired = true;
			SetVerticesDirty();
			SetLayoutDirty();
		}
	}

	public bool isLinkedTextComponent
	{
		get
		{
			return m_isLinkedTextComponent;
		}
		set
		{
			m_isLinkedTextComponent = value;
			if (!m_isLinkedTextComponent)
			{
				m_firstVisibleCharacter = 0;
			}
			m_havePropertiesChanged = true;
			m_isCalculateSizeRequired = true;
			SetVerticesDirty();
			SetLayoutDirty();
		}
	}

	public bool isTextTruncated => m_isTextTruncated;

	public bool enableKerning
	{
		get
		{
			return m_enableKerning;
		}
		set
		{
			if (m_enableKerning != value)
			{
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				m_enableKerning = value;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public bool extraPadding
	{
		get
		{
			return m_enableExtraPadding;
		}
		set
		{
			if (m_enableExtraPadding != value)
			{
				m_havePropertiesChanged = true;
				m_enableExtraPadding = value;
				UpdateMeshPadding();
				SetVerticesDirty();
			}
		}
	}

	public bool richText
	{
		get
		{
			return m_isRichText;
		}
		set
		{
			if (m_isRichText != value)
			{
				m_isRichText = value;
				m_havePropertiesChanged = true;
				m_isCalculateSizeRequired = true;
				m_isInputParsingRequired = true;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public bool isOverlay
	{
		get
		{
			return m_isOverlay;
		}
		set
		{
			if (m_isOverlay != value)
			{
				m_isOverlay = value;
				SetShaderDepth();
				m_havePropertiesChanged = true;
				SetVerticesDirty();
			}
		}
	}

	public bool isOrthographic
	{
		get
		{
			return m_isOrthographic;
		}
		set
		{
			if (m_isOrthographic != value)
			{
				m_havePropertiesChanged = true;
				m_isOrthographic = value;
				SetVerticesDirty();
			}
		}
	}

	public bool enableCulling
	{
		get
		{
			return m_isCullingEnabled;
		}
		set
		{
			if (m_isCullingEnabled != value)
			{
				m_isCullingEnabled = value;
				SetCulling();
				m_havePropertiesChanged = true;
			}
		}
	}

	public bool ignoreRectMaskCulling
	{
		get
		{
			return m_ignoreRectMaskCulling;
		}
		set
		{
			if (m_ignoreRectMaskCulling != value)
			{
				m_ignoreRectMaskCulling = value;
				m_havePropertiesChanged = true;
			}
		}
	}

	public bool ignoreVisibility
	{
		get
		{
			return m_ignoreCulling;
		}
		set
		{
			if (m_ignoreCulling != value)
			{
				m_havePropertiesChanged = true;
				m_ignoreCulling = value;
			}
		}
	}

	public TextureMappingOptions horizontalMapping
	{
		get
		{
			return m_horizontalMapping;
		}
		set
		{
			if (m_horizontalMapping != value)
			{
				m_havePropertiesChanged = true;
				m_horizontalMapping = value;
				SetVerticesDirty();
			}
		}
	}

	public TextureMappingOptions verticalMapping
	{
		get
		{
			return m_verticalMapping;
		}
		set
		{
			if (m_verticalMapping != value)
			{
				m_havePropertiesChanged = true;
				m_verticalMapping = value;
				SetVerticesDirty();
			}
		}
	}

	public float mappingUvLineOffset
	{
		get
		{
			return m_uvLineOffset;
		}
		set
		{
			if (m_uvLineOffset != value)
			{
				m_havePropertiesChanged = true;
				m_uvLineOffset = value;
				SetVerticesDirty();
			}
		}
	}

	public TextRenderFlags renderMode
	{
		get
		{
			return m_renderMode;
		}
		set
		{
			if (m_renderMode != value)
			{
				m_renderMode = value;
				m_havePropertiesChanged = true;
			}
		}
	}

	public VertexSortingOrder geometrySortingOrder
	{
		get
		{
			return m_geometrySortingOrder;
		}
		set
		{
			m_geometrySortingOrder = value;
			m_havePropertiesChanged = true;
			SetVerticesDirty();
		}
	}

	public bool isTextObjectScaleStatic
	{
		get
		{
			return m_IsTextObjectScaleStatic;
		}
		set
		{
			m_IsTextObjectScaleStatic = value;
			if (m_IsTextObjectScaleStatic)
			{
				TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
			}
			else
			{
				TMP_UpdateManager.RegisterTextObjectForUpdate(this);
			}
		}
	}

	public bool vertexBufferAutoSizeReduction
	{
		get
		{
			return m_VertexBufferAutoSizeReduction;
		}
		set
		{
			m_VertexBufferAutoSizeReduction = value;
			m_havePropertiesChanged = true;
			SetVerticesDirty();
		}
	}

	public int firstVisibleCharacter
	{
		get
		{
			return m_firstVisibleCharacter;
		}
		set
		{
			if (m_firstVisibleCharacter != value)
			{
				m_havePropertiesChanged = true;
				m_firstVisibleCharacter = value;
				SetVerticesDirty();
			}
		}
	}

	public int maxVisibleCharacters
	{
		get
		{
			return m_maxVisibleCharacters;
		}
		set
		{
			if (m_maxVisibleCharacters != value)
			{
				m_havePropertiesChanged = true;
				m_maxVisibleCharacters = value;
				SetVerticesDirty();
			}
		}
	}

	public int maxVisibleWords
	{
		get
		{
			return m_maxVisibleWords;
		}
		set
		{
			if (m_maxVisibleWords != value)
			{
				m_havePropertiesChanged = true;
				m_maxVisibleWords = value;
				SetVerticesDirty();
			}
		}
	}

	public int maxVisibleLines
	{
		get
		{
			return m_maxVisibleLines;
		}
		set
		{
			if (m_maxVisibleLines != value)
			{
				m_havePropertiesChanged = true;
				m_isInputParsingRequired = true;
				m_maxVisibleLines = value;
				SetVerticesDirty();
			}
		}
	}

	public bool useMaxVisibleDescender
	{
		get
		{
			return m_useMaxVisibleDescender;
		}
		set
		{
			if (m_useMaxVisibleDescender != value)
			{
				m_havePropertiesChanged = true;
				m_isInputParsingRequired = true;
				SetVerticesDirty();
			}
		}
	}

	public int pageToDisplay
	{
		get
		{
			return m_pageToDisplay;
		}
		set
		{
			if (m_pageToDisplay != value)
			{
				m_havePropertiesChanged = true;
				m_pageToDisplay = value;
				SetVerticesDirty();
			}
		}
	}

	public virtual Vector4 margin
	{
		get
		{
			return m_margin;
		}
		set
		{
			if (!(m_margin == value))
			{
				m_margin = value;
				ComputeMarginSize();
				m_havePropertiesChanged = true;
				SetVerticesDirty();
			}
		}
	}

	public TMP_TextInfo textInfo => m_textInfo;

	public bool havePropertiesChanged
	{
		get
		{
			return m_havePropertiesChanged;
		}
		set
		{
			if (m_havePropertiesChanged != value)
			{
				m_havePropertiesChanged = value;
				m_isInputParsingRequired = true;
				SetAllDirty();
			}
		}
	}

	public bool isUsingLegacyAnimationComponent
	{
		get
		{
			return m_isUsingLegacyAnimationComponent;
		}
		set
		{
			m_isUsingLegacyAnimationComponent = value;
		}
	}

	public new Transform transform
	{
		get
		{
			if (m_transform == null)
			{
				m_transform = GetComponent<Transform>();
			}
			return m_transform;
		}
	}

	public new RectTransform rectTransform
	{
		get
		{
			if (m_rectTransform == null)
			{
				CacheRectTransform();
			}
			return m_rectTransform;
		}
	}

	public virtual bool autoSizeTextContainer { get; set; }

	public virtual Mesh mesh => m_mesh;

	public bool isVolumetricText
	{
		get
		{
			return m_isVolumetricText;
		}
		set
		{
			if (m_isVolumetricText != value)
			{
				m_havePropertiesChanged = value;
				m_textInfo.ResetVertexLayout(value);
				m_isInputParsingRequired = true;
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}
	}

	public Bounds bounds
	{
		get
		{
			if (m_mesh == null)
			{
				return default(Bounds);
			}
			return GetCompoundBounds();
		}
	}

	public Bounds textBounds
	{
		get
		{
			if (m_textInfo == null)
			{
				return default(Bounds);
			}
			return GetTextBounds();
		}
	}

	public float flexibleHeight => m_flexibleHeight;

	public float flexibleWidth => m_flexibleWidth;

	public float minWidth => m_minWidth;

	public float minHeight => m_minHeight;

	public float maxWidth => m_maxWidth;

	public float maxHeight => m_maxHeight;

	protected LayoutElement layoutElement
	{
		get
		{
			if (m_LayoutElement == null)
			{
				m_LayoutElement = GetComponent<LayoutElement>();
			}
			return m_LayoutElement;
		}
	}

	public virtual float preferredWidth
	{
		get
		{
			if (!m_isPreferredWidthDirty)
			{
				return m_preferredWidth;
			}
			m_preferredWidth = GetPreferredWidth();
			return m_preferredWidth;
		}
	}

	public virtual float preferredHeight
	{
		get
		{
			if (!m_isPreferredHeightDirty)
			{
				return m_preferredHeight;
			}
			m_preferredHeight = GetPreferredHeight();
			return m_preferredHeight;
		}
	}

	public virtual float renderedWidth => GetRenderedWidth();

	public virtual float renderedHeight => GetRenderedHeight();

	public int layoutPriority => m_layoutPriority;

	public virtual event Action<TMP_TextInfo> OnPreRenderText = delegate
	{
	};

	public void CacheRectTransform()
	{
		m_rectTransform = GetComponent<RectTransform>();
	}

	protected virtual void LoadFontAsset()
	{
	}

	protected virtual void SetSharedMaterial(Material mat)
	{
	}

	protected virtual Material GetMaterial(Material mat)
	{
		return null;
	}

	protected virtual void SetFontBaseMaterial(Material mat)
	{
	}

	protected virtual Material[] GetSharedMaterials()
	{
		return null;
	}

	protected virtual void SetSharedMaterials(Material[] materials)
	{
	}

	protected virtual Material[] GetMaterials(Material[] mats)
	{
		return null;
	}

	protected virtual Material CreateMaterialInstance(Material source)
	{
		Material obj = new Material(source)
		{
			shaderKeywords = source.shaderKeywords
		};
		obj.name += " (Instance)";
		return obj;
	}

	protected void SetVertexColorGradient(TMP_ColorGradient gradient)
	{
		if (!(gradient == null))
		{
			m_fontColorGradient.bottomLeft = gradient.bottomLeft;
			m_fontColorGradient.bottomRight = gradient.bottomRight;
			m_fontColorGradient.topLeft = gradient.topLeft;
			m_fontColorGradient.topRight = gradient.topRight;
			SetVerticesDirty();
		}
	}

	protected void SetTextSortingOrder(VertexSortingOrder order)
	{
	}

	protected void SetTextSortingOrder(int[] order)
	{
	}

	protected virtual void SetFaceColor(Color32 color)
	{
	}

	protected virtual void SetOutlineColor(Color32 color)
	{
	}

	protected virtual void SetOutlineThickness(float thickness)
	{
	}

	protected virtual void SetShaderDepth()
	{
	}

	protected virtual void SetCulling()
	{
	}

	protected virtual float GetPaddingForMaterial()
	{
		return 0f;
	}

	protected virtual float GetPaddingForMaterial(Material mat)
	{
		return 0f;
	}

	protected virtual Vector3[] GetTextContainerLocalCorners()
	{
		return null;
	}

	public virtual void ForceMeshUpdate()
	{
	}

	public virtual void ForceMeshUpdate(bool ignoreActiveState)
	{
	}

	internal void SetTextInternal(string text)
	{
		m_text = text;
		m_renderMode = TextRenderFlags.DontRender;
		m_isInputParsingRequired = true;
		ForceMeshUpdate();
		m_renderMode = TextRenderFlags.Render;
	}

	public virtual void UpdateGeometry(Mesh mesh, int index)
	{
	}

	public virtual void UpdateVertexData(TMP_VertexDataUpdateFlags flags)
	{
	}

	public virtual void UpdateVertexData()
	{
	}

	public virtual void SetVertices(Vector3[] vertices)
	{
	}

	public virtual void UpdateMeshPadding()
	{
	}

	public override void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
	{
		base.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha);
		InternalCrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha);
	}

	public override void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
	{
		base.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
		InternalCrossFadeAlpha(alpha, duration, ignoreTimeScale);
	}

	protected virtual void InternalCrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
	{
	}

	protected virtual void InternalCrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
	{
	}

	protected void ParseInputText()
	{
		m_isInputParsingRequired = false;
		StringToCharArray(m_text, ref m_TextParsingBuffer);
		SetArraySizes(m_TextParsingBuffer);
	}

	protected void StringToCharArray(string sourceText, ref UnicodeChar[] charBuffer)
	{
		if (charBuffer == null)
		{
			charBuffer = new UnicodeChar[8];
		}
		if (sourceText == null)
		{
			charBuffer[0].unicode = 0;
			return;
		}
		m_styleStack.SetDefault(0);
		int num = 0;
		for (int i = 0; i < sourceText.Length; i++)
		{
			if (char.IsHighSurrogate(sourceText[i]) && i + 1 < sourceText.Length && char.IsLowSurrogate(sourceText[i + 1]))
			{
				if (num == charBuffer.Length)
				{
					ResizeInternalArray(ref charBuffer);
				}
				charBuffer[num].unicode = char.ConvertToUtf32(sourceText[i], sourceText[i + 1]);
				charBuffer[num].stringIndex = i;
				charBuffer[num].length = 2;
				i++;
				num++;
			}
			else if (sourceText[i] == '<' && m_isRichText && IsTagName(ref sourceText, "<BR>", i))
			{
				if (num == charBuffer.Length)
				{
					ResizeInternalArray(ref charBuffer);
				}
				charBuffer[num].unicode = 10;
				charBuffer[num].stringIndex = i;
				charBuffer[num].length = 1;
				num++;
				i += 3;
			}
			else
			{
				if (num == charBuffer.Length)
				{
					ResizeInternalArray(ref charBuffer);
				}
				charBuffer[num].unicode = sourceText[i];
				charBuffer[num].stringIndex = i;
				charBuffer[num].length = 1;
				num++;
			}
		}
		if (num == charBuffer.Length)
		{
			ResizeInternalArray(ref charBuffer);
		}
		charBuffer[num].unicode = 0;
	}

	protected void StringBuilderToIntArray(StringBuilder sourceText, ref UnicodeChar[] charBuffer)
	{
		if (sourceText == null)
		{
			charBuffer[0].unicode = 0;
			return;
		}
		if (charBuffer == null)
		{
			charBuffer = new UnicodeChar[8];
		}
		m_styleStack.Clear();
		int num = 0;
		for (int i = 0; i < sourceText.Length; i++)
		{
			if (char.IsHighSurrogate(sourceText[i]) && char.IsLowSurrogate(sourceText[i + 1]))
			{
				if (num == charBuffer.Length)
				{
					ResizeInternalArray(ref charBuffer);
				}
				charBuffer[num].unicode = char.ConvertToUtf32(sourceText[i], sourceText[i + 1]);
				i++;
				num++;
			}
			else if (sourceText[i] == '<' && IsTagName(ref sourceText, "<BR>", i))
			{
				if (num == charBuffer.Length)
				{
					ResizeInternalArray(ref charBuffer);
				}
				charBuffer[num].unicode = 10;
				num++;
				i += 3;
			}
			else
			{
				if (num == charBuffer.Length)
				{
					ResizeInternalArray(ref charBuffer);
				}
				charBuffer[num].unicode = sourceText[i];
				num++;
			}
		}
		if (num == charBuffer.Length)
		{
			ResizeInternalArray(ref charBuffer);
		}
		charBuffer[num].unicode = 0;
	}

	private bool IsTagName(ref string text, string tag, int index)
	{
		if (text.Length < index + tag.Length)
		{
			return false;
		}
		for (int i = 0; i < tag.Length; i++)
		{
			if (TMP_TextUtilities.ToUpperFast(text[index + i]) != tag[i])
			{
				return false;
			}
		}
		return true;
	}

	private bool IsTagName(ref char[] text, string tag, int index)
	{
		if (text.Length < index + tag.Length)
		{
			return false;
		}
		for (int i = 0; i < tag.Length; i++)
		{
			if (TMP_TextUtilities.ToUpperFast(text[index + i]) != tag[i])
			{
				return false;
			}
		}
		return true;
	}

	private bool IsTagName(ref int[] text, string tag, int index)
	{
		if (text.Length < index + tag.Length)
		{
			return false;
		}
		for (int i = 0; i < tag.Length; i++)
		{
			if (TMP_TextUtilities.ToUpperFast((char)text[index + i]) != tag[i])
			{
				return false;
			}
		}
		return true;
	}

	private bool IsTagName(ref StringBuilder text, string tag, int index)
	{
		if (text.Length < index + tag.Length)
		{
			return false;
		}
		for (int i = 0; i < tag.Length; i++)
		{
			if (TMP_TextUtilities.ToUpperFast(text[index + i]) != tag[i])
			{
				return false;
			}
		}
		return true;
	}

	private int GetTagHashCode(ref string text, int index, out int closeIndex)
	{
		int num = 0;
		closeIndex = 0;
		for (int i = index; i < text.Length; i++)
		{
			if (text[i] != '"')
			{
				if (text[i] == '>')
				{
					closeIndex = i;
					break;
				}
				num = ((num << 5) + num) ^ text[i];
			}
		}
		return num;
	}

	private int GetTagHashCode(ref char[] text, int index, out int closeIndex)
	{
		int num = 0;
		closeIndex = 0;
		for (int i = index; i < text.Length; i++)
		{
			if (text[i] != '"')
			{
				if (text[i] == '>')
				{
					closeIndex = i;
					break;
				}
				num = ((num << 5) + num) ^ text[i];
			}
		}
		return num;
	}

	private int GetTagHashCode(ref int[] text, int index, out int closeIndex)
	{
		int num = 0;
		closeIndex = 0;
		for (int i = index; i < text.Length; i++)
		{
			if (text[i] != 34)
			{
				if (text[i] == 62)
				{
					closeIndex = i;
					break;
				}
				num = ((num << 5) + num) ^ text[i];
			}
		}
		return num;
	}

	private int GetTagHashCode(ref StringBuilder text, int index, out int closeIndex)
	{
		int num = 0;
		closeIndex = 0;
		for (int i = index; i < text.Length; i++)
		{
			if (text[i] != '"')
			{
				if (text[i] == '>')
				{
					closeIndex = i;
					break;
				}
				num = ((num << 5) + num) ^ text[i];
			}
		}
		return num;
	}

	private void ResizeInternalArray<T>(ref T[] array)
	{
		int newSize = Mathf.NextPowerOfTwo(array.Length + 1);
		Array.Resize(ref array, newSize);
	}

	protected virtual int SetArraySizes(UnicodeChar[] chars)
	{
		return 0;
	}

	protected virtual void GenerateTextMesh()
	{
	}

	public Vector2 GetPreferredValues()
	{
		if (m_isInputParsingRequired || m_isTextTruncated)
		{
			m_isCalculatingPreferredValues = true;
			ParseInputText();
		}
		float x = GetPreferredWidth();
		float y = GetPreferredHeight();
		return new Vector2(x, y);
	}

	public Vector2 GetPreferredValues(float width, float height)
	{
		if (m_isInputParsingRequired || m_isTextTruncated)
		{
			m_isCalculatingPreferredValues = true;
			ParseInputText();
		}
		Vector2 vector = new Vector2(width, height);
		float x = GetPreferredWidth(vector);
		float y = GetPreferredHeight(vector);
		return new Vector2(x, y);
	}

	public Vector2 GetPreferredValues(string text)
	{
		m_isCalculatingPreferredValues = true;
		StringToCharArray(text, ref m_TextParsingBuffer);
		SetArraySizes(m_TextParsingBuffer);
		Vector2 vector = k_LargePositiveVector2;
		float x = GetPreferredWidth(vector);
		float y = GetPreferredHeight(vector);
		return new Vector2(x, y);
	}

	public Vector2 GetPreferredValues(string text, float width, float height)
	{
		m_isCalculatingPreferredValues = true;
		StringToCharArray(text, ref m_TextParsingBuffer);
		SetArraySizes(m_TextParsingBuffer);
		Vector2 vector = new Vector2(width, height);
		float x = GetPreferredWidth(vector);
		float y = GetPreferredHeight(vector);
		return new Vector2(x, y);
	}

	protected float GetPreferredWidth()
	{
		if (TMP_Settings.instance == null)
		{
			return 0f;
		}
		float defaultFontSize = (m_enableAutoSizing ? m_fontSizeMax : m_fontSize);
		m_minFontSize = m_fontSizeMin;
		m_maxFontSize = m_fontSizeMax;
		m_charWidthAdjDelta = 0f;
		Vector2 marginSize = k_LargePositiveVector2;
		if (m_isInputParsingRequired || m_isTextTruncated)
		{
			m_isCalculatingPreferredValues = true;
			ParseInputText();
		}
		m_recursiveCount = 0;
		float x = CalculatePreferredValues(defaultFontSize, marginSize, ignoreTextAutoSizing: true).x;
		m_isPreferredWidthDirty = false;
		return x;
	}

	protected float GetPreferredWidth(Vector2 margin)
	{
		float defaultFontSize = (m_enableAutoSizing ? m_fontSizeMax : m_fontSize);
		m_minFontSize = m_fontSizeMin;
		m_maxFontSize = m_fontSizeMax;
		m_charWidthAdjDelta = 0f;
		m_recursiveCount = 0;
		return CalculatePreferredValues(defaultFontSize, margin, ignoreTextAutoSizing: true).x;
	}

	protected float GetPreferredHeight()
	{
		if (TMP_Settings.instance == null)
		{
			return 0f;
		}
		float defaultFontSize = (m_enableAutoSizing ? m_fontSizeMax : m_fontSize);
		m_minFontSize = m_fontSizeMin;
		m_maxFontSize = m_fontSizeMax;
		m_charWidthAdjDelta = 0f;
		Vector2 marginSize = new Vector2((m_marginWidth != 0f) ? m_marginWidth : k_LargePositiveFloat, k_LargePositiveFloat);
		if (m_isInputParsingRequired || m_isTextTruncated)
		{
			m_isCalculatingPreferredValues = true;
			ParseInputText();
		}
		m_recursiveCount = 0;
		float y = CalculatePreferredValues(defaultFontSize, marginSize, !m_enableAutoSizing).y;
		m_isPreferredHeightDirty = false;
		return y;
	}

	protected float GetPreferredHeight(Vector2 margin)
	{
		float defaultFontSize = (m_enableAutoSizing ? m_fontSizeMax : m_fontSize);
		m_minFontSize = m_fontSizeMin;
		m_maxFontSize = m_fontSizeMax;
		m_charWidthAdjDelta = 0f;
		m_recursiveCount = 0;
		return CalculatePreferredValues(defaultFontSize, margin, ignoreTextAutoSizing: true).y;
	}

	public Vector2 GetRenderedValues()
	{
		return GetTextBounds().size;
	}

	public Vector2 GetRenderedValues(bool onlyVisibleCharacters)
	{
		return GetTextBounds(onlyVisibleCharacters).size;
	}

	protected float GetRenderedWidth()
	{
		return GetRenderedValues().x;
	}

	protected float GetRenderedWidth(bool onlyVisibleCharacters)
	{
		return GetRenderedValues(onlyVisibleCharacters).x;
	}

	protected float GetRenderedHeight()
	{
		return GetRenderedValues().y;
	}

	protected float GetRenderedHeight(bool onlyVisibleCharacters)
	{
		return GetRenderedValues(onlyVisibleCharacters).y;
	}

	protected virtual Vector2 CalculatePreferredValues(float defaultFontSize, Vector2 marginSize, bool ignoreTextAutoSizing)
	{
		if (m_fontAsset == null || m_fontAsset.characterLookupTable == null)
		{
			UnityEngine.Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned to Object ID: " + GetInstanceID());
			return Vector2.zero;
		}
		if (m_TextParsingBuffer == null || m_TextParsingBuffer.Length == 0 || m_TextParsingBuffer[0].unicode == 0)
		{
			return Vector2.zero;
		}
		m_currentFontAsset = m_fontAsset;
		m_currentMaterial = m_sharedMaterial;
		m_currentMaterialIndex = 0;
		m_materialReferenceStack.SetDefault(new MaterialReference(0, m_currentFontAsset, m_currentMaterial, m_padding));
		int totalCharacterCount = m_totalCharacterCount;
		if (m_internalCharacterInfo == null || totalCharacterCount > m_internalCharacterInfo.Length)
		{
			m_internalCharacterInfo = new TMP_CharacterInfo[(totalCharacterCount > 1024) ? (totalCharacterCount + 256) : Mathf.NextPowerOfTwo(totalCharacterCount)];
		}
		float num = (m_fontScale = defaultFontSize / (float)m_fontAsset.faceInfo.pointSize * m_fontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f));
		float num2 = num;
		m_fontScaleMultiplier = 1f;
		m_currentFontSize = defaultFontSize;
		m_sizeStack.SetDefault(m_currentFontSize);
		float num3 = 0f;
		int num4 = 0;
		m_FontStyleInternal = m_fontStyle;
		m_lineJustification = m_textAlignment;
		m_lineJustificationStack.SetDefault(m_lineJustification);
		float num5 = 1f;
		m_baselineOffset = 0f;
		m_baselineOffsetStack.Clear();
		m_lineOffset = 0f;
		m_lineHeight = -32767f;
		float num6 = m_currentFontAsset.faceInfo.lineHeight - (m_currentFontAsset.faceInfo.ascentLine - m_currentFontAsset.faceInfo.descentLine);
		m_cSpacing = 0f;
		m_monoSpacing = 0f;
		float num7 = 0f;
		m_xAdvance = 0f;
		float a = 0f;
		tag_LineIndent = 0f;
		tag_Indent = 0f;
		m_indentStack.SetDefault(0f);
		tag_NoParsing = false;
		m_characterCount = 0;
		m_firstCharacterOfLine = 0;
		m_maxLineAscender = k_LargeNegativeFloat;
		m_maxLineDescender = k_LargePositiveFloat;
		m_lineNumber = 0;
		float x = marginSize.x;
		m_marginLeft = 0f;
		m_marginRight = 0f;
		m_width = -1f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		m_isCalculatingPreferredValues = true;
		m_maxAscender = 0f;
		m_maxDescender = 0f;
		bool flag = true;
		bool flag2 = false;
		WordWrapState state = default(WordWrapState);
		SaveWordWrappingState(ref state, 0, 0);
		WordWrapState state2 = default(WordWrapState);
		int num11 = 0;
		m_recursiveCount++;
		for (int i = 0; m_TextParsingBuffer[i].unicode != 0; i++)
		{
			num4 = m_TextParsingBuffer[i].unicode;
			if (m_isRichText && num4 == 60)
			{
				m_isParsingText = true;
				m_textElementType = TMP_TextElementType.Character;
				if (ValidateHtmlTag(m_TextParsingBuffer, i + 1, out var endIndex))
				{
					i = endIndex;
					if (m_textElementType == TMP_TextElementType.Character)
					{
						continue;
					}
				}
			}
			else
			{
				m_textElementType = m_textInfo.characterInfo[m_characterCount].elementType;
				m_currentMaterialIndex = m_textInfo.characterInfo[m_characterCount].materialReferenceIndex;
				m_currentFontAsset = m_textInfo.characterInfo[m_characterCount].fontAsset;
			}
			_ = m_currentMaterialIndex;
			bool isUsingAlternateTypeface = m_textInfo.characterInfo[m_characterCount].isUsingAlternateTypeface;
			m_isParsingText = false;
			float num12 = 1f;
			if (m_textElementType == TMP_TextElementType.Character)
			{
				if ((m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
				{
					if (char.IsLower((char)num4))
					{
						num4 = char.ToUpper((char)num4);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
				{
					if (char.IsUpper((char)num4))
					{
						num4 = char.ToLower((char)num4);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num4))
				{
					num12 = 0.8f;
					num4 = char.ToUpper((char)num4);
				}
			}
			if (m_textElementType == TMP_TextElementType.Character)
			{
				m_cached_TextElement = m_textInfo.characterInfo[m_characterCount].textElement;
				if (m_cached_TextElement == null)
				{
					continue;
				}
				m_currentMaterialIndex = m_textInfo.characterInfo[m_characterCount].materialReferenceIndex;
				m_fontScale = m_currentFontSize * num12 / (float)m_currentFontAsset.faceInfo.pointSize * m_currentFontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f);
				num2 = m_fontScale * m_fontScaleMultiplier * m_cached_TextElement.scale;
				m_internalCharacterInfo[m_characterCount].elementType = TMP_TextElementType.Character;
			}
			float num13 = num2;
			if (num4 == 173)
			{
				num2 = 0f;
			}
			m_internalCharacterInfo[m_characterCount].character = (char)num4;
			TMP_GlyphValueRecord tMP_GlyphValueRecord = default(TMP_GlyphValueRecord);
			float num14 = m_characterSpacing;
			if (m_enableKerning)
			{
				if (m_characterCount < totalCharacterCount - 1)
				{
					uint glyphIndex = m_cached_TextElement.glyphIndex;
					uint glyphIndex2 = m_textInfo.characterInfo[m_characterCount + 1].textElement.glyphIndex;
					long key = new GlyphPairKey(glyphIndex, glyphIndex2).key;
					if (m_currentFontAsset.fontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(key, out var value))
					{
						tMP_GlyphValueRecord = value.firstAdjustmentRecord.glyphValueRecord;
						num14 = (((value.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num14);
					}
				}
				if (m_characterCount >= 1)
				{
					uint glyphIndex3 = m_textInfo.characterInfo[m_characterCount - 1].textElement.glyphIndex;
					uint glyphIndex4 = m_cached_TextElement.glyphIndex;
					long key2 = new GlyphPairKey(glyphIndex3, glyphIndex4).key;
					if (m_currentFontAsset.fontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(key2, out var value2))
					{
						tMP_GlyphValueRecord += value2.secondAdjustmentRecord.glyphValueRecord;
						num14 = (((value2.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num14);
					}
				}
			}
			float num15 = 0f;
			if (m_monoSpacing != 0f)
			{
				num15 = m_monoSpacing / 2f - (m_cached_TextElement.glyph.metrics.width / 2f + m_cached_TextElement.glyph.metrics.horizontalBearingX) * num2;
				m_xAdvance += num15;
			}
			num5 = ((m_textElementType != TMP_TextElementType.Character || isUsingAlternateTypeface || (m_FontStyleInternal & FontStyles.Bold) != FontStyles.Bold) ? 1f : (1f + m_currentFontAsset.boldSpacing * 0.01f));
			m_internalCharacterInfo[m_characterCount].baseLine = 0f - m_lineOffset + m_baselineOffset;
			float num16 = m_currentFontAsset.faceInfo.ascentLine * ((m_textElementType == TMP_TextElementType.Character) ? (num2 / num12) : m_internalCharacterInfo[m_characterCount].scale) + m_baselineOffset;
			m_internalCharacterInfo[m_characterCount].ascender = num16 - m_lineOffset;
			m_maxLineAscender = ((num16 > m_maxLineAscender) ? num16 : m_maxLineAscender);
			float num17 = m_currentFontAsset.faceInfo.descentLine * ((m_textElementType == TMP_TextElementType.Character) ? (num2 / num12) : m_internalCharacterInfo[m_characterCount].scale) + m_baselineOffset;
			float num18 = (m_internalCharacterInfo[m_characterCount].descender = num17 - m_lineOffset);
			m_maxLineDescender = ((num17 < m_maxLineDescender) ? num17 : m_maxLineDescender);
			if ((m_FontStyleInternal & FontStyles.Subscript) == FontStyles.Subscript || (m_FontStyleInternal & FontStyles.Superscript) == FontStyles.Superscript)
			{
				float num19 = (num16 - m_baselineOffset) / m_currentFontAsset.faceInfo.subscriptSize;
				num16 = m_maxLineAscender;
				m_maxLineAscender = ((num19 > m_maxLineAscender) ? num19 : m_maxLineAscender);
				float num20 = (num17 - m_baselineOffset) / m_currentFontAsset.faceInfo.subscriptSize;
				num17 = m_maxLineDescender;
				m_maxLineDescender = ((num20 < m_maxLineDescender) ? num20 : m_maxLineDescender);
			}
			if (m_lineNumber == 0)
			{
				m_maxAscender = ((m_maxAscender > num16) ? m_maxAscender : num16);
			}
			if (num4 == 9 || num4 == 160 || num4 == 8199 || (!char.IsWhiteSpace((char)num4) && num4 != 8203))
			{
				float num21 = ((m_width != -1f) ? Mathf.Min(x + 0.0001f - m_marginLeft - m_marginRight, m_width) : (x + 0.0001f - m_marginLeft - m_marginRight));
				bool flag3 = (m_lineJustification & (TextAlignmentOptions)16) == (TextAlignmentOptions)16 || (m_lineJustification & (TextAlignmentOptions)8) == (TextAlignmentOptions)8;
				num10 = m_xAdvance + m_cached_TextElement.glyph.metrics.horizontalAdvance * (1f - m_charWidthAdjDelta) * ((num4 != 173) ? num2 : num13);
				if (num10 > num21 * (flag3 ? 1.05f : 1f))
				{
					if (enableWordWrapping && m_characterCount != m_firstCharacterOfLine)
					{
						if (num11 == state2.previous_WordBreak || flag)
						{
							if (!ignoreTextAutoSizing && m_currentFontSize > m_fontSizeMin)
							{
								if (m_charWidthAdjDelta < m_charWidthMaxAdj / 100f)
								{
									m_recursiveCount = 0;
									m_charWidthAdjDelta += 0.01f;
									return CalculatePreferredValues(defaultFontSize, marginSize, ignoreTextAutoSizing: false);
								}
								m_maxFontSize = defaultFontSize;
								defaultFontSize -= Mathf.Max((defaultFontSize - m_minFontSize) / 2f, 0.05f);
								defaultFontSize = (float)(int)(Mathf.Max(defaultFontSize, m_fontSizeMin) * 20f + 0.5f) / 20f;
								if (m_recursiveCount > 20)
								{
									return new Vector2(num8, num9);
								}
								return CalculatePreferredValues(defaultFontSize, marginSize, ignoreTextAutoSizing: false);
							}
							if (!m_isCharacterWrappingEnabled)
							{
								m_isCharacterWrappingEnabled = true;
							}
							else
							{
								flag2 = true;
							}
						}
						i = RestoreWordWrappingState(ref state2);
						num11 = i;
						if (m_TextParsingBuffer[i].unicode == 173)
						{
							m_isTextTruncated = true;
							m_TextParsingBuffer[i].unicode = 45;
							return CalculatePreferredValues(defaultFontSize, marginSize, ignoreTextAutoSizing: true);
						}
						if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == -32767f)
						{
							float num22 = m_maxLineAscender - m_startOfLineAscender;
							m_lineOffset += num22;
							state2.lineOffset = m_lineOffset;
							state2.previousLineAscender = m_maxLineAscender;
						}
						float num23 = m_maxLineAscender - m_lineOffset;
						float num24 = m_maxLineDescender - m_lineOffset;
						m_maxDescender = ((m_maxDescender < num24) ? m_maxDescender : num24);
						m_firstCharacterOfLine = m_characterCount;
						num8 += m_xAdvance;
						num9 = ((!m_enableWordWrapping) ? Mathf.Max(num9, num23 - num24) : (m_maxAscender - m_maxDescender));
						SaveWordWrappingState(ref state, i, m_characterCount - 1);
						m_lineNumber++;
						if (m_lineHeight == -32767f)
						{
							float num25 = m_internalCharacterInfo[m_characterCount].ascender - m_internalCharacterInfo[m_characterCount].baseLine;
							num7 = 0f - m_maxLineDescender + num25 + (num6 + m_lineSpacing + m_lineSpacingDelta) * num;
							m_lineOffset += num7;
							m_startOfLineAscender = num25;
						}
						else
						{
							m_lineOffset += m_lineHeight + m_lineSpacing * num;
						}
						m_maxLineAscender = k_LargeNegativeFloat;
						m_maxLineDescender = k_LargePositiveFloat;
						m_xAdvance = 0f + tag_Indent;
						continue;
					}
					if (!ignoreTextAutoSizing && defaultFontSize > m_fontSizeMin)
					{
						if (m_charWidthAdjDelta < m_charWidthMaxAdj / 100f)
						{
							m_recursiveCount = 0;
							m_charWidthAdjDelta += 0.01f;
							return CalculatePreferredValues(defaultFontSize, marginSize, ignoreTextAutoSizing: false);
						}
						m_maxFontSize = defaultFontSize;
						defaultFontSize -= Mathf.Max((defaultFontSize - m_minFontSize) / 2f, 0.05f);
						defaultFontSize = (float)(int)(Mathf.Max(defaultFontSize, m_fontSizeMin) * 20f + 0.5f) / 20f;
						if (m_recursiveCount > 20)
						{
							return new Vector2(num8, num9);
						}
						return CalculatePreferredValues(defaultFontSize, marginSize, ignoreTextAutoSizing: false);
					}
				}
			}
			if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == -32767f && !m_isNewPage)
			{
				float num26 = m_maxLineAscender - m_startOfLineAscender;
				num18 -= num26;
				m_lineOffset += num26;
				m_startOfLineAscender += num26;
				state2.lineOffset = m_lineOffset;
				state2.previousLineAscender = m_startOfLineAscender;
			}
			if (num4 == 9)
			{
				float num27 = m_currentFontAsset.faceInfo.tabWidth * num2;
				float num28 = Mathf.Ceil(m_xAdvance / num27) * num27;
				m_xAdvance = ((num28 > m_xAdvance) ? num28 : (m_xAdvance + num27));
			}
			else if (m_monoSpacing != 0f)
			{
				m_xAdvance += (m_monoSpacing - num15 + (num14 + m_currentFontAsset.normalSpacingOffset) * num2 + m_cSpacing) * (1f - m_charWidthAdjDelta);
				if (char.IsWhiteSpace((char)num4) || num4 == 8203)
				{
					m_xAdvance += m_wordSpacing * num2;
				}
			}
			else
			{
				m_xAdvance += ((m_cached_TextElement.glyph.metrics.horizontalAdvance * num5 + num14 + m_currentFontAsset.normalSpacingOffset + tMP_GlyphValueRecord.xAdvance) * num2 + m_cSpacing) * (1f - m_charWidthAdjDelta);
				if (char.IsWhiteSpace((char)num4) || num4 == 8203)
				{
					m_xAdvance += m_wordSpacing * num2;
				}
			}
			if (num4 == 13)
			{
				a = Mathf.Max(a, num8 + m_xAdvance);
				num8 = 0f;
				m_xAdvance = 0f + tag_Indent;
			}
			if (num4 == 10 || m_characterCount == totalCharacterCount - 1)
			{
				if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == -32767f)
				{
					float num29 = m_maxLineAscender - m_startOfLineAscender;
					num18 -= num29;
					m_lineOffset += num29;
				}
				float num30 = m_maxLineDescender - m_lineOffset;
				m_maxDescender = ((m_maxDescender < num30) ? m_maxDescender : num30);
				m_firstCharacterOfLine = m_characterCount + 1;
				if (num4 == 10 && m_characterCount != totalCharacterCount - 1)
				{
					a = Mathf.Max(a, num8 + num10);
					num8 = 0f;
				}
				else
				{
					num8 = Mathf.Max(a, num8 + num10);
				}
				num9 = m_maxAscender - m_maxDescender;
				if (num4 == 10)
				{
					SaveWordWrappingState(ref state, i, m_characterCount);
					SaveWordWrappingState(ref state2, i, m_characterCount);
					m_lineNumber++;
					if (m_lineHeight == -32767f)
					{
						num7 = 0f - m_maxLineDescender + num16 + (num6 + m_lineSpacing + m_paragraphSpacing + m_lineSpacingDelta) * num;
						m_lineOffset += num7;
					}
					else
					{
						m_lineOffset += m_lineHeight + (m_lineSpacing + m_paragraphSpacing) * num;
					}
					m_maxLineAscender = k_LargeNegativeFloat;
					m_maxLineDescender = k_LargePositiveFloat;
					m_startOfLineAscender = num16;
					m_xAdvance = 0f + tag_LineIndent + tag_Indent;
					m_characterCount++;
					continue;
				}
			}
			if (m_enableWordWrapping || m_overflowMode == TextOverflowModes.Truncate || m_overflowMode == TextOverflowModes.Ellipsis)
			{
				if ((char.IsWhiteSpace((char)num4) || num4 == 8203 || num4 == 45 || num4 == 173) && !m_isNonBreakingSpace && num4 != 160 && num4 != 8209 && num4 != 8239 && num4 != 8288)
				{
					SaveWordWrappingState(ref state2, i, m_characterCount);
					m_isCharacterWrappingEnabled = false;
					flag = false;
				}
				else if (((num4 > 4352 && num4 < 4607) || (num4 > 11904 && num4 < 40959) || (num4 > 43360 && num4 < 43391) || (num4 > 44032 && num4 < 55295) || (num4 > 63744 && num4 < 64255) || (num4 > 65072 && num4 < 65103) || (num4 > 65280 && num4 < 65519)) && !m_isNonBreakingSpace)
				{
					if (flag || flag2 || (!TMP_Settings.linebreakingRules.leadingCharacters.ContainsKey(num4) && m_characterCount < totalCharacterCount - 1 && !TMP_Settings.linebreakingRules.followingCharacters.ContainsKey(m_internalCharacterInfo[m_characterCount + 1].character)))
					{
						SaveWordWrappingState(ref state2, i, m_characterCount);
						m_isCharacterWrappingEnabled = false;
						flag = false;
					}
				}
				else if (flag || m_isCharacterWrappingEnabled || flag2)
				{
					SaveWordWrappingState(ref state2, i, m_characterCount);
				}
			}
			m_characterCount++;
		}
		num3 = m_maxFontSize - m_minFontSize;
		if (!m_isCharacterWrappingEnabled && !ignoreTextAutoSizing && num3 > 0.051f && defaultFontSize < m_fontSizeMax)
		{
			m_minFontSize = defaultFontSize;
			defaultFontSize += Mathf.Max((m_maxFontSize - defaultFontSize) / 2f, 0.05f);
			defaultFontSize = (float)(int)(Mathf.Min(defaultFontSize, m_fontSizeMax) * 20f + 0.5f) / 20f;
			if (m_recursiveCount > 20)
			{
				return new Vector2(num8, num9);
			}
			return CalculatePreferredValues(defaultFontSize, marginSize, ignoreTextAutoSizing: false);
		}
		m_isCharacterWrappingEnabled = false;
		m_isCalculatingPreferredValues = false;
		num8 += ((m_margin.x > 0f) ? m_margin.x : 0f);
		num8 += ((m_margin.z > 0f) ? m_margin.z : 0f);
		num9 += ((m_margin.y > 0f) ? m_margin.y : 0f);
		num9 += ((m_margin.w > 0f) ? m_margin.w : 0f);
		num8 = (float)(int)(num8 * 100f + 1f) / 100f;
		num9 = (float)(int)(num9 * 100f + 1f) / 100f;
		return new Vector2(num8, num9);
	}

	protected virtual Bounds GetCompoundBounds()
	{
		return default(Bounds);
	}

	protected Bounds GetTextBounds()
	{
		if (m_textInfo == null || m_textInfo.characterCount > m_textInfo.characterInfo.Length)
		{
			return default(Bounds);
		}
		Extents extents = new Extents(k_LargePositiveVector2, k_LargeNegativeVector2);
		for (int i = 0; i < m_textInfo.characterCount && i < m_textInfo.characterInfo.Length; i++)
		{
			if (m_textInfo.characterInfo[i].isVisible)
			{
				extents.min.x = Mathf.Min(extents.min.x, m_textInfo.characterInfo[i].bottomLeft.x);
				extents.min.y = Mathf.Min(extents.min.y, m_textInfo.characterInfo[i].descender);
				extents.max.x = Mathf.Max(extents.max.x, m_textInfo.characterInfo[i].xAdvance);
				extents.max.y = Mathf.Max(extents.max.y, m_textInfo.characterInfo[i].ascender);
			}
		}
		Vector2 vector = default(Vector2);
		vector.x = extents.max.x - extents.min.x;
		vector.y = extents.max.y - extents.min.y;
		return new Bounds((extents.min + extents.max) / 2f, vector);
	}

	protected Bounds GetTextBounds(bool onlyVisibleCharacters)
	{
		if (m_textInfo == null)
		{
			return default(Bounds);
		}
		Extents extents = new Extents(k_LargePositiveVector2, k_LargeNegativeVector2);
		for (int i = 0; i < m_textInfo.characterCount && !((i > maxVisibleCharacters || m_textInfo.characterInfo[i].lineNumber > m_maxVisibleLines) && onlyVisibleCharacters); i++)
		{
			if (!onlyVisibleCharacters || m_textInfo.characterInfo[i].isVisible)
			{
				extents.min.x = Mathf.Min(extents.min.x, m_textInfo.characterInfo[i].origin);
				extents.min.y = Mathf.Min(extents.min.y, m_textInfo.characterInfo[i].descender);
				extents.max.x = Mathf.Max(extents.max.x, m_textInfo.characterInfo[i].xAdvance);
				extents.max.y = Mathf.Max(extents.max.y, m_textInfo.characterInfo[i].ascender);
			}
		}
		Vector2 vector = default(Vector2);
		vector.x = extents.max.x - extents.min.x;
		vector.y = extents.max.y - extents.min.y;
		return new Bounds((extents.min + extents.max) / 2f, vector);
	}

	protected virtual void AdjustLineOffset(int startIndex, int endIndex, float offset)
	{
	}

	protected void ResizeLineExtents(int size)
	{
		size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size + 1));
		TMP_LineInfo[] array = new TMP_LineInfo[size];
		for (int i = 0; i < size; i++)
		{
			if (i < m_textInfo.lineInfo.Length)
			{
				array[i] = m_textInfo.lineInfo[i];
				continue;
			}
			array[i].lineExtents.min = k_LargePositiveVector2;
			array[i].lineExtents.max = k_LargeNegativeVector2;
			array[i].ascender = k_LargeNegativeFloat;
			array[i].descender = k_LargePositiveFloat;
		}
		m_textInfo.lineInfo = array;
	}

	public virtual TMP_TextInfo GetTextInfo(string text)
	{
		return null;
	}

	public virtual void ComputeMarginSize()
	{
	}

	protected void SaveWordWrappingState(ref WordWrapState state, int index, int count)
	{
		state.currentFontAsset = m_currentFontAsset;
		state.currentMaterial = m_currentMaterial;
		state.currentMaterialIndex = m_currentMaterialIndex;
		state.previous_WordBreak = index;
		state.total_CharacterCount = count;
		state.visible_CharacterCount = m_lineVisibleCharacterCount;
		state.visible_LinkCount = m_textInfo.linkCount;
		state.firstCharacterIndex = m_firstCharacterOfLine;
		state.firstVisibleCharacterIndex = m_firstVisibleCharacterOfLine;
		state.lastVisibleCharIndex = m_lastVisibleCharacterOfLine;
		state.fontStyle = m_FontStyleInternal;
		state.fontScale = m_fontScale;
		state.fontScaleMultiplier = m_fontScaleMultiplier;
		state.currentFontSize = m_currentFontSize;
		state.xAdvance = m_xAdvance;
		state.maxCapHeight = m_maxCapHeight;
		state.maxAscender = m_maxAscender;
		state.maxDescender = m_maxDescender;
		state.maxLineAscender = m_maxLineAscender;
		state.maxLineDescender = m_maxLineDescender;
		state.previousLineAscender = m_startOfLineAscender;
		state.preferredWidth = m_preferredWidth;
		state.preferredHeight = m_preferredHeight;
		state.meshExtents = m_meshExtents;
		state.lineNumber = m_lineNumber;
		state.lineOffset = m_lineOffset;
		state.baselineOffset = m_baselineOffset;
		state.vertexColor = m_htmlColor;
		state.underlineColor = m_underlineColor;
		state.strikethroughColor = m_strikethroughColor;
		state.highlightColor = m_highlightColor;
		state.isNonBreakingSpace = m_isNonBreakingSpace;
		state.tagNoParsing = tag_NoParsing;
		state.basicStyleStack = m_fontStyleStack;
		state.colorStack = m_colorStack;
		state.underlineColorStack = m_underlineColorStack;
		state.strikethroughColorStack = m_strikethroughColorStack;
		state.highlightColorStack = m_highlightColorStack;
		state.colorGradientStack = m_colorGradientStack;
		state.sizeStack = m_sizeStack;
		state.indentStack = m_indentStack;
		state.fontWeightStack = m_FontWeightStack;
		state.styleStack = m_styleStack;
		state.baselineStack = m_baselineOffsetStack;
		state.actionStack = m_actionStack;
		state.materialReferenceStack = m_materialReferenceStack;
		state.lineJustificationStack = m_lineJustificationStack;
		state.spriteAnimationID = m_spriteAnimationID;
		if (m_lineNumber < m_textInfo.lineInfo.Length)
		{
			state.lineInfo = m_textInfo.lineInfo[m_lineNumber];
		}
	}

	protected int RestoreWordWrappingState(ref WordWrapState state)
	{
		int previous_WordBreak = state.previous_WordBreak;
		m_currentFontAsset = state.currentFontAsset;
		m_currentMaterial = state.currentMaterial;
		m_currentMaterialIndex = state.currentMaterialIndex;
		m_characterCount = state.total_CharacterCount + 1;
		m_lineVisibleCharacterCount = state.visible_CharacterCount;
		m_textInfo.linkCount = state.visible_LinkCount;
		m_firstCharacterOfLine = state.firstCharacterIndex;
		m_firstVisibleCharacterOfLine = state.firstVisibleCharacterIndex;
		m_lastVisibleCharacterOfLine = state.lastVisibleCharIndex;
		m_FontStyleInternal = state.fontStyle;
		m_fontScale = state.fontScale;
		m_fontScaleMultiplier = state.fontScaleMultiplier;
		m_currentFontSize = state.currentFontSize;
		m_xAdvance = state.xAdvance;
		m_maxCapHeight = state.maxCapHeight;
		m_maxAscender = state.maxAscender;
		m_maxDescender = state.maxDescender;
		m_maxLineAscender = state.maxLineAscender;
		m_maxLineDescender = state.maxLineDescender;
		m_startOfLineAscender = state.previousLineAscender;
		m_preferredWidth = state.preferredWidth;
		m_preferredHeight = state.preferredHeight;
		m_meshExtents = state.meshExtents;
		m_lineNumber = state.lineNumber;
		m_lineOffset = state.lineOffset;
		m_baselineOffset = state.baselineOffset;
		m_htmlColor = state.vertexColor;
		m_underlineColor = state.underlineColor;
		m_strikethroughColor = state.strikethroughColor;
		m_highlightColor = state.highlightColor;
		m_isNonBreakingSpace = state.isNonBreakingSpace;
		tag_NoParsing = state.tagNoParsing;
		m_fontStyleStack = state.basicStyleStack;
		m_colorStack = state.colorStack;
		m_underlineColorStack = state.underlineColorStack;
		m_strikethroughColorStack = state.strikethroughColorStack;
		m_highlightColorStack = state.highlightColorStack;
		m_colorGradientStack = state.colorGradientStack;
		m_sizeStack = state.sizeStack;
		m_indentStack = state.indentStack;
		m_FontWeightStack = state.fontWeightStack;
		m_styleStack = state.styleStack;
		m_baselineOffsetStack = state.baselineStack;
		m_actionStack = state.actionStack;
		m_materialReferenceStack = state.materialReferenceStack;
		m_lineJustificationStack = state.lineJustificationStack;
		m_spriteAnimationID = state.spriteAnimationID;
		if (m_lineNumber < m_textInfo.lineInfo.Length)
		{
			m_textInfo.lineInfo[m_lineNumber] = state.lineInfo;
		}
		return previous_WordBreak;
	}

	protected virtual void SaveGlyphVertexInfo(float padding, float style_padding, Color32 vertexColor)
	{
		m_textInfo.characterInfo[m_characterCount].vertex_BL.position = m_textInfo.characterInfo[m_characterCount].bottomLeft;
		m_textInfo.characterInfo[m_characterCount].vertex_TL.position = m_textInfo.characterInfo[m_characterCount].topLeft;
		m_textInfo.characterInfo[m_characterCount].vertex_TR.position = m_textInfo.characterInfo[m_characterCount].topRight;
		m_textInfo.characterInfo[m_characterCount].vertex_BR.position = m_textInfo.characterInfo[m_characterCount].bottomRight;
		vertexColor.a = ((m_fontColor32.a < vertexColor.a) ? m_fontColor32.a : vertexColor.a);
		if (!m_enableVertexGradient)
		{
			m_textInfo.characterInfo[m_characterCount].vertex_BL.color = vertexColor;
			m_textInfo.characterInfo[m_characterCount].vertex_TL.color = vertexColor;
			m_textInfo.characterInfo[m_characterCount].vertex_TR.color = vertexColor;
			m_textInfo.characterInfo[m_characterCount].vertex_BR.color = vertexColor;
		}
		else if (!m_overrideHtmlColors && m_colorStack.m_Index > 1)
		{
			m_textInfo.characterInfo[m_characterCount].vertex_BL.color = vertexColor;
			m_textInfo.characterInfo[m_characterCount].vertex_TL.color = vertexColor;
			m_textInfo.characterInfo[m_characterCount].vertex_TR.color = vertexColor;
			m_textInfo.characterInfo[m_characterCount].vertex_BR.color = vertexColor;
		}
		else if (m_fontColorGradientPreset != null)
		{
			m_textInfo.characterInfo[m_characterCount].vertex_BL.color = m_fontColorGradientPreset.bottomLeft * vertexColor;
			m_textInfo.characterInfo[m_characterCount].vertex_TL.color = m_fontColorGradientPreset.topLeft * vertexColor;
			m_textInfo.characterInfo[m_characterCount].vertex_TR.color = m_fontColorGradientPreset.topRight * vertexColor;
			m_textInfo.characterInfo[m_characterCount].vertex_BR.color = m_fontColorGradientPreset.bottomRight * vertexColor;
		}
		else
		{
			m_textInfo.characterInfo[m_characterCount].vertex_BL.color = m_fontColorGradient.bottomLeft * vertexColor;
			m_textInfo.characterInfo[m_characterCount].vertex_TL.color = m_fontColorGradient.topLeft * vertexColor;
			m_textInfo.characterInfo[m_characterCount].vertex_TR.color = m_fontColorGradient.topRight * vertexColor;
			m_textInfo.characterInfo[m_characterCount].vertex_BR.color = m_fontColorGradient.bottomRight * vertexColor;
		}
		if (m_colorGradientPreset != null)
		{
			ref Color32 reference = ref m_textInfo.characterInfo[m_characterCount].vertex_BL.color;
			reference *= m_colorGradientPreset.bottomLeft;
			ref Color32 reference2 = ref m_textInfo.characterInfo[m_characterCount].vertex_TL.color;
			reference2 *= m_colorGradientPreset.topLeft;
			ref Color32 reference3 = ref m_textInfo.characterInfo[m_characterCount].vertex_TR.color;
			reference3 *= m_colorGradientPreset.topRight;
			ref Color32 reference4 = ref m_textInfo.characterInfo[m_characterCount].vertex_BR.color;
			reference4 *= m_colorGradientPreset.bottomRight;
		}
		if (!m_isSDFShader)
		{
			style_padding = 0f;
		}
		Vector2 uv = default(Vector2);
		uv.x = ((float)m_cached_TextElement.glyph.glyphRect.x - padding - style_padding) / (float)m_currentFontAsset.atlasWidth;
		uv.y = ((float)m_cached_TextElement.glyph.glyphRect.y - padding - style_padding) / (float)m_currentFontAsset.atlasHeight;
		Vector2 uv2 = default(Vector2);
		uv2.x = uv.x;
		uv2.y = ((float)m_cached_TextElement.glyph.glyphRect.y + padding + style_padding + (float)m_cached_TextElement.glyph.glyphRect.height) / (float)m_currentFontAsset.atlasHeight;
		Vector2 uv3 = default(Vector2);
		uv3.x = ((float)m_cached_TextElement.glyph.glyphRect.x + padding + style_padding + (float)m_cached_TextElement.glyph.glyphRect.width) / (float)m_currentFontAsset.atlasWidth;
		uv3.y = uv2.y;
		Vector2 uv4 = default(Vector2);
		uv4.x = uv3.x;
		uv4.y = uv.y;
		m_textInfo.characterInfo[m_characterCount].vertex_BL.uv = uv;
		m_textInfo.characterInfo[m_characterCount].vertex_TL.uv = uv2;
		m_textInfo.characterInfo[m_characterCount].vertex_TR.uv = uv3;
		m_textInfo.characterInfo[m_characterCount].vertex_BR.uv = uv4;
	}

	protected virtual void FillCharacterVertexBuffers(int i)
	{
		int materialReferenceIndex = m_textInfo.characterInfo[i].materialReferenceIndex;
		int vertexCount = m_textInfo.meshInfo[materialReferenceIndex].vertexCount;
		if (vertexCount >= m_textInfo.meshInfo[materialReferenceIndex].vertices.Length)
		{
			m_textInfo.meshInfo[materialReferenceIndex].ResizeMeshInfo(Mathf.NextPowerOfTwo((vertexCount + 4) / 4));
		}
		TMP_CharacterInfo[] characterInfo = m_textInfo.characterInfo;
		m_textInfo.characterInfo[i].vertexIndex = vertexCount;
		m_textInfo.meshInfo[materialReferenceIndex].vertices[vertexCount] = characterInfo[i].vertex_BL.position;
		m_textInfo.meshInfo[materialReferenceIndex].vertices[1 + vertexCount] = characterInfo[i].vertex_TL.position;
		m_textInfo.meshInfo[materialReferenceIndex].vertices[2 + vertexCount] = characterInfo[i].vertex_TR.position;
		m_textInfo.meshInfo[materialReferenceIndex].vertices[3 + vertexCount] = characterInfo[i].vertex_BR.position;
		m_textInfo.meshInfo[materialReferenceIndex].uvs0[vertexCount] = characterInfo[i].vertex_BL.uv;
		m_textInfo.meshInfo[materialReferenceIndex].uvs0[1 + vertexCount] = characterInfo[i].vertex_TL.uv;
		m_textInfo.meshInfo[materialReferenceIndex].uvs0[2 + vertexCount] = characterInfo[i].vertex_TR.uv;
		m_textInfo.meshInfo[materialReferenceIndex].uvs0[3 + vertexCount] = characterInfo[i].vertex_BR.uv;
		m_textInfo.meshInfo[materialReferenceIndex].uvs2[vertexCount] = characterInfo[i].vertex_BL.uv2;
		m_textInfo.meshInfo[materialReferenceIndex].uvs2[1 + vertexCount] = characterInfo[i].vertex_TL.uv2;
		m_textInfo.meshInfo[materialReferenceIndex].uvs2[2 + vertexCount] = characterInfo[i].vertex_TR.uv2;
		m_textInfo.meshInfo[materialReferenceIndex].uvs2[3 + vertexCount] = characterInfo[i].vertex_BR.uv2;
		m_textInfo.meshInfo[materialReferenceIndex].colors32[vertexCount] = characterInfo[i].vertex_BL.color;
		m_textInfo.meshInfo[materialReferenceIndex].colors32[1 + vertexCount] = characterInfo[i].vertex_TL.color;
		m_textInfo.meshInfo[materialReferenceIndex].colors32[2 + vertexCount] = characterInfo[i].vertex_TR.color;
		m_textInfo.meshInfo[materialReferenceIndex].colors32[3 + vertexCount] = characterInfo[i].vertex_BR.color;
		m_textInfo.meshInfo[materialReferenceIndex].vertexCount = vertexCount + 4;
	}

	protected virtual void FillCharacterVertexBuffers(int i, bool isVolumetric)
	{
		int materialReferenceIndex = m_textInfo.characterInfo[i].materialReferenceIndex;
		int vertexCount = m_textInfo.meshInfo[materialReferenceIndex].vertexCount;
		if (vertexCount >= m_textInfo.meshInfo[materialReferenceIndex].vertices.Length)
		{
			m_textInfo.meshInfo[materialReferenceIndex].ResizeMeshInfo(Mathf.NextPowerOfTwo((vertexCount + (isVolumetric ? 8 : 4)) / 4));
		}
		TMP_CharacterInfo[] characterInfo = m_textInfo.characterInfo;
		m_textInfo.characterInfo[i].vertexIndex = vertexCount;
		m_textInfo.meshInfo[materialReferenceIndex].vertices[vertexCount] = characterInfo[i].vertex_BL.position;
		m_textInfo.meshInfo[materialReferenceIndex].vertices[1 + vertexCount] = characterInfo[i].vertex_TL.position;
		m_textInfo.meshInfo[materialReferenceIndex].vertices[2 + vertexCount] = characterInfo[i].vertex_TR.position;
		m_textInfo.meshInfo[materialReferenceIndex].vertices[3 + vertexCount] = characterInfo[i].vertex_BR.position;
		m_textInfo.meshInfo[materialReferenceIndex].uvs0[vertexCount] = characterInfo[i].vertex_BL.uv;
		m_textInfo.meshInfo[materialReferenceIndex].uvs0[1 + vertexCount] = characterInfo[i].vertex_TL.uv;
		m_textInfo.meshInfo[materialReferenceIndex].uvs0[2 + vertexCount] = characterInfo[i].vertex_TR.uv;
		m_textInfo.meshInfo[materialReferenceIndex].uvs0[3 + vertexCount] = characterInfo[i].vertex_BR.uv;
		if (isVolumetric)
		{
			m_textInfo.meshInfo[materialReferenceIndex].uvs0[4 + vertexCount] = characterInfo[i].vertex_BL.uv;
			m_textInfo.meshInfo[materialReferenceIndex].uvs0[5 + vertexCount] = characterInfo[i].vertex_TL.uv;
			m_textInfo.meshInfo[materialReferenceIndex].uvs0[6 + vertexCount] = characterInfo[i].vertex_TR.uv;
			m_textInfo.meshInfo[materialReferenceIndex].uvs0[7 + vertexCount] = characterInfo[i].vertex_BR.uv;
		}
		m_textInfo.meshInfo[materialReferenceIndex].uvs2[vertexCount] = characterInfo[i].vertex_BL.uv2;
		m_textInfo.meshInfo[materialReferenceIndex].uvs2[1 + vertexCount] = characterInfo[i].vertex_TL.uv2;
		m_textInfo.meshInfo[materialReferenceIndex].uvs2[2 + vertexCount] = characterInfo[i].vertex_TR.uv2;
		m_textInfo.meshInfo[materialReferenceIndex].uvs2[3 + vertexCount] = characterInfo[i].vertex_BR.uv2;
		if (isVolumetric)
		{
			m_textInfo.meshInfo[materialReferenceIndex].uvs2[4 + vertexCount] = characterInfo[i].vertex_BL.uv2;
			m_textInfo.meshInfo[materialReferenceIndex].uvs2[5 + vertexCount] = characterInfo[i].vertex_TL.uv2;
			m_textInfo.meshInfo[materialReferenceIndex].uvs2[6 + vertexCount] = characterInfo[i].vertex_TR.uv2;
			m_textInfo.meshInfo[materialReferenceIndex].uvs2[7 + vertexCount] = characterInfo[i].vertex_BR.uv2;
		}
		m_textInfo.meshInfo[materialReferenceIndex].colors32[vertexCount] = characterInfo[i].vertex_BL.color;
		m_textInfo.meshInfo[materialReferenceIndex].colors32[1 + vertexCount] = characterInfo[i].vertex_TL.color;
		m_textInfo.meshInfo[materialReferenceIndex].colors32[2 + vertexCount] = characterInfo[i].vertex_TR.color;
		m_textInfo.meshInfo[materialReferenceIndex].colors32[3 + vertexCount] = characterInfo[i].vertex_BR.color;
		if (isVolumetric)
		{
			Color32 color = new Color32(byte.MaxValue, byte.MaxValue, 128, byte.MaxValue);
			m_textInfo.meshInfo[materialReferenceIndex].colors32[4 + vertexCount] = color;
			m_textInfo.meshInfo[materialReferenceIndex].colors32[5 + vertexCount] = color;
			m_textInfo.meshInfo[materialReferenceIndex].colors32[6 + vertexCount] = color;
			m_textInfo.meshInfo[materialReferenceIndex].colors32[7 + vertexCount] = color;
		}
		m_textInfo.meshInfo[materialReferenceIndex].vertexCount = vertexCount + ((!isVolumetric) ? 4 : 8);
	}

	protected virtual void DrawUnderlineMesh(Vector3 start, Vector3 end, ref int index, float startScale, float endScale, float maxScale, float sdfScale, Color32 underlineColor)
	{
		if (m_cached_Underline_Character == null)
		{
			if (!TMP_Settings.warningsDisabled)
			{
				UnityEngine.Debug.LogWarning("Unable to add underline since the Font Asset doesn't contain the underline character.", this);
			}
			return;
		}
		int num = index + 12;
		if (num > m_textInfo.meshInfo[0].vertices.Length)
		{
			m_textInfo.meshInfo[0].ResizeMeshInfo(num / 4);
		}
		start.y = Mathf.Min(start.y, end.y);
		end.y = Mathf.Min(start.y, end.y);
		float num2 = m_cached_Underline_Character.glyph.metrics.width / 2f * maxScale;
		if (end.x - start.x < m_cached_Underline_Character.glyph.metrics.width * maxScale)
		{
			num2 = (end.x - start.x) / 2f;
		}
		float num3 = m_padding * startScale / maxScale;
		float num4 = m_padding * endScale / maxScale;
		float underlineThickness = m_fontAsset.faceInfo.underlineThickness;
		Vector3[] vertices = m_textInfo.meshInfo[0].vertices;
		vertices[index] = start + new Vector3(0f, 0f - (underlineThickness + m_padding) * maxScale, 0f);
		vertices[index + 1] = start + new Vector3(0f, m_padding * maxScale, 0f);
		vertices[index + 2] = vertices[index + 1] + new Vector3(num2, 0f, 0f);
		vertices[index + 3] = vertices[index] + new Vector3(num2, 0f, 0f);
		vertices[index + 4] = vertices[index + 3];
		vertices[index + 5] = vertices[index + 2];
		vertices[index + 6] = end + new Vector3(0f - num2, m_padding * maxScale, 0f);
		vertices[index + 7] = end + new Vector3(0f - num2, (0f - (underlineThickness + m_padding)) * maxScale, 0f);
		vertices[index + 8] = vertices[index + 7];
		vertices[index + 9] = vertices[index + 6];
		vertices[index + 10] = end + new Vector3(0f, m_padding * maxScale, 0f);
		vertices[index + 11] = end + new Vector3(0f, (0f - (underlineThickness + m_padding)) * maxScale, 0f);
		Vector2[] uvs = m_textInfo.meshInfo[0].uvs0;
		Vector2 vector = new Vector2(((float)m_cached_Underline_Character.glyph.glyphRect.x - num3) / (float)m_fontAsset.atlasWidth, ((float)m_cached_Underline_Character.glyph.glyphRect.y - m_padding) / (float)m_fontAsset.atlasHeight);
		Vector2 vector2 = new Vector2(vector.x, ((float)(m_cached_Underline_Character.glyph.glyphRect.y + m_cached_Underline_Character.glyph.glyphRect.height) + m_padding) / (float)m_fontAsset.atlasHeight);
		Vector2 vector3 = new Vector2(((float)m_cached_Underline_Character.glyph.glyphRect.x - num3 + (float)m_cached_Underline_Character.glyph.glyphRect.width / 2f) / (float)m_fontAsset.atlasWidth, vector2.y);
		Vector2 vector4 = new Vector2(vector3.x, vector.y);
		Vector2 vector5 = new Vector2(((float)m_cached_Underline_Character.glyph.glyphRect.x + num4 + (float)m_cached_Underline_Character.glyph.glyphRect.width / 2f) / (float)m_fontAsset.atlasWidth, vector2.y);
		Vector2 vector6 = new Vector2(vector5.x, vector.y);
		Vector2 vector7 = new Vector2(((float)m_cached_Underline_Character.glyph.glyphRect.x + num4 + (float)m_cached_Underline_Character.glyph.glyphRect.width) / (float)m_fontAsset.atlasWidth, vector2.y);
		Vector2 vector8 = new Vector2(vector7.x, vector.y);
		uvs[index] = vector;
		uvs[1 + index] = vector2;
		uvs[2 + index] = vector3;
		uvs[3 + index] = vector4;
		uvs[4 + index] = new Vector2(vector3.x - vector3.x * 0.001f, vector.y);
		uvs[5 + index] = new Vector2(vector3.x - vector3.x * 0.001f, vector2.y);
		uvs[6 + index] = new Vector2(vector3.x + vector3.x * 0.001f, vector2.y);
		uvs[7 + index] = new Vector2(vector3.x + vector3.x * 0.001f, vector.y);
		uvs[8 + index] = vector6;
		uvs[9 + index] = vector5;
		uvs[10 + index] = vector7;
		uvs[11 + index] = vector8;
		float num5 = 0f;
		float x = (vertices[index + 2].x - start.x) / (end.x - start.x);
		float scale = Mathf.Abs(sdfScale);
		Vector2[] uvs2 = m_textInfo.meshInfo[0].uvs2;
		uvs2[index] = PackUV(0f, 0f, scale);
		uvs2[1 + index] = PackUV(0f, 1f, scale);
		uvs2[2 + index] = PackUV(x, 1f, scale);
		uvs2[3 + index] = PackUV(x, 0f, scale);
		num5 = (vertices[index + 4].x - start.x) / (end.x - start.x);
		x = (vertices[index + 6].x - start.x) / (end.x - start.x);
		uvs2[4 + index] = PackUV(num5, 0f, scale);
		uvs2[5 + index] = PackUV(num5, 1f, scale);
		uvs2[6 + index] = PackUV(x, 1f, scale);
		uvs2[7 + index] = PackUV(x, 0f, scale);
		num5 = (vertices[index + 8].x - start.x) / (end.x - start.x);
		x = (vertices[index + 6].x - start.x) / (end.x - start.x);
		uvs2[8 + index] = PackUV(num5, 0f, scale);
		uvs2[9 + index] = PackUV(num5, 1f, scale);
		uvs2[10 + index] = PackUV(1f, 1f, scale);
		uvs2[11 + index] = PackUV(1f, 0f, scale);
		underlineColor.a = ((m_fontColor32.a < underlineColor.a) ? m_fontColor32.a : underlineColor.a);
		Color32[] colors = m_textInfo.meshInfo[0].colors32;
		colors[index] = underlineColor;
		colors[1 + index] = underlineColor;
		colors[2 + index] = underlineColor;
		colors[3 + index] = underlineColor;
		colors[4 + index] = underlineColor;
		colors[5 + index] = underlineColor;
		colors[6 + index] = underlineColor;
		colors[7 + index] = underlineColor;
		colors[8 + index] = underlineColor;
		colors[9 + index] = underlineColor;
		colors[10 + index] = underlineColor;
		colors[11 + index] = underlineColor;
		index += 12;
	}

	protected virtual void DrawTextHighlight(Vector3 start, Vector3 end, ref int index, Color32 highlightColor)
	{
		if (m_cached_Underline_Character == null)
		{
			if (!TMP_Settings.warningsDisabled)
			{
				UnityEngine.Debug.LogWarning("Unable to add underline since the Font Asset doesn't contain the underline character.", this);
			}
			return;
		}
		int num = index + 4;
		if (num > m_textInfo.meshInfo[0].vertices.Length)
		{
			m_textInfo.meshInfo[0].ResizeMeshInfo(num / 4);
		}
		Vector3[] vertices = m_textInfo.meshInfo[0].vertices;
		vertices[index] = start;
		vertices[index + 1] = new Vector3(start.x, end.y, 0f);
		vertices[index + 2] = end;
		vertices[index + 3] = new Vector3(end.x, start.y, 0f);
		Vector2[] uvs = m_textInfo.meshInfo[0].uvs0;
		Vector2 vector = new Vector2(((float)m_cached_Underline_Character.glyph.glyphRect.x + (float)(m_cached_Underline_Character.glyph.glyphRect.width / 2)) / (float)m_fontAsset.atlasWidth, ((float)m_cached_Underline_Character.glyph.glyphRect.y + (float)m_cached_Underline_Character.glyph.glyphRect.height / 2f) / (float)m_fontAsset.atlasHeight);
		uvs[index] = vector;
		uvs[1 + index] = vector;
		uvs[2 + index] = vector;
		uvs[3 + index] = vector;
		Vector2[] uvs2 = m_textInfo.meshInfo[0].uvs2;
		Vector2 vector2 = new Vector2(0f, 1f);
		uvs2[index] = vector2;
		uvs2[1 + index] = vector2;
		uvs2[2 + index] = vector2;
		uvs2[3 + index] = vector2;
		highlightColor.a = ((m_fontColor32.a < highlightColor.a) ? m_fontColor32.a : highlightColor.a);
		Color32[] colors = m_textInfo.meshInfo[0].colors32;
		colors[index] = highlightColor;
		colors[1 + index] = highlightColor;
		colors[2 + index] = highlightColor;
		colors[3 + index] = highlightColor;
		index += 4;
	}

	protected void LoadDefaultSettings()
	{
		if (m_text != null && !m_isWaitingOnResourceLoad)
		{
			return;
		}
		if (TMP_Settings.autoSizeTextContainer)
		{
			autoSizeTextContainer = true;
		}
		else
		{
			m_rectTransform = rectTransform;
			if (GetType() == typeof(TextMeshPro))
			{
				m_rectTransform.sizeDelta = TMP_Settings.defaultTextMeshProTextContainerSize;
			}
			else
			{
				m_rectTransform.sizeDelta = TMP_Settings.defaultTextMeshProUITextContainerSize;
			}
		}
		m_enableWordWrapping = TMP_Settings.enableWordWrapping;
		m_enableKerning = TMP_Settings.enableKerning;
		m_enableExtraPadding = TMP_Settings.enableExtraPadding;
		m_fontSize = (m_fontSizeBase = TMP_Settings.defaultFontSize);
		m_fontSizeMin = m_fontSize * TMP_Settings.defaultTextAutoSizingMinRatio;
		m_fontSizeMax = m_fontSize * TMP_Settings.defaultTextAutoSizingMaxRatio;
		m_isWaitingOnResourceLoad = false;
		raycastTarget = TMP_Settings.enableRaycastTarget;
	}

	protected void GetSpecialCharacters(TMP_FontAsset fontAsset)
	{
		if (!fontAsset.characterLookupTable.TryGetValue(95u, out m_cached_Underline_Character))
		{
			m_cached_Underline_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(95u, fontAsset, includeFallbacks: false, m_FontStyleInternal, m_FontWeightInternal, out var _, out var _);
			if (m_cached_Underline_Character == null && !TMP_Settings.warningsDisabled)
			{
				UnityEngine.Debug.LogWarning("The character used for Underline and Strikethrough is not available in font asset [" + fontAsset.name + "].", this);
			}
		}
		if (!fontAsset.characterLookupTable.TryGetValue(8230u, out m_cached_Ellipsis_Character))
		{
			m_cached_Ellipsis_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(8230u, fontAsset, includeFallbacks: false, m_FontStyleInternal, m_FontWeightInternal, out var _, out var _);
			if (m_cached_Ellipsis_Character == null && !TMP_Settings.warningsDisabled)
			{
				UnityEngine.Debug.LogWarning("The character used for Ellipsis is not available in font asset [" + fontAsset.name + "].", this);
			}
		}
	}

	protected void ReplaceTagWithCharacter(int[] chars, int insertionIndex, int tagLength, char c)
	{
		chars[insertionIndex] = c;
		for (int i = insertionIndex + tagLength; i < chars.Length; i++)
		{
			chars[i - 3] = chars[i];
		}
	}

	protected TMP_FontAsset GetFontAssetForWeight(int fontWeight)
	{
		bool num = (m_FontStyleInternal & FontStyles.Italic) == FontStyles.Italic || (m_fontStyle & FontStyles.Italic) == FontStyles.Italic;
		TMP_FontAsset tMP_FontAsset = null;
		int num2 = fontWeight / 100;
		if (num)
		{
			return m_currentFontAsset.fontWeightTable[num2].italicTypeface;
		}
		return m_currentFontAsset.fontWeightTable[num2].regularTypeface;
	}

	protected virtual void SetActiveSubMeshes(bool state)
	{
	}

	protected virtual void ClearSubMeshObjects()
	{
	}

	public virtual void ClearMesh()
	{
	}

	public virtual void ClearMesh(bool uploadGeometry)
	{
	}

	public virtual string GetParsedText()
	{
		if (m_textInfo == null)
		{
			return string.Empty;
		}
		int characterCount = m_textInfo.characterCount;
		char[] array = new char[characterCount];
		for (int i = 0; i < characterCount && i < m_textInfo.characterInfo.Length; i++)
		{
			array[i] = m_textInfo.characterInfo[i].character;
		}
		return new string(array);
	}

	protected Vector2 PackUV(float x, float y, float scale)
	{
		Vector2 result = default(Vector2);
		result.x = (int)(x * 511f);
		result.y = (int)(y * 511f);
		result.x = result.x * 4096f + result.y;
		result.y = scale;
		return result;
	}

	protected float PackUV(float x, float y)
	{
		double num = (int)(x * 511f);
		double num2 = (int)(y * 511f);
		return (float)(num * 4096.0 + num2);
	}

	internal virtual void InternalUpdate()
	{
	}

	protected int HexToInt(char hex)
	{
		return hex switch
		{
			'0' => 0, 
			'1' => 1, 
			'2' => 2, 
			'3' => 3, 
			'4' => 4, 
			'5' => 5, 
			'6' => 6, 
			'7' => 7, 
			'8' => 8, 
			'9' => 9, 
			'A' => 10, 
			'B' => 11, 
			'C' => 12, 
			'D' => 13, 
			'E' => 14, 
			'F' => 15, 
			'a' => 10, 
			'b' => 11, 
			'c' => 12, 
			'd' => 13, 
			'e' => 14, 
			'f' => 15, 
			_ => 15, 
		};
	}

	protected int GetUTF16(string text, int i)
	{
		return 0 + (HexToInt(text[i]) << 12) + (HexToInt(text[i + 1]) << 8) + (HexToInt(text[i + 2]) << 4) + HexToInt(text[i + 3]);
	}

	protected int GetUTF16(StringBuilder text, int i)
	{
		return 0 + (HexToInt(text[i]) << 12) + (HexToInt(text[i + 1]) << 8) + (HexToInt(text[i + 2]) << 4) + HexToInt(text[i + 3]);
	}

	protected int GetUTF32(string text, int i)
	{
		return 0 + (HexToInt(text[i]) << 30) + (HexToInt(text[i + 1]) << 24) + (HexToInt(text[i + 2]) << 20) + (HexToInt(text[i + 3]) << 16) + (HexToInt(text[i + 4]) << 12) + (HexToInt(text[i + 5]) << 8) + (HexToInt(text[i + 6]) << 4) + HexToInt(text[i + 7]);
	}

	protected int GetUTF32(StringBuilder text, int i)
	{
		return 0 + (HexToInt(text[i]) << 30) + (HexToInt(text[i + 1]) << 24) + (HexToInt(text[i + 2]) << 20) + (HexToInt(text[i + 3]) << 16) + (HexToInt(text[i + 4]) << 12) + (HexToInt(text[i + 5]) << 8) + (HexToInt(text[i + 6]) << 4) + HexToInt(text[i + 7]);
	}

	protected Color32 HexCharsToColor(char[] hexChars, int tagCount)
	{
		switch (tagCount)
		{
		case 4:
		{
			byte r8 = (byte)(HexToInt(hexChars[1]) * 16 + HexToInt(hexChars[1]));
			byte g8 = (byte)(HexToInt(hexChars[2]) * 16 + HexToInt(hexChars[2]));
			byte b8 = (byte)(HexToInt(hexChars[3]) * 16 + HexToInt(hexChars[3]));
			return new Color32(r8, g8, b8, byte.MaxValue);
		}
		case 5:
		{
			byte r7 = (byte)(HexToInt(hexChars[1]) * 16 + HexToInt(hexChars[1]));
			byte g7 = (byte)(HexToInt(hexChars[2]) * 16 + HexToInt(hexChars[2]));
			byte b7 = (byte)(HexToInt(hexChars[3]) * 16 + HexToInt(hexChars[3]));
			byte a4 = (byte)(HexToInt(hexChars[4]) * 16 + HexToInt(hexChars[4]));
			return new Color32(r7, g7, b7, a4);
		}
		case 7:
		{
			byte r6 = (byte)(HexToInt(hexChars[1]) * 16 + HexToInt(hexChars[2]));
			byte g6 = (byte)(HexToInt(hexChars[3]) * 16 + HexToInt(hexChars[4]));
			byte b6 = (byte)(HexToInt(hexChars[5]) * 16 + HexToInt(hexChars[6]));
			return new Color32(r6, g6, b6, byte.MaxValue);
		}
		case 9:
		{
			byte r5 = (byte)(HexToInt(hexChars[1]) * 16 + HexToInt(hexChars[2]));
			byte g5 = (byte)(HexToInt(hexChars[3]) * 16 + HexToInt(hexChars[4]));
			byte b5 = (byte)(HexToInt(hexChars[5]) * 16 + HexToInt(hexChars[6]));
			byte a3 = (byte)(HexToInt(hexChars[7]) * 16 + HexToInt(hexChars[8]));
			return new Color32(r5, g5, b5, a3);
		}
		case 10:
		{
			byte r4 = (byte)(HexToInt(hexChars[7]) * 16 + HexToInt(hexChars[7]));
			byte g4 = (byte)(HexToInt(hexChars[8]) * 16 + HexToInt(hexChars[8]));
			byte b4 = (byte)(HexToInt(hexChars[9]) * 16 + HexToInt(hexChars[9]));
			return new Color32(r4, g4, b4, byte.MaxValue);
		}
		case 11:
		{
			byte r3 = (byte)(HexToInt(hexChars[7]) * 16 + HexToInt(hexChars[7]));
			byte g3 = (byte)(HexToInt(hexChars[8]) * 16 + HexToInt(hexChars[8]));
			byte b3 = (byte)(HexToInt(hexChars[9]) * 16 + HexToInt(hexChars[9]));
			byte a2 = (byte)(HexToInt(hexChars[10]) * 16 + HexToInt(hexChars[10]));
			return new Color32(r3, g3, b3, a2);
		}
		case 13:
		{
			byte r2 = (byte)(HexToInt(hexChars[7]) * 16 + HexToInt(hexChars[8]));
			byte g2 = (byte)(HexToInt(hexChars[9]) * 16 + HexToInt(hexChars[10]));
			byte b2 = (byte)(HexToInt(hexChars[11]) * 16 + HexToInt(hexChars[12]));
			return new Color32(r2, g2, b2, byte.MaxValue);
		}
		case 15:
		{
			byte r = (byte)(HexToInt(hexChars[7]) * 16 + HexToInt(hexChars[8]));
			byte g = (byte)(HexToInt(hexChars[9]) * 16 + HexToInt(hexChars[10]));
			byte b = (byte)(HexToInt(hexChars[11]) * 16 + HexToInt(hexChars[12]));
			byte a = (byte)(HexToInt(hexChars[13]) * 16 + HexToInt(hexChars[14]));
			return new Color32(r, g, b, a);
		}
		default:
			return new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}
	}

	protected Color32 HexCharsToColor(char[] hexChars, int startIndex, int length)
	{
		switch (length)
		{
		case 7:
		{
			byte r2 = (byte)(HexToInt(hexChars[startIndex + 1]) * 16 + HexToInt(hexChars[startIndex + 2]));
			byte g2 = (byte)(HexToInt(hexChars[startIndex + 3]) * 16 + HexToInt(hexChars[startIndex + 4]));
			byte b2 = (byte)(HexToInt(hexChars[startIndex + 5]) * 16 + HexToInt(hexChars[startIndex + 6]));
			return new Color32(r2, g2, b2, byte.MaxValue);
		}
		case 9:
		{
			byte r = (byte)(HexToInt(hexChars[startIndex + 1]) * 16 + HexToInt(hexChars[startIndex + 2]));
			byte g = (byte)(HexToInt(hexChars[startIndex + 3]) * 16 + HexToInt(hexChars[startIndex + 4]));
			byte b = (byte)(HexToInt(hexChars[startIndex + 5]) * 16 + HexToInt(hexChars[startIndex + 6]));
			byte a = (byte)(HexToInt(hexChars[startIndex + 7]) * 16 + HexToInt(hexChars[startIndex + 8]));
			return new Color32(r, g, b, a);
		}
		default:
			return s_colorWhite;
		}
	}

	private int GetAttributeParameters(char[] chars, int startIndex, int length, ref float[] parameters)
	{
		int lastIndex = startIndex;
		int num = 0;
		while (lastIndex < startIndex + length)
		{
			parameters[num] = ConvertToFloat(chars, startIndex, length, out lastIndex);
			length -= lastIndex - startIndex + 1;
			startIndex = lastIndex + 1;
			num++;
		}
		return num;
	}

	protected float ConvertToFloat(char[] chars, int startIndex, int length)
	{
		int lastIndex;
		return ConvertToFloat(chars, startIndex, length, out lastIndex);
	}

	protected float ConvertToFloat(char[] chars, int startIndex, int length, out int lastIndex)
	{
		if (startIndex == 0)
		{
			lastIndex = 0;
			return -9999f;
		}
		int num = startIndex + length;
		bool flag = true;
		float num2 = 0f;
		int num3 = 1;
		if (chars[startIndex] == '+')
		{
			num3 = 1;
			startIndex++;
		}
		else if (chars[startIndex] == '-')
		{
			num3 = -1;
			startIndex++;
		}
		float num4 = 0f;
		for (int i = startIndex; i < num; i++)
		{
			uint num5 = chars[i];
			if (num5 < 48 || num5 > 57)
			{
				switch (num5)
				{
				case 46u:
					break;
				case 44u:
					if (i + 1 < num && chars[i + 1] == ' ')
					{
						lastIndex = i + 1;
					}
					else
					{
						lastIndex = i;
					}
					return num4;
				default:
					continue;
				}
			}
			if (num5 == 46)
			{
				flag = false;
				num2 = 0.1f;
			}
			else if (flag)
			{
				num4 = num4 * 10f + (float)((num5 - 48) * num3);
			}
			else
			{
				num4 += (float)(num5 - 48) * num2 * (float)num3;
				num2 *= 0.1f;
			}
		}
		lastIndex = num;
		return num4;
	}

	protected bool ValidateHtmlTag(UnicodeChar[] chars, int startIndex, out int endIndex)
	{
		int num = 0;
		byte b = 0;
		int num2 = 0;
		m_xmlAttribute[num2].nameHashCode = 0;
		m_xmlAttribute[num2].valueHashCode = 0;
		m_xmlAttribute[num2].valueStartIndex = 0;
		m_xmlAttribute[num2].valueLength = 0;
		TagValueType tagValueType = (m_xmlAttribute[num2].valueType = TagValueType.None);
		TagUnitType tagUnitType = (m_xmlAttribute[num2].unitType = TagUnitType.Pixels);
		m_xmlAttribute[1].nameHashCode = 0;
		m_xmlAttribute[2].nameHashCode = 0;
		m_xmlAttribute[3].nameHashCode = 0;
		m_xmlAttribute[4].nameHashCode = 0;
		endIndex = startIndex;
		bool flag = false;
		bool flag2 = false;
		for (int i = startIndex; i < chars.Length && chars[i].unicode != 0; i++)
		{
			if (num >= m_htmlTag.Length)
			{
				break;
			}
			if (chars[i].unicode == 60)
			{
				break;
			}
			int unicode = chars[i].unicode;
			if (unicode == 62)
			{
				flag2 = true;
				endIndex = i;
				m_htmlTag[num] = '\0';
				break;
			}
			m_htmlTag[num] = (char)unicode;
			num++;
			if (b == 1)
			{
				switch (tagValueType)
				{
				case TagValueType.None:
					switch (unicode)
					{
					case 43:
					case 45:
					case 46:
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
					case 57:
						tagUnitType = TagUnitType.Pixels;
						tagValueType = (m_xmlAttribute[num2].valueType = TagValueType.NumericalValue);
						m_xmlAttribute[num2].valueStartIndex = num - 1;
						m_xmlAttribute[num2].valueLength++;
						break;
					default:
						switch (unicode)
						{
						case 35:
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (m_xmlAttribute[num2].valueType = TagValueType.ColorValue);
							m_xmlAttribute[num2].valueStartIndex = num - 1;
							m_xmlAttribute[num2].valueLength++;
							break;
						case 34:
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (m_xmlAttribute[num2].valueType = TagValueType.StringValue);
							m_xmlAttribute[num2].valueStartIndex = num;
							break;
						default:
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (m_xmlAttribute[num2].valueType = TagValueType.StringValue);
							m_xmlAttribute[num2].valueStartIndex = num - 1;
							m_xmlAttribute[num2].valueHashCode = ((m_xmlAttribute[num2].valueHashCode << 5) + m_xmlAttribute[num2].valueHashCode) ^ unicode;
							m_xmlAttribute[num2].valueLength++;
							break;
						}
						break;
					}
					break;
				case TagValueType.NumericalValue:
					if (unicode == 112 || unicode == 101 || unicode == 37 || unicode == 32)
					{
						b = 2;
						tagValueType = TagValueType.None;
						tagUnitType = unicode switch
						{
							101 => m_xmlAttribute[num2].unitType = TagUnitType.FontUnits, 
							37 => m_xmlAttribute[num2].unitType = TagUnitType.Percentage, 
							_ => m_xmlAttribute[num2].unitType = TagUnitType.Pixels, 
						};
						num2++;
						m_xmlAttribute[num2].nameHashCode = 0;
						m_xmlAttribute[num2].valueHashCode = 0;
						m_xmlAttribute[num2].valueType = TagValueType.None;
						m_xmlAttribute[num2].unitType = TagUnitType.Pixels;
						m_xmlAttribute[num2].valueStartIndex = 0;
						m_xmlAttribute[num2].valueLength = 0;
					}
					else if (b != 2)
					{
						m_xmlAttribute[num2].valueLength++;
					}
					break;
				case TagValueType.ColorValue:
					if (unicode != 32)
					{
						m_xmlAttribute[num2].valueLength++;
						break;
					}
					b = 2;
					tagValueType = TagValueType.None;
					tagUnitType = TagUnitType.Pixels;
					num2++;
					m_xmlAttribute[num2].nameHashCode = 0;
					m_xmlAttribute[num2].valueType = TagValueType.None;
					m_xmlAttribute[num2].unitType = TagUnitType.Pixels;
					m_xmlAttribute[num2].valueHashCode = 0;
					m_xmlAttribute[num2].valueStartIndex = 0;
					m_xmlAttribute[num2].valueLength = 0;
					break;
				case TagValueType.StringValue:
					if (unicode != 34)
					{
						m_xmlAttribute[num2].valueHashCode = ((m_xmlAttribute[num2].valueHashCode << 5) + m_xmlAttribute[num2].valueHashCode) ^ unicode;
						m_xmlAttribute[num2].valueLength++;
						break;
					}
					b = 2;
					tagValueType = TagValueType.None;
					tagUnitType = TagUnitType.Pixels;
					num2++;
					m_xmlAttribute[num2].nameHashCode = 0;
					m_xmlAttribute[num2].valueType = TagValueType.None;
					m_xmlAttribute[num2].unitType = TagUnitType.Pixels;
					m_xmlAttribute[num2].valueHashCode = 0;
					m_xmlAttribute[num2].valueStartIndex = 0;
					m_xmlAttribute[num2].valueLength = 0;
					break;
				}
			}
			if (unicode == 61)
			{
				b = 1;
			}
			if (b == 0 && unicode == 32)
			{
				if (flag)
				{
					return false;
				}
				flag = true;
				b = 2;
				tagValueType = TagValueType.None;
				tagUnitType = TagUnitType.Pixels;
				num2++;
				m_xmlAttribute[num2].nameHashCode = 0;
				m_xmlAttribute[num2].valueType = TagValueType.None;
				m_xmlAttribute[num2].unitType = TagUnitType.Pixels;
				m_xmlAttribute[num2].valueHashCode = 0;
				m_xmlAttribute[num2].valueStartIndex = 0;
				m_xmlAttribute[num2].valueLength = 0;
			}
			if (b == 0)
			{
				m_xmlAttribute[num2].nameHashCode = (m_xmlAttribute[num2].nameHashCode << 3) - m_xmlAttribute[num2].nameHashCode + unicode;
			}
			if (b == 2 && unicode == 32)
			{
				b = 0;
			}
		}
		if (!flag2)
		{
			return false;
		}
		if (tag_NoParsing && m_xmlAttribute[0].nameHashCode != 53822163 && m_xmlAttribute[0].nameHashCode != 49429939)
		{
			return false;
		}
		if (m_xmlAttribute[0].nameHashCode == 53822163 || m_xmlAttribute[0].nameHashCode == 49429939)
		{
			tag_NoParsing = false;
			return true;
		}
		if (m_htmlTag[0] == '#' && num == 4)
		{
			m_htmlColor = HexCharsToColor(m_htmlTag, num);
			m_colorStack.Add(m_htmlColor);
			return true;
		}
		if (m_htmlTag[0] == '#' && num == 5)
		{
			m_htmlColor = HexCharsToColor(m_htmlTag, num);
			m_colorStack.Add(m_htmlColor);
			return true;
		}
		if (m_htmlTag[0] == '#' && num == 7)
		{
			m_htmlColor = HexCharsToColor(m_htmlTag, num);
			m_colorStack.Add(m_htmlColor);
			return true;
		}
		if (m_htmlTag[0] == '#' && num == 9)
		{
			m_htmlColor = HexCharsToColor(m_htmlTag, num);
			m_colorStack.Add(m_htmlColor);
			return true;
		}
		float num3 = 0f;
		Material currentMaterial;
		switch (m_xmlAttribute[0].nameHashCode)
		{
		case 66:
		case 98:
			m_FontStyleInternal |= FontStyles.Bold;
			m_fontStyleStack.Add(FontStyles.Bold);
			m_FontWeightInternal = FontWeight.Bold;
			return true;
		case 395:
		case 427:
			if ((m_fontStyle & FontStyles.Bold) != FontStyles.Bold && m_fontStyleStack.Remove(FontStyles.Bold) == 0)
			{
				m_FontStyleInternal &= (FontStyles)(-2);
				m_FontWeightInternal = m_FontWeightStack.Peek();
			}
			return true;
		case 73:
		case 105:
			m_FontStyleInternal |= FontStyles.Italic;
			m_fontStyleStack.Add(FontStyles.Italic);
			return true;
		case 402:
		case 434:
			if ((m_fontStyle & FontStyles.Italic) != FontStyles.Italic && m_fontStyleStack.Remove(FontStyles.Italic) == 0)
			{
				m_FontStyleInternal &= (FontStyles)(-3);
			}
			return true;
		case 83:
		case 115:
			m_FontStyleInternal |= FontStyles.Strikethrough;
			m_fontStyleStack.Add(FontStyles.Strikethrough);
			if (m_xmlAttribute[1].nameHashCode == 281955 || m_xmlAttribute[1].nameHashCode == 192323)
			{
				m_strikethroughColor = HexCharsToColor(m_htmlTag, m_xmlAttribute[1].valueStartIndex, m_xmlAttribute[1].valueLength);
				m_strikethroughColor.a = ((m_htmlColor.a < m_strikethroughColor.a) ? m_htmlColor.a : m_strikethroughColor.a);
			}
			else
			{
				m_strikethroughColor = m_htmlColor;
			}
			m_strikethroughColorStack.Add(m_strikethroughColor);
			return true;
		case 412:
		case 444:
			if ((m_fontStyle & FontStyles.Strikethrough) != FontStyles.Strikethrough && m_fontStyleStack.Remove(FontStyles.Strikethrough) == 0)
			{
				m_FontStyleInternal &= (FontStyles)(-65);
			}
			return true;
		case 85:
		case 117:
			m_FontStyleInternal |= FontStyles.Underline;
			m_fontStyleStack.Add(FontStyles.Underline);
			if (m_xmlAttribute[1].nameHashCode == 281955 || m_xmlAttribute[1].nameHashCode == 192323)
			{
				m_underlineColor = HexCharsToColor(m_htmlTag, m_xmlAttribute[1].valueStartIndex, m_xmlAttribute[1].valueLength);
				m_underlineColor.a = ((m_htmlColor.a < m_underlineColor.a) ? m_htmlColor.a : m_underlineColor.a);
			}
			else
			{
				m_underlineColor = m_htmlColor;
			}
			m_underlineColorStack.Add(m_underlineColor);
			return true;
		case 414:
		case 446:
			if ((m_fontStyle & FontStyles.Underline) != FontStyles.Underline)
			{
				m_underlineColor = m_underlineColorStack.Remove();
				if (m_fontStyleStack.Remove(FontStyles.Underline) == 0)
				{
					m_FontStyleInternal &= (FontStyles)(-5);
				}
			}
			return true;
		case 30245:
		case 43045:
		{
			m_FontStyleInternal |= FontStyles.Highlight;
			m_fontStyleStack.Add(FontStyles.Highlight);
			m_highlightColor = HexCharsToColor(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			m_highlightColor.a = ((m_htmlColor.a < m_highlightColor.a) ? m_htmlColor.a : m_highlightColor.a);
			m_highlightColorStack.Add(m_highlightColor);
			for (int k = 0; k < m_xmlAttribute.Length && m_xmlAttribute[k].nameHashCode != 0; k++)
			{
				int nameHashCode = m_xmlAttribute[k].nameHashCode;
				if (nameHashCode != 281955 && nameHashCode == 15087385)
				{
					if (GetAttributeParameters(m_htmlTag, m_xmlAttribute[k].valueStartIndex, m_xmlAttribute[k].valueLength, ref m_attributeParameterValues) != 4)
					{
						return false;
					}
					m_highlightPadding = new Vector4(m_attributeParameterValues[0], m_attributeParameterValues[1], m_attributeParameterValues[2], m_attributeParameterValues[3]);
				}
			}
			return true;
		}
		case 143092:
		case 155892:
			if ((m_fontStyle & FontStyles.Highlight) != FontStyles.Highlight)
			{
				m_highlightColor = m_highlightColorStack.Remove();
				if (m_fontStyleStack.Remove(FontStyles.Highlight) == 0)
				{
					m_FontStyleInternal &= (FontStyles)(-513);
				}
			}
			return true;
		case 4728:
		case 6552:
			m_fontScaleMultiplier *= ((m_currentFontAsset.faceInfo.subscriptSize > 0f) ? m_currentFontAsset.faceInfo.subscriptSize : 1f);
			m_baselineOffsetStack.Push(m_baselineOffset);
			m_baselineOffset += m_currentFontAsset.faceInfo.subscriptOffset * m_fontScale * m_fontScaleMultiplier;
			m_fontStyleStack.Add(FontStyles.Subscript);
			m_FontStyleInternal |= FontStyles.Subscript;
			return true;
		case 20849:
		case 22673:
			if ((m_FontStyleInternal & FontStyles.Subscript) == FontStyles.Subscript)
			{
				if (m_fontScaleMultiplier < 1f)
				{
					m_baselineOffset = m_baselineOffsetStack.Pop();
					m_fontScaleMultiplier /= ((m_currentFontAsset.faceInfo.subscriptSize > 0f) ? m_currentFontAsset.faceInfo.subscriptSize : 1f);
				}
				if (m_fontStyleStack.Remove(FontStyles.Subscript) == 0)
				{
					m_FontStyleInternal &= (FontStyles)(-257);
				}
			}
			return true;
		case 4742:
		case 6566:
			m_fontScaleMultiplier *= ((m_currentFontAsset.faceInfo.superscriptSize > 0f) ? m_currentFontAsset.faceInfo.superscriptSize : 1f);
			m_baselineOffsetStack.Push(m_baselineOffset);
			m_baselineOffset += m_currentFontAsset.faceInfo.superscriptOffset * m_fontScale * m_fontScaleMultiplier;
			m_fontStyleStack.Add(FontStyles.Superscript);
			m_FontStyleInternal |= FontStyles.Superscript;
			return true;
		case 20863:
		case 22687:
			if ((m_FontStyleInternal & FontStyles.Superscript) == FontStyles.Superscript)
			{
				if (m_fontScaleMultiplier < 1f)
				{
					m_baselineOffset = m_baselineOffsetStack.Pop();
					m_fontScaleMultiplier /= ((m_currentFontAsset.faceInfo.superscriptSize > 0f) ? m_currentFontAsset.faceInfo.superscriptSize : 1f);
				}
				if (m_fontStyleStack.Remove(FontStyles.Superscript) == 0)
				{
					m_FontStyleInternal &= (FontStyles)(-129);
				}
			}
			return true;
		case -330774850:
		case 2012149182:
			num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			switch ((int)num3)
			{
			case 100:
				m_FontWeightInternal = FontWeight.Thin;
				break;
			case 200:
				m_FontWeightInternal = FontWeight.ExtraLight;
				break;
			case 300:
				m_FontWeightInternal = FontWeight.Light;
				break;
			case 400:
				m_FontWeightInternal = FontWeight.Regular;
				break;
			case 500:
				m_FontWeightInternal = FontWeight.Medium;
				break;
			case 600:
				m_FontWeightInternal = FontWeight.SemiBold;
				break;
			case 700:
				m_FontWeightInternal = FontWeight.Bold;
				break;
			case 800:
				m_FontWeightInternal = FontWeight.Heavy;
				break;
			case 900:
				m_FontWeightInternal = FontWeight.Black;
				break;
			}
			m_FontWeightStack.Add(m_FontWeightInternal);
			return true;
		case -1885698441:
		case 457225591:
			m_FontWeightStack.Remove();
			if (m_FontStyleInternal == FontStyles.Bold)
			{
				m_FontWeightInternal = FontWeight.Bold;
			}
			else
			{
				m_FontWeightInternal = m_FontWeightStack.Peek();
			}
			return true;
		case 4556:
		case 6380:
			num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			if (num3 == -9999f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_xAdvance = num3 * (m_isOrthographic ? 1f : 0.1f);
				return true;
			case TagUnitType.FontUnits:
				m_xAdvance = num3 * m_currentFontSize * (m_isOrthographic ? 1f : 0.1f);
				return true;
			case TagUnitType.Percentage:
				m_xAdvance = m_marginWidth * num3 / 100f;
				return true;
			default:
				return false;
			}
		case 20677:
		case 22501:
			m_isIgnoringAlignment = false;
			return true;
		case 11642281:
		case 16034505:
			num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			if (num3 == -9999f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_baselineOffset = num3 * (m_isOrthographic ? 1f : 0.1f);
				return true;
			case TagUnitType.FontUnits:
				m_baselineOffset = num3 * (m_isOrthographic ? 1f : 0.1f) * m_currentFontSize;
				return true;
			case TagUnitType.Percentage:
				return false;
			default:
				return false;
			}
		case 50348802:
		case 54741026:
			m_baselineOffset = 0f;
			return true;
		case 31191:
		case 43991:
			if (m_overflowMode == TextOverflowModes.Page)
			{
				m_xAdvance = 0f + tag_LineIndent + tag_Indent;
				m_lineOffset = 0f;
				m_pageNumber++;
				m_isNewPage = true;
			}
			return true;
		case 31169:
		case 43969:
			m_isNonBreakingSpace = true;
			return true;
		case 144016:
		case 156816:
			m_isNonBreakingSpace = false;
			return true;
		case 32745:
		case 45545:
			num3 = Mathf.Clamp(ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength), 0f, 400f);
			if (num3 == -9999f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				if (m_htmlTag[5] == '+')
				{
					m_currentFontSize = m_fontSize + num3;
					m_sizeStack.Add(m_currentFontSize);
					m_fontScale = m_currentFontSize / (float)m_currentFontAsset.faceInfo.pointSize * m_currentFontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f);
					return true;
				}
				if (m_htmlTag[5] == '-')
				{
					m_currentFontSize = m_fontSize + num3;
					m_sizeStack.Add(m_currentFontSize);
					m_fontScale = m_currentFontSize / (float)m_currentFontAsset.faceInfo.pointSize * m_currentFontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f);
					return true;
				}
				m_currentFontSize = num3;
				m_sizeStack.Add(m_currentFontSize);
				m_fontScale = m_currentFontSize / (float)m_currentFontAsset.faceInfo.pointSize * m_currentFontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f);
				return true;
			case TagUnitType.FontUnits:
				m_currentFontSize = m_fontSize * num3;
				m_sizeStack.Add(m_currentFontSize);
				m_fontScale = m_currentFontSize / (float)m_currentFontAsset.faceInfo.pointSize * m_currentFontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f);
				return true;
			case TagUnitType.Percentage:
				m_currentFontSize = m_fontSize * num3 / 100f;
				m_sizeStack.Add(m_currentFontSize);
				m_fontScale = m_currentFontSize / (float)m_currentFontAsset.faceInfo.pointSize * m_currentFontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f);
				return true;
			default:
				return false;
			}
		case 145592:
		case 158392:
			m_currentFontSize = m_sizeStack.Remove();
			m_fontScale = m_currentFontSize / (float)m_currentFontAsset.faceInfo.pointSize * m_currentFontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f);
			return true;
		case 28511:
		case 41311:
		{
			int valueHashCode4 = m_xmlAttribute[0].valueHashCode;
			int nameHashCode2 = m_xmlAttribute[1].nameHashCode;
			int valueHashCode3 = m_xmlAttribute[1].valueHashCode;
			if (valueHashCode4 == 764638571 || valueHashCode4 == 523367755)
			{
				m_currentFontAsset = m_materialReferences[0].fontAsset;
				m_currentMaterial = m_materialReferences[0].material;
				m_currentMaterialIndex = 0;
				m_fontScale = m_currentFontSize / (float)m_currentFontAsset.faceInfo.pointSize * m_currentFontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f);
				m_materialReferenceStack.Add(m_materialReferences[0]);
				return true;
			}
			if (!MaterialReferenceManager.TryGetFontAsset(valueHashCode4, out var fontAsset))
			{
				fontAsset = Resources.Load<TMP_FontAsset>(TMP_Settings.defaultFontAssetPath + new string(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength));
				if (fontAsset == null)
				{
					return false;
				}
				MaterialReferenceManager.AddFontAsset(fontAsset);
			}
			if (nameHashCode2 == 0 && valueHashCode3 == 0)
			{
				m_currentMaterial = fontAsset.material;
				m_currentMaterialIndex = MaterialReference.AddMaterialReference(m_currentMaterial, fontAsset, m_materialReferences, m_materialReferenceIndexLookup);
				m_materialReferenceStack.Add(m_materialReferences[m_currentMaterialIndex]);
			}
			else
			{
				if (nameHashCode2 != 103415287 && nameHashCode2 != 72669687)
				{
					return false;
				}
				if (MaterialReferenceManager.TryGetMaterial(valueHashCode3, out currentMaterial))
				{
					m_currentMaterial = currentMaterial;
					m_currentMaterialIndex = MaterialReference.AddMaterialReference(m_currentMaterial, fontAsset, m_materialReferences, m_materialReferenceIndexLookup);
					m_materialReferenceStack.Add(m_materialReferences[m_currentMaterialIndex]);
				}
				else
				{
					currentMaterial = Resources.Load<Material>(TMP_Settings.defaultFontAssetPath + new string(m_htmlTag, m_xmlAttribute[1].valueStartIndex, m_xmlAttribute[1].valueLength));
					if (currentMaterial == null)
					{
						return false;
					}
					MaterialReferenceManager.AddFontMaterial(valueHashCode3, currentMaterial);
					m_currentMaterial = currentMaterial;
					m_currentMaterialIndex = MaterialReference.AddMaterialReference(m_currentMaterial, fontAsset, m_materialReferences, m_materialReferenceIndexLookup);
					m_materialReferenceStack.Add(m_materialReferences[m_currentMaterialIndex]);
				}
			}
			m_currentFontAsset = fontAsset;
			m_fontScale = m_currentFontSize / (float)m_currentFontAsset.faceInfo.pointSize * m_currentFontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f);
			return true;
		}
		case 141358:
		case 154158:
		{
			MaterialReference materialReference2 = m_materialReferenceStack.Remove();
			m_currentFontAsset = materialReference2.fontAsset;
			m_currentMaterial = materialReference2.material;
			m_currentMaterialIndex = materialReference2.index;
			m_fontScale = m_currentFontSize / (float)m_currentFontAsset.faceInfo.pointSize * m_currentFontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f);
			return true;
		}
		case 72669687:
		case 103415287:
		{
			int valueHashCode3 = m_xmlAttribute[0].valueHashCode;
			if (valueHashCode3 == 764638571 || valueHashCode3 == 523367755)
			{
				m_currentMaterial = m_materialReferences[0].material;
				m_currentMaterialIndex = 0;
				m_materialReferenceStack.Add(m_materialReferences[0]);
				return true;
			}
			if (MaterialReferenceManager.TryGetMaterial(valueHashCode3, out currentMaterial))
			{
				m_currentMaterial = currentMaterial;
				m_currentMaterialIndex = MaterialReference.AddMaterialReference(m_currentMaterial, m_currentFontAsset, m_materialReferences, m_materialReferenceIndexLookup);
				m_materialReferenceStack.Add(m_materialReferences[m_currentMaterialIndex]);
			}
			else
			{
				currentMaterial = Resources.Load<Material>(TMP_Settings.defaultFontAssetPath + new string(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength));
				if (currentMaterial == null)
				{
					return false;
				}
				MaterialReferenceManager.AddFontMaterial(valueHashCode3, currentMaterial);
				m_currentMaterial = currentMaterial;
				m_currentMaterialIndex = MaterialReference.AddMaterialReference(m_currentMaterial, m_currentFontAsset, m_materialReferences, m_materialReferenceIndexLookup);
				m_materialReferenceStack.Add(m_materialReferences[m_currentMaterialIndex]);
			}
			return true;
		}
		case 343615334:
		case 374360934:
		{
			MaterialReference materialReference = m_materialReferenceStack.Remove();
			m_currentMaterial = materialReference.material;
			m_currentMaterialIndex = materialReference.index;
			return true;
		}
		case 230446:
		case 320078:
			num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			if (num3 == -9999f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_xAdvance += num3 * (m_isOrthographic ? 1f : 0.1f);
				return true;
			case TagUnitType.FontUnits:
				m_xAdvance += num3 * (m_isOrthographic ? 1f : 0.1f) * m_currentFontSize;
				return true;
			case TagUnitType.Percentage:
				return false;
			default:
				return false;
			}
		case 186622:
		case 276254:
			if (m_xmlAttribute[0].valueLength != 3)
			{
				return false;
			}
			m_htmlColor.a = (byte)(HexToInt(m_htmlTag[7]) * 16 + HexToInt(m_htmlTag[8]));
			return true;
		case 1750458:
			return false;
		case 426:
			return true;
		case 30266:
		case 43066:
			if (m_isParsingText && !m_isCalculatingPreferredValues)
			{
				int linkCount = m_textInfo.linkCount;
				if (linkCount + 1 > m_textInfo.linkInfo.Length)
				{
					TMP_TextInfo.Resize(ref m_textInfo.linkInfo, linkCount + 1);
				}
				m_textInfo.linkInfo[linkCount].textComponent = this;
				m_textInfo.linkInfo[linkCount].hashCode = m_xmlAttribute[0].valueHashCode;
				m_textInfo.linkInfo[linkCount].linkTextfirstCharacterIndex = m_characterCount;
				m_textInfo.linkInfo[linkCount].linkIdFirstCharacterIndex = startIndex + m_xmlAttribute[0].valueStartIndex;
				m_textInfo.linkInfo[linkCount].linkIdLength = m_xmlAttribute[0].valueLength;
				m_textInfo.linkInfo[linkCount].SetLinkID(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			}
			return true;
		case 143113:
		case 155913:
			if (m_isParsingText && !m_isCalculatingPreferredValues && m_textInfo.linkCount < m_textInfo.linkInfo.Length)
			{
				m_textInfo.linkInfo[m_textInfo.linkCount].linkTextLength = m_characterCount - m_textInfo.linkInfo[m_textInfo.linkCount].linkTextfirstCharacterIndex;
				m_textInfo.linkCount++;
			}
			return true;
		case 186285:
		case 275917:
			switch (m_xmlAttribute[0].valueHashCode)
			{
			case 3774683:
				m_lineJustification = TextAlignmentOptions.Left;
				m_lineJustificationStack.Add(m_lineJustification);
				return true;
			case 136703040:
				m_lineJustification = TextAlignmentOptions.Right;
				m_lineJustificationStack.Add(m_lineJustification);
				return true;
			case -458210101:
				m_lineJustification = TextAlignmentOptions.Center;
				m_lineJustificationStack.Add(m_lineJustification);
				return true;
			case -523808257:
				m_lineJustification = TextAlignmentOptions.Justified;
				m_lineJustificationStack.Add(m_lineJustification);
				return true;
			case 122383428:
				m_lineJustification = TextAlignmentOptions.Flush;
				m_lineJustificationStack.Add(m_lineJustification);
				return true;
			default:
				return false;
			}
		case 976214:
		case 1065846:
			m_lineJustification = m_lineJustificationStack.Remove();
			return true;
		case 237918:
		case 327550:
			num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			if (num3 == -9999f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_width = num3 * (m_isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				return false;
			case TagUnitType.Percentage:
				m_width = m_marginWidth * num3 / 100f;
				break;
			}
			return true;
		case 1027847:
		case 1117479:
			m_width = -1f;
			return true;
		case 192323:
		case 281955:
			if (m_htmlTag[6] == '#' && num == 10)
			{
				m_htmlColor = HexCharsToColor(m_htmlTag, num);
				m_colorStack.Add(m_htmlColor);
				return true;
			}
			if (m_htmlTag[6] == '#' && num == 11)
			{
				m_htmlColor = HexCharsToColor(m_htmlTag, num);
				m_colorStack.Add(m_htmlColor);
				return true;
			}
			if (m_htmlTag[6] == '#' && num == 13)
			{
				m_htmlColor = HexCharsToColor(m_htmlTag, num);
				m_colorStack.Add(m_htmlColor);
				return true;
			}
			if (m_htmlTag[6] == '#' && num == 15)
			{
				m_htmlColor = HexCharsToColor(m_htmlTag, num);
				m_colorStack.Add(m_htmlColor);
				return true;
			}
			switch (m_xmlAttribute[0].valueHashCode)
			{
			case 125395:
				m_htmlColor = Color.red;
				m_colorStack.Add(m_htmlColor);
				return true;
			case 3573310:
				m_htmlColor = Color.blue;
				m_colorStack.Add(m_htmlColor);
				return true;
			case 117905991:
				m_htmlColor = Color.black;
				m_colorStack.Add(m_htmlColor);
				return true;
			case 121463835:
				m_htmlColor = Color.green;
				m_colorStack.Add(m_htmlColor);
				return true;
			case 140357351:
				m_htmlColor = Color.white;
				m_colorStack.Add(m_htmlColor);
				return true;
			case 26556144:
				m_htmlColor = new Color32(byte.MaxValue, 128, 0, byte.MaxValue);
				m_colorStack.Add(m_htmlColor);
				return true;
			case -36881330:
				m_htmlColor = new Color32(160, 32, 240, byte.MaxValue);
				m_colorStack.Add(m_htmlColor);
				return true;
			case 554054276:
				m_htmlColor = Color.yellow;
				m_colorStack.Add(m_htmlColor);
				return true;
			default:
				return false;
			}
		case 69403544:
		case 100149144:
		{
			int valueHashCode2 = m_xmlAttribute[0].valueHashCode;
			if (MaterialReferenceManager.TryGetColorGradientPreset(valueHashCode2, out var gradientPreset))
			{
				m_colorGradientPreset = gradientPreset;
			}
			else
			{
				if (gradientPreset == null)
				{
					gradientPreset = Resources.Load<TMP_ColorGradient>(TMP_Settings.defaultColorGradientPresetsPath + new string(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength));
				}
				if (gradientPreset == null)
				{
					return false;
				}
				MaterialReferenceManager.AddColorGradientPreset(valueHashCode2, gradientPreset);
				m_colorGradientPreset = gradientPreset;
			}
			m_colorGradientStack.Add(m_colorGradientPreset);
			return true;
		}
		case 340349191:
		case 371094791:
			m_colorGradientPreset = m_colorGradientStack.Remove();
			return true;
		case 1356515:
		case 1983971:
			num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			if (num3 == -9999f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_cSpacing = num3 * (m_isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				m_cSpacing = num3 * (m_isOrthographic ? 1f : 0.1f) * m_currentFontSize;
				break;
			case TagUnitType.Percentage:
				return false;
			}
			return true;
		case 6886018:
		case 7513474:
			if (!m_isParsingText)
			{
				return true;
			}
			if (m_characterCount > 0)
			{
				m_xAdvance -= m_cSpacing;
				m_textInfo.characterInfo[m_characterCount - 1].xAdvance = m_xAdvance;
			}
			m_cSpacing = 0f;
			return true;
		case 1524585:
		case 2152041:
			num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			if (num3 == -9999f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_monoSpacing = num3 * (m_isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				m_monoSpacing = num3 * (m_isOrthographic ? 1f : 0.1f) * m_currentFontSize;
				break;
			case TagUnitType.Percentage:
				return false;
			}
			return true;
		case 7054088:
		case 7681544:
			m_monoSpacing = 0f;
			return true;
		case 280416:
			return false;
		case 982252:
		case 1071884:
			m_htmlColor = m_colorStack.Remove();
			return true;
		case 1441524:
		case 2068980:
			num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			if (num3 == -9999f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				tag_Indent = num3 * (m_isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				tag_Indent = num3 * (m_isOrthographic ? 1f : 0.1f) * m_currentFontSize;
				break;
			case TagUnitType.Percentage:
				tag_Indent = m_marginWidth * num3 / 100f;
				break;
			}
			m_indentStack.Add(tag_Indent);
			m_xAdvance = tag_Indent;
			return true;
		case 6971027:
		case 7598483:
			tag_Indent = m_indentStack.Remove();
			return true;
		case -842656867:
		case 1109386397:
			num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			if (num3 == -9999f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				tag_LineIndent = num3 * (m_isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				tag_LineIndent = num3 * (m_isOrthographic ? 1f : 0.1f) * m_currentFontSize;
				break;
			case TagUnitType.Percentage:
				tag_LineIndent = m_marginWidth * num3 / 100f;
				break;
			}
			m_xAdvance += tag_LineIndent;
			return true;
		case -445537194:
		case 1897386838:
			tag_LineIndent = 0f;
			return true;
		case 2246877:
		case 514803617:
		case 730022849:
			m_FontStyleInternal |= FontStyles.LowerCase;
			m_fontStyleStack.Add(FontStyles.LowerCase);
			return true;
		case -1883544150:
		case -1668324918:
			if ((m_fontStyle & FontStyles.LowerCase) != FontStyles.LowerCase && m_fontStyleStack.Remove(FontStyles.LowerCase) == 0)
			{
				m_FontStyleInternal &= (FontStyles)(-9);
			}
			return true;
		case 9133802:
		case 13526026:
		case 566686826:
		case 781906058:
			m_FontStyleInternal |= FontStyles.UpperCase;
			m_fontStyleStack.Add(FontStyles.UpperCase);
			return true;
		case -1831660941:
		case -1616441709:
		case 47840323:
		case 52232547:
			if ((m_fontStyle & FontStyles.UpperCase) != FontStyles.UpperCase && m_fontStyleStack.Remove(FontStyles.UpperCase) == 0)
			{
				m_FontStyleInternal &= (FontStyles)(-17);
			}
			return true;
		case 551025096:
		case 766244328:
			m_FontStyleInternal |= FontStyles.SmallCaps;
			m_fontStyleStack.Add(FontStyles.SmallCaps);
			return true;
		case -1847322671:
		case -1632103439:
			if ((m_fontStyle & FontStyles.SmallCaps) != FontStyles.SmallCaps && m_fontStyleStack.Remove(FontStyles.SmallCaps) == 0)
			{
				m_FontStyleInternal &= (FontStyles)(-33);
			}
			return true;
		case 1482398:
		case 2109854:
			switch (m_xmlAttribute[0].valueType)
			{
			case TagValueType.NumericalValue:
				num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
				if (num3 == -9999f)
				{
					return false;
				}
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					m_marginLeft = num3 * (m_isOrthographic ? 1f : 0.1f);
					break;
				case TagUnitType.FontUnits:
					m_marginLeft = num3 * (m_isOrthographic ? 1f : 0.1f) * m_currentFontSize;
					break;
				case TagUnitType.Percentage:
					m_marginLeft = (m_marginWidth - ((m_width != -1f) ? m_width : 0f)) * num3 / 100f;
					break;
				}
				m_marginLeft = ((m_marginLeft >= 0f) ? m_marginLeft : 0f);
				m_marginRight = m_marginLeft;
				return true;
			case TagValueType.None:
			{
				for (int l = 1; l < m_xmlAttribute.Length && m_xmlAttribute[l].nameHashCode != 0; l++)
				{
					switch (m_xmlAttribute[l].nameHashCode)
					{
					case 42823:
						num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[l].valueStartIndex, m_xmlAttribute[l].valueLength);
						if (num3 == -9999f)
						{
							return false;
						}
						switch (m_xmlAttribute[l].unitType)
						{
						case TagUnitType.Pixels:
							m_marginLeft = num3 * (m_isOrthographic ? 1f : 0.1f);
							break;
						case TagUnitType.FontUnits:
							m_marginLeft = num3 * (m_isOrthographic ? 1f : 0.1f) * m_currentFontSize;
							break;
						case TagUnitType.Percentage:
							m_marginLeft = (m_marginWidth - ((m_width != -1f) ? m_width : 0f)) * num3 / 100f;
							break;
						}
						m_marginLeft = ((m_marginLeft >= 0f) ? m_marginLeft : 0f);
						break;
					case 315620:
						num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[l].valueStartIndex, m_xmlAttribute[l].valueLength);
						if (num3 == -9999f)
						{
							return false;
						}
						switch (m_xmlAttribute[l].unitType)
						{
						case TagUnitType.Pixels:
							m_marginRight = num3 * (m_isOrthographic ? 1f : 0.1f);
							break;
						case TagUnitType.FontUnits:
							m_marginRight = num3 * (m_isOrthographic ? 1f : 0.1f) * m_currentFontSize;
							break;
						case TagUnitType.Percentage:
							m_marginRight = (m_marginWidth - ((m_width != -1f) ? m_width : 0f)) * num3 / 100f;
							break;
						}
						m_marginRight = ((m_marginRight >= 0f) ? m_marginRight : 0f);
						break;
					}
				}
				return true;
			}
			default:
				return false;
			}
		case 7011901:
		case 7639357:
			m_marginLeft = 0f;
			m_marginRight = 0f;
			return true;
		case -855002522:
		case 1100728678:
			num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			if (num3 == -9999f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_marginLeft = num3 * (m_isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				m_marginLeft = num3 * (m_isOrthographic ? 1f : 0.1f) * m_currentFontSize;
				break;
			case TagUnitType.Percentage:
				m_marginLeft = (m_marginWidth - ((m_width != -1f) ? m_width : 0f)) * num3 / 100f;
				break;
			}
			m_marginLeft = ((m_marginLeft >= 0f) ? m_marginLeft : 0f);
			return true;
		case -1690034531:
		case -884817987:
			num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			if (num3 == -9999f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_marginRight = num3 * (m_isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				m_marginRight = num3 * (m_isOrthographic ? 1f : 0.1f) * m_currentFontSize;
				break;
			case TagUnitType.Percentage:
				m_marginRight = (m_marginWidth - ((m_width != -1f) ? m_width : 0f)) * num3 / 100f;
				break;
			}
			m_marginRight = ((m_marginRight >= 0f) ? m_marginRight : 0f);
			return true;
		case -842693512:
		case 1109349752:
			num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			if (num3 == -9999f || num3 == 0f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_lineHeight = num3 * (m_isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				m_lineHeight = num3 * (m_isOrthographic ? 1f : 0.1f) * m_currentFontSize;
				break;
			case TagUnitType.Percentage:
				m_lineHeight = m_fontAsset.faceInfo.lineHeight * num3 / 100f * m_fontScale;
				break;
			}
			return true;
		case -445573839:
		case 1897350193:
			m_lineHeight = -32767f;
			return true;
		case 10723418:
		case 15115642:
			tag_NoParsing = true;
			return true;
		case 1286342:
		case 1913798:
		{
			int valueHashCode = m_xmlAttribute[0].valueHashCode;
			if (m_isParsingText)
			{
				m_actionStack.Add(valueHashCode);
				UnityEngine.Debug.Log("Action ID: [" + valueHashCode + "] First character index: " + m_characterCount);
			}
			return true;
		}
		case 6815845:
		case 7443301:
			if (m_isParsingText)
			{
				UnityEngine.Debug.Log("Action ID: [" + m_actionStack.CurrentItem() + "] Last character index: " + (m_characterCount - 1));
			}
			m_actionStack.Remove();
			return true;
		case 226050:
		case 315682:
			num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			if (num3 == -9999f)
			{
				return false;
			}
			m_FXMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(num3, 1f, 1f));
			m_isFXMatrixSet = true;
			return true;
		case 1015979:
		case 1105611:
			m_isFXMatrixSet = false;
			return true;
		case 1600507:
		case 2227963:
			num3 = ConvertToFloat(m_htmlTag, m_xmlAttribute[0].valueStartIndex, m_xmlAttribute[0].valueLength);
			if (num3 == -9999f)
			{
				return false;
			}
			m_FXMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, num3), Vector3.one);
			m_isFXMatrixSet = true;
			return true;
		case 7130010:
		case 7757466:
			m_isFXMatrixSet = false;
			return true;
		case 227814:
		case 317446:
			if (m_xmlAttribute[1].nameHashCode == 327550)
			{
				float num5 = ConvertToFloat(m_htmlTag, m_xmlAttribute[1].valueStartIndex, m_xmlAttribute[1].valueLength);
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					UnityEngine.Debug.Log("Table width = " + num5 + "px.");
					break;
				case TagUnitType.FontUnits:
					UnityEngine.Debug.Log("Table width = " + num5 + "em.");
					break;
				case TagUnitType.Percentage:
					UnityEngine.Debug.Log("Table width = " + num5 + "%.");
					break;
				}
			}
			return true;
		case 1017743:
		case 1107375:
			return true;
		case 670:
		case 926:
			return true;
		case 2973:
		case 3229:
			return true;
		case 660:
		case 916:
			return true;
		case 2963:
		case 3219:
			return true;
		case 656:
		case 912:
		{
			for (int j = 1; j < m_xmlAttribute.Length && m_xmlAttribute[j].nameHashCode != 0; j++)
			{
				switch (m_xmlAttribute[j].nameHashCode)
				{
				case 327550:
				{
					float num4 = ConvertToFloat(m_htmlTag, m_xmlAttribute[j].valueStartIndex, m_xmlAttribute[j].valueLength);
					switch (tagUnitType)
					{
					case TagUnitType.Pixels:
						UnityEngine.Debug.Log("Table width = " + num4 + "px.");
						break;
					case TagUnitType.FontUnits:
						UnityEngine.Debug.Log("Table width = " + num4 + "em.");
						break;
					case TagUnitType.Percentage:
						UnityEngine.Debug.Log("Table width = " + num4 + "%.");
						break;
					}
					break;
				}
				case 275917:
					switch (m_xmlAttribute[j].valueHashCode)
					{
					case 3774683:
						UnityEngine.Debug.Log("TD align=\"left\".");
						break;
					case 136703040:
						UnityEngine.Debug.Log("TD align=\"right\".");
						break;
					case -458210101:
						UnityEngine.Debug.Log("TD align=\"center\".");
						break;
					case -523808257:
						UnityEngine.Debug.Log("TD align=\"justified\".");
						break;
					}
					break;
				}
			}
			return true;
		}
		case 2959:
		case 3215:
			return true;
		default:
			return false;
		}
	}
}
public enum TextElementType : byte
{
	Character = 1,
	Sprite
}
[Serializable]
public class TMP_TextElement
{
	[SerializeField]
	protected TextElementType m_ElementType;

	[SerializeField]
	private uint m_Unicode;

	private Glyph m_Glyph;

	[SerializeField]
	private uint m_GlyphIndex;

	[SerializeField]
	private float m_Scale;

	public TextElementType elementType => m_ElementType;

	public uint unicode
	{
		get
		{
			return m_Unicode;
		}
		set
		{
			m_Unicode = value;
		}
	}

	public Glyph glyph
	{
		get
		{
			return m_Glyph;
		}
		set
		{
			m_Glyph = value;
		}
	}

	public uint glyphIndex
	{
		get
		{
			return m_GlyphIndex;
		}
		set
		{
			m_GlyphIndex = value;
		}
	}

	public float scale
	{
		get
		{
			return m_Scale;
		}
		set
		{
			m_Scale = value;
		}
	}
}
[Serializable]
public class TMP_TextElement_Legacy
{
	public int id;

	public float x;

	public float y;

	public float width;

	public float height;

	public float xOffset;

	public float yOffset;

	public float xAdvance;

	public float scale;
}
[Serializable]
public class TMP_TextInfo
{
	private static Vector2 k_InfinityVectorPositive = new Vector2(32767f, 32767f);

	private static Vector2 k_InfinityVectorNegative = new Vector2(-32767f, -32767f);

	public TMP_Text textComponent;

	public int characterCount;

	public int spriteCount;

	public int spaceCount;

	public int wordCount;

	public int linkCount;

	public int lineCount;

	public int pageCount;

	public int materialCount;

	public TMP_CharacterInfo[] characterInfo;

	public TMP_WordInfo[] wordInfo;

	public TMP_LinkInfo[] linkInfo;

	public TMP_LineInfo[] lineInfo;

	public TMP_PageInfo[] pageInfo;

	public TMP_MeshInfo[] meshInfo;

	private TMP_MeshInfo[] m_CachedMeshInfo;

	public TMP_TextInfo()
	{
		characterInfo = new TMP_CharacterInfo[8];
		wordInfo = new TMP_WordInfo[16];
		linkInfo = new TMP_LinkInfo[0];
		lineInfo = new TMP_LineInfo[2];
		pageInfo = new TMP_PageInfo[4];
		meshInfo = new TMP_MeshInfo[1];
	}

	public TMP_TextInfo(TMP_Text textComponent)
	{
		this.textComponent = textComponent;
		characterInfo = new TMP_CharacterInfo[8];
		wordInfo = new TMP_WordInfo[4];
		linkInfo = new TMP_LinkInfo[0];
		lineInfo = new TMP_LineInfo[2];
		pageInfo = new TMP_PageInfo[4];
		meshInfo = new TMP_MeshInfo[1];
		meshInfo[0].mesh = textComponent.mesh;
		materialCount = 1;
	}

	public void Clear()
	{
		characterCount = 0;
		spaceCount = 0;
		wordCount = 0;
		linkCount = 0;
		lineCount = 0;
		pageCount = 0;
		spriteCount = 0;
		for (int i = 0; i < meshInfo.Length; i++)
		{
			meshInfo[i].vertexCount = 0;
		}
	}

	public void ClearMeshInfo(bool updateMesh)
	{
		for (int i = 0; i < meshInfo.Length; i++)
		{
			meshInfo[i].Clear(updateMesh);
		}
	}

	public void ClearAllMeshInfo()
	{
		for (int i = 0; i < meshInfo.Length; i++)
		{
			meshInfo[i].Clear(uploadChanges: true);
		}
	}

	public void ResetVertexLayout(bool isVolumetric)
	{
		for (int i = 0; i < meshInfo.Length; i++)
		{
			meshInfo[i].ResizeMeshInfo(0, isVolumetric);
		}
	}

	public void ClearUnusedVertices(MaterialReference[] materials)
	{
		for (int i = 0; i < meshInfo.Length; i++)
		{
			int startIndex = 0;
			meshInfo[i].ClearUnusedVertices(startIndex);
		}
	}

	public void ClearLineInfo()
	{
		if (lineInfo == null)
		{
			lineInfo = new TMP_LineInfo[2];
		}
		for (int i = 0; i < lineInfo.Length; i++)
		{
			lineInfo[i].characterCount = 0;
			lineInfo[i].spaceCount = 0;
			lineInfo[i].wordCount = 0;
			lineInfo[i].controlCharacterCount = 0;
			lineInfo[i].width = 0f;
			lineInfo[i].ascender = k_InfinityVectorNegative.x;
			lineInfo[i].descender = k_InfinityVectorPositive.x;
			lineInfo[i].lineExtents.min = k_InfinityVectorPositive;
			lineInfo[i].lineExtents.max = k_InfinityVectorNegative;
			lineInfo[i].maxAdvance = 0f;
		}
	}

	public TMP_MeshInfo[] CopyMeshInfoVertexData()
	{
		if (m_CachedMeshInfo == null || m_CachedMeshInfo.Length != meshInfo.Length)
		{
			m_CachedMeshInfo = new TMP_MeshInfo[meshInfo.Length];
			for (int i = 0; i < m_CachedMeshInfo.Length; i++)
			{
				int num = meshInfo[i].vertices.Length;
				m_CachedMeshInfo[i].vertices = new Vector3[num];
				m_CachedMeshInfo[i].uvs0 = new Vector2[num];
				m_CachedMeshInfo[i].uvs2 = new Vector2[num];
				m_CachedMeshInfo[i].colors32 = new Color32[num];
			}
		}
		for (int j = 0; j < m_CachedMeshInfo.Length; j++)
		{
			int num2 = meshInfo[j].vertices.Length;
			if (m_CachedMeshInfo[j].vertices.Length != num2)
			{
				m_CachedMeshInfo[j].vertices = new Vector3[num2];
				m_CachedMeshInfo[j].uvs0 = new Vector2[num2];
				m_CachedMeshInfo[j].uvs2 = new Vector2[num2];
				m_CachedMeshInfo[j].colors32 = new Color32[num2];
			}
			Array.Copy(meshInfo[j].vertices, m_CachedMeshInfo[j].vertices, num2);
			Array.Copy(meshInfo[j].uvs0, m_CachedMeshInfo[j].uvs0, num2);
			Array.Copy(meshInfo[j].uvs2, m_CachedMeshInfo[j].uvs2, num2);
			Array.Copy(meshInfo[j].colors32, m_CachedMeshInfo[j].colors32, num2);
		}
		return m_CachedMeshInfo;
	}

	public static void Resize<T>(ref T[] array, int size)
	{
		int newSize = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
		Array.Resize(ref array, newSize);
	}

	public static void Resize<T>(ref T[] array, int size, bool isBlockAllocated)
	{
		if (isBlockAllocated)
		{
			size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
		}
		if (size != array.Length)
		{
			Array.Resize(ref array, size);
		}
	}
}
public class TMP_TextParsingUtilities
{
	private static readonly TMP_TextParsingUtilities s_Instance;

	private const string k_LookupStringL = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-";

	private const string k_LookupStringU = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-";

	public static TMP_TextParsingUtilities instance => s_Instance;

	static TMP_TextParsingUtilities()
	{
		s_Instance = new TMP_TextParsingUtilities();
	}

	public static uint GetHashCode(string s)
	{
		uint num = 0u;
		for (int i = 0; i < s.Length; i++)
		{
			num = ((num << 5) + num) ^ ToUpperASCIIFast(s[i]);
		}
		return num;
	}

	public static int GetHashCodeCaseSensitive(string s)
	{
		int num = 0;
		for (int i = 0; i < s.Length; i++)
		{
			num = ((num << 5) + num) ^ s[i];
		}
		return num;
	}

	public static char ToLowerASCIIFast(char c)
	{
		if (c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1)
		{
			return c;
		}
		return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-"[c];
	}

	public static char ToUpperASCIIFast(char c)
	{
		if (c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1)
		{
			return c;
		}
		return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[c];
	}

	public static uint ToUpperASCIIFast(uint c)
	{
		if (c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1)
		{
			return c;
		}
		return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
	}

	public static uint ToLowerASCIIFast(uint c)
	{
		if (c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1)
		{
			return c;
		}
		return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-"[(int)c];
	}

	public static bool IsHighSurrogate(uint c)
	{
		if (c > 55296)
		{
			return c < 56319;
		}
		return false;
	}

	public static bool IsLowSurrogate(uint c)
	{
		if (c > 56320)
		{
			return c < 57343;
		}
		return false;
	}
}
public enum CaretPosition
{
	None,
	Left,
	Right
}
public struct CaretInfo
{
	public int index;

	public CaretPosition position;

	public CaretInfo(int index, CaretPosition position)
	{
		this.index = index;
		this.position = position;
	}
}
public static class TMP_TextUtilities
{
	private struct LineSegment
	{
		public Vector3 Point1;

		public Vector3 Point2;

		public LineSegment(Vector3 p1, Vector3 p2)
		{
			Point1 = p1;
			Point2 = p2;
		}
	}

	private static Vector3[] m_rectWorldCorners = new Vector3[4];

	private const string k_lookupStringL = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-";

	private const string k_lookupStringU = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-";

	public static int GetCursorIndexFromPosition(TMP_Text textComponent, Vector3 position, Camera camera)
	{
		int num = FindNearestCharacter(textComponent, position, camera, visibleOnly: false);
		RectTransform rectTransform = textComponent.rectTransform;
		ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
		TMP_CharacterInfo tMP_CharacterInfo = textComponent.textInfo.characterInfo[num];
		Vector3 vector = rectTransform.TransformPoint(tMP_CharacterInfo.bottomLeft);
		Vector3 vector2 = rectTransform.TransformPoint(tMP_CharacterInfo.topRight);
		if ((position.x - vector.x) / (vector2.x - vector.x) < 0.5f)
		{
			return num;
		}
		return num + 1;
	}

	public static int GetCursorIndexFromPosition(TMP_Text textComponent, Vector3 position, Camera camera, out CaretPosition cursor)
	{
		int num = FindNearestLine(textComponent, position, camera);
		int num2 = FindNearestCharacterOnLine(textComponent, position, num, camera, visibleOnly: false);
		if (textComponent.textInfo.lineInfo[num].characterCount == 1)
		{
			cursor = CaretPosition.Left;
			return num2;
		}
		RectTransform rectTransform = textComponent.rectTransform;
		ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
		TMP_CharacterInfo tMP_CharacterInfo = textComponent.textInfo.characterInfo[num2];
		Vector3 vector = rectTransform.TransformPoint(tMP_CharacterInfo.bottomLeft);
		Vector3 vector2 = rectTransform.TransformPoint(tMP_CharacterInfo.topRight);
		if ((position.x - vector.x) / (vector2.x - vector.x) < 0.5f)
		{
			cursor = CaretPosition.Left;
			return num2;
		}
		cursor = CaretPosition.Right;
		return num2;
	}

	public static int FindNearestLine(TMP_Text text, Vector3 position, Camera camera)
	{
		RectTransform rectTransform = text.rectTransform;
		float num = float.PositiveInfinity;
		int result = -1;
		ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
		for (int i = 0; i < text.textInfo.lineCount; i++)
		{
			TMP_LineInfo tMP_LineInfo = text.textInfo.lineInfo[i];
			float y = rectTransform.TransformPoint(new Vector3(0f, tMP_LineInfo.ascender, 0f)).y;
			float y2 = rectTransform.TransformPoint(new Vector3(0f, tMP_LineInfo.descender, 0f)).y;
			if (y > position.y && y2 < position.y)
			{
				return i;
			}
			float a = Mathf.Abs(y - position.y);
			float b = Mathf.Abs(y2 - position.y);
			float num2 = Mathf.Min(a, b);
			if (num2 < num)
			{
				num = num2;
				result = i;
			}
		}
		return result;
	}

	public static int FindNearestCharacterOnLine(TMP_Text text, Vector3 position, int line, Camera camera, bool visibleOnly)
	{
		RectTransform rectTransform = text.rectTransform;
		ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
		int firstCharacterIndex = text.textInfo.lineInfo[line].firstCharacterIndex;
		int lastCharacterIndex = text.textInfo.lineInfo[line].lastCharacterIndex;
		float num = float.PositiveInfinity;
		int result = lastCharacterIndex;
		for (int i = firstCharacterIndex; i <= lastCharacterIndex; i++)
		{
			TMP_CharacterInfo tMP_CharacterInfo = text.textInfo.characterInfo[i];
			if ((!visibleOnly || tMP_CharacterInfo.isVisible) && (i != lastCharacterIndex || tMP_CharacterInfo.character != '\u200b'))
			{
				Vector3 vector = rectTransform.TransformPoint(tMP_CharacterInfo.bottomLeft);
				Vector3 vector2 = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.bottomLeft.x, tMP_CharacterInfo.topRight.y, 0f));
				Vector3 vector3 = rectTransform.TransformPoint(tMP_CharacterInfo.topRight);
				Vector3 vector4 = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.bottomLeft.y, 0f));
				if (PointIntersectRectangle(position, vector, vector2, vector3, vector4))
				{
					result = i;
					break;
				}
				float num2 = DistanceToLine(vector, vector2, position);
				float num3 = DistanceToLine(vector2, vector3, position);
				float num4 = DistanceToLine(vector3, vector4, position);
				float num5 = DistanceToLine(vector4, vector, position);
				float num6 = ((num2 < num3) ? num2 : num3);
				num6 = ((num6 < num4) ? num6 : num4);
				num6 = ((num6 < num5) ? num6 : num5);
				if (num > num6)
				{
					num = num6;
					result = i;
				}
			}
		}
		return result;
	}

	public static bool IsIntersectingRectTransform(RectTransform rectTransform, Vector3 position, Camera camera)
	{
		ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
		rectTransform.GetWorldCorners(m_rectWorldCorners);
		if (PointIntersectRectangle(position, m_rectWorldCorners[0], m_rectWorldCorners[1], m_rectWorldCorners[2], m_rectWorldCorners[3]))
		{
			return true;
		}
		return false;
	}

	public static int FindIntersectingCharacter(TMP_Text text, Vector3 position, Camera camera, bool visibleOnly)
	{
		RectTransform rectTransform = text.rectTransform;
		ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
		for (int i = 0; i < text.textInfo.characterCount; i++)
		{
			TMP_CharacterInfo tMP_CharacterInfo = text.textInfo.characterInfo[i];
			if (!visibleOnly || tMP_CharacterInfo.isVisible)
			{
				Vector3 a = rectTransform.TransformPoint(tMP_CharacterInfo.bottomLeft);
				Vector3 b = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.bottomLeft.x, tMP_CharacterInfo.topRight.y, 0f));
				Vector3 c = rectTransform.TransformPoint(tMP_CharacterInfo.topRight);
				Vector3 d = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.bottomLeft.y, 0f));
				if (PointIntersectRectangle(position, a, b, c, d))
				{
					return i;
				}
			}
		}
		return -1;
	}

	public static int FindNearestCharacter(TMP_Text text, Vector3 position, Camera camera, bool visibleOnly)
	{
		RectTransform rectTransform = text.rectTransform;
		float num = float.PositiveInfinity;
		int result = 0;
		ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
		for (int i = 0; i < text.textInfo.characterCount; i++)
		{
			TMP_CharacterInfo tMP_CharacterInfo = text.textInfo.characterInfo[i];
			if (!visibleOnly || tMP_CharacterInfo.isVisible)
			{
				Vector3 vector = rectTransform.TransformPoint(tMP_CharacterInfo.bottomLeft);
				Vector3 vector2 = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.bottomLeft.x, tMP_CharacterInfo.topRight.y, 0f));
				Vector3 vector3 = rectTransform.TransformPoint(tMP_CharacterInfo.topRight);
				Vector3 vector4 = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.bottomLeft.y, 0f));
				if (PointIntersectRectangle(position, vector, vector2, vector3, vector4))
				{
					return i;
				}
				float num2 = DistanceToLine(vector, vector2, position);
				float num3 = DistanceToLine(vector2, vector3, position);
				float num4 = DistanceToLine(vector3, vector4, position);
				float num5 = DistanceToLine(vector4, vector, position);
				float num6 = ((num2 < num3) ? num2 : num3);
				num6 = ((num6 < num4) ? num6 : num4);
				num6 = ((num6 < num5) ? num6 : num5);
				if (num > num6)
				{
					num = num6;
					result = i;
				}
			}
		}
		return result;
	}

	public static int FindIntersectingWord(TMP_Text text, Vector3 position, Camera camera)
	{
		RectTransform rectTransform = text.rectTransform;
		ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
		for (int i = 0; i < text.textInfo.wordCount; i++)
		{
			TMP_WordInfo tMP_WordInfo = text.textInfo.wordInfo[i];
			bool flag = false;
			Vector3 a = Vector3.zero;
			Vector3 b = Vector3.zero;
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			float num = float.NegativeInfinity;
			float num2 = float.PositiveInfinity;
			for (int j = 0; j < tMP_WordInfo.characterCount; j++)
			{
				int num3 = tMP_WordInfo.firstCharacterIndex + j;
				TMP_CharacterInfo tMP_CharacterInfo = text.textInfo.characterInfo[num3];
				int lineNumber = tMP_CharacterInfo.lineNumber;
				bool isVisible = tMP_CharacterInfo.isVisible;
				num = Mathf.Max(num, tMP_CharacterInfo.ascender);
				num2 = Mathf.Min(num2, tMP_CharacterInfo.descender);
				if (!flag && isVisible)
				{
					flag = true;
					a = new Vector3(tMP_CharacterInfo.bottomLeft.x, tMP_CharacterInfo.descender, 0f);
					b = new Vector3(tMP_CharacterInfo.bottomLeft.x, tMP_CharacterInfo.ascender, 0f);
					if (tMP_WordInfo.characterCount == 1)
					{
						flag = false;
						zero = new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.descender, 0f);
						zero2 = new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.ascender, 0f);
						a = rectTransform.TransformPoint(new Vector3(a.x, num2, 0f));
						b = rectTransform.TransformPoint(new Vector3(b.x, num, 0f));
						zero2 = rectTransform.TransformPoint(new Vector3(zero2.x, num, 0f));
						zero = rectTransform.TransformPoint(new Vector3(zero.x, num2, 0f));
						if (PointIntersectRectangle(position, a, b, zero2, zero))
						{
							return i;
						}
					}
				}
				if (flag && j == tMP_WordInfo.characterCount - 1)
				{
					flag = false;
					zero = new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.descender, 0f);
					zero2 = new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.ascender, 0f);
					a = rectTransform.TransformPoint(new Vector3(a.x, num2, 0f));
					b = rectTransform.TransformPoint(new Vector3(b.x, num, 0f));
					zero2 = rectTransform.TransformPoint(new Vector3(zero2.x, num, 0f));
					zero = rectTransform.TransformPoint(new Vector3(zero.x, num2, 0f));
					if (PointIntersectRectangle(position, a, b, zero2, zero))
					{
						return i;
					}
				}
				else if (flag && lineNumber != text.textInfo.characterInfo[num3 + 1].lineNumber)
				{
					flag = false;
					zero = new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.descender, 0f);
					zero2 = new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.ascender, 0f);
					a = rectTransform.TransformPoint(new Vector3(a.x, num2, 0f));
					b = rectTransform.TransformPoint(new Vector3(b.x, num, 0f));
					zero2 = rectTransform.TransformPoint(new Vector3(zero2.x, num, 0f));
					zero = rectTransform.TransformPoint(new Vector3(zero.x, num2, 0f));
					num = float.NegativeInfinity;
					num2 = float.PositiveInfinity;
					if (PointIntersectRectangle(position, a, b, zero2, zero))
					{
						return i;
					}
				}
			}
		}
		return -1;
	}

	public static int FindNearestWord(TMP_Text text, Vector3 position, Camera camera)
	{
		RectTransform rectTransform = text.rectTransform;
		float num = float.PositiveInfinity;
		int result = 0;
		ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
		for (int i = 0; i < text.textInfo.wordCount; i++)
		{
			TMP_WordInfo tMP_WordInfo = text.textInfo.wordInfo[i];
			bool flag = false;
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			for (int j = 0; j < tMP_WordInfo.characterCount; j++)
			{
				int num2 = tMP_WordInfo.firstCharacterIndex + j;
				TMP_CharacterInfo tMP_CharacterInfo = text.textInfo.characterInfo[num2];
				int lineNumber = tMP_CharacterInfo.lineNumber;
				bool isVisible = tMP_CharacterInfo.isVisible;
				if (!flag && isVisible)
				{
					flag = true;
					vector = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.bottomLeft.x, tMP_CharacterInfo.descender, 0f));
					vector2 = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.bottomLeft.x, tMP_CharacterInfo.ascender, 0f));
					if (tMP_WordInfo.characterCount == 1)
					{
						flag = false;
						zero = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.descender, 0f));
						zero2 = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.ascender, 0f));
						if (PointIntersectRectangle(position, vector, vector2, zero2, zero))
						{
							return i;
						}
						float num3 = DistanceToLine(vector, vector2, position);
						float num4 = DistanceToLine(vector2, zero2, position);
						float num5 = DistanceToLine(zero2, zero, position);
						float num6 = DistanceToLine(zero, vector, position);
						float num7 = ((num3 < num4) ? num3 : num4);
						num7 = ((num7 < num5) ? num7 : num5);
						num7 = ((num7 < num6) ? num7 : num6);
						if (num > num7)
						{
							num = num7;
							result = i;
						}
					}
				}
				if (flag && j == tMP_WordInfo.characterCount - 1)
				{
					flag = false;
					zero = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.descender, 0f));
					zero2 = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.ascender, 0f));
					if (PointIntersectRectangle(position, vector, vector2, zero2, zero))
					{
						return i;
					}
					float num8 = DistanceToLine(vector, vector2, position);
					float num9 = DistanceToLine(vector2, zero2, position);
					float num10 = DistanceToLine(zero2, zero, position);
					float num11 = DistanceToLine(zero, vector, position);
					float num12 = ((num8 < num9) ? num8 : num9);
					num12 = ((num12 < num10) ? num12 : num10);
					num12 = ((num12 < num11) ? num12 : num11);
					if (num > num12)
					{
						num = num12;
						result = i;
					}
				}
				else if (flag && lineNumber != text.textInfo.characterInfo[num2 + 1].lineNumber)
				{
					flag = false;
					zero = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.descender, 0f));
					zero2 = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.ascender, 0f));
					if (PointIntersectRectangle(position, vector, vector2, zero2, zero))
					{
						return i;
					}
					float num13 = DistanceToLine(vector, vector2, position);
					float num14 = DistanceToLine(vector2, zero2, position);
					float num15 = DistanceToLine(zero2, zero, position);
					float num16 = DistanceToLine(zero, vector, position);
					float num17 = ((num13 < num14) ? num13 : num14);
					num17 = ((num17 < num15) ? num17 : num15);
					num17 = ((num17 < num16) ? num17 : num16);
					if (num > num17)
					{
						num = num17;
						result = i;
					}
				}
			}
		}
		return result;
	}

	public static int FindIntersectingLine(TMP_Text text, Vector3 position, Camera camera)
	{
		RectTransform rectTransform = text.rectTransform;
		int result = -1;
		ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
		for (int i = 0; i < text.textInfo.lineCount; i++)
		{
			TMP_LineInfo tMP_LineInfo = text.textInfo.lineInfo[i];
			float y = rectTransform.TransformPoint(new Vector3(0f, tMP_LineInfo.ascender, 0f)).y;
			float y2 = rectTransform.TransformPoint(new Vector3(0f, tMP_LineInfo.descender, 0f)).y;
			if (y > position.y && y2 < position.y)
			{
				return i;
			}
		}
		return result;
	}

	public static int FindIntersectingLink(TMP_Text text, Vector3 position, Camera camera)
	{
		Transform transform = text.transform;
		ScreenPointToWorldPointInRectangle(transform, position, camera, out position);
		for (int i = 0; i < text.textInfo.linkCount; i++)
		{
			TMP_LinkInfo tMP_LinkInfo = text.textInfo.linkInfo[i];
			bool flag = false;
			Vector3 a = Vector3.zero;
			Vector3 b = Vector3.zero;
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			for (int j = 0; j < tMP_LinkInfo.linkTextLength; j++)
			{
				int num = tMP_LinkInfo.linkTextfirstCharacterIndex + j;
				TMP_CharacterInfo tMP_CharacterInfo = text.textInfo.characterInfo[num];
				int lineNumber = tMP_CharacterInfo.lineNumber;
				if (text.overflowMode == TextOverflowModes.Page && tMP_CharacterInfo.pageNumber + 1 != text.pageToDisplay)
				{
					continue;
				}
				if (!flag)
				{
					flag = true;
					a = transform.TransformPoint(new Vector3(tMP_CharacterInfo.bottomLeft.x, tMP_CharacterInfo.descender, 0f));
					b = transform.TransformPoint(new Vector3(tMP_CharacterInfo.bottomLeft.x, tMP_CharacterInfo.ascender, 0f));
					if (tMP_LinkInfo.linkTextLength == 1)
					{
						flag = false;
						zero = transform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.descender, 0f));
						zero2 = transform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.ascender, 0f));
						if (PointIntersectRectangle(position, a, b, zero2, zero))
						{
							return i;
						}
					}
				}
				if (flag && j == tMP_LinkInfo.linkTextLength - 1)
				{
					flag = false;
					zero = transform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.descender, 0f));
					zero2 = transform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.ascender, 0f));
					if (PointIntersectRectangle(position, a, b, zero2, zero))
					{
						return i;
					}
				}
				else if (flag && lineNumber != text.textInfo.characterInfo[num + 1].lineNumber)
				{
					flag = false;
					zero = transform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.descender, 0f));
					zero2 = transform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.ascender, 0f));
					if (PointIntersectRectangle(position, a, b, zero2, zero))
					{
						return i;
					}
				}
			}
		}
		return -1;
	}

	public static int FindNearestLink(TMP_Text text, Vector3 position, Camera camera)
	{
		RectTransform rectTransform = text.rectTransform;
		ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
		float num = float.PositiveInfinity;
		int result = 0;
		for (int i = 0; i < text.textInfo.linkCount; i++)
		{
			TMP_LinkInfo tMP_LinkInfo = text.textInfo.linkInfo[i];
			bool flag = false;
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			for (int j = 0; j < tMP_LinkInfo.linkTextLength; j++)
			{
				int num2 = tMP_LinkInfo.linkTextfirstCharacterIndex + j;
				TMP_CharacterInfo tMP_CharacterInfo = text.textInfo.characterInfo[num2];
				int lineNumber = tMP_CharacterInfo.lineNumber;
				if (text.overflowMode == TextOverflowModes.Page && tMP_CharacterInfo.pageNumber + 1 != text.pageToDisplay)
				{
					continue;
				}
				if (!flag)
				{
					flag = true;
					vector = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.bottomLeft.x, tMP_CharacterInfo.descender, 0f));
					vector2 = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.bottomLeft.x, tMP_CharacterInfo.ascender, 0f));
					if (tMP_LinkInfo.linkTextLength == 1)
					{
						flag = false;
						zero = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.descender, 0f));
						zero2 = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.ascender, 0f));
						if (PointIntersectRectangle(position, vector, vector2, zero2, zero))
						{
							return i;
						}
						float num3 = DistanceToLine(vector, vector2, position);
						float num4 = DistanceToLine(vector2, zero2, position);
						float num5 = DistanceToLine(zero2, zero, position);
						float num6 = DistanceToLine(zero, vector, position);
						float num7 = ((num3 < num4) ? num3 : num4);
						num7 = ((num7 < num5) ? num7 : num5);
						num7 = ((num7 < num6) ? num7 : num6);
						if (num > num7)
						{
							num = num7;
							result = i;
						}
					}
				}
				if (flag && j == tMP_LinkInfo.linkTextLength - 1)
				{
					flag = false;
					zero = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.descender, 0f));
					zero2 = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.ascender, 0f));
					if (PointIntersectRectangle(position, vector, vector2, zero2, zero))
					{
						return i;
					}
					float num8 = DistanceToLine(vector, vector2, position);
					float num9 = DistanceToLine(vector2, zero2, position);
					float num10 = DistanceToLine(zero2, zero, position);
					float num11 = DistanceToLine(zero, vector, position);
					float num12 = ((num8 < num9) ? num8 : num9);
					num12 = ((num12 < num10) ? num12 : num10);
					num12 = ((num12 < num11) ? num12 : num11);
					if (num > num12)
					{
						num = num12;
						result = i;
					}
				}
				else if (flag && lineNumber != text.textInfo.characterInfo[num2 + 1].lineNumber)
				{
					flag = false;
					zero = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.descender, 0f));
					zero2 = rectTransform.TransformPoint(new Vector3(tMP_CharacterInfo.topRight.x, tMP_CharacterInfo.ascender, 0f));
					if (PointIntersectRectangle(position, vector, vector2, zero2, zero))
					{
						return i;
					}
					float num13 = DistanceToLine(vector, vector2, position);
					float num14 = DistanceToLine(vector2, zero2, position);
					float num15 = DistanceToLine(zero2, zero, position);
					float num16 = DistanceToLine(zero, vector, position);
					float num17 = ((num13 < num14) ? num13 : num14);
					num17 = ((num17 < num15) ? num17 : num15);
					num17 = ((num17 < num16) ? num17 : num16);
					if (num > num17)
					{
						num = num17;
						result = i;
					}
				}
			}
		}
		return result;
	}

	private static bool PointIntersectRectangle(Vector3 m, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		Vector3 vector = b - a;
		Vector3 rhs = m - a;
		Vector3 vector2 = c - b;
		Vector3 rhs2 = m - b;
		float num = Vector3.Dot(vector, rhs);
		float num2 = Vector3.Dot(vector2, rhs2);
		if (0f <= num && num <= Vector3.Dot(vector, vector) && 0f <= num2)
		{
			return num2 <= Vector3.Dot(vector2, vector2);
		}
		return false;
	}

	public static bool ScreenPointToWorldPointInRectangle(Transform transform, Vector2 screenPoint, Camera cam, out Vector3 worldPoint)
	{
		worldPoint = Vector2.zero;
		Ray ray = RectTransformUtility.ScreenPointToRay(cam, screenPoint);
		if (!new Plane(transform.rotation * Vector3.back, transform.position).Raycast(ray, out var enter))
		{
			return false;
		}
		worldPoint = ray.GetPoint(enter);
		return true;
	}

	private static bool IntersectLinePlane(LineSegment line, Vector3 point, Vector3 normal, out Vector3 intersectingPoint)
	{
		intersectingPoint = Vector3.zero;
		Vector3 vector = line.Point2 - line.Point1;
		Vector3 rhs = line.Point1 - point;
		float num = Vector3.Dot(normal, vector);
		float num2 = 0f - Vector3.Dot(normal, rhs);
		if (Mathf.Abs(num) < Mathf.Epsilon)
		{
			if (num2 == 0f)
			{
				return true;
			}
			return false;
		}
		float num3 = num2 / num;
		if (num3 < 0f || num3 > 1f)
		{
			return false;
		}
		intersectingPoint = line.Point1 + num3 * vector;
		return true;
	}

	public static float DistanceToLine(Vector3 a, Vector3 b, Vector3 point)
	{
		Vector3 vector = b - a;
		Vector3 vector2 = a - point;
		float num = Vector3.Dot(vector, vector2);
		if (num > 0f)
		{
			return Vector3.Dot(vector2, vector2);
		}
		Vector3 vector3 = point - b;
		if (Vector3.Dot(vector, vector3) > 0f)
		{
			return Vector3.Dot(vector3, vector3);
		}
		Vector3 vector4 = vector2 - vector * (num / Vector3.Dot(vector, vector));
		return Vector3.Dot(vector4, vector4);
	}

	public static char ToLowerFast(char c)
	{
		if (c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1)
		{
			return c;
		}
		return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-"[c];
	}

	public static char ToUpperFast(char c)
	{
		if (c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1)
		{
			return c;
		}
		return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[c];
	}

	public static int GetSimpleHashCode(string s)
	{
		int num = 0;
		for (int i = 0; i < s.Length; i++)
		{
			num = ((num << 5) + num) ^ s[i];
		}
		return num;
	}

	public static uint GetSimpleHashCodeLowercase(string s)
	{
		uint num = 5381u;
		for (int i = 0; i < s.Length; i++)
		{
			num = ((num << 5) + num) ^ ToLowerFast(s[i]);
		}
		return num;
	}

	public static int HexToInt(char hex)
	{
		return hex switch
		{
			'0' => 0, 
			'1' => 1, 
			'2' => 2, 
			'3' => 3, 
			'4' => 4, 
			'5' => 5, 
			'6' => 6, 
			'7' => 7, 
			'8' => 8, 
			'9' => 9, 
			'A' => 10, 
			'B' => 11, 
			'C' => 12, 
			'D' => 13, 
			'E' => 14, 
			'F' => 15, 
			'a' => 10, 
			'b' => 11, 
			'c' => 12, 
			'd' => 13, 
			'e' => 14, 
			'f' => 15, 
			_ => 15, 
		};
	}

	public static int StringHexToInt(string s)
	{
		int num = 0;
		for (int i = 0; i < s.Length; i++)
		{
			num += HexToInt(s[i]) * (int)Mathf.Pow(16f, s.Length - 1 - i);
		}
		return num;
	}
}
public class TMP_UpdateManager
{
	private static TMP_UpdateManager s_Instance;

	private readonly List<TMP_Text> m_LayoutRebuildQueue = new List<TMP_Text>();

	private Dictionary<int, int> m_LayoutQueueLookup = new Dictionary<int, int>();

	private readonly List<TMP_Text> m_GraphicRebuildQueue = new List<TMP_Text>();

	private Dictionary<int, int> m_GraphicQueueLookup = new Dictionary<int, int>();

	private readonly List<TMP_Text> m_InternalUpdateQueue = new List<TMP_Text>();

	private Dictionary<int, int> m_InternalUpdateLookup = new Dictionary<int, int>();

	public static TMP_UpdateManager instance
	{
		get
		{
			if (s_Instance == null)
			{
				s_Instance = new TMP_UpdateManager();
			}
			return s_Instance;
		}
	}

	public List<TMP_Text> GetInternalUpdateQueue()
	{
		return m_InternalUpdateQueue;
	}

	protected TMP_UpdateManager()
	{
		Canvas.willRenderCanvases += DoRebuilds;
	}

	public static void RegisterTextObjectForUpdate(TMP_Text textObject)
	{
		instance.InternalRegisterTextObjectForUpdate(textObject);
	}

	private void InternalRegisterTextObjectForUpdate(TMP_Text textObject)
	{
		int instanceID = textObject.GetInstanceID();
		if (!m_InternalUpdateLookup.ContainsKey(instanceID))
		{
			m_InternalUpdateLookup[instanceID] = instanceID;
			m_InternalUpdateQueue.Add(textObject);
		}
	}

	public static void RegisterTextElementForLayoutRebuild(TMP_Text element)
	{
		instance.InternalRegisterTextElementForLayoutRebuild(element);
	}

	private bool InternalRegisterTextElementForLayoutRebuild(TMP_Text element)
	{
		int instanceID = element.GetInstanceID();
		if (m_LayoutQueueLookup.ContainsKey(instanceID))
		{
			return false;
		}
		m_LayoutQueueLookup[instanceID] = instanceID;
		m_LayoutRebuildQueue.Add(element);
		return true;
	}

	public static void RegisterTextElementForGraphicRebuild(TMP_Text element)
	{
		instance.InternalRegisterTextElementForGraphicRebuild(element);
	}

	private bool InternalRegisterTextElementForGraphicRebuild(TMP_Text element)
	{
		int instanceID = element.GetInstanceID();
		if (m_GraphicQueueLookup.ContainsKey(instanceID))
		{
			return false;
		}
		m_GraphicQueueLookup[instanceID] = instanceID;
		m_GraphicRebuildQueue.Add(element);
		return true;
	}

	private void OnBeginFrameRendering(ScriptableRenderContext renderContext, Camera[] cameras)
	{
		DoRebuilds();
	}

	private void OnCameraPreCull(Camera cam)
	{
		DoRebuilds();
	}

	private void DoRebuilds()
	{
		for (int i = 0; i < m_InternalUpdateQueue.Count; i++)
		{
			m_InternalUpdateQueue[i].InternalUpdate();
		}
		for (int j = 0; j < m_LayoutRebuildQueue.Count; j++)
		{
			m_LayoutRebuildQueue[j].Rebuild(CanvasUpdate.Prelayout);
		}
		if (m_LayoutRebuildQueue.Count > 0)
		{
			m_LayoutRebuildQueue.Clear();
			m_LayoutQueueLookup.Clear();
		}
		for (int k = 0; k < m_GraphicRebuildQueue.Count; k++)
		{
			m_GraphicRebuildQueue[k].Rebuild(CanvasUpdate.PreRender);
		}
		if (m_GraphicRebuildQueue.Count > 0)
		{
			m_GraphicRebuildQueue.Clear();
			m_GraphicQueueLookup.Clear();
		}
	}

	public static void UnRegisterTextObjectForUpdate(TMP_Text textObject)
	{
		instance.InternalUnRegisterTextObjectForUpdate(textObject);
	}

	public static void UnRegisterTextElementForRebuild(TMP_Text element)
	{
		instance.InternalUnRegisterTextElementForGraphicRebuild(element);
		instance.InternalUnRegisterTextElementForLayoutRebuild(element);
		instance.InternalUnRegisterTextObjectForUpdate(element);
	}

	private void InternalUnRegisterTextElementForGraphicRebuild(TMP_Text element)
	{
		int instanceID = element.GetInstanceID();
		instance.m_GraphicRebuildQueue.Remove(element);
		m_GraphicQueueLookup.Remove(instanceID);
	}

	private void InternalUnRegisterTextElementForLayoutRebuild(TMP_Text element)
	{
		int instanceID = element.GetInstanceID();
		instance.m_LayoutRebuildQueue.Remove(element);
		m_LayoutQueueLookup.Remove(instanceID);
	}

	private void InternalUnRegisterTextObjectForUpdate(TMP_Text textObject)
	{
		int instanceID = textObject.GetInstanceID();
		instance.m_InternalUpdateQueue.Remove(textObject);
		m_InternalUpdateLookup.Remove(instanceID);
	}
}
public class TMP_UpdateRegistry
{
	private static TMP_UpdateRegistry s_Instance;

	private readonly List<ICanvasElement> m_LayoutRebuildQueue = new List<ICanvasElement>();

	private Dictionary<int, int> m_LayoutQueueLookup = new Dictionary<int, int>();

	private readonly List<ICanvasElement> m_GraphicRebuildQueue = new List<ICanvasElement>();

	private Dictionary<int, int> m_GraphicQueueLookup = new Dictionary<int, int>();

	public static TMP_UpdateRegistry instance
	{
		get
		{
			if (s_Instance == null)
			{
				s_Instance = new TMP_UpdateRegistry();
			}
			return s_Instance;
		}
	}

	protected TMP_UpdateRegistry()
	{
		Canvas.willRenderCanvases += PerformUpdateForCanvasRendererObjects;
	}

	public static void RegisterCanvasElementForLayoutRebuild(ICanvasElement element)
	{
		instance.InternalRegisterCanvasElementForLayoutRebuild(element);
	}

	private bool InternalRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
	{
		int instanceID = (element as UnityEngine.Object).GetInstanceID();
		if (m_LayoutQueueLookup.ContainsKey(instanceID))
		{
			return false;
		}
		m_LayoutQueueLookup[instanceID] = instanceID;
		m_LayoutRebuildQueue.Add(element);
		return true;
	}

	public static void RegisterCanvasElementForGraphicRebuild(ICanvasElement element)
	{
		instance.InternalRegisterCanvasElementForGraphicRebuild(element);
	}

	private bool InternalRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
	{
		int instanceID = (element as UnityEngine.Object).GetInstanceID();
		if (m_GraphicQueueLookup.ContainsKey(instanceID))
		{
			return false;
		}
		m_GraphicQueueLookup[instanceID] = instanceID;
		m_GraphicRebuildQueue.Add(element);
		return true;
	}

	private void PerformUpdateForCanvasRendererObjects()
	{
		for (int i = 0; i < m_LayoutRebuildQueue.Count; i++)
		{
			instance.m_LayoutRebuildQueue[i].Rebuild(CanvasUpdate.Prelayout);
		}
		if (m_LayoutRebuildQueue.Count > 0)
		{
			m_LayoutRebuildQueue.Clear();
			m_LayoutQueueLookup.Clear();
		}
		for (int j = 0; j < m_GraphicRebuildQueue.Count; j++)
		{
			instance.m_GraphicRebuildQueue[j].Rebuild(CanvasUpdate.PreRender);
		}
		if (m_GraphicRebuildQueue.Count > 0)
		{
			m_GraphicRebuildQueue.Clear();
			m_GraphicQueueLookup.Clear();
		}
	}

	private void PerformUpdateForMeshRendererObjects()
	{
		UnityEngine.Debug.Log("Perform update of MeshRenderer objects.");
	}

	public static void UnRegisterCanvasElementForRebuild(ICanvasElement element)
	{
		instance.InternalUnRegisterCanvasElementForLayoutRebuild(element);
		instance.InternalUnRegisterCanvasElementForGraphicRebuild(element);
	}

	private void InternalUnRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
	{
		int instanceID = (element as UnityEngine.Object).GetInstanceID();
		instance.m_LayoutRebuildQueue.Remove(element);
		m_GraphicQueueLookup.Remove(instanceID);
	}

	private void InternalUnRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
	{
		int instanceID = (element as UnityEngine.Object).GetInstanceID();
		instance.m_GraphicRebuildQueue.Remove(element);
		m_LayoutQueueLookup.Remove(instanceID);
	}
}
