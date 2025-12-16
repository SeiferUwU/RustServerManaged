#define UNITY_ASSERTIONS
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Facepunch;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Rust.UI;

public class HttpImage : MonoBehaviour
{
	private static bool _isRunningCleanup;

	public static Dictionary<string, Task> RequestCache = new Dictionary<string, Task>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, (Texture2D Texture, Sprite Sprite, bool IsAsset)> TextureCache = new Dictionary<string, (Texture2D, Sprite, bool)>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, (int Count, double LastModified)> ReferenceCounts = new Dictionary<string, (int, double)>(StringComparer.OrdinalIgnoreCase);

	public Texture2D LoadingImage;

	public Texture2D MissingImage;

	public string Url = "";

	public bool AutosizeHeight;

	[Tooltip("Fill the RectTransform with the image without skewing when rendering in a RawImage and AutosizeHeight is disabled.")]
	public bool UseCoverFill;

	public bool GenerateMipmaps = true;

	public float MaxWidth;

	public float MaxHeight;

	public bool AllowDisablingImage = true;

	public RawImage rawImage;

	private bool imageLoaded;

	[Header("Duplicate?")]
	public RawImage secondaryRawImage;

	public Image secondaryImage;

	private Image image;

	private Sprite loadingSprite;

	private Sprite missingSprite;

	private bool didLoad;

	public bool HasLoaded => imageLoaded;

	public bool IsLoading { get; private set; }

	public event Action OnImageLoaded;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void ResetStaticFields()
	{
		_isRunningCleanup = false;
		RequestCache = new Dictionary<string, Task>(StringComparer.OrdinalIgnoreCase);
		TextureCache = new Dictionary<string, (Texture2D, Sprite, bool)>(StringComparer.OrdinalIgnoreCase);
		ReferenceCounts = new Dictionary<string, (int, double)>(StringComparer.OrdinalIgnoreCase);
	}

	private void Init()
	{
		rawImage = GetComponent<RawImage>();
		image = GetComponent<Image>();
		if (LoadingImage != null)
		{
			loadingSprite = CreateSprite(LoadingImage);
		}
		if (MissingImage != null)
		{
			missingSprite = CreateSprite(MissingImage);
		}
		SetImageEnabled(enabled: false);
	}

	private void Start()
	{
		if (!didLoad)
		{
			Init();
			if (!string.IsNullOrWhiteSpace(Url))
			{
				string url = Url;
				Url = null;
				Load(url);
			}
		}
	}

	private void OnDestroy()
	{
		ClearUrl();
		if (loadingSprite != null)
		{
			UnityEngine.Object.Destroy(loadingSprite);
		}
		if (missingSprite != null)
		{
			UnityEngine.Object.Destroy(missingSprite);
		}
	}

	private void ClearUrl()
	{
		string url = Url;
		Url = null;
		if (!string.IsNullOrWhiteSpace(url))
		{
			if (ReferenceCounts.TryGetValue(url, out (int, double) value))
			{
				ReferenceCounts[url] = (Mathf.Max(value.Item1 - 1, 0), Time.realtimeSinceStartupAsDouble);
			}
			if (!_isRunningCleanup)
			{
				_isRunningCleanup = true;
				Global.Runner.InvokeRandomized(CleanupCache, 60f, 60f, 10f);
			}
		}
	}

	private static void CleanupCache()
	{
		double realtimeSinceStartupAsDouble = Time.realtimeSinceStartupAsDouble;
		List<string> obj = Pool.Get<List<string>>();
		foreach (var (text2, tuple2) in ReferenceCounts)
		{
			if ((!TextureCache.TryGetValue(text2, out (Texture2D, Sprite, bool) value) || !value.Item3) && tuple2.Item1 <= 0 && realtimeSinceStartupAsDouble - tuple2.Item2 > 10.0)
			{
				obj.Add(text2);
			}
		}
		foreach (string item in obj)
		{
			RequestCache.Remove(item);
			ReferenceCounts.Remove(item);
			if (TextureCache.Remove(item, out (Texture2D, Sprite, bool) value2))
			{
				Assert.IsFalse(value2.Item3, "cache.IsAsset");
				UnityEngine.Object.Destroy(value2.Item1);
				UnityEngine.Object.Destroy(value2.Item2);
			}
		}
		Pool.FreeUnmanaged(ref obj);
	}

