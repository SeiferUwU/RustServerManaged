using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: AssemblyProduct("Mono.Cecil")]
[assembly: AssemblyCopyright("Copyright © 2008 - 2018 Jb Evain")]
[assembly: ComVisible(false)]
[assembly: AssemblyFileVersion("0.11.5.0")]
[assembly: AssemblyInformationalVersion("0.11.5.0")]
[assembly: AssemblyTitle("Mono.Cecil.Rocks")]
[assembly: CLSCompliant(false)]
[assembly: TargetFramework(".NETFramework,Version=v4.0", FrameworkDisplayName = ".NET Framework 4")]
[assembly: AssemblyVersion("0.11.5.0")]
namespace Mono.Cecil.Rocks;

public class DocCommentId
{
	private class GenericTypeOptions
	{
		public bool IsArgument { get; set; }

		public bool IsNestedType { get; set; }

		public IList<TypeReference> Arguments { get; set; }

		public int ArgumentIndex { get; set; }

		public static GenericTypeOptions Empty()
		{
			return new GenericTypeOptions();
		}
	}

	private IMemberDefinition commentMember;

	private StringBuilder id;

	private DocCommentId(IMemberDefinition member)
	{
		commentMember = member;
		id = new StringBuilder();
	}

	private void WriteField(FieldDefinition field)
	{
		WriteDefinition('F', field);
	}

	private void WriteEvent(EventDefinition @event)
	{
		WriteDefinition('E', @event);
	}

	private void WriteType(TypeDefinition type)
	{
		id.Append('T').Append(':');
		WriteTypeFullName(type);
	}

	private void WriteMethod(MethodDefinition method)
	{
		WriteDefinition('M', method);
		if (method.HasGenericParameters)
		{
			id.Append('`').Append('`');
			id.Append(method.GenericParameters.Count);
		}
		if (method.HasParameters)
		{
			WriteParameters(method.Parameters);
		}
		if (IsConversionOperator(method))
		{
			WriteReturnType(method);
		}
	}

	private static bool IsConversionOperator(MethodDefinition self)
	{
		if (self == null)
		{
			throw new ArgumentNullException("self");
		}
		return self.IsSpecialName && (self.Name == "op_Explicit" || self.Name == "op_Implicit");
	}

	private void WriteReturnType(MethodDefinition method)
	{
		id.Append('~');
		WriteTypeSignature(method.ReturnType);
	}

	private void WriteProperty(PropertyDefinition property)
	{
		WriteDefinition('P', property);
		if (property.HasParameters)
		{
			WriteParameters(property.Parameters);
		}
	}

	private void WriteParameters(IList<ParameterDefinition> parameters)
	{
		id.Append('(');
		WriteList(parameters, delegate(ParameterDefinition p)
		{
			WriteTypeSignature(p.ParameterType);
		});
		id.Append(')');
	}

	private void WriteTypeSignature(TypeReference type)
	{
		switch (type.MetadataType)
		{
		case MetadataType.Array:
			WriteArrayTypeSignature((ArrayType)type);
			break;
		case MetadataType.ByReference:
			WriteTypeSignature(((ByReferenceType)type).ElementType);
			id.Append('@');
			break;
		case MetadataType.FunctionPointer:
			WriteFunctionPointerTypeSignature((FunctionPointerType)type);
			break;
		case MetadataType.GenericInstance:
			WriteGenericInstanceTypeSignature((GenericInstanceType)type);
			break;
		case MetadataType.Var:
			if (IsGenericMethodTypeParameter(type))
			{
				id.Append('`');
			}
			id.Append('`');
			id.Append(((GenericParameter)type).Position);
			break;
		case MetadataType.MVar:
			id.Append('`').Append('`');
			id.Append(((GenericParameter)type).Position);
			break;
		case MetadataType.OptionalModifier:
			WriteModiferTypeSignature((OptionalModifierType)type, '!');
			break;
		case MetadataType.RequiredModifier:
			WriteModiferTypeSignature((RequiredModifierType)type, '|');
			break;
		case MetadataType.Pointer:
			WriteTypeSignature(((PointerType)type).ElementType);
			id.Append('*');
			break;
		default:
			WriteTypeFullName(type);
			break;
		}
	}

	private bool IsGenericMethodTypeParameter(TypeReference type)
	{
		if (commentMember is MethodDefinition methodDefinition)
		{
			GenericParameter genericParameter = type as GenericParameter;
			if (genericParameter != null)
			{
				return methodDefinition.GenericParameters.Any((GenericParameter i) => i.Name == genericParameter.Name);
			}
		}
		return false;
	}

	private void WriteGenericInstanceTypeSignature(GenericInstanceType type)
	{
		if (type.ElementType.IsTypeSpecification())
		{
			throw new NotSupportedException();
		}
		GenericTypeOptions options = new GenericTypeOptions
		{
			IsArgument = true,
			IsNestedType = type.IsNested,
			Arguments = type.GenericArguments
		};
		WriteTypeFullName(type.ElementType, options);
	}

	private void WriteList<T>(IList<T> list, Action<T> action)
	{
		for (int i = 0; i < list.Count; i++)
		{
			if (i > 0)
			{
				id.Append(',');
			}
			action(list[i]);
		}
	}

