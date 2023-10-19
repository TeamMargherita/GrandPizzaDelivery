using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PizzaNS;

// 한석호 작성
public class PizzaStoreUI : MonoBehaviour, IIngredientSlot
{
	[SerializeField] private GameObject[] slotObjArr;	// 재료 슬롯
	[SerializeField] private GameObject[] choiceSlotArr;	// 선택한 재료가 담긴 슬롯
	[SerializeField] private Text ingredientExplainText;
	[SerializeField] private Text choiceIngredientExplainText;

	//private List<int> choiceIngredientList = new List<int>();
	/// <summary>
	/// 재료 스프라이트
	/// </summary>
	private Sprite[] pizzaIngredientSprArr;
	private PizzaIngredientSlots[] pizzaIngredientSlotsArr;
	private ChoiceIngredientSlot[] choiceIngredientSlotArr;

	private int attractiveness;
	private int declineAt;
	private int ingredientPrice;
	private int nowPage;
	private void Awake()
	{
		pizzaIngredientSprArr = Resources.LoadAll<Sprite>("UI/Ingredients_120_120");
		pizzaIngredientSlotsArr = new PizzaIngredientSlots[slotObjArr.Length];
		choiceIngredientSlotArr = new ChoiceIngredientSlot[choiceSlotArr.Length];
		
		for (int i = 0; i < slotObjArr.Length; i++)
		{
			pizzaIngredientSlotsArr[i] = slotObjArr[i].GetComponent<PizzaIngredientSlots>();
			pizzaIngredientSlotsArr[i].SlotNumber = i;
			pizzaIngredientSlotsArr[i].InitCompo();
			pizzaIngredientSlotsArr[i].InitInterface(this);
		}

		for (int i = 0; i < choiceSlotArr.Length; i++)
		{
			choiceIngredientSlotArr[i] = choiceSlotArr[i].GetComponent<ChoiceIngredientSlot>();
			choiceIngredientSlotArr[i].SlotNumber = i;
			choiceIngredientSlotArr[i].InitCompo();
			choiceIngredientSlotArr[i].InitInterface(this);
			choiceIngredientSlotArr[i].SetIngredientsSpr(pizzaIngredientSprArr[0]);
		}

		nowPage = 0;
		InitValue();
	}
	private void OnEnable()
	{
		// 재료슬롯은 켜질때 0페이지 고정
		// 스프라이트가 재료 슬롯에 들어가야됨
		BackMakePizza();
	}
	/// <summary>
	/// 피자 재료판 초기화. 피자 재료판 화면이 켜질 때 호출
	/// </summary>
	public void BackMakePizza()
	{
		InitPage(0);
		SetChoiceText(false);
		InitChoiceIngredient();
		Constant.PizzaAttractiveness = 0;
	}
	/// <summary>
	/// 만드려는 피자의 능력치 초기화
	/// </summary>
	private void InitValue()
	{
		attractiveness = 0;
		declineAt = 0;
		ingredientPrice = 0;
	}

	/// <summary>
	/// 재료 페이지 넘김
	/// </summary>
	/// <param name="next"></param>
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
	/// <summary>
	/// 페이지에 해당하는 재료들로 이미지, 정보 갱신
	/// </summary>
	/// <param name="page"></param>
	private void InitPage(int page)
	{
		nowPage = page;

		for (int i = 0; i < slotObjArr.Length; i++)
		{
			// 과일 배열 범위를 초과하는 슬롯은 전부 없음으로 상태를 변경
			if (i + (nowPage * slotObjArr.Length) + 1 >= Constant.IngredientsArray.GetLength(0))
			{
				pizzaIngredientSlotsArr[i].IngredientNumber = 0;
				pizzaIngredientSlotsArr[i].SetIngredientsSpr(pizzaIngredientSprArr[0]);
			}
			else
			{
				// 해금된 과일들만 정보를 넣어줘야함

				// + 1 을 해주는 이유는 이미지의 0 값이 '없음'을 의미하기 때문.
				pizzaIngredientSlotsArr[i].IngredientNumber = i + (nowPage * slotObjArr.Length) + 1;
				pizzaIngredientSlotsArr[i].SetIngredientsSpr(pizzaIngredientSprArr[i + (nowPage * slotObjArr.Length) + 1]);
			}
		}
	}
	/// <summary>
	/// 피자 재료에 대한 설명
	/// </summary>
	/// <param name="ingNum"></param>
	public void IngredientExplain(int ingNum)
	{
		if (ingNum == 0)
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
	public void InitChoiceIngredient()
	{
		Constant.ChoiceIngredientList.Clear();
		for (int i = 0; i < choiceSlotArr.Length; i++)
		{
			choiceIngredientSlotArr[i].SetIngredientsSpr(pizzaIngredientSprArr[0]);
			choiceIngredientSlotArr[i].IngredientNumber = 0;
		}
	}
	public void ChoiceIngredient(int ingNum, int index)
	{
		// 재료를 추가
		if (index == -1)
		{
			if (Constant.ChoiceIngredientList.Count >= 10) { return; }

			Constant.ChoiceIngredientList.Add(ingNum);
		}
		// 재료를 제거
		else
		{
			if (Constant.ChoiceIngredientList.Count - 1 < index) { return; }

			Constant.ChoiceIngredientList.RemoveAt(index);
		}

		InitValue();

		for (int i = 0; i < choiceSlotArr.Length; i++)
		{
			if (Constant.ChoiceIngredientList.Count - 1 < i)
			{
				choiceIngredientSlotArr[i].SetIngredientsSpr(pizzaIngredientSprArr[0]);
				choiceIngredientSlotArr[i].IngredientNumber = 0;
			}
			else
			{
				choiceIngredientSlotArr[i].SetIngredientsSpr(pizzaIngredientSprArr[Constant.ChoiceIngredientList[i]]);
				choiceIngredientSlotArr[i].IngredientNumber = Constant.ChoiceIngredientList[i];

				attractiveness += int.Parse(Constant.IngredientsArray[Constant.ChoiceIngredientList[i], 1]);
				declineAt += int.Parse(Constant.IngredientsArray[Constant.ChoiceIngredientList[i], 2]);
				ingredientPrice += int.Parse(Constant.IngredientsArray[Constant.ChoiceIngredientList[i], 3]);
			}
		}

		Constant.PizzaAttractiveness = attractiveness;
		Constant.TotalDeclineAt = declineAt;
		Constant.ProductionCost = ingredientPrice;
		Constant.ingreds.Clear();
		for (int i = 0; i< Constant.ChoiceIngredientList.Count; i++)
		{
			Constant.ingreds.Add((Ingredient)Constant.ChoiceIngredientList[i]);
		}
		SetChoiceText(true);
	}

	private void SetChoiceText(bool bo)
	{
		if (bo)
		{
			choiceIngredientExplainText.text
			  = "총 매력도 : " + attractiveness + "\n"
			  + "총 매력하락도 : " + declineAt + "\n"
			  + "총 재료값 : " + ingredientPrice;
		}
		else
		{
			choiceIngredientExplainText.text = "";
		}
	}
	
}
