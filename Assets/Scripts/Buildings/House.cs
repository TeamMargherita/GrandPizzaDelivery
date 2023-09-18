using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingAddressNS;

// 한석호 작성

public class House : MonoBehaviour, IAddress, IHouse
{
    static private Color activeColor = new Color(248, 70, 6);

    private SpriteRenderer spriteRenderer;
    private IMap iMap;

    private int houseNumber;
    private bool isEnable = false;

	private void Awake()
	{
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
	}

	public void InitAddress(int number, List<AddressS> addressSList)
    {
        houseNumber = number % 1000;

        addressSList.Add(new AddressS(number / 1000, houseNumber, this));
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
	}

    public void DisableHouse()
	{
        spriteRenderer.color = Color.white;
        isEnable = false;
	}

}
