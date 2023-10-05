using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingAddressNS;

// 한석호 작성

public class GoalCheckCollider : MonoBehaviour, IPriorityCode
{
    private List<IPriorityCode> priorityList = new List<IPriorityCode>();

    private IDeliveryPanelControl iDeliveryPanelControl;
    public IHouse iHouse;
    public AddressS addr;
    // 목표지점에 도달시, 배달 패널을 연다.
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<IPriorityCode>() != null)
        {
            priorityList.Add(collision.gameObject.GetComponent<IPriorityCode>());
            // 우선순위를 고려하여 가장 우선순위가 높은 것을 제외한 나머지는 트리고 발동 시 해야하는 행위를 막는다.
            if (!CheckPriority())
            {
                return;
            }
        }
        //Debug.Log("작동");

        if (collision.tag.Equals("Player"))
        {
            if (iDeliveryPanelControl == null) { return; }
            iDeliveryPanelControl.SetIHouseDeliveryUI(iHouse);
            iDeliveryPanelControl.ControlDeliveryUI(true);
            GameObject.Find("InventoryManager").GetComponent<InventoryManager>().GoalAddressS = this.GetComponent<GoalCheckCollider>();
        }
    }
    private bool CheckPriority()
    {
        if (priorityList.FindIndex(a => a.GetPriorityCode() > GetPriorityCode()) != -1)
        {
            // 행위 불가
            return false;
        }
        else
        {
            // 행위 가능
            return true;
        }
    }
    public void GetAddress(AddressS addressS)
    {
        addr = addressS;
    }


    public int GetPriorityCode()
    {
        return (addr.BuildingAddress * 1000 + addr.HouseAddress);
    }

    public void SetIDeliveryPanelControl(IDeliveryPanelControl iDeliveryPanelControl, IHouse iHouse)
    {
        this.iDeliveryPanelControl = iDeliveryPanelControl;
        this.iHouse = iHouse;
    }
}
