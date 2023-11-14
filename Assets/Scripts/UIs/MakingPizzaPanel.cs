using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 한석호 작성
public class MakingPizzaPanel : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private PlayerMove playerMove;
	[SerializeField] private RectTransform mainPanelRect;
	[SerializeField] private Text backText;
	[SerializeField] private Text mainText;

	public MakingPizza MakingPizza { get; set; }
	public int PizzaIndex { get; set; }
	public bool isComplete { get; set; }
	private Pizza temPizza = new Pizza();	// 임시로 저장할 피자

	/// <summary>
	/// 피자를 임시로 저장합니다.
	/// </summary>
	/// <param name="pizza"></param>

	public void SetPizza(Pizza pizza)
	{
		temPizza = pizza;
		backText.text = temPizza.Name;
		mainText.text = temPizza.Name;
		//Debug.Log("작동 1");
	}

	public Pizza GetPizza()
    {
		return temPizza;
    }
	/// <summary>
	/// 피자를 비교합니다. 값이 같다면 true, 틀리다면 false를 반환합니다.
	/// </summary>
	/// <param name="pizza"></param>
	/// <returns></returns>
	public bool ComparePizza(Pizza pizza)
	{
		if (temPizza.Ingreds == null) { return false; }
		// 구조체 안에 리스트 들어있어서 equals가 안먹혀서 일일히 비교해야됨 아오.
		if (temPizza.Name.Equals(pizza.Name) && temPizza.Charisma == pizza.Charisma
			&& temPizza.ProductionCost == pizza.ProductionCost && temPizza.SellCost == pizza.SellCost &&
			temPizza.TotalDeclineAt == pizza.TotalDeclineAt && temPizza.Ingreds.Count == pizza.Ingreds.Count)
		{
			for (int i = 0; i < temPizza.Ingreds.Count; i++)
			{
				if (temPizza.Ingreds[i] != pizza.Ingreds[i])
				{
					return false;
				}
			}

			return true;
		}
		else
		{
			return false;
		}
	}
	/// <summary>
	/// 피자의 생산 진행률을 갱신합니다.
	/// </summary>
	/// <param name="percentage"></param>
	public void SetMainPanelRect(float percentage)
	{
		mainPanelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,600f * ((100 - percentage) / 100f));
		
		if (mainPanelRect.rect.width < 4f)
		{
			mainPanelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0f);
		}
	}
	public float GetMainPanelRect()
	{
		//Debug.Log(mainPanelRect.rect.width);
		return mainPanelRect.rect.width;
	}

    public void OnPointerClick(PointerEventData eventData)
    {
		playerMove.AddPizzaInven(PizzaIndex);
    }
}
