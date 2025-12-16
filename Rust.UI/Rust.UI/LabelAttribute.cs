using System;

namespace Rust.UI;

public class LabelAttribute : Attribute
{
	public Translate.Phrase Phrase;

	public LabelAttribute(string token, string english)
	{
		Phrase = new Translate.Phrase(token, english);
	}
}
