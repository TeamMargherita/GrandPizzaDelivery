using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성


namespace BuildingNS
{
    public enum BuildingShape {SQUARE, WIDTHLONG, LENGTHLONG, COMPOSITE };  
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
            NONE, MOVING, STOP, INSPECTING
        };
    }
}