using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Coffee.UIParticleInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: InternalsVisibleTo("Coffee.UIParticle.Editor")]
[assembly: InternalsVisibleTo("Coffee.UIParticle.PerformanceDemo")]
[assembly: InternalsVisibleTo("Coffee.UIParticle.Demo")]
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
			FilePathsData = new byte[1815]
			{
				0, 0, 0, 1, 0, 0, 0, 67, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 80, 97, 114, 116, 105, 99,
				108, 101, 69, 102, 102, 101, 99, 116, 70, 111,
				114, 85, 71, 85, 73, 92, 82, 117, 110, 116,
				105, 109, 101, 92, 65, 110, 105, 109, 97, 116,
				97, 98, 108, 101, 80, 114, 111, 112, 101, 114,
				116, 121, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 85, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 80,
				97, 114, 116, 105, 99, 108, 101, 69, 102, 102,
				101, 99, 116, 70, 111, 114, 85, 71, 85, 73,
				92, 82, 117, 110, 116, 105, 109, 101, 92, 73,
				110, 116, 101, 114, 110, 97, 108, 92, 69, 120,
				116, 101, 110, 115, 105, 111, 110, 115, 92, 67,
				97, 110, 118, 97, 115, 69, 120, 116, 101, 110,
				115, 105, 111, 110, 115, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 86, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 80, 97, 114, 116, 105, 99, 108, 101,
				69, 102, 102, 101, 99, 116, 70, 111, 114, 85,
				71, 85, 73, 92, 82, 117, 110, 116, 105, 109,
				101, 92, 73, 110, 116, 101, 114, 110, 97, 108,
				92, 69, 120, 116, 101, 110, 115, 105, 111, 110,
				115, 92, 67, 111, 108, 111, 114, 51, 50, 69,
				120, 116, 101, 110, 115, 105, 111, 110, 115, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 88,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 80, 97, 114, 116,
				105, 99, 108, 101, 69, 102, 102, 101, 99, 116,
				70, 111, 114, 85, 71, 85, 73, 92, 82, 117,
				110, 116, 105, 109, 101, 92, 73, 110, 116, 101,
				114, 110, 97, 108, 92, 69, 120, 116, 101, 110,
				115, 105, 111, 110, 115, 92, 67, 111, 109, 112,
				111, 110, 101, 110, 116, 69, 120, 116, 101, 110,
				115, 105, 111, 110, 115, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 85, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 80, 97, 114, 116, 105, 99, 108, 101,
				69, 102, 102, 101, 99, 116, 70, 111, 114, 85,
				71, 85, 73, 92, 82, 117, 110, 116, 105, 109,
				101, 92, 73, 110, 116, 101, 114, 110, 97, 108,
				92, 69, 120, 116, 101, 110, 115, 105, 111, 110,
				115, 92, 83, 112, 114, 105, 116, 101, 69, 120,
				116, 101, 110, 115, 105, 111, 110, 115, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 86, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 80, 97, 114, 116, 105,
				99, 108, 101, 69, 102, 102, 101, 99, 116, 70,
				111, 114, 85, 71, 85, 73, 92, 82, 117, 110,
				116, 105, 109, 101, 92, 73, 110, 116, 101, 114,
				110, 97, 108, 92, 69, 120, 116, 101, 110, 115,
				105, 111, 110, 115, 92, 86, 101, 99, 116, 111,
				114, 51, 69, 120, 116, 101, 110, 115, 105, 111,
				110, 115, 46, 99, 115, 0, 0, 0, 2, 0,
				0, 0, 98, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 80,
				97, 114, 116, 105, 99, 108, 101, 69, 102, 102,
				101, 99, 116, 70, 111, 114, 85, 71, 85, 73,
				92, 82, 117, 110, 116, 105, 109, 101, 92, 73,
				110, 116, 101, 114, 110, 97, 108, 92, 80, 114,
				111, 106, 101, 99, 116, 83, 101, 116, 116, 105,
				110, 103, 115, 92, 80, 114, 101, 108, 111, 97,
				100, 101, 100, 80, 114, 111, 106, 101, 99, 116,
				83, 101, 116, 116, 105, 110, 103, 115, 46, 99,
				115, 0, 0, 0, 2, 0, 0, 0, 78, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 80, 97, 114, 116, 105,
				99, 108, 101, 69, 102, 102, 101, 99, 116, 70,
				111, 114, 85, 71, 85, 73, 92, 82, 117, 110,
				116, 105, 109, 101, 92, 73, 110, 116, 101, 114,
				110, 97, 108, 92, 85, 116, 105, 108, 105, 116,
				105, 101, 115, 92, 70, 97, 115, 116, 65, 99,
				116, 105, 111, 110, 46, 99, 115, 0, 0, 0,
				3, 0, 0, 0, 78, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 80, 97, 114, 116, 105, 99, 108, 101, 69,
				102, 102, 101, 99, 116, 70, 111, 114, 85, 71,
				85, 73, 92, 82, 117, 110, 116, 105, 109, 101,
				92, 73, 110, 116, 101, 114, 110, 97, 108, 92,
				85, 116, 105, 108, 105, 116, 105, 101, 115, 92,
				70, 114, 97, 109, 101, 67, 97, 99, 104, 101,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				75, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 80, 97, 114,
				116, 105, 99, 108, 101, 69, 102, 102, 101, 99,
				116, 70, 111, 114, 85, 71, 85, 73, 92, 82,
				117, 110, 116, 105, 109, 101, 92, 73, 110, 116,
				101, 114, 110, 97, 108, 92, 85, 116, 105, 108,
				105, 116, 105, 101, 115, 92, 76, 111, 103, 103,
				105, 110, 103, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 86, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				80, 97, 114, 116, 105, 99, 108, 101, 69, 102,
				102, 101, 99, 116, 70, 111, 114, 85, 71, 85,
				73, 92, 82, 117, 110, 116, 105, 109, 101, 92,
				73, 110, 116, 101, 114, 110, 97, 108, 92, 85,
				116, 105, 108, 105, 116, 105, 101, 115, 92, 77,
				97, 116, 101, 114, 105, 97, 108, 82, 101, 112,
				111, 115, 105, 116, 111, 114, 121, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 72, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 80, 97, 114, 116, 105, 99,
				108, 101, 69, 102, 102, 101, 99, 116, 70, 111,
				114, 85, 71, 85, 73, 92, 82, 117, 110, 116,
				105, 109, 101, 92, 73, 110, 116, 101, 114, 110,
				97, 108, 92, 85, 116, 105, 108, 105, 116, 105,
				101, 115, 92, 77, 105, 115, 99, 46, 99, 115,
				0, 0, 0, 2, 0, 0, 0, 78, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 80, 97, 114, 116, 105, 99,
				108, 101, 69, 102, 102, 101, 99, 116, 70, 111,
				114, 85, 71, 85, 73, 92, 82, 117, 110, 116,
				105, 109, 101, 92, 73, 110, 116, 101, 114, 110,
				97, 108, 92, 85, 116, 105, 108, 105, 116, 105,
				101, 115, 92, 79, 98, 106, 101, 99, 116, 80,
				111, 111, 108, 46, 99, 115, 0, 0, 0, 2,
				0, 0, 0, 84, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				80, 97, 114, 116, 105, 99, 108, 101, 69, 102,
				102, 101, 99, 116, 70, 111, 114, 85, 71, 85,
				73, 92, 82, 117, 110, 116, 105, 109, 101, 92,
				73, 110, 116, 101, 114, 110, 97, 108, 92, 85,
				116, 105, 108, 105, 116, 105, 101, 115, 92, 79,
				98, 106, 101, 99, 116, 82, 101, 112, 111, 115,
				105, 116, 111, 114, 121, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 84, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 80, 97, 114, 116, 105, 99, 108, 101,
				69, 102, 102, 101, 99, 116, 70, 111, 114, 85,
				71, 85, 73, 92, 82, 117, 110, 116, 105, 109,
				101, 92, 73, 110, 116, 101, 114, 110, 97, 108,
				92, 85, 116, 105, 108, 105, 116, 105, 101, 115,
				92, 85, 73, 69, 120, 116, 114, 97, 67, 97,
				108, 108, 98, 97, 99, 107, 115, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 59, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 80, 97, 114, 116, 105, 99,
				108, 101, 69, 102, 102, 101, 99, 116, 70, 111,
				114, 85, 71, 85, 73, 92, 82, 117, 110, 116,
				105, 109, 101, 92, 85, 73, 80, 97, 114, 116,
				105, 99, 108, 101, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 68, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 80, 97, 114, 116, 105, 99, 108, 101, 69,
				102, 102, 101, 99, 116, 70, 111, 114, 85, 71,
				85, 73, 92, 82, 117, 110, 116, 105, 109, 101,
				92, 85, 73, 80, 97, 114, 116, 105, 99, 108,
				101, 65, 116, 116, 114, 97, 99, 116, 111, 114,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				74, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 80, 97, 114,
				116, 105, 99, 108, 101, 69, 102, 102, 101, 99,
				116, 70, 111, 114, 85, 71, 85, 73, 92, 82,
				117, 110, 116, 105, 109, 101, 92, 85, 73, 80,
				97, 114, 116, 105, 99, 108, 101, 80, 114, 111,
				106, 101, 99, 116, 83, 101, 116, 116, 105, 110,
				103, 115, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 67, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 80,
				97, 114, 116, 105, 99, 108, 101, 69, 102, 102,
				101, 99, 116, 70, 111, 114, 85, 71, 85, 73,
				92, 82, 117, 110, 116, 105, 109, 101, 92, 85,
				73, 80, 97, 114, 116, 105, 99, 108, 101, 82,
				101, 110, 100, 101, 114, 101, 114, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 66, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 80, 97, 114, 116, 105, 99,
				108, 101, 69, 102, 102, 101, 99, 116, 70, 111,
				114, 85, 71, 85, 73, 92, 82, 117, 110, 116,
				105, 109, 101, 92, 85, 73, 80, 97, 114, 116,
				105, 99, 108, 101, 85, 112, 100, 97, 116, 101,
				114, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 83, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 80, 97,
				114, 116, 105, 99, 108, 101, 69, 102, 102, 101,
				99, 116, 70, 111, 114, 85, 71, 85, 73, 92,
				82, 117, 110, 116, 105, 109, 101, 92, 85, 116,
				105, 108, 105, 116, 105, 101, 115, 92, 80, 97,
				114, 116, 105, 99, 108, 101, 83, 121, 115, 116,
				101, 109, 69, 120, 116, 101, 110, 115, 105, 111,
				110, 115, 46, 99, 115
			},
			TypesData = new byte[1268]
			{
				0, 0, 0, 0, 38, 67, 111, 102, 102, 101,
				101, 46, 85, 73, 69, 120, 116, 101, 110, 115,
				105, 111, 110, 115, 124, 65, 110, 105, 109, 97,
				116, 97, 98, 108, 101, 80, 114, 111, 112, 101,
				114, 116, 121, 0, 0, 0, 0, 42, 67, 111,
				102, 102, 101, 101, 46, 85, 73, 80, 97, 114,
				116, 105, 99, 108, 101, 73, 110, 116, 101, 114,
				110, 97, 108, 124, 67, 97, 110, 118, 97, 115,
				69, 120, 116, 101, 110, 115, 105, 111, 110, 115,
				0, 0, 0, 0, 43, 67, 111, 102, 102, 101,
				101, 46, 85, 73, 80, 97, 114, 116, 105, 99,
				108, 101, 73, 110, 116, 101, 114, 110, 97, 108,
				124, 67, 111, 108, 111, 114, 51, 50, 69, 120,
				116, 101, 110, 115, 105, 111, 110, 115, 0, 0,
				0, 0, 45, 67, 111, 102, 102, 101, 101, 46,
				85, 73, 80, 97, 114, 116, 105, 99, 108, 101,
				73, 110, 116, 101, 114, 110, 97, 108, 124, 67,
				111, 109, 112, 111, 110, 101, 110, 116, 69, 120,
				116, 101, 110, 115, 105, 111, 110, 115, 0, 0,
				0, 0, 42, 67, 111, 102, 102, 101, 101, 46,
				85, 73, 80, 97, 114, 116, 105, 99, 108, 101,
				73, 110, 116, 101, 114, 110, 97, 108, 124, 83,
				112, 114, 105, 116, 101, 69, 120, 116, 101, 110,
				115, 105, 111, 110, 115, 0, 0, 0, 0, 43,
				67, 111, 102, 102, 101, 101, 46, 85, 73, 80,
				97, 114, 116, 105, 99, 108, 101, 73, 110, 116,
				101, 114, 110, 97, 108, 124, 86, 101, 99, 116,
				111, 114, 51, 69, 120, 116, 101, 110, 115, 105,
				111, 110, 115, 1, 0, 0, 0, 50, 67, 111,
				102, 102, 101, 101, 46, 85, 73, 80, 97, 114,
				116, 105, 99, 108, 101, 73, 110, 116, 101, 114,
				110, 97, 108, 124, 80, 114, 101, 108, 111, 97,
				100, 101, 100, 80, 114, 111, 106, 101, 99, 116,
				83, 101, 116, 116, 105, 110, 103, 115, 1, 0,
				0, 0, 50, 67, 111, 102, 102, 101, 101, 46,
				85, 73, 80, 97, 114, 116, 105, 99, 108, 101,
				73, 110, 116, 101, 114, 110, 97, 108, 124, 80,
				114, 101, 108, 111, 97, 100, 101, 100, 80, 114,
				111, 106, 101, 99, 116, 83, 101, 116, 116, 105,
				110, 103, 115, 0, 0, 0, 0, 40, 67, 111,
				102, 102, 101, 101, 46, 85, 73, 80, 97, 114,
				116, 105, 99, 108, 101, 73, 110, 116, 101, 114,
				110, 97, 108, 124, 70, 97, 115, 116, 65, 99,
				116, 105, 111, 110, 66, 97, 115, 101, 0, 0,
				0, 0, 36, 67, 111, 102, 102, 101, 101, 46,
				85, 73, 80, 97, 114, 116, 105, 99, 108, 101,
				73, 110, 116, 101, 114, 110, 97, 108, 124, 70,
				97, 115, 116, 65, 99, 116, 105, 111, 110, 0,
				0, 0, 0, 36, 67, 111, 102, 102, 101, 101,
				46, 85, 73, 80, 97, 114, 116, 105, 99, 108,
				101, 73, 110, 116, 101, 114, 110, 97, 108, 124,
				70, 114, 97, 109, 101, 67, 97, 99, 104, 101,
				0, 0, 0, 0, 48, 67, 111, 102, 102, 101,
				101, 46, 85, 73, 80, 97, 114, 116, 105, 99,
				108, 101, 73, 110, 116, 101, 114, 110, 97, 108,
				46, 70, 114, 97, 109, 101, 67, 97, 99, 104,
				101, 124, 73, 70, 114, 97, 109, 101, 67, 97,
				99, 104, 101, 0, 0, 0, 0, 56, 67, 111,
				102, 102, 101, 101, 46, 85, 73, 80, 97, 114,
				116, 105, 99, 108, 101, 73, 110, 116, 101, 114,
				110, 97, 108, 46, 70, 114, 97, 109, 101, 67,
				97, 99, 104, 101, 124, 70, 114, 97, 109, 101,
				67, 97, 99, 104, 101, 67, 111, 110, 116, 97,
				105, 110, 101, 114, 0, 0, 0, 0, 33, 67,
				111, 102, 102, 101, 101, 46, 85, 73, 80, 97,
				114, 116, 105, 99, 108, 101, 73, 110, 116, 101,
				114, 110, 97, 108, 124, 76, 111, 103, 103, 105,
				110, 103, 0, 0, 0, 0, 44, 67, 111, 102,
				102, 101, 101, 46, 85, 73, 80, 97, 114, 116,
				105, 99, 108, 101, 73, 110, 116, 101, 114, 110,
				97, 108, 124, 77, 97, 116, 101, 114, 105, 97,
				108, 82, 101, 112, 111, 115, 105, 116, 111, 114,
				121, 0, 0, 0, 0, 30, 67, 111, 102, 102,
				101, 101, 46, 85, 73, 80, 97, 114, 116, 105,
				99, 108, 101, 73, 110, 116, 101, 114, 110, 97,
				108, 124, 77, 105, 115, 99, 0, 0, 0, 0,
				44, 67, 111, 102, 102, 101, 101, 46, 85, 73,
				80, 97, 114, 116, 105, 99, 108, 101, 73, 110,
				116, 101, 114, 110, 97, 108, 124, 73, 110, 116,
				101, 114, 110, 97, 108, 79, 98, 106, 101, 99,
				116, 80, 111, 111, 108, 0, 0, 0, 0, 42,
				67, 111, 102, 102, 101, 101, 46, 85, 73, 80,
				97, 114, 116, 105, 99, 108, 101, 73, 110, 116,
				101, 114, 110, 97, 108, 124, 73, 110, 116, 101,
				114, 110, 97, 108, 76, 105, 115, 116, 80, 111,
				111, 108, 0, 0, 0, 0, 42, 67, 111, 102,
				102, 101, 101, 46, 85, 73, 80, 97, 114, 116,
				105, 99, 108, 101, 73, 110, 116, 101, 114, 110,
				97, 108, 124, 79, 98, 106, 101, 99, 116, 82,
				101, 112, 111, 115, 105, 116, 111, 114, 121, 0,
				0, 0, 0, 48, 67, 111, 102, 102, 101, 101,
				46, 85, 73, 80, 97, 114, 116, 105, 99, 108,
				101, 73, 110, 116, 101, 114, 110, 97, 108, 46,
				79, 98, 106, 101, 99, 116, 82, 101, 112, 111,
				115, 105, 116, 111, 114, 121, 124, 69, 110, 116,
				114, 121, 0, 0, 0, 0, 42, 67, 111, 102,
				102, 101, 101, 46, 85, 73, 80, 97, 114, 116,
				105, 99, 108, 101, 73, 110, 116, 101, 114, 110,
				97, 108, 124, 85, 73, 69, 120, 116, 114, 97,
				67, 97, 108, 108, 98, 97, 99, 107, 115, 0,
				0, 0, 0, 30, 67, 111, 102, 102, 101, 101,
				46, 85, 73, 69, 120, 116, 101, 110, 115, 105,
				111, 110, 115, 124, 85, 73, 80, 97, 114, 116,
				105, 99, 108, 101, 0, 0, 0, 0, 39, 67,
				111, 102, 102, 101, 101, 46, 85, 73, 69, 120,
				116, 101, 110, 115, 105, 111, 110, 115, 124, 85,
				73, 80, 97, 114, 116, 105, 99, 108, 101, 65,
				116, 116, 114, 97, 99, 116, 111, 114, 0, 0,
				0, 0, 45, 67, 111, 102, 102, 101, 101, 46,
				85, 73, 69, 120, 116, 101, 110, 115, 105, 111,
				110, 115, 124, 85, 73, 80, 97, 114, 116, 105,
				99, 108, 101, 80, 114, 111, 106, 101, 99, 116,
				83, 101, 116, 116, 105, 110, 103, 115, 0, 0,
				0, 0, 38, 67, 111, 102, 102, 101, 101, 46,
				85, 73, 69, 120, 116, 101, 110, 115, 105, 111,
				110, 115, 124, 85, 73, 80, 97, 114, 116, 105,
				99, 108, 101, 82, 101, 110, 100, 101, 114, 101,
				114, 0, 0, 0, 0, 37, 67, 111, 102, 102,
				101, 101, 46, 85, 73, 69, 120, 116, 101, 110,
				115, 105, 111, 110, 115, 124, 85, 73, 80, 97,
				114, 116, 105, 99, 108, 101, 85, 112, 100, 97,
				116, 101, 114, 0, 0, 0, 0, 50, 67, 111,
				102, 102, 101, 101, 46, 85, 73, 80, 97, 114,
				116, 105, 99, 108, 101, 73, 110, 116, 101, 114,
				110, 97, 108, 124, 80, 97, 114, 116, 105, 99,
				108, 101, 83, 121, 115, 116, 101, 109, 69, 120,
				116, 101, 110, 115, 105, 111, 110, 115
			},
			TotalFiles = 21,
			TotalTypes = 27,
			IsEditorOnly = false
		};
	}
}
namespace Coffee.UIParticleInternal
{
	internal static class CanvasExtensions
	{
		public static bool ShouldGammaToLinearInShader(this Canvas canvas)
		{
			if (QualitySettings.activeColorSpace == ColorSpace.Linear)
			{
				return canvas.vertexColorAlwaysGammaSpace;
			}
			return false;
		}

