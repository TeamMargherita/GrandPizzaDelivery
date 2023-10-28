using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성
/// <summary>
/// 점원과 관련된 네임스페이스
/// </summary>
namespace ClerkNS
{
    public enum Tier{ ONE = -1, TWO = 1, THREE = 3, FOUR = 6 };
    // 나중에 클래스로 바꿀듯
    public class ClerkC
    {
        public Tier Agility { get; private set; } // 순발력
        public Tier Career { get; private set; }  // 경력
        public Tier Creativity { get; private set; }  // 창의력
        public int Handicraft { get; private set; }  // 손재주
        public int Stress { get; set; } // 스트레스
        public int Pay { get; set; } // 주급
        public int Max { get; private set; }    // 최종 능력치 최대치
        public int Min { get; private set; }    // 최종 능력치 최소치
        public string Name { get; private set; } // 이름
        public int MinPayScale { get; private set; }
        public int MaxPayScale { get; private set; }
        public int MaxStress { get; private set; }
        public ClerkC (int Handicraft, Tier Agility, Tier Career, Tier Creativity, int Stress, int Pay, string Name)
		{
            this.Handicraft = Handicraft;
            this.Agility = Agility;
            this.Career = Career;
            this.Creativity = Creativity;
            this.Stress = Stress;
            this.Pay = Pay;
            this.Name = Name;

            Max = (this.Handicraft + 8) + (int)Creativity;
            Min = (this.Handicraft - 8) + (int)Career;

            MinPayScale = Handicraft - (int)Agility + (int)Creativity + (int)Career - 10;
            MaxPayScale = Handicraft - (int)Agility + (int)Creativity + (int)Career + 10;

            MaxStress = 150;
        }
    }
}
/// <summary>
/// 빌딩 형태에 관한 네임스페이스
/// </summary>
namespace BuildingNS
{
    public enum BuildingShape {SQUARE, WIDTHLONG, LENGTHLONG, COMPOSITE }; 
    /// <summary>
    /// 집 상태에 관한 네임스페이스
    /// </summary>
    namespace HouseNS
    {
        public enum HouseType { NONE, PIZZASTORE, HOUSE, DICESTORE, PINEAPPLESTORE, INGREDIENTSTORE, PINEAPPLESTORETWO, GUNSTORE };
    }
}
/// <summary>
/// 집 주소에 관한 네임스페이스
/// </summary>
namespace BuildingAddressNS
{
    public struct AddressS
	{
        /// <summary>
        /// 건물의 주소
        /// </summary>
        public int BuildingAddress;
        /// <summary>
        /// 건물을 구성하는 각 집의 주소
        /// </summary>
        public int HouseAddress;

        public IHouse IHouse;
        public AddressS(int BuildingAddress, int HouseAddress, IHouse iHouse)
		{
            this.BuildingAddress = BuildingAddress;
            this.HouseAddress = HouseAddress;
            this.IHouse = iHouse;
		}
	}
}
/// <summary>
/// 피자 재료에 관한 네임스페이스
/// </summary>
namespace PizzaNS
{
    public enum Ingredient { PINEAPPLE, TOMATO, CHEESE, BASIL, POTATO, BACON, CORN, JALAPENO, CHICKEN, MEAT, APPLE, CARROT, BIGGREENONION };
    /// <summary>
    /// 재료 네임스페이스
    /// </summary>
    public struct IngredientS
    {
        public Ingredient Ingred;   // 재료
        public int Attractiveness;  // 매력도
        public int DeclineAt;    // 매력 하락도
        public int IngredientPrice; // 재료값

        public IngredientS(Ingredient ingred, int attractiveness, int declineAt, int ingredientPrice)
		{
            this.Ingred = ingred;
            this.Attractiveness = attractiveness;
            this.DeclineAt = declineAt;
            this.IngredientPrice = ingredientPrice;
		}
    }
    /// <summary>
    /// 완성된 피자의 설명을 위한 구조체
    /// </summary>
    public struct PizzaExplain
	{
        public List<Ingredient> Ingreds;    // 피자에 들어간 재료들
        public int TotalDeclineAt;  // 총 매력하락도 

		public PizzaExplain(List<Ingredient> Ingreds, int TotalDeclineAt)
		{
			this.Ingreds = new List<Ingredient>();
			for (int i = 0; i < Ingreds.Count; i++)
			{
				this.Ingreds.Add(Ingreds[i]);
			}
            this.TotalDeclineAt = TotalDeclineAt; 
		}
	}
    /// <summary>
    /// 고객에 관련된 네임스페이스
    /// </summary>
	namespace CustomerNS
    {
        public struct CustomerS
        {
            public List<Ingredient> IngredList;  //선호 재료
            public int PizzaCutLine;    //피자 완성도 커트라인

