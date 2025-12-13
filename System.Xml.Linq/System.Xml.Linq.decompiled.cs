using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using Unity;

[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("System.Xml.Linq.dll")]
[assembly: AssemblyDescription("System.Xml.Linq.dll")]
[assembly: AssemblyDefaultAlias("System.Xml.Linq.dll")]
[assembly: AssemblyCompany("Mono development team")]
[assembly: AssemblyProduct("Mono Common Language Infrastructure")]
[assembly: AssemblyCopyright("(c) Various Mono authors")]
[assembly: SatelliteContractVersion("4.0.0.0")]
[assembly: AssemblyInformationalVersion("4.6.57.0")]
[assembly: AssemblyFileVersion("4.6.57.0")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: CLSCompliant(true)]
[assembly: AssemblyDelaySign(true)]
[assembly: AssemblyKeyFile("../ecma.pub")]
[assembly: AllowPartiallyTrustedCallers]
[assembly: ComVisible(false)]
[assembly: CompilationRelaxations(CompilationRelaxations.NoStringInterning)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("4.0.0.0")]
[module: UnverifiableCode]
internal static class SR
{
	public const string Argument_AddAttribute = "An attribute cannot be added to content.";

	public const string Argument_AddNode = "A node of type {0} cannot be added to content.";

	public const string Argument_AddNonWhitespace = "Non-whitespace characters cannot be added to content.";

	public const string Argument_ConvertToString = "The argument cannot be converted to a string.";

	public const string Argument_InvalidExpandedName = "'{0}' is an invalid expanded name.";

	public const string Argument_InvalidPIName = "'{0}' is an invalid name for a processing instruction.";

	public const string Argument_InvalidPrefix = "'{0}' is an invalid prefix.";

	public const string Argument_MustBeDerivedFrom = "The argument must be derived from {0}.";

	public const string Argument_NamespaceDeclarationPrefixed = "The prefix '{0}' cannot be bound to the empty namespace name.";

	public const string Argument_NamespaceDeclarationXml = "The prefix 'xml' is bound to the namespace name 'http://www.w3.org/XML/1998/namespace'. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.";

	public const string Argument_NamespaceDeclarationXmlns = "The prefix 'xmlns' is bound to the namespace name 'http://www.w3.org/2000/xmlns/'. It must not be declared. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.";

	public const string Argument_XObjectValue = "An XObject cannot be used as a value.";

	public const string InvalidOperation_DeserializeInstance = "This instance cannot be deserialized.";

	public const string InvalidOperation_DocumentStructure = "This operation would create an incorrectly structured document.";

	public const string InvalidOperation_DuplicateAttribute = "Duplicate attribute.";

	public const string InvalidOperation_ExpectedEndOfFile = "The XmlReader state should be EndOfFile after this operation.";

	public const string InvalidOperation_ExpectedInteractive = "The XmlReader state should be Interactive.";

	public const string InvalidOperation_ExpectedNodeType = "The XmlReader must be on a node of type {0} instead of a node of type {1}.";

	public const string InvalidOperation_ExternalCode = "This operation was corrupted by external code.";

	public const string InvalidOperation_MissingAncestor = "A common ancestor is missing.";

	public const string InvalidOperation_MissingParent = "The parent is missing.";

	public const string InvalidOperation_MissingRoot = "The root element is missing.";

	public const string InvalidOperation_UnexpectedNodeType = "The XmlReader should not be on a node of type {0}.";

	public const string InvalidOperation_UnresolvedEntityReference = "The XmlReader cannot resolve entity references.";

	public const string InvalidOperation_WriteAttribute = "An attribute cannot be written after content.";

	public const string NotSupported_WriteBase64 = "This XmlWriter does not support base64 encoded data.";

	public const string NotSupported_WriteEntityRef = "This XmlWriter does not support entity references.";

	public const string Argument_CreateNavigator = "This XPathNavigator cannot be created on a node of type {0}.";

	public const string InvalidOperation_BadNodeType = "This operation is not valid on a node of type {0}.";

	public const string InvalidOperation_UnexpectedEvaluation = "The XPath expression evaluated to unexpected type {0}.";

	public const string NotSupported_MoveToId = "This XPathNavigator does not support IDs.";

	internal static string GetString(string name, params object[] args)
	{
		return GetString(CultureInfo.InvariantCulture, name, args);
	}

	internal static string GetString(CultureInfo culture, string name, params object[] args)
	{
		return string.Format(culture, name, args);
	}

	internal static string GetString(string name)
	{
		return name;
	}

	internal static string GetString(CultureInfo culture, string name)
	{
		return name;
	}

	internal static string Format(string resourceFormat, params object[] args)
	{
		if (args != null)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, args);
		}
		return resourceFormat;
	}

	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}

	internal static string Format(string resourceFormat, object p1, object p2)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2);
	}

	internal static string Format(CultureInfo ci, string resourceFormat, object p1, object p2)
	{
		return string.Format(ci, resourceFormat, p1, p2);
	}

	internal static string Format(string resourceFormat, object p1, object p2, object p3)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2, p3);
	}

	internal static string GetResourceString(string str)
	{
		return str;
	}
}
namespace System.Runtime.CompilerServices
{
	internal class FriendAccessAllowedAttribute : Attribute
	{
	}
}
namespace System.Xml.XPath
{
	internal static class XAttributeExtensions
	{
		public static string GetPrefixOfNamespace(this XAttribute attribute, XNamespace ns)
		{
			string namespaceName = ns.NamespaceName;
			if (namespaceName.Length == 0)
			{
				return string.Empty;
			}
			if (attribute.GetParent() != null)
			{
				return ((XElement)attribute.GetParent()).GetPrefixOfNamespace(ns);
			}
			if ((object)namespaceName == XNodeNavigator.xmlPrefixNamespace)
			{
				return "xml";
			}
			if ((object)namespaceName == XNodeNavigator.xmlnsPrefixNamespace)
			{
				return "xmlns";
			}
			return null;
		}
	}
	internal class XNodeNavigator : XPathNavigator, IXmlLineInfo
	{
		internal static readonly string xmlPrefixNamespace = XNamespace.Xml.NamespaceName;

		internal static readonly string xmlnsPrefixNamespace = XNamespace.Xmlns.NamespaceName;

		private const int DocumentContentMask = 386;

		private static readonly int[] s_ElementContentMasks = new int[10] { 0, 2, 0, 0, 24, 0, 0, 128, 256, 410 };

		private new const int TextMask = 24;

		private static XAttribute s_XmlNamespaceDeclaration;

		private XObject _source;

		private XElement _parent;

		private XmlNameTable _nameTable;

		public override string BaseURI
		{
			get
			{
				if (_source != null)
				{
					return _source.BaseUri;
				}
				if (_parent != null)
				{
					return _parent.BaseUri;
				}
				return string.Empty;
			}
		}

		public override bool HasAttributes
		{
			get
			{
				if (_source is XElement xElement)
				{
					foreach (XAttribute item in xElement.Attributes())
					{
						if (!item.IsNamespaceDeclaration)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		public override bool HasChildren
		{
			get
			{
				if (_source is XContainer xContainer)
				{
					foreach (XNode item in xContainer.Nodes())
					{
						if (IsContent(xContainer, item))
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		public override bool IsEmptyElement
		{
			get
			{
				if (_source is XElement xElement)
				{
					return xElement.IsEmpty;
				}
				return false;
			}
		}

		public override string LocalName => _nameTable.Add(GetLocalName());

		public override string Name
		{
			get
			{
				string prefix = GetPrefix();
				if (prefix.Length == 0)
				{
					return _nameTable.Add(GetLocalName());
				}
				return _nameTable.Add(prefix + ":" + GetLocalName());
			}
		}

		public override string NamespaceURI => _nameTable.Add(GetNamespaceURI());

		public override XmlNameTable NameTable => _nameTable;

		public override XPathNodeType NodeType
		{
			get
			{
				if (_source != null)
				{
					switch (_source.NodeType)
					{
					case XmlNodeType.Element:
						return XPathNodeType.Element;
					case XmlNodeType.Attribute:
						if (!((XAttribute)_source).IsNamespaceDeclaration)
						{
							return XPathNodeType.Attribute;
						}
						return XPathNodeType.Namespace;
					case XmlNodeType.Document:
						return XPathNodeType.Root;
					case XmlNodeType.Comment:
						return XPathNodeType.Comment;
					case XmlNodeType.ProcessingInstruction:
						return XPathNodeType.ProcessingInstruction;
					default:
						return XPathNodeType.Text;
					}
				}
				return XPathNodeType.Text;
			}
		}

		public override string Prefix => _nameTable.Add(GetPrefix());

		public override object UnderlyingObject => _source;

		public override string Value
		{
			get
			{
				if (_source != null)
				{
					switch (_source.NodeType)
					{
					case XmlNodeType.Element:
						return ((XElement)_source).Value;
					case XmlNodeType.Attribute:
						return ((XAttribute)_source).Value;
					case XmlNodeType.Document:
					{
						XElement root = ((XDocument)_source).Root;
						if (root == null)
						{
							return string.Empty;
						}
						return root.Value;
					}
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						return CollectText((XText)_source);
					case XmlNodeType.Comment:
						return ((XComment)_source).Value;
					case XmlNodeType.ProcessingInstruction:
						return ((XProcessingInstruction)_source).Data;
					default:
						return string.Empty;
					}
				}
				return string.Empty;
			}
		}

		int IXmlLineInfo.LineNumber => ((IXmlLineInfo)_source)?.LineNumber ?? 0;

		int IXmlLineInfo.LinePosition => ((IXmlLineInfo)_source)?.LinePosition ?? 0;

		public XNodeNavigator(XNode node, XmlNameTable nameTable)
		{
			_source = node;
			_nameTable = ((nameTable != null) ? nameTable : CreateNameTable());
		}

		public XNodeNavigator(XNodeNavigator other)
		{
			_source = other._source;
			_parent = other._parent;
			_nameTable = other._nameTable;
		}

		private string GetLocalName()
		{
			if (_source is XElement xElement)
			{
				return xElement.Name.LocalName;
			}
			if (_source is XAttribute xAttribute)
			{
				if (_parent != null && xAttribute.Name.NamespaceName.Length == 0)
				{
					return string.Empty;
				}
				return xAttribute.Name.LocalName;
			}
			if (_source is XProcessingInstruction xProcessingInstruction)
			{
				return xProcessingInstruction.Target;
			}
			return string.Empty;
		}

		private string GetNamespaceURI()
		{
			if (_source is XElement xElement)
			{
				return xElement.Name.NamespaceName;
			}
			if (_source is XAttribute xAttribute)
			{
				if (_parent != null)
				{
					return string.Empty;
				}
				return xAttribute.Name.NamespaceName;
			}
			return string.Empty;
		}

		private string GetPrefix()
		{
			if (_source is XElement xElement)
			{
				string prefixOfNamespace = xElement.GetPrefixOfNamespace(xElement.Name.Namespace);
				if (prefixOfNamespace != null)
				{
					return prefixOfNamespace;
				}
				return string.Empty;
			}
			if (_source is XAttribute xAttribute)
			{
				if (_parent != null)
				{
					return string.Empty;
				}
				string prefixOfNamespace2 = xAttribute.GetPrefixOfNamespace(xAttribute.Name.Namespace);
				if (prefixOfNamespace2 != null)
				{
					return prefixOfNamespace2;
				}
			}
			return string.Empty;
		}

		public override XPathNavigator Clone()
		{
			return new XNodeNavigator(this);
		}

		public override bool IsSamePosition(XPathNavigator navigator)
		{
			if (!(navigator is XNodeNavigator n))
			{
				return false;
			}
			return IsSamePosition(this, n);
		}

		public override bool MoveTo(XPathNavigator navigator)
		{
			if (navigator is XNodeNavigator xNodeNavigator)
			{
				_source = xNodeNavigator._source;
				_parent = xNodeNavigator._parent;
				return true;
			}
			return false;
		}

		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			if (_source is XElement xElement)
			{
				foreach (XAttribute item in xElement.Attributes())
				{
					if (item.Name.LocalName == localName && item.Name.NamespaceName == namespaceName && !item.IsNamespaceDeclaration)
					{
						_source = item;
						return true;
					}
				}
			}
			return false;
		}

		public override bool MoveToChild(string localName, string namespaceName)
		{
			if (_source is XContainer xContainer)
			{
				foreach (XElement item in xContainer.Elements())
				{
					if (item.Name.LocalName == localName && item.Name.NamespaceName == namespaceName)
					{
						_source = item;
						return true;
					}
				}
			}
			return false;
		}

		public override bool MoveToChild(XPathNodeType type)
		{
			if (_source is XContainer xContainer)
			{
				int num = GetElementContentMask(type);
				if ((0x18 & num) != 0 && xContainer.GetParent() == null && xContainer is XDocument)
				{
					num &= -25;
				}
				foreach (XNode item in xContainer.Nodes())
				{
					if (((1 << (int)item.NodeType) & num) != 0)
					{
						_source = item;
						return true;
					}
				}
			}
			return false;
		}

		public override bool MoveToFirstAttribute()
		{
			if (_source is XElement xElement)
			{
				foreach (XAttribute item in xElement.Attributes())
				{
					if (!item.IsNamespaceDeclaration)
					{
						_source = item;
						return true;
					}
				}
			}
			return false;
		}

		public override bool MoveToFirstChild()
		{
			if (_source is XContainer xContainer)
			{
				foreach (XNode item in xContainer.Nodes())
				{
					if (IsContent(xContainer, item))
					{
						_source = item;
						return true;
					}
				}
			}
			return false;
		}

		public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
		{
			if (_source is XElement xElement)
			{
				XAttribute xAttribute = null;
				switch (scope)
				{
				case XPathNamespaceScope.Local:
					xAttribute = GetFirstNamespaceDeclarationLocal(xElement);
					break;
				case XPathNamespaceScope.ExcludeXml:
					xAttribute = GetFirstNamespaceDeclarationGlobal(xElement);
					while (xAttribute != null && xAttribute.Name.LocalName == "xml")
					{
						xAttribute = GetNextNamespaceDeclarationGlobal(xAttribute);
					}
					break;
				case XPathNamespaceScope.All:
					xAttribute = GetFirstNamespaceDeclarationGlobal(xElement);
					if (xAttribute == null)
					{
						xAttribute = GetXmlNamespaceDeclaration();
					}
					break;
				}
				if (xAttribute != null)
				{
					_source = xAttribute;
					_parent = xElement;
					return true;
				}
			}
			return false;
		}

		public override bool MoveToId(string id)
		{
			throw new NotSupportedException("This XPathNavigator does not support IDs.");
		}

		public override bool MoveToNamespace(string localName)
		{
			if (_source is XElement xElement)
			{
				if (localName == "xmlns")
				{
					return false;
				}
				if (localName != null && localName.Length == 0)
				{
					localName = "xmlns";
				}
				for (XAttribute xAttribute = GetFirstNamespaceDeclarationGlobal(xElement); xAttribute != null; xAttribute = GetNextNamespaceDeclarationGlobal(xAttribute))
				{
					if (xAttribute.Name.LocalName == localName)
					{
						_source = xAttribute;
						_parent = xElement;
						return true;
					}
				}
				if (localName == "xml")
				{
					_source = GetXmlNamespaceDeclaration();
					_parent = xElement;
					return true;
				}
			}
			return false;
		}

		public override bool MoveToNext()
		{
			if (_source is XNode xNode)
			{
				XContainer parent = xNode.GetParent();
				if (parent != null)
				{
					XNode xNode2 = null;
					for (XNode xNode3 = xNode; xNode3 != null; xNode3 = xNode2)
					{
						xNode2 = xNode3.NextNode;
						if (xNode2 == null)
						{
							break;
						}
						if (IsContent(parent, xNode2) && (!(xNode3 is XText) || !(xNode2 is XText)))
						{
							_source = xNode2;
							return true;
						}
					}
				}
			}
			return false;
		}

		public override bool MoveToNext(string localName, string namespaceName)
		{
			if (_source is XNode xNode)
			{
				foreach (XElement item in xNode.ElementsAfterSelf())
				{
					if (item.Name.LocalName == localName && item.Name.NamespaceName == namespaceName)
					{
						_source = item;
						return true;
					}
				}
			}
			return false;
		}

		public override bool MoveToNext(XPathNodeType type)
		{
			if (_source is XNode xNode)
			{
				XContainer parent = xNode.GetParent();
				if (parent != null)
				{
					int num = GetElementContentMask(type);
					if ((0x18 & num) != 0 && parent.GetParent() == null && parent is XDocument)
					{
						num &= -25;
					}
					XNode xNode2 = null;
					for (XNode xNode3 = xNode; xNode3 != null; xNode3 = xNode2)
					{
						xNode2 = xNode3.NextNode;
						if (((1 << (int)xNode2.NodeType) & num) != 0 && (!(xNode3 is XText) || !(xNode2 is XText)))
						{
							_source = xNode2;
							return true;
						}
					}
				}
			}
			return false;
		}

		public override bool MoveToNextAttribute()
		{
			if (_source is XAttribute xAttribute && _parent == null && (XElement)xAttribute.GetParent() != null)
			{
				for (XAttribute nextAttribute = xAttribute.NextAttribute; nextAttribute != null; nextAttribute = nextAttribute.NextAttribute)
				{
					if (!nextAttribute.IsNamespaceDeclaration)
					{
						_source = nextAttribute;
						return true;
					}
				}
			}
			return false;
		}

		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			XAttribute xAttribute = _source as XAttribute;
			if (xAttribute != null && _parent != null && !IsXmlNamespaceDeclaration(xAttribute))
			{
				switch (scope)
				{
				case XPathNamespaceScope.Local:
					if (xAttribute.GetParent() != _parent)
					{
						return false;
					}
					xAttribute = GetNextNamespaceDeclarationLocal(xAttribute);
					break;
				case XPathNamespaceScope.ExcludeXml:
					do
					{
						xAttribute = GetNextNamespaceDeclarationGlobal(xAttribute);
					}
					while (xAttribute != null && (xAttribute.Name.LocalName == "xml" || HasNamespaceDeclarationInScope(xAttribute, _parent)));
					break;
				case XPathNamespaceScope.All:
					do
					{
						xAttribute = GetNextNamespaceDeclarationGlobal(xAttribute);
					}
					while (xAttribute != null && HasNamespaceDeclarationInScope(xAttribute, _parent));
					if (xAttribute == null && !HasNamespaceDeclarationInScope(GetXmlNamespaceDeclaration(), _parent))
					{
						xAttribute = GetXmlNamespaceDeclaration();
					}
					break;
				}
				if (xAttribute != null)
				{
					_source = xAttribute;
					return true;
				}
			}
			return false;
		}

		public override bool MoveToParent()
		{
			if (_parent != null)
			{
				_source = _parent;
				_parent = null;
				return true;
			}
			XNode parent = _source.GetParent();
			if (parent != null)
			{
				_source = parent;
				return true;
			}
			return false;
		}

		public override bool MoveToPrevious()
		{
			if (_source is XNode xNode)
			{
				XContainer parent = xNode.GetParent();
				if (parent != null)
				{
					XNode xNode2 = null;
					foreach (XNode item in parent.Nodes())
					{
						if (item == xNode)
						{
							if (xNode2 != null)
							{
								_source = xNode2;
								return true;
							}
							return false;
						}
						if (IsContent(parent, item))
						{
							xNode2 = item;
						}
					}
				}
			}
			return false;
		}

		public override XmlReader ReadSubtree()
		{
			return ((_source as XContainer) ?? throw new InvalidOperationException(global::SR.Format("This operation is not valid on a node of type {0}.", NodeType))).CreateReader();
		}

		bool IXmlLineInfo.HasLineInfo()
		{
			return ((IXmlLineInfo)_source)?.HasLineInfo() ?? false;
		}

		private static string CollectText(XText n)
		{
			string text = n.Value;
			if (n.GetParent() != null)
			{
				using IEnumerator<XNode> enumerator = n.NodesAfterSelf().GetEnumerator();
				while (enumerator.MoveNext() && enumerator.Current is XText xText)
				{
					text += xText.Value;
				}
			}
			return text;
		}

		private static XmlNameTable CreateNameTable()
		{
			NameTable nameTable = new NameTable();
			nameTable.Add(string.Empty);
			nameTable.Add(xmlnsPrefixNamespace);
			nameTable.Add(xmlPrefixNamespace);
			return nameTable;
		}

		private static bool IsContent(XContainer c, XNode n)
		{
			if (c.GetParent() != null || c is XElement)
			{
				return true;
			}
			return ((1 << (int)n.NodeType) & 0x182) != 0;
		}

		private static bool IsSamePosition(XNodeNavigator n1, XNodeNavigator n2)
		{
			if (n1._source == n2._source)
			{
				return n1._source.GetParent() == n2._source.GetParent();
			}
			return false;
		}

		private static bool IsXmlNamespaceDeclaration(XAttribute a)
		{
			return a == GetXmlNamespaceDeclaration();
		}

		private static int GetElementContentMask(XPathNodeType type)
		{
			return s_ElementContentMasks[(int)type];
		}

		private static XAttribute GetFirstNamespaceDeclarationGlobal(XElement e)
		{
			do
			{
				XAttribute firstNamespaceDeclarationLocal = GetFirstNamespaceDeclarationLocal(e);
				if (firstNamespaceDeclarationLocal != null)
				{
					return firstNamespaceDeclarationLocal;
				}
				e = e.Parent;
			}
			while (e != null);
			return null;
		}

		private static XAttribute GetFirstNamespaceDeclarationLocal(XElement e)
		{
			foreach (XAttribute item in e.Attributes())
			{
				if (item.IsNamespaceDeclaration)
				{
					return item;
				}
			}
			return null;
		}

		private static XAttribute GetNextNamespaceDeclarationGlobal(XAttribute a)
		{
			XElement xElement = (XElement)a.GetParent();
			if (xElement == null)
			{
				return null;
			}
			XAttribute nextNamespaceDeclarationLocal = GetNextNamespaceDeclarationLocal(a);
			if (nextNamespaceDeclarationLocal != null)
			{
				return nextNamespaceDeclarationLocal;
			}
			xElement = xElement.Parent;
			if (xElement == null)
			{
				return null;
			}
			return GetFirstNamespaceDeclarationGlobal(xElement);
		}

		private static XAttribute GetNextNamespaceDeclarationLocal(XAttribute a)
		{
			if (a.Parent == null)
			{
				return null;
			}
			for (a = a.NextAttribute; a != null; a = a.NextAttribute)
			{
				if (a.IsNamespaceDeclaration)
				{
					return a;
				}
			}
			return null;
		}

		private static XAttribute GetXmlNamespaceDeclaration()
		{
			if (s_XmlNamespaceDeclaration == null)
			{
				Interlocked.CompareExchange(ref s_XmlNamespaceDeclaration, new XAttribute(XNamespace.Xmlns.GetName("xml"), xmlPrefixNamespace), null);
			}
			return s_XmlNamespaceDeclaration;
		}

		private static bool HasNamespaceDeclarationInScope(XAttribute a, XElement e)
		{
			XName name = a.Name;
			while (e != null && e != a.GetParent())
			{
				if (e.Attribute(name) != null)
				{
					return true;
				}
				e = e.Parent;
			}
			return false;
		}
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal readonly struct XPathEvaluator
	{
		public object Evaluate<T>(XNode node, string expression, IXmlNamespaceResolver resolver) where T : class
		{
			object obj = node.CreateNavigator().Evaluate(expression, resolver);
			if (obj is XPathNodeIterator result)
			{
				return EvaluateIterator<T>(result);
			}
			if (!(obj is T))
			{
				throw new InvalidOperationException(global::SR.Format("The XPath expression evaluated to unexpected type {0}.", obj.GetType()));
			}
			return (T)obj;
		}

		private IEnumerable<T> EvaluateIterator<T>(XPathNodeIterator result)
		{
			foreach (XPathNavigator item in result)
			{
				object r = item.UnderlyingObject;
				if (!(r is T))
				{
					throw new InvalidOperationException(global::SR.Format("The XPath expression evaluated to unexpected type {0}.", r.GetType()));
				}
				yield return (T)r;
				XText t = r as XText;
				if (t == null || t.GetParent() == null)
				{
					continue;
				}
				do
				{
					t = t.NextNode as XText;
					if (t == null)
					{
						break;
					}
					yield return (T)(object)t;
				}
				while (t != t.GetParent().LastNode);
			}
		}
	}
	/// <summary>This class contains the LINQ to XML extension methods that enable you to evaluate XPath expressions.</summary>
	public static class Extensions
	{
		/// <summary>Creates an <see cref="T:System.Xml.XPath.XPathNavigator" /> for an <see cref="T:System.Xml.Linq.XNode" />.</summary>
		/// <param name="node">An <see cref="T:System.Xml.Linq.XNode" /> that can process XPath queries.</param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> that can process XPath queries.</returns>
		public static XPathNavigator CreateNavigator(this XNode node)
		{
			return node.CreateNavigator(null);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XPath.XPathNavigator" /> for an <see cref="T:System.Xml.Linq.XNode" />. The <see cref="T:System.Xml.XmlNameTable" /> enables more efficient XPath expression processing.</summary>
		/// <param name="node">An <see cref="T:System.Xml.Linq.XNode" /> that can process an XPath query.</param>
		/// <param name="nameTable">A <see cref="T:System.Xml.XmlNameTable" /> to be used by <see cref="T:System.Xml.XPath.XPathNavigator" />.</param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> that can process XPath queries.</returns>
		public static XPathNavigator CreateNavigator(this XNode node, XmlNameTable nameTable)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node is XDocumentType)
			{
				throw new ArgumentException(global::SR.Format("This XPathNavigator cannot be created on a node of type {0}.", XmlNodeType.DocumentType));
			}
			if (node is XText xText)
			{
				if (xText.GetParent() is XDocument)
				{
					throw new ArgumentException(global::SR.Format("This XPathNavigator cannot be created on a node of type {0}.", XmlNodeType.Whitespace));
				}
				node = CalibrateText(xText);
			}
			return new XNodeNavigator(node, nameTable);
		}

		/// <summary>Evaluates an XPath expression.</summary>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> on which to evaluate the XPath expression.</param>
		/// <param name="expression">A <see cref="T:System.String" /> that contains an XPath expression.</param>
		/// <returns>An object that can contain a <see langword="bool" />, a <see langword="double" />, a <see langword="string" />, or an <see cref="T:System.Collections.Generic.IEnumerable`1" />.</returns>
		public static object XPathEvaluate(this XNode node, string expression)
		{
			return node.XPathEvaluate(expression, null);
		}

		/// <summary>Evaluates an XPath expression, resolving namespace prefixes using the specified <see cref="T:System.Xml.IXmlNamespaceResolver" />.</summary>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> on which to evaluate the XPath expression.</param>
		/// <param name="expression">A <see cref="T:System.String" /> that contains an XPath expression.</param>
		/// <param name="resolver">A <see cref="T:System.Xml.IXmlNamespaceResolver" /> for the namespace prefixes in the XPath expression.</param>
		/// <returns>An object that contains the result of evaluating the expression. The object can be a <see langword="bool" />, a <see langword="double" />, a <see langword="string" />, or an <see cref="T:System.Collections.Generic.IEnumerable`1" />.</returns>
		public static object XPathEvaluate(this XNode node, string expression, IXmlNamespaceResolver resolver)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			return default(XPathEvaluator).Evaluate<object>(node, expression, resolver);
		}

		/// <summary>Selects an <see cref="T:System.Xml.Linq.XElement" /> using a XPath expression.</summary>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> on which to evaluate the XPath expression.</param>
		/// <param name="expression">A <see cref="T:System.String" /> that contains an XPath expression.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" />, or null.</returns>
		public static XElement XPathSelectElement(this XNode node, string expression)
		{
			return node.XPathSelectElement(expression, null);
		}

		/// <summary>Selects an <see cref="T:System.Xml.Linq.XElement" /> using a XPath expression, resolving namespace prefixes using the specified <see cref="T:System.Xml.IXmlNamespaceResolver" />.</summary>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> on which to evaluate the XPath expression.</param>
		/// <param name="expression">A <see cref="T:System.String" /> that contains an XPath expression.</param>
		/// <param name="resolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> for the namespace prefixes in the XPath expression.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" />, or null.</returns>
		public static XElement XPathSelectElement(this XNode node, string expression, IXmlNamespaceResolver resolver)
		{
			return node.XPathSelectElements(expression, resolver).FirstOrDefault();
		}

		/// <summary>Selects a collection of elements using an XPath expression.</summary>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> on which to evaluate the XPath expression.</param>
		/// <param name="expression">A <see cref="T:System.String" /> that contains an XPath expression.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the selected elements.</returns>
		public static IEnumerable<XElement> XPathSelectElements(this XNode node, string expression)
		{
			return node.XPathSelectElements(expression, null);
		}

		/// <summary>Selects a collection of elements using an XPath expression, resolving namespace prefixes using the specified <see cref="T:System.Xml.IXmlNamespaceResolver" />.</summary>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> on which to evaluate the XPath expression.</param>
		/// <param name="expression">A <see cref="T:System.String" /> that contains an XPath expression.</param>
		/// <param name="resolver">A <see cref="T:System.Xml.IXmlNamespaceResolver" /> for the namespace prefixes in the XPath expression.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the selected elements.</returns>
		public static IEnumerable<XElement> XPathSelectElements(this XNode node, string expression, IXmlNamespaceResolver resolver)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			return (IEnumerable<XElement>)default(XPathEvaluator).Evaluate<XElement>(node, expression, resolver);
		}

		private static XText CalibrateText(XText n)
		{
			XContainer parent = n.GetParent();
			if (parent == null)
			{
				return n;
			}
			foreach (XNode item in parent.Nodes())
			{
				if (item is XText result && item == n)
				{
					return result;
				}
			}
			return null;
		}
	}
	internal static class XObjectExtensions
	{
		public static XContainer GetParent(this XObject obj)
		{
			XContainer xContainer = obj.Parent;
			if (xContainer == null)
			{
				xContainer = obj.Document;
			}
			if (xContainer == obj)
			{
				return null;
			}
			return xContainer;
		}
	}
	/// <summary>Extends the <see cref="T:System.Xml.Linq.XDocument" /> class by providing a method for navigating and editing an XML node.</summary>
	public static class XDocumentExtensions
	{
		private class XDocumentNavigable : IXPathNavigable
		{
			private XNode _node;

			public XDocumentNavigable(XNode n)
			{
				_node = n;
			}

			public XPathNavigator CreateNavigator()
			{
				return _node.CreateNavigator();
			}
		}

		/// <summary>Returns an accessor that allows you to navigate and edit the specified <see cref="T:System.Xml.Linq.XNode" />.</summary>
		/// <param name="node">The XML node to navigate.</param>
		/// <returns>An interface that provides an accessor to the <see cref="T:System.Xml.XPath.XPathNavigator" /> class.</returns>
		public static IXPathNavigable ToXPathNavigable(this XNode node)
		{
			return new XDocumentNavigable(node);
		}
	}
}
namespace System.Xml.Schema
{
	internal class XNodeValidator
	{
		private XmlSchemaSet schemas;

		private ValidationEventHandler validationEventHandler;

		private XObject source;

		private bool addSchemaInfo;

		private XmlNamespaceManager namespaceManager;

		private XmlSchemaValidator validator;

		private Dictionary<XmlSchemaInfo, XmlSchemaInfo> schemaInfos;

		private ArrayList defaultAttributes;

		private XName xsiTypeName;

		private XName xsiNilName;

		public XNodeValidator(XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			this.schemas = schemas;
			this.validationEventHandler = validationEventHandler;
			XNamespace xNamespace = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
			xsiTypeName = xNamespace.GetName("type");
			xsiNilName = xNamespace.GetName("nil");
		}

		public void Validate(XObject source, XmlSchemaObject partialValidationType, bool addSchemaInfo)
		{
			this.source = source;
			this.addSchemaInfo = addSchemaInfo;
			XmlSchemaValidationFlags xmlSchemaValidationFlags = XmlSchemaValidationFlags.AllowXmlAttributes;
			XmlNodeType nodeType = source.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					if (nodeType != XmlNodeType.Document)
					{
						goto IL_007a;
					}
					source = ((XDocument)source).Root;
					if (source == null)
					{
						throw new InvalidOperationException(global::SR.Format("The root element is missing."));
					}
					xmlSchemaValidationFlags |= XmlSchemaValidationFlags.ProcessIdentityConstraints;
				}
				else
				{
					if (((XAttribute)source).IsNamespaceDeclaration)
					{
						goto IL_007a;
					}
					if (source.Parent == null)
					{
						throw new InvalidOperationException(global::SR.Format("The parent is missing."));
					}
				}
			}
			namespaceManager = new XmlNamespaceManager(schemas.NameTable);
			PushAncestorsAndSelf(source.Parent);
			validator = new XmlSchemaValidator(schemas.NameTable, schemas, namespaceManager, xmlSchemaValidationFlags);
			validator.ValidationEventHandler += ValidationCallback;
			validator.XmlResolver = null;
			if (partialValidationType != null)
			{
				validator.Initialize(partialValidationType);
			}
			else
			{
				validator.Initialize();
			}
			IXmlLineInfo originalLineInfo = SaveLineInfo(source);
			if (nodeType == XmlNodeType.Attribute)
			{
				ValidateAttribute((XAttribute)source);
			}
			else
			{
				ValidateElement((XElement)source);
			}
			validator.EndValidation();
			RestoreLineInfo(originalLineInfo);
			return;
			IL_007a:
			throw new InvalidOperationException(global::SR.Format("This operation is not valid on a node of type {0}.", nodeType));
		}

		private XmlSchemaInfo GetDefaultAttributeSchemaInfo(XmlSchemaAttribute sa)
		{
			XmlSchemaInfo xmlSchemaInfo = new XmlSchemaInfo();
			xmlSchemaInfo.IsDefault = true;
			xmlSchemaInfo.IsNil = false;
			xmlSchemaInfo.SchemaAttribute = sa;
			XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)(xmlSchemaInfo.SchemaType = sa.AttributeSchemaType);
			if (xmlSchemaSimpleType.Datatype.Variety == XmlSchemaDatatypeVariety.Union)
			{
				string defaultValue = GetDefaultValue(sa);
				XmlSchemaSimpleType[] baseMemberTypes = ((XmlSchemaSimpleTypeUnion)xmlSchemaSimpleType.Content).BaseMemberTypes;
				foreach (XmlSchemaSimpleType xmlSchemaSimpleType2 in baseMemberTypes)
				{
					object obj = null;
					try
					{
						obj = xmlSchemaSimpleType2.Datatype.ParseValue(defaultValue, schemas.NameTable, namespaceManager);
					}
					catch (XmlSchemaException)
					{
					}
					if (obj != null)
					{
						xmlSchemaInfo.MemberType = xmlSchemaSimpleType2;
						break;
					}
				}
			}
			xmlSchemaInfo.Validity = XmlSchemaValidity.Valid;
			return xmlSchemaInfo;
		}

		private string GetDefaultValue(XmlSchemaAttribute sa)
		{
			XmlQualifiedName refName = sa.RefName;
			if (!refName.IsEmpty)
			{
				sa = schemas.GlobalAttributes[refName] as XmlSchemaAttribute;
				if (sa == null)
				{
					return null;
				}
			}
			string fixedValue = sa.FixedValue;
			if (fixedValue != null)
			{
				return fixedValue;
			}
			return sa.DefaultValue;
		}

		private string GetDefaultValue(XmlSchemaElement se)
		{
			XmlQualifiedName refName = se.RefName;
			if (!refName.IsEmpty)
			{
				se = schemas.GlobalElements[refName] as XmlSchemaElement;
				if (se == null)
				{
					return null;
				}
			}
			string fixedValue = se.FixedValue;
			if (fixedValue != null)
			{
				return fixedValue;
			}
			return se.DefaultValue;
		}

		private void ReplaceSchemaInfo(XObject o, XmlSchemaInfo schemaInfo)
		{
			if (schemaInfos == null)
			{
				schemaInfos = new Dictionary<XmlSchemaInfo, XmlSchemaInfo>(new XmlSchemaInfoEqualityComparer());
			}
			XmlSchemaInfo value = o.Annotation<XmlSchemaInfo>();
			if (value != null)
			{
				if (!schemaInfos.ContainsKey(value))
				{
					schemaInfos.Add(value, value);
				}
				o.RemoveAnnotations<XmlSchemaInfo>();
			}
			if (!schemaInfos.TryGetValue(schemaInfo, out value))
			{
				value = schemaInfo;
				schemaInfos.Add(value, value);
			}
			o.AddAnnotation(value);
		}

		private void PushAncestorsAndSelf(XElement e)
		{
			while (e != null)
			{
				XAttribute xAttribute = e.lastAttr;
				if (xAttribute != null)
				{
					do
					{
						xAttribute = xAttribute.next;
						if (xAttribute.IsNamespaceDeclaration)
						{
							string text = xAttribute.Name.LocalName;
							if (text == "xmlns")
							{
								text = string.Empty;
							}
							if (!namespaceManager.HasNamespace(text))
							{
								namespaceManager.AddNamespace(text, xAttribute.Value);
							}
						}
					}
					while (xAttribute != e.lastAttr);
				}
				e = e.parent as XElement;
			}
		}

		private void PushElement(XElement e, ref string xsiType, ref string xsiNil)
		{
			namespaceManager.PushScope();
			XAttribute xAttribute = e.lastAttr;
			if (xAttribute == null)
			{
				return;
			}
			do
			{
				xAttribute = xAttribute.next;
				if (xAttribute.IsNamespaceDeclaration)
				{
					string text = xAttribute.Name.LocalName;
					if (text == "xmlns")
					{
						text = string.Empty;
					}
					namespaceManager.AddNamespace(text, xAttribute.Value);
					continue;
				}
				XName name = xAttribute.Name;
				if (name == xsiTypeName)
				{
					xsiType = xAttribute.Value;
				}
				else if (name == xsiNilName)
				{
					xsiNil = xAttribute.Value;
				}
			}
			while (xAttribute != e.lastAttr);
		}

		private IXmlLineInfo SaveLineInfo(XObject source)
		{
			IXmlLineInfo lineInfoProvider = validator.LineInfoProvider;
			validator.LineInfoProvider = source;
			return lineInfoProvider;
		}

		private void RestoreLineInfo(IXmlLineInfo originalLineInfo)
		{
			validator.LineInfoProvider = originalLineInfo;
		}

		private void ValidateAttribute(XAttribute a)
		{
			IXmlLineInfo originalLineInfo = SaveLineInfo(a);
			XmlSchemaInfo schemaInfo = (addSchemaInfo ? new XmlSchemaInfo() : null);
			source = a;
			validator.ValidateAttribute(a.Name.LocalName, a.Name.NamespaceName, a.Value, schemaInfo);
			if (addSchemaInfo)
			{
				ReplaceSchemaInfo(a, schemaInfo);
			}
			RestoreLineInfo(originalLineInfo);
		}

		private void ValidateAttributes(XElement e)
		{
			XAttribute xAttribute = e.lastAttr;
			IXmlLineInfo originalLineInfo = SaveLineInfo(xAttribute);
			if (xAttribute != null)
			{
				do
				{
					xAttribute = xAttribute.next;
					if (!xAttribute.IsNamespaceDeclaration)
					{
						ValidateAttribute(xAttribute);
					}
				}
				while (xAttribute != e.lastAttr);
				source = e;
			}
			if (addSchemaInfo)
			{
				if (defaultAttributes == null)
				{
					defaultAttributes = new ArrayList();
				}
				else
				{
					defaultAttributes.Clear();
				}
				validator.GetUnspecifiedDefaultAttributes(defaultAttributes);
				foreach (XmlSchemaAttribute defaultAttribute in defaultAttributes)
				{
					xAttribute = new XAttribute(XNamespace.Get(defaultAttribute.QualifiedName.Namespace).GetName(defaultAttribute.QualifiedName.Name), GetDefaultValue(defaultAttribute));
					ReplaceSchemaInfo(xAttribute, GetDefaultAttributeSchemaInfo(defaultAttribute));
					e.Add(xAttribute);
				}
			}
			RestoreLineInfo(originalLineInfo);
		}

		private void ValidateElement(XElement e)
		{
			XmlSchemaInfo xmlSchemaInfo = (addSchemaInfo ? new XmlSchemaInfo() : null);
			string xsiType = null;
			string xsiNil = null;
			PushElement(e, ref xsiType, ref xsiNil);
			IXmlLineInfo originalLineInfo = SaveLineInfo(e);
			source = e;
			validator.ValidateElement(e.Name.LocalName, e.Name.NamespaceName, xmlSchemaInfo, xsiType, xsiNil, null, null);
			ValidateAttributes(e);
			validator.ValidateEndOfAttributes(xmlSchemaInfo);
			ValidateNodes(e);
			validator.ValidateEndElement(xmlSchemaInfo);
			if (addSchemaInfo)
			{
				if (xmlSchemaInfo.Validity == XmlSchemaValidity.Valid && xmlSchemaInfo.IsDefault)
				{
					e.Value = GetDefaultValue(xmlSchemaInfo.SchemaElement);
				}
				ReplaceSchemaInfo(e, xmlSchemaInfo);
			}
			RestoreLineInfo(originalLineInfo);
			namespaceManager.PopScope();
		}

		private void ValidateNodes(XElement e)
		{
			XNode xNode = e.content as XNode;
			IXmlLineInfo originalLineInfo = SaveLineInfo(xNode);
			if (xNode != null)
			{
				do
				{
					xNode = xNode.next;
					if (xNode is XElement e2)
					{
						ValidateElement(e2);
					}
					else if (xNode is XText { Value: var value } xText && value.Length > 0)
					{
						validator.LineInfoProvider = xText;
						validator.ValidateText(value);
					}
				}
				while (xNode != e.content);
				source = e;
			}
			else if (e.content is string { Length: >0 } text)
			{
				validator.ValidateText(text);
			}
			RestoreLineInfo(originalLineInfo);
		}

		private void ValidationCallback(object sender, ValidationEventArgs e)
		{
			if (validationEventHandler != null)
			{
				validationEventHandler(source, e);
			}
			else if (e.Severity == XmlSeverityType.Error)
			{
				throw e.Exception;
			}
		}
	}
	internal class XmlSchemaInfoEqualityComparer : IEqualityComparer<XmlSchemaInfo>
	{
		public bool Equals(XmlSchemaInfo si1, XmlSchemaInfo si2)
		{
			if (si1 == si2)
			{
				return true;
			}
			if (si1 == null || si2 == null)
			{
				return false;
			}
			if (si1.ContentType == si2.ContentType && si1.IsDefault == si2.IsDefault && si1.IsNil == si2.IsNil && si1.MemberType == si2.MemberType && si1.SchemaAttribute == si2.SchemaAttribute && si1.SchemaElement == si2.SchemaElement && si1.SchemaType == si2.SchemaType)
			{
				return si1.Validity == si2.Validity;
			}
			return false;
		}

