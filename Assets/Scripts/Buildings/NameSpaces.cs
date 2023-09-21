using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성


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
/*
집마다 성향이 제각각인 손님들이 존재한다. 한번 성향이 정해진 손님들은 쭉 유지된다.
경찰차가 플레이어를 추격한다.(미정)
속도계를 만든다.
재료들을 만든다. 재료마다 매력도가 다름. 매력도 하락수치도 다름
가게 내부 화면을 만든다.
경찰차 리젠(추격이벤트때는 리젠 안됨)
경찰차 바나나
손님 리뷰
*/
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