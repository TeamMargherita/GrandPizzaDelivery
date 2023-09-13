using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingNS;
using PoliceNS.PolicePathNS;

// 한석호 작성

public class Building : MonoBehaviour, IAddress, IBuilding
{
    [SerializeField] private BuildingShape buildingShape;
    [SerializeField] private Vector2 compositePos;
    [SerializeField] private float[] policeCarValue;
    [SerializeField] private int[] policeCarBehaviour;

    private List<PolicePath> pathList = new List<PolicePath>();

    private Vector2 policeCarDis; // 경찰차가 스폰될 때 이 건물과 경찰차의 거리입니다.
    private Vector2 buildingPos;

    private int buildingNumber;
    private bool isPoliceCar;

    public void Awake()
    {

        buildingPos = this.gameObject.transform.position;

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
            pathList.Add(new PolicePath(policeCarBehaviour[i], policeCarValue[i]));
        }
    }

    public void InitAddress(int number)
    {
        buildingNumber = number;
        int n = 0;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).GetComponent<IAddress>() != null)
            {
                this.transform.GetChild(i).GetComponent<IAddress>().InitAddress(number * 1000 + n);
                n++;
            }
        }
        //Debug.Log($"BuildingNumber + {buildingNumber}");
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
}
