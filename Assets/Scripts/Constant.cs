using ClerkNS;
using DayNS;
using PizzaNS;
using PizzaNS.CustomerNS;
using StoreNS;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성
public static class Constant
{
    public static void InitConstant()
    {
        House.activeColor = new Color(248 / 255f, 70 / 255f, 6 / 255f);
        House.CustomerSDic = new Dictionary<int, Dictionary<int, CustomerS>>();
        House.nowDate = new Dictionary<int, Dictionary<int, int>>();

        Color lightOnColor = new Color(255 / 255f, 177 / 255f, 0 / 255f);
        Color lightOffColor = Color.black;

        EmployeeStressCon.nowDate = 0;
        EmployeeRecruit.nowDate = 0;
        EmployeeFire.WorkingDay = new Dictionary<int, List<ClerkC>>();

        PlayerStat.PlayerIsGod = false;
        PlayerStat.HP = PlayerStat.MaxHP;

        ChasePoliceCar.isStop = false;

        DiceStore.IsOneDayDiceStore = false;
        DiceStore.NowDate = 1;

        IngredientStore.Contract = 0;
        IngredientStore.Hint = false;
        IngredientStore.OneChance = true;

        IngredientStoreTwo.IsTalk = false;  // 가게 주인과 잡담을 한번이라도 했는지 여부
        IngredientStoreTwo.IsGalicQuest = false;    // 가게 주인의 마늘 고민을 한번이라도 들었는지 여부(마늘 고민 해결했는지 여부)
        IngredientStoreTwo.OneChanceGalicClear = false;  // 마늘 고민 해결 후 첫 대화 
        IngredientStoreTwo.NowDate = 1;
        IngredientStoreTwo.Ingredient = 0;
        IngredientStoreTwo.Discount = -1;
        IngredientStoreTwo.BounsDiscount = 0;
        IngredientStoreTwo.Contract = 0;

        LuckyStore.IsAngry = false;
        LuckyStore.IsLuckyTest = false;
        LuckyStore.ClearGalicQuest = false;
        LuckyStore.ClearSonQuest = false;
        LuckyStore.BigDicePlus = false;
        LuckyStore.BigDiceMinus = false;
        LuckyStore.SmallDicePlus = false;
        LuckyStore.SmallDiceMinus = false;
        LuckyStore.AngryDate = 0;
        LuckyStore.NowDate = 1;

        MoneyStore.IsTalk = false;  // 대출업체의 어머니에 관한 이야기를 했는지 여부
        MoneyStore.StartSonQuest = false;   // 가족 관련 퀘스트를 시작했는지 여부
        MoneyStore.OneChanceClearSon = false;   // 클리어 이후 첫 대사
        MoneyStore.IsTalkOneChanceDiscount = false; // 한번에 한해서 이자를 깎을 수 있음. true가 되면 할인 시도 이미 한 것.
        MoneyStore.SumBorrow = 0;    // 총 빌린 금액
        MoneyStore.NowDate = 1;  // 날짜
        MoneyStore.ClearMoney = 0;   // 퀘스트 성공시 받는 돈
        MoneyStore.loseMoney = 0;    // 오늘 하루 빌릴수 있는 돈

        MoneyStoreTwo.SumBorrow = 0;    // 총 빌린 금액
        MoneyStoreTwo.NowDate = 1;  // 날짜
        MoneyStoreTwo.loseMoney = 0;   // 오늘 하루 빌릴수 있는 돈

        PineAppleStoreTwo.isPineapple = true;
        PineAppleStoreTwo.isContract = false;
        PineAppleStoreTwo.isMeet = false;

        PineAppleStore.isFineapple = true;
        PineAppleStore.isFirstTime = true;

        FineMessage.minusColor = new Color(0, 0, 0, 0.1f / 255f);

        PineappleCount.nowDate = 0;

        EmployeeRecruit.RecruitClerk = new ClerkC[3];
        EmployeeRecruit.IsRecruited = new bool[3] { false, false, false };

        MakingPizza.pizzaRequestList = new List<Request>();   // 만들어야할 피자 리스트
        MakingPizza.CompletePizzaList = new List<Pizza>();  // 완성된 피자 리스트
        MakingPizza.IsSaveIndex = false;
        MakingPizza.nowDate = 0;
        //Map.nowDate = 0;
        Turorial.IsTuto = false;
        Turorial.IsEmployeeTuto = false;

        UIControl.isIn = false;

        Dept = 0;
        PayMoneyDate = PayMoneyDate = new Dictionary<int, Dictionary<int, int>>() { { 1, new Dictionary<int, int>() { { 1, 20000000 } } } };
        DeptMulitplex = new float[2] { 1.1f, 1.05f };
        MoneyStoreCode = new int[2] { 0, 1 };
        MoneyMaxBorrow = new int[2] { 50000000, 30000000 };
        ClerkMoney = 0;
        PizzaIngMoney = 0;
        Fine = 0;
        IsDead = false;
        NowDay = DayEnum.MONDAY;
        NowDate = 1;
        DiceBonus = 0;
        ChoiceIngredientList = new List<int>();
        ingreds = new List<Ingredient>();
        IngredientsArray = new string[16, 5]
    {
        {"0","-1","-1","-1" ,"없음"},	// 없음
		{"1","25","3","150","토마토" },	// 토마토
		{"2","30","2","160","치즈"},	// 치즈
		{"3","15","2","80","바질" },	// 바질
		{"4","20","1","120","감자" },	// 감자
		{"5","45","7","500","베이컨" },	// 베이컨
		{"6","27","3","140","옥수수" },	// 옥수수
		{"7","40","5","320","할라피뇨" },	// 할라피뇨
		{"8","65","12","960","닭고기" },	// 닭고기
		{"9","78","20","1350","소고기" },    // 소고기
		{"10","32","4","150","사과" }, // 사과
		{"11","27","2","200","당근" }, // 당근
		{"12","17","1","100","대파" },    // 대파
		{"13", "34", "7", "230", "마늘" },    // 마늘
		{"14", "28", "5", "170", "양파" },    // 양파
		{"15", "22", "1", "210", "고추" },	// 고추
	};
        UsableIngredient = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        DevelopPizza = new List<Pizza>();
        menuDateDic = new Dictionary<Pizza, int>();
        IsMakePizza = false;
        isStartGame = false;
        StopTime = false;
        PineAppleCount = 0;
        PineapplePizza = new Pizza("PineapplePizza", 100, 0, 2000000, 99999, new List<Ingredient>() { Ingredient.TOMATO, Ingredient.CHEESE, Ingredient.PINEAPPLE }, 0, 100, 0);
        OneTime = new WaitForSeconds(0.02f);
        ClerkList = new List<ClerkC>() { new ClerkC(47, Tier.THREE, Tier.ONE, Tier.FOUR, 0, 10000, "프레이야", null, 0) };
        DiceItem = new ItemS[10]
    {
        new ItemS(ItemType.DICE, 2, "고무 주사위", "고무로 만든 주사위다. \n 주사위 각 면은 0,1,2,3,4,5 을 상징한다.", 0),
        new ItemS(ItemType.DICE, 2, "금속 주사위", "금속으로 만든 주사위다. \n 주사위 각 면은 3,4,5,6,7,8 을 상징한다.", 1),
        new ItemS(ItemType.DICE, 2, "8면 주사위", "8면으로 된 주사위다. \n 주사위 각 면은 2,2,3,3,4,4,5,6 을 상징한다.", 2),
        new ItemS(ItemType.DICE, 2, "12면 주사위", "12면으로 된 주사위다. \n 주사위 각 면은 \n1,2,3,3,4,4,5,5,6,7,8,9 을 상징한다.", 3),
        new ItemS(ItemType.DICE, 2, "짝수 주사위", "짝수만 존재하는 주사위다. \n 주사위 각 면은 2,2,4,4,6,6 을 상징한다.", 4),
        new ItemS(ItemType.DICE, 2, "홀수 주사위", "홀수만 존재하는 주사위다. \n 주사위 각 면은 1,1,3,3,5,5 을 상징한다.", 5),
        new ItemS(ItemType.DICE, 2, "소수 주사위", "소수만 존재하는 주사위다. \n 주사위 각 면은 2,3,5,7,11,13 을 상징한다.", 6),
        new ItemS(ItemType.DICE, 2, "흑백 주사위", "숫자가 둘 뿐인 주사위다. \n 주사위 각 면은 1,1,1,1,1,15 을 상징한다.", 7),
        new ItemS(ItemType.DICE, 2, "나무 주사위", "나무로 만든 주사위다. \n 주사위 각 면은 2,3,4,5,6,7 을 상징한다.", 8),
        new ItemS(ItemType.DICE, 2, "플라스틱 주사위", "플라스틱으로 만든 주사위다. \n 주사위 각 면은 1,2,3,4,5,6 을 상징한다.", 9)
    };
        PlayerItemDIc = new Dictionary<ItemS, int>() { { DiceItem[9], 2 }, { GunItem[0], 1 } };
        DiceInfo = new DiceS[10]
    {
        new DiceS(6, new int[6] { 0, 1, 2, 3, 4, 5} , "UI/RubberDice_80_80"),
        new DiceS(6, new int[6] { 3, 4, 5, 6, 7, 8} , "UI/MetalDice_80_80"),
        new DiceS(8, new int[8] { 2, 2, 3, 3, 4, 4, 5, 6} , "UI/EightDice_80_80"),
        new DiceS(12, new int[12] { 1, 2, 3, 3, 4, 4, 5, 5, 6, 7, 8, 9} , "UI/TwelveDice_80_80"),
        new DiceS(6, new int[6] { 2, 2, 4, 4, 6, 6} , "UI/EvenDice_80_80"),
        new DiceS(6, new int[6] { 1, 1, 3, 3, 5, 5} , "UI/OddDice_80_80"),
        new DiceS(6, new int[6] { 2, 3, 5, 7, 11, 13} , "UI/PrimeDice_80_80"),
        new DiceS(6, new int[6] { 1, 1, 1, 1, 1, 15} , "UI/TwoDice_80_80"),
        new DiceS(6, new int[6] { 2, 3, 4, 5, 6, 7} , "UI/WoodDice_80_80"),
        new DiceS(6, new int[6] { 1, 2, 3, 4, 5, 6} , "UI/MiniDice_80_80")
    };
        nowDice = new int[2] { 9, 9 };
        GunItem = new ItemS[8]
    {
        new ItemS(ItemType.GUN, 1, "Glick 19","연사속도 : 중간 \n대미지 : 약함 \n장탄수 : 15\n재장전시간 : 3", 0),
        new ItemS(ItemType.GUN, 1, "S&U m500","연사속도 : 느림 \n대미지 : 강함 \n장탄수 : 6\n재장전시간 : 3", 1),
        new ItemS(ItemType.GUN, 1, "Mi1911","연사속도 : 조금느림 \n대미지 : 매우약함 \n장탄수 : 7\n재장전시간 : 3", 2),
        new ItemS(ItemType.GUN, 1, "MiP9","연사속도 : 매우빠름 \n대미지 : 약함 \n장탄수 : 30\n재장전시간 : 3", 3),
        new ItemS(ItemType.GUN, 1, "MiPX","연사속도 : 빠름 \n대미지 : 중간 \n장탄수 : 30\n재장전시간 : 3", 4),
        new ItemS(ItemType.GUN, 1, "Pi90","연사속도 : 빠름 \n대미지 : 중간 \n장탄수 : 50\n재장전시간 : 3", 5),
        new ItemS(ItemType.GUN, 1, "Kress Victor","연사속도 : 매우매우빠름 \n대미지 : 약함 \n장탄수 : 30\n재장전시간 : 3", 6),
        new ItemS(ItemType.GUN, 1, "Thimpson SMG","연사속도 : 빠름 \n대미지 : 매우약함 \n장탄수 : 30\n재장전시간 : 3", 7)
    };
        GunInfo = new GunS[8]
   {
        new GunS(LoadEnum.SEMIAUTO, 0.5f, 20, 100, 15, 3, "UI/Glick19_240_120"),
        new GunS(LoadEnum.MANUAL, 0.1f, 150, 100, 6, 3, "UI/S&Um500_240_120"),
        new GunS(LoadEnum.SEMIAUTO, 0.3f, 10, 100, 7, 3, "UI/Mi1911_240_120"),
        new GunS(LoadEnum.AUTO, 0.8f, 20, 100, 30, 3, "UI/MiP9_240_120"),
        new GunS(LoadEnum.AUTO, 0.65f, 50, 100, 30, 3, "UI/MiPX_240_120"),
        new GunS(LoadEnum.AUTO, 0.65f, 50, 100, 50, 3, "UI/Pi90_240_120"),
        new GunS(LoadEnum.AUTO, 0.9f, 20, 100, 30, 3, "UI/KressVictor_240_120"),
        new GunS(LoadEnum.AUTO, 0.65f, 10, 100, 30, 3, "UI/ThimpsonSMG_240_120")
   };
        NowGun = new int[1] { 0 };
        IsHospital = false;
        pizzaStoreStar = 1.0f;
        TemAddress = new int[5] { -1, -1, -1, -1, -1 };
    }
    /// <summary>
    /// 빚
    /// </summary>
    public static int Dept = 0;
    /// <summary>
    /// 돈빌린 날짜, 갚을 금액, 대출업체 코드
    /// </summary>
    public static Dictionary<int, Dictionary<int, int>> PayMoneyDate = new Dictionary<int, Dictionary<int, int>>()
    { { 1, new Dictionary<int, int>() { { 1, 20000000 } } } };
    /// <summary>
    /// 대출업체별 이자 배율(매일 복리)
    /// </summary>
    public static float[] DeptMulitplex = new float[2] { 1.1f, 1.05f };
    /// <summary>
    /// 대출업체 코드
    /// </summary>
    public static int[] MoneyStoreCode = new int[2] { 0, 1 };
    /// <summary>
    /// 대출업체에서 빌릴 수 있는 최대 금액
    /// </summary>
    public static int[] MoneyMaxBorrow = new int[2] { 50000000, 30000000 };
    /// <summary>
    /// 강제로 돈을 빌리러 오는지 여부
    /// </summary>
    public static bool[] MoneyConfiscated = new bool[2] { true, false };
    /// <summary>
    /// 대출업체 이름
    /// </summary>
    public static string[] MoneyStoreName = new string[2] { "미소", "행복" };
    /// <summary>
    /// 하루 동안 점원에게 쓴 비용
    /// </summary>
    public static int ClerkMoney = 0;
    /// <summary>
    /// 하루 동안 피자 만드는 데 쓴 비용
    /// </summary>
    public static int PizzaIngMoney = 0;
    /// <summary>
    /// 벌금
    /// </summary>
    public static int Fine = 0;
    /// <summary>
    /// 죽었는지 여부
    /// </summary>
    public static bool IsDead = false;
    /// <summary>
    /// 현재 요일
    /// </summary>
    public static DayEnum NowDay = DayEnum.MONDAY;
    /// <summary>
    /// 현재 일수
    /// </summary>
    public static int NowDate = 1;
    /// <summary>
    /// 주사위 보너스
    /// </summary>
    public static short DiceBonus = 0;
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
    /// 피자 재료값. [,0]은 재료번호, [,1]은 매력도, [,2]는 매력하락도, [,3]은 재료값, [,4]은 재료이름 [0,]은 재료없음임.
    /// </summary>
    public static string[,] IngredientsArray = new string[16, 5]
    {
        {"0","-1","-1","-1" ,"없음"},	// 없음
		{"1","25","3","150","토마토" },	// 토마토
		{"2","30","2","160","치즈"},	// 치즈
		{"3","15","2","80","바질" },	// 바질
		{"4","20","1","120","감자" },	// 감자
		{"5","45","7","500","베이컨" },	// 베이컨
		{"6","27","3","140","옥수수" },	// 옥수수
		{"7","40","5","320","할라피뇨" },	// 할라피뇨
		{"8","65","12","960","닭고기" },	// 닭고기
		{"9","78","20","1350","소고기" },    // 소고기
		{"10","32","4","150","사과" }, // 사과
		{"11","27","2","200","당근" }, // 당근
		{"12","17","1","100","대파" },    // 대파
		{"13", "34", "7", "230", "마늘" },    // 마늘
		{"14", "28", "5", "170", "양파" },    // 양파
		{"15", "22", "1", "210", "고추" },	// 고추
	};
    /// <summary>
    /// 사용 가능한 피자 재료의 번호들
    /// </summary>
    public static List<int> UsableIngredient = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    /// <summary>
    /// 개발한 피자 리스트
    /// </summary>
    public static List<Pizza> DevelopPizza = new List<Pizza>();
    /// <summary>
    /// 피자가 메뉴판에 있었던 시간
    /// </summary>
    public static Dictionary<Pizza, int> menuDateDic = new Dictionary<Pizza, int>();
    public static bool IsMakePizza = false;
    public static bool isStartGame = false;
    public static bool StopTime = false;
    /// <summary>
    /// 소지한 파인애플 개수
    /// </summary>
    public static int PineAppleCount = 0;
    /// <summary>
    /// 파인애플 피자
    /// </summary>
    public static Pizza PineapplePizza = new Pizza("PineapplePizza", 100, 0, 2000000, 99999, new List<Ingredient>() { Ingredient.TOMATO, Ingredient.CHEESE, Ingredient.PINEAPPLE }, 0, 100, 0);
    /// <summary>
    /// 0.02초간의 텀
    /// </summary>
    public static WaitForSeconds OneTime = new WaitForSeconds(0.02f);
    /// <summary>
    /// 고용한 점원 리스트
    /// </summary>
    public static List<ClerkC> ClerkList = new List<ClerkC>() { new ClerkC(47, Tier.THREE, Tier.ONE, Tier.FOUR, 0, 20000, "프레이야", null, 0) };

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
    /// <summary>
    /// 두 리스트의 재료를 비교하는 확장 메서드
    /// </summary>
    /// <param name="list"></param>
    /// <param name="one"></param>
    /// <returns></returns>
    public static bool CompareIngredientList(this List<Ingredient> list, List<Ingredient> one)
    {
        if (one.Count != list.Count) { return false; }

        int index = -1;

        List<Ingredient> two = new List<Ingredient>();

        for (int i = 0; i < one.Count; i++)
        {
            two.Add(one[i]);
        }

        for (int i = 0; i < list.Count; i++)
        {
            index = two.FindIndex(a => a.Equals(list[i]));
            if (index == -1)
            {
                return false;
            }
            else
            {
                two.RemoveAt(index);
            }
        }
        if (two.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }

        return false;
    }
    public static ItemS[] DiceItem = new ItemS[10]
    {
        new ItemS(ItemType.DICE, 2, "고무 주사위", "고무로 만든 주사위다. \n 주사위 각 면은 0,1,2,3,4,5 을 상징한다.", 0),
        new ItemS(ItemType.DICE, 2, "금속 주사위", "금속으로 만든 주사위다. \n 주사위 각 면은 3,4,5,6,7,8 을 상징한다.", 1),
        new ItemS(ItemType.DICE, 2, "8면 주사위", "8면으로 된 주사위다. \n 주사위 각 면은 2,2,3,3,4,4,5,6 을 상징한다.", 2),
        new ItemS(ItemType.DICE, 2, "12면 주사위", "12면으로 된 주사위다. \n 주사위 각 면은 \n1,2,3,3,4,4,5,5,6,7,8,9 을 상징한다.", 3),
        new ItemS(ItemType.DICE, 2, "짝수 주사위", "짝수만 존재하는 주사위다. \n 주사위 각 면은 2,2,4,4,6,6 을 상징한다.", 4),
        new ItemS(ItemType.DICE, 2, "홀수 주사위", "홀수만 존재하는 주사위다. \n 주사위 각 면은 1,1,3,3,5,5 을 상징한다.", 5),
        new ItemS(ItemType.DICE, 2, "소수 주사위", "소수만 존재하는 주사위다. \n 주사위 각 면은 2,3,5,7,11,13 을 상징한다.", 6),
        new ItemS(ItemType.DICE, 2, "흑백 주사위", "숫자가 둘 뿐인 주사위다. \n 주사위 각 면은 1,1,1,1,1,15 을 상징한다.", 7),
        new ItemS(ItemType.DICE, 2, "나무 주사위", "나무로 만든 주사위다. \n 주사위 각 면은 2,3,4,5,6,7 을 상징한다.", 8),
        new ItemS(ItemType.DICE, 2, "플라스틱 주사위", "플라스틱으로 만든 주사위다. \n 주사위 각 면은 1,2,3,4,5,6 을 상징한다.", 9)
    };

