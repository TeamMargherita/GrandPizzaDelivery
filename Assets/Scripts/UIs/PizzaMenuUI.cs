using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PizzaNS;

// 한석호 작성

public class PizzaMenuUI : MonoBehaviour, IAddPizza
{
	[SerializeField] private GameObject[] addPizzaSlot;

	[SerializeField] private GameObject pizzaMenuListObj;
	[SerializeField] private GameObject menuObj;
	[SerializeField] private GameObject questionPanel;
	[SerializeField] private GameObject addPizzaPanel;
	[SerializeField] private GameObject changePizzaNamePanel;
	[SerializeField] private Text addPizzaExplainText;
	
	private List<int> openExplainList = new List<int>();
	private AddPizzaSlot[] addPizzaSlots;
	private GameObject[] pizzaMenuSlot;
	private Text[] pizzaMenuSlotText;
	private Text[] explainMenuSlotText;
	private Text[] addPizzaSlotText;

	private GridLayoutGroup gridLayoutGroup;
	private RectTransform pizzaMenuListRect;
	private int temIndex = -1;
	private int temSlotNumber = -1;
	private int nowPage = 0;
	private void Awake()
	{
		//for (int i = 0; i < 7; i++)
		//{
		//	List<Ingredient> ing = new List<Ingredient>();
		//	ing.Add(Ingredient.BACON);
		//	ing.Add(Ingredient.TOMATO);
		//	ing.Add(Ingredient.CHEESE);
		//	Constant.DevelopPizza.Add(new Pizza("SuperPizza", 60, 5000, 10000, Random.Range(0, 1500) + 500, ing, Random.Range(0, 1500) + 200));
		//}

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
		RefreshAllSlot();

		addPizzaSlotText = new Text[addPizzaSlot.Length];
		addPizzaSlots = new AddPizzaSlot[addPizzaSlot.Length];
		for (int i = 0; i < addPizzaSlotText.Length; i++)
		{
			addPizzaSlotText[i] = addPizzaSlot[i].transform.GetChild(0).GetComponent<Text>();
			addPizzaSlots[i] = addPizzaSlot[i].GetComponent<AddPizzaSlot>();
			addPizzaSlots[i].InitAddPizzaSlot(this);
		}
	}
	private void OnEnable()
	{
		RefreshAllSlot();
	}
	private void RefreshAllSlot()
	{
		InitSlot();
		for (int i = 0; i < GameManager.Instance.PizzaMenu.Count; i++)
		{
			if (i < pizzaMenuSlotText.Length)
			{
				SetSlot(GameManager.Instance.PizzaMenu[i], i);
			}
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
		explainMenuSlotText[temIndex].gameObject.transform.parent.gameObject.SetActive(false);
		openExplainList.Remove(temIndex);

		GameManager.Instance.PizzaMenu.RemoveAt(temIndex);
		RefreshAllSlot();
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

	public void AddMenu()
	{
		if (GameManager.Instance.PizzaMenu.Count >= 30) { return; }

		addPizzaPanel.SetActive(true);

		InitAddPizzaPage(0);
		SetTemSlotNumber(-1);
	}
	public void NextAddMenuList()
	{
		if (nowPage >= Constant.DevelopPizza.Count / addPizzaSlot.Length)
		{
			return;
		}
		else
		{
			nowPage++;
			InitAddPizzaPage(nowPage);
		}
	}
	public void BackAddMenuList()
	{
		if (nowPage <= 0)
		{
			return;
		}
		else
		{
			nowPage--;
			InitAddPizzaPage(nowPage);
		}
	}
	private void InitAddPizzaPage(int page)
	{
		nowPage = page;

		for (int i = 0 + page * addPizzaSlot.Length; i < addPizzaSlot.Length + page * addPizzaSlot.Length; i++)
		{
			if (i < Constant.DevelopPizza.Count)
			{
				addPizzaSlotText[i % addPizzaSlot.Length].text = Constant.DevelopPizza[i].Name;
				addPizzaSlots[i % addPizzaSlot.Length].SlotNumber = i;
			}
			else
			{
				addPizzaSlotText[i % addPizzaSlot.Length].text = "없음";
				addPizzaSlots[i % addPizzaSlot.Length].SlotNumber = -1;
			}
		}
	}
	public void CloseMenu()
	{
		for (int i = 0; i < addPizzaSlots.Length; i++)
		{
			addPizzaSlots[i].InitColor();
		}

		SetTemSlotNumber(-1);
		addPizzaPanel.SetActive(false);

	}
	public void SetAddPizzaExplain(int num)
	{
		if (num == - 1) { addPizzaExplainText.text = "";  return; }

		string str = "";
		for (int i = 0; i < Constant.DevelopPizza[num].Ingreds.Count; i++)
		{
			str += Constant.DevelopPizza[num].Ingreds[i].ToString() + ", ";
		}
		addPizzaExplainText.text = "피자이름 : " + Constant.DevelopPizza[num].Name 
								+ "\n피자매력도 : " + Constant.DevelopPizza[num].Charisma
								+ "\n피자가격 : " + Constant.DevelopPizza[num].SellCost
								+ "\n피자생산비용 : " + Constant.DevelopPizza[num].ProductionCost
								+ "\n피자 재료 : " + str;
	}

	public void SetTemSlotNumber(int num)
	{

		if (temSlotNumber != -1)
		{
			addPizzaSlots[temSlotNumber].InitColor();
		}
		temSlotNumber = num;
	}
	/// <summary>
	/// 개발한 피자의 이름을 바꿔줌
	/// </summary>
	/// <param name="str"></param>
	public void ChangeDevelopPizzaName(string str)
	{
		if (temSlotNumber == -1) { return; }
		// 개발한 피자들 이름의 메뉴 사용 못하게 방지
		for (int i = 0; i < Constant.DevelopPizza.Count; i++)
        {
			if (Constant.DevelopPizza[i].Name.Equals(str))
            {
				// 경고창 떠야됨
				return;
            }
        }

		Constant.DevelopPizza[temSlotNumber] = new Pizza
			(str,
			Constant.DevelopPizza[temSlotNumber].Perfection,
			Constant.DevelopPizza[temSlotNumber].ProductionCost,
			Constant.DevelopPizza[temSlotNumber].SellCost,
			Constant.DevelopPizza[temSlotNumber].Charisma,
			Constant.DevelopPizza[temSlotNumber].Ingreds,
			Constant.DevelopPizza[temSlotNumber].TotalDeclineAt);

		InitAddPizzaPage(nowPage);
		changePizzaNamePanel.SetActive(false);
	}
	/// <summary>
	/// 개발한 피자를 메뉴에 추가한다.
	/// </summary>
	public void ChoiceDevelopPizza()
	{
		if (temSlotNumber == -1) { return; }
		// 중복체크해서 중복된이름이나 재료의 피자는 넣을수 없게 함
		for (int i = 0; i < GameManager.Instance.PizzaMenu.Count; i++)
        {
			if (GameManager.Instance.PizzaMenu[i].Name.Equals(Constant.DevelopPizza[temSlotNumber].Name) ||
				GameManager.Instance.PizzaMenu[i].Ingreds.CompareIngredientList(Constant.DevelopPizza[temSlotNumber].Ingreds))
            {
				// 경고창 떠야됨
				return;
            }
        }
		// 해당 피자와 같은 이름이거나, 같은 재료일 경우, 메뉴판에 최소 일주일이 지나야 추가할 수 있도록 함.
		foreach ( var key in Constant.menuDateDic.Keys)
        {
			// 해당 피자와 같은 이름이거나 같은 재료이지만 메뉴에는 없는 경우임
			if (key.Name == Constant.DevelopPizza[temSlotNumber].Name ||
				key.Ingreds.CompareIngredientList(Constant.DevelopPizza[temSlotNumber].Ingreds))
            {
				// 메뉴판에 해당 메뉴가 생긴 시점부터 최소 일주일은 있어야 추가가 가능함.
				if (Constant.menuDateDic[key] <= 7)
                {
					// 경고창 떠야됨
					return;
                }
            }
        }


		Constant.menuDateDic.Add(Constant.DevelopPizza[temSlotNumber], 0);
		GameManager.Instance.PizzaMenu.Add(Constant.DevelopPizza[temSlotNumber]);

		RefreshAllSlot();
	}
	public void OpenChangePizzaNamePanel()
	{
		if (temSlotNumber == -1) { return; }

		changePizzaNamePanel.SetActive(true);
	}
	/// <summary>
	/// 하루가 지나면 발동해야 하는 함수임
	/// </summary>
	private void OneDayMenuInit()
    {
		List<Pizza> p = new List<Pizza>();
		foreach (var key in Constant.menuDateDic.Keys)
        {
			Constant.menuDateDic[key]++;
			if (GameManager.Instance.PizzaMenu.FindIndex(a => a.Name == key.Name) == -1 &&
				GameManager.Instance.PizzaMenu.FindIndex(a => a.Ingreds.CompareIngredientList(key.Ingreds)) == -1)
            {
				p.Add(key);
            }
        }
		for (int i = 0; i < p.Count; i++)
        {
			Constant.menuDateDic.Remove(p[i]);
        }
    }
}
