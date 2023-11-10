using BuildingAddressNS;
using BuildingNS.HouseNS;
using PizzaNS;
using PizzaNS.CustomerNS;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성

// 집마다 한명의 손님이 존재하며, 손님의 취향은 전부 제각각이다. 단, 한번 정해진 취향은 바뀌지 않는다.
public class House : MonoBehaviour, IAddress, IHouse, IActiveHouse
{
    // 0 = 상, 1 = 하, 2 = 좌, 3 = 우
    [SerializeField] private int direction = 0;
    [SerializeField] private GameObject goalObj;
    [SerializeField] private GameObject activeObj;

    public static Color activeColor = new Color(248/255f, 70/255f, 6/255f);   // 활성화 색(배달해야 하는 곳임을 알림)
    public static Dictionary<int, Dictionary<int, CustomerS>> CustomerSDic = new Dictionary<int, Dictionary<int, CustomerS>>();
    public static Dictionary<int, Dictionary<int, int>> nowDate = new Dictionary<int, Dictionary<int, int>>();

    private SpriteRenderer spriteRenderer;
    private Transform goalTrans;

    private IMap iMap;
    private IDeliveryPanelControl iDeliveryPanelControl;
    private IHouseActiveUIControl iHouseActiveUIControl;

    private List<Ingredient> temIng;
    private HouseType houseType;

    private Color houseColor;

    private CustomerS customerS;    // 손님
    private AddressS houseAddress;  // 집주소

    private float spendingTime; // 배달에 소요한 시간
    private int houseNumber;    // 건물 내에서 집 번호
    private int tip;    // 팁
    private bool isEnable = false;  // 해당 집에 주문을 해야되는지 여부
    private bool inHouse = false;
    
