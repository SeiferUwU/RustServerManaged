using System;
using System.Collections.Generic;

namespace Facepunch.Nexus.Models;

public class VariableDictionary : Dictionary<string, VariableData>
{
	public VariableDictionary()
		: base((IEqualityComparer<string>)StringComparer.InvariantCultureIgnoreCase)
	{
	}
}
