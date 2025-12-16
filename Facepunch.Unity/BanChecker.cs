using System.Collections.Generic;

public class BanChecker
{
	private readonly HashSet<string> _exact = new HashSet<string>();

	private readonly IpPrefixTrie _trie = new IpPrefixTrie();

	public BanChecker(IEnumerable<string> banned)
	{
		foreach (string item in banned)
		{
			if (item.EndsWith("*"))
			{
				IpPrefixTrie trie = _trie;
				string text = item;
				trie.Add(text.Substring(0, text.Length - 1));
			}
			else
			{
				_exact.Add(item);
			}
		}
	}

	public bool IsBanned(string ip)
	{
		if (!_exact.Contains(ip))
		{
			return _trie.Matches(ip);
		}
		return true;
	}
}
