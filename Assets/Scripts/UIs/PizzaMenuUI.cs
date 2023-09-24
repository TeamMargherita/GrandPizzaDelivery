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
		for (int i = 0; i < 5; i++)
		{
			List<Ingredient> ing = new List<Ingredient>();
			ing.Add(Ingredient.CHEESE);
			GameManager.Instance.PizzaMenu.Add(new Pizza("CheesePizza5", 60, 5000, 10000, Random.Range(0, 500) + 500, ing, Random.Range(0, 100) + 200));
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
		for (int i = 0; i < GameManager.Instance.PizzaMenu.Count; i++)
		{
			if (i < pizzaMenuSlotText.Length)
			{
				SetSlot(GameManager.Instance.PizzaMenu[i], i);
			}
		}
		ReSize();
	}

	private void ReSize()
	{
		int n = GameManager.Instance.PizzaMenu.Count * 160 + openExplainList.Count * 400;
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
		pizzaMenuSlotText[index].text = $"<size=50>{pizza.Name}</size>\n<size=40>{pizza.SellCost} 원</size>";

		string str = "";
		for (int i = 0; i < pizza.Ingreds.Count; i++)
		{
			str += pizza.Ingreds[i].ToString() + " ,";
		}
		explainMenuSlotText[index].text
			= $"<size=35>매력도 : {pizza.Charisma}\n매력하락도 : {pizza.TotalDeclineAt}\n생산비용 : {pizza.ProductionCost}\n</size>"
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
		GameManager.Instance.PizzaMenu.RemoveAt(temIndex);
		InitSlot();

		for (int i = 0; i < GameManager.Instance.PizzaMenu.Count; i++)
		{
			if (i < pizzaMenuSlotText.Length)
			{
				SetSlot(GameManager.Instance.PizzaMenu[i], i);
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
