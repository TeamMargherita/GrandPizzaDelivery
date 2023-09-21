using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PizzaNS;

// 한석호 작성
public class PizzaStoreUI : MonoBehaviour, IPizzaStore
{
	[SerializeField] private GameObject[] slotObjArr;
	[SerializeField] private Text ingredientExplainText;

	private Sprite[] pizzaIngredientSprArr;
	private PizzaIngredientSlots[] pizzaIngredientSlotsArr;
	private int nowPage;
	private void Awake()
	{
		pizzaIngredientSprArr = Resources.LoadAll<Sprite>("UI/Ingredients_120_120");
		pizzaIngredientSlotsArr = new PizzaIngredientSlots[slotObjArr.Length];
		for (int i = 0; i < slotObjArr.Length; i++)
		{
			pizzaIngredientSlotsArr[i] = slotObjArr[i].GetComponent<PizzaIngredientSlots>();
			pizzaIngredientSlotsArr[i].SlotNumber = i;
			pizzaIngredientSlotsArr[i].InitCompo();
			pizzaIngredientSlotsArr[i].InitInterface(this);
		}

		nowPage = 0;

	}
	

	private void OnEnable()
	{
		// 재료슬롯은 켜질때 0페이지 고정
		// 스프라이트가 재료 슬롯에 들어가야됨
		InitPage(0);
	}
	// 재료 페이지 넘김
	public void Page(bool next)
	{
		if (next)
		{
			if (Constant.IngredientsArray.GetLength(0) / slotObjArr.Length <= nowPage)
			{
				return;
			}
			else
			{
				InitPage(++nowPage);
			}
		}
		else
		{
			if (nowPage <= 0)
			{
				return;
			}
			else
			{
				InitPage(0);
			}
		}
	}

	private void InitPage(int page)
	{
		nowPage = page;

		for (int i = 0; i < slotObjArr.Length; i++)
		{
			if (i + (nowPage * slotObjArr.Length) + 1 >= Constant.IngredientsArray.GetLength(0))
			{
				pizzaIngredientSlotsArr[i].IngredientNumber = 0;
				pizzaIngredientSlotsArr[i].SetIngredientsSpr(pizzaIngredientSprArr[0]);
			}
			else
			{
				pizzaIngredientSlotsArr[i].IngredientNumber = i + (nowPage * slotObjArr.Length) + 1;
				pizzaIngredientSlotsArr[i].SetIngredientsSpr(pizzaIngredientSprArr[i + (nowPage * slotObjArr.Length) + 1]);
			}
		}
	}

	public void IngredientExplain(int ingNum)
	{
		if (ingNum == 0)
		{
			ingredientExplainText.text 
				= "매력도 : " + Constant.IngredientsArray[0, 1] + "\n"
				+ "매력하락도 : " + Constant.IngredientsArray[0,2] + "\n"
				+ "재료값 : " + Constant.IngredientsArray[0,3];
		}
		else if (ingNum == -1)
		{
			ingredientExplainText.text = "";
		}
		else
		{
			ingredientExplainText.text
			= "매력도 : " + Constant.IngredientsArray[ingNum, 1] + "\n"
			+ "매력하락도 : " + Constant.IngredientsArray[ingNum, 2] + "\n"
			+ "재료값 : " + Constant.IngredientsArray[ingNum, 3];

		}
	}
}
