using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddPizzaSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	public int SlotNumber { get; set; }

	private IAddPizza iAddPizza;
	
	public void InitAddPizzaSlot(IAddPizza iAddPizza)
	{
		this.iAddPizza = iAddPizza;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		iAddPizza.SetTemSlotNumber(SlotNumber);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		iAddPizza.SetAddPizzaExplain(SlotNumber);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		iAddPizza.SetAddPizzaExplain(-1);
	}
}
