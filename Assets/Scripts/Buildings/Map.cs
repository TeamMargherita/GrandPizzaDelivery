using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingAddressNS;
using BuildingNS.HouseNS;

// 한석호 작성

//맵에 존재해야할 오브젝트들을 배치하고, 건물마다 주소를 붙여줌으로써 맵을 구현합니다. 
public class Map : MonoBehaviour, IMap, IStop
{
    [SerializeField] private GameObject uiControlObj;
    [SerializeField] private GameObject policeCar;
    [SerializeField] private GameObject banana;
    [SerializeField] private GameObject effectControl;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private Sprite[] houseMarkArr;

    // addressList를 통해 빌딩의 주소를 초기화하거나 받아올 수 있습니다.
    private List<IAddress> addressList = new List<IAddress>();
    // 각 건물에 경찰차를 배정하기 위한 리스트입니다.
    private List<IBuilding> buildingList = new List<IBuilding>();
    private List<IPoliceCar> policeList = new List<IPoliceCar>();

    private List<AddressS> houseAddressList = new List<AddressS>();
    private List<AddressS> temHouseAddressList = new List<AddressS>();
    private Dictionary<AddressS, float> deliveryTimeDic = new Dictionary<AddressS, float>();
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
                addressList[n].InitAddress(n, houseAddressList);
                addressList[n].SetIMap(this);
                addressList[n].SetIDeliveryPanelControl(uiControlObj.GetComponent<IDeliveryPanelControl>());
                addressList[n].SetIHouseActiveUIControl(uiControlObj.GetComponent<IHouseActiveUIControl>());
                buildingList.Add(ob.GetComponent<IBuilding>());
                n++;
            }
        }
        uiControlObj.GetComponent<UIControl>().SetIStop(this);

    }

    private void Start()
    {
        //Debug.Log(houseAddressList.Count);
        // 피자집에 마크를 붙인다.
        houseAddressList[36].IHouse.SetHouseType(houseMarkArr[0], HouseType.PIZZASTORE);
        houseAddressList[66].IHouse.SetHouseType(houseMarkArr[1], HouseType.DICESTORE);
        houseAddressList[55].IHouse.SetHouseType(houseMarkArr[2], HouseType.PINEAPPLESTORE);
        houseAddressList[22].IHouse.SetHouseType(houseMarkArr[3], HouseType.INGREDIENTSTORE);
        houseAddressList[78].IHouse.SetHouseType(houseMarkArr[2], HouseType.PINEAPPLESTORETWO);
        houseAddressList[43].IHouse.SetHouseType(houseMarkArr[4], HouseType.GUNSTORE);

        MakeAPoliceCar(45);
    }
    private void test()
    {
        List<AddressS> ad = GetRandAddressSList(5);
        for (int i = 0; i < ad.Count; i++)
        {
            ad[i].IHouse.EnableHouse();
        }
    }
    /// <summary>
    /// 경찰차를 랜덤한 건물마다 배정해주는 함수입니다.
    /// </summary>
    /// <param name="cnt"></param>
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

                if (policeCar.GetComponent<IPoliceCar>() != null)
                {
                    policeList.Add(policeCar.GetComponent<IPoliceCar>());
                    policeCar.GetComponent<IPoliceCar>().SetPlayerMove(playerMove);
                    //policeCar.GetComponent<IPoliceCar>().SetPoliceSmokeEffect(effectControl.GetComponent<ISetTransform>());
                    policeCar.GetComponent<IPoliceCar>().SetMap(this);
                    policeCar.GetComponent<IPoliceCar>().SetBanana(banana);
                    // 각 경찰차에게 건물에 맞는 루트를 짜서 넘겨야한다.
                    if (buildingList[ran].GetPolicePath().Count != 0)
                    {
                        policeCar.GetComponent<IPoliceCar>().InitPoliceCarPath(buildingList[ran].GetPolicePath());
                    }
                    policeCar.GetComponent<IPoliceCar>().SetIInspectingPanelControl(uiControlObj.GetComponent<IConversationPanelControl>());
                }
                if (policeCar.GetComponent<Police>() != null)
				{
                    policeCar.GetComponent<Police>().SetSmokeEffectTrans(effectControl.GetComponent<ISetTransform>());
				}
                // 경찰차가 배정되었으므로 cnt를 하나 내리고, 경찰차가 배정되었음을 건물(Building)에 알립니다.
                buildingList[ran].SetIsPoliceCar(true);
                cnt--;
            }
        }
    }
    public void AddAddress(AddressS addressS)
    {
        deliveryTimeDic.Add(addressS, GameManager.Instance.time);
    }
    /// <summary>
    /// 배달이 끝나 해당 주소에서의 시간재기를 끝내고 소요한 배달시간을 반환합니다.
    /// </summary>
    /// <param name="addressS"></param>
    /// <returns></returns>
    public float RemoveAddress(AddressS addressS)
    {
        foreach (var addr in deliveryTimeDic.Keys)
        {
            if (addr.BuildingAddress == addressS.BuildingAddress 
                && addr.HouseAddress == addressS.HouseAddress
                && addr.IHouse == addressS.IHouse)
            {
                float f = GameManager.Instance.time - deliveryTimeDic[addr]; 
                deliveryTimeDic.Remove(addr);
                return f;
            }
        }
        return -1f;
    }
	/// <summary>
	/// 랜덤한 집주소 여러 개를  알려준다.
	/// </summary>
	/// <param name="n">집주소들의 개수이다.</param>
	/// <returns>반환형식은 List<AddressS> 형식이다. </returns>
	public List<AddressS> GetRandAddressSList(int n)
	{
        if (houseAddressList.Count == 0) { return null; }

        temHouseAddressList.Clear();
        for (int i = 0; i < houseAddressList.Count; i++)
		{
            temHouseAddressList.Add(houseAddressList[i]);
		}

        List<AddressS> list = new List<AddressS>();
        int r = 0;
        for (int i = 0; i < n; i++)
		{
            while (true)
            {
                r = Random.Range(0, temHouseAddressList.Count);
                if (temHouseAddressList[r].IHouse.GetHouseType() == HouseType.HOUSE)
                {
                    break;
                }
            }
            list.Add(temHouseAddressList[r]);
            temHouseAddressList.RemoveAt(r);
		}

        return list;
	}
    /// <summary>
    /// 랜덤한 집 주소 1개를 알려준다. 주소는 비활성화된 것들중에서 고른다.
    /// </summary>
    /// <returns>반환 형식은 AddressS이다.</returns>
    public AddressS GetRandAddressS()
	{
        int r = 0;
        while (true)
        {
            r = Random.Range(0, houseAddressList.Count);

            if (!houseAddressList[r].IHouse.GetIsEnable() && houseAddressList[r].IHouse.GetHouseType() == HouseType.HOUSE)
            {
                Debug.Log(houseAddressList[r].IHouse.GetLocation());
                return houseAddressList[r];
            }
        }

    }
    /// <summary>
    /// 맵에 경찰차들과 플레이어를 정지시킨다.
    /// </summary>
    /// <param name="bo"></param>
    public void StopMap(bool bo) 
    {
        int n = 0;

        if (bo)
        {
            while(true)
            {
                n = policeList.Count;
                for (int i = 0; i < n; i++)
                {
                    if (n != policeList.Count) { break; }
                    policeList[i].SetIsStop(true);
                }
                break;
            }
            playerMove.Stop = true;
        }
        else
        {
            while (true)
            {
                n = policeList.Count;
                for (int i = 0; i < n; i++)
                {
                    if (n != policeList.Count) { break; }
                    policeList[i].SetIsStop(false);
                }
                break;
            }
            playerMove.Stop = false;
        }
    }

    public void RemovePoliceList(IPoliceCar iPoliceCar)
    {
        policeList.Remove(iPoliceCar);
    }
}