	private void WriteModiferTypeSignature(IModifierType type, char id)
	{
		WriteTypeSignature(type.ElementType);
		this.id.Append(id);
		WriteTypeSignature(type.ModifierType);
	}

	private void WriteFunctionPointerTypeSignature(FunctionPointerType type)
	{
		id.Append("=FUNC:");
		WriteTypeSignature(type.ReturnType);
		if (type.HasParameters)
		{
			WriteParameters(type.Parameters);
		}
	}

	private void WriteArrayTypeSignature(ArrayType type)
	{
		WriteTypeSignature(type.ElementType);
		if (type.IsVector)
		{
			id.Append("[]");
			return;
		}
		id.Append("[");
		WriteList(type.Dimensions, delegate(ArrayDimension dimension)
		{
			if (dimension.LowerBound.HasValue)
			{
				id.Append(dimension.LowerBound.Value);
			}
			id.Append(':');
			if (dimension.UpperBound.HasValue)
			{
				id.Append(dimension.UpperBound.Value - (dimension.LowerBound.GetValueOrDefault() + 1));
			}
		});
		id.Append("]");
	}

	private void WriteDefinition(char id, IMemberDefinition member)
	{
		this.id.Append(id).Append(':');
		WriteTypeFullName(member.DeclaringType);
		this.id.Append('.');
		WriteItemName(member.Name);
	}

	private void WriteTypeFullName(TypeReference type)
	{
		WriteTypeFullName(type, GenericTypeOptions.Empty());
	}

	private void WriteTypeFullName(TypeReference type, GenericTypeOptions options)
	{
		if (type.DeclaringType != null)
		{
			WriteTypeFullName(type.DeclaringType, options);
			id.Append('.');
		}
		if (!string.IsNullOrEmpty(type.Namespace))
		{
			id.Append(type.Namespace);
			id.Append('.');
		}
		string text = type.Name;
		if (options.IsArgument)
		{
			int num = text.LastIndexOf('`');
			if (num > 0)
			{
				text = text.Substring(0, num);
			}
		}
		id.Append(text);
		WriteGenericTypeParameters(type, options);
	}

	private void WriteGenericTypeParameters(TypeReference type, GenericTypeOptions options)
	{
		if (options.IsArgument && IsGenericType(type))
		{
			id.Append('{');
			WriteList(GetGenericTypeArguments(type, options), WriteTypeSignature);
			id.Append('}');
		}
	}

	private static bool IsGenericType(TypeReference type)
	{
		if (type.HasGenericParameters)
		{
			string text = string.Empty;
			int num = type.Name.LastIndexOf('`');
			if (num >= 0)
			{
				text = type.Name.Substring(0, num);
			}
			return type.Name.LastIndexOf('`') == text.Length;
		}
		return false;
	}

	private IList<TypeReference> GetGenericTypeArguments(TypeReference type, GenericTypeOptions options)
	{
		if (options.IsNestedType)
		{
			int count = type.GenericParameters.Count;
			List<TypeReference> result = options.Arguments.Skip(options.ArgumentIndex).Take(count).ToList();
			options.ArgumentIndex += count;
			return result;
		}
		return options.Arguments;
	}

	private void WriteItemName(string name)
	{
		id.Append(name.Replace('.', '#').Replace('<', '{').Replace('>', '}'));
	}

	public override string ToString()
	{
		return id.ToString();
	}

	public static string GetDocCommentId(IMemberDefinition member)
	{
		if (member == null)
		{
			throw new ArgumentNullException("member");
		}
		DocCommentId docCommentId = new DocCommentId(member);
		switch (member.MetadataToken.TokenType)
		{
		case TokenType.Field:
			docCommentId.WriteField((FieldDefinition)member);
			break;
		case TokenType.Method:
			docCommentId.WriteMethod((MethodDefinition)member);
			break;
		case TokenType.TypeDef:
			docCommentId.WriteType((TypeDefinition)member);
			break;
		case TokenType.Event:
			docCommentId.WriteEvent((EventDefinition)member);
			break;
		case TokenType.Property:
			docCommentId.WriteProperty((PropertyDefinition)member);
			break;
		default:
			throw new NotSupportedException(member.FullName);
		}
		return docCommentId.ToString();
	}
}
internal static class Functional
{
	public static Func<A, R> Y<A, R>(Func<Func<A, R>, Func<A, R>> f)
	{
		Func<A, R> g = null;
		g = f((A a) => g(a));
		return g;
	}

	public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource element)
	{
		if (source == null)
		{
			throw new ArgumentNullException("source");
		}
		return PrependIterator(source, element);
	}

	private static IEnumerable<TSource> PrependIterator<TSource>(IEnumerable<TSource> source, TSource element)
	{
		yield return element;
		foreach (TSource item in source)
		{
			yield return item;
		}
	}
}
public interface IILVisitor
{
	void OnInlineNone(OpCode opcode);

	void OnInlineSByte(OpCode opcode, sbyte value);

	void OnInlineByte(OpCode opcode, byte value);

