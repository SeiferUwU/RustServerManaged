using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButtonTools : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IScrollHandler
{
	public ScrollRect MainScroll;

	public Image image;

	public Image backgroundImage;

	public ItemDefinition itemDef;

	public void GiveSelf(int amount)
	{
		DebugLog();
		ConsoleSystem.Run(ConsoleSystem.Option.Client, "inventory.giveid", itemDef.itemid, amount);
	}

	public void GiveArmed()
	{
		DebugLog();
		ConsoleSystem.Run(ConsoleSystem.Option.Client, "inventory.givearm", itemDef.itemid);
	}

	public void GiveStack()
	{
		DebugLog();
		ConsoleSystem.Run(ConsoleSystem.Option.Client, "inventory.giveid", itemDef.itemid, itemDef.stackable);
	}

	public void GiveBlueprint()
	{
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
