using ClerkNS;
using DayNS;
using PizzaNS;
using PizzaNS.CustomerNS;
using StoreNS;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�
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

        IngredientStoreTwo.IsTalk = false;  // ���� ���ΰ� ����� �ѹ��̶� �ߴ��� ����
        IngredientStoreTwo.IsGalicQuest = false;    // ���� ������ ���� ����� �ѹ��̶� ������� ����(���� ��� �ذ��ߴ��� ����)
        IngredientStoreTwo.OneChanceGalicClear = false;  // ���� ��� �ذ� �� ù ��ȭ 
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

        MoneyStore.IsTalk = false;  // �����ü�� ��ӴϿ� ���� �̾߱⸦ �ߴ��� ����
        MoneyStore.StartSonQuest = false;   // ���� ���� ����Ʈ�� �����ߴ��� ����
        MoneyStore.OneChanceClearSon = false;   // Ŭ���� ���� ù ���
        MoneyStore.IsTalkOneChanceDiscount = false; // �ѹ��� ���ؼ� ���ڸ� ���� �� ����. true�� �Ǹ� ���� �õ� �̹� �� ��.
        MoneyStore.SumBorrow = 0;    // �� ���� �ݾ�
        MoneyStore.NowDate = 1;  // ��¥
        MoneyStore.ClearMoney = 0;   // ����Ʈ ������ �޴� ��
        MoneyStore.loseMoney = 0;    // ���� �Ϸ� ������ �ִ� ��

        MoneyStoreTwo.SumBorrow = 0;    // �� ���� �ݾ�
        MoneyStoreTwo.NowDate = 1;  // ��¥
        MoneyStoreTwo.loseMoney = 0;   // ���� �Ϸ� ������ �ִ� ��

        PineAppleStoreTwo.isPineapple = true;
        PineAppleStoreTwo.isContract = false;
        PineAppleStoreTwo.isMeet = false;

        PineAppleStore.isFineapple = true;
        PineAppleStore.isFirstTime = true;

        FineMessage.minusColor = new Color(0, 0, 0, 0.1f / 255f);

        PineappleCount.nowDate = 0;

        EmployeeRecruit.RecruitClerk = new ClerkC[3];
        EmployeeRecruit.IsRecruited = new bool[3] { false, false, false };

        MakingPizza.pizzaRequestList = new List<Request>();   // �������� ���� ����Ʈ
        MakingPizza.CompletePizzaList = new List<Pizza>();  // �ϼ��� ���� ����Ʈ
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
        {"0","-1","-1","-1" ,"����"},	// ����
		{"1","25","3","150","�丶��" },	// �丶��
		{"2","30","2","160","ġ��"},	// ġ��
		{"3","15","2","80","����" },	// ����
		{"4","20","1","120","����" },	// ����
		{"5","45","7","500","������" },	// ������
		{"6","27","3","140","������" },	// ������
		{"7","40","5","320","�Ҷ��Ǵ�" },	// �Ҷ��Ǵ�
		{"8","65","12","960","�߰��" },	// �߰��
		{"9","78","20","1350","�Ұ��" },    // �Ұ��
		{"10","32","4","150","���" }, // ���
		{"11","27","2","200","���" }, // ���
		{"12","17","1","100","����" },    // ����
		{"13", "34", "7", "230", "����" },    // ����
		{"14", "28", "5", "170", "����" },    // ����
		{"15", "22", "1", "210", "����" },	// ����
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
        ClerkList = new List<ClerkC>() { new ClerkC(47, Tier.THREE, Tier.ONE, Tier.FOUR, 0, 10000, "�����̾�", null, 0) };
        DiceItem = new ItemS[10]
    {
        new ItemS(ItemType.DICE, 2, "�� �ֻ���", "���� ���� �ֻ�����. \n �ֻ��� �� ���� 0,1,2,3,4,5 �� ��¡�Ѵ�.", 0),
        new ItemS(ItemType.DICE, 2, "�ݼ� �ֻ���", "�ݼ����� ���� �ֻ�����. \n �ֻ��� �� ���� 3,4,5,6,7,8 �� ��¡�Ѵ�.", 1),
        new ItemS(ItemType.DICE, 2, "8�� �ֻ���", "8������ �� �ֻ�����. \n �ֻ��� �� ���� 2,2,3,3,4,4,5,6 �� ��¡�Ѵ�.", 2),
        new ItemS(ItemType.DICE, 2, "12�� �ֻ���", "12������ �� �ֻ�����. \n �ֻ��� �� ���� \n1,2,3,3,4,4,5,5,6,7,8,9 �� ��¡�Ѵ�.", 3),
        new ItemS(ItemType.DICE, 2, "¦�� �ֻ���", "¦���� �����ϴ� �ֻ�����. \n �ֻ��� �� ���� 2,2,4,4,6,6 �� ��¡�Ѵ�.", 4),
        new ItemS(ItemType.DICE, 2, "Ȧ�� �ֻ���", "Ȧ���� �����ϴ� �ֻ�����. \n �ֻ��� �� ���� 1,1,3,3,5,5 �� ��¡�Ѵ�.", 5),
        new ItemS(ItemType.DICE, 2, "�Ҽ� �ֻ���", "�Ҽ��� �����ϴ� �ֻ�����. \n �ֻ��� �� ���� 2,3,5,7,11,13 �� ��¡�Ѵ�.", 6),
        new ItemS(ItemType.DICE, 2, "��� �ֻ���", "���ڰ� �� ���� �ֻ�����. \n �ֻ��� �� ���� 1,1,1,1,1,15 �� ��¡�Ѵ�.", 7),
        new ItemS(ItemType.DICE, 2, "���� �ֻ���", "������ ���� �ֻ�����. \n �ֻ��� �� ���� 2,3,4,5,6,7 �� ��¡�Ѵ�.", 8),
        new ItemS(ItemType.DICE, 2, "�ö�ƽ �ֻ���", "�ö�ƽ���� ���� �ֻ�����. \n �ֻ��� �� ���� 1,2,3,4,5,6 �� ��¡�Ѵ�.", 9)
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
        new ItemS(ItemType.GUN, 1, "Glick 19","����ӵ� : �߰� \n����� : ���� \n��ź�� : 15\n�������ð� : 3", 0),
        new ItemS(ItemType.GUN, 1, "S&U m500","����ӵ� : ���� \n����� : ���� \n��ź�� : 6\n�������ð� : 3", 1),
        new ItemS(ItemType.GUN, 1, "Mi1911","����ӵ� : ���ݴ��� \n����� : �ſ���� \n��ź�� : 7\n�������ð� : 3", 2),
        new ItemS(ItemType.GUN, 1, "MiP9","����ӵ� : �ſ���� \n����� : ���� \n��ź�� : 30\n�������ð� : 3", 3),
        new ItemS(ItemType.GUN, 1, "MiPX","����ӵ� : ���� \n����� : �߰� \n��ź�� : 30\n�������ð� : 3", 4),
        new ItemS(ItemType.GUN, 1, "Pi90","����ӵ� : ���� \n����� : �߰� \n��ź�� : 50\n�������ð� : 3", 5),
        new ItemS(ItemType.GUN, 1, "Kress Victor","����ӵ� : �ſ�ſ���� \n����� : ���� \n��ź�� : 30\n�������ð� : 3", 6),
        new ItemS(ItemType.GUN, 1, "Thimpson SMG","����ӵ� : ���� \n����� : �ſ���� \n��ź�� : 30\n�������ð� : 3", 7)
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
    /// ��
    /// </summary>
    public static int Dept = 0;
    /// <summary>
    /// ������ ��¥, ���� �ݾ�, �����ü �ڵ�
    /// </summary>
    public static Dictionary<int, Dictionary<int, int>> PayMoneyDate = new Dictionary<int, Dictionary<int, int>>()
    { { 1, new Dictionary<int, int>() { { 1, 20000000 } } } };
    /// <summary>
    /// �����ü�� ���� ����(���� ����)
    /// </summary>
    public static float[] DeptMulitplex = new float[2] { 1.1f, 1.05f };
    /// <summary>
    /// �����ü �ڵ�
    /// </summary>
    public static int[] MoneyStoreCode = new int[2] { 0, 1 };
    /// <summary>
    /// �����ü���� ���� �� �ִ� �ִ� �ݾ�
    /// </summary>
    public static int[] MoneyMaxBorrow = new int[2] { 50000000, 30000000 };
    /// <summary>
    /// ������ ���� ������ ������ ����
    /// </summary>
    public static bool[] MoneyConfiscated = new bool[2] { true, false };
    /// <summary>
    /// �����ü �̸�
    /// </summary>
    public static string[] MoneyStoreName = new string[2] { "�̼�", "�ູ" };
    /// <summary>
    /// �Ϸ� ���� �������� �� ���
    /// </summary>
    public static int ClerkMoney = 0;
    /// <summary>
    /// �Ϸ� ���� ���� ����� �� �� ���
    /// </summary>
    public static int PizzaIngMoney = 0;
    /// <summary>
    /// ����
    /// </summary>
    public static int Fine = 0;
    /// <summary>
    /// �׾����� ����
    /// </summary>
    public static bool IsDead = false;
    /// <summary>
    /// ���� ����
    /// </summary>
    public static DayEnum NowDay = DayEnum.MONDAY;
    /// <summary>
    /// ���� �ϼ�
    /// </summary>
    public static int NowDate = 1;
    /// <summary>
    /// �ֻ��� ���ʽ�
    /// </summary>
    public static short DiceBonus = 0;
    /// <summary>
    /// ������ ��� ��ȣ ����Ʈ. �ߺ��Ǵ� ��ȣ�� �ִ�.
    /// </summary>
    public static List<int> ChoiceIngredientList = new List<int>();
    /// <summary>
    /// ������ �ŷµ�. ������� 100% ��Ȯ�� ������ ���� �ŷµ��̴�. ��Ȯ�� ���̸� �ŷµ��� ����.
    /// </summary>
    public static int PizzaAttractiveness;
    public static int Perfection;
    public static int ProductionCost;
    public static int SellCost;
    public static int TotalDeclineAt;
    public static List<Ingredient> ingreds = new List<Ingredient>();
    /// <summary>
    /// ���� ��ᰪ. [,0]�� ����ȣ, [,1]�� �ŷµ�, [,2]�� �ŷ��϶���, [,3]�� ��ᰪ, [,4]�� ����̸� [0,]�� ��������.
    /// </summary>
    public static string[,] IngredientsArray = new string[16, 5]
    {
        {"0","-1","-1","-1" ,"����"},	// ����
		{"1","25","3","150","�丶��" },	// �丶��
		{"2","30","2","160","ġ��"},	// ġ��
		{"3","15","2","80","����" },	// ����
		{"4","20","1","120","����" },	// ����
		{"5","45","7","500","������" },	// ������
		{"6","27","3","140","������" },	// ������
		{"7","40","5","320","�Ҷ��Ǵ�" },	// �Ҷ��Ǵ�
		{"8","65","12","960","�߰��" },	// �߰��
		{"9","78","20","1350","�Ұ��" },    // �Ұ��
		{"10","32","4","150","���" }, // ���
		{"11","27","2","200","���" }, // ���
		{"12","17","1","100","����" },    // ����
		{"13", "34", "7", "230", "����" },    // ����
		{"14", "28", "5", "170", "����" },    // ����
		{"15", "22", "1", "210", "����" },	// ����
	};
    /// <summary>
    /// ��� ������ ���� ����� ��ȣ��
    /// </summary>
    public static List<int> UsableIngredient = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    /// <summary>
    /// ������ ���� ����Ʈ
    /// </summary>
    public static List<Pizza> DevelopPizza = new List<Pizza>();
    /// <summary>
    /// ���ڰ� �޴��ǿ� �־��� �ð�
    /// </summary>
    public static Dictionary<Pizza, int> menuDateDic = new Dictionary<Pizza, int>();
    public static bool IsMakePizza = false;
    public static bool isStartGame = false;
    public static bool StopTime = false;
    /// <summary>
    /// ������ ���ξ��� ����
    /// </summary>
    public static int PineAppleCount = 0;
    /// <summary>
    /// ���ξ��� ����
    /// </summary>
    public static Pizza PineapplePizza = new Pizza("PineapplePizza", 100, 0, 2000000, 99999, new List<Ingredient>() { Ingredient.TOMATO, Ingredient.CHEESE, Ingredient.PINEAPPLE }, 0, 100, 0);
    /// <summary>
    /// 0.02�ʰ��� ��
    /// </summary>
    public static WaitForSeconds OneTime = new WaitForSeconds(0.02f);
    /// <summary>
    /// ����� ���� ����Ʈ
    /// </summary>
    public static List<ClerkC> ClerkList = new List<ClerkC>() { new ClerkC(47, Tier.THREE, Tier.ONE, Tier.FOUR, 0, 20000, "�����̾�", null, 0) };

    /// <summary>
    /// ���� Ÿ���� �������� ���� ��ųʸ����� ã�Ƽ� Dictionary<ItemS, int> ������ �����Ѵ�.
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
    /// �ε����� ���� ���θ� Ȯ���ϴ� Ȯ��޼���
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
    /// �ε����� �´� Ű�� ã�� Ȯ�� �޼���
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
    /// �� ����Ʈ�� ��Ḧ ���ϴ� Ȯ�� �޼���
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
        new ItemS(ItemType.DICE, 2, "�� �ֻ���", "���� ���� �ֻ�����. \n �ֻ��� �� ���� 0,1,2,3,4,5 �� ��¡�Ѵ�.", 0),
        new ItemS(ItemType.DICE, 2, "�ݼ� �ֻ���", "�ݼ����� ���� �ֻ�����. \n �ֻ��� �� ���� 3,4,5,6,7,8 �� ��¡�Ѵ�.", 1),
        new ItemS(ItemType.DICE, 2, "8�� �ֻ���", "8������ �� �ֻ�����. \n �ֻ��� �� ���� 2,2,3,3,4,4,5,6 �� ��¡�Ѵ�.", 2),
        new ItemS(ItemType.DICE, 2, "12�� �ֻ���", "12������ �� �ֻ�����. \n �ֻ��� �� ���� \n1,2,3,3,4,4,5,5,6,7,8,9 �� ��¡�Ѵ�.", 3),
        new ItemS(ItemType.DICE, 2, "¦�� �ֻ���", "¦���� �����ϴ� �ֻ�����. \n �ֻ��� �� ���� 2,2,4,4,6,6 �� ��¡�Ѵ�.", 4),
        new ItemS(ItemType.DICE, 2, "Ȧ�� �ֻ���", "Ȧ���� �����ϴ� �ֻ�����. \n �ֻ��� �� ���� 1,1,3,3,5,5 �� ��¡�Ѵ�.", 5),
        new ItemS(ItemType.DICE, 2, "�Ҽ� �ֻ���", "�Ҽ��� �����ϴ� �ֻ�����. \n �ֻ��� �� ���� 2,3,5,7,11,13 �� ��¡�Ѵ�.", 6),
        new ItemS(ItemType.DICE, 2, "��� �ֻ���", "���ڰ� �� ���� �ֻ�����. \n �ֻ��� �� ���� 1,1,1,1,1,15 �� ��¡�Ѵ�.", 7),
        new ItemS(ItemType.DICE, 2, "���� �ֻ���", "������ ���� �ֻ�����. \n �ֻ��� �� ���� 2,3,4,5,6,7 �� ��¡�Ѵ�.", 8),
        new ItemS(ItemType.DICE, 2, "�ö�ƽ �ֻ���", "�ö�ƽ���� ���� �ֻ�����. \n �ֻ��� �� ���� 1,2,3,4,5,6 �� ��¡�Ѵ�.", 9)
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
        new ItemS(ItemType.GUN, 1, "Glick 19","����ӵ� : �߰� \n����� : ���� \n��ź�� : 15\n�������ð� : 3", 0),
        new ItemS(ItemType.GUN, 1, "S&U m500","����ӵ� : ���� \n����� : ���� \n��ź�� : 6\n�������ð� : 3", 1),
        new ItemS(ItemType.GUN, 1, "Mi1911","����ӵ� : ���ݴ��� \n����� : �ſ���� \n��ź�� : 7\n�������ð� : 3", 2),
        new ItemS(ItemType.GUN, 1, "MiP9","����ӵ� : �ſ���� \n����� : ���� \n��ź�� : 30\n�������ð� : 3", 3),
        new ItemS(ItemType.GUN, 1, "MiPX","����ӵ� : ���� \n����� : �߰� \n��ź�� : 30\n�������ð� : 3", 4),
        new ItemS(ItemType.GUN, 1, "Pi90","����ӵ� : ���� \n����� : �߰� \n��ź�� : 50\n�������ð� : 3", 5),
        new ItemS(ItemType.GUN, 1, "Kress Victor","����ӵ� : �ſ�ſ���� \n����� : ���� \n��ź�� : 30\n�������ð� : 3", 6),
        new ItemS(ItemType.GUN, 1, "Thimpson SMG","����ӵ� : ���� \n����� : �ſ���� \n��ź�� : 30\n�������ð� : 3", 7)
    };
    /// <summary>
    /// ����ӵ� - 10~19 : �ſ���� - 20~29 : ���� - 30~39 : ���� ���� - 40~55 : �߰� - 56~ 64 : ���� ���� - 65~79 : ���� - 80~89 : �ſ� ���� - 90~ : �ſ�ſ����
    /// ����� - 10~19 : �ſ� ���� - 20~29 : ���� - 30~39 : ���� ���� -40~55 : �߰� - 56~74 : ���� ���� - 75~85 : ���� - 86~ : �ſ� ����
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
    /// �÷��̾ ������ ������. ItemS�� ItemType���� �����۵��� �з��� �� �ִ�. int�� ���� ����
    /// </summary>
    public static Dictionary<ItemS, int> PlayerItemDIc = new Dictionary<ItemS, int>() { { DiceItem[9], 2 }, { GunItem[0], 1 } };

    /// <summary>
    /// ���� ������ ��. -1�� ������ ���¸� �ǹ���.
    /// </summary>
    public static int[] NowGun = new int[1] { 0 };
    /// <summary>
    /// �������� ��Ȱ�ؾ��ϴ� �� ����
    /// </summary>
    public static bool IsHospital = false;

    private static float pizzaStoreStar = 1.0f;
    /// <summary>
    /// ���� ���� ����
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
