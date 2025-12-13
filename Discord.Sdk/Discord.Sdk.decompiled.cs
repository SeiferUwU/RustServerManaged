using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using AOT;
using Microsoft.CodeAnalysis;
using UnityEngine;
using UnityEngine.LowLevel;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace Microsoft.CodeAnalysis
{
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	internal sealed class EmbeddedAttribute : Attribute
	{
	}
}
namespace System.Runtime.CompilerServices
{
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
	internal sealed class NullableAttribute : Attribute
	{
		public readonly byte[] NullableFlags;

		public NullableAttribute(byte P_0)
		{
			NullableFlags = new byte[1] { P_0 };
		}

		public NullableAttribute(byte[] P_0)
		{
			NullableFlags = P_0;
		}
	}
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	internal sealed class NullableContextAttribute : Attribute
	{
		public readonly byte Flag;

		public NullableContextAttribute(byte P_0)
		{
			Flag = P_0;
		}
	}
}
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
			FilePathsData = new byte[59]
			{
				0, 0, 0, 71, 0, 0, 0, 51, 92, 80,
				97, 99, 107, 97, 103, 101, 115, 92, 99, 111,
				109, 46, 100, 105, 115, 99, 111, 114, 100, 46,
				112, 97, 114, 116, 110, 101, 114, 115, 100, 107,
				92, 82, 117, 110, 116, 105, 109, 101, 92, 68,
				105, 115, 99, 111, 114, 100, 46, 99, 115
			},
			TypesData = new byte[2887]
			{
				0, 0, 0, 0, 25, 68, 105, 115, 99, 111,
				114, 100, 46, 83, 100, 107, 124, 78, 97, 116,
				105, 118, 101, 77, 101, 116, 104, 111, 100, 115,
				0, 0, 0, 0, 41, 68, 105, 115, 99, 111,
				114, 100, 46, 83, 100, 107, 46, 78, 97, 116,
				105, 118, 101, 77, 101, 116, 104, 111, 100, 115,
				124, 77, 97, 110, 97, 103, 101, 100, 85, 115,
				101, 114, 68, 97, 116, 97, 0, 0, 0, 0,
				40, 68, 105, 115, 99, 111, 114, 100, 46, 83,
				100, 107, 46, 78, 97, 116, 105, 118, 101, 77,
				101, 116, 104, 111, 100, 115, 124, 68, 105, 115,
				99, 111, 114, 100, 95, 83, 116, 114, 105, 110,
				103, 0, 0, 0, 0, 52, 68, 105, 115, 99,
				111, 114, 100, 46, 83, 100, 107, 46, 78, 97,
				116, 105, 118, 101, 77, 101, 116, 104, 111, 100,
				115, 124, 68, 105, 115, 99, 111, 114, 100, 95,
				65, 99, 116, 105, 118, 105, 116, 121, 66, 117,
				116, 116, 111, 110, 83, 112, 97, 110, 0, 0,
				0, 0, 44, 68, 105, 115, 99, 111, 114, 100,
				46, 83, 100, 107, 46, 78, 97, 116, 105, 118,
				101, 77, 101, 116, 104, 111, 100, 115, 124, 68,
				105, 115, 99, 111, 114, 100, 95, 85, 73, 110,
				116, 54, 52, 83, 112, 97, 110, 0, 0, 0,
				0, 55, 68, 105, 115, 99, 111, 114, 100, 46,
				83, 100, 107, 46, 78, 97, 116, 105, 118, 101,
				77, 101, 116, 104, 111, 100, 115, 124, 68, 105,
				115, 99, 111, 114, 100, 95, 76, 111, 98, 98,
				121, 77, 101, 109, 98, 101, 114, 72, 97, 110,
				100, 108, 101, 83, 112, 97, 110, 0, 0, 0,
				0, 42, 68, 105, 115, 99, 111, 114, 100, 46,
				83, 100, 107, 46, 78, 97, 116, 105, 118, 101,
				77, 101, 116, 104, 111, 100, 115, 124, 68, 105,
				115, 99, 111, 114, 100, 95, 67, 97, 108, 108,
				83, 112, 97, 110, 0, 0, 0, 0, 49, 68,
				105, 115, 99, 111, 114, 100, 46, 83, 100, 107,
				46, 78, 97, 116, 105, 118, 101, 77, 101, 116,
				104, 111, 100, 115, 124, 68, 105, 115, 99, 111,
				114, 100, 95, 65, 117, 100, 105, 111, 68, 101,
				118, 105, 99, 101, 83, 112, 97, 110, 0, 0,
				0, 0, 50, 68, 105, 115, 99, 111, 114, 100,
				46, 83, 100, 107, 46, 78, 97, 116, 105, 118,
				101, 77, 101, 116, 104, 111, 100, 115, 124, 68,
				105, 115, 99, 111, 114, 100, 95, 71, 117, 105,
				108, 100, 67, 104, 97, 110, 110, 101, 108, 83,
				112, 97, 110, 0, 0, 0, 0, 50, 68, 105,
				115, 99, 111, 114, 100, 46, 83, 100, 107, 46,
				78, 97, 116, 105, 118, 101, 77, 101, 116, 104,
				111, 100, 115, 124, 68, 105, 115, 99, 111, 114,
				100, 95, 71, 117, 105, 108, 100, 77, 105, 110,
				105, 109, 97, 108, 83, 112, 97, 110, 0, 0,
				0, 0, 56, 68, 105, 115, 99, 111, 114, 100,
				46, 83, 100, 107, 46, 78, 97, 116, 105, 118,
				101, 77, 101, 116, 104, 111, 100, 115, 124, 68,
				105, 115, 99, 111, 114, 100, 95, 82, 101, 108,
				97, 116, 105, 111, 110, 115, 104, 105, 112, 72,
				97, 110, 100, 108, 101, 83, 112, 97, 110, 0,
				0, 0, 0, 48, 68, 105, 115, 99, 111, 114,
				100, 46, 83, 100, 107, 46, 78, 97, 116, 105,
				118, 101, 77, 101, 116, 104, 111, 100, 115, 124,
				68, 105, 115, 99, 111, 114, 100, 95, 85, 115,
				101, 114, 72, 97, 110, 100, 108, 101, 83, 112,
				97, 110, 0, 0, 0, 0, 44, 68, 105, 115,
				99, 111, 114, 100, 46, 83, 100, 107, 46, 78,
				97, 116, 105, 118, 101, 77, 101, 116, 104, 111,
				100, 115, 124, 68, 105, 115, 99, 111, 114, 100,
				95, 80, 114, 111, 112, 101, 114, 116, 105, 101,
				115, 0, 0, 0, 0, 40, 68, 105, 115, 99,
				111, 114, 100, 46, 83, 100, 107, 46, 78, 97,
				116, 105, 118, 101, 77, 101, 116, 104, 111, 100,
				115, 124, 65, 99, 116, 105, 118, 105, 116, 121,
				73, 110, 118, 105, 116, 101, 0, 0, 0, 0,
				40, 68, 105, 115, 99, 111, 114, 100, 46, 83,
				100, 107, 46, 78, 97, 116, 105, 118, 101, 77,
				101, 116, 104, 111, 100, 115, 124, 65, 99, 116,
				105, 118, 105, 116, 121, 65, 115, 115, 101, 116,
				115, 0, 0, 0, 0, 44, 68, 105, 115, 99,
				111, 114, 100, 46, 83, 100, 107, 46, 78, 97,
				116, 105, 118, 101, 77, 101, 116, 104, 111, 100,
				115, 124, 65, 99, 116, 105, 118, 105, 116, 121,
				84, 105, 109, 101, 115, 116, 97, 109, 112, 115,
				0, 0, 0, 0, 39, 68, 105, 115, 99, 111,
				114, 100, 46, 83, 100, 107, 46, 78, 97, 116,
				105, 118, 101, 77, 101, 116, 104, 111, 100, 115,
				124, 65, 99, 116, 105, 118, 105, 116, 121, 80,
				97, 114, 116, 121, 0, 0, 0, 0, 41, 68,
				105, 115, 99, 111, 114, 100, 46, 83, 100, 107,
				46, 78, 97, 116, 105, 118, 101, 77, 101, 116,
				104, 111, 100, 115, 124, 65, 99, 116, 105, 118,
				105, 116, 121, 83, 101, 99, 114, 101, 116, 115,
				0, 0, 0, 0, 40, 68, 105, 115, 99, 111,
				114, 100, 46, 83, 100, 107, 46, 78, 97, 116,
				105, 118, 101, 77, 101, 116, 104, 111, 100, 115,
				124, 65, 99, 116, 105, 118, 105, 116, 121, 66,
				117, 116, 116, 111, 110, 0, 0, 0, 0, 34,
				68, 105, 115, 99, 111, 114, 100, 46, 83, 100,
				107, 46, 78, 97, 116, 105, 118, 101, 77, 101,
				116, 104, 111, 100, 115, 124, 65, 99, 116, 105,
				118, 105, 116, 121, 0, 0, 0, 0, 38, 68,
				105, 115, 99, 111, 114, 100, 46, 83, 100, 107,
				46, 78, 97, 116, 105, 118, 101, 77, 101, 116,
				104, 111, 100, 115, 124, 67, 108, 105, 101, 110,
				116, 82, 101, 115, 117, 108, 116, 0, 0, 0,
				0, 52, 68, 105, 115, 99, 111, 114, 100, 46,
				83, 100, 107, 46, 78, 97, 116, 105, 118, 101,
				77, 101, 116, 104, 111, 100, 115, 124, 65, 117,
				116, 104, 111, 114, 105, 122, 97, 116, 105, 111,
				110, 67, 111, 100, 101, 67, 104, 97, 108, 108,
				101, 110, 103, 101, 0, 0, 0, 0, 51, 68,
				105, 115, 99, 111, 114, 100, 46, 83, 100, 107,
				46, 78, 97, 116, 105, 118, 101, 77, 101, 116,
				104, 111, 100, 115, 124, 65, 117, 116, 104, 111,
				114, 105, 122, 97, 116, 105, 111, 110, 67, 111,
				100, 101, 86, 101, 114, 105, 102, 105, 101, 114,
				0, 0, 0, 0, 43, 68, 105, 115, 99, 111,
				114, 100, 46, 83, 100, 107, 46, 78, 97, 116,
				105, 118, 101, 77, 101, 116, 104, 111, 100, 115,
				124, 65, 117, 116, 104, 111, 114, 105, 122, 97,
				116, 105, 111, 110, 65, 114, 103, 115, 0, 0,
				0, 0, 49, 68, 105, 115, 99, 111, 114, 100,
				46, 83, 100, 107, 46, 78, 97, 116, 105, 118,
				101, 77, 101, 116, 104, 111, 100, 115, 124, 68,
				101, 118, 105, 99, 101, 65, 117, 116, 104, 111,
				114, 105, 122, 97, 116, 105, 111, 110, 65, 114,
				103, 115, 0, 0, 0, 0, 42, 68, 105, 115,
				99, 111, 114, 100, 46, 83, 100, 107, 46, 78,
				97, 116, 105, 118, 101, 77, 101, 116, 104, 111,
				100, 115, 124, 86, 111, 105, 99, 101, 83, 116,
				97, 116, 101, 72, 97, 110, 100, 108, 101, 0,
				0, 0, 0, 46, 68, 105, 115, 99, 111, 114,
				100, 46, 83, 100, 107, 46, 78, 97, 116, 105,
				118, 101, 77, 101, 116, 104, 111, 100, 115, 124,
				86, 65, 68, 84, 104, 114, 101, 115, 104, 111,
				108, 100, 83, 101, 116, 116, 105, 110, 103, 115,
				0, 0, 0, 0, 30, 68, 105, 115, 99, 111,
				114, 100, 46, 83, 100, 107, 46, 78, 97, 116,
				105, 118, 101, 77, 101, 116, 104, 111, 100, 115,
				124, 67, 97, 108, 108, 0, 0, 0, 0, 39,
				68, 105, 115, 99, 111, 114, 100, 46, 83, 100,
				107, 46, 78, 97, 116, 105, 118, 101, 77, 101,
				116, 104, 111, 100, 115, 124, 67, 104, 97, 110,
				110, 101, 108, 72, 97, 110, 100, 108, 101, 0,
				0, 0, 0, 38, 68, 105, 115, 99, 111, 114,
				100, 46, 83, 100, 107, 46, 78, 97, 116, 105,
				118, 101, 77, 101, 116, 104, 111, 100, 115, 124,
				71, 117, 105, 108, 100, 77, 105, 110, 105, 109,
				97, 108, 0, 0, 0, 0, 38, 68, 105, 115,
				99, 111, 114, 100, 46, 83, 100, 107, 46, 78,
				97, 116, 105, 118, 101, 77, 101, 116, 104, 111,
				100, 115, 124, 71, 117, 105, 108, 100, 67, 104,
				97, 110, 110, 101, 108, 0, 0, 0, 0, 37,
				68, 105, 115, 99, 111, 114, 100, 46, 83, 100,
				107, 46, 78, 97, 116, 105, 118, 101, 77, 101,
				116, 104, 111, 100, 115, 124, 76, 105, 110, 107,
				101, 100, 76, 111, 98, 98, 121, 0, 0, 0,
				0, 39, 68, 105, 115, 99, 111, 114, 100, 46,
				83, 100, 107, 46, 78, 97, 116, 105, 118, 101,
				77, 101, 116, 104, 111, 100, 115, 124, 76, 105,
				110, 107, 101, 100, 67, 104, 97, 110, 110, 101,
				108, 0, 0, 0, 0, 44, 68, 105, 115, 99,
				111, 114, 100, 46, 83, 100, 107, 46, 78, 97,
				116, 105, 118, 101, 77, 101, 116, 104, 111, 100,
				115, 124, 82, 101, 108, 97, 116, 105, 111, 110,
				115, 104, 105, 112, 72, 97, 110, 100, 108, 101,
				0, 0, 0, 0, 36, 68, 105, 115, 99, 111,
				114, 100, 46, 83, 100, 107, 46, 78, 97, 116,
				105, 118, 101, 77, 101, 116, 104, 111, 100, 115,
				124, 85, 115, 101, 114, 72, 97, 110, 100, 108,
				101, 0, 0, 0, 0, 43, 68, 105, 115, 99,
				111, 114, 100, 46, 83, 100, 107, 46, 78, 97,
				116, 105, 118, 101, 77, 101, 116, 104, 111, 100,
				115, 124, 76, 111, 98, 98, 121, 77, 101, 109,
				98, 101, 114, 72, 97, 110, 100, 108, 101, 0,
				0, 0, 0, 37, 68, 105, 115, 99, 111, 114,
				100, 46, 83, 100, 107, 46, 78, 97, 116, 105,
				118, 101, 77, 101, 116, 104, 111, 100, 115, 124,
				76, 111, 98, 98, 121, 72, 97, 110, 100, 108,
				101, 0, 0, 0, 0, 43, 68, 105, 115, 99,
				111, 114, 100, 46, 83, 100, 107, 46, 78, 97,
				116, 105, 118, 101, 77, 101, 116, 104, 111, 100,
				115, 124, 65, 100, 100, 105, 116, 105, 111, 110,
				97, 108, 67, 111, 110, 116, 101, 110, 116, 0,
				0, 0, 0, 39, 68, 105, 115, 99, 111, 114,
				100, 46, 83, 100, 107, 46, 78, 97, 116, 105,
				118, 101, 77, 101, 116, 104, 111, 100, 115, 124,
				77, 101, 115, 115, 97, 103, 101, 72, 97, 110,
				100, 108, 101, 0, 0, 0, 0, 37, 68, 105,
				115, 99, 111, 114, 100, 46, 83, 100, 107, 46,
				78, 97, 116, 105, 118, 101, 77, 101, 116, 104,
				111, 100, 115, 124, 65, 117, 100, 105, 111, 68,
				101, 118, 105, 99, 101, 0, 0, 0, 0, 32,
				68, 105, 115, 99, 111, 114, 100, 46, 83, 100,
				107, 46, 78, 97, 116, 105, 118, 101, 77, 101,
				116, 104, 111, 100, 115, 124, 67, 108, 105, 101,
				110, 116, 0, 0, 0, 0, 40, 68, 105, 115,
				99, 111, 114, 100, 46, 83, 100, 107, 46, 78,
				97, 116, 105, 118, 101, 77, 101, 116, 104, 111,
				100, 115, 124, 67, 97, 108, 108, 73, 110, 102,
				111, 72, 97, 110, 100, 108, 101, 0, 0, 0,
				0, 26, 68, 105, 115, 99, 111, 114, 100, 46,
				83, 100, 107, 124, 65, 99, 116, 105, 118, 105,
				116, 121, 73, 110, 118, 105, 116, 101, 0, 0,
				0, 0, 26, 68, 105, 115, 99, 111, 114, 100,
				46, 83, 100, 107, 124, 65, 99, 116, 105, 118,
				105, 116, 121, 65, 115, 115, 101, 116, 115, 0,
				0, 0, 0, 30, 68, 105, 115, 99, 111, 114,
				100, 46, 83, 100, 107, 124, 65, 99, 116, 105,
				118, 105, 116, 121, 84, 105, 109, 101, 115, 116,
				97, 109, 112, 115, 0, 0, 0, 0, 25, 68,
				105, 115, 99, 111, 114, 100, 46, 83, 100, 107,
				124, 65, 99, 116, 105, 118, 105, 116, 121, 80,
				97, 114, 116, 121, 0, 0, 0, 0, 27, 68,
				105, 115, 99, 111, 114, 100, 46, 83, 100, 107,
				124, 65, 99, 116, 105, 118, 105, 116, 121, 83,
				101, 99, 114, 101, 116, 115, 0, 0, 0, 0,
				26, 68, 105, 115, 99, 111, 114, 100, 46, 83,
				100, 107, 124, 65, 99, 116, 105, 118, 105, 116,
				121, 66, 117, 116, 116, 111, 110, 0, 0, 0,
				0, 20, 68, 105, 115, 99, 111, 114, 100, 46,
				83, 100, 107, 124, 65, 99, 116, 105, 118, 105,
				116, 121, 0, 0, 0, 0, 24, 68, 105, 115,
				99, 111, 114, 100, 46, 83, 100, 107, 124, 67,
				108, 105, 101, 110, 116, 82, 101, 115, 117, 108,
				116, 0, 0, 0, 0, 38, 68, 105, 115, 99,
				111, 114, 100, 46, 83, 100, 107, 124, 65, 117,
				116, 104, 111, 114, 105, 122, 97, 116, 105, 111,
				110, 67, 111, 100, 101, 67, 104, 97, 108, 108,
				101, 110, 103, 101, 0, 0, 0, 0, 37, 68,
				105, 115, 99, 111, 114, 100, 46, 83, 100, 107,
				124, 65, 117, 116, 104, 111, 114, 105, 122, 97,
				116, 105, 111, 110, 67, 111, 100, 101, 86, 101,
				114, 105, 102, 105, 101, 114, 0, 0, 0, 0,
				29, 68, 105, 115, 99, 111, 114, 100, 46, 83,
				100, 107, 124, 65, 117, 116, 104, 111, 114, 105,
				122, 97, 116, 105, 111, 110, 65, 114, 103, 115,
				0, 0, 0, 0, 35, 68, 105, 115, 99, 111,
				114, 100, 46, 83, 100, 107, 124, 68, 101, 118,
				105, 99, 101, 65, 117, 116, 104, 111, 114, 105,
				122, 97, 116, 105, 111, 110, 65, 114, 103, 115,
				0, 0, 0, 0, 28, 68, 105, 115, 99, 111,
				114, 100, 46, 83, 100, 107, 124, 86, 111, 105,
				99, 101, 83, 116, 97, 116, 101, 72, 97, 110,
				100, 108, 101, 0, 0, 0, 0, 32, 68, 105,
				115, 99, 111, 114, 100, 46, 83, 100, 107, 124,
				86, 65, 68, 84, 104, 114, 101, 115, 104, 111,
				108, 100, 83, 101, 116, 116, 105, 110, 103, 115,
				0, 0, 0, 0, 16, 68, 105, 115, 99, 111,
				114, 100, 46, 83, 100, 107, 124, 67, 97, 108,
				108, 0, 0, 0, 0, 25, 68, 105, 115, 99,
				111, 114, 100, 46, 83, 100, 107, 124, 67, 104,
				97, 110, 110, 101, 108, 72, 97, 110, 100, 108,
				101, 0, 0, 0, 0, 24, 68, 105, 115, 99,
				111, 114, 100, 46, 83, 100, 107, 124, 71, 117,
				105, 108, 100, 77, 105, 110, 105, 109, 97, 108,
				0, 0, 0, 0, 24, 68, 105, 115, 99, 111,
				114, 100, 46, 83, 100, 107, 124, 71, 117, 105,
				108, 100, 67, 104, 97, 110, 110, 101, 108, 0,
				0, 0, 0, 23, 68, 105, 115, 99, 111, 114,
				100, 46, 83, 100, 107, 124, 76, 105, 110, 107,
				101, 100, 76, 111, 98, 98, 121, 0, 0, 0,
				0, 25, 68, 105, 115, 99, 111, 114, 100, 46,
				83, 100, 107, 124, 76, 105, 110, 107, 101, 100,
				67, 104, 97, 110, 110, 101, 108, 0, 0, 0,
				0, 30, 68, 105, 115, 99, 111, 114, 100, 46,
				83, 100, 107, 124, 82, 101, 108, 97, 116, 105,
				111, 110, 115, 104, 105, 112, 72, 97, 110, 100,
				108, 101, 0, 0, 0, 0, 22, 68, 105, 115,
				99, 111, 114, 100, 46, 83, 100, 107, 124, 85,
				115, 101, 114, 72, 97, 110, 100, 108, 101, 0,
				0, 0, 0, 29, 68, 105, 115, 99, 111, 114,
				100, 46, 83, 100, 107, 124, 76, 111, 98, 98,
				121, 77, 101, 109, 98, 101, 114, 72, 97, 110,
				100, 108, 101, 0, 0, 0, 0, 23, 68, 105,
				115, 99, 111, 114, 100, 46, 83, 100, 107, 124,
				76, 111, 98, 98, 121, 72, 97, 110, 100, 108,
				101, 0, 0, 0, 0, 29, 68, 105, 115, 99,
				111, 114, 100, 46, 83, 100, 107, 124, 65, 100,
				100, 105, 116, 105, 111, 110, 97, 108, 67, 111,
				110, 116, 101, 110, 116, 0, 0, 0, 0, 25,
				68, 105, 115, 99, 111, 114, 100, 46, 83, 100,
				107, 124, 77, 101, 115, 115, 97, 103, 101, 72,
				97, 110, 100, 108, 101, 0, 0, 0, 0, 23,
				68, 105, 115, 99, 111, 114, 100, 46, 83, 100,
				107, 124, 65, 117, 100, 105, 111, 68, 101, 118,
				105, 99, 101, 0, 0, 0, 0, 18, 68, 105,
				115, 99, 111, 114, 100, 46, 83, 100, 107, 124,
				67, 108, 105, 101, 110, 116, 0, 0, 0, 0,
				26, 68, 105, 115, 99, 111, 114, 100, 46, 83,
				100, 107, 124, 67, 97, 108, 108, 73, 110, 102,
				111, 72, 97, 110, 100, 108, 101
			},
			TotalFiles = 1,
			TotalTypes = 71,
			IsEditorOnly = false
		};
	}
}
namespace Discord.Sdk
{
	public enum ActivityActionTypes
	{
		Join = 1,
		JoinRequest = 5
	}
	public enum ActivityPartyPrivacy
	{
		Private,
		Public
	}
	public enum ActivityTypes
	{
		Playing,
		Streaming,
		Listening,
		Watching,
		CustomStatus,
		Competing,
		HangStatus
	}
	[Flags]
	public enum ActivityGamePlatforms
	{
		Desktop = 1,
		Xbox = 2,
		Samsung = 4,
		IOS = 8,
		Android = 0x10,
		Embedded = 0x20,
		PS4 = 0x40,
		PS5 = 0x80
	}
	public enum ErrorType
	{
		None,
		NetworkError,
		HTTPError,
		ClientNotReady,
		Disabled,
		ClientDestroyed,
		ValidationError,
		Aborted,
		AuthorizationFailed,
		RPCError
	}
	public enum HttpStatusCode
	{
		None = 0,
		Continue = 100,
		SwitchingProtocols = 101,
		Processing = 102,
		EarlyHints = 103,
		Ok = 200,
		Created = 201,
		Accepted = 202,
		NonAuthoritativeInfo = 203,
		NoContent = 204,
		ResetContent = 205,
		PartialContent = 206,
		MultiStatus = 207,
		AlreadyReported = 208,
		ImUsed = 209,
		MultipleChoices = 300,
		MovedPermanently = 301,
		Found = 302,
		SeeOther = 303,
		NotModified = 304,
		TemporaryRedirect = 307,
		PermanentRedirect = 308,
		BadRequest = 400,
		Unauthorized = 401,
		PaymentRequired = 402,
		Forbidden = 403,
		NotFound = 404,
		MethodNotAllowed = 405,
		NotAcceptable = 406,
		ProxyAuthRequired = 407,
		RequestTimeout = 408,
		Conflict = 409,
		Gone = 410,
		LengthRequired = 411,
		PreconditionFailed = 412,
		PayloadTooLarge = 413,
		UriTooLong = 414,
		UnsupportedMediaType = 415,
		RangeNotSatisfiable = 416,
		ExpectationFailed = 417,
		MisdirectedRequest = 421,
		UnprocessableEntity = 422,
		Locked = 423,
		FailedDependency = 424,
		TooEarly = 425,
		UpgradeRequired = 426,
		PreconditionRequired = 428,
		TooManyRequests = 429,
		RequestHeaderFieldsTooLarge = 431,
		InternalServerError = 500,
		NotImplemented = 501,
		BadGateway = 502,
		ServiceUnavailable = 503,
		GatewayTimeout = 504,
		HttpVersionNotSupported = 505,
		VariantAlsoNegotiates = 506,
		InsufficientStorage = 507,
		LoopDetected = 508,
		NotExtended = 510,
		NetworkAuthorizationRequired = 511
	}
	public enum AuthenticationCodeChallengeMethod
	{
		S256
	}
	public enum AdditionalContentType
	{
		Other,
		Attachment,
		Poll,
		VoiceMessage,
		Thread,
		Embed,
		Sticker
	}
	public enum AudioModeType
	{
		MODE_UNINIT,
		MODE_VAD,
		MODE_PTT
	}
	public enum ChannelType
	{
		GuildText = 0,
		Dm = 1,
		GuildVoice = 2,
		GroupDm = 3,
		GuildCategory = 4,
		GuildNews = 5,
		GuildStore = 6,
		GuildNewsThread = 10,
		GuildPublicThread = 11,
		GuildPrivateThread = 12,
		GuildStageVoice = 13,
		GuildDirectory = 14,
		GuildForum = 15,
		GuildMedia = 16,
		Lobby = 17,
		EphemeralDm = 18
	}
	public enum RelationshipType
	{
		None,
		Friend,
		Blocked,
		PendingIncoming,
		PendingOutgoing,
		Implicit,
		Suggestion
	}
	public enum StatusType
	{
		Online,
		Offline,
		Blocked,
		Idle,
		Dnd,
		Invisible,
		Streaming,
		Unknown
	}
	public enum DisclosureTypes
	{
		MessageDataVisibleOnDiscord = 3
	}
	public enum AuthorizationTokenType
	{
		User,
		Bearer
	}
	public enum AuthenticationExternalAuthType
	{
		OIDC,
		EpicOnlineServicesAccessToken,
		EpicOnlineServicesIdToken,
		SteamSessionTicket,
		UnityServicesIdToken
	}
	public enum LoggingSeverity
	{
		Verbose = 1,
		Info,
		Warning,
		Error,
		None
	}
	public static class NativeMethods
	{
		public unsafe delegate void Discord_FreeFn(void* ptr);

		internal class ManagedUserData
		{
			public Delegate managedCallback;

			public unsafe static void* Free;

			public ManagedUserData(Delegate managedCallback)
			{
				this.managedCallback = managedCallback;
			}

			unsafe static ManagedUserData()
			{
				Free = (void*)Marshal.GetFunctionPointerForDelegate<Discord_FreeFn>(UnmanagedFree);
			}

			[MonoPInvokeCallback(typeof(Discord_FreeFn))]
			public unsafe static void UnmanagedFree(void* userData)
			{
				GCHandle.FromIntPtr((IntPtr)userData).Free();
			}

			public unsafe static T DelegateFromPointer<T>(void* userData) where T : Delegate
			{
				return (T)((ManagedUserData)GCHandle.FromIntPtr((IntPtr)userData).Target).managedCallback;
			}

			public unsafe static void* CreateHandle(Delegate cb)
			{
				return GCHandle.ToIntPtr(GCHandle.Alloc(new ManagedUserData(cb))).ToPointer();
			}
		}

		public struct Discord_String
		{
			public unsafe byte* ptr;

			public UIntPtr size;
		}

		public struct Discord_ActivityButtonSpan
		{
			public unsafe ActivityButton* ptr;

			public UIntPtr size;
		}

		public struct Discord_UInt64Span
		{
			public unsafe ulong* ptr;

			public UIntPtr size;
		}

		public struct Discord_LobbyMemberHandleSpan
		{
			public unsafe LobbyMemberHandle* ptr;

			public UIntPtr size;
		}

		public struct Discord_CallSpan
		{
			public unsafe Call* ptr;

			public UIntPtr size;
		}

		public struct Discord_AudioDeviceSpan
		{
			public unsafe AudioDevice* ptr;

			public UIntPtr size;
		}

		public struct Discord_GuildChannelSpan
		{
			public unsafe GuildChannel* ptr;

			public UIntPtr size;
		}

		public struct Discord_GuildMinimalSpan
		{
			public unsafe GuildMinimal* ptr;

			public UIntPtr size;
		}

		public struct Discord_RelationshipHandleSpan
		{
			public unsafe RelationshipHandle* ptr;

			public UIntPtr size;
		}

		public struct Discord_UserHandleSpan
		{
			public unsafe UserHandle* ptr;

			public UIntPtr size;
		}

		public struct Discord_Properties
		{
			public IntPtr size;

			public unsafe Discord_String* keys;

			public unsafe Discord_String* values;
		}

