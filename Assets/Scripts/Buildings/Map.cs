using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingNS;

//맵에 존재해야할 오브젝트들을 배치하고, 건물마다 주소를 붙여줌으로써 맵을 구현합니다. 
public class Map : MonoBehaviour
{
    [SerializeField] private GameObject policeCar;

    // addressList를 통해 빌딩의 주소를 초기화하거나 받아올 수 있습니다.
    private List<IAddress> addressList = new List<IAddress>();
    private List<IBuilding> buildingList = new List<IBuilding>();
    void Awake()
    {
        // 건물에 주소를 붙여줍니다.
        int n = 0;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).GetComponent<IAddress>() != null)
            {
                GameObject ob = this.transform.GetChild(i).gameObject;
                addressList.Add(ob.GetComponent<IAddress>());
                addressList[n].InitAddress(n);
                buildingList.Add(ob.GetComponent<IBuilding>());
                n++;
            }
        }
    }

    private void Start()
    {

        MakeAPoliceCar(15);
    }
    // 경찰차를 랜덤한 건물마다 배정해주는 함수입니다.
    private void MakeAPoliceCar(int cnt)
    {
        if (cnt >= buildingList.Count) { cnt = buildingList.Count; }
        // 건물들을 기준으로 해서 경찰차를 소환합니다. 경찰차가 소환되는 건물은 랜덤입니다.
        while (cnt > 0)
        {
            // 랜덤으로 건물들 중에 하나를 고릅니다.
            int ran = Random.Range(0, buildingList.Count);
            // 해당 건물에 경찰차가 이미 배정되어 있는지 확인하고 배정되어 있지 않아야 경찰차를 배정할 수 있음을 조건문으로 표시합니다.
            if (!buildingList[ran].GetIsPoliceCar())
            {
                // 경찰차를 배정하기에 앞서 건물의 모양과 건물의 위치를 확인합니다.
                // 건물의 모양에 따라 경찰차의 위치도 달라진다.
                GameObject policeCar = Instantiate(this.policeCar);
                policeCar.transform.position = buildingList[ran].GetpoliceCarDis() + buildingList[ran].GetBuildingPos();
                // 각 경찰차에게 건물에 맞는 루트를 짜서 넘겨야한다.


                // 경찰차가 배정되었으므로 cnt를 하나 내리고, 경찰차가 배정되었음을 건물(Building)에 알립니다.
                buildingList[ran].SetIsPoliceCar(true);
                cnt--;
            }
        }
    }
}