	void OnInlineInt32(OpCode opcode, int value);

	void OnInlineInt64(OpCode opcode, long value);

	void OnInlineSingle(OpCode opcode, float value);

	void OnInlineDouble(OpCode opcode, double value);

	void OnInlineString(OpCode opcode, string value);

	void OnInlineBranch(OpCode opcode, int offset);

	void OnInlineSwitch(OpCode opcode, int[] offsets);

	void OnInlineVariable(OpCode opcode, VariableDefinition variable);

	void OnInlineArgument(OpCode opcode, ParameterDefinition parameter);

	void OnInlineSignature(OpCode opcode, CallSite callSite);

	void OnInlineType(OpCode opcode, TypeReference type);

	void OnInlineField(OpCode opcode, FieldReference field);

	void OnInlineMethod(OpCode opcode, MethodReference method);
}
public static class ILParser
{
	private class ParseContext
	{
		public CodeReader Code { get; set; }

		public int Position { get; set; }

		public MetadataReader Metadata { get; set; }

		public Collection<VariableDefinition> Variables { get; set; }

		public IILVisitor Visitor { get; set; }
	}

	public static void Parse(MethodDefinition method, IILVisitor visitor)
	{
		if (method == null)
		{
			throw new ArgumentNullException("method");
		}
		if (visitor == null)
		{
			throw new ArgumentNullException("visitor");
		}
		if (!method.HasBody || !method.HasImage)
		{
			throw new ArgumentException();
		}
		method.Module.Read(method, delegate(MethodDefinition m, MetadataReader _)
		{
			ParseMethod(m, visitor);
			return true;
		});
	}

	private static void ParseMethod(MethodDefinition method, IILVisitor visitor)
	{
		ParseContext parseContext = CreateContext(method, visitor);
		CodeReader code = parseContext.Code;
		byte b = code.ReadByte();
		switch (b & 3)
		{
		case 2:
		{
			int code_size = b >> 2;
			ParseCode(code_size, parseContext);
			break;
		}
		case 3:
			code.Advance(-1);
			ParseFatMethod(parseContext);
			break;
		default:
			throw new NotSupportedException();
		}
		code.MoveBackTo(parseContext.Position);
	}

	private static ParseContext CreateContext(MethodDefinition method, IILVisitor visitor)
	{
		CodeReader codeReader = method.Module.Read(method, (MethodDefinition _, MetadataReader reader) => reader.code);
		int position = codeReader.MoveTo(method);
		return new ParseContext
		{
			Code = codeReader,
			Position = position,
			Metadata = codeReader.reader,
			Visitor = visitor
		};
	}

	private static void ParseFatMethod(ParseContext context)
	{
		CodeReader code = context.Code;
		code.Advance(4);
		int code_size = code.ReadInt32();
		MetadataToken metadataToken = code.ReadToken();
		if (metadataToken != MetadataToken.Zero)
		{
			context.Variables = code.ReadVariables(metadataToken);
		}
		ParseCode(code_size, context);
	}

	private static void ParseCode(int code_size, ParseContext context)
	{
		CodeReader code = context.Code;
		MetadataReader metadata = context.Metadata;
		IILVisitor visitor = context.Visitor;
		int position = code.Position;
		int num = position + code_size;
		while (code.Position < num)
		{
			byte b = code.ReadByte();
			OpCode opCode = ((b != 254) ? OpCodes.OneByteOpCode[b] : OpCodes.TwoBytesOpCode[code.ReadByte()]);
			switch (opCode.OperandType)
			{
			case OperandType.InlineNone:
				visitor.OnInlineNone(opCode);
				break;
			case OperandType.InlineSwitch:
			{
				int num2 = code.ReadInt32();
				int[] array = new int[num2];
				for (int i = 0; i < num2; i++)
				{
					array[i] = code.ReadInt32();
				}
				visitor.OnInlineSwitch(opCode, array);
				break;
			}
			case OperandType.ShortInlineBrTarget:
				visitor.OnInlineBranch(opCode, code.ReadSByte());
				break;
			case OperandType.InlineBrTarget:
				visitor.OnInlineBranch(opCode, code.ReadInt32());
				break;
			case OperandType.ShortInlineI:
				if (opCode == OpCodes.Ldc_I4_S)
				{
					visitor.OnInlineSByte(opCode, code.ReadSByte());
				}
				else
				{
					visitor.OnInlineByte(opCode, code.ReadByte());
				}
				break;
			case OperandType.InlineI:
				visitor.OnInlineInt32(opCode, code.ReadInt32());
				break;
			case OperandType.InlineI8:
				visitor.OnInlineInt64(opCode, code.ReadInt64());
				break;
			case OperandType.ShortInlineR:
				visitor.OnInlineSingle(opCode, code.ReadSingle());
				break;
			case OperandType.InlineR:
				visitor.OnInlineDouble(opCode, code.ReadDouble());
				break;
			case OperandType.InlineSig:
				visitor.OnInlineSignature(opCode, code.GetCallSite(code.ReadToken()));
				break;
			case OperandType.InlineString:
				visitor.OnInlineString(opCode, code.GetString(code.ReadToken()));
				break;
			case OperandType.ShortInlineArg:
				visitor.OnInlineArgument(opCode, code.GetParameter(code.ReadByte()));
				break;
			case OperandType.InlineArg:
				visitor.OnInlineArgument(opCode, code.GetParameter(code.ReadInt16()));
				break;
			case OperandType.ShortInlineVar:
				visitor.OnInlineVariable(opCode, GetVariable(context, code.ReadByte()));
				break;
			case OperandType.InlineVar:
				visitor.OnInlineVariable(opCode, GetVariable(context, code.ReadInt16()));
				break;
			case OperandType.InlineField:
			case OperandType.InlineMethod:
			case OperandType.InlineTok:
			case OperandType.InlineType:
			{
				IMetadataTokenProvider metadataTokenProvider = metadata.LookupToken(code.ReadToken());
				switch (metadataTokenProvider.MetadataToken.TokenType)
				{
				case TokenType.TypeRef:
				case TokenType.TypeDef:
				case TokenType.TypeSpec:
					visitor.OnInlineType(opCode, (TypeReference)metadataTokenProvider);
					break;
				case TokenType.Method:
				case TokenType.MethodSpec:
					visitor.OnInlineMethod(opCode, (MethodReference)metadataTokenProvider);
					break;
				case TokenType.Field:
					visitor.OnInlineField(opCode, (FieldReference)metadataTokenProvider);
					break;
				case TokenType.MemberRef:
					if (metadataTokenProvider is FieldReference field)
					{
						visitor.OnInlineField(opCode, field);
						break;
					}
					if (metadataTokenProvider is MethodReference method)
					{
						visitor.OnInlineMethod(opCode, method);
						break;
					}
					throw new InvalidOperationException();
				}
				break;
			}
			}
		}
	}

