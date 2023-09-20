using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingAddressNS;

// 한석호 작성

// 집마다 한명의 손님이 존재하며, 손님의 취향은 전부 제각각이다. 단, 한번 정해진 취향은 바뀌지 않는다.
public class House : MonoBehaviour, IAddress, IHouse
{
    // 0 = 상, 1 = 하, 2 = 좌, 3 = 우
    [SerializeField] private int direction = 0;
    [SerializeField] private GameObject goalObj;

    static private Color activeColor = new Color(248/255f, 70/255f, 6/255f);   // 활성화 색(배달해야 하는 곳임을 알림)

    private SpriteRenderer spriteRenderer;
    private Transform goalTrans;

    private IMap iMap;
    private IDeliveryPanelControl iDeliveryPanelControl;

    private AddressS houseAddress;  // 집주소

    private float spendingTime; // 배달에 소요한 시간
    private int houseNumber;    // 건물 내에서 집 번호
    private bool isEnable = false;
    
	private void Awake()
	{
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        goalTrans = goalObj.transform;

        if (direction == 0) { goalTrans.position += new Vector3(0, 1); }
        else if (direction == 1) { goalTrans.position += new Vector3(0, -1); }
        else if (direction == 2) { goalTrans.position += new Vector3(-1, 0); }
        else if (direction == 3) { goalTrans.position += new Vector3(1, 0); }
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
}
