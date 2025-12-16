using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class HoverListener : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public UnityEvent OnEnter;

	public UnityEvent OnExit;

	public void OnPointerEnter(PointerEventData eventData)
	{
		OnEnter?.Invoke();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		OnExit?.Invoke();
	}
}
