using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolicePathNS;

// 한석호 작성

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
    public List<PolicePath> GetPolicePath();
}

interface IPoliceCar
{
    public void InitPoliceCarPath(List<PolicePath> policePathList);
}
// 경찰차 제어에 관한 인터페이스
public interface IPoliceCarControl
{
    public void SetIsBehaviour(bool bo);
    public int GetPoliceCarCode();
}
