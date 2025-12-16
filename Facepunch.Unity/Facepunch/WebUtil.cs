using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Facepunch.Crypt;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Facepunch;

public static class WebUtil
{
	public static string[] ValidVideoExtensions = new string[9] { ".dv", ".m4v", ".mov", ".mp4", ".mpg", ".mpeg", ".ogv", ".vp8", ".webm" };

	public static string[] ValidImageExtensions = new string[5] { ".bmp", ".jpg", ".jpeg", ".png", ".tga" };

	internal static void Get(string url, Action<string> result)
	{
		if (url.Contains("https://localhost"))
		{
			url = url.Replace("https://localhost", "http://localhost");
		}
		if (Application.Integration.DebugOutput)
		{
			UnityEngine.Debug.Log("[Get] \"" + url + "\"");
		}
		UnityWebRequest www = UnityWebRequest.Get(url);
		Application.Controller.StartCoroutine(DownloadStringCoroutine(www, result));
	}

	private static IEnumerator DownloadStringCoroutine(UnityWebRequest www, Action<string> result)
	{
		yield return www.SendWebRequest();
		if (www.isNetworkError)
		{
			UnityEngine.Debug.LogWarning("Error with download: " + www.error + " (" + www.url + ")");
		}
		result(www.downloadHandler.text);
		www.Dispose();
	}

	public static async Task<string> DownloadFileTemp(string url, string[] validExtensions = null)
	{
		if (!Directory.Exists("temp"))
		{
			Directory.CreateDirectory("temp");
		}
		int num = url.IndexOf("?", StringComparison.Ordinal);
		string text = Path.GetExtension((num >= 0) ? url.Substring(0, num) : url)?.ToLowerInvariant();
		if (validExtensions != null && !validExtensions.Contains(text))
		{
			UnityEngine.Debug.LogWarning("Trying to download file with invalid extension: " + url);
			return null;
		}
		if (string.IsNullOrEmpty(text))
		{
			text = ".dat";
		}
		string targetName = "temp/" + Md5.Calculate(url.ToLowerInvariant()) + text;
		FileInfo fileInfo = new FileInfo(targetName);
		if (fileInfo.Exists)
		{
			return fileInfo.FullName;
		}
		using UnityWebRequest request = UnityWebRequest.Get(url);
		request.SendWebRequest();
		while (!request.isDone)
		{
			await Task.Delay(100);
		}
		if (request.isNetworkError || request.isHttpError)
		{
			return null;
		}
		await File.WriteAllBytesAsync(targetName, request.downloadHandler.data);
		return fileInfo.FullName;
	}

	private static void DownloadString(UnityWebRequest www, Action<string> result)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = www.SendWebRequest();
		Stopwatch stopwatch = Stopwatch.StartNew();
		while (!unityWebRequestAsyncOperation.isDone)
		{
			if (stopwatch.Elapsed.TotalSeconds > 10.0)
			{
				return;
			}
		}
		if (www.isNetworkError)
		{
			UnityEngine.Debug.LogError("Error with download: " + www.error);
		}
		else
		{
			result(www.downloadHandler.text);
		}
		www.Dispose();
	}

	internal static void Post(string url, Dictionary<string, string> data, bool wait, Action<string> result)
	{
		if (url.Contains("https://localhost"))
		{
			url = url.Replace("https://localhost", "http://localhost");
		}
		WWWForm wWWForm = new WWWForm();
		foreach (KeyValuePair<string, string> datum in data)
		{
			wWWForm.AddField(datum.Key, datum.Value);
		}
		if (Application.Integration.DebugOutput)
		{
			UnityEngine.Debug.Log("[Post] \"" + url + "\"");
		}
		WWW wWW = new WWW(url, wWWForm);
		if (wait)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			while (!wWW.isDone && !(stopwatch.Elapsed.TotalSeconds > 5.0))
			{
			}
			wWW.Dispose();
		}
		else
		{
			Application.Controller.StartCoroutine(PostValuesCoroutine(wWW, result));
		}
	}

	internal static async Task<string> PostAsync(string url, Dictionary<string, string> data)
	{
		if (url.Contains("https://localhost"))
		{
			url = url.Replace("https://localhost", "http://localhost");
		}
		WWWForm wWWForm = new WWWForm();
		foreach (KeyValuePair<string, string> datum in data)
		{
			wWWForm.AddField(datum.Key, datum.Value);
		}
		if (Application.Integration.DebugOutput)
		{
			UnityEngine.Debug.Log("[Post] \"" + url + "\"");
		}
		Stopwatch waitTime = Stopwatch.StartNew();
		using UnityWebRequest request = UnityWebRequest.Post(url, wWWForm);
		request.SendWebRequest();
		while (!request.isDone)
		{
			if (waitTime.Elapsed.TotalSeconds > 30.0)
			{
				return "error: 30 seconds time out";
			}
			await Task.Delay(10);
		}
		if (request.isNetworkError || request.isHttpError)
		{
			return $"error [{request.isNetworkError}|{request.isHttpError}|{request.responseCode}|{request.downloadHandler.text}]";
		}
		return request.downloadHandler.text;
	}

	internal static void PostData(string url, object dataObject, Action<string> result, bool wait = false)
	{
		string value = JsonConvert.SerializeObject(dataObject, Formatting.Indented);
		WWWForm wWWForm = new WWWForm();
		wWWForm.AddField("data", value);
		UnityWebRequest www = UnityWebRequest.Post(url, wWWForm);
		if (wait)
		{
			DownloadString(www, result);
		}
		else
		{
			Application.Controller.StartCoroutine(DownloadStringCoroutine(www, result));
		}
	}

	internal static async Task<string> PostDataAsync(string url, object dataObject)
	{
		string value = JsonConvert.SerializeObject(dataObject, Formatting.Indented);
		WWWForm wWWForm = new WWWForm();
		wWWForm.AddField("data", value);
		UnityWebRequest www = UnityWebRequest.Post(url, wWWForm);
		UnityWebRequestAsyncOperation request = www.SendWebRequest();
		Stopwatch waitTime = Stopwatch.StartNew();
		while (!request.isDone)
		{
			if (waitTime.Elapsed.TotalSeconds > 10.0)
			{
				return "timed out";
			}
			await Task.Delay(10);
		}
		if (www.isNetworkError)
		{
			UnityEngine.Debug.LogError("Error with download: " + www.error);
			return null;
		}
		string text = www.downloadHandler.text;
		www.Dispose();
		return text;
	}

	private static IEnumerator PostValuesCoroutine(WWW www, Action<string> result)
	{
		yield return www;
		if (www.error == null)
		{
			result?.Invoke(www.text);
		}
		if (Application.Integration.DebugOutput)
		{
			if (www.error != null)
			{
				UnityEngine.Debug.LogWarning("[Post] Error: \"" + www.error + "\" - \"" + www.text + "\" ");
			}
			else
			{
				UnityEngine.Debug.Log("[Post] Response: \"" + www.text + "\"");
			}
		}
		www.Dispose();
	}

	internal static string Escape(string type)
	{
		return UnityWebRequest.EscapeURL(type);
	}
}
