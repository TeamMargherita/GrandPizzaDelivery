using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IAddress
{
    public void InitAddress(int number);
    public int GetAddress();
}

interface IBuilding
{
    public bool GetIsPoliceCar();
    public void SetIsPoliceCar(bool b);
    public Vector2 GetpoliceCarDis();
    public Vector2 GetBuildingPos();

}
