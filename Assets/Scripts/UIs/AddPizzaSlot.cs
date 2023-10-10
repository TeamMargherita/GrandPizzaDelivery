using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 한석호 작성

public class AddPizzaSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	public int SlotNumber { get; set; }

	private UnityEngine.UI.Image img;
	private IAddPizza iAddPizza;
	private bool isClick;

	private void Awake()
	{
		img = this.gameObject.GetComponent<UnityEngine.UI.Image>();
	}
	public void InitAddPizzaSlot(IAddPizza iAddPizza)
	{
		this.iAddPizza = iAddPizza;
	}
	public void InitColor()
	{
		img.color = Color.white;
		isClick = false;
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		if (SlotNumber == -1) { return; }

		iAddPizza.SetTemSlotNumber(SlotNumber);
		if (!isClick)
		{
			img.color = Color.magenta;
			isClick = true;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		iAddPizza.SetAddPizzaExplain(SlotNumber);
		if (!isClick)
		{
			img.color = Color.gray;
		}
		else
		{
			img.color = Color.magenta;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		iAddPizza.SetAddPizzaExplain(-1);
		if (!isClick)
		{
			img.color = Color.white;
		}
		else
		{
			img.color = Color.magenta;
		}
	}
}
