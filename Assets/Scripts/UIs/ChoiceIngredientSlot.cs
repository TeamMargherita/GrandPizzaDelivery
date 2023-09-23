using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChoiceIngredientSlot : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
	private IIngredientSlot iIngredientSlot;
	public int SlotNumber { get; set; } // 슬롯 번호

	public int IngredientNumber { get; set; }   // 재료번호

	private Color grayColor = new Color(150 / 255f, 150 / 255f, 150 / 255f, 1f);
	private Color darkColor = new Color(100 / 255f, 100 / 255f, 100 / 255f, 1f);

	private Image img;
	public void InitCompo()
	{
		img = this.GetComponent<Image>();
	}
	public void SetIngredientsSpr(Sprite spr)
	{
		img.sprite = spr;
	}
	public void InitInterface(IIngredientSlot iIngredientSlot)
	{
		this.iIngredientSlot = iIngredientSlot;
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		img.color = grayColor;
		iIngredientSlot.IngredientExplain(IngredientNumber);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		img.color = darkColor;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		img.color = grayColor;
		iIngredientSlot.ChoiceIngredient(IngredientNumber, SlotNumber);
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		img.color = Color.white;
	}
}