		public int GetHashCode(XmlSchemaInfo si)
		{
			if (si == null)
			{
				return 0;
			}
			int num = (int)si.ContentType;
			if (si.IsDefault)
			{
				num ^= 1;
			}
			if (si.IsNil)
			{
				num ^= 1;
			}
			XmlSchemaSimpleType memberType = si.MemberType;
			if (memberType != null)
			{
				num ^= memberType.GetHashCode();
			}
			XmlSchemaAttribute schemaAttribute = si.SchemaAttribute;
			if (schemaAttribute != null)
			{
				num ^= schemaAttribute.GetHashCode();
			}
			XmlSchemaElement schemaElement = si.SchemaElement;
			if (schemaElement != null)
			{
				num ^= schemaElement.GetHashCode();
			}
			XmlSchemaType schemaType = si.SchemaType;
			if (schemaType != null)
			{
				num ^= schemaType.GetHashCode();
			}
			return num ^ (int)si.Validity;
		}
	}
	/// <summary>This class contains the LINQ to XML extension methods for XSD validation.</summary>
	public static class Extensions
	{
		/// <summary>Gets the post-schema-validation infoset (PSVI) of a validated element.</summary>
		/// <param name="source">An <see cref="T:System.Xml.Linq.XElement" /> that has been previously validated.</param>
		/// <returns>A <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> that contains the post-schema-validation infoset (PSVI) for an <see cref="T:System.Xml.Linq.XElement" />.</returns>
		public static IXmlSchemaInfo GetSchemaInfo(this XElement source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.Annotation<IXmlSchemaInfo>();
		}

		/// <summary>Gets the post-schema-validation infoset (PSVI) of a validated attribute.</summary>
		/// <param name="source">An <see cref="T:System.Xml.Linq.XAttribute" /> that has been previously validated.</param>
		/// <returns>A <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> that contains the post-schema-validation infoset for an <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		public static IXmlSchemaInfo GetSchemaInfo(this XAttribute source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.Annotation<IXmlSchemaInfo>();
		}

		/// <summary>This method validates that an <see cref="T:System.Xml.Linq.XDocument" /> conforms to an XSD in an <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XDocument" /> to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If <see langword="null" />, throws an exception upon validation errors.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		public static void Validate(this XDocument source, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			source.Validate(schemas, validationEventHandler, addSchemaInfo: false);
		}

		/// <summary>Validates that an <see cref="T:System.Xml.Linq.XDocument" /> conforms to an XSD in an <see cref="T:System.Xml.Schema.XmlSchemaSet" />, optionally populating the XML tree with the post-schema-validation infoset (PSVI).</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XDocument" /> to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If <see langword="null" />, throws an exception upon validation errors.</param>
		/// <param name="addSchemaInfo">A <see cref="T:System.Boolean" /> indicating whether to populate the post-schema-validation infoset (PSVI).</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		public static void Validate(this XDocument source, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler, bool addSchemaInfo)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			new XNodeValidator(schemas, validationEventHandler).Validate(source, null, addSchemaInfo);
		}

		/// <summary>This method validates that an <see cref="T:System.Xml.Linq.XElement" /> sub-tree conforms to a specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> and an <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XElement" /> to validate.</param>
		/// <param name="partialValidationType">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that specifies the sub-tree to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If <see langword="null" />, throws an exception upon validation errors.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		public static void Validate(this XElement source, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			source.Validate(partialValidationType, schemas, validationEventHandler, addSchemaInfo: false);
		}

		/// <summary>Validates that an <see cref="T:System.Xml.Linq.XElement" /> sub-tree conforms to a specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> and an <see cref="T:System.Xml.Schema.XmlSchemaSet" />, optionally populating the XML tree with the post-schema-validation infoset (PSVI).</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XElement" /> to validate.</param>
		/// <param name="partialValidationType">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that specifies the sub-tree to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If <see langword="null" />, throws an exception upon validation errors.</param>
		/// <param name="addSchemaInfo">A <see cref="T:System.Boolean" /> indicating whether to populate the post-schema-validation infoset (PSVI).</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		public static void Validate(this XElement source, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler, bool addSchemaInfo)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (partialValidationType == null)
			{
				throw new ArgumentNullException("partialValidationType");
			}
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			new XNodeValidator(schemas, validationEventHandler).Validate(source, partialValidationType, addSchemaInfo);
		}

		/// <summary>This method validates that an <see cref="T:System.Xml.Linq.XAttribute" /> conforms to a specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> and an <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XAttribute" /> to validate.</param>
		/// <param name="partialValidationType">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that specifies the sub-tree to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If <see langword="null" />, throws an exception upon validation errors.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		public static void Validate(this XAttribute source, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			source.Validate(partialValidationType, schemas, validationEventHandler, addSchemaInfo: false);
		}

		/// <summary>Validates that an <see cref="T:System.Xml.Linq.XAttribute" /> conforms to a specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> and an <see cref="T:System.Xml.Schema.XmlSchemaSet" />, optionally populating the XML tree with the post-schema-validation infoset (PSVI).</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XAttribute" /> to validate.</param>
		/// <param name="partialValidationType">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that specifies the sub-tree to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If <see langword="null" />, throws an exception upon validation errors.</param>
		/// <param name="addSchemaInfo">A <see cref="T:System.Boolean" /> indicating whether to populate the post-schema-validation infoset (PSVI).</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		public static void Validate(this XAttribute source, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler, bool addSchemaInfo)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (partialValidationType == null)
			{
				throw new ArgumentNullException("partialValidationType");
			}
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			new XNodeValidator(schemas, validationEventHandler).Validate(source, partialValidationType, addSchemaInfo);
		}
	}
}
namespace System.Xml.Linq
{
	internal class BaseUriAnnotation
	{
		internal string baseUri;

		public BaseUriAnnotation(string baseUri)
		{
			this.baseUri = baseUri;
		}
	}
	/// <summary>Contains the LINQ to XML extension methods.</summary>
	public static class Extensions
	{
		/// <summary>Returns a collection of the attributes of every element in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> that contains the attributes of every element in the source collection.</returns>
		public static IEnumerable<XAttribute> Attributes(this IEnumerable<XElement> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return GetAttributes(source, null);
		}

