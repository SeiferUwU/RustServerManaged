using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine.UI;

namespace Oxide.Game.Rust.Cui;

public class CuiContentSizeFitterComponent : ICuiComponent, ICuiEnableable
{
	public string Type => "UnityEngine.UI.ContentSizeFitter";

	[JsonConverter(typeof(StringEnumConverter))]
	[JsonProperty("horizontalFit")]
	public ContentSizeFitter.FitMode HorizontalFit { get; set; }

	[JsonConverter(typeof(StringEnumConverter))]
	[JsonProperty("verticalFit")]
	public ContentSizeFitter.FitMode VerticalFit { get; set; }

	public bool? Enabled { get; set; }
}