		public struct ActivityInvite
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_Init")]
			public unsafe static extern void Init(ActivityInvite* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_Drop")]
			public unsafe static extern void Drop(ActivityInvite* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_Clone")]
			public unsafe static extern void Clone(ActivityInvite* self, ActivityInvite* rhs);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_SenderId")]
			public unsafe static extern ulong SenderId(ActivityInvite* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_SetSenderId")]
			public unsafe static extern void SetSenderId(ActivityInvite* self, ulong value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_ChannelId")]
			public unsafe static extern ulong ChannelId(ActivityInvite* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_SetChannelId")]
			public unsafe static extern void SetChannelId(ActivityInvite* self, ulong value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_MessageId")]
			public unsafe static extern ulong MessageId(ActivityInvite* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_SetMessageId")]
			public unsafe static extern void SetMessageId(ActivityInvite* self, ulong value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_Type")]
			public unsafe static extern ActivityActionTypes Type(ActivityInvite* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_SetType")]
			public unsafe static extern void SetType(ActivityInvite* self, ActivityActionTypes value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_ApplicationId")]
			public unsafe static extern ulong ApplicationId(ActivityInvite* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_SetApplicationId")]
			public unsafe static extern void SetApplicationId(ActivityInvite* self, ulong value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_PartyId")]
			public unsafe static extern void PartyId(ActivityInvite* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_SetPartyId")]
			public unsafe static extern void SetPartyId(ActivityInvite* self, Discord_String value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_SessionId")]
			public unsafe static extern void SessionId(ActivityInvite* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_SetSessionId")]
			public unsafe static extern void SetSessionId(ActivityInvite* self, Discord_String value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_IsValid")]
			public unsafe static extern bool IsValid(ActivityInvite* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityInvite_SetIsValid")]
			public unsafe static extern void SetIsValid(ActivityInvite* self, bool value);
		}

		public struct ActivityAssets
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityAssets_Init")]
			public unsafe static extern void Init(ActivityAssets* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityAssets_Drop")]
			public unsafe static extern void Drop(ActivityAssets* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityAssets_Clone")]
			public unsafe static extern void Clone(ActivityAssets* self, ActivityAssets* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityAssets_LargeImage")]
			public unsafe static extern bool LargeImage(ActivityAssets* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityAssets_SetLargeImage")]
			public unsafe static extern void SetLargeImage(ActivityAssets* self, Discord_String* value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityAssets_LargeText")]
			public unsafe static extern bool LargeText(ActivityAssets* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityAssets_SetLargeText")]
			public unsafe static extern void SetLargeText(ActivityAssets* self, Discord_String* value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityAssets_SmallImage")]
			public unsafe static extern bool SmallImage(ActivityAssets* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityAssets_SetSmallImage")]
			public unsafe static extern void SetSmallImage(ActivityAssets* self, Discord_String* value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityAssets_SmallText")]
			public unsafe static extern bool SmallText(ActivityAssets* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityAssets_SetSmallText")]
			public unsafe static extern void SetSmallText(ActivityAssets* self, Discord_String* value);
		}

		public struct ActivityTimestamps
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityTimestamps_Init")]
			public unsafe static extern void Init(ActivityTimestamps* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityTimestamps_Drop")]
			public unsafe static extern void Drop(ActivityTimestamps* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityTimestamps_Clone")]
			public unsafe static extern void Clone(ActivityTimestamps* self, ActivityTimestamps* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityTimestamps_Start")]
			public unsafe static extern ulong Start(ActivityTimestamps* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityTimestamps_SetStart")]
			public unsafe static extern void SetStart(ActivityTimestamps* self, ulong value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityTimestamps_End")]
			public unsafe static extern ulong End(ActivityTimestamps* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityTimestamps_SetEnd")]
			public unsafe static extern void SetEnd(ActivityTimestamps* self, ulong value);
		}

		public struct ActivityParty
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityParty_Init")]
			public unsafe static extern void Init(ActivityParty* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityParty_Drop")]
			public unsafe static extern void Drop(ActivityParty* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityParty_Clone")]
			public unsafe static extern void Clone(ActivityParty* self, ActivityParty* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityParty_Id")]
			public unsafe static extern void Id(ActivityParty* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityParty_SetId")]
			public unsafe static extern void SetId(ActivityParty* self, Discord_String value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityParty_CurrentSize")]
			public unsafe static extern int CurrentSize(ActivityParty* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityParty_SetCurrentSize")]
			public unsafe static extern void SetCurrentSize(ActivityParty* self, int value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityParty_MaxSize")]
			public unsafe static extern int MaxSize(ActivityParty* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityParty_SetMaxSize")]
			public unsafe static extern void SetMaxSize(ActivityParty* self, int value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityParty_Privacy")]
			public unsafe static extern ActivityPartyPrivacy Privacy(ActivityParty* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityParty_SetPrivacy")]
			public unsafe static extern void SetPrivacy(ActivityParty* self, ActivityPartyPrivacy value);
		}

		public struct ActivitySecrets
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivitySecrets_Init")]
			public unsafe static extern void Init(ActivitySecrets* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivitySecrets_Drop")]
			public unsafe static extern void Drop(ActivitySecrets* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivitySecrets_Clone")]
			public unsafe static extern void Clone(ActivitySecrets* self, ActivitySecrets* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivitySecrets_Join")]
			public unsafe static extern void Join(ActivitySecrets* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivitySecrets_SetJoin")]
			public unsafe static extern void SetJoin(ActivitySecrets* self, Discord_String value);
		}

		public struct ActivityButton
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityButton_Init")]
			public unsafe static extern void Init(ActivityButton* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityButton_Drop")]
			public unsafe static extern void Drop(ActivityButton* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityButton_Clone")]
			public unsafe static extern void Clone(ActivityButton* self, ActivityButton* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityButton_Label")]
			public unsafe static extern void Label(ActivityButton* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityButton_SetLabel")]
			public unsafe static extern void SetLabel(ActivityButton* self, Discord_String value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityButton_Url")]
			public unsafe static extern void Url(ActivityButton* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ActivityButton_SetUrl")]
			public unsafe static extern void SetUrl(ActivityButton* self, Discord_String value);
		}

		public struct Activity
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_Init")]
			public unsafe static extern void Init(Activity* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_Drop")]
			public unsafe static extern void Drop(Activity* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_Clone")]
			public unsafe static extern void Clone(Activity* self, Activity* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_AddButton")]
			public unsafe static extern void AddButton(Activity* self, ActivityButton* button);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_Equals")]
			public unsafe static extern bool Equals(Activity* self, Activity* other);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_GetButtons")]
			public unsafe static extern void GetButtons(Activity* self, Discord_ActivityButtonSpan* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_Name")]
			public unsafe static extern void Name(Activity* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_SetName")]
			public unsafe static extern void SetName(Activity* self, Discord_String value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_Type")]
			public unsafe static extern ActivityTypes Type(Activity* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_SetType")]
			public unsafe static extern void SetType(Activity* self, ActivityTypes value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_State")]
			public unsafe static extern bool State(Activity* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_SetState")]
			public unsafe static extern void SetState(Activity* self, Discord_String* value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_Details")]
			public unsafe static extern bool Details(Activity* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_SetDetails")]
			public unsafe static extern void SetDetails(Activity* self, Discord_String* value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_ApplicationId")]
			public unsafe static extern bool ApplicationId(Activity* self, ulong* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_SetApplicationId")]
			public unsafe static extern void SetApplicationId(Activity* self, ulong* value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_Assets")]
			public unsafe static extern bool Assets(Activity* self, ActivityAssets* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_SetAssets")]
			public unsafe static extern void SetAssets(Activity* self, ActivityAssets* value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_Timestamps")]
			public unsafe static extern bool Timestamps(Activity* self, ActivityTimestamps* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_SetTimestamps")]
			public unsafe static extern void SetTimestamps(Activity* self, ActivityTimestamps* value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_Party")]
			public unsafe static extern bool Party(Activity* self, ActivityParty* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_SetParty")]
			public unsafe static extern void SetParty(Activity* self, ActivityParty* value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_Secrets")]
			public unsafe static extern bool Secrets(Activity* self, ActivitySecrets* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_SetSecrets")]
			public unsafe static extern void SetSecrets(Activity* self, ActivitySecrets* value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_SupportedPlatforms")]
			public unsafe static extern ActivityGamePlatforms SupportedPlatforms(Activity* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Activity_SetSupportedPlatforms")]
			public unsafe static extern void SetSupportedPlatforms(Activity* self, ActivityGamePlatforms value);
		}

		public struct ClientResult
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_Drop")]
			public unsafe static extern void Drop(ClientResult* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_Clone")]
			public unsafe static extern void Clone(ClientResult* self, ClientResult* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_ToString")]
			public unsafe static extern void ToString(ClientResult* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_Type")]
			public unsafe static extern ErrorType Type(ClientResult* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_SetType")]
			public unsafe static extern void SetType(ClientResult* self, ErrorType value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_Error")]
			public unsafe static extern void Error(ClientResult* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_SetError")]
			public unsafe static extern void SetError(ClientResult* self, Discord_String value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_ErrorCode")]
			public unsafe static extern int ErrorCode(ClientResult* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_SetErrorCode")]
			public unsafe static extern void SetErrorCode(ClientResult* self, int value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_Status")]
			public unsafe static extern HttpStatusCode Status(ClientResult* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_SetStatus")]
			public unsafe static extern void SetStatus(ClientResult* self, HttpStatusCode value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_ResponseBody")]
			public unsafe static extern void ResponseBody(ClientResult* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_SetResponseBody")]
			public unsafe static extern void SetResponseBody(ClientResult* self, Discord_String value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_Successful")]
			public unsafe static extern bool Successful(ClientResult* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_SetSuccessful")]
			public unsafe static extern void SetSuccessful(ClientResult* self, bool value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_Retryable")]
			public unsafe static extern bool Retryable(ClientResult* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_SetRetryable")]
			public unsafe static extern void SetRetryable(ClientResult* self, bool value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_RetryAfter")]
			public unsafe static extern float RetryAfter(ClientResult* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClientResult_SetRetryAfter")]
			public unsafe static extern void SetRetryAfter(ClientResult* self, float value);
		}

		public struct AuthorizationCodeChallenge
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationCodeChallenge_Init")]
			public unsafe static extern void Init(AuthorizationCodeChallenge* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationCodeChallenge_Drop")]
			public unsafe static extern void Drop(AuthorizationCodeChallenge* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationCodeChallenge_Clone")]
			public unsafe static extern void Clone(AuthorizationCodeChallenge* self, AuthorizationCodeChallenge* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationCodeChallenge_Method")]
			public unsafe static extern AuthenticationCodeChallengeMethod Method(AuthorizationCodeChallenge* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationCodeChallenge_SetMethod")]
			public unsafe static extern void SetMethod(AuthorizationCodeChallenge* self, AuthenticationCodeChallengeMethod value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationCodeChallenge_Challenge")]
			public unsafe static extern void Challenge(AuthorizationCodeChallenge* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationCodeChallenge_SetChallenge")]
			public unsafe static extern void SetChallenge(AuthorizationCodeChallenge* self, Discord_String value);
		}

		public struct AuthorizationCodeVerifier
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationCodeVerifier_Drop")]
			public unsafe static extern void Drop(AuthorizationCodeVerifier* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationCodeVerifier_Clone")]
			public unsafe static extern void Clone(AuthorizationCodeVerifier* self, AuthorizationCodeVerifier* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationCodeVerifier_Challenge")]
			public unsafe static extern void Challenge(AuthorizationCodeVerifier* self, AuthorizationCodeChallenge* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationCodeVerifier_SetChallenge")]
			public unsafe static extern void SetChallenge(AuthorizationCodeVerifier* self, AuthorizationCodeChallenge* value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationCodeVerifier_Verifier")]
			public unsafe static extern void Verifier(AuthorizationCodeVerifier* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationCodeVerifier_SetVerifier")]
			public unsafe static extern void SetVerifier(AuthorizationCodeVerifier* self, Discord_String value);
		}

		public struct AuthorizationArgs
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationArgs_Init")]
			public unsafe static extern void Init(AuthorizationArgs* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationArgs_Drop")]
			public unsafe static extern void Drop(AuthorizationArgs* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationArgs_Clone")]
			public unsafe static extern void Clone(AuthorizationArgs* self, AuthorizationArgs* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationArgs_ClientId")]
			public unsafe static extern ulong ClientId(AuthorizationArgs* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationArgs_SetClientId")]
			public unsafe static extern void SetClientId(AuthorizationArgs* self, ulong value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationArgs_Scopes")]
			public unsafe static extern void Scopes(AuthorizationArgs* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationArgs_SetScopes")]
			public unsafe static extern void SetScopes(AuthorizationArgs* self, Discord_String value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationArgs_State")]
			public unsafe static extern bool State(AuthorizationArgs* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationArgs_SetState")]
			public unsafe static extern void SetState(AuthorizationArgs* self, Discord_String* value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationArgs_Nonce")]
			public unsafe static extern bool Nonce(AuthorizationArgs* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationArgs_SetNonce")]
			public unsafe static extern void SetNonce(AuthorizationArgs* self, Discord_String* value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationArgs_CodeChallenge")]
			public unsafe static extern bool CodeChallenge(AuthorizationArgs* self, AuthorizationCodeChallenge* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AuthorizationArgs_SetCodeChallenge")]
			public unsafe static extern void SetCodeChallenge(AuthorizationArgs* self, AuthorizationCodeChallenge* value);
		}

		public struct DeviceAuthorizationArgs
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_DeviceAuthorizationArgs_Init")]
			public unsafe static extern void Init(DeviceAuthorizationArgs* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_DeviceAuthorizationArgs_Drop")]
			public unsafe static extern void Drop(DeviceAuthorizationArgs* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_DeviceAuthorizationArgs_Clone")]
			public unsafe static extern void Clone(DeviceAuthorizationArgs* self, DeviceAuthorizationArgs* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_DeviceAuthorizationArgs_ClientId")]
			public unsafe static extern ulong ClientId(DeviceAuthorizationArgs* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_DeviceAuthorizationArgs_SetClientId")]
			public unsafe static extern void SetClientId(DeviceAuthorizationArgs* self, ulong value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_DeviceAuthorizationArgs_Scopes")]
			public unsafe static extern void Scopes(DeviceAuthorizationArgs* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_DeviceAuthorizationArgs_SetScopes")]
			public unsafe static extern void SetScopes(DeviceAuthorizationArgs* self, Discord_String value);
		}

		public struct VoiceStateHandle
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_VoiceStateHandle_Drop")]
			public unsafe static extern void Drop(VoiceStateHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_VoiceStateHandle_Clone")]
			public unsafe static extern void Clone(VoiceStateHandle* self, VoiceStateHandle* other);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_VoiceStateHandle_SelfDeaf")]
			public unsafe static extern bool SelfDeaf(VoiceStateHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_VoiceStateHandle_SelfMute")]
			public unsafe static extern bool SelfMute(VoiceStateHandle* self);
		}

		public struct VADThresholdSettings
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_VADThresholdSettings_Drop")]
			public unsafe static extern void Drop(VADThresholdSettings* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_VADThresholdSettings_VadThreshold")]
			public unsafe static extern float VadThreshold(VADThresholdSettings* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_VADThresholdSettings_SetVadThreshold")]
			public unsafe static extern void SetVadThreshold(VADThresholdSettings* self, float value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_VADThresholdSettings_Automatic")]
			public unsafe static extern bool Automatic(VADThresholdSettings* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_VADThresholdSettings_SetAutomatic")]
			public unsafe static extern void SetAutomatic(VADThresholdSettings* self, bool value);
		}

		public struct Call
		{
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void OnVoiceStateChanged(ulong userId, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void OnParticipantChanged(ulong userId, bool added, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void OnSpeakingStatusChanged(ulong userId, bool isPlayingSound, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void OnStatusChanged(Discord.Sdk.Call.Status status, Discord.Sdk.Call.Error error, int errorDetail, void* __userData);

			public IntPtr Opaque0;

			public IntPtr Opaque1;

			public IntPtr Opaque2;

			[MonoPInvokeCallback(typeof(OnVoiceStateChanged))]
			public unsafe static void OnVoiceStateChanged_Handler(ulong userId, void* __userData)
			{
				Discord.Sdk.Call.OnVoiceStateChanged onVoiceStateChanged = ManagedUserData.DelegateFromPointer<Discord.Sdk.Call.OnVoiceStateChanged>(__userData);
				try
				{
					onVoiceStateChanged(userId);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(OnParticipantChanged))]
			public unsafe static void OnParticipantChanged_Handler(ulong userId, bool added, void* __userData)
			{
				Discord.Sdk.Call.OnParticipantChanged onParticipantChanged = ManagedUserData.DelegateFromPointer<Discord.Sdk.Call.OnParticipantChanged>(__userData);
				try
				{
					onParticipantChanged(userId, added);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(OnSpeakingStatusChanged))]
			public unsafe static void OnSpeakingStatusChanged_Handler(ulong userId, bool isPlayingSound, void* __userData)
			{
				Discord.Sdk.Call.OnSpeakingStatusChanged onSpeakingStatusChanged = ManagedUserData.DelegateFromPointer<Discord.Sdk.Call.OnSpeakingStatusChanged>(__userData);
				try
				{
					onSpeakingStatusChanged(userId, isPlayingSound);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(OnStatusChanged))]
			public unsafe static void OnStatusChanged_Handler(Discord.Sdk.Call.Status status, Discord.Sdk.Call.Error error, int errorDetail, void* __userData)
			{
				Discord.Sdk.Call.OnStatusChanged onStatusChanged = ManagedUserData.DelegateFromPointer<Discord.Sdk.Call.OnStatusChanged>(__userData);
				try
				{
					onStatusChanged(status, error, errorDetail);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_Drop")]
			public unsafe static extern void Drop(Call* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_Clone")]
			public unsafe static extern void Clone(Call* self, Call* other);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_ErrorToString")]
			public unsafe static extern void ErrorToString(Discord.Sdk.Call.Error type, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_GetAudioMode")]
			public unsafe static extern AudioModeType GetAudioMode(Call* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_GetChannelId")]
			public unsafe static extern ulong GetChannelId(Call* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_GetGuildId")]
			public unsafe static extern ulong GetGuildId(Call* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_GetLocalMute")]
			public unsafe static extern bool GetLocalMute(Call* self, ulong userId);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_GetParticipants")]
			public unsafe static extern void GetParticipants(Call* self, Discord_UInt64Span* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_GetParticipantVolume")]
			public unsafe static extern float GetParticipantVolume(Call* self, ulong userId);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_GetPTTActive")]
			public unsafe static extern bool GetPTTActive(Call* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_GetPTTReleaseDelay")]
			public unsafe static extern uint GetPTTReleaseDelay(Call* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_GetSelfDeaf")]
			public unsafe static extern bool GetSelfDeaf(Call* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_GetSelfMute")]
			public unsafe static extern bool GetSelfMute(Call* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_GetStatus")]
			public unsafe static extern Discord.Sdk.Call.Status GetStatus(Call* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_GetVADThreshold")]
			public unsafe static extern void GetVADThreshold(Call* self, VADThresholdSettings* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_GetVoiceStateHandle")]
			public unsafe static extern bool GetVoiceStateHandle(Call* self, ulong userId, VoiceStateHandle* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_SetAudioMode")]
			public unsafe static extern void SetAudioMode(Call* self, AudioModeType audioMode);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_SetLocalMute")]
			public unsafe static extern void SetLocalMute(Call* self, ulong userId, bool mute);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_SetOnVoiceStateChangedCallback")]
			public unsafe static extern void SetOnVoiceStateChangedCallback(Call* self, OnVoiceStateChanged cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_SetParticipantChangedCallback")]
			public unsafe static extern void SetParticipantChangedCallback(Call* self, OnParticipantChanged cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_SetParticipantVolume")]
			public unsafe static extern void SetParticipantVolume(Call* self, ulong userId, float volume);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_SetPTTActive")]
			public unsafe static extern void SetPTTActive(Call* self, bool active);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_SetPTTReleaseDelay")]
			public unsafe static extern void SetPTTReleaseDelay(Call* self, uint releaseDelayMs);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_SetSelfDeaf")]
			public unsafe static extern void SetSelfDeaf(Call* self, bool deaf);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_SetSelfMute")]
			public unsafe static extern void SetSelfMute(Call* self, bool mute);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_SetSpeakingStatusChangedCallback")]
			public unsafe static extern void SetSpeakingStatusChangedCallback(Call* self, OnSpeakingStatusChanged cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_SetStatusChangedCallback")]
			public unsafe static extern void SetStatusChangedCallback(Call* self, OnStatusChanged cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_SetVADThreshold")]
			public unsafe static extern void SetVADThreshold(Call* self, bool automatic, float threshold);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Call_StatusToString")]
			public unsafe static extern void StatusToString(Discord.Sdk.Call.Status type, Discord_String* returnValue);
		}

		public struct ChannelHandle
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ChannelHandle_Drop")]
			public unsafe static extern void Drop(ChannelHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ChannelHandle_Clone")]
			public unsafe static extern void Clone(ChannelHandle* self, ChannelHandle* other);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ChannelHandle_Id")]
			public unsafe static extern ulong Id(ChannelHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ChannelHandle_Name")]
			public unsafe static extern void Name(ChannelHandle* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ChannelHandle_Recipients")]
			public unsafe static extern void Recipients(ChannelHandle* self, Discord_UInt64Span* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ChannelHandle_Type")]
			public unsafe static extern ChannelType Type(ChannelHandle* self);
		}

		public struct GuildMinimal
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildMinimal_Drop")]
			public unsafe static extern void Drop(GuildMinimal* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildMinimal_Clone")]
			public unsafe static extern void Clone(GuildMinimal* self, GuildMinimal* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildMinimal_Id")]
			public unsafe static extern ulong Id(GuildMinimal* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildMinimal_SetId")]
			public unsafe static extern void SetId(GuildMinimal* self, ulong value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildMinimal_Name")]
			public unsafe static extern void Name(GuildMinimal* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildMinimal_SetName")]
			public unsafe static extern void SetName(GuildMinimal* self, Discord_String value);
		}

		public struct GuildChannel
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildChannel_Drop")]
			public unsafe static extern void Drop(GuildChannel* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildChannel_Clone")]
			public unsafe static extern void Clone(GuildChannel* self, GuildChannel* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildChannel_Id")]
			public unsafe static extern ulong Id(GuildChannel* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildChannel_SetId")]
			public unsafe static extern void SetId(GuildChannel* self, ulong value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildChannel_Name")]
			public unsafe static extern void Name(GuildChannel* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildChannel_SetName")]
			public unsafe static extern void SetName(GuildChannel* self, Discord_String value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildChannel_IsLinkable")]
			public unsafe static extern bool IsLinkable(GuildChannel* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildChannel_SetIsLinkable")]
			public unsafe static extern void SetIsLinkable(GuildChannel* self, bool value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildChannel_IsViewableAndWriteableByAllMembers")]
			public unsafe static extern bool IsViewableAndWriteableByAllMembers(GuildChannel* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildChannel_SetIsViewableAndWriteableByAllMembers")]
			public unsafe static extern void SetIsViewableAndWriteableByAllMembers(GuildChannel* self, bool value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildChannel_LinkedLobby")]
			public unsafe static extern bool LinkedLobby(GuildChannel* self, LinkedLobby* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_GuildChannel_SetLinkedLobby")]
			public unsafe static extern void SetLinkedLobby(GuildChannel* self, LinkedLobby* value);
		}

		public struct LinkedLobby
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LinkedLobby_Init")]
			public unsafe static extern void Init(LinkedLobby* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LinkedLobby_Drop")]
			public unsafe static extern void Drop(LinkedLobby* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LinkedLobby_Clone")]
			public unsafe static extern void Clone(LinkedLobby* self, LinkedLobby* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LinkedLobby_ApplicationId")]
			public unsafe static extern ulong ApplicationId(LinkedLobby* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LinkedLobby_SetApplicationId")]
			public unsafe static extern void SetApplicationId(LinkedLobby* self, ulong value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LinkedLobby_LobbyId")]
			public unsafe static extern ulong LobbyId(LinkedLobby* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LinkedLobby_SetLobbyId")]
			public unsafe static extern void SetLobbyId(LinkedLobby* self, ulong value);
		}

		public struct LinkedChannel
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LinkedChannel_Drop")]
			public unsafe static extern void Drop(LinkedChannel* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LinkedChannel_Clone")]
			public unsafe static extern void Clone(LinkedChannel* self, LinkedChannel* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LinkedChannel_Id")]
			public unsafe static extern ulong Id(LinkedChannel* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LinkedChannel_SetId")]
			public unsafe static extern void SetId(LinkedChannel* self, ulong value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LinkedChannel_Name")]
			public unsafe static extern void Name(LinkedChannel* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LinkedChannel_SetName")]
			public unsafe static extern void SetName(LinkedChannel* self, Discord_String value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LinkedChannel_GuildId")]
			public unsafe static extern ulong GuildId(LinkedChannel* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LinkedChannel_SetGuildId")]
			public unsafe static extern void SetGuildId(LinkedChannel* self, ulong value);
		}

		public struct RelationshipHandle
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_RelationshipHandle_Drop")]
			public unsafe static extern void Drop(RelationshipHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_RelationshipHandle_Clone")]
			public unsafe static extern void Clone(RelationshipHandle* self, RelationshipHandle* other);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_RelationshipHandle_DiscordRelationshipType")]
			public unsafe static extern RelationshipType DiscordRelationshipType(RelationshipHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_RelationshipHandle_GameRelationshipType")]
			public unsafe static extern RelationshipType GameRelationshipType(RelationshipHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_RelationshipHandle_Id")]
			public unsafe static extern ulong Id(RelationshipHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_RelationshipHandle_User")]
			public unsafe static extern bool User(RelationshipHandle* self, UserHandle* returnValue);
		}

		public struct UserHandle
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UserHandle_Drop")]
			public unsafe static extern void Drop(UserHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UserHandle_Clone")]
			public unsafe static extern void Clone(UserHandle* self, UserHandle* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UserHandle_Avatar")]
			public unsafe static extern bool Avatar(UserHandle* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UserHandle_AvatarTypeToString")]
			public unsafe static extern void AvatarTypeToString(Discord.Sdk.UserHandle.AvatarType type, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UserHandle_AvatarUrl")]
			public unsafe static extern void AvatarUrl(UserHandle* self, Discord.Sdk.UserHandle.AvatarType animatedType, Discord.Sdk.UserHandle.AvatarType staticType, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UserHandle_DisplayName")]
			public unsafe static extern void DisplayName(UserHandle* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UserHandle_GameActivity")]
			public unsafe static extern bool GameActivity(UserHandle* self, Activity* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UserHandle_GlobalName")]
			public unsafe static extern bool GlobalName(UserHandle* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UserHandle_Id")]
			public unsafe static extern ulong Id(UserHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UserHandle_IsProvisional")]
			public unsafe static extern bool IsProvisional(UserHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UserHandle_Relationship")]
			public unsafe static extern void Relationship(UserHandle* self, RelationshipHandle* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UserHandle_Status")]
			public unsafe static extern StatusType Status(UserHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UserHandle_Username")]
			public unsafe static extern void Username(UserHandle* self, Discord_String* returnValue);
		}

		public struct LobbyMemberHandle
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyMemberHandle_Drop")]
			public unsafe static extern void Drop(LobbyMemberHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyMemberHandle_Clone")]
			public unsafe static extern void Clone(LobbyMemberHandle* self, LobbyMemberHandle* other);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyMemberHandle_CanLinkLobby")]
			public unsafe static extern bool CanLinkLobby(LobbyMemberHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyMemberHandle_Connected")]
			public unsafe static extern bool Connected(LobbyMemberHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyMemberHandle_Id")]
			public unsafe static extern ulong Id(LobbyMemberHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyMemberHandle_Metadata")]
			public unsafe static extern void Metadata(LobbyMemberHandle* self, Discord_Properties* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyMemberHandle_User")]
			public unsafe static extern bool User(LobbyMemberHandle* self, UserHandle* returnValue);
		}

		public struct LobbyHandle
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyHandle_Drop")]
			public unsafe static extern void Drop(LobbyHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyHandle_Clone")]
			public unsafe static extern void Clone(LobbyHandle* self, LobbyHandle* other);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyHandle_GetCallInfoHandle")]
			public unsafe static extern bool GetCallInfoHandle(LobbyHandle* self, CallInfoHandle* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyHandle_GetLobbyMemberHandle")]
			public unsafe static extern bool GetLobbyMemberHandle(LobbyHandle* self, ulong memberId, LobbyMemberHandle* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyHandle_Id")]
			public unsafe static extern ulong Id(LobbyHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyHandle_LinkedChannel")]
			public unsafe static extern bool LinkedChannel(LobbyHandle* self, LinkedChannel* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyHandle_LobbyMemberIds")]
			public unsafe static extern void LobbyMemberIds(LobbyHandle* self, Discord_UInt64Span* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyHandle_LobbyMembers")]
			public unsafe static extern void LobbyMembers(LobbyHandle* self, Discord_LobbyMemberHandleSpan* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_LobbyHandle_Metadata")]
			public unsafe static extern void Metadata(LobbyHandle* self, Discord_Properties* returnValue);
		}

		public struct AdditionalContent
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AdditionalContent_Init")]
			public unsafe static extern void Init(AdditionalContent* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AdditionalContent_Drop")]
			public unsafe static extern void Drop(AdditionalContent* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AdditionalContent_Clone")]
			public unsafe static extern void Clone(AdditionalContent* self, AdditionalContent* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AdditionalContent_Equals")]
			public unsafe static extern bool Equals(AdditionalContent* self, AdditionalContent* rhs);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AdditionalContent_TypeToString")]
			public unsafe static extern void TypeToString(AdditionalContentType type, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AdditionalContent_Type")]
			public unsafe static extern AdditionalContentType Type(AdditionalContent* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AdditionalContent_SetType")]
			public unsafe static extern void SetType(AdditionalContent* self, AdditionalContentType value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AdditionalContent_Title")]
			public unsafe static extern bool Title(AdditionalContent* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AdditionalContent_SetTitle")]
			public unsafe static extern void SetTitle(AdditionalContent* self, Discord_String* value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AdditionalContent_Count")]
			public unsafe static extern byte Count(AdditionalContent* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AdditionalContent_SetCount")]
			public unsafe static extern void SetCount(AdditionalContent* self, byte value);
		}

		public struct MessageHandle
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_Drop")]
			public unsafe static extern void Drop(MessageHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_Clone")]
			public unsafe static extern void Clone(MessageHandle* self, MessageHandle* other);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_AdditionalContent")]
			public unsafe static extern bool AdditionalContent(MessageHandle* self, AdditionalContent* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_Author")]
			public unsafe static extern bool Author(MessageHandle* self, UserHandle* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_AuthorId")]
			public unsafe static extern ulong AuthorId(MessageHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_Channel")]
			public unsafe static extern bool Channel(MessageHandle* self, ChannelHandle* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_ChannelId")]
			public unsafe static extern ulong ChannelId(MessageHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_Content")]
			public unsafe static extern void Content(MessageHandle* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_DisclosureType")]
			public unsafe static extern bool DisclosureType(MessageHandle* self, DisclosureTypes* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_EditedTimestamp")]
			public unsafe static extern ulong EditedTimestamp(MessageHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_Id")]
			public unsafe static extern ulong Id(MessageHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_Lobby")]
			public unsafe static extern bool Lobby(MessageHandle* self, LobbyHandle* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_Metadata")]
			public unsafe static extern void Metadata(MessageHandle* self, Discord_Properties* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_RawContent")]
			public unsafe static extern void RawContent(MessageHandle* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_Recipient")]
			public unsafe static extern bool Recipient(MessageHandle* self, UserHandle* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_RecipientId")]
			public unsafe static extern ulong RecipientId(MessageHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_SentFromGame")]
			public unsafe static extern bool SentFromGame(MessageHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_MessageHandle_SentTimestamp")]
			public unsafe static extern ulong SentTimestamp(MessageHandle* self);
		}

		public struct AudioDevice
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AudioDevice_Drop")]
			public unsafe static extern void Drop(AudioDevice* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AudioDevice_Clone")]
			public unsafe static extern void Clone(AudioDevice* self, AudioDevice* arg0);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AudioDevice_Equals")]
			public unsafe static extern bool Equals(AudioDevice* self, AudioDevice* rhs);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AudioDevice_Id")]
			public unsafe static extern void Id(AudioDevice* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AudioDevice_SetId")]
			public unsafe static extern void SetId(AudioDevice* self, Discord_String value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AudioDevice_Name")]
			public unsafe static extern void Name(AudioDevice* self, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AudioDevice_SetName")]
			public unsafe static extern void SetName(AudioDevice* self, Discord_String value);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AudioDevice_IsDefault")]
			public unsafe static extern bool IsDefault(AudioDevice* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_AudioDevice_SetIsDefault")]
			public unsafe static extern void SetIsDefault(AudioDevice* self, bool value);
		}

		public struct Client
		{
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void EndCallCallback(void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void EndCallsCallback(void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void GetCurrentInputDeviceCallback(AudioDevice* device, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void GetCurrentOutputDeviceCallback(AudioDevice* device, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void GetInputDevicesCallback(Discord_AudioDeviceSpan devices, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void GetOutputDevicesCallback(Discord_AudioDeviceSpan devices, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void DeviceChangeCallback(Discord_AudioDeviceSpan inputDevices, Discord_AudioDeviceSpan outputDevices, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void SetInputDeviceCallback(ClientResult* result, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void NoAudioInputCallback(bool inputDetected, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void SetOutputDeviceCallback(ClientResult* result, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void VoiceParticipantChangedCallback(ulong lobbyId, ulong memberId, bool added, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void UserAudioReceivedCallback(ulong userId, short* data, ulong samplesPerChannel, int sampleRate, ulong channels, bool* outShouldMute, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void UserAudioCapturedCallback(short* data, ulong samplesPerChannel, int sampleRate, ulong channels, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void AuthorizationCallback(ClientResult* result, Discord_String code, Discord_String redirectUri, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void FetchCurrentUserCallback(ClientResult* result, ulong id, Discord_String name, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void TokenExchangeCallback(ClientResult* result, Discord_String accessToken, Discord_String refreshToken, AuthorizationTokenType tokenType, int expiresIn, Discord_String scopes, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void AuthorizeDeviceScreenClosedCallback(void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void TokenExpirationCallback(void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void UpdateProvisionalAccountDisplayNameCallback(ClientResult* result, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void UpdateTokenCallback(ClientResult* result, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void DeleteUserMessageCallback(ClientResult* result, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void EditUserMessageCallback(ClientResult* result, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void ProvisionalUserMergeRequiredCallback(void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void OpenMessageInDiscordCallback(ClientResult* result, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void SendUserMessageCallback(ClientResult* result, ulong messageId, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void MessageCreatedCallback(ulong messageId, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void MessageDeletedCallback(ulong messageId, ulong channelId, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void MessageUpdatedCallback(ulong messageId, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void LogCallback(Discord_String message, LoggingSeverity severity, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void OpenConnectedGamesSettingsInDiscordCallback(ClientResult* result, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void OnStatusChanged(Discord.Sdk.Client.Status status, Discord.Sdk.Client.Error error, int errorDetail, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void CreateOrJoinLobbyCallback(ClientResult* result, ulong lobbyId, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void GetGuildChannelsCallback(ClientResult* result, Discord_GuildChannelSpan guildChannels, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void GetUserGuildsCallback(ClientResult* result, Discord_GuildMinimalSpan guilds, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void LeaveLobbyCallback(ClientResult* result, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void LinkOrUnlinkChannelCallback(ClientResult* result, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void LobbyCreatedCallback(ulong lobbyId, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void LobbyDeletedCallback(ulong lobbyId, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void LobbyMemberAddedCallback(ulong lobbyId, ulong memberId, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void LobbyMemberRemovedCallback(ulong lobbyId, ulong memberId, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void LobbyMemberUpdatedCallback(ulong lobbyId, ulong memberId, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void LobbyUpdatedCallback(ulong lobbyId, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void AcceptActivityInviteCallback(ClientResult* result, Discord_String joinSecret, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void SendActivityInviteCallback(ClientResult* result, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void ActivityInviteCallback(ActivityInvite* invite, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void ActivityJoinCallback(Discord_String joinSecret, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void UpdateStatusCallback(ClientResult* result, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void UpdateRichPresenceCallback(ClientResult* result, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void UpdateRelationshipCallback(ClientResult* result, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void SendFriendRequestCallback(ClientResult* result, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void RelationshipCreatedCallback(ulong userId, bool isDiscordRelationshipUpdate, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void RelationshipDeletedCallback(ulong userId, bool isDiscordRelationshipUpdate, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void GetDiscordClientConnectedUserCallback(ClientResult* result, UserHandle* user, void* __userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void UserUpdatedCallback(ulong userId, void* __userData);

			public IntPtr Handle;

			[MonoPInvokeCallback(typeof(EndCallCallback))]
			public unsafe static void EndCallCallback_Handler(void* __userData)
			{
				Discord.Sdk.Client.EndCallCallback endCallCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.EndCallCallback>(__userData);
				try
				{
					endCallCallback();
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(EndCallsCallback))]
			public unsafe static void EndCallsCallback_Handler(void* __userData)
			{
				Discord.Sdk.Client.EndCallsCallback endCallsCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.EndCallsCallback>(__userData);
				try
				{
					endCallsCallback();
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(GetCurrentInputDeviceCallback))]
			public unsafe static void GetCurrentInputDeviceCallback_Handler(AudioDevice* device, void* __userData)
			{
				Discord.Sdk.Client.GetCurrentInputDeviceCallback getCurrentInputDeviceCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.GetCurrentInputDeviceCallback>(__userData);
				try
				{
					getCurrentInputDeviceCallback(new Discord.Sdk.AudioDevice(device));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(GetCurrentOutputDeviceCallback))]
			public unsafe static void GetCurrentOutputDeviceCallback_Handler(AudioDevice* device, void* __userData)
			{
				Discord.Sdk.Client.GetCurrentOutputDeviceCallback getCurrentOutputDeviceCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.GetCurrentOutputDeviceCallback>(__userData);
				try
				{
					getCurrentOutputDeviceCallback(new Discord.Sdk.AudioDevice(device));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(GetInputDevicesCallback))]
			public unsafe static void GetInputDevicesCallback_Handler(Discord_AudioDeviceSpan devices, void* __userData)
			{
				Discord.Sdk.Client.GetInputDevicesCallback getInputDevicesCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.GetInputDevicesCallback>(__userData);
				try
				{
					getInputDevicesCallback((from __native in new Span<AudioDevice>(devices.ptr, (int)(uint)devices.size).ToArray()
						select new Discord.Sdk.AudioDevice(__native, 0)).ToArray());
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
				finally
				{
					Discord_Free(devices.ptr);
				}
			}

			[MonoPInvokeCallback(typeof(GetOutputDevicesCallback))]
			public unsafe static void GetOutputDevicesCallback_Handler(Discord_AudioDeviceSpan devices, void* __userData)
			{
				Discord.Sdk.Client.GetOutputDevicesCallback getOutputDevicesCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.GetOutputDevicesCallback>(__userData);
				try
				{
					getOutputDevicesCallback((from __native in new Span<AudioDevice>(devices.ptr, (int)(uint)devices.size).ToArray()
						select new Discord.Sdk.AudioDevice(__native, 0)).ToArray());
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
				finally
				{
					Discord_Free(devices.ptr);
				}
			}

			[MonoPInvokeCallback(typeof(DeviceChangeCallback))]
			public unsafe static void DeviceChangeCallback_Handler(Discord_AudioDeviceSpan inputDevices, Discord_AudioDeviceSpan outputDevices, void* __userData)
			{
				Discord.Sdk.Client.DeviceChangeCallback deviceChangeCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.DeviceChangeCallback>(__userData);
				try
				{
					deviceChangeCallback((from __native in new Span<AudioDevice>(inputDevices.ptr, (int)(uint)inputDevices.size).ToArray()
						select new Discord.Sdk.AudioDevice(__native, 0)).ToArray(), (from __native in new Span<AudioDevice>(outputDevices.ptr, (int)(uint)outputDevices.size).ToArray()
						select new Discord.Sdk.AudioDevice(__native, 0)).ToArray());
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
				finally
				{
					Discord_Free(inputDevices.ptr);
					Discord_Free(outputDevices.ptr);
				}
			}

			[MonoPInvokeCallback(typeof(SetInputDeviceCallback))]
			public unsafe static void SetInputDeviceCallback_Handler(ClientResult* result, void* __userData)
			{
				Discord.Sdk.Client.SetInputDeviceCallback setInputDeviceCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.SetInputDeviceCallback>(__userData);
				try
				{
					setInputDeviceCallback(new Discord.Sdk.ClientResult(*result, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(NoAudioInputCallback))]
			public unsafe static void NoAudioInputCallback_Handler(bool inputDetected, void* __userData)
			{
				Discord.Sdk.Client.NoAudioInputCallback noAudioInputCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.NoAudioInputCallback>(__userData);
				try
				{
					noAudioInputCallback(inputDetected);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(SetOutputDeviceCallback))]
			public unsafe static void SetOutputDeviceCallback_Handler(ClientResult* result, void* __userData)
			{
				Discord.Sdk.Client.SetOutputDeviceCallback setOutputDeviceCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.SetOutputDeviceCallback>(__userData);
				try
				{
					setOutputDeviceCallback(new Discord.Sdk.ClientResult(*result, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(VoiceParticipantChangedCallback))]
			public unsafe static void VoiceParticipantChangedCallback_Handler(ulong lobbyId, ulong memberId, bool added, void* __userData)
			{
				Discord.Sdk.Client.VoiceParticipantChangedCallback voiceParticipantChangedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.VoiceParticipantChangedCallback>(__userData);
				try
				{
					voiceParticipantChangedCallback(lobbyId, memberId, added);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(UserAudioReceivedCallback))]
			public unsafe static void UserAudioReceivedCallback_Handler(ulong userId, short* data, ulong samplesPerChannel, int sampleRate, ulong channels, bool* outShouldMute, void* __userData)
			{
				Discord.Sdk.Client.UserAudioReceivedCallback userAudioReceivedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.UserAudioReceivedCallback>(__userData);
				try
				{
					userAudioReceivedCallback(userId, (IntPtr)data, samplesPerChannel, sampleRate, channels, ref *outShouldMute);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(UserAudioCapturedCallback))]
			public unsafe static void UserAudioCapturedCallback_Handler(short* data, ulong samplesPerChannel, int sampleRate, ulong channels, void* __userData)
			{
				Discord.Sdk.Client.UserAudioCapturedCallback userAudioCapturedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.UserAudioCapturedCallback>(__userData);
				try
				{
					userAudioCapturedCallback((IntPtr)data, samplesPerChannel, sampleRate, channels);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(AuthorizationCallback))]
			public unsafe static void AuthorizationCallback_Handler(ClientResult* result, Discord_String code, Discord_String redirectUri, void* __userData)
			{
				Discord.Sdk.Client.AuthorizationCallback authorizationCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.AuthorizationCallback>(__userData);
				try
				{
					authorizationCallback(new Discord.Sdk.ClientResult(*result, 0), Marshal.PtrToStringUTF8((IntPtr)code.ptr, (int)(uint)code.size), Marshal.PtrToStringUTF8((IntPtr)redirectUri.ptr, (int)(uint)redirectUri.size));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
				finally
				{
					Discord_Free(code.ptr);
					Discord_Free(redirectUri.ptr);
				}
			}

			[MonoPInvokeCallback(typeof(FetchCurrentUserCallback))]
			public unsafe static void FetchCurrentUserCallback_Handler(ClientResult* result, ulong id, Discord_String name, void* __userData)
			{
				Discord.Sdk.Client.FetchCurrentUserCallback fetchCurrentUserCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.FetchCurrentUserCallback>(__userData);
				try
				{
					fetchCurrentUserCallback(new Discord.Sdk.ClientResult(*result, 0), id, Marshal.PtrToStringUTF8((IntPtr)name.ptr, (int)(uint)name.size));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
				finally
				{
					Discord_Free(name.ptr);
				}
			}

			[MonoPInvokeCallback(typeof(TokenExchangeCallback))]
			public unsafe static void TokenExchangeCallback_Handler(ClientResult* result, Discord_String accessToken, Discord_String refreshToken, AuthorizationTokenType tokenType, int expiresIn, Discord_String scopes, void* __userData)
			{
				Discord.Sdk.Client.TokenExchangeCallback tokenExchangeCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.TokenExchangeCallback>(__userData);
				try
				{
					tokenExchangeCallback(new Discord.Sdk.ClientResult(*result, 0), Marshal.PtrToStringUTF8((IntPtr)accessToken.ptr, (int)(uint)accessToken.size), Marshal.PtrToStringUTF8((IntPtr)refreshToken.ptr, (int)(uint)refreshToken.size), tokenType, expiresIn, Marshal.PtrToStringUTF8((IntPtr)scopes.ptr, (int)(uint)scopes.size));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
				finally
				{
					Discord_Free(accessToken.ptr);
					Discord_Free(refreshToken.ptr);
					Discord_Free(scopes.ptr);
				}
			}

			[MonoPInvokeCallback(typeof(AuthorizeDeviceScreenClosedCallback))]
			public unsafe static void AuthorizeDeviceScreenClosedCallback_Handler(void* __userData)
			{
				Discord.Sdk.Client.AuthorizeDeviceScreenClosedCallback authorizeDeviceScreenClosedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.AuthorizeDeviceScreenClosedCallback>(__userData);
				try
				{
					authorizeDeviceScreenClosedCallback();
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(TokenExpirationCallback))]
			public unsafe static void TokenExpirationCallback_Handler(void* __userData)
			{
				Discord.Sdk.Client.TokenExpirationCallback tokenExpirationCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.TokenExpirationCallback>(__userData);
				try
				{
					tokenExpirationCallback();
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(UpdateProvisionalAccountDisplayNameCallback))]
			public unsafe static void UpdateProvisionalAccountDisplayNameCallback_Handler(ClientResult* result, void* __userData)
			{
				Discord.Sdk.Client.UpdateProvisionalAccountDisplayNameCallback updateProvisionalAccountDisplayNameCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.UpdateProvisionalAccountDisplayNameCallback>(__userData);
				try
				{
					updateProvisionalAccountDisplayNameCallback(new Discord.Sdk.ClientResult(*result, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(UpdateTokenCallback))]
			public unsafe static void UpdateTokenCallback_Handler(ClientResult* result, void* __userData)
			{
				Discord.Sdk.Client.UpdateTokenCallback updateTokenCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.UpdateTokenCallback>(__userData);
				try
				{
					updateTokenCallback(new Discord.Sdk.ClientResult(*result, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(DeleteUserMessageCallback))]
			public unsafe static void DeleteUserMessageCallback_Handler(ClientResult* result, void* __userData)
			{
				Discord.Sdk.Client.DeleteUserMessageCallback deleteUserMessageCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.DeleteUserMessageCallback>(__userData);
				try
				{
					deleteUserMessageCallback(new Discord.Sdk.ClientResult(*result, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(EditUserMessageCallback))]
			public unsafe static void EditUserMessageCallback_Handler(ClientResult* result, void* __userData)
			{
				Discord.Sdk.Client.EditUserMessageCallback editUserMessageCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.EditUserMessageCallback>(__userData);
				try
				{
					editUserMessageCallback(new Discord.Sdk.ClientResult(*result, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(ProvisionalUserMergeRequiredCallback))]
			public unsafe static void ProvisionalUserMergeRequiredCallback_Handler(void* __userData)
			{
				Discord.Sdk.Client.ProvisionalUserMergeRequiredCallback provisionalUserMergeRequiredCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.ProvisionalUserMergeRequiredCallback>(__userData);
				try
				{
					provisionalUserMergeRequiredCallback();
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(OpenMessageInDiscordCallback))]
			public unsafe static void OpenMessageInDiscordCallback_Handler(ClientResult* result, void* __userData)
			{
				Discord.Sdk.Client.OpenMessageInDiscordCallback openMessageInDiscordCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.OpenMessageInDiscordCallback>(__userData);
				try
				{
					openMessageInDiscordCallback(new Discord.Sdk.ClientResult(*result, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(SendUserMessageCallback))]
			public unsafe static void SendUserMessageCallback_Handler(ClientResult* result, ulong messageId, void* __userData)
			{
				Discord.Sdk.Client.SendUserMessageCallback sendUserMessageCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.SendUserMessageCallback>(__userData);
				try
				{
					sendUserMessageCallback(new Discord.Sdk.ClientResult(*result, 0), messageId);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(MessageCreatedCallback))]
			public unsafe static void MessageCreatedCallback_Handler(ulong messageId, void* __userData)
			{
				Discord.Sdk.Client.MessageCreatedCallback messageCreatedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.MessageCreatedCallback>(__userData);
				try
				{
					messageCreatedCallback(messageId);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(MessageDeletedCallback))]
			public unsafe static void MessageDeletedCallback_Handler(ulong messageId, ulong channelId, void* __userData)
			{
				Discord.Sdk.Client.MessageDeletedCallback messageDeletedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.MessageDeletedCallback>(__userData);
				try
				{
					messageDeletedCallback(messageId, channelId);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(MessageUpdatedCallback))]
			public unsafe static void MessageUpdatedCallback_Handler(ulong messageId, void* __userData)
			{
				Discord.Sdk.Client.MessageUpdatedCallback messageUpdatedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.MessageUpdatedCallback>(__userData);
				try
				{
					messageUpdatedCallback(messageId);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(LogCallback))]
			public unsafe static void LogCallback_Handler(Discord_String message, LoggingSeverity severity, void* __userData)
			{
				Discord.Sdk.Client.LogCallback logCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.LogCallback>(__userData);
				try
				{
					logCallback(Marshal.PtrToStringUTF8((IntPtr)message.ptr, (int)(uint)message.size), severity);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
				finally
				{
					Discord_Free(message.ptr);
				}
			}

			[MonoPInvokeCallback(typeof(OpenConnectedGamesSettingsInDiscordCallback))]
			public unsafe static void OpenConnectedGamesSettingsInDiscordCallback_Handler(ClientResult* result, void* __userData)
			{
				Discord.Sdk.Client.OpenConnectedGamesSettingsInDiscordCallback openConnectedGamesSettingsInDiscordCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.OpenConnectedGamesSettingsInDiscordCallback>(__userData);
				try
				{
					openConnectedGamesSettingsInDiscordCallback(new Discord.Sdk.ClientResult(*result, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(OnStatusChanged))]
			public unsafe static void OnStatusChanged_Handler(Discord.Sdk.Client.Status status, Discord.Sdk.Client.Error error, int errorDetail, void* __userData)
			{
				Discord.Sdk.Client.OnStatusChanged onStatusChanged = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.OnStatusChanged>(__userData);
				try
				{
					onStatusChanged(status, error, errorDetail);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(CreateOrJoinLobbyCallback))]
			public unsafe static void CreateOrJoinLobbyCallback_Handler(ClientResult* result, ulong lobbyId, void* __userData)
			{
				Discord.Sdk.Client.CreateOrJoinLobbyCallback createOrJoinLobbyCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.CreateOrJoinLobbyCallback>(__userData);
				try
				{
					createOrJoinLobbyCallback(new Discord.Sdk.ClientResult(*result, 0), lobbyId);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(GetGuildChannelsCallback))]
			public unsafe static void GetGuildChannelsCallback_Handler(ClientResult* result, Discord_GuildChannelSpan guildChannels, void* __userData)
			{
				Discord.Sdk.Client.GetGuildChannelsCallback getGuildChannelsCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.GetGuildChannelsCallback>(__userData);
				try
				{
					getGuildChannelsCallback(new Discord.Sdk.ClientResult(*result, 0), (from __native in new Span<GuildChannel>(guildChannels.ptr, (int)(uint)guildChannels.size).ToArray()
						select new Discord.Sdk.GuildChannel(__native, 0)).ToArray());
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
				finally
				{
					Discord_Free(guildChannels.ptr);
				}
			}

			[MonoPInvokeCallback(typeof(GetUserGuildsCallback))]
			public unsafe static void GetUserGuildsCallback_Handler(ClientResult* result, Discord_GuildMinimalSpan guilds, void* __userData)
			{
				Discord.Sdk.Client.GetUserGuildsCallback getUserGuildsCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.GetUserGuildsCallback>(__userData);
				try
				{
					getUserGuildsCallback(new Discord.Sdk.ClientResult(*result, 0), (from __native in new Span<GuildMinimal>(guilds.ptr, (int)(uint)guilds.size).ToArray()
						select new Discord.Sdk.GuildMinimal(__native, 0)).ToArray());
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
				finally
				{
					Discord_Free(guilds.ptr);
				}
			}

			[MonoPInvokeCallback(typeof(LeaveLobbyCallback))]
			public unsafe static void LeaveLobbyCallback_Handler(ClientResult* result, void* __userData)
			{
				Discord.Sdk.Client.LeaveLobbyCallback leaveLobbyCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.LeaveLobbyCallback>(__userData);
				try
				{
					leaveLobbyCallback(new Discord.Sdk.ClientResult(*result, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(LinkOrUnlinkChannelCallback))]
			public unsafe static void LinkOrUnlinkChannelCallback_Handler(ClientResult* result, void* __userData)
			{
				Discord.Sdk.Client.LinkOrUnlinkChannelCallback linkOrUnlinkChannelCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.LinkOrUnlinkChannelCallback>(__userData);
				try
				{
					linkOrUnlinkChannelCallback(new Discord.Sdk.ClientResult(*result, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(LobbyCreatedCallback))]
			public unsafe static void LobbyCreatedCallback_Handler(ulong lobbyId, void* __userData)
			{
				Discord.Sdk.Client.LobbyCreatedCallback lobbyCreatedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.LobbyCreatedCallback>(__userData);
				try
				{
					lobbyCreatedCallback(lobbyId);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(LobbyDeletedCallback))]
			public unsafe static void LobbyDeletedCallback_Handler(ulong lobbyId, void* __userData)
			{
				Discord.Sdk.Client.LobbyDeletedCallback lobbyDeletedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.LobbyDeletedCallback>(__userData);
				try
				{
					lobbyDeletedCallback(lobbyId);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(LobbyMemberAddedCallback))]
			public unsafe static void LobbyMemberAddedCallback_Handler(ulong lobbyId, ulong memberId, void* __userData)
			{
				Discord.Sdk.Client.LobbyMemberAddedCallback lobbyMemberAddedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.LobbyMemberAddedCallback>(__userData);
				try
				{
					lobbyMemberAddedCallback(lobbyId, memberId);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(LobbyMemberRemovedCallback))]
			public unsafe static void LobbyMemberRemovedCallback_Handler(ulong lobbyId, ulong memberId, void* __userData)
			{
				Discord.Sdk.Client.LobbyMemberRemovedCallback lobbyMemberRemovedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.LobbyMemberRemovedCallback>(__userData);
				try
				{
					lobbyMemberRemovedCallback(lobbyId, memberId);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(LobbyMemberUpdatedCallback))]
			public unsafe static void LobbyMemberUpdatedCallback_Handler(ulong lobbyId, ulong memberId, void* __userData)
			{
				Discord.Sdk.Client.LobbyMemberUpdatedCallback lobbyMemberUpdatedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.LobbyMemberUpdatedCallback>(__userData);
				try
				{
					lobbyMemberUpdatedCallback(lobbyId, memberId);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(LobbyUpdatedCallback))]
			public unsafe static void LobbyUpdatedCallback_Handler(ulong lobbyId, void* __userData)
			{
				Discord.Sdk.Client.LobbyUpdatedCallback lobbyUpdatedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.LobbyUpdatedCallback>(__userData);
				try
				{
					lobbyUpdatedCallback(lobbyId);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(AcceptActivityInviteCallback))]
			public unsafe static void AcceptActivityInviteCallback_Handler(ClientResult* result, Discord_String joinSecret, void* __userData)
			{
				Discord.Sdk.Client.AcceptActivityInviteCallback acceptActivityInviteCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.AcceptActivityInviteCallback>(__userData);
				try
				{
					acceptActivityInviteCallback(new Discord.Sdk.ClientResult(*result, 0), Marshal.PtrToStringUTF8((IntPtr)joinSecret.ptr, (int)(uint)joinSecret.size));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
				finally
				{
					Discord_Free(joinSecret.ptr);
				}
			}

			[MonoPInvokeCallback(typeof(SendActivityInviteCallback))]
			public unsafe static void SendActivityInviteCallback_Handler(ClientResult* result, void* __userData)
			{
				Discord.Sdk.Client.SendActivityInviteCallback sendActivityInviteCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.SendActivityInviteCallback>(__userData);
				try
				{
					sendActivityInviteCallback(new Discord.Sdk.ClientResult(*result, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(ActivityInviteCallback))]
			public unsafe static void ActivityInviteCallback_Handler(ActivityInvite* invite, void* __userData)
			{
				Discord.Sdk.Client.ActivityInviteCallback activityInviteCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.ActivityInviteCallback>(__userData);
				try
				{
					activityInviteCallback(new Discord.Sdk.ActivityInvite(*invite, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(ActivityJoinCallback))]
			public unsafe static void ActivityJoinCallback_Handler(Discord_String joinSecret, void* __userData)
			{
				Discord.Sdk.Client.ActivityJoinCallback activityJoinCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.ActivityJoinCallback>(__userData);
				try
				{
					activityJoinCallback(Marshal.PtrToStringUTF8((IntPtr)joinSecret.ptr, (int)(uint)joinSecret.size));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
				finally
				{
					Discord_Free(joinSecret.ptr);
				}
			}

			[MonoPInvokeCallback(typeof(UpdateStatusCallback))]
			public unsafe static void UpdateStatusCallback_Handler(ClientResult* result, void* __userData)
			{
				Discord.Sdk.Client.UpdateStatusCallback updateStatusCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.UpdateStatusCallback>(__userData);
				try
				{
					updateStatusCallback(new Discord.Sdk.ClientResult(*result, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(UpdateRichPresenceCallback))]
			public unsafe static void UpdateRichPresenceCallback_Handler(ClientResult* result, void* __userData)
			{
				Discord.Sdk.Client.UpdateRichPresenceCallback updateRichPresenceCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.UpdateRichPresenceCallback>(__userData);
				try
				{
					updateRichPresenceCallback(new Discord.Sdk.ClientResult(*result, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(UpdateRelationshipCallback))]
			public unsafe static void UpdateRelationshipCallback_Handler(ClientResult* result, void* __userData)
			{
				Discord.Sdk.Client.UpdateRelationshipCallback updateRelationshipCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.UpdateRelationshipCallback>(__userData);
				try
				{
					updateRelationshipCallback(new Discord.Sdk.ClientResult(*result, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(SendFriendRequestCallback))]
			public unsafe static void SendFriendRequestCallback_Handler(ClientResult* result, void* __userData)
			{
				Discord.Sdk.Client.SendFriendRequestCallback sendFriendRequestCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.SendFriendRequestCallback>(__userData);
				try
				{
					sendFriendRequestCallback(new Discord.Sdk.ClientResult(*result, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(RelationshipCreatedCallback))]
			public unsafe static void RelationshipCreatedCallback_Handler(ulong userId, bool isDiscordRelationshipUpdate, void* __userData)
			{
				Discord.Sdk.Client.RelationshipCreatedCallback relationshipCreatedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.RelationshipCreatedCallback>(__userData);
				try
				{
					relationshipCreatedCallback(userId, isDiscordRelationshipUpdate);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(RelationshipDeletedCallback))]
			public unsafe static void RelationshipDeletedCallback_Handler(ulong userId, bool isDiscordRelationshipUpdate, void* __userData)
			{
				Discord.Sdk.Client.RelationshipDeletedCallback relationshipDeletedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.RelationshipDeletedCallback>(__userData);
				try
				{
					relationshipDeletedCallback(userId, isDiscordRelationshipUpdate);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(GetDiscordClientConnectedUserCallback))]
			public unsafe static void GetDiscordClientConnectedUserCallback_Handler(ClientResult* result, UserHandle* user, void* __userData)
			{
				Discord.Sdk.Client.GetDiscordClientConnectedUserCallback getDiscordClientConnectedUserCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.GetDiscordClientConnectedUserCallback>(__userData);
				try
				{
					getDiscordClientConnectedUserCallback(new Discord.Sdk.ClientResult(*result, 0), (user == null) ? null : new Discord.Sdk.UserHandle(*user, 0));
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[MonoPInvokeCallback(typeof(UserUpdatedCallback))]
			public unsafe static void UserUpdatedCallback_Handler(ulong userId, void* __userData)
			{
				Discord.Sdk.Client.UserUpdatedCallback userUpdatedCallback = ManagedUserData.DelegateFromPointer<Discord.Sdk.Client.UserUpdatedCallback>(__userData);
				try
				{
					userUpdatedCallback(userId);
				}
				catch (Exception ex)
				{
					__ReportUnhandledException(ex);
				}
			}

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_Init")]
			public unsafe static extern void Init(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_InitWithBases")]
			public unsafe static extern void InitWithBases(Client* self, Discord_String apiBase, Discord_String webBase);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_Drop")]
			public unsafe static extern void Drop(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_ErrorToString")]
			public unsafe static extern void ErrorToString(Discord.Sdk.Client.Error type, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetApplicationId")]
			public unsafe static extern ulong GetApplicationId(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetDefaultAudioDeviceId")]
			public unsafe static extern void GetDefaultAudioDeviceId(Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetDefaultCommunicationScopes")]
			public unsafe static extern void GetDefaultCommunicationScopes(Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetDefaultPresenceScopes")]
			public unsafe static extern void GetDefaultPresenceScopes(Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetVersionHash")]
			public unsafe static extern void GetVersionHash(Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetVersionMajor")]
			public static extern int GetVersionMajor();

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetVersionMinor")]
			public static extern int GetVersionMinor();

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetVersionPatch")]
			public static extern int GetVersionPatch();

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetHttpRequestTimeout")]
			public unsafe static extern void SetHttpRequestTimeout(Client* self, int httpTimeoutInMilliseconds);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_StatusToString")]
			public unsafe static extern void StatusToString(Discord.Sdk.Client.Status type, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_ThreadToString")]
			public unsafe static extern void ThreadToString(Discord.Sdk.Client.Thread type, Discord_String* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_EndCall")]
			public unsafe static extern void EndCall(Client* self, ulong channelId, EndCallCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_EndCalls")]
			public unsafe static extern void EndCalls(Client* self, EndCallsCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetCall")]
			public unsafe static extern bool GetCall(Client* self, ulong channelId, Call* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetCalls")]
			public unsafe static extern void GetCalls(Client* self, Discord_CallSpan* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetCurrentInputDevice")]
			public unsafe static extern void GetCurrentInputDevice(Client* self, GetCurrentInputDeviceCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetCurrentOutputDevice")]
			public unsafe static extern void GetCurrentOutputDevice(Client* self, GetCurrentOutputDeviceCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetInputDevices")]
			public unsafe static extern void GetInputDevices(Client* self, GetInputDevicesCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetInputVolume")]
			public unsafe static extern float GetInputVolume(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetOutputDevices")]
			public unsafe static extern void GetOutputDevices(Client* self, GetOutputDevicesCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetOutputVolume")]
			public unsafe static extern float GetOutputVolume(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetSelfDeafAll")]
			public unsafe static extern bool GetSelfDeafAll(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetSelfMuteAll")]
			public unsafe static extern bool GetSelfMuteAll(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetAutomaticGainControl")]
			public unsafe static extern void SetAutomaticGainControl(Client* self, bool on);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetDeviceChangeCallback")]
			public unsafe static extern void SetDeviceChangeCallback(Client* self, DeviceChangeCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetEchoCancellation")]
			public unsafe static extern void SetEchoCancellation(Client* self, bool on);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetEngineManagedAudioSession")]
			public unsafe static extern void SetEngineManagedAudioSession(Client* self, bool isEngineManaged);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetInputDevice")]
			public unsafe static extern void SetInputDevice(Client* self, Discord_String deviceId, SetInputDeviceCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetInputVolume")]
			public unsafe static extern void SetInputVolume(Client* self, float inputVolume);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetNoAudioInputCallback")]
			public unsafe static extern void SetNoAudioInputCallback(Client* self, NoAudioInputCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetNoAudioInputThreshold")]
			public unsafe static extern void SetNoAudioInputThreshold(Client* self, float dBFSThreshold);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetNoiseSuppression")]
			public unsafe static extern void SetNoiseSuppression(Client* self, bool on);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetOpusHardwareCoding")]
			public unsafe static extern void SetOpusHardwareCoding(Client* self, bool encode, bool decode);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetOutputDevice")]
			public unsafe static extern void SetOutputDevice(Client* self, Discord_String deviceId, SetOutputDeviceCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetOutputVolume")]
			public unsafe static extern void SetOutputVolume(Client* self, float outputVolume);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetSelfDeafAll")]
			public unsafe static extern void SetSelfDeafAll(Client* self, bool deaf);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetSelfMuteAll")]
			public unsafe static extern void SetSelfMuteAll(Client* self, bool mute);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetSpeakerMode")]
			public unsafe static extern bool SetSpeakerMode(Client* self, bool speakerMode);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetThreadPriority")]
			public unsafe static extern void SetThreadPriority(Client* self, Discord.Sdk.Client.Thread thread, int priority);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetVoiceParticipantChangedCallback")]
			public unsafe static extern void SetVoiceParticipantChangedCallback(Client* self, VoiceParticipantChangedCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_ShowAudioRoutePicker")]
			public unsafe static extern bool ShowAudioRoutePicker(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_StartCall")]
			public unsafe static extern bool StartCall(Client* self, ulong channelId, Call* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_StartCallWithAudioCallbacks")]
			public unsafe static extern bool StartCallWithAudioCallbacks(Client* self, ulong lobbyId, UserAudioReceivedCallback receivedCb, void* receivedCb__userDataFree, void* receivedCb__userData, UserAudioCapturedCallback capturedCb, void* capturedCb__userDataFree, void* capturedCb__userData, Call* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_AbortAuthorize")]
			public unsafe static extern void AbortAuthorize(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_AbortGetTokenFromDevice")]
			public unsafe static extern void AbortGetTokenFromDevice(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_Authorize")]
			public unsafe static extern void Authorize(Client* self, AuthorizationArgs* args, AuthorizationCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_CloseAuthorizeDeviceScreen")]
			public unsafe static extern void CloseAuthorizeDeviceScreen(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_CreateAuthorizationCodeVerifier")]
			public unsafe static extern void CreateAuthorizationCodeVerifier(Client* self, AuthorizationCodeVerifier* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_FetchCurrentUser")]
			public unsafe static extern void FetchCurrentUser(Client* self, AuthorizationTokenType tokenType, Discord_String token, FetchCurrentUserCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetProvisionalToken")]
			public unsafe static extern void GetProvisionalToken(Client* self, ulong applicationId, AuthenticationExternalAuthType externalAuthType, Discord_String externalAuthToken, TokenExchangeCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetToken")]
			public unsafe static extern void GetToken(Client* self, ulong applicationId, Discord_String code, Discord_String codeVerifier, Discord_String redirectUri, TokenExchangeCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetTokenFromDevice")]
			public unsafe static extern void GetTokenFromDevice(Client* self, DeviceAuthorizationArgs* args, TokenExchangeCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetTokenFromDeviceProvisionalMerge")]
			public unsafe static extern void GetTokenFromDeviceProvisionalMerge(Client* self, DeviceAuthorizationArgs* args, AuthenticationExternalAuthType externalAuthType, Discord_String externalAuthToken, TokenExchangeCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetTokenFromProvisionalMerge")]
			public unsafe static extern void GetTokenFromProvisionalMerge(Client* self, ulong applicationId, Discord_String code, Discord_String codeVerifier, Discord_String redirectUri, AuthenticationExternalAuthType externalAuthType, Discord_String externalAuthToken, TokenExchangeCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_IsAuthenticated")]
			public unsafe static extern bool IsAuthenticated(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_OpenAuthorizeDeviceScreen")]
			public unsafe static extern void OpenAuthorizeDeviceScreen(Client* self, ulong clientId, Discord_String userCode);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_ProvisionalUserMergeCompleted")]
			public unsafe static extern void ProvisionalUserMergeCompleted(Client* self, bool success);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_RefreshToken")]
			public unsafe static extern void RefreshToken(Client* self, ulong applicationId, Discord_String refreshToken, TokenExchangeCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetAuthorizeDeviceScreenClosedCallback")]
			public unsafe static extern void SetAuthorizeDeviceScreenClosedCallback(Client* self, AuthorizeDeviceScreenClosedCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetGameWindowPid")]
			public unsafe static extern void SetGameWindowPid(Client* self, int pid);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetTokenExpirationCallback")]
			public unsafe static extern void SetTokenExpirationCallback(Client* self, TokenExpirationCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_UpdateProvisionalAccountDisplayName")]
			public unsafe static extern void UpdateProvisionalAccountDisplayName(Client* self, Discord_String name, UpdateProvisionalAccountDisplayNameCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_UpdateToken")]
			public unsafe static extern void UpdateToken(Client* self, AuthorizationTokenType tokenType, Discord_String token, UpdateTokenCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_CanOpenMessageInDiscord")]
			public unsafe static extern bool CanOpenMessageInDiscord(Client* self, ulong messageId);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_DeleteUserMessage")]
			public unsafe static extern void DeleteUserMessage(Client* self, ulong recipientId, ulong messageId, DeleteUserMessageCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_EditUserMessage")]
			public unsafe static extern void EditUserMessage(Client* self, ulong recipientId, ulong messageId, Discord_String content, EditUserMessageCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetChannelHandle")]
			public unsafe static extern bool GetChannelHandle(Client* self, ulong channelId, ChannelHandle* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetMessageHandle")]
			public unsafe static extern bool GetMessageHandle(Client* self, ulong messageId, MessageHandle* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_OpenMessageInDiscord")]
			public unsafe static extern void OpenMessageInDiscord(Client* self, ulong messageId, ProvisionalUserMergeRequiredCallback provisionalUserMergeRequiredCallback, void* provisionalUserMergeRequiredCallback__userDataFree, void* provisionalUserMergeRequiredCallback__userData, OpenMessageInDiscordCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SendLobbyMessage")]
			public unsafe static extern void SendLobbyMessage(Client* self, ulong lobbyId, Discord_String content, SendUserMessageCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SendLobbyMessageWithMetadata")]
			public unsafe static extern void SendLobbyMessageWithMetadata(Client* self, ulong lobbyId, Discord_String content, Discord_Properties metadata, SendUserMessageCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SendUserMessage")]
			public unsafe static extern void SendUserMessage(Client* self, ulong recipientId, Discord_String content, SendUserMessageCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SendUserMessageWithMetadata")]
			public unsafe static extern void SendUserMessageWithMetadata(Client* self, ulong recipientId, Discord_String content, Discord_Properties metadata, SendUserMessageCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetMessageCreatedCallback")]
			public unsafe static extern void SetMessageCreatedCallback(Client* self, MessageCreatedCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetMessageDeletedCallback")]
			public unsafe static extern void SetMessageDeletedCallback(Client* self, MessageDeletedCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetMessageUpdatedCallback")]
			public unsafe static extern void SetMessageUpdatedCallback(Client* self, MessageUpdatedCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetShowingChat")]
			public unsafe static extern void SetShowingChat(Client* self, bool showingChat);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_AddLogCallback")]
			public unsafe static extern void AddLogCallback(Client* self, LogCallback callback, void* callback__userDataFree, void* callback__userData, LoggingSeverity minSeverity);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_AddVoiceLogCallback")]
			public unsafe static extern void AddVoiceLogCallback(Client* self, LogCallback callback, void* callback__userDataFree, void* callback__userData, LoggingSeverity minSeverity);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_Connect")]
			public unsafe static extern void Connect(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_Disconnect")]
			public unsafe static extern void Disconnect(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetStatus")]
			public unsafe static extern Discord.Sdk.Client.Status GetStatus(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_OpenConnectedGamesSettingsInDiscord")]
			public unsafe static extern void OpenConnectedGamesSettingsInDiscord(Client* self, OpenConnectedGamesSettingsInDiscordCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetApplicationId")]
			public unsafe static extern void SetApplicationId(Client* self, ulong applicationId);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetLogDir")]
			public unsafe static extern bool SetLogDir(Client* self, Discord_String path, LoggingSeverity minSeverity);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetStatusChangedCallback")]
			public unsafe static extern void SetStatusChangedCallback(Client* self, OnStatusChanged cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetVoiceLogDir")]
			public unsafe static extern void SetVoiceLogDir(Client* self, Discord_String path, LoggingSeverity minSeverity);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_CreateOrJoinLobby")]
			public unsafe static extern void CreateOrJoinLobby(Client* self, Discord_String secret, CreateOrJoinLobbyCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_CreateOrJoinLobbyWithMetadata")]
			public unsafe static extern void CreateOrJoinLobbyWithMetadata(Client* self, Discord_String secret, Discord_Properties lobbyMetadata, Discord_Properties memberMetadata, CreateOrJoinLobbyCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetGuildChannels")]
			public unsafe static extern void GetGuildChannels(Client* self, ulong guildId, GetGuildChannelsCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetLobbyHandle")]
			public unsafe static extern bool GetLobbyHandle(Client* self, ulong lobbyId, LobbyHandle* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetLobbyIds")]
			public unsafe static extern void GetLobbyIds(Client* self, Discord_UInt64Span* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetUserGuilds")]
			public unsafe static extern void GetUserGuilds(Client* self, GetUserGuildsCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_LeaveLobby")]
			public unsafe static extern void LeaveLobby(Client* self, ulong lobbyId, LeaveLobbyCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_LinkChannelToLobby")]
			public unsafe static extern void LinkChannelToLobby(Client* self, ulong lobbyId, ulong channelId, LinkOrUnlinkChannelCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetLobbyCreatedCallback")]
			public unsafe static extern void SetLobbyCreatedCallback(Client* self, LobbyCreatedCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetLobbyDeletedCallback")]
			public unsafe static extern void SetLobbyDeletedCallback(Client* self, LobbyDeletedCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetLobbyMemberAddedCallback")]
			public unsafe static extern void SetLobbyMemberAddedCallback(Client* self, LobbyMemberAddedCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetLobbyMemberRemovedCallback")]
			public unsafe static extern void SetLobbyMemberRemovedCallback(Client* self, LobbyMemberRemovedCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetLobbyMemberUpdatedCallback")]
			public unsafe static extern void SetLobbyMemberUpdatedCallback(Client* self, LobbyMemberUpdatedCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetLobbyUpdatedCallback")]
			public unsafe static extern void SetLobbyUpdatedCallback(Client* self, LobbyUpdatedCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_UnlinkChannelFromLobby")]
			public unsafe static extern void UnlinkChannelFromLobby(Client* self, ulong lobbyId, LinkOrUnlinkChannelCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_AcceptActivityInvite")]
			public unsafe static extern void AcceptActivityInvite(Client* self, ActivityInvite* invite, AcceptActivityInviteCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_ClearRichPresence")]
			public unsafe static extern void ClearRichPresence(Client* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_RegisterLaunchCommand")]
			public unsafe static extern bool RegisterLaunchCommand(Client* self, ulong applicationId, Discord_String command);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_RegisterLaunchSteamApplication")]
			public unsafe static extern bool RegisterLaunchSteamApplication(Client* self, ulong applicationId, uint steamAppId);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SendActivityInvite")]
			public unsafe static extern void SendActivityInvite(Client* self, ulong userId, Discord_String content, SendActivityInviteCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SendActivityJoinRequest")]
			public unsafe static extern void SendActivityJoinRequest(Client* self, ulong userId, SendActivityInviteCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SendActivityJoinRequestReply")]
			public unsafe static extern void SendActivityJoinRequestReply(Client* self, ActivityInvite* invite, SendActivityInviteCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetActivityInviteCreatedCallback")]
			public unsafe static extern void SetActivityInviteCreatedCallback(Client* self, ActivityInviteCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetActivityInviteUpdatedCallback")]
			public unsafe static extern void SetActivityInviteUpdatedCallback(Client* self, ActivityInviteCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetActivityJoinCallback")]
			public unsafe static extern void SetActivityJoinCallback(Client* self, ActivityJoinCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetOnlineStatus")]
			public unsafe static extern void SetOnlineStatus(Client* self, StatusType status, UpdateStatusCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_UpdateRichPresence")]
			public unsafe static extern void UpdateRichPresence(Client* self, Activity* activity, UpdateRichPresenceCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_AcceptDiscordFriendRequest")]
			public unsafe static extern void AcceptDiscordFriendRequest(Client* self, ulong userId, UpdateRelationshipCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_AcceptGameFriendRequest")]
			public unsafe static extern void AcceptGameFriendRequest(Client* self, ulong userId, UpdateRelationshipCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_BlockUser")]
			public unsafe static extern void BlockUser(Client* self, ulong userId, UpdateRelationshipCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_CancelDiscordFriendRequest")]
			public unsafe static extern void CancelDiscordFriendRequest(Client* self, ulong userId, UpdateRelationshipCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_CancelGameFriendRequest")]
			public unsafe static extern void CancelGameFriendRequest(Client* self, ulong userId, UpdateRelationshipCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetRelationshipHandle")]
			public unsafe static extern void GetRelationshipHandle(Client* self, ulong userId, RelationshipHandle* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetRelationships")]
			public unsafe static extern void GetRelationships(Client* self, Discord_RelationshipHandleSpan* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_RejectDiscordFriendRequest")]
			public unsafe static extern void RejectDiscordFriendRequest(Client* self, ulong userId, UpdateRelationshipCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_RejectGameFriendRequest")]
			public unsafe static extern void RejectGameFriendRequest(Client* self, ulong userId, UpdateRelationshipCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_RemoveDiscordAndGameFriend")]
			public unsafe static extern void RemoveDiscordAndGameFriend(Client* self, ulong userId, UpdateRelationshipCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_RemoveGameFriend")]
			public unsafe static extern void RemoveGameFriend(Client* self, ulong userId, UpdateRelationshipCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SearchFriendsByUsername")]
			public unsafe static extern void SearchFriendsByUsername(Client* self, Discord_String searchStr, Discord_UserHandleSpan* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SendDiscordFriendRequest")]
			public unsafe static extern void SendDiscordFriendRequest(Client* self, Discord_String username, SendFriendRequestCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SendDiscordFriendRequestById")]
			public unsafe static extern void SendDiscordFriendRequestById(Client* self, ulong userId, UpdateRelationshipCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SendGameFriendRequest")]
			public unsafe static extern void SendGameFriendRequest(Client* self, Discord_String username, SendFriendRequestCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SendGameFriendRequestById")]
			public unsafe static extern void SendGameFriendRequestById(Client* self, ulong userId, UpdateRelationshipCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetRelationshipCreatedCallback")]
			public unsafe static extern void SetRelationshipCreatedCallback(Client* self, RelationshipCreatedCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetRelationshipDeletedCallback")]
			public unsafe static extern void SetRelationshipDeletedCallback(Client* self, RelationshipDeletedCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_UnblockUser")]
			public unsafe static extern void UnblockUser(Client* self, ulong userId, UpdateRelationshipCallback cb, void* cb__userDataFree, void* cb__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetCurrentUser")]
			public unsafe static extern void GetCurrentUser(Client* self, UserHandle* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetDiscordClientConnectedUser")]
			public unsafe static extern void GetDiscordClientConnectedUser(Client* self, ulong applicationId, GetDiscordClientConnectedUserCallback callback, void* callback__userDataFree, void* callback__userData);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_GetUser")]
			public unsafe static extern bool GetUser(Client* self, ulong userId, UserHandle* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Client_SetUserUpdatedCallback")]
			public unsafe static extern void SetUserUpdatedCallback(Client* self, UserUpdatedCallback cb, void* cb__userDataFree, void* cb__userData);
		}

		public struct CallInfoHandle
		{
			public IntPtr Handle;

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_CallInfoHandle_Drop")]
			public unsafe static extern void Drop(CallInfoHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_CallInfoHandle_Clone")]
			public unsafe static extern void Clone(CallInfoHandle* self, CallInfoHandle* other);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_CallInfoHandle_ChannelId")]
			public unsafe static extern ulong ChannelId(CallInfoHandle* self);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_CallInfoHandle_GetParticipants")]
			public unsafe static extern void GetParticipants(CallInfoHandle* self, Discord_UInt64Span* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_CallInfoHandle_GetVoiceStateHandle")]
			public unsafe static extern bool GetVoiceStateHandle(CallInfoHandle* self, ulong userId, VoiceStateHandle* returnValue);

			[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_CallInfoHandle_GuildId")]
			public unsafe static extern ulong GuildId(CallInfoHandle* self);
		}

		public const string LibraryName = "discord_partner_sdk";

		public static event Action<Exception>? UnhandledException;

		static NativeMethods()
		{
			PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
			List<PlayerLoopSystem> list = currentPlayerLoop.subSystemList.ToList();
			PlayerLoopSystem item = new PlayerLoopSystem
			{
				type = typeof(NativeMethods),
				updateDelegate = Discord_RunCallbacks
			};
			list.Insert(0, item);
			currentPlayerLoop.subSystemList = list.ToArray();
			PlayerLoop.SetPlayerLoop(currentPlayerLoop);
			Discord_ResetCallbacks();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void __Init()
		{
		}

		public static void __ReportUnhandledException(Exception ex)
		{
			Action<Exception> unhandledException = NativeMethods.UnhandledException;
			if (unhandledException != null)
			{
				unhandledException(ex);
			}
			else
			{
				UnityEngine.Debug.LogException(ex);
			}
		}

		public static void __OnPostConstruct(object obj)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void __InitString(Discord_String* str, string value)
		{
			str->ptr = (byte*)(void*)Marshal.StringToCoTaskMemUTF8(value);
			str->size = (UIntPtr)(ulong)Encoding.UTF8.GetByteCount(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void __FreeString(Discord_String* str)
		{
			Marshal.FreeCoTaskMem((IntPtr)str->ptr);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static bool __InitStringLocal(byte* buf, int* bufUsed, int bufCapacity, Discord_String* str, string value)
		{
			int byteCount = Encoding.UTF8.GetByteCount(value);
			if (*bufUsed + byteCount > bufCapacity)
			{
				str->ptr = (byte*)(void*)Marshal.StringToCoTaskMemUTF8(value);
				str->size = (UIntPtr)(ulong)byteCount;
				return true;
			}
			Span<byte> bytes = new Span<byte>(buf + *bufUsed, bufCapacity - *bufUsed);
			int bytes2 = Encoding.UTF8.GetBytes(value, bytes);
			str->ptr = buf + *bufUsed;
			*bufUsed += bytes2;
			str->size = (UIntPtr)(ulong)byteCount;
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static bool __InitNullableStringLocal(byte* buf, int* bufUsed, int bufCapacity, Discord_String* str, string? value)
		{
			if (value == null)
			{
				str->ptr = null;
				str->size = UIntPtr.Zero;
				return false;
			}
			return __InitStringLocal(buf, bufUsed, bufCapacity, str, value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static bool __AllocLocal(byte* buf, int* bufUsed, int bufCapacity, void** ptrOut, int size)
		{
			int num = (size + 7) & -8;
			if (*bufUsed + num > bufCapacity)
			{
				*ptrOut = (void*)Marshal.AllocCoTaskMem(size);
				return true;
			}
			*ptrOut = buf + *bufUsed + (num - size);
			*bufUsed += num;
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static bool __AllocLocalStringArray(byte* buf, int* bufUsed, int bufCapacity, Discord_String** ptrOut, int count)
		{
			void* ptr = default(void*);
			bool result = __AllocLocal(buf, bufUsed, bufCapacity, &ptr, count * sizeof(Discord_String));
			*ptrOut = (Discord_String*)ptr;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static bool __AllocateLocalBoolArray(byte* buf, int* bufUsed, int bufCapacity, bool** ptrOut, int count)
		{
			void* ptr = default(void*);
			bool result = __AllocLocal(buf, bufUsed, bufCapacity, &ptr, count);
			*ptrOut = (bool*)ptr;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void __FreeLocal(Discord_String* str, bool owned)
		{
			if (owned)
			{
				Marshal.FreeCoTaskMem((IntPtr)str->ptr);
			}
		}

		[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl)]
		public unsafe static extern void* Discord_Alloc(UIntPtr size);

		[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl)]
		public unsafe static extern void Discord_Free(void* ptr);

		[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Discord_FreeProperties(Discord_Properties props);

		[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Discord_SetFreeThreaded();

		[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Discord_RunCallbacks();

		[DllImport("discord_partner_sdk", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Discord_ResetCallbacks();
	}
	public class ActivityInvite : IDisposable
	{
		internal NativeMethods.ActivityInvite self;

		private int disposed_;

		internal ActivityInvite(NativeMethods.ActivityInvite self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~ActivityInvite()
		{
			Dispose();
		}

		public unsafe ActivityInvite()
		{
			NativeMethods.__Init();
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				NativeMethods.ActivityInvite.Init(ptr);
			}
			NativeMethods.__OnPostConstruct(this);
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.ActivityInvite* ptr = &self)
				{
					NativeMethods.ActivityInvite.Drop(ptr);
				}
			}
		}

		public unsafe ActivityInvite(ActivityInvite other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.ActivityInvite* rhs = &other.self)
			{
				fixed (NativeMethods.ActivityInvite* ptr = &self)
				{
					NativeMethods.ActivityInvite.Clone(ptr, rhs);
				}
			}
		}

		internal unsafe ActivityInvite(NativeMethods.ActivityInvite* otherPtr)
		{
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				NativeMethods.ActivityInvite.Clone(ptr, otherPtr);
			}
		}

		public unsafe ulong SenderId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			ulong result;
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				result = NativeMethods.ActivityInvite.SenderId(ptr);
			}
			return result;
		}

		public unsafe void SetSenderId(ulong value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				NativeMethods.ActivityInvite.SetSenderId(ptr, value);
			}
		}

		public unsafe ulong ChannelId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			ulong result;
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				result = NativeMethods.ActivityInvite.ChannelId(ptr);
			}
			return result;
		}

		public unsafe void SetChannelId(ulong value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				NativeMethods.ActivityInvite.SetChannelId(ptr, value);
			}
		}

		public unsafe ulong MessageId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			ulong result;
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				result = NativeMethods.ActivityInvite.MessageId(ptr);
			}
			return result;
		}

		public unsafe void SetMessageId(ulong value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				NativeMethods.ActivityInvite.SetMessageId(ptr, value);
			}
		}

		public unsafe ActivityActionTypes Type()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			ActivityActionTypes result;
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				result = NativeMethods.ActivityInvite.Type(ptr);
			}
			return result;
		}

		public unsafe void SetType(ActivityActionTypes value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				NativeMethods.ActivityInvite.SetType(ptr, value);
			}
		}

		public unsafe ulong ApplicationId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			ulong result;
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				result = NativeMethods.ActivityInvite.ApplicationId(ptr);
			}
			return result;
		}

		public unsafe void SetApplicationId(ulong value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				NativeMethods.ActivityInvite.SetApplicationId(ptr, value);
			}
		}

		public unsafe string PartyId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				NativeMethods.ActivityInvite.PartyId(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetPartyId(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				NativeMethods.ActivityInvite.SetPartyId(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}

		public unsafe string SessionId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				NativeMethods.ActivityInvite.SessionId(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetSessionId(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				NativeMethods.ActivityInvite.SetSessionId(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}

		public unsafe bool IsValid()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			bool result;
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				result = NativeMethods.ActivityInvite.IsValid(ptr);
			}
			return result;
		}

		public unsafe void SetIsValid(bool value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityInvite");
			}
			fixed (NativeMethods.ActivityInvite* ptr = &self)
			{
				NativeMethods.ActivityInvite.SetIsValid(ptr, value);
			}
		}
	}
	public class ActivityAssets : IDisposable
	{
		internal NativeMethods.ActivityAssets self;

		private int disposed_;

		internal ActivityAssets(NativeMethods.ActivityAssets self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~ActivityAssets()
		{
			Dispose();
		}

		public unsafe ActivityAssets()
		{
			NativeMethods.__Init();
			fixed (NativeMethods.ActivityAssets* ptr = &self)
			{
				NativeMethods.ActivityAssets.Init(ptr);
			}
			NativeMethods.__OnPostConstruct(this);
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.ActivityAssets* ptr = &self)
				{
					NativeMethods.ActivityAssets.Drop(ptr);
				}
			}
		}

		public unsafe ActivityAssets(ActivityAssets other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityAssets");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.ActivityAssets* arg = &other.self)
			{
				fixed (NativeMethods.ActivityAssets* ptr = &self)
				{
					NativeMethods.ActivityAssets.Clone(ptr, arg);
				}
			}
		}

		internal unsafe ActivityAssets(NativeMethods.ActivityAssets* otherPtr)
		{
			fixed (NativeMethods.ActivityAssets* ptr = &self)
			{
				NativeMethods.ActivityAssets.Clone(ptr, otherPtr);
			}
		}

		public unsafe string? LargeImage()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityAssets");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool num;
			fixed (NativeMethods.ActivityAssets* ptr = &self)
			{
				num = NativeMethods.ActivityAssets.LargeImage(ptr, &discord_String);
			}
			if (!num)
			{
				return null;
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetLargeImage(string? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityAssets");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitNullableStringLocal(buf, &num, 1024, &discord_String, value);
			fixed (NativeMethods.ActivityAssets* ptr = &self)
			{
				NativeMethods.ActivityAssets.SetLargeImage(ptr, (value != null) ? (&discord_String) : null);
			}
			NativeMethods.__FreeLocal(&discord_String, owned);
		}

		public unsafe string? LargeText()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityAssets");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool num;
			fixed (NativeMethods.ActivityAssets* ptr = &self)
			{
				num = NativeMethods.ActivityAssets.LargeText(ptr, &discord_String);
			}
			if (!num)
			{
				return null;
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetLargeText(string? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityAssets");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitNullableStringLocal(buf, &num, 1024, &discord_String, value);
			fixed (NativeMethods.ActivityAssets* ptr = &self)
			{
				NativeMethods.ActivityAssets.SetLargeText(ptr, (value != null) ? (&discord_String) : null);
			}
			NativeMethods.__FreeLocal(&discord_String, owned);
		}

		public unsafe string? SmallImage()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityAssets");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool num;
			fixed (NativeMethods.ActivityAssets* ptr = &self)
			{
				num = NativeMethods.ActivityAssets.SmallImage(ptr, &discord_String);
			}
			if (!num)
			{
				return null;
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetSmallImage(string? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityAssets");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitNullableStringLocal(buf, &num, 1024, &discord_String, value);
			fixed (NativeMethods.ActivityAssets* ptr = &self)
			{
				NativeMethods.ActivityAssets.SetSmallImage(ptr, (value != null) ? (&discord_String) : null);
			}
			NativeMethods.__FreeLocal(&discord_String, owned);
		}

		public unsafe string? SmallText()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityAssets");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool num;
			fixed (NativeMethods.ActivityAssets* ptr = &self)
			{
				num = NativeMethods.ActivityAssets.SmallText(ptr, &discord_String);
			}
			if (!num)
			{
				return null;
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetSmallText(string? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityAssets");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitNullableStringLocal(buf, &num, 1024, &discord_String, value);
			fixed (NativeMethods.ActivityAssets* ptr = &self)
			{
				NativeMethods.ActivityAssets.SetSmallText(ptr, (value != null) ? (&discord_String) : null);
			}
			NativeMethods.__FreeLocal(&discord_String, owned);
		}
	}
	public class ActivityTimestamps : IDisposable
	{
		internal NativeMethods.ActivityTimestamps self;

		private int disposed_;

		internal ActivityTimestamps(NativeMethods.ActivityTimestamps self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~ActivityTimestamps()
		{
			Dispose();
		}

		public unsafe ActivityTimestamps()
		{
			NativeMethods.__Init();
			fixed (NativeMethods.ActivityTimestamps* ptr = &self)
			{
				NativeMethods.ActivityTimestamps.Init(ptr);
			}
			NativeMethods.__OnPostConstruct(this);
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.ActivityTimestamps* ptr = &self)
				{
					NativeMethods.ActivityTimestamps.Drop(ptr);
				}
			}
		}

		public unsafe ActivityTimestamps(ActivityTimestamps other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityTimestamps");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.ActivityTimestamps* arg = &other.self)
			{
				fixed (NativeMethods.ActivityTimestamps* ptr = &self)
				{
					NativeMethods.ActivityTimestamps.Clone(ptr, arg);
				}
			}
		}

		internal unsafe ActivityTimestamps(NativeMethods.ActivityTimestamps* otherPtr)
		{
			fixed (NativeMethods.ActivityTimestamps* ptr = &self)
			{
				NativeMethods.ActivityTimestamps.Clone(ptr, otherPtr);
			}
		}

		public unsafe ulong Start()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityTimestamps");
			}
			ulong result;
			fixed (NativeMethods.ActivityTimestamps* ptr = &self)
			{
				result = NativeMethods.ActivityTimestamps.Start(ptr);
			}
			return result;
		}

		public unsafe void SetStart(ulong value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityTimestamps");
			}
			fixed (NativeMethods.ActivityTimestamps* ptr = &self)
			{
				NativeMethods.ActivityTimestamps.SetStart(ptr, value);
			}
		}

		public unsafe ulong End()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityTimestamps");
			}
			ulong result;
			fixed (NativeMethods.ActivityTimestamps* ptr = &self)
			{
				result = NativeMethods.ActivityTimestamps.End(ptr);
			}
			return result;
		}

		public unsafe void SetEnd(ulong value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityTimestamps");
			}
			fixed (NativeMethods.ActivityTimestamps* ptr = &self)
			{
				NativeMethods.ActivityTimestamps.SetEnd(ptr, value);
			}
		}
	}
	public class ActivityParty : IDisposable
	{
		internal NativeMethods.ActivityParty self;

		private int disposed_;

		internal ActivityParty(NativeMethods.ActivityParty self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~ActivityParty()
		{
			Dispose();
		}

		public unsafe ActivityParty()
		{
			NativeMethods.__Init();
			fixed (NativeMethods.ActivityParty* ptr = &self)
			{
				NativeMethods.ActivityParty.Init(ptr);
			}
			NativeMethods.__OnPostConstruct(this);
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.ActivityParty* ptr = &self)
				{
					NativeMethods.ActivityParty.Drop(ptr);
				}
			}
		}

		public unsafe ActivityParty(ActivityParty other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityParty");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.ActivityParty* arg = &other.self)
			{
				fixed (NativeMethods.ActivityParty* ptr = &self)
				{
					NativeMethods.ActivityParty.Clone(ptr, arg);
				}
			}
		}

		internal unsafe ActivityParty(NativeMethods.ActivityParty* otherPtr)
		{
			fixed (NativeMethods.ActivityParty* ptr = &self)
			{
				NativeMethods.ActivityParty.Clone(ptr, otherPtr);
			}
		}

		public unsafe string Id()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityParty");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.ActivityParty* ptr = &self)
			{
				NativeMethods.ActivityParty.Id(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetId(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityParty");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.ActivityParty* ptr = &self)
			{
				NativeMethods.ActivityParty.SetId(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}

		public unsafe int CurrentSize()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityParty");
			}
			int result;
			fixed (NativeMethods.ActivityParty* ptr = &self)
			{
				result = NativeMethods.ActivityParty.CurrentSize(ptr);
			}
			return result;
		}

		public unsafe void SetCurrentSize(int value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityParty");
			}
			fixed (NativeMethods.ActivityParty* ptr = &self)
			{
				NativeMethods.ActivityParty.SetCurrentSize(ptr, value);
			}
		}

		public unsafe int MaxSize()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityParty");
			}
			int result;
			fixed (NativeMethods.ActivityParty* ptr = &self)
			{
				result = NativeMethods.ActivityParty.MaxSize(ptr);
			}
			return result;
		}

		public unsafe void SetMaxSize(int value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityParty");
			}
			fixed (NativeMethods.ActivityParty* ptr = &self)
			{
				NativeMethods.ActivityParty.SetMaxSize(ptr, value);
			}
		}

		public unsafe ActivityPartyPrivacy Privacy()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityParty");
			}
			ActivityPartyPrivacy result;
			fixed (NativeMethods.ActivityParty* ptr = &self)
			{
				result = NativeMethods.ActivityParty.Privacy(ptr);
			}
			return result;
		}

		public unsafe void SetPrivacy(ActivityPartyPrivacy value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityParty");
			}
			fixed (NativeMethods.ActivityParty* ptr = &self)
			{
				NativeMethods.ActivityParty.SetPrivacy(ptr, value);
			}
		}
	}
	public class ActivitySecrets : IDisposable
	{
		internal NativeMethods.ActivitySecrets self;

		private int disposed_;

		internal ActivitySecrets(NativeMethods.ActivitySecrets self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~ActivitySecrets()
		{
			Dispose();
		}

		public unsafe ActivitySecrets()
		{
			NativeMethods.__Init();
			fixed (NativeMethods.ActivitySecrets* ptr = &self)
			{
				NativeMethods.ActivitySecrets.Init(ptr);
			}
			NativeMethods.__OnPostConstruct(this);
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.ActivitySecrets* ptr = &self)
				{
					NativeMethods.ActivitySecrets.Drop(ptr);
				}
			}
		}

		public unsafe ActivitySecrets(ActivitySecrets other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivitySecrets");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.ActivitySecrets* arg = &other.self)
			{
				fixed (NativeMethods.ActivitySecrets* ptr = &self)
				{
					NativeMethods.ActivitySecrets.Clone(ptr, arg);
				}
			}
		}

		internal unsafe ActivitySecrets(NativeMethods.ActivitySecrets* otherPtr)
		{
			fixed (NativeMethods.ActivitySecrets* ptr = &self)
			{
				NativeMethods.ActivitySecrets.Clone(ptr, otherPtr);
			}
		}

		public unsafe string Join()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivitySecrets");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.ActivitySecrets* ptr = &self)
			{
				NativeMethods.ActivitySecrets.Join(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetJoin(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivitySecrets");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.ActivitySecrets* ptr = &self)
			{
				NativeMethods.ActivitySecrets.SetJoin(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}
	}
	public class ActivityButton : IDisposable
	{
		internal NativeMethods.ActivityButton self;

		private int disposed_;

		internal ActivityButton(NativeMethods.ActivityButton self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~ActivityButton()
		{
			Dispose();
		}

		public unsafe ActivityButton()
		{
			NativeMethods.__Init();
			fixed (NativeMethods.ActivityButton* ptr = &self)
			{
				NativeMethods.ActivityButton.Init(ptr);
			}
			NativeMethods.__OnPostConstruct(this);
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.ActivityButton* ptr = &self)
				{
					NativeMethods.ActivityButton.Drop(ptr);
				}
			}
		}

		public unsafe ActivityButton(ActivityButton other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityButton");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.ActivityButton* arg = &other.self)
			{
				fixed (NativeMethods.ActivityButton* ptr = &self)
				{
					NativeMethods.ActivityButton.Clone(ptr, arg);
				}
			}
		}

		internal unsafe ActivityButton(NativeMethods.ActivityButton* otherPtr)
		{
			fixed (NativeMethods.ActivityButton* ptr = &self)
			{
				NativeMethods.ActivityButton.Clone(ptr, otherPtr);
			}
		}

		public unsafe string Label()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityButton");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.ActivityButton* ptr = &self)
			{
				NativeMethods.ActivityButton.Label(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetLabel(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityButton");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.ActivityButton* ptr = &self)
			{
				NativeMethods.ActivityButton.SetLabel(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}

		public unsafe string Url()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityButton");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.ActivityButton* ptr = &self)
			{
				NativeMethods.ActivityButton.Url(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetUrl(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ActivityButton");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.ActivityButton* ptr = &self)
			{
				NativeMethods.ActivityButton.SetUrl(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}
	}
	public class Activity : IDisposable
	{
		internal NativeMethods.Activity self;

		private int disposed_;

		internal Activity(NativeMethods.Activity self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~Activity()
		{
			Dispose();
		}

		public unsafe Activity()
		{
			NativeMethods.__Init();
			fixed (NativeMethods.Activity* ptr = &self)
			{
				NativeMethods.Activity.Init(ptr);
			}
			NativeMethods.__OnPostConstruct(this);
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.Activity* ptr = &self)
				{
					NativeMethods.Activity.Drop(ptr);
				}
			}
		}

		public unsafe Activity(Activity other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.Activity* arg = &other.self)
			{
				fixed (NativeMethods.Activity* ptr = &self)
				{
					NativeMethods.Activity.Clone(ptr, arg);
				}
			}
		}

		internal unsafe Activity(NativeMethods.Activity* otherPtr)
		{
			fixed (NativeMethods.Activity* ptr = &self)
			{
				NativeMethods.Activity.Clone(ptr, otherPtr);
			}
		}

		public unsafe void AddButton(ActivityButton button)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			fixed (NativeMethods.ActivityButton* button2 = &button.self)
			{
				fixed (NativeMethods.Activity* ptr = &self)
				{
					NativeMethods.Activity.AddButton(ptr, button2);
				}
			}
		}

		public unsafe bool Equals(Activity other)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			bool result;
			fixed (NativeMethods.Activity* other2 = &other.self)
			{
				fixed (NativeMethods.Activity* ptr = &self)
				{
					result = NativeMethods.Activity.Equals(ptr, other2);
				}
			}
			return result;
		}

		public unsafe ActivityButton[] GetButtons()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			NativeMethods.Discord_ActivityButtonSpan discord_ActivityButtonSpan = default(NativeMethods.Discord_ActivityButtonSpan);
			fixed (NativeMethods.Activity* ptr = &self)
			{
				NativeMethods.Activity.GetButtons(ptr, &discord_ActivityButtonSpan);
			}
			ActivityButton[] array = new ActivityButton[(uint)discord_ActivityButtonSpan.size];
			for (int i = 0; i < (int)(uint)discord_ActivityButtonSpan.size; i++)
			{
				array[i] = new ActivityButton(discord_ActivityButtonSpan.ptr[i], 0);
			}
			NativeMethods.Discord_Free(discord_ActivityButtonSpan.ptr);
			return array;
		}

		public unsafe string Name()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.Activity* ptr = &self)
			{
				NativeMethods.Activity.Name(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetName(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.Activity* ptr = &self)
			{
				NativeMethods.Activity.SetName(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}

		public unsafe ActivityTypes Type()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			ActivityTypes result;
			fixed (NativeMethods.Activity* ptr = &self)
			{
				result = NativeMethods.Activity.Type(ptr);
			}
			return result;
		}

		public unsafe void SetType(ActivityTypes value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			fixed (NativeMethods.Activity* ptr = &self)
			{
				NativeMethods.Activity.SetType(ptr, value);
			}
		}

		public unsafe string? State()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool num;
			fixed (NativeMethods.Activity* ptr = &self)
			{
				num = NativeMethods.Activity.State(ptr, &discord_String);
			}
			if (!num)
			{
				return null;
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetState(string? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitNullableStringLocal(buf, &num, 1024, &discord_String, value);
			fixed (NativeMethods.Activity* ptr = &self)
			{
				NativeMethods.Activity.SetState(ptr, (value != null) ? (&discord_String) : null);
			}
			NativeMethods.__FreeLocal(&discord_String, owned);
		}

		public unsafe string? Details()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool num;
			fixed (NativeMethods.Activity* ptr = &self)
			{
				num = NativeMethods.Activity.Details(ptr, &discord_String);
			}
			if (!num)
			{
				return null;
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetDetails(string? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitNullableStringLocal(buf, &num, 1024, &discord_String, value);
			fixed (NativeMethods.Activity* ptr = &self)
			{
				NativeMethods.Activity.SetDetails(ptr, (value != null) ? (&discord_String) : null);
			}
			NativeMethods.__FreeLocal(&discord_String, owned);
		}

		public unsafe ulong? ApplicationId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			bool num;
			ulong value = default(ulong);
			fixed (NativeMethods.Activity* ptr = &self)
			{
				num = NativeMethods.Activity.ApplicationId(ptr, &value);
			}
			if (!num)
			{
				return null;
			}
			return value;
		}

		public unsafe void SetApplicationId(ulong? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			ulong valueOrDefault = value.GetValueOrDefault();
			fixed (NativeMethods.Activity* ptr = &self)
			{
				NativeMethods.Activity.SetApplicationId(ptr, value.HasValue ? (&valueOrDefault) : null);
			}
		}

		public unsafe ActivityAssets? Assets()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			NativeMethods.ActivityAssets activityAssets = default(NativeMethods.ActivityAssets);
			bool num;
			fixed (NativeMethods.Activity* ptr = &self)
			{
				num = NativeMethods.Activity.Assets(ptr, &activityAssets);
			}
			if (!num)
			{
				return null;
			}
			return new ActivityAssets(activityAssets, 0);
		}

		public unsafe void SetAssets(ActivityAssets? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			NativeMethods.ActivityAssets activityAssets = value?.self ?? default(NativeMethods.ActivityAssets);
			fixed (NativeMethods.Activity* ptr = &self)
			{
				NativeMethods.Activity.SetAssets(ptr, (value != null) ? (&activityAssets) : null);
			}
			if (value != null)
			{
				value.self = activityAssets;
			}
		}

		public unsafe ActivityTimestamps? Timestamps()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			NativeMethods.ActivityTimestamps activityTimestamps = default(NativeMethods.ActivityTimestamps);
			bool num;
			fixed (NativeMethods.Activity* ptr = &self)
			{
				num = NativeMethods.Activity.Timestamps(ptr, &activityTimestamps);
			}
			if (!num)
			{
				return null;
			}
			return new ActivityTimestamps(activityTimestamps, 0);
		}

		public unsafe void SetTimestamps(ActivityTimestamps? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			NativeMethods.ActivityTimestamps activityTimestamps = value?.self ?? default(NativeMethods.ActivityTimestamps);
			fixed (NativeMethods.Activity* ptr = &self)
			{
				NativeMethods.Activity.SetTimestamps(ptr, (value != null) ? (&activityTimestamps) : null);
			}
			if (value != null)
			{
				value.self = activityTimestamps;
			}
		}

		public unsafe ActivityParty? Party()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			NativeMethods.ActivityParty activityParty = default(NativeMethods.ActivityParty);
			bool num;
			fixed (NativeMethods.Activity* ptr = &self)
			{
				num = NativeMethods.Activity.Party(ptr, &activityParty);
			}
			if (!num)
			{
				return null;
			}
			return new ActivityParty(activityParty, 0);
		}

		public unsafe void SetParty(ActivityParty? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			NativeMethods.ActivityParty activityParty = value?.self ?? default(NativeMethods.ActivityParty);
			fixed (NativeMethods.Activity* ptr = &self)
			{
				NativeMethods.Activity.SetParty(ptr, (value != null) ? (&activityParty) : null);
			}
			if (value != null)
			{
				value.self = activityParty;
			}
		}

		public unsafe ActivitySecrets? Secrets()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			NativeMethods.ActivitySecrets activitySecrets = default(NativeMethods.ActivitySecrets);
			bool num;
			fixed (NativeMethods.Activity* ptr = &self)
			{
				num = NativeMethods.Activity.Secrets(ptr, &activitySecrets);
			}
			if (!num)
			{
				return null;
			}
			return new ActivitySecrets(activitySecrets, 0);
		}

		public unsafe void SetSecrets(ActivitySecrets? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			NativeMethods.ActivitySecrets activitySecrets = value?.self ?? default(NativeMethods.ActivitySecrets);
			fixed (NativeMethods.Activity* ptr = &self)
			{
				NativeMethods.Activity.SetSecrets(ptr, (value != null) ? (&activitySecrets) : null);
			}
			if (value != null)
			{
				value.self = activitySecrets;
			}
		}

		public unsafe ActivityGamePlatforms SupportedPlatforms()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			ActivityGamePlatforms result;
			fixed (NativeMethods.Activity* ptr = &self)
			{
				result = NativeMethods.Activity.SupportedPlatforms(ptr);
			}
			return result;
		}

		public unsafe void SetSupportedPlatforms(ActivityGamePlatforms value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Activity");
			}
			fixed (NativeMethods.Activity* ptr = &self)
			{
				NativeMethods.Activity.SetSupportedPlatforms(ptr, value);
			}
		}
	}
	public class ClientResult : IDisposable
	{
		internal NativeMethods.ClientResult self;

		private int disposed_;

		internal ClientResult(NativeMethods.ClientResult self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~ClientResult()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.ClientResult* ptr = &self)
				{
					NativeMethods.ClientResult.Drop(ptr);
				}
			}
		}

		public unsafe ClientResult(ClientResult other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.ClientResult* arg = &other.self)
			{
				fixed (NativeMethods.ClientResult* ptr = &self)
				{
					NativeMethods.ClientResult.Clone(ptr, arg);
				}
			}
		}

		internal unsafe ClientResult(NativeMethods.ClientResult* otherPtr)
		{
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				NativeMethods.ClientResult.Clone(ptr, otherPtr);
			}
		}

		public unsafe override string ToString()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				NativeMethods.ClientResult.ToString(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe ErrorType Type()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			ErrorType result;
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				result = NativeMethods.ClientResult.Type(ptr);
			}
			return result;
		}

		public unsafe void SetType(ErrorType value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				NativeMethods.ClientResult.SetType(ptr, value);
			}
		}

		public unsafe string Error()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				NativeMethods.ClientResult.Error(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetError(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				NativeMethods.ClientResult.SetError(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}

		public unsafe int ErrorCode()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			int result;
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				result = NativeMethods.ClientResult.ErrorCode(ptr);
			}
			return result;
		}

		public unsafe void SetErrorCode(int value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				NativeMethods.ClientResult.SetErrorCode(ptr, value);
			}
		}

		public unsafe HttpStatusCode Status()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			HttpStatusCode result;
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				result = NativeMethods.ClientResult.Status(ptr);
			}
			return result;
		}

		public unsafe void SetStatus(HttpStatusCode value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				NativeMethods.ClientResult.SetStatus(ptr, value);
			}
		}

		public unsafe string ResponseBody()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				NativeMethods.ClientResult.ResponseBody(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetResponseBody(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				NativeMethods.ClientResult.SetResponseBody(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}

		public unsafe bool Successful()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			bool result;
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				result = NativeMethods.ClientResult.Successful(ptr);
			}
			return result;
		}

		public unsafe void SetSuccessful(bool value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				NativeMethods.ClientResult.SetSuccessful(ptr, value);
			}
		}

		public unsafe bool Retryable()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			bool result;
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				result = NativeMethods.ClientResult.Retryable(ptr);
			}
			return result;
		}

		public unsafe void SetRetryable(bool value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				NativeMethods.ClientResult.SetRetryable(ptr, value);
			}
		}

		public unsafe float RetryAfter()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			float result;
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				result = NativeMethods.ClientResult.RetryAfter(ptr);
			}
			return result;
		}

		public unsafe void SetRetryAfter(float value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ClientResult");
			}
			fixed (NativeMethods.ClientResult* ptr = &self)
			{
				NativeMethods.ClientResult.SetRetryAfter(ptr, value);
			}
		}
	}
	public class AuthorizationCodeChallenge : IDisposable
	{
		internal NativeMethods.AuthorizationCodeChallenge self;

		private int disposed_;

		internal AuthorizationCodeChallenge(NativeMethods.AuthorizationCodeChallenge self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~AuthorizationCodeChallenge()
		{
			Dispose();
		}

		public unsafe AuthorizationCodeChallenge()
		{
			NativeMethods.__Init();
			fixed (NativeMethods.AuthorizationCodeChallenge* ptr = &self)
			{
				NativeMethods.AuthorizationCodeChallenge.Init(ptr);
			}
			NativeMethods.__OnPostConstruct(this);
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.AuthorizationCodeChallenge* ptr = &self)
				{
					NativeMethods.AuthorizationCodeChallenge.Drop(ptr);
				}
			}
		}

		public unsafe AuthorizationCodeChallenge(AuthorizationCodeChallenge other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationCodeChallenge");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.AuthorizationCodeChallenge* arg = &other.self)
			{
				fixed (NativeMethods.AuthorizationCodeChallenge* ptr = &self)
				{
					NativeMethods.AuthorizationCodeChallenge.Clone(ptr, arg);
				}
			}
		}

		internal unsafe AuthorizationCodeChallenge(NativeMethods.AuthorizationCodeChallenge* otherPtr)
		{
			fixed (NativeMethods.AuthorizationCodeChallenge* ptr = &self)
			{
				NativeMethods.AuthorizationCodeChallenge.Clone(ptr, otherPtr);
			}
		}

		public unsafe AuthenticationCodeChallengeMethod Method()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationCodeChallenge");
			}
			AuthenticationCodeChallengeMethod result;
			fixed (NativeMethods.AuthorizationCodeChallenge* ptr = &self)
			{
				result = NativeMethods.AuthorizationCodeChallenge.Method(ptr);
			}
			return result;
		}

		public unsafe void SetMethod(AuthenticationCodeChallengeMethod value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationCodeChallenge");
			}
			fixed (NativeMethods.AuthorizationCodeChallenge* ptr = &self)
			{
				NativeMethods.AuthorizationCodeChallenge.SetMethod(ptr, value);
			}
		}

		public unsafe string Challenge()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationCodeChallenge");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.AuthorizationCodeChallenge* ptr = &self)
			{
				NativeMethods.AuthorizationCodeChallenge.Challenge(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetChallenge(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationCodeChallenge");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.AuthorizationCodeChallenge* ptr = &self)
			{
				NativeMethods.AuthorizationCodeChallenge.SetChallenge(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}
	}
	public class AuthorizationCodeVerifier : IDisposable
	{
		internal NativeMethods.AuthorizationCodeVerifier self;

		private int disposed_;

		internal AuthorizationCodeVerifier(NativeMethods.AuthorizationCodeVerifier self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~AuthorizationCodeVerifier()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.AuthorizationCodeVerifier* ptr = &self)
				{
					NativeMethods.AuthorizationCodeVerifier.Drop(ptr);
				}
			}
		}

		public unsafe AuthorizationCodeVerifier(AuthorizationCodeVerifier other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationCodeVerifier");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.AuthorizationCodeVerifier* arg = &other.self)
			{
				fixed (NativeMethods.AuthorizationCodeVerifier* ptr = &self)
				{
					NativeMethods.AuthorizationCodeVerifier.Clone(ptr, arg);
				}
			}
		}

		internal unsafe AuthorizationCodeVerifier(NativeMethods.AuthorizationCodeVerifier* otherPtr)
		{
			fixed (NativeMethods.AuthorizationCodeVerifier* ptr = &self)
			{
				NativeMethods.AuthorizationCodeVerifier.Clone(ptr, otherPtr);
			}
		}

		public unsafe AuthorizationCodeChallenge Challenge()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationCodeVerifier");
			}
			NativeMethods.AuthorizationCodeChallenge authorizationCodeChallenge = default(NativeMethods.AuthorizationCodeChallenge);
			fixed (NativeMethods.AuthorizationCodeVerifier* ptr = &self)
			{
				NativeMethods.AuthorizationCodeVerifier.Challenge(ptr, &authorizationCodeChallenge);
			}
			return new AuthorizationCodeChallenge(authorizationCodeChallenge, 0);
		}

		public unsafe void SetChallenge(AuthorizationCodeChallenge value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationCodeVerifier");
			}
			fixed (NativeMethods.AuthorizationCodeChallenge* value2 = &value.self)
			{
				fixed (NativeMethods.AuthorizationCodeVerifier* ptr = &self)
				{
					NativeMethods.AuthorizationCodeVerifier.SetChallenge(ptr, value2);
				}
			}
		}

		public unsafe string Verifier()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationCodeVerifier");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.AuthorizationCodeVerifier* ptr = &self)
			{
				NativeMethods.AuthorizationCodeVerifier.Verifier(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetVerifier(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationCodeVerifier");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.AuthorizationCodeVerifier* ptr = &self)
			{
				NativeMethods.AuthorizationCodeVerifier.SetVerifier(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}
	}
	public class AuthorizationArgs : IDisposable
	{
		internal NativeMethods.AuthorizationArgs self;

		private int disposed_;

		internal AuthorizationArgs(NativeMethods.AuthorizationArgs self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~AuthorizationArgs()
		{
			Dispose();
		}

		public unsafe AuthorizationArgs()
		{
			NativeMethods.__Init();
			fixed (NativeMethods.AuthorizationArgs* ptr = &self)
			{
				NativeMethods.AuthorizationArgs.Init(ptr);
			}
			NativeMethods.__OnPostConstruct(this);
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.AuthorizationArgs* ptr = &self)
				{
					NativeMethods.AuthorizationArgs.Drop(ptr);
				}
			}
		}

		public unsafe AuthorizationArgs(AuthorizationArgs other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationArgs");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.AuthorizationArgs* arg = &other.self)
			{
				fixed (NativeMethods.AuthorizationArgs* ptr = &self)
				{
					NativeMethods.AuthorizationArgs.Clone(ptr, arg);
				}
			}
		}

		internal unsafe AuthorizationArgs(NativeMethods.AuthorizationArgs* otherPtr)
		{
			fixed (NativeMethods.AuthorizationArgs* ptr = &self)
			{
				NativeMethods.AuthorizationArgs.Clone(ptr, otherPtr);
			}
		}

		public unsafe ulong ClientId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationArgs");
			}
			ulong result;
			fixed (NativeMethods.AuthorizationArgs* ptr = &self)
			{
				result = NativeMethods.AuthorizationArgs.ClientId(ptr);
			}
			return result;
		}

		public unsafe void SetClientId(ulong value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationArgs");
			}
			fixed (NativeMethods.AuthorizationArgs* ptr = &self)
			{
				NativeMethods.AuthorizationArgs.SetClientId(ptr, value);
			}
		}

		public unsafe string Scopes()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationArgs");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.AuthorizationArgs* ptr = &self)
			{
				NativeMethods.AuthorizationArgs.Scopes(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetScopes(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationArgs");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.AuthorizationArgs* ptr = &self)
			{
				NativeMethods.AuthorizationArgs.SetScopes(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}

		public unsafe string? State()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationArgs");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool num;
			fixed (NativeMethods.AuthorizationArgs* ptr = &self)
			{
				num = NativeMethods.AuthorizationArgs.State(ptr, &discord_String);
			}
			if (!num)
			{
				return null;
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetState(string? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationArgs");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitNullableStringLocal(buf, &num, 1024, &discord_String, value);
			fixed (NativeMethods.AuthorizationArgs* ptr = &self)
			{
				NativeMethods.AuthorizationArgs.SetState(ptr, (value != null) ? (&discord_String) : null);
			}
			NativeMethods.__FreeLocal(&discord_String, owned);
		}

		public unsafe string? Nonce()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationArgs");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool num;
			fixed (NativeMethods.AuthorizationArgs* ptr = &self)
			{
				num = NativeMethods.AuthorizationArgs.Nonce(ptr, &discord_String);
			}
			if (!num)
			{
				return null;
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetNonce(string? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationArgs");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitNullableStringLocal(buf, &num, 1024, &discord_String, value);
			fixed (NativeMethods.AuthorizationArgs* ptr = &self)
			{
				NativeMethods.AuthorizationArgs.SetNonce(ptr, (value != null) ? (&discord_String) : null);
			}
			NativeMethods.__FreeLocal(&discord_String, owned);
		}

		public unsafe AuthorizationCodeChallenge? CodeChallenge()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationArgs");
			}
			NativeMethods.AuthorizationCodeChallenge authorizationCodeChallenge = default(NativeMethods.AuthorizationCodeChallenge);
			bool num;
			fixed (NativeMethods.AuthorizationArgs* ptr = &self)
			{
				num = NativeMethods.AuthorizationArgs.CodeChallenge(ptr, &authorizationCodeChallenge);
			}
			if (!num)
			{
				return null;
			}
			return new AuthorizationCodeChallenge(authorizationCodeChallenge, 0);
		}

		public unsafe void SetCodeChallenge(AuthorizationCodeChallenge? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AuthorizationArgs");
			}
			NativeMethods.AuthorizationCodeChallenge authorizationCodeChallenge = value?.self ?? default(NativeMethods.AuthorizationCodeChallenge);
			fixed (NativeMethods.AuthorizationArgs* ptr = &self)
			{
				NativeMethods.AuthorizationArgs.SetCodeChallenge(ptr, (value != null) ? (&authorizationCodeChallenge) : null);
			}
			if (value != null)
			{
				value.self = authorizationCodeChallenge;
			}
		}
	}
	public class DeviceAuthorizationArgs : IDisposable
	{
		internal NativeMethods.DeviceAuthorizationArgs self;

		private int disposed_;

		internal DeviceAuthorizationArgs(NativeMethods.DeviceAuthorizationArgs self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~DeviceAuthorizationArgs()
		{
			Dispose();
		}

		public unsafe DeviceAuthorizationArgs()
		{
			NativeMethods.__Init();
			fixed (NativeMethods.DeviceAuthorizationArgs* ptr = &self)
			{
				NativeMethods.DeviceAuthorizationArgs.Init(ptr);
			}
			NativeMethods.__OnPostConstruct(this);
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.DeviceAuthorizationArgs* ptr = &self)
				{
					NativeMethods.DeviceAuthorizationArgs.Drop(ptr);
				}
			}
		}

		public unsafe DeviceAuthorizationArgs(DeviceAuthorizationArgs other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("DeviceAuthorizationArgs");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.DeviceAuthorizationArgs* arg = &other.self)
			{
				fixed (NativeMethods.DeviceAuthorizationArgs* ptr = &self)
				{
					NativeMethods.DeviceAuthorizationArgs.Clone(ptr, arg);
				}
			}
		}

		internal unsafe DeviceAuthorizationArgs(NativeMethods.DeviceAuthorizationArgs* otherPtr)
		{
			fixed (NativeMethods.DeviceAuthorizationArgs* ptr = &self)
			{
				NativeMethods.DeviceAuthorizationArgs.Clone(ptr, otherPtr);
			}
		}

		public unsafe ulong ClientId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("DeviceAuthorizationArgs");
			}
			ulong result;
			fixed (NativeMethods.DeviceAuthorizationArgs* ptr = &self)
			{
				result = NativeMethods.DeviceAuthorizationArgs.ClientId(ptr);
			}
			return result;
		}

		public unsafe void SetClientId(ulong value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("DeviceAuthorizationArgs");
			}
			fixed (NativeMethods.DeviceAuthorizationArgs* ptr = &self)
			{
				NativeMethods.DeviceAuthorizationArgs.SetClientId(ptr, value);
			}
		}

		public unsafe string Scopes()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("DeviceAuthorizationArgs");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.DeviceAuthorizationArgs* ptr = &self)
			{
				NativeMethods.DeviceAuthorizationArgs.Scopes(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetScopes(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("DeviceAuthorizationArgs");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.DeviceAuthorizationArgs* ptr = &self)
			{
				NativeMethods.DeviceAuthorizationArgs.SetScopes(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}
	}
	public class VoiceStateHandle : IDisposable
	{
		internal NativeMethods.VoiceStateHandle self;

		private int disposed_;

		internal VoiceStateHandle(NativeMethods.VoiceStateHandle self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~VoiceStateHandle()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.VoiceStateHandle* ptr = &self)
				{
					NativeMethods.VoiceStateHandle.Drop(ptr);
				}
			}
		}

		public unsafe VoiceStateHandle(VoiceStateHandle other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("VoiceStateHandle");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.VoiceStateHandle* other2 = &other.self)
			{
				fixed (NativeMethods.VoiceStateHandle* ptr = &self)
				{
					NativeMethods.VoiceStateHandle.Clone(ptr, other2);
				}
			}
		}

		internal unsafe VoiceStateHandle(NativeMethods.VoiceStateHandle* otherPtr)
		{
			fixed (NativeMethods.VoiceStateHandle* ptr = &self)
			{
				NativeMethods.VoiceStateHandle.Clone(ptr, otherPtr);
			}
		}

		public unsafe bool SelfDeaf()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("VoiceStateHandle");
			}
			bool result;
			fixed (NativeMethods.VoiceStateHandle* ptr = &self)
			{
				result = NativeMethods.VoiceStateHandle.SelfDeaf(ptr);
			}
			return result;
		}

		public unsafe bool SelfMute()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("VoiceStateHandle");
			}
			bool result;
			fixed (NativeMethods.VoiceStateHandle* ptr = &self)
			{
				result = NativeMethods.VoiceStateHandle.SelfMute(ptr);
			}
			return result;
		}
	}
	public class VADThresholdSettings : IDisposable
	{
		internal NativeMethods.VADThresholdSettings self;

		private int disposed_;

		internal VADThresholdSettings(NativeMethods.VADThresholdSettings self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~VADThresholdSettings()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.VADThresholdSettings* ptr = &self)
				{
					NativeMethods.VADThresholdSettings.Drop(ptr);
				}
			}
		}

		public unsafe float VadThreshold()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("VADThresholdSettings");
			}
			float result;
			fixed (NativeMethods.VADThresholdSettings* ptr = &self)
			{
				result = NativeMethods.VADThresholdSettings.VadThreshold(ptr);
			}
			return result;
		}

		public unsafe void SetVadThreshold(float value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("VADThresholdSettings");
			}
			fixed (NativeMethods.VADThresholdSettings* ptr = &self)
			{
				NativeMethods.VADThresholdSettings.SetVadThreshold(ptr, value);
			}
		}

		public unsafe bool Automatic()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("VADThresholdSettings");
			}
			bool result;
			fixed (NativeMethods.VADThresholdSettings* ptr = &self)
			{
				result = NativeMethods.VADThresholdSettings.Automatic(ptr);
			}
			return result;
		}

		public unsafe void SetAutomatic(bool value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("VADThresholdSettings");
			}
			fixed (NativeMethods.VADThresholdSettings* ptr = &self)
			{
				NativeMethods.VADThresholdSettings.SetAutomatic(ptr, value);
			}
		}
	}
	public class Call : IDisposable
	{
		public enum Error
		{
			None,
			SignalingConnectionFailed,
			SignalingUnexpectedClose,
			VoiceConnectionFailed,
			JoinTimeout,
			Forbidden
		}

		public enum Status
		{
			Disconnected,
			Joining,
			Connecting,
			SignalingConnected,
			Connected,
			Reconnecting,
			Disconnecting
		}

		public delegate void OnVoiceStateChanged(ulong userId);

		public delegate void OnParticipantChanged(ulong userId, bool added);

		public delegate void OnSpeakingStatusChanged(ulong userId, bool isPlayingSound);

		public delegate void OnStatusChanged(Status status, Error error, int errorDetail);

		internal NativeMethods.Call self;

		private int disposed_;

		internal Call(NativeMethods.Call self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~Call()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.Call* ptr = &self)
				{
					NativeMethods.Call.Drop(ptr);
				}
			}
		}

		public unsafe Call(Call other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.Call* other2 = &other.self)
			{
				fixed (NativeMethods.Call* ptr = &self)
				{
					NativeMethods.Call.Clone(ptr, other2);
				}
			}
		}

		internal unsafe Call(NativeMethods.Call* otherPtr)
		{
			fixed (NativeMethods.Call* ptr = &self)
			{
				NativeMethods.Call.Clone(ptr, otherPtr);
			}
		}

		public unsafe static string ErrorToString(Error type)
		{
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			NativeMethods.Call.ErrorToString(type, &discord_String);
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe AudioModeType GetAudioMode()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			AudioModeType audioMode;
			fixed (NativeMethods.Call* ptr = &self)
			{
				audioMode = NativeMethods.Call.GetAudioMode(ptr);
			}
			return audioMode;
		}

		public unsafe ulong GetChannelId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			ulong channelId;
			fixed (NativeMethods.Call* ptr = &self)
			{
				channelId = NativeMethods.Call.GetChannelId(ptr);
			}
			return channelId;
		}

		public unsafe ulong GetGuildId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			ulong guildId;
			fixed (NativeMethods.Call* ptr = &self)
			{
				guildId = NativeMethods.Call.GetGuildId(ptr);
			}
			return guildId;
		}

		public unsafe bool GetLocalMute(ulong userId)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			bool localMute;
			fixed (NativeMethods.Call* ptr = &self)
			{
				localMute = NativeMethods.Call.GetLocalMute(ptr, userId);
			}
			return localMute;
		}

		public unsafe ulong[] GetParticipants()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			NativeMethods.Discord_UInt64Span discord_UInt64Span = default(NativeMethods.Discord_UInt64Span);
			fixed (NativeMethods.Call* ptr = &self)
			{
				NativeMethods.Call.GetParticipants(ptr, &discord_UInt64Span);
			}
			ulong[] result = new Span<ulong>(discord_UInt64Span.ptr, (int)(uint)discord_UInt64Span.size).ToArray();
			NativeMethods.Discord_Free(discord_UInt64Span.ptr);
			return result;
		}

		public unsafe float GetParticipantVolume(ulong userId)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			float participantVolume;
			fixed (NativeMethods.Call* ptr = &self)
			{
				participantVolume = NativeMethods.Call.GetParticipantVolume(ptr, userId);
			}
			return participantVolume;
		}

		public unsafe bool GetPTTActive()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			bool pTTActive;
			fixed (NativeMethods.Call* ptr = &self)
			{
				pTTActive = NativeMethods.Call.GetPTTActive(ptr);
			}
			return pTTActive;
		}

		public unsafe uint GetPTTReleaseDelay()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			uint pTTReleaseDelay;
			fixed (NativeMethods.Call* ptr = &self)
			{
				pTTReleaseDelay = NativeMethods.Call.GetPTTReleaseDelay(ptr);
			}
			return pTTReleaseDelay;
		}

		public unsafe bool GetSelfDeaf()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			bool selfDeaf;
			fixed (NativeMethods.Call* ptr = &self)
			{
				selfDeaf = NativeMethods.Call.GetSelfDeaf(ptr);
			}
			return selfDeaf;
		}

		public unsafe bool GetSelfMute()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			bool selfMute;
			fixed (NativeMethods.Call* ptr = &self)
			{
				selfMute = NativeMethods.Call.GetSelfMute(ptr);
			}
			return selfMute;
		}

		public unsafe Status GetStatus()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			Status status;
			fixed (NativeMethods.Call* ptr = &self)
			{
				status = NativeMethods.Call.GetStatus(ptr);
			}
			return status;
		}

		public unsafe VADThresholdSettings GetVADThreshold()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			NativeMethods.VADThresholdSettings vADThresholdSettings = default(NativeMethods.VADThresholdSettings);
			fixed (NativeMethods.Call* ptr = &self)
			{
				NativeMethods.Call.GetVADThreshold(ptr, &vADThresholdSettings);
			}
			return new VADThresholdSettings(vADThresholdSettings, 0);
		}

		public unsafe VoiceStateHandle? GetVoiceStateHandle(ulong userId)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			NativeMethods.VoiceStateHandle voiceStateHandle = default(NativeMethods.VoiceStateHandle);
			bool voiceStateHandle2;
			fixed (NativeMethods.Call* ptr = &self)
			{
				voiceStateHandle2 = NativeMethods.Call.GetVoiceStateHandle(ptr, userId, &voiceStateHandle);
			}
			if (!voiceStateHandle2)
			{
				return null;
			}
			return new VoiceStateHandle(voiceStateHandle, 0);
		}

		public unsafe void SetAudioMode(AudioModeType audioMode)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			fixed (NativeMethods.Call* ptr = &self)
			{
				NativeMethods.Call.SetAudioMode(ptr, audioMode);
			}
		}

		public unsafe void SetLocalMute(ulong userId, bool mute)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			fixed (NativeMethods.Call* ptr = &self)
			{
				NativeMethods.Call.SetLocalMute(ptr, userId, mute);
			}
		}

		public unsafe void SetOnVoiceStateChangedCallback(OnVoiceStateChanged cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			NativeMethods.Call.OnVoiceStateChanged cb2 = NativeMethods.Call.OnVoiceStateChanged_Handler;
			fixed (NativeMethods.Call* ptr = &self)
			{
				NativeMethods.Call.SetOnVoiceStateChangedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetParticipantChangedCallback(OnParticipantChanged cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			NativeMethods.Call.OnParticipantChanged cb2 = NativeMethods.Call.OnParticipantChanged_Handler;
			fixed (NativeMethods.Call* ptr = &self)
			{
				NativeMethods.Call.SetParticipantChangedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetParticipantVolume(ulong userId, float volume)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			fixed (NativeMethods.Call* ptr = &self)
			{
				NativeMethods.Call.SetParticipantVolume(ptr, userId, volume);
			}
		}

		public unsafe void SetPTTActive(bool active)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			fixed (NativeMethods.Call* ptr = &self)
			{
				NativeMethods.Call.SetPTTActive(ptr, active);
			}
		}

		public unsafe void SetPTTReleaseDelay(uint releaseDelayMs)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			fixed (NativeMethods.Call* ptr = &self)
			{
				NativeMethods.Call.SetPTTReleaseDelay(ptr, releaseDelayMs);
			}
		}

		public unsafe void SetSelfDeaf(bool deaf)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			fixed (NativeMethods.Call* ptr = &self)
			{
				NativeMethods.Call.SetSelfDeaf(ptr, deaf);
			}
		}

		public unsafe void SetSelfMute(bool mute)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			fixed (NativeMethods.Call* ptr = &self)
			{
				NativeMethods.Call.SetSelfMute(ptr, mute);
			}
		}

		public unsafe void SetSpeakingStatusChangedCallback(OnSpeakingStatusChanged cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			NativeMethods.Call.OnSpeakingStatusChanged cb2 = NativeMethods.Call.OnSpeakingStatusChanged_Handler;
			fixed (NativeMethods.Call* ptr = &self)
			{
				NativeMethods.Call.SetSpeakingStatusChangedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetStatusChangedCallback(OnStatusChanged cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			NativeMethods.Call.OnStatusChanged cb2 = NativeMethods.Call.OnStatusChanged_Handler;
			fixed (NativeMethods.Call* ptr = &self)
			{
				NativeMethods.Call.SetStatusChangedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetVADThreshold(bool automatic, float threshold)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Call");
			}
			fixed (NativeMethods.Call* ptr = &self)
			{
				NativeMethods.Call.SetVADThreshold(ptr, automatic, threshold);
			}
		}

		public unsafe static string StatusToString(Status type)
		{
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			NativeMethods.Call.StatusToString(type, &discord_String);
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}
	}
	public class ChannelHandle : IDisposable
	{
		internal NativeMethods.ChannelHandle self;

		private int disposed_;

		internal ChannelHandle(NativeMethods.ChannelHandle self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~ChannelHandle()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.ChannelHandle* ptr = &self)
				{
					NativeMethods.ChannelHandle.Drop(ptr);
				}
			}
		}

		public unsafe ChannelHandle(ChannelHandle other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ChannelHandle");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.ChannelHandle* other2 = &other.self)
			{
				fixed (NativeMethods.ChannelHandle* ptr = &self)
				{
					NativeMethods.ChannelHandle.Clone(ptr, other2);
				}
			}
		}

		internal unsafe ChannelHandle(NativeMethods.ChannelHandle* otherPtr)
		{
			fixed (NativeMethods.ChannelHandle* ptr = &self)
			{
				NativeMethods.ChannelHandle.Clone(ptr, otherPtr);
			}
		}

		public unsafe ulong Id()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ChannelHandle");
			}
			ulong result;
			fixed (NativeMethods.ChannelHandle* ptr = &self)
			{
				result = NativeMethods.ChannelHandle.Id(ptr);
			}
			return result;
		}

		public unsafe string Name()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ChannelHandle");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.ChannelHandle* ptr = &self)
			{
				NativeMethods.ChannelHandle.Name(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe ulong[] Recipients()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ChannelHandle");
			}
			NativeMethods.Discord_UInt64Span discord_UInt64Span = default(NativeMethods.Discord_UInt64Span);
			fixed (NativeMethods.ChannelHandle* ptr = &self)
			{
				NativeMethods.ChannelHandle.Recipients(ptr, &discord_UInt64Span);
			}
			ulong[] result = new Span<ulong>(discord_UInt64Span.ptr, (int)(uint)discord_UInt64Span.size).ToArray();
			NativeMethods.Discord_Free(discord_UInt64Span.ptr);
			return result;
		}

		public unsafe ChannelType Type()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("ChannelHandle");
			}
			ChannelType result;
			fixed (NativeMethods.ChannelHandle* ptr = &self)
			{
				result = NativeMethods.ChannelHandle.Type(ptr);
			}
			return result;
		}
	}
	public class GuildMinimal : IDisposable
	{
		internal NativeMethods.GuildMinimal self;

		private int disposed_;

		internal GuildMinimal(NativeMethods.GuildMinimal self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~GuildMinimal()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.GuildMinimal* ptr = &self)
				{
					NativeMethods.GuildMinimal.Drop(ptr);
				}
			}
		}

		public unsafe GuildMinimal(GuildMinimal other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildMinimal");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.GuildMinimal* arg = &other.self)
			{
				fixed (NativeMethods.GuildMinimal* ptr = &self)
				{
					NativeMethods.GuildMinimal.Clone(ptr, arg);
				}
			}
		}

		internal unsafe GuildMinimal(NativeMethods.GuildMinimal* otherPtr)
		{
			fixed (NativeMethods.GuildMinimal* ptr = &self)
			{
				NativeMethods.GuildMinimal.Clone(ptr, otherPtr);
			}
		}

		public unsafe ulong Id()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildMinimal");
			}
			ulong result;
			fixed (NativeMethods.GuildMinimal* ptr = &self)
			{
				result = NativeMethods.GuildMinimal.Id(ptr);
			}
			return result;
		}

		public unsafe void SetId(ulong value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildMinimal");
			}
			fixed (NativeMethods.GuildMinimal* ptr = &self)
			{
				NativeMethods.GuildMinimal.SetId(ptr, value);
			}
		}

		public unsafe string Name()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildMinimal");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.GuildMinimal* ptr = &self)
			{
				NativeMethods.GuildMinimal.Name(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetName(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildMinimal");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.GuildMinimal* ptr = &self)
			{
				NativeMethods.GuildMinimal.SetName(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}
	}
	public class GuildChannel : IDisposable
	{
		internal NativeMethods.GuildChannel self;

		private int disposed_;

		internal GuildChannel(NativeMethods.GuildChannel self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~GuildChannel()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.GuildChannel* ptr = &self)
				{
					NativeMethods.GuildChannel.Drop(ptr);
				}
			}
		}

		public unsafe GuildChannel(GuildChannel other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildChannel");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.GuildChannel* arg = &other.self)
			{
				fixed (NativeMethods.GuildChannel* ptr = &self)
				{
					NativeMethods.GuildChannel.Clone(ptr, arg);
				}
			}
		}

		internal unsafe GuildChannel(NativeMethods.GuildChannel* otherPtr)
		{
			fixed (NativeMethods.GuildChannel* ptr = &self)
			{
				NativeMethods.GuildChannel.Clone(ptr, otherPtr);
			}
		}

		public unsafe ulong Id()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildChannel");
			}
			ulong result;
			fixed (NativeMethods.GuildChannel* ptr = &self)
			{
				result = NativeMethods.GuildChannel.Id(ptr);
			}
			return result;
		}

		public unsafe void SetId(ulong value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildChannel");
			}
			fixed (NativeMethods.GuildChannel* ptr = &self)
			{
				NativeMethods.GuildChannel.SetId(ptr, value);
			}
		}

		public unsafe string Name()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildChannel");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.GuildChannel* ptr = &self)
			{
				NativeMethods.GuildChannel.Name(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetName(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildChannel");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.GuildChannel* ptr = &self)
			{
				NativeMethods.GuildChannel.SetName(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}

		public unsafe bool IsLinkable()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildChannel");
			}
			bool result;
			fixed (NativeMethods.GuildChannel* ptr = &self)
			{
				result = NativeMethods.GuildChannel.IsLinkable(ptr);
			}
			return result;
		}

		public unsafe void SetIsLinkable(bool value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildChannel");
			}
			fixed (NativeMethods.GuildChannel* ptr = &self)
			{
				NativeMethods.GuildChannel.SetIsLinkable(ptr, value);
			}
		}

		public unsafe bool IsViewableAndWriteableByAllMembers()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildChannel");
			}
			bool result;
			fixed (NativeMethods.GuildChannel* ptr = &self)
			{
				result = NativeMethods.GuildChannel.IsViewableAndWriteableByAllMembers(ptr);
			}
			return result;
		}

		public unsafe void SetIsViewableAndWriteableByAllMembers(bool value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildChannel");
			}
			fixed (NativeMethods.GuildChannel* ptr = &self)
			{
				NativeMethods.GuildChannel.SetIsViewableAndWriteableByAllMembers(ptr, value);
			}
		}

		public unsafe LinkedLobby? LinkedLobby()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildChannel");
			}
			NativeMethods.LinkedLobby linkedLobby = default(NativeMethods.LinkedLobby);
			bool num;
			fixed (NativeMethods.GuildChannel* ptr = &self)
			{
				num = NativeMethods.GuildChannel.LinkedLobby(ptr, &linkedLobby);
			}
			if (!num)
			{
				return null;
			}
			return new LinkedLobby(linkedLobby, 0);
		}

		public unsafe void SetLinkedLobby(LinkedLobby? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("GuildChannel");
			}
			NativeMethods.LinkedLobby linkedLobby = value?.self ?? default(NativeMethods.LinkedLobby);
			fixed (NativeMethods.GuildChannel* ptr = &self)
			{
				NativeMethods.GuildChannel.SetLinkedLobby(ptr, (value != null) ? (&linkedLobby) : null);
			}
			if (value != null)
			{
				value.self = linkedLobby;
			}
		}
	}
	public class LinkedLobby : IDisposable
	{
		internal NativeMethods.LinkedLobby self;

		private int disposed_;

		internal LinkedLobby(NativeMethods.LinkedLobby self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~LinkedLobby()
		{
			Dispose();
		}

		public unsafe LinkedLobby()
		{
			NativeMethods.__Init();
			fixed (NativeMethods.LinkedLobby* ptr = &self)
			{
				NativeMethods.LinkedLobby.Init(ptr);
			}
			NativeMethods.__OnPostConstruct(this);
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.LinkedLobby* ptr = &self)
				{
					NativeMethods.LinkedLobby.Drop(ptr);
				}
			}
		}

		public unsafe LinkedLobby(LinkedLobby other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LinkedLobby");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.LinkedLobby* arg = &other.self)
			{
				fixed (NativeMethods.LinkedLobby* ptr = &self)
				{
					NativeMethods.LinkedLobby.Clone(ptr, arg);
				}
			}
		}

		internal unsafe LinkedLobby(NativeMethods.LinkedLobby* otherPtr)
		{
			fixed (NativeMethods.LinkedLobby* ptr = &self)
			{
				NativeMethods.LinkedLobby.Clone(ptr, otherPtr);
			}
		}

		public unsafe ulong ApplicationId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LinkedLobby");
			}
			ulong result;
			fixed (NativeMethods.LinkedLobby* ptr = &self)
			{
				result = NativeMethods.LinkedLobby.ApplicationId(ptr);
			}
			return result;
		}

		public unsafe void SetApplicationId(ulong value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LinkedLobby");
			}
			fixed (NativeMethods.LinkedLobby* ptr = &self)
			{
				NativeMethods.LinkedLobby.SetApplicationId(ptr, value);
			}
		}

		public unsafe ulong LobbyId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LinkedLobby");
			}
			ulong result;
			fixed (NativeMethods.LinkedLobby* ptr = &self)
			{
				result = NativeMethods.LinkedLobby.LobbyId(ptr);
			}
			return result;
		}

		public unsafe void SetLobbyId(ulong value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LinkedLobby");
			}
			fixed (NativeMethods.LinkedLobby* ptr = &self)
			{
				NativeMethods.LinkedLobby.SetLobbyId(ptr, value);
			}
		}
	}
	public class LinkedChannel : IDisposable
	{
		internal NativeMethods.LinkedChannel self;

		private int disposed_;

		internal LinkedChannel(NativeMethods.LinkedChannel self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~LinkedChannel()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.LinkedChannel* ptr = &self)
				{
					NativeMethods.LinkedChannel.Drop(ptr);
				}
			}
		}

		public unsafe LinkedChannel(LinkedChannel other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LinkedChannel");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.LinkedChannel* arg = &other.self)
			{
				fixed (NativeMethods.LinkedChannel* ptr = &self)
				{
					NativeMethods.LinkedChannel.Clone(ptr, arg);
				}
			}
		}

		internal unsafe LinkedChannel(NativeMethods.LinkedChannel* otherPtr)
		{
			fixed (NativeMethods.LinkedChannel* ptr = &self)
			{
				NativeMethods.LinkedChannel.Clone(ptr, otherPtr);
			}
		}

		public unsafe ulong Id()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LinkedChannel");
			}
			ulong result;
			fixed (NativeMethods.LinkedChannel* ptr = &self)
			{
				result = NativeMethods.LinkedChannel.Id(ptr);
			}
			return result;
		}

		public unsafe void SetId(ulong value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LinkedChannel");
			}
			fixed (NativeMethods.LinkedChannel* ptr = &self)
			{
				NativeMethods.LinkedChannel.SetId(ptr, value);
			}
		}

		public unsafe string Name()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LinkedChannel");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.LinkedChannel* ptr = &self)
			{
				NativeMethods.LinkedChannel.Name(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetName(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LinkedChannel");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.LinkedChannel* ptr = &self)
			{
				NativeMethods.LinkedChannel.SetName(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}

		public unsafe ulong GuildId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LinkedChannel");
			}
			ulong result;
			fixed (NativeMethods.LinkedChannel* ptr = &self)
			{
				result = NativeMethods.LinkedChannel.GuildId(ptr);
			}
			return result;
		}

		public unsafe void SetGuildId(ulong value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LinkedChannel");
			}
			fixed (NativeMethods.LinkedChannel* ptr = &self)
			{
				NativeMethods.LinkedChannel.SetGuildId(ptr, value);
			}
		}
	}
	public class RelationshipHandle : IDisposable
	{
		internal NativeMethods.RelationshipHandle self;

		private int disposed_;

		internal RelationshipHandle(NativeMethods.RelationshipHandle self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~RelationshipHandle()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.RelationshipHandle* ptr = &self)
				{
					NativeMethods.RelationshipHandle.Drop(ptr);
				}
			}
		}

		public unsafe RelationshipHandle(RelationshipHandle other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("RelationshipHandle");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.RelationshipHandle* other2 = &other.self)
			{
				fixed (NativeMethods.RelationshipHandle* ptr = &self)
				{
					NativeMethods.RelationshipHandle.Clone(ptr, other2);
				}
			}
		}

		internal unsafe RelationshipHandle(NativeMethods.RelationshipHandle* otherPtr)
		{
			fixed (NativeMethods.RelationshipHandle* ptr = &self)
			{
				NativeMethods.RelationshipHandle.Clone(ptr, otherPtr);
			}
		}

		public unsafe RelationshipType DiscordRelationshipType()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("RelationshipHandle");
			}
			RelationshipType result;
			fixed (NativeMethods.RelationshipHandle* ptr = &self)
			{
				result = NativeMethods.RelationshipHandle.DiscordRelationshipType(ptr);
			}
			return result;
		}

		public unsafe RelationshipType GameRelationshipType()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("RelationshipHandle");
			}
			RelationshipType result;
			fixed (NativeMethods.RelationshipHandle* ptr = &self)
			{
				result = NativeMethods.RelationshipHandle.GameRelationshipType(ptr);
			}
			return result;
		}

		public unsafe ulong Id()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("RelationshipHandle");
			}
			ulong result;
			fixed (NativeMethods.RelationshipHandle* ptr = &self)
			{
				result = NativeMethods.RelationshipHandle.Id(ptr);
			}
			return result;
		}

		public unsafe UserHandle? User()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("RelationshipHandle");
			}
			NativeMethods.UserHandle userHandle = default(NativeMethods.UserHandle);
			bool num;
			fixed (NativeMethods.RelationshipHandle* ptr = &self)
			{
				num = NativeMethods.RelationshipHandle.User(ptr, &userHandle);
			}
			if (!num)
			{
				return null;
			}
			return new UserHandle(userHandle, 0);
		}
	}
	public class UserHandle : IDisposable
	{
		public enum AvatarType
		{
			Gif,
			Webp,
			Png,
			Jpeg
		}

		internal NativeMethods.UserHandle self;

		private int disposed_;

		internal UserHandle(NativeMethods.UserHandle self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~UserHandle()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.UserHandle* ptr = &self)
				{
					NativeMethods.UserHandle.Drop(ptr);
				}
			}
		}

		public unsafe UserHandle(UserHandle other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("UserHandle");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.UserHandle* arg = &other.self)
			{
				fixed (NativeMethods.UserHandle* ptr = &self)
				{
					NativeMethods.UserHandle.Clone(ptr, arg);
				}
			}
		}

		internal unsafe UserHandle(NativeMethods.UserHandle* otherPtr)
		{
			fixed (NativeMethods.UserHandle* ptr = &self)
			{
				NativeMethods.UserHandle.Clone(ptr, otherPtr);
			}
		}

		public unsafe string? Avatar()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("UserHandle");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool num;
			fixed (NativeMethods.UserHandle* ptr = &self)
			{
				num = NativeMethods.UserHandle.Avatar(ptr, &discord_String);
			}
			if (!num)
			{
				return null;
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe static string AvatarTypeToString(AvatarType type)
		{
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			NativeMethods.UserHandle.AvatarTypeToString(type, &discord_String);
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe string AvatarUrl(AvatarType animatedType, AvatarType staticType)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("UserHandle");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.UserHandle* ptr = &self)
			{
				NativeMethods.UserHandle.AvatarUrl(ptr, animatedType, staticType, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe string DisplayName()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("UserHandle");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.UserHandle* ptr = &self)
			{
				NativeMethods.UserHandle.DisplayName(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe Activity? GameActivity()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("UserHandle");
			}
			NativeMethods.Activity activity = default(NativeMethods.Activity);
			bool num;
			fixed (NativeMethods.UserHandle* ptr = &self)
			{
				num = NativeMethods.UserHandle.GameActivity(ptr, &activity);
			}
			if (!num)
			{
				return null;
			}
			return new Activity(activity, 0);
		}

		public unsafe string? GlobalName()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("UserHandle");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool num;
			fixed (NativeMethods.UserHandle* ptr = &self)
			{
				num = NativeMethods.UserHandle.GlobalName(ptr, &discord_String);
			}
			if (!num)
			{
				return null;
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe ulong Id()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("UserHandle");
			}
			ulong result;
			fixed (NativeMethods.UserHandle* ptr = &self)
			{
				result = NativeMethods.UserHandle.Id(ptr);
			}
			return result;
		}

		public unsafe bool IsProvisional()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("UserHandle");
			}
			bool result;
			fixed (NativeMethods.UserHandle* ptr = &self)
			{
				result = NativeMethods.UserHandle.IsProvisional(ptr);
			}
			return result;
		}

		public unsafe RelationshipHandle Relationship()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("UserHandle");
			}
			NativeMethods.RelationshipHandle relationshipHandle = default(NativeMethods.RelationshipHandle);
			fixed (NativeMethods.UserHandle* ptr = &self)
			{
				NativeMethods.UserHandle.Relationship(ptr, &relationshipHandle);
			}
			return new RelationshipHandle(relationshipHandle, 0);
		}

		public unsafe StatusType Status()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("UserHandle");
			}
			StatusType result;
			fixed (NativeMethods.UserHandle* ptr = &self)
			{
				result = NativeMethods.UserHandle.Status(ptr);
			}
			return result;
		}

		public unsafe string Username()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("UserHandle");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.UserHandle* ptr = &self)
			{
				NativeMethods.UserHandle.Username(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}
	}
	public class LobbyMemberHandle : IDisposable
	{
		internal NativeMethods.LobbyMemberHandle self;

		private int disposed_;

		internal LobbyMemberHandle(NativeMethods.LobbyMemberHandle self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~LobbyMemberHandle()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.LobbyMemberHandle* ptr = &self)
				{
					NativeMethods.LobbyMemberHandle.Drop(ptr);
				}
			}
		}

		public unsafe LobbyMemberHandle(LobbyMemberHandle other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LobbyMemberHandle");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.LobbyMemberHandle* other2 = &other.self)
			{
				fixed (NativeMethods.LobbyMemberHandle* ptr = &self)
				{
					NativeMethods.LobbyMemberHandle.Clone(ptr, other2);
				}
			}
		}

		internal unsafe LobbyMemberHandle(NativeMethods.LobbyMemberHandle* otherPtr)
		{
			fixed (NativeMethods.LobbyMemberHandle* ptr = &self)
			{
				NativeMethods.LobbyMemberHandle.Clone(ptr, otherPtr);
			}
		}

		public unsafe bool CanLinkLobby()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LobbyMemberHandle");
			}
			bool result;
			fixed (NativeMethods.LobbyMemberHandle* ptr = &self)
			{
				result = NativeMethods.LobbyMemberHandle.CanLinkLobby(ptr);
			}
			return result;
		}

		public unsafe bool Connected()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LobbyMemberHandle");
			}
			bool result;
			fixed (NativeMethods.LobbyMemberHandle* ptr = &self)
			{
				result = NativeMethods.LobbyMemberHandle.Connected(ptr);
			}
			return result;
		}

		public unsafe ulong Id()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LobbyMemberHandle");
			}
			ulong result;
			fixed (NativeMethods.LobbyMemberHandle* ptr = &self)
			{
				result = NativeMethods.LobbyMemberHandle.Id(ptr);
			}
			return result;
		}

		public unsafe Dictionary<string, string> Metadata()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LobbyMemberHandle");
			}
			NativeMethods.Discord_Properties props = default(NativeMethods.Discord_Properties);
			fixed (NativeMethods.LobbyMemberHandle* ptr = &self)
			{
				NativeMethods.LobbyMemberHandle.Metadata(ptr, &props);
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>((int)props.size);
			for (int i = 0; i < (int)props.size; i++)
			{
				string key = Marshal.PtrToStringUTF8((IntPtr)props.keys[i].ptr, (int)(uint)props.keys[i].size);
				string value = Marshal.PtrToStringUTF8((IntPtr)props.values[i].ptr, (int)(uint)props.values[i].size);
				dictionary[key] = value;
			}
			NativeMethods.Discord_FreeProperties(props);
			return dictionary;
		}

		public unsafe UserHandle? User()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LobbyMemberHandle");
			}
			NativeMethods.UserHandle userHandle = default(NativeMethods.UserHandle);
			bool num;
			fixed (NativeMethods.LobbyMemberHandle* ptr = &self)
			{
				num = NativeMethods.LobbyMemberHandle.User(ptr, &userHandle);
			}
			if (!num)
			{
				return null;
			}
			return new UserHandle(userHandle, 0);
		}
	}
	public class LobbyHandle : IDisposable
	{
		internal NativeMethods.LobbyHandle self;

		private int disposed_;

		internal LobbyHandle(NativeMethods.LobbyHandle self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~LobbyHandle()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.LobbyHandle* ptr = &self)
				{
					NativeMethods.LobbyHandle.Drop(ptr);
				}
			}
		}

		public unsafe LobbyHandle(LobbyHandle other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LobbyHandle");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.LobbyHandle* other2 = &other.self)
			{
				fixed (NativeMethods.LobbyHandle* ptr = &self)
				{
					NativeMethods.LobbyHandle.Clone(ptr, other2);
				}
			}
		}

		internal unsafe LobbyHandle(NativeMethods.LobbyHandle* otherPtr)
		{
			fixed (NativeMethods.LobbyHandle* ptr = &self)
			{
				NativeMethods.LobbyHandle.Clone(ptr, otherPtr);
			}
		}

		public unsafe CallInfoHandle? GetCallInfoHandle()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LobbyHandle");
			}
			NativeMethods.CallInfoHandle callInfoHandle = default(NativeMethods.CallInfoHandle);
			bool callInfoHandle2;
			fixed (NativeMethods.LobbyHandle* ptr = &self)
			{
				callInfoHandle2 = NativeMethods.LobbyHandle.GetCallInfoHandle(ptr, &callInfoHandle);
			}
			if (!callInfoHandle2)
			{
				return null;
			}
			return new CallInfoHandle(callInfoHandle, 0);
		}

		public unsafe LobbyMemberHandle? GetLobbyMemberHandle(ulong memberId)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LobbyHandle");
			}
			NativeMethods.LobbyMemberHandle lobbyMemberHandle = default(NativeMethods.LobbyMemberHandle);
			bool lobbyMemberHandle2;
			fixed (NativeMethods.LobbyHandle* ptr = &self)
			{
				lobbyMemberHandle2 = NativeMethods.LobbyHandle.GetLobbyMemberHandle(ptr, memberId, &lobbyMemberHandle);
			}
			if (!lobbyMemberHandle2)
			{
				return null;
			}
			return new LobbyMemberHandle(lobbyMemberHandle, 0);
		}

		public unsafe ulong Id()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LobbyHandle");
			}
			ulong result;
			fixed (NativeMethods.LobbyHandle* ptr = &self)
			{
				result = NativeMethods.LobbyHandle.Id(ptr);
			}
			return result;
		}

		public unsafe LinkedChannel? LinkedChannel()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LobbyHandle");
			}
			NativeMethods.LinkedChannel linkedChannel = default(NativeMethods.LinkedChannel);
			bool num;
			fixed (NativeMethods.LobbyHandle* ptr = &self)
			{
				num = NativeMethods.LobbyHandle.LinkedChannel(ptr, &linkedChannel);
			}
			if (!num)
			{
				return null;
			}
			return new LinkedChannel(linkedChannel, 0);
		}

		public unsafe ulong[] LobbyMemberIds()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LobbyHandle");
			}
			NativeMethods.Discord_UInt64Span discord_UInt64Span = default(NativeMethods.Discord_UInt64Span);
			fixed (NativeMethods.LobbyHandle* ptr = &self)
			{
				NativeMethods.LobbyHandle.LobbyMemberIds(ptr, &discord_UInt64Span);
			}
			ulong[] result = new Span<ulong>(discord_UInt64Span.ptr, (int)(uint)discord_UInt64Span.size).ToArray();
			NativeMethods.Discord_Free(discord_UInt64Span.ptr);
			return result;
		}

		public unsafe LobbyMemberHandle[] LobbyMembers()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LobbyHandle");
			}
			NativeMethods.Discord_LobbyMemberHandleSpan discord_LobbyMemberHandleSpan = default(NativeMethods.Discord_LobbyMemberHandleSpan);
			fixed (NativeMethods.LobbyHandle* ptr = &self)
			{
				NativeMethods.LobbyHandle.LobbyMembers(ptr, &discord_LobbyMemberHandleSpan);
			}
			LobbyMemberHandle[] array = new LobbyMemberHandle[(uint)discord_LobbyMemberHandleSpan.size];
			for (int i = 0; i < (int)(uint)discord_LobbyMemberHandleSpan.size; i++)
			{
				array[i] = new LobbyMemberHandle(discord_LobbyMemberHandleSpan.ptr[i], 0);
			}
			NativeMethods.Discord_Free(discord_LobbyMemberHandleSpan.ptr);
			return array;
		}

		public unsafe Dictionary<string, string> Metadata()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("LobbyHandle");
			}
			NativeMethods.Discord_Properties props = default(NativeMethods.Discord_Properties);
			fixed (NativeMethods.LobbyHandle* ptr = &self)
			{
				NativeMethods.LobbyHandle.Metadata(ptr, &props);
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>((int)props.size);
			for (int i = 0; i < (int)props.size; i++)
			{
				string key = Marshal.PtrToStringUTF8((IntPtr)props.keys[i].ptr, (int)(uint)props.keys[i].size);
				string value = Marshal.PtrToStringUTF8((IntPtr)props.values[i].ptr, (int)(uint)props.values[i].size);
				dictionary[key] = value;
			}
			NativeMethods.Discord_FreeProperties(props);
			return dictionary;
		}
	}
	public class AdditionalContent : IDisposable
	{
		internal NativeMethods.AdditionalContent self;

		private int disposed_;

		internal AdditionalContent(NativeMethods.AdditionalContent self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~AdditionalContent()
		{
			Dispose();
		}

		public unsafe AdditionalContent()
		{
			NativeMethods.__Init();
			fixed (NativeMethods.AdditionalContent* ptr = &self)
			{
				NativeMethods.AdditionalContent.Init(ptr);
			}
			NativeMethods.__OnPostConstruct(this);
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.AdditionalContent* ptr = &self)
				{
					NativeMethods.AdditionalContent.Drop(ptr);
				}
			}
		}

		public unsafe AdditionalContent(AdditionalContent other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AdditionalContent");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.AdditionalContent* arg = &other.self)
			{
				fixed (NativeMethods.AdditionalContent* ptr = &self)
				{
					NativeMethods.AdditionalContent.Clone(ptr, arg);
				}
			}
		}

		internal unsafe AdditionalContent(NativeMethods.AdditionalContent* otherPtr)
		{
			fixed (NativeMethods.AdditionalContent* ptr = &self)
			{
				NativeMethods.AdditionalContent.Clone(ptr, otherPtr);
			}
		}

		public unsafe bool Equals(AdditionalContent rhs)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AdditionalContent");
			}
			bool result;
			fixed (NativeMethods.AdditionalContent* rhs2 = &rhs.self)
			{
				fixed (NativeMethods.AdditionalContent* ptr = &self)
				{
					result = NativeMethods.AdditionalContent.Equals(ptr, rhs2);
				}
			}
			return result;
		}

		public unsafe static string TypeToString(AdditionalContentType type)
		{
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			NativeMethods.AdditionalContent.TypeToString(type, &discord_String);
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe AdditionalContentType Type()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AdditionalContent");
			}
			AdditionalContentType result;
			fixed (NativeMethods.AdditionalContent* ptr = &self)
			{
				result = NativeMethods.AdditionalContent.Type(ptr);
			}
			return result;
		}

		public unsafe void SetType(AdditionalContentType value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AdditionalContent");
			}
			fixed (NativeMethods.AdditionalContent* ptr = &self)
			{
				NativeMethods.AdditionalContent.SetType(ptr, value);
			}
		}

		public unsafe string? Title()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AdditionalContent");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool num;
			fixed (NativeMethods.AdditionalContent* ptr = &self)
			{
				num = NativeMethods.AdditionalContent.Title(ptr, &discord_String);
			}
			if (!num)
			{
				return null;
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetTitle(string? value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AdditionalContent");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitNullableStringLocal(buf, &num, 1024, &discord_String, value);
			fixed (NativeMethods.AdditionalContent* ptr = &self)
			{
				NativeMethods.AdditionalContent.SetTitle(ptr, (value != null) ? (&discord_String) : null);
			}
			NativeMethods.__FreeLocal(&discord_String, owned);
		}

		public unsafe byte Count()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AdditionalContent");
			}
			byte result;
			fixed (NativeMethods.AdditionalContent* ptr = &self)
			{
				result = NativeMethods.AdditionalContent.Count(ptr);
			}
			return result;
		}

		public unsafe void SetCount(byte value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AdditionalContent");
			}
			fixed (NativeMethods.AdditionalContent* ptr = &self)
			{
				NativeMethods.AdditionalContent.SetCount(ptr, value);
			}
		}
	}
	public class MessageHandle : IDisposable
	{
		internal NativeMethods.MessageHandle self;

		private int disposed_;

		internal MessageHandle(NativeMethods.MessageHandle self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~MessageHandle()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.MessageHandle* ptr = &self)
				{
					NativeMethods.MessageHandle.Drop(ptr);
				}
			}
		}

		public unsafe MessageHandle(MessageHandle other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.MessageHandle* other2 = &other.self)
			{
				fixed (NativeMethods.MessageHandle* ptr = &self)
				{
					NativeMethods.MessageHandle.Clone(ptr, other2);
				}
			}
		}

		internal unsafe MessageHandle(NativeMethods.MessageHandle* otherPtr)
		{
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				NativeMethods.MessageHandle.Clone(ptr, otherPtr);
			}
		}

		public unsafe AdditionalContent? AdditionalContent()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			NativeMethods.AdditionalContent additionalContent = default(NativeMethods.AdditionalContent);
			bool num;
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				num = NativeMethods.MessageHandle.AdditionalContent(ptr, &additionalContent);
			}
			if (!num)
			{
				return null;
			}
			return new AdditionalContent(additionalContent, 0);
		}

		public unsafe UserHandle? Author()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			NativeMethods.UserHandle userHandle = default(NativeMethods.UserHandle);
			bool num;
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				num = NativeMethods.MessageHandle.Author(ptr, &userHandle);
			}
			if (!num)
			{
				return null;
			}
			return new UserHandle(userHandle, 0);
		}

		public unsafe ulong AuthorId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			ulong result;
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				result = NativeMethods.MessageHandle.AuthorId(ptr);
			}
			return result;
		}

		public unsafe ChannelHandle? Channel()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			NativeMethods.ChannelHandle channelHandle = default(NativeMethods.ChannelHandle);
			bool num;
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				num = NativeMethods.MessageHandle.Channel(ptr, &channelHandle);
			}
			if (!num)
			{
				return null;
			}
			return new ChannelHandle(channelHandle, 0);
		}

		public unsafe ulong ChannelId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			ulong result;
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				result = NativeMethods.MessageHandle.ChannelId(ptr);
			}
			return result;
		}

		public unsafe string Content()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				NativeMethods.MessageHandle.Content(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe DisclosureTypes? DisclosureType()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			bool num;
			DisclosureTypes value = default(DisclosureTypes);
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				num = NativeMethods.MessageHandle.DisclosureType(ptr, &value);
			}
			if (!num)
			{
				return null;
			}
			return value;
		}

		public unsafe ulong EditedTimestamp()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			ulong result;
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				result = NativeMethods.MessageHandle.EditedTimestamp(ptr);
			}
			return result;
		}

		public unsafe ulong Id()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			ulong result;
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				result = NativeMethods.MessageHandle.Id(ptr);
			}
			return result;
		}

		public unsafe LobbyHandle? Lobby()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			NativeMethods.LobbyHandle lobbyHandle = default(NativeMethods.LobbyHandle);
			bool num;
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				num = NativeMethods.MessageHandle.Lobby(ptr, &lobbyHandle);
			}
			if (!num)
			{
				return null;
			}
			return new LobbyHandle(lobbyHandle, 0);
		}

		public unsafe Dictionary<string, string> Metadata()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			NativeMethods.Discord_Properties props = default(NativeMethods.Discord_Properties);
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				NativeMethods.MessageHandle.Metadata(ptr, &props);
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>((int)props.size);
			for (int i = 0; i < (int)props.size; i++)
			{
				string key = Marshal.PtrToStringUTF8((IntPtr)props.keys[i].ptr, (int)(uint)props.keys[i].size);
				string value = Marshal.PtrToStringUTF8((IntPtr)props.values[i].ptr, (int)(uint)props.values[i].size);
				dictionary[key] = value;
			}
			NativeMethods.Discord_FreeProperties(props);
			return dictionary;
		}

		public unsafe string RawContent()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				NativeMethods.MessageHandle.RawContent(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe UserHandle? Recipient()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			NativeMethods.UserHandle userHandle = default(NativeMethods.UserHandle);
			bool num;
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				num = NativeMethods.MessageHandle.Recipient(ptr, &userHandle);
			}
			if (!num)
			{
				return null;
			}
			return new UserHandle(userHandle, 0);
		}

		public unsafe ulong RecipientId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			ulong result;
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				result = NativeMethods.MessageHandle.RecipientId(ptr);
			}
			return result;
		}

		public unsafe bool SentFromGame()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			bool result;
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				result = NativeMethods.MessageHandle.SentFromGame(ptr);
			}
			return result;
		}

		public unsafe ulong SentTimestamp()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("MessageHandle");
			}
			ulong result;
			fixed (NativeMethods.MessageHandle* ptr = &self)
			{
				result = NativeMethods.MessageHandle.SentTimestamp(ptr);
			}
			return result;
		}
	}
	public class AudioDevice : IDisposable
	{
		internal NativeMethods.AudioDevice self;

		private int disposed_;

		internal AudioDevice(NativeMethods.AudioDevice self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~AudioDevice()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.AudioDevice* ptr = &self)
				{
					NativeMethods.AudioDevice.Drop(ptr);
				}
			}
		}

		public unsafe AudioDevice(AudioDevice other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AudioDevice");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.AudioDevice* arg = &other.self)
			{
				fixed (NativeMethods.AudioDevice* ptr = &self)
				{
					NativeMethods.AudioDevice.Clone(ptr, arg);
				}
			}
		}

		internal unsafe AudioDevice(NativeMethods.AudioDevice* otherPtr)
		{
			fixed (NativeMethods.AudioDevice* ptr = &self)
			{
				NativeMethods.AudioDevice.Clone(ptr, otherPtr);
			}
		}

		public unsafe bool Equals(AudioDevice rhs)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AudioDevice");
			}
			bool result;
			fixed (NativeMethods.AudioDevice* rhs2 = &rhs.self)
			{
				fixed (NativeMethods.AudioDevice* ptr = &self)
				{
					result = NativeMethods.AudioDevice.Equals(ptr, rhs2);
				}
			}
			return result;
		}

		public unsafe string Id()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AudioDevice");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.AudioDevice* ptr = &self)
			{
				NativeMethods.AudioDevice.Id(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetId(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AudioDevice");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.AudioDevice* ptr = &self)
			{
				NativeMethods.AudioDevice.SetId(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}

		public unsafe string Name()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AudioDevice");
			}
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			fixed (NativeMethods.AudioDevice* ptr = &self)
			{
				NativeMethods.AudioDevice.Name(ptr, &discord_String);
			}
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void SetName(string value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AudioDevice");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String value2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &value2, value);
			fixed (NativeMethods.AudioDevice* ptr = &self)
			{
				NativeMethods.AudioDevice.SetName(ptr, value2);
			}
			NativeMethods.__FreeLocal(&value2, owned);
		}

		public unsafe bool IsDefault()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AudioDevice");
			}
			bool result;
			fixed (NativeMethods.AudioDevice* ptr = &self)
			{
				result = NativeMethods.AudioDevice.IsDefault(ptr);
			}
			return result;
		}

		public unsafe void SetIsDefault(bool value)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("AudioDevice");
			}
			fixed (NativeMethods.AudioDevice* ptr = &self)
			{
				NativeMethods.AudioDevice.SetIsDefault(ptr, value);
			}
		}
	}
	public class Client : IDisposable
	{
		public enum Error
		{
			None,
			ConnectionFailed,
			UnexpectedClose,
			ConnectionCanceled
		}

		public enum Status
		{
			Disconnected,
			Connecting,
			Connected,
			Ready,
			Reconnecting,
			Disconnecting,
			HttpWait
		}

		public enum Thread
		{
			Client,
			Voice,
			Network
		}

		public delegate void EndCallCallback();

		public delegate void EndCallsCallback();

		public delegate void GetCurrentInputDeviceCallback(AudioDevice device);

		public delegate void GetCurrentOutputDeviceCallback(AudioDevice device);

		public delegate void GetInputDevicesCallback(AudioDevice[] devices);

		public delegate void GetOutputDevicesCallback(AudioDevice[] devices);

		public delegate void DeviceChangeCallback(AudioDevice[] inputDevices, AudioDevice[] outputDevices);

		public delegate void SetInputDeviceCallback(ClientResult result);

		public delegate void NoAudioInputCallback(bool inputDetected);

		public delegate void SetOutputDeviceCallback(ClientResult result);

		public delegate void VoiceParticipantChangedCallback(ulong lobbyId, ulong memberId, bool added);

		public delegate void UserAudioReceivedCallback(ulong userId, IntPtr data, ulong samplesPerChannel, int sampleRate, ulong channels, ref bool outShouldMute);

		public delegate void UserAudioCapturedCallback(IntPtr data, ulong samplesPerChannel, int sampleRate, ulong channels);

		public delegate void AuthorizationCallback(ClientResult result, string code, string redirectUri);

		public delegate void FetchCurrentUserCallback(ClientResult result, ulong id, string name);

		public delegate void TokenExchangeCallback(ClientResult result, string accessToken, string refreshToken, AuthorizationTokenType tokenType, int expiresIn, string scopes);

		public delegate void AuthorizeDeviceScreenClosedCallback();

		public delegate void TokenExpirationCallback();

		public delegate void UpdateProvisionalAccountDisplayNameCallback(ClientResult result);

		public delegate void UpdateTokenCallback(ClientResult result);

		public delegate void DeleteUserMessageCallback(ClientResult result);

		public delegate void EditUserMessageCallback(ClientResult result);

		public delegate void ProvisionalUserMergeRequiredCallback();

		public delegate void OpenMessageInDiscordCallback(ClientResult result);

		public delegate void SendUserMessageCallback(ClientResult result, ulong messageId);

		public delegate void MessageCreatedCallback(ulong messageId);

		public delegate void MessageDeletedCallback(ulong messageId, ulong channelId);

		public delegate void MessageUpdatedCallback(ulong messageId);

		public delegate void LogCallback(string message, LoggingSeverity severity);

		public delegate void OpenConnectedGamesSettingsInDiscordCallback(ClientResult result);

		public delegate void OnStatusChanged(Status status, Error error, int errorDetail);

		public delegate void CreateOrJoinLobbyCallback(ClientResult result, ulong lobbyId);

		public delegate void GetGuildChannelsCallback(ClientResult result, GuildChannel[] guildChannels);

		public delegate void GetUserGuildsCallback(ClientResult result, GuildMinimal[] guilds);

		public delegate void LeaveLobbyCallback(ClientResult result);

		public delegate void LinkOrUnlinkChannelCallback(ClientResult result);

		public delegate void LobbyCreatedCallback(ulong lobbyId);

		public delegate void LobbyDeletedCallback(ulong lobbyId);

		public delegate void LobbyMemberAddedCallback(ulong lobbyId, ulong memberId);

		public delegate void LobbyMemberRemovedCallback(ulong lobbyId, ulong memberId);

		public delegate void LobbyMemberUpdatedCallback(ulong lobbyId, ulong memberId);

		public delegate void LobbyUpdatedCallback(ulong lobbyId);

		public delegate void AcceptActivityInviteCallback(ClientResult result, string joinSecret);

		public delegate void SendActivityInviteCallback(ClientResult result);

		public delegate void ActivityInviteCallback(ActivityInvite invite);

		public delegate void ActivityJoinCallback(string joinSecret);

		public delegate void UpdateStatusCallback(ClientResult result);

		public delegate void UpdateRichPresenceCallback(ClientResult result);

		public delegate void UpdateRelationshipCallback(ClientResult result);

		public delegate void SendFriendRequestCallback(ClientResult result);

		public delegate void RelationshipCreatedCallback(ulong userId, bool isDiscordRelationshipUpdate);

		public delegate void RelationshipDeletedCallback(ulong userId, bool isDiscordRelationshipUpdate);

		public delegate void GetDiscordClientConnectedUserCallback(ClientResult result, UserHandle? user);

		public delegate void UserUpdatedCallback(ulong userId);

		internal NativeMethods.Client self;

		private int disposed_;

		internal Client(NativeMethods.Client self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~Client()
		{
			Dispose();
		}

		public unsafe Client()
		{
			NativeMethods.__Init();
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.Init(ptr);
			}
			NativeMethods.__OnPostConstruct(this);
		}

		public unsafe Client(string apiBase, string webBase)
		{
			NativeMethods.__Init();
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String apiBase2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &apiBase2, apiBase);
			NativeMethods.Discord_String webBase2 = default(NativeMethods.Discord_String);
			bool owned2 = NativeMethods.__InitStringLocal(buf, &num, 1024, &webBase2, webBase);
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.InitWithBases(ptr, apiBase2, webBase2);
			}
			NativeMethods.__FreeLocal(&webBase2, owned2);
			NativeMethods.__FreeLocal(&apiBase2, owned);
			NativeMethods.__OnPostConstruct(this);
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.Client* ptr = &self)
				{
					NativeMethods.Client.Drop(ptr);
				}
			}
		}

		public unsafe static string ErrorToString(Error type)
		{
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			NativeMethods.Client.ErrorToString(type, &discord_String);
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe ulong GetApplicationId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			ulong applicationId;
			fixed (NativeMethods.Client* ptr = &self)
			{
				applicationId = NativeMethods.Client.GetApplicationId(ptr);
			}
			return applicationId;
		}

		public unsafe static string GetDefaultAudioDeviceId()
		{
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			NativeMethods.Client.GetDefaultAudioDeviceId(&discord_String);
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe static string GetDefaultCommunicationScopes()
		{
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			NativeMethods.Client.GetDefaultCommunicationScopes(&discord_String);
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe static string GetDefaultPresenceScopes()
		{
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			NativeMethods.Client.GetDefaultPresenceScopes(&discord_String);
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe static string GetVersionHash()
		{
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			NativeMethods.Client.GetVersionHash(&discord_String);
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public static int GetVersionMajor()
		{
			return NativeMethods.Client.GetVersionMajor();
		}

		public static int GetVersionMinor()
		{
			return NativeMethods.Client.GetVersionMinor();
		}

		public static int GetVersionPatch()
		{
			return NativeMethods.Client.GetVersionPatch();
		}

		public unsafe void SetHttpRequestTimeout(int httpTimeoutInMilliseconds)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetHttpRequestTimeout(ptr, httpTimeoutInMilliseconds);
			}
		}

		public unsafe static string StatusToString(Status type)
		{
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			NativeMethods.Client.StatusToString(type, &discord_String);
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe static string ThreadToString(Thread type)
		{
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			NativeMethods.Client.ThreadToString(type, &discord_String);
			string result = Marshal.PtrToStringUTF8((IntPtr)discord_String.ptr, (int)(uint)discord_String.size);
			NativeMethods.Discord_Free(discord_String.ptr);
			return result;
		}

		public unsafe void EndCall(ulong channelId, EndCallCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.EndCallCallback callback2 = NativeMethods.Client.EndCallCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.EndCall(ptr, channelId, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
		}

		public unsafe void EndCalls(EndCallsCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.EndCallsCallback callback2 = NativeMethods.Client.EndCallsCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.EndCalls(ptr, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
		}

		public unsafe Call? GetCall(ulong channelId)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Call call = default(NativeMethods.Call);
			bool call2;
			fixed (NativeMethods.Client* ptr = &self)
			{
				call2 = NativeMethods.Client.GetCall(ptr, channelId, &call);
			}
			if (!call2)
			{
				return null;
			}
			return new Call(call, 0);
		}

		public unsafe Call?[] GetCalls()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Discord_CallSpan discord_CallSpan = default(NativeMethods.Discord_CallSpan);
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.GetCalls(ptr, &discord_CallSpan);
			}
			Call[] array = new Call[(uint)discord_CallSpan.size];
			for (int i = 0; i < (int)(uint)discord_CallSpan.size; i++)
			{
				array[i] = new Call(discord_CallSpan.ptr[i], 0);
			}
			NativeMethods.Discord_Free(discord_CallSpan.ptr);
			return array;
		}

		public unsafe void GetCurrentInputDevice(GetCurrentInputDeviceCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.GetCurrentInputDeviceCallback cb2 = NativeMethods.Client.GetCurrentInputDeviceCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.GetCurrentInputDevice(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void GetCurrentOutputDevice(GetCurrentOutputDeviceCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.GetCurrentOutputDeviceCallback cb2 = NativeMethods.Client.GetCurrentOutputDeviceCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.GetCurrentOutputDevice(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void GetInputDevices(GetInputDevicesCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.GetInputDevicesCallback cb2 = NativeMethods.Client.GetInputDevicesCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.GetInputDevices(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe float GetInputVolume()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			float inputVolume;
			fixed (NativeMethods.Client* ptr = &self)
			{
				inputVolume = NativeMethods.Client.GetInputVolume(ptr);
			}
			return inputVolume;
		}

		public unsafe void GetOutputDevices(GetOutputDevicesCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.GetOutputDevicesCallback cb2 = NativeMethods.Client.GetOutputDevicesCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.GetOutputDevices(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe float GetOutputVolume()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			float outputVolume;
			fixed (NativeMethods.Client* ptr = &self)
			{
				outputVolume = NativeMethods.Client.GetOutputVolume(ptr);
			}
			return outputVolume;
		}

		public unsafe bool GetSelfDeafAll()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			bool selfDeafAll;
			fixed (NativeMethods.Client* ptr = &self)
			{
				selfDeafAll = NativeMethods.Client.GetSelfDeafAll(ptr);
			}
			return selfDeafAll;
		}

		public unsafe bool GetSelfMuteAll()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			bool selfMuteAll;
			fixed (NativeMethods.Client* ptr = &self)
			{
				selfMuteAll = NativeMethods.Client.GetSelfMuteAll(ptr);
			}
			return selfMuteAll;
		}

		public unsafe void SetAutomaticGainControl(bool on)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetAutomaticGainControl(ptr, on);
			}
		}

		public unsafe void SetDeviceChangeCallback(DeviceChangeCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.DeviceChangeCallback callback2 = NativeMethods.Client.DeviceChangeCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetDeviceChangeCallback(ptr, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
		}

		public unsafe void SetEchoCancellation(bool on)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetEchoCancellation(ptr, on);
			}
		}

		public unsafe void SetEngineManagedAudioSession(bool isEngineManaged)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetEngineManagedAudioSession(ptr, isEngineManaged);
			}
		}

		public unsafe void SetInputDevice(string deviceId, SetInputDeviceCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String deviceId2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &deviceId2, deviceId);
			NativeMethods.Client.SetInputDeviceCallback cb2 = NativeMethods.Client.SetInputDeviceCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetInputDevice(ptr, deviceId2, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
			NativeMethods.__FreeLocal(&deviceId2, owned);
		}

		public unsafe void SetInputVolume(float inputVolume)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetInputVolume(ptr, inputVolume);
			}
		}

		public unsafe void SetNoAudioInputCallback(NoAudioInputCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.NoAudioInputCallback callback2 = NativeMethods.Client.NoAudioInputCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetNoAudioInputCallback(ptr, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
		}

		public unsafe void SetNoAudioInputThreshold(float dBFSThreshold)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetNoAudioInputThreshold(ptr, dBFSThreshold);
			}
		}

		public unsafe void SetNoiseSuppression(bool on)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetNoiseSuppression(ptr, on);
			}
		}

		public unsafe void SetOpusHardwareCoding(bool encode, bool decode)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetOpusHardwareCoding(ptr, encode, decode);
			}
		}

		public unsafe void SetOutputDevice(string deviceId, SetOutputDeviceCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String deviceId2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &deviceId2, deviceId);
			NativeMethods.Client.SetOutputDeviceCallback cb2 = NativeMethods.Client.SetOutputDeviceCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetOutputDevice(ptr, deviceId2, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
			NativeMethods.__FreeLocal(&deviceId2, owned);
		}

		public unsafe void SetOutputVolume(float outputVolume)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetOutputVolume(ptr, outputVolume);
			}
		}

		public unsafe void SetSelfDeafAll(bool deaf)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetSelfDeafAll(ptr, deaf);
			}
		}

		public unsafe void SetSelfMuteAll(bool mute)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetSelfMuteAll(ptr, mute);
			}
		}

		public unsafe bool SetSpeakerMode(bool speakerMode)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			bool result;
			fixed (NativeMethods.Client* ptr = &self)
			{
				result = NativeMethods.Client.SetSpeakerMode(ptr, speakerMode);
			}
			return result;
		}

		public unsafe void SetThreadPriority(Thread thread, int priority)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetThreadPriority(ptr, thread, priority);
			}
		}

		public unsafe void SetVoiceParticipantChangedCallback(VoiceParticipantChangedCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.VoiceParticipantChangedCallback cb2 = NativeMethods.Client.VoiceParticipantChangedCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetVoiceParticipantChangedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe bool ShowAudioRoutePicker()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			bool result;
			fixed (NativeMethods.Client* ptr = &self)
			{
				result = NativeMethods.Client.ShowAudioRoutePicker(ptr);
			}
			return result;
		}

		public unsafe Call? StartCall(ulong channelId)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Call call = default(NativeMethods.Call);
			bool num;
			fixed (NativeMethods.Client* ptr = &self)
			{
				num = NativeMethods.Client.StartCall(ptr, channelId, &call);
			}
			if (!num)
			{
				return null;
			}
			return new Call(call, 0);
		}

		public unsafe Call? StartCallWithAudioCallbacks(ulong lobbyId, UserAudioReceivedCallback receivedCb, UserAudioCapturedCallback capturedCb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Call call = default(NativeMethods.Call);
			NativeMethods.Client.UserAudioReceivedCallback receivedCb2 = NativeMethods.Client.UserAudioReceivedCallback_Handler;
			NativeMethods.Client.UserAudioCapturedCallback capturedCb2 = NativeMethods.Client.UserAudioCapturedCallback_Handler;
			bool num;
			fixed (NativeMethods.Client* ptr = &self)
			{
				num = NativeMethods.Client.StartCallWithAudioCallbacks(ptr, lobbyId, receivedCb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(receivedCb), capturedCb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(capturedCb), &call);
			}
			if (!num)
			{
				return null;
			}
			return new Call(call, 0);
		}

		public unsafe void AbortAuthorize()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.AbortAuthorize(ptr);
			}
		}

		public unsafe void AbortGetTokenFromDevice()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.AbortGetTokenFromDevice(ptr);
			}
		}

		public unsafe void Authorize(AuthorizationArgs args, AuthorizationCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.AuthorizationArgs* args2 = &args.self)
			{
				NativeMethods.Client.AuthorizationCallback callback2 = NativeMethods.Client.AuthorizationCallback_Handler;
				fixed (NativeMethods.Client* ptr = &self)
				{
					NativeMethods.Client.Authorize(ptr, args2, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
				}
			}
		}

		public unsafe void CloseAuthorizeDeviceScreen()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.CloseAuthorizeDeviceScreen(ptr);
			}
		}

		public unsafe AuthorizationCodeVerifier CreateAuthorizationCodeVerifier()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.AuthorizationCodeVerifier authorizationCodeVerifier = default(NativeMethods.AuthorizationCodeVerifier);
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.CreateAuthorizationCodeVerifier(ptr, &authorizationCodeVerifier);
			}
			return new AuthorizationCodeVerifier(authorizationCodeVerifier, 0);
		}

		public unsafe void FetchCurrentUser(AuthorizationTokenType tokenType, string token, FetchCurrentUserCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String token2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &token2, token);
			NativeMethods.Client.FetchCurrentUserCallback callback2 = NativeMethods.Client.FetchCurrentUserCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.FetchCurrentUser(ptr, tokenType, token2, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
			NativeMethods.__FreeLocal(&token2, owned);
		}

		public unsafe void GetProvisionalToken(ulong applicationId, AuthenticationExternalAuthType externalAuthType, string externalAuthToken, TokenExchangeCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String externalAuthToken2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &externalAuthToken2, externalAuthToken);
			NativeMethods.Client.TokenExchangeCallback callback2 = NativeMethods.Client.TokenExchangeCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.GetProvisionalToken(ptr, applicationId, externalAuthType, externalAuthToken2, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
			NativeMethods.__FreeLocal(&externalAuthToken2, owned);
		}

		public unsafe void GetToken(ulong applicationId, string code, string codeVerifier, string redirectUri, TokenExchangeCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String code2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &code2, code);
			NativeMethods.Discord_String codeVerifier2 = default(NativeMethods.Discord_String);
			bool owned2 = NativeMethods.__InitStringLocal(buf, &num, 1024, &codeVerifier2, codeVerifier);
			NativeMethods.Discord_String redirectUri2 = default(NativeMethods.Discord_String);
			bool owned3 = NativeMethods.__InitStringLocal(buf, &num, 1024, &redirectUri2, redirectUri);
			NativeMethods.Client.TokenExchangeCallback callback2 = NativeMethods.Client.TokenExchangeCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.GetToken(ptr, applicationId, code2, codeVerifier2, redirectUri2, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
			NativeMethods.__FreeLocal(&redirectUri2, owned3);
			NativeMethods.__FreeLocal(&codeVerifier2, owned2);
			NativeMethods.__FreeLocal(&code2, owned);
		}

		public unsafe void GetTokenFromDevice(DeviceAuthorizationArgs args, TokenExchangeCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.DeviceAuthorizationArgs* args2 = &args.self)
			{
				NativeMethods.Client.TokenExchangeCallback callback2 = NativeMethods.Client.TokenExchangeCallback_Handler;
				fixed (NativeMethods.Client* ptr = &self)
				{
					NativeMethods.Client.GetTokenFromDevice(ptr, args2, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
				}
			}
		}

		public unsafe void GetTokenFromDeviceProvisionalMerge(DeviceAuthorizationArgs args, AuthenticationExternalAuthType externalAuthType, string externalAuthToken, TokenExchangeCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.DeviceAuthorizationArgs* args2 = &args.self)
			{
				byte* buf = stackalloc byte[1024];
				int num = 0;
				NativeMethods.Discord_String externalAuthToken2 = default(NativeMethods.Discord_String);
				bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &externalAuthToken2, externalAuthToken);
				NativeMethods.Client.TokenExchangeCallback callback2 = NativeMethods.Client.TokenExchangeCallback_Handler;
				fixed (NativeMethods.Client* ptr = &self)
				{
					NativeMethods.Client.GetTokenFromDeviceProvisionalMerge(ptr, args2, externalAuthType, externalAuthToken2, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
				}
				NativeMethods.__FreeLocal(&externalAuthToken2, owned);
			}
		}

		public unsafe void GetTokenFromProvisionalMerge(ulong applicationId, string code, string codeVerifier, string redirectUri, AuthenticationExternalAuthType externalAuthType, string externalAuthToken, TokenExchangeCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String code2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &code2, code);
			NativeMethods.Discord_String codeVerifier2 = default(NativeMethods.Discord_String);
			bool owned2 = NativeMethods.__InitStringLocal(buf, &num, 1024, &codeVerifier2, codeVerifier);
			NativeMethods.Discord_String redirectUri2 = default(NativeMethods.Discord_String);
			bool owned3 = NativeMethods.__InitStringLocal(buf, &num, 1024, &redirectUri2, redirectUri);
			NativeMethods.Discord_String externalAuthToken2 = default(NativeMethods.Discord_String);
			bool owned4 = NativeMethods.__InitStringLocal(buf, &num, 1024, &externalAuthToken2, externalAuthToken);
			NativeMethods.Client.TokenExchangeCallback callback2 = NativeMethods.Client.TokenExchangeCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.GetTokenFromProvisionalMerge(ptr, applicationId, code2, codeVerifier2, redirectUri2, externalAuthType, externalAuthToken2, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
			NativeMethods.__FreeLocal(&externalAuthToken2, owned4);
			NativeMethods.__FreeLocal(&redirectUri2, owned3);
			NativeMethods.__FreeLocal(&codeVerifier2, owned2);
			NativeMethods.__FreeLocal(&code2, owned);
		}

		public unsafe bool IsAuthenticated()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			bool result;
			fixed (NativeMethods.Client* ptr = &self)
			{
				result = NativeMethods.Client.IsAuthenticated(ptr);
			}
			return result;
		}

		public unsafe void OpenAuthorizeDeviceScreen(ulong clientId, string userCode)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String userCode2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &userCode2, userCode);
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.OpenAuthorizeDeviceScreen(ptr, clientId, userCode2);
			}
			NativeMethods.__FreeLocal(&userCode2, owned);
		}

		public unsafe void ProvisionalUserMergeCompleted(bool success)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.ProvisionalUserMergeCompleted(ptr, success);
			}
		}

		public unsafe void RefreshToken(ulong applicationId, string refreshToken, TokenExchangeCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String refreshToken2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &refreshToken2, refreshToken);
			NativeMethods.Client.TokenExchangeCallback callback2 = NativeMethods.Client.TokenExchangeCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.RefreshToken(ptr, applicationId, refreshToken2, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
			NativeMethods.__FreeLocal(&refreshToken2, owned);
		}

		public unsafe void SetAuthorizeDeviceScreenClosedCallback(AuthorizeDeviceScreenClosedCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.AuthorizeDeviceScreenClosedCallback cb2 = NativeMethods.Client.AuthorizeDeviceScreenClosedCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetAuthorizeDeviceScreenClosedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetGameWindowPid(int pid)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetGameWindowPid(ptr, pid);
			}
		}

		public unsafe void SetTokenExpirationCallback(TokenExpirationCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.TokenExpirationCallback callback2 = NativeMethods.Client.TokenExpirationCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetTokenExpirationCallback(ptr, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
		}

		public unsafe void UpdateProvisionalAccountDisplayName(string name, UpdateProvisionalAccountDisplayNameCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String name2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &name2, name);
			NativeMethods.Client.UpdateProvisionalAccountDisplayNameCallback callback2 = NativeMethods.Client.UpdateProvisionalAccountDisplayNameCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.UpdateProvisionalAccountDisplayName(ptr, name2, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
			NativeMethods.__FreeLocal(&name2, owned);
		}

		public unsafe void UpdateToken(AuthorizationTokenType tokenType, string token, UpdateTokenCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String token2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &token2, token);
			NativeMethods.Client.UpdateTokenCallback callback2 = NativeMethods.Client.UpdateTokenCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.UpdateToken(ptr, tokenType, token2, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
			NativeMethods.__FreeLocal(&token2, owned);
		}

		public unsafe bool CanOpenMessageInDiscord(ulong messageId)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			bool result;
			fixed (NativeMethods.Client* ptr = &self)
			{
				result = NativeMethods.Client.CanOpenMessageInDiscord(ptr, messageId);
			}
			return result;
		}

		public unsafe void DeleteUserMessage(ulong recipientId, ulong messageId, DeleteUserMessageCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.DeleteUserMessageCallback cb2 = NativeMethods.Client.DeleteUserMessageCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.DeleteUserMessage(ptr, recipientId, messageId, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void EditUserMessage(ulong recipientId, ulong messageId, string content, EditUserMessageCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String content2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &content2, content);
			NativeMethods.Client.EditUserMessageCallback cb2 = NativeMethods.Client.EditUserMessageCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.EditUserMessage(ptr, recipientId, messageId, content2, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
			NativeMethods.__FreeLocal(&content2, owned);
		}

		public unsafe ChannelHandle? GetChannelHandle(ulong channelId)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.ChannelHandle channelHandle = default(NativeMethods.ChannelHandle);
			bool channelHandle2;
			fixed (NativeMethods.Client* ptr = &self)
			{
				channelHandle2 = NativeMethods.Client.GetChannelHandle(ptr, channelId, &channelHandle);
			}
			if (!channelHandle2)
			{
				return null;
			}
			return new ChannelHandle(channelHandle, 0);
		}

		public unsafe MessageHandle? GetMessageHandle(ulong messageId)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.MessageHandle messageHandle = default(NativeMethods.MessageHandle);
			bool messageHandle2;
			fixed (NativeMethods.Client* ptr = &self)
			{
				messageHandle2 = NativeMethods.Client.GetMessageHandle(ptr, messageId, &messageHandle);
			}
			if (!messageHandle2)
			{
				return null;
			}
			return new MessageHandle(messageHandle, 0);
		}

		public unsafe void OpenMessageInDiscord(ulong messageId, ProvisionalUserMergeRequiredCallback provisionalUserMergeRequiredCallback, OpenMessageInDiscordCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.ProvisionalUserMergeRequiredCallback provisionalUserMergeRequiredCallback2 = NativeMethods.Client.ProvisionalUserMergeRequiredCallback_Handler;
			NativeMethods.Client.OpenMessageInDiscordCallback callback2 = NativeMethods.Client.OpenMessageInDiscordCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.OpenMessageInDiscord(ptr, messageId, provisionalUserMergeRequiredCallback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(provisionalUserMergeRequiredCallback), callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
		}

		public unsafe void SendLobbyMessage(ulong lobbyId, string content, SendUserMessageCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String content2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &content2, content);
			NativeMethods.Client.SendUserMessageCallback cb2 = NativeMethods.Client.SendUserMessageCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SendLobbyMessage(ptr, lobbyId, content2, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
			NativeMethods.__FreeLocal(&content2, owned);
		}

		public unsafe void SendLobbyMessageWithMetadata(ulong lobbyId, string content, Dictionary<string, string> metadata, SendUserMessageCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String content2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &content2, content);
			NativeMethods.Discord_Properties metadata2 = default(NativeMethods.Discord_Properties);
			metadata2.size = (IntPtr)metadata.Count;
			NativeMethods.Discord_String* ptr = default(NativeMethods.Discord_String*);
			bool owned2 = NativeMethods.__AllocLocalStringArray(buf, &num, 1024, &ptr, metadata.Count);
			NativeMethods.Discord_String* ptr2 = default(NativeMethods.Discord_String*);
			bool owned3 = NativeMethods.__AllocLocalStringArray(buf, &num, 1024, &ptr2, metadata.Count);
			bool* ptr3 = default(bool*);
			NativeMethods.__AllocateLocalBoolArray(buf, &num, 1024, &ptr3, metadata.Count);
			bool* ptr4 = default(bool*);
			NativeMethods.__AllocateLocalBoolArray(buf, &num, 1024, &ptr4, metadata.Count);
			int num2 = 0;
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			NativeMethods.Discord_String discord_String2 = default(NativeMethods.Discord_String);
			foreach (var (value, value2) in metadata)
			{
				ptr3[num2] = NativeMethods.__InitStringLocal(buf, &num, 1024, &discord_String, value);
				ptr4[num2] = NativeMethods.__InitStringLocal(buf, &num, 1024, &discord_String2, value2);
				ptr[num2] = discord_String;
				ptr2[num2] = discord_String2;
				num2++;
			}
			metadata2.keys = ptr;
			metadata2.values = ptr2;
			NativeMethods.Client.SendUserMessageCallback cb2 = NativeMethods.Client.SendUserMessageCallback_Handler;
			fixed (NativeMethods.Client* ptr5 = &self)
			{
				NativeMethods.Client.SendLobbyMessageWithMetadata(ptr5, lobbyId, content2, metadata2, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
			for (int i = 0; i < (int)metadata2.size; i++)
			{
				NativeMethods.__FreeLocal(ptr + i, ptr3[i]);
				NativeMethods.__FreeLocal(ptr2 + i, ptr4[i]);
			}
			NativeMethods.__FreeLocal(ptr, owned2);
			NativeMethods.__FreeLocal(ptr2, owned3);
			NativeMethods.__FreeLocal(&content2, owned);
		}

		public unsafe void SendUserMessage(ulong recipientId, string content, SendUserMessageCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String content2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &content2, content);
			NativeMethods.Client.SendUserMessageCallback cb2 = NativeMethods.Client.SendUserMessageCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SendUserMessage(ptr, recipientId, content2, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
			NativeMethods.__FreeLocal(&content2, owned);
		}

		public unsafe void SendUserMessageWithMetadata(ulong recipientId, string content, Dictionary<string, string> metadata, SendUserMessageCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String content2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &content2, content);
			NativeMethods.Discord_Properties metadata2 = default(NativeMethods.Discord_Properties);
			metadata2.size = (IntPtr)metadata.Count;
			NativeMethods.Discord_String* ptr = default(NativeMethods.Discord_String*);
			bool owned2 = NativeMethods.__AllocLocalStringArray(buf, &num, 1024, &ptr, metadata.Count);
			NativeMethods.Discord_String* ptr2 = default(NativeMethods.Discord_String*);
			bool owned3 = NativeMethods.__AllocLocalStringArray(buf, &num, 1024, &ptr2, metadata.Count);
			bool* ptr3 = default(bool*);
			NativeMethods.__AllocateLocalBoolArray(buf, &num, 1024, &ptr3, metadata.Count);
			bool* ptr4 = default(bool*);
			NativeMethods.__AllocateLocalBoolArray(buf, &num, 1024, &ptr4, metadata.Count);
			int num2 = 0;
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			NativeMethods.Discord_String discord_String2 = default(NativeMethods.Discord_String);
			foreach (var (value, value2) in metadata)
			{
				ptr3[num2] = NativeMethods.__InitStringLocal(buf, &num, 1024, &discord_String, value);
				ptr4[num2] = NativeMethods.__InitStringLocal(buf, &num, 1024, &discord_String2, value2);
				ptr[num2] = discord_String;
				ptr2[num2] = discord_String2;
				num2++;
			}
			metadata2.keys = ptr;
			metadata2.values = ptr2;
			NativeMethods.Client.SendUserMessageCallback cb2 = NativeMethods.Client.SendUserMessageCallback_Handler;
			fixed (NativeMethods.Client* ptr5 = &self)
			{
				NativeMethods.Client.SendUserMessageWithMetadata(ptr5, recipientId, content2, metadata2, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
			for (int i = 0; i < (int)metadata2.size; i++)
			{
				NativeMethods.__FreeLocal(ptr + i, ptr3[i]);
				NativeMethods.__FreeLocal(ptr2 + i, ptr4[i]);
			}
			NativeMethods.__FreeLocal(ptr, owned2);
			NativeMethods.__FreeLocal(ptr2, owned3);
			NativeMethods.__FreeLocal(&content2, owned);
		}

		public unsafe void SetMessageCreatedCallback(MessageCreatedCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.MessageCreatedCallback cb2 = NativeMethods.Client.MessageCreatedCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetMessageCreatedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetMessageDeletedCallback(MessageDeletedCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.MessageDeletedCallback cb2 = NativeMethods.Client.MessageDeletedCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetMessageDeletedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetMessageUpdatedCallback(MessageUpdatedCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.MessageUpdatedCallback cb2 = NativeMethods.Client.MessageUpdatedCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetMessageUpdatedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetShowingChat(bool showingChat)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetShowingChat(ptr, showingChat);
			}
		}

		public unsafe void AddLogCallback(LogCallback callback, LoggingSeverity minSeverity)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.LogCallback callback2 = NativeMethods.Client.LogCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.AddLogCallback(ptr, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback), minSeverity);
			}
		}

		public unsafe void AddVoiceLogCallback(LogCallback callback, LoggingSeverity minSeverity)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.LogCallback callback2 = NativeMethods.Client.LogCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.AddVoiceLogCallback(ptr, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback), minSeverity);
			}
		}

		public unsafe void Connect()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.Connect(ptr);
			}
		}

		public unsafe void Disconnect()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.Disconnect(ptr);
			}
		}

		public unsafe Status GetStatus()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			Status status;
			fixed (NativeMethods.Client* ptr = &self)
			{
				status = NativeMethods.Client.GetStatus(ptr);
			}
			return status;
		}

		public unsafe void OpenConnectedGamesSettingsInDiscord(OpenConnectedGamesSettingsInDiscordCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.OpenConnectedGamesSettingsInDiscordCallback callback2 = NativeMethods.Client.OpenConnectedGamesSettingsInDiscordCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.OpenConnectedGamesSettingsInDiscord(ptr, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
		}

		public unsafe void SetApplicationId(ulong applicationId)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetApplicationId(ptr, applicationId);
			}
		}

		public unsafe bool SetLogDir(string path, LoggingSeverity minSeverity)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String path2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &path2, path);
			bool result;
			fixed (NativeMethods.Client* ptr = &self)
			{
				result = NativeMethods.Client.SetLogDir(ptr, path2, minSeverity);
			}
			NativeMethods.__FreeLocal(&path2, owned);
			return result;
		}

		public unsafe void SetStatusChangedCallback(OnStatusChanged cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.OnStatusChanged cb2 = NativeMethods.Client.OnStatusChanged_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetStatusChangedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetVoiceLogDir(string path, LoggingSeverity minSeverity)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String path2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &path2, path);
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetVoiceLogDir(ptr, path2, minSeverity);
			}
			NativeMethods.__FreeLocal(&path2, owned);
		}

		public unsafe void CreateOrJoinLobby(string secret, CreateOrJoinLobbyCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String secret2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &secret2, secret);
			NativeMethods.Client.CreateOrJoinLobbyCallback callback2 = NativeMethods.Client.CreateOrJoinLobbyCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.CreateOrJoinLobby(ptr, secret2, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
			NativeMethods.__FreeLocal(&secret2, owned);
		}

		public unsafe void CreateOrJoinLobbyWithMetadata(string secret, Dictionary<string, string> lobbyMetadata, Dictionary<string, string> memberMetadata, CreateOrJoinLobbyCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String secret2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &secret2, secret);
			NativeMethods.Discord_Properties lobbyMetadata2 = default(NativeMethods.Discord_Properties);
			lobbyMetadata2.size = (IntPtr)lobbyMetadata.Count;
			NativeMethods.Discord_String* ptr = default(NativeMethods.Discord_String*);
			bool owned2 = NativeMethods.__AllocLocalStringArray(buf, &num, 1024, &ptr, lobbyMetadata.Count);
			NativeMethods.Discord_String* ptr2 = default(NativeMethods.Discord_String*);
			bool owned3 = NativeMethods.__AllocLocalStringArray(buf, &num, 1024, &ptr2, lobbyMetadata.Count);
			bool* ptr3 = default(bool*);
			NativeMethods.__AllocateLocalBoolArray(buf, &num, 1024, &ptr3, lobbyMetadata.Count);
			bool* ptr4 = default(bool*);
			NativeMethods.__AllocateLocalBoolArray(buf, &num, 1024, &ptr4, lobbyMetadata.Count);
			int num2 = 0;
			string value;
			string key;
			NativeMethods.Discord_String discord_String = default(NativeMethods.Discord_String);
			NativeMethods.Discord_String discord_String2 = default(NativeMethods.Discord_String);
			foreach (KeyValuePair<string, string> lobbyMetadatum in lobbyMetadata)
			{
				lobbyMetadatum.Deconstruct(out value, out key);
				string value2 = value;
				string value3 = key;
				ptr3[num2] = NativeMethods.__InitStringLocal(buf, &num, 1024, &discord_String, value2);
				ptr4[num2] = NativeMethods.__InitStringLocal(buf, &num, 1024, &discord_String2, value3);
				ptr[num2] = discord_String;
				ptr2[num2] = discord_String2;
				num2++;
			}
			lobbyMetadata2.keys = ptr;
			lobbyMetadata2.values = ptr2;
			NativeMethods.Discord_Properties memberMetadata2 = default(NativeMethods.Discord_Properties);
			memberMetadata2.size = (IntPtr)memberMetadata.Count;
			NativeMethods.Discord_String* ptr5 = default(NativeMethods.Discord_String*);
			bool owned4 = NativeMethods.__AllocLocalStringArray(buf, &num, 1024, &ptr5, memberMetadata.Count);
			NativeMethods.Discord_String* ptr6 = default(NativeMethods.Discord_String*);
			bool owned5 = NativeMethods.__AllocLocalStringArray(buf, &num, 1024, &ptr6, memberMetadata.Count);
			bool* ptr7 = default(bool*);
			NativeMethods.__AllocateLocalBoolArray(buf, &num, 1024, &ptr7, memberMetadata.Count);
			bool* ptr8 = default(bool*);
			NativeMethods.__AllocateLocalBoolArray(buf, &num, 1024, &ptr8, memberMetadata.Count);
			int num3 = 0;
			NativeMethods.Discord_String discord_String3 = default(NativeMethods.Discord_String);
			NativeMethods.Discord_String discord_String4 = default(NativeMethods.Discord_String);
			foreach (KeyValuePair<string, string> memberMetadatum in memberMetadata)
			{
				memberMetadatum.Deconstruct(out key, out value);
				string value4 = key;
				string value5 = value;
				ptr7[num3] = NativeMethods.__InitStringLocal(buf, &num, 1024, &discord_String3, value4);
				ptr8[num3] = NativeMethods.__InitStringLocal(buf, &num, 1024, &discord_String4, value5);
				ptr5[num3] = discord_String3;
				ptr6[num3] = discord_String4;
				num3++;
			}
			memberMetadata2.keys = ptr5;
			memberMetadata2.values = ptr6;
			NativeMethods.Client.CreateOrJoinLobbyCallback callback2 = NativeMethods.Client.CreateOrJoinLobbyCallback_Handler;
			fixed (NativeMethods.Client* ptr9 = &self)
			{
				NativeMethods.Client.CreateOrJoinLobbyWithMetadata(ptr9, secret2, lobbyMetadata2, memberMetadata2, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
			for (int i = 0; i < (int)memberMetadata2.size; i++)
			{
				NativeMethods.__FreeLocal(ptr5 + i, ptr7[i]);
				NativeMethods.__FreeLocal(ptr6 + i, ptr8[i]);
			}
			NativeMethods.__FreeLocal(ptr5, owned4);
			NativeMethods.__FreeLocal(ptr6, owned5);
			for (int j = 0; j < (int)lobbyMetadata2.size; j++)
			{
				NativeMethods.__FreeLocal(ptr + j, ptr3[j]);
				NativeMethods.__FreeLocal(ptr2 + j, ptr4[j]);
			}
			NativeMethods.__FreeLocal(ptr, owned2);
			NativeMethods.__FreeLocal(ptr2, owned3);
			NativeMethods.__FreeLocal(&secret2, owned);
		}

		public unsafe void GetGuildChannels(ulong guildId, GetGuildChannelsCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.GetGuildChannelsCallback cb2 = NativeMethods.Client.GetGuildChannelsCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.GetGuildChannels(ptr, guildId, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe LobbyHandle? GetLobbyHandle(ulong lobbyId)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.LobbyHandle lobbyHandle = default(NativeMethods.LobbyHandle);
			bool lobbyHandle2;
			fixed (NativeMethods.Client* ptr = &self)
			{
				lobbyHandle2 = NativeMethods.Client.GetLobbyHandle(ptr, lobbyId, &lobbyHandle);
			}
			if (!lobbyHandle2)
			{
				return null;
			}
			return new LobbyHandle(lobbyHandle, 0);
		}

		public unsafe ulong[] GetLobbyIds()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Discord_UInt64Span discord_UInt64Span = default(NativeMethods.Discord_UInt64Span);
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.GetLobbyIds(ptr, &discord_UInt64Span);
			}
			ulong[] result = new Span<ulong>(discord_UInt64Span.ptr, (int)(uint)discord_UInt64Span.size).ToArray();
			NativeMethods.Discord_Free(discord_UInt64Span.ptr);
			return result;
		}

		public unsafe void GetUserGuilds(GetUserGuildsCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.GetUserGuildsCallback cb2 = NativeMethods.Client.GetUserGuildsCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.GetUserGuilds(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void LeaveLobby(ulong lobbyId, LeaveLobbyCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.LeaveLobbyCallback callback2 = NativeMethods.Client.LeaveLobbyCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.LeaveLobby(ptr, lobbyId, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
		}

		public unsafe void LinkChannelToLobby(ulong lobbyId, ulong channelId, LinkOrUnlinkChannelCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.LinkOrUnlinkChannelCallback callback2 = NativeMethods.Client.LinkOrUnlinkChannelCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.LinkChannelToLobby(ptr, lobbyId, channelId, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
		}

		public unsafe void SetLobbyCreatedCallback(LobbyCreatedCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.LobbyCreatedCallback cb2 = NativeMethods.Client.LobbyCreatedCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetLobbyCreatedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetLobbyDeletedCallback(LobbyDeletedCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.LobbyDeletedCallback cb2 = NativeMethods.Client.LobbyDeletedCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetLobbyDeletedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetLobbyMemberAddedCallback(LobbyMemberAddedCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.LobbyMemberAddedCallback cb2 = NativeMethods.Client.LobbyMemberAddedCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetLobbyMemberAddedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetLobbyMemberRemovedCallback(LobbyMemberRemovedCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.LobbyMemberRemovedCallback cb2 = NativeMethods.Client.LobbyMemberRemovedCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetLobbyMemberRemovedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetLobbyMemberUpdatedCallback(LobbyMemberUpdatedCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.LobbyMemberUpdatedCallback cb2 = NativeMethods.Client.LobbyMemberUpdatedCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetLobbyMemberUpdatedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetLobbyUpdatedCallback(LobbyUpdatedCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.LobbyUpdatedCallback cb2 = NativeMethods.Client.LobbyUpdatedCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetLobbyUpdatedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void UnlinkChannelFromLobby(ulong lobbyId, LinkOrUnlinkChannelCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.LinkOrUnlinkChannelCallback callback2 = NativeMethods.Client.LinkOrUnlinkChannelCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.UnlinkChannelFromLobby(ptr, lobbyId, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
		}

		public unsafe void AcceptActivityInvite(ActivityInvite invite, AcceptActivityInviteCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.ActivityInvite* invite2 = &invite.self)
			{
				NativeMethods.Client.AcceptActivityInviteCallback cb2 = NativeMethods.Client.AcceptActivityInviteCallback_Handler;
				fixed (NativeMethods.Client* ptr = &self)
				{
					NativeMethods.Client.AcceptActivityInvite(ptr, invite2, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
				}
			}
		}

		public unsafe void ClearRichPresence()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.ClearRichPresence(ptr);
			}
		}

		public unsafe bool RegisterLaunchCommand(ulong applicationId, string command)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String command2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &command2, command);
			bool result;
			fixed (NativeMethods.Client* ptr = &self)
			{
				result = NativeMethods.Client.RegisterLaunchCommand(ptr, applicationId, command2);
			}
			NativeMethods.__FreeLocal(&command2, owned);
			return result;
		}

		public unsafe bool RegisterLaunchSteamApplication(ulong applicationId, uint steamAppId)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			bool result;
			fixed (NativeMethods.Client* ptr = &self)
			{
				result = NativeMethods.Client.RegisterLaunchSteamApplication(ptr, applicationId, steamAppId);
			}
			return result;
		}

		public unsafe void SendActivityInvite(ulong userId, string content, SendActivityInviteCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String content2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &content2, content);
			NativeMethods.Client.SendActivityInviteCallback cb2 = NativeMethods.Client.SendActivityInviteCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SendActivityInvite(ptr, userId, content2, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
			NativeMethods.__FreeLocal(&content2, owned);
		}

		public unsafe void SendActivityJoinRequest(ulong userId, SendActivityInviteCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.SendActivityInviteCallback cb2 = NativeMethods.Client.SendActivityInviteCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SendActivityJoinRequest(ptr, userId, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SendActivityJoinRequestReply(ActivityInvite invite, SendActivityInviteCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.ActivityInvite* invite2 = &invite.self)
			{
				NativeMethods.Client.SendActivityInviteCallback cb2 = NativeMethods.Client.SendActivityInviteCallback_Handler;
				fixed (NativeMethods.Client* ptr = &self)
				{
					NativeMethods.Client.SendActivityJoinRequestReply(ptr, invite2, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
				}
			}
		}

		public unsafe void SetActivityInviteCreatedCallback(ActivityInviteCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.ActivityInviteCallback cb2 = NativeMethods.Client.ActivityInviteCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetActivityInviteCreatedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetActivityInviteUpdatedCallback(ActivityInviteCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.ActivityInviteCallback cb2 = NativeMethods.Client.ActivityInviteCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetActivityInviteUpdatedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetActivityJoinCallback(ActivityJoinCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.ActivityJoinCallback cb2 = NativeMethods.Client.ActivityJoinCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetActivityJoinCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetOnlineStatus(StatusType status, UpdateStatusCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.UpdateStatusCallback callback2 = NativeMethods.Client.UpdateStatusCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetOnlineStatus(ptr, status, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
		}

		public unsafe void UpdateRichPresence(Activity activity, UpdateRichPresenceCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			fixed (NativeMethods.Activity* activity2 = &activity.self)
			{
				NativeMethods.Client.UpdateRichPresenceCallback cb2 = NativeMethods.Client.UpdateRichPresenceCallback_Handler;
				fixed (NativeMethods.Client* ptr = &self)
				{
					NativeMethods.Client.UpdateRichPresence(ptr, activity2, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
				}
			}
		}

		public unsafe void AcceptDiscordFriendRequest(ulong userId, UpdateRelationshipCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.UpdateRelationshipCallback cb2 = NativeMethods.Client.UpdateRelationshipCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.AcceptDiscordFriendRequest(ptr, userId, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void AcceptGameFriendRequest(ulong userId, UpdateRelationshipCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.UpdateRelationshipCallback cb2 = NativeMethods.Client.UpdateRelationshipCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.AcceptGameFriendRequest(ptr, userId, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void BlockUser(ulong userId, UpdateRelationshipCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.UpdateRelationshipCallback cb2 = NativeMethods.Client.UpdateRelationshipCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.BlockUser(ptr, userId, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void CancelDiscordFriendRequest(ulong userId, UpdateRelationshipCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.UpdateRelationshipCallback cb2 = NativeMethods.Client.UpdateRelationshipCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.CancelDiscordFriendRequest(ptr, userId, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void CancelGameFriendRequest(ulong userId, UpdateRelationshipCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.UpdateRelationshipCallback cb2 = NativeMethods.Client.UpdateRelationshipCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.CancelGameFriendRequest(ptr, userId, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe RelationshipHandle GetRelationshipHandle(ulong userId)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.RelationshipHandle relationshipHandle = default(NativeMethods.RelationshipHandle);
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.GetRelationshipHandle(ptr, userId, &relationshipHandle);
			}
			return new RelationshipHandle(relationshipHandle, 0);
		}

		public unsafe RelationshipHandle[] GetRelationships()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Discord_RelationshipHandleSpan discord_RelationshipHandleSpan = default(NativeMethods.Discord_RelationshipHandleSpan);
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.GetRelationships(ptr, &discord_RelationshipHandleSpan);
			}
			RelationshipHandle[] array = new RelationshipHandle[(uint)discord_RelationshipHandleSpan.size];
			for (int i = 0; i < (int)(uint)discord_RelationshipHandleSpan.size; i++)
			{
				array[i] = new RelationshipHandle(discord_RelationshipHandleSpan.ptr[i], 0);
			}
			NativeMethods.Discord_Free(discord_RelationshipHandleSpan.ptr);
			return array;
		}

		public unsafe void RejectDiscordFriendRequest(ulong userId, UpdateRelationshipCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.UpdateRelationshipCallback cb2 = NativeMethods.Client.UpdateRelationshipCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.RejectDiscordFriendRequest(ptr, userId, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void RejectGameFriendRequest(ulong userId, UpdateRelationshipCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.UpdateRelationshipCallback cb2 = NativeMethods.Client.UpdateRelationshipCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.RejectGameFriendRequest(ptr, userId, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void RemoveDiscordAndGameFriend(ulong userId, UpdateRelationshipCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.UpdateRelationshipCallback cb2 = NativeMethods.Client.UpdateRelationshipCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.RemoveDiscordAndGameFriend(ptr, userId, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void RemoveGameFriend(ulong userId, UpdateRelationshipCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.UpdateRelationshipCallback cb2 = NativeMethods.Client.UpdateRelationshipCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.RemoveGameFriend(ptr, userId, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe UserHandle[] SearchFriendsByUsername(string searchStr)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Discord_UserHandleSpan discord_UserHandleSpan = default(NativeMethods.Discord_UserHandleSpan);
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String searchStr2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &searchStr2, searchStr);
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SearchFriendsByUsername(ptr, searchStr2, &discord_UserHandleSpan);
			}
			NativeMethods.__FreeLocal(&searchStr2, owned);
			UserHandle[] array = new UserHandle[(uint)discord_UserHandleSpan.size];
			for (int i = 0; i < (int)(uint)discord_UserHandleSpan.size; i++)
			{
				array[i] = new UserHandle(discord_UserHandleSpan.ptr[i], 0);
			}
			NativeMethods.Discord_Free(discord_UserHandleSpan.ptr);
			return array;
		}

		public unsafe void SendDiscordFriendRequest(string username, SendFriendRequestCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String username2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &username2, username);
			NativeMethods.Client.SendFriendRequestCallback cb2 = NativeMethods.Client.SendFriendRequestCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SendDiscordFriendRequest(ptr, username2, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
			NativeMethods.__FreeLocal(&username2, owned);
		}

		public unsafe void SendDiscordFriendRequestById(ulong userId, UpdateRelationshipCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.UpdateRelationshipCallback cb2 = NativeMethods.Client.UpdateRelationshipCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SendDiscordFriendRequestById(ptr, userId, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SendGameFriendRequest(string username, SendFriendRequestCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			byte* buf = stackalloc byte[1024];
			int num = 0;
			NativeMethods.Discord_String username2 = default(NativeMethods.Discord_String);
			bool owned = NativeMethods.__InitStringLocal(buf, &num, 1024, &username2, username);
			NativeMethods.Client.SendFriendRequestCallback cb2 = NativeMethods.Client.SendFriendRequestCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SendGameFriendRequest(ptr, username2, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
			NativeMethods.__FreeLocal(&username2, owned);
		}

		public unsafe void SendGameFriendRequestById(ulong userId, UpdateRelationshipCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.UpdateRelationshipCallback cb2 = NativeMethods.Client.UpdateRelationshipCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SendGameFriendRequestById(ptr, userId, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetRelationshipCreatedCallback(RelationshipCreatedCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.RelationshipCreatedCallback cb2 = NativeMethods.Client.RelationshipCreatedCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetRelationshipCreatedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void SetRelationshipDeletedCallback(RelationshipDeletedCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.RelationshipDeletedCallback cb2 = NativeMethods.Client.RelationshipDeletedCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetRelationshipDeletedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe void UnblockUser(ulong userId, UpdateRelationshipCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.UpdateRelationshipCallback cb2 = NativeMethods.Client.UpdateRelationshipCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.UnblockUser(ptr, userId, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}

		public unsafe UserHandle GetCurrentUser()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.UserHandle userHandle = default(NativeMethods.UserHandle);
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.GetCurrentUser(ptr, &userHandle);
			}
			return new UserHandle(userHandle, 0);
		}

		public unsafe void GetDiscordClientConnectedUser(ulong applicationId, GetDiscordClientConnectedUserCallback callback)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.GetDiscordClientConnectedUserCallback callback2 = NativeMethods.Client.GetDiscordClientConnectedUserCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.GetDiscordClientConnectedUser(ptr, applicationId, callback2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(callback));
			}
		}

		public unsafe UserHandle? GetUser(ulong userId)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.UserHandle userHandle = default(NativeMethods.UserHandle);
			bool user;
			fixed (NativeMethods.Client* ptr = &self)
			{
				user = NativeMethods.Client.GetUser(ptr, userId, &userHandle);
			}
			if (!user)
			{
				return null;
			}
			return new UserHandle(userHandle, 0);
		}

		public unsafe void SetUserUpdatedCallback(UserUpdatedCallback cb)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("Client");
			}
			NativeMethods.Client.UserUpdatedCallback cb2 = NativeMethods.Client.UserUpdatedCallback_Handler;
			fixed (NativeMethods.Client* ptr = &self)
			{
				NativeMethods.Client.SetUserUpdatedCallback(ptr, cb2, NativeMethods.ManagedUserData.Free, NativeMethods.ManagedUserData.CreateHandle(cb));
			}
		}
	}
	public class CallInfoHandle : IDisposable
	{
		internal NativeMethods.CallInfoHandle self;

		private int disposed_;

		internal CallInfoHandle(NativeMethods.CallInfoHandle self, int disposed)
		{
			this.self = self;
			disposed_ = disposed;
		}

		~CallInfoHandle()
		{
			Dispose();
		}

		public unsafe void Dispose()
		{
			if (Interlocked.Exchange(ref disposed_, 1) == 0)
			{
				GC.SuppressFinalize(this);
				fixed (NativeMethods.CallInfoHandle* ptr = &self)
				{
					NativeMethods.CallInfoHandle.Drop(ptr);
				}
			}
		}

		public unsafe CallInfoHandle(CallInfoHandle other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("CallInfoHandle");
			}
			if (other.disposed_ != 0)
			{
				throw new ObjectDisposedException("other");
			}
			fixed (NativeMethods.CallInfoHandle* other2 = &other.self)
			{
				fixed (NativeMethods.CallInfoHandle* ptr = &self)
				{
					NativeMethods.CallInfoHandle.Clone(ptr, other2);
				}
			}
		}

		internal unsafe CallInfoHandle(NativeMethods.CallInfoHandle* otherPtr)
		{
			fixed (NativeMethods.CallInfoHandle* ptr = &self)
			{
				NativeMethods.CallInfoHandle.Clone(ptr, otherPtr);
			}
		}

		public unsafe ulong ChannelId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("CallInfoHandle");
			}
			ulong result;
			fixed (NativeMethods.CallInfoHandle* ptr = &self)
			{
				result = NativeMethods.CallInfoHandle.ChannelId(ptr);
			}
			return result;
		}

		public unsafe ulong[] GetParticipants()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("CallInfoHandle");
			}
			NativeMethods.Discord_UInt64Span discord_UInt64Span = default(NativeMethods.Discord_UInt64Span);
			fixed (NativeMethods.CallInfoHandle* ptr = &self)
			{
				NativeMethods.CallInfoHandle.GetParticipants(ptr, &discord_UInt64Span);
			}
			ulong[] result = new Span<ulong>(discord_UInt64Span.ptr, (int)(uint)discord_UInt64Span.size).ToArray();
			NativeMethods.Discord_Free(discord_UInt64Span.ptr);
			return result;
		}

		public unsafe VoiceStateHandle? GetVoiceStateHandle(ulong userId)
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("CallInfoHandle");
			}
			NativeMethods.VoiceStateHandle voiceStateHandle = default(NativeMethods.VoiceStateHandle);
			bool voiceStateHandle2;
			fixed (NativeMethods.CallInfoHandle* ptr = &self)
			{
				voiceStateHandle2 = NativeMethods.CallInfoHandle.GetVoiceStateHandle(ptr, userId, &voiceStateHandle);
			}
			if (!voiceStateHandle2)
			{
				return null;
			}
			return new VoiceStateHandle(voiceStateHandle, 0);
		}

		public unsafe ulong GuildId()
		{
			if (disposed_ != 0)
			{
				throw new ObjectDisposedException("CallInfoHandle");
			}
			ulong result;
			fixed (NativeMethods.CallInfoHandle* ptr = &self)
			{
				result = NativeMethods.CallInfoHandle.GuildId(ptr);
			}
			return result;
		}
	}
}
