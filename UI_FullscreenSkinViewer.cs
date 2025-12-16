using UnityEngine;
using UnityEngine.UI;

public class UI_FullscreenSkinViewer : SingletonComponent<UI_FullscreenSkinViewer>
{
	public CanvasGroup background;

	public Image glowImage;

	public GameObject closeButton;

	private UI_SkinViewerControls _source;
}
