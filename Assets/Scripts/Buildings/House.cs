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

    static private Color activeColor = new Color(248/255f, 70/255f, 6/255f);   // 활성화 색(배달해야 하는 곳임을 알림)

    private SpriteRenderer spriteRenderer;
    private Transform goalTrans;

    private IMap iMap;
    private IDeliveryPanelControl iDeliveryPanelControl;
    private IHouseActiveUIControl iHouseActiveUIControl;

    private HouseType houseType;
    
    private CustomerS customerS;
    private AddressS houseAddress;  // 집주소

    private float spendingTime; // 배달에 소요한 시간
    private int houseNumber;    // 건물 내에서 집 번호
    private bool isEnable = false;  // 해당 집에 주문을 해야되는지 여부
    private bool inHouse = false;
	private void Awake()
	{
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        goalTrans = goalObj.transform;
        Vector3 vec = Vector3.zero;
        if (direction == 0) { vec = new Vector3(0, 1); }
        else if (direction == 1) { vec = new Vector3(0, -1); }
        else if (direction == 2) { vec = new Vector3(-1, 0); }
        else if (direction == 3) { vec = new Vector3(1, 0); }
        goalObj.transform.position += vec;
        activeObj.transform.position += vec;

        activeObj.GetComponent<HouseActiveCheck>().SetIActiveHouse(this);
        houseType = HouseType.HOUSE;
        SetCustomer();
	}
    // 고객 생성
    private void SetCustomer()
    {
        List<Ingredient> ing = new List<Ingredient>();
        int r = 0;
        while (ing.Count < 2)
        {
            r = Random.Range(0, System.Enum.GetValues(typeof(Ingredient)).Length);
            if (ing.FindIndex(a => a.Equals((Ingredient)r)) == -1 && r != 0)
            {
                ing.Add((Ingredient)r);
            }
        }
    }
	public void InitAddress(int number, List<AddressS> addressSList)
    {
        houseNumber = number % 1000;
        houseAddress = new AddressS(number / 1000, houseNumber, this);

        addressSList.Add(houseAddress);
    }
    public void SetIMap(IMap iMap)
	{
        this.iMap = iMap;
	}
    public int GetAddress()
    {
        return houseNumber;
    }

    // 집이 반짝이며 활성화된다.
    // 활성화되면 맵에 해당 집이 표시되며, Map 클래스에서 시간을 재기 시작한다.
    public void EnableHouse()
	{
        spriteRenderer.color = activeColor;
        isEnable = true;
        goalObj.SetActive(true);
        iMap.AddAddress(houseAddress);
	}

    // 피자 배달을 끝마쳤을 때
    // 배달이 끝난 후 걸린 시간, 평점 등을 구조체 형식으로 묶어서 전달한다.
    public void DisableHouse()
	{
        spriteRenderer.color = Color.white;
        isEnable = false;
        goalObj.SetActive(false);
        spendingTime = iMap.RemoveAddress(houseAddress);
	}

    public bool GetIsEnable()
	{
        return isEnable;
	}

    public void SetIDeliveryPanelControl(IDeliveryPanelControl iDeliveryPanelControl)
    {
        this.iDeliveryPanelControl = iDeliveryPanelControl;
        goalObj.GetComponent<GoalCheckCollider>().SetIDeliveryPanelControl(iDeliveryPanelControl, this);
    }
    public void SetIHouseActiveUIControl(IHouseActiveUIControl iHouseActiveControl)
    {
        this.iHouseActiveUIControl = iHouseActiveControl;
        activeObj.GetComponent<HouseActiveCheck>().SetIHouseActiveUIControl(iHouseActiveControl);

    }
    // 특정 집에는 로고를 붙인다.
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
    public bool ActiveHouse(bool bo)
    {
        if (houseType != HouseType.NONE && houseType != HouseType.HOUSE)
        {
            SetInHouse(bo);
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
    // 집 근처로 가면 집의 색 바꿔주기.
    private void ChangeColor(bool bo)
    {
        if (!isEnable)
        {
            if (bo)
            {
                spriteRenderer.color = Color.cyan;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }
    }

	public Transform GetLocation()
	{
        return this.transform;
	}
}
