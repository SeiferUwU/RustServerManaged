using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VehicleButtonTools : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IScrollHandler
{
	public ScrollRect MainScroll;

	public Image image;

	public Image backgroundImage;

	public ItemDefinition itemDef;

	public void Spawn()
	{
		DebugLog();
		string shortname = itemDef.shortname;
		ItemModEntityReference component = itemDef.GetComponent<ItemModEntityReference>();
		if ((bool)component)
		{
			shortname = component.entityPrefab.Get().name;
		}
		ConsoleSystem.Run(ConsoleSystem.Option.Client, "spawn", shortname);
	}

	private void DebugLog()
	{
		if (Input.GetKey(KeyCode.LeftAlt))
		{
			Debug.Log(itemDef.gameObject.name, itemDef.gameObject);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		MainScroll.OnBeginDrag(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		MainScroll.OnDrag(eventData);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		MainScroll.OnEndDrag(eventData);
	}

	public void OnScroll(PointerEventData data)
	{
		MainScroll.OnScroll(data);
	}
}
