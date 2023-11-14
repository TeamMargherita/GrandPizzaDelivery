using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PolicePathNS;
using PoliceNS.PoliceStateNS;
using BuildingAddressNS;
using BuildingNS.HouseNS;
using StoreNS;
// 한석호 작성
/// <summary>
/// 건물, 집 주소와 관련된 인터페이스
/// </summary>
public interface IAddress
{
    public void InitAddress(int number, List<AddressS> addressSList);
    public int GetAddress();
    public void SetIMap(IMap iMap);
    public void SetIDeliveryPanelControl(IDeliveryPanelControl iDeliveryPanelControl);
    public void SetIHouseActiveUIControl(IHouseActiveUIControl iHouseActiveControl);
}
/// <summary>
/// 건물별 경찰차를 할당하고 경로를 설정하기 위한 인터페이스
/// </summary>
public interface IBuilding
{
    public bool GetIsPoliceCar();
    public void SetIsPoliceCar(bool b);
    public Vector2 GetpoliceCarDis();
    public Vector2 GetBuildingPos();
    public List<PolicePath> GetPolicePath();
}
/// <summary>
/// 경찰차의 속성과 관련된 인터페이스
/// </summary>
public interface IPoliceCar
{
    public void InitPoliceCarPath(List<PolicePath> policePathList);
    public void SetIInspectingPanelControl(IConversationPanelControl iInspectingPanelControl);
    public void SetPlayerMove(PlayerMove playerMove);
    public Rigidbody2D GetRigidBody2D();
    public float GetSpeed();
    public PoliceState GetPoliceState();
    public void SetMap(IStop iStop);
    public void SetIsStop(bool bo);

    public void SetBanana(GameObject banana);

}
/// <summary>
/// 경찰차 제어에 관한 인터페이스
/// </summary>
public interface IMovingPoliceCarControl
{
    public void SetIsBehaviour(bool bo);
    //public int GetPoliceCarCode();
}
/// <summary>
/// 경찰차 우선순위 번호와 관련된 인터페이스
/// </summary>
public interface IPriorityCode
{
    public int GetPriorityCode();
}

