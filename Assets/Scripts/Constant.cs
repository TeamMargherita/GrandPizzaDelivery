using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PizzaNS;
using ClerkNS;

// 한석호 작성
public static class Constant
{
	/// <summary>
	/// 피자의 재료 번호 리스트. 중복되는 번호도 있다.
	/// </summary>
	public static List<int> ChoiceIngredientList = new List<int>();
	/// <summary>
	/// 피자의 매력도. 리듬게임 100% 정확도 기준일 때의 매력도이다. 정확도 깎이면 매력도도 깎임.
	/// </summary>
	public static int PizzaAttractiveness;
	public static int Perfection;
	public static int ProductionCost;
	public static int SellCost;
	public static int TotalDeclineAt;
	public static List<Ingredient> ingreds = new List<Ingredient>();
	/// <summary>
	/// 피자 재료값. [,0]은 재료번호, [,1]은 매력도, [,2]는 매력하락도, [,3]은 재료값. [0,]은 재료없음임.
	/// </summary>
	public static string[,] IngredientsArray = new string[10, 4]
	{
		{"0","-1","-1","-1" },
		{"1","25","3","150" },
		{"2","30","2","160" },
		{"3","15","2","80" },
		{"4","20","1","120" },
		{"5","45","7","500" },
		{"6","27","3","140" },
		{"7","40","5","320" },
		{"8","65","12","960" },
		{"9","78","20","1350" }
	};
	/// <summary>
	/// 개발한 피자 리스트
	/// </summary>
	public static List<Pizza> DevelopPizza = new List<Pizza>();

	public static bool IsMakePizza = false;
	/// <summary>
	/// 0.1초간의 텀
	/// </summary>
	public static WaitForSeconds OneTime = new WaitForSeconds(0.1f);
	/// <summary>
	/// 고용한 점원 리스트
	/// </summary>
	public static List<ClerkC> ClerkList = new List<ClerkC>();
	
}
