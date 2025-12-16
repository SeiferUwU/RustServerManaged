using UnityEngine;

namespace Rust.Workshop.Editor;

public class MaterialRow : MonoBehaviour
{
	public string ParamName;

	protected WorkshopItemEditor Editor => GetComponentInParent<WorkshopItemEditor>();

	public virtual void Read(Material source, Material def)
	{
	}
}