	private static VariableDefinition GetVariable(ParseContext context, int index)
	{
		return context.Variables[index];
	}
}
public static class MethodBodyRocks
{
	public static void SimplifyMacros(this Mono.Cecil.Cil.MethodBody self)
	{
		if (self == null)
		{
			throw new ArgumentNullException("self");
		}
		foreach (Instruction instruction in self.Instructions)
		{
			if (instruction.OpCode.OpCodeType == OpCodeType.Macro)
			{
				switch (instruction.OpCode.Code)
				{
				case Code.Ldarg_0:
					ExpandMacro(instruction, OpCodes.Ldarg, self.GetParameter(0));
					break;
				case Code.Ldarg_1:
					ExpandMacro(instruction, OpCodes.Ldarg, self.GetParameter(1));
					break;
				case Code.Ldarg_2:
					ExpandMacro(instruction, OpCodes.Ldarg, self.GetParameter(2));
					break;
				case Code.Ldarg_3:
					ExpandMacro(instruction, OpCodes.Ldarg, self.GetParameter(3));
					break;
				case Code.Ldloc_0:
					ExpandMacro(instruction, OpCodes.Ldloc, self.Variables[0]);
					break;
				case Code.Ldloc_1:
					ExpandMacro(instruction, OpCodes.Ldloc, self.Variables[1]);
					break;
				case Code.Ldloc_2:
					ExpandMacro(instruction, OpCodes.Ldloc, self.Variables[2]);
					break;
				case Code.Ldloc_3:
					ExpandMacro(instruction, OpCodes.Ldloc, self.Variables[3]);
					break;
				case Code.Stloc_0:
					ExpandMacro(instruction, OpCodes.Stloc, self.Variables[0]);
					break;
				case Code.Stloc_1:
					ExpandMacro(instruction, OpCodes.Stloc, self.Variables[1]);
					break;
				case Code.Stloc_2:
					ExpandMacro(instruction, OpCodes.Stloc, self.Variables[2]);
					break;
				case Code.Stloc_3:
					ExpandMacro(instruction, OpCodes.Stloc, self.Variables[3]);
					break;
				case Code.Ldarg_S:
					instruction.OpCode = OpCodes.Ldarg;
					break;
				case Code.Ldarga_S:
					instruction.OpCode = OpCodes.Ldarga;
					break;
				case Code.Starg_S:
					instruction.OpCode = OpCodes.Starg;
					break;
				case Code.Ldloc_S:
					instruction.OpCode = OpCodes.Ldloc;
					break;
				case Code.Ldloca_S:
					instruction.OpCode = OpCodes.Ldloca;
					break;
				case Code.Stloc_S:
					instruction.OpCode = OpCodes.Stloc;
					break;
				case Code.Ldc_I4_M1:
					ExpandMacro(instruction, OpCodes.Ldc_I4, -1);
					break;
				case Code.Ldc_I4_0:
					ExpandMacro(instruction, OpCodes.Ldc_I4, 0);
					break;
				case Code.Ldc_I4_1:
					ExpandMacro(instruction, OpCodes.Ldc_I4, 1);
					break;
				case Code.Ldc_I4_2:
					ExpandMacro(instruction, OpCodes.Ldc_I4, 2);
					break;
				case Code.Ldc_I4_3:
					ExpandMacro(instruction, OpCodes.Ldc_I4, 3);
					break;
				case Code.Ldc_I4_4:
					ExpandMacro(instruction, OpCodes.Ldc_I4, 4);
					break;
				case Code.Ldc_I4_5:
					ExpandMacro(instruction, OpCodes.Ldc_I4, 5);
					break;
				case Code.Ldc_I4_6:
					ExpandMacro(instruction, OpCodes.Ldc_I4, 6);
					break;
				case Code.Ldc_I4_7:
					ExpandMacro(instruction, OpCodes.Ldc_I4, 7);
					break;
				case Code.Ldc_I4_8:
					ExpandMacro(instruction, OpCodes.Ldc_I4, 8);
					break;
				case Code.Ldc_I4_S:
					ExpandMacro(instruction, OpCodes.Ldc_I4, (int)(sbyte)instruction.Operand);
					break;
				case Code.Br_S:
					instruction.OpCode = OpCodes.Br;
					break;
				case Code.Brfalse_S:
					instruction.OpCode = OpCodes.Brfalse;
					break;
				case Code.Brtrue_S:
					instruction.OpCode = OpCodes.Brtrue;
					break;
				case Code.Beq_S:
					instruction.OpCode = OpCodes.Beq;
					break;
				case Code.Bge_S:
					instruction.OpCode = OpCodes.Bge;
					break;
				case Code.Bgt_S:
					instruction.OpCode = OpCodes.Bgt;
					break;
				case Code.Ble_S:
					instruction.OpCode = OpCodes.Ble;
					break;
				case Code.Blt_S:
					instruction.OpCode = OpCodes.Blt;
					break;
				case Code.Bne_Un_S:
					instruction.OpCode = OpCodes.Bne_Un;
					break;
				case Code.Bge_Un_S:
					instruction.OpCode = OpCodes.Bge_Un;
					break;
				case Code.Bgt_Un_S:
					instruction.OpCode = OpCodes.Bgt_Un;
					break;
				case Code.Ble_Un_S:
					instruction.OpCode = OpCodes.Ble_Un;
					break;
				case Code.Blt_Un_S:
					instruction.OpCode = OpCodes.Blt_Un;
					break;
				case Code.Leave_S:
					instruction.OpCode = OpCodes.Leave;
					break;
				}
			}
		}
	}

