using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingNS;
using PoliceNS.PolicePathNS;
using BuildingAddressNS;

// 한석호 작성

public class Building : MonoBehaviour, IAddress, IBuilding
{
    [SerializeField] private BuildingShape buildingShape;
    [SerializeField] private Vector2 compositePos;
    [SerializeField] private float[] policeCarValue;    // 경찰차가 수행해야 하는 행동의 거리, 혹은 회전 값
    [SerializeField] private int[] policeCarBehaviour;  // 경찰차가 수행해야 하는 행동 번호

    private List<PolicePath> pathList = new List<PolicePath>();

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
    // 주소값 초기화
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
    public int GetAddress()
    {
        return buildingNumber;
    }

    public bool GetIsPoliceCar()
    {
        return isPoliceCar;
    }
    public List<PolicePath> GetPolicePath()
    {
        return pathList;
    }
    public void SetIsPoliceCar(bool b)
    {
        isPoliceCar = b;
    }
    public Vector2 GetpoliceCarDis()
    {
        return policeCarDis;
    }
    public Vector2 GetBuildingPos()
    {
        return buildingPos;
    }
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
