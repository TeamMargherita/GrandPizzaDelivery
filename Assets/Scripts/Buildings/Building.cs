using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingNS;
using PoliceNS.PolicePathNS;
using BuildingAddressNS;

// 한석호 작성

public class Building : MonoBehaviour, IAddress, IBuilding
{
    [SerializeField] private BuildingShape buildingShape;   // 빌딩의 모양
    [SerializeField] private Vector2 compositePos;  // 복합적인 형태로 집이 갖춰졌을 때 경찰차의 시작 위치를 임의로 정하기 위한 변수
    [SerializeField] private float[] policeCarValue;    // 경찰차가 수행해야 하는 행동의 거리, 혹은 회전 값
    [SerializeField] private int[] policeCarBehaviour;  // 경찰차가 수행해야 하는 행동 번호

    private List<PolicePath> pathList = new List<PolicePath>(); // 집마다 존재하는 경로 리스트

    private Vector2 policeCarDis; // 경찰차가 스폰될 때 이 건물과 경찰차의 거리입니다.
    private Vector2 buildingPos;    // 빌딩의 Position값

    private int buildingNumber; // 빌딩의 고유 번호
    private bool isPoliceCar;   // 이 빌딩에 경찰차가 존재해야하는지 여부

    public void Awake()
    {

        buildingPos = this.gameObject.transform.position;
        // 빌딩의 모양에 따라 경찰차의 시작 위치가 달라진다.
        if (buildingShape == BuildingShape.SQUARE)
        {
            policeCarDis = new Vector2(-2, 5);
        }
        else if (buildingShape == BuildingShape.WIDTHLONG)
        {
            policeCarDis = new Vector2(-1, 5);
        }
        else if (buildingShape == BuildingShape.LENGTHLONG)
        {
            policeCarDis = new Vector2(-2, 7);
        }
        else if (buildingShape == BuildingShape.COMPOSITE)
        {
            policeCarDis = compositePos;
        }

        for (int i = 0; i < policeCarBehaviour.Length; i++)
        {
            // 이 빌딩에서 수행할 수 있는 경로 값을 경로 리스트에 넣어준다.
            pathList.Add(new PolicePath(policeCarBehaviour[i], policeCarValue[i]));
        }
    }
    /// <summary>
    /// 주소값 초기화
    /// </summary>
    /// <param name="number">빌딩번호</param>
    /// <param name="addressSList">집 주소 명단</param>
    public void InitAddress(int number, List<AddressS> addressSList)
    {
        buildingNumber = number;
        int n = 0;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).GetComponent<IAddress>() != null)
            {
                this.transform.GetChild(i).GetComponent<IAddress>().InitAddress(number * 1000 + n, addressSList);
                n++;
            }
        }
    }
    /// <summary>
    /// 집집마다 IMap 인터페이스를 전달하여 집 주소를 저장하기 위함
    /// </summary>
    /// <param name="iMap"></param>
    public void SetIMap(IMap iMap)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).GetComponent<IAddress>() != null)
            {
                this.transform.GetChild(i).GetComponent<IAddress>().SetIMap(iMap);
            }
        }
    }
    /// <summary>
    /// 빌딩 번호 반환
    /// </summary>
    /// <returns></returns>
    public int GetAddress()
    {
        return buildingNumber;
    }
    /// <summary>
    /// 해당 빌딩에 경찰차가 있는지 여부 반환
    /// </summary>
    /// <returns></returns>
    public bool GetIsPoliceCar()
    {
        return isPoliceCar;
    }
    /// <summary>
    /// 해당 빌딩을 돌아다니는 경찰차의 경로 반환
    /// </summary>
    /// <returns></returns>
    public List<PolicePath> GetPolicePath()
    {
        return pathList;
    }
    /// <summary>
    /// 해당 빌딩에 경찰차가 돌아다니는 지 아닌지 설정
    /// </summary>
    /// <param name="b"></param>
    public void SetIsPoliceCar(bool b)
    {
        isPoliceCar = b;
    }
    /// <summary>
    /// 경찰차의 시작위치에서 건물까지의 거리 반환
    /// </summary>
    /// <returns></returns>
    public Vector2 GetpoliceCarDis()
    {
        return policeCarDis;
    }
    /// <summary>
    /// 빌딩 위치 반환
    /// </summary>
    /// <returns></returns>
    public Vector2 GetBuildingPos()
    {
        return buildingPos;
    }
    /// <summary>
    /// 집마다 도달 시 배달 여부 UI가 뜰 수 있도록 인터페이스를 집집마다 전달 
    /// </summary>
    /// <param name="iDeliveryPanelControl"></param>
    public void SetIDeliveryPanelControl(IDeliveryPanelControl iDeliveryPanelControl)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).GetComponent<IAddress>() != null)
            {
                this.transform.GetChild(i).GetComponent<IAddress>().SetIDeliveryPanelControl(iDeliveryPanelControl);
            }
        }
    }
    /// <summary>
    /// 집집마다 인터페이스를 전달
    /// </summary>
    /// <param name="iHouseActiveControl"></param>
    public void SetIHouseActiveUIControl(IHouseActiveUIControl iHouseActiveControl)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).GetComponent<IAddress>() != null)
            {
                this.transform.GetChild(i).GetComponent<IAddress>().SetIHouseActiveUIControl(iHouseActiveControl);
            }
        }
    }
}
