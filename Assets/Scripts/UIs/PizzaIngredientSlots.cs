using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PizzaNS;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 한석호 작성
public class PizzaIngredientSlots : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
	public int SlotNumber { get; set; }	// 슬롯 번호
	public int IngredientNumber { get; set; }   // 재료번호

	private IPizzaStore iPizzaStore;

	private Color grayColor = new Color(150 / 255f, 150 / 255f, 150 / 255f, 1f);
	private Color darkColor = new Color(100 / 255f, 100 / 255f, 100 / 255f, 1f);

	private Image childImg;
	private Image img;
	private RectTransform childRect;
	private Sprite IngredientSprtie;    // 재료 스프라이트
	public void InitCompo()
	{
		img = this.GetComponent<Image>();
		childImg = this.transform.GetChild(0).GetComponent<Image>();
		childRect = this.transform.GetChild(0).GetComponent<RectTransform>();
	}
	public void InitInterface(IPizzaStore iPizzaStore)
	{
		this.iPizzaStore = iPizzaStore;
	}
	public void SetIngredientsSpr(Sprite spr)
	{
		childImg.sprite = spr;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		img.color = grayColor;
		iPizzaStore.IngredientExplain(IngredientNumber);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		img.color = darkColor;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		img.color = grayColor;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		img.color = Color.white;
	}
}