		/// <summary>Returns a filtered collection of the attributes of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> that contains a filtered collection of the attributes of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		public static IEnumerable<XAttribute> Attributes(this IEnumerable<XElement> source, XName name)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XAttribute.EmptySequence;
			}
			return GetAttributes(source, name);
		}

		/// <summary>Returns a collection of elements that contains the ancestors of every node in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XNode" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the ancestors of every node in the source collection.</returns>
		public static IEnumerable<XElement> Ancestors<T>(this IEnumerable<T> source) where T : XNode
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return GetAncestors(source, null, self: false);
		}

		/// <summary>Returns a filtered collection of elements that contains the ancestors of every node in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XNode" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the ancestors of every node in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		public static IEnumerable<XElement> Ancestors<T>(this IEnumerable<T> source, XName name) where T : XNode
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return GetAncestors(source, name, self: false);
		}

		/// <summary>Returns a collection of elements that contains every element in the source collection, and the ancestors of every element in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains every element in the source collection, and the ancestors of every element in the source collection.</returns>
		public static IEnumerable<XElement> AncestorsAndSelf(this IEnumerable<XElement> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return GetAncestors(source, null, self: true);
		}

		/// <summary>Returns a filtered collection of elements that contains every element in the source collection, and the ancestors of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains every element in the source collection, and the ancestors of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		public static IEnumerable<XElement> AncestorsAndSelf(this IEnumerable<XElement> source, XName name)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return GetAncestors(source, name, self: true);
		}

		/// <summary>Returns a collection of the child nodes of every document and element in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> of the child nodes of every document and element in the source collection.</returns>
		public static IEnumerable<XNode> Nodes<T>(this IEnumerable<T> source) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return NodesIterator(source);
		}

		private static IEnumerable<XNode> NodesIterator<T>(IEnumerable<T> source) where T : XContainer
		{
			foreach (T root in source)
			{
				if (root == null)
				{
					continue;
				}
				XNode n = root.LastNode;
				if (n != null)
				{
					do
					{
						n = n.next;
						yield return n;
					}
					while (n.parent == root && n != root.content);
				}
			}
		}

		/// <summary>Returns a collection of the descendant nodes of every document and element in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XContainer" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> of the descendant nodes of every document and element in the source collection.</returns>
		public static IEnumerable<XNode> DescendantNodes<T>(this IEnumerable<T> source) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return GetDescendantNodes(source, self: false);
		}

		/// <summary>Returns a collection of elements that contains the descendant elements of every element and document in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XContainer" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the descendant elements of every element and document in the source collection.</returns>
		public static IEnumerable<XElement> Descendants<T>(this IEnumerable<T> source) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return GetDescendants(source, null, self: false);
		}

		/// <summary>Returns a filtered collection of elements that contains the descendant elements of every element and document in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XContainer" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the descendant elements of every element and document in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		public static IEnumerable<XElement> Descendants<T>(this IEnumerable<T> source, XName name) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return GetDescendants(source, name, self: false);
		}

		/// <summary>Returns a collection of nodes that contains every element in the source collection, and the descendant nodes of every element in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains every element in the source collection, and the descendant nodes of every element in the source collection.</returns>
		public static IEnumerable<XNode> DescendantNodesAndSelf(this IEnumerable<XElement> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return GetDescendantNodes(source, self: true);
		}

		/// <summary>Returns a collection of elements that contains every element in the source collection, and the descendent elements of every element in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains every element in the source collection, and the descendent elements of every element in the source collection.</returns>
		public static IEnumerable<XElement> DescendantsAndSelf(this IEnumerable<XElement> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return GetDescendants(source, null, self: true);
		}

		/// <summary>Returns a filtered collection of elements that contains every element in the source collection, and the descendents of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains every element in the source collection, and the descendents of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		public static IEnumerable<XElement> DescendantsAndSelf(this IEnumerable<XElement> source, XName name)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return GetDescendants(source, name, self: true);
		}

		/// <summary>Returns a collection of the child elements of every element and document in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the child elements of every element or document in the source collection.</returns>
		public static IEnumerable<XElement> Elements<T>(this IEnumerable<T> source) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return GetElements(source, null);
		}

		/// <summary>Returns a filtered collection of the child elements of every element and document in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the child elements of every element and document in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		public static IEnumerable<XElement> Elements<T>(this IEnumerable<T> source, XName name) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return GetElements(source, name);
		}

		/// <summary>Returns a collection of nodes that contains all nodes in the source collection, sorted in document order.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XNode" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains all nodes in the source collection, sorted in document order.</returns>
		public static IEnumerable<T> InDocumentOrder<T>(this IEnumerable<T> source) where T : XNode
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return DocumentOrderIterator(source);
		}

		private static IEnumerable<T> DocumentOrderIterator<T>(IEnumerable<T> source) where T : XNode
		{
			int count;
			T[] items = System.Collections.Generic.EnumerableHelpers.ToArray(source, out count);
			if (count > 0)
			{
				Array.Sort(items, 0, count, XNode.DocumentOrderComparer);
				int i = 0;
				while (i != count)
				{
					yield return items[i];
					int num = i + 1;
					i = num;
				}
			}
		}

		/// <summary>Removes every attribute in the source collection from its parent element.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> that contains the source collection.</param>
		public static void Remove(this IEnumerable<XAttribute> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			int length;
			XAttribute[] array = System.Collections.Generic.EnumerableHelpers.ToArray(source, out length);
			for (int i = 0; i < length; i++)
			{
				array[i]?.Remove();
			}
		}

		/// <summary>Removes every node in the source collection from its parent node.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XNode" />.</typeparam>
		public static void Remove<T>(this IEnumerable<T> source) where T : XNode
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			int length;
			T[] array = System.Collections.Generic.EnumerableHelpers.ToArray(source, out length);
			for (int i = 0; i < length; i++)
			{
				array[i]?.Remove();
			}
		}

		private static IEnumerable<XAttribute> GetAttributes(IEnumerable<XElement> source, XName name)
		{
			foreach (XElement e in source)
			{
				if (e == null)
				{
					continue;
				}
				XAttribute a = e.lastAttr;
				if (a == null)
				{
					continue;
				}
				do
				{
					a = a.next;
					if (name == null || a.name == name)
					{
						yield return a;
					}
				}
				while (a.parent == e && a != e.lastAttr);
			}
		}

		private static IEnumerable<XElement> GetAncestors<T>(IEnumerable<T> source, XName name, bool self) where T : XNode
		{
			foreach (T item in source)
			{
				if (item == null)
				{
					continue;
				}
				for (XElement e = (self ? ((XNode)item) : ((XNode)item.parent)) as XElement; e != null; e = e.parent as XElement)
				{
					if (name == null || e.name == name)
					{
						yield return e;
					}
				}
			}
		}

		private static IEnumerable<XNode> GetDescendantNodes<T>(IEnumerable<T> source, bool self) where T : XContainer
		{
			foreach (T root in source)
			{
				if (root == null)
				{
					continue;
				}
				if (self)
				{
					yield return root;
				}
				XNode n = root;
				while (true)
				{
					XNode firstNode;
					if (n is XContainer xContainer && (firstNode = xContainer.FirstNode) != null)
					{
						n = firstNode;
					}
					else
					{
						while (n != null && n != root && n == n.parent.content)
						{
							n = n.parent;
						}
						if (n == null || n == root)
						{
							break;
						}
						n = n.next;
					}
					yield return n;
				}
			}
		}

		private static IEnumerable<XElement> GetDescendants<T>(IEnumerable<T> source, XName name, bool self) where T : XContainer
		{
			foreach (T root in source)
			{
				if (root == null)
				{
					continue;
				}
				if (self)
				{
					XElement xElement = (XElement)(object)root;
					if (name == null || xElement.name == name)
					{
						yield return xElement;
					}
				}
				XNode n = root;
				XContainer xContainer = root;
				while (true)
				{
					if (xContainer != null && xContainer.content is XNode)
					{
						n = ((XNode)xContainer.content).next;
					}
					else
					{
						while (n != null && n != root && n == n.parent.content)
						{
							n = n.parent;
						}
						if (n == null || n == root)
						{
							break;
						}
						n = n.next;
					}
					XElement e = n as XElement;
					if (e != null && (name == null || e.name == name))
					{
						yield return e;
					}
					xContainer = e;
				}
			}
		}

		private static IEnumerable<XElement> GetElements<T>(IEnumerable<T> source, XName name) where T : XContainer
		{
			foreach (T root in source)
			{
				if (root == null)
				{
					continue;
				}
				XNode n = root.content as XNode;
				if (n == null)
				{
					continue;
				}
				do
				{
					n = n.next;
					if (n is XElement xElement && (name == null || xElement.name == name))
					{
						yield return xElement;
					}
				}
				while (n.parent == root && n != root.content);
			}
		}
	}
	internal class LineInfoAnnotation
	{
		internal int lineNumber;

		internal int linePosition;

		public LineInfoAnnotation(int lineNumber, int linePosition)
		{
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}
	}
	internal class LineInfoEndElementAnnotation : LineInfoAnnotation
	{
		public LineInfoEndElementAnnotation(int lineNumber, int linePosition)
			: base(lineNumber, linePosition)
		{
		}
	}
	/// <summary>Represents an XML attribute.</summary>
	public class XAttribute : XObject
	{
		internal XAttribute next;

		internal XName name;

		internal string value;

		/// <summary>Gets an empty collection of attributes.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> containing an empty collection.</returns>
		public static IEnumerable<XAttribute> EmptySequence => Array.Empty<XAttribute>();

		/// <summary>Determines if this attribute is a namespace declaration.</summary>
		/// <returns>
		///   <see langword="true" /> if this attribute is a namespace declaration; otherwise <see langword="false" />.</returns>
		public bool IsNamespaceDeclaration
		{
			get
			{
				string namespaceName = name.NamespaceName;
				if (namespaceName.Length == 0)
				{
					return name.LocalName == "xmlns";
				}
				return (object)namespaceName == "http://www.w3.org/2000/xmlns/";
			}
		}

		/// <summary>Gets the expanded name of this attribute.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> containing the name of this attribute.</returns>
		public XName Name => name;

		/// <summary>Gets the next attribute of the parent element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XAttribute" /> containing the next attribute of the parent element.</returns>
		public XAttribute NextAttribute
		{
			get
			{
				if (parent == null || ((XElement)parent).lastAttr == this)
				{
					return null;
				}
				return next;
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XAttribute" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Attribute" />.</returns>
		public override XmlNodeType NodeType => XmlNodeType.Attribute;

		/// <summary>Gets the previous attribute of the parent element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XAttribute" /> containing the previous attribute of the parent element.</returns>
		public XAttribute PreviousAttribute
		{
			get
			{
				if (parent == null)
				{
					return null;
				}
				XAttribute lastAttr = ((XElement)parent).lastAttr;
				while (lastAttr.next != this)
				{
					lastAttr = lastAttr.next;
				}
				if (lastAttr == ((XElement)parent).lastAttr)
				{
					return null;
				}
				return lastAttr;
			}
		}

		/// <summary>Gets or sets the value of this attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value of this attribute.</returns>
		/// <exception cref="T:System.ArgumentNullException">When setting, the <paramref name="value" /> is <see langword="null" />.</exception>
		public string Value
		{
			get
			{
				return value;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ValidateAttribute(name, value);
				bool num = NotifyChanging(this, XObjectChangeEventArgs.Value);
				this.value = value;
				if (num)
				{
					NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XAttribute" /> class from the specified name and value.</summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> of the attribute.</param>
		/// <param name="value">An <see cref="T:System.Object" /> containing the value of the attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> or <paramref name="value" /> parameter is <see langword="null" />.</exception>
		public XAttribute(XName name, object value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string stringValue = XContainer.GetStringValue(value);
			ValidateAttribute(name, stringValue);
			this.name = name;
			this.value = stringValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XAttribute" /> class from another <see cref="T:System.Xml.Linq.XAttribute" /> object.</summary>
		/// <param name="other">An <see cref="T:System.Xml.Linq.XAttribute" /> object to copy from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="other" /> parameter is <see langword="null" />.</exception>
		public XAttribute(XAttribute other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			name = other.name;
			value = other.value;
		}

		/// <summary>Removes this attribute from its parent element.</summary>
		/// <exception cref="T:System.InvalidOperationException">The parent element is <see langword="null" />.</exception>
		public void Remove()
		{
			if (parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			((XElement)parent).RemoveAttribute(this);
		}

		/// <summary>Sets the value of this attribute.</summary>
		/// <param name="value">The value to assign to this attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> is an <see cref="T:System.Xml.Linq.XObject" />.</exception>
		public void SetValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Value = XContainer.GetStringValue(value);
		}

		/// <summary>Converts the current <see cref="T:System.Xml.Linq.XAttribute" /> object to a string representation.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the XML text representation of an attribute and its value.</returns>
		public override string ToString()
		{
			using StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
			using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
			{
				xmlWriter.WriteAttributeString(GetPrefixOfNamespace(name.Namespace), name.LocalName, name.NamespaceName, value);
			}
			return stringWriter.ToString().Trim();
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.String" />.</param>
		/// <returns>A <see cref="T:System.String" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		[CLSCompliant(false)]
		public static explicit operator string(XAttribute attribute)
		{
			return attribute?.value;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Boolean" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Boolean" />.</param>
		/// <returns>A <see cref="T:System.Boolean" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Boolean" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator bool(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToBoolean(attribute.value.ToLowerInvariant());
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Boolean" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Boolean" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Boolean" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Boolean" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator bool?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return XmlConvert.ToBoolean(attribute.value.ToLowerInvariant());
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to an <see cref="T:System.Int32" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Int32" />.</param>
		/// <returns>A <see cref="T:System.Int32" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Int32" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator int(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToInt32(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int32" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int32" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int32" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		[CLSCompliant(false)]
		public static explicit operator int?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return XmlConvert.ToInt32(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.UInt32" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.UInt32" />.</param>
		/// <returns>A <see cref="T:System.UInt32" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.UInt32" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator uint(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToUInt32(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt32" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt32" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt32" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.UInt32" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator uint?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return XmlConvert.ToUInt32(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to an <see cref="T:System.Int64" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Int64" />.</param>
		/// <returns>A <see cref="T:System.Int64" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Int64" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator long(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToInt64(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int64" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int64" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int64" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Int64" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator long?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return XmlConvert.ToInt64(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.UInt64" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.UInt64" />.</param>
		/// <returns>A <see cref="T:System.UInt64" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.UInt64" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator ulong(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToUInt64(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt64" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt64" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt64" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.UInt64" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator ulong?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return XmlConvert.ToUInt64(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Single" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Single" />.</param>
		/// <returns>A <see cref="T:System.Single" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Single" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator float(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToSingle(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Single" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Single" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Single" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Single" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator float?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return XmlConvert.ToSingle(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Double" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Double" />.</param>
		/// <returns>A <see cref="T:System.Double" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Double" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator double(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToDouble(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Double" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Double" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Double" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Double" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator double?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return XmlConvert.ToDouble(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Decimal" />.</param>
		/// <returns>A <see cref="T:System.Decimal" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Decimal" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator decimal(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToDecimal(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Decimal" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Decimal" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Decimal" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Decimal" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator decimal?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return XmlConvert.ToDecimal(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.DateTime" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.DateTime" />.</param>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.DateTime" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator DateTime(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return DateTime.Parse(attribute.value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTime" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTime" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTime" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.DateTime" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator DateTime?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return DateTime.Parse(attribute.value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.DateTimeOffset" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.DateTimeOffset" />.</param>
		/// <returns>A <see cref="T:System.DateTimeOffset" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.DateTimeOffset" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator DateTimeOffset(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToDateTimeOffset(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTimeOffset" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTimeOffset" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTimeOffset" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.DateTimeOffset" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator DateTimeOffset?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return XmlConvert.ToDateTimeOffset(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.TimeSpan" />.</param>
		/// <returns>A <see cref="T:System.TimeSpan" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.TimeSpan" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator TimeSpan(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToTimeSpan(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.TimeSpan" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.TimeSpan" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.TimeSpan" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator TimeSpan?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return XmlConvert.ToTimeSpan(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Guid" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Guid" />.</param>
		/// <returns>A <see cref="T:System.Guid" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Guid" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator Guid(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToGuid(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Guid" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Guid" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Guid" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Guid" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator Guid?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return XmlConvert.ToGuid(attribute.value);
		}

		internal int GetDeepHashCode()
		{
			return name.GetHashCode() ^ value.GetHashCode();
		}

		internal string GetPrefixOfNamespace(XNamespace ns)
		{
			string namespaceName = ns.NamespaceName;
			if (namespaceName.Length == 0)
			{
				return string.Empty;
			}
			if (parent != null)
			{
				return ((XElement)parent).GetPrefixOfNamespace(ns);
			}
			if ((object)namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			if ((object)namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		private static void ValidateAttribute(XName name, string value)
		{
			string namespaceName = name.NamespaceName;
			if ((object)namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				if (value.Length == 0)
				{
					throw new ArgumentException(global::SR.Format("The prefix '{0}' cannot be bound to the empty namespace name.", name.LocalName));
				}
				if (value == "http://www.w3.org/XML/1998/namespace")
				{
					if (name.LocalName != "xml")
					{
						throw new ArgumentException("The prefix 'xml' is bound to the namespace name 'http://www.w3.org/XML/1998/namespace'. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.");
					}
					return;
				}
				if (value == "http://www.w3.org/2000/xmlns/")
				{
					throw new ArgumentException("The prefix 'xmlns' is bound to the namespace name 'http://www.w3.org/2000/xmlns/'. It must not be declared. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.");
				}
				string localName = name.LocalName;
				if (localName == "xml")
				{
					throw new ArgumentException("The prefix 'xml' is bound to the namespace name 'http://www.w3.org/XML/1998/namespace'. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.");
				}
				if (localName == "xmlns")
				{
					throw new ArgumentException("The prefix 'xmlns' is bound to the namespace name 'http://www.w3.org/2000/xmlns/'. It must not be declared. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.");
				}
			}
			else if (namespaceName.Length == 0 && name.LocalName == "xmlns")
			{
				if (value == "http://www.w3.org/XML/1998/namespace")
				{
					throw new ArgumentException("The prefix 'xml' is bound to the namespace name 'http://www.w3.org/XML/1998/namespace'. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.");
				}
				if (value == "http://www.w3.org/2000/xmlns/")
				{
					throw new ArgumentException("The prefix 'xmlns' is bound to the namespace name 'http://www.w3.org/2000/xmlns/'. It must not be declared. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.");
				}
			}
		}
	}
	/// <summary>Represents a text node that contains CDATA.</summary>
	public class XCData : XText
	{
		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XCData" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.CDATA" />.</returns>
		public override XmlNodeType NodeType => XmlNodeType.CDATA;

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XCData" /> class.</summary>
		/// <param name="value">A string that contains the value of the <see cref="T:System.Xml.Linq.XCData" /> node.</param>
		public XCData(string value)
			: base(value)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XCData" /> class.</summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XCData" /> node to copy from.</param>
		public XCData(XCData other)
			: base(other)
		{
		}

		internal XCData(XmlReader r)
			: base(r)
		{
		}

		/// <summary>Writes this CDATA object to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteCData(text);
		}

		public override Task WriteToAsync(XmlWriter writer, CancellationToken cancellationToken)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			return writer.WriteCDataAsync(text);
		}

		internal override XNode CloneNode()
		{
			return new XCData(this);
		}
	}
	/// <summary>Represents an XML comment.</summary>
	public class XComment : XNode
	{
		internal string value;

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XComment" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Comment" />.</returns>
		public override XmlNodeType NodeType => XmlNodeType.Comment;

		/// <summary>Gets or sets the string value of this comment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the string value of this comment.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> is <see langword="null" />.</exception>
		public string Value
		{
			get
			{
				return value;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				bool num = NotifyChanging(this, XObjectChangeEventArgs.Value);
				this.value = value;
				if (num)
				{
					NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XComment" /> class with the specified string content.</summary>
		/// <param name="value">A string that contains the contents of the new <see cref="T:System.Xml.Linq.XComment" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		public XComment(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XComment" /> class from an existing comment node.</summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XComment" /> node to copy from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="other" /> parameter is <see langword="null" />.</exception>
		public XComment(XComment other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			value = other.value;
		}

		internal XComment(XmlReader r)
		{
			value = r.Value;
			r.Read();
		}

		/// <summary>Write this comment to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteComment(value);
		}

		public override Task WriteToAsync(XmlWriter writer, CancellationToken cancellationToken)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			return writer.WriteCommentAsync(value);
		}

		internal override XNode CloneNode()
		{
			return new XComment(this);
		}

		internal override bool DeepEquals(XNode node)
		{
			if (node is XComment xComment)
			{
				return value == xComment.value;
			}
			return false;
		}

		internal override int GetDeepHashCode()
		{
			return value.GetHashCode();
		}
	}
	/// <summary>Represents a node that can contain other nodes.</summary>
	public abstract class XContainer : XNode
	{
		private sealed class ContentReader
		{
			private readonly NamespaceCache _eCache;

			private readonly NamespaceCache _aCache;

			private readonly IXmlLineInfo _lineInfo;

			private XContainer _currentContainer;

			private string _baseUri;

			public ContentReader(XContainer rootContainer)
			{
				_currentContainer = rootContainer;
			}

			public ContentReader(XContainer rootContainer, XmlReader r, LoadOptions o)
			{
				_currentContainer = rootContainer;
				_baseUri = (((o & LoadOptions.SetBaseUri) != LoadOptions.None) ? r.BaseURI : null);
				_lineInfo = (((o & LoadOptions.SetLineInfo) != LoadOptions.None) ? (r as IXmlLineInfo) : null);
			}

			public bool ReadContentFrom(XContainer rootContainer, XmlReader r)
			{
				switch (r.NodeType)
				{
				case XmlNodeType.Element:
				{
					XElement xElement = new XElement(_eCache.Get(r.NamespaceURI).GetName(r.LocalName));
					if (r.MoveToFirstAttribute())
					{
						do
						{
							xElement.AppendAttributeSkipNotify(new XAttribute(_aCache.Get((r.Prefix.Length == 0) ? string.Empty : r.NamespaceURI).GetName(r.LocalName), r.Value));
						}
						while (r.MoveToNextAttribute());
						r.MoveToElement();
					}
					_currentContainer.AddNodeSkipNotify(xElement);
					if (!r.IsEmptyElement)
					{
						_currentContainer = xElement;
					}
					break;
				}
				case XmlNodeType.EndElement:
					if (_currentContainer.content == null)
					{
						_currentContainer.content = string.Empty;
					}
					if (_currentContainer == rootContainer)
					{
						return false;
					}
					_currentContainer = _currentContainer.parent;
					break;
				case XmlNodeType.Text:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					_currentContainer.AddStringSkipNotify(r.Value);
					break;
				case XmlNodeType.CDATA:
					_currentContainer.AddNodeSkipNotify(new XCData(r.Value));
					break;
				case XmlNodeType.Comment:
					_currentContainer.AddNodeSkipNotify(new XComment(r.Value));
					break;
				case XmlNodeType.ProcessingInstruction:
					_currentContainer.AddNodeSkipNotify(new XProcessingInstruction(r.Name, r.Value));
					break;
				case XmlNodeType.DocumentType:
					_currentContainer.AddNodeSkipNotify(new XDocumentType(r.LocalName, r.GetAttribute("PUBLIC"), r.GetAttribute("SYSTEM"), r.Value));
					break;
				case XmlNodeType.EntityReference:
					if (!r.CanResolveEntity)
					{
						throw new InvalidOperationException("The XmlReader cannot resolve entity references.");
					}
					r.ResolveEntity();
					break;
				default:
					throw new InvalidOperationException(global::SR.Format("The XmlReader should not be on a node of type {0}.", r.NodeType));
				case XmlNodeType.EndEntity:
					break;
				}
				return true;
			}

			public bool ReadContentFrom(XContainer rootContainer, XmlReader r, LoadOptions o)
			{
				XNode xNode = null;
				string baseURI = r.BaseURI;
				switch (r.NodeType)
				{
				case XmlNodeType.Element:
				{
					XElement xElement2 = new XElement(_eCache.Get(r.NamespaceURI).GetName(r.LocalName));
					if (_baseUri != null && _baseUri != baseURI)
					{
						xElement2.SetBaseUri(baseURI);
					}
					if (_lineInfo != null && _lineInfo.HasLineInfo())
					{
						xElement2.SetLineInfo(_lineInfo.LineNumber, _lineInfo.LinePosition);
					}
					if (r.MoveToFirstAttribute())
					{
						do
						{
							XAttribute xAttribute = new XAttribute(_aCache.Get((r.Prefix.Length == 0) ? string.Empty : r.NamespaceURI).GetName(r.LocalName), r.Value);
							if (_lineInfo != null && _lineInfo.HasLineInfo())
							{
								xAttribute.SetLineInfo(_lineInfo.LineNumber, _lineInfo.LinePosition);
							}
							xElement2.AppendAttributeSkipNotify(xAttribute);
						}
						while (r.MoveToNextAttribute());
						r.MoveToElement();
					}
					_currentContainer.AddNodeSkipNotify(xElement2);
					if (!r.IsEmptyElement)
					{
						_currentContainer = xElement2;
						if (_baseUri != null)
						{
							_baseUri = baseURI;
						}
					}
					break;
				}
				case XmlNodeType.EndElement:
					if (_currentContainer.content == null)
					{
						_currentContainer.content = string.Empty;
					}
					if (_currentContainer is XElement xElement && _lineInfo != null && _lineInfo.HasLineInfo())
					{
						xElement.SetEndElementLineInfo(_lineInfo.LineNumber, _lineInfo.LinePosition);
					}
					if (_currentContainer == rootContainer)
					{
						return false;
					}
					if (_baseUri != null && _currentContainer.HasBaseUri)
					{
						_baseUri = _currentContainer.parent.BaseUri;
					}
					_currentContainer = _currentContainer.parent;
					break;
				case XmlNodeType.Text:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					if ((_baseUri != null && _baseUri != baseURI) || (_lineInfo != null && _lineInfo.HasLineInfo()))
					{
						xNode = new XText(r.Value);
					}
					else
					{
						_currentContainer.AddStringSkipNotify(r.Value);
					}
					break;
				case XmlNodeType.CDATA:
					xNode = new XCData(r.Value);
					break;
				case XmlNodeType.Comment:
					xNode = new XComment(r.Value);
					break;
				case XmlNodeType.ProcessingInstruction:
					xNode = new XProcessingInstruction(r.Name, r.Value);
					break;
				case XmlNodeType.DocumentType:
					xNode = new XDocumentType(r.LocalName, r.GetAttribute("PUBLIC"), r.GetAttribute("SYSTEM"), r.Value);
					break;
				case XmlNodeType.EntityReference:
					if (!r.CanResolveEntity)
					{
						throw new InvalidOperationException("The XmlReader cannot resolve entity references.");
					}
					r.ResolveEntity();
					break;
				default:
					throw new InvalidOperationException(global::SR.Format("The XmlReader should not be on a node of type {0}.", r.NodeType));
				case XmlNodeType.EndEntity:
					break;
				}
				if (xNode != null)
				{
					if (_baseUri != null && _baseUri != baseURI)
					{
						xNode.SetBaseUri(baseURI);
					}
					if (_lineInfo != null && _lineInfo.HasLineInfo())
					{
						xNode.SetLineInfo(_lineInfo.LineNumber, _lineInfo.LinePosition);
					}
					_currentContainer.AddNodeSkipNotify(xNode);
					xNode = null;
				}
				return true;
			}
		}

		internal object content;

		/// <summary>Gets the first child node of this node.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNode" /> containing the first child node of the <see cref="T:System.Xml.Linq.XContainer" />.</returns>
		public XNode FirstNode => LastNode?.next;

		/// <summary>Gets the last child node of this node.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNode" /> containing the last child node of the <see cref="T:System.Xml.Linq.XContainer" />.</returns>
		public XNode LastNode
		{
			get
			{
				if (content == null)
				{
					return null;
				}
				if (content is XNode result)
				{
					return result;
				}
				if (content is string text)
				{
					if (text.Length == 0)
					{
						return null;
					}
					XText xText = new XText(text);
					xText.parent = this;
					xText.next = xText;
					Interlocked.CompareExchange<object>(ref content, (object)xText, (object)text);
				}
				return (XNode)content;
			}
		}

		internal XContainer()
		{
		}

		internal XContainer(XContainer other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other.content is string)
			{
				content = other.content;
				return;
			}
			XNode xNode = (XNode)other.content;
			if (xNode != null)
			{
				do
				{
					xNode = xNode.next;
					AppendNodeSkipNotify(xNode.CloneNode());
				}
				while (xNode != other.content);
			}
		}

		/// <summary>Adds the specified content as children of this <see cref="T:System.Xml.Linq.XContainer" />.</summary>
		/// <param name="content">A content object containing simple content or a collection of content objects to be added.</param>
		public void Add(object content)
		{
			if (SkipNotify())
			{
				AddContentSkipNotify(content);
			}
			else
			{
				if (content == null)
				{
					return;
				}
				if (content is XNode n)
				{
					AddNode(n);
					return;
				}
				if (content is string s)
				{
					AddString(s);
					return;
				}
				if (content is XAttribute a)
				{
					AddAttribute(a);
					return;
				}
				if (content is XStreamingElement other)
				{
					AddNode(new XElement(other));
					return;
				}
				if (content is object[] array)
				{
					object[] array2 = array;
					foreach (object obj in array2)
					{
						Add(obj);
					}
					return;
				}
				if (content is IEnumerable enumerable)
				{
					{
						foreach (object item in enumerable)
						{
							Add(item);
						}
						return;
					}
				}
				AddString(GetStringValue(content));
			}
		}

		/// <summary>Adds the specified content as children of this <see cref="T:System.Xml.Linq.XContainer" />.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		public void Add(params object[] content)
		{
			Add((object)content);
		}

		/// <summary>Adds the specified content as the first children of this document or element.</summary>
		/// <param name="content">A content object containing simple content or a collection of content objects to be added.</param>
		public void AddFirst(object content)
		{
			new Inserter(this, null).Add(content);
		}

		/// <summary>Adds the specified content as the first children of this document or element.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		/// <exception cref="T:System.InvalidOperationException">The parent is <see langword="null" />.</exception>
		public void AddFirst(params object[] content)
		{
			AddFirst((object)content);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlWriter" /> that can be used to add nodes to the <see cref="T:System.Xml.Linq.XContainer" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> that is ready to have content written to it.</returns>
		public XmlWriter CreateWriter()
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.ConformanceLevel = ((!(this is XDocument)) ? ConformanceLevel.Fragment : ConformanceLevel.Document);
			return XmlWriter.Create(new XNodeBuilder(this), xmlWriterSettings);
		}

		/// <summary>Returns a collection of the descendant nodes for this document or element, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> containing the descendant nodes of the <see cref="T:System.Xml.Linq.XContainer" />, in document order.</returns>
		public IEnumerable<XNode> DescendantNodes()
		{
			return GetDescendantNodes(self: false);
		}

		/// <summary>Returns a collection of the descendant elements for this document or element, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> containing the descendant elements of the <see cref="T:System.Xml.Linq.XContainer" />.</returns>
		public IEnumerable<XElement> Descendants()
		{
			return GetDescendants(null, self: false);
		}

		/// <summary>Returns a filtered collection of the descendant elements for this document or element, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> containing the descendant elements of the <see cref="T:System.Xml.Linq.XContainer" /> that match the specified <see cref="T:System.Xml.Linq.XName" />.</returns>
		public IEnumerable<XElement> Descendants(XName name)
		{
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return GetDescendants(name, self: false);
		}

		/// <summary>Gets the first (in document order) child element with the specified <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>A <see cref="T:System.Xml.Linq.XElement" /> that matches the specified <see cref="T:System.Xml.Linq.XName" />, or <see langword="null" />.</returns>
		public XElement Element(XName name)
		{
			XNode xNode = content as XNode;
			if (xNode != null)
			{
				do
				{
					xNode = xNode.next;
					if (xNode is XElement xElement && xElement.name == name)
					{
						return xElement;
					}
				}
				while (xNode != content);
			}
			return null;
		}

		/// <summary>Returns a collection of the child elements of this element or document, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> containing the child elements of this <see cref="T:System.Xml.Linq.XContainer" />, in document order.</returns>
		public IEnumerable<XElement> Elements()
		{
			return GetElements(null);
		}

		/// <summary>Returns a filtered collection of the child elements of this element or document, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> containing the children of the <see cref="T:System.Xml.Linq.XContainer" /> that have a matching <see cref="T:System.Xml.Linq.XName" />, in document order.</returns>
		public IEnumerable<XElement> Elements(XName name)
		{
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return GetElements(name);
		}

		/// <summary>Returns a collection of the child nodes of this element or document, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> containing the contents of this <see cref="T:System.Xml.Linq.XContainer" />, in document order.</returns>
		public IEnumerable<XNode> Nodes()
		{
			XNode n = LastNode;
			if (n != null)
			{
				do
				{
					n = n.next;
					yield return n;
				}
				while (n.parent == this && n != content);
			}
		}

		/// <summary>Removes the child nodes from this document or element.</summary>
		public void RemoveNodes()
		{
			if (SkipNotify())
			{
				RemoveNodesSkipNotify();
				return;
			}
			while (content != null)
			{
				if (content is string text)
				{
					if (text.Length > 0)
					{
						ConvertTextToNode();
					}
					else if (this is XElement)
					{
						NotifyChanging(this, XObjectChangeEventArgs.Value);
						if (text != content)
						{
							throw new InvalidOperationException("This operation was corrupted by external code.");
						}
						content = null;
						NotifyChanged(this, XObjectChangeEventArgs.Value);
					}
					else
					{
						content = null;
					}
				}
				if (content is XNode { next: var xNode2 } xNode)
				{
					NotifyChanging(xNode2, XObjectChangeEventArgs.Remove);
					if (xNode != content || xNode2 != xNode.next)
					{
						throw new InvalidOperationException("This operation was corrupted by external code.");
					}
					if (xNode2 != xNode)
					{
						xNode.next = xNode2.next;
					}
					else
					{
						content = null;
					}
					xNode2.parent = null;
					xNode2.next = null;
					NotifyChanged(xNode2, XObjectChangeEventArgs.Remove);
				}
			}
		}

		/// <summary>Replaces the children nodes of this document or element with the specified content.</summary>
		/// <param name="content">A content object containing simple content or a collection of content objects that replace the children nodes.</param>
		public void ReplaceNodes(object content)
		{
			content = GetContentSnapshot(content);
			RemoveNodes();
			Add(content);
		}

		/// <summary>Replaces the children nodes of this document or element with the specified content.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		public void ReplaceNodes(params object[] content)
		{
			ReplaceNodes((object)content);
		}

		internal virtual void AddAttribute(XAttribute a)
		{
		}

		internal virtual void AddAttributeSkipNotify(XAttribute a)
		{
		}

		internal void AddContentSkipNotify(object content)
		{
			if (content == null)
			{
				return;
			}
			if (content is XNode n)
			{
				AddNodeSkipNotify(n);
				return;
			}
			if (content is string s)
			{
				AddStringSkipNotify(s);
				return;
			}
			if (content is XAttribute a)
			{
				AddAttributeSkipNotify(a);
				return;
			}
			if (content is XStreamingElement other)
			{
				AddNodeSkipNotify(new XElement(other));
				return;
			}
			if (content is object[] array)
			{
				object[] array2 = array;
				foreach (object obj in array2)
				{
					AddContentSkipNotify(obj);
				}
				return;
			}
			if (content is IEnumerable enumerable)
			{
				{
					foreach (object item in enumerable)
					{
						AddContentSkipNotify(item);
					}
					return;
				}
			}
			AddStringSkipNotify(GetStringValue(content));
		}

		internal void AddNode(XNode n)
		{
			ValidateNode(n, this);
			if (n.parent != null)
			{
				n = n.CloneNode();
			}
			else
			{
				XNode xNode = this;
				while (xNode.parent != null)
				{
					xNode = xNode.parent;
				}
				if (n == xNode)
				{
					n = n.CloneNode();
				}
			}
			ConvertTextToNode();
			AppendNode(n);
		}

		internal void AddNodeSkipNotify(XNode n)
		{
			ValidateNode(n, this);
			if (n.parent != null)
			{
				n = n.CloneNode();
			}
			else
			{
				XNode xNode = this;
				while (xNode.parent != null)
				{
					xNode = xNode.parent;
				}
				if (n == xNode)
				{
					n = n.CloneNode();
				}
			}
			ConvertTextToNode();
			AppendNodeSkipNotify(n);
		}

		internal void AddString(string s)
		{
			ValidateString(s);
			if (content == null)
			{
				if (s.Length > 0)
				{
					AppendNode(new XText(s));
				}
				else if (this is XElement)
				{
					NotifyChanging(this, XObjectChangeEventArgs.Value);
					if (content != null)
					{
						throw new InvalidOperationException("This operation was corrupted by external code.");
					}
					content = s;
					NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
				else
				{
					content = s;
				}
			}
			else if (s.Length > 0)
			{
				ConvertTextToNode();
				if (content is XText xText && !(xText is XCData))
				{
					xText.Value += s;
				}
				else
				{
					AppendNode(new XText(s));
				}
			}
		}

		internal void AddStringSkipNotify(string s)
		{
			ValidateString(s);
			if (content == null)
			{
				content = s;
			}
			else if (s.Length > 0)
			{
				if (content is string text)
				{
					content = text + s;
				}
				else if (content is XText xText && !(xText is XCData))
				{
					xText.text += s;
				}
				else
				{
					AppendNodeSkipNotify(new XText(s));
				}
			}
		}

		internal void AppendNode(XNode n)
		{
			bool num = NotifyChanging(n, XObjectChangeEventArgs.Add);
			if (n.parent != null)
			{
				throw new InvalidOperationException("This operation was corrupted by external code.");
			}
			AppendNodeSkipNotify(n);
			if (num)
			{
				NotifyChanged(n, XObjectChangeEventArgs.Add);
			}
		}

		internal void AppendNodeSkipNotify(XNode n)
		{
			n.parent = this;
			if (content == null || content is string)
			{
				n.next = n;
			}
			else
			{
				XNode xNode = (XNode)content;
				n.next = xNode.next;
				xNode.next = n;
			}
			content = n;
		}

		internal override void AppendText(StringBuilder sb)
		{
			if (content is string value)
			{
				sb.Append(value);
				return;
			}
			XNode xNode = (XNode)content;
			if (xNode != null)
			{
				do
				{
					xNode = xNode.next;
					xNode.AppendText(sb);
				}
				while (xNode != content);
			}
		}

		private string GetTextOnly()
		{
			if (content == null)
			{
				return null;
			}
			string text = content as string;
			if (text == null)
			{
				XNode xNode = (XNode)content;
				do
				{
					xNode = xNode.next;
					if (xNode.NodeType != XmlNodeType.Text)
					{
						return null;
					}
					text += ((XText)xNode).Value;
				}
				while (xNode != content);
			}
			return text;
		}

		private string CollectText(ref XNode n)
		{
			string text = "";
			while (n != null && n.NodeType == XmlNodeType.Text)
			{
				text += ((XText)n).Value;
				n = ((n != content) ? n.next : null);
			}
			return text;
		}

		internal bool ContentsEqual(XContainer e)
		{
			if (content == e.content)
			{
				return true;
			}
			string textOnly = GetTextOnly();
			if (textOnly != null)
			{
				return textOnly == e.GetTextOnly();
			}
			XNode xNode = content as XNode;
			XNode xNode2 = e.content as XNode;
			if (xNode != null && xNode2 != null)
			{
				xNode = xNode.next;
				xNode2 = xNode2.next;
				while (!(CollectText(ref xNode) != e.CollectText(ref xNode2)))
				{
					if (xNode == null && xNode2 == null)
					{
						return true;
					}
					if (xNode == null || xNode2 == null || !xNode.DeepEquals(xNode2))
					{
						break;
					}
					xNode = ((xNode != content) ? xNode.next : null);
					xNode2 = ((xNode2 != e.content) ? xNode2.next : null);
				}
			}
			return false;
		}

		internal int ContentsHashCode()
		{
			string textOnly = GetTextOnly();
			if (textOnly != null)
			{
				return textOnly.GetHashCode();
			}
			int num = 0;
			XNode n = content as XNode;
			if (n != null)
			{
				do
				{
					n = n.next;
					string text = CollectText(ref n);
					if (text.Length > 0)
					{
						num ^= text.GetHashCode();
					}
					if (n == null)
					{
						break;
					}
					num ^= n.GetDeepHashCode();
				}
				while (n != content);
			}
			return num;
		}

		internal void ConvertTextToNode()
		{
			string value = content as string;
			if (!string.IsNullOrEmpty(value))
			{
				XText xText = new XText(value);
				xText.parent = this;
				xText.next = xText;
				content = xText;
			}
		}

		internal IEnumerable<XNode> GetDescendantNodes(bool self)
		{
			if (self)
			{
				yield return this;
			}
			XNode n = this;
			while (true)
			{
				XNode firstNode;
				if (n is XContainer xContainer && (firstNode = xContainer.FirstNode) != null)
				{
					n = firstNode;
				}
				else
				{
					while (n != null && n != this && n == n.parent.content)
					{
						n = n.parent;
					}
					if (n == null || n == this)
					{
						break;
					}
					n = n.next;
				}
				yield return n;
			}
		}

		internal IEnumerable<XElement> GetDescendants(XName name, bool self)
		{
			if (self)
			{
				XElement xElement = (XElement)this;
				if (name == null || xElement.name == name)
				{
					yield return xElement;
				}
			}
			XNode n = this;
			XContainer xContainer = this;
			while (true)
			{
				if (xContainer != null && xContainer.content is XNode)
				{
					n = ((XNode)xContainer.content).next;
				}
				else
				{
					while (n != this && n == n.parent.content)
					{
						n = n.parent;
					}
					if (n == this)
					{
						break;
					}
					n = n.next;
				}
				XElement e = n as XElement;
				if (e != null && (name == null || e.name == name))
				{
					yield return e;
				}
				xContainer = e;
			}
		}

		private IEnumerable<XElement> GetElements(XName name)
		{
			XNode n = content as XNode;
			if (n == null)
			{
				yield break;
			}
			do
			{
				n = n.next;
				if (n is XElement xElement && (name == null || xElement.name == name))
				{
					yield return xElement;
				}
			}
			while (n.parent == this && n != content);
		}

		internal static string GetStringValue(object value)
		{
			if (value is string result)
			{
				return result;
			}
			string text;
			if (value is double)
			{
				text = XmlConvert.ToString((double)value);
			}
			else if (value is float)
			{
				text = XmlConvert.ToString((float)value);
			}
			else if (value is decimal)
			{
				text = XmlConvert.ToString((decimal)value);
			}
			else if (value is bool)
			{
				text = XmlConvert.ToString((bool)value);
			}
			else if (value is DateTime)
			{
				text = XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.RoundtripKind);
			}
			else if (value is DateTimeOffset)
			{
				text = XmlConvert.ToString((DateTimeOffset)value);
			}
			else if (value is TimeSpan)
			{
				text = XmlConvert.ToString((TimeSpan)value);
			}
			else
			{
				if (value is XObject)
				{
					throw new ArgumentException("An XObject cannot be used as a value.");
				}
				text = value.ToString();
			}
			if (text == null)
			{
				throw new ArgumentException("The argument cannot be converted to a string.");
			}
			return text;
		}

		internal void ReadContentFrom(XmlReader r)
		{
			if (r.ReadState != ReadState.Interactive)
			{
				throw new InvalidOperationException("The XmlReader state should be Interactive.");
			}
			ContentReader contentReader = new ContentReader(this);
			while (contentReader.ReadContentFrom(this, r) && r.Read())
			{
			}
		}

		internal void ReadContentFrom(XmlReader r, LoadOptions o)
		{
			if ((o & (LoadOptions.SetBaseUri | LoadOptions.SetLineInfo)) == 0)
			{
				ReadContentFrom(r);
				return;
			}
			if (r.ReadState != ReadState.Interactive)
			{
				throw new InvalidOperationException("The XmlReader state should be Interactive.");
			}
			ContentReader contentReader = new ContentReader(this, r, o);
			while (contentReader.ReadContentFrom(this, r, o) && r.Read())
			{
			}
		}

		internal async Task ReadContentFromAsync(XmlReader r, CancellationToken cancellationToken)
		{
			if (r.ReadState != ReadState.Interactive)
			{
				throw new InvalidOperationException("The XmlReader state should be Interactive.");
			}
			ContentReader cr = new ContentReader(this);
			bool flag;
			do
			{
				cancellationToken.ThrowIfCancellationRequested();
				flag = cr.ReadContentFrom(this, r);
				if (flag)
				{
					flag = await r.ReadAsync().ConfigureAwait(continueOnCapturedContext: false);
				}
			}
			while (flag);
		}

		internal async Task ReadContentFromAsync(XmlReader r, LoadOptions o, CancellationToken cancellationToken)
		{
			if ((o & (LoadOptions.SetBaseUri | LoadOptions.SetLineInfo)) == 0)
			{
				await ReadContentFromAsync(r, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				return;
			}
			if (r.ReadState != ReadState.Interactive)
			{
				throw new InvalidOperationException("The XmlReader state should be Interactive.");
			}
			ContentReader cr = new ContentReader(this, r, o);
			bool flag;
			do
			{
				cancellationToken.ThrowIfCancellationRequested();
				flag = cr.ReadContentFrom(this, r, o);
				if (flag)
				{
					flag = await r.ReadAsync().ConfigureAwait(continueOnCapturedContext: false);
				}
			}
			while (flag);
		}

		internal void RemoveNode(XNode n)
		{
			bool flag = NotifyChanging(n, XObjectChangeEventArgs.Remove);
			if (n.parent != this)
			{
				throw new InvalidOperationException("This operation was corrupted by external code.");
			}
			XNode xNode = (XNode)content;
			while (xNode.next != n)
			{
				xNode = xNode.next;
			}
			if (xNode == n)
			{
				content = null;
			}
			else
			{
				if (content == n)
				{
					content = xNode;
				}
				xNode.next = n.next;
			}
			n.parent = null;
			n.next = null;
			if (flag)
			{
				NotifyChanged(n, XObjectChangeEventArgs.Remove);
			}
		}

		private void RemoveNodesSkipNotify()
		{
			XNode xNode = content as XNode;
			if (xNode != null)
			{
				do
				{
					XNode xNode2 = xNode.next;
					xNode.parent = null;
					xNode.next = null;
					xNode = xNode2;
				}
				while (xNode != content);
			}
			content = null;
		}

		internal virtual void ValidateNode(XNode node, XNode previous)
		{
		}

		internal virtual void ValidateString(string s)
		{
		}

		internal void WriteContentTo(XmlWriter writer)
		{
			if (content == null)
			{
				return;
			}
			if (content is string text)
			{
				if (this is XDocument)
				{
					writer.WriteWhitespace(text);
				}
				else
				{
					writer.WriteString(text);
				}
				return;
			}
			XNode xNode = (XNode)content;
			do
			{
				xNode = xNode.next;
				xNode.WriteTo(writer);
			}
			while (xNode != content);
		}

		internal async Task WriteContentToAsync(XmlWriter writer, CancellationToken cancellationToken)
		{
			if (content == null)
			{
				return;
			}
			if (content is string text)
			{
				cancellationToken.ThrowIfCancellationRequested();
				Task task = ((!(this is XDocument)) ? writer.WriteStringAsync(text) : writer.WriteWhitespaceAsync(text));
				await task.ConfigureAwait(continueOnCapturedContext: false);
				return;
			}
			XNode n = (XNode)content;
			do
			{
				n = n.next;
				await n.WriteToAsync(writer, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			while (n != content);
		}

		private static void AddContentToList(List<object> list, object content)
		{
			IEnumerable enumerable = ((content is string) ? null : (content as IEnumerable));
			if (enumerable == null)
			{
				list.Add(content);
				return;
			}
			foreach (object item in enumerable)
			{
				if (item != null)
				{
					AddContentToList(list, item);
				}
			}
		}

		internal static object GetContentSnapshot(object content)
		{
			if (content is string || !(content is IEnumerable))
			{
				return content;
			}
			List<object> list = new List<object>();
			AddContentToList(list, content);
			return list;
		}
	}
	/// <summary>Represents an XML declaration.</summary>
	public class XDeclaration
	{
		private string _version;

		private string _encoding;

		private string _standalone;

		/// <summary>Gets or sets the encoding for this document.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the code page name for this document.</returns>
		public string Encoding
		{
			get
			{
				return _encoding;
			}
			set
			{
				_encoding = value;
			}
		}

		/// <summary>Gets or sets the standalone property for this document.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the standalone property for this document.</returns>
		public string Standalone
		{
			get
			{
				return _standalone;
			}
			set
			{
				_standalone = value;
			}
		}

		/// <summary>Gets or sets the version property for this document.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the version property for this document.</returns>
		public string Version
		{
			get
			{
				return _version;
			}
			set
			{
				_version = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDeclaration" /> class with the specified version, encoding, and standalone status.</summary>
		/// <param name="version">The version of the XML, usually "1.0".</param>
		/// <param name="encoding">The encoding for the XML document.</param>
		/// <param name="standalone">A string containing "yes" or "no" that specifies whether the XML is standalone or requires external entities to be resolved.</param>
		public XDeclaration(string version, string encoding, string standalone)
		{
			_version = version;
			_encoding = encoding;
			_standalone = standalone;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDeclaration" /> class from another <see cref="T:System.Xml.Linq.XDeclaration" /> object.</summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XDeclaration" /> used to initialize this <see cref="T:System.Xml.Linq.XDeclaration" /> object.</param>
		public XDeclaration(XDeclaration other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			_version = other._version;
			_encoding = other._encoding;
			_standalone = other._standalone;
		}

		internal XDeclaration(XmlReader r)
		{
			_version = r.GetAttribute("version");
			_encoding = r.GetAttribute("encoding");
			_standalone = r.GetAttribute("standalone");
			r.Read();
		}

		/// <summary>Provides the declaration as a formatted string.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the formatted XML string.</returns>
		public override string ToString()
		{
			StringBuilder stringBuilder = System.Text.StringBuilderCache.Acquire();
			stringBuilder.Append("<?xml");
			if (_version != null)
			{
				stringBuilder.Append(" version=\"");
				stringBuilder.Append(_version);
				stringBuilder.Append('"');
			}
			if (_encoding != null)
			{
				stringBuilder.Append(" encoding=\"");
				stringBuilder.Append(_encoding);
				stringBuilder.Append('"');
			}
			if (_standalone != null)
			{
				stringBuilder.Append(" standalone=\"");
				stringBuilder.Append(_standalone);
				stringBuilder.Append('"');
			}
			stringBuilder.Append("?>");
			return System.Text.StringBuilderCache.GetStringAndRelease(stringBuilder);
		}
	}
	/// <summary>Represents an XML document. For the components and usage of an <see cref="T:System.Xml.Linq.XDocument" /> object, see XDocument Class Overview.</summary>
	public class XDocument : XContainer
	{
		private XDeclaration _declaration;

		/// <summary>Gets or sets the XML declaration for this document.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XDeclaration" /> that contains the XML declaration for this document.</returns>
		public XDeclaration Declaration
		{
			get
			{
				return _declaration;
			}
			set
			{
				_declaration = value;
			}
		}

		/// <summary>Gets the Document Type Definition (DTD) for this document.</summary>
		/// <returns>A <see cref="T:System.Xml.Linq.XDocumentType" /> that contains the DTD for this document.</returns>
		public XDocumentType DocumentType => GetFirstNode<XDocumentType>();

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XDocument" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Document" />.</returns>
		public override XmlNodeType NodeType => XmlNodeType.Document;

		/// <summary>Gets the root element of the XML Tree for this document.</summary>
		/// <returns>The root <see cref="T:System.Xml.Linq.XElement" /> of the XML tree.</returns>
		public XElement Root => GetFirstNode<XElement>();

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDocument" /> class.</summary>
		public XDocument()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDocument" /> class with the specified content.</summary>
		/// <param name="content">A parameter list of content objects to add to this document.</param>
		public XDocument(params object[] content)
			: this()
		{
			AddContentSkipNotify(content);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDocument" /> class with the specified <see cref="T:System.Xml.Linq.XDeclaration" /> and content.</summary>
		/// <param name="declaration">An <see cref="T:System.Xml.Linq.XDeclaration" /> for the document.</param>
		/// <param name="content">The content of the document.</param>
		public XDocument(XDeclaration declaration, params object[] content)
			: this(content)
		{
			_declaration = declaration;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDocument" /> class from an existing <see cref="T:System.Xml.Linq.XDocument" /> object.</summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XDocument" /> object that will be copied.</param>
		public XDocument(XDocument other)
			: base(other)
		{
			if (other._declaration != null)
			{
				_declaration = new XDeclaration(other._declaration);
			}
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a file.</summary>
		/// <param name="uri">A URI string that references the file to load into a new <see cref="T:System.Xml.Linq.XDocument" />.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the contents of the specified file.</returns>
		public static XDocument Load(string uri)
		{
			return Load(uri, LoadOptions.None);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a file, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <param name="uri">A URI string that references the file to load into a new <see cref="T:System.Xml.Linq.XDocument" />.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the contents of the specified file.</returns>
		public static XDocument Load(string uri, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			using XmlReader reader = XmlReader.Create(uri, xmlReaderSettings);
			return Load(reader, options);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> instance by using the specified stream.</summary>
		/// <param name="stream">The stream that contains the XML data.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> object that reads the data that is contained in the stream.</returns>
		public static XDocument Load(Stream stream)
		{
			return Load(stream, LoadOptions.None);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> instance by using the specified stream, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <param name="stream">The stream containing the XML data.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies whether to load base URI and line information.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> object that reads the data that is contained in the stream.</returns>
		public static XDocument Load(Stream stream, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			using XmlReader reader = XmlReader.Create(stream, xmlReaderSettings);
			return Load(reader, options);
		}

		public static async Task<XDocument> LoadAsync(Stream stream, LoadOptions options, CancellationToken cancellationToken)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			xmlReaderSettings.Async = true;
			using XmlReader r = XmlReader.Create(stream, xmlReaderSettings);
			return await LoadAsync(r, options, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="textReader">A <see cref="T:System.IO.TextReader" /> that contains the content for the <see cref="T:System.Xml.Linq.XDocument" />.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the contents of the specified <see cref="T:System.IO.TextReader" />.</returns>
		public static XDocument Load(TextReader textReader)
		{
			return Load(textReader, LoadOptions.None);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a <see cref="T:System.IO.TextReader" />, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <param name="textReader">A <see cref="T:System.IO.TextReader" /> that contains the content for the <see cref="T:System.Xml.Linq.XDocument" />.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the XML that was read from the specified <see cref="T:System.IO.TextReader" />.</returns>
		public static XDocument Load(TextReader textReader, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			using XmlReader reader = XmlReader.Create(textReader, xmlReaderSettings);
			return Load(reader, options);
		}

		public static async Task<XDocument> LoadAsync(TextReader textReader, LoadOptions options, CancellationToken cancellationToken)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			xmlReaderSettings.Async = true;
			using XmlReader r = XmlReader.Create(textReader, xmlReaderSettings);
			return await LoadAsync(r, options, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" /> that contains the content for the <see cref="T:System.Xml.Linq.XDocument" />.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the contents of the specified <see cref="T:System.Xml.XmlReader" />.</returns>
		public static XDocument Load(XmlReader reader)
		{
			return Load(reader, LoadOptions.None);
		}

		/// <summary>Loads an <see cref="T:System.Xml.Linq.XDocument" /> from an <see cref="T:System.Xml.XmlReader" />, optionally setting the base URI, and retaining line information.</summary>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" /> that will be read for the content of the <see cref="T:System.Xml.Linq.XDocument" />.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies whether to load base URI and line information.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the XML that was read from the specified <see cref="T:System.Xml.XmlReader" />.</returns>
		public static XDocument Load(XmlReader reader, LoadOptions options)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.ReadState == ReadState.Initial)
			{
				reader.Read();
			}
			XDocument xDocument = InitLoad(reader, options);
			xDocument.ReadContentFrom(reader, options);
			if (!reader.EOF)
			{
				throw new InvalidOperationException("The XmlReader state should be EndOfFile after this operation.");
			}
			if (xDocument.Root == null)
			{
				throw new InvalidOperationException("The root element is missing.");
			}
			return xDocument;
		}

		public static Task<XDocument> LoadAsync(XmlReader reader, LoadOptions options, CancellationToken cancellationToken)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<XDocument>(cancellationToken);
			}
			return LoadAsyncInternal(reader, options, cancellationToken);
		}

		private static async Task<XDocument> LoadAsyncInternal(XmlReader reader, LoadOptions options, CancellationToken cancellationToken)
		{
			if (reader.ReadState == ReadState.Initial)
			{
				await reader.ReadAsync().ConfigureAwait(continueOnCapturedContext: false);
			}
			XDocument d = InitLoad(reader, options);
			await d.ReadContentFromAsync(reader, options, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (!reader.EOF)
			{
				throw new InvalidOperationException("The XmlReader state should be EndOfFile after this operation.");
			}
			if (d.Root == null)
			{
				throw new InvalidOperationException("The root element is missing.");
			}
			return d;
		}

		private static XDocument InitLoad(XmlReader reader, LoadOptions options)
		{
			XDocument xDocument = new XDocument();
			if ((options & LoadOptions.SetBaseUri) != LoadOptions.None)
			{
				string baseURI = reader.BaseURI;
				if (!string.IsNullOrEmpty(baseURI))
				{
					xDocument.SetBaseUri(baseURI);
				}
			}
			if ((options & LoadOptions.SetLineInfo) != LoadOptions.None && reader is IXmlLineInfo xmlLineInfo && xmlLineInfo.HasLineInfo())
			{
				xDocument.SetLineInfo(xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
			}
			if (reader.NodeType == XmlNodeType.XmlDeclaration)
			{
				xDocument.Declaration = new XDeclaration(reader);
			}
			return xDocument;
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a string.</summary>
		/// <param name="text">A string that contains XML.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> populated from the string that contains XML.</returns>
		public static XDocument Parse(string text)
		{
			return Parse(text, LoadOptions.None);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a string, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <param name="text">A string that contains XML.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> populated from the string that contains XML.</returns>
		public static XDocument Parse(string text, LoadOptions options)
		{
			using StringReader input = new StringReader(text);
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			using XmlReader reader = XmlReader.Create(input, xmlReaderSettings);
			return Load(reader, options);
		}

		/// <summary>Outputs this <see cref="T:System.Xml.Linq.XDocument" /> to the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">The stream to output this <see cref="T:System.Xml.Linq.XDocument" /> to.</param>
		public void Save(Stream stream)
		{
			Save(stream, GetSaveOptionsFromAnnotations());
		}

		/// <summary>Outputs this <see cref="T:System.Xml.Linq.XDocument" /> to the specified <see cref="T:System.IO.Stream" />, optionally specifying formatting behavior.</summary>
		/// <param name="stream">The stream to output this <see cref="T:System.Xml.Linq.XDocument" /> to.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		public void Save(Stream stream, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			if (_declaration != null && !string.IsNullOrEmpty(_declaration.Encoding))
			{
				try
				{
					xmlWriterSettings.Encoding = Encoding.GetEncoding(_declaration.Encoding);
				}
				catch (ArgumentException)
				{
				}
			}
			using XmlWriter writer = XmlWriter.Create(stream, xmlWriterSettings);
			Save(writer);
		}

		public async Task SaveAsync(Stream stream, SaveOptions options, CancellationToken cancellationToken)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			xmlWriterSettings.Async = true;
			if (_declaration != null && !string.IsNullOrEmpty(_declaration.Encoding))
			{
				try
				{
					xmlWriterSettings.Encoding = Encoding.GetEncoding(_declaration.Encoding);
				}
				catch (ArgumentException)
				{
				}
			}
			using XmlWriter w = XmlWriter.Create(stream, xmlWriterSettings);
			await WriteToAsync(w, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		/// <summary>Serialize this <see cref="T:System.Xml.Linq.XDocument" /> to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="textWriter">A <see cref="T:System.IO.TextWriter" /> that the <see cref="T:System.Xml.Linq.XDocument" /> will be written to.</param>
		public void Save(TextWriter textWriter)
		{
			Save(textWriter, GetSaveOptionsFromAnnotations());
		}

		/// <summary>Serialize this <see cref="T:System.Xml.Linq.XDocument" /> to a <see cref="T:System.IO.TextWriter" />, optionally disabling formatting.</summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> to output the XML to.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		public void Save(TextWriter textWriter, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using XmlWriter writer = XmlWriter.Create(textWriter, xmlWriterSettings);
			Save(writer);
		}

		/// <summary>Serialize this <see cref="T:System.Xml.Linq.XDocument" /> to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" /> that the <see cref="T:System.Xml.Linq.XDocument" /> will be written to.</param>
		public void Save(XmlWriter writer)
		{
			WriteTo(writer);
		}

		public async Task SaveAsync(TextWriter textWriter, SaveOptions options, CancellationToken cancellationToken)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			xmlWriterSettings.Async = true;
			using XmlWriter w = XmlWriter.Create(textWriter, xmlWriterSettings);
			await WriteToAsync(w, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		/// <summary>Serialize this <see cref="T:System.Xml.Linq.XDocument" /> to a file, overwriting an existing file, if it exists.</summary>
		/// <param name="fileName">A string that contains the name of the file.</param>
		public void Save(string fileName)
		{
			Save(fileName, GetSaveOptionsFromAnnotations());
		}

		public Task SaveAsync(XmlWriter writer, CancellationToken cancellationToken)
		{
			return WriteToAsync(writer, cancellationToken);
		}

		/// <summary>Serialize this <see cref="T:System.Xml.Linq.XDocument" /> to a file, optionally disabling formatting.</summary>
		/// <param name="fileName">A string that contains the name of the file.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		public void Save(string fileName, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			if (_declaration != null && !string.IsNullOrEmpty(_declaration.Encoding))
			{
				try
				{
					xmlWriterSettings.Encoding = Encoding.GetEncoding(_declaration.Encoding);
				}
				catch (ArgumentException)
				{
				}
			}
			using XmlWriter writer = XmlWriter.Create(fileName, xmlWriterSettings);
			Save(writer);
		}

		/// <summary>Write this document to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (_declaration != null && _declaration.Standalone == "yes")
			{
				writer.WriteStartDocument(standalone: true);
			}
			else if (_declaration != null && _declaration.Standalone == "no")
			{
				writer.WriteStartDocument(standalone: false);
			}
			else
			{
				writer.WriteStartDocument();
			}
			WriteContentTo(writer);
			writer.WriteEndDocument();
		}

		public override Task WriteToAsync(XmlWriter writer, CancellationToken cancellationToken)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			return WriteToAsyncInternal(writer, cancellationToken);
		}

		private async Task WriteToAsyncInternal(XmlWriter writer, CancellationToken cancellationToken)
		{
			Task task = ((_declaration != null && _declaration.Standalone == "yes") ? writer.WriteStartDocumentAsync(standalone: true) : ((_declaration == null || !(_declaration.Standalone == "no")) ? writer.WriteStartDocumentAsync() : writer.WriteStartDocumentAsync(standalone: false)));
			await task.ConfigureAwait(continueOnCapturedContext: false);
			await WriteContentToAsync(writer, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await writer.WriteEndDocumentAsync().ConfigureAwait(continueOnCapturedContext: false);
		}

		internal override void AddAttribute(XAttribute a)
		{
			throw new ArgumentException("An attribute cannot be added to content.");
		}

		internal override void AddAttributeSkipNotify(XAttribute a)
		{
			throw new ArgumentException("An attribute cannot be added to content.");
		}

		internal override XNode CloneNode()
		{
			return new XDocument(this);
		}

		internal override bool DeepEquals(XNode node)
		{
			if (node is XDocument e)
			{
				return ContentsEqual(e);
			}
			return false;
		}

		internal override int GetDeepHashCode()
		{
			return ContentsHashCode();
		}

		private T GetFirstNode<T>() where T : XNode
		{
			XNode xNode = content as XNode;
			if (xNode != null)
			{
				do
				{
					xNode = xNode.next;
					if (xNode is T result)
					{
						return result;
					}
				}
				while (xNode != content);
			}
			return null;
		}

		internal static bool IsWhitespace(string s)
		{
			foreach (char c in s)
			{
				if (c != ' ' && c != '\t' && c != '\r' && c != '\n')
				{
					return false;
				}
			}
			return true;
		}

		internal override void ValidateNode(XNode node, XNode previous)
		{
			switch (node.NodeType)
			{
			case XmlNodeType.Text:
				ValidateString(((XText)node).Value);
				break;
			case XmlNodeType.Element:
				ValidateDocument(previous, XmlNodeType.DocumentType, XmlNodeType.None);
				break;
			case XmlNodeType.DocumentType:
				ValidateDocument(previous, XmlNodeType.None, XmlNodeType.Element);
				break;
			case XmlNodeType.CDATA:
				throw new ArgumentException(global::SR.Format("A node of type {0} cannot be added to content.", XmlNodeType.CDATA));
			case XmlNodeType.Document:
				throw new ArgumentException(global::SR.Format("A node of type {0} cannot be added to content.", XmlNodeType.Document));
			}
		}

		private void ValidateDocument(XNode previous, XmlNodeType allowBefore, XmlNodeType allowAfter)
		{
			XNode xNode = content as XNode;
			if (xNode == null)
			{
				return;
			}
			if (previous == null)
			{
				allowBefore = allowAfter;
			}
			do
			{
				xNode = xNode.next;
				XmlNodeType nodeType = xNode.NodeType;
				if (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.DocumentType)
				{
					if (nodeType != allowBefore)
					{
						throw new InvalidOperationException("This operation would create an incorrectly structured document.");
					}
					allowBefore = XmlNodeType.None;
				}
				if (xNode == previous)
				{
					allowBefore = allowAfter;
				}
			}
			while (xNode != content);
		}

		internal override void ValidateString(string s)
		{
			if (!IsWhitespace(s))
			{
				throw new ArgumentException("Non-whitespace characters cannot be added to content.");
			}
		}
	}
	/// <summary>Represents an XML Document Type Definition (DTD).</summary>
	public class XDocumentType : XNode
	{
		private string _name;

		private string _publicId;

		private string _systemId;

		private string _internalSubset;

		/// <summary>Gets or sets the internal subset for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the internal subset for this Document Type Definition (DTD).</returns>
		public string InternalSubset
		{
			get
			{
				return _internalSubset;
			}
			set
			{
				bool num = NotifyChanging(this, XObjectChangeEventArgs.Value);
				_internalSubset = value;
				if (num)
				{
					NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Gets or sets the name for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name for this Document Type Definition (DTD).</returns>
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				value = XmlConvert.VerifyName(value);
				bool num = NotifyChanging(this, XObjectChangeEventArgs.Name);
				_name = value;
				if (num)
				{
					NotifyChanged(this, XObjectChangeEventArgs.Name);
				}
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XDocumentType" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.DocumentType" />.</returns>
		public override XmlNodeType NodeType => XmlNodeType.DocumentType;

		/// <summary>Gets or sets the public identifier for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the public identifier for this Document Type Definition (DTD).</returns>
		public string PublicId
		{
			get
			{
				return _publicId;
			}
			set
			{
				bool num = NotifyChanging(this, XObjectChangeEventArgs.Value);
				_publicId = value;
				if (num)
				{
					NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Gets or sets the system identifier for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the system identifier for this Document Type Definition (DTD).</returns>
		public string SystemId
		{
			get
			{
				return _systemId;
			}
			set
			{
				bool num = NotifyChanging(this, XObjectChangeEventArgs.Value);
				_systemId = value;
				if (num)
				{
					NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Xml.Linq.XDocumentType" /> class.</summary>
		/// <param name="name">A <see cref="T:System.String" /> that contains the qualified name of the DTD, which is the same as the qualified name of the root element of the XML document.</param>
		/// <param name="publicId">A <see cref="T:System.String" /> that contains the public identifier of an external public DTD.</param>
		/// <param name="systemId">A <see cref="T:System.String" /> that contains the system identifier of an external private DTD.</param>
		/// <param name="internalSubset">A <see cref="T:System.String" /> that contains the internal subset for an internal DTD.</param>
		public XDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			_name = XmlConvert.VerifyName(name);
			_publicId = publicId;
			_systemId = systemId;
			_internalSubset = internalSubset;
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Xml.Linq.XDocumentType" /> class from another <see cref="T:System.Xml.Linq.XDocumentType" /> object.</summary>
		/// <param name="other">An <see cref="T:System.Xml.Linq.XDocumentType" /> object to copy from.</param>
		public XDocumentType(XDocumentType other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			_name = other._name;
			_publicId = other._publicId;
			_systemId = other._systemId;
			_internalSubset = other._internalSubset;
		}

		internal XDocumentType(XmlReader r)
		{
			_name = r.Name;
			_publicId = r.GetAttribute("PUBLIC");
			_systemId = r.GetAttribute("SYSTEM");
			_internalSubset = r.Value;
			r.Read();
		}

		/// <summary>Write this <see cref="T:System.Xml.Linq.XDocumentType" /> to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteDocType(_name, _publicId, _systemId, _internalSubset);
		}

		public override Task WriteToAsync(XmlWriter writer, CancellationToken cancellationToken)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			return writer.WriteDocTypeAsync(_name, _publicId, _systemId, _internalSubset);
		}

		internal override XNode CloneNode()
		{
			return new XDocumentType(this);
		}

		internal override bool DeepEquals(XNode node)
		{
			if (node is XDocumentType xDocumentType && _name == xDocumentType._name && _publicId == xDocumentType._publicId && _systemId == xDocumentType.SystemId)
			{
				return _internalSubset == xDocumentType._internalSubset;
			}
			return false;
		}

		internal override int GetDeepHashCode()
		{
			return _name.GetHashCode() ^ ((_publicId != null) ? _publicId.GetHashCode() : 0) ^ ((_systemId != null) ? _systemId.GetHashCode() : 0) ^ ((_internalSubset != null) ? _internalSubset.GetHashCode() : 0);
		}
	}
	/// <summary>Represents an XML element.  See XElement Class Overview and the Remarks section on this page for usage information and examples.</summary>
	[XmlSchemaProvider(null, IsAny = true)]
	public class XElement : XContainer, IXmlSerializable
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		private struct AsyncConstructionSentry
		{
		}

		internal XName name;

		internal XAttribute lastAttr;

		/// <summary>Gets an empty collection of elements.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains an empty collection.</returns>
		public static IEnumerable<XElement> EmptySequence => Array.Empty<XElement>();

		/// <summary>Gets the first attribute of this element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XAttribute" /> that contains the first attribute of this element.</returns>
		public XAttribute FirstAttribute
		{
			get
			{
				if (lastAttr == null)
				{
					return null;
				}
				return lastAttr.next;
			}
		}

		/// <summary>Gets a value indicating whether this element has at least one attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if this element has at least one attribute; otherwise <see langword="false" />.</returns>
		public bool HasAttributes => lastAttr != null;

		/// <summary>Gets a value indicating whether this element has at least one child element.</summary>
		/// <returns>
		///   <see langword="true" /> if this element has at least one child element; otherwise <see langword="false" />.</returns>
		public bool HasElements
		{
			get
			{
				XNode xNode = content as XNode;
				if (xNode != null)
				{
					do
					{
						if (xNode is XElement)
						{
							return true;
						}
						xNode = xNode.next;
					}
					while (xNode != content);
				}
				return false;
			}
		}

		/// <summary>Gets a value indicating whether this element contains no content.</summary>
		/// <returns>
		///   <see langword="true" /> if this element contains no content; otherwise <see langword="false" />.</returns>
		public bool IsEmpty => content == null;

		/// <summary>Gets the last attribute of this element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XAttribute" /> that contains the last attribute of this element.</returns>
		public XAttribute LastAttribute => lastAttr;

		/// <summary>Gets or sets the name of this element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> that contains the name of this element.</returns>
		public XName Name
		{
			get
			{
				return name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				bool num = NotifyChanging(this, XObjectChangeEventArgs.Name);
				name = value;
				if (num)
				{
					NotifyChanged(this, XObjectChangeEventArgs.Name);
				}
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XElement" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Element" />.</returns>
		public override XmlNodeType NodeType => XmlNodeType.Element;

		/// <summary>Gets or sets the concatenated text contents of this element.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains all of the text content of this element. If there are multiple text nodes, they will be concatenated.</returns>
		public string Value
		{
			get
			{
				if (content == null)
				{
					return string.Empty;
				}
				if (content is string result)
				{
					return result;
				}
				StringBuilder sb = System.Text.StringBuilderCache.Acquire();
				AppendText(sb);
				return System.Text.StringBuilderCache.GetStringAndRelease(sb);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				RemoveNodes();
				Add(value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class with the specified name.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the name of the element.</param>
		public XElement(XName name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class with the specified name and content.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the element name.</param>
		/// <param name="content">The contents of the element.</param>
		public XElement(XName name, object content)
			: this(name)
		{
			AddContentSkipNotify(content);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class with the specified name and content.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the element name.</param>
		/// <param name="content">The initial content of the element.</param>
		public XElement(XName name, params object[] content)
			: this(name, (object)content)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class from another <see cref="T:System.Xml.Linq.XElement" /> object.</summary>
		/// <param name="other">An <see cref="T:System.Xml.Linq.XElement" /> object to copy from.</param>
		public XElement(XElement other)
			: base(other)
		{
			name = other.name;
			XAttribute xAttribute = other.lastAttr;
			if (xAttribute != null)
			{
				do
				{
					xAttribute = xAttribute.next;
					AppendAttributeSkipNotify(new XAttribute(xAttribute));
				}
				while (xAttribute != other.lastAttr);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class from an <see cref="T:System.Xml.Linq.XStreamingElement" /> object.</summary>
		/// <param name="other">An <see cref="T:System.Xml.Linq.XStreamingElement" /> that contains unevaluated queries that will be iterated for the contents of this <see cref="T:System.Xml.Linq.XElement" />.</param>
		public XElement(XStreamingElement other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			name = other.name;
			AddContentSkipNotify(other.content);
		}

		internal XElement()
			: this("default")
		{
		}

		internal XElement(XmlReader r)
			: this(r, LoadOptions.None)
		{
		}

		private XElement(AsyncConstructionSentry s)
		{
		}

		internal XElement(XmlReader r, LoadOptions o)
		{
			ReadElementFrom(r, o);
		}

		internal static async Task<XElement> CreateAsync(XmlReader r, CancellationToken cancellationToken)
		{
			XElement xe = new XElement(default(AsyncConstructionSentry));
			await xe.ReadElementFromAsync(r, LoadOptions.None, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return xe;
		}

		/// <summary>Serialize this element to a file.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the name of the file.</param>
		public void Save(string fileName)
		{
			Save(fileName, GetSaveOptionsFromAnnotations());
		}

		/// <summary>Serialize this element to a file, optionally disabling formatting.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the name of the file.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		public void Save(string fileName, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using XmlWriter writer = XmlWriter.Create(fileName, xmlWriterSettings);
			Save(writer);
		}

		/// <summary>Returns a collection of elements that contain this element, and the ancestors of this element.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of elements that contain this element, and the ancestors of this element.</returns>
		public IEnumerable<XElement> AncestorsAndSelf()
		{
			return GetAncestors(null, self: true);
		}

		/// <summary>Returns a filtered collection of elements that contain this element, and the ancestors of this element. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contain this element, and the ancestors of this element. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		public IEnumerable<XElement> AncestorsAndSelf(XName name)
		{
			if (!(name != null))
			{
				return EmptySequence;
			}
			return GetAncestors(name, self: true);
		}

		/// <summary>Returns the <see cref="T:System.Xml.Linq.XAttribute" /> of this <see cref="T:System.Xml.Linq.XElement" /> that has the specified <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> of the <see cref="T:System.Xml.Linq.XAttribute" /> to get.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XAttribute" /> that has the specified <see cref="T:System.Xml.Linq.XName" />; <see langword="null" /> if there is no attribute with the specified name.</returns>
		public XAttribute Attribute(XName name)
		{
			XAttribute xAttribute = lastAttr;
			if (xAttribute != null)
			{
				do
				{
					xAttribute = xAttribute.next;
					if (xAttribute.name == name)
					{
						return xAttribute;
					}
				}
				while (xAttribute != lastAttr);
			}
			return null;
		}

		/// <summary>Returns a collection of attributes of this element.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> of attributes of this element.</returns>
		public IEnumerable<XAttribute> Attributes()
		{
			return GetAttributes(null);
		}

		/// <summary>Returns a filtered collection of attributes of this element. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> that contains the attributes of this element. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		public IEnumerable<XAttribute> Attributes(XName name)
		{
			if (!(name != null))
			{
				return XAttribute.EmptySequence;
			}
			return GetAttributes(name);
		}

		/// <summary>Returns a collection of nodes that contain this element, and all descendant nodes of this element, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contain this element, and all descendant nodes of this element, in document order.</returns>
		public IEnumerable<XNode> DescendantNodesAndSelf()
		{
			return GetDescendantNodes(self: true);
		}

		/// <summary>Returns a collection of elements that contain this element, and all descendant elements of this element, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of elements that contain this element, and all descendant elements of this element, in document order.</returns>
		public IEnumerable<XElement> DescendantsAndSelf()
		{
			return GetDescendants(null, self: true);
		}

		/// <summary>Returns a filtered collection of elements that contain this element, and all descendant elements of this element, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contain this element, and all descendant elements of this element, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		public IEnumerable<XElement> DescendantsAndSelf(XName name)
		{
			if (!(name != null))
			{
				return EmptySequence;
			}
			return GetDescendants(name, self: true);
		}

		/// <summary>Gets the default <see cref="T:System.Xml.Linq.XNamespace" /> of this <see cref="T:System.Xml.Linq.XElement" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNamespace" /> that contains the default namespace of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		public XNamespace GetDefaultNamespace()
		{
			string namespaceOfPrefixInScope = GetNamespaceOfPrefixInScope("xmlns", null);
			if (namespaceOfPrefixInScope == null)
			{
				return XNamespace.None;
			}
			return XNamespace.Get(namespaceOfPrefixInScope);
		}

		/// <summary>Gets the namespace associated with a particular prefix for this <see cref="T:System.Xml.Linq.XElement" />.</summary>
		/// <param name="prefix">A string that contains the namespace prefix to look up.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XNamespace" /> for the namespace associated with the prefix for this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		public XNamespace GetNamespaceOfPrefix(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			if (prefix.Length == 0)
			{
				throw new ArgumentException(global::SR.Format("'{0}' is an invalid prefix.", prefix));
			}
			if (prefix == "xmlns")
			{
				return XNamespace.Xmlns;
			}
			string namespaceOfPrefixInScope = GetNamespaceOfPrefixInScope(prefix, null);
			if (namespaceOfPrefixInScope != null)
			{
				return XNamespace.Get(namespaceOfPrefixInScope);
			}
			if (prefix == "xml")
			{
				return XNamespace.Xml;
			}
			return null;
		}

		/// <summary>Gets the prefix associated with a namespace for this <see cref="T:System.Xml.Linq.XElement" />.</summary>
		/// <param name="ns">An <see cref="T:System.Xml.Linq.XNamespace" /> to look up.</param>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace prefix.</returns>
		public string GetPrefixOfNamespace(XNamespace ns)
		{
			if (ns == null)
			{
				throw new ArgumentNullException("ns");
			}
			string namespaceName = ns.NamespaceName;
			bool flag = false;
			XElement xElement = this;
			do
			{
				XAttribute xAttribute = xElement.lastAttr;
				if (xAttribute != null)
				{
					bool flag2 = false;
					do
					{
						xAttribute = xAttribute.next;
						if (xAttribute.IsNamespaceDeclaration)
						{
							if (xAttribute.Value == namespaceName && xAttribute.Name.NamespaceName.Length != 0 && (!flag || GetNamespaceOfPrefixInScope(xAttribute.Name.LocalName, xElement) == null))
							{
								return xAttribute.Name.LocalName;
							}
							flag2 = true;
						}
					}
					while (xAttribute != xElement.lastAttr);
					flag = flag || flag2;
				}
				xElement = xElement.parent as XElement;
			}
			while (xElement != null);
			if ((object)namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				if (!flag || GetNamespaceOfPrefixInScope("xml", null) == null)
				{
					return "xml";
				}
			}
			else if ((object)namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		/// <summary>Loads an <see cref="T:System.Xml.Linq.XElement" /> from a file.</summary>
		/// <param name="uri">A URI string referencing the file to load into a new <see cref="T:System.Xml.Linq.XElement" />.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> that contains the contents of the specified file.</returns>
		public static XElement Load(string uri)
		{
			return Load(uri, LoadOptions.None);
		}

		/// <summary>Loads an <see cref="T:System.Xml.Linq.XElement" /> from a file, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <param name="uri">A URI string referencing the file to load into an <see cref="T:System.Xml.Linq.XElement" />.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> that contains the contents of the specified file.</returns>
		public static XElement Load(string uri, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			using XmlReader reader = XmlReader.Create(uri, xmlReaderSettings);
			return Load(reader, options);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XElement" /> instance by using the specified stream.</summary>
		/// <param name="stream">The stream that contains the XML data.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> object used to read the data that is contained in the stream.</returns>
		public static XElement Load(Stream stream)
		{
			return Load(stream, LoadOptions.None);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XElement" /> instance by using the specified stream, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <param name="stream">The stream containing the XML data.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> object that specifies whether to load base URI and line information.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> object used to read the data that the stream contains.</returns>
		public static XElement Load(Stream stream, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			using XmlReader reader = XmlReader.Create(stream, xmlReaderSettings);
			return Load(reader, options);
		}

		public static async Task<XElement> LoadAsync(Stream stream, LoadOptions options, CancellationToken cancellationToken)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			xmlReaderSettings.Async = true;
			using XmlReader r = XmlReader.Create(stream, xmlReaderSettings);
			return await LoadAsync(r, options, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		/// <summary>Loads an <see cref="T:System.Xml.Linq.XElement" /> from a <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="textReader">A <see cref="T:System.IO.TextReader" /> that will be read for the <see cref="T:System.Xml.Linq.XElement" /> content.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> that contains the XML that was read from the specified <see cref="T:System.IO.TextReader" />.</returns>
		public static XElement Load(TextReader textReader)
		{
			return Load(textReader, LoadOptions.None);
		}

		/// <summary>Loads an <see cref="T:System.Xml.Linq.XElement" /> from a <see cref="T:System.IO.TextReader" />, optionally preserving white space and retaining line information.</summary>
		/// <param name="textReader">A <see cref="T:System.IO.TextReader" /> that will be read for the <see cref="T:System.Xml.Linq.XElement" /> content.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> that contains the XML that was read from the specified <see cref="T:System.IO.TextReader" />.</returns>
		public static XElement Load(TextReader textReader, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			using XmlReader reader = XmlReader.Create(textReader, xmlReaderSettings);
			return Load(reader, options);
		}

		public static async Task<XElement> LoadAsync(TextReader textReader, LoadOptions options, CancellationToken cancellationToken)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			xmlReaderSettings.Async = true;
			using XmlReader r = XmlReader.Create(textReader, xmlReaderSettings);
			return await LoadAsync(r, options, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		/// <summary>Loads an <see cref="T:System.Xml.Linq.XElement" /> from an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" /> that will be read for the content of the <see cref="T:System.Xml.Linq.XElement" />.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> that contains the XML that was read from the specified <see cref="T:System.Xml.XmlReader" />.</returns>
		public static XElement Load(XmlReader reader)
		{
			return Load(reader, LoadOptions.None);
		}

		/// <summary>Loads an <see cref="T:System.Xml.Linq.XElement" /> from an <see cref="T:System.Xml.XmlReader" />, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" /> that will be read for the content of the <see cref="T:System.Xml.Linq.XElement" />.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> that contains the XML that was read from the specified <see cref="T:System.Xml.XmlReader" />.</returns>
		public static XElement Load(XmlReader reader, LoadOptions options)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.MoveToContent() != XmlNodeType.Element)
			{
				throw new InvalidOperationException(global::SR.Format("The XmlReader must be on a node of type {0} instead of a node of type {1}.", XmlNodeType.Element, reader.NodeType));
			}
			XElement result = new XElement(reader, options);
			reader.MoveToContent();
			if (!reader.EOF)
			{
				throw new InvalidOperationException("The XmlReader state should be EndOfFile after this operation.");
			}
			return result;
		}

		public static Task<XElement> LoadAsync(XmlReader reader, LoadOptions options, CancellationToken cancellationToken)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<XElement>(cancellationToken);
			}
			return LoadAsyncInternal(reader, options, cancellationToken);
		}

		private static async Task<XElement> LoadAsyncInternal(XmlReader reader, LoadOptions options, CancellationToken cancellationToken)
		{
			if (await reader.MoveToContentAsync().ConfigureAwait(continueOnCapturedContext: false) != XmlNodeType.Element)
			{
				throw new InvalidOperationException(global::SR.Format("The XmlReader must be on a node of type {0} instead of a node of type {1}.", XmlNodeType.Element, reader.NodeType));
			}
			XElement e = new XElement(default(AsyncConstructionSentry));
			await e.ReadElementFromAsync(reader, options, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			cancellationToken.ThrowIfCancellationRequested();
			await reader.MoveToContentAsync().ConfigureAwait(continueOnCapturedContext: false);
			if (!reader.EOF)
			{
				throw new InvalidOperationException("The XmlReader state should be EndOfFile after this operation.");
			}
			return e;
		}

		/// <summary>Load an <see cref="T:System.Xml.Linq.XElement" /> from a string that contains XML.</summary>
		/// <param name="text">A <see cref="T:System.String" /> that contains XML.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> populated from the string that contains XML.</returns>
		public static XElement Parse(string text)
		{
			return Parse(text, LoadOptions.None);
		}

		/// <summary>Load an <see cref="T:System.Xml.Linq.XElement" /> from a string that contains XML, optionally preserving white space and retaining line information.</summary>
		/// <param name="text">A <see cref="T:System.String" /> that contains XML.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> populated from the string that contains XML.</returns>
		public static XElement Parse(string text, LoadOptions options)
		{
			using StringReader input = new StringReader(text);
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			using XmlReader reader = XmlReader.Create(input, xmlReaderSettings);
			return Load(reader, options);
		}

		/// <summary>Removes nodes and attributes from this <see cref="T:System.Xml.Linq.XElement" />.</summary>
		public void RemoveAll()
		{
			RemoveAttributes();
			RemoveNodes();
		}

		/// <summary>Removes the attributes of this <see cref="T:System.Xml.Linq.XElement" />.</summary>
		public void RemoveAttributes()
		{
			if (SkipNotify())
			{
				RemoveAttributesSkipNotify();
				return;
			}
			while (lastAttr != null)
			{
				XAttribute xAttribute = lastAttr.next;
				NotifyChanging(xAttribute, XObjectChangeEventArgs.Remove);
				if (lastAttr == null || xAttribute != lastAttr.next)
				{
					throw new InvalidOperationException("This operation was corrupted by external code.");
				}
				if (xAttribute != lastAttr)
				{
					lastAttr.next = xAttribute.next;
				}
				else
				{
					lastAttr = null;
				}
				xAttribute.parent = null;
				xAttribute.next = null;
				NotifyChanged(xAttribute, XObjectChangeEventArgs.Remove);
			}
		}

		/// <summary>Replaces the child nodes and the attributes of this element with the specified content.</summary>
		/// <param name="content">The content that will replace the child nodes and attributes of this element.</param>
		public void ReplaceAll(object content)
		{
			content = XContainer.GetContentSnapshot(content);
			RemoveAll();
			Add(content);
		}

		/// <summary>Replaces the child nodes and the attributes of this element with the specified content.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		public void ReplaceAll(params object[] content)
		{
			ReplaceAll((object)content);
		}

		/// <summary>Replaces the attributes of this element with the specified content.</summary>
		/// <param name="content">The content that will replace the attributes of this element.</param>
		public void ReplaceAttributes(object content)
		{
			content = XContainer.GetContentSnapshot(content);
			RemoveAttributes();
			Add(content);
		}

		/// <summary>Replaces the attributes of this element with the specified content.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		public void ReplaceAttributes(params object[] content)
		{
			ReplaceAttributes((object)content);
		}

		/// <summary>Outputs this <see cref="T:System.Xml.Linq.XElement" /> to the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">The stream to output this <see cref="T:System.Xml.Linq.XElement" /> to.</param>
		public void Save(Stream stream)
		{
			Save(stream, GetSaveOptionsFromAnnotations());
		}

		/// <summary>Outputs this <see cref="T:System.Xml.Linq.XElement" /> to the specified <see cref="T:System.IO.Stream" />, optionally specifying formatting behavior.</summary>
		/// <param name="stream">The stream to output this <see cref="T:System.Xml.Linq.XElement" /> to.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> object that specifies formatting behavior.</param>
		public void Save(Stream stream, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using XmlWriter writer = XmlWriter.Create(stream, xmlWriterSettings);
			Save(writer);
		}

		public async Task SaveAsync(Stream stream, SaveOptions options, CancellationToken cancellationToken)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			xmlWriterSettings.Async = true;
			using XmlWriter w = XmlWriter.Create(stream, xmlWriterSettings);
			await SaveAsync(w, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		/// <summary>Serialize this element to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="textWriter">A <see cref="T:System.IO.TextWriter" /> that the <see cref="T:System.Xml.Linq.XElement" /> will be written to.</param>
		public void Save(TextWriter textWriter)
		{
			Save(textWriter, GetSaveOptionsFromAnnotations());
		}

		/// <summary>Serialize this element to a <see cref="T:System.IO.TextWriter" />, optionally disabling formatting.</summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> to output the XML to.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		public void Save(TextWriter textWriter, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using XmlWriter writer = XmlWriter.Create(textWriter, xmlWriterSettings);
			Save(writer);
		}

		public async Task SaveAsync(TextWriter textWriter, SaveOptions options, CancellationToken cancellationToken)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			xmlWriterSettings.Async = true;
			using XmlWriter w = XmlWriter.Create(textWriter, xmlWriterSettings);
			await SaveAsync(w, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		/// <summary>Serialize this element to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" /> that the <see cref="T:System.Xml.Linq.XElement" /> will be written to.</param>
		public void Save(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteStartDocument();
			WriteTo(writer);
			writer.WriteEndDocument();
		}

		public Task SaveAsync(XmlWriter writer, CancellationToken cancellationToken)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			return SaveAsyncInternal(writer, cancellationToken);
		}

		private async Task SaveAsyncInternal(XmlWriter writer, CancellationToken cancellationToken)
		{
			await writer.WriteStartDocumentAsync().ConfigureAwait(continueOnCapturedContext: false);
			await WriteToAsync(writer, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			cancellationToken.ThrowIfCancellationRequested();
			await writer.WriteEndDocumentAsync().ConfigureAwait(continueOnCapturedContext: false);
		}

		/// <summary>Sets the value of an attribute, adds an attribute, or removes an attribute.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the name of the attribute to change.</param>
		/// <param name="value">The value to assign to the attribute. The attribute is removed if the value is <see langword="null" />. Otherwise, the value is converted to its string representation and assigned to the <see cref="P:System.Xml.Linq.XAttribute.Value" /> property of the attribute.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> is an instance of <see cref="T:System.Xml.Linq.XObject" />.</exception>
		public void SetAttributeValue(XName name, object value)
		{
			XAttribute xAttribute = Attribute(name);
			if (value == null)
			{
				if (xAttribute != null)
				{
					RemoveAttribute(xAttribute);
				}
			}
			else if (xAttribute != null)
			{
				xAttribute.Value = XContainer.GetStringValue(value);
			}
			else
			{
				AppendAttribute(new XAttribute(name, value));
			}
		}

		/// <summary>Sets the value of a child element, adds a child element, or removes a child element.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the name of the child element to change.</param>
		/// <param name="value">The value to assign to the child element. The child element is removed if the value is <see langword="null" />. Otherwise, the value is converted to its string representation and assigned to the <see cref="P:System.Xml.Linq.XElement.Value" /> property of the child element.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> is an instance of <see cref="T:System.Xml.Linq.XObject" />.</exception>
		public void SetElementValue(XName name, object value)
		{
			XElement xElement = Element(name);
			if (value == null)
			{
				if (xElement != null)
				{
					RemoveNode(xElement);
				}
			}
			else if (xElement != null)
			{
				xElement.Value = XContainer.GetStringValue(value);
			}
			else
			{
				AddNode(new XElement(name, XContainer.GetStringValue(value)));
			}
		}

		/// <summary>Sets the value of this element.</summary>
		/// <param name="value">The value to assign to this element. The value is converted to its string representation and assigned to the <see cref="P:System.Xml.Linq.XElement.Value" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> is an <see cref="T:System.Xml.Linq.XObject" />.</exception>
		public void SetValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Value = XContainer.GetStringValue(value);
		}

		/// <summary>Write this element to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			new ElementWriter(writer).WriteElement(this);
		}

		public override Task WriteToAsync(XmlWriter writer, CancellationToken cancellationToken)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			return new ElementWriter(writer).WriteElementAsync(this, cancellationToken);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.String" />.</param>
		/// <returns>A <see cref="T:System.String" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		[CLSCompliant(false)]
		public static explicit operator string(XElement element)
		{
			return element?.Value;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Boolean" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Boolean" />.</param>
		/// <returns>A <see cref="T:System.Boolean" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Boolean" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator bool(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToBoolean(element.Value.ToLowerInvariant());
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Boolean" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Boolean" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Boolean" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element is not <see langword="null" /> and does not contain a valid <see cref="T:System.Boolean" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator bool?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return XmlConvert.ToBoolean(element.Value.ToLowerInvariant());
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to an <see cref="T:System.Int32" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Int32" />.</param>
		/// <returns>A <see cref="T:System.Int32" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Int32" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator int(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToInt32(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int32" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int32" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int32" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element is not <see langword="null" /> and does not contain a valid <see cref="T:System.Int32" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator int?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return XmlConvert.ToInt32(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.UInt32" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.UInt32" />.</param>
		/// <returns>A <see cref="T:System.UInt32" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.UInt32" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator uint(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToUInt32(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt32" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt32" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt32" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element is not <see langword="null" /> and does not contain a valid <see cref="T:System.UInt32" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator uint?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return XmlConvert.ToUInt32(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to an <see cref="T:System.Int64" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Int64" />.</param>
		/// <returns>A <see cref="T:System.Int64" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Int64" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator long(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToInt64(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int64" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int64" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int64" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element is not <see langword="null" /> and does not contain a valid <see cref="T:System.Int64" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator long?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return XmlConvert.ToInt64(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.UInt64" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.UInt64" />.</param>
		/// <returns>A <see cref="T:System.UInt64" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.UInt64" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator ulong(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToUInt64(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt64" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt64" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt64" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element is not <see langword="null" /> and does not contain a valid <see cref="T:System.UInt64" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator ulong?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return XmlConvert.ToUInt64(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Single" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Single" />.</param>
		/// <returns>A <see cref="T:System.Single" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Single" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator float(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToSingle(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Single" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Single" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Single" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element is not <see langword="null" /> and does not contain a valid <see cref="T:System.Single" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator float?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return XmlConvert.ToSingle(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Double" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Double" />.</param>
		/// <returns>A <see cref="T:System.Double" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Double" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator double(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToDouble(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Double" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Double" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Double" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element is not <see langword="null" /> and does not contain a valid <see cref="T:System.Double" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator double?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return XmlConvert.ToDouble(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Decimal" />.</param>
		/// <returns>A <see cref="T:System.Decimal" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Decimal" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator decimal(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToDecimal(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Decimal" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Decimal" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Decimal" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element is not <see langword="null" /> and does not contain a valid <see cref="T:System.Decimal" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator decimal?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return XmlConvert.ToDecimal(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.DateTime" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.DateTime" />.</param>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.DateTime" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator DateTime(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return DateTime.Parse(element.Value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTime" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTime" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTime" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element is not <see langword="null" /> and does not contain a valid <see cref="T:System.DateTime" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator DateTime?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return DateTime.Parse(element.Value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.DateTimeOffset" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.DateTimeOffset" />.</param>
		/// <returns>A <see cref="T:System.DateTimeOffset" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.DateTimeOffset" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator DateTimeOffset(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToDateTimeOffset(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTimeOffset" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to an <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTimeOffset" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTimeOffset" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element is not <see langword="null" /> and does not contain a valid <see cref="T:System.DateTimeOffset" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator DateTimeOffset?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return XmlConvert.ToDateTimeOffset(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.TimeSpan" />.</param>
		/// <returns>A <see cref="T:System.TimeSpan" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.TimeSpan" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator TimeSpan(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToTimeSpan(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.TimeSpan" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.TimeSpan" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element is not <see langword="null" /> and does not contain a valid <see cref="T:System.TimeSpan" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator TimeSpan?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return XmlConvert.ToTimeSpan(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Guid" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Guid" />.</param>
		/// <returns>A <see cref="T:System.Guid" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Guid" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator Guid(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToGuid(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Guid" />.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Guid" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Guid" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <exception cref="T:System.FormatException">The element is not <see langword="null" /> and does not contain a valid <see cref="T:System.Guid" /> value.</exception>
		[CLSCompliant(false)]
		public static explicit operator Guid?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return XmlConvert.ToGuid(element.Value);
		}

		/// <summary>Gets an XML schema definition that describes the XML representation of this object.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.</returns>
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>Generates an object from its XML representation.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which the object is deserialized.</param>
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (parent != null || annotations != null || content != null || lastAttr != null)
			{
				throw new InvalidOperationException("This instance cannot be deserialized.");
			}
			if (reader.MoveToContent() != XmlNodeType.Element)
			{
				throw new InvalidOperationException(global::SR.Format("The XmlReader must be on a node of type {0} instead of a node of type {1}.", XmlNodeType.Element, reader.NodeType));
			}
			ReadElementFrom(reader, LoadOptions.None);
		}

		/// <summary>Converts an object into its XML representation.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to which this object is serialized.</param>
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			WriteTo(writer);
		}

		internal override void AddAttribute(XAttribute a)
		{
			if (Attribute(a.Name) != null)
			{
				throw new InvalidOperationException("Duplicate attribute.");
			}
			if (a.parent != null)
			{
				a = new XAttribute(a);
			}
			AppendAttribute(a);
		}

		internal override void AddAttributeSkipNotify(XAttribute a)
		{
			if (Attribute(a.Name) != null)
			{
				throw new InvalidOperationException("Duplicate attribute.");
			}
			if (a.parent != null)
			{
				a = new XAttribute(a);
			}
			AppendAttributeSkipNotify(a);
		}

		internal void AppendAttribute(XAttribute a)
		{
			bool num = NotifyChanging(a, XObjectChangeEventArgs.Add);
			if (a.parent != null)
			{
				throw new InvalidOperationException("This operation was corrupted by external code.");
			}
			AppendAttributeSkipNotify(a);
			if (num)
			{
				NotifyChanged(a, XObjectChangeEventArgs.Add);
			}
		}

		internal void AppendAttributeSkipNotify(XAttribute a)
		{
			a.parent = this;
			if (lastAttr == null)
			{
				a.next = a;
			}
			else
			{
				a.next = lastAttr.next;
				lastAttr.next = a;
			}
			lastAttr = a;
		}

		private bool AttributesEqual(XElement e)
		{
			XAttribute xAttribute = lastAttr;
			XAttribute xAttribute2 = e.lastAttr;
			if (xAttribute != null && xAttribute2 != null)
			{
				do
				{
					xAttribute = xAttribute.next;
					xAttribute2 = xAttribute2.next;
					if (xAttribute.name != xAttribute2.name || xAttribute.value != xAttribute2.value)
					{
						return false;
					}
				}
				while (xAttribute != lastAttr);
				return xAttribute2 == e.lastAttr;
			}
			if (xAttribute == null)
			{
				return xAttribute2 == null;
			}
			return false;
		}

		internal override XNode CloneNode()
		{
			return new XElement(this);
		}

		internal override bool DeepEquals(XNode node)
		{
			if (node is XElement xElement && name == xElement.name && ContentsEqual(xElement))
			{
				return AttributesEqual(xElement);
			}
			return false;
		}

		private IEnumerable<XAttribute> GetAttributes(XName name)
		{
			XAttribute a = lastAttr;
			if (a == null)
			{
				yield break;
			}
			do
			{
				a = a.next;
				if (name == null || a.name == name)
				{
					yield return a;
				}
			}
			while (a.parent == this && a != lastAttr);
		}

		private string GetNamespaceOfPrefixInScope(string prefix, XElement outOfScope)
		{
			for (XElement xElement = this; xElement != outOfScope; xElement = xElement.parent as XElement)
			{
				XAttribute xAttribute = xElement.lastAttr;
				if (xAttribute != null)
				{
					do
					{
						xAttribute = xAttribute.next;
						if (xAttribute.IsNamespaceDeclaration && xAttribute.Name.LocalName == prefix)
						{
							return xAttribute.Value;
						}
					}
					while (xAttribute != xElement.lastAttr);
				}
			}
			return null;
		}

		internal override int GetDeepHashCode()
		{
			int hashCode = name.GetHashCode();
			hashCode ^= ContentsHashCode();
			XAttribute xAttribute = lastAttr;
			if (xAttribute != null)
			{
				do
				{
					xAttribute = xAttribute.next;
					hashCode ^= xAttribute.GetDeepHashCode();
				}
				while (xAttribute != lastAttr);
			}
			return hashCode;
		}

		private void ReadElementFrom(XmlReader r, LoadOptions o)
		{
			ReadElementFromImpl(r, o);
			if (!r.IsEmptyElement)
			{
				r.Read();
				ReadContentFrom(r, o);
			}
			r.Read();
		}

		private async Task ReadElementFromAsync(XmlReader r, LoadOptions o, CancellationToken cancellationTokentoken)
		{
			ReadElementFromImpl(r, o);
			if (!r.IsEmptyElement)
			{
				cancellationTokentoken.ThrowIfCancellationRequested();
				await r.ReadAsync().ConfigureAwait(continueOnCapturedContext: false);
				await ReadContentFromAsync(r, o, cancellationTokentoken).ConfigureAwait(continueOnCapturedContext: false);
			}
			cancellationTokentoken.ThrowIfCancellationRequested();
			await r.ReadAsync().ConfigureAwait(continueOnCapturedContext: false);
		}

		private void ReadElementFromImpl(XmlReader r, LoadOptions o)
		{
			if (r.ReadState != ReadState.Interactive)
			{
				throw new InvalidOperationException("The XmlReader state should be Interactive.");
			}
			name = XNamespace.Get(r.NamespaceURI).GetName(r.LocalName);
			if ((o & LoadOptions.SetBaseUri) != LoadOptions.None)
			{
				string baseURI = r.BaseURI;
				if (!string.IsNullOrEmpty(baseURI))
				{
					SetBaseUri(baseURI);
				}
			}
			IXmlLineInfo xmlLineInfo = null;
			if ((o & LoadOptions.SetLineInfo) != LoadOptions.None)
			{
				xmlLineInfo = r as IXmlLineInfo;
				if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
				{
					SetLineInfo(xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
				}
			}
			if (!r.MoveToFirstAttribute())
			{
				return;
			}
			do
			{
				XAttribute xAttribute = new XAttribute(XNamespace.Get((r.Prefix.Length == 0) ? string.Empty : r.NamespaceURI).GetName(r.LocalName), r.Value);
				if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
				{
					xAttribute.SetLineInfo(xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
				}
				AppendAttributeSkipNotify(xAttribute);
			}
			while (r.MoveToNextAttribute());
			r.MoveToElement();
		}

		internal void RemoveAttribute(XAttribute a)
		{
			bool flag = NotifyChanging(a, XObjectChangeEventArgs.Remove);
			if (a.parent != this)
			{
				throw new InvalidOperationException("This operation was corrupted by external code.");
			}
			XAttribute xAttribute = lastAttr;
			XAttribute xAttribute2;
			while ((xAttribute2 = xAttribute.next) != a)
			{
				xAttribute = xAttribute2;
			}
			if (xAttribute == a)
			{
				lastAttr = null;
			}
			else
			{
				if (lastAttr == a)
				{
					lastAttr = xAttribute;
				}
				xAttribute.next = a.next;
			}
			a.parent = null;
			a.next = null;
			if (flag)
			{
				NotifyChanged(a, XObjectChangeEventArgs.Remove);
			}
		}

		private void RemoveAttributesSkipNotify()
		{
			if (lastAttr != null)
			{
				XAttribute xAttribute = lastAttr;
				do
				{
					XAttribute xAttribute2 = xAttribute.next;
					xAttribute.parent = null;
					xAttribute.next = null;
					xAttribute = xAttribute2;
				}
				while (xAttribute != lastAttr);
				lastAttr = null;
			}
		}

		internal void SetEndElementLineInfo(int lineNumber, int linePosition)
		{
			AddAnnotation(new LineInfoEndElementAnnotation(lineNumber, linePosition));
		}

		internal override void ValidateNode(XNode node, XNode previous)
		{
			if (node is XDocument)
			{
				throw new ArgumentException(global::SR.Format("A node of type {0} cannot be added to content.", XmlNodeType.Document));
			}
			if (node is XDocumentType)
			{
				throw new ArgumentException(global::SR.Format("A node of type {0} cannot be added to content.", XmlNodeType.DocumentType));
			}
		}
	}
	internal sealed class XHashtable<TValue>
	{
		public delegate string ExtractKeyDelegate(TValue value);

		private sealed class XHashtableState
		{
			private struct Entry
			{
				public TValue Value;

				public int HashCode;

				public int Next;
			}

			private int[] _buckets;

			private Entry[] _entries;

			private int _numEntries;

			private ExtractKeyDelegate _extractKey;

			private const int EndOfList = 0;

			private const int FullList = -1;

			public XHashtableState(ExtractKeyDelegate extractKey, int capacity)
			{
				_buckets = new int[capacity];
				_entries = new Entry[capacity];
				_extractKey = extractKey;
			}

			public XHashtableState Resize()
			{
				if (_numEntries < _buckets.Length)
				{
					return this;
				}
				int num = 0;
				for (int i = 0; i < _buckets.Length; i++)
				{
					int num2 = _buckets[i];
					if (num2 == 0)
					{
						num2 = Interlocked.CompareExchange(ref _buckets[i], -1, 0);
					}
					while (num2 > 0)
					{
						if (_extractKey(_entries[num2].Value) != null)
						{
							num++;
						}
						num2 = ((_entries[num2].Next != 0) ? _entries[num2].Next : Interlocked.CompareExchange(ref _entries[num2].Next, -1, 0));
					}
				}
				if (num < _buckets.Length / 2)
				{
					num = _buckets.Length;
				}
				else
				{
					num = _buckets.Length * 2;
					if (num < 0)
					{
						throw new OverflowException();
					}
				}
				XHashtableState xHashtableState = new XHashtableState(_extractKey, num);
				for (int j = 0; j < _buckets.Length; j++)
				{
					for (int num3 = _buckets[j]; num3 > 0; num3 = _entries[num3].Next)
					{
						xHashtableState.TryAdd(_entries[num3].Value, out var _);
					}
				}
				return xHashtableState;
			}

			public bool TryGetValue(string key, int index, int count, out TValue value)
			{
				int hashCode = ComputeHashCode(key, index, count);
				int entryIndex = 0;
				if (FindEntry(hashCode, key, index, count, ref entryIndex))
				{
					value = _entries[entryIndex].Value;
					return true;
				}
				value = default(TValue);
				return false;
			}

			public bool TryAdd(TValue value, out TValue newValue)
			{
				newValue = value;
				string text = _extractKey(value);
				if (text == null)
				{
					return true;
				}
				int num = ComputeHashCode(text, 0, text.Length);
				int num2 = Interlocked.Increment(ref _numEntries);
				if (num2 < 0 || num2 >= _buckets.Length)
				{
					return false;
				}
				_entries[num2].Value = value;
				_entries[num2].HashCode = num;
				Thread.MemoryBarrier();
				int entryIndex = 0;
				while (!FindEntry(num, text, 0, text.Length, ref entryIndex))
				{
					entryIndex = ((entryIndex != 0) ? Interlocked.CompareExchange(ref _entries[entryIndex].Next, num2, 0) : Interlocked.CompareExchange(ref _buckets[num & (_buckets.Length - 1)], num2, 0));
					if (entryIndex <= 0)
					{
						return entryIndex == 0;
					}
				}
				newValue = _entries[entryIndex].Value;
				return true;
			}

			private bool FindEntry(int hashCode, string key, int index, int count, ref int entryIndex)
			{
				int num = entryIndex;
				int num2 = ((num != 0) ? num : _buckets[hashCode & (_buckets.Length - 1)]);
				while (num2 > 0)
				{
					if (_entries[num2].HashCode == hashCode)
					{
						string text = _extractKey(_entries[num2].Value);
						if (text == null)
						{
							if (_entries[num2].Next > 0)
							{
								_entries[num2].Value = default(TValue);
								num2 = _entries[num2].Next;
								if (num == 0)
								{
									_buckets[hashCode & (_buckets.Length - 1)] = num2;
								}
								else
								{
									_entries[num].Next = num2;
								}
								continue;
							}
						}
						else if (count == text.Length && string.CompareOrdinal(key, index, text, 0, count) == 0)
						{
							entryIndex = num2;
							return true;
						}
					}
					num = num2;
					num2 = _entries[num2].Next;
				}
				entryIndex = num;
				return false;
			}

			private static int ComputeHashCode(string key, int index, int count)
			{
				int num = 352654597;
				int num2 = index + count;
				for (int i = index; i < num2; i++)
				{
					num += (num << 7) ^ key[i];
				}
				num -= num >> 17;
				num -= num >> 11;
				num -= num >> 5;
				return num & 0x7FFFFFFF;
			}
		}

		private XHashtableState _state;

		private const int StartingHash = 352654597;

		public XHashtable(ExtractKeyDelegate extractKey, int capacity)
		{
			_state = new XHashtableState(extractKey, capacity);
		}

		public bool TryGetValue(string key, int index, int count, out TValue value)
		{
			return _state.TryGetValue(key, index, count, out value);
		}

		public TValue Add(TValue value)
		{
			TValue newValue;
			while (!_state.TryAdd(value, out newValue))
			{
				lock (this)
				{
					XHashtableState state = _state.Resize();
					Thread.MemoryBarrier();
					_state = state;
				}
			}
			return newValue;
		}
	}
	internal static class XHelper
	{
		internal static bool IsInstanceOfType(object o, Type type)
		{
			if (o == null)
			{
				return false;
			}
			return type.GetTypeInfo().IsAssignableFrom(o.GetType().GetTypeInfo());
		}
	}
	internal struct Inserter
	{
		private XContainer _parent;

		private XNode _previous;

		private string _text;

		public Inserter(XContainer parent, XNode anchor)
		{
			_parent = parent;
			_previous = anchor;
			_text = null;
		}

		public void Add(object content)
		{
			AddContent(content);
			if (_text == null)
			{
				return;
			}
			if (_parent.content == null)
			{
				if (_parent.SkipNotify())
				{
					_parent.content = _text;
				}
				else if (_text.Length > 0)
				{
					InsertNode(new XText(_text));
				}
				else if (_parent is XElement)
				{
					_parent.NotifyChanging(_parent, XObjectChangeEventArgs.Value);
					if (_parent.content != null)
					{
						throw new InvalidOperationException("This operation was corrupted by external code.");
					}
					_parent.content = _text;
					_parent.NotifyChanged(_parent, XObjectChangeEventArgs.Value);
				}
				else
				{
					_parent.content = _text;
				}
			}
			else if (_text.Length > 0)
			{
				if (_previous is XText xText && !(_previous is XCData))
				{
					xText.Value += _text;
					return;
				}
				_parent.ConvertTextToNode();
				InsertNode(new XText(_text));
			}
		}

		private void AddContent(object content)
		{
			if (content == null)
			{
				return;
			}
			if (content is XNode n)
			{
				AddNode(n);
				return;
			}
			if (content is string s)
			{
				AddString(s);
				return;
			}
			if (content is XStreamingElement other)
			{
				AddNode(new XElement(other));
				return;
			}
			if (content is object[] array)
			{
				object[] array2 = array;
				foreach (object content2 in array2)
				{
					AddContent(content2);
				}
				return;
			}
			if (content is IEnumerable enumerable)
			{
				{
					foreach (object item in enumerable)
					{
						AddContent(item);
					}
					return;
				}
			}
			if (content is XAttribute)
			{
				throw new ArgumentException("An attribute cannot be added to content.");
			}
			AddString(XContainer.GetStringValue(content));
		}

		private void AddNode(XNode n)
		{
			_parent.ValidateNode(n, _previous);
			if (n.parent != null)
			{
				n = n.CloneNode();
			}
			else
			{
				XNode parent = _parent;
				while (parent.parent != null)
				{
					parent = parent.parent;
				}
				if (n == parent)
				{
					n = n.CloneNode();
				}
			}
			_parent.ConvertTextToNode();
			if (_text != null)
			{
				if (_text.Length > 0)
				{
					if (_previous is XText xText && !(_previous is XCData))
					{
						xText.Value += _text;
					}
					else
					{
						InsertNode(new XText(_text));
					}
				}
				_text = null;
			}
			InsertNode(n);
		}

		private void AddString(string s)
		{
			_parent.ValidateString(s);
			_text += s;
		}

		private void InsertNode(XNode n)
		{
			bool num = _parent.NotifyChanging(n, XObjectChangeEventArgs.Add);
			if (n.parent != null)
			{
				throw new InvalidOperationException("This operation was corrupted by external code.");
			}
			n.parent = _parent;
			if (_parent.content == null || _parent.content is string)
			{
				n.next = n;
				_parent.content = n;
			}
			else if (_previous == null)
			{
				XNode xNode = (XNode)_parent.content;
				n.next = xNode.next;
				xNode.next = n;
			}
			else
			{
				n.next = _previous.next;
				_previous.next = n;
				if (_parent.content == _previous)
				{
					_parent.content = n;
				}
			}
			_previous = n;
			if (num)
			{
				_parent.NotifyChanged(n, XObjectChangeEventArgs.Add);
			}
		}
	}
	internal struct NamespaceCache
	{
		private XNamespace _ns;

		private string _namespaceName;

		public XNamespace Get(string namespaceName)
		{
			if ((object)namespaceName == _namespaceName)
			{
				return _ns;
			}
			_namespaceName = namespaceName;
			_ns = XNamespace.Get(namespaceName);
			return _ns;
		}
	}
	internal struct ElementWriter
	{
		private XmlWriter _writer;

		private NamespaceResolver _resolver;

		public ElementWriter(XmlWriter writer)
		{
			_writer = writer;
			_resolver = default(NamespaceResolver);
		}

		public void WriteElement(XElement e)
		{
			PushAncestors(e);
			XElement xElement = e;
			XNode xNode = e;
			while (true)
			{
				e = xNode as XElement;
				if (e != null)
				{
					WriteStartElement(e);
					if (e.content == null)
					{
						WriteEndElement();
					}
					else
					{
						if (!(e.content is string text))
						{
							xNode = ((XNode)e.content).next;
							continue;
						}
						_writer.WriteString(text);
						WriteFullEndElement();
					}
				}
				else
				{
					xNode.WriteTo(_writer);
				}
				while (xNode != xElement && xNode == xNode.parent.content)
				{
					xNode = xNode.parent;
					WriteFullEndElement();
				}
				if (xNode != xElement)
				{
					xNode = xNode.next;
					continue;
				}
				break;
			}
		}

		public async Task WriteElementAsync(XElement e, CancellationToken cancellationToken)
		{
			PushAncestors(e);
			XElement root = e;
			XNode n = e;
			while (true)
			{
				e = n as XElement;
				if (e == null)
				{
					await n.WriteToAsync(_writer, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
				else
				{
					await WriteStartElementAsync(e, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					if (e.content == null)
					{
						await WriteEndElementAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					}
					else
					{
						if (!(e.content is string text))
						{
							n = ((XNode)e.content).next;
							continue;
						}
						cancellationToken.ThrowIfCancellationRequested();
						await _writer.WriteStringAsync(text).ConfigureAwait(continueOnCapturedContext: false);
						await WriteFullEndElementAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					}
				}
				while (n != root && n == n.parent.content)
				{
					n = n.parent;
					await WriteFullEndElementAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
				if (n != root)
				{
					n = n.next;
					continue;
				}
				break;
			}
		}

		private string GetPrefixOfNamespace(XNamespace ns, bool allowDefaultNamespace)
		{
			string namespaceName = ns.NamespaceName;
			if (namespaceName.Length == 0)
			{
				return string.Empty;
			}
			string prefixOfNamespace = _resolver.GetPrefixOfNamespace(ns, allowDefaultNamespace);
			if (prefixOfNamespace != null)
			{
				return prefixOfNamespace;
			}
			if ((object)namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			if ((object)namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		private void PushAncestors(XElement e)
		{
			while (true)
			{
				e = e.parent as XElement;
				if (e == null)
				{
					break;
				}
				XAttribute xAttribute = e.lastAttr;
				if (xAttribute == null)
				{
					continue;
				}
				do
				{
					xAttribute = xAttribute.next;
					if (xAttribute.IsNamespaceDeclaration)
					{
						_resolver.AddFirst((xAttribute.Name.NamespaceName.Length == 0) ? string.Empty : xAttribute.Name.LocalName, XNamespace.Get(xAttribute.Value));
					}
				}
				while (xAttribute != e.lastAttr);
			}
		}

		private void PushElement(XElement e)
		{
			_resolver.PushScope();
			XAttribute xAttribute = e.lastAttr;
			if (xAttribute == null)
			{
				return;
			}
			do
			{
				xAttribute = xAttribute.next;
				if (xAttribute.IsNamespaceDeclaration)
				{
					_resolver.Add((xAttribute.Name.NamespaceName.Length == 0) ? string.Empty : xAttribute.Name.LocalName, XNamespace.Get(xAttribute.Value));
				}
			}
			while (xAttribute != e.lastAttr);
		}

		private void WriteEndElement()
		{
			_writer.WriteEndElement();
			_resolver.PopScope();
		}

		private async Task WriteEndElementAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			await _writer.WriteEndElementAsync().ConfigureAwait(continueOnCapturedContext: false);
			_resolver.PopScope();
		}

		private void WriteFullEndElement()
		{
			_writer.WriteFullEndElement();
			_resolver.PopScope();
		}

		private async Task WriteFullEndElementAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			await _writer.WriteFullEndElementAsync().ConfigureAwait(continueOnCapturedContext: false);
			_resolver.PopScope();
		}

		private void WriteStartElement(XElement e)
		{
			PushElement(e);
			XNamespace xNamespace = e.Name.Namespace;
			_writer.WriteStartElement(GetPrefixOfNamespace(xNamespace, allowDefaultNamespace: true), e.Name.LocalName, xNamespace.NamespaceName);
			XAttribute xAttribute = e.lastAttr;
			if (xAttribute != null)
			{
				do
				{
					xAttribute = xAttribute.next;
					xNamespace = xAttribute.Name.Namespace;
					string localName = xAttribute.Name.LocalName;
					string namespaceName = xNamespace.NamespaceName;
					_writer.WriteAttributeString(GetPrefixOfNamespace(xNamespace, allowDefaultNamespace: false), localName, (namespaceName.Length == 0 && localName == "xmlns") ? "http://www.w3.org/2000/xmlns/" : namespaceName, xAttribute.Value);
				}
				while (xAttribute != e.lastAttr);
			}
		}

		private async Task WriteStartElementAsync(XElement e, CancellationToken cancellationToken)
		{
			PushElement(e);
			XNamespace xNamespace = e.Name.Namespace;
			await _writer.WriteStartElementAsync(GetPrefixOfNamespace(xNamespace, allowDefaultNamespace: true), e.Name.LocalName, xNamespace.NamespaceName).ConfigureAwait(continueOnCapturedContext: false);
			XAttribute a = e.lastAttr;
			if (a != null)
			{
				do
				{
					a = a.next;
					xNamespace = a.Name.Namespace;
					string localName = a.Name.LocalName;
					string namespaceName = xNamespace.NamespaceName;
					await _writer.WriteAttributeStringAsync(GetPrefixOfNamespace(xNamespace, allowDefaultNamespace: false), localName, (namespaceName.Length == 0 && localName == "xmlns") ? "http://www.w3.org/2000/xmlns/" : namespaceName, a.Value).ConfigureAwait(continueOnCapturedContext: false);
				}
				while (a != e.lastAttr);
			}
		}
	}
	internal struct NamespaceResolver
	{
		private class NamespaceDeclaration
		{
			public string prefix;

			public XNamespace ns;

			public int scope;

			public NamespaceDeclaration prev;
		}

		private int _scope;

		private NamespaceDeclaration _declaration;

		private NamespaceDeclaration _rover;

		public void PushScope()
		{
			_scope++;
		}

		public void PopScope()
		{
			NamespaceDeclaration namespaceDeclaration = _declaration;
			if (namespaceDeclaration != null)
			{
				do
				{
					namespaceDeclaration = namespaceDeclaration.prev;
					if (namespaceDeclaration.scope != _scope)
					{
						break;
					}
					if (namespaceDeclaration == _declaration)
					{
						_declaration = null;
					}
					else
					{
						_declaration.prev = namespaceDeclaration.prev;
					}
					_rover = null;
				}
				while (namespaceDeclaration != _declaration && _declaration != null);
			}
			_scope--;
		}

		public void Add(string prefix, XNamespace ns)
		{
			NamespaceDeclaration namespaceDeclaration = new NamespaceDeclaration();
			namespaceDeclaration.prefix = prefix;
			namespaceDeclaration.ns = ns;
			namespaceDeclaration.scope = _scope;
			if (_declaration == null)
			{
				_declaration = namespaceDeclaration;
			}
			else
			{
				namespaceDeclaration.prev = _declaration.prev;
			}
			_declaration.prev = namespaceDeclaration;
			_rover = null;
		}

		public void AddFirst(string prefix, XNamespace ns)
		{
			NamespaceDeclaration namespaceDeclaration = new NamespaceDeclaration();
			namespaceDeclaration.prefix = prefix;
			namespaceDeclaration.ns = ns;
			namespaceDeclaration.scope = _scope;
			if (_declaration == null)
			{
				namespaceDeclaration.prev = namespaceDeclaration;
			}
			else
			{
				namespaceDeclaration.prev = _declaration.prev;
				_declaration.prev = namespaceDeclaration;
			}
			_declaration = namespaceDeclaration;
			_rover = null;
		}

		public string GetPrefixOfNamespace(XNamespace ns, bool allowDefaultNamespace)
		{
			if (_rover != null && _rover.ns == ns && (allowDefaultNamespace || _rover.prefix.Length > 0))
			{
				return _rover.prefix;
			}
			NamespaceDeclaration namespaceDeclaration = _declaration;
			if (namespaceDeclaration != null)
			{
				do
				{
					namespaceDeclaration = namespaceDeclaration.prev;
					if (!(namespaceDeclaration.ns == ns))
					{
						continue;
					}
					NamespaceDeclaration prev = _declaration.prev;
					while (prev != namespaceDeclaration && prev.prefix != namespaceDeclaration.prefix)
					{
						prev = prev.prev;
					}
					if (prev == namespaceDeclaration)
					{
						if (allowDefaultNamespace)
						{
							_rover = namespaceDeclaration;
							return namespaceDeclaration.prefix;
						}
						if (namespaceDeclaration.prefix.Length > 0)
						{
							return namespaceDeclaration.prefix;
						}
					}
				}
				while (namespaceDeclaration != _declaration);
			}
			return null;
		}
	}
	internal struct StreamingElementWriter
	{
		private XmlWriter _writer;

		private XStreamingElement _element;

		private List<XAttribute> _attributes;

		private NamespaceResolver _resolver;

		public StreamingElementWriter(XmlWriter w)
		{
			_writer = w;
			_element = null;
			_attributes = new List<XAttribute>();
			_resolver = default(NamespaceResolver);
		}

		private void FlushElement()
		{
			if (_element == null)
			{
				return;
			}
			PushElement();
			XNamespace xNamespace = _element.Name.Namespace;
			_writer.WriteStartElement(GetPrefixOfNamespace(xNamespace, allowDefaultNamespace: true), _element.Name.LocalName, xNamespace.NamespaceName);
			foreach (XAttribute attribute in _attributes)
			{
				xNamespace = attribute.Name.Namespace;
				string localName = attribute.Name.LocalName;
				string namespaceName = xNamespace.NamespaceName;
				_writer.WriteAttributeString(GetPrefixOfNamespace(xNamespace, allowDefaultNamespace: false), localName, (namespaceName.Length == 0 && localName == "xmlns") ? "http://www.w3.org/2000/xmlns/" : namespaceName, attribute.Value);
			}
			_element = null;
			_attributes.Clear();
		}

		private string GetPrefixOfNamespace(XNamespace ns, bool allowDefaultNamespace)
		{
			string namespaceName = ns.NamespaceName;
			if (namespaceName.Length == 0)
			{
				return string.Empty;
			}
			string prefixOfNamespace = _resolver.GetPrefixOfNamespace(ns, allowDefaultNamespace);
			if (prefixOfNamespace != null)
			{
				return prefixOfNamespace;
			}
			if ((object)namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			if ((object)namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		private void PushElement()
		{
			_resolver.PushScope();
			foreach (XAttribute attribute in _attributes)
			{
				if (attribute.IsNamespaceDeclaration)
				{
					_resolver.Add((attribute.Name.NamespaceName.Length == 0) ? string.Empty : attribute.Name.LocalName, XNamespace.Get(attribute.Value));
				}
			}
		}

		private void Write(object content)
		{
			if (content == null)
			{
				return;
			}
			if (content is XNode n)
			{
				WriteNode(n);
				return;
			}
			if (content is string s)
			{
				WriteString(s);
				return;
			}
			if (content is XAttribute a)
			{
				WriteAttribute(a);
				return;
			}
			if (content is XStreamingElement e)
			{
				WriteStreamingElement(e);
				return;
			}
			if (content is object[] array)
			{
				object[] array2 = array;
				foreach (object content2 in array2)
				{
					Write(content2);
				}
				return;
			}
			if (content is IEnumerable enumerable)
			{
				{
					foreach (object item in enumerable)
					{
						Write(item);
					}
					return;
				}
			}
			WriteString(XContainer.GetStringValue(content));
		}

		private void WriteAttribute(XAttribute a)
		{
			if (_element == null)
			{
				throw new InvalidOperationException("An attribute cannot be written after content.");
			}
			_attributes.Add(a);
		}

		private void WriteNode(XNode n)
		{
			FlushElement();
			n.WriteTo(_writer);
		}

		internal void WriteStreamingElement(XStreamingElement e)
		{
			FlushElement();
			_element = e;
			Write(e.content);
			FlushElement();
			_writer.WriteEndElement();
			_resolver.PopScope();
		}

		private void WriteString(string s)
		{
			FlushElement();
			_writer.WriteString(s);
		}
	}
	/// <summary>Specifies the event type when an event is raised for an <see cref="T:System.Xml.Linq.XObject" />.</summary>
	public enum XObjectChange
	{
		/// <summary>An <see cref="T:System.Xml.Linq.XObject" /> has been or will be added to an <see cref="T:System.Xml.Linq.XContainer" />.</summary>
		Add,
		/// <summary>An <see cref="T:System.Xml.Linq.XObject" /> has been or will be removed from an <see cref="T:System.Xml.Linq.XContainer" />.</summary>
		Remove,
		/// <summary>An <see cref="T:System.Xml.Linq.XObject" /> has been or will be renamed.</summary>
		Name,
		/// <summary>The value of an <see cref="T:System.Xml.Linq.XObject" /> has been or will be changed. In addition, a change in the serialization of an empty element (either from an empty tag to start/end tag pair or vice versa) raises this event.</summary>
		Value
	}
	/// <summary>Specifies load options when parsing XML.</summary>
	[Flags]
	public enum LoadOptions
	{
		/// <summary>Does not preserve insignificant white space or load base URI and line information.</summary>
		None = 0,
		/// <summary>Preserves insignificant white space while parsing.</summary>
		PreserveWhitespace = 1,
		/// <summary>Requests the base URI information from the <see cref="T:System.Xml.XmlReader" />, and makes it available via the <see cref="P:System.Xml.Linq.XObject.BaseUri" /> property.</summary>
		SetBaseUri = 2,
		/// <summary>Requests the line information from the <see cref="T:System.Xml.XmlReader" /> and makes it available via properties on <see cref="T:System.Xml.Linq.XObject" />.</summary>
		SetLineInfo = 4
	}
	/// <summary>Specifies serialization options.</summary>
	[Flags]
	public enum SaveOptions
	{
		/// <summary>Format (indent) the XML while serializing.</summary>
		None = 0,
		/// <summary>Preserve all insignificant white space while serializing.</summary>
		DisableFormatting = 1,
		/// <summary>Remove the duplicate namespace declarations while serializing.</summary>
		OmitDuplicateNamespaces = 2
	}
	/// <summary>Specifies whether to omit duplicate namespaces when loading an <see cref="T:System.Xml.Linq.XDocument" /> with an <see cref="T:System.Xml.XmlReader" />.</summary>
	[Flags]
	public enum ReaderOptions
	{
		/// <summary>No reader options specified.</summary>
		None = 0,
		/// <summary>Omit duplicate namespaces when loading the <see cref="T:System.Xml.Linq.XDocument" />.</summary>
		OmitDuplicateNamespaces = 1
	}
	/// <summary>Represents a name of an XML element or attribute.</summary>
	[Serializable]
	public sealed class XName : IEquatable<XName>, ISerializable
	{
		private XNamespace _ns;

		private string _localName;

		private int _hashCode;

		/// <summary>Gets the local (unqualified) part of the name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the local (unqualified) part of the name.</returns>
		public string LocalName => _localName;

		/// <summary>Gets the namespace part of the fully qualified name.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNamespace" /> that contains the namespace part of the name.</returns>
		public XNamespace Namespace => _ns;

		/// <summary>Returns the URI of the <see cref="T:System.Xml.Linq.XNamespace" /> for this <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <returns>The URI of the <see cref="T:System.Xml.Linq.XNamespace" /> for this <see cref="T:System.Xml.Linq.XName" />.</returns>
		public string NamespaceName => _ns.NamespaceName;

		internal XName(XNamespace ns, string localName)
		{
			_ns = ns;
			_localName = XmlConvert.VerifyNCName(localName);
			_hashCode = ns.GetHashCode() ^ localName.GetHashCode();
		}

		/// <summary>Returns the expanded XML name in the format {namespace}localname.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the expanded XML name in the format {namespace}localname.</returns>
		public override string ToString()
		{
			if (_ns.NamespaceName.Length == 0)
			{
				return _localName;
			}
			return "{" + _ns.NamespaceName + "}" + _localName;
		}

		/// <summary>Gets an <see cref="T:System.Xml.Linq.XName" /> object from an expanded name.</summary>
		/// <param name="expandedName">A <see cref="T:System.String" /> that contains an expanded XML name in the format {namespace}localname.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> object constructed from the expanded name.</returns>
		public static XName Get(string expandedName)
		{
			if (expandedName == null)
			{
				throw new ArgumentNullException("expandedName");
			}
			if (expandedName.Length == 0)
			{
				throw new ArgumentException(global::SR.Format("'{0}' is an invalid expanded name.", expandedName));
			}
			if (expandedName[0] == '{')
			{
				int num = expandedName.LastIndexOf('}');
				if (num <= 1 || num == expandedName.Length - 1)
				{
					throw new ArgumentException(global::SR.Format("'{0}' is an invalid expanded name.", expandedName));
				}
				return XNamespace.Get(expandedName, 1, num - 1).GetName(expandedName, num + 1, expandedName.Length - num - 1);
			}
			return XNamespace.None.GetName(expandedName);
		}

		/// <summary>Gets an <see cref="T:System.Xml.Linq.XName" /> object from a local name and a namespace.</summary>
		/// <param name="localName">A local (unqualified) name.</param>
		/// <param name="namespaceName">An XML namespace.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> object created from the specified local name and namespace.</returns>
		public static XName Get(string localName, string namespaceName)
		{
			return XNamespace.Get(namespaceName).GetName(localName);
		}

		/// <summary>Converts a string formatted as an expanded XML name (that is,{namespace}localname) to an <see cref="T:System.Xml.Linq.XName" /> object.</summary>
		/// <param name="expandedName">A string that contains an expanded XML name in the format {namespace}localname.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> object constructed from the expanded name.</returns>
		[CLSCompliant(false)]
		public static implicit operator XName(string expandedName)
		{
			if (expandedName == null)
			{
				return null;
			}
			return Get(expandedName);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Xml.Linq.XName" /> is equal to this <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <param name="obj">The <see cref="T:System.Xml.Linq.XName" /> to compare to the current <see cref="T:System.Xml.Linq.XName" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Xml.Linq.XName" /> is equal to the current <see cref="T:System.Xml.Linq.XName" />; otherwise <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the hash code for the <see cref="T:System.Xml.Linq.XName" />.</returns>
		public override int GetHashCode()
		{
			return _hashCode;
		}

		/// <summary>Returns a value indicating whether two instances of <see cref="T:System.Xml.Linq.XName" /> are equal.</summary>
		/// <param name="left">The first <see cref="T:System.Xml.Linq.XName" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Xml.Linq.XName" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise <see langword="false" />.</returns>
		public static bool operator ==(XName left, XName right)
		{
			return (object)left == right;
		}

		/// <summary>Returns a value indicating whether two instances of <see cref="T:System.Xml.Linq.XName" /> are not equal.</summary>
		/// <param name="left">The first <see cref="T:System.Xml.Linq.XName" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Xml.Linq.XName" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise <see langword="false" />.</returns>
		public static bool operator !=(XName left, XName right)
		{
			return (object)left != right;
		}

		/// <summary>Indicates whether the current <see cref="T:System.Xml.Linq.XName" /> is equal to the specified <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XName" /> to compare with this <see cref="T:System.Xml.Linq.XName" />.</param>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Xml.Linq.XName" /> is equal to the specified <see cref="T:System.Xml.Linq.XName" />, otherwise <see langword="false" />.</returns>
		bool IEquatable<XName>.Equals(XName other)
		{
			return (object)this == other;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data required to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		internal XName()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Represents an XML namespace. This class cannot be inherited.</summary>
	public sealed class XNamespace
	{
		internal const string xmlPrefixNamespace = "http://www.w3.org/XML/1998/namespace";

		internal const string xmlnsPrefixNamespace = "http://www.w3.org/2000/xmlns/";

		private static XHashtable<WeakReference> s_namespaces;

		private static WeakReference s_refNone;

		private static WeakReference s_refXml;

		private static WeakReference s_refXmlns;

		private string _namespaceName;

		private int _hashCode;

		private XHashtable<XName> _names;

		private const int NamesCapacity = 8;

		private const int NamespacesCapacity = 32;

		/// <summary>Gets the Uniform Resource Identifier (URI) of this namespace.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the URI of the namespace.</returns>
		public string NamespaceName => _namespaceName;

		/// <summary>Gets the <see cref="T:System.Xml.Linq.XNamespace" /> object that corresponds to no namespace.</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XNamespace" /> that corresponds to no namespace.</returns>
		public static XNamespace None => EnsureNamespace(ref s_refNone, string.Empty);

		/// <summary>Gets the <see cref="T:System.Xml.Linq.XNamespace" /> object that corresponds to the XML URI (http://www.w3.org/XML/1998/namespace).</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XNamespace" /> that corresponds to the XML URI (http://www.w3.org/XML/1998/namespace).</returns>
		public static XNamespace Xml => EnsureNamespace(ref s_refXml, "http://www.w3.org/XML/1998/namespace");

		/// <summary>Gets the <see cref="T:System.Xml.Linq.XNamespace" /> object that corresponds to the xmlns URI (http://www.w3.org/2000/xmlns/).</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XNamespace" /> that corresponds to the xmlns URI (http://www.w3.org/2000/xmlns/).</returns>
		public static XNamespace Xmlns => EnsureNamespace(ref s_refXmlns, "http://www.w3.org/2000/xmlns/");

		internal XNamespace(string namespaceName)
		{
			_namespaceName = namespaceName;
			_hashCode = namespaceName.GetHashCode();
			_names = new XHashtable<XName>(ExtractLocalName, 8);
		}

		/// <summary>Returns an <see cref="T:System.Xml.Linq.XName" /> object created from this <see cref="T:System.Xml.Linq.XNamespace" /> and the specified local name.</summary>
		/// <param name="localName">A <see cref="T:System.String" /> that contains a local name.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> created from this <see cref="T:System.Xml.Linq.XNamespace" /> and the specified local name.</returns>
		public XName GetName(string localName)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			return GetName(localName, 0, localName.Length);
		}

		/// <summary>Returns the URI of this <see cref="T:System.Xml.Linq.XNamespace" />.</summary>
		/// <returns>The URI of this <see cref="T:System.Xml.Linq.XNamespace" />.</returns>
		public override string ToString()
		{
			return _namespaceName;
		}

		/// <summary>Gets an <see cref="T:System.Xml.Linq.XNamespace" /> for the specified Uniform Resource Identifier (URI).</summary>
		/// <param name="namespaceName">A <see cref="T:System.String" /> that contains a namespace URI.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XNamespace" /> created from the specified URI.</returns>
		public static XNamespace Get(string namespaceName)
		{
			if (namespaceName == null)
			{
				throw new ArgumentNullException("namespaceName");
			}
			return Get(namespaceName, 0, namespaceName.Length);
		}

		/// <summary>Converts a string containing a Uniform Resource Identifier (URI) to an <see cref="T:System.Xml.Linq.XNamespace" />.</summary>
		/// <param name="namespaceName">A <see cref="T:System.String" /> that contains the namespace URI.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XNamespace" /> constructed from the URI string.</returns>
		[CLSCompliant(false)]
		public static implicit operator XNamespace(string namespaceName)
		{
			if (namespaceName == null)
			{
				return null;
			}
			return Get(namespaceName);
		}

		/// <summary>Combines an <see cref="T:System.Xml.Linq.XNamespace" /> object with a local name to create an <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <param name="ns">An <see cref="T:System.Xml.Linq.XNamespace" /> that contains the namespace.</param>
		/// <param name="localName">A <see cref="T:System.String" /> that contains the local name.</param>
		/// <returns>The new <see cref="T:System.Xml.Linq.XName" /> constructed from the namespace and local name.</returns>
		public static XName operator +(XNamespace ns, string localName)
		{
			if (ns == null)
			{
				throw new ArgumentNullException("ns");
			}
			return ns.GetName(localName);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Xml.Linq.XNamespace" /> is equal to the current <see cref="T:System.Xml.Linq.XNamespace" />.</summary>
		/// <param name="obj">The <see cref="T:System.Xml.Linq.XNamespace" /> to compare to the current <see cref="T:System.Xml.Linq.XNamespace" />.</param>
		/// <returns>A <see cref="T:System.Boolean" /> that indicates whether the specified <see cref="T:System.Xml.Linq.XNamespace" /> is equal to the current <see cref="T:System.Xml.Linq.XNamespace" />.</returns>
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Xml.Linq.XNamespace" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the hash code for the <see cref="T:System.Xml.Linq.XNamespace" />.</returns>
		public override int GetHashCode()
		{
			return _hashCode;
		}

		/// <summary>Returns a value indicating whether two instances of <see cref="T:System.Xml.Linq.XNamespace" /> are equal.</summary>
		/// <param name="left">The first <see cref="T:System.Xml.Linq.XNamespace" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Xml.Linq.XNamespace" /> to compare.</param>
		/// <returns>A <see cref="T:System.Boolean" /> that indicates whether <paramref name="left" /> and <paramref name="right" /> are equal.</returns>
		public static bool operator ==(XNamespace left, XNamespace right)
		{
			return (object)left == right;
		}

		/// <summary>Returns a value indicating whether two instances of <see cref="T:System.Xml.Linq.XNamespace" /> are not equal.</summary>
		/// <param name="left">The first <see cref="T:System.Xml.Linq.XNamespace" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Xml.Linq.XNamespace" /> to compare.</param>
		/// <returns>A <see cref="T:System.Boolean" /> that indicates whether <paramref name="left" /> and <paramref name="right" /> are not equal.</returns>
		public static bool operator !=(XNamespace left, XNamespace right)
		{
			return (object)left != right;
		}

		internal XName GetName(string localName, int index, int count)
		{
			if (_names.TryGetValue(localName, index, count, out var value))
			{
				return value;
			}
			return _names.Add(new XName(this, localName.Substring(index, count)));
		}

		internal static XNamespace Get(string namespaceName, int index, int count)
		{
			if (count == 0)
			{
				return None;
			}
			if (s_namespaces == null)
			{
				Interlocked.CompareExchange(ref s_namespaces, new XHashtable<WeakReference>(ExtractNamespace, 32), null);
			}
			XNamespace xNamespace;
			do
			{
				if (!s_namespaces.TryGetValue(namespaceName, index, count, out var value))
				{
					if (count == "http://www.w3.org/XML/1998/namespace".Length && string.CompareOrdinal(namespaceName, index, "http://www.w3.org/XML/1998/namespace", 0, count) == 0)
					{
						return Xml;
					}
					if (count == "http://www.w3.org/2000/xmlns/".Length && string.CompareOrdinal(namespaceName, index, "http://www.w3.org/2000/xmlns/", 0, count) == 0)
					{
						return Xmlns;
					}
					value = s_namespaces.Add(new WeakReference(new XNamespace(namespaceName.Substring(index, count))));
				}
				xNamespace = ((value != null) ? ((XNamespace)value.Target) : null);
			}
			while (xNamespace == null);
			return xNamespace;
		}

		private static string ExtractLocalName(XName n)
		{
			return n.LocalName;
		}

		private static string ExtractNamespace(WeakReference r)
		{
			XNamespace xNamespace;
			if (r == null || (xNamespace = (XNamespace)r.Target) == null)
			{
				return null;
			}
			return xNamespace.NamespaceName;
		}

		private static XNamespace EnsureNamespace(ref WeakReference refNmsp, string namespaceName)
		{
			XNamespace xNamespace;
			while (true)
			{
				WeakReference weakReference = refNmsp;
				if (weakReference != null)
				{
					xNamespace = (XNamespace)weakReference.Target;
					if (xNamespace != null)
					{
						break;
					}
				}
				Interlocked.CompareExchange(ref refNmsp, new WeakReference(new XNamespace(namespaceName)), weakReference);
			}
			return xNamespace;
		}

		internal XNamespace()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Represents the abstract concept of a node (element, comment, document type, processing instruction, or text node) in the XML tree.</summary>
	public abstract class XNode : XObject
	{
		private static XNodeDocumentOrderComparer s_documentOrderComparer;

		private static XNodeEqualityComparer s_equalityComparer;

		internal XNode next;

		/// <summary>Gets the next sibling node of this node.</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XNode" /> that contains the next sibling node.</returns>
		public XNode NextNode
		{
			get
			{
				if (parent != null && this != parent.content)
				{
					return next;
				}
				return null;
			}
		}

		/// <summary>Gets the previous sibling node of this node.</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XNode" /> that contains the previous sibling node.</returns>
		public XNode PreviousNode
		{
			get
			{
				if (parent == null)
				{
					return null;
				}
				XNode xNode = ((XNode)parent.content).next;
				XNode result = null;
				while (xNode != this)
				{
					result = xNode;
					xNode = xNode.next;
				}
				return result;
			}
		}

		/// <summary>Gets a comparer that can compare the relative position of two nodes.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNodeDocumentOrderComparer" /> that can compare the relative position of two nodes.</returns>
		public static XNodeDocumentOrderComparer DocumentOrderComparer
		{
			get
			{
				if (s_documentOrderComparer == null)
				{
					s_documentOrderComparer = new XNodeDocumentOrderComparer();
				}
				return s_documentOrderComparer;
			}
		}

		/// <summary>Gets a comparer that can compare two nodes for value equality.</summary>
		/// <returns>A <see cref="T:System.Xml.Linq.XNodeEqualityComparer" /> that can compare two nodes for value equality.</returns>
		public static XNodeEqualityComparer EqualityComparer
		{
			get
			{
				if (s_equalityComparer == null)
				{
					s_equalityComparer = new XNodeEqualityComparer();
				}
				return s_equalityComparer;
			}
		}

		internal XNode()
		{
		}

		/// <summary>Adds the specified content immediately after this node.</summary>
		/// <param name="content">A content object that contains simple content or a collection of content objects to be added after this node.</param>
		/// <exception cref="T:System.InvalidOperationException">The parent is <see langword="null" />.</exception>
		public void AddAfterSelf(object content)
		{
			if (parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			new Inserter(parent, this).Add(content);
		}

		/// <summary>Adds the specified content immediately after this node.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		/// <exception cref="T:System.InvalidOperationException">The parent is <see langword="null" />.</exception>
		public void AddAfterSelf(params object[] content)
		{
			AddAfterSelf((object)content);
		}

		/// <summary>Adds the specified content immediately before this node.</summary>
		/// <param name="content">A content object that contains simple content or a collection of content objects to be added before this node.</param>
		/// <exception cref="T:System.InvalidOperationException">The parent is <see langword="null" />.</exception>
		public void AddBeforeSelf(object content)
		{
			if (parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			XNode xNode = (XNode)parent.content;
			while (xNode.next != this)
			{
				xNode = xNode.next;
			}
			if (xNode == parent.content)
			{
				xNode = null;
			}
			new Inserter(parent, xNode).Add(content);
		}

		/// <summary>Adds the specified content immediately before this node.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		/// <exception cref="T:System.InvalidOperationException">The parent is <see langword="null" />.</exception>
		public void AddBeforeSelf(params object[] content)
		{
			AddBeforeSelf((object)content);
		}

		/// <summary>Returns a collection of the ancestor elements of this node.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the ancestor elements of this node.</returns>
		public IEnumerable<XElement> Ancestors()
		{
			return GetAncestors(null, self: false);
		}

		/// <summary>Returns a filtered collection of the ancestor elements of this node. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the ancestor elements of this node. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.  
		///  The nodes in the returned collection are in reverse document order.  
		///  This method uses deferred execution.</returns>
		public IEnumerable<XElement> Ancestors(XName name)
		{
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return GetAncestors(name, self: false);
		}

		/// <summary>Compares two nodes to determine their relative XML document order.</summary>
		/// <param name="n1">First <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <param name="n2">Second <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <returns>An <see langword="int" /> containing 0 if the nodes are equal; -1 if <paramref name="n1" /> is before <paramref name="n2" />; 1 if <paramref name="n1" /> is after <paramref name="n2" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The two nodes do not share a common ancestor.</exception>
		public static int CompareDocumentOrder(XNode n1, XNode n2)
		{
			if (n1 == n2)
			{
				return 0;
			}
			if (n1 == null)
			{
				return -1;
			}
			if (n2 == null)
			{
				return 1;
			}
			if (n1.parent != n2.parent)
			{
				int num = 0;
				XNode xNode = n1;
				while (xNode.parent != null)
				{
					xNode = xNode.parent;
					num++;
				}
				XNode xNode2 = n2;
				while (xNode2.parent != null)
				{
					xNode2 = xNode2.parent;
					num--;
				}
				if (xNode != xNode2)
				{
					throw new InvalidOperationException("A common ancestor is missing.");
				}
				if (num < 0)
				{
					do
					{
						n2 = n2.parent;
						num++;
					}
					while (num != 0);
					if (n1 == n2)
					{
						return -1;
					}
				}
				else if (num > 0)
				{
					do
					{
						n1 = n1.parent;
						num--;
					}
					while (num != 0);
					if (n1 == n2)
					{
						return 1;
					}
				}
				while (n1.parent != n2.parent)
				{
					n1 = n1.parent;
					n2 = n2.parent;
				}
			}
			else if (n1.parent == null)
			{
				throw new InvalidOperationException("A common ancestor is missing.");
			}
			XNode xNode3 = (XNode)n1.parent.content;
			do
			{
				xNode3 = xNode3.next;
				if (xNode3 == n1)
				{
					return -1;
				}
			}
			while (xNode3 != n2);
			return 1;
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlReader" /> for this node.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> that can be used to read this node and its descendants.</returns>
		public XmlReader CreateReader()
		{
			return new XNodeReader(this, null);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlReader" /> with the options specified by the <paramref name="readerOptions" /> parameter.</summary>
		/// <param name="readerOptions">A <see cref="T:System.Xml.Linq.ReaderOptions" /> object that specifies whether to omit duplicate namespaces.</param>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> object.</returns>
		public XmlReader CreateReader(ReaderOptions readerOptions)
		{
			return new XNodeReader(this, null, readerOptions);
		}

		/// <summary>Returns a collection of the sibling nodes after this node, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> of the sibling nodes after this node, in document order.</returns>
		public IEnumerable<XNode> NodesAfterSelf()
		{
			XNode n = this;
			while (n.parent != null && n != n.parent.content)
			{
				n = n.next;
				yield return n;
			}
		}

		/// <summary>Returns a collection of the sibling nodes before this node, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> of the sibling nodes before this node, in document order.</returns>
		public IEnumerable<XNode> NodesBeforeSelf()
		{
			if (parent == null)
			{
				yield break;
			}
			XNode n = (XNode)parent.content;
			do
			{
				n = n.next;
				if (n != this)
				{
					yield return n;
					continue;
				}
				break;
			}
			while (parent != null && parent == n.parent);
		}

		/// <summary>Returns a collection of the sibling elements after this node, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the sibling elements after this node, in document order.</returns>
		public IEnumerable<XElement> ElementsAfterSelf()
		{
			return GetElementsAfterSelf(null);
		}

		/// <summary>Returns a filtered collection of the sibling elements after this node, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the sibling elements after this node, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		public IEnumerable<XElement> ElementsAfterSelf(XName name)
		{
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return GetElementsAfterSelf(name);
		}

		/// <summary>Returns a collection of the sibling elements before this node, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the sibling elements before this node, in document order.</returns>
		public IEnumerable<XElement> ElementsBeforeSelf()
		{
			return GetElementsBeforeSelf(null);
		}

		/// <summary>Returns a filtered collection of the sibling elements before this node, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the sibling elements before this node, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		public IEnumerable<XElement> ElementsBeforeSelf(XName name)
		{
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return GetElementsBeforeSelf(name);
		}

		/// <summary>Determines if the current node appears after a specified node in terms of document order.</summary>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> to compare for document order.</param>
		/// <returns>
		///   <see langword="true" /> if this node appears after the specified node; otherwise <see langword="false" />.</returns>
		public bool IsAfter(XNode node)
		{
			return CompareDocumentOrder(this, node) > 0;
		}

		/// <summary>Determines if the current node appears before a specified node in terms of document order.</summary>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> to compare for document order.</param>
		/// <returns>
		///   <see langword="true" /> if this node appears before the specified node; otherwise <see langword="false" />.</returns>
		public bool IsBefore(XNode node)
		{
			return CompareDocumentOrder(this, node) < 0;
		}

		/// <summary>Creates an <see cref="T:System.Xml.Linq.XNode" /> from an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">An <see cref="T:System.Xml.XmlReader" /> positioned at the node to read into this <see cref="T:System.Xml.Linq.XNode" />.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XNode" /> that contains the node and its descendant nodes that were read from the reader. The runtime type of the node is determined by the node type (<see cref="P:System.Xml.Linq.XObject.NodeType" />) of the first node encountered in the reader.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on a recognized node type.</exception>
		/// <exception cref="T:System.Xml.XmlException">The underlying <see cref="T:System.Xml.XmlReader" /> throws an exception.</exception>
		public static XNode ReadFrom(XmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.ReadState != ReadState.Interactive)
			{
				throw new InvalidOperationException("The XmlReader state should be Interactive.");
			}
			switch (reader.NodeType)
			{
			case XmlNodeType.Text:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				return new XText(reader);
			case XmlNodeType.CDATA:
				return new XCData(reader);
			case XmlNodeType.Comment:
				return new XComment(reader);
			case XmlNodeType.DocumentType:
				return new XDocumentType(reader);
			case XmlNodeType.Element:
				return new XElement(reader);
			case XmlNodeType.ProcessingInstruction:
				return new XProcessingInstruction(reader);
			default:
				throw new InvalidOperationException(global::SR.Format("The XmlReader should not be on a node of type {0}.", reader.NodeType));
			}
		}

		public static Task<XNode> ReadFromAsync(XmlReader reader, CancellationToken cancellationToken)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<XNode>(cancellationToken);
			}
			return ReadFromAsyncInternal(reader, cancellationToken);
		}

		private static async Task<XNode> ReadFromAsyncInternal(XmlReader reader, CancellationToken cancellationToken)
		{
			if (reader.ReadState != ReadState.Interactive)
			{
				throw new InvalidOperationException("The XmlReader state should be Interactive.");
			}
			XNode ret;
			switch (reader.NodeType)
			{
			case XmlNodeType.Text:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				ret = new XText(reader.Value);
				break;
			case XmlNodeType.CDATA:
				ret = new XCData(reader.Value);
				break;
			case XmlNodeType.Comment:
				ret = new XComment(reader.Value);
				break;
			case XmlNodeType.DocumentType:
			{
				string name2 = reader.Name;
				string attribute = reader.GetAttribute("PUBLIC");
				string attribute2 = reader.GetAttribute("SYSTEM");
				string value2 = reader.Value;
				ret = new XDocumentType(name2, attribute, attribute2, value2);
				break;
			}
			case XmlNodeType.Element:
				return await XElement.CreateAsync(reader, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			case XmlNodeType.ProcessingInstruction:
			{
				string name = reader.Name;
				string value = reader.Value;
				ret = new XProcessingInstruction(name, value);
				break;
			}
			default:
				throw new InvalidOperationException(global::SR.Format("The XmlReader should not be on a node of type {0}.", reader.NodeType));
			}
			cancellationToken.ThrowIfCancellationRequested();
			await reader.ReadAsync().ConfigureAwait(continueOnCapturedContext: false);
			return ret;
		}

		/// <summary>Removes this node from its parent.</summary>
		/// <exception cref="T:System.InvalidOperationException">The parent is <see langword="null" />.</exception>
		public void Remove()
		{
			if (parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			parent.RemoveNode(this);
		}

		/// <summary>Replaces this node with the specified content.</summary>
		/// <param name="content">Content that replaces this node.</param>
		public void ReplaceWith(object content)
		{
			if (parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			XContainer xContainer = parent;
			XNode xNode = (XNode)parent.content;
			while (xNode.next != this)
			{
				xNode = xNode.next;
			}
			if (xNode == parent.content)
			{
				xNode = null;
			}
			parent.RemoveNode(this);
			if (xNode != null && xNode.parent != xContainer)
			{
				throw new InvalidOperationException("This operation was corrupted by external code.");
			}
			new Inserter(xContainer, xNode).Add(content);
		}

		/// <summary>Replaces this node with the specified content.</summary>
		/// <param name="content">A parameter list of the new content.</param>
		public void ReplaceWith(params object[] content)
		{
			ReplaceWith((object)content);
		}

		/// <summary>Returns the indented XML for this node.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the indented XML.</returns>
		public override string ToString()
		{
			return GetXmlString(GetSaveOptionsFromAnnotations());
		}

		/// <summary>Returns the XML for this node, optionally disabling formatting.</summary>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		/// <returns>A <see cref="T:System.String" /> containing the XML.</returns>
		public string ToString(SaveOptions options)
		{
			return GetXmlString(options);
		}

		/// <summary>Compares the values of two nodes, including the values of all descendant nodes.</summary>
		/// <param name="n1">The first <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <param name="n2">The second <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the nodes are equal; otherwise <see langword="false" />.</returns>
		public static bool DeepEquals(XNode n1, XNode n2)
		{
			if (n1 == n2)
			{
				return true;
			}
			if (n1 == null || n2 == null)
			{
				return false;
			}
			return n1.DeepEquals(n2);
		}

		/// <summary>Writes this node to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		public abstract void WriteTo(XmlWriter writer);

		public abstract Task WriteToAsync(XmlWriter writer, CancellationToken cancellationToken);

		internal virtual void AppendText(StringBuilder sb)
		{
		}

		internal abstract XNode CloneNode();

		internal abstract bool DeepEquals(XNode node);

		internal IEnumerable<XElement> GetAncestors(XName name, bool self)
		{
			for (XElement e = (self ? this : parent) as XElement; e != null; e = e.parent as XElement)
			{
				if (name == null || e.name == name)
				{
					yield return e;
				}
			}
		}

		private IEnumerable<XElement> GetElementsAfterSelf(XName name)
		{
			XNode n = this;
			while (n.parent != null && n != n.parent.content)
			{
				n = n.next;
				if (n is XElement xElement && (name == null || xElement.name == name))
				{
					yield return xElement;
				}
			}
		}

		private IEnumerable<XElement> GetElementsBeforeSelf(XName name)
		{
			if (parent == null)
			{
				yield break;
			}
			XNode n = (XNode)parent.content;
			do
			{
				n = n.next;
				if (n != this)
				{
					if (n is XElement xElement && (name == null || xElement.name == name))
					{
						yield return xElement;
					}
					continue;
				}
				break;
			}
			while (parent != null && parent == n.parent);
		}

		internal abstract int GetDeepHashCode();

		internal static XmlReaderSettings GetXmlReaderSettings(LoadOptions o)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			if ((o & LoadOptions.PreserveWhitespace) == 0)
			{
				xmlReaderSettings.IgnoreWhitespace = true;
			}
			xmlReaderSettings.DtdProcessing = DtdProcessing.Parse;
			xmlReaderSettings.MaxCharactersFromEntities = 10000000L;
			return xmlReaderSettings;
		}

		internal static XmlWriterSettings GetXmlWriterSettings(SaveOptions o)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			if ((o & SaveOptions.DisableFormatting) == 0)
			{
				xmlWriterSettings.Indent = true;
			}
			if ((o & SaveOptions.OmitDuplicateNamespaces) != SaveOptions.None)
			{
				xmlWriterSettings.NamespaceHandling |= NamespaceHandling.OmitDuplicates;
			}
			return xmlWriterSettings;
		}

		private string GetXmlString(SaveOptions o)
		{
			using StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.OmitXmlDeclaration = true;
			if ((o & SaveOptions.DisableFormatting) == 0)
			{
				xmlWriterSettings.Indent = true;
			}
			if ((o & SaveOptions.OmitDuplicateNamespaces) != SaveOptions.None)
			{
				xmlWriterSettings.NamespaceHandling |= NamespaceHandling.OmitDuplicates;
			}
			if (this is XText)
			{
				xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
			}
			using (XmlWriter writer = XmlWriter.Create(stringWriter, xmlWriterSettings))
			{
				if (this is XDocument xDocument)
				{
					xDocument.WriteContentTo(writer);
				}
				else
				{
					WriteTo(writer);
				}
			}
			return stringWriter.ToString();
		}
	}
	internal class XNodeBuilder : XmlWriter
	{
		private List<object> _content;

		private XContainer _parent;

		private XName _attrName;

		private string _attrValue;

		private XContainer _root;

		public override XmlWriterSettings Settings => new XmlWriterSettings
		{
			ConformanceLevel = ConformanceLevel.Auto
		};

		public override WriteState WriteState
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public XNodeBuilder(XContainer container)
		{
			_root = container;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Close();
			}
		}

		public override void Close()
		{
			_root.Add(_content);
		}

		public override void Flush()
		{
		}

		public override string LookupPrefix(string namespaceName)
		{
			throw new NotSupportedException();
		}

		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException("This XmlWriter does not support base64 encoded data.");
		}

		public override void WriteCData(string text)
		{
			AddNode(new XCData(text));
		}

		public override void WriteCharEntity(char ch)
		{
			AddString(new string(ch, 1));
		}

		public override void WriteChars(char[] buffer, int index, int count)
		{
			AddString(new string(buffer, index, count));
		}

		public override void WriteComment(string text)
		{
			AddNode(new XComment(text));
		}

		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			AddNode(new XDocumentType(name, pubid, sysid, subset));
		}

		public override void WriteEndAttribute()
		{
			XAttribute xAttribute = new XAttribute(_attrName, _attrValue);
			_attrName = null;
			_attrValue = null;
			if (_parent != null)
			{
				_parent.Add(xAttribute);
			}
			else
			{
				Add(xAttribute);
			}
		}

		public override void WriteEndDocument()
		{
		}

		public override void WriteEndElement()
		{
			_parent = ((XElement)_parent).parent;
		}

		public override void WriteEntityRef(string name)
		{
			switch (name)
			{
			case "amp":
				AddString("&");
				break;
			case "apos":
				AddString("'");
				break;
			case "gt":
				AddString(">");
				break;
			case "lt":
				AddString("<");
				break;
			case "quot":
				AddString("\"");
				break;
			default:
				throw new NotSupportedException("This XmlWriter does not support entity references.");
			}
		}

		public override void WriteFullEndElement()
		{
			XElement xElement = (XElement)_parent;
			if (xElement.IsEmpty)
			{
				xElement.Add(string.Empty);
			}
			_parent = xElement.parent;
		}

		public override void WriteProcessingInstruction(string name, string text)
		{
			if (!(name == "xml"))
			{
				AddNode(new XProcessingInstruction(name, text));
			}
		}

		public override void WriteRaw(char[] buffer, int index, int count)
		{
			AddString(new string(buffer, index, count));
		}

		public override void WriteRaw(string data)
		{
			AddString(data);
		}

		public override void WriteStartAttribute(string prefix, string localName, string namespaceName)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			_attrName = XNamespace.Get((prefix.Length == 0) ? string.Empty : namespaceName).GetName(localName);
			_attrValue = string.Empty;
		}

		public override void WriteStartDocument()
		{
		}

		public override void WriteStartDocument(bool standalone)
		{
		}

		public override void WriteStartElement(string prefix, string localName, string namespaceName)
		{
			AddNode(new XElement(XNamespace.Get(namespaceName).GetName(localName)));
		}

		public override void WriteString(string text)
		{
			AddString(text);
		}

		public override void WriteSurrogateCharEntity(char lowCh, char highCh)
		{
			AddString(new string(new char[2] { highCh, lowCh }));
		}

		public override void WriteValue(DateTimeOffset value)
		{
			WriteString(XmlConvert.ToString(value));
		}

		public override void WriteWhitespace(string ws)
		{
			AddString(ws);
		}

		private void Add(object o)
		{
			if (_content == null)
			{
				_content = new List<object>();
			}
			_content.Add(o);
		}

		private void AddNode(XNode n)
		{
			if (_parent != null)
			{
				_parent.Add(n);
			}
			else
			{
				Add(n);
			}
			if (n is XContainer parent)
			{
				_parent = parent;
			}
		}

		private void AddString(string s)
		{
			if (s != null)
			{
				if (_attrValue != null)
				{
					_attrValue += s;
				}
				else if (_parent != null)
				{
					_parent.Add(s);
				}
				else
				{
					Add(s);
				}
			}
		}
	}
	/// <summary>Contains functionality to compare nodes for their document order. This class cannot be inherited.</summary>
	public sealed class XNodeDocumentOrderComparer : IComparer, IComparer<XNode>
	{
		/// <summary>Compares two nodes to determine their relative document order.</summary>
		/// <param name="x">The first <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <param name="y">The second <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <returns>An <see cref="T:System.Int32" /> that contains 0 if the nodes are equal; -1 if <paramref name="x" /> is before <paramref name="y" />; 1 if <paramref name="x" /> is after <paramref name="y" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The two nodes do not share a common ancestor.</exception>
		public int Compare(XNode x, XNode y)
		{
			return XNode.CompareDocumentOrder(x, y);
		}

		/// <summary>Compares two nodes to determine their relative document order.</summary>
		/// <param name="x">The first <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <param name="y">The second <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <returns>An <see cref="T:System.Int32" /> that contains 0 if the nodes are equal; -1 if <paramref name="x" /> is before <paramref name="y" />; 1 if <paramref name="x" /> is after <paramref name="y" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The two nodes do not share a common ancestor.</exception>
		/// <exception cref="T:System.ArgumentException">The two nodes are not derived from <see cref="T:System.Xml.Linq.XNode" />.</exception>
		int IComparer.Compare(object x, object y)
		{
			XNode xNode = x as XNode;
			if (xNode == null && x != null)
			{
				throw new ArgumentException(global::SR.Format("The argument must be derived from {0}.", typeof(XNode)), "x");
			}
			XNode xNode2 = y as XNode;
			if (xNode2 == null && y != null)
			{
				throw new ArgumentException(global::SR.Format("The argument must be derived from {0}.", typeof(XNode)), "y");
			}
			return Compare(xNode, xNode2);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XNodeDocumentOrderComparer" /> class.</summary>
		public XNodeDocumentOrderComparer()
		{
		}
	}
	/// <summary>Compares nodes to determine whether they are equal. This class cannot be inherited.</summary>
	public sealed class XNodeEqualityComparer : IEqualityComparer, IEqualityComparer<XNode>
	{
		/// <summary>Compares the values of two nodes.</summary>
		/// <param name="x">The first <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <param name="y">The second <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <returns>A <see cref="T:System.Boolean" /> indicating if the nodes are equal.</returns>
		public bool Equals(XNode x, XNode y)
		{
			return XNode.DeepEquals(x, y);
		}

		/// <summary>Returns a hash code based on an <see cref="T:System.Xml.Linq.XNode" />.</summary>
		/// <param name="obj">The <see cref="T:System.Xml.Linq.XNode" /> to hash.</param>
		/// <returns>A <see cref="T:System.Int32" /> that contains a value-based hash code for the node.</returns>
		public int GetHashCode(XNode obj)
		{
			return obj?.GetDeepHashCode() ?? 0;
		}

		/// <summary>Compares the values of two nodes.</summary>
		/// <param name="x">The first <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <param name="y">The second <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the nodes are equal; otherwise <see langword="false" />.</returns>
		bool IEqualityComparer.Equals(object x, object y)
		{
			XNode xNode = x as XNode;
			if (xNode == null && x != null)
			{
				throw new ArgumentException(global::SR.Format("The argument must be derived from {0}.", typeof(XNode)), "x");
			}
			XNode xNode2 = y as XNode;
			if (xNode2 == null && y != null)
			{
				throw new ArgumentException(global::SR.Format("The argument must be derived from {0}.", typeof(XNode)), "y");
			}
			return Equals(xNode, xNode2);
		}

		/// <summary>Returns a hash code based on the value of a node.</summary>
		/// <param name="obj">The node to hash.</param>
		/// <returns>A <see cref="T:System.Int32" /> that contains a value-based hash code for the node.</returns>
		int IEqualityComparer.GetHashCode(object obj)
		{
			XNode xNode = obj as XNode;
			if (xNode == null && obj != null)
			{
				throw new ArgumentException(global::SR.Format("The argument must be derived from {0}.", typeof(XNode)), "obj");
			}
			return GetHashCode(xNode);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XNodeEqualityComparer" /> class.</summary>
		public XNodeEqualityComparer()
		{
		}
	}
	internal class XNodeReader : XmlReader, IXmlLineInfo
	{
		private static readonly char[] s_WhitespaceChars = new char[4] { ' ', '\t', '\n', '\r' };

		private object _source;

		private object _parent;

		private ReadState _state;

		private XNode _root;

		private XmlNameTable _nameTable;

		private bool _omitDuplicateNamespaces;

		public override int AttributeCount
		{
			get
			{
				if (!IsInteractive)
				{
					return 0;
				}
				int num = 0;
				XElement elementInAttributeScope = GetElementInAttributeScope();
				if (elementInAttributeScope != null)
				{
					XAttribute xAttribute = elementInAttributeScope.lastAttr;
					if (xAttribute != null)
					{
						do
						{
							xAttribute = xAttribute.next;
							if (!_omitDuplicateNamespaces || !IsDuplicateNamespaceAttribute(xAttribute))
							{
								num++;
							}
						}
						while (xAttribute != elementInAttributeScope.lastAttr);
					}
				}
				return num;
			}
		}

		public override string BaseURI
		{
			get
			{
				if (_source is XObject xObject)
				{
					return xObject.BaseUri;
				}
				if (_parent is XObject xObject2)
				{
					return xObject2.BaseUri;
				}
				return string.Empty;
			}
		}

		public override int Depth
		{
			get
			{
				if (!IsInteractive)
				{
					return 0;
				}
				if (_source is XObject o)
				{
					return GetDepth(o);
				}
				if (_parent is XObject o2)
				{
					return GetDepth(o2) + 1;
				}
				return 0;
			}
		}

		public override bool EOF => _state == ReadState.EndOfFile;

		public override bool HasAttributes
		{
			get
			{
				if (!IsInteractive)
				{
					return false;
				}
				XElement elementInAttributeScope = GetElementInAttributeScope();
				if (elementInAttributeScope != null && elementInAttributeScope.lastAttr != null)
				{
					if (_omitDuplicateNamespaces)
					{
						return GetFirstNonDuplicateNamespaceAttribute(elementInAttributeScope.lastAttr.next) != null;
					}
					return true;
				}
				return false;
			}
		}

		public override bool HasValue
		{
			get
			{
				if (!IsInteractive)
				{
					return false;
				}
				if (_source is XObject xObject)
				{
					switch (xObject.NodeType)
					{
					case XmlNodeType.Attribute:
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
					case XmlNodeType.DocumentType:
						return true;
					default:
						return false;
					}
				}
				return true;
			}
		}

		public override bool IsEmptyElement
		{
			get
			{
				if (!IsInteractive)
				{
					return false;
				}
				if (_source is XElement xElement)
				{
					return xElement.IsEmpty;
				}
				return false;
			}
		}

		public override string LocalName => _nameTable.Add(GetLocalName());

		public override string Name
		{
			get
			{
				string prefix = GetPrefix();
				if (prefix.Length == 0)
				{
					return _nameTable.Add(GetLocalName());
				}
				return _nameTable.Add(prefix + ":" + GetLocalName());
			}
		}

		public override string NamespaceURI => _nameTable.Add(GetNamespaceURI());

		public override XmlNameTable NameTable => _nameTable;

		public override XmlNodeType NodeType
		{
			get
			{
				if (!IsInteractive)
				{
					return XmlNodeType.None;
				}
				if (_source is XObject xObject)
				{
					if (IsEndElement)
					{
						return XmlNodeType.EndElement;
					}
					XmlNodeType nodeType = xObject.NodeType;
					if (nodeType != XmlNodeType.Text)
					{
						return nodeType;
					}
					if (xObject.parent != null && xObject.parent.parent == null && xObject.parent is XDocument)
					{
						return XmlNodeType.Whitespace;
					}
					return XmlNodeType.Text;
				}
				if (_parent is XDocument)
				{
					return XmlNodeType.Whitespace;
				}
				return XmlNodeType.Text;
			}
		}

		public override string Prefix => _nameTable.Add(GetPrefix());

		public override ReadState ReadState => _state;

		public override XmlReaderSettings Settings => new XmlReaderSettings
		{
			CheckCharacters = false
		};

		public override string Value
		{
			get
			{
				if (!IsInteractive)
				{
					return string.Empty;
				}
				if (_source is XObject xObject)
				{
					switch (xObject.NodeType)
					{
					case XmlNodeType.Attribute:
						return ((XAttribute)xObject).Value;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						return ((XText)xObject).Value;
					case XmlNodeType.Comment:
						return ((XComment)xObject).Value;
					case XmlNodeType.ProcessingInstruction:
						return ((XProcessingInstruction)xObject).Data;
					case XmlNodeType.DocumentType:
						return ((XDocumentType)xObject).InternalSubset;
					default:
						return string.Empty;
					}
				}
				return (string)_source;
			}
		}

		public override string XmlLang
		{
			get
			{
				if (!IsInteractive)
				{
					return string.Empty;
				}
				XElement xElement = GetElementInScope();
				if (xElement != null)
				{
					XName name = XNamespace.Xml.GetName("lang");
					do
					{
						XAttribute xAttribute = xElement.Attribute(name);
						if (xAttribute != null)
						{
							return xAttribute.Value;
						}
						xElement = xElement.parent as XElement;
					}
					while (xElement != null);
				}
				return string.Empty;
			}
		}

		public override XmlSpace XmlSpace
		{
			get
			{
				if (!IsInteractive)
				{
					return XmlSpace.None;
				}
				XElement xElement = GetElementInScope();
				if (xElement != null)
				{
					XName name = XNamespace.Xml.GetName("space");
					do
					{
						XAttribute xAttribute = xElement.Attribute(name);
						if (xAttribute != null)
						{
							string text = xAttribute.Value.Trim(s_WhitespaceChars);
							if (text == "preserve")
							{
								return XmlSpace.Preserve;
							}
							if (text == "default")
							{
								return XmlSpace.Default;
							}
						}
						xElement = xElement.parent as XElement;
					}
					while (xElement != null);
				}
				return XmlSpace.None;
			}
		}

		int IXmlLineInfo.LineNumber
		{
			get
			{
				if (IsEndElement)
				{
					if (_source is XElement xElement)
					{
						LineInfoEndElementAnnotation lineInfoEndElementAnnotation = xElement.Annotation<LineInfoEndElementAnnotation>();
						if (lineInfoEndElementAnnotation != null)
						{
							return lineInfoEndElementAnnotation.lineNumber;
						}
					}
				}
				else if (_source is IXmlLineInfo xmlLineInfo)
				{
					return xmlLineInfo.LineNumber;
				}
				return 0;
			}
		}

		int IXmlLineInfo.LinePosition
		{
			get
			{
				if (IsEndElement)
				{
					if (_source is XElement xElement)
					{
						LineInfoEndElementAnnotation lineInfoEndElementAnnotation = xElement.Annotation<LineInfoEndElementAnnotation>();
						if (lineInfoEndElementAnnotation != null)
						{
							return lineInfoEndElementAnnotation.linePosition;
						}
					}
				}
				else if (_source is IXmlLineInfo xmlLineInfo)
				{
					return xmlLineInfo.LinePosition;
				}
				return 0;
			}
		}

		private bool IsEndElement
		{
			get
			{
				return _parent == _source;
			}
			set
			{
				_parent = (value ? _source : null);
			}
		}

		private bool IsInteractive => _state == ReadState.Interactive;

		internal XNodeReader(XNode node, XmlNameTable nameTable, ReaderOptions options)
		{
			_source = node;
			_root = node;
			_nameTable = ((nameTable != null) ? nameTable : CreateNameTable());
			_omitDuplicateNamespaces = (((options & ReaderOptions.OmitDuplicateNamespaces) != ReaderOptions.None) ? true : false);
		}

		internal XNodeReader(XNode node, XmlNameTable nameTable)
			: this(node, nameTable, ((node.GetSaveOptionsFromAnnotations() & SaveOptions.OmitDuplicateNamespaces) != SaveOptions.None) ? ReaderOptions.OmitDuplicateNamespaces : ReaderOptions.None)
		{
		}

		private static int GetDepth(XObject o)
		{
			int num = 0;
			while (o.parent != null)
			{
				num++;
				o = o.parent;
			}
			if (o is XDocument)
			{
				num--;
			}
			return num;
		}

		private string GetLocalName()
		{
			if (!IsInteractive)
			{
				return string.Empty;
			}
			if (_source is XElement xElement)
			{
				return xElement.Name.LocalName;
			}
			if (_source is XAttribute xAttribute)
			{
				return xAttribute.Name.LocalName;
			}
			if (_source is XProcessingInstruction xProcessingInstruction)
			{
				return xProcessingInstruction.Target;
			}
			if (_source is XDocumentType xDocumentType)
			{
				return xDocumentType.Name;
			}
			return string.Empty;
		}

		private string GetNamespaceURI()
		{
			if (!IsInteractive)
			{
				return string.Empty;
			}
			if (_source is XElement xElement)
			{
				return xElement.Name.NamespaceName;
			}
			if (_source is XAttribute xAttribute)
			{
				string namespaceName = xAttribute.Name.NamespaceName;
				if (namespaceName.Length == 0 && xAttribute.Name.LocalName == "xmlns")
				{
					return "http://www.w3.org/2000/xmlns/";
				}
				return namespaceName;
			}
			return string.Empty;
		}

		private string GetPrefix()
		{
			if (!IsInteractive)
			{
				return string.Empty;
			}
			if (_source is XElement xElement)
			{
				string prefixOfNamespace = xElement.GetPrefixOfNamespace(xElement.Name.Namespace);
				if (prefixOfNamespace != null)
				{
					return prefixOfNamespace;
				}
				return string.Empty;
			}
			if (_source is XAttribute xAttribute)
			{
				string prefixOfNamespace2 = xAttribute.GetPrefixOfNamespace(xAttribute.Name.Namespace);
				if (prefixOfNamespace2 != null)
				{
					return prefixOfNamespace2;
				}
			}
			return string.Empty;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && ReadState != ReadState.Closed)
			{
				Close();
			}
		}

		public override void Close()
		{
			_source = null;
			_parent = null;
			_root = null;
			_state = ReadState.Closed;
		}

		public override string GetAttribute(string name)
		{
			if (!IsInteractive)
			{
				return null;
			}
			XElement elementInAttributeScope = GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				GetNameInAttributeScope(name, elementInAttributeScope, out var localName, out var namespaceName);
				XAttribute xAttribute = elementInAttributeScope.lastAttr;
				if (xAttribute != null)
				{
					do
					{
						xAttribute = xAttribute.next;
						if (xAttribute.Name.LocalName == localName && xAttribute.Name.NamespaceName == namespaceName)
						{
							if (_omitDuplicateNamespaces && IsDuplicateNamespaceAttribute(xAttribute))
							{
								return null;
							}
							return xAttribute.Value;
						}
					}
					while (xAttribute != elementInAttributeScope.lastAttr);
				}
				return null;
			}
			if (_source is XDocumentType xDocumentType)
			{
				if (name == "PUBLIC")
				{
					return xDocumentType.PublicId;
				}
				if (name == "SYSTEM")
				{
					return xDocumentType.SystemId;
				}
			}
			return null;
		}

		public override string GetAttribute(string localName, string namespaceName)
		{
			if (!IsInteractive)
			{
				return null;
			}
			XElement elementInAttributeScope = GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				if (localName == "xmlns")
				{
					if (namespaceName != null && namespaceName.Length == 0)
					{
						return null;
					}
					if (namespaceName == "http://www.w3.org/2000/xmlns/")
					{
						namespaceName = string.Empty;
					}
				}
				XAttribute xAttribute = elementInAttributeScope.lastAttr;
				if (xAttribute != null)
				{
					do
					{
						xAttribute = xAttribute.next;
						if (xAttribute.Name.LocalName == localName && xAttribute.Name.NamespaceName == namespaceName)
						{
							if (_omitDuplicateNamespaces && IsDuplicateNamespaceAttribute(xAttribute))
							{
								return null;
							}
							return xAttribute.Value;
						}
					}
					while (xAttribute != elementInAttributeScope.lastAttr);
				}
			}
			return null;
		}

		public override string GetAttribute(int index)
		{
			if (!IsInteractive)
			{
				return null;
			}
			if (index < 0)
			{
				return null;
			}
			XElement elementInAttributeScope = GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				XAttribute xAttribute = elementInAttributeScope.lastAttr;
				if (xAttribute != null)
				{
					do
					{
						xAttribute = xAttribute.next;
						if ((!_omitDuplicateNamespaces || !IsDuplicateNamespaceAttribute(xAttribute)) && index-- == 0)
						{
							return xAttribute.Value;
						}
					}
					while (xAttribute != elementInAttributeScope.lastAttr);
				}
			}
			return null;
		}

		public override string LookupNamespace(string prefix)
		{
			if (!IsInteractive)
			{
				return null;
			}
			if (prefix == null)
			{
				return null;
			}
			XElement elementInScope = GetElementInScope();
			if (elementInScope != null)
			{
				XNamespace xNamespace = ((prefix.Length == 0) ? elementInScope.GetDefaultNamespace() : elementInScope.GetNamespaceOfPrefix(prefix));
				if (xNamespace != null)
				{
					return _nameTable.Add(xNamespace.NamespaceName);
				}
			}
			return null;
		}

		public override bool MoveToAttribute(string name)
		{
			if (!IsInteractive)
			{
				return false;
			}
			XElement elementInAttributeScope = GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				GetNameInAttributeScope(name, elementInAttributeScope, out var localName, out var namespaceName);
				XAttribute xAttribute = elementInAttributeScope.lastAttr;
				if (xAttribute != null)
				{
					do
					{
						xAttribute = xAttribute.next;
						if (xAttribute.Name.LocalName == localName && xAttribute.Name.NamespaceName == namespaceName)
						{
							if (_omitDuplicateNamespaces && IsDuplicateNamespaceAttribute(xAttribute))
							{
								return false;
							}
							_source = xAttribute;
							_parent = null;
							return true;
						}
					}
					while (xAttribute != elementInAttributeScope.lastAttr);
				}
			}
			return false;
		}

		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			if (!IsInteractive)
			{
				return false;
			}
			XElement elementInAttributeScope = GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				if (localName == "xmlns")
				{
					if (namespaceName != null && namespaceName.Length == 0)
					{
						return false;
					}
					if (namespaceName == "http://www.w3.org/2000/xmlns/")
					{
						namespaceName = string.Empty;
					}
				}
				XAttribute xAttribute = elementInAttributeScope.lastAttr;
				if (xAttribute != null)
				{
					do
					{
						xAttribute = xAttribute.next;
						if (xAttribute.Name.LocalName == localName && xAttribute.Name.NamespaceName == namespaceName)
						{
							if (_omitDuplicateNamespaces && IsDuplicateNamespaceAttribute(xAttribute))
							{
								return false;
							}
							_source = xAttribute;
							_parent = null;
							return true;
						}
					}
					while (xAttribute != elementInAttributeScope.lastAttr);
				}
			}
			return false;
		}

		public override void MoveToAttribute(int index)
		{
			if (!IsInteractive)
			{
				return;
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			XElement elementInAttributeScope = GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				XAttribute xAttribute = elementInAttributeScope.lastAttr;
				if (xAttribute != null)
				{
					do
					{
						xAttribute = xAttribute.next;
						if ((!_omitDuplicateNamespaces || !IsDuplicateNamespaceAttribute(xAttribute)) && index-- == 0)
						{
							_source = xAttribute;
							_parent = null;
							return;
						}
					}
					while (xAttribute != elementInAttributeScope.lastAttr);
				}
			}
			throw new ArgumentOutOfRangeException("index");
		}

		public override bool MoveToElement()
		{
			if (!IsInteractive)
			{
				return false;
			}
			XAttribute xAttribute = _source as XAttribute;
			if (xAttribute == null)
			{
				xAttribute = _parent as XAttribute;
			}
			if (xAttribute != null && xAttribute.parent != null)
			{
				_source = xAttribute.parent;
				_parent = null;
				return true;
			}
			return false;
		}

		public override bool MoveToFirstAttribute()
		{
			if (!IsInteractive)
			{
				return false;
			}
			XElement elementInAttributeScope = GetElementInAttributeScope();
			if (elementInAttributeScope != null && elementInAttributeScope.lastAttr != null)
			{
				if (_omitDuplicateNamespaces)
				{
					object firstNonDuplicateNamespaceAttribute = GetFirstNonDuplicateNamespaceAttribute(elementInAttributeScope.lastAttr.next);
					if (firstNonDuplicateNamespaceAttribute == null)
					{
						return false;
					}
					_source = firstNonDuplicateNamespaceAttribute;
				}
				else
				{
					_source = elementInAttributeScope.lastAttr.next;
				}
				return true;
			}
			return false;
		}

		public override bool MoveToNextAttribute()
		{
			if (!IsInteractive)
			{
				return false;
			}
			if (_source is XElement xElement)
			{
				if (IsEndElement)
				{
					return false;
				}
				if (xElement.lastAttr != null)
				{
					if (_omitDuplicateNamespaces)
					{
						object firstNonDuplicateNamespaceAttribute = GetFirstNonDuplicateNamespaceAttribute(xElement.lastAttr.next);
						if (firstNonDuplicateNamespaceAttribute == null)
						{
							return false;
						}
						_source = firstNonDuplicateNamespaceAttribute;
					}
					else
					{
						_source = xElement.lastAttr.next;
					}
					return true;
				}
				return false;
			}
			XAttribute xAttribute = _source as XAttribute;
			if (xAttribute == null)
			{
				xAttribute = _parent as XAttribute;
			}
			if (xAttribute != null && xAttribute.parent != null && ((XElement)xAttribute.parent).lastAttr != xAttribute)
			{
				if (_omitDuplicateNamespaces)
				{
					object firstNonDuplicateNamespaceAttribute2 = GetFirstNonDuplicateNamespaceAttribute(xAttribute.next);
					if (firstNonDuplicateNamespaceAttribute2 == null)
					{
						return false;
					}
					_source = firstNonDuplicateNamespaceAttribute2;
				}
				else
				{
					_source = xAttribute.next;
				}
				_parent = null;
				return true;
			}
			return false;
		}

		public override bool Read()
		{
			switch (_state)
			{
			case ReadState.Initial:
				_state = ReadState.Interactive;
				if (_source is XDocument d)
				{
					return ReadIntoDocument(d);
				}
				return true;
			case ReadState.Interactive:
				return Read(skipContent: false);
			default:
				return false;
			}
		}

		public override bool ReadAttributeValue()
		{
			if (!IsInteractive)
			{
				return false;
			}
			if (_source is XAttribute a)
			{
				return ReadIntoAttribute(a);
			}
			return false;
		}

		public override bool ReadToDescendant(string localName, string namespaceName)
		{
			if (!IsInteractive)
			{
				return false;
			}
			MoveToElement();
			if (_source is XElement { IsEmpty: false } xElement)
			{
				if (IsEndElement)
				{
					return false;
				}
				foreach (XElement item in xElement.Descendants())
				{
					if (item.Name.LocalName == localName && item.Name.NamespaceName == namespaceName)
					{
						_source = item;
						return true;
					}
				}
				IsEndElement = true;
			}
			return false;
		}

		public override bool ReadToFollowing(string localName, string namespaceName)
		{
			while (Read())
			{
				if (_source is XElement xElement && !IsEndElement && xElement.Name.LocalName == localName && xElement.Name.NamespaceName == namespaceName)
				{
					return true;
				}
			}
			return false;
		}

		public override bool ReadToNextSibling(string localName, string namespaceName)
		{
			if (!IsInteractive)
			{
				return false;
			}
			MoveToElement();
			if (_source != _root)
			{
				if (_source is XNode xNode)
				{
					foreach (XElement item in xNode.ElementsAfterSelf())
					{
						if (item.Name.LocalName == localName && item.Name.NamespaceName == namespaceName)
						{
							_source = item;
							IsEndElement = false;
							return true;
						}
					}
					if (xNode.parent is XElement)
					{
						_source = xNode.parent;
						IsEndElement = true;
						return false;
					}
				}
				else if (_parent is XElement)
				{
					_source = _parent;
					_parent = null;
					IsEndElement = true;
					return false;
				}
			}
			return ReadToEnd();
		}

		public override void ResolveEntity()
		{
		}

		public override void Skip()
		{
			if (IsInteractive)
			{
				Read(skipContent: true);
			}
		}

		bool IXmlLineInfo.HasLineInfo()
		{
			if (IsEndElement)
			{
				if (_source is XElement xElement)
				{
					return xElement.Annotation<LineInfoEndElementAnnotation>() != null;
				}
			}
			else if (_source is IXmlLineInfo xmlLineInfo)
			{
				return xmlLineInfo.HasLineInfo();
			}
			return false;
		}

		private static XmlNameTable CreateNameTable()
		{
			NameTable nameTable = new NameTable();
			nameTable.Add(string.Empty);
			nameTable.Add("http://www.w3.org/2000/xmlns/");
			nameTable.Add("http://www.w3.org/XML/1998/namespace");
			return nameTable;
		}

		private XElement GetElementInAttributeScope()
		{
			if (_source is XElement result)
			{
				if (IsEndElement)
				{
					return null;
				}
				return result;
			}
			if (_source is XAttribute xAttribute)
			{
				return (XElement)xAttribute.parent;
			}
			if (_parent is XAttribute xAttribute2)
			{
				return (XElement)xAttribute2.parent;
			}
			return null;
		}

		private XElement GetElementInScope()
		{
			if (_source is XElement result)
			{
				return result;
			}
			if (_source is XNode xNode)
			{
				return xNode.parent as XElement;
			}
			if (_source is XAttribute xAttribute)
			{
				return (XElement)xAttribute.parent;
			}
			if (_parent is XElement result2)
			{
				return result2;
			}
			if (_parent is XAttribute xAttribute2)
			{
				return (XElement)xAttribute2.parent;
			}
			return null;
		}

		private static void GetNameInAttributeScope(string qualifiedName, XElement e, out string localName, out string namespaceName)
		{
			if (!string.IsNullOrEmpty(qualifiedName))
			{
				int num = qualifiedName.IndexOf(':');
				if (num != 0 && num != qualifiedName.Length - 1)
				{
					if (num == -1)
					{
						localName = qualifiedName;
						namespaceName = string.Empty;
						return;
					}
					XNamespace namespaceOfPrefix = e.GetNamespaceOfPrefix(qualifiedName.Substring(0, num));
					if (namespaceOfPrefix != null)
					{
						localName = qualifiedName.Substring(num + 1, qualifiedName.Length - num - 1);
						namespaceName = namespaceOfPrefix.NamespaceName;
						return;
					}
				}
			}
			localName = null;
			namespaceName = null;
		}

		private bool Read(bool skipContent)
		{
			if (_source is XElement xElement)
			{
				if (xElement.IsEmpty || IsEndElement || skipContent)
				{
					return ReadOverNode(xElement);
				}
				return ReadIntoElement(xElement);
			}
			if (_source is XNode n)
			{
				return ReadOverNode(n);
			}
			if (_source is XAttribute a)
			{
				return ReadOverAttribute(a, skipContent);
			}
			return ReadOverText(skipContent);
		}

		private bool ReadIntoDocument(XDocument d)
		{
			if (d.content is XNode xNode)
			{
				_source = xNode.next;
				return true;
			}
			if (d.content is string { Length: >0 } text)
			{
				_source = text;
				_parent = d;
				return true;
			}
			return ReadToEnd();
		}

		private bool ReadIntoElement(XElement e)
		{
			if (e.content is XNode xNode)
			{
				_source = xNode.next;
				return true;
			}
			if (e.content is string text)
			{
				if (text.Length > 0)
				{
					_source = text;
					_parent = e;
				}
				else
				{
					_source = e;
					IsEndElement = true;
				}
				return true;
			}
			return ReadToEnd();
		}

		private bool ReadIntoAttribute(XAttribute a)
		{
			_source = a.value;
			_parent = a;
			return true;
		}

		private bool ReadOverAttribute(XAttribute a, bool skipContent)
		{
			XElement xElement = (XElement)a.parent;
			if (xElement != null)
			{
				if (xElement.IsEmpty || skipContent)
				{
					return ReadOverNode(xElement);
				}
				return ReadIntoElement(xElement);
			}
			return ReadToEnd();
		}

		private bool ReadOverNode(XNode n)
		{
			if (n == _root)
			{
				return ReadToEnd();
			}
			XNode next = n.next;
			if (next == null || next == n || n == n.parent.content)
			{
				if (n.parent == null || (n.parent.parent == null && n.parent is XDocument))
				{
					return ReadToEnd();
				}
				_source = n.parent;
				IsEndElement = true;
			}
			else
			{
				_source = next;
				IsEndElement = false;
			}
			return true;
		}

		private bool ReadOverText(bool skipContent)
		{
			if (_parent is XElement)
			{
				_source = _parent;
				_parent = null;
				IsEndElement = true;
				return true;
			}
			if (_parent is XAttribute a)
			{
				_parent = null;
				return ReadOverAttribute(a, skipContent);
			}
			return ReadToEnd();
		}

		private bool ReadToEnd()
		{
			_state = ReadState.EndOfFile;
			return false;
		}

		private bool IsDuplicateNamespaceAttribute(XAttribute candidateAttribute)
		{
			if (!candidateAttribute.IsNamespaceDeclaration)
			{
				return false;
			}
			return IsDuplicateNamespaceAttributeInner(candidateAttribute);
		}

		private bool IsDuplicateNamespaceAttributeInner(XAttribute candidateAttribute)
		{
			if (candidateAttribute.Name.LocalName == "xml")
			{
				return true;
			}
			XElement xElement = candidateAttribute.parent as XElement;
			if (xElement == _root || xElement == null)
			{
				return false;
			}
			for (xElement = xElement.parent as XElement; xElement != null; xElement = xElement.parent as XElement)
			{
				XAttribute xAttribute = xElement.lastAttr;
				if (xAttribute != null)
				{
					do
					{
						if (xAttribute.name == candidateAttribute.name)
						{
							if (xAttribute.Value == candidateAttribute.Value)
							{
								return true;
							}
							return false;
						}
						xAttribute = xAttribute.next;
					}
					while (xAttribute != xElement.lastAttr);
				}
				if (xElement == _root)
				{
					return false;
				}
			}
			return false;
		}

		private XAttribute GetFirstNonDuplicateNamespaceAttribute(XAttribute candidate)
		{
			if (!IsDuplicateNamespaceAttribute(candidate))
			{
				return candidate;
			}
			if (candidate.parent is XElement xElement && candidate != xElement.lastAttr)
			{
				do
				{
					candidate = candidate.next;
					if (!IsDuplicateNamespaceAttribute(candidate))
					{
						return candidate;
					}
				}
				while (candidate != xElement.lastAttr);
			}
			return null;
		}
	}
	/// <summary>Represents a node or an attribute in an XML tree.</summary>
	public abstract class XObject : IXmlLineInfo
	{
		internal XContainer parent;

		internal object annotations;

		/// <summary>Gets the base URI for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the base URI for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		public string BaseUri
		{
			get
			{
				XObject xObject = this;
				while (true)
				{
					if (xObject != null && xObject.annotations == null)
					{
						xObject = xObject.parent;
						continue;
					}
					if (xObject == null)
					{
						break;
					}
					BaseUriAnnotation baseUriAnnotation = xObject.Annotation<BaseUriAnnotation>();
					if (baseUriAnnotation != null)
					{
						return baseUriAnnotation.baseUri;
					}
					xObject = xObject.parent;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Linq.XDocument" /> for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XDocument" /> for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		public XDocument Document
		{
			get
			{
				XObject xObject = this;
				while (xObject.parent != null)
				{
					xObject = xObject.parent;
				}
				return xObject as XDocument;
			}
		}

		/// <summary>Gets the node type for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>The node type for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		public abstract XmlNodeType NodeType { get; }

		/// <summary>Gets the parent <see cref="T:System.Xml.Linq.XElement" /> of this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>The parent <see cref="T:System.Xml.Linq.XElement" /> of this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		public XElement Parent => parent as XElement;

		/// <summary>Gets the line number that the underlying <see cref="T:System.Xml.XmlReader" /> reported for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the line number reported by the <see cref="T:System.Xml.XmlReader" /> for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		int IXmlLineInfo.LineNumber => Annotation<LineInfoAnnotation>()?.lineNumber ?? 0;

		/// <summary>Gets the line position that the underlying <see cref="T:System.Xml.XmlReader" /> reported for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the line position reported by the <see cref="T:System.Xml.XmlReader" /> for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		int IXmlLineInfo.LinePosition => Annotation<LineInfoAnnotation>()?.linePosition ?? 0;

		internal bool HasBaseUri => Annotation<BaseUriAnnotation>() != null;

		/// <summary>Raised when this <see cref="T:System.Xml.Linq.XObject" /> or any of its descendants have changed.</summary>
		public event EventHandler<XObjectChangeEventArgs> Changed
		{
			add
			{
				if (value != null)
				{
					XObjectChangeAnnotation xObjectChangeAnnotation = Annotation<XObjectChangeAnnotation>();
					if (xObjectChangeAnnotation == null)
					{
						xObjectChangeAnnotation = new XObjectChangeAnnotation();
						AddAnnotation(xObjectChangeAnnotation);
					}
					XObjectChangeAnnotation xObjectChangeAnnotation2 = xObjectChangeAnnotation;
					xObjectChangeAnnotation2.changed = (EventHandler<XObjectChangeEventArgs>)Delegate.Combine(xObjectChangeAnnotation2.changed, value);
				}
			}
			remove
			{
				if (value == null)
				{
					return;
				}
				XObjectChangeAnnotation xObjectChangeAnnotation = Annotation<XObjectChangeAnnotation>();
				if (xObjectChangeAnnotation != null)
				{
					xObjectChangeAnnotation.changed = (EventHandler<XObjectChangeEventArgs>)Delegate.Remove(xObjectChangeAnnotation.changed, value);
					if (xObjectChangeAnnotation.changing == null && xObjectChangeAnnotation.changed == null)
					{
						RemoveAnnotations<XObjectChangeAnnotation>();
					}
				}
			}
		}

		/// <summary>Raised when this <see cref="T:System.Xml.Linq.XObject" /> or any of its descendants are about to change.</summary>
		public event EventHandler<XObjectChangeEventArgs> Changing
		{
			add
			{
				if (value != null)
				{
					XObjectChangeAnnotation xObjectChangeAnnotation = Annotation<XObjectChangeAnnotation>();
					if (xObjectChangeAnnotation == null)
					{
						xObjectChangeAnnotation = new XObjectChangeAnnotation();
						AddAnnotation(xObjectChangeAnnotation);
					}
					XObjectChangeAnnotation xObjectChangeAnnotation2 = xObjectChangeAnnotation;
					xObjectChangeAnnotation2.changing = (EventHandler<XObjectChangeEventArgs>)Delegate.Combine(xObjectChangeAnnotation2.changing, value);
				}
			}
			remove
			{
				if (value == null)
				{
					return;
				}
				XObjectChangeAnnotation xObjectChangeAnnotation = Annotation<XObjectChangeAnnotation>();
				if (xObjectChangeAnnotation != null)
				{
					xObjectChangeAnnotation.changing = (EventHandler<XObjectChangeEventArgs>)Delegate.Remove(xObjectChangeAnnotation.changing, value);
					if (xObjectChangeAnnotation.changing == null && xObjectChangeAnnotation.changed == null)
					{
						RemoveAnnotations<XObjectChangeAnnotation>();
					}
				}
			}
		}

		internal XObject()
		{
		}

		/// <summary>Adds an object to the annotation list of this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <param name="annotation">An object that contains the annotation to add.</param>
		public void AddAnnotation(object annotation)
		{
			if (annotation == null)
			{
				throw new ArgumentNullException("annotation");
			}
			if (annotations == null)
			{
				annotations = ((!(annotation is object[])) ? annotation : new object[1] { annotation });
				return;
			}
			object[] array = annotations as object[];
			if (array == null)
			{
				annotations = new object[2] { annotations, annotation };
				return;
			}
			int i;
			for (i = 0; i < array.Length && array[i] != null; i++)
			{
			}
			if (i == array.Length)
			{
				Array.Resize(ref array, i * 2);
				annotations = array;
			}
			array[i] = annotation;
		}

		/// <summary>Gets the first annotation object of the specified type from this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <param name="type">The type of the annotation to retrieve.</param>
		/// <returns>The <see cref="T:System.Object" /> that contains the first annotation object that matches the specified type, or <see langword="null" /> if no annotation is of the specified type.</returns>
		public object Annotation(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (annotations != null)
			{
				if (!(annotations is object[] array))
				{
					if (XHelper.IsInstanceOfType(annotations, type))
					{
						return annotations;
					}
				}
				else
				{
					foreach (object obj in array)
					{
						if (obj == null)
						{
							break;
						}
						if (XHelper.IsInstanceOfType(obj, type))
						{
							return obj;
						}
					}
				}
			}
			return null;
		}

		private object AnnotationForSealedType(Type type)
		{
			if (annotations != null)
			{
				if (!(annotations is object[] array))
				{
					if (annotations.GetType() == type)
					{
						return annotations;
					}
				}
				else
				{
					foreach (object obj in array)
					{
						if (obj == null)
						{
							break;
						}
						if (obj.GetType() == type)
						{
							return obj;
						}
					}
				}
			}
			return null;
		}

		/// <summary>Gets the first annotation object of the specified type from this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <typeparam name="T">The type of the annotation to retrieve.</typeparam>
		/// <returns>The first annotation object that matches the specified type, or <see langword="null" /> if no annotation is of the specified type.</returns>
		public T Annotation<T>() where T : class
		{
			if (annotations != null)
			{
				if (!(annotations is object[] array))
				{
					return annotations as T;
				}
				foreach (object obj in array)
				{
					if (obj == null)
					{
						break;
					}
					if (obj is T result)
					{
						return result;
					}
				}
			}
			return null;
		}

		/// <summary>Gets a collection of annotations of the specified type for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <param name="type">The type of the annotations to retrieve.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Object" /> that contains the annotations that match the specified type for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		public IEnumerable<object> Annotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return AnnotationsIterator(type);
		}

		private IEnumerable<object> AnnotationsIterator(Type type)
		{
			if (annotations == null)
			{
				yield break;
			}
			if (!(annotations is object[] a))
			{
				if (XHelper.IsInstanceOfType(annotations, type))
				{
					yield return annotations;
				}
				yield break;
			}
			foreach (object obj in a)
			{
				if (obj != null)
				{
					if (XHelper.IsInstanceOfType(obj, type))
					{
						yield return obj;
					}
					continue;
				}
				break;
			}
		}

		/// <summary>Gets a collection of annotations of the specified type for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <typeparam name="T">The type of the annotations to retrieve.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the annotations for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		public IEnumerable<T> Annotations<T>() where T : class
		{
			if (annotations == null)
			{
				yield break;
			}
			if (!(annotations is object[] a))
			{
				if (annotations is T val)
				{
					yield return val;
				}
				yield break;
			}
			foreach (object obj in a)
			{
				if (obj != null)
				{
					if (obj is T val2)
					{
						yield return val2;
					}
					continue;
				}
				break;
			}
		}

		/// <summary>Removes the annotations of the specified type from this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <param name="type">The type of annotations to remove.</param>
		public void RemoveAnnotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (annotations == null)
			{
				return;
			}
			if (!(annotations is object[] array))
			{
				if (XHelper.IsInstanceOfType(annotations, type))
				{
					annotations = null;
				}
				return;
			}
			int i = 0;
			int num = 0;
			for (; i < array.Length; i++)
			{
				object obj = array[i];
				if (obj == null)
				{
					break;
				}
				if (!XHelper.IsInstanceOfType(obj, type))
				{
					array[num++] = obj;
				}
			}
			if (num == 0)
			{
				annotations = null;
				return;
			}
			while (num < i)
			{
				array[num++] = null;
			}
		}

		/// <summary>Removes the annotations of the specified type from this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <typeparam name="T">The type of annotations to remove.</typeparam>
		public void RemoveAnnotations<T>() where T : class
		{
			if (annotations == null)
			{
				return;
			}
			if (!(annotations is object[] array))
			{
				if (annotations is T)
				{
					annotations = null;
				}
				return;
			}
			int i = 0;
			int num = 0;
			for (; i < array.Length; i++)
			{
				object obj = array[i];
				if (obj == null)
				{
					break;
				}
				if (!(obj is T))
				{
					array[num++] = obj;
				}
			}
			if (num == 0)
			{
				annotations = null;
				return;
			}
			while (num < i)
			{
				array[num++] = null;
			}
		}

		/// <summary>Gets a value indicating whether or not this <see cref="T:System.Xml.Linq.XObject" /> has line information.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Xml.Linq.XObject" /> has line information; otherwise, <see langword="false" />.</returns>
		bool IXmlLineInfo.HasLineInfo()
		{
			return Annotation<LineInfoAnnotation>() != null;
		}

		internal bool NotifyChanged(object sender, XObjectChangeEventArgs e)
		{
			bool result = false;
			XObject xObject = this;
			while (true)
			{
				if (xObject != null && xObject.annotations == null)
				{
					xObject = xObject.parent;
					continue;
				}
				if (xObject == null)
				{
					break;
				}
				XObjectChangeAnnotation xObjectChangeAnnotation = xObject.Annotation<XObjectChangeAnnotation>();
				if (xObjectChangeAnnotation != null)
				{
					result = true;
					if (xObjectChangeAnnotation.changed != null)
					{
						xObjectChangeAnnotation.changed(sender, e);
					}
				}
				xObject = xObject.parent;
			}
			return result;
		}

		internal bool NotifyChanging(object sender, XObjectChangeEventArgs e)
		{
			bool result = false;
			XObject xObject = this;
			while (true)
			{
				if (xObject != null && xObject.annotations == null)
				{
					xObject = xObject.parent;
					continue;
				}
				if (xObject == null)
				{
					break;
				}
				XObjectChangeAnnotation xObjectChangeAnnotation = xObject.Annotation<XObjectChangeAnnotation>();
				if (xObjectChangeAnnotation != null)
				{
					result = true;
					if (xObjectChangeAnnotation.changing != null)
					{
						xObjectChangeAnnotation.changing(sender, e);
					}
				}
				xObject = xObject.parent;
			}
			return result;
		}

		internal void SetBaseUri(string baseUri)
		{
			AddAnnotation(new BaseUriAnnotation(baseUri));
		}

		internal void SetLineInfo(int lineNumber, int linePosition)
		{
			AddAnnotation(new LineInfoAnnotation(lineNumber, linePosition));
		}

		internal bool SkipNotify()
		{
			XObject xObject = this;
			while (true)
			{
				if (xObject != null && xObject.annotations == null)
				{
					xObject = xObject.parent;
					continue;
				}
				if (xObject == null)
				{
					return true;
				}
				if (xObject.Annotation<XObjectChangeAnnotation>() != null)
				{
					break;
				}
				xObject = xObject.parent;
			}
			return false;
		}

		internal SaveOptions GetSaveOptionsFromAnnotations()
		{
			XObject xObject = this;
			object obj;
			while (true)
			{
				if (xObject != null && xObject.annotations == null)
				{
					xObject = xObject.parent;
					continue;
				}
				if (xObject == null)
				{
					return SaveOptions.None;
				}
				obj = xObject.AnnotationForSealedType(typeof(SaveOptions));
				if (obj != null)
				{
					break;
				}
				xObject = xObject.parent;
			}
			return (SaveOptions)obj;
		}
	}
	internal class XObjectChangeAnnotation
	{
		internal EventHandler<XObjectChangeEventArgs> changing;

		internal EventHandler<XObjectChangeEventArgs> changed;
	}
	/// <summary>Provides data for the <see cref="E:System.Xml.Linq.XObject.Changing" /> and <see cref="E:System.Xml.Linq.XObject.Changed" /> events.</summary>
	public class XObjectChangeEventArgs : EventArgs
	{
		private XObjectChange _objectChange;

		/// <summary>Event argument for an <see cref="F:System.Xml.Linq.XObjectChange.Add" /> change event.</summary>
		public static readonly XObjectChangeEventArgs Add = new XObjectChangeEventArgs(XObjectChange.Add);

		/// <summary>Event argument for a <see cref="F:System.Xml.Linq.XObjectChange.Remove" /> change event.</summary>
		public static readonly XObjectChangeEventArgs Remove = new XObjectChangeEventArgs(XObjectChange.Remove);

		/// <summary>Event argument for a <see cref="F:System.Xml.Linq.XObjectChange.Name" /> change event.</summary>
		public static readonly XObjectChangeEventArgs Name = new XObjectChangeEventArgs(XObjectChange.Name);

		/// <summary>Event argument for a <see cref="F:System.Xml.Linq.XObjectChange.Value" /> change event.</summary>
		public static readonly XObjectChangeEventArgs Value = new XObjectChangeEventArgs(XObjectChange.Value);

		/// <summary>Gets the type of change.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XObjectChange" /> that contains the type of change.</returns>
		public XObjectChange ObjectChange => _objectChange;

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XObjectChangeEventArgs" /> class.</summary>
		/// <param name="objectChange">An <see cref="T:System.Xml.Linq.XObjectChange" /> that contains the event arguments for LINQ to XML events.</param>
		public XObjectChangeEventArgs(XObjectChange objectChange)
		{
			_objectChange = objectChange;
		}
	}
	/// <summary>Represents an XML processing instruction.</summary>
	public class XProcessingInstruction : XNode
	{
		internal string target;

		internal string data;

		/// <summary>Gets or sets the string value of this processing instruction.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the string value of this processing instruction.</returns>
		/// <exception cref="T:System.ArgumentNullException">The string <paramref name="value" /> is <see langword="null" />.</exception>
		public string Data
		{
			get
			{
				return data;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				bool num = NotifyChanging(this, XObjectChangeEventArgs.Value);
				data = value;
				if (num)
				{
					NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XProcessingInstruction" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.ProcessingInstruction" />.</returns>
		public override XmlNodeType NodeType => XmlNodeType.ProcessingInstruction;

		/// <summary>Gets or sets a string containing the target application for this processing instruction.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the target application for this processing instruction.</returns>
		/// <exception cref="T:System.ArgumentNullException">The string <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> does not follow the constraints of an XML name.</exception>
		public string Target
		{
			get
			{
				return target;
			}
			set
			{
				ValidateName(value);
				bool num = NotifyChanging(this, XObjectChangeEventArgs.Name);
				target = value;
				if (num)
				{
					NotifyChanged(this, XObjectChangeEventArgs.Name);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XProcessingInstruction" /> class.</summary>
		/// <param name="target">A <see cref="T:System.String" /> containing the target application for this <see cref="T:System.Xml.Linq.XProcessingInstruction" />.</param>
		/// <param name="data">The string data for this <see cref="T:System.Xml.Linq.XProcessingInstruction" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="target" /> or <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> does not follow the constraints of an XML name.</exception>
		public XProcessingInstruction(string target, string data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			ValidateName(target);
			this.target = target;
			this.data = data;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XProcessingInstruction" /> class.</summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XProcessingInstruction" /> node to copy from.</param>
		public XProcessingInstruction(XProcessingInstruction other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			target = other.target;
			data = other.data;
		}

		internal XProcessingInstruction(XmlReader r)
		{
			target = r.Name;
			data = r.Value;
			r.Read();
		}

		/// <summary>Writes this processing instruction to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to write this processing instruction to.</param>
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteProcessingInstruction(target, data);
		}

		public override Task WriteToAsync(XmlWriter writer, CancellationToken cancellationToken)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			return writer.WriteProcessingInstructionAsync(target, data);
		}

		internal override XNode CloneNode()
		{
			return new XProcessingInstruction(this);
		}

		internal override bool DeepEquals(XNode node)
		{
			if (node is XProcessingInstruction xProcessingInstruction && target == xProcessingInstruction.target)
			{
				return data == xProcessingInstruction.data;
			}
			return false;
		}

		internal override int GetDeepHashCode()
		{
			return target.GetHashCode() ^ data.GetHashCode();
		}

		private static void ValidateName(string name)
		{
			XmlConvert.VerifyNCName(name);
			if (string.Equals(name, "xml", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(global::SR.Format("'{0}' is an invalid name for a processing instruction.", name));
			}
		}
	}
	/// <summary>Represents elements in an XML tree that supports deferred streaming output.</summary>
	public class XStreamingElement
	{
		internal XName name;

		internal object content;

		/// <summary>Gets or sets the name of this streaming element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> that contains the name of this streaming element.</returns>
		public XName Name
		{
			get
			{
				return name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				name = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class from the specified <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the name of the element.</param>
		public XStreamingElement(XName name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XStreamingElement" /> class with the specified name and content.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the element name.</param>
		/// <param name="content">The contents of the element.</param>
		public XStreamingElement(XName name, object content)
			: this(name)
		{
			this.content = ((!(content is List<object>)) ? content : new object[1] { content });
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XStreamingElement" /> class with the specified name and content.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the element name.</param>
		/// <param name="content">The contents of the element.</param>
		public XStreamingElement(XName name, params object[] content)
			: this(name)
		{
			this.content = content;
		}

		/// <summary>Adds the specified content as children to this <see cref="T:System.Xml.Linq.XStreamingElement" />.</summary>
		/// <param name="content">Content to be added to the streaming element.</param>
		public void Add(object content)
		{
			if (content == null)
			{
				return;
			}
			List<object> list = this.content as List<object>;
			if (list == null)
			{
				list = new List<object>();
				if (this.content != null)
				{
					list.Add(this.content);
				}
				this.content = list;
			}
			list.Add(content);
		}

		/// <summary>Adds the specified content as children to this <see cref="T:System.Xml.Linq.XStreamingElement" />.</summary>
		/// <param name="content">Content to be added to the streaming element.</param>
		public void Add(params object[] content)
		{
			Add((object)content);
		}

		/// <summary>Outputs this <see cref="T:System.Xml.Linq.XStreamingElement" /> to the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">The stream to output this <see cref="T:System.Xml.Linq.XDocument" /> to.</param>
		public void Save(Stream stream)
		{
			Save(stream, SaveOptions.None);
		}

		/// <summary>Outputs this <see cref="T:System.Xml.Linq.XStreamingElement" /> to the specified <see cref="T:System.IO.Stream" />, optionally specifying formatting behavior.</summary>
		/// <param name="stream">The stream to output this <see cref="T:System.Xml.Linq.XDocument" /> to.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> object that specifies formatting behavior.</param>
		public void Save(Stream stream, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using XmlWriter writer = XmlWriter.Create(stream, xmlWriterSettings);
			Save(writer);
		}

		/// <summary>Serialize this streaming element to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="textWriter">A <see cref="T:System.IO.TextWriter" /> that the <see cref="T:System.Xml.Linq.XStreamingElement" /> will be written to.</param>
		public void Save(TextWriter textWriter)
		{
			Save(textWriter, SaveOptions.None);
		}

		/// <summary>Serialize this streaming element to a <see cref="T:System.IO.TextWriter" />, optionally disabling formatting.</summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> to output the XML to.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		public void Save(TextWriter textWriter, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using XmlWriter writer = XmlWriter.Create(textWriter, xmlWriterSettings);
			Save(writer);
		}

		/// <summary>Serialize this streaming element to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" /> that the <see cref="T:System.Xml.Linq.XElement" /> will be written to.</param>
		public void Save(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteStartDocument();
			WriteTo(writer);
			writer.WriteEndDocument();
		}

		/// <summary>Serialize this streaming element to a file.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the name of the file.</param>
		public void Save(string fileName)
		{
			Save(fileName, SaveOptions.None);
		}

		/// <summary>Serialize this streaming element to a file, optionally disabling formatting.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the name of the file.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> object that specifies formatting behavior.</param>
		public void Save(string fileName, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using XmlWriter writer = XmlWriter.Create(fileName, xmlWriterSettings);
			Save(writer);
		}

		/// <summary>Returns the formatted (indented) XML for this streaming element.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the indented XML.</returns>
		public override string ToString()
		{
			return GetXmlString(SaveOptions.None);
		}

		/// <summary>Returns the XML for this streaming element, optionally disabling formatting.</summary>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		/// <returns>A <see cref="T:System.String" /> containing the XML.</returns>
		public string ToString(SaveOptions options)
		{
			return GetXmlString(options);
		}

		/// <summary>Writes this streaming element to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		public void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			new StreamingElementWriter(writer).WriteStreamingElement(this);
		}

		private string GetXmlString(SaveOptions o)
		{
			using StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.OmitXmlDeclaration = true;
			if ((o & SaveOptions.DisableFormatting) == 0)
			{
				xmlWriterSettings.Indent = true;
			}
			if ((o & SaveOptions.OmitDuplicateNamespaces) != SaveOptions.None)
			{
				xmlWriterSettings.NamespaceHandling |= NamespaceHandling.OmitDuplicates;
			}
			using (XmlWriter writer = XmlWriter.Create(stringWriter, xmlWriterSettings))
			{
				WriteTo(writer);
			}
			return stringWriter.ToString();
		}
	}
	/// <summary>Represents a text node.</summary>
	public class XText : XNode
	{
		internal string text;

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XText" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Text" />.</returns>
		public override XmlNodeType NodeType => XmlNodeType.Text;

		/// <summary>Gets or sets the value of this node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value of this node.</returns>
		public string Value
		{
			get
			{
				return text;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				bool num = NotifyChanging(this, XObjectChangeEventArgs.Value);
				text = value;
				if (num)
				{
					NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XText" /> class.</summary>
		/// <param name="value">The <see cref="T:System.String" /> that contains the value of the <see cref="T:System.Xml.Linq.XText" /> node.</param>
		public XText(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			text = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XText" /> class from another <see cref="T:System.Xml.Linq.XText" /> object.</summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XText" /> node to copy from.</param>
		public XText(XText other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			text = other.text;
		}

		internal XText(XmlReader r)
		{
			text = r.Value;
			r.Read();
		}

		/// <summary>Writes this node to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (parent is XDocument)
			{
				writer.WriteWhitespace(text);
			}
			else
			{
				writer.WriteString(text);
			}
		}

		public override Task WriteToAsync(XmlWriter writer, CancellationToken cancellationToken)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (!(parent is XDocument))
			{
				return writer.WriteStringAsync(text);
			}
			return writer.WriteWhitespaceAsync(text);
		}

		internal override void AppendText(StringBuilder sb)
		{
			sb.Append(text);
		}

		internal override XNode CloneNode()
		{
			return new XText(this);
		}

		internal override bool DeepEquals(XNode node)
		{
			if (node != null && NodeType == node.NodeType)
			{
				return text == ((XText)node).text;
			}
			return false;
		}

		internal override int GetDeepHashCode()
		{
			return text.GetHashCode();
		}
	}
}
namespace System.Collections.Generic
{
	internal struct ArrayBuilder<T>
	{
		private const int DefaultCapacity = 4;

		private const int MaxCoreClrArrayLength = 2146435071;

		private T[] _array;

		private int _count;

		public int Capacity
		{
			get
			{
				T[] array = _array;
				if (array == null)
				{
					return 0;
				}
				return array.Length;
			}
		}

		public int Count => _count;

		public T this[int index]
		{
			get
			{
				return _array[index];
			}
			set
			{
				_array[index] = value;
			}
		}

		public ArrayBuilder(int capacity)
		{
			this = default(System.Collections.Generic.ArrayBuilder<T>);
			if (capacity > 0)
			{
				_array = new T[capacity];
			}
		}

		public void Add(T item)
		{
			if (_count == Capacity)
			{
				EnsureCapacity(_count + 1);
			}
			UncheckedAdd(item);
		}

		public T First()
		{
			return _array[0];
		}

		public T Last()
		{
			return _array[_count - 1];
		}

		public T[] ToArray()
		{
			if (_count == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = _array;
			if (_count < array.Length)
			{
				array = new T[_count];
				Array.Copy(_array, 0, array, 0, _count);
			}
			return array;
		}

		public void UncheckedAdd(T item)
		{
			_array[_count++] = item;
		}

		private void EnsureCapacity(int minimum)
		{
			int capacity = Capacity;
			int num = ((capacity == 0) ? 4 : (2 * capacity));
			if ((uint)num > 2146435071u)
			{
				num = Math.Max(capacity + 1, 2146435071);
			}
			num = Math.Max(num, minimum);
			T[] array = new T[num];
			if (_count > 0)
			{
				Array.Copy(_array, 0, array, 0, _count);
			}
			_array = array;
		}
	}
	internal static class EnumerableHelpers
	{
		internal static void Copy<T>(IEnumerable<T> source, T[] array, int arrayIndex, int count)
		{
			if (source is ICollection<T> collection)
			{
				collection.CopyTo(array, arrayIndex);
			}
			else
			{
				IterativeCopy(source, array, arrayIndex, count);
			}
		}

		internal static void IterativeCopy<T>(IEnumerable<T> source, T[] array, int arrayIndex, int count)
		{
			foreach (T item in source)
			{
				array[arrayIndex++] = item;
			}
		}

		internal static T[] ToArray<T>(IEnumerable<T> source)
		{
			if (source is ICollection<T> { Count: var count } collection)
			{
				if (count == 0)
				{
					return Array.Empty<T>();
				}
				T[] array = new T[count];
				collection.CopyTo(array, 0);
				return array;
			}
			System.Collections.Generic.LargeArrayBuilder<T> largeArrayBuilder = new System.Collections.Generic.LargeArrayBuilder<T>(initialize: true);
			largeArrayBuilder.AddRange(source);
			return largeArrayBuilder.ToArray();
		}

		internal static T[] ToArray<T>(IEnumerable<T> source, out int length)
		{
			if (source is ICollection<T> { Count: var count } collection)
			{
				if (count != 0)
				{
					T[] array = new T[count];
					collection.CopyTo(array, 0);
					length = count;
					return array;
				}
			}
			else
			{
				using IEnumerator<T> enumerator = source.GetEnumerator();
				if (enumerator.MoveNext())
				{
					T[] array2 = new T[4]
					{
						enumerator.Current,
						default(T),
						default(T),
						default(T)
					};
					int num = 1;
					while (enumerator.MoveNext())
					{
						if (num == array2.Length)
						{
							int num2 = num << 1;
							if ((uint)num2 > 2146435071u)
							{
								num2 = ((2146435071 <= num) ? (num + 1) : 2146435071);
							}
							Array.Resize(ref array2, num2);
						}
						array2[num++] = enumerator.Current;
					}
					length = num;
					return array2;
				}
			}
			length = 0;
			return Array.Empty<T>();
		}
	}
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal readonly struct CopyPosition
	{
		public static System.Collections.Generic.CopyPosition Start => default(System.Collections.Generic.CopyPosition);

		internal int Row { get; }

		internal int Column { get; }

		private string DebuggerDisplay => $"[{Row}, {Column}]";

		internal CopyPosition(int row, int column)
		{
			Row = row;
			Column = column;
		}

		public System.Collections.Generic.CopyPosition Normalize(int endColumn)
		{
			if (Column != endColumn)
			{
				return this;
			}
			return new System.Collections.Generic.CopyPosition(Row + 1, 0);
		}
	}
	internal struct LargeArrayBuilder<T>
	{
		private const int StartingCapacity = 4;

		private const int ResizeLimit = 8;

		private readonly int _maxCapacity;

		private T[] _first;

		private System.Collections.Generic.ArrayBuilder<T[]> _buffers;

		private T[] _current;

		private int _index;

		private int _count;

		public int Count => _count;

		public LargeArrayBuilder(bool initialize)
			: this(int.MaxValue)
		{
		}

		public LargeArrayBuilder(int maxCapacity)
		{
			this = default(System.Collections.Generic.LargeArrayBuilder<T>);
			_first = (_current = Array.Empty<T>());
			_maxCapacity = maxCapacity;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(T item)
		{
			int index = _index;
			T[] current = _current;
			if ((uint)index >= (uint)current.Length)
			{
				AddWithBufferAllocation(item);
			}
			else
			{
				current[index] = item;
				_index = index + 1;
			}
			_count++;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddWithBufferAllocation(T item)
		{
			AllocateBuffer();
			_current[_index++] = item;
		}

		public void AddRange(IEnumerable<T> items)
		{
			using IEnumerator<T> enumerator = items.GetEnumerator();
			T[] destination = _current;
			int index = _index;
			while (enumerator.MoveNext())
			{
				T current = enumerator.Current;
				if ((uint)index >= (uint)destination.Length)
				{
					AddWithBufferAllocation(current, ref destination, ref index);
				}
				else
				{
					destination[index] = current;
				}
				index++;
			}
			_count += index - _index;
			_index = index;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddWithBufferAllocation(T item, ref T[] destination, ref int index)
		{
			_count += index - _index;
			_index = index;
			AllocateBuffer();
			destination = _current;
			index = _index;
			_current[index] = item;
		}

		public void CopyTo(T[] array, int arrayIndex, int count)
		{
			int num = 0;
			while (count > 0)
			{
				T[] buffer = GetBuffer(num);
				int num2 = Math.Min(count, buffer.Length);
				Array.Copy(buffer, 0, array, arrayIndex, num2);
				count -= num2;
				arrayIndex += num2;
				num++;
			}
		}

		public System.Collections.Generic.CopyPosition CopyTo(System.Collections.Generic.CopyPosition position, T[] array, int arrayIndex, int count)
		{
			int num = position.Row;
			int column = position.Column;
			T[] buffer = GetBuffer(num);
			int num2 = CopyToCore(buffer, column);
			if (count == 0)
			{
				return new System.Collections.Generic.CopyPosition(num, column + num2).Normalize(buffer.Length);
			}
			do
			{
				buffer = GetBuffer(++num);
				num2 = CopyToCore(buffer, 0);
			}
			while (count > 0);
			return new System.Collections.Generic.CopyPosition(num, num2).Normalize(buffer.Length);
			int CopyToCore(T[] sourceBuffer, int sourceIndex)
			{
				int num3 = Math.Min(sourceBuffer.Length - sourceIndex, count);
				Array.Copy(sourceBuffer, sourceIndex, array, arrayIndex, num3);
				arrayIndex += num3;
				count -= num3;
				return num3;
			}
		}

		public T[] GetBuffer(int index)
		{
			if (index != 0)
			{
				if (index > _buffers.Count)
				{
					return _current;
				}
				return _buffers[index - 1];
			}
			return _first;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SlowAdd(T item)
		{
			Add(item);
		}

		public T[] ToArray()
		{
			if (TryMove(out var array))
			{
				return array;
			}
			array = new T[_count];
			CopyTo(array, 0, _count);
			return array;
		}

		public bool TryMove(out T[] array)
		{
			array = _first;
			return _count == _first.Length;
		}

		private void AllocateBuffer()
		{
			if ((uint)_count < 8u)
			{
				int num = Math.Min((_count == 0) ? 4 : (_count * 2), _maxCapacity);
				_current = new T[num];
				Array.Copy(_first, 0, _current, 0, _count);
				_first = _current;
				return;
			}
			int num2;
			if (_count == 8)
			{
				num2 = 8;
			}
			else
			{
				_buffers.Add(_current);
				num2 = Math.Min(_count, _maxCapacity - _count);
			}
			_current = new T[num2];
			_index = 0;
		}
	}
}
namespace System.Text
{
	internal static class StringBuilderCache
	{
		private const int MaxBuilderSize = 360;

		private const int DefaultCapacity = 16;

		[ThreadStatic]
		private static StringBuilder t_cachedInstance;

		public static StringBuilder Acquire(int capacity = 16)
		{
			if (capacity <= 360)
			{
				StringBuilder stringBuilder = t_cachedInstance;
				if (stringBuilder != null && capacity <= stringBuilder.Capacity)
				{
					t_cachedInstance = null;
					stringBuilder.Clear();
					return stringBuilder;
				}
			}
			return new StringBuilder(capacity);
		}

		public static void Release(StringBuilder sb)
		{
			if (sb.Capacity <= 360)
			{
				t_cachedInstance = sb;
			}
		}

		public static string GetStringAndRelease(StringBuilder sb)
		{
			string result = sb.ToString();
			Release(sb);
			return result;
		}
	}
}
namespace Unity
{
	internal sealed class ThrowStub : ObjectDisposedException
	{
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