	private static void ExpandMacro(Instruction instruction, OpCode opcode, object operand)
	{
		instruction.OpCode = opcode;
		instruction.Operand = operand;
	}

	private static void MakeMacro(Instruction instruction, OpCode opcode)
	{
		instruction.OpCode = opcode;
		instruction.Operand = null;
	}

	public static void Optimize(this Mono.Cecil.Cil.MethodBody self)
	{
		if (self == null)
		{
			throw new ArgumentNullException("self");
		}
		self.OptimizeLongs();
		self.OptimizeMacros();
	}

	private static void OptimizeLongs(this Mono.Cecil.Cil.MethodBody self)
	{
		for (int i = 0; i < self.Instructions.Count; i++)
		{
			Instruction instruction = self.Instructions[i];
			if (instruction.OpCode.Code == Code.Ldc_I8)
			{
				long num = (long)instruction.Operand;
				if (num < int.MaxValue && num > int.MinValue)
				{
					ExpandMacro(instruction, OpCodes.Ldc_I4, (int)num);
					self.Instructions.Insert(++i, Instruction.Create(OpCodes.Conv_I8));
				}
			}
		}
	}

	public static void OptimizeMacros(this Mono.Cecil.Cil.MethodBody self)
	{
		if (self == null)
		{
			throw new ArgumentNullException("self");
		}
		MethodDefinition method = self.Method;
		foreach (Instruction instruction in self.Instructions)
		{
			switch (instruction.OpCode.Code)
			{
			case Code.Ldarg:
			{
				int index = ((ParameterDefinition)instruction.Operand).Index;
				if (index == -1 && instruction.Operand == self.ThisParameter)
				{
					index = 0;
				}
				else if (method.HasThis)
				{
					index++;
				}
				switch (index)
				{
				case 0:
					MakeMacro(instruction, OpCodes.Ldarg_0);
					break;
				case 1:
					MakeMacro(instruction, OpCodes.Ldarg_1);
					break;
				case 2:
					MakeMacro(instruction, OpCodes.Ldarg_2);
					break;
				case 3:
					MakeMacro(instruction, OpCodes.Ldarg_3);
					break;
				default:
					if (index < 256)
					{
						ExpandMacro(instruction, OpCodes.Ldarg_S, instruction.Operand);
					}
					break;
				}
				break;
			}
			case Code.Ldloc:
			{
				int index = ((VariableDefinition)instruction.Operand).Index;
				switch (index)
				{
				case 0:
					MakeMacro(instruction, OpCodes.Ldloc_0);
					break;
				case 1:
					MakeMacro(instruction, OpCodes.Ldloc_1);
					break;
				case 2:
					MakeMacro(instruction, OpCodes.Ldloc_2);
					break;
				case 3:
					MakeMacro(instruction, OpCodes.Ldloc_3);
					break;
				default:
					if (index < 256)
					{
						ExpandMacro(instruction, OpCodes.Ldloc_S, instruction.Operand);
					}
					break;
				}
				break;
			}
			case Code.Stloc:
			{
				int index = ((VariableDefinition)instruction.Operand).Index;
				switch (index)
				{
				case 0:
					MakeMacro(instruction, OpCodes.Stloc_0);
					break;
				case 1:
					MakeMacro(instruction, OpCodes.Stloc_1);
					break;
				case 2:
					MakeMacro(instruction, OpCodes.Stloc_2);
					break;
				case 3:
					MakeMacro(instruction, OpCodes.Stloc_3);
					break;
				default:
					if (index < 256)
					{
						ExpandMacro(instruction, OpCodes.Stloc_S, instruction.Operand);
					}
					break;
				}
				break;
			}
			case Code.Ldarga:
			{
				int index = ((ParameterDefinition)instruction.Operand).Index;
				if (index == -1 && instruction.Operand == self.ThisParameter)
				{
					index = 0;
				}
				else if (method.HasThis)
				{
					index++;
				}
				if (index < 256)
				{
					ExpandMacro(instruction, OpCodes.Ldarga_S, instruction.Operand);
				}
				break;
			}
			case Code.Ldloca:
				if (((VariableDefinition)instruction.Operand).Index < 256)
				{
					ExpandMacro(instruction, OpCodes.Ldloca_S, instruction.Operand);
				}
				break;
			case Code.Ldc_I4:
			{
				int num = (int)instruction.Operand;
				switch (num)
				{
				case -1:
					MakeMacro(instruction, OpCodes.Ldc_I4_M1);
					break;
				case 0:
					MakeMacro(instruction, OpCodes.Ldc_I4_0);
					break;
				case 1:
					MakeMacro(instruction, OpCodes.Ldc_I4_1);
					break;
				case 2:
					MakeMacro(instruction, OpCodes.Ldc_I4_2);
					break;
				case 3:
					MakeMacro(instruction, OpCodes.Ldc_I4_3);
					break;
				case 4:
					MakeMacro(instruction, OpCodes.Ldc_I4_4);
					break;
				case 5:
					MakeMacro(instruction, OpCodes.Ldc_I4_5);
					break;
				case 6:
					MakeMacro(instruction, OpCodes.Ldc_I4_6);
					break;
				case 7:
					MakeMacro(instruction, OpCodes.Ldc_I4_7);
					break;
				case 8:
					MakeMacro(instruction, OpCodes.Ldc_I4_8);
					break;
				default:
					if (num >= -128 && num < 128)
					{
						ExpandMacro(instruction, OpCodes.Ldc_I4_S, (sbyte)num);
					}
					break;
				}
				break;
			}
			}
		}
		OptimizeBranches(self);
	}

