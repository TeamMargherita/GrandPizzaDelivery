using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PizzaNS;

// 한석호 작성

public class PizzaMenuUI : MonoBehaviour
{
	[SerializeField] private GameObject pizzaMenuListObj;
	[SerializeField] private GameObject menuObj;
	[SerializeField] private GameObject questionPanel;

	private List<int> openExplainList = new List<int>();
	private GameObject[] pizzaMenuSlot;
	private Text[] pizzaMenuSlotText;
	private Text[] explainMenuSlotText;

	private GridLayoutGroup gridLayoutGroup;
	private RectTransform pizzaMenuListRect;
	private int temIndex = -1;
	private void Awake()
	{
		Constant.PizzaMenuList.Add(new Pizza("CheesePizza1", 60, 5000, 10000, 1300));
		Constant.PizzaMenuList.Add(new Pizza("CheesePizza2", 60, 5000, 10000, 1200));
		Constant.PizzaMenuList.Add(new Pizza("CheesePizza3", 60, 5000, 10000, 1170));
		Constant.PizzaMenuList.Add(new Pizza("CheesePizza4", 60, 5000, 10000, 1070));
		Constant.PizzaMenuList.Add(new Pizza("CheesePizza5", 60, 5000, 10000, 970));

		for (int i = 0; i < Constant.PizzaMenuList.Count; i++)
		{
			List<Ingredient> ing = new List<Ingredient>();
			ing.Add(Ingredient.CHEESE);
			Constant.PizzaExplainMenuList.Add(new PizzaExplain(ing, Random.Range(0,100) + 200));
		}

		pizzaMenuSlot = new GameObject[pizzaMenuListObj.transform.childCount];
		pizzaMenuSlotText = new Text[pizzaMenuSlot.Length];
		explainMenuSlotText = new Text[pizzaMenuSlot.Length];

		gridLayoutGroup = pizzaMenuListObj.GetComponent<GridLayoutGroup>();
		pizzaMenuListRect = pizzaMenuListObj.GetComponent<RectTransform>();

		for (int i = 0; i < pizzaMenuSlot.Length; i++)
		{
			pizzaMenuSlot[i] = pizzaMenuListObj.transform.GetChild(i).gameObject;
			pizzaMenuSlotText[i] = pizzaMenuSlot[i].transform.GetChild(0).GetComponent<Text>();
			explainMenuSlotText[i] = pizzaMenuSlot[i].transform.GetChild(1).GetChild(0).GetComponent<Text>();
		}
		InitSlot();
		for (int i = 0; i < Constant.PizzaMenuList.Count; i++)
		{
			if (i < pizzaMenuSlotText.Length)
			{
				SetSlot(Constant.PizzaMenuList[i], i);
			}
		}
		ReSize();
	}

	private void ReSize()
	{
		int n = Constant.PizzaMenuList.Count * 160 + openExplainList.Count * 400;
		if (n > 1080)
		{
			pizzaMenuListRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, n);
		}
		else
		{
			pizzaMenuListRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1080);
		}
	}
	// 메뉴 초기화
	private void InitSlot()
	{
		for (int i = 0; i < pizzaMenuSlot.Length; i++)
		{
			pizzaMenuSlotText[i].text = "";
			explainMenuSlotText[i].text = "";
			pizzaMenuSlot[i].SetActive(false);
		}
	}
	// 메뉴를 넣어준다.
	private void SetSlot(Pizza pizza, int index)
	{
		pizzaMenuSlot[index].SetActive(true);
		pizzaMenuSlotText[index].text = $"<size=50>{Constant.PizzaMenuList[index].Name}</size>\n<size=40>{Constant.PizzaMenuList[index].SellCost} 원</size>";

		string str = "";
		for (int i = 0; i < Constant.PizzaExplainMenuList[index].Ingreds.Count; i++)
		{
			str += Constant.PizzaExplainMenuList[index].Ingreds[i].ToString() + " ,";
		}
		explainMenuSlotText[index].text
			= $"<size=35>매력도 : {Constant.PizzaMenuList[index].Charisma}\n매력하락도 : {Constant.PizzaExplainMenuList[index].TotalDeclineAt}\n생산비용 : {Constant.PizzaMenuList[index].ProductionCost}\n</size>"
			+ $"<size=30>들어간 재료 : {str}</size>";

	}
	public void RemoveQuestion(int index)
	{
		temIndex = index;
		questionPanel.SetActive(true);
	}
	public void CancelQuestion()
	{
		temIndex = -1;
		questionPanel.SetActive(false);
	}
	public void ReMovePizza()
	{
		Constant.PizzaExplainMenuList.RemoveAt(temIndex);
		Constant.PizzaMenuList.RemoveAt(temIndex);
		InitSlot();

		for (int i = 0; i < Constant.PizzaMenuList.Count; i++)
		{
			if (i < pizzaMenuSlotText.Length)
			{
				SetSlot(Constant.PizzaMenuList[i], i);
			}
		}
		ReSize();

		temIndex = -1;

		questionPanel.SetActive(false);
	}
	public void OnOffExplainPizza(int index)
	{
		if (!explainMenuSlotText[index].gameObject.transform.parent.gameObject.activeSelf)
		{
			gridLayoutGroup.enabled = false;
			explainMenuSlotText[index].gameObject.transform.parent.gameObject.SetActive(true);
			for (int i = index + 1; i < pizzaMenuSlot.Length; i++)
			{
				pizzaMenuSlot[i].GetComponent<RectTransform>().localPosition += new Vector3(0, -400);
			}
			openExplainList.Add(index);
		}
		else
		{
			explainMenuSlotText[index].gameObject.transform.parent.gameObject.SetActive(false);
			for (int i = index + 1; i < pizzaMenuSlot.Length; i++)
			{
				pizzaMenuSlot[i].GetComponent<RectTransform>().localPosition += new Vector3(0, 400);
			}
			openExplainList.Remove(index);
		}

		ReSize();

		if (openExplainList.Count == 0)
		{
			gridLayoutGroup.enabled = true;
		}

		if (menuObj.GetComponent<RectTransform>().localPosition.y >= 0)
		{
			pizzaMenuListRect.localPosition = new Vector3(0, pizzaMenuListRect.rect.height - 540);
		}
	}

}
