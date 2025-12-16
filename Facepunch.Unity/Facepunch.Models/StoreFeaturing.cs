namespace Facepunch.Models;

[JsonModel]
public class StoreFeaturing
{
	public int ItemID { get; set; }

	public string HeaderText { get; set; }

	public string TitleText { get; set; }

	public string SubtitleText { get; set; }

	public string ImageUrl { get; set; }

	public string VideoUrl { get; set; }

	public string TargetUrl { get; set; }

	public int Priority { get; set; }
}