		public static bool ShouldGammaToLinearInMesh(this Canvas canvas)
		{
			if (QualitySettings.activeColorSpace == ColorSpace.Linear)
			{
				return !canvas.vertexColorAlwaysGammaSpace;
			}
			return false;
		}

		public static bool IsStereoCanvas(this Canvas canvas)
		{
			return false;
		}

		public static void GetViewProjectionMatrix(this Canvas canvas, out Matrix4x4 vpMatrix)
		{
			canvas.GetViewProjectionMatrix(Camera.MonoOrStereoscopicEye.Mono, out vpMatrix);
		}

		public static void GetViewProjectionMatrix(this Canvas canvas, Camera.MonoOrStereoscopicEye eye, out Matrix4x4 vpMatrix)
		{
			if (!FrameCache.TryGet<Matrix4x4>(canvas, "GetViewProjectionMatrix", out vpMatrix))
			{
				canvas.GetViewProjectionMatrix(eye, out var vMatrix, out var pMatrix);
				vpMatrix = vMatrix * pMatrix;
				FrameCache.Set(canvas, "GetViewProjectionMatrix", vpMatrix);
			}
		}

		public static void GetViewProjectionMatrix(this Canvas canvas, out Matrix4x4 vMatrix, out Matrix4x4 pMatrix)
		{
			canvas.GetViewProjectionMatrix(Camera.MonoOrStereoscopicEye.Mono, out vMatrix, out pMatrix);
		}

		public static void GetViewProjectionMatrix(this Canvas canvas, Camera.MonoOrStereoscopicEye eye, out Matrix4x4 vMatrix, out Matrix4x4 pMatrix)
		{
			if (FrameCache.TryGet<Matrix4x4>(canvas, "GetViewMatrix", (int)eye, out vMatrix) && FrameCache.TryGet<Matrix4x4>(canvas, "GetProjectionMatrix", (int)eye, out pMatrix))
			{
				return;
			}
			Canvas rootCanvas = canvas.rootCanvas;
			Camera worldCamera = rootCanvas.worldCamera;
			if ((bool)rootCanvas && rootCanvas.renderMode != RenderMode.ScreenSpaceOverlay && (bool)worldCamera)
			{
				if (eye == Camera.MonoOrStereoscopicEye.Mono)
				{
					vMatrix = worldCamera.worldToCameraMatrix;
					pMatrix = GL.GetGPUProjectionMatrix(worldCamera.projectionMatrix, renderIntoTexture: false);
				}
				else
				{
					pMatrix = worldCamera.GetStereoProjectionMatrix((Camera.StereoscopicEye)eye);
					vMatrix = worldCamera.GetStereoViewMatrix((Camera.StereoscopicEye)eye);
					pMatrix = GL.GetGPUProjectionMatrix(pMatrix, renderIntoTexture: false);
				}
			}
			else
			{
				Vector3 position = rootCanvas.transform.position;
				vMatrix = Matrix4x4.TRS(new Vector3(0f - position.x, 0f - position.y, -1000f), Quaternion.identity, new Vector3(1f, 1f, -1f));
				pMatrix = Matrix4x4.TRS(new Vector3(0f, 0f, -1f), Quaternion.identity, new Vector3(1f / position.x, 1f / position.y, -0.0002f));
			}
			FrameCache.Set(canvas, "GetViewMatrix", (int)eye, vMatrix);
			FrameCache.Set(canvas, "GetProjectionMatrix", (int)eye, pMatrix);
		}
	}
	internal static class Color32Extensions
	{
		private static readonly List<Color32> s_Colors = new List<Color32>();

		private static byte[] s_LinearToGammaLut;

		private static byte[] s_GammaToLinearLut;

		public static byte LinearToGamma(this byte self)
		{
			if (s_LinearToGammaLut == null)
			{
				s_LinearToGammaLut = new byte[256];
				for (int i = 0; i < 256; i++)
				{
					s_LinearToGammaLut[i] = (byte)(Mathf.LinearToGammaSpace((float)i / 255f) * 255f);
				}
			}
			return s_LinearToGammaLut[self];
		}

		public static byte GammaToLinear(this byte self)
		{
			if (s_GammaToLinearLut == null)
			{
				s_GammaToLinearLut = new byte[256];
				for (int i = 0; i < 256; i++)
				{
					s_GammaToLinearLut[i] = (byte)(Mathf.GammaToLinearSpace((float)i / 255f) * 255f);
				}
			}
			return s_GammaToLinearLut[self];
		}

		public static void LinearToGamma(this Mesh self)
		{
			self.GetColors(s_Colors);
			int count = s_Colors.Count;
			for (int i = 0; i < count; i++)
			{
				Color32 value = s_Colors[i];
				value.r = value.r.LinearToGamma();
				value.g = value.g.LinearToGamma();
				value.b = value.b.LinearToGamma();
				s_Colors[i] = value;
			}
			self.SetColors(s_Colors);
		}