    public static DiceS[] DiceInfo = new DiceS[10]
    {
        new DiceS(6, new int[6] { 0, 1, 2, 3, 4, 5} , "UI/RubberDice_80_80"),
        new DiceS(6, new int[6] { 3, 4, 5, 6, 7, 8} , "UI/MetalDice_80_80"),
        new DiceS(8, new int[8] { 2, 2, 3, 3, 4, 4, 5, 6} , "UI/EightDice_80_80"),
        new DiceS(12, new int[12] { 1, 2, 3, 3, 4, 4, 5, 5, 6, 7, 8, 9} , "UI/TwelveDice_80_80"),
        new DiceS(6, new int[6] { 2, 2, 4, 4, 6, 6} , "UI/EvenDice_80_80"),
        new DiceS(6, new int[6] { 1, 1, 3, 3, 5, 5} , "UI/OddDice_80_80"),
        new DiceS(6, new int[6] { 2, 3, 5, 7, 11, 13} , "UI/PrimeDice_80_80"),
        new DiceS(6, new int[6] { 1, 1, 1, 1, 1, 15} , "UI/TwoDice_80_80"),
        new DiceS(6, new int[6] { 2, 3, 4, 5, 6, 7} , "UI/WoodDice_80_80"),
        new DiceS(6, new int[6] { 1, 2, 3, 4, 5, 6} , "UI/MiniDice_80_80")
    };

