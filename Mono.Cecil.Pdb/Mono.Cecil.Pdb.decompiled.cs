using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Cci.Pdb;
using Mono.Cecil.Cil;
using Mono.Cecil.PE;
using Mono.Collections.Generic;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: AssemblyProduct("Mono.Cecil")]
[assembly: AssemblyCopyright("Copyright © 2008 - 2018 Jb Evain")]
[assembly: ComVisible(false)]
[assembly: AssemblyFileVersion("0.11.5.0")]
[assembly: AssemblyInformationalVersion("0.11.5.0")]
[assembly: AssemblyTitle("Mono.Cecil.Pdb")]
[assembly: CLSCompliant(false)]
[assembly: TargetFramework(".NETFramework,Version=v4.0", FrameworkDisplayName = ".NET Framework 4")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.11.5.0")]
[module: UnverifiableCode]
namespace Mono.Cecil.Pdb
{
	[ComImport]
	[Guid("B01FAFEB-C450-3A4D-BEEC-B4CEEC01E006")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface ISymUnmanagedDocumentWriter
	{
		void SetSource(uint sourceSize, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] byte[] source);

		void SetCheckSum(Guid algorithmId, uint checkSumSize, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] checkSum);
	}
	[ComImport]
	[Guid("0B97726E-9E6D-4f05-9A26-424022093CAA")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface ISymUnmanagedWriter2
	{
		void DefineDocument([In][MarshalAs(UnmanagedType.LPWStr)] string url, [In] ref Guid langauge, [In] ref Guid languageVendor, [In] ref Guid documentType, [MarshalAs(UnmanagedType.Interface)] out ISymUnmanagedDocumentWriter pRetVal);

		void SetUserEntryPoint([In] int methodToken);

		void OpenMethod([In] int methodToken);

		void CloseMethod();

		void OpenScope([In] int startOffset, out int pRetVal);

		void CloseScope([In] int endOffset);

		void SetScopeRange_Placeholder();

		void DefineLocalVariable_Placeholder();

		void DefineParameter_Placeholder();

		void DefineField_Placeholder();

		void DefineGlobalVariable_Placeholder();

		void Close();

		void SetSymAttribute(uint parent, string name, uint data, IntPtr signature);

		void OpenNamespace([In][MarshalAs(UnmanagedType.LPWStr)] string name);

		void CloseNamespace();

		void UsingNamespace([In][MarshalAs(UnmanagedType.LPWStr)] string fullName);

		void SetMethodSourceRange_Placeholder();

		void Initialize([In][MarshalAs(UnmanagedType.IUnknown)] object emitter, [In][MarshalAs(UnmanagedType.LPWStr)] string filename, [In] IStream pIStream, [In] bool fFullBuild);

		void GetDebugInfo(out ImageDebugDirectory pIDD, [In] int cData, out int pcData, [In][Out][MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] data);

		void DefineSequencePoints([In][MarshalAs(UnmanagedType.Interface)] ISymUnmanagedDocumentWriter document, [In] int spCount, [In][MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] offsets, [In][MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] lines, [In][MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] columns, [In][MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] endLines, [In][MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] endColumns);

		void RemapToken_Placeholder();

		void Initialize2_Placeholder();

		void DefineConstant_Placeholder();

		void Abort_Placeholder();

		void DefineLocalVariable2([In][MarshalAs(UnmanagedType.LPWStr)] string name, [In] int attributes, [In] int sigToken, [In] int addrKind, [In] int addr1, [In] int addr2, [In] int addr3, [In] int startOffset, [In] int endOffset);

		void DefineGlobalVariable2_Placeholder();

		void DefineConstant2([In][MarshalAs(UnmanagedType.LPWStr)] string name, [In][MarshalAs(UnmanagedType.Struct)] object variant, [In] int sigToken);
	}
	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("BA3FEE4C-ECB9-4e41-83B7-183FA41CD859")]
	internal interface IMetaDataEmit
	{
		void SetModuleProps(string szName);

		void Save(string szFile, uint dwSaveFlags);

		void SaveToStream(IntPtr pIStream, uint dwSaveFlags);

		uint GetSaveSize(uint fSave);

		uint DefineTypeDef(IntPtr szTypeDef, uint dwTypeDefFlags, uint tkExtends, IntPtr rtkImplements);

		uint DefineNestedType(IntPtr szTypeDef, uint dwTypeDefFlags, uint tkExtends, IntPtr rtkImplements, uint tdEncloser);

		void SetHandler([In][MarshalAs(UnmanagedType.IUnknown)] object pUnk);

		uint DefineMethod(uint td, IntPtr zName, uint dwMethodFlags, IntPtr pvSigBlob, uint cbSigBlob, uint ulCodeRVA, uint dwImplFlags);

		void DefineMethodImpl(uint td, uint tkBody, uint tkDecl);

		uint DefineTypeRefByName(uint tkResolutionScope, IntPtr szName);

		uint DefineImportType(IntPtr pAssemImport, IntPtr pbHashValue, uint cbHashValue, IMetaDataImport pImport, uint tdImport, IntPtr pAssemEmit);

		uint DefineMemberRef(uint tkImport, string szName, IntPtr pvSigBlob, uint cbSigBlob);

		uint DefineImportMember(IntPtr pAssemImport, IntPtr pbHashValue, uint cbHashValue, IMetaDataImport pImport, uint mbMember, IntPtr pAssemEmit, uint tkParent);

		uint DefineEvent(uint td, string szEvent, uint dwEventFlags, uint tkEventType, uint mdAddOn, uint mdRemoveOn, uint mdFire, IntPtr rmdOtherMethods);

		void SetClassLayout(uint td, uint dwPackSize, IntPtr rFieldOffsets, uint ulClassSize);

		void DeleteClassLayout(uint td);

		void SetFieldMarshal(uint tk, IntPtr pvNativeType, uint cbNativeType);

		void DeleteFieldMarshal(uint tk);

		uint DefinePermissionSet(uint tk, uint dwAction, IntPtr pvPermission, uint cbPermission);

		void SetRVA(uint md, uint ulRVA);

		uint GetTokenFromSig(IntPtr pvSig, uint cbSig);

		uint DefineModuleRef(string szName);

		void SetParent(uint mr, uint tk);

		uint GetTokenFromTypeSpec(IntPtr pvSig, uint cbSig);

		void SaveToMemory(IntPtr pbData, uint cbData);

		uint DefineUserString(string szString, uint cchString);

		void DeleteToken(uint tkObj);

		void SetMethodProps(uint md, uint dwMethodFlags, uint ulCodeRVA, uint dwImplFlags);

		void SetTypeDefProps(uint td, uint dwTypeDefFlags, uint tkExtends, IntPtr rtkImplements);

		void SetEventProps(uint ev, uint dwEventFlags, uint tkEventType, uint mdAddOn, uint mdRemoveOn, uint mdFire, IntPtr rmdOtherMethods);

		uint SetPermissionSetProps(uint tk, uint dwAction, IntPtr pvPermission, uint cbPermission);

		void DefinePinvokeMap(uint tk, uint dwMappingFlags, string szImportName, uint mrImportDLL);

		void SetPinvokeMap(uint tk, uint dwMappingFlags, string szImportName, uint mrImportDLL);

		void DeletePinvokeMap(uint tk);

		uint DefineCustomAttribute(uint tkObj, uint tkType, IntPtr pCustomAttribute, uint cbCustomAttribute);

		void SetCustomAttributeValue(uint pcv, IntPtr pCustomAttribute, uint cbCustomAttribute);

		uint DefineField(uint td, string szName, uint dwFieldFlags, IntPtr pvSigBlob, uint cbSigBlob, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue);

		uint DefineProperty(uint td, string szProperty, uint dwPropFlags, IntPtr pvSig, uint cbSig, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue, uint mdSetter, uint mdGetter, IntPtr rmdOtherMethods);

		uint DefineParam(uint md, uint ulParamSeq, string szName, uint dwParamFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue);

		void SetFieldProps(uint fd, uint dwFieldFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue);

		void SetPropertyProps(uint pr, uint dwPropFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue, uint mdSetter, uint mdGetter, IntPtr rmdOtherMethods);

		void SetParamProps(uint pd, string szName, uint dwParamFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue);

		uint DefineSecurityAttributeSet(uint tkObj, IntPtr rSecAttrs, uint cSecAttrs);

		void ApplyEditAndContinue([MarshalAs(UnmanagedType.IUnknown)] object pImport);

		uint TranslateSigWithScope(IntPtr pAssemImport, IntPtr pbHashValue, uint cbHashValue, IMetaDataImport import, IntPtr pbSigBlob, uint cbSigBlob, IntPtr pAssemEmit, IMetaDataEmit emit, IntPtr pvTranslatedSig, uint cbTranslatedSigMax);

		void SetMethodImplFlags(uint md, uint dwImplFlags);

		void SetFieldRVA(uint fd, uint ulRVA);

		void Merge(IMetaDataImport pImport, IntPtr pHostMapToken, [MarshalAs(UnmanagedType.IUnknown)] object pHandler);

		void MergeEnd();
	}
	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("7DAC8207-D3AE-4c75-9B67-92801A497D44")]
	internal interface IMetaDataImport
	{
		[PreserveSig]
		void CloseEnum(uint hEnum);

		uint CountEnum(uint hEnum);

		void ResetEnum(uint hEnum, uint ulPos);

		uint EnumTypeDefs(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] rTypeDefs, uint cMax);

		uint EnumInterfaceImpls(ref uint phEnum, uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] uint[] rImpls, uint cMax);

		uint EnumTypeRefs(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] rTypeRefs, uint cMax);

		uint FindTypeDefByName(string szTypeDef, uint tkEnclosingClass);

		Guid GetScopeProps(StringBuilder szName, uint cchName, out uint pchName);

		uint GetModuleFromScope();

		[PreserveSig]
		unsafe uint GetTypeDefProps(uint td, char* szTypeDef, uint cchTypeDef, uint* pchTypeDef, uint* pdwTypeDefFlags, uint* ptkExtends);

		uint GetInterfaceImplProps(uint iiImpl, out uint pClass);

		uint GetTypeRefProps(uint tr, out uint ptkResolutionScope, StringBuilder szName, uint cchName);

		uint ResolveTypeRef(uint tr, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppIScope);

		uint EnumMembers(ref uint phEnum, uint cl, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] uint[] rMembers, uint cMax);

		uint EnumMembersWithName(ref uint phEnum, uint cl, string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] rMembers, uint cMax);

		uint EnumMethods(ref uint phEnum, uint cl, IntPtr rMethods, uint cMax);

		uint EnumMethodsWithName(ref uint phEnum, uint cl, string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] rMethods, uint cMax);

		uint EnumFields(ref uint phEnum, uint cl, IntPtr rFields, uint cMax);

		uint EnumFieldsWithName(ref uint phEnum, uint cl, string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] rFields, uint cMax);

		uint EnumParams(ref uint phEnum, uint mb, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] uint[] rParams, uint cMax);

		uint EnumMemberRefs(ref uint phEnum, uint tkParent, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] uint[] rMemberRefs, uint cMax);

		uint EnumMethodImpls(ref uint phEnum, uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] rMethodBody, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] rMethodDecl, uint cMax);

		uint EnumPermissionSets(ref uint phEnum, uint tk, uint dwActions, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] rPermission, uint cMax);

		uint FindMember(uint td, string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] pvSigBlob, uint cbSigBlob);

		uint FindMethod(uint td, string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] pvSigBlob, uint cbSigBlob);

		uint FindField(uint td, string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] pvSigBlob, uint cbSigBlob);

		uint FindMemberRef(uint td, string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] pvSigBlob, uint cbSigBlob);

		[PreserveSig]
		unsafe uint GetMethodProps(uint mb, uint* pClass, char* szMethod, uint cchMethod, uint* pchMethod, uint* pdwAttr, IntPtr ppvSigBlob, IntPtr pcbSigBlob, uint* pulCodeRVA, uint* pdwImplFlags);

		uint GetMemberRefProps(uint mr, ref uint ptk, StringBuilder szMember, uint cchMember, out uint pchMember, out IntPtr ppvSigBlob);

		uint EnumProperties(ref uint phEnum, uint td, IntPtr rProperties, uint cMax);

		uint EnumEvents(ref uint phEnum, uint td, IntPtr rEvents, uint cMax);

		uint GetEventProps(uint ev, out uint pClass, StringBuilder szEvent, uint cchEvent, out uint pchEvent, out uint pdwEventFlags, out uint ptkEventType, out uint pmdAddOn, out uint pmdRemoveOn, out uint pmdFire, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 11)] uint[] rmdOtherMethod, uint cMax);

		uint EnumMethodSemantics(ref uint phEnum, uint mb, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] uint[] rEventProp, uint cMax);

		uint GetMethodSemantics(uint mb, uint tkEventProp);

		uint GetClassLayout(uint td, out uint pdwPackSize, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] IntPtr rFieldOffset, uint cMax, out uint pcFieldOffset);

		uint GetFieldMarshal(uint tk, out IntPtr ppvNativeType);

		uint GetRVA(uint tk, out uint pulCodeRVA);

		uint GetPermissionSetProps(uint pm, out uint pdwAction, out IntPtr ppvPermission);

		uint GetSigFromToken(uint mdSig, out IntPtr ppvSig);

		uint GetModuleRefProps(uint mur, StringBuilder szName, uint cchName);

		uint EnumModuleRefs(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] rModuleRefs, uint cmax);

		uint GetTypeSpecFromToken(uint typespec, out IntPtr ppvSig);

		uint GetNameFromToken(uint tk);

		uint EnumUnresolvedMethods(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] rMethods, uint cMax);

		uint GetUserString(uint stk, StringBuilder szString, uint cchString);

		uint GetPinvokeMap(uint tk, out uint pdwMappingFlags, StringBuilder szImportName, uint cchImportName, out uint pchImportName);

		uint EnumSignatures(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] rSignatures, uint cmax);

		uint EnumTypeSpecs(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] rTypeSpecs, uint cmax);

		uint EnumUserStrings(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] rStrings, uint cmax);

		[PreserveSig]
		int GetParamForMethodIndex(uint md, uint ulParamSeq, out uint pParam);

		uint EnumCustomAttributes(ref uint phEnum, uint tk, uint tkType, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] rCustomAttributes, uint cMax);

		uint GetCustomAttributeProps(uint cv, out uint ptkObj, out uint ptkType, out IntPtr ppBlob);

		uint FindTypeRef(uint tkResolutionScope, string szName);

		uint GetMemberProps(uint mb, out uint pClass, StringBuilder szMember, uint cchMember, out uint pchMember, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pulCodeRVA, out uint pdwImplFlags, out uint pdwCPlusTypeFlag, out IntPtr ppValue);

		uint GetFieldProps(uint mb, out uint pClass, StringBuilder szField, uint cchField, out uint pchField, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pdwCPlusTypeFlag, out IntPtr ppValue);

		uint GetPropertyProps(uint prop, out uint pClass, StringBuilder szProperty, uint cchProperty, out uint pchProperty, out uint pdwPropFlags, out IntPtr ppvSig, out uint pbSig, out uint pdwCPlusTypeFlag, out IntPtr ppDefaultValue, out uint pcchDefaultValue, out uint pmdSetter, out uint pmdGetter, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 14)] uint[] rmdOtherMethod, uint cMax);

		uint GetParamProps(uint tk, out uint pmd, out uint pulSequence, StringBuilder szName, uint cchName, out uint pchName, out uint pdwAttr, out uint pdwCPlusTypeFlag, out IntPtr ppValue);

		uint GetCustomAttributeByName(uint tkObj, string szName, out IntPtr ppData);

		[PreserveSig]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsValidToken(uint tk);

		[PreserveSig]
		unsafe uint GetNestedClassProps(uint tdNestedClass, uint* ptdEnclosingClass);

		uint GetNativeCallConvFromSig(IntPtr pvSig, uint cbSig);

		int IsGlobal(uint pd);
	}
	internal class ModuleMetadata : IMetaDataEmit, IMetaDataImport
	{
		private readonly ModuleDefinition module;

		private Dictionary<uint, TypeDefinition> types;

		private Dictionary<uint, MethodDefinition> methods;

		private const uint S_OK = 0u;

		private const uint E_FAIL = 2147500037u;

		public ModuleMetadata(ModuleDefinition module)
		{
			this.module = module;
		}

		private bool TryGetType(uint token, out TypeDefinition type)
		{
			if (types == null)
			{
				InitializeMetadata(module);
			}
			return types.TryGetValue(token, out type);
		}

		private bool TryGetMethod(uint token, out MethodDefinition method)
		{
			if (methods == null)
			{
				InitializeMetadata(module);
			}
			return methods.TryGetValue(token, out method);
		}

		private void InitializeMetadata(ModuleDefinition module)
		{
			types = new Dictionary<uint, TypeDefinition>();
			methods = new Dictionary<uint, MethodDefinition>();
			foreach (TypeDefinition type in module.GetTypes())
			{
				types.Add(type.MetadataToken.ToUInt32(), type);
				InitializeMethods(type);
			}
		}

		private void InitializeMethods(TypeDefinition type)
		{
			foreach (MethodDefinition method in type.Methods)
			{
				methods.Add(method.MetadataToken.ToUInt32(), method);
			}
		}

		public void SetModuleProps(string szName)
		{
			throw new NotImplementedException();
		}

		public void Save(string szFile, uint dwSaveFlags)
		{
			throw new NotImplementedException();
		}

		public void SaveToStream(IntPtr pIStream, uint dwSaveFlags)
		{
			throw new NotImplementedException();
		}

		public uint GetSaveSize(uint fSave)
		{
			throw new NotImplementedException();
		}

		public uint DefineTypeDef(IntPtr szTypeDef, uint dwTypeDefFlags, uint tkExtends, IntPtr rtkImplements)
		{
			throw new NotImplementedException();
		}

		public uint DefineNestedType(IntPtr szTypeDef, uint dwTypeDefFlags, uint tkExtends, IntPtr rtkImplements, uint tdEncloser)
		{
			throw new NotImplementedException();
		}

		public void SetHandler(object pUnk)
		{
			throw new NotImplementedException();
		}

		public uint DefineMethod(uint td, IntPtr zName, uint dwMethodFlags, IntPtr pvSigBlob, uint cbSigBlob, uint ulCodeRVA, uint dwImplFlags)
		{
			throw new NotImplementedException();
		}

		public void DefineMethodImpl(uint td, uint tkBody, uint tkDecl)
		{
			throw new NotImplementedException();
		}

		public uint DefineTypeRefByName(uint tkResolutionScope, IntPtr szName)
		{
			throw new NotImplementedException();
		}

		public uint DefineImportType(IntPtr pAssemImport, IntPtr pbHashValue, uint cbHashValue, IMetaDataImport pImport, uint tdImport, IntPtr pAssemEmit)
		{
			throw new NotImplementedException();
		}

		public uint DefineMemberRef(uint tkImport, string szName, IntPtr pvSigBlob, uint cbSigBlob)
		{
			throw new NotImplementedException();
		}

		public uint DefineImportMember(IntPtr pAssemImport, IntPtr pbHashValue, uint cbHashValue, IMetaDataImport pImport, uint mbMember, IntPtr pAssemEmit, uint tkParent)
		{
			throw new NotImplementedException();
		}

		public uint DefineEvent(uint td, string szEvent, uint dwEventFlags, uint tkEventType, uint mdAddOn, uint mdRemoveOn, uint mdFire, IntPtr rmdOtherMethods)
		{
			throw new NotImplementedException();
		}

		public void SetClassLayout(uint td, uint dwPackSize, IntPtr rFieldOffsets, uint ulClassSize)
		{
			throw new NotImplementedException();
		}

		public void DeleteClassLayout(uint td)
		{
			throw new NotImplementedException();
		}

		public void SetFieldMarshal(uint tk, IntPtr pvNativeType, uint cbNativeType)
		{
			throw new NotImplementedException();
		}

		public void DeleteFieldMarshal(uint tk)
		{
			throw new NotImplementedException();
		}

		public uint DefinePermissionSet(uint tk, uint dwAction, IntPtr pvPermission, uint cbPermission)
		{
			throw new NotImplementedException();
		}

		public void SetRVA(uint md, uint ulRVA)
		{
			throw new NotImplementedException();
		}

		public uint GetTokenFromSig(IntPtr pvSig, uint cbSig)
		{
			throw new NotImplementedException();
		}

		public uint DefineModuleRef(string szName)
		{
			throw new NotImplementedException();
		}

		public void SetParent(uint mr, uint tk)
		{
			throw new NotImplementedException();
		}

		public uint GetTokenFromTypeSpec(IntPtr pvSig, uint cbSig)
		{
			throw new NotImplementedException();
		}

		public void SaveToMemory(IntPtr pbData, uint cbData)
		{
			throw new NotImplementedException();
		}

		public uint DefineUserString(string szString, uint cchString)
		{
			throw new NotImplementedException();
		}

		public void DeleteToken(uint tkObj)
		{
			throw new NotImplementedException();
		}

		public void SetMethodProps(uint md, uint dwMethodFlags, uint ulCodeRVA, uint dwImplFlags)
		{
			throw new NotImplementedException();
		}

		public void SetTypeDefProps(uint td, uint dwTypeDefFlags, uint tkExtends, IntPtr rtkImplements)
		{
			throw new NotImplementedException();
		}

		public void SetEventProps(uint ev, uint dwEventFlags, uint tkEventType, uint mdAddOn, uint mdRemoveOn, uint mdFire, IntPtr rmdOtherMethods)
		{
			throw new NotImplementedException();
		}

		public uint SetPermissionSetProps(uint tk, uint dwAction, IntPtr pvPermission, uint cbPermission)
		{
			throw new NotImplementedException();
		}

		public void DefinePinvokeMap(uint tk, uint dwMappingFlags, string szImportName, uint mrImportDLL)
		{
			throw new NotImplementedException();
		}

		public void SetPinvokeMap(uint tk, uint dwMappingFlags, string szImportName, uint mrImportDLL)
		{
			throw new NotImplementedException();
		}

		public void DeletePinvokeMap(uint tk)
		{
			throw new NotImplementedException();
		}

		public uint DefineCustomAttribute(uint tkObj, uint tkType, IntPtr pCustomAttribute, uint cbCustomAttribute)
		{
			throw new NotImplementedException();
		}

		public void SetCustomAttributeValue(uint pcv, IntPtr pCustomAttribute, uint cbCustomAttribute)
		{
			throw new NotImplementedException();
		}

		public uint DefineField(uint td, string szName, uint dwFieldFlags, IntPtr pvSigBlob, uint cbSigBlob, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue)
		{
			throw new NotImplementedException();
		}

		public uint DefineProperty(uint td, string szProperty, uint dwPropFlags, IntPtr pvSig, uint cbSig, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue, uint mdSetter, uint mdGetter, IntPtr rmdOtherMethods)
		{
			throw new NotImplementedException();
		}

		public uint DefineParam(uint md, uint ulParamSeq, string szName, uint dwParamFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue)
		{
			throw new NotImplementedException();
		}

		public void SetFieldProps(uint fd, uint dwFieldFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue)
		{
			throw new NotImplementedException();
		}

		public void SetPropertyProps(uint pr, uint dwPropFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue, uint mdSetter, uint mdGetter, IntPtr rmdOtherMethods)
		{
			throw new NotImplementedException();
		}

		public void SetParamProps(uint pd, string szName, uint dwParamFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue)
		{
			throw new NotImplementedException();
		}

		public uint DefineSecurityAttributeSet(uint tkObj, IntPtr rSecAttrs, uint cSecAttrs)
		{
			throw new NotImplementedException();
		}

		public void ApplyEditAndContinue(object pImport)
		{
			throw new NotImplementedException();
		}

		public uint TranslateSigWithScope(IntPtr pAssemImport, IntPtr pbHashValue, uint cbHashValue, IMetaDataImport import, IntPtr pbSigBlob, uint cbSigBlob, IntPtr pAssemEmit, IMetaDataEmit emit, IntPtr pvTranslatedSig, uint cbTranslatedSigMax)
		{
			throw new NotImplementedException();
		}

		public void SetMethodImplFlags(uint md, uint dwImplFlags)
		{
			throw new NotImplementedException();
		}

		public void SetFieldRVA(uint fd, uint ulRVA)
		{
			throw new NotImplementedException();
		}

		public void Merge(IMetaDataImport pImport, IntPtr pHostMapToken, object pHandler)
		{
			throw new NotImplementedException();
		}

		public void MergeEnd()
		{
			throw new NotImplementedException();
		}

		public void CloseEnum(uint hEnum)
		{
			throw new NotImplementedException();
		}

		public uint CountEnum(uint hEnum)
		{
			throw new NotImplementedException();
		}

		public void ResetEnum(uint hEnum, uint ulPos)
		{
			throw new NotImplementedException();
		}

		public uint EnumTypeDefs(ref uint phEnum, uint[] rTypeDefs, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint EnumInterfaceImpls(ref uint phEnum, uint td, uint[] rImpls, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint EnumTypeRefs(ref uint phEnum, uint[] rTypeRefs, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint FindTypeDefByName(string szTypeDef, uint tkEnclosingClass)
		{
			throw new NotImplementedException();
		}

		public Guid GetScopeProps(StringBuilder szName, uint cchName, out uint pchName)
		{
			throw new NotImplementedException();
		}

		public uint GetModuleFromScope()
		{
			throw new NotImplementedException();
		}

		public unsafe uint GetTypeDefProps(uint td, char* szTypeDef, uint cchTypeDef, uint* pchTypeDef, uint* pdwTypeDefFlags, uint* ptkExtends)
		{
			if (!TryGetType(td, out var type))
			{
				return 2147500037u;
			}
			string name = (type.IsNested ? type.Name : type.FullName);
			WriteNameBuffer(name, szTypeDef, cchTypeDef, pchTypeDef);
			if (pdwTypeDefFlags != null)
			{
				*pdwTypeDefFlags = (uint)type.Attributes;
			}
			if (ptkExtends != null)
			{
				*ptkExtends = ((type.BaseType != null) ? type.BaseType.MetadataToken.ToUInt32() : 0u);
			}
			return 0u;
		}

		public uint GetInterfaceImplProps(uint iiImpl, out uint pClass)
		{
			throw new NotImplementedException();
		}

		public uint GetTypeRefProps(uint tr, out uint ptkResolutionScope, StringBuilder szName, uint cchName)
		{
			throw new NotImplementedException();
		}

		public uint ResolveTypeRef(uint tr, ref Guid riid, out object ppIScope)
		{
			throw new NotImplementedException();
		}

		public uint EnumMembers(ref uint phEnum, uint cl, uint[] rMembers, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint EnumMembersWithName(ref uint phEnum, uint cl, string szName, uint[] rMembers, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint EnumMethods(ref uint phEnum, uint cl, IntPtr rMethods, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint EnumMethodsWithName(ref uint phEnum, uint cl, string szName, uint[] rMethods, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint EnumFields(ref uint phEnum, uint cl, IntPtr rFields, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint EnumFieldsWithName(ref uint phEnum, uint cl, string szName, uint[] rFields, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint EnumParams(ref uint phEnum, uint mb, uint[] rParams, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint EnumMemberRefs(ref uint phEnum, uint tkParent, uint[] rMemberRefs, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint EnumMethodImpls(ref uint phEnum, uint td, uint[] rMethodBody, uint[] rMethodDecl, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint EnumPermissionSets(ref uint phEnum, uint tk, uint dwActions, uint[] rPermission, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint FindMember(uint td, string szName, byte[] pvSigBlob, uint cbSigBlob)
		{
			throw new NotImplementedException();
		}

		public uint FindMethod(uint td, string szName, byte[] pvSigBlob, uint cbSigBlob)
		{
			throw new NotImplementedException();
		}

		public uint FindField(uint td, string szName, byte[] pvSigBlob, uint cbSigBlob)
		{
			throw new NotImplementedException();
		}

		public uint FindMemberRef(uint td, string szName, byte[] pvSigBlob, uint cbSigBlob)
		{
			throw new NotImplementedException();
		}

		public unsafe uint GetMethodProps(uint mb, uint* pClass, char* szMethod, uint cchMethod, uint* pchMethod, uint* pdwAttr, IntPtr ppvSigBlob, IntPtr pcbSigBlob, uint* pulCodeRVA, uint* pdwImplFlags)
		{
			if (!TryGetMethod(mb, out var method))
			{
				return 2147500037u;
			}
			if (pClass != null)
			{
				*pClass = method.DeclaringType.MetadataToken.ToUInt32();
			}
			WriteNameBuffer(method.Name, szMethod, cchMethod, pchMethod);
			if (pdwAttr != null)
			{
				*pdwAttr = (uint)method.Attributes;
			}
			if (pulCodeRVA != null)
			{
				*pulCodeRVA = (uint)method.RVA;
			}
			if (pdwImplFlags != null)
			{
				*pdwImplFlags = (uint)method.ImplAttributes;
			}
			return 0u;
		}

		private unsafe static void WriteNameBuffer(string name, char* buffer, uint bufferLength, uint* actualLength)
		{
			long num = System.Math.Min(name.Length, bufferLength - 1);
			if (actualLength != null)
			{
				*actualLength = (uint)num;
			}
			if (buffer != null && bufferLength != 0)
			{
				for (int i = 0; i < num; i++)
				{
					buffer[i] = name[i];
				}
				buffer[num + 1] = '\0';
			}
		}

		public uint GetMemberRefProps(uint mr, ref uint ptk, StringBuilder szMember, uint cchMember, out uint pchMember, out IntPtr ppvSigBlob)
		{
			throw new NotImplementedException();
		}

		public uint EnumProperties(ref uint phEnum, uint td, IntPtr rProperties, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint EnumEvents(ref uint phEnum, uint td, IntPtr rEvents, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint GetEventProps(uint ev, out uint pClass, StringBuilder szEvent, uint cchEvent, out uint pchEvent, out uint pdwEventFlags, out uint ptkEventType, out uint pmdAddOn, out uint pmdRemoveOn, out uint pmdFire, uint[] rmdOtherMethod, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint EnumMethodSemantics(ref uint phEnum, uint mb, uint[] rEventProp, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint GetMethodSemantics(uint mb, uint tkEventProp)
		{
			throw new NotImplementedException();
		}

		public uint GetClassLayout(uint td, out uint pdwPackSize, IntPtr rFieldOffset, uint cMax, out uint pcFieldOffset)
		{
			throw new NotImplementedException();
		}

		public uint GetFieldMarshal(uint tk, out IntPtr ppvNativeType)
		{
			throw new NotImplementedException();
		}

		public uint GetRVA(uint tk, out uint pulCodeRVA)
		{
			throw new NotImplementedException();
		}

		public uint GetPermissionSetProps(uint pm, out uint pdwAction, out IntPtr ppvPermission)
		{
			throw new NotImplementedException();
		}

		public uint GetSigFromToken(uint mdSig, out IntPtr ppvSig)
		{
			throw new NotImplementedException();
		}

		public uint GetModuleRefProps(uint mur, StringBuilder szName, uint cchName)
		{
			throw new NotImplementedException();
		}

		public uint EnumModuleRefs(ref uint phEnum, uint[] rModuleRefs, uint cmax)
		{
			throw new NotImplementedException();
		}

		public uint GetTypeSpecFromToken(uint typespec, out IntPtr ppvSig)
		{
			throw new NotImplementedException();
		}

		public uint GetNameFromToken(uint tk)
		{
			throw new NotImplementedException();
		}

		public uint EnumUnresolvedMethods(ref uint phEnum, uint[] rMethods, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint GetUserString(uint stk, StringBuilder szString, uint cchString)
		{
			throw new NotImplementedException();
		}

		public uint GetPinvokeMap(uint tk, out uint pdwMappingFlags, StringBuilder szImportName, uint cchImportName, out uint pchImportName)
		{
			throw new NotImplementedException();
		}

		public uint EnumSignatures(ref uint phEnum, uint[] rSignatures, uint cmax)
		{
			throw new NotImplementedException();
		}

		public uint EnumTypeSpecs(ref uint phEnum, uint[] rTypeSpecs, uint cmax)
		{
			throw new NotImplementedException();
		}

		public uint EnumUserStrings(ref uint phEnum, uint[] rStrings, uint cmax)
		{
			throw new NotImplementedException();
		}

		public int GetParamForMethodIndex(uint md, uint ulParamSeq, out uint pParam)
		{
			throw new NotImplementedException();
		}

		public uint EnumCustomAttributes(ref uint phEnum, uint tk, uint tkType, uint[] rCustomAttributes, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint GetCustomAttributeProps(uint cv, out uint ptkObj, out uint ptkType, out IntPtr ppBlob)
		{
			throw new NotImplementedException();
		}

		public uint FindTypeRef(uint tkResolutionScope, string szName)
		{
			throw new NotImplementedException();
		}

		public uint GetMemberProps(uint mb, out uint pClass, StringBuilder szMember, uint cchMember, out uint pchMember, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pulCodeRVA, out uint pdwImplFlags, out uint pdwCPlusTypeFlag, out IntPtr ppValue)
		{
			throw new NotImplementedException();
		}

		public uint GetFieldProps(uint mb, out uint pClass, StringBuilder szField, uint cchField, out uint pchField, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pdwCPlusTypeFlag, out IntPtr ppValue)
		{
			throw new NotImplementedException();
		}

		public uint GetPropertyProps(uint prop, out uint pClass, StringBuilder szProperty, uint cchProperty, out uint pchProperty, out uint pdwPropFlags, out IntPtr ppvSig, out uint pbSig, out uint pdwCPlusTypeFlag, out IntPtr ppDefaultValue, out uint pcchDefaultValue, out uint pmdSetter, out uint pmdGetter, uint[] rmdOtherMethod, uint cMax)
		{
			throw new NotImplementedException();
		}

		public uint GetParamProps(uint tk, out uint pmd, out uint pulSequence, StringBuilder szName, uint cchName, out uint pchName, out uint pdwAttr, out uint pdwCPlusTypeFlag, out IntPtr ppValue)
		{
			throw new NotImplementedException();
		}

		public uint GetCustomAttributeByName(uint tkObj, string szName, out IntPtr ppData)
		{
			throw new NotImplementedException();
		}

		public bool IsValidToken(uint tk)
		{
			throw new NotImplementedException();
		}

		public unsafe uint GetNestedClassProps(uint tdNestedClass, uint* ptdEnclosingClass)
		{
			if (!TryGetType(tdNestedClass, out var type))
			{
				return 2147500037u;
			}
			if (ptdEnclosingClass != null)
			{
				*ptdEnclosingClass = (type.IsNested ? type.DeclaringType.MetadataToken.ToUInt32() : 0u);
			}
			return 0u;
		}

		public uint GetNativeCallConvFromSig(IntPtr pvSig, uint cbSig)
		{
			throw new NotImplementedException();
		}

		public int IsGlobal(uint pd)
		{
			throw new NotImplementedException();
		}
	}
	public class NativePdbReader : ISymbolReader, IDisposable
	{
		private readonly Disposable<Stream> pdb_file;

		private readonly Dictionary<string, Document> documents = new Dictionary<string, Document>();

		private readonly Dictionary<uint, PdbFunction> functions = new Dictionary<uint, PdbFunction>();

		private readonly Dictionary<PdbScope, ImportDebugInformation> imports = new Dictionary<PdbScope, ImportDebugInformation>();

		internal NativePdbReader(Disposable<Stream> file)
		{
			pdb_file = file;
		}

		public ISymbolWriterProvider GetWriterProvider()
		{
			return new NativePdbWriterProvider();
		}

		public bool ProcessDebugHeader(ImageDebugHeader header)
		{
			if (!header.HasEntries)
			{
				return false;
			}
			using (pdb_file)
			{
				PdbInfo pdbInfo = PdbFile.LoadFunctions(pdb_file.value);
				ImageDebugHeaderEntry[] entries = header.Entries;
				foreach (ImageDebugHeaderEntry entry in entries)
				{
					if (IsMatchingEntry(pdbInfo, entry))
					{
						PdbFunction[] array = pdbInfo.Functions;
						foreach (PdbFunction pdbFunction in array)
						{
							functions.Add(pdbFunction.token, pdbFunction);
						}
						return true;
					}
				}
			}
			return false;
		}

		private static bool IsMatchingEntry(PdbInfo info, ImageDebugHeaderEntry entry)
		{
			if (entry.Directory.Type != ImageDebugType.CodeView)
			{
				return false;
			}
			byte[] data = entry.Data;
			if (data.Length < 24)
			{
				return false;
			}
			int num = ReadInt32(data, 0);
			if (num != 1396986706)
			{
				return false;
			}
			byte[] array = new byte[16];
			Buffer.BlockCopy(data, 4, array, 0, 16);
			return info.Guid == new Guid(array);
		}

		private static int ReadInt32(byte[] bytes, int start)
		{
			return bytes[start] | (bytes[start + 1] << 8) | (bytes[start + 2] << 16) | (bytes[start + 3] << 24);
		}

		public MethodDebugInformation Read(MethodDefinition method)
		{
			MetadataToken metadataToken = method.MetadataToken;
			if (!functions.TryGetValue(metadataToken.ToUInt32(), out var value))
			{
				return null;
			}
			MethodDebugInformation methodDebugInformation = new MethodDebugInformation(method);
			ReadSequencePoints(value, methodDebugInformation);
			methodDebugInformation.scope = ((!value.scopes.IsNullOrEmpty()) ? ReadScopeAndLocals(value.scopes[0], methodDebugInformation) : new ScopeDebugInformation
			{
				Start = new InstructionOffset(0),
				End = new InstructionOffset((int)value.length)
			});
			if (value.tokenOfMethodWhoseUsingInfoAppliesToThisMethod != method.MetadataToken.ToUInt32() && value.tokenOfMethodWhoseUsingInfoAppliesToThisMethod != 0)
			{
				methodDebugInformation.scope.import = GetImport(value.tokenOfMethodWhoseUsingInfoAppliesToThisMethod, method.Module);
			}
			if (value.scopes.Length > 1)
			{
				for (int i = 1; i < value.scopes.Length; i++)
				{
					ScopeDebugInformation scopeDebugInformation = ReadScopeAndLocals(value.scopes[i], methodDebugInformation);
					if (!AddScope(methodDebugInformation.scope.Scopes, scopeDebugInformation))
					{
						methodDebugInformation.scope.Scopes.Add(scopeDebugInformation);
					}
				}
			}
			if (value.iteratorScopes != null)
			{
				StateMachineScopeDebugInformation stateMachineScopeDebugInformation = new StateMachineScopeDebugInformation();
				foreach (ILocalScope iteratorScope in value.iteratorScopes)
				{
					stateMachineScopeDebugInformation.Scopes.Add(new StateMachineScope((int)iteratorScope.Offset, (int)(iteratorScope.Offset + iteratorScope.Length + 1)));
				}
				methodDebugInformation.CustomDebugInformations.Add(stateMachineScopeDebugInformation);
			}
			if (value.synchronizationInformation != null)
			{
				AsyncMethodBodyDebugInformation asyncMethodBodyDebugInformation = new AsyncMethodBodyDebugInformation((int)value.synchronizationInformation.GeneratedCatchHandlerOffset);
				PdbSynchronizationPoint[] synchronizationPoints = value.synchronizationInformation.synchronizationPoints;
				foreach (PdbSynchronizationPoint pdbSynchronizationPoint in synchronizationPoints)
				{
					asyncMethodBodyDebugInformation.Yields.Add(new InstructionOffset((int)pdbSynchronizationPoint.SynchronizeOffset));
					asyncMethodBodyDebugInformation.Resumes.Add(new InstructionOffset((int)pdbSynchronizationPoint.ContinuationOffset));
					asyncMethodBodyDebugInformation.ResumeMethods.Add(method);
				}
				methodDebugInformation.CustomDebugInformations.Add(asyncMethodBodyDebugInformation);
				methodDebugInformation.StateMachineKickOffMethod = (MethodDefinition)method.Module.LookupToken((int)value.synchronizationInformation.kickoffMethodToken);
			}
			return methodDebugInformation;
		}

		private Collection<ScopeDebugInformation> ReadScopeAndLocals(PdbScope[] scopes, MethodDebugInformation info)
		{
			Collection<ScopeDebugInformation> collection = new Collection<ScopeDebugInformation>(scopes.Length);
			foreach (PdbScope pdbScope in scopes)
			{
				if (pdbScope != null)
				{
					collection.Add(ReadScopeAndLocals(pdbScope, info));
				}
			}
			return collection;
		}

		private ScopeDebugInformation ReadScopeAndLocals(PdbScope scope, MethodDebugInformation info)
		{
			ScopeDebugInformation scopeDebugInformation = new ScopeDebugInformation();
			scopeDebugInformation.Start = new InstructionOffset((int)scope.offset);
			scopeDebugInformation.End = new InstructionOffset((int)(scope.offset + scope.length));
			if (!scope.slots.IsNullOrEmpty())
			{
				scopeDebugInformation.variables = new Collection<VariableDebugInformation>(scope.slots.Length);
				PdbSlot[] slots = scope.slots;
				foreach (PdbSlot pdbSlot in slots)
				{
					if ((pdbSlot.flags & 1) == 0)
					{
						int slot = (int)pdbSlot.slot;
						VariableDebugInformation variableDebugInformation = new VariableDebugInformation(slot, pdbSlot.name);
						if ((pdbSlot.flags & 4) != 0)
						{
							variableDebugInformation.IsDebuggerHidden = true;
						}
						scopeDebugInformation.variables.Add(variableDebugInformation);
					}
				}
			}
			if (!scope.constants.IsNullOrEmpty())
			{
				scopeDebugInformation.constants = new Collection<ConstantDebugInformation>(scope.constants.Length);
				PdbConstant[] constants = scope.constants;
				foreach (PdbConstant pdbConstant in constants)
				{
					TypeReference typeReference = info.Method.Module.Read(pdbConstant, (PdbConstant c, MetadataReader r) => r.ReadConstantSignature(new MetadataToken(c.token)));
					object obj = pdbConstant.value;
					if (typeReference != null && !typeReference.IsValueType && obj is int && (int)obj == 0)
					{
						obj = null;
					}
					scopeDebugInformation.constants.Add(new ConstantDebugInformation(pdbConstant.name, typeReference, obj));
				}
			}
			if (!scope.usedNamespaces.IsNullOrEmpty())
			{
				if (imports.TryGetValue(scope, out var value))
				{
					scopeDebugInformation.import = value;
				}
				else
				{
					value = GetImport(scope, info.Method.Module);
					imports.Add(scope, value);
					scopeDebugInformation.import = value;
				}
			}
			scopeDebugInformation.scopes = ReadScopeAndLocals(scope.scopes, info);
			return scopeDebugInformation;
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

		private ImportDebugInformation GetImport(uint token, ModuleDefinition module)
		{
			if (!functions.TryGetValue(token, out var value))
			{
				return null;
			}
			if (value.scopes.Length != 1)
			{
				return null;
			}
			PdbScope pdbScope = value.scopes[0];
			if (imports.TryGetValue(pdbScope, out var value2))
			{
				return value2;
			}
			value2 = GetImport(pdbScope, module);
			imports.Add(pdbScope, value2);
			return value2;
		}

		private static ImportDebugInformation GetImport(PdbScope scope, ModuleDefinition module)
		{
			if (scope.usedNamespaces.IsNullOrEmpty())
			{
				return null;
			}
			ImportDebugInformation importDebugInformation = new ImportDebugInformation();
			string[] usedNamespaces = scope.usedNamespaces;
			foreach (string text in usedNamespaces)
			{
				if (string.IsNullOrEmpty(text))
				{
					continue;
				}
				ImportTarget importTarget = null;
				string text2 = text.Substring(1);
				switch (text[0])
				{
				case 'U':
					importTarget = new ImportTarget(ImportTargetKind.ImportNamespace)
					{
						@namespace = text2
					};
					break;
				case 'T':
				{
					TypeReference typeReference2 = TypeParser.ParseType(module, text2);
					if (typeReference2 != null)
					{
						importTarget = new ImportTarget(ImportTargetKind.ImportType)
						{
							type = typeReference2
						};
					}
					break;
				}
				case 'A':
				{
					int num = text.IndexOf(' ');
					if (num < 0)
					{
						importTarget = new ImportTarget(ImportTargetKind.ImportNamespace)
						{
							@namespace = text
						};
						break;
					}
					string alias = text.Substring(1, num - 1);
					string text3 = text.Substring(num + 2);
					switch (text[num + 1])
					{
					case 'U':
						importTarget = new ImportTarget(ImportTargetKind.DefineNamespaceAlias)
						{
							alias = alias,
							@namespace = text3
						};
						break;
					case 'T':
					{
						TypeReference typeReference = TypeParser.ParseType(module, text3);
						if (typeReference != null)
						{
							importTarget = new ImportTarget(ImportTargetKind.DefineTypeAlias)
							{
								alias = alias,
								type = typeReference
							};
						}
						break;
					}
					}
					break;
				}
				case '*':
					importTarget = new ImportTarget(ImportTargetKind.ImportNamespace)
					{
						@namespace = text2
					};
					break;
				case '@':
					if (!text2.StartsWith("P:"))
					{
						continue;
					}
					importTarget = new ImportTarget(ImportTargetKind.ImportNamespace)
					{
						@namespace = text2.Substring(2)
					};
					break;
				}
				if (importTarget != null)
				{
					importDebugInformation.Targets.Add(importTarget);
				}
			}
			return importDebugInformation;
		}

		private void ReadSequencePoints(PdbFunction function, MethodDebugInformation info)
		{
			if (function.lines != null)
			{
				info.sequence_points = new Collection<SequencePoint>();
				PdbLines[] lines = function.lines;
				foreach (PdbLines lines2 in lines)
				{
					ReadLines(lines2, info);
				}
			}
		}

		private void ReadLines(PdbLines lines, MethodDebugInformation info)
		{
			Document document = GetDocument(lines.file);
			PdbLine[] lines2 = lines.lines;
			foreach (PdbLine line in lines2)
			{
				ReadLine(line, document, info);
			}
		}

		private static void ReadLine(PdbLine line, Document document, MethodDebugInformation info)
		{
			SequencePoint sequencePoint = new SequencePoint((int)line.offset, document);
			sequencePoint.StartLine = (int)line.lineBegin;
			sequencePoint.StartColumn = line.colBegin;
			sequencePoint.EndLine = (int)line.lineEnd;
			sequencePoint.EndColumn = line.colEnd;
			info.sequence_points.Add(sequencePoint);
		}

		private Document GetDocument(PdbSource source)
		{
			string name = source.name;
			if (documents.TryGetValue(name, out var value))
			{
				return value;
			}
			value = new Document(name)
			{
				LanguageGuid = source.language,
				LanguageVendorGuid = source.vendor,
				TypeGuid = source.doctype,
				HashAlgorithmGuid = source.checksumAlgorithm,
				Hash = source.checksum
			};
			documents.Add(name, value);
			return value;
		}

		public void Dispose()
		{
			pdb_file.Dispose();
		}
	}
	public class NativePdbWriter : ISymbolWriter, IDisposable
	{
		private readonly ModuleDefinition module;

		private readonly MetadataBuilder metadata;

		private readonly SymWriter writer;

		private readonly Dictionary<string, SymDocumentWriter> documents;

		private readonly Dictionary<ImportDebugInformation, MetadataToken> import_info_to_parent;

		private ImageDebugDirectory debug_directory;

		private byte[] debug_info;

		internal NativePdbWriter(ModuleDefinition module, SymWriter writer)
		{
			this.module = module;
			metadata = module.metadata_builder;
			this.writer = writer;
			documents = new Dictionary<string, SymDocumentWriter>();
			import_info_to_parent = new Dictionary<ImportDebugInformation, MetadataToken>();
		}

		public ISymbolReaderProvider GetReaderProvider()
		{
			return new NativePdbReaderProvider();
		}

		public ImageDebugHeader GetDebugHeader()
		{
			return new ImageDebugHeader(new ImageDebugHeaderEntry(debug_directory, debug_info));
		}

		public void Write(MethodDebugInformation info)
		{
			int methodToken = info.method.MetadataToken.ToInt32();
			if (info.HasSequencePoints || info.scope != null || info.HasCustomDebugInformations || info.StateMachineKickOffMethod != null)
			{
				writer.OpenMethod(methodToken);
				if (!info.sequence_points.IsNullOrEmpty())
				{
					DefineSequencePoints(info.sequence_points);
				}
				MetadataToken import_parent = default(MetadataToken);
				if (info.scope != null)
				{
					DefineScope(info.scope, info, out import_parent);
				}
				DefineCustomMetadata(info, import_parent);
				writer.CloseMethod();
			}
		}

		private void DefineCustomMetadata(MethodDebugInformation info, MetadataToken import_parent)
		{
			CustomMetadataWriter customMetadataWriter = new CustomMetadataWriter(writer);
			if (import_parent.RID != 0)
			{
				customMetadataWriter.WriteForwardInfo(import_parent);
			}
			else if (info.scope != null && info.scope.Import != null && info.scope.Import.HasTargets)
			{
				customMetadataWriter.WriteUsingInfo(info.scope.Import);
			}
			if (info.Method.HasCustomAttributes)
			{
				foreach (CustomAttribute customAttribute in info.Method.CustomAttributes)
				{
					TypeReference attributeType = customAttribute.AttributeType;
					if ((attributeType.IsTypeOf("System.Runtime.CompilerServices", "IteratorStateMachineAttribute") || attributeType.IsTypeOf("System.Runtime.CompilerServices", "AsyncStateMachineAttribute")) && customAttribute.ConstructorArguments[0].Value is TypeReference type)
					{
						customMetadataWriter.WriteForwardIterator(type);
					}
				}
			}
			if (info.HasCustomDebugInformations && info.CustomDebugInformations.FirstOrDefault((CustomDebugInformation cdi) => cdi.Kind == CustomDebugInformationKind.StateMachineScope) is StateMachineScopeDebugInformation state_machine)
			{
				customMetadataWriter.WriteIteratorScopes(state_machine, info);
			}
			customMetadataWriter.WriteCustomMetadata();
			DefineAsyncCustomMetadata(info);
		}

		private void DefineAsyncCustomMetadata(MethodDebugInformation info)
		{
			if (!info.HasCustomDebugInformations)
			{
				return;
			}
			foreach (CustomDebugInformation customDebugInformation in info.CustomDebugInformations)
			{
				if (!(customDebugInformation is AsyncMethodBodyDebugInformation asyncMethodBodyDebugInformation))
				{
					continue;
				}
				using MemoryStream memoryStream = new MemoryStream();
				BinaryStreamWriter binaryStreamWriter = new BinaryStreamWriter(memoryStream);
				binaryStreamWriter.WriteUInt32((info.StateMachineKickOffMethod != null) ? info.StateMachineKickOffMethod.MetadataToken.ToUInt32() : 0u);
				binaryStreamWriter.WriteUInt32((uint)asyncMethodBodyDebugInformation.CatchHandler.Offset);
				binaryStreamWriter.WriteUInt32((uint)asyncMethodBodyDebugInformation.Resumes.Count);
				for (int i = 0; i < asyncMethodBodyDebugInformation.Resumes.Count; i++)
				{
					binaryStreamWriter.WriteUInt32((uint)asyncMethodBodyDebugInformation.Yields[i].Offset);
					binaryStreamWriter.WriteUInt32(asyncMethodBodyDebugInformation.resume_methods[i].MetadataToken.ToUInt32());
					binaryStreamWriter.WriteUInt32((uint)asyncMethodBodyDebugInformation.Resumes[i].Offset);
				}
				writer.DefineCustomMetadata("asyncMethodInfo", memoryStream.ToArray());
			}
		}

		private void DefineScope(ScopeDebugInformation scope, MethodDebugInformation info, out MetadataToken import_parent)
		{
			int offset = scope.Start.Offset;
			int num = (scope.End.IsEndOfMethod ? info.code_size : scope.End.Offset);
			import_parent = new MetadataToken(0u);
			writer.OpenScope(offset);
			if (scope.Import != null && scope.Import.HasTargets && !import_info_to_parent.TryGetValue(info.scope.Import, out import_parent))
			{
				foreach (ImportTarget target in scope.Import.Targets)
				{
					switch (target.Kind)
					{
					case ImportTargetKind.ImportNamespace:
						writer.UsingNamespace("U" + target.@namespace);
						break;
					case ImportTargetKind.ImportType:
						writer.UsingNamespace("T" + TypeParser.ToParseable(target.type));
						break;
					case ImportTargetKind.DefineNamespaceAlias:
						writer.UsingNamespace("A" + target.Alias + " U" + target.@namespace);
						break;
					case ImportTargetKind.DefineTypeAlias:
						writer.UsingNamespace("A" + target.Alias + " T" + TypeParser.ToParseable(target.type));
						break;
					}
				}
				import_info_to_parent.Add(info.scope.Import, info.method.MetadataToken);
			}
			int local_var_token = info.local_var_token.ToInt32();
			if (!scope.variables.IsNullOrEmpty())
			{
				for (int i = 0; i < scope.variables.Count; i++)
				{
					VariableDebugInformation variable = scope.variables[i];
					DefineLocalVariable(variable, local_var_token, offset, num);
				}
			}
			if (!scope.constants.IsNullOrEmpty())
			{
				for (int j = 0; j < scope.constants.Count; j++)
				{
					ConstantDebugInformation constant = scope.constants[j];
					DefineConstant(constant);
				}
			}
			if (!scope.scopes.IsNullOrEmpty())
			{
				for (int k = 0; k < scope.scopes.Count; k++)
				{
					DefineScope(scope.scopes[k], info, out var _);
				}
			}
			writer.CloseScope(num);
		}

		private void DefineSequencePoints(Collection<SequencePoint> sequence_points)
		{
			for (int i = 0; i < sequence_points.Count; i++)
			{
				SequencePoint sequencePoint = sequence_points[i];
				writer.DefineSequencePoints(GetDocument(sequencePoint.Document), new int[1] { sequencePoint.Offset }, new int[1] { sequencePoint.StartLine }, new int[1] { sequencePoint.StartColumn }, new int[1] { sequencePoint.EndLine }, new int[1] { sequencePoint.EndColumn });
			}
		}

		private void DefineLocalVariable(VariableDebugInformation variable, int local_var_token, int start_offset, int end_offset)
		{
			writer.DefineLocalVariable2(variable.Name, variable.Attributes, local_var_token, variable.Index, 0, 0, start_offset, end_offset);
		}

		private void DefineConstant(ConstantDebugInformation constant)
		{
			uint rid = metadata.AddStandAloneSignature(metadata.GetConstantTypeBlobIndex(constant.ConstantType));
			MetadataToken metadataToken = new MetadataToken(TokenType.Signature, rid);
			writer.DefineConstant2(constant.Name, constant.Value, metadataToken.ToInt32());
		}

		private SymDocumentWriter GetDocument(Document document)
		{
			if (document == null)
			{
				return null;
			}
			if (documents.TryGetValue(document.Url, out var value))
			{
				return value;
			}
			value = writer.DefineDocument(document.Url, document.LanguageGuid, document.LanguageVendorGuid, document.TypeGuid);
			if (!document.Hash.IsNullOrEmpty())
			{
				value.SetCheckSum(document.HashAlgorithmGuid, document.Hash);
			}
			documents[document.Url] = value;
			return value;
		}

		public void Write()
		{
			MethodDefinition entryPoint = module.EntryPoint;
			if (entryPoint != null)
			{
				writer.SetUserEntryPoint(entryPoint.MetadataToken.ToInt32());
			}
			debug_info = writer.GetDebugInfo(out debug_directory);
			debug_directory.TimeDateStamp = (int)module.timestamp;
			writer.Close();
		}

		public void Dispose()
		{
		}
	}
	internal enum CustomMetadataType : byte
	{
		UsingInfo = 0,
		ForwardInfo = 1,
		IteratorScopes = 3,
		ForwardIterator = 4
	}
	internal class CustomMetadataWriter : IDisposable
	{
		private readonly SymWriter sym_writer;

		private readonly MemoryStream stream;

		private readonly BinaryStreamWriter writer;

		private int count;

		private const byte version = 4;

		public CustomMetadataWriter(SymWriter sym_writer)
		{
			this.sym_writer = sym_writer;
			stream = new MemoryStream();
			writer = new BinaryStreamWriter(stream);
			writer.WriteByte(4);
			writer.WriteByte(0);
			writer.Align(4);
		}

		public void WriteUsingInfo(ImportDebugInformation import_info)
		{
			Write(CustomMetadataType.UsingInfo, delegate
			{
				writer.WriteUInt16(1);
				writer.WriteUInt16((ushort)import_info.Targets.Count);
			});
		}

		public void WriteForwardInfo(MetadataToken import_parent)
		{
			Write(CustomMetadataType.ForwardInfo, delegate
			{
				writer.WriteUInt32(import_parent.ToUInt32());
			});
		}

		public void WriteIteratorScopes(StateMachineScopeDebugInformation state_machine, MethodDebugInformation debug_info)
		{
			Write(CustomMetadataType.IteratorScopes, delegate
			{
				Collection<StateMachineScope> scopes = state_machine.Scopes;
				writer.WriteInt32(scopes.Count);
				foreach (StateMachineScope item in scopes)
				{
					int offset = item.Start.Offset;
					int num = (item.End.IsEndOfMethod ? debug_info.code_size : item.End.Offset);
					writer.WriteInt32(offset);
					writer.WriteInt32(num - 1);
				}
			});
		}

		public void WriteForwardIterator(TypeReference type)
		{
			Write(CustomMetadataType.ForwardIterator, delegate
			{
				writer.WriteBytes(Encoding.Unicode.GetBytes(type.Name));
			});
		}

		private void Write(CustomMetadataType type, Action write)
		{
			count++;
			writer.WriteByte(4);
			writer.WriteByte((byte)type);
			writer.Align(4);
			int position = writer.Position;
			writer.WriteUInt32(0u);
			write();
			writer.Align(4);
			int position2 = writer.Position;
			int value = position2 - position + 4;
			writer.Position = position;
			writer.WriteInt32(value);
			writer.Position = position2;
		}

		public void WriteCustomMetadata()
		{
			if (count != 0)
			{
				writer.BaseStream.Position = 1L;
				writer.WriteByte((byte)count);
				writer.Flush();
				sym_writer.DefineCustomMetadata("MD2", stream.ToArray());
			}
		}

		public void Dispose()
		{
			stream.Dispose();
		}
	}
	public sealed class NativePdbReaderProvider : ISymbolReaderProvider
	{
		public ISymbolReader GetSymbolReader(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			return new NativePdbReader(Disposable.Owned((Stream)File.OpenRead(Mixin.GetPdbFileName(fileName))));
		}

		public ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream)
		{
			Mixin.CheckModule(module);
			Mixin.CheckStream(symbolStream);
			return new NativePdbReader(Disposable.NotOwned(symbolStream));
		}
	}
	public sealed class PdbReaderProvider : ISymbolReaderProvider
	{
		public ISymbolReader GetSymbolReader(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			if (module.HasDebugHeader)
			{
				ImageDebugHeader debugHeader = module.GetDebugHeader();
				ImageDebugHeaderEntry embeddedPortablePdbEntry = debugHeader.GetEmbeddedPortablePdbEntry();
				if (embeddedPortablePdbEntry != null)
				{
					return new EmbeddedPortablePdbReaderProvider().GetSymbolReader(module, fileName);
				}
			}
			Mixin.CheckFileName(fileName);
			return Mixin.IsPortablePdb(Mixin.GetPdbFileName(fileName)) ? new PortablePdbReaderProvider().GetSymbolReader(module, fileName) : new NativePdbReaderProvider().GetSymbolReader(module, fileName);
		}

		public ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream)
		{
			Mixin.CheckModule(module);
			Mixin.CheckStream(symbolStream);
			Mixin.CheckReadSeek(symbolStream);
			return Mixin.IsPortablePdb(symbolStream) ? new PortablePdbReaderProvider().GetSymbolReader(module, symbolStream) : new NativePdbReaderProvider().GetSymbolReader(module, symbolStream);
		}
	}
	public sealed class NativePdbWriterProvider : ISymbolWriterProvider
	{
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			return new NativePdbWriter(module, CreateWriter(module, Mixin.GetPdbFileName(fileName)));
		}

		private static SymWriter CreateWriter(ModuleDefinition module, string pdb)
		{
			SymWriter symWriter = new SymWriter();
			if (File.Exists(pdb))
			{
				File.Delete(pdb);
			}
			symWriter.Initialize(new ModuleMetadata(module), pdb, fFullBuild: true);
			return symWriter;
		}

		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream)
		{
			throw new NotImplementedException();
		}
	}
	public sealed class PdbWriterProvider : ISymbolWriterProvider
	{
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			if (HasPortablePdbSymbols(module))
			{
				return new PortablePdbWriterProvider().GetSymbolWriter(module, fileName);
			}
			return new NativePdbWriterProvider().GetSymbolWriter(module, fileName);
		}

		private static bool HasPortablePdbSymbols(ModuleDefinition module)
		{
			return module.symbol_reader != null && module.symbol_reader is PortablePdbReader;
		}

		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream)
		{
			Mixin.CheckModule(module);
			Mixin.CheckStream(symbolStream);
			Mixin.CheckReadSeek(symbolStream);
			if (HasPortablePdbSymbols(module))
			{
				return new PortablePdbWriterProvider().GetSymbolWriter(module, symbolStream);
			}
			return new NativePdbWriterProvider().GetSymbolWriter(module, symbolStream);
		}
	}
	internal class SymDocumentWriter
	{
		private readonly ISymUnmanagedDocumentWriter writer;

		public ISymUnmanagedDocumentWriter Writer => writer;

		public SymDocumentWriter(ISymUnmanagedDocumentWriter writer)
		{
			this.writer = writer;
		}

		public void SetSource(byte[] source)
		{
			writer.SetSource((uint)source.Length, source);
		}

		public void SetCheckSum(Guid hashAlgo, byte[] checkSum)
		{
			writer.SetCheckSum(hashAlgo, (uint)checkSum.Length, checkSum);
		}
	}
	internal class SymWriter
	{
		private static Guid s_symUnmangedWriterIID = new Guid("0b97726e-9e6d-4f05-9a26-424022093caa");

		private static Guid s_CorSymWriter_SxS_ClassID = new Guid("108296c1-281e-11d3-bd22-0000f80849bd");

		private readonly ISymUnmanagedWriter2 writer;

		private readonly Collection<ISymUnmanagedDocumentWriter> documents;

		[DllImport("ole32.dll")]
		private static extern int CoCreateInstance([In] ref Guid rclsid, [In][MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] uint dwClsContext, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);

		public SymWriter()
		{
			CoCreateInstance(ref s_CorSymWriter_SxS_ClassID, null, 1u, ref s_symUnmangedWriterIID, out var ppv);
			writer = (ISymUnmanagedWriter2)ppv;
			documents = new Collection<ISymUnmanagedDocumentWriter>();
		}

		public byte[] GetDebugInfo(out ImageDebugDirectory idd)
		{
			writer.GetDebugInfo(out idd, 0, out var pcData, null);
			byte[] array = new byte[pcData];
			writer.GetDebugInfo(out idd, pcData, out pcData, array);
			return array;
		}

		public void DefineLocalVariable2(string name, VariableAttributes attributes, int sigToken, int addr1, int addr2, int addr3, int startOffset, int endOffset)
		{
			writer.DefineLocalVariable2(name, (int)attributes, sigToken, 1, addr1, addr2, addr3, startOffset, endOffset);
		}

		public void DefineConstant2(string name, object value, int sigToken)
		{
			if (value == null)
			{
				writer.DefineConstant2(name, 0, sigToken);
			}
			else
			{
				writer.DefineConstant2(name, value, sigToken);
			}
		}

		public void Close()
		{
			writer.Close();
			Marshal.ReleaseComObject(writer);
			foreach (ISymUnmanagedDocumentWriter document in documents)
			{
				Marshal.ReleaseComObject(document);
			}
		}

		public void CloseMethod()
		{
			writer.CloseMethod();
		}

		public void CloseNamespace()
		{
			writer.CloseNamespace();
		}

		public void CloseScope(int endOffset)
		{
			writer.CloseScope(endOffset);
		}

		public SymDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType)
		{
			writer.DefineDocument(url, ref language, ref languageVendor, ref documentType, out var pRetVal);
			documents.Add(pRetVal);
			return new SymDocumentWriter(pRetVal);
		}

		public void DefineSequencePoints(SymDocumentWriter document, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns)
		{
			writer.DefineSequencePoints(document.Writer, offsets.Length, offsets, lines, columns, endLines, endColumns);
		}

		public void Initialize(object emitter, string filename, bool fFullBuild)
		{
			writer.Initialize(emitter, filename, null, fFullBuild);
		}

		public void SetUserEntryPoint(int methodToken)
		{
			writer.SetUserEntryPoint(methodToken);
		}

		public void OpenMethod(int methodToken)
		{
			writer.OpenMethod(methodToken);
		}

		public void OpenNamespace(string name)
		{
			writer.OpenNamespace(name);
		}

		public int OpenScope(int startOffset)
		{
			writer.OpenScope(startOffset, out var pRetVal);
			return pRetVal;
		}

		public void UsingNamespace(string fullName)
		{
			writer.UsingNamespace(fullName);
		}

		public void DefineCustomMetadata(string name, byte[] metadata)
		{
			GCHandle gCHandle = GCHandle.Alloc(metadata, GCHandleType.Pinned);
			writer.SetSymAttribute(0u, name, (uint)metadata.Length, gCHandle.AddrOfPinnedObject());
			gCHandle.Free();
		}
	}
}
namespace Microsoft.Cci.Pdb
{
	internal class BitAccess
	{
		private byte[] buffer;

		private int offset;

		internal byte[] Buffer => buffer;

		internal int Position
		{
			get
			{
				return offset;
			}
			set
			{
				offset = value;
			}
		}

		internal BitAccess(int capacity)
		{
			buffer = new byte[capacity];
		}

		internal BitAccess(byte[] buffer)
		{
			this.buffer = buffer;
			offset = 0;
		}

		internal void FillBuffer(Stream stream, int capacity)
		{
			MinCapacity(capacity);
			stream.Read(buffer, 0, capacity);
			offset = 0;
		}

		internal void Append(Stream stream, int count)
		{
			int num = offset + count;
			if (buffer.Length < num)
			{
				byte[] destinationArray = new byte[num];
				Array.Copy(buffer, destinationArray, buffer.Length);
				buffer = destinationArray;
			}
			stream.Read(buffer, offset, count);
			offset += count;
		}

		internal void MinCapacity(int capacity)
		{
			if (buffer.Length < capacity)
			{
				buffer = new byte[capacity];
			}
			offset = 0;
		}

		internal void Align(int alignment)
		{
			while (offset % alignment != 0)
			{
				offset++;
			}
		}

		internal void ReadInt16(out short value)
		{
			value = (short)((buffer[offset] & 0xFF) | (buffer[offset + 1] << 8));
			offset += 2;
		}

		internal void ReadInt8(out sbyte value)
		{
			value = (sbyte)buffer[offset];
			offset++;
		}

		internal void ReadInt32(out int value)
		{
			value = (buffer[offset] & 0xFF) | (buffer[offset + 1] << 8) | (buffer[offset + 2] << 16) | (buffer[offset + 3] << 24);
			offset += 4;
		}

		internal void ReadInt64(out long value)
		{
			value = (long)(((ulong)buffer[offset] & 0xFFuL) | ((ulong)buffer[offset + 1] << 8) | ((ulong)buffer[offset + 2] << 16) | ((ulong)buffer[offset + 3] << 24) | ((ulong)buffer[offset + 4] << 32) | ((ulong)buffer[offset + 5] << 40) | ((ulong)buffer[offset + 6] << 48) | ((ulong)buffer[offset + 7] << 56));
			offset += 8;
		}

		internal void ReadUInt16(out ushort value)
		{
			value = (ushort)((buffer[offset] & 0xFF) | (buffer[offset + 1] << 8));
			offset += 2;
		}

		internal void ReadUInt8(out byte value)
		{
			value = (byte)(buffer[offset] & 0xFF);
			offset++;
		}

		internal void ReadUInt32(out uint value)
		{
			value = (uint)((buffer[offset] & 0xFF) | (buffer[offset + 1] << 8) | (buffer[offset + 2] << 16) | (buffer[offset + 3] << 24));
			offset += 4;
		}

		internal void ReadUInt64(out ulong value)
		{
			value = ((ulong)buffer[offset] & 0xFFuL) | ((ulong)buffer[offset + 1] << 8) | ((ulong)buffer[offset + 2] << 16) | ((ulong)buffer[offset + 3] << 24) | ((ulong)buffer[offset + 4] << 32) | ((ulong)buffer[offset + 5] << 40) | ((ulong)buffer[offset + 6] << 48) | ((ulong)buffer[offset + 7] << 56);
			offset += 8;
		}

		internal void ReadInt32(int[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				ReadInt32(out values[i]);
			}
		}

		internal void ReadUInt32(uint[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				ReadUInt32(out values[i]);
			}
		}

		internal void ReadBytes(byte[] bytes)
		{
			for (int i = 0; i < bytes.Length; i++)
			{
				bytes[i] = buffer[offset++];
			}
		}

		internal float ReadFloat()
		{
			float result = BitConverter.ToSingle(buffer, offset);
			offset += 4;
			return result;
		}

		internal double ReadDouble()
		{
			double result = BitConverter.ToDouble(buffer, offset);
			offset += 8;
			return result;
		}

		internal decimal ReadDecimal()
		{
			int[] array = new int[4];
			ReadInt32(array);
			return new decimal(array[2], array[3], array[1], array[0] < 0, (byte)((array[0] & 0xFF0000) >> 16));
		}

		internal void ReadBString(out string value)
		{
			ReadUInt16(out var value2);
			value = Encoding.UTF8.GetString(buffer, offset, value2);
			offset += value2;
		}

		internal string ReadBString(int len)
		{
			string result = Encoding.UTF8.GetString(buffer, offset, len);
			offset += len;
			return result;
		}

		internal void ReadCString(out string value)
		{
			int i;
			for (i = 0; offset + i < buffer.Length && buffer[offset + i] != 0; i++)
			{
			}
			value = Encoding.UTF8.GetString(buffer, offset, i);
			offset += i + 1;
		}

		internal void SkipCString(out string value)
		{
			int i;
			for (i = 0; offset + i < buffer.Length && buffer[offset + i] != 0; i++)
			{
			}
			offset += i + 1;
			value = null;
		}

		internal void ReadGuid(out Guid guid)
		{
			ReadUInt32(out var value);
			ReadUInt16(out var value2);
			ReadUInt16(out var value3);
			ReadUInt8(out var value4);
			ReadUInt8(out var value5);
			ReadUInt8(out var value6);
			ReadUInt8(out var value7);
			ReadUInt8(out var value8);
			ReadUInt8(out var value9);
			ReadUInt8(out var value10);
			ReadUInt8(out var value11);
			guid = new Guid(value, value2, value3, value4, value5, value6, value7, value8, value9, value10, value11);
		}

		internal string ReadString()
		{
			int i;
			for (i = 0; offset + i < buffer.Length && buffer[offset + i] != 0; i += 2)
			{
			}
			string result = Encoding.Unicode.GetString(buffer, offset, i);
			offset += i + 2;
			return result;
		}
	}
	internal struct BitSet
	{
		private int size;

		private uint[] words;

		internal bool IsEmpty => size == 0;

		internal BitSet(BitAccess bits)
		{
			bits.ReadInt32(out size);
			words = new uint[size];
			bits.ReadUInt32(words);
		}

		internal bool IsSet(int index)
		{
			int num = index / 32;
			if (num >= size)
			{
				return false;
			}
			return (words[num] & GetBit(index)) != 0;
		}

		private static uint GetBit(int index)
		{
			return (uint)(1 << index % 32);
		}
	}
	internal struct FLOAT10
	{
		internal byte Data_0;

		internal byte Data_1;

		internal byte Data_2;

		internal byte Data_3;

		internal byte Data_4;

		internal byte Data_5;

		internal byte Data_6;

		internal byte Data_7;

		internal byte Data_8;

		internal byte Data_9;
	}
	internal enum CV_SIGNATURE
	{
		C6 = 0,
		C7 = 1,
		C11 = 2,
		C13 = 4,
		RESERVERD = 5
	}
	internal enum CV_prmode
	{
		CV_TM_DIRECT = 0,
		CV_TM_NPTR32 = 4,
		CV_TM_NPTR64 = 6,
		CV_TM_NPTR128 = 7
	}
	internal enum CV_type
	{
		CV_SPECIAL = 0,
		CV_SIGNED = 1,
		CV_UNSIGNED = 2,
		CV_BOOLEAN = 3,
		CV_REAL = 4,
		CV_COMPLEX = 5,
		CV_SPECIAL2 = 6,
		CV_INT = 7,
		CV_CVRESERVED = 15
	}
	internal enum CV_special
	{
		CV_SP_NOTYPE,
		CV_SP_ABS,
		CV_SP_SEGMENT,
		CV_SP_VOID,
		CV_SP_CURRENCY,
		CV_SP_NBASICSTR,
		CV_SP_FBASICSTR,
		CV_SP_NOTTRANS,
		CV_SP_HRESULT
	}
	internal enum CV_special2
	{
		CV_S2_BIT,
		CV_S2_PASCHAR
	}
	internal enum CV_integral
	{
		CV_IN_1BYTE,
		CV_IN_2BYTE,
		CV_IN_4BYTE,
		CV_IN_8BYTE,
		CV_IN_16BYTE
	}
	internal enum CV_real
	{
		CV_RC_REAL32,
		CV_RC_REAL64,
		CV_RC_REAL80,
		CV_RC_REAL128
	}
	internal enum CV_int
	{
		CV_RI_CHAR = 0,
		CV_RI_INT1 = 0,
		CV_RI_WCHAR = 1,
		CV_RI_UINT1 = 1,
		CV_RI_INT2 = 2,
		CV_RI_UINT2 = 3,
		CV_RI_INT4 = 4,
		CV_RI_UINT4 = 5,
		CV_RI_INT8 = 6,
		CV_RI_UINT8 = 7,
		CV_RI_INT16 = 8,
		CV_RI_UINT16 = 9
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct CV_PRIMITIVE_TYPE
	{
		private const uint CV_MMASK = 1792u;

		private const uint CV_TMASK = 240u;

		private const uint CV_SMASK = 15u;

		private const int CV_MSHIFT = 8;

		private const int CV_TSHIFT = 4;

		private const int CV_SSHIFT = 0;

		private const uint CV_FIRST_NONPRIM = 4096u;
	}
	internal enum TYPE_ENUM
	{
		T_NOTYPE = 0,
		T_ABS = 1,
		T_SEGMENT = 2,
		T_VOID = 3,
		T_HRESULT = 8,
		T_32PHRESULT = 1032,
		T_64PHRESULT = 1544,
		T_PVOID = 259,
		T_PFVOID = 515,
		T_PHVOID = 771,
		T_32PVOID = 1027,
		T_64PVOID = 1539,
		T_CURRENCY = 4,
		T_NOTTRANS = 7,
		T_BIT = 96,
		T_PASCHAR = 97,
		T_CHAR = 16,
		T_32PCHAR = 1040,
		T_64PCHAR = 1552,
		T_UCHAR = 32,
		T_32PUCHAR = 1056,
		T_64PUCHAR = 1568,
		T_RCHAR = 112,
		T_32PRCHAR = 1136,
		T_64PRCHAR = 1648,
		T_WCHAR = 113,
		T_32PWCHAR = 1137,
		T_64PWCHAR = 1649,
		T_INT1 = 104,
		T_32PINT1 = 1128,
		T_64PINT1 = 1640,
		T_UINT1 = 105,
		T_32PUINT1 = 1129,
		T_64PUINT1 = 1641,
		T_SHORT = 17,
		T_32PSHORT = 1041,
		T_64PSHORT = 1553,
		T_USHORT = 33,
		T_32PUSHORT = 1057,
		T_64PUSHORT = 1569,
		T_INT2 = 114,
		T_32PINT2 = 1138,
		T_64PINT2 = 1650,
		T_UINT2 = 115,
		T_32PUINT2 = 1139,
		T_64PUINT2 = 1651,
		T_LONG = 18,
		T_ULONG = 34,
		T_32PLONG = 1042,
		T_32PULONG = 1058,
		T_64PLONG = 1554,
		T_64PULONG = 1570,
		T_INT4 = 116,
		T_32PINT4 = 1140,
		T_64PINT4 = 1652,
		T_UINT4 = 117,
		T_32PUINT4 = 1141,
		T_64PUINT4 = 1653,
		T_QUAD = 19,
		T_32PQUAD = 1043,
		T_64PQUAD = 1555,
		T_UQUAD = 35,
		T_32PUQUAD = 1059,
		T_64PUQUAD = 1571,
		T_INT8 = 118,
		T_32PINT8 = 1142,
		T_64PINT8 = 1654,
		T_UINT8 = 119,
		T_32PUINT8 = 1143,
		T_64PUINT8 = 1655,
		T_OCT = 20,
		T_32POCT = 1044,
		T_64POCT = 1556,
		T_UOCT = 36,
		T_32PUOCT = 1060,
		T_64PUOCT = 1572,
		T_INT16 = 120,
		T_32PINT16 = 1144,
		T_64PINT16 = 1656,
		T_UINT16 = 121,
		T_32PUINT16 = 1145,
		T_64PUINT16 = 1657,
		T_REAL32 = 64,
		T_32PREAL32 = 1088,
		T_64PREAL32 = 1600,
		T_REAL64 = 65,
		T_32PREAL64 = 1089,
		T_64PREAL64 = 1601,
		T_REAL80 = 66,
		T_32PREAL80 = 1090,
		T_64PREAL80 = 1602,
		T_REAL128 = 67,
		T_32PREAL128 = 1091,
		T_64PREAL128 = 1603,
		T_CPLX32 = 80,
		T_32PCPLX32 = 1104,
		T_64PCPLX32 = 1616,
		T_CPLX64 = 81,
		T_32PCPLX64 = 1105,
		T_64PCPLX64 = 1617,
		T_CPLX80 = 82,
		T_32PCPLX80 = 1106,
		T_64PCPLX80 = 1618,
		T_CPLX128 = 83,
		T_32PCPLX128 = 1107,
		T_64PCPLX128 = 1619,
		T_BOOL08 = 48,
		T_32PBOOL08 = 1072,
		T_64PBOOL08 = 1584,
		T_BOOL16 = 49,
		T_32PBOOL16 = 1073,
		T_64PBOOL16 = 1585,
		T_BOOL32 = 50,
		T_32PBOOL32 = 1074,
		T_64PBOOL32 = 1586,
		T_BOOL64 = 51,
		T_32PBOOL64 = 1075,
		T_64PBOOL64 = 1587
	}
	internal enum LEAF
	{
		LF_VTSHAPE = 10,
		LF_COBOL1 = 12,
		LF_LABEL = 14,
		LF_NULL = 15,
		LF_NOTTRAN = 16,
		LF_ENDPRECOMP = 20,
		LF_TYPESERVER_ST = 22,
		LF_LIST = 515,
		LF_REFSYM = 524,
		LF_ENUMERATE_ST = 1027,
		LF_TI16_MAX = 4096,
		LF_MODIFIER = 4097,
		LF_POINTER = 4098,
		LF_ARRAY_ST = 4099,
		LF_CLASS_ST = 4100,
		LF_STRUCTURE_ST = 4101,
		LF_UNION_ST = 4102,
		LF_ENUM_ST = 4103,
		LF_PROCEDURE = 4104,
		LF_MFUNCTION = 4105,
		LF_COBOL0 = 4106,
		LF_BARRAY = 4107,
		LF_DIMARRAY_ST = 4108,
		LF_VFTPATH = 4109,
		LF_PRECOMP_ST = 4110,
		LF_OEM = 4111,
		LF_ALIAS_ST = 4112,
		LF_OEM2 = 4113,
		LF_SKIP = 4608,
		LF_ARGLIST = 4609,
		LF_DEFARG_ST = 4610,
		LF_FIELDLIST = 4611,
		LF_DERIVED = 4612,
		LF_BITFIELD = 4613,
		LF_METHODLIST = 4614,
		LF_DIMCONU = 4615,
		LF_DIMCONLU = 4616,
		LF_DIMVARU = 4617,
		LF_DIMVARLU = 4618,
		LF_BCLASS = 5120,
		LF_VBCLASS = 5121,
		LF_IVBCLASS = 5122,
		LF_FRIENDFCN_ST = 5123,
		LF_INDEX = 5124,
		LF_MEMBER_ST = 5125,
		LF_STMEMBER_ST = 5126,
		LF_METHOD_ST = 5127,
		LF_NESTTYPE_ST = 5128,
		LF_VFUNCTAB = 5129,
		LF_FRIENDCLS = 5130,
		LF_ONEMETHOD_ST = 5131,
		LF_VFUNCOFF = 5132,
		LF_NESTTYPEEX_ST = 5133,
		LF_MEMBERMODIFY_ST = 5134,
		LF_MANAGED_ST = 5135,
		LF_ST_MAX = 5376,
		LF_TYPESERVER = 5377,
		LF_ENUMERATE = 5378,
		LF_ARRAY = 5379,
		LF_CLASS = 5380,
		LF_STRUCTURE = 5381,
		LF_UNION = 5382,
		LF_ENUM = 5383,
		LF_DIMARRAY = 5384,
		LF_PRECOMP = 5385,
		LF_ALIAS = 5386,
		LF_DEFARG = 5387,
		LF_FRIENDFCN = 5388,
		LF_MEMBER = 5389,
		LF_STMEMBER = 5390,
		LF_METHOD = 5391,
		LF_NESTTYPE = 5392,
		LF_ONEMETHOD = 5393,
		LF_NESTTYPEEX = 5394,
		LF_MEMBERMODIFY = 5395,
		LF_MANAGED = 5396,
		LF_TYPESERVER2 = 5397,
		LF_NUMERIC = 32768,
		LF_CHAR = 32768,
		LF_SHORT = 32769,
		LF_USHORT = 32770,
		LF_LONG = 32771,
		LF_ULONG = 32772,
		LF_REAL32 = 32773,
		LF_REAL64 = 32774,
		LF_REAL80 = 32775,
		LF_REAL128 = 32776,
		LF_QUADWORD = 32777,
		LF_UQUADWORD = 32778,
		LF_COMPLEX32 = 32780,
		LF_COMPLEX64 = 32781,
		LF_COMPLEX80 = 32782,
		LF_COMPLEX128 = 32783,
		LF_VARSTRING = 32784,
		LF_OCTWORD = 32791,
		LF_UOCTWORD = 32792,
		LF_DECIMAL = 32793,
		LF_DATE = 32794,
		LF_UTF8STRING = 32795,
		LF_PAD0 = 240,
		LF_PAD1 = 241,
		LF_PAD2 = 242,
		LF_PAD3 = 243,
		LF_PAD4 = 244,
		LF_PAD5 = 245,
		LF_PAD6 = 246,
		LF_PAD7 = 247,
		LF_PAD8 = 248,
		LF_PAD9 = 249,
		LF_PAD10 = 250,
		LF_PAD11 = 251,
		LF_PAD12 = 252,
		LF_PAD13 = 253,
		LF_PAD14 = 254,
		LF_PAD15 = 255
	}
	internal enum CV_ptrtype
	{
		CV_PTR_BASE_SEG = 3,
		CV_PTR_BASE_VAL = 4,
		CV_PTR_BASE_SEGVAL = 5,
		CV_PTR_BASE_ADDR = 6,
		CV_PTR_BASE_SEGADDR = 7,
		CV_PTR_BASE_TYPE = 8,
		CV_PTR_BASE_SELF = 9,
		CV_PTR_NEAR32 = 10,
		CV_PTR_64 = 12,
		CV_PTR_UNUSEDPTR = 13
	}
	internal enum CV_ptrmode
	{
		CV_PTR_MODE_PTR,
		CV_PTR_MODE_REF,
		CV_PTR_MODE_PMEM,
		CV_PTR_MODE_PMFUNC,
		CV_PTR_MODE_RESERVED
	}
	internal enum CV_pmtype
	{
		CV_PMTYPE_Undef,
		CV_PMTYPE_D_Single,
		CV_PMTYPE_D_Multiple,
		CV_PMTYPE_D_Virtual,
		CV_PMTYPE_D_General,
		CV_PMTYPE_F_Single,
		CV_PMTYPE_F_Multiple,
		CV_PMTYPE_F_Virtual,
		CV_PMTYPE_F_General
	}
	internal enum CV_methodprop
	{
		CV_MTvanilla,
		CV_MTvirtual,
		CV_MTstatic,
		CV_MTfriend,
		CV_MTintro,
		CV_MTpurevirt,
		CV_MTpureintro
	}
	internal enum CV_VTS_desc
	{
		CV_VTS_near,
		CV_VTS_far,
		CV_VTS_thin,
		CV_VTS_outer,
		CV_VTS_meta,
		CV_VTS_near32,
		CV_VTS_far32,
		CV_VTS_unused
	}
	internal enum CV_LABEL_TYPE
	{
		CV_LABEL_NEAR = 0,
		CV_LABEL_FAR = 4
	}
	[Flags]
	internal enum CV_modifier : ushort
	{
		MOD_const = 1,
		MOD_volatile = 2,
		MOD_unaligned = 4
	}
	[Flags]
	internal enum CV_prop : ushort
	{
		packed = 1,
		ctor = 2,
		ovlops = 4,
		isnested = 8,
		cnested = 0x10,
		opassign = 0x20,
		opcast = 0x40,
		fwdref = 0x80,
		scoped = 0x100
	}
	[Flags]
	internal enum CV_fldattr
	{
		access = 3,
		mprop = 0x1C,
		pseudo = 0x20,
		noinherit = 0x40,
		noconstruct = 0x80,
		compgenx = 0x100
	}
	internal struct TYPTYPE
	{
		internal ushort len;

		internal ushort leaf;
	}
	internal struct CV_PDMR32_NVVFCN
	{
		internal int mdisp;
	}
	internal struct CV_PDMR32_VBASE
	{
		internal int mdisp;

		internal int pdisp;

		internal int vdisp;
	}
	internal struct CV_PMFR32_NVSA
	{
		internal uint off;
	}
	internal struct CV_PMFR32_NVMA
	{
		internal uint off;

		internal int disp;
	}
	internal struct CV_PMFR32_VBASE
	{
		internal uint off;

		internal int mdisp;

		internal int pdisp;

		internal int vdisp;
	}
	internal struct LeafModifier
	{
		internal uint type;

		internal CV_modifier attr;
	}
	[Flags]
	internal enum LeafPointerAttr : uint
	{
		ptrtype = 0x1Fu,
		ptrmode = 0xE0u,
		isflat32 = 0x100u,
		isvolatile = 0x200u,
		isconst = 0x400u,
		isunaligned = 0x800u,
		isrestrict = 0x1000u
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct LeafPointer
	{
		internal struct LeafPointerBody
		{
			internal uint utype;

			internal LeafPointerAttr attr;
		}
	}
	internal struct LeafArray
	{
		internal uint elemtype;

		internal uint idxtype;

		internal byte[] data;

		internal string name;
	}
	internal struct LeafClass
	{
		internal ushort count;

		internal ushort property;

		internal uint field;

		internal uint derived;

		internal uint vshape;

		internal byte[] data;

		internal string name;
	}
	internal struct LeafUnion
	{
		internal ushort count;

		internal ushort property;

		internal uint field;

		internal byte[] data;

		internal string name;
	}
	internal struct LeafAlias
	{
		internal uint utype;

		internal string name;
	}
	internal struct LeafManaged
	{
		internal string name;
	}
	internal struct LeafEnum
	{
		internal ushort count;

		internal ushort property;

		internal uint utype;

		internal uint field;

		internal string name;
	}
	internal struct LeafProc
	{
		internal uint rvtype;

		internal byte calltype;

		internal byte reserved;

		internal ushort parmcount;

		internal uint arglist;
	}
	internal struct LeafMFunc
	{
		internal uint rvtype;

		internal uint classtype;

		internal uint thistype;

		internal byte calltype;

		internal byte reserved;

		internal ushort parmcount;

		internal uint arglist;

		internal int thisadjust;
	}
	internal struct LeafVTShape
	{
		internal ushort count;

		internal byte[] desc;
	}
	internal struct LeafCobol0
	{
		internal uint type;

		internal byte[] data;
	}
	internal struct LeafCobol1
	{
		internal byte[] data;
	}
	internal struct LeafBArray
	{
		internal uint utype;
	}
	internal struct LeafLabel
	{
		internal ushort mode;
	}
	internal struct LeafDimArray
	{
		internal uint utype;

		internal uint diminfo;

		internal string name;
	}
	internal struct LeafVFTPath
	{
		internal uint count;

		internal uint[] bases;
	}
	internal struct LeafPreComp
	{
		internal uint start;

		internal uint count;

		internal uint signature;

		internal string name;
	}
	internal struct LeafEndPreComp
	{
		internal uint signature;
	}
	internal struct LeafOEM
	{
		internal ushort cvOEM;

		internal ushort recOEM;

		internal uint count;

		internal uint[] index;
	}
	internal enum OEM_ID
	{
		OEM_MS_FORTRAN90 = 61584,
		OEM_ODI = 16,
		OEM_THOMSON_SOFTWARE = 21587,
		OEM_ODI_REC_BASELIST = 0
	}
	internal struct LeafOEM2
	{
		internal Guid idOem;

		internal uint count;

		internal uint[] index;
	}
	internal struct LeafTypeServer
	{
		internal uint signature;

		internal uint age;

		internal string name;
	}
	internal struct LeafTypeServer2
	{
		internal Guid sig70;

		internal uint age;

		internal string name;
	}
	internal struct LeafSkip
	{
		internal uint type;

		internal byte[] data;
	}
	internal struct LeafArgList
	{
		internal uint count;

		internal uint[] arg;
	}
	internal struct LeafDerived
	{
		internal uint count;

		internal uint[] drvdcls;
	}
	internal struct LeafDefArg
	{
		internal uint type;

		internal byte[] expr;
	}
	internal struct LeafList
	{
		internal byte[] data;
	}
	internal struct LeafFieldList
	{
		internal char[] data;
	}
	internal struct mlMethod
	{
		internal ushort attr;

		internal ushort pad0;

		internal uint index;

		internal uint[] vbaseoff;
	}
	internal struct LeafMethodList
	{
		internal byte[] mList;
	}
	internal struct LeafBitfield
	{
		internal uint type;

		internal byte length;

		internal byte position;
	}
	internal struct LeafDimCon
	{
		internal uint typ;

		internal ushort rank;

		internal byte[] dim;
	}
	internal struct LeafDimVar
	{
		internal uint rank;

		internal uint typ;

		internal uint[] dim;
	}
	internal struct LeafRefSym
	{
		internal byte[] Sym;
	}
	internal struct LeafChar
	{
		internal sbyte val;
	}
	internal struct LeafShort
	{
		internal short val;
	}
	internal struct LeafUShort
	{
		internal ushort val;
	}
	internal struct LeafLong
	{
		internal int val;
	}
	internal struct LeafULong
	{
		internal uint val;
	}
	internal struct LeafQuad
	{
		internal long val;
	}
	internal struct LeafUQuad
	{
		internal ulong val;
	}
	internal struct LeafOct
	{
		internal ulong val0;

		internal ulong val1;
	}
	internal struct LeafUOct
	{
		internal ulong val0;

		internal ulong val1;
	}
	internal struct LeafReal32
	{
		internal float val;
	}
	internal struct LeafReal64
	{
		internal double val;
	}
	internal struct LeafReal80
	{
		internal FLOAT10 val;
	}
	internal struct LeafReal128
	{
		internal ulong val0;

		internal ulong val1;
	}
	internal struct LeafCmplx32
	{
		internal float val_real;

		internal float val_imag;
	}
	internal struct LeafCmplx64
	{
		internal double val_real;

		internal double val_imag;
	}
	internal struct LeafCmplx80
	{
		internal FLOAT10 val_real;

		internal FLOAT10 val_imag;
	}
	internal struct LeafCmplx128
	{
		internal ulong val0_real;

		internal ulong val1_real;

		internal ulong val0_imag;

		internal ulong val1_imag;
	}
	internal struct LeafVarString
	{
		internal ushort len;

		internal byte[] value;
	}
	internal struct LeafIndex
	{
		internal ushort pad0;

		internal uint index;
	}
	internal struct LeafBClass
	{
		internal ushort attr;

		internal uint index;

		internal byte[] offset;
	}
	internal struct LeafVBClass
	{
		internal ushort attr;

		internal uint index;

		internal uint vbptr;

		internal byte[] vbpoff;
	}
	internal struct LeafFriendCls
	{
		internal ushort pad0;

		internal uint index;
	}
	internal struct LeafFriendFcn
	{
		internal ushort pad0;

		internal uint index;

		internal string name;
	}
	internal struct LeafMember
	{
		internal ushort attr;

		internal uint index;

		internal byte[] offset;

		internal string name;
	}
	internal struct LeafSTMember
	{
		internal ushort attr;

		internal uint index;

		internal string name;
	}
	internal struct LeafVFuncTab
	{
		internal ushort pad0;

		internal uint type;
	}
	internal struct LeafVFuncOff
	{
		internal ushort pad0;

		internal uint type;

		internal int offset;
	}
	internal struct LeafMethod
	{
		internal ushort count;

		internal uint mList;

		internal string name;
	}
	internal struct LeafOneMethod
	{
		internal ushort attr;

		internal uint index;

		internal uint[] vbaseoff;

		internal string name;
	}
	internal struct LeafEnumerate
	{
		internal ushort attr;

		internal byte[] value;

		internal string name;
	}
	internal struct LeafNestType
	{
		internal ushort pad0;

		internal uint index;

		internal string name;
	}
	internal struct LeafNestTypeEx
	{
		internal ushort attr;

		internal uint index;

		internal string name;
	}
	internal struct LeafMemberModify
	{
		internal ushort attr;

		internal uint index;

		internal string name;
	}
	internal struct LeafPad
	{
		internal byte leaf;
	}
	internal enum SYM
	{
		S_END = 6,
		S_OEM = 1028,
		S_REGISTER_ST = 4097,
		S_CONSTANT_ST = 4098,
		S_UDT_ST = 4099,
		S_COBOLUDT_ST = 4100,
		S_MANYREG_ST = 4101,
		S_BPREL32_ST = 4102,
		S_LDATA32_ST = 4103,
		S_GDATA32_ST = 4104,
		S_PUB32_ST = 4105,
		S_LPROC32_ST = 4106,
		S_GPROC32_ST = 4107,
		S_VFTABLE32 = 4108,
		S_REGREL32_ST = 4109,
		S_LTHREAD32_ST = 4110,
		S_GTHREAD32_ST = 4111,
		S_LPROCMIPS_ST = 4112,
		S_GPROCMIPS_ST = 4113,
		S_FRAMEPROC = 4114,
		S_COMPILE2_ST = 4115,
		S_MANYREG2_ST = 4116,
		S_LPROCIA64_ST = 4117,
		S_GPROCIA64_ST = 4118,
		S_LOCALSLOT_ST = 4119,
		S_PARAMSLOT_ST = 4120,
		S_ANNOTATION = 4121,
		S_GMANPROC_ST = 4122,
		S_LMANPROC_ST = 4123,
		S_RESERVED1 = 4124,
		S_RESERVED2 = 4125,
		S_RESERVED3 = 4126,
		S_RESERVED4 = 4127,
		S_LMANDATA_ST = 4128,
		S_GMANDATA_ST = 4129,
		S_MANFRAMEREL_ST = 4130,
		S_MANREGISTER_ST = 4131,
		S_MANSLOT_ST = 4132,
		S_MANMANYREG_ST = 4133,
		S_MANREGREL_ST = 4134,
		S_MANMANYREG2_ST = 4135,
		S_MANTYPREF = 4136,
		S_UNAMESPACE_ST = 4137,
		S_ST_MAX = 4352,
		S_OBJNAME = 4353,
		S_THUNK32 = 4354,
		S_BLOCK32 = 4355,
		S_WITH32 = 4356,
		S_LABEL32 = 4357,
		S_REGISTER = 4358,
		S_CONSTANT = 4359,
		S_UDT = 4360,
		S_COBOLUDT = 4361,
		S_MANYREG = 4362,
		S_BPREL32 = 4363,
		S_LDATA32 = 4364,
		S_GDATA32 = 4365,
		S_PUB32 = 4366,
		S_LPROC32 = 4367,
		S_GPROC32 = 4368,
		S_REGREL32 = 4369,
		S_LTHREAD32 = 4370,
		S_GTHREAD32 = 4371,
		S_LPROCMIPS = 4372,
		S_GPROCMIPS = 4373,
		S_COMPILE2 = 4374,
		S_MANYREG2 = 4375,
		S_LPROCIA64 = 4376,
		S_GPROCIA64 = 4377,
		S_LOCALSLOT = 4378,
		S_SLOT = 4378,
		S_PARAMSLOT = 4379,
		S_LMANDATA = 4380,
		S_GMANDATA = 4381,
		S_MANFRAMEREL = 4382,
		S_MANREGISTER = 4383,
		S_MANSLOT = 4384,
		S_MANMANYREG = 4385,
		S_MANREGREL = 4386,
		S_MANMANYREG2 = 4387,
		S_UNAMESPACE = 4388,
		S_PROCREF = 4389,
		S_DATAREF = 4390,
		S_LPROCREF = 4391,
		S_ANNOTATIONREF = 4392,
		S_TOKENREF = 4393,
		S_GMANPROC = 4394,
		S_LMANPROC = 4395,
		S_TRAMPOLINE = 4396,
		S_MANCONSTANT = 4397,
		S_ATTR_FRAMEREL = 4398,
		S_ATTR_REGISTER = 4399,
		S_ATTR_REGREL = 4400,
		S_ATTR_MANYREG = 4401,
		S_SEPCODE = 4402,
		S_LOCAL = 4403,
		S_DEFRANGE = 4404,
		S_DEFRANGE2 = 4405,
		S_SECTION = 4406,
		S_COFFGROUP = 4407,
		S_EXPORT = 4408,
		S_CALLSITEINFO = 4409,
		S_FRAMECOOKIE = 4410,
		S_DISCARDED = 4411,
		S_RECTYPE_MAX = 4412,
		S_RECTYPE_LAST = 4411
	}
	internal enum CV_CFL_DATA
	{
		CV_CFL_DNEAR,
		CV_CFL_DFAR,
		CV_CFL_DHUGE
	}
	internal enum CV_CFL_CODE
	{
		CV_CFL_CNEAR,
		CV_CFL_CFAR,
		CV_CFL_CHUGE
	}
	internal enum CV_CFL_FPKG
	{
		CV_CFL_NDP,
		CV_CFL_EMU,
		CV_CFL_ALT
	}
	[Flags]
	internal enum CV_PROCFLAGS : byte
	{
		CV_PFLAG_NOFPO = 1,
		CV_PFLAG_INT = 2,
		CV_PFLAG_FAR = 4,
		CV_PFLAG_NEVER = 8,
		CV_PFLAG_NOTREACHED = 0x10,
		CV_PFLAG_CUST_CALL = 0x20,
		CV_PFLAG_NOINLINE = 0x40,
		CV_PFLAG_OPTDBGINFO = 0x80
	}
	internal struct CV_EXPROCFLAGS
	{
		internal byte flags;

		internal byte reserved;
	}
	[Flags]
	internal enum CV_LVARFLAGS : ushort
	{
		fIsParam = 1,
		fAddrTaken = 2,
		fCompGenx = 4,
		fIsAggregate = 8,
		fIsAggregated = 0x10,
		fIsAliased = 0x20,
		fIsAlias = 0x40
	}
	internal struct CV_lvar_addr_range
	{
		internal uint offStart;

		internal ushort isectStart;

		internal uint cbRange;
	}
	internal enum CV_GENERIC_STYLE
	{
		CV_GENERIC_VOID,
		CV_GENERIC_REG,
		CV_GENERIC_ICAN,
		CV_GENERIC_ICAF,
		CV_GENERIC_IRAN,
		CV_GENERIC_IRAF,
		CV_GENERIC_UNUSED
	}
	[Flags]
	internal enum CV_GENERIC_FLAG : ushort
	{
		cstyle = 1,
		rsclean = 2
	}
	[Flags]
	internal enum CV_SEPCODEFLAGS : uint
	{
		fIsLexicalScope = 1u,
		fReturnsToParent = 2u
	}
	internal struct SYMTYPE
	{
		internal ushort reclen;

		internal ushort rectyp;
	}
	internal struct RegSym
	{
		internal uint typind;

		internal ushort reg;

		internal string name;
	}
	internal struct AttrRegSym
	{
		internal uint typind;

		internal uint offCod;

		internal ushort segCod;

		internal ushort flags;

		internal ushort reg;

		internal string name;
	}
	internal struct ManyRegSym
	{
		internal uint typind;

		internal byte count;

		internal byte[] reg;

		internal string name;
	}
	internal struct ManyRegSym2
	{
		internal uint typind;

		internal ushort count;

		internal ushort[] reg;

		internal string name;
	}
	internal struct AttrManyRegSym
	{
		internal uint typind;

		internal uint offCod;

		internal ushort segCod;

		internal ushort flags;

		internal byte count;

		internal byte[] reg;

		internal string name;
	}
	internal struct AttrManyRegSym2
	{
		internal uint typind;

		internal uint offCod;

		internal ushort segCod;

		internal ushort flags;

		internal ushort count;

		internal ushort[] reg;

		internal string name;
	}
	internal struct ConstSym
	{
		internal uint typind;

		internal ushort value;

		internal string name;
	}
	internal struct UdtSym
	{
		internal uint typind;

		internal string name;
	}
	internal struct ManyTypRef
	{
		internal uint typind;
	}
	internal struct SearchSym
	{
		internal uint startsym;

		internal ushort seg;
	}
	[Flags]
	internal enum CFLAGSYM_FLAGS : ushort
	{
		pcode = 1,
		floatprec = 6,
		floatpkg = 0x18,
		ambdata = 0xE0,
		ambcode = 0x700,
		mode32 = 0x800
	}
	internal struct CFlagSym
	{
		internal byte machine;

		internal byte language;

		internal ushort flags;

		internal string ver;
	}
	[Flags]
	internal enum COMPILESYM_FLAGS : uint
	{
		iLanguage = 0xFFu,
		fEC = 0x100u,
		fNoDbgInfo = 0x200u,
		fLTCG = 0x400u,
		fNoDataAlign = 0x800u,
		fManagedPresent = 0x1000u,
		fSecurityChecks = 0x2000u,
		fHotPatch = 0x4000u,
		fCVTCIL = 0x8000u,
		fMSILModule = 0x10000u
	}
	internal struct CompileSym
	{
		internal uint flags;

		internal ushort machine;

		internal ushort verFEMajor;

		internal ushort verFEMinor;

		internal ushort verFEBuild;

		internal ushort verMajor;

		internal ushort verMinor;

		internal ushort verBuild;

		internal string verSt;

		internal string[] verArgs;
	}
	internal struct ObjNameSym
	{
		internal uint signature;

		internal string name;
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct EndArgSym
	{
	}
	internal struct ReturnSym
	{
		internal CV_GENERIC_FLAG flags;

		internal byte style;
	}
	internal struct EntryThisSym
	{
		internal byte thissym;
	}
	internal struct BpRelSym32
	{
		internal int off;

		internal uint typind;

		internal string name;
	}
	internal struct FrameRelSym
	{
		internal int off;

		internal uint typind;

		internal uint offCod;

		internal ushort segCod;

		internal ushort flags;

		internal string name;
	}
	internal struct SlotSym32
	{
		internal uint index;

		internal uint typind;

		internal string name;
	}
	internal struct AttrSlotSym
	{
		internal uint index;

		internal uint typind;

		internal uint offCod;

		internal ushort segCod;

		internal ushort flags;

		internal string name;
	}
	internal struct AnnotationSym
	{
		internal uint off;

		internal ushort seg;

		internal ushort csz;

		internal string[] rgsz;
	}
	internal struct DatasSym32
	{
		internal uint typind;

		internal uint off;

		internal ushort seg;

		internal string name;
	}
	[Flags]
	internal enum CV_PUBSYMFLAGS : uint
	{
		fNone = 0u,
		fCode = 1u,
		fFunction = 2u,
		fManaged = 4u,
		fMSIL = 8u
	}
	internal struct PubSym32
	{
		internal uint flags;

		internal uint off;

		internal ushort seg;

		internal string name;
	}
	internal struct ProcSym32
	{
		internal uint parent;

		internal uint end;

		internal uint next;

		internal uint len;

		internal uint dbgStart;

		internal uint dbgEnd;

		internal uint typind;

		internal uint off;

		internal ushort seg;

		internal byte flags;

		internal string name;
	}
	internal struct ManProcSym
	{
		internal uint parent;

		internal uint end;

		internal uint next;

		internal uint len;

		internal uint dbgStart;

		internal uint dbgEnd;

		internal uint token;

		internal uint off;

		internal ushort seg;

		internal byte flags;

		internal ushort retReg;

		internal string name;
	}
	internal struct ManProcSymMips
	{
		internal uint parent;

		internal uint end;

		internal uint next;

		internal uint len;

		internal uint dbgStart;

		internal uint dbgEnd;

		internal uint regSave;

		internal uint fpSave;

		internal uint intOff;

		internal uint fpOff;

		internal uint token;

		internal uint off;

		internal ushort seg;

		internal byte retReg;

		internal byte frameReg;

		internal string name;
	}
	internal struct ThunkSym32
	{
		internal uint parent;

		internal uint end;

		internal uint next;

		internal uint off;

		internal ushort seg;

		internal ushort len;

		internal byte ord;

		internal string name;

		internal byte[] variant;
	}
	internal enum TRAMP
	{
		trampIncremental,
		trampBranchIsland
	}
	internal struct TrampolineSym
	{
		internal ushort trampType;

		internal ushort cbThunk;

		internal uint offThunk;

		internal uint offTarget;

		internal ushort sectThunk;

		internal ushort sectTarget;
	}
	internal struct LabelSym32
	{
		internal uint off;

		internal ushort seg;

		internal byte flags;

		internal string name;
	}
	internal struct BlockSym32
	{
		internal uint parent;

		internal uint end;

		internal uint len;

		internal uint off;

		internal ushort seg;

		internal string name;
	}
	internal struct WithSym32
	{
		internal uint parent;

		internal uint end;

		internal uint len;

		internal uint off;

		internal ushort seg;

		internal string expr;
	}
	internal struct VpathSym32
	{
		internal uint root;

		internal uint path;

		internal uint off;

		internal ushort seg;
	}
	internal struct RegRel32
	{
		internal uint off;

		internal uint typind;

		internal ushort reg;

		internal string name;
	}
	internal struct AttrRegRel
	{
		internal uint off;

		internal uint typind;

		internal ushort reg;

		internal uint offCod;

		internal ushort segCod;

		internal ushort flags;

		internal string name;
	}
	internal struct ThreadSym32
	{
		internal uint typind;

		internal uint off;

		internal ushort seg;

		internal string name;
	}
	internal struct Slink32
	{
		internal uint framesize;

		internal int off;

		internal ushort reg;
	}
	internal struct ProcSymMips
	{
		internal uint parent;

		internal uint end;

		internal uint next;

		internal uint len;

		internal uint dbgStart;

		internal uint dbgEnd;

		internal uint regSave;

		internal uint fpSave;

		internal uint intOff;

		internal uint fpOff;

		internal uint typind;

		internal uint off;

		internal ushort seg;

		internal byte retReg;

		internal byte frameReg;

		internal string name;
	}
	internal struct ProcSymIa64
	{
		internal uint parent;

		internal uint end;

		internal uint next;

		internal uint len;

		internal uint dbgStart;

		internal uint dbgEnd;

		internal uint typind;

		internal uint off;

		internal ushort seg;

		internal ushort retReg;

		internal byte flags;

		internal string name;
	}
	internal struct RefSym
	{
		internal uint sumName;

		internal uint ibSym;

		internal ushort imod;

		internal ushort usFill;
	}
	internal struct RefSym2
	{
		internal uint sumName;

		internal uint ibSym;

		internal ushort imod;

		internal string name;
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct AlignSym
	{
	}
	internal struct OemSymbol
	{
		internal Guid idOem;

		internal uint typind;

		internal byte[] rgl;
	}
	[Flags]
	internal enum FRAMEPROCSYM_FLAGS : uint
	{
		fHasAlloca = 1u,
		fHasSetJmp = 2u,
		fHasLongJmp = 4u,
		fHasInlAsm = 8u,
		fHasEH = 0x10u,
		fInlSpec = 0x20u,
		fHasSEH = 0x40u,
		fNaked = 0x80u,
		fSecurityChecks = 0x100u,
		fAsyncEH = 0x200u,
		fGSNoStackOrdering = 0x400u,
		fWasInlined = 0x800u
	}
	internal struct FrameProcSym
	{
		internal uint cbFrame;

		internal uint cbPad;

		internal uint offPad;

		internal uint cbSaveRegs;

		internal uint offExHdlr;

		internal ushort secExHdlr;

		internal uint flags;
	}
	internal struct UnamespaceSym
	{
		internal string name;
	}
	internal struct SepCodSym
	{
		internal uint parent;

		internal uint end;

		internal uint length;

		internal uint scf;

		internal uint off;

		internal uint offParent;

		internal ushort sec;

		internal ushort secParent;
	}
	internal struct LocalSym
	{
		internal uint id;

		internal uint typind;

		internal ushort flags;

		internal uint idParent;

		internal uint offParent;

		internal uint expr;

		internal uint pad0;

		internal uint pad1;

		internal string name;
	}
	internal struct DefRangeSym
	{
		internal uint id;

		internal uint program;

		internal CV_lvar_addr_range range;
	}
	internal struct DefRangeSym2
	{
		internal uint id;

		internal uint program;

		internal ushort count;

		internal CV_lvar_addr_range[] range;
	}
	internal struct SectionSym
	{
		internal ushort isec;

		internal byte align;

		internal byte bReserved;

		internal uint rva;

		internal uint cb;

		internal uint characteristics;

		internal string name;
	}
	internal struct CoffGroupSym
	{
		internal uint cb;

		internal uint characteristics;

		internal uint off;

		internal ushort seg;

		internal string name;
	}
	[Flags]
	internal enum EXPORTSYM_FLAGS : ushort
	{
		fConstant = 1,
		fData = 2,
		fPrivate = 4,
		fNoName = 8,
		fOrdinal = 0x10,
		fForwarder = 0x20
	}
	internal struct ExportSym
	{
		internal ushort ordinal;

		internal ushort flags;

		internal string name;
	}
	internal struct CallsiteInfo
	{
		internal int off;

		internal ushort ect;

		internal ushort pad0;

		internal uint typind;
	}
	internal enum CV_cookietype
	{
		CV_COOKIETYPE_COPY,
		CV_COOKIETYPE_XOR_SP,
		CV_COOKIETYPE_XOR_BP,
		CV_COOKIETYPE_XOR_R13
	}
	internal struct FrameCookie
	{
		internal int off;

		internal ushort reg;

		internal int cookietype;

		internal byte flags;
	}
	internal enum CV_DISCARDED : uint
	{
		CV_DISCARDED_UNKNOWN,
		CV_DISCARDED_NOT_SELECTED,
		CV_DISCARDED_NOT_REFERENCED
	}
	internal struct DiscardedSym
	{
		internal CV_DISCARDED iscarded;

		internal uint fileid;

		internal uint linenum;

		internal byte[] data;
	}
	internal enum DEBUG_S_SUBSECTION_TYPE : uint
	{
		DEBUG_S_IGNORE = 2147483648u,
		DEBUG_S_SYMBOLS = 241u,
		DEBUG_S_LINES = 242u,
		DEBUG_S_STRINGTABLE = 243u,
		DEBUG_S_FILECHKSMS = 244u,
		DEBUG_S_FRAMEDATA = 245u
	}
	internal enum CV_LINE_SUBSECTION_FLAGS : ushort
	{
		CV_LINES_HAVE_COLUMNS = 1
	}
	internal struct CV_LineSection
	{
		internal uint off;

		internal ushort sec;

		internal ushort flags;

		internal uint cod;
	}
	internal struct CV_SourceFile
	{
		internal uint index;

		internal uint count;

		internal uint linsiz;
	}
	[Flags]
	internal enum CV_Line_Flags : uint
	{
		linenumStart = 0xFFFFFFu,
		deltaLineEnd = 0x7F000000u,
		fStatement = 0x80000000u
	}
	internal struct CV_Line
	{
		internal uint offset;

		internal uint flags;
	}
	internal struct CV_Column
	{
		internal ushort offColumnStart;

		internal ushort offColumnEnd;
	}
	internal enum CV_FILE_CHECKSUM_TYPE : byte
	{
		None,
		MD5
	}
	internal struct CV_FileCheckSum
	{
		internal uint name;

		internal byte len;

		internal byte type;
	}
	[Flags]
	internal enum FRAMEDATA_FLAGS : uint
	{
		fHasSEH = 1u,
		fHasEH = 2u,
		fIsFunctionStart = 4u
	}
	internal struct FrameData
	{
		internal uint ulRvaStart;

		internal uint cbBlock;

		internal uint cbLocals;

		internal uint cbParams;

		internal uint cbStkMax;

		internal uint frameFunc;

		internal ushort cbProlog;

		internal ushort cbSavedRegs;

		internal uint flags;
	}
	internal struct XFixupData
	{
		internal ushort wType;

		internal ushort wExtra;

		internal uint rva;

		internal uint rvaTarget;
	}
	internal enum DEBUG_S_SUBSECTION
	{
		SYMBOLS = 241,
		LINES,
		STRINGTABLE,
		FILECHKSMS,
		FRAMEDATA
	}
	internal class DataStream
	{
		internal int contentSize;

		internal int[] pages;

		internal int Length => contentSize;

		internal DataStream()
		{
		}

		internal DataStream(int contentSize, BitAccess bits, int count)
		{
			this.contentSize = contentSize;
			if (count > 0)
			{
				pages = new int[count];
				bits.ReadInt32(pages);
			}
		}

		internal void Read(PdbReader reader, BitAccess bits)
		{
			bits.MinCapacity(contentSize);
			Read(reader, 0, bits.Buffer, 0, contentSize);
		}

		internal void Read(PdbReader reader, int position, byte[] bytes, int offset, int data)
		{
			if (position + data > contentSize)
			{
				throw new PdbException("DataStream can't read off end of stream. (pos={0},siz={1})", position, data);
			}
			if (position == contentSize)
			{
				return;
			}
			int num = data;
			int num2 = position / reader.pageSize;
			int num3 = position % reader.pageSize;
			if (num3 != 0)
			{
				int num4 = reader.pageSize - num3;
				if (num4 > num)
				{
					num4 = num;
				}
				reader.Seek(pages[num2], num3);
				reader.Read(bytes, offset, num4);
				offset += num4;
				num -= num4;
				num2++;
			}
			while (num > 0)
			{
				int num5 = reader.pageSize;
				if (num5 > num)
				{
					num5 = num;
				}
				reader.Seek(pages[num2], 0);
				reader.Read(bytes, offset, num5);
				offset += num5;
				num -= num5;
				num2++;
			}
		}
	}
	internal struct DbiDbgHdr
	{
		internal ushort snFPO;

		internal ushort snException;

		internal ushort snFixup;

		internal ushort snOmapToSrc;

		internal ushort snOmapFromSrc;

		internal ushort snSectionHdr;

		internal ushort snTokenRidMap;

		internal ushort snXdata;

		internal ushort snPdata;

		internal ushort snNewFPO;

		internal ushort snSectionHdrOrig;

		internal DbiDbgHdr(BitAccess bits)
		{
			bits.ReadUInt16(out snFPO);
			bits.ReadUInt16(out snException);
			bits.ReadUInt16(out snFixup);
			bits.ReadUInt16(out snOmapToSrc);
			bits.ReadUInt16(out snOmapFromSrc);
			bits.ReadUInt16(out snSectionHdr);
			bits.ReadUInt16(out snTokenRidMap);
			bits.ReadUInt16(out snXdata);
			bits.ReadUInt16(out snPdata);
			bits.ReadUInt16(out snNewFPO);
			bits.ReadUInt16(out snSectionHdrOrig);
		}
	}
	internal struct DbiHeader
	{
		internal int sig;

		internal int ver;

		internal int age;

		internal short gssymStream;

		internal ushort vers;

		internal short pssymStream;

		internal ushort pdbver;

		internal short symrecStream;

		internal ushort pdbver2;

		internal int gpmodiSize;

		internal int secconSize;

		internal int secmapSize;

		internal int filinfSize;

		internal int tsmapSize;

		internal int mfcIndex;

		internal int dbghdrSize;

		internal int ecinfoSize;

		internal ushort flags;

		internal ushort machine;

		internal int reserved;

		internal DbiHeader(BitAccess bits)
		{
			bits.ReadInt32(out sig);
			bits.ReadInt32(out ver);
			bits.ReadInt32(out age);
			bits.ReadInt16(out gssymStream);
			bits.ReadUInt16(out vers);
			bits.ReadInt16(out pssymStream);
			bits.ReadUInt16(out pdbver);
			bits.ReadInt16(out symrecStream);
			bits.ReadUInt16(out pdbver2);
			bits.ReadInt32(out gpmodiSize);
			bits.ReadInt32(out secconSize);
			bits.ReadInt32(out secmapSize);
			bits.ReadInt32(out filinfSize);
			bits.ReadInt32(out tsmapSize);
			bits.ReadInt32(out mfcIndex);
			bits.ReadInt32(out dbghdrSize);
			bits.ReadInt32(out ecinfoSize);
			bits.ReadUInt16(out flags);
			bits.ReadUInt16(out machine);
			bits.ReadInt32(out reserved);
		}
	}
	internal class DbiModuleInfo
	{
		internal int opened;

		internal ushort flags;

		internal short stream;

		internal int cbSyms;

		internal int cbOldLines;

		internal int cbLines;

		internal short files;

		internal short pad1;

		internal uint offsets;

		internal int niSource;

		internal int niCompiler;

		internal string moduleName;

		internal string objectName;

		internal DbiModuleInfo(BitAccess bits, bool readStrings)
		{
			bits.ReadInt32(out opened);
			new DbiSecCon(bits);
			bits.ReadUInt16(out flags);
			bits.ReadInt16(out stream);
			bits.ReadInt32(out cbSyms);
			bits.ReadInt32(out cbOldLines);
			bits.ReadInt32(out cbLines);
			bits.ReadInt16(out files);
			bits.ReadInt16(out pad1);
			bits.ReadUInt32(out offsets);
			bits.ReadInt32(out niSource);
			bits.ReadInt32(out niCompiler);
			if (readStrings)
			{
				bits.ReadCString(out moduleName);
				bits.ReadCString(out objectName);
			}
			else
			{
				bits.SkipCString(out moduleName);
				bits.SkipCString(out objectName);
			}
			bits.Align(4);
		}
	}
	internal struct DbiSecCon
	{
		internal short section;

		internal short pad1;

		internal int offset;

		internal int size;

		internal uint flags;

		internal short module;

		internal short pad2;

		internal uint dataCrc;

		internal uint relocCrc;

		internal DbiSecCon(BitAccess bits)
		{
			bits.ReadInt16(out section);
			bits.ReadInt16(out pad1);
			bits.ReadInt32(out offset);
			bits.ReadInt32(out size);
			bits.ReadUInt32(out flags);
			bits.ReadInt16(out module);
			bits.ReadInt16(out pad2);
			bits.ReadUInt32(out dataCrc);
			bits.ReadUInt32(out relocCrc);
		}
	}
	internal interface ILocalScope
	{
		uint Offset { get; }

		uint Length { get; }
	}
	internal interface INamespaceScope
	{
		IEnumerable<IUsedNamespace> UsedNamespaces { get; }
	}
	internal interface IUsedNamespace
	{
		IName Alias { get; }

		IName NamespaceName { get; }
	}
	internal interface IName
	{
		int UniqueKey { get; }

		int UniqueKeyIgnoringCase { get; }

		string Value { get; }
	}
	internal class IntHashTable
	{
		private struct bucket
		{
			internal int key;

			internal int hash_coll;

			internal object val;
		}

		private static readonly int[] primes = new int[72]
		{
			3, 7, 11, 17, 23, 29, 37, 47, 59, 71,
			89, 107, 131, 163, 197, 239, 293, 353, 431, 521,
			631, 761, 919, 1103, 1327, 1597, 1931, 2333, 2801, 3371,
			4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591, 17519, 21023,
			25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363,
			156437, 187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403,
			968897, 1162687, 1395263, 1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559,
			5999471, 7199369
		};

		private bucket[] buckets;

		private int count;

		private int occupancy;

		private int loadsize;

		private int loadFactorPerc;

		private int version;

		internal object this[int key]
		{
			get
			{
				if (key < 0)
				{
					throw new ArgumentException("Argument_KeyLessThanZero");
				}
				bucket[] array = buckets;
				uint seed;
				uint incr;
				uint num = InitHash(key, array.Length, out seed, out incr);
				int num2 = 0;
				bucket bucket;
				do
				{
					int num3 = (int)(seed % (uint)array.Length);
					bucket = array[num3];
					if (bucket.val == null)
					{
						return null;
					}
					if ((bucket.hash_coll & 0x7FFFFFFF) == num && key == bucket.key)
					{
						return bucket.val;
					}
					seed += incr;
				}
				while (bucket.hash_coll < 0 && ++num2 < array.Length);
				return null;
			}
		}

		private static int GetPrime(int minSize)
		{
			if (minSize < 0)
			{
				throw new ArgumentException("Arg_HTCapacityOverflow");
			}
			for (int i = 0; i < primes.Length; i++)
			{
				int num = primes[i];
				if (num >= minSize)
				{
					return num;
				}
			}
			throw new ArgumentException("Arg_HTCapacityOverflow");
		}

		internal IntHashTable()
			: this(0, 100)
		{
		}

		internal IntHashTable(int capacity, int loadFactorPerc)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "ArgumentOutOfRange_NeedNonNegNum");
			}
			if (loadFactorPerc < 10 || loadFactorPerc > 100)
			{
				throw new ArgumentOutOfRangeException("loadFactorPerc", string.Format("ArgumentOutOfRange_IntHashTableLoadFactor", 10, 100));
			}
			this.loadFactorPerc = loadFactorPerc * 72 / 100;
			int prime = GetPrime(capacity / this.loadFactorPerc);
			buckets = new bucket[prime];
			loadsize = this.loadFactorPerc * prime / 100;
			if (loadsize >= prime)
			{
				loadsize = prime - 1;
			}
		}

		private static uint InitHash(int key, int hashsize, out uint seed, out uint incr)
		{
			uint result = (seed = (uint)(key & 0x7FFFFFFF));
			incr = 1 + ((seed >> 5) + 1) % (uint)(hashsize - 1);
			return result;
		}

		internal void Add(int key, object value)
		{
			Insert(key, value, add: true);
		}

		private void expand()
		{
			rehash(GetPrime(1 + buckets.Length * 2));
		}

		private void rehash()
		{
			rehash(buckets.Length);
		}

		private void rehash(int newsize)
		{
			occupancy = 0;
			bucket[] newBuckets = new bucket[newsize];
			for (int i = 0; i < buckets.Length; i++)
			{
				bucket bucket = buckets[i];
				if (bucket.val != null)
				{
					putEntry(newBuckets, bucket.key, bucket.val, bucket.hash_coll & 0x7FFFFFFF);
				}
			}
			version++;
			buckets = newBuckets;
			loadsize = loadFactorPerc * newsize / 100;
			if (loadsize >= newsize)
			{
				loadsize = newsize - 1;
			}
		}

		private void Insert(int key, object nvalue, bool add)
		{
			if (key < 0)
			{
				throw new ArgumentException("Argument_KeyLessThanZero");
			}
			if (nvalue == null)
			{
				throw new ArgumentNullException("nvalue", "ArgumentNull_Value");
			}
			if (count >= loadsize)
			{
				expand();
			}
			else if (occupancy > loadsize && count > 100)
			{
				rehash();
			}
			uint seed;
			uint incr;
			uint num = InitHash(key, buckets.Length, out seed, out incr);
			int num2 = 0;
			int num3 = -1;
			do
			{
				int num4 = (int)(seed % (uint)buckets.Length);
				if (buckets[num4].val == null)
				{
					if (num3 != -1)
					{
						num4 = num3;
					}
					buckets[num4].val = nvalue;
					buckets[num4].key = key;
					buckets[num4].hash_coll |= (int)num;
					count++;
					version++;
					return;
				}
				if ((buckets[num4].hash_coll & 0x7FFFFFFF) == num && key == buckets[num4].key)
				{
					if (add)
					{
						throw new ArgumentException("Argument_AddingDuplicate__" + buckets[num4].key);
					}
					buckets[num4].val = nvalue;
					version++;
					return;
				}
				if (num3 == -1 && buckets[num4].hash_coll >= 0)
				{
					buckets[num4].hash_coll |= int.MinValue;
					occupancy++;
				}
				seed += incr;
			}
			while (++num2 < buckets.Length);
			if (num3 != -1)
			{
				buckets[num3].val = nvalue;
				buckets[num3].key = key;
				buckets[num3].hash_coll |= (int)num;
				count++;
				version++;
				return;
			}
			throw new InvalidOperationException("InvalidOperation_HashInsertFailed");
		}

		private void putEntry(bucket[] newBuckets, int key, object nvalue, int hashcode)
		{
			uint num = (uint)hashcode;
			uint num2 = 1 + ((num >> 5) + 1) % (uint)(newBuckets.Length - 1);
			int num3;
			while (true)
			{
				num3 = (int)(num % (uint)newBuckets.Length);
				if (newBuckets[num3].val == null)
				{
					break;
				}
				if (newBuckets[num3].hash_coll >= 0)
				{
					newBuckets[num3].hash_coll |= int.MinValue;
					occupancy++;
				}
				num += num2;
				bool flag = true;
			}
			newBuckets[num3].val = nvalue;
			newBuckets[num3].key = key;
			newBuckets[num3].hash_coll |= hashcode;
		}
	}
	internal class MsfDirectory
	{
		internal DataStream[] streams;

		internal MsfDirectory(PdbReader reader, PdbFileHeader head, BitAccess bits)
		{
			int num = reader.PagesFromSize(head.directorySize);
			bits.MinCapacity(head.directorySize);
			int num2 = head.directoryRoot.Length;
			int num3 = head.pageSize / 4;
			int num4 = num;
			for (int i = 0; i < num2; i++)
			{
				int num5 = ((num4 <= num3) ? num4 : num3);
				reader.Seek(head.directoryRoot[i], 0);
				bits.Append(reader.reader, num5 * 4);
				num4 -= num5;
			}
			bits.Position = 0;
			DataStream dataStream = new DataStream(head.directorySize, bits, num);
			bits.MinCapacity(head.directorySize);
			dataStream.Read(reader, bits);
			bits.ReadInt32(out var value);
			int[] array = new int[value];
			bits.ReadInt32(array);
			streams = new DataStream[value];
			for (int j = 0; j < value; j++)
			{
				if (array[j] <= 0)
				{
					streams[j] = new DataStream();
				}
				else
				{
					streams[j] = new DataStream(array[j], bits, reader.PagesFromSize(array[j]));
				}
			}
		}
	}
	internal class PdbConstant
	{
		internal string name;

		internal uint token;

		internal object value;

		internal PdbConstant(string name, uint token, object value)
		{
			this.name = name;
			this.token = token;
			this.value = value;
		}

		internal PdbConstant(BitAccess bits)
		{
			bits.ReadUInt32(out token);
			bits.ReadUInt8(out var b);
			bits.ReadUInt8(out var b2);
			switch (b2)
			{
			case 0:
				value = b;
				break;
			case 128:
				switch (b)
				{
				case 0:
				{
					bits.ReadInt8(out var b3);
					value = b3;
					break;
				}
				case 1:
				{
					bits.ReadInt16(out var num6);
					value = num6;
					break;
				}
				case 2:
				{
					bits.ReadUInt16(out var num5);
					value = num5;
					break;
				}
				case 3:
				{
					bits.ReadInt32(out var num4);
					value = num4;
					break;
				}
				case 4:
				{
					bits.ReadUInt32(out var num3);
					value = num3;
					break;
				}
				case 5:
					value = bits.ReadFloat();
					break;
				case 6:
					value = bits.ReadDouble();
					break;
				case 9:
				{
					bits.ReadInt64(out var num2);
					value = num2;
					break;
				}
				case 10:
				{
					bits.ReadUInt64(out var num);
					value = num;
					break;
				}
				case 16:
				{
					bits.ReadBString(out var text);
					value = text;
					break;
				}
				case 25:
					value = bits.ReadDecimal();
					break;
				}
				break;
			}
			bits.ReadCString(out name);
		}
	}
	internal class PdbDebugException : IOException
	{
		internal PdbDebugException(string format, params object[] args)
			: base(string.Format(format, args))
		{
		}
	}
	internal class PdbException : IOException
	{
		internal PdbException(string format, params object[] args)
			: base(string.Format(format, args))
		{
		}
	}
	internal class PdbFile
	{
		private static readonly Guid BasicLanguageGuid = new Guid(974311608, -15764, 4560, 180, 66, 0, 160, 36, 74, 29, 210);

		public static readonly Guid SymDocumentType_Text = new Guid(1518771467, 26129, 4563, 189, 42, 0, 0, 248, 8, 73, 189);

		private PdbFile()
		{
		}

		private static void LoadInjectedSourceInformation(BitAccess bits, out Guid doctype, out Guid language, out Guid vendor, out Guid checksumAlgo, out byte[] checksum)
		{
			checksum = null;
			bits.ReadGuid(out language);
			bits.ReadGuid(out vendor);
			bits.ReadGuid(out doctype);
			bits.ReadGuid(out checksumAlgo);
			bits.ReadInt32(out var value);
			bits.ReadInt32(out var _);
			if (value > 0)
			{
				checksum = new byte[value];
				bits.ReadBytes(checksum);
			}
		}

		private static Dictionary<string, int> LoadNameIndex(BitAccess bits, out int age, out Guid guid)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			bits.ReadInt32(out var _);
			bits.ReadInt32(out var _);
			bits.ReadInt32(out age);
			bits.ReadGuid(out guid);
			bits.ReadInt32(out var value3);
			int position = bits.Position;
			int position2 = bits.Position + value3;
			bits.Position = position2;
			bits.ReadInt32(out var value4);
			bits.ReadInt32(out var value5);
			BitSet bitSet = new BitSet(bits);
			BitSet bitSet2 = new BitSet(bits);
			int num = 0;
			for (int i = 0; i < value5; i++)
			{
				if (bitSet.IsSet(i))
				{
					bits.ReadInt32(out var value6);
					bits.ReadInt32(out var value7);
					int position3 = bits.Position;
					bits.Position = position + value6;
					bits.ReadCString(out var value8);
					bits.Position = position3;
					dictionary.Add(value8.ToUpperInvariant(), value7);
					num++;
				}
			}
			if (num != value4)
			{
				throw new PdbDebugException("Count mismatch. ({0} != {1})", num, value4);
			}
			return dictionary;
		}

		private static IntHashTable LoadNameStream(BitAccess bits)
		{
			IntHashTable intHashTable = new IntHashTable();
			bits.ReadUInt32(out var value);
			bits.ReadInt32(out var value2);
			bits.ReadInt32(out var value3);
			if (value != 4026462206u || value2 != 1)
			{
				throw new PdbDebugException("Unsupported Name Stream version. (sig={0:x8}, ver={1})", value, value2);
			}
			int position = bits.Position;
			int position2 = bits.Position + value3;
			bits.Position = position2;
			bits.ReadInt32(out var value4);
			position2 = bits.Position;
			for (int i = 0; i < value4; i++)
			{
				bits.ReadInt32(out var value5);
				if (value5 != 0)
				{
					int position3 = bits.Position;
					bits.Position = position + value5;
					bits.ReadCString(out var value6);
					bits.Position = position3;
					intHashTable.Add(value5, value6);
				}
			}
			bits.Position = position2;
			return intHashTable;
		}

		private static int FindFunction(PdbFunction[] funcs, ushort sec, uint off)
		{
			PdbFunction value = new PdbFunction
			{
				segment = sec,
				address = off
			};
			return Array.BinarySearch(funcs, value, PdbFunction.byAddress);
		}

		private static void LoadManagedLines(PdbFunction[] funcs, IntHashTable names, BitAccess bits, MsfDirectory dir, Dictionary<string, int> nameIndex, PdbReader reader, uint limit, Dictionary<string, PdbSource> sourceCache)
		{
			Array.Sort(funcs, PdbFunction.byAddressAndToken);
			int position = bits.Position;
			IntHashTable intHashTable = ReadSourceFileInfo(bits, limit, names, dir, nameIndex, reader, sourceCache);
			bits.Position = position;
			CV_LineSection cV_LineSection = default(CV_LineSection);
			CV_SourceFile cV_SourceFile = default(CV_SourceFile);
			CV_SourceFile cV_SourceFile2 = default(CV_SourceFile);
			CV_Line cV_Line = default(CV_Line);
			while (bits.Position < limit)
			{
				bits.ReadInt32(out var value);
				bits.ReadInt32(out var value2);
				int num = bits.Position + value2;
				DEBUG_S_SUBSECTION dEBUG_S_SUBSECTION = (DEBUG_S_SUBSECTION)value;
				DEBUG_S_SUBSECTION dEBUG_S_SUBSECTION2 = dEBUG_S_SUBSECTION;
				if (dEBUG_S_SUBSECTION2 == DEBUG_S_SUBSECTION.LINES)
				{
					bits.ReadUInt32(out cV_LineSection.off);
					bits.ReadUInt16(out cV_LineSection.sec);
					bits.ReadUInt16(out cV_LineSection.flags);
					bits.ReadUInt32(out cV_LineSection.cod);
					int i = FindFunction(funcs, cV_LineSection.sec, cV_LineSection.off);
					if (i >= 0)
					{
						PdbFunction pdbFunction = funcs[i];
						if (pdbFunction.lines == null)
						{
							while (i > 0)
							{
								PdbFunction pdbFunction2 = funcs[i - 1];
								if (pdbFunction2.lines != null || pdbFunction2.segment != cV_LineSection.sec || pdbFunction2.address != cV_LineSection.off)
								{
									break;
								}
								pdbFunction = pdbFunction2;
								i--;
							}
						}
						else
						{
							for (; i < funcs.Length - 1; i++)
							{
								if (pdbFunction.lines == null)
								{
									break;
								}
								PdbFunction pdbFunction3 = funcs[i + 1];
								if (pdbFunction3.segment != cV_LineSection.sec || pdbFunction3.address != cV_LineSection.off)
								{
									break;
								}
								pdbFunction = pdbFunction3;
							}
						}
						if (pdbFunction.lines == null)
						{
							int position2 = bits.Position;
							int num2 = 0;
							while (bits.Position < num)
							{
								bits.ReadUInt32(out cV_SourceFile.index);
								bits.ReadUInt32(out cV_SourceFile.count);
								bits.ReadUInt32(out cV_SourceFile.linsiz);
								int num3 = (int)cV_SourceFile.count * (8 + (((cV_LineSection.flags & 1) != 0) ? 4 : 0));
								bits.Position += num3;
								num2++;
							}
							pdbFunction.lines = new PdbLines[num2];
							int num4 = 0;
							bits.Position = position2;
							while (bits.Position < num)
							{
								bits.ReadUInt32(out cV_SourceFile2.index);
								bits.ReadUInt32(out cV_SourceFile2.count);
								bits.ReadUInt32(out cV_SourceFile2.linsiz);
								PdbSource pdbSource = (PdbSource)intHashTable[(int)cV_SourceFile2.index];
								if (pdbSource.language.Equals(BasicLanguageGuid))
								{
									pdbFunction.AdjustVisualBasicScopes();
								}
								PdbLines pdbLines = new PdbLines(pdbSource, cV_SourceFile2.count);
								pdbFunction.lines[num4++] = pdbLines;
								PdbLine[] lines = pdbLines.lines;
								int position3 = bits.Position;
								int num5 = bits.Position + (int)(8 * cV_SourceFile2.count);
								for (int j = 0; j < cV_SourceFile2.count; j++)
								{
									CV_Column cV_Column = default(CV_Column);
									bits.Position = position3 + 8 * j;
									bits.ReadUInt32(out cV_Line.offset);
									bits.ReadUInt32(out cV_Line.flags);
									uint num6 = cV_Line.flags & 0xFFFFFF;
									uint num7 = (cV_Line.flags & 0x7F000000) >> 24;
									if ((cV_LineSection.flags & 1) != 0)
									{
										bits.Position = num5 + 4 * j;
										bits.ReadUInt16(out cV_Column.offColumnStart);
										bits.ReadUInt16(out cV_Column.offColumnEnd);
									}
									lines[j] = new PdbLine(cV_Line.offset, num6, cV_Column.offColumnStart, num6 + num7, cV_Column.offColumnEnd);
								}
							}
						}
					}
				}
				bits.Position = num;
			}
		}

		private static void LoadFuncsFromDbiModule(BitAccess bits, DbiModuleInfo info, IntHashTable names, List<PdbFunction> funcList, bool readStrings, MsfDirectory dir, Dictionary<string, int> nameIndex, PdbReader reader, Dictionary<string, PdbSource> sourceCache)
		{
			PdbFunction[] array = null;
			bits.Position = 0;
			bits.ReadInt32(out var value);
			if (value != 4)
			{
				throw new PdbDebugException("Invalid signature. (sig={0})", value);
			}
			bits.Position = 4;
			array = PdbFunction.LoadManagedFunctions(bits, (uint)info.cbSyms, readStrings);
			if (array != null)
			{
				bits.Position = info.cbSyms + info.cbOldLines;
				LoadManagedLines(array, names, bits, dir, nameIndex, reader, (uint)(info.cbSyms + info.cbOldLines + info.cbLines), sourceCache);
				for (int i = 0; i < array.Length; i++)
				{
					funcList.Add(array[i]);
				}
			}
		}

		private static void LoadDbiStream(BitAccess bits, out DbiModuleInfo[] modules, out DbiDbgHdr header, bool readStrings)
		{
			DbiHeader dbiHeader = new DbiHeader(bits);
			header = default(DbiDbgHdr);
			List<DbiModuleInfo> list = new List<DbiModuleInfo>();
			int num = bits.Position + dbiHeader.gpmodiSize;
			while (bits.Position < num)
			{
				DbiModuleInfo item = new DbiModuleInfo(bits, readStrings);
				list.Add(item);
			}
			if (bits.Position != num)
			{
				throw new PdbDebugException("Error reading DBI stream, pos={0} != {1}", bits.Position, num);
			}
			if (list.Count > 0)
			{
				modules = list.ToArray();
			}
			else
			{
				modules = null;
			}
			bits.Position += dbiHeader.secconSize;
			bits.Position += dbiHeader.secmapSize;
			bits.Position += dbiHeader.filinfSize;
			bits.Position += dbiHeader.tsmapSize;
			bits.Position += dbiHeader.ecinfoSize;
			num = bits.Position + dbiHeader.dbghdrSize;
			if (dbiHeader.dbghdrSize > 0)
			{
				header = new DbiDbgHdr(bits);
			}
			bits.Position = num;
		}

		internal static PdbInfo LoadFunctions(Stream read)
		{
			PdbInfo pdbInfo = new PdbInfo();
			pdbInfo.TokenToSourceMapping = new Dictionary<uint, PdbTokenLine>();
			BitAccess bitAccess = new BitAccess(65536);
			PdbFileHeader pdbFileHeader = new PdbFileHeader(read, bitAccess);
			PdbReader reader = new PdbReader(read, pdbFileHeader.pageSize);
			MsfDirectory msfDirectory = new MsfDirectory(reader, pdbFileHeader, bitAccess);
			DbiModuleInfo[] modules = null;
			Dictionary<string, PdbSource> sourceCache = new Dictionary<string, PdbSource>();
			msfDirectory.streams[1].Read(reader, bitAccess);
			Dictionary<string, int> dictionary = LoadNameIndex(bitAccess, out pdbInfo.Age, out pdbInfo.Guid);
			if (!dictionary.TryGetValue("/NAMES", out var value))
			{
				throw new PdbException("Could not find the '/NAMES' stream: the PDB file may be a public symbol file instead of a private symbol file");
			}
			msfDirectory.streams[value].Read(reader, bitAccess);
			IntHashTable names = LoadNameStream(bitAccess);
			if (!dictionary.TryGetValue("SRCSRV", out var value2))
			{
				pdbInfo.SourceServerData = string.Empty;
			}
			else
			{
				DataStream dataStream = msfDirectory.streams[value2];
				byte[] array = new byte[dataStream.contentSize];
				dataStream.Read(reader, bitAccess);
				pdbInfo.SourceServerData = bitAccess.ReadBString(array.Length);
			}
			if (dictionary.TryGetValue("SOURCELINK", out var value3))
			{
				DataStream dataStream2 = msfDirectory.streams[value3];
				pdbInfo.SourceLinkData = new byte[dataStream2.contentSize];
				dataStream2.Read(reader, bitAccess);
				bitAccess.ReadBytes(pdbInfo.SourceLinkData);
			}
			msfDirectory.streams[3].Read(reader, bitAccess);
			LoadDbiStream(bitAccess, out modules, out var header, readStrings: true);
			List<PdbFunction> list = new List<PdbFunction>();
			if (modules != null)
			{
				foreach (DbiModuleInfo dbiModuleInfo in modules)
				{
					if (dbiModuleInfo.stream > 0)
					{
						msfDirectory.streams[dbiModuleInfo.stream].Read(reader, bitAccess);
						if (dbiModuleInfo.moduleName == "TokenSourceLineInfo")
						{
							LoadTokenToSourceInfo(bitAccess, dbiModuleInfo, names, msfDirectory, dictionary, reader, pdbInfo.TokenToSourceMapping, sourceCache);
						}
						else
						{
							LoadFuncsFromDbiModule(bitAccess, dbiModuleInfo, names, list, readStrings: true, msfDirectory, dictionary, reader, sourceCache);
						}
					}
				}
			}
			PdbFunction[] array2 = list.ToArray();
			if (header.snTokenRidMap != 0 && header.snTokenRidMap != ushort.MaxValue)
			{
				msfDirectory.streams[header.snTokenRidMap].Read(reader, bitAccess);
				uint[] array3 = new uint[msfDirectory.streams[header.snTokenRidMap].Length / 4];
				bitAccess.ReadUInt32(array3);
				PdbFunction[] array4 = array2;
				foreach (PdbFunction pdbFunction in array4)
				{
					pdbFunction.token = 0x6000000 | array3[pdbFunction.token & 0xFFFFFF];
				}
			}
			Array.Sort(array2, PdbFunction.byAddressAndToken);
			pdbInfo.Functions = array2;
			return pdbInfo;
		}

		private static void LoadTokenToSourceInfo(BitAccess bits, DbiModuleInfo module, IntHashTable names, MsfDirectory dir, Dictionary<string, int> nameIndex, PdbReader reader, Dictionary<uint, PdbTokenLine> tokenToSourceMapping, Dictionary<string, PdbSource> sourceCache)
		{
			bits.Position = 0;
			bits.ReadInt32(out var value);
			if (value != 4)
			{
				throw new PdbDebugException("Invalid signature. (sig={0})", value);
			}
			bits.Position = 4;
			OemSymbol oemSymbol = default(OemSymbol);
			while (bits.Position < module.cbSyms)
			{
				bits.ReadUInt16(out var value2);
				int position = bits.Position;
				int position2 = bits.Position + value2;
				bits.Position = position;
				bits.ReadUInt16(out var value3);
				switch ((SYM)value3)
				{
				case SYM.S_OEM:
					bits.ReadGuid(out oemSymbol.idOem);
					bits.ReadUInt32(out oemSymbol.typind);
					if (oemSymbol.idOem == PdbFunction.msilMetaData)
					{
						string text = bits.ReadString();
						if (text == "TSLI")
						{
							bits.ReadUInt32(out var value4);
							bits.ReadUInt32(out var value5);
							bits.ReadUInt32(out var value6);
							bits.ReadUInt32(out var value7);
							bits.ReadUInt32(out var value8);
							bits.ReadUInt32(out var value9);
							if (!tokenToSourceMapping.TryGetValue(value4, out var value10))
							{
								tokenToSourceMapping.Add(value4, new PdbTokenLine(value4, value5, value6, value7, value8, value9));
							}
							else
							{
								while (value10.nextLine != null)
								{
									value10 = value10.nextLine;
								}
								value10.nextLine = new PdbTokenLine(value4, value5, value6, value7, value8, value9);
							}
						}
						bits.Position = position2;
						break;
					}
					throw new PdbDebugException("OEM section: guid={0} ti={1}", oemSymbol.idOem, oemSymbol.typind);
				case SYM.S_END:
					bits.Position = position2;
					break;
				default:
					bits.Position = position2;
					break;
				}
			}
			bits.Position = module.cbSyms + module.cbOldLines;
			int limit = module.cbSyms + module.cbOldLines + module.cbLines;
			IntHashTable intHashTable = ReadSourceFileInfo(bits, (uint)limit, names, dir, nameIndex, reader, sourceCache);
			foreach (PdbTokenLine value11 in tokenToSourceMapping.Values)
			{
				value11.sourceFile = (PdbSource)intHashTable[(int)value11.file_id];
			}
		}

		private static IntHashTable ReadSourceFileInfo(BitAccess bits, uint limit, IntHashTable names, MsfDirectory dir, Dictionary<string, int> nameIndex, PdbReader reader, Dictionary<string, PdbSource> sourceCache)
		{
			IntHashTable intHashTable = new IntHashTable();
			int position = bits.Position;
			CV_FileCheckSum cV_FileCheckSum = default(CV_FileCheckSum);
			while (bits.Position < limit)
			{
				bits.ReadInt32(out var value);
				bits.ReadInt32(out var value2);
				int position2 = bits.Position;
				int num = bits.Position + value2;
				DEBUG_S_SUBSECTION dEBUG_S_SUBSECTION = (DEBUG_S_SUBSECTION)value;
				DEBUG_S_SUBSECTION dEBUG_S_SUBSECTION2 = dEBUG_S_SUBSECTION;
				if (dEBUG_S_SUBSECTION2 == DEBUG_S_SUBSECTION.FILECHKSMS)
				{
					while (bits.Position < num)
					{
						int key = bits.Position - position2;
						bits.ReadUInt32(out cV_FileCheckSum.name);
						bits.ReadUInt8(out cV_FileCheckSum.len);
						bits.ReadUInt8(out cV_FileCheckSum.type);
						string text = (string)names[(int)cV_FileCheckSum.name];
						if (!sourceCache.TryGetValue(text, out var value3))
						{
							Guid doctype = SymDocumentType_Text;
							Guid language = Guid.Empty;
							Guid vendor = Guid.Empty;
							Guid checksumAlgo = Guid.Empty;
							byte[] checksum = null;
							if (nameIndex.TryGetValue("/SRC/FILES/" + text.ToUpperInvariant(), out var value4))
							{
								BitAccess bits2 = new BitAccess(256);
								dir.streams[value4].Read(reader, bits2);
								LoadInjectedSourceInformation(bits2, out doctype, out language, out vendor, out checksumAlgo, out checksum);
							}
							value3 = new PdbSource(text, doctype, language, vendor, checksumAlgo, checksum);
							sourceCache.Add(text, value3);
						}
						intHashTable.Add(key, value3);
						bits.Position += cV_FileCheckSum.len;
						bits.Align(4);
					}
					bits.Position = num;
				}
				else
				{
					bits.Position = num;
				}
			}
			return intHashTable;
		}
	}
	internal class PdbFileHeader
	{
		private readonly byte[] windowsPdbMagic = new byte[32]
		{
			77, 105, 99, 114, 111, 115, 111, 102, 116, 32,
			67, 47, 67, 43, 43, 32, 77, 83, 70, 32,
			55, 46, 48, 48, 13, 10, 26, 68, 83, 0,
			0, 0
		};

		internal readonly byte[] magic;

		internal readonly int pageSize;

		internal int freePageMap;

		internal int pagesUsed;

		internal int directorySize;

		internal readonly int zero;

		internal int[] directoryRoot;

		internal PdbFileHeader(Stream reader, BitAccess bits)
		{
			bits.MinCapacity(56);
			reader.Seek(0L, SeekOrigin.Begin);
			bits.FillBuffer(reader, 52);
			magic = new byte[32];
			bits.ReadBytes(magic);
			bits.ReadInt32(out pageSize);
			bits.ReadInt32(out freePageMap);
			bits.ReadInt32(out pagesUsed);
			bits.ReadInt32(out directorySize);
			bits.ReadInt32(out zero);
			if (!magic.SequenceEqual(windowsPdbMagic))
			{
				throw new PdbException("The PDB file is not recognized as a Windows PDB file");
			}
			int num = ((directorySize + pageSize - 1) / pageSize * 4 + pageSize - 1) / pageSize;
			directoryRoot = new int[num];
			bits.FillBuffer(reader, num * 4);
			bits.ReadInt32(directoryRoot);
		}
	}
	internal class PdbFunction
	{
		internal class PdbFunctionsByAddress : IComparer
		{
			public int Compare(object x, object y)
			{
				PdbFunction pdbFunction = (PdbFunction)x;
				PdbFunction pdbFunction2 = (PdbFunction)y;
				if (pdbFunction.segment < pdbFunction2.segment)
				{
					return -1;
				}
				if (pdbFunction.segment > pdbFunction2.segment)
				{
					return 1;
				}
				if (pdbFunction.address < pdbFunction2.address)
				{
					return -1;
				}
				if (pdbFunction.address > pdbFunction2.address)
				{
					return 1;
				}
				return 0;
			}
		}

		internal class PdbFunctionsByAddressAndToken : IComparer
		{
			public int Compare(object x, object y)
			{
				PdbFunction pdbFunction = (PdbFunction)x;
				PdbFunction pdbFunction2 = (PdbFunction)y;
				if (pdbFunction.segment < pdbFunction2.segment)
				{
					return -1;
				}
				if (pdbFunction.segment > pdbFunction2.segment)
				{
					return 1;
				}
				if (pdbFunction.address < pdbFunction2.address)
				{
					return -1;
				}
				if (pdbFunction.address > pdbFunction2.address)
				{
					return 1;
				}
				if (pdbFunction.token < pdbFunction2.token)
				{
					return -1;
				}
				if (pdbFunction.token > pdbFunction2.token)
				{
					return 1;
				}
				return 0;
			}
		}

		internal static readonly Guid msilMetaData = new Guid(3337240521u, 22963, 18902, 188, 37, 9, 2, 187, 171, 180, 96);

		internal static readonly IComparer byAddress = new PdbFunctionsByAddress();

		internal static readonly IComparer byAddressAndToken = new PdbFunctionsByAddressAndToken();

		internal uint token;

		internal uint slotToken;

		internal uint tokenOfMethodWhoseUsingInfoAppliesToThisMethod;

		internal uint segment;

		internal uint address;

		internal uint length;

		internal PdbScope[] scopes;

		internal PdbSlot[] slots;

		internal PdbConstant[] constants;

		internal string[] usedNamespaces;

		internal PdbLines[] lines;

		internal ushort[] usingCounts;

		internal IEnumerable<INamespaceScope> namespaceScopes;

		internal string iteratorClass;

		internal List<ILocalScope> iteratorScopes;

		internal PdbSynchronizationInformation synchronizationInformation;

		private bool visualBasicScopesAdjusted = false;

		private static string StripNamespace(string module)
		{
			int num = module.LastIndexOf('.');
			if (num > 0)
			{
				return module.Substring(num + 1);
			}
			return module;
		}

		internal void AdjustVisualBasicScopes()
		{
			if (!visualBasicScopesAdjusted)
			{
				visualBasicScopesAdjusted = true;
				PdbScope[] array = scopes;
				foreach (PdbScope pdbScope in array)
				{
					AdjustVisualBasicScopes(pdbScope.scopes);
				}
			}
		}

		private void AdjustVisualBasicScopes(PdbScope[] scopes)
		{
			foreach (PdbScope pdbScope in scopes)
			{
				pdbScope.length++;
				AdjustVisualBasicScopes(pdbScope.scopes);
			}
		}

		internal static PdbFunction[] LoadManagedFunctions(BitAccess bits, uint limit, bool readStrings)
		{
			int position = bits.Position;
			int num = 0;
			ManProcSym manProcSym = default(ManProcSym);
			while (bits.Position < limit)
			{
				bits.ReadUInt16(out var value);
				int position2 = bits.Position;
				int position3 = bits.Position + value;
				bits.Position = position2;
				bits.ReadUInt16(out var value2);
				switch ((SYM)value2)
				{
				case SYM.S_GMANPROC:
				case SYM.S_LMANPROC:
					bits.ReadUInt32(out manProcSym.parent);
					bits.ReadUInt32(out manProcSym.end);
					bits.Position = (int)manProcSym.end;
					num++;
					break;
				case SYM.S_END:
					bits.Position = position3;
					break;
				default:
					bits.Position = position3;
					break;
				}
			}
			if (num == 0)
			{
				return null;
			}
			bits.Position = position;
			PdbFunction[] array = new PdbFunction[num];
			int num2 = 0;
			ManProcSym proc = default(ManProcSym);
			while (bits.Position < limit)
			{
				bits.ReadUInt16(out var value3);
				int position4 = bits.Position;
				int position5 = bits.Position + value3;
				bits.ReadUInt16(out var value4);
				SYM sYM = (SYM)value4;
				SYM sYM2 = sYM;
				if ((uint)(sYM2 - 4394) <= 1u)
				{
					bits.ReadUInt32(out proc.parent);
					bits.ReadUInt32(out proc.end);
					bits.ReadUInt32(out proc.next);
					bits.ReadUInt32(out proc.len);
					bits.ReadUInt32(out proc.dbgStart);
					bits.ReadUInt32(out proc.dbgEnd);
					bits.ReadUInt32(out proc.token);
					bits.ReadUInt32(out proc.off);
					bits.ReadUInt16(out proc.seg);
					bits.ReadUInt8(out proc.flags);
					bits.ReadUInt16(out proc.retReg);
					if (readStrings)
					{
						bits.ReadCString(out proc.name);
					}
					else
					{
						bits.SkipCString(out proc.name);
					}
					bits.Position = position5;
					array[num2++] = new PdbFunction(proc, bits);
				}
				else
				{
					bits.Position = position5;
				}
			}
			return array;
		}

		internal static void CountScopesAndSlots(BitAccess bits, uint limit, out int constants, out int scopes, out int slots, out int usedNamespaces)
		{
			int position = bits.Position;
			constants = 0;
			slots = 0;
			scopes = 0;
			usedNamespaces = 0;
			BlockSym32 blockSym = default(BlockSym32);
			while (bits.Position < limit)
			{
				bits.ReadUInt16(out var value);
				int position2 = bits.Position;
				int position3 = bits.Position + value;
				bits.Position = position2;
				bits.ReadUInt16(out var value2);
				switch ((SYM)value2)
				{
				case SYM.S_BLOCK32:
					bits.ReadUInt32(out blockSym.parent);
					bits.ReadUInt32(out blockSym.end);
					scopes++;
					bits.Position = (int)blockSym.end;
					break;
				case SYM.S_MANSLOT:
					slots++;
					bits.Position = position3;
					break;
				case SYM.S_UNAMESPACE:
					usedNamespaces++;
					bits.Position = position3;
					break;
				case SYM.S_MANCONSTANT:
					constants++;
					bits.Position = position3;
					break;
				default:
					bits.Position = position3;
					break;
				}
			}
			bits.Position = position;
		}

		internal PdbFunction()
		{
		}

		internal PdbFunction(ManProcSym proc, BitAccess bits)
		{
			token = proc.token;
			segment = proc.seg;
			address = proc.off;
			length = proc.len;
			if (proc.seg != 1)
			{
				throw new PdbDebugException("Segment is {0}, not 1.", proc.seg);
			}
			if (proc.parent != 0 || proc.next != 0)
			{
				throw new PdbDebugException("Warning parent={0}, next={1}", proc.parent, proc.next);
			}
			CountScopesAndSlots(bits, proc.end, out var num, out var num2, out var num3, out var num4);
			int num5 = ((num > 0 || num3 > 0 || num4 > 0) ? 1 : 0);
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			scopes = new PdbScope[num2 + num5];
			slots = new PdbSlot[num3];
			constants = new PdbConstant[num];
			usedNamespaces = new string[num4];
			if (num5 > 0)
			{
				scopes[0] = new PdbScope(address, proc.len, slots, constants, usedNamespaces);
			}
			OemSymbol oemSymbol = default(OemSymbol);
			while (bits.Position < proc.end)
			{
				bits.ReadUInt16(out var value);
				int position = bits.Position;
				int position2 = bits.Position + value;
				bits.Position = position;
				bits.ReadUInt16(out var value2);
				switch ((SYM)value2)
				{
				case SYM.S_OEM:
					bits.ReadGuid(out oemSymbol.idOem);
					bits.ReadUInt32(out oemSymbol.typind);
					if (oemSymbol.idOem == msilMetaData)
					{
						string text = bits.ReadString();
						if (text == "MD2")
						{
							ReadMD2CustomMetadata(bits);
						}
						else if (text == "asyncMethodInfo")
						{
							synchronizationInformation = new PdbSynchronizationInformation(bits);
						}
						bits.Position = position2;
						break;
					}
					throw new PdbDebugException("OEM section: guid={0} ti={1}", oemSymbol.idOem, oemSymbol.typind);
				case SYM.S_BLOCK32:
				{
					BlockSym32 block = default(BlockSym32);
					bits.ReadUInt32(out block.parent);
					bits.ReadUInt32(out block.end);
					bits.ReadUInt32(out block.len);
					bits.ReadUInt32(out block.off);
					bits.ReadUInt16(out block.seg);
					bits.SkipCString(out block.name);
					bits.Position = position2;
					scopes[num5++] = new PdbScope(address, block, bits, out slotToken);
					bits.Position = (int)block.end;
					break;
				}
				case SYM.S_MANSLOT:
					slots[num6++] = new PdbSlot(bits);
					bits.Position = position2;
					break;
				case SYM.S_MANCONSTANT:
					constants[num7++] = new PdbConstant(bits);
					bits.Position = position2;
					break;
				case SYM.S_UNAMESPACE:
					bits.ReadCString(out usedNamespaces[num8++]);
					bits.Position = position2;
					break;
				case SYM.S_END:
					bits.Position = position2;
					break;
				default:
					bits.Position = position2;
					break;
				}
			}
			if (bits.Position != proc.end)
			{
				throw new PdbDebugException("Not at S_END");
			}
			bits.ReadUInt16(out var _);
			bits.ReadUInt16(out var value4);
			if (value4 != 6)
			{
				throw new PdbDebugException("Missing S_END");
			}
		}

		internal void ReadMD2CustomMetadata(BitAccess bits)
		{
			bits.ReadUInt8(out var value);
			if (value == 4)
			{
				bits.ReadUInt8(out var value2);
				bits.Align(4);
				while (value2-- > 0)
				{
					ReadCustomMetadata(bits);
				}
			}
		}

		private void ReadCustomMetadata(BitAccess bits)
		{
			int position = bits.Position;
			bits.ReadUInt8(out var value);
			bits.ReadUInt8(out var value2);
			bits.Position += 2;
			bits.ReadUInt32(out var value3);
			if (value == 4)
			{
				switch (value2)
				{
				case 0:
					ReadUsingInfo(bits);
					break;
				case 1:
					ReadForwardInfo(bits);
					break;
				case 3:
					ReadIteratorLocals(bits);
					break;
				case 4:
					ReadForwardIterator(bits);
					break;
				}
			}
			bits.Position = position + (int)value3;
		}

		private void ReadForwardIterator(BitAccess bits)
		{
			iteratorClass = bits.ReadString();
		}

		private void ReadIteratorLocals(BitAccess bits)
		{
			bits.ReadUInt32(out var value);
			iteratorScopes = new List<ILocalScope>((int)value);
			while (value-- != 0)
			{
				bits.ReadUInt32(out var value2);
				bits.ReadUInt32(out var value3);
				iteratorScopes.Add(new PdbIteratorScope(value2, value3 - value2));
			}
		}

		private void ReadForwardInfo(BitAccess bits)
		{
			bits.ReadUInt32(out tokenOfMethodWhoseUsingInfoAppliesToThisMethod);
		}

		private void ReadUsingInfo(BitAccess bits)
		{
			bits.ReadUInt16(out var value);
			usingCounts = new ushort[value];
			for (ushort num = 0; num < value; num++)
			{
				bits.ReadUInt16(out usingCounts[num]);
			}
		}
	}
	internal class PdbSynchronizationInformation
	{
		internal uint kickoffMethodToken;

		internal uint generatedCatchHandlerIlOffset;

		internal PdbSynchronizationPoint[] synchronizationPoints;

		public uint GeneratedCatchHandlerOffset => generatedCatchHandlerIlOffset;

		internal PdbSynchronizationInformation(BitAccess bits)
		{
			bits.ReadUInt32(out kickoffMethodToken);
			bits.ReadUInt32(out generatedCatchHandlerIlOffset);
			bits.ReadUInt32(out var value);
			synchronizationPoints = new PdbSynchronizationPoint[value];
			for (uint num = 0u; num < value; num++)
			{
				synchronizationPoints[num] = new PdbSynchronizationPoint(bits);
			}
		}
	}
	internal class PdbSynchronizationPoint
	{
		internal uint synchronizeOffset;

		internal uint continuationMethodToken;

		internal uint continuationOffset;

		public uint SynchronizeOffset => synchronizeOffset;

		public uint ContinuationOffset => continuationOffset;

		internal PdbSynchronizationPoint(BitAccess bits)
		{
			bits.ReadUInt32(out synchronizeOffset);
			bits.ReadUInt32(out continuationMethodToken);
			bits.ReadUInt32(out continuationOffset);
		}
	}
	internal class PdbInfo
	{
		public PdbFunction[] Functions;

		public Dictionary<uint, PdbTokenLine> TokenToSourceMapping;

		public string SourceServerData;

		public int Age;

		public Guid Guid;

		public byte[] SourceLinkData;
	}
	internal struct PdbLine
	{
		internal uint offset;

		internal uint lineBegin;

		internal uint lineEnd;

		internal ushort colBegin;

		internal ushort colEnd;

		internal PdbLine(uint offset, uint lineBegin, ushort colBegin, uint lineEnd, ushort colEnd)
		{
			this.offset = offset;
			this.lineBegin = lineBegin;
			this.colBegin = colBegin;
			this.lineEnd = lineEnd;
			this.colEnd = colEnd;
		}
	}
	internal class PdbLines
	{
		internal PdbSource file;

		internal PdbLine[] lines;

		internal PdbLines(PdbSource file, uint count)
		{
			this.file = file;
			lines = new PdbLine[count];
		}
	}
	internal class PdbReader
	{
		internal readonly int pageSize;

		internal readonly Stream reader;

		internal PdbReader(Stream reader, int pageSize)
		{
			this.pageSize = pageSize;
			this.reader = reader;
		}

		internal void Seek(int page, int offset)
		{
			reader.Seek(page * pageSize + offset, SeekOrigin.Begin);
		}

		internal void Read(byte[] bytes, int offset, int count)
		{
			reader.Read(bytes, offset, count);
		}

		internal int PagesFromSize(int size)
		{
			return (size + pageSize - 1) / pageSize;
		}
	}
	internal class PdbScope
	{
		internal PdbConstant[] constants;

		internal PdbSlot[] slots;

		internal PdbScope[] scopes;

		internal string[] usedNamespaces;

		internal uint address;

		internal uint offset;

		internal uint length;

		internal PdbScope(uint address, uint offset, uint length, PdbSlot[] slots, PdbConstant[] constants, string[] usedNamespaces)
		{
			this.constants = constants;
			this.slots = slots;
			scopes = new PdbScope[0];
			this.usedNamespaces = usedNamespaces;
			this.address = address;
			this.offset = offset;
			this.length = length;
		}

		internal PdbScope(uint address, uint length, PdbSlot[] slots, PdbConstant[] constants, string[] usedNamespaces)
			: this(address, 0u, length, slots, constants, usedNamespaces)
		{
		}

		internal PdbScope(uint funcOffset, BlockSym32 block, BitAccess bits, out uint typind)
		{
			address = block.off;
			offset = block.off - funcOffset;
			length = block.len;
			typind = 0u;
			PdbFunction.CountScopesAndSlots(bits, block.end, out var num, out var num2, out var num3, out var num4);
			constants = new PdbConstant[num];
			scopes = new PdbScope[num2];
			slots = new PdbSlot[num3];
			usedNamespaces = new string[num4];
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			while (bits.Position < block.end)
			{
				bits.ReadUInt16(out var value);
				int position = bits.Position;
				int position2 = bits.Position + value;
				bits.Position = position;
				bits.ReadUInt16(out var value2);
				switch ((SYM)value2)
				{
				case SYM.S_BLOCK32:
				{
					BlockSym32 block2 = default(BlockSym32);
					bits.ReadUInt32(out block2.parent);
					bits.ReadUInt32(out block2.end);
					bits.ReadUInt32(out block2.len);
					bits.ReadUInt32(out block2.off);
					bits.ReadUInt16(out block2.seg);
					bits.SkipCString(out block2.name);
					bits.Position = position2;
					scopes[num6++] = new PdbScope(funcOffset, block2, bits, out typind);
					break;
				}
				case SYM.S_MANSLOT:
					slots[num7++] = new PdbSlot(bits);
					bits.Position = position2;
					break;
				case SYM.S_UNAMESPACE:
					bits.ReadCString(out usedNamespaces[num8++]);
					bits.Position = position2;
					break;
				case SYM.S_END:
					bits.Position = position2;
					break;
				case SYM.S_MANCONSTANT:
					constants[num5++] = new PdbConstant(bits);
					bits.Position = position2;
					break;
				default:
					bits.Position = position2;
					break;
				}
			}
			if (bits.Position != block.end)
			{
				throw new Exception("Not at S_END");
			}
			bits.ReadUInt16(out var _);
			bits.ReadUInt16(out var value4);
			if (value4 != 6)
			{
				throw new Exception("Missing S_END");
			}
		}
	}
	internal class PdbSlot
	{
		internal uint slot;

		internal uint typeToken;

		internal string name;

		internal ushort flags;

		internal PdbSlot(uint slot, uint typeToken, string name, ushort flags)
		{
			this.slot = slot;
			this.typeToken = typeToken;
			this.name = name;
			this.flags = flags;
		}

		internal PdbSlot(BitAccess bits)
		{
			AttrSlotSym attrSlotSym = default(AttrSlotSym);
			bits.ReadUInt32(out attrSlotSym.index);
			bits.ReadUInt32(out attrSlotSym.typind);
			bits.ReadUInt32(out attrSlotSym.offCod);
			bits.ReadUInt16(out attrSlotSym.segCod);
			bits.ReadUInt16(out attrSlotSym.flags);
			bits.ReadCString(out attrSlotSym.name);
			slot = attrSlotSym.index;
			typeToken = attrSlotSym.typind;
			name = attrSlotSym.name;
			flags = attrSlotSym.flags;
		}
	}
	internal class PdbSource
	{
		internal string name;

		internal Guid doctype;

		internal Guid language;

		internal Guid vendor;

		internal Guid checksumAlgorithm;

		internal byte[] checksum;

		internal PdbSource(string name, Guid doctype, Guid language, Guid vendor, Guid checksumAlgorithm, byte[] checksum)
		{
			this.name = name;
			this.doctype = doctype;
			this.language = language;
			this.vendor = vendor;
			this.checksumAlgorithm = checksumAlgorithm;
			this.checksum = checksum;
		}
	}
	internal class PdbTokenLine
	{
		internal uint token;

		internal uint file_id;

		internal uint line;

		internal uint column;

		internal uint endLine;

		internal uint endColumn;

		internal PdbSource sourceFile;

		internal PdbTokenLine nextLine;

		internal PdbTokenLine(uint token, uint file_id, uint line, uint column, uint endLine, uint endColumn)
		{
			this.token = token;
			this.file_id = file_id;
			this.line = line;
			this.column = column;
			this.endLine = endLine;
			this.endColumn = endColumn;
		}
	}
	internal sealed class PdbIteratorScope : ILocalScope
	{
		private uint offset;

		private uint length;

		public uint Offset => offset;

		public uint Length => length;

		internal PdbIteratorScope(uint offset, uint length)
		{
			this.offset = offset;
			this.length = length;
		}
	}
}