            public CustomerS(int pizzaCutLine, List<Ingredient> ingredList)
            {
                PizzaCutLine = pizzaCutLine;

                IngredList = new List<Ingredient>();
                for (int i = 0; i < ingredList.Count; i++)
                {
                    IngredList.Add(ingredList[i]);
                }
            }
        }
    }
}
/// <summary>
/// 경찰과 관련된 네임스페이스
/// </summary>
namespace PoliceNS
{
    namespace PolicePathNS
    {
        public struct PolicePath
        {
            public int Behaviour;   // 경찰의 행동 번호
            public float Value; // 행동과 관련된 값(예. 이동거리, 회전 각도 등)

            public PolicePath(int behaviour, float value)
            {
                this.Behaviour = behaviour;
                this.Value = value;
            }
        }
    }
    /// <summary>
    /// 경찰차의 상태를 따지기 위한 네임스페이스이다.
    /// </summary>
    namespace PoliceStateNS
    {
        // 차례로 없음, 이동중, 불심검문을 위한 멈춤, 불심검문중, 파괴됨, 무조건 플레이어 쫓아옴, 자동 주행, 맵 밖으로 탈출 이다.
        public enum PoliceState
        {
            NONE, MOVING, STOP, INSPECTING, DESTROY, SPUERCHASE, AUTOMOVE, OUTMAP
        };
    }
}
/// <summary>
/// 대화를 위한 네임스페이스이다.
/// </summary>
namespace ConversationNS
{
    public class TextNodeC
    {
        public int NowTextNum;  // 현재 텍스트 번호
        public int[] NextTextNum;  // 현재 텍스트 번호 다음에 연결되어 있는 텍스트 번호들
        public MethodS[] MethodSArr;    // 현재 텍스트 등장과 동시에 실행되어야 하는 함수들과 인자값을 넣어놓은 구조체의 배열
        public bool[] NextTextIsAble;   // 현재 텍스트 번호 다음에 연결되어 있는 텍스트의 연결 여부

        public TextNodeC (int nowTextNum, int[] nextTextNum, MethodS[] methodSArr, bool[] nextTextIsAble)
        {
            NowTextNum = nowTextNum;
            NextTextNum = nextTextNum;
            MethodSArr = methodSArr;
            NextTextIsAble = nextTextIsAble;
        }
    }
    /// <summary>
    /// 대화 선택지를 하나 고를 때마다 실행되어야 하는 메소드에 관한 구조체이다.
    /// </summary>
    public struct MethodS
    {
        public MethodEnum MethodNum;    // 실행할 메소드 번호
        public int[] MethodParameter;   // 실행할 메소드의 파라미터 값

        public MethodS (MethodEnum methodNum, int[] methodParameter)
        {
            MethodNum = methodNum;
            MethodParameter = methodParameter;
        }
    }
    /// <summary>
    /// 실행할 메소드의 종류
    /// </summary>
    public enum MethodEnum { NONE, SETSIZECONTENTS, CHANGENPCIMAGE, CHANGEPLAYERIMAGE, SETRANDNPCTEXT, ENDPANEL, SPAWNPOLICE, OPENSTORE, SAVETEXTINDEX, SETISCONDITION };
}
/// <summary>
/// 가게와 관련된 네임스페이스이다.
/// </summary>
namespace StoreNS
{
    /// <summary>
    /// 가게 종류
    /// </summary>
    public enum ItemType { NONE, DICE, GUN };
    /// <summary>
    /// 아이템 구조체
    /// </summary>
    public struct ItemS
	{
        public ItemType Type;   // 아이템 타입
        public int MaxCnt;  // 아이템 최대 소지 개수 
        public string Name; // 아이템 이름
        public string Explain;  // 아이템 설명
        public int ItemNumber;  // 아이템 번호

        public ItemS (ItemType type, int maxCnt, string name, string explain, int itemNumber)
		{
            Type = type;
            MaxCnt = maxCnt;
            Name = name;
            Explain = explain;
            ItemNumber = itemNumber;
		}
	}
    /// <summary>
    /// 주사위 아이템 구조체
    /// </summary>
    public struct DiceS
	{
        public int DiceCnt; // 주사위의 면 개수
        public int[] DiceArr;   // 면마다 적혀있는 번호
        public string Path; // 주사위 이미지의 경로

        public DiceS(int diceCnt, int[] diceArr, string path)
		{
            DiceCnt = diceCnt;
            DiceArr = diceArr;
            Path = path;
        }
	}
    /// <summary>
    /// 총의 장전방식 열거형
    /// </summary>
    public enum LoadEnum : short { NONE, AUTO, SEMIAUTO, MANUAL }
    /// <summary>
    /// 총 아이템 구조체
    /// </summary>
    public struct GunS
	{
        public LoadEnum LoadType;   // 총의 장전방식
        public float Speed; // 발사속도
        public short Damage;    // 발사 대미지
        public short Accuracy;  // 발사 정확도
        public short Magazine; // 장탄수
        public short ReloadSpeed; //재장전 속도
        public string Path; //총 이미지 경로

        public GunS(LoadEnum loadType, float speed, short damage, short accuracy, short magazine, short reloadSpeed, string path)
		{
            LoadType = loadType;
            Speed = speed;
            Damage = damage;
            Accuracy = accuracy;
            Magazine = magazine;
            ReloadSpeed = reloadSpeed;
            Path = path;
		}
	}
}