    public static int[] nowDice = new int[2] { 9, 9 };

    public static ItemS[] GunItem = new ItemS[8]
    {
        new ItemS(ItemType.GUN, 1, "Glick 19","연사속도 : 중간 \n대미지 : 약함 \n장탄수 : 15\n재장전시간 : 3", 0),
        new ItemS(ItemType.GUN, 1, "S&U m500","연사속도 : 느림 \n대미지 : 강함 \n장탄수 : 6\n재장전시간 : 3", 1),
        new ItemS(ItemType.GUN, 1, "Mi1911","연사속도 : 조금느림 \n대미지 : 매우약함 \n장탄수 : 7\n재장전시간 : 3", 2),
        new ItemS(ItemType.GUN, 1, "MiP9","연사속도 : 매우빠름 \n대미지 : 약함 \n장탄수 : 30\n재장전시간 : 3", 3),
        new ItemS(ItemType.GUN, 1, "MiPX","연사속도 : 빠름 \n대미지 : 중간 \n장탄수 : 30\n재장전시간 : 3", 4),
        new ItemS(ItemType.GUN, 1, "Pi90","연사속도 : 빠름 \n대미지 : 중간 \n장탄수 : 50\n재장전시간 : 3", 5),
        new ItemS(ItemType.GUN, 1, "Kress Victor","연사속도 : 매우매우빠름 \n대미지 : 약함 \n장탄수 : 30\n재장전시간 : 3", 6),
        new ItemS(ItemType.GUN, 1, "Thimpson SMG","연사속도 : 빠름 \n대미지 : 매우약함 \n장탄수 : 30\n재장전시간 : 3", 7)
    };
    /// <summary>
    /// 연사속도 - 10~19 : 매우느림 - 20~29 : 느림 - 30~39 : 조금 느림 - 40~55 : 중간 - 56~ 64 : 조금 빠름 - 65~79 : 빠름 - 80~89 : 매우 빠름 - 90~ : 매우매우빠름
    /// 대미지 - 10~19 : 매우 약함 - 20~29 : 약함 - 30~39 : 조금 약함 -40~55 : 중간 - 56~74 : 조금 강함 - 75~85 : 강함 - 86~ : 매우 강함
    /// </summary>
    public static GunS[] GunInfo = new GunS[8]
    {
        new GunS(LoadEnum.SEMIAUTO, 0.5f, 20, 100, 15, 3, "UI/Glick19_240_120"),
        new GunS(LoadEnum.MANUAL, 0.1f, 150, 100, 6, 3, "UI/S&Um500_240_120"),
        new GunS(LoadEnum.SEMIAUTO, 0.3f, 10, 100, 7, 3, "UI/Mi1911_240_120"),
        new GunS(LoadEnum.AUTO, 0.8f, 20, 100, 30, 3, "UI/MiP9_240_120"),
        new GunS(LoadEnum.AUTO, 0.65f, 50, 100, 30, 3, "UI/MiPX_240_120"),
        new GunS(LoadEnum.AUTO, 0.65f, 50, 100, 50, 3, "UI/Pi90_240_120"),
        new GunS(LoadEnum.AUTO, 0.9f, 20, 100, 30, 3, "UI/KressVictor_240_120"),
        new GunS(LoadEnum.AUTO, 0.65f, 10, 100, 30, 3, "UI/ThimpsonSMG_240_120")
    };
    /// <summary>
    /// 플레이어가 소지한 아이템. ItemS의 ItemType으로 아이템들을 분류할 수 있다. int는 소지 개수
    /// </summary>
    public static Dictionary<ItemS, int> PlayerItemDIc = new Dictionary<ItemS, int>() { { DiceItem[9], 2 }, { GunItem[0], 1 } };

    /// <summary>
    /// 현재 착용한 총. -1은 미착용 상태를 의미함.
    /// </summary>
    public static int[] NowGun = new int[1] { 0 };
    /// <summary>
    /// 병원에서 부활해야하는 지 여부
    /// </summary>
    public static bool IsHospital = false;

    private static float pizzaStoreStar = 1.0f;
    /// <summary>
    /// 피자 가게 평점
    /// </summary>
    public static float PizzaStoreStar
    {
        get
        {
            return pizzaStoreStar;
        }
        set
        {
            if (value >= 5)
            {
                pizzaStoreStar = 5.0f;
            }
            else if (value < 0)
            {
                pizzaStoreStar = 0.0f;
            }
            else
            {
                pizzaStoreStar = value;
            }
        }
    }

    public static int[] TemAddress = new int[5] { -1, -1, -1, -1, -1 };

}