	private void Awake()
	{
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        houseColor = Color.HSVToRGB(Random.Range(0, 361f) / 360f, 40 / 100f, 100 / 100f);
        spriteRenderer.color = houseColor;
        goalTrans = goalObj.transform;
        Vector3 vec = Vector3.zero;
        if (direction == 0) { vec = new Vector3(0, 1); }
        else if (direction == 1) { vec = new Vector3(0, -1); }
        else if (direction == 2) { vec = new Vector3(-1, 0); }
        else if (direction == 3) { vec = new Vector3(1, 0); }
        goalObj.transform.position += vec;
        activeObj.transform.position += vec;

        if (Constant.NowDate == 1 && GameManager.Instance.time >= 32400 && GameManager.Instance.time <= 32500)
        {
            activeColor = new Color(248 / 255f, 70 / 255f, 6 / 255f);
        }

        //customerS = new CustomerS(Random.Range(1, 101), Random.Range(60, 240), Random.Range(200, 2000));
        activeObj.GetComponent<HouseActiveCheck>().SetIActiveHouse(this);
        houseType = HouseType.HOUSE;
	}
    /// <summary>
    /// 고객 생성
    /// </summary>
    private void SetCustomer()
    {
        temIng = new List<Ingredient>();
        int r = 0;
        while (temIng.Count < 5)
        {
            r = Random.Range(0, System.Enum.GetValues(typeof(Ingredient)).Length);
            if (temIng.FindIndex(a => a.Equals((Ingredient)r)) == -1 && r != 0)
            {
                temIng.Add((Ingredient)r);
            }
        }

        int pizzaCutline = Random.Range(0, 101);
        float pizzaSpeed = Random.Range(30f, 120f);
        int pizzaCarismaCutline = Random.Range(200, 2000);
        int moneyPower = (int)(Random.Range(1, 10f) * Mathf.Pow(10, Constant.PizzaStoreStar));

        if (nowDate[houseAddress.BuildingAddress][houseAddress.HouseAddress] != Constant.NowDate)
        {
            nowDate[houseAddress.BuildingAddress][houseAddress.HouseAddress] = Constant.NowDate;
            customerS = new CustomerS(pizzaCutline, pizzaSpeed, pizzaCarismaCutline, temIng, moneyPower);
            if (!CustomerSDic.ContainsKey(houseAddress.BuildingAddress))
            {
                CustomerSDic.Add(houseAddress.BuildingAddress, new Dictionary<int, CustomerS>() { { houseAddress.HouseAddress, customerS } });

            }
            else
            {
                if (!CustomerSDic[houseAddress.BuildingAddress].ContainsKey(houseAddress.HouseAddress))
                {
                    CustomerSDic[houseAddress.BuildingAddress].Add(houseAddress.HouseAddress, customerS);
                }
                else
				{
                    CustomerSDic[houseAddress.BuildingAddress][houseAddress.HouseAddress] = customerS;
				}
            }
        }
    }
    /// <summary>
    /// 주소 초기화
    /// </summary>
    /// <param name="number"></param>
    /// <param name="addressSList"></param>
	public void InitAddress(int number, List<AddressS> addressSList)
    {
        houseNumber = number % 1000;
        houseAddress = new AddressS(number / 1000, houseNumber, this);
        goalObj.GetComponent<GoalCheckCollider>().addr = houseAddress;

        addressSList.Add(houseAddress);
        if (!nowDate.ContainsKey(houseAddress.BuildingAddress))
        {
           nowDate.Add(houseAddress.BuildingAddress, new Dictionary<int, int>() { { houseAddress.HouseAddress, 0 } });

        }
        else
        {
            if (!nowDate[houseAddress.BuildingAddress].ContainsKey(houseAddress.HouseAddress))
            {
                nowDate[houseAddress.BuildingAddress].Add(houseAddress.HouseAddress, 0);
            }
        }

        SetCustomer();

    }
    public void SetIMap(IMap iMap)
	{
        this.iMap = iMap;
	}
    public int GetAddress()
    {
        return houseNumber;
    }
    /// <summary>
    /// 집이 반짝이며 활성화된다.
    /// 활성화되면 맵에 해당 집이 표시되며, Map 클래스에서 시간을 재기 시작한다.
    /// </summary>
    public void EnableHouse()
	{
        spriteRenderer.color = activeColor;
        isEnable = true;
        goalObj.SetActive(true);
        iMap.AddAddress(houseAddress);
    }
    /// <summary>
    /// 피자 배달을 끝마쳤을 때
    /// 배달이 끝난 후 걸린 시간, 평점 등을 구조체 형식으로 묶어서 전달한다.
    /// </summary> 
    public void DisableHouse(Pizza pizza)
	{
        // 전달받은 피자를 손님의 취향과 비교해서 팁을 얼마나 줄지 정하고, 평점을 얼마나 줄지 정한다.
        AddTip(pizza);
        spriteRenderer.color = houseColor;
        isEnable = false;
        goalObj.SetActive(false);
        spendingTime = iMap.RemoveAddress(houseAddress);
	}

    private void AddTip(Pizza pizza)
    {
        tip = 0;
        int percent = 0;
        if (CustomerSDic[houseAddress.BuildingAddress][houseAddress.HouseAddress].PizzaCutLine > pizza.Perfection)
        {
            percent += 15;
        }
        if (CustomerSDic[houseAddress.BuildingAddress][houseAddress.HouseAddress].PizzaDeliverySpeed > spendingTime)
        {
            percent += 15;
        }
        if (CustomerSDic[houseAddress.BuildingAddress][houseAddress.HouseAddress].PizzaCarismaCutLine > pizza.Charisma)
        {
            percent += 20;
        }
        for (int i = 0; i < CustomerSDic[houseAddress.BuildingAddress][houseAddress.HouseAddress].IngList.Count; i++)
        {
            if (pizza.Ingreds.FindIndex(a => a.Equals(CustomerSDic[houseAddress.BuildingAddress][houseAddress.HouseAddress].IngList[i])) != -1)
            {
                percent += 10;
            }
        }

        percent = (int)(percent * (pizza.Freshness * 0.01f));

        tip = (int)(CustomerSDic[houseAddress.BuildingAddress][houseAddress.HouseAddress].MoneyPower * (percent * 0.01f));
        Debug.Log(tip);
        Invoke("PlusMoney", 1.5f);
        if (tip < 40)
        {
            Constant.PizzaStoreStar += -0.001f * tip;
        }
        else
        {
            Constant.PizzaStoreStar += 0.001f * (tip - 40);
        }
    }
    private void PlusMoney()
    {
        Debug.Log("abc");
        GameManager.Instance.Money += tip;
    }

