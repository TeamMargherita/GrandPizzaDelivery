using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PizzaNS;
using UnityEngine.UI;

// 한석호 작성
public class PizzaIngredientSlots : MonoBehaviour
{
    public int SlotNumber { get; set; }
	public int IngredientNumber { get; set; }
	public Sprite IngredientSprtie { get; set; } 
	private IngredientS ingredientS;
    public void SetIngredients(IngredientS ingredientS)
	{
		this.ingredientS = ingredientS;
	}
}