		public static void GammaToLinear(this Mesh self)
		{
			self.GetColors(s_Colors);
			int count = s_Colors.Count;
			for (int i = 0; i < count; i++)
			{
				Color32 value = s_Colors[i];
				value.r = value.r.GammaToLinear();
				value.g = value.g.GammaToLinear();
				value.b = value.b.GammaToLinear();
				s_Colors[i] = value;
			}
			self.SetColors(s_Colors);
		}
	}
	internal static class ComponentExtensions
	{
		public static T[] GetComponentsInChildren<T>(this UnityEngine.Component self, int depth) where T : UnityEngine.Component
		{
			List<T> toRelease = InternalListPool<T>.Rent();
			self.GetComponentsInChildren_Internal(toRelease, depth);
			T[] result = toRelease.ToArray();
			InternalListPool<T>.Return(ref toRelease);
			return result;
		}

		public static void GetComponentsInChildren<T>(this UnityEngine.Component self, List<T> results, int depth) where T : UnityEngine.Component
		{
			results.Clear();
			self.GetComponentsInChildren_Internal(results, depth);
		}

		private static void GetComponentsInChildren_Internal<T>(this UnityEngine.Component self, List<T> results, int depth) where T : UnityEngine.Component
		{
			if (!self || results == null || depth < 0)
			{
				return;
			}
			Transform transform = self.transform;
			if (transform.TryGetComponent<T>(out var component))
			{
				results.Add(component);
			}
			if (depth - 1 >= 0)
			{
				int childCount = transform.childCount;
				for (int i = 0; i < childCount; i++)
				{
					transform.GetChild(i).GetComponentsInChildren_Internal(results, depth - 1);
				}
			}
		}

		public static T GetOrAddComponent<T>(this UnityEngine.Component self) where T : UnityEngine.Component
		{
			if (!self)
			{
				return null;
			}
			if (!self.TryGetComponent<T>(out var component))
			{
				return self.gameObject.AddComponent<T>();
			}
			return component;
		}

		public static T GetRootComponent<T>(this UnityEngine.Component self) where T : UnityEngine.Component
		{
			T result = null;
			Transform transform = self.transform;
			while ((bool)transform)
			{
				if (transform.TryGetComponent<T>(out var component))
				{
					result = component;
				}
				transform = transform.parent;
			}
			return result;
		}

		public static T GetComponentInParent<T>(this UnityEngine.Component self, bool includeSelf, Transform stopAfter, Predicate<T> valid) where T : UnityEngine.Component
		{
			Transform transform = (includeSelf ? self.transform : self.transform.parent);
			while ((bool)transform)
			{
				if (transform.TryGetComponent<T>(out var component) && valid(component))
				{
					return component;
				}
				if (transform == stopAfter)
				{
					return null;
				}
				transform = transform.parent;
			}
			return null;
		}

		public static void AddComponentOnChildren<T>(this UnityEngine.Component self, HideFlags hideFlags, bool includeSelf) where T : UnityEngine.Component
		{
			if (self == null)
			{
				return;
			}
			if (includeSelf && !self.TryGetComponent<T>(out var component))
			{
				self.gameObject.AddComponent<T>().hideFlags = hideFlags;
			}
			int childCount = self.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = self.transform.GetChild(i);
				if (!child.TryGetComponent<T>(out component))
				{
					child.gameObject.AddComponent<T>().hideFlags = hideFlags;
				}
			}
		}

