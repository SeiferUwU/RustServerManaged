using System;
using System.Collections;
using System.IO;
using System.Linq;
using Rust;
using UnityEngine;
using UnityEngine.Video;

public class MenuBackgroundVideo : SingletonComponent<MenuBackgroundVideo>
{
	[ClientVar]
	public static bool RestrictMP4 = true;

	private int index;

	private bool errored;

	private string[] mp4Videos;

	private string[] webmVideos;

	private bool forceWebmOnly;

	private VideoPlayer _videoPlayer;

	protected override void Awake()
	{
		base.Awake();
		if (RestrictMP4)
		{
			forceWebmOnly = true;
		}
		_videoPlayer = GetComponent<VideoPlayer>();
		_videoPlayer.source = VideoSource.Url;
		if (UnityEngine.Application.platform == RuntimePlatform.LinuxPlayer || UnityEngine.Application.platform == RuntimePlatform.LinuxEditor)
		{
			forceWebmOnly = true;
		}
		_videoPlayer.errorReceived += OnVideoError;
		LoadVideoList();
		NextVideo();
	}

	private void OnVideoError(VideoPlayer source, string message)
	{
		errored = true;
		forceWebmOnly = true;
		_videoPlayer.Stop();
		NextVideo();
	}

	public void LoadVideoList()
	{
		string path = Path.Combine(UnityEngine.Application.streamingAssetsPath, "MenuVideo");
		string path2 = Path.Combine(path, "mp4");
		string path3 = Path.Combine(path, "webm");
		if (Directory.Exists(path2))
		{
			mp4Videos = (from x in Directory.EnumerateFiles(path2, "*.mp4")
				orderby Guid.NewGuid()
				select x).ToArray();
		}
		else
		{
			mp4Videos = Array.Empty<string>();
		}
		if (Directory.Exists(path3))
		{
			webmVideos = (from x in Directory.EnumerateFiles(path3, "*.webm")
				orderby Guid.NewGuid()
				select x).ToArray();
		}
		else
		{
			webmVideos = Array.Empty<string>();
		}
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			LoadVideoList();
		}
		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			NextVideo();
		}
	}

	private void NextVideo()
	{
		if (Rust.Application.isQuitting)
		{
			return;
		}
		errored = false;
		string[] array = (forceWebmOnly ? webmVideos : mp4Videos);
		if (array.Length == 0)
		{
			Debug.LogWarning("[MenuBackgroundVideo] No available videos to play.");
			return;
		}
		string text = array[index++ % array.Length];
		if (!forceWebmOnly && Global.LaunchCountThisVersion <= 3)
		{
			string text2 = mp4Videos.FirstOrDefault((string x) => x.EndsWith("whatsnew.mp4", StringComparison.OrdinalIgnoreCase));
			if (!string.IsNullOrEmpty(text2))
			{
				text = text2;
			}
		}
		_videoPlayer.url = "file://" + text;
		_videoPlayer.Play();
	}

	internal IEnumerator ReadyVideo()
	{
		if (!errored)
		{
			if (_videoPlayer == null)
			{
				_videoPlayer = GetComponent<VideoPlayer>();
			}
			NextVideo();
			while (!_videoPlayer.isPrepared && !errored)
			{
				yield return null;
			}
		}
	}

	[ClientVar]
	public static void ForceNextVideo()
	{
		MenuBackgroundVideo[] array = UnityEngine.Object.FindObjectsOfType<MenuBackgroundVideo>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].NextVideo();
		}
	}

	[ClientVar]
	public static void EmulateError()
	{
		UnityEngine.Object.FindObjectOfType<MenuBackgroundVideo>().OnVideoError(null, null);
	}
}
