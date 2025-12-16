using System.Collections.Generic;

public class IpPrefixTrie
{
	private class Node
	{
		public Dictionary<char, Node> Children = new Dictionary<char, Node>();

		public bool IsTerminal;
	}

	private readonly Node _root = new Node();

	public void Add(string prefix)
	{
		Node node = _root;
		foreach (char key in prefix)
		{
			if (!node.Children.TryGetValue(key, out var value))
			{
				value = (node.Children[key] = new Node());
			}
			node = value;
		}
		node.IsTerminal = true;
	}

	public bool Matches(string ip)
	{
		Node value = _root;
		foreach (char key in ip)
		{
			if (value.IsTerminal)
			{
				return true;
			}
			if (!value.Children.TryGetValue(key, out value))
			{
				return false;
			}
		}
		return value.IsTerminal;
	}
}