		public static void AddComponentOnChildren<T>(this UnityEngine.Component self, bool includeSelf) where T : UnityEngine.Component
		{
			if (self == null)
			{
				return;
			}
			if (includeSelf && !self.TryGetComponent<T>(out var component))
			{
				self.gameObject.AddComponent<T>();
			}
			int childCount = self.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = self.transform.GetChild(i);
				if (!child.TryGetComponent<T>(out component))
				{
					child.gameObject.AddComponent<T>();
				}
			}
		}
	}
	internal static class SpriteExtensions
	{
		internal static Texture2D GetActualTexture(this Sprite self)
		{
			if (!self)
			{
				return null;
			}
			return self.texture;
		}
	}
	internal static class Vector3Extensions
	{
		public static Vector3 Inverse(this Vector3 self)
		{
			self.x = (Mathf.Approximately(self.x, 0f) ? 1f : (1f / self.x));
			self.y = (Mathf.Approximately(self.y, 0f) ? 1f : (1f / self.y));
			self.z = (Mathf.Approximately(self.z, 0f) ? 1f : (1f / self.z));
			return self;
		}

		public static Vector3 GetScaled(this Vector3 self, Vector3 other1)
		{
			self.Scale(other1);
			return self;
		}

		public static Vector3 GetScaled(this Vector3 self, Vector3 other1, Vector3 other2)
		{
			self.Scale(other1);
			self.Scale(other2);
			return self;
		}

		public static Vector3 GetScaled(this Vector3 self, Vector3 other1, Vector3 other2, Vector3 other3)
		{
			self.Scale(other1);
			self.Scale(other2);
			self.Scale(other3);
			return self;
		}

		public static bool IsVisible(this Vector3 self)
		{
			return 0f < Mathf.Abs(self.x * self.y * self.z);
		}

		public static bool IsVisible2D(this Vector3 self)
		{
			return 0f < Mathf.Abs(self.x * self.y);
		}
	}
	public abstract class PreloadedProjectSettings : ScriptableObject
	{
	}
	public abstract class PreloadedProjectSettings<T> : PreloadedProjectSettings where T : PreloadedProjectSettings<T>
	{
		private static T s_Instance;

		public static T instance
		{
			get
			{
				if (!s_Instance)
				{
					return s_Instance = ScriptableObject.CreateInstance<T>();
				}
				return s_Instance;
			}
		}

		protected virtual void OnEnable()
		{
			if (!s_Instance)
			{
				s_Instance = this as T;
			}
		}

		protected virtual void OnDisable()
		{
			if (!(s_Instance != this))
			{
				s_Instance = null;
			}
		}
	}
	internal class FastActionBase<T>
	{
		private static readonly InternalObjectPool<LinkedListNode<T>> s_NodePool = new InternalObjectPool<LinkedListNode<T>>(() => new LinkedListNode<T>(default(T)), (LinkedListNode<T> _) => true, delegate(LinkedListNode<T> x)
		{
			x.Value = default(T);
		});

		private readonly LinkedList<T> _delegates = new LinkedList<T>();

		public void Add(T rhs)
		{
			if (rhs != null)
			{
				LinkedListNode<T> linkedListNode = s_NodePool.Rent();
				linkedListNode.Value = rhs;
				_delegates.AddLast(linkedListNode);
			}
		}

		public void Remove(T rhs)
		{
			if (rhs != null)
			{
				LinkedListNode<T> instance = _delegates.Find(rhs);
				if (instance != null)
				{
					_delegates.Remove(instance);
					s_NodePool.Return(ref instance);
				}
			}
		}

		protected void Invoke(Action<T> callback)
		{
			for (LinkedListNode<T> linkedListNode = _delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				try
				{
					callback(linkedListNode.Value);
				}
				catch (Exception exception)
				{
					UnityEngine.Debug.LogException(exception);
				}
			}
		}

		public void Clear()
		{
			_delegates.Clear();
		}
	}
	internal class FastAction : FastActionBase<Action>
	{
		public void Invoke()
		{
			Invoke(delegate(Action action)
			{
				action();
			});
		}
	}
	internal static class FrameCache
	{
		private interface IFrameCache
		{
			void Clear();
		}

		private class FrameCacheContainer<T> : IFrameCache
		{
			private readonly Dictionary<(int, int), T> _caches = new Dictionary<(int, int), T>();

			public void Clear()
			{
				_caches.Clear();
			}

			public bool TryGet((int, int) key, out T result)
			{
				return _caches.TryGetValue(key, out result);
			}

			public void Set((int, int) key, T result)
			{
				_caches[key] = result;
			}
		}

		private static readonly Dictionary<Type, IFrameCache> s_Caches;

		static FrameCache()
		{
			s_Caches = new Dictionary<Type, IFrameCache>();
			s_Caches.Clear();
			UIExtraCallbacks.onLateAfterCanvasRebuild += ClearAllCache;
		}

		public static bool TryGet<T>(object key1, string key2, out T result)
		{
			return GetFrameCache<T>().TryGet((key1.GetHashCode(), key2.GetHashCode()), out result);
		}

		public static bool TryGet<T>(object key1, string key2, int key3, out T result)
		{
			return GetFrameCache<T>().TryGet((key1.GetHashCode(), key2.GetHashCode() + key3), out result);
		}

		public static void Set<T>(object key1, string key2, T result)
		{
			GetFrameCache<T>().Set((key1.GetHashCode(), key2.GetHashCode()), result);
		}

		public static void Set<T>(object key1, string key2, int key3, T result)
		{
			GetFrameCache<T>().Set((key1.GetHashCode(), key2.GetHashCode() + key3), result);
		}

		private static void ClearAllCache()
		{
			foreach (IFrameCache value in s_Caches.Values)
			{
				value.Clear();
			}
		}

		private static FrameCacheContainer<T> GetFrameCache<T>()
		{
			Type typeFromHandle = typeof(T);
			if (s_Caches.TryGetValue(typeFromHandle, out var value))
			{
				return value as FrameCacheContainer<T>;
			}
			value = new FrameCacheContainer<T>();
			s_Caches.Add(typeFromHandle, value);
			return (FrameCacheContainer<T>)value;
		}
	}
	internal static class Logging
	{
		private const string k_DisableSymbol = "DISABLE_COFFEE_LOGGER";

		[Conditional("DISABLE_COFFEE_LOGGER")]
		private static void Log_Internal(LogType type, object tag, object message, UnityEngine.Object context)
		{
		}

		[Conditional("DISABLE_COFFEE_LOGGER")]
		public static void LogIf(bool enable, object tag, object message, UnityEngine.Object context = null)
		{
		}

		[Conditional("DISABLE_COFFEE_LOGGER")]
		public static void Log(object tag, object message, UnityEngine.Object context = null)
		{
		}

		[Conditional("DISABLE_COFFEE_LOGGER")]
		public static void LogWarning(object tag, object message, UnityEngine.Object context = null)
		{
		}

		public static void LogError(object tag, object message, UnityEngine.Object context = null)
		{
			UnityEngine.Debug.LogError($"{tag}: {message}", context);
		}

		[Conditional("DISABLE_COFFEE_LOGGER")]
		public static void LogMulticast(Type type, string fieldName, object instance = null, string message = null)
		{
		}

		[Conditional("DISABLE_COFFEE_LOGGER")]
		private static void AppendTag(StringBuilder sb, object tag)
		{
		}

		[Conditional("DISABLE_COFFEE_LOGGER")]
		private static void AppendType(StringBuilder sb, Type type)
		{
		}

		[Conditional("DISABLE_COFFEE_LOGGER")]
		private static void AppendReadableCode(StringBuilder sb, object tag)
		{
		}
	}
	internal static class MaterialRepository
	{
		private static readonly ObjectRepository<Material> s_Repository = new ObjectRepository<Material>();

		public static int count => s_Repository.count;

		public static bool Valid(Hash128 hash, Material material)
		{
			return s_Repository.Valid(hash, material);
		}

		public static void Get(Hash128 hash, ref Material material, Func<Material> onCreate)
		{
			s_Repository.Get(hash, ref material, onCreate);
		}

		public static void Get(Hash128 hash, ref Material material, string shaderName)
		{
			s_Repository.Get(hash, ref material, (string x) => new Material(Shader.Find(x))
			{
				hideFlags = (HideFlags.DontSave | HideFlags.NotEditable)
			}, shaderName);
		}

		public static void Get(Hash128 hash, ref Material material, string shaderName, string[] keywords)
		{
			s_Repository.Get(hash, ref material, ((string shaderName, string[] keywords) x) => new Material(Shader.Find(x.shaderName))
			{
				hideFlags = (HideFlags.DontSave | HideFlags.NotEditable),
				shaderKeywords = x.keywords
			}, (shaderName, keywords));
		}

		public static void Get<T>(Hash128 hash, ref Material material, Func<T, Material> onCreate, T source)
		{
			s_Repository.Get(hash, ref material, onCreate, source);
		}

		public static void Release(ref Material material)
		{
			s_Repository.Release(ref material);
		}
	}
	internal static class Misc
	{
		public static T[] FindObjectsOfType<T>() where T : UnityEngine.Object
		{
			return UnityEngine.Object.FindObjectsOfType<T>();
		}

		public static void Destroy(UnityEngine.Object obj)
		{
			if ((bool)obj)
			{
				UnityEngine.Object.Destroy(obj);
			}
		}

		public static void DestroyImmediate(UnityEngine.Object obj)
		{
			if ((bool)obj)
			{
				UnityEngine.Object.Destroy(obj);
			}
		}

		[Conditional("UNITY_EDITOR")]
		public static void SetDirty(UnityEngine.Object obj)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void QueuePlayerLoopUpdate()
		{
		}
	}
	internal class InternalObjectPool<T> where T : class
	{
		private readonly Predicate<T> _onValid;

		private readonly ObjectPool<T> _pool;

		public InternalObjectPool(Func<T> onCreate, Predicate<T> onValid, Action<T> onReturn)
		{
			_pool = new ObjectPool<T>(onCreate, null, onReturn);
			_onValid = onValid;
		}

		public T Rent()
		{
			while (0 < _pool.CountInactive)
			{
				T val = _pool.Get();
				if (_onValid(val))
				{
					return val;
				}
			}
			return _pool.Get();
		}

		public void Return(ref T instance)
		{
			if (instance != null)
			{
				_pool.Release(instance);
				instance = null;
			}
		}
	}
	internal static class InternalListPool<T>
	{
		public static List<T> Rent()
		{
			return CollectionPool<List<T>, T>.Get();
		}

		public static void Return(ref List<T> toRelease)
		{
			if (toRelease != null)
			{
				CollectionPool<List<T>, T>.Release(toRelease);
			}
			toRelease = null;
		}
	}
	internal class ObjectRepository<T> where T : UnityEngine.Object
	{
		private class Entry
		{
			public Hash128 hash;

			public int reference;

			public T storedObject;

			public void Release(Action<T> onRelease)
			{
				reference = 0;
				if ((bool)storedObject)
				{
					onRelease?.Invoke(storedObject);
				}
				storedObject = null;
			}

			public override string ToString()
			{
				return $"h{(uint)hash.GetHashCode()} (refs#{reference}), {storedObject}";
			}
		}

		private readonly Dictionary<Hash128, Entry> _cache = new Dictionary<Hash128, Entry>(8);

		private readonly Dictionary<int, Hash128> _objectKey = new Dictionary<int, Hash128>(8);

		private readonly string _name;

		private readonly Action<T> _onRelease;

		private readonly Stack<Entry> _pool = new Stack<Entry>(8);

		public int count => _cache.Count;

		public ObjectRepository(Action<T> onRelease = null)
		{
			_name = typeof(T).Name + "Repository";
			if (onRelease == null)
			{
				_onRelease = delegate(T x)
				{
					UnityEngine.Object.Destroy(x);
				};
			}
			else
			{
				_onRelease = onRelease;
			}
			for (int num = 0; num < 8; num++)
			{
				_pool.Push(new Entry());
			}
		}

		public void Clear()
		{
			foreach (KeyValuePair<Hash128, Entry> item in _cache)
			{
				Entry value = item.Value;
				if (value != null)
				{
					value.Release(_onRelease);
					_pool.Push(value);
				}
			}
			_cache.Clear();
			_objectKey.Clear();
		}

		public bool Valid(Hash128 hash, T obj)
		{
			if (_cache.TryGetValue(hash, out var value))
			{
				return value.storedObject == obj;
			}
			return false;
		}

		public void Get(Hash128 hash, ref T obj, Func<T> onCreate)
		{
			if (!GetFromCache(hash, ref obj))
			{
				Add(hash, ref obj, onCreate());
			}
		}

		public void Get<TS>(Hash128 hash, ref T obj, Func<TS, T> onCreate, TS source)
		{
			if (!GetFromCache(hash, ref obj))
			{
				Add(hash, ref obj, onCreate(source));
			}
		}

		private bool GetFromCache(Hash128 hash, ref T obj)
		{
			if (_cache.TryGetValue(hash, out var value))
			{
				if (!value.storedObject)
				{
					Release(ref value.storedObject);
					return false;
				}
				if (value.storedObject != obj)
				{
					Release(ref obj);
					value.reference++;
					obj = value.storedObject;
				}
				return true;
			}
			return false;
		}

		private void Add(Hash128 hash, ref T obj, T newObject)
		{
			if (!newObject)
			{
				Release(ref obj);
				obj = newObject;
				return;
			}
			Entry entry = ((0 < _pool.Count) ? _pool.Pop() : new Entry());
			entry.storedObject = newObject;
			entry.hash = hash;
			entry.reference = 1;
			_cache[hash] = entry;
			_objectKey[newObject.GetInstanceID()] = hash;
			Release(ref obj);
			obj = newObject;
		}

		public void Release(ref T obj)
		{
			if ((object)obj == null)
			{
				return;
			}
			int instanceID = obj.GetInstanceID();
			if (_objectKey.TryGetValue(instanceID, out var value) && _cache.TryGetValue(value, out var value2))
			{
				value2.reference--;
				if (value2.reference <= 0 || !value2.storedObject)
				{
					Remove(value2);
				}
			}
			obj = null;
		}

		private void Remove(Entry entry)
		{
			if (entry != null)
			{
				_cache.Remove(entry.hash);
				_objectKey.Remove(entry.storedObject.GetInstanceID());
				_pool.Push(entry);
				entry.reference = 0;
				entry.Release(_onRelease);
			}
		}
	}
	internal static class UIExtraCallbacks
	{
		private static bool s_IsInitializedAfterCanvasRebuild;

		private static readonly FastAction s_AfterCanvasRebuildAction;

		private static readonly FastAction s_LateAfterCanvasRebuildAction;

		private static readonly FastAction s_BeforeCanvasRebuildAction;

		private static readonly FastAction s_OnScreenSizeChangedAction;

		private static Vector2Int s_LastScreenSize;

		public static event Action onLateAfterCanvasRebuild
		{
			add
			{
				s_LateAfterCanvasRebuildAction.Add(value);
			}
			remove
			{
				s_LateAfterCanvasRebuildAction.Remove(value);
			}
		}

		public static event Action onBeforeCanvasRebuild
		{
			add
			{
				s_BeforeCanvasRebuildAction.Add(value);
			}
			remove
			{
				s_BeforeCanvasRebuildAction.Remove(value);
			}
		}

		public static event Action onAfterCanvasRebuild
		{
			add
			{
				s_AfterCanvasRebuildAction.Add(value);
			}
			remove
			{
				s_AfterCanvasRebuildAction.Remove(value);
			}
		}

		public static event Action onScreenSizeChanged
		{
			add
			{
				s_OnScreenSizeChangedAction.Add(value);
			}
			remove
			{
				s_OnScreenSizeChangedAction.Remove(value);
			}
		}

		static UIExtraCallbacks()
		{
			s_AfterCanvasRebuildAction = new FastAction();
			s_LateAfterCanvasRebuildAction = new FastAction();
			s_BeforeCanvasRebuildAction = new FastAction();
			s_OnScreenSizeChangedAction = new FastAction();
		}

		private static void InitializeAfterCanvasRebuild()
		{
			if (!s_IsInitializedAfterCanvasRebuild)
			{
				s_IsInitializedAfterCanvasRebuild = true;
				CanvasUpdateRegistry.IsRebuildingLayout();
				Canvas.willRenderCanvases += OnAfterCanvasRebuild;
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeOnLoad()
		{
			Canvas.willRenderCanvases -= OnAfterCanvasRebuild;
			s_IsInitializedAfterCanvasRebuild = false;
		}

		private static void OnBeforeCanvasRebuild()
		{
			Vector2Int vector2Int = new Vector2Int(Screen.width, Screen.height);
			if (s_LastScreenSize != vector2Int)
			{
				if (s_LastScreenSize != default(Vector2Int))
				{
					s_OnScreenSizeChangedAction.Invoke();
				}
				s_LastScreenSize = vector2Int;
			}
			s_BeforeCanvasRebuildAction.Invoke();
			InitializeAfterCanvasRebuild();
		}

		private static void OnAfterCanvasRebuild()
		{
			s_AfterCanvasRebuildAction.Invoke();
			s_LateAfterCanvasRebuildAction.Invoke();
		}
	}
	internal static class ParticleSystemExtensions
	{
		private static ParticleSystem.Particle[] s_TmpParticles = new ParticleSystem.Particle[2048];

		public static ParticleSystem.Particle[] GetParticleArray(int size)
		{
			if (s_TmpParticles.Length < size)
			{
				while (s_TmpParticles.Length < size)
				{
					size = Mathf.NextPowerOfTwo(size);
				}
				s_TmpParticles = new ParticleSystem.Particle[size];
			}
			return s_TmpParticles;
		}

		public static void ValidateShape(this ParticleSystem self)
		{
			ParticleSystem.ShapeModule shape = self.shape;
			if (shape.enabled && shape.alignToDirection && Mathf.Approximately(shape.scale.x * shape.scale.y * shape.scale.z, 0f))
			{
				if (Mathf.Approximately(shape.scale.x, 0f))
				{
					shape.scale.Set(0.0001f, shape.scale.y, shape.scale.z);
				}
				else if (Mathf.Approximately(shape.scale.y, 0f))
				{
					shape.scale.Set(shape.scale.x, 0.0001f, shape.scale.z);
				}
				else if (Mathf.Approximately(shape.scale.z, 0f))
				{
					shape.scale.Set(shape.scale.x, shape.scale.y, 0.0001f);
				}
			}
		}

		public static bool CanBakeMesh(this ParticleSystemRenderer self)
		{
			if (self.renderMode == ParticleSystemRenderMode.Mesh && self.mesh == null)
			{
				return false;
			}
			if (self.renderMode == ParticleSystemRenderMode.None)
			{
				return false;
			}
			return true;
		}

		public static ParticleSystemSimulationSpace GetActualSimulationSpace(this ParticleSystem self)
		{
			ParticleSystem.MainModule main = self.main;
			ParticleSystemSimulationSpace particleSystemSimulationSpace = main.simulationSpace;
			if (particleSystemSimulationSpace == ParticleSystemSimulationSpace.Custom && !main.customSimulationSpace)
			{
				particleSystemSimulationSpace = ParticleSystemSimulationSpace.Local;
			}
			return particleSystemSimulationSpace;
		}

		public static bool IsLocalSpace(this ParticleSystem self)
		{
			return self.GetActualSimulationSpace() == ParticleSystemSimulationSpace.Local;
		}

		public static bool IsWorldSpace(this ParticleSystem self)
		{
			return self.GetActualSimulationSpace() == ParticleSystemSimulationSpace.World;
		}

		public static void SortForRendering(this List<ParticleSystem> self, Transform transform, bool sortByMaterial)
		{
			self.Sort(delegate(ParticleSystem a, ParticleSystem b)
			{
				ParticleSystemRenderer component = a.GetComponent<ParticleSystemRenderer>();
				ParticleSystemRenderer component2 = b.GetComponent<ParticleSystemRenderer>();
				Material material = (component.sharedMaterial ? component.sharedMaterial : component.trailMaterial);
				Material material2 = (component2.sharedMaterial ? component2.sharedMaterial : component2.trailMaterial);
				if (!material && !material2)
				{
					return 0;
				}
				if (!material)
				{
					return -1;
				}
				if (!material2)
				{
					return 1;
				}
				if (sortByMaterial)
				{
					return material.GetInstanceID() - material2.GetInstanceID();
				}
				if (material.renderQueue != material2.renderQueue)
				{
					return material.renderQueue - material2.renderQueue;
				}
				if (component.sortingLayerID != component2.sortingLayerID)
				{
					return SortingLayer.GetLayerValueFromID(component.sortingLayerID) - SortingLayer.GetLayerValueFromID(component2.sortingLayerID);
				}
				if (component.sortingOrder != component2.sortingOrder)
				{
					return component.sortingOrder - component2.sortingOrder;
				}
				Transform transform2 = a.transform;
				Transform transform3 = b.transform;
				float num = transform.InverseTransformPoint(transform2.position).z + component.sortingFudge;
				float num2 = transform.InverseTransformPoint(transform3.position).z + component2.sortingFudge;
				return (!Mathf.Approximately(num, num2)) ? ((int)Mathf.Sign(num2 - num)) : ((int)Mathf.Sign(GetIndex(self, a) - GetIndex(self, b)));
			});
		}

		private static int GetIndex(IList<ParticleSystem> list, UnityEngine.Object ps)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].GetInstanceID() == ps.GetInstanceID())
				{
					return i;
				}
			}
			return 0;
		}

		public static Texture2D GetTextureForSprite(this ParticleSystem self)
		{
			if (!self)
			{
				return null;
			}
			ParticleSystem.TextureSheetAnimationModule textureSheetAnimation = self.textureSheetAnimation;
			if (!textureSheetAnimation.enabled || textureSheetAnimation.mode != ParticleSystemAnimationMode.Sprites)
			{
				return null;
			}
			for (int i = 0; i < textureSheetAnimation.spriteCount; i++)
			{
				Sprite sprite = textureSheetAnimation.GetSprite(i);
				if ((bool)sprite)
				{
					return sprite.GetActualTexture();
				}
			}
			return null;
		}

		public static void Exec(this List<ParticleSystem> self, Action<ParticleSystem> action)
		{
			foreach (ParticleSystem item in self)
			{
				if ((bool)item)
				{
					action(item);
				}
			}
		}

		public static ParticleSystem GetMainEmitter(this ParticleSystem self, List<ParticleSystem> list)
		{
			if (!self || list == null || list.Count == 0)
			{
				return null;
			}
			for (int i = 0; i < list.Count; i++)
			{
				ParticleSystem particleSystem = list[i];
				if (particleSystem != self && self.IsSubEmitterOf(particleSystem))
				{
					return particleSystem;
				}
			}
			return null;
		}

		public static bool IsSubEmitterOf(this ParticleSystem self, ParticleSystem parent)
		{
			if (!self || !parent)
			{
				return false;
			}
			ParticleSystem.SubEmittersModule subEmitters = parent.subEmitters;
			int subEmittersCount = subEmitters.subEmittersCount;
			for (int i = 0; i < subEmittersCount; i++)
			{
				if (subEmitters.GetSubEmitterSystem(i) == self)
				{
					return true;
				}
			}
			return false;
		}
	}
}
namespace Coffee.UIExtensions
{
	[Serializable]
	public class AnimatableProperty : ISerializationCallbackReceiver
	{
		public enum ShaderPropertyType
		{
			Color,
			Vector,
			Float,
			Range,
			Texture
		}

