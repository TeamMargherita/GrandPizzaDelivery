using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PolicePathNS;
using PoliceNS.PoliceStateNS;

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
    public void SetIInspectingPanelControl(IInspectingPanelControl iInspectingPanelControl);
    public void SetPlayerMove(PlayerMove playerMove);
    public void SetPoliceSmokeEffect(IPoliceSmokeEffect iPoliceSmokeEffect);

}
// 경찰차 제어에 관한 인터페이스
public interface IMovingPoliceCarControl
{
    public void SetIsBehaviour(bool bo);
    public int GetPoliceCarCode();
}

public interface IInspectingPoliceCarControl
{
    public void SetPoliceState(PoliceState policeState);
}

public interface IInspectingPanelControl
{
    public void ControlInspectUI(bool isOn, IEndInspecting iEndInspecting);
}

public interface IInspectingUIText
{
    public void ChoiceText(int num);
}

public interface IEndInspecting
{
    public void EndInspecting();
}

public interface IPoliceSmokeEffect
{
    public void InsPoliceSmokeEfectObj(Transform trans);
}
