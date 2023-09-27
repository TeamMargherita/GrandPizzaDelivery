using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성

namespace ClerkNS
{
    public enum Tier{ ONE = -1, TWO = 1, THREE = 3, FOUR = 6 };
    // 나중에 클래스로 바꿀듯
    public struct ClerkS
	{
        public Tier Handicraft;  // 손재주
        public Tier Agility; // 순발력
        public Tier Career;  // 경력
        public Tier Creativity;  // 창의력
        public int Stress;  // 스트레스
        public int Pay; // 주급

        public ClerkS (Tier Handicraft, Tier Agility, Tier Career, Tier Creativity, int Stress, int Pay)
		{
            this.Handicraft = Handicraft;
            this.Agility = Agility;
            this.Career = Career;
            this.Creativity = Creativity;
            this.Stress = Stress;
            this.Pay = Pay;
		}
    }
}

namespace BuildingNS
{
    public enum BuildingShape {SQUARE, WIDTHLONG, LENGTHLONG, COMPOSITE }; 
    
    namespace HouseNS
    {
        public enum HouseType { NONE, PIZZASTORE, HOUSE};
    }
}
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

namespace PizzaNS
{
    public enum Ingredient { NONE, TOMATO, CHEESE, BASIL, POTATO, BACON, CORN, JALAPENO, CHICKEN, MEAT };

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

    public struct PizzaExplain
	{
        public List<Ingredient> Ingreds;
        public int TotalDeclineAt;

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

	namespace CustomerNS
    {
        public struct CustomerS
        {
            //선호 재료
            public List<Ingredient> IngredList;
            //피자 완성도 커트라인
            public int PizzaCutLine;

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
namespace PoliceNS
{
    namespace PolicePathNS
    {
        public struct PolicePath
        {
            public int Behaviour;
            public float Value;

            public PolicePath(int behaviour, float value)
            {
                this.Behaviour = behaviour;
                this.Value = value;
            }
        }
    }

    namespace PoliceStateNS
    {
        // 차례로 없음, 이동중, 불심검문을 위한 멈춤, 불심검문중 이다.
        public enum PoliceState
        {
            NONE, MOVING, STOP, INSPECTING, DESTROY
        };
    }
}