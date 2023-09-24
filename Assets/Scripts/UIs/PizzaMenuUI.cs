using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성
public class PizzaMenuUI : MonoBehaviour
{
	[SerializeField] private GameObject pizzaMenuListObj;

	private GameObject[] pizzaMenuSlot;
	private GridLayout gridLayout;

	private void Awake()
	{
		Constant.PizzaMenuList.Add(new Pizza("CheesePizza1", 60, 5000, 10000, 70));
		Constant.PizzaMenuList.Add(new Pizza("CheesePizza2", 60, 5000, 10000, 70));
		Constant.PizzaMenuList.Add(new Pizza("CheesePizza3", 60, 5000, 10000, 70));
		Constant.PizzaMenuList.Add(new Pizza("CheesePizza4", 60, 5000, 10000, 70));
		Constant.PizzaMenuList.Add(new Pizza("CheesePizza5", 60, 5000, 10000, 70));

		pizzaMenuSlot = new GameObject[pizzaMenuListObj.transform.childCount];

		for (int i = 0; i < pizzaMenuSlot.Length; i++)
		{
			pizzaMenuSlot[i] = pizzaMenuListObj.transform.GetChild(i).gameObject;
		}
	}




}
