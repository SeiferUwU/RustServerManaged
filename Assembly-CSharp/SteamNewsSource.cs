using System.Collections;
using System.Collections.Generic;
using JSON;
using UnityEngine;

public static class SteamNewsSource
{
	public struct Story
	{
		public string name;

		public string url;

		public int date;

		public string text;

		public string author;
	}

	public static Story[] Stories;

	public static IEnumerator GetStories()
	{
		WWW www = new WWW("https://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid=252490&count=8&format=json&feeds=steam_community_announcements");
		yield return www;
		JSON.Object obj = JSON.Object.Parse(www.text);
		www.Dispose();
		if (obj == null)
		{
			yield break;
		}
		Array array = obj.GetObject("appnews").GetArray("newsitems");
		List<Story> list = new List<Story>();
		foreach (Value item in array)
		{
			string text = item.Obj.GetString("contents", "Missing Contents");
			text = text.Replace("\\n", "\n").Replace("\\r", "").Replace("\\\"", "\"");
			list.Add(new Story
			{
				name = item.Obj.GetString("title", "Missing Title"),
				url = item.Obj.GetString("url", "Missing URL"),
				date = item.Obj.GetInt("date"),
				text = text,
				author = item.Obj.GetString("author", "Missing Author")
			});
		}
		Stories = list.ToArray();
	}
}
