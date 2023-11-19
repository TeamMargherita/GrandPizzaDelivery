using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// �Ѽ�ȣ �ۼ�
public class MakingPizzaPanel : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private PlayerMove playerMove;
	[SerializeField] private RectTransform mainPanelRect;
	[SerializeField] private Text backText;
	[SerializeField] private Text mainText;

	public MakingPizza MakingPizza { get; set; }
	public int PizzaIndex { get; set; }
	public bool isComplete { get; set; }
	private Pizza temPizza = new Pizza();	// �ӽ÷� ������ ����

	/// <summary>
	/// ���ڸ� �ӽ÷� �����մϴ�.
	/// </summary>
	/// <param name="pizza"></param>

	public void SetPizza(Pizza pizza)
	{
		temPizza = pizza;
		backText.text = temPizza.Name;
		mainText.text = temPizza.Name;
		//Debug.Log("�۵� 1");
	}

	public Pizza GetPizza()
    {
		return temPizza;
    }
	/// <summary>
	/// ���ڸ� ���մϴ�. ���� ���ٸ� true, Ʋ���ٸ� false�� ��ȯ�մϴ�.
	/// </summary>
	/// <param name="pizza"></param>
	/// <returns></returns>
	public bool ComparePizza(Pizza pizza)
	{
		if (temPizza.Ingreds == null) { return false; }
		// ����ü �ȿ� ����Ʈ ����־ equals�� �ȸ����� ������ ���ؾߵ� �ƿ�.
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
	/// ������ ���� ������� �����մϴ�.
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