	private static void OptimizeBranches(Mono.Cecil.Cil.MethodBody body)
	{
		ComputeOffsets(body);
		foreach (Instruction instruction in body.Instructions)
		{
			if (instruction.OpCode.OperandType == OperandType.InlineBrTarget && OptimizeBranch(instruction))
			{
				ComputeOffsets(body);
			}
		}
	}

	private static bool OptimizeBranch(Instruction instruction)
	{
		int num = ((Instruction)instruction.Operand).Offset - (instruction.Offset + instruction.OpCode.Size + 4);
		if (num < -128 || num > 127)
		{
			return false;
		}
		switch (instruction.OpCode.Code)
		{
		case Code.Br:
			instruction.OpCode = OpCodes.Br_S;
			break;
		case Code.Brfalse:
			instruction.OpCode = OpCodes.Brfalse_S;
			break;
		case Code.Brtrue:
			instruction.OpCode = OpCodes.Brtrue_S;
			break;
		case Code.Beq:
			instruction.OpCode = OpCodes.Beq_S;
			break;
		case Code.Bge:
			instruction.OpCode = OpCodes.Bge_S;
			break;
		case Code.Bgt:
			instruction.OpCode = OpCodes.Bgt_S;
			break;
		case Code.Ble:
			instruction.OpCode = OpCodes.Ble_S;
			break;
		case Code.Blt:
			instruction.OpCode = OpCodes.Blt_S;
			break;
		case Code.Bne_Un:
			instruction.OpCode = OpCodes.Bne_Un_S;
			break;
		case Code.Bge_Un:
			instruction.OpCode = OpCodes.Bge_Un_S;
			break;
		case Code.Bgt_Un:
			instruction.OpCode = OpCodes.Bgt_Un_S;
			break;
		case Code.Ble_Un:
			instruction.OpCode = OpCodes.Ble_Un_S;
			break;
		case Code.Blt_Un:
			instruction.OpCode = OpCodes.Blt_Un_S;
			break;
		case Code.Leave:
			instruction.OpCode = OpCodes.Leave_S;
			break;
		}
		return true;
	}

