using Coffee.UIEffects;
using UnityEngine;

namespace Rust.UI.MainMenu;

[RequireComponent(typeof(RectTransform))]
public class GradientMouseFollow : MonoBehaviour
{
	[SerializeField]
	private UIGradient gradient;
}