		[SerializeField]
		private string m_Name = "";

		[SerializeField]
		private ShaderPropertyType m_Type = ShaderPropertyType.Vector;

		public int id { get; private set; }

		public ShaderPropertyType type => m_Type;

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			id = Shader.PropertyToID(m_Name);
		}

		public void UpdateMaterialProperties(Material material, MaterialPropertyBlock mpb)
		{
			if (!material.HasProperty(id))
			{
				return;
			}
			switch (type)
			{
			case ShaderPropertyType.Color:
			{
				Color color = mpb.GetColor(id);
				if (color != default(Color))
				{
					material.SetColor(id, color);
				}
				break;
			}
			case ShaderPropertyType.Vector:
			{
				Vector4 vector = mpb.GetVector(id);
				if (vector != default(Vector4))
				{
					material.SetVector(id, vector);
				}
				break;
			}
			case ShaderPropertyType.Float:
			case ShaderPropertyType.Range:
			{
				float num = mpb.GetFloat(id);
				if (!Mathf.Approximately(num, 0f))
				{
					material.SetFloat(id, num);
				}
				break;
			}
			case ShaderPropertyType.Texture:
			{
				Texture texture = mpb.GetTexture(id);
				if (texture != null)
				{
					material.SetTexture(id, texture);
				}
				break;
			}
			}
		}
	}
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasRenderer))]
	public class UIParticle : MaskableGraphic, ISerializationCallbackReceiver
	{
		public enum AutoScalingMode
		{
			None,
			UIParticle,
			Transform
		}

		public enum MeshSharing
		{
			None,
			Auto,
			Primary,
			PrimarySimulator,
			Replica
		}

		public enum PositionMode
		{
			Relative,
			Absolute
		}

		[HideInInspector]
		[SerializeField]
		[Obsolete]
		internal bool m_IsTrail;

		[HideInInspector]
		[FormerlySerializedAs("m_IgnoreParent")]
		[SerializeField]
		[Obsolete]
		private bool m_IgnoreCanvasScaler;

		[HideInInspector]
		[SerializeField]
		[Obsolete]
		internal bool m_AbsoluteMode;

		[Tooltip("Scale the rendering particles. When the `3D` toggle is enabled, 3D scale (x, y, z) is supported.")]
		[SerializeField]
		private Vector3 m_Scale3D = new Vector3(10f, 10f, 10f);

		[Tooltip("If you want to update material properties (e.g. _MainTex_ST, _Color) in AnimationClip, use this to mark as animatable.")]
		[SerializeField]
		internal AnimatableProperty[] m_AnimatableProperties = new AnimatableProperty[0];

		[Tooltip("Particles")]
		[SerializeField]
		private List<ParticleSystem> m_Particles = new List<ParticleSystem>();

		[Tooltip("Particle simulation results are shared within the same group. A large number of the same effects can be displayed with a small load.\nNone: Disable mesh sharing.\nAuto: Automatically select Primary/Replica.\nPrimary: Provides particle simulation results to the same group.\nPrimary Simulator: Primary, but do not render the particle (simulation only).\nReplica: Render simulation results provided by the primary.")]
		[SerializeField]
		private MeshSharing m_MeshSharing;

		[Tooltip("Mesh sharing group ID.\nIf non-zero is specified, particle simulation results are shared within the group.")]
		[SerializeField]
		private int m_GroupId;

		[SerializeField]
		private int m_GroupMaxId;

		[Tooltip("Emission position mode.\nRelative: The particles will be emitted from the scaled position.\nAbsolute: The particles will be emitted from the world position.")]
		[SerializeField]
		private PositionMode m_PositionMode;

		[SerializeField]
		[Obsolete]
		internal bool m_AutoScaling;

		[SerializeField]
		[Tooltip("How to automatically adjust when the Canvas scale is changed by the screen size or reference resolution.\nNone: Do nothing.\nTransform: Transform.lossyScale (=world scale) will be set to (1, 1, 1).\nUIParticle: UIParticle.scale will be adjusted.")]
		private AutoScalingMode m_AutoScalingMode = AutoScalingMode.Transform;

		[SerializeField]
		[Tooltip("Use a custom view.\nUse this if the particles are not displayed correctly due to min/max particle size.")]
		private bool m_UseCustomView;

		[SerializeField]
		[Tooltip("Custom view size.\nChange the bake view size.")]
		private float m_CustomViewSize = 10f;

		[SerializeField]
		[Tooltip("Time scale multiplier.")]
		private float m_TimeScaleMultiplier = 1f;

		private readonly List<UIParticleRenderer> _renderers = new List<UIParticleRenderer>();

		private Camera _bakeCamera;

		private int _groupId;

		private bool _isScaleStored;

		private Vector3 _storedScale;

		private DrivenRectTransformTracker _tracker;

		public override bool raycastTarget
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public MeshSharing meshSharing
		{
			get
			{
				return m_MeshSharing;
			}
			set
			{
				m_MeshSharing = value;
			}
		}

		public int groupId
		{
			get
			{
				return _groupId;
			}
			set
			{
				if (m_GroupId != value)
				{
					m_GroupId = value;
					if (m_GroupId != m_GroupMaxId)
					{
						ResetGroupId();
					}
				}
			}
		}

		public int groupMaxId
		{
			get
			{
				return m_GroupMaxId;
			}
			set
			{
				if (m_GroupMaxId != value)
				{
					m_GroupMaxId = value;
					ResetGroupId();
				}
			}
		}

		public PositionMode positionMode
		{
			get
			{
				return m_PositionMode;
			}
			set
			{
				m_PositionMode = value;
			}
		}

		[Obsolete("The absoluteMode is now obsolete. Please use the autoScalingMode instead.", false)]
		public bool absoluteMode
		{
			get
			{
				return m_PositionMode == PositionMode.Absolute;
			}
			set
			{
				positionMode = (value ? PositionMode.Absolute : PositionMode.Relative);
			}
		}

		[Obsolete("The autoScaling is now obsolete. Please use the autoScalingMode instead.", false)]
		public bool autoScaling
		{
			get
			{
				return m_AutoScalingMode != AutoScalingMode.None;
			}
			set
			{
				autoScalingMode = (value ? AutoScalingMode.Transform : AutoScalingMode.None);
			}
		}

		public AutoScalingMode autoScalingMode
		{
			get
			{
				return m_AutoScalingMode;
			}
			set
			{
				if (m_AutoScalingMode != value)
				{
					m_AutoScalingMode = value;
					if (autoScalingMode != AutoScalingMode.Transform && _isScaleStored)
					{
						base.transform.localScale = _storedScale;
						_isScaleStored = false;
					}
				}
			}
		}

		public bool useCustomView
		{
			get
			{
				return m_UseCustomView;
			}
			set
			{
				m_UseCustomView = value;
			}
		}

		public float customViewSize
		{
			get
			{
				return m_CustomViewSize;
			}
			set
			{
				m_CustomViewSize = Mathf.Max(0.1f, value);
			}
		}

		public float timeScaleMultiplier
		{
			get
			{
				return m_TimeScaleMultiplier;
			}
			set
			{
				m_TimeScaleMultiplier = value;
			}
		}

		internal bool useMeshSharing => m_MeshSharing != MeshSharing.None;

		internal bool isPrimary
		{
			get
			{
				if (m_MeshSharing != MeshSharing.Primary)
				{
					return m_MeshSharing == MeshSharing.PrimarySimulator;
				}
				return true;
			}
		}

		internal bool canSimulate
		{
			get
			{
				if (m_MeshSharing != MeshSharing.None && m_MeshSharing != MeshSharing.Auto && m_MeshSharing != MeshSharing.Primary)
				{
					return m_MeshSharing == MeshSharing.PrimarySimulator;
				}
				return true;
			}
		}

		internal bool canRender
		{
			get
			{
				if (m_MeshSharing != MeshSharing.None && m_MeshSharing != MeshSharing.Auto && m_MeshSharing != MeshSharing.Primary)
				{
					return m_MeshSharing == MeshSharing.Replica;
				}
				return true;
			}
		}

		public float scale
		{
			get
			{
				return m_Scale3D.x;
			}
			set
			{
				m_Scale3D = new Vector3(value, value, value);
			}
		}

		public Vector3 scale3D
		{
			get
			{
				return m_Scale3D;
			}
			set
			{
				m_Scale3D = value;
			}
		}

		public Vector3 scale3DForCalc
		{
			get
			{
				if (autoScalingMode != AutoScalingMode.Transform)
				{
					return m_Scale3D.GetScaled(canvasScale, base.transform.localScale);
				}
				return m_Scale3D;
			}
		}

		public List<ParticleSystem> particles => m_Particles;

		public bool isPaused { get; private set; }

		public Vector3 parentScale { get; private set; }

		public Vector3 canvasScale { get; private set; }

		protected override void OnEnable()
		{
			_isScaleStored = false;
			ResetGroupId();
			UIParticleUpdater.Register(this);
			RegisterDirtyMaterialCallback(UpdateRendererMaterial);
			if (0 < particles.Count)
			{
				RefreshParticles(particles);
			}
			else
			{
				RefreshParticles();
			}
			base.OnEnable();
		}

		protected override void OnDisable()
		{
			_tracker.Clear();
			if (autoScalingMode == AutoScalingMode.Transform && _isScaleStored)
			{
				base.transform.localScale = _storedScale;
			}
			_isScaleStored = false;
			UIParticleUpdater.Unregister(this);
			_renderers.ForEach(delegate(UIParticleRenderer r)
			{
				r.Reset();
			});
			UnregisterDirtyMaterialCallback(UpdateRendererMaterial);
			base.OnDisable();
		}

		protected override void OnDidApplyAnimationProperties()
		{
		}

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (m_IgnoreCanvasScaler || m_AutoScaling)
			{
				m_IgnoreCanvasScaler = false;
				m_AutoScaling = false;
				m_AutoScalingMode = AutoScalingMode.Transform;
			}
			if (m_AbsoluteMode)
			{
				m_AbsoluteMode = false;
				m_PositionMode = PositionMode.Absolute;
			}
		}

		public void Play()
		{
			particles.Exec(delegate(ParticleSystem p)
			{
				p.Simulate(0f, withChildren: false, restart: true);
			});
			isPaused = false;
		}

		public void Pause()
		{
			particles.Exec(delegate(ParticleSystem p)
			{
				p.Pause();
			});
			isPaused = true;
		}

		public void Resume()
		{
			isPaused = false;
		}

		public void Stop()
		{
			particles.Exec(delegate(ParticleSystem p)
			{
				p.Stop();
			});
			isPaused = true;
		}

		public void StartEmission()
		{
			particles.Exec(delegate(ParticleSystem p)
			{
				ParticleSystem.EmissionModule emission = p.emission;
				emission.enabled = true;
			});
		}

		public void StopEmission()
		{
			particles.Exec(delegate(ParticleSystem p)
			{
				ParticleSystem.EmissionModule emission = p.emission;
				emission.enabled = false;
			});
		}

		public void Clear()
		{
			particles.Exec(delegate(ParticleSystem p)
			{
				p.Clear();
			});
			isPaused = true;
		}

		public void GetMaterials(List<Material> result)
		{
			if (result == null)
			{
				return;
			}
			for (int i = 0; i < _renderers.Count; i++)
			{
				UIParticleRenderer uIParticleRenderer = _renderers[i];
				if ((bool)uIParticleRenderer && (bool)uIParticleRenderer.material)
				{
					result.Add(uIParticleRenderer.material);
				}
			}
		}

		public void SetParticleSystemInstance(GameObject instance)
		{
			SetParticleSystemInstance(instance, destroyOldParticles: true);
		}

		public void SetParticleSystemInstance(GameObject instance, bool destroyOldParticles)
		{
			if (!instance)
			{
				return;
			}
			int childCount = base.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				GameObject gameObject = base.transform.GetChild(i).gameObject;
				if ((!gameObject.TryGetComponent<Camera>(out var component) || !(component == _bakeCamera)) && !gameObject.TryGetComponent<UIParticleRenderer>(out var _))
				{
					gameObject.SetActive(value: false);
					if (destroyOldParticles)
					{
						Misc.Destroy(gameObject);
					}
				}
			}
			Transform obj = instance.transform;
			obj.SetParent(base.transform, worldPositionStays: false);
			obj.localPosition = Vector3.zero;
			RefreshParticles(instance);
		}

		public void SetParticleSystemPrefab(GameObject prefab)
		{
			if ((bool)prefab)
			{
				SetParticleSystemInstance(UnityEngine.Object.Instantiate(prefab.gameObject), destroyOldParticles: true);
			}
		}

		public void RefreshParticles()
		{
			RefreshParticles(base.gameObject);
		}

		private void RefreshParticles(GameObject root)
		{
			if (!root)
			{
				return;
			}
			root.GetComponentsInChildren(includeInactive: true, particles);
			int num = particles.Count - 1;
			while (0 <= num)
			{
				ParticleSystem particleSystem = particles[num];
				if (!particleSystem || particleSystem.GetComponentInParent<UIParticle>(includeInactive: true) != this)
				{
					particles.RemoveAt(num);
				}
				num--;
			}
			for (int i = 0; i < particles.Count; i++)
			{
				ParticleSystem.TextureSheetAnimationModule textureSheetAnimation = particles[i].textureSheetAnimation;
				if (textureSheetAnimation.mode == ParticleSystemAnimationMode.Sprites && textureSheetAnimation.uvChannelMask == (UVChannelFlags)0)
				{
					textureSheetAnimation.uvChannelMask = UVChannelFlags.UV0;
				}
			}
			RefreshParticles(particles);
		}

		public void RefreshParticles(List<ParticleSystem> particleSystems)
		{
			_renderers.Clear();
			int childCount = base.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				if (base.transform.GetChild(i).TryGetComponent<UIParticleRenderer>(out var component))
				{
					_renderers.Add(component);
				}
			}
			for (int j = 0; j < _renderers.Count; j++)
			{
				_renderers[j].Reset(j);
			}
			int num = 0;
			for (int k = 0; k < particleSystems.Count; k++)
			{
				ParticleSystem particleSystem = particleSystems[k];
				if ((bool)particleSystem)
				{
					ParticleSystem mainEmitter = particleSystem.GetMainEmitter(particleSystems);
					GetRenderer(num++).Set(this, particleSystem, isTrail: false, mainEmitter);
					if (particleSystem.trails.enabled)
					{
						GetRenderer(num++).Set(this, particleSystem, isTrail: true, mainEmitter);
					}
				}
			}
		}

		internal void UpdateTransformScale()
		{
			_tracker.Clear();
			canvasScale = base.canvas.rootCanvas.transform.localScale.Inverse();
			parentScale = base.transform.parent.lossyScale;
			if (autoScalingMode != AutoScalingMode.Transform)
			{
				if (_isScaleStored)
				{
					base.transform.localScale = _storedScale;
				}
				_isScaleStored = false;
				return;
			}
			Vector3 localScale = base.transform.localScale;
			if (!_isScaleStored)
			{
				_storedScale = (localScale.IsVisible() ? localScale : Vector3.one);
				_isScaleStored = true;
			}
			_tracker.Add(this, base.rectTransform, DrivenTransformProperties.Scale);
			Vector3 vector = parentScale.Inverse();
			if (localScale != vector)
			{
				base.transform.localScale = vector;
			}
		}

		internal void UpdateRenderers()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			for (int i = 0; i < _renderers.Count; i++)
			{
				if (!_renderers[i])
				{
					RefreshParticles(particles);
					break;
				}
			}
			Camera bakeCamera = GetBakeCamera();
			for (int j = 0; j < _renderers.Count; j++)
			{
				UIParticleRenderer uIParticleRenderer = _renderers[j];
				if ((bool)uIParticleRenderer)
				{
					uIParticleRenderer.UpdateMesh(bakeCamera);
				}
			}
		}

		internal void ResetGroupId()
		{
			_groupId = ((m_GroupId == m_GroupMaxId) ? m_GroupId : UnityEngine.Random.Range(m_GroupId, m_GroupMaxId + 1));
		}

		protected override void UpdateMaterial()
		{
		}

		protected override void UpdateGeometry()
		{
		}

		private void UpdateRendererMaterial()
		{
			for (int i = 0; i < _renderers.Count; i++)
			{
				UIParticleRenderer uIParticleRenderer = _renderers[i];
				if ((bool)uIParticleRenderer)
				{
					uIParticleRenderer.maskable = base.maskable;
					uIParticleRenderer.SetMaterialDirty();
				}
			}
		}

		internal UIParticleRenderer GetRenderer(int index)
		{
			if (_renderers.Count <= index)
			{
				_renderers.Add(UIParticleRenderer.AddRenderer(this, index));
			}
			if (!_renderers[index])
			{
				_renderers[index] = UIParticleRenderer.AddRenderer(this, index);
			}
			return _renderers[index];
		}

		private Camera GetBakeCamera()
		{
			if (!base.canvas)
			{
				return Camera.main;
			}
			if (!useCustomView && base.canvas.renderMode != RenderMode.ScreenSpaceOverlay && (bool)base.canvas.rootCanvas.worldCamera)
			{
				return base.canvas.rootCanvas.worldCamera;
			}
			if ((bool)_bakeCamera)
			{
				_bakeCamera.orthographicSize = (useCustomView ? customViewSize : 10f);
				return _bakeCamera;
			}
			int childCount = base.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				if (base.transform.GetChild(i).TryGetComponent<Camera>(out var component) && component.name == "[generated] UIParticle BakingCamera")
				{
					_bakeCamera = component;
					break;
				}
			}
			if (!_bakeCamera)
			{
				GameObject gameObject = new GameObject("[generated] UIParticle BakingCamera");
				gameObject.SetActive(value: false);
				gameObject.transform.SetParent(base.transform, worldPositionStays: false);
				_bakeCamera = gameObject.AddComponent<Camera>();
			}
			_bakeCamera.enabled = false;
			_bakeCamera.orthographicSize = (useCustomView ? customViewSize : 10f);
			_bakeCamera.transform.SetPositionAndRotation(new Vector3(0f, 0f, -1000f), Quaternion.identity);
			_bakeCamera.orthographic = true;
			_bakeCamera.farClipPlane = 2000f;
			_bakeCamera.clearFlags = CameraClearFlags.Nothing;
			_bakeCamera.cullingMask = 0;
			_bakeCamera.allowHDR = false;
			_bakeCamera.allowMSAA = false;
			_bakeCamera.renderingPath = RenderingPath.Forward;
			_bakeCamera.useOcclusionCulling = false;
			_bakeCamera.gameObject.SetActive(value: false);
			_bakeCamera.gameObject.hideFlags = UIParticleProjectSettings.globalHideFlags;
			return _bakeCamera;
		}
	}
	[ExecuteAlways]
	public class UIParticleAttractor : MonoBehaviour, ISerializationCallbackReceiver
	{
		public enum Movement
		{
			Linear,
			Smooth,
			Sphere
		}

		public enum UpdateMode
		{
			Normal,
			UnscaledTime
		}

		[SerializeField]
		[HideInInspector]
		private ParticleSystem m_ParticleSystem;

		[SerializeField]
		private List<ParticleSystem> m_ParticleSystems = new List<ParticleSystem>();

		[Range(0.1f, 10f)]
		[SerializeField]
		private float m_DestinationRadius = 1f;

		[Range(0f, 0.95f)]
		[SerializeField]
		private float m_DelayRate;

		[Range(0.001f, 100f)]
		[SerializeField]
		private float m_MaxSpeed = 1f;

		[SerializeField]
		private Movement m_Movement;

		[SerializeField]
		private UpdateMode m_UpdateMode;

		[SerializeField]
		private UnityEvent m_OnAttracted;

		private List<UIParticle> _uiParticles = new List<UIParticle>();

		public float destinationRadius
		{
			get
			{
				return m_DestinationRadius;
			}
			set
			{
				m_DestinationRadius = Mathf.Clamp(value, 0.1f, 10f);
			}
		}

		public float delay
		{
			get
			{
				return m_DelayRate;
			}
			set
			{
				m_DelayRate = value;
			}
		}

		public float maxSpeed
		{
			get
			{
				return m_MaxSpeed;
			}
			set
			{
				m_MaxSpeed = value;
			}
		}

		public Movement movement
		{
			get
			{
				return m_Movement;
			}
			set
			{
				m_Movement = value;
			}
		}

		public UpdateMode updateMode
		{
			get
			{
				return m_UpdateMode;
			}
			set
			{
				m_UpdateMode = value;
			}
		}

		public UnityEvent onAttracted
		{
			get
			{
				return m_OnAttracted;
			}
			set
			{
				m_OnAttracted = value;
			}
		}

		public IReadOnlyList<ParticleSystem> particleSystems => m_ParticleSystems;

		public void AddParticleSystem(ParticleSystem ps)
		{
			if (m_ParticleSystems == null)
			{
				m_ParticleSystems = new List<ParticleSystem>();
			}
			int num = m_ParticleSystems.IndexOf(ps);
			if (0 > num)
			{
				m_ParticleSystems.Add(ps);
				_uiParticles.Clear();
			}
		}

		public void RemoveParticleSystem(ParticleSystem ps)
		{
			if (m_ParticleSystems != null)
			{
				int num = m_ParticleSystems.IndexOf(ps);
				if (num >= 0)
				{
					m_ParticleSystems.RemoveAt(num);
					_uiParticles.Clear();
				}
			}
		}

		private void Awake()
		{
			UpgradeIfNeeded();
		}

		private void OnEnable()
		{
			UIParticleUpdater.Register(this);
		}

		private void OnDisable()
		{
			UIParticleUpdater.Unregister(this);
		}

		private void OnDestroy()
		{
			_uiParticles = null;
			m_ParticleSystems = null;
		}

		internal void Attract()
		{
			CollectUIParticlesIfNeeded();
			for (int i = 0; i < m_ParticleSystems.Count; i++)
			{
				ParticleSystem particleSystem = m_ParticleSystems[i];
				if (particleSystem == null || !particleSystem.gameObject.activeInHierarchy)
				{
					continue;
				}
				int particleCount = particleSystem.particleCount;
				if (particleCount == 0)
				{
					continue;
				}
				ParticleSystem.Particle[] particleArray = ParticleSystemExtensions.GetParticleArray(particleCount);
				particleSystem.GetParticles(particleArray, particleCount);
				UIParticle uiParticle = _uiParticles[i];
				Vector3 destinationPosition = GetDestinationPosition(uiParticle, particleSystem);
				for (int j = 0; j < particleCount; j++)
				{
					ParticleSystem.Particle particle = particleArray[j];
					if (0f < particle.remainingLifetime && Vector3.Distance(particle.position, destinationPosition) < m_DestinationRadius)
					{
						particle.remainingLifetime = 0f;
						particleArray[j] = particle;
						if (m_OnAttracted != null)
						{
							try
							{
								m_OnAttracted.Invoke();
							}
							catch (Exception exception)
							{
								UnityEngine.Debug.LogException(exception);
							}
						}
					}
					else
					{
						float num = particle.startLifetime * m_DelayRate;
						float duration = particle.startLifetime - num;
						float num2 = Mathf.Max(0f, particle.startLifetime - particle.remainingLifetime - num);
						if (!(num2 <= 0f))
						{
							particle.position = GetAttractedPosition(particle.position, destinationPosition, duration, num2);
							particle.velocity *= 0.5f;
							particleArray[j] = particle;
						}
					}
				}
				particleSystem.SetParticles(particleArray, particleCount);
			}
		}

		private Vector3 GetDestinationPosition(UIParticle uiParticle, ParticleSystem particleSystem)
		{
			bool num = (bool)uiParticle && uiParticle.enabled;
			Vector3 position = particleSystem.transform.position;
			Vector3 vector = base.transform.position;
			if (particleSystem.IsLocalSpace())
			{
				vector = particleSystem.transform.InverseTransformPoint(vector);
			}
			if (num)
			{
				Vector3 vector2 = uiParticle.parentScale.Inverse();
				Vector3 scale3DForCalc = uiParticle.scale3DForCalc;
				vector = vector.GetScaled(vector2, scale3DForCalc.Inverse());
				if (uiParticle.positionMode == UIParticle.PositionMode.Relative)
				{
					Vector3 vector3 = uiParticle.transform.position - position;
					vector3.Scale(scale3DForCalc - vector2);
					vector3.Scale(scale3DForCalc.Inverse());
					vector += vector3;
				}
			}
			return vector;
		}

		private Vector3 GetAttractedPosition(Vector3 current, Vector3 target, float duration, float time)
		{
			float num = m_MaxSpeed;
			switch (m_UpdateMode)
			{
			case UpdateMode.Normal:
				num *= 60f * Time.deltaTime;
				break;
			case UpdateMode.UnscaledTime:
				num *= 60f * Time.unscaledDeltaTime;
				break;
			}
			switch (m_Movement)
			{
			case Movement.Linear:
				num /= duration;
				break;
			case Movement.Smooth:
				target = Vector3.Lerp(current, target, time / duration);
				break;
			case Movement.Sphere:
				target = Vector3.Slerp(current, target, time / duration);
				break;
			}
			return Vector3.MoveTowards(current, target, num);
		}

		private void CollectUIParticlesIfNeeded()
		{
			if (m_ParticleSystems.Count == 0 || _uiParticles.Count != 0)
			{
				return;
			}
			if (_uiParticles.Capacity < m_ParticleSystems.Capacity)
			{
				_uiParticles.Capacity = m_ParticleSystems.Capacity;
			}
			for (int i = 0; i < m_ParticleSystems.Count; i++)
			{
				ParticleSystem particleSystem = m_ParticleSystems[i];
				if (particleSystem == null)
				{
					_uiParticles.Add(null);
					continue;
				}
				UIParticle componentInParent = particleSystem.GetComponentInParent<UIParticle>(includeInactive: true);
				_uiParticles.Add(componentInParent.particles.Contains(particleSystem) ? componentInParent : null);
			}
		}

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			UpgradeIfNeeded();
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
		}

		private void UpgradeIfNeeded()
		{
			if (m_ParticleSystem != null)
			{
				if (!m_ParticleSystems.Contains(m_ParticleSystem))
				{
					m_ParticleSystems.Add(m_ParticleSystem);
				}
				m_ParticleSystem = null;
				UnityEngine.Debug.Log("Upgraded!");
			}
		}
	}
	public class UIParticleProjectSettings : PreloadedProjectSettings<UIParticleProjectSettings>
	{
		[Header("Setting")]
		[SerializeField]
		internal bool m_EnableLinearToGamma = true;

		[Header("Editor")]
		[Tooltip("Hide the automatically generated objects.\n  - UIParticleRenderer\n  - UIParticle BakingCamera")]
		[SerializeField]
		private bool m_HideGeneratedObjects = true;

		public static bool enableLinearToGamma
		{
			get
			{
				return PreloadedProjectSettings<UIParticleProjectSettings>.instance.m_EnableLinearToGamma;
			}
			set
			{
				PreloadedProjectSettings<UIParticleProjectSettings>.instance.m_EnableLinearToGamma = value;
			}
		}

		public static HideFlags globalHideFlags
		{
			get
			{
				if (!PreloadedProjectSettings<UIParticleProjectSettings>.instance.m_HideGeneratedObjects)
				{
					return HideFlags.DontSave | HideFlags.NotEditable;
				}
				return HideFlags.HideAndDontSave | HideFlags.HideInInspector;
			}
		}
	}
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasRenderer))]
	[AddComponentMenu("")]
	internal class UIParticleRenderer : MaskableGraphic
	{
		private static readonly CombineInstance[] s_CombineInstances = new CombineInstance[1];

		private static readonly List<Material> s_Materials = new List<Material>(2);

		private static MaterialPropertyBlock s_Mpb;

		private static readonly Vector3[] s_Corners = new Vector3[4];

		private bool _delay;

		private int _index;

		private bool _isPrevStored;

		private bool _isTrail;

		private Bounds _lastBounds;

		private Material _materialForRendering;

		private Material _modifiedMaterial;

		private UIParticle _parent;

		private ParticleSystem _particleSystem;

		private float _prevCanvasScale;

		private Vector3 _prevPsPos;

		private Vector3 _prevScale;

		private Vector2Int _prevScreenSize;

		private bool _preWarm;

		private ParticleSystemRenderer _renderer;

		private ParticleSystem _mainEmitter;

		public override Texture mainTexture
		{
			get
			{
				if (!_isTrail)
				{
					return _particleSystem.GetTextureForSprite();
				}
				return null;
			}
		}

		public override bool raycastTarget => false;

		private Rect rootCanvasRect
		{
			get
			{
				s_Corners[0] = base.transform.TransformPoint(_lastBounds.min.x, _lastBounds.min.y, 0f);
				s_Corners[1] = base.transform.TransformPoint(_lastBounds.min.x, _lastBounds.max.y, 0f);
				s_Corners[2] = base.transform.TransformPoint(_lastBounds.max.x, _lastBounds.max.y, 0f);
				s_Corners[3] = base.transform.TransformPoint(_lastBounds.max.x, _lastBounds.min.y, 0f);
				if ((bool)base.canvas)
				{
					Matrix4x4 worldToLocalMatrix = base.canvas.rootCanvas.transform.worldToLocalMatrix;
					for (int i = 0; i < 4; i++)
					{
						s_Corners[i] = worldToLocalMatrix.MultiplyPoint(s_Corners[i]);
					}
				}
				Vector2 vector = s_Corners[0];
				Vector2 vector2 = s_Corners[0];
				for (int j = 1; j < 4; j++)
				{
					if (s_Corners[j].x < vector.x)
					{
						vector.x = s_Corners[j].x;
					}
					else if (s_Corners[j].x > vector2.x)
					{
						vector2.x = s_Corners[j].x;
					}
					if (s_Corners[j].y < vector.y)
					{
						vector.y = s_Corners[j].y;
					}
					else if (s_Corners[j].y > vector2.y)
					{
						vector2.y = s_Corners[j].y;
					}
				}
				return new Rect(vector, vector2 - vector);
			}
		}

		public override Material materialForRendering
		{
			get
			{
				if (!_materialForRendering)
				{
					_materialForRendering = base.materialForRendering;
				}
				return _materialForRendering;
			}
		}

		public void Reset(int index = -1)
		{
			if ((bool)_renderer)
			{
				_renderer.enabled = true;
			}
			_parent = null;
			_particleSystem = null;
			_renderer = null;
			_mainEmitter = null;
			if (0 <= index)
			{
				_index = index;
			}
			if ((bool)this && base.isActiveAndEnabled)
			{
				material = null;
				base.canvasRenderer.Clear();
				_lastBounds = default(Bounds);
				base.enabled = false;
			}
			else
			{
				MaterialRepository.Release(ref _modifiedMaterial);
				_materialForRendering = null;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			base.hideFlags = UIParticleProjectSettings.globalHideFlags;
			if (!s_CombineInstances[0].mesh)
			{
				s_CombineInstances[0].mesh = new Mesh
				{
					name = "[UIParticleRenderer] Combine Instance Mesh",
					hideFlags = HideFlags.HideAndDontSave
				};
			}
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			MaterialRepository.Release(ref _modifiedMaterial);
			_materialForRendering = null;
			_isPrevStored = false;
		}

		public static UIParticleRenderer AddRenderer(UIParticle parent, int index)
		{
			GameObject obj = new GameObject("[generated] UIParticleRenderer", typeof(UIParticleRenderer))
			{
				hideFlags = UIParticleProjectSettings.globalHideFlags,
				layer = parent.gameObject.layer
			};
			Transform obj2 = obj.transform;
			obj2.SetParent(parent.transform, worldPositionStays: false);
			obj2.localPosition = Vector3.zero;
			obj2.localRotation = Quaternion.identity;
			obj2.localScale = Vector3.one;
			UIParticleRenderer component = obj.GetComponent<UIParticleRenderer>();
			component._parent = parent;
			component._index = index;
			return component;
		}

		public override Material GetModifiedMaterial(Material baseMaterial)
		{
			if (!IsActive() || !_parent)
			{
				MaterialRepository.Release(ref _modifiedMaterial);
				return baseMaterial;
			}
			Material modifiedMaterial = base.GetModifiedMaterial(baseMaterial);
			Texture texture = mainTexture;
			if (texture == null && _parent.m_AnimatableProperties.Length == 0)
			{
				MaterialRepository.Release(ref _modifiedMaterial);
				return modifiedMaterial;
			}
			Hash128 hash = new Hash128(modifiedMaterial ? ((uint)modifiedMaterial.GetInstanceID()) : 0u, texture ? ((uint)texture.GetInstanceID()) : 0u, (_parent.m_AnimatableProperties.Length != 0) ? ((uint)GetInstanceID()) : 0u, 0u);
			if (!MaterialRepository.Valid(hash, _modifiedMaterial))
			{
				MaterialRepository.Get(hash, ref _modifiedMaterial, ((Material mat, Texture texture) x) => new Material(x.mat)
				{
					hideFlags = HideFlags.HideAndDontSave,
					mainTexture = (x.texture ? x.texture : x.mat.mainTexture)
				}, (modifiedMaterial, texture));
			}
			return _modifiedMaterial;
		}

		public void Set(UIParticle parent, ParticleSystem ps, bool isTrail, ParticleSystem mainEmitter)
		{
			_parent = parent;
			base.maskable = parent.maskable;
			base.gameObject.layer = parent.gameObject.layer;
			_particleSystem = ps;
			_preWarm = _particleSystem.main.prewarm;
			if (_particleSystem.isPlaying || _preWarm)
			{
				_particleSystem.Clear();
				_particleSystem.Pause();
			}
			ps.TryGetComponent<ParticleSystemRenderer>(out _renderer);
			_renderer.enabled = false;
			_isTrail = isTrail;
			_renderer.GetSharedMaterials(s_Materials);
			material = s_Materials[isTrail ? 1 : 0];
			s_Materials.Clear();
			ParticleSystem.TextureSheetAnimationModule textureSheetAnimation = ps.textureSheetAnimation;
			if (textureSheetAnimation.mode == ParticleSystemAnimationMode.Sprites && textureSheetAnimation.uvChannelMask == (UVChannelFlags)0)
			{
				textureSheetAnimation.uvChannelMask = UVChannelFlags.UV0;
			}
			_prevScale = GetWorldScale();
			_prevPsPos = _particleSystem.transform.position;
			_prevScreenSize = new Vector2Int(Screen.width, Screen.height);
			_prevCanvasScale = (base.canvas ? base.canvas.scaleFactor : 1f);
			_delay = true;
			_mainEmitter = mainEmitter;
			base.canvasRenderer.SetTexture(null);
			base.enabled = true;
		}

		public void UpdateMesh(Camera bakeCamera)
		{
			if (!base.isActiveAndEnabled || !_particleSystem || !_parent || !base.canvasRenderer || !base.canvas || !bakeCamera || _parent.meshSharing == UIParticle.MeshSharing.Replica || !base.transform.lossyScale.GetScaled(_parent.scale3DForCalc).IsVisible() || (!_particleSystem.IsAlive() && !_particleSystem.isPlaying) || (_isTrail && !_particleSystem.trails.enabled) || base.canvasRenderer.GetInheritedAlpha() < 0.01f)
			{
				Graphic.workerMesh.Clear();
				base.canvasRenderer.SetMesh(Graphic.workerMesh);
				_lastBounds = default(Bounds);
				return;
			}
			ParticleSystem.MainModule main = _particleSystem.main;
			Vector3 worldScale = GetWorldScale();
			Vector3 position = _particleSystem.transform.position;
			if (!_isTrail && _parent.canSimulate && !_mainEmitter)
			{
				ResolveResolutionChange(position, worldScale);
				Simulate(worldScale, _parent.isPaused || _delay);
				if (_delay && !_parent.isPaused)
				{
					Simulate(worldScale, _parent.isPaused);
				}
				if (!main.loop && main.duration <= _particleSystem.time && (_particleSystem.IsAlive() || _particleSystem.particleCount == 0))
				{
					_particleSystem.Stop(withChildren: false);
				}
				_prevScale = worldScale;
				_prevPsPos = position;
				_delay = false;
			}
			s_CombineInstances[0].mesh.Clear(keepVertexLayout: false);
			float x = s_CombineInstances[0].mesh.bounds.extents.x;
			if (!float.IsNaN(x) && !float.IsInfinity(x) && 0f < x)
			{
				s_CombineInstances[0].mesh.RecalculateBounds();
			}
			if (_isTrail && _parent.canSimulate && 0 < _particleSystem.particleCount)
			{
				_renderer.BakeTrailsMesh(s_CombineInstances[0].mesh, bakeCamera, ParticleSystemBakeMeshOptions.BakeRotationAndScale);
			}
			else if (!_isTrail && _renderer.CanBakeMesh())
			{
				_particleSystem.ValidateShape();
				_renderer.BakeMesh(s_CombineInstances[0].mesh, bakeCamera, ParticleSystemBakeMeshOptions.BakeRotationAndScale);
			}
			if (65535 <= s_CombineInstances[0].mesh.vertexCount)
			{
				UnityEngine.Debug.LogErrorFormat(this, "Too many vertices to render. index={0}, isTrail={1}, vertexCount={2}(>=65535)", _index, _isTrail, s_CombineInstances[0].mesh.vertexCount);
				s_CombineInstances[0].mesh.Clear(keepVertexLayout: false);
			}
			if (_parent.canSimulate)
			{
				if (_parent.positionMode == UIParticle.PositionMode.Absolute)
				{
					s_CombineInstances[0].transform = base.canvasRenderer.transform.worldToLocalMatrix * GetWorldMatrix(position, worldScale);
				}
				else
				{
					Vector3 self = _particleSystem.transform.position - _parent.transform.position;
					s_CombineInstances[0].transform = base.canvasRenderer.transform.worldToLocalMatrix * Matrix4x4.Translate(self.GetScaled(worldScale - Vector3.one)) * GetWorldMatrix(position, worldScale);
				}
				Graphic.workerMesh.CombineMeshes(s_CombineInstances, mergeSubMeshes: true, useMatrices: true);
				Graphic.workerMesh.RecalculateBounds();
				Bounds bounds = Graphic.workerMesh.bounds;
				Vector3 center = bounds.center;
				center.z = 0f;
				bounds.center = center;
				Vector3 extents = bounds.extents;
				extents.z = 0f;
				bounds.extents = extents;
				Graphic.workerMesh.bounds = bounds;
				_lastBounds = bounds;
				if (UIParticleProjectSettings.enableLinearToGamma && base.canvas.ShouldGammaToLinearInMesh())
				{
					Graphic.workerMesh.LinearToGamma();
				}
				List<UnityEngine.Component> toRelease = InternalListPool<UnityEngine.Component>.Rent();
				GetComponents(typeof(IMeshModifier), toRelease);
				for (int i = 0; i < toRelease.Count; i++)
				{
					((IMeshModifier)toRelease[i]).ModifyMesh(Graphic.workerMesh);
				}
				InternalListPool<UnityEngine.Component>.Return(ref toRelease);
			}
			UpdateMaterialProperties();
			List<UIParticleRenderer> toRelease2 = InternalListPool<UIParticleRenderer>.Rent();
			if (_parent.useMeshSharing)
			{
				UIParticleUpdater.GetGroupedRenderers(_parent.groupId, _index, toRelease2);
			}
			for (int j = 0; j < toRelease2.Count; j++)
			{
				UIParticleRenderer uIParticleRenderer = toRelease2[j];
				if (!(uIParticleRenderer == this))
				{
					uIParticleRenderer.canvasRenderer.SetMesh(Graphic.workerMesh);
					uIParticleRenderer._lastBounds = _lastBounds;
					uIParticleRenderer.canvasRenderer.materialCount = 1;
					uIParticleRenderer.canvasRenderer.SetMaterial(materialForRendering, 0);
				}
			}
			InternalListPool<UIParticleRenderer>.Return(ref toRelease2);
			if (_parent.canRender)
			{
				base.canvasRenderer.SetMesh(Graphic.workerMesh);
			}
			else
			{
				Graphic.workerMesh.Clear();
			}
		}

		public override void SetMaterialDirty()
		{
			_materialForRendering = null;
			base.SetMaterialDirty();
		}

		protected override void UpdateGeometry()
		{
		}

		public override void Cull(Rect clipRect, bool validRect)
		{
			bool flag = _lastBounds.extents == Vector3.zero || !validRect || !clipRect.Overlaps(rootCanvasRect, allowInverse: true);
			if (base.canvasRenderer.cull != flag)
			{
				base.canvasRenderer.cull = flag;
				UISystemProfilerApi.AddMarker("MaskableGraphic.cullingChanged", this);
				base.onCullStateChanged.Invoke(flag);
				OnCullingChanged();
			}
		}

		private Vector3 GetWorldScale()
		{
			Vector3 scaled = _parent.scale3DForCalc.GetScaled(_parent.parentScale);
			if (_parent.autoScalingMode == UIParticle.AutoScalingMode.UIParticle && _particleSystem.main.scalingMode == ParticleSystemScalingMode.Local && (bool)_parent.canvas)
			{
				scaled = scaled.GetScaled(_parent.canvas.rootCanvas.transform.localScale);
			}
			return scaled;
		}

		private Matrix4x4 GetWorldMatrix(Vector3 psPos, Vector3 scale)
		{
			ParticleSystemSimulationSpace particleSystemSimulationSpace = _particleSystem.GetActualSimulationSpace();
			if (_isTrail && _particleSystem.trails.worldSpace)
			{
				particleSystemSimulationSpace = ParticleSystemSimulationSpace.World;
			}
			switch (particleSystemSimulationSpace)
			{
			case ParticleSystemSimulationSpace.Local:
				return Matrix4x4.Translate(psPos) * Matrix4x4.Scale(scale);
			case ParticleSystemSimulationSpace.World:
				if (_isTrail)
				{
					return Matrix4x4.Translate(psPos) * Matrix4x4.Scale(scale) * Matrix4x4.Translate(-psPos);
				}
				if ((bool)_mainEmitter)
				{
					if (_mainEmitter.IsLocalSpace())
					{
						return Matrix4x4.Translate(psPos) * Matrix4x4.Scale(scale) * Matrix4x4.Translate(-psPos);
					}
					psPos = _particleSystem.transform.position - _mainEmitter.transform.position;
					return Matrix4x4.Translate(psPos) * Matrix4x4.Scale(scale) * Matrix4x4.Translate(-psPos);
				}
				return Matrix4x4.Scale(scale);
			case ParticleSystemSimulationSpace.Custom:
				return Matrix4x4.Translate(_particleSystem.main.customSimulationSpace.position.GetScaled(scale)) * Matrix4x4.Scale(scale);
			default:
				throw new NotSupportedException();
			}
		}

		private void ResolveResolutionChange(Vector3 psPos, Vector3 scale)
		{
			Vector2Int vector2Int = new Vector2Int(Screen.width, Screen.height);
			bool flag = _particleSystem.IsWorldSpace();
			float b = (_parent.canvas ? _parent.canvas.scaleFactor : 1f);
			if ((_prevScreenSize != vector2Int || !Mathf.Approximately(_prevCanvasScale, b)) && flag && _isPrevStored)
			{
				int particleCount = _particleSystem.particleCount;
				ParticleSystem.Particle[] particleArray = ParticleSystemExtensions.GetParticleArray(particleCount);
				_particleSystem.GetParticles(particleArray, particleCount);
				Vector3 scaled = psPos.GetScaled(scale.Inverse(), _prevPsPos.Inverse(), _prevScale);
				for (int i = 0; i < particleCount; i++)
				{
					ParticleSystem.Particle particle = particleArray[i];
					particle.position = particle.position.GetScaled(scaled);
					particleArray[i] = particle;
				}
				_particleSystem.SetParticles(particleArray, particleCount);
				_delay = true;
				_prevScale = scale;
				_prevPsPos = psPos;
				_isPrevStored = true;
			}
			_prevCanvasScale = (base.canvas ? base.canvas.scaleFactor : 1f);
			_prevScreenSize = vector2Int;
		}

		private void Simulate(Vector3 scale, bool paused)
		{
			ParticleSystem.MainModule main = _particleSystem.main;
			float num = (paused ? 0f : (main.useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime));
			num *= _parent.timeScaleMultiplier;
			if (0f < num && _preWarm)
			{
				num += main.duration;
				_preWarm = false;
			}
			bool flag = _particleSystem.IsLocalSpace();
			Transform transform = _particleSystem.transform;
			Vector3 localPosition = transform.localPosition;
			Quaternion localRotation = transform.localRotation;
			Vector3 position = transform.position;
			Quaternion rotation = transform.rotation;
			ParticleSystem.EmissionModule emission = _particleSystem.emission;
			if (emission.enabled && 0f < emission.rateOverDistance.constant && 0f < emission.rateOverDistanceMultiplier && !paused && _isPrevStored)
			{
				Vector3 position2 = (flag ? _prevPsPos : _prevPsPos.GetScaled(_prevScale.Inverse()));
				transform.SetPositionAndRotation(position2, rotation);
				_particleSystem.Simulate(0f, withChildren: false, restart: false, fixedTimeStep: false);
			}
			Vector3 position3 = (flag ? position : position.GetScaled(scale.Inverse()));
			transform.SetPositionAndRotation(position3, rotation);
			_particleSystem.Simulate(num, withChildren: false, restart: false, fixedTimeStep: false);
			transform.localPosition = localPosition;
			transform.localRotation = localRotation;
		}

		private void UpdateMaterialProperties()
		{
			if (_parent.m_AnimatableProperties.Length == 0)
			{
				return;
			}
			if (s_Mpb == null)
			{
				s_Mpb = new MaterialPropertyBlock();
			}
			_renderer.GetPropertyBlock(s_Mpb);
			if (!s_Mpb.isEmpty && (bool)materialForRendering)
			{
				for (int i = 0; i < _parent.m_AnimatableProperties.Length; i++)
				{
					_parent.m_AnimatableProperties[i].UpdateMaterialProperties(materialForRendering, s_Mpb);
				}
				s_Mpb.Clear();
			}
		}
	}
	internal static class UIParticleUpdater
	{
		private static readonly List<UIParticle> s_ActiveParticles = new List<UIParticle>();

		private static readonly List<UIParticleAttractor> s_ActiveAttractors = new List<UIParticleAttractor>();

		private static readonly HashSet<int> s_UpdatedGroupIds = new HashSet<int>();

		private static int s_FrameCount;

		public static int uiParticleCount => s_ActiveParticles.Count;

		public static void Register(UIParticle particle)
		{
			if ((bool)particle)
			{
				s_ActiveParticles.Add(particle);
			}
		}

		public static void Unregister(UIParticle particle)
		{
			if ((bool)particle)
			{
				s_ActiveParticles.Remove(particle);
			}
		}

		public static void Register(UIParticleAttractor attractor)
		{
			if ((bool)attractor)
			{
				s_ActiveAttractors.Add(attractor);
			}
		}

		public static void Unregister(UIParticleAttractor attractor)
		{
			if ((bool)attractor)
			{
				s_ActiveAttractors.Remove(attractor);
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeOnLoad()
		{
			UIExtraCallbacks.onAfterCanvasRebuild += Refresh;
		}

		private static void Refresh()
		{
			if (s_FrameCount == Time.frameCount)
			{
				return;
			}
			s_FrameCount = Time.frameCount;
			for (int i = 0; i < s_ActiveParticles.Count; i++)
			{
				UIParticle uIParticle = s_ActiveParticles[i];
				if ((bool)uIParticle && (bool)uIParticle.canvas && uIParticle.isPrimary && s_UpdatedGroupIds.Add(uIParticle.groupId))
				{
					uIParticle.UpdateTransformScale();
					uIParticle.UpdateRenderers();
				}
			}
			for (int j = 0; j < s_ActiveParticles.Count; j++)
			{
				UIParticle uIParticle2 = s_ActiveParticles[j];
				if ((bool)uIParticle2 && (bool)uIParticle2.canvas)
				{
					uIParticle2.UpdateTransformScale();
					if (!uIParticle2.useMeshSharing)
					{
						uIParticle2.UpdateRenderers();
					}
					else if (s_UpdatedGroupIds.Add(uIParticle2.groupId))
					{
						uIParticle2.UpdateRenderers();
					}
				}
			}
			s_UpdatedGroupIds.Clear();
			for (int k = 0; k < s_ActiveAttractors.Count; k++)
			{
				s_ActiveAttractors[k].Attract();
			}
		}

		public static void GetGroupedRenderers(int groupId, int index, List<UIParticleRenderer> results)
		{
			results.Clear();
			for (int i = 0; i < s_ActiveParticles.Count; i++)
			{
				UIParticle uIParticle = s_ActiveParticles[i];
				if (uIParticle.useMeshSharing && uIParticle.groupId == groupId)
				{
					results.Add(uIParticle.GetRenderer(index));
				}
			}
		}

		internal static UIParticle GetPrimary(int groupId)
		{
			UIParticle uIParticle = null;
			for (int i = 0; i < s_ActiveParticles.Count; i++)
			{
				UIParticle uIParticle2 = s_ActiveParticles[i];
				if (uIParticle2.useMeshSharing && uIParticle2.groupId == groupId)
				{
					if (uIParticle2.isPrimary)
					{
						return uIParticle2;
					}
					if (!uIParticle && uIParticle2.canSimulate)
					{
						uIParticle = uIParticle2;
					}
				}
			}
			return uIParticle;
		}
	}
}