	private static void ComputeOffsets(Mono.Cecil.Cil.MethodBody body)
	{
		int num = 0;
		foreach (Instruction instruction in body.Instructions)
		{
			instruction.Offset = num;
			num += instruction.GetSize();
		}
	}
}
public static class MethodDefinitionRocks
{
	public static MethodDefinition GetBaseMethod(this MethodDefinition self)
	{
		if (self == null)
		{
			throw new ArgumentNullException("self");
		}
		if (!self.IsVirtual)
		{
			return self;
		}
		if (self.IsNewSlot)
		{
			return self;
		}
		for (TypeDefinition typeDefinition = ResolveBaseType(self.DeclaringType); typeDefinition != null; typeDefinition = ResolveBaseType(typeDefinition))
		{
			MethodDefinition matchingMethod = GetMatchingMethod(typeDefinition, self);
			if (matchingMethod != null)
			{
				return matchingMethod;
			}
		}
		return self;
	}

	public static MethodDefinition GetOriginalBaseMethod(this MethodDefinition self)
	{
		if (self == null)
		{
			throw new ArgumentNullException("self");
		}
		while (true)
		{
			MethodDefinition baseMethod = self.GetBaseMethod();
			if (baseMethod == self)
			{
				break;
			}
			self = baseMethod;
		}
		return self;
	}

	private static TypeDefinition ResolveBaseType(TypeDefinition type)
	{
		return type?.BaseType?.Resolve();
	}

	private static MethodDefinition GetMatchingMethod(TypeDefinition type, MethodDefinition method)
	{
		return MetadataResolver.GetMethod(type.Methods, method);
	}
}
public static class ModuleDefinitionRocks
{
	public static IEnumerable<TypeDefinition> GetAllTypes(this ModuleDefinition self)
	{
		if (self == null)
		{
			throw new ArgumentNullException("self");
		}
		return self.Types.SelectMany(Functional.Y((Func<TypeDefinition, IEnumerable<TypeDefinition>> f) => (TypeDefinition type) => type.NestedTypes.SelectMany(f).Prepend(type)));
	}
}
public static class ParameterReferenceRocks
{
	public static int GetSequence(this ParameterReference self)
	{
		return self.Index + 1;
	}
}
public static class SecurityDeclarationRocks
{
	public static PermissionSet ToPermissionSet(this SecurityDeclaration self)
	{
		if (self == null)
		{
			throw new ArgumentNullException("self");
		}
		if (TryProcessPermissionSetAttribute(self, out var set))
		{
			return set;
		}
		return CreatePermissionSet(self);
	}

	private static bool TryProcessPermissionSetAttribute(SecurityDeclaration declaration, out PermissionSet set)
	{
		set = null;
		if (!declaration.HasSecurityAttributes && declaration.SecurityAttributes.Count != 1)
		{
			return false;
		}
		SecurityAttribute securityAttribute = declaration.SecurityAttributes[0];
		if (!securityAttribute.AttributeType.IsTypeOf("System.Security.Permissions", "PermissionSetAttribute"))
		{
			return false;
		}
		PermissionSetAttribute permissionSetAttribute = new PermissionSetAttribute((System.Security.Permissions.SecurityAction)declaration.Action);
		CustomAttributeNamedArgument customAttributeNamedArgument = securityAttribute.Properties[0];
		string text = (string)customAttributeNamedArgument.Argument.Value;
		string name = customAttributeNamedArgument.Name;
		string text2 = name;
		if (!(text2 == "XML"))
		{
			if (!(text2 == "Name"))
			{
				throw new NotImplementedException(customAttributeNamedArgument.Name);
			}
			permissionSetAttribute.Name = text;
		}
		else
		{
			permissionSetAttribute.XML = text;
		}
		set = permissionSetAttribute.CreatePermissionSet();
		return true;
	}

	private static PermissionSet CreatePermissionSet(SecurityDeclaration declaration)
	{
		PermissionSet permissionSet = new PermissionSet(PermissionState.None);
		foreach (SecurityAttribute securityAttribute in declaration.SecurityAttributes)
		{
			IPermission perm = CreatePermission(declaration, securityAttribute);
			permissionSet.AddPermission(perm);
		}
		return permissionSet;
	}

	private static IPermission CreatePermission(SecurityDeclaration declaration, SecurityAttribute attribute)
	{
		Type type = Type.GetType(attribute.AttributeType.FullName);
		if (type == null)
		{
			throw new ArgumentException("attribute");
		}
		System.Security.Permissions.SecurityAttribute securityAttribute = CreateSecurityAttribute(type, declaration);
		if (securityAttribute == null)
		{
			throw new InvalidOperationException();
		}
		CompleteSecurityAttribute(securityAttribute, attribute);
		return securityAttribute.CreatePermission();
	}

	private static void CompleteSecurityAttribute(System.Security.Permissions.SecurityAttribute security_attribute, SecurityAttribute attribute)
	{
		if (attribute.HasFields)
		{
			CompleteSecurityAttributeFields(security_attribute, attribute);
		}
		if (attribute.HasProperties)
		{
			CompleteSecurityAttributeProperties(security_attribute, attribute);
		}
	}

	private static void CompleteSecurityAttributeFields(System.Security.Permissions.SecurityAttribute security_attribute, SecurityAttribute attribute)
	{
		Type type = security_attribute.GetType();
		foreach (CustomAttributeNamedArgument field in attribute.Fields)
		{
			type.GetField(field.Name).SetValue(security_attribute, field.Argument.Value);
		}
	}

