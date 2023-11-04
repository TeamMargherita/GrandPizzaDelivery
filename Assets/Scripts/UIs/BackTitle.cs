using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PizzaNS;
using Inventory;
using UnityEngine.SceneManagement;
using ClerkNS;
using StoreNS;
using DayNS;

public class BackTitle : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;

    public void OnClickTitle()
    {
        Destroystatic("GameManager");
        PlayerStat.HP = PlayerStat.MaxHP;
        SceneManager.LoadScene("MainPage");
        Destroystatic("RhythmManager");
        // -------------------------------------static -----------------------------------------------------//
        Constant.BorrowMoneyDate = new Dictionary<int, Dictionary<int, int>>();
        Constant.PayMoneyDate = new Dictionary<int, Dictionary<int, int>>();
        Constant.NowDay = DayEnum.MONDAY;
        Constant.NowDate = 1;
        Constant.DiceBonus = 0;
        Constant.ChoiceIngredientList = new List<int>();
        Constant.ingreds = new List<Ingredient>();
        Constant.IngredientsArray = new string[16, 5]
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
        Constant.UsableIngredient = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Constant.DevelopPizza = new List<Pizza>();
        Constant.menuDateDic = new Dictionary<Pizza, int>();
        Constant.IsMakePizza = false;
        Constant.isStartGame = false;
        Constant.StopTime = false;
        Constant.PineappleCount = 0;
        Constant.PineapplePizza = new Pizza("PineapplePizza", 100, 0, 2000000, 99999, new List<Ingredient>() { Ingredient.TOMATO, Ingredient.CHEESE, Ingredient.PINEAPPLE }, 0, 100, 0);
        Constant.OneTime = new WaitForSeconds(0.02f);
        Constant.ClerkList = new List<ClerkC>() { new ClerkC(47, Tier.THREE, Tier.ONE, Tier.FOUR, 0, 20000, "프레이야", null, 0) };
        Constant.DiceItem = new ItemS[10]
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
        Constant.PlayerItemDIc = new Dictionary<ItemS, int>() { { Constant.DiceItem[9], 2 } };
        Constant.DiceInfo = new DiceS[10]
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
        Constant.nowDice = new int[2] { 9, 9 };
        Constant.GunItem = new ItemS[8]
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
        Constant.GunInfo = new GunS[8]
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
        Constant.nowGun = new int[1] { -1 };

        House.activeColor = new Color(248 / 255f, 70 / 255f, 6 / 255f);

        StreetLamp.lightOnColor = new Color(255 / 255f, 177 / 255f, 0 / 255f);
        StreetLamp.lightOffColor = Color.black;

        EmployeeFire.WorkingDay = new Dictionary<int, List<ClerkC>>(); ;

        DiceStore.IsOneDayDiceStore = false;
        
        IngredientStore.Contract = 0;
        IngredientStore.Hint = false;
        IngredientStore.OneChance = true;

        IngredientStoreTwo.IsTalk = false;
        IngredientStoreTwo.IsGalicQuest = false;
        IngredientStoreTwo.OneChanceGalicClear = false;
        IngredientStoreTwo.NowDate = 1;
        IngredientStoreTwo.Ingredient = 0;
        IngredientStoreTwo.Discount = 0;
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

        MoneyStore.IsTalk = false;
        MoneyStore.StartSonQuest = false;
        MoneyStore.OneChanceClearSon = false;
        MoneyStore.IsTalkOneChanceDiscount = false;

        MoneyStore.SumBorrow = 0;
        MoneyStore.PlusMoney = 1.1f;
        MoneyStore.NowDate = 1;
        MoneyStore.ClearMoney = 0;

        PineAppleStore.isFirstTime = true;
        PineAppleStore.isFineapple = true;

        PineAppleStoreTwo.isPineapple = true;
        PineAppleStoreTwo.isContract = false;
        PineAppleStoreTwo.isMeet = false;

        PizzaMenuUI.nowDate = 0;


        // -------------------------------------------------------------------------------------------------//
    }
    private void Destroystatic(string gameOB)
    {
        GameObject temporary = GameObject.Find(gameOB);
        if (temporary != null)
        {
            Destroy(temporary);
        }
    }

    public void OnClickGameQuit()
    {
        Application.Quit();
    }
}
