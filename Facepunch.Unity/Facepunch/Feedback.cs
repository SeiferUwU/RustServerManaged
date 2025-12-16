using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Facepunch.Models;
using Newtonsoft.Json;

namespace Facepunch;

public static class Feedback
{
	public static async Task<string> Report(Facepunch.Models.Feedback feedback)
	{
		if (Application.Manifest == null)
		{
			return "manifest is null";
		}
		if (string.IsNullOrEmpty(Application.Manifest.ReportUrl))
		{
			return "no report url";
		}
		return "not supported on servers";
	}

	public static async Task<string> ServerReport(string endpoint, ulong fromPlayer, string key, Facepunch.Models.Feedback feedback)
	{
		if (string.IsNullOrEmpty(endpoint))
		{
			return "Failed to send report! No report endpoint url, set one on server.reportsserverendpoint";
		}
		if (!endpoint.StartsWith("http"))
		{
			return "Failed to send report! Invalid report endpoint url, missing http on endpoint: " + endpoint;
		}
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("data", JsonConvert.SerializeObject(feedback));
		dictionary.Add("userid", fromPlayer.ToString());
		if (!string.IsNullOrEmpty(key))
		{
			dictionary.Add("key", key);
		}
		try
		{
			return await WebUtil.PostAsync(endpoint, dictionary);
		}
		catch (Exception ex)
		{
			return "Failed to send report! Exception: " + ex.Message;
		}
	}
}