	private static void CompleteSecurityAttributeProperties(System.Security.Permissions.SecurityAttribute security_attribute, SecurityAttribute attribute)
	{
		Type type = security_attribute.GetType();
		foreach (CustomAttributeNamedArgument property in attribute.Properties)
		{
			type.GetProperty(property.Name).SetValue(security_attribute, property.Argument.Value, null);
		}
	}

	private static System.Security.Permissions.SecurityAttribute CreateSecurityAttribute(Type attribute_type, SecurityDeclaration declaration)
	{
		try
		{
			return (System.Security.Permissions.SecurityAttribute)Activator.CreateInstance(attribute_type, (System.Security.Permissions.SecurityAction)declaration.Action);
		}
		catch (MissingMethodException)
		{
			return (System.Security.Permissions.SecurityAttribute)Activator.CreateInstance(attribute_type, new object[0]);
		}
	}

	public static SecurityDeclaration ToSecurityDeclaration(this PermissionSet self, SecurityAction action, ModuleDefinition module)
	{
		if (self == null)
		{
			throw new ArgumentNullException("self");
		}
		if (module == null)
		{
			throw new ArgumentNullException("module");
		}
		SecurityDeclaration securityDeclaration = new SecurityDeclaration(action);
		SecurityAttribute securityAttribute = new SecurityAttribute(module.TypeSystem.LookupType("System.Security.Permissions", "PermissionSetAttribute"));
		securityAttribute.Properties.Add(new CustomAttributeNamedArgument("XML", new CustomAttributeArgument(module.TypeSystem.String, self.ToXml().ToString())));
		securityDeclaration.SecurityAttributes.Add(securityAttribute);
		return securityDeclaration;
	}
}
public static class TypeDefinitionRocks
{
	public static IEnumerable<MethodDefinition> GetConstructors(this TypeDefinition self)
	{
		if (self == null)
		{
			throw new ArgumentNullException("self");
		}
		if (!self.HasMethods)
		{
			return Empty<MethodDefinition>.Array;
		}
		return self.Methods.Where((MethodDefinition method) => method.IsConstructor);
	}

	public static MethodDefinition GetStaticConstructor(this TypeDefinition self)
	{
		if (self == null)
		{
			throw new ArgumentNullException("self");
		}
		if (!self.HasMethods)
		{
			return null;
		}
		return self.GetConstructors().FirstOrDefault((MethodDefinition ctor) => ctor.IsStatic);
	}

	public static IEnumerable<MethodDefinition> GetMethods(this TypeDefinition self)
	{
		if (self == null)
		{
			throw new ArgumentNullException("self");
		}
		if (!self.HasMethods)
		{
			return Empty<MethodDefinition>.Array;
		}
		return self.Methods.Where((MethodDefinition method) => !method.IsConstructor);
	}

	public static TypeReference GetEnumUnderlyingType(this TypeDefinition self)
	{
		if (self == null)
		{
			throw new ArgumentNullException("self");
		}
		if (!self.IsEnum)
		{
			throw new ArgumentException();
		}
		return Mixin.GetEnumUnderlyingType(self);
	}
}
public static class TypeReferenceRocks
{
	public static ArrayType MakeArrayType(this TypeReference self)
	{
		return new ArrayType(self);
	}

	public static ArrayType MakeArrayType(this TypeReference self, int rank)
	{
		if (rank == 0)
		{
			throw new ArgumentOutOfRangeException("rank");
		}
		ArrayType arrayType = new ArrayType(self);
		for (int i = 1; i < rank; i++)
		{
			arrayType.Dimensions.Add(default(ArrayDimension));
		}
		return arrayType;
	}

	public static PointerType MakePointerType(this TypeReference self)
	{
		return new PointerType(self);
	}

	public static ByReferenceType MakeByReferenceType(this TypeReference self)
	{
		return new ByReferenceType(self);
	}

	public static OptionalModifierType MakeOptionalModifierType(this TypeReference self, TypeReference modifierType)
	{
		return new OptionalModifierType(modifierType, self);
	}

	public static RequiredModifierType MakeRequiredModifierType(this TypeReference self, TypeReference modifierType)
	{
		return new RequiredModifierType(modifierType, self);
	}

	public static GenericInstanceType MakeGenericInstanceType(this TypeReference self, params TypeReference[] arguments)
	{
		if (self == null)
		{
			throw new ArgumentNullException("self");
		}
		if (arguments == null)
		{
			throw new ArgumentNullException("arguments");
		}
		if (arguments.Length == 0)
		{
			throw new ArgumentException();
		}
		if (self.GenericParameters.Count != arguments.Length)
		{
			throw new ArgumentException();
		}
		GenericInstanceType genericInstanceType = new GenericInstanceType(self, arguments.Length);
		foreach (TypeReference item in arguments)
		{
			genericInstanceType.GenericArguments.Add(item);
		}
		return genericInstanceType;
	}

	public static PinnedType MakePinnedType(this TypeReference self)
	{
		return new PinnedType(self);
	}

	public static SentinelType MakeSentinelType(this TypeReference self)
	{
		return new SentinelType(self);
	}
}