	public bool Load(string url)
	{
		if (string.IsNullOrWhiteSpace(url))
		{
			return false;
		}
		if (string.Equals(Url, url, StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}
		if (rawImage == null && image == null)
		{
			Init();
		}
		imageLoaded = false;
		IsLoading = true;
		SetLoadingImage();
		Url = url;
		didLoad = true;
		if (ReferenceCounts.TryGetValue(url, out (int, double) value))
		{
			ReferenceCounts[url] = (value.Item1 + 1, Time.realtimeSinceStartupAsDouble);
		}
		else
		{
			ReferenceCounts.Add(url, (1, Time.realtimeSinceStartupAsDouble));
		}
		if (TextureCache.TryGetValue(url, out (Texture2D, Sprite, bool) value2))
		{
			UpdateImageTexture(value2.Item1, value2.Item2);
			imageLoaded = true;
			this.OnImageLoaded?.Invoke();
			return false;
		}
		if (RequestCache.ContainsKey(url))
		{
			SetImageEnabled(enabled: false);
			Global.Runner.StartCoroutine(WaitForLoad(url));
			return false;
		}
		SetImageEnabled(enabled: false);
		Global.Runner.StartCoroutine(StartAndWaitForLoad(url));
		return true;
	}

	public void Load(Sprite sprite)
	{
		if (rawImage == null && image == null)
		{
			Init();
		}
		ClearUrl();
		didLoad = true;
		UpdateImageTexture(sprite.texture, sprite);
	}

	private IEnumerator WaitForLoad(string url)
	{
		while (!TextureCache.ContainsKey(url) && string.Equals(url, Url, StringComparison.OrdinalIgnoreCase))
		{
			yield return null;
		}
		if ((bool)this && string.Equals(url, Url, StringComparison.OrdinalIgnoreCase) && (rawImage != null || image != null) && TextureCache.TryGetValue(url, out (Texture2D, Sprite, bool) value))
		{
			UpdateImageTexture(value.Item1, value.Item2);
		}
		imageLoaded = true;
		this.OnImageLoaded?.Invoke();
	}

	private void UpdateImageTexture(Texture2D tex, Sprite sprite)
	{
		if (!(tex == null) && !(sprite == null))
		{
			if (rawImage != null)
			{
				rawImage.texture = tex;
			}
			if (image != null)
			{
				image.sprite = sprite;
			}
			if (secondaryRawImage != null)
			{
				secondaryRawImage.texture = tex;
			}
			if (secondaryImage != null)
			{
				secondaryImage.sprite = sprite;
			}
			SetImageEnabled(enabled: true);
			AutosizeForTexture(tex);
		}
	}

	public void SetMissingImage()
	{
		ClearUrl();
		UpdateImageTexture(MissingImage, missingSprite);
	}

	public void SetLoadingImage()
	{
		ClearUrl();
		UpdateImageTexture(LoadingImage, loadingSprite);
	}

	private IEnumerator StartAndWaitForLoad(string url)
	{
		Task<string> downloadTask = WebUtil.DownloadFileTemp(url);
		RequestCache.Add(url, downloadTask);
		while (!downloadTask.IsCompleted)
		{
			yield return null;
		}
		RequestCache.Remove(url);
		bool flag = false;
		Texture2D texture2D;
		if (downloadTask.IsCompletedSuccessfully)
		{
			string result = downloadTask.Result;
			if (string.IsNullOrWhiteSpace(result))
			{
				Debug.LogWarning("Failed to download image from " + url);
				texture2D = null;
			}
			else
			{
				texture2D = new Texture2D(16, 16, TextureFormat.ARGB32, GenerateMipmaps);
				texture2D.name = url;
				texture2D.wrapMode = TextureWrapMode.Clamp;
				byte[] data = File.ReadAllBytes(result);
				if (!texture2D.LoadImage(data, markNonReadable: true))
				{
					UnityEngine.Object.DestroyImmediate(texture2D);
					texture2D = null;
				}
				else if ((MaxWidth > 0f && (float)texture2D.width > MaxWidth) || (MaxHeight > 0f && (float)texture2D.height > MaxHeight))
				{
					Debug.Log("Texture from " + url + " was too big, ignoring");
					UnityEngine.Object.DestroyImmediate(texture2D);
					texture2D = null;
				}
				else
				{
					flag = true;
				}
			}
		}
		else
		{
			Debug.LogWarning($"Failed to download image from {url}: {downloadTask.Exception}");
			texture2D = null;
		}
		if (texture2D == null)
		{
			if (MissingImage != null)
			{
				texture2D = MissingImage;
			}
			if (texture2D == null)
			{
				texture2D = Texture2D.blackTexture;
			}
		}
		Sprite item = CreateSprite(texture2D);
		(Texture2D, Sprite, bool) value = (texture2D, item, !flag);
		TextureCache.Add(url, value);
		if ((bool)this && string.Equals(url, Url, StringComparison.OrdinalIgnoreCase) && (rawImage != null || image != null))
		{
			UpdateImageTexture(value.Item1, value.Item2);
			AutosizeForTexture(texture2D);
		}
		imageLoaded = true;
		this.OnImageLoaded?.Invoke();
	}

	private void AutosizeForTexture(Texture2D texture)
	{
		if (AutosizeHeight)
		{
			float num = (float)texture.width / (float)texture.height;
			if (rawImage != null)
			{
				rawImage.rectTransform.sizeDelta = new Vector2(rawImage.rectTransform.sizeDelta.x, rawImage.rectTransform.sizeDelta.x / num);
			}
			if (image != null)
			{
				image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.x, image.rectTransform.sizeDelta.x / num);
			}
			SetImageEnabled(enabled: true);
		}
		UpdateCoverFill();
	}

	private void UpdateCoverFill()
	{
		if (UseCoverFill && !(rawImage == null))
		{
			Texture texture = rawImage.texture;
			Vector2 size = rawImage.GetPixelAdjustedRect().size;
			Vector2 vector = new Vector2(texture.width, texture.height);
			float num = size.x / size.y;
			float num2 = vector.x / vector.y;
			float num3 = ((num >= num2) ? (size.x / vector.x) : (size.y / vector.y));
			float num4 = vector.x * num3;
			float num5 = vector.y * num3;
			rawImage.uvRect = new Rect
			{
				x = (num4 - size.x) / 2f / num4,
				y = (num5 - size.y) / 2f / num5,
				width = size.x / num4,
				height = size.y / num5
			};
		}
	}

	private void SetImageEnabled(bool enabled)
	{
		if (AllowDisablingImage)
		{
			if (rawImage != null)
			{
				rawImage.enabled = enabled;
			}
			if (image != null)
			{
				image.enabled = enabled;
			}
			if (enabled)
			{
				IsLoading = false;
			}
		}
	}

	private void OnRectTransformDimensionsChange()
	{
		UpdateCoverFill();
	}

	private void SetHasLoaded()
	{
		imageLoaded = true;
	}

	private static Sprite CreateSprite(Texture2D texture)
	{
		return Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(texture.width, texture.height)), new Vector2(0.5f, 0.5f));
	}
}