/// <summary>
/// 경찰차의 상태에 따른 불심검문 여부를 따지기 위한 인터페이스
/// </summary>
public interface IInspectingPoliceCarControl
{
    public void SetPoliceState(PoliceState policeState);
}
/// <summary>
/// 대화창 UI를 제어하기 위한 인터페이스
/// </summary>
public interface IConversationPanelControl
{
    public void ControlConversationUI(bool isOn, IEndConversation iEndInspecting, int type);
}
/// <summary>
/// 배달 도착여부를 따지는 인터페이스
/// </summary>
public interface IDeliveryPanelControl
{
    public void ControlDeliveryUI(bool isOn);
    public void SetIHouseDeliveryUI(IHouse iHouse);
}
/// <summary>
///  집 타입에 따라 집 근처에서 행할 수 있는 조작키 설명 패널을 켜주는 인터페이스
/// </summary>
public interface IHouseActiveUIControl
{
    public void ActiveTrueKeyExplainPanel(bool bo);
    public void SetHouseType(HouseType houseType);

}
/// <summary>
/// 대화 도중 선택지를 고르게 해주는 인터페이스
/// </summary>
public interface IInspectingUIText
{
    public void ChoiceText(int num);
}
/// <summary>
/// 대화창 UI를 끝냈을 때 실행할 일들을 다루는 함수가 담긴 인터페이스
/// </summary>
public interface IEndConversation
{
    public void EndConversation();
}
/// <summary>
/// 집 타입을 바꿔주거나 배달 여부에 따른 집의 상태를 바꿔주기 위한 인터페이스
/// </summary>
public interface IHouse
{
    public void EnableHouse();
    public void DisableHouse(Pizza pizza);
    public void EndDeliveryDisableHouse();
    public bool GetIsEnable();
    public void SetHouseType(Sprite mark, HouseType houseType);
    public HouseType GetHouseType();
    public Transform GetLocation();

}
/// <summary>
/// 활성화된 집 근처에 왔는지 따지고 그에 따른 상태를 변화하기 위한 인터페이스
/// </summary>
public interface IActiveHouse
{ 
    public bool ActiveHouse(bool bo);
    public void IntoHouse(bool bo);
}
/// <summary>
/// 맵에 있는 모든 집의 주소를 저장하기 위한 인터페이스
/// </summary>
public interface IMap
{
    public void AddAddress(AddressS addressS);
    public float RemoveAddress(AddressS addressS);
}
/// <summary>
/// 경찰을 전부 멈추거나(맵 일시정지 효과) 파괴된 경찰을 삭제하기 위한 함수를 담은 인터페이스. 
/// </summary>
public interface IStop
{
    public void StopMap(bool bo);
    public void RemovePoliceList(IPoliceCar iPoliceCar);
}
/// <summary>
/// 음식 재료 중 무엇을 선택했는지 그리고 선택한 재료의 설명을 보여주기 위한 인터페이스
/// </summary>
public interface IIngredientSlot
{
    public void IngredientExplain(int ingNum);
    public void ChoiceIngredient(int ingNum, int index);
}
/// <summary>
///  피자 설명을 위한 인터페이스
/// </summary>
public interface IAddPizza
{
    public void SetAddPizzaExplain(int num);
    public void SetTemSlotNumber(int num);
}
/// <summary>
/// 피자집에서 생성된 피자에 관한 정보를 전달하는 인터페이스
/// </summary>
public interface IMakingPizzaPanel
{
    public void SetPizza(Pizza pizza);
    public bool ComparePizza(Pizza pizza);
}
/// <summary>
/// 화면 상단에 알람을 띄우는 인터페이스
/// </summary>
public interface IAlarmMessagePanel
{
    /// <summary>
    /// 알림 창 열고닫기. 알림창에 보여줄 텍스트도 설정해야됨
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="text">띄울 텍스트를 적는다.</param>
    public void ControlAlarmMessageUI(bool isOn, string text);
}
/// <summary>
/// 추격 경찰차를 소환하는 인터페이스
/// </summary>
public interface ISpawnCar
{
    /// <summary>
    /// 추격 경찰차를 count 수 만큼 소환합니다.
    /// </summary>
    /// <param name="count">소환할 추격 경찰차의 수입니다.</param>
    public void SpawnCar(int count);
}
/// <summary>
/// 트랜스폼을 가져오는 인터페이스
/// </summary>
public interface ISetTransform
{
    public void SetTransform(Transform trans);
}
/// <summary>
/// boolean 값을 반환하는 인터페이스
/// </summary>
public interface IGetBool
{
    public bool GetBool();
}
/// <summary>
/// 추격 경찰차 근처에 방해되는 오브젝트가 있는지 확인하기 위한 인터페이스
/// </summary>
public interface ICheckCol
{
    public void InitNumber(int num, IUpdateCheckList iUpdateCheckList);
}
/// <summary>
/// 추격 경찰차 근처에서 탐지한 것을 전달 혹은 사라졌음을 알리는 인터페이스
/// </summary>
public interface IUpdateCheckList
{
    public void UpdateCheck(int num, bool isAdd);
}
/// <summary>
/// 주사위 돌리는 코루틴을 가진 인터페이스
/// </summary>
public interface ICoroutineDice
{
    public void StartDice(int num);
}
/// <summary>
/// 상점 정보 초기화하는 인터페이스
/// </summary>
public interface IInitStore
{
    public void InitStore(Store store);
    public void OpenStore();
    public void InitSelectItemCnt();
}
/// <summary>
/// 상점을 닫는 인터페이스
/// </summary>
public interface ICloseStore
{
    public void CloseStore(int cost, Dictionary<ItemS, int> dic);
}
/// <summary>
/// 신호등이 초록불인지 확인하기 위한 인터페이스
/// </summary>
public interface ICheckIsGreen
{
    public bool CheckIsGreen();
}
/// <summary>
/// 생산되는 피자를 리셋시키기 위한 인터페이스
/// </summary>
public interface IResetPizzaMaking
{
    public void ResetPizzaMaking();
}