    public void EndDeliveryDisableHouse()
    {
        spriteRenderer.color = houseColor;
        isEnable = false;
        goalObj.SetActive(false);
        spendingTime = iMap.RemoveAddress(houseAddress);
    }
    public bool GetIsEnable()
	{
        return isEnable;
	}
    /// <summary>
    /// 배달 도착 여부를 따지는 인터페이스 전달.
    /// </summary>
    /// <param name="iDeliveryPanelControl"></param>
    public void SetIDeliveryPanelControl(IDeliveryPanelControl iDeliveryPanelControl)
    {
        this.iDeliveryPanelControl = iDeliveryPanelControl;
        goalObj.GetComponent<GoalCheckCollider>().SetIDeliveryPanelControl(iDeliveryPanelControl, this);
    }
    /// <summary>
    /// 집 근처에서 조작할 수 있는 방식을 설명한 패널에 관한 인터페이스 전달
    /// </summary>
    /// <param name="iHouseActiveControl"></param>
    public void SetIHouseActiveUIControl(IHouseActiveUIControl iHouseActiveControl)
    {
        this.iHouseActiveUIControl = iHouseActiveControl;
        activeObj.GetComponent<HouseActiveCheck>().SetIHouseActiveUIControl(iHouseActiveControl);

    }
    /// <summary>
    /// 특정 집에는 로고를 붙인다.
    /// </summary>
    /// <param name="mark">로고 스프라이트</param>
    /// <param name="houseType">집 타입</param>
    public void SetHouseType(Sprite mark, HouseType houseType)
    {
        GameObject obj = new GameObject();
        obj.transform.parent = this.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.AddComponent<SpriteRenderer>();
        obj.GetComponent<SpriteRenderer>().sprite = mark;
        obj.GetComponent<SpriteRenderer>().sortingOrder = 200;
        this.houseType = houseType;
    }
    /// <summary>
    /// 집 근처에 왔을 때 타입에 따라 가능한 조작을 다르게 함
    /// </summary>
    /// <param name="bo"></param>
    public void IntoHouse(bool bo)
    {
        if (bo)
        {
            iHouseActiveUIControl.SetHouseType(houseType);
        }
        else
        {
            iHouseActiveUIControl.SetHouseType(HouseType.NONE);
        }
    }
    public HouseType GetHouseType()
    {
        return houseType;
    }
    /// <summary>
    /// 일반 집인 경우에는 집 근처로 가도 집의 색상이 변하지 않음
    /// </summary>
    /// <param name="bo"></param>
    /// <returns></returns>
    public bool ActiveHouse(bool bo)
    {
        if (houseType != HouseType.NONE && houseType != HouseType.HOUSE)
        {
            SetInHouse(bo);
            // 집의 색상을 바꿔줌
            ChangeColor(bo);
            return true;
        }
        else
        {
            return false;
        }
    }
    private  void SetInHouse(bool bo)
    {
        inHouse = bo;
    }
    /// <summary>
    /// 집 근처로 가면 집의 색 바꿔줌
    /// </summary>
    /// <param name="bo"></param>
    private void ChangeColor(bool bo)
    {
        if (!isEnable)
        {
            if (bo)
            {
                spriteRenderer.color = Color.red;
            }
            else
            {
                spriteRenderer.color = houseColor;
            }
        }
    }

	public Transform GetLocation()
	{
        return this.transform;
	}
}
