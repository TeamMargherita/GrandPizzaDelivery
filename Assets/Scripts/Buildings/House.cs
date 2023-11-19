using BuildingAddressNS;
using BuildingNS.HouseNS;
using PizzaNS;
using PizzaNS.CustomerNS;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�

// ������ �Ѹ��� �մ��� �����ϸ�, �մ��� ������ ���� �������̴�. ��, �ѹ� ������ ������ �ٲ��� �ʴ´�.
public class House : MonoBehaviour, IAddress, IHouse, IActiveHouse
{
    // 0 = ��, 1 = ��, 2 = ��, 3 = ��
    [SerializeField] private int direction = 0;
    [SerializeField] private GameObject goalObj;
    [SerializeField] private GameObject activeObj;

    public static Color activeColor = new Color(248/255f, 70/255f, 6/255f);   // Ȱ��ȭ ��(����ؾ� �ϴ� ������ �˸�)
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

    private CustomerS customerS;    // �մ�
    private AddressS houseAddress;  // ���ּ�

    private float spendingTime; // ��޿� �ҿ��� �ð�
    private int houseNumber;    // �ǹ� ������ �� ��ȣ
    private int tip;    // ��
    private bool isEnable = false;  // �ش� ���� �ֹ��� �ؾߵǴ��� ����
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
    /// �� ����
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
    /// �ּ� �ʱ�ȭ
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
    /// ���� ��¦�̸� Ȱ��ȭ�ȴ�.
    /// Ȱ��ȭ�Ǹ� �ʿ� �ش� ���� ǥ�õǸ�, Map Ŭ�������� �ð��� ��� �����Ѵ�.
    /// </summary>
    public void EnableHouse()
	{
        spriteRenderer.color = activeColor;
        isEnable = true;
        goalObj.SetActive(true);
        iMap.AddAddress(houseAddress);
    }
    /// <summary>
    /// ���� ����� �������� ��
    /// ����� ���� �� �ɸ� �ð�, ���� ���� ����ü �������� ��� �����Ѵ�.
    /// </summary> 
    public void DisableHouse(Pizza pizza)
	{
        // ���޹��� ���ڸ� �մ��� ����� ���ؼ� ���� �󸶳� ���� ���ϰ�, ������ �󸶳� ���� ���Ѵ�.
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
        tip += (int)(pizza.Charisma * Random.Range(Constant.PizzaStoreStar, Constant.PizzaStoreStar + 1f));
        //Debug.Log(tip);
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
    /// ��� ���� ���θ� ������ �������̽� ����.
    /// </summary>
    /// <param name="iDeliveryPanelControl"></param>
    public void SetIDeliveryPanelControl(IDeliveryPanelControl iDeliveryPanelControl)
    {
        this.iDeliveryPanelControl = iDeliveryPanelControl;
        goalObj.GetComponent<GoalCheckCollider>().SetIDeliveryPanelControl(iDeliveryPanelControl, this);
    }
    /// <summary>
    /// �� ��ó���� ������ �� �ִ� ����� ������ �гο� ���� �������̽� ����
    /// </summary>
    /// <param name="iHouseActiveControl"></param>
    public void SetIHouseActiveUIControl(IHouseActiveUIControl iHouseActiveControl)
    {
        this.iHouseActiveUIControl = iHouseActiveControl;
        activeObj.GetComponent<HouseActiveCheck>().SetIHouseActiveUIControl(iHouseActiveControl);

    }
    /// <summary>
    /// Ư�� ������ �ΰ� ���δ�.
    /// </summary>
    /// <param name="mark">�ΰ� ��������Ʈ</param>
    /// <param name="houseType">�� Ÿ��</param>
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
    /// �� ��ó�� ���� �� Ÿ�Կ� ���� ������ ������ �ٸ��� ��
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
    /// �Ϲ� ���� ��쿡�� �� ��ó�� ���� ���� ������ ������ ����
    /// </summary>
    /// <param name="bo"></param>
    /// <returns></returns>
    public bool ActiveHouse(bool bo)
    {
        if (houseType != HouseType.NONE && houseType != HouseType.HOUSE)
        {
            SetInHouse(bo);
            // ���� ������ �ٲ���
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
    /// �� ��ó�� ���� ���� �� �ٲ���
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
