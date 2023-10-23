using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PizzaNS;
using ClerkNS;
using StoreNS;

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
	public static string[,] IngredientsArray = new string[13, 4]
	{
		{"0","-1","-1","-1" },	// 없음
		{"1","25","3","150" },	// 토마토
		{"2","30","2","160" },	// 치즈
		{"3","15","2","80" },	// 바질
		{"4","20","1","120" },	// 감자
		{"5","45","7","500" },	// 베이컨
		{"6","27","3","140" },	// 옥수수
		{"7","40","5","320" },	// 할라피뇨
		{"8","65","12","960" },	// 닭고기
		{"9","78","20","1350" },    // 소고기
		{"10","32","4","150" }, // 사과
		{"11","27","2","200" }, // 당근
		{"12","17","1","100" }	// 대파
	};
	/// <summary>
	/// 사용 가능한 피자 재료의 번호들
	/// </summary>
	public static List<int> UsableIngredient = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
	/// <summary>
	/// 개발한 피자 리스트
	/// </summary>
	public static List<Pizza> DevelopPizza = new List<Pizza>();
	
	public static bool IsMakePizza = false;
	/// <summary>
	/// 소지한 파인애플 개수
	/// </summary>
	public static int PineappleCount = 0;
	/// <summary>
	/// 파인애플 피자
	/// </summary>
	public static Pizza PineapplePizza = new Pizza("PineapplePizza", 100, 0, 2000000, 99999, new List<Ingredient>() { Ingredient.TOMATO, Ingredient.CHEESE, Ingredient.PINEAPPLE }, 0);
	/// <summary>
	/// 0.02초간의 텀
	/// </summary>
	public static WaitForSeconds OneTime = new WaitForSeconds(0.02f);
	/// <summary>
	/// 고용한 점원 리스트
	/// </summary>
	public static List<ClerkC> ClerkList = new List<ClerkC>();
	/// <summary>
	/// 플레이어가 소지한 아이템. ItemS의 ItemType으로 아이템들을 분류할 수 있다. int는 소지 개수
	/// </summary>
	public static Dictionary<ItemS, int> PlayerItemDIc = new Dictionary<ItemS, int>();
	/// <summary>
	/// 같은 타입의 아이템을 담은 딕셔너리만을 찾아서 Dictionary<ItemS, int> 형으로 리턴한다.
	/// </summary>
	/// <param name="dic"></param>
	/// <param name="type"></param>
	/// <returns></returns>
	public static Dictionary<ItemS, int> FindAllItemS(this Dictionary<ItemS, int> dic, ItemType type)
	{
		Dictionary<ItemS, int> dictionary = new Dictionary<ItemS, int>();
		foreach (var key in dic.Keys)
		{
			if (key.Type == type)
			{
				dictionary.Add(key, dic[key]);
			}
		}

		return dictionary;
	}
	/// <summary>
	/// 인덱스의 존재 여부를 확인하는 확장메서드
	/// </summary>
	/// <param name="dic"></param>
	/// <param name="index"></param>
	/// <returns></returns>
	public static bool CheckIndexDic(this Dictionary<ItemS, int> dic, int index)
	{
		if (dic.Count > index) { return true; }
		else { return false; }
	}
	/// <summary>
	/// 인덱스에 맞는 키를 찾는 확장 메서드
	/// </summary>
	/// <param name="dic"></param>
	/// <param name="index"></param>
	/// <returns></returns>
	public static ItemS? FindKeyForIndex(this Dictionary<ItemS, int> dic, int index)
	{
		if (dic.Count <= index) { return null; }
		int n = 0;
		foreach (var key in dic.Keys)
		{
			if (index == n) { return key; }
			n++;
		}
		return null;
	}
	public static ItemS[] DiceItem = new ItemS[10]
	{
		new ItemS(ItemType.DICE, 1, "고무 주사위", "고무로 만든 주사위다. \n 주사위 각 면은 0,1,2,3,4,5 을 상징한다.", 0),
		new ItemS(ItemType.DICE, 1, "금속 주사위", "금속으로 만든 주사위다. \n 주사위 각 면은 3,4,5,6,7,8 을 상징한다.", 1),
		new ItemS(ItemType.DICE, 1, "8면 주사위", "8면으로 된 주사위다. \n 주사위 각 면은 2,2,3,3,4,4,5,6 을 상징한다.", 2),
		new ItemS(ItemType.DICE, 1, "12면 주사위", "12면으로 된 주사위다. \n 주사위 각 면은 \n1,2,3,3,4,4,5,5,6,7,8,9 을 상징한다.", 3),
		new ItemS(ItemType.DICE, 1, "짝수 주사위", "짝수만 존재하는 주사위다. \n 주사위 각 면은 2,2,4,4,6,6 을 상징한다.", 4),
		new ItemS(ItemType.DICE, 1, "홀수 주사위", "홀수만 존재하는 주사위다. \n 주사위 각 면은 1,1,3,3,5,5 을 상징한다.", 5),
		new ItemS(ItemType.DICE, 1, "소수 주사위", "소수만 존재하는 주사위다. \n 주사위 각 면은 2,3,5,7,11,13 을 상징한다.", 6),
		new ItemS(ItemType.DICE, 1, "흑백 주사위", "숫자가 둘 뿐인 주사위다. \n 주사위 각 면은 1,1,1,1,1,15 을 상징한다.", 7),
		new ItemS(ItemType.DICE, 1, "나무 주사위", "나무로 만든 주사위다. \n 주사위 각 면은 2,3,4,5,6,7 을 상징한다.", 8),
		new ItemS(ItemType.DICE, 1, "플라스틱 주사위", "플라스틱으로 만든 주사위다. \n 주사위 각 면은 1,2,3,4,5,6 을 상징한다.", 9)
	};

	public static DiceS[] DiceInfo = new DiceS[10]
	{
		new DiceS(6, new int[6] { 0, 1, 2, 3, 4, 5} , "UI/MiniDice_80_80"),
		new DiceS(6, new int[6] { 3, 4, 5, 6, 7, 8} , "UI/MiniDice_80_80"),
		new DiceS(8, new int[8] { 2, 2, 3, 3, 4, 4, 5, 6} , "UI/MiniDice_80_80"),
		new DiceS(12, new int[12] { 1, 2, 3, 3, 4, 4, 5, 5, 6, 7, 8, 9} , "UI/MiniDice_80_80"),
		new DiceS(6, new int[6] { 2, 2, 4, 4, 6, 6} , "UI/MiniDice_80_80"),
		new DiceS(6, new int[6] { 1, 1, 3, 3, 5, 5} , "UI/MiniDice_80_80"),
		new DiceS(6, new int[6] { 2, 3, 5, 7, 11, 13} , "UI/MiniDice_80_80"),
		new DiceS(6, new int[6] { 1, 1, 1, 1, 1, 15} , "UI/MiniDice_80_80"),
		new DiceS(6, new int[6] { 2, 3, 4, 5, 6, 7} , "UI/MiniDice_80_80"),
		new DiceS(6, new int[6] { 1, 2, 3, 4, 5, 6} , "UI/MiniDice_80_80")
	};

	public static int[] nowDice = new int[2] { 9, 9 };

	public static ItemS[] GunItem = new ItemS[8]
	{
		new ItemS(ItemType.GUN, 1, "Glick 19","장전방식 : 반자동 \n연사속도 : 중간 \n대미지 : 약함 \n명중률 : ", 0),
		new ItemS(ItemType.GUN, 1, "S&U m500","장전방식 : 수동 \n연사속도 : 느림 \n대미지 : 강함 \n명중률 : ", 1),
		new ItemS(ItemType.GUN, 1, "Mi1911","장전방식 : 반자동 \n연사속도 : 조금느림 \n대미지 : 매우약함 \n명중률 : ", 2),
		new ItemS(ItemType.GUN, 1, "MiP9","장전방식 : 자동 \n연사속도 : 매우빠름 \n대미지 : 약함 \n명중률 : ", 3),
		new ItemS(ItemType.GUN, 1, "MiPX","장전방식 : 자동 \n연사속도 : 빠름 \n대미지 : 중간 \n명중률 : ", 4),
		new ItemS(ItemType.GUN, 1, "Pi90","장전방식 : 자동 \n연사속도 : 빠름 \n대미지 : 중간 \n명중률 : ", 5),
		new ItemS(ItemType.GUN, 1, "Kress Victor","장전방식 : 자동 \n연사속도 : 매우매우빠름 \n대미지 : 약함 \n명중률 : ", 6),
		new ItemS(ItemType.GUN, 1, "Thimpson SMG","장전방식 : 자동 \n연사속도 : 빠름 \n대미지 : 매우약함 \n명중률 : ", 7)
	};
	/// <summary>
	/// 연사속도 - 10~19 : 매우느림 - 20~29 : 느림 - 30~39 : 조금 느림 - 40~55 : 중간 - 56~ 64 : 조금 빠름 - 65~79 : 빠름 - 80~89 : 매우 빠름 - 90~ : 매우매우빠름
	/// 대미지 - 10~19 : 매우 약함 - 20~29 : 약함 - 30~39 : 조금 약함 -40~55 : 중간 - 56~74 : 조금 강함 - 75~85 : 강함 - 86~ : 매우 강함
	/// </summary>
	public static GunS[] GunInfo = new GunS[8]
	{
		new GunS(LoadEnum.SEMIAUTO, 50, 20, 100, "UI/Glick19_240_120"),
		new GunS(LoadEnum.MANUAL, 20, 80, 100, "UI/S&Um500_240_120"),
		new GunS(LoadEnum.SEMIAUTO, 30, 10, 100, "UI/Mi1911_240_120"),
		new GunS(LoadEnum.AUTO, 80, 20, 100, "UI/MiP9_240_120"),
		new GunS(LoadEnum.AUTO, 65, 50, 100, "UI/MiPX_240_120"),
		new GunS(LoadEnum.AUTO, 65, 50, 100, "UI/Pi90_240_120"),
		new GunS(LoadEnum.AUTO, 90, 20, 100, "UI/KressVictor_240_120"),
		new GunS(LoadEnum.AUTO, 65, 10, 100, "UI/ThimpsonSMG_240_120")
	};
	/// <summary>
	/// 현재 착용한 총. -1은 미착용 상태를 의미함.
	/// </summary>
	public static int[] nowGun = new int[1] { -1 };
}
