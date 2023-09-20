using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성


namespace BuildingNS
{
    public enum BuildingShape {SQUARE, WIDTHLONG, LENGTHLONG, COMPOSITE };  
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

        public IHouse iHouse;
        public AddressS(int BuildingAddress, int HouseAddress, IHouse iHouse)
		{
            this.BuildingAddress = BuildingAddress;
            this.HouseAddress = HouseAddress;
            this.iHouse = iHouse;
		}
	}
}
/*
집마다 성향이 제각각인 손님들이 존재한다. 한번 성향이 정해진 손님들은 쭉 유지된다.
경찰차가 플레이어를 추격한다.(미정)
속도계를 만든다.
재료들을 만든다.
가게 내부 화면을 만든다.
경찰차 리젠(추격이벤트때는 리젠 안됨)
경찰차 바나나
손님 리뷰
*/
namespace CustomerNS
{
    public struct CustomerS
	{
        //선호 재료
        //피자 완성도 커트라인